﻿namespace BackendApp.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string OrderCode { get; set; }
    }
}
