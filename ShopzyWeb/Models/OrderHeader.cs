using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ShopzyWeb.Models
{
    public class OrderHeader
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public string Address { get; set; } = "";

        public DateTime OrderDate { get; set; } = DateTime.Now;
        public double OrderTotal { get; set; }

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public int UserId { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
            = new List<OrderDetail>();
    }
}
