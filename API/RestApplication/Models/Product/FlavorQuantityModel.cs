using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RestApplication.Models.Product
{
    public class FlavorQuantityModel
    {
        [Key]
        [JsonIgnore]
        [Required]
        public Guid id { get; set; }

        [Required]
        public string flavor { get; set; }

        //number of products with a certain flavor
        [Required]
        public uint quantity { get; set; }

        [ForeignKey("product")]
        public Guid productId;

        [JsonIgnore]
        public ProductModel product { get; set; }
    }
}