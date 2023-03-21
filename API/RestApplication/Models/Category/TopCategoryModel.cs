using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RestApplication.Models.Category
{
    public class TopCategoryModel
    {
        [Key]
        [JsonIgnore]
        [Required]
        public Guid id { get; set; }

        [JsonIgnore]
        public Guid? parentCategoryId { get; set; } = null;

        [JsonIgnore]
        public List<MiddleCategoryModel> middleCategories { get; set; }

        [JsonIgnore]
        //string? is redundant, cause strings can be null, but put it for consistency
        public string? parentCategoryName { get; set; } = null;

        [Required]
        public string name { get; set; }
    }
}
