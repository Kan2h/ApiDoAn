﻿namespace BackendApp.Dtos.Items
{
    public class CreateItemDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string ImageUrl { get; set; }
    }
}
