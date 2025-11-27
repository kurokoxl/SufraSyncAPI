namespace SufraSyncAPI.Models.DTOs.CartDtos
{
    public class CartDto
    {
        public string UserId { get; set; }
        public List<CartItemDto> Items { get; set; }
        public decimal TotalPrice { get; set; }
    }
}