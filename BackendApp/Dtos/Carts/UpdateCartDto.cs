namespace BackendApp.Dtos.Carts
{
    public class UpdateCartDto
    {
        public int UserId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
    }
}
