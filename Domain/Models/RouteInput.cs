namespace Domain.Models
{
    public class RouteInput
    {
        public required string Origin { get; set; } = string.Empty;
        public required string Destination { get; set; } = string.Empty;
        public required decimal Cost { get; set; }
    }
}
