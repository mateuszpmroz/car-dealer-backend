using System;
using System.Threading.Tasks;
using CarDealer.Infrastructure.Bundles.Car.DTO;
using CarDealer.Infrastructure.Bundles.Car.Service;
using CarDealer.Infrastructure.Context;
using Microsoft.AspNetCore.Mvc;

namespace CarDealer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarController : ControllerBase
    {
            private readonly ICarService _iCarService;
            private readonly CarDealerContext _carDealerContext;

            public CarController(ICarService iCarService, CarDealerContext carDealerContext)
            {
                _iCarService = iCarService;
                _carDealerContext = carDealerContext;
            }

            [HttpGet("GetCar/{Id}")]
            public async Task<IActionResult> GetCarById(long id)
            {
                try
                {
                    var car = await _iCarService.GetById(id);
                    return Ok(car);
                }
                catch (NullReferenceException e)
                {
                    return NotFound($"Can't found car with id = {id}");
                }
            }
 
            [HttpGet("GetAllCars")]
            public async Task<IActionResult> GetAllCars()
            {
                var cars = await _iCarService.GetAll();
                return Ok(cars);
            }

            [HttpPost]
            public async Task<IActionResult> CreateAdvert([FromBody] CarDto carDto)
            {
                if (carDto == null)
                {
                    return BadRequest();
                }

                await _iCarService.Add(carDto);
                return Created("Created new car", carDto);
            }

            [HttpPut("UpdateCar")]
            public async Task<IActionResult> UpdateCar([FromBody] CarDto carDto)
            {
                if (carDto == null)
                {
                    return BadRequest();
                }

                await _iCarService.Update(carDto);
                return Ok($"Updated car with name = {carDto.Name}");
            }

            [HttpDelete("DeleteCar/{id}")]

            public async Task<IActionResult> DeleteCar(long id)
            {
                await _iCarService.Delete(id);
                return Ok($"Car with id = {id} deleted");
            }
    }
}