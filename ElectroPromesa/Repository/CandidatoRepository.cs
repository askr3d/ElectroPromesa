using ElectroPromesa.Data;
using ElectroPromesa.Interfaces;
using ElectroPromesa.Models;
using Microsoft.EntityFrameworkCore;

namespace ElectroPromesa.Repository
{
    public class CandidatoRepository : ICandidatoRepository
    {
        private readonly ApplicationDbContext _context;

        public CandidatoRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(Candidato candidato)
        {
            _context.Add(candidato);
            return Save();
        }

        public bool Delete(Candidato candidato)
        {
            _context.Remove(candidato);
            return Save();
        }

        public async Task<IEnumerable<Candidato>> GetAll()
        {
            return await _context.Candidatos
                        .Include(c => c.Partidos)
                        .ThenInclude(canpar => canpar.Partido)
                        .ToListAsync();
        }

        public async Task<Candidato> GetByIdAsync(int id)
        {
            return await _context.Candidatos.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Candidato> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Candidatos.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0;
        }

        public bool Update(Candidato candidato)
        {
            _context.Update(candidato);
            return Save();
        }
    }
}
