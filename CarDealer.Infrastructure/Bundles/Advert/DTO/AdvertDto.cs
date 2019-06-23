using Microsoft.AspNetCore.SignalR;

namespace CarDealer.Infrastructure.Bundles.Advert.DTO
{
    public class AdvertDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public bool Status { get; set; }
        
        public string Name { get; set; }
        public string FuelType { get; set; }
        public int? Mileage { get; set; }
        public bool IsBroken { get; set; }
        public bool IsNew { get; set; }
        public int? HorsePower { get; set; }
        public int? EngineSize { get; set; }
        public long? Id { get; set; }
        public long? UserId { get; set; }
    }
}