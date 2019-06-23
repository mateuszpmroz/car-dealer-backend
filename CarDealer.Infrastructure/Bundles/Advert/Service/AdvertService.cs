using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarDealer.Infrastructure.Bundles.Advert.DTO;
using CarDealer.Infrastructure.Bundles.Advert.Mapper;
using CarDealer.Infrastructure.Bundles.Advert.Repository;

namespace CarDealer.Infrastructure.Bundles.Advert.Service
{
    public class AdvertService : IAdvertService
    {
        private readonly IAdvertRepository _iAdvertRepository;

        public AdvertService(IAdvertRepository iAdvertRepository)
        {
            _iAdvertRepository = iAdvertRepository;
        }
        
        public async Task<IEnumerable<AdvertDto>> GetAll()
        {
            var adverts = await _iAdvertRepository.GetAll();
            return adverts.Select(AdvertMapper.MapAdvertToDto).ToList();
        }

        public async Task<AdvertDto> GetById(long id)
        {
            var advert = await _iAdvertRepository.GetById(id);
            return AdvertMapper.MapAdvertToDto(advert);
        }

        public async Task Add(AdvertDto advertDto)
        {
            await _iAdvertRepository.Add(AdvertMapper.MapDtoToAdvert(advertDto));
        }

        public async Task Update(AdvertDto entity)
        {
            await _iAdvertRepository.Update(AdvertMapper.MapDtoToAdvert(entity));

        }

        public async Task Delete(long id)
        {
            await _iAdvertRepository.Delete(id);
        }
    }
}