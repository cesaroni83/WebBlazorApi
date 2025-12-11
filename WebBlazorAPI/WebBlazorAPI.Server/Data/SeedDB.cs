namespace WebBlazorAPI.Server.Data
{
    public class SeedDB
    {
        private readonly AppDbContext _context;
        public SeedDB(AppDbContext context)
        {
            _context = context;
            
        }
        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
        }
    }
}
