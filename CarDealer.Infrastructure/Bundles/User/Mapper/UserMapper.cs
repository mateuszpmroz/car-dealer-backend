using CarDealer.Infrastructure.Bundles.User.DTO;

namespace CarDealer.Infrastructure.Bundles.User.Mapper
{
    public class UserMapper
    {
        public static UserDto MapUserToDto(Model.User user)
        {
            return new UserDto()
            {
                Login = user.Login,
                Password = user.Password,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FirstName = user.FirstName,
                Adverts = user.Adverts
            };
        }

        public static Model.User MapDtoToUser(UserDto user)
        {
            return new Model.User()
            {
                Login = user.Login,
                Password = user.Password,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FirstName = user.FirstName,
                Adverts = user.Adverts
            };
        }
    }
}