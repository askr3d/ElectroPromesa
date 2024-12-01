using System.ComponentModel.DataAnnotations;

namespace ElectroPromesa.Models
{
    public class Candidato
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string FotoUrl { get; set; }
        public ICollection<CandidatoPartido> Partidos { get; set; }
    }
}
