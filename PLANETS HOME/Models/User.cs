using System.ComponentModel.DataAnnotations;

namespace PLANETS_HOME.Models
{
    public class User
    {
        public int id { get; set; }
        [Required]
        public string username { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
        public string role { get; set; } = "user";
        public DateTime created { get; set; } = DateTime.Now;

    }
}
