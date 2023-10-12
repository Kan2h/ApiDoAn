﻿namespace BackendApp.Dtos.Items
{
    public class FavoriteItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string ImageUrl { get; set; }
        public bool IsFavorite { get; set; }
    }
}
