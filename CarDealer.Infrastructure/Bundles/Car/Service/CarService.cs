using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarDealer.Infrastructure.Bundles.Car.DTO;
using CarDealer.Infrastructure.Bundles.Car.Mapper;
using CarDealer.Infrastructure.Bundles.Car.Repository;

namespace CarDealer.Infrastructure.Bundles.Car.Service
{
    public class CarService: ICarService
    {
        private readonly ICarRepository _iCarRepository;

        public CarService(ICarRepository iCarRepository)
        {
            this._iCarRepository = iCarRepository;
        }
        
        public async Task<IEnumerable<CarDto>> GetAll()
        {
            var cars = await _iCarRepository.GetAll();
            return cars.Select(CarMapper.MapCarToDto).ToList();
        }

        public async Task<CarDto> GetById(long id)
        {
            var car = await _iCarRepository.GetById(id);
            return CarMapper.MapCarToDto(car); 
        }

        public async Task Add(CarDto carDto)
        {
            await _iCarRepository.Add(CarMapper.MapDtoToCar(carDto));
        }

        public async Task Update(CarDto entity)
        {
            await _iCarRepository.Update(CarMapper.MapDtoToCar(entity));
        }

        public async Task Delete(long id)
        {
            await _iCarRepository.Delete(id);
        }
    }
}