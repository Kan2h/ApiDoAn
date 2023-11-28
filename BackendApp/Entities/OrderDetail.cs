namespace BackendApp.Entities
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public string OrderCode { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
    }
}
