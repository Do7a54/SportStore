using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;  // added page 255 
namespace SportsStore.Models
{
    public class Product
    {
        public long? ProductID { get; set; }

        [Required(ErrorMessage = "Please enter a product name")]    // added page 255
        public string Name { get; set; } = String.Empty;

        [Required(ErrorMessage = "Please enter a description")]     // added page 255
        public string Description { get; set; } = String.Empty;

        [Required]
        [Range(0.01, double.MaxValue,ErrorMessage = "Please enter a positive price")]    // added page 255

        [Column(TypeName = "decimal(8, 2)")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Please specify a category")]     // added page 255  -- Validation Attributes
        public string Category { get; set; } = String.Empty;
    }
}