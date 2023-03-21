using System.ComponentModel.DataAnnotations;

namespace RestApplication.Models.AppUser
{
    public class AccesToken
    {
        [Key]
        [Required]
        public Guid id { get; set; }

        [Required]
        public string accesToken { get; set; }

        [Required]
        public string refreshToken { get; set; }

        [Required]
        public DateTime expirationDate { get; set; }
    }
}
