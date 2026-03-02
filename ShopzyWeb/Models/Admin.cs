using System.ComponentModel.DataAnnotations;

namespace ShopzyWeb.Models
{
    public class Admin
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
