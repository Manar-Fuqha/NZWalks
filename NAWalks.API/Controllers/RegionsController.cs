using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using System.Linq.Expressions;
using System.Text.Json;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IRegionRepository regionRepository;
        private readonly ILogger logger;

        public RegionsController(NZWalksDbContext dbContext , IMapper mapper , IRegionRepository regionRepository
            , ILogger<RegionsController>  logger)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.regionRepository = regionRepository;
            this.logger = logger;
        }
        
        [HttpGet("GetAllRegions")]
       [Authorize(Roles ="Reader")]
    
        public async Task<IActionResult> getAllRegions()
        {
          
                var regions = await regionRepository.GetAllAsync();
                logger.LogInformation($"Finished GetAllRegions with data : {JsonSerializer.Serialize(regions)}");
                return Ok(mapper.Map<List<RegionDto>>(regions));
           
           
           
        }


       
        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader")]

        public async Task<IActionResult> GetRegionById([FromRoute] Guid id)
        {
            var region = mapper.Map < RegionDto > (await regionRepository.GetByIdAsync(id));
            if(region == null) { return NotFound(); }
            return Ok(region);
        }



        //create new region 
        [HttpPost]
        [Authorize(Roles = "Writer")]
        [ValidateModel]
        
        public async Task<IActionResult> createRegion([FromBody]AddRegionRequestDto addRegionRequestDto)
        {
            return Ok(await regionRepository.CreateAsync(mapper.Map<Region>(addRegionRequestDto)));
        }



        //update region
        [HttpPut("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        [ValidateModel]

        public async Task<IActionResult> updateRegion([FromRoute] Guid id , [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
         
            var regionMapper = mapper.Map<Region>(updateRegionRequestDto);
            // check if region found 
           var region = await regionRepository.Update(id, regionMapper);
          if(region == null) { return NotFound(); }

         
            return Ok(region);
        }



        //delete region
        [HttpDelete("{id:Guid}")]
        [Authorize(Roles = "Writer,Reader")]
        public async Task<IActionResult> deleteRegion([FromRoute] Guid id)
        {
            // check if region is exist
            var region = await regionRepository.Delete(id);
            if(region == null) { return NotFound();}           
            return Ok();

        }


    }
}
