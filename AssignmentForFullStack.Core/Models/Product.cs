using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AssignmentForFullStack.Core.Models
{
    public class Product
    {
        [Key]
        public string ProductCode { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Image { get; set; }
        
        [Range(1, double.MaxValue)]
        public double Price { get; set; }
        public int MinQuantity { get; set; }
        
        public decimal DiscountRate { get; set; }
        
        [ForeignKey("Category")]
        public int CatId { get; set; }
        [JsonIgnore]
        public virtual Category? Category { get; set; }
    }
}
