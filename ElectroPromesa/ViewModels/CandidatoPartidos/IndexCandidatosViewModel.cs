using ElectroPromesa.Models;
using System.ComponentModel.DataAnnotations;

namespace ElectroPromesa.ViewModels.CandidatoPartidos
{
    public class IndexCandidatosViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string FotoUrl { get; set; }
        public ICollection<CandidatoPartido> Partidos { get; set; }
    }
}
