using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarDealer.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CarDealer.Infrastructure.Bundles.User.Repository
{
    public class UserRepository: IUserRepository
    {
    private readonly CarDealerContext _carDealerContext;

        public UserRepository(CarDealerContext carDealerContext)
        {
            _carDealerContext = carDealerContext;
        }
        
        public async Task<IEnumerable<Infrastructure.Bundles.User.Model.User>> GetAll()
        {
            var users = await _carDealerContext.Users.ToListAsync();
            users.ForEach(x => { _carDealerContext.Entry(x).Collection(y => y.Adverts).LoadAsync(); });
            return users;
        }

        public async Task<Infrastructure.Bundles.User.Model.User> GetById(long id)
        {
            var user = await _carDealerContext.Users
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();
            await _carDealerContext.Entry(user).Collection(x => x.Adverts).LoadAsync();
            return user;
        }

        public async Task Add(Infrastructure.Bundles.User.Model.User user)
        {
            user.CreatedAt = DateTime.Now;
            await _carDealerContext.Users
                .Include(x => x.Adverts)
                .FirstAsync();

            await _carDealerContext.Users.AddAsync(user);
            await _carDealerContext.SaveChangesAsync();
        }

        public async Task Update(Infrastructure.Bundles.User.Model.User entity)
        {
            var userToUpdate = await _carDealerContext.Users
                .Include(x => x.Adverts)
                .SingleOrDefaultAsync(x => x.Id == entity.Id);

            if (userToUpdate != null)
            {
                userToUpdate.Email = entity.Email;
                userToUpdate.Login = entity.Login;
                userToUpdate.Password = entity.Password;
                userToUpdate.FirstName = entity.FirstName;
                userToUpdate.PhoneNumber = entity.PhoneNumber;
                userToUpdate.UpdatedAt = entity.UpdatedAt;
                userToUpdate.Adverts = entity.Adverts;

                if (userToUpdate.Adverts != null && entity.Adverts != null)
                {
                    var advertsToUpdate = userToUpdate.Adverts.ToList();
                    foreach (var advert in advertsToUpdate)
                    {
                        foreach (var entityAdvert in entity.Adverts)
                        {
                            if (advert.Id == entityAdvert.Id)
                            {
                                _carDealerContext.Entry(advertsToUpdate).CurrentValues.SetValues(entity.Adverts);
                            }
                        }
                    }
                }
                await _carDealerContext.SaveChangesAsync();
            }
        }

        public async Task Delete(long id)
        {
            var userToUpdate = await _carDealerContext.Users.SingleOrDefaultAsync(user => user.Id == id);
            if (userToUpdate != null)
            {
                _carDealerContext.Users.Remove(userToUpdate);
                await _carDealerContext.SaveChangesAsync();
            }
        }    
    }
}