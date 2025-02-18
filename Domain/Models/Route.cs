﻿namespace Domain.Models
{
    public class Route
    {
        public int Id { get; set; }
        public string Origin { get; set; } = string.Empty;
        public string Destination { get; set; } = string.Empty;
        public decimal Cost { get; set; }
    }
}
