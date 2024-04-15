using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Csharp_Advanced.Services;
using Csharp_Advanced.Models;
using AutoMapper;

namespace Csharp_Advanced.Controllers
{
    /// <summary>
    /// Controller voor het beheren van locaties, Versie 2.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [ApiVersion("2")]
    public class Locations2Controller : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly LocationService _locationService;
        private readonly IMapper _mapper; // Injecteer IMapper


        public Locations2Controller(AppDbContext context, LocationService locationService, IMapper mapper)
        {
            _context = context;
            _locationService = locationService;
            _mapper = mapper;
        }

        // Return all locations: api/Locations?api-version=2/GetAll
        /// <summary>
        /// Haalt alle locaties op in versie 2 van de API.
        /// </summary>
        /// <param name="version">De versie van de API.</param>
        /// <returns>Een IActionResult met een lijst van alle locaties in versie 2 DTO-formaat.</returns>
        [HttpGet]
        public IActionResult GetLocations(ApiVersion version)
        {
            // Haal de locaties op
            var locations = _locationService.GetAllLocationsNew();

            // Map de locaties naar de DTO van versie 2
            var locationsDto = _mapper.Map<List<LocationDtoV2>>(locations);

            return Ok(locationsDto);
        }
    }
}
