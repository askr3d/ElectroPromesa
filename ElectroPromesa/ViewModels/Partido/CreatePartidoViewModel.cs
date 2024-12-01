using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ElectroPromesa.ViewModels.Partido
{
    public class CreatePartidoViewModel
    {
        [Display(Name = "Nombre del partido político")]
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; }
        [Display(Name = "Abreviatura del partido")]
        [Required(ErrorMessage = "La abreviatura es obligatoria")]
        public string Abreviatura { get; set; }
        public IFormFile FotoUrl { get; set; }
    }
}
