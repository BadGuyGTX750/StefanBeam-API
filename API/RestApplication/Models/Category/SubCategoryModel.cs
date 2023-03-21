using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using RestApplication.Models.Product;

namespace RestApplication.Models.Category
{
    public class SubCategoryModel
    {
        [Key]
        [JsonIgnore]
        [Required]
        public Guid id { get; set; }

        [Required]
        [ForeignKey("middleCategory")]
        [JsonIgnore]
        public Guid parentCategoryId { get; set; }

        [JsonIgnore]
        public MiddleCategoryModel middleCategory { get; set; }

        [JsonIgnore]
        public List<ProductModel> products { get; set; }

        [Required]
        public string parentCategoryName { get; set; } = null;

        [Required]
        public string name { get; set; }
    }
}
