namespace ShopzyWeb.Models
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }
        public string ImageUrl { get; set; }
    }
}
