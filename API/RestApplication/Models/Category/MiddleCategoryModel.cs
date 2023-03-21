using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RestApplication.Models.Category
{
    public class MiddleCategoryModel
    {
        [Key]
        [JsonIgnore]
        [Required]
        public Guid id { get; set; }

        [Required]
        [ForeignKey("topCategory")]
        [JsonIgnore]
        public Guid parentCategoryId { get; set; }

        [JsonIgnore]
        public TopCategoryModel topCategory { get; set; }

        [JsonIgnore]
        public List<SubCategoryModel> subCategories { get; set; }

        [Required]
        public string parentCategoryName { get; set; } = null;

        [Required]
        public string name { get; set; }
    }
}
