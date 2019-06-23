using CarDealer.Infrastructure.Bundles.Car.DTO;

namespace CarDealer.Infrastructure.Bundles.Car.Mapper
{
    internal static class CarMapper
    {
        public static CarDto MapCarToDto(Infrastructure.Bundles.Car.Model.Car car)
        {
            return new CarDto()
            {
                Name = car.Name,
                FuelType = car.FuelType,
                Mileage = car.Mileage,
                IsBroken = car.IsBroken,
                IsNew = car.IsNew,
                HorsePower = car.HorsePower,
                EngineSize = car.EngineSize
            };
        }

        public static Infrastructure.Bundles.Car.Model.Car MapDtoToCar(CarDto car)
        {
            return new Infrastructure.Bundles.Car.Model.Car()
            {
                Name = car.Name,
                FuelType = car.FuelType,
                Mileage = car.Mileage.GetValueOrDefault(),
                IsBroken = car.IsBroken,
                IsNew = car.IsNew,
                HorsePower = car.HorsePower.GetValueOrDefault(),
                EngineSize = car.EngineSize.GetValueOrDefault()
            };
        }
    }
}