﻿namespace BackendApp.Entities
{
    public class Favorite
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ItemId { get; set; }
        public bool IsFavorite { get; set; }
    }
}
