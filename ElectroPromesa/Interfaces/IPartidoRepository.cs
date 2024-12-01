using ElectroPromesa.Models;

namespace ElectroPromesa.Interfaces
{
    public interface IPartidoRepository
    {
        Task<IEnumerable<Partido>> GetAll();
        Task<Partido> GetByIdAsync(int id);
        Task<Partido> GetByIdAsyncNoTracking(int id);
        bool Add(Partido partido);
        bool Update(Partido partido);
        bool Delete(Partido partido);
        bool Save();
    }
}
