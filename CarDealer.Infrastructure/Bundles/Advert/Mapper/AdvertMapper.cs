using CarDealer.Infrastructure.Bundles.Advert.DTO;

namespace CarDealer.Infrastructure.Bundles.Advert.Mapper
{
    internal static class AdvertMapper 
    {
        public static AdvertDto MapAdvertToDto(Infrastructure.Bundles.Advert.Model.Advert advert)
        {
            return new AdvertDto()
            {
                Title = advert.Title,
                Description = advert.Description,
                Price = advert.Price,
                Status = advert.Status,
                EngineSize = advert.Car?.EngineSize,
                Name = advert.Car?.Name,
                FuelType = advert.Car?.FuelType,
                Mileage = advert.Car?.Mileage,
                IsBroken = advert.Car.IsBroken,
                IsNew = advert.Car.IsNew,
                HorsePower = advert.Car?.HorsePower,
                Id = advert.Id,
                UserId = advert.UserId
            };
        }

        public static Infrastructure.Bundles.Advert.Model.Advert MapDtoToAdvert(AdvertDto advert)
        {
            return new Infrastructure.Bundles.Advert.Model.Advert()
            {
                Title = advert.Title,
                Description = advert.Description,
                Price = advert.Price,
                Status = advert.Status,
                Id = advert.Id.GetValueOrDefault(),
                UserId = advert.UserId.GetValueOrDefault(),
                Car = new Infrastructure.Bundles.Car.Model.Car()
                {
                    EngineSize = advert.EngineSize.GetValueOrDefault(),
                    Name = advert.Name,
                    FuelType = advert.FuelType,
                    Mileage = advert.Mileage.GetValueOrDefault(),
                    IsBroken = advert.IsBroken,
                    IsNew = advert.IsNew,
                    HorsePower = advert.HorsePower.GetValueOrDefault(),
                },
            };
        }
    }
}