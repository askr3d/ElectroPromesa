using ElectroPromesa.Data;
using ElectroPromesa.Interfaces;
using ElectroPromesa.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ElectroPromesa.Repository
{
    public class CandidatoPartidoRepository : ICandidatoPartidoRepository
    {
        private readonly ApplicationDbContext _context;

        public CandidatoPartidoRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Candidato> GetCandidatoByIdAsync(int id)
        {
            return await _context.Candidatos
                        .Include(c => c.Partidos)
                        .ThenInclude(canpar => canpar.Partido)
                        .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<SelectListItem>> GetAllPartidos()
        {
            return await _context.Partidos.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.Nombre
            }).ToListAsync();
        }

        public async Task<CandidatoPartido> GetById(int id)
        {
            return await _context.CandidatoPartidos.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<CandidatoPartido> ExistsCandidatoPartido(CandidatoPartido candidatoPartido)
        {
            var candidatoPartidoTmp = await _context.CandidatoPartidos
                                        .AsNoTracking()
                                        .Where(canpar => 
                                                canpar.PartidoId == candidatoPartido.PartidoId &&
                                                canpar.CandidatoId == candidatoPartido.CandidatoId)
                                        .FirstOrDefaultAsync();
            return candidatoPartidoTmp;
        }

        public async Task<bool> EndFechaFinPartido(int candidatoId)
        {
            var candidatoPartido = await _context.CandidatoPartidos
                                .AsNoTracking()
                                .Where(canpar => canpar.CandidatoId == candidatoId)
                                .Where(canpar => canpar.FechaInicio > canpar.FechaFin)
                                .FirstOrDefaultAsync();
            if (candidatoPartido == null)
                return true;
            candidatoPartido.FechaFin = DateOnly.FromDateTime(DateTime.Now);

            _context.Update(candidatoPartido);

            return Save();
        }

        public bool Create(CandidatoPartido candidatoPartido)
        {
            _context.CandidatoPartidos.Add(candidatoPartido);
            return Save();
        }

        public bool Update(CandidatoPartido candidatoPartido)
        {
            _context.CandidatoPartidos.Update(candidatoPartido);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}
