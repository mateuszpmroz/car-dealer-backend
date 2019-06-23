using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarDealer.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CarDealer.Infrastructure.Bundles.Car.Repository
{
    public class CarRepository: ICarRepository
    {
         private readonly CarDealerContext _carDealerContext;

        public CarRepository(CarDealerContext carDealerContext)
        {
            _carDealerContext = carDealerContext;
        }
        
        public async Task<IEnumerable<Infrastructure.Bundles.Car.Model.Car>> GetAll()
        {
            var cars = await _carDealerContext.Cars.ToListAsync();
            cars.ForEach(x => { _carDealerContext.Entry(x).Reference(y => y.Advert).LoadAsync(); });
            return cars;
        }

        public async Task<Infrastructure.Bundles.Car.Model.Car> GetById(long id)
        {
            var car = await _carDealerContext.Cars
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();
            await _carDealerContext.Entry(car).Reference(x => x.Advert).LoadAsync();
            return car;
        }

        public async Task Add(Infrastructure.Bundles.Car.Model.Car car)
        {
            car.CreatedAt = DateTime.Now;
            await _carDealerContext.Cars
                .Include(x => x.Advert)
                .FirstAsync();

            await _carDealerContext.Cars.AddAsync(car);
            await _carDealerContext.SaveChangesAsync();
        }

        public async Task Update(Infrastructure.Bundles.Car.Model.Car entity)
        {
            var carToUpdate = await _carDealerContext.Cars
                .Include(x => x.Advert)
                .SingleOrDefaultAsync(x => x.Id == entity.Id);

            if (carToUpdate != null)
            {
                carToUpdate.Name = entity.Name;
                carToUpdate.Mileage = entity.Mileage;
                carToUpdate.IsNew = entity.IsNew;
                carToUpdate.Advert = entity.Advert;
                carToUpdate.FuelType = entity.FuelType;
                carToUpdate.IsBroken = entity.IsBroken;
                carToUpdate.EngineSize = entity.EngineSize;
                carToUpdate.HorsePower = entity.HorsePower;
                carToUpdate.UpdatedAt = entity.UpdatedAt;
                await _carDealerContext.SaveChangesAsync();
            }
        }

        public async Task Delete(long id)
        {
            var carToUpdate = await _carDealerContext.Cars.SingleOrDefaultAsync(car => car.Id == id);
            if (carToUpdate != null)
            {
                _carDealerContext.Cars.Remove(carToUpdate);
                await _carDealerContext.SaveChangesAsync();
            }
        }
    }
}