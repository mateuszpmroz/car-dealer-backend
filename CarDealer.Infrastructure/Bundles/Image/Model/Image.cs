using CarDealer.Infrastructure.Core;

namespace CarDealer.Infrastructure.Bundles.Image.Model
{
    public class Image : AbstractEntity
    {
        public byte[] Data { get; set; }
        public Infrastructure.Bundles.Advert.Model.Advert Advert { get; set; }
    }
}