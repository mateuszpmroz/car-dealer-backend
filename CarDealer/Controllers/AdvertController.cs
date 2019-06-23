using System;
using System.Threading.Tasks;
using CarDealer.Infrastructure.Bundles.Advert.DTO;
using CarDealer.Infrastructure.Bundles.Advert.Service;
using CarDealer.Infrastructure.Context;
using Microsoft.AspNetCore.Mvc;

namespace CarDealer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdvertController : ControllerBase
    {
        private readonly IAdvertService _iAdvertService;
        private readonly CarDealerContext _carDealerContext;

        public AdvertController(IAdvertService iAdvertService, CarDealerContext carDealerContext)
        {
            _iAdvertService = iAdvertService;
            _carDealerContext = carDealerContext;
        }

        [HttpGet("GetAdvert/{Id}")]
        public async Task<IActionResult> GetAdvertById(long id)
        {
            try
            {
                var advert = await _iAdvertService.GetById(id);
                return Ok(advert);
            }
            catch (NullReferenceException e)
            {
                return NotFound($"Can't found advert with id = {id}");
            }
        }
 
        [HttpGet("GetAllAdverts")]
        public async Task<IActionResult> GetAllAdverts()
        {
            var adverts = await _iAdvertService.GetAll();
            return Ok(adverts);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAdvert([FromBody] AdvertDto advertDto)
        {
            if (advertDto == null)
            {
                return BadRequest();
            }

            await _iAdvertService.Add(advertDto);
            return Created("Created new advert", advertDto);
        }

        [HttpPut("UpdateAdvert")]
        public async Task<IActionResult> UpdateAdvert([FromBody] AdvertDto advertDto)
        {
            if (advertDto == null)
            {
                return BadRequest();
            }

            await _iAdvertService.Update(advertDto);
            return Ok($"Updated advert with name = {advertDto.Name}");
        }

        [HttpDelete("DeleteAdvert/{id}")]

        public async Task<IActionResult> DeleteAdvert(long id)
        {
            await _iAdvertService.Delete(id);
            return Ok($"Advert with id = {id} deleted");
        }
    }
}