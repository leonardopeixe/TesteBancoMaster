namespace Domain.Models
{
    public class BestRouteResponse
    {
        public BestRouteResponse(
            string route, 
            decimal totalCost,
            string message
        ) {
            Route = route;
            Message = message;
            TotalCost = totalCost;
        }
        public string Route { get; private set; } = string.Empty;
        public string Message { get; private set; } = string.Empty;
        public decimal TotalCost { get; private set; }
    }
}
