using AutoMapper;
using GdeIzaci.Data;
using GdeIzaci.Models.Domain;
using GdeIzaci.Models.DTO;
using GdeIzaci.Repository.Interfaces;
using GdeIzaci.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

namespace GdeIzaci.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaceItemController : ControllerBase
    {
        private readonly IPlaceItemService placeItemService;

        public PlaceItemController(IPlaceItemService placeItemService)
        {
            this.placeItemService = placeItemService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Manager, RegularUser")]
        public async Task<IActionResult> GetAll()
        {
            var placeItemsDto = await placeItemService.GetAllAsync();

            return Ok(placeItemsDto);

        }
    }
}
