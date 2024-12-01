namespace ElectroPromesa.Models
{
    public class Partido
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Abreviatura { get; set; }
        public string FotoUrl { get; set; }
        public ICollection<CandidatoPartido> Candidatos { get; set; }
    }
}
