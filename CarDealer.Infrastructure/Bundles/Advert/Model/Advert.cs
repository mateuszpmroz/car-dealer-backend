using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CarDealer.Infrastructure.Bundles.Image.Model;
using CarDealer.Infrastructure.Core;

namespace CarDealer.Infrastructure.Bundles.Advert.Model
{
    public class Advert: AbstractEntity
    {
        public Car.Model.Car Car { get; set; }
        [Required]
        [Column(TypeName = "varchar(200)")]
        public string Title { get; set; }
        [Required]
        [Column(TypeName = "varchar(1000)")]
        public string Description { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public bool Status { get; set; }
        public IEnumerable<Image.Model.Image> Images { get; set; }
        public long? UserId { get; set; }
    }
}