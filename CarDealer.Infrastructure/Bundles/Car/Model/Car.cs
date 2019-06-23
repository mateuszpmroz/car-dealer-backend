using CarDealer.Infrastructure.Core;

namespace CarDealer.Infrastructure.Bundles.Car.Model
{
    public class Car: AbstractEntity
    {
        public Advert.Model.Advert Advert { get; set; }
        public string Name { get; set; }
        public string FuelType { get; set; }
        public int Mileage { get; set; }
        public bool IsBroken { get; set; }
        public bool IsNew { get; set; }
        public int HorsePower { get; set; }
        public int EngineSize { get; set; }
    }
}