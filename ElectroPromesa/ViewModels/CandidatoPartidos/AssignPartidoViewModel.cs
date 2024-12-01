
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ElectroPromesa.ViewModels.CandidatoPartidos
{
    public class AssignPartidoViewModel
    {
        [Required]
        public int CandidatoId { get; set; }
        [Required(ErrorMessage = "Por favor seleccione un partido político")]
        [DisplayName("Partido Político")]
        public int PartidoId { get; set; }
        public IEnumerable<SelectListItem> Partidos { get; set; }
    }
}
