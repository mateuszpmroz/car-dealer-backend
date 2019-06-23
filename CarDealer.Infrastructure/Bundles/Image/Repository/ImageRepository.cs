using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarDealer.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CarDealer.Infrastructure.Bundles.Image.Repository
{
    public class ImageRepository : IImageRepository
    {
         private readonly CarDealerContext _carDealerContext;

        public ImageRepository(CarDealerContext carDealerContext)
        {
            _carDealerContext = carDealerContext;
        }
        
        public async Task<IEnumerable<Model.Image>> GetAll()
        {
            var images = await _carDealerContext.Images.ToListAsync();
            images.ForEach(x => { _carDealerContext.Entry(x).Reference(y => y.Advert).LoadAsync(); });
            return images;
        }

        public async Task<Model.Image> GetById(long id)
        {
            var image = await _carDealerContext.Images
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();
            await _carDealerContext.Entry(image).Reference(x => x.Advert).LoadAsync();
            return image;
        }

        public async Task Add(Model.Image image)
        {
            image.CreatedAt = DateTime.Now;
            await _carDealerContext.Images
                .Include(x => x.Advert)
                .FirstAsync();

            await _carDealerContext.Images.AddAsync(image);
            await _carDealerContext.SaveChangesAsync();
        }

        public async Task Update(Model.Image entity)
        {
            var imageToUpdate = await _carDealerContext.Images
                .Include(x => x.Advert)
                .SingleOrDefaultAsync(x => x.Id == entity.Id);

            if (imageToUpdate != null)
            {
                imageToUpdate.Data = entity.Data;
                imageToUpdate.Advert = entity.Advert;
              

                if (imageToUpdate.Advert != null && entity.Advert != null)
                {
                    entity.Advert.Id = imageToUpdate.Advert.Id;
                    _carDealerContext.Entry(imageToUpdate.Advert).CurrentValues.SetValues(entity.Advert);
                }
                await _carDealerContext.SaveChangesAsync();
            }
        }

        public async Task Delete(long id)
        {
            var imageToUpdate = await _carDealerContext.Users.SingleOrDefaultAsync(user => user.Id == id);
            if (imageToUpdate != null)
            {
                _carDealerContext.Users.Remove(imageToUpdate);
                await _carDealerContext.SaveChangesAsync();
            }
        }    
    }
}