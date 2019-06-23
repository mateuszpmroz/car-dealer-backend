using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using CarDealer.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CarDealer.Infrastructure.Bundles.Advert.Repository
{
    public class AdvertRepository : IAdvertRepository
    {
        private readonly CarDealerContext _carDealerContext;

        public AdvertRepository(CarDealerContext carDealerContext)
        {
            _carDealerContext = carDealerContext;
        }
        
        public async Task<IEnumerable<Model.Advert>> GetAll()
        {
            var adverts = await _carDealerContext.Adverts.ToListAsync();
            adverts.ForEach(x => { _carDealerContext.Entry(x).Reference(y => y.Car).LoadAsync(); });
            return adverts;
        }

        public async Task<Model.Advert> GetById(long id)
        {
            var advert = await _carDealerContext.Adverts
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();
            try
            {
                await _carDealerContext.Entry(advert).Reference(x => x.Car).LoadAsync();

            }
            catch (Exception e)
            {
                return null;
            }
            return advert;
        }

        public async Task Add(Model.Advert advert)
        {
            advert.CreatedAt = DateTime.Now;
            await _carDealerContext.Adverts
                .Include(x => x.Car)
                .Include(x => x.Images)
                .FirstAsync();
            await _carDealerContext.Adverts.AddAsync(advert);
            await _carDealerContext.SaveChangesAsync();
        }

        public async Task Update(Model.Advert entity)
        {
            var advertToUpdate = await _carDealerContext.Adverts
                .Include(x => x.Car)
                .SingleOrDefaultAsync(x => x.Id == entity.Id);

            if (advertToUpdate != null)
            {
                advertToUpdate.Price = entity.Price;
                advertToUpdate.Title = entity.Title;
                advertToUpdate.Status = entity.Status;
                advertToUpdate.Images = entity.Images;
                advertToUpdate.Description = entity.Description;
                advertToUpdate.UpdatedAt = DateTime.Now;
                advertToUpdate.Car = entity.Car;
                advertToUpdate.UserId = entity.UserId;

                if (entity.Car != null && advertToUpdate.Car != null)
                {
                    entity.Car.Id = advertToUpdate.Car.Id;
                    _carDealerContext.Entry(advertToUpdate.Car).CurrentValues.SetValues(entity.Car);
                }

                if (advertToUpdate.Images != null && entity.Images != null)
                {
                    var imagesToUpdate = advertToUpdate.Images.ToList();
                    foreach (var image in imagesToUpdate)
                    {
                        foreach (var entityImage in entity.Images)
                        {
                            if (image.Id == entityImage.Id)
                            {
                                _carDealerContext.Entry(imagesToUpdate).CurrentValues.SetValues(entity.Images);
                            }
                        }
                    }
                }
                
                await _carDealerContext.SaveChangesAsync();
            }
        }

        public async Task Delete(long id)
        {
            var advertToDelete = await _carDealerContext.Adverts.SingleOrDefaultAsync(advert => advert.Id == id);
            if (advertToDelete != null)
            {
                _carDealerContext.Adverts.Remove(advertToDelete);
                await _carDealerContext.SaveChangesAsync();
            }
        }
    }
}