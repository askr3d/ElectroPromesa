using ElectroPromesa.Models;

namespace ElectroPromesa.Interfaces
{
    public interface ICandidatoRepository
    {
        Task<IEnumerable<Candidato>> GetAll();
        Task<Candidato> GetByIdAsync(int id);
        Task<Candidato> GetByIdAsyncNoTracking(int id);
        bool Add(Candidato candidato);
        bool Update(Candidato candidato);
        bool Delete(Candidato candidato);
        bool Save();
    }
}
