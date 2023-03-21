using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RestApplication.Models.Product
{
    public class WeightPriceModel
    {
        [Key]
        [JsonIgnore]
        [Required]
        public Guid id { get; set; }

        [Required]
        // Specify how much is a certain weight selection
        // For example 250g = 4$ or 500g = 7$
        public string weight { get; set; }

        [Required]
        public int price { get; set; }

        [Required]
        public string currency { get; set; }

        [ForeignKey("product")]
        public Guid productId;

        [JsonIgnore]
        public ProductModel product { get; set; }
    }
}
