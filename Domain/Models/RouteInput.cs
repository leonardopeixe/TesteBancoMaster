﻿namespace Domain.Models
{
    public class RouteInput
    {
        public string Origin { get; set; } = string.Empty;
        public string Destination { get; set; } = string.Empty;
        public decimal Cost { get; set; }
    }
}
