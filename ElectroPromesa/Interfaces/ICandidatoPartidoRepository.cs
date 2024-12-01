using ElectroPromesa.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ElectroPromesa.Interfaces
{
    public interface ICandidatoPartidoRepository
    {
        public Task<Candidato> GetCandidatoByIdAsync(int id);
        public Task<IEnumerable<SelectListItem>> GetAllPartidos();
        public Task<CandidatoPartido> GetById(int id);
        public Task<CandidatoPartido> ExistsCandidatoPartido(CandidatoPartido candidatoPartido);
        public Task<bool> EndFechaFinPartido(int candidatoId);
        public bool Create(CandidatoPartido candidatoPartido);
        public bool Update(CandidatoPartido candidatoPartido);
        public bool Save();
    }
}
