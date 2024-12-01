using System.ComponentModel.DataAnnotations;

namespace ElectroPromesa.ViewModels.Candidato
{
    public class EditCandidatoViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre del candidato es obligatorio")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Los apellidos del candidato son obligatorios")]
        public string Apellidos { get; set; }
        public string FotoSrc { get; set; }
        public IFormFile FotoUrl { get; set; }
    }
}
