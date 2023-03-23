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
            this.verificationToken = Guid.NewGuid().ToString() + Guid.NewGuid().ToString();
            this.verificationTokenCreationDate = DateTime.UtcNow;
            this.changedPassword = "1234";
            this.changePaswwordToken = Guid.NewGuid().ToString() + Guid.NewGuid().ToString();
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
        public string changedPassword { get; set; }

        [Required]
        public string changePaswwordToken { get; set; }

        public DateTime changePasswordRequestDate { get; set; }

        public DateTime newPasswordCreationDate { get; set; }

        [Required]
        public bool isVerified { get; set; }

        [Required]
        public string verificationToken { get; set; }

        [Required]
        public DateTime verificationTokenCreationDate { get; set; }

        public DateTime tokenVerifiedAt { get; set; }

        [Required]
        public string role { get; set; }
    }
}
