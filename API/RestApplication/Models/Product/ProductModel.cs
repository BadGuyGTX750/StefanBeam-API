using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using RestApplication.Models.Attachment;
using RestApplication.Models.Category;

namespace RestApplication.Models.Product
{
    public class ProductModel
    {
        [Key]
        [JsonIgnore]
        [Required]
        public Guid id { get; set; }

        [Required]
        public string name { get; set; }

        [Required]
        public string shortDescr { get; set; }

        [Required]
        public string longDescr { get; set; }

        [JsonIgnore]
        [ForeignKey("subCategory")]
        [Required]
        public Guid subCategoryId { get; set; }

        [JsonIgnore]
        public SubCategoryModel subCategory { get; set; }

        [Required]
        public string categoryName { get; set; }

        [JsonIgnore]
        public PhotoAttachmentModel photoAttachment { get; set; }

        [Required]
        public List<WeightPriceModel> weight_price { get; set; }

        [Required]
        public List<FlavorQuantityModel> flavor_quantity { get; set; }

        [JsonIgnore]
        public bool isInStock { get; set; }
    }
}
