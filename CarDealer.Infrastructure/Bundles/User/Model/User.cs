using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CarDealer.Infrastructure.Core;

namespace CarDealer.Infrastructure.Bundles.User.Model
{
    public class User: AbstractEntity
    {
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Email { get; set; }
        [Required]
        [Column(TypeName = "varchar(9)")]
        public string PhoneNumber { get; set; }
        [Required]
        public string FirstName { get; set; }
        public List<Advert.Model.Advert> Adverts { get; set; }
    }
}