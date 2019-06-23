using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarDealer.Infrastructure.Bundles.User.DTO;
using CarDealer.Infrastructure.Bundles.User.Mapper;
using CarDealer.Infrastructure.Bundles.User.Repository;

namespace CarDealer.Infrastructure.Bundles.User.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _iUserRepository;

        public UserService(IUserRepository iUserRepository)
        {
            this._iUserRepository = iUserRepository;
        }
        
        public async Task<IEnumerable<UserDto>> GetAll()
        {
            var users = await _iUserRepository.GetAll();
            return users.Select(UserMapper.MapUserToDto).ToList();
        }

        public async Task<UserDto> GetById(long id)
        {
            var user = await _iUserRepository.GetById(id);
            return UserMapper.MapUserToDto(user); 
        }

        public async Task Add(UserDto userDto)
        {
            await _iUserRepository.Add(UserMapper.MapDtoToUser(userDto));
        }

        public async Task Update(UserDto entity)
        {
            await _iUserRepository.Update(UserMapper.MapDtoToUser(entity));
        }

        public async Task Delete(long id)
        {
            await _iUserRepository.Delete(id);
        }
    }
}