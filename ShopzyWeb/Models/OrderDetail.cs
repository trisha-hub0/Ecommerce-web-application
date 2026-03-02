using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopzyWeb.Models
{
    public class OrderDetail
    {
        [Key]
        public int Id { get; set; }

        // 🔗 Foreign Key → OrderHeader
        [Required]
        public int OrderHeaderId { get; set; }

        [ForeignKey(nameof(OrderHeaderId))]
        public OrderHeader OrderHeader { get; set; } = null!;

        // 🔗 Product reference
        [Required]
        public int ProductId { get; set; }

        [Required]
        public string ProductTitle { get; set; } = string.Empty;

        [Required]
        public int Count { get; set; }

        [Required]
        public double Price { get; set; }
    }
}
