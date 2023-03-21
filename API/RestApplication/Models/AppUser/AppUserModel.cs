using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RestApplication.Models.AppUser
{
    public class AppUserModel
    {
        public AppUserModel()
        {
            this.role = "Basic";
            this.isVerified = false;
        }

        [Key]
        [Required]
        public Guid id { get; set; }

        [Required]
        public string firstName { get; set; }

        [Required]
        public string lastName { get; set; }

        [Required]
        public string email { get; set; }

        [Required]
        public string password { get; set; }

        [Required]
        public bool isVerified { get; set; }

        public string role { get; set; }
    }
}
