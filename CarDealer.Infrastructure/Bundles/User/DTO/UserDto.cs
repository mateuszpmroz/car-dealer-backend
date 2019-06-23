using System.Collections.Generic;

namespace CarDealer.Infrastructure.Bundles.User.DTO
{
    public class UserDto
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public List<Advert.Model.Advert> Adverts { get; set; }
    }
}