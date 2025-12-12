namespace WebBlazorAPI.Server.Helper.Implementacion
{
    public class FileStorage : IFileStorage
    {
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FileStorage(IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
        {
            _env = env;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Guarda una imagen en base64 en la carpeta especificada dentro de wwwroot
        /// </summary>
        /// <param name="base64Image">Imagen en Base64</param>
        /// <param name="folderName">Carpeta donde guardar la imagen (por defecto "UserImagen")</param>
        /// <returns>URL pública de la imagen</returns>
        public async Task<string> SaveImageAsync(string base64Image, string folderName = "UserImagen")
        {
            try
            {
                if (base64Image.Contains(","))
                    base64Image = base64Image[(base64Image.IndexOf(",") + 1)..];

                byte[] imageBytes = Convert.FromBase64String(base64Image);

                string webRootPath = _env.WebRootPath ?? Path.Combine(_env.ContentRootPath, "wwwroot");
                if (!Directory.Exists(webRootPath))
                    Directory.CreateDirectory(webRootPath);

                // Carpeta dinámica según el parámetro folderName
                string uploadsPath = Path.Combine(webRootPath, folderName);
                if (!Directory.Exists(uploadsPath))
                    Directory.CreateDirectory(uploadsPath);

                string fileName = $"{Guid.NewGuid()}.jpg";
                string filePath = Path.Combine(uploadsPath, fileName);

                await File.WriteAllBytesAsync(filePath, imageBytes);

                var request = _httpContextAccessor.HttpContext?.Request;
                if (request == null)
                    throw new InvalidOperationException("No se pudo obtener el contexto HTTP para construir la URL.");

                string baseUrl = $"{request.Scheme}://{request.Host}";
                string publicUrl = $"{baseUrl}/{folderName}/{fileName}";

                return publicUrl;
            }
            catch (FormatException)
            {
                throw new ArgumentException("El formato Base64 no es válido.");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al guardar la imagen: {ex.Message}");
            }
        }

        public async Task<string> UpdateImageAsync(string oldImageUrl, string newBase64Image, string folderName = "UserImagen")
        {
            await DeleteImageAsync(oldImageUrl, folderName);
            return await SaveImageAsync(newBase64Image, folderName);
        }

        public Task<bool> DeleteImageAsync(string imageUrl, string folderName = "UserImagen")
        {
            try
            {
                string fileName = Path.GetFileName(imageUrl);
                string filePath = Path.Combine(_env.WebRootPath, folderName, fileName);

                if (File.Exists(filePath))
                    File.Delete(filePath);

                return Task.FromResult(true);
            }
            catch
            {
                return Task.FromResult(false);
            }
        }

        public async Task<string> SaveImageFromUrlAsync(string imageUrl, string folderName = "UserImagen")
        {
            if (string.IsNullOrEmpty(imageUrl))
                return null;

            using var httpClient = new HttpClient();
            var imageBytes = await httpClient.GetByteArrayAsync(imageUrl);
            string base64Image = Convert.ToBase64String(imageBytes);

            return await SaveImageAsync(base64Image, folderName);
        }

    }
}
