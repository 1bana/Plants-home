namespace PLANETS_HOME.Models
{
    public class Plant
    {
        public int id{  get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string name{ get; set; }
        public string? category{ get; set; }
        public string? light{ get; set; }
        public string? water{ get; set; }
        public string? humidity { get; set; }
        public string? description{ get; set; }
        public string? caretips { get; set; }


    }
}
