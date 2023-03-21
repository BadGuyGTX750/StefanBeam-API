using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using RestApplication.Models.Product;

namespace RestApplication.Models.Attachment
{
    public class PhotoAttachmentModel
    {
        [Key]
        [Required]
        public Guid id { get; set; }

        [Required]
        public string name { get; set; }

        [ForeignKey("product")]
        [Required]
        public Guid? parentProductId { get; set; }

        [Required]
        public ProductModel product { get; set; }

        [Required]
        public string productName { get; set; }

        [Required]
        public string filePath { get; set; }

        [Required]
        public string ext { get; set; }
    }
}
