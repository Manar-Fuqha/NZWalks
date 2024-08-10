using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using System.Net;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IWalksRepository walksRepository;
        private readonly IMapper mapper;
      

        public WalksController(IWalksRepository walksRepository , IMapper mapper  )
        {
            this.walksRepository = walksRepository;
            this.mapper = mapper;
          
        }


        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
          
            return Ok(await walksRepository.CreateAsync(mapper.Map<Walk>(addWalkRequestDto)));

        }


        //get Walks 
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn , [FromQuery] string? filterQuery ,
             [FromQuery] string? sortBy , [FromQuery] bool? isAscending ,
            [FromQuery] int pageNumber=1 , [FromQuery] int pageSize=10)
        {
           
               
                return Ok(mapper.Map<List<WalkDto>>(await walksRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize)));
          
        }


        //Get Walk By Id
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walk = await walksRepository.GetByIdAsync(id);
            if (walk == null) return NotFound();
            return Ok(mapper.Map<WalkDto>(walk));
        }


        //Update walk by id
        [HttpPut("{id:guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id , [FromBody] UpdateWalkRequestDto updateWalkRequestDto)
        {
           
            var walkMapper = mapper.Map<Walk>(updateWalkRequestDto);
            var walk= await walksRepository.UpdateAsync(id , walkMapper);
            if(walk == null) return NotFound();
            return Ok(mapper.Map<WalkDto>(walk));

        }


        // Delete Walk By Id
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var walk = await walksRepository.DeleteAsync(id);
            if (walk == null) return NotFound();
            return Ok(mapper.Map<WalkDto>(walk));
           
        }
    }
}
