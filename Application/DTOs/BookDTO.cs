﻿namespace Application.DTOs
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
    }
}
