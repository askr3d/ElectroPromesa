namespace ElectroPromesa.Interfaces
{
    public interface IPhotoService
    {
        Task<string> AddPhotoAsync(IFormFile file, string carpeta);
        Task<bool> DeletePhotoAsync(string fileName);
    }
}
