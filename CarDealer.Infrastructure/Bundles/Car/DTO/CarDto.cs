namespace CarDealer.Infrastructure.Bundles.Car.DTO
{
    public class CarDto
    {
        public string Name { get; set; }
        public string FuelType { get; set; }
        public int? Mileage { get; set; }
        public bool IsBroken { get; set; }
        public bool IsNew { get; set; }
        public int? HorsePower { get; set; }
        public int? EngineSize { get; set; }
    }
}