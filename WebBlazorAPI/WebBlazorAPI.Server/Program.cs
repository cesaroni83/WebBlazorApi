using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using WebBlazorAPI.Server.AutoMaper;
using WebBlazorAPI.Server.Data;
using WebBlazorAPI.Server.GoogleService;
using WebBlazorAPI.Server.Helper;
using WebBlazorAPI.Server.Helper.Implementacion;
using WebBlazorAPI.Server.RepositorioGeneral;
using WebBlazorAPI.Server.Servicios;
using WebBlazorAPI.Server.Servicios.Implementacion;
using WebBlazorAPI.Shared.Enums;
using WebBlazorAPI.Shared.Google;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sales API", Version = "v1" });

    // JWT
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Enter 'Bearer {token}'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new List<string>()
        }
    });
});

builder.Services.AddControllers()
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
//-------------------- Database --------------------//
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CadenaSql")));
builder.Services.AddTransient<SeedDB>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddScoped<IUserHelper, UserHelper>();
builder.Services.AddScoped<IFileStorage, FileStorage>();
builder.Services.AddScoped<IMailHelper, MailHelper>();
builder.Services.AddScoped<IGoogleAuth, GoogleAuth>();
builder.Services.AddScoped<IGoogleAuthorization, GoogleAuthorization>();
builder.Services.AddScoped<IApiService, ApiService>();
builder.Services.AddTransient(typeof(IGenericoModelo<>), typeof(GenericoModelo<>));
builder.Services.AddScoped<IPersonas, Personas>();



//-------------------- Identity --------------------//
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Password.RequireDigit = false;
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 7;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();
////*************************
// Servicios de Razor Pages / Blazor
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor(); // ?? esto registra SignalR automáticamente

///*****************
//-------------------- Authentication --------------------//
builder.Services.AddAuthentication(options =>
{
    // Flujo principal de login: cookies + Google/Facebook
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme; // challenge por defecto
})
.AddCookie(options =>
{
    options.Cookie.SameSite = SameSiteMode.Lax;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
})
.AddJwtBearer(options =>
{
    options.Events = new JwtBearerEvents
    {
        OnTokenValidated = context =>
        {
            Console.WriteLine("Token Validated: " + context.Principal?.Identity?.Name);
            return Task.CompletedTask;
        },
        OnAuthenticationFailed = context =>
        {
            Console.WriteLine("Authentication Failed: " + context.Exception.Message);
            return Task.CompletedTask;
        }
    };

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["jwtKey"]!)),
        ClockSkew = TimeSpan.Zero
    };
})

//****//
.AddGoogle(options =>
{
    options.ClientId = builder.Configuration["Google:ClientId"]!;
    options.ClientSecret = builder.Configuration["Google:ClientSecret"]!;
    options.CallbackPath = "/signin-google"; // Debe coincidir con Redirect URI en Google Cloud
    options.SaveTokens = true;
})

//****//
.AddFacebook(options =>
{
    options.AppId = builder.Configuration["Authentication:Facebook:AppId"]!;
    options.AppSecret = builder.Configuration["Authentication:Facebook:AppSecret"]!;
    options.CallbackPath = "/signin-facebook"; // Debe coincidir con Redirect URI en Facebook
    options.SaveTokens = true;

    options.Fields.Add("name");     // Nombre completo
    options.Fields.Add("email");    // Email
    options.Fields.Add("picture");  // Foto de perfil
    // Pedir permisos correctos
    options.Scope.Add("email");
    options.Scope.Add("public_profile");

    options.Events = new Microsoft.AspNetCore.Authentication.OAuth.OAuthEvents
    {
        OnCreatingTicket = async context =>
        {
            // ?? Obtener el access token
            var accessToken = context.AccessToken;

            // ?? Llamar al Graph API de Facebook para obtener la imagen de perfil
            var request = new HttpRequestMessage(HttpMethod.Get,
                $"https://graph.facebook.com/me?fields=id,name,email,picture.width(200).height(200)&access_token={accessToken}");

            var response = await context.Backchannel.SendAsync(request);
            response.EnsureSuccessStatusCode();

            using var user = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
            var root = user.RootElement;

            // ?? Extraer la URL de la imagen
            var pictureUrl = root.GetProperty("picture").GetProperty("data").GetProperty("url").GetString();

            // ? Agregar el claim manualmente
            context.Identity.AddClaim(new Claim("urn:facebook:picture", pictureUrl!));
        },




        OnRemoteFailure = context =>
        {
            context.Response.Redirect("/Login");
            context.HandleResponse();
            return Task.CompletedTask;
        }
    };
})
//-------------------- Handler personalizado para tokens Google --------------------//
.AddScheme<AuthenticationSchemeOptions, GoogleAccessTokenAuthenticationHandler>(
    Constant.Scheme, "GoogleAccessToken", null);



builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazor",
        policy =>
        {
            policy.WithOrigins("https://localhost:7063")

                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  //.SetIsOriginAllowed(origin => true)
                  .AllowCredentials(); // necesario si usas cookies o JWT
        });
});
var app = builder.Build();
////-------------------- Seed Database --------------------//
SeedData(app);

void SeedData(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var service = scope.ServiceProvider.GetRequiredService<SeedDB>();
    service.SeedAsync().Wait();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowBlazor");
app.UseAuthentication(); // Debe ir antes de Authorization
app.UseAuthorization();

//-------------------- Static Files --------------------//
string webRootPath = Path.Combine(builder.Environment.ContentRootPath, "wwwroot");

// Carpeta para UserImagen
string userImagePath = Path.Combine(webRootPath, "UserImagen");
if (!Directory.Exists(userImagePath))
    Directory.CreateDirectory(userImagePath);

// Carpeta para Productos/Imagenes
string productImagePath = Path.Combine(webRootPath, "Productos", "Imagenes");
if (!Directory.Exists(productImagePath))
    Directory.CreateDirectory(productImagePath);

// Servir UserImagen
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(userImagePath),
    RequestPath = "/UserImagen"
});

// Servir Productos/Imagenes
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(productImagePath),
    RequestPath = "/Productos/Imagenes"
});

app.MapControllers();
app.MapBlazorHub();               // 🔹 necesario para Blazor Server
app.MapFallbackToFile("index.html");// página de fallback
app.Run();
