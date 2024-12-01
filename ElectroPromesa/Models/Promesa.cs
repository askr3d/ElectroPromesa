using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectroPromesa.Models
{
    public class Promesa
    {
        [Key]
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateOnly? FechaCumplio { get; set; }
        [NotMapped]
        public bool Cumplio
        {
            get => FechaCumplio.HasValue;
        }
        [ForeignKey("CandidatoPartido")]
        public int CandidatoPartidoId { get; set; }
        public CandidatoPartido CandidatoPartido { get; set; }
    }
}
