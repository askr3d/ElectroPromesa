using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectroPromesa.Models
{
    public class CandidatoPartido
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Candidato")]
        public int CandidatoId { get; set; }
        public Candidato Candidato { get; set; }
        [ForeignKey("Partido")]
        public int PartidoId { get; set; }
        public Partido Partido { get; set; }
        public DateOnly FechaInicio { get; set; }
        public DateOnly FechaFin { get; set; }
    }
}
