using ElectroPromesa.Data;
using ElectroPromesa.Interfaces;
using ElectroPromesa.Models;
using Microsoft.EntityFrameworkCore;

namespace ElectroPromesa.Repository
{
    public class PartidoRepository : IPartidoRepository
    {
        private readonly ApplicationDbContext _context;

        public PartidoRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(Partido partido)
        {
            _context.Add(partido);
            return Save();
        }

        public bool Delete(Partido partido)
        {
            _context.Remove(partido);
            return Save();
        }

        public async Task<IEnumerable<Partido>> GetAll()
        {
            return await _context.Partidos.ToListAsync();
        }

        public async Task<Partido> GetByIdAsync(int id)
        {
            return await _context.Partidos.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Partido> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Partidos.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool Update(Partido partido)
        {
            _context.Update(partido);
            return Save();
        }
    }
}
