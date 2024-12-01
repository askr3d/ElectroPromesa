using ElectroPromesa.Interfaces;

namespace ElectroPromesa.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PhotoService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;

        }
        public async Task<string> AddPhotoAsync(IFormFile file, string carpeta)
        {
            // Crear la carpeta si no existe
            if (!Directory.Exists(_webHostEnvironment.WebRootPath + "/" + carpeta))
            {
                Directory.CreateDirectory(_webHostEnvironment.WebRootPath + "/" + carpeta);
            }

            if (file.Length > 0)
            {
                string fileName = carpeta + "/" + Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string filePath = Path.Combine(_webHostEnvironment.WebRootPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return fileName;
            }
            return null;
        }

        public Task<bool> DeletePhotoAsync(string fileName)
        {
            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }
    }
}
