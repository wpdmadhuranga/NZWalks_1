using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.Data;
using NZWalks.Mapping;
using NZWalks.Models.domain1;
using NZWalks.Models.DTO;
using NZWalks.Repository;

namespace NZWalks.Controllers;
    
    [Route("api/[controller]")]
    [ApiController]
public class RegionController : ControllerBase
{
    private readonly NZWalksDbContext1 _dbContext1;
    private readonly IRegionResitory _regionResitory;
    private readonly IMapper _mapper;

    public RegionController(NZWalksDbContext1 dbContext, IRegionResitory regionResitory,IMapper mapper)
    {
        _dbContext1 = dbContext;
        _regionResitory = regionResitory;
        _mapper = mapper;
    }
    //Get All Regions
    //Get: https://localhost:7032/api/regions
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        //Get Data from Database-Domain model
        var regionsDomain = await _regionResitory.GetAllAsync();
        //Map Domain models to Dtos
        return Ok(_mapper.Map<RegionDTO>(Region));
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute]Guid id)
    {   
        // var region = _dbContext1.Regions.Find(id); // Only for primary key(Find)
        // get region domain model from database 
        var regionDomain = await _regionResitory.GetByIdAsync(id);
        
        
        if (regionDomain == null)
        {
            return NotFound();
        }
        
            var regionDTO = new RegionDTO
                
            {
                id=regionDomain.id,
                code=regionDomain.code,
                name=regionDomain.name,
                RegionImageUrl = regionDomain.RegionImageUrl
            };
        
        return Ok(regionDTO);
    }
    
    //Post
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
    {
        // map dto to domain model
        var regionDomainModel = new Region()
        {
            code = addRegionRequestDto.code,
            name = addRegionRequestDto.name,
            RegionImageUrl = addRegionRequestDto.RegionImageUrl
        };
        
        //use domain model to create region
        regionDomainModel = await _regionResitory.AddAsync(regionDomainModel);
        
        
        // map domain model to back dto s 
        var regionDto = new RegionDTO()
        {
            id = regionDomainModel.id,
            code = regionDomainModel.code,
            name = regionDomainModel.name,
            RegionImageUrl = regionDomainModel.RegionImageUrl
        };
        return CreatedAtAction(nameof(GetById), new { id = regionDomainModel.id }, regionDto);
    }

    [HttpPut]
    [Route("{id:guid}")]

    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
    {
        //map dto to domain model
        var regionDomainModel = new Region
        {
            code = updateRegionRequestDto.code,
            name = updateRegionRequestDto.name,
            RegionImageUrl = updateRegionRequestDto.RegionImageUrl
        };
        
        //check if region is exist
        regionDomainModel = await _regionResitory.UpdateAsync(id,regionDomainModel);
        if (regionDomainModel == null)
        {
            return NotFound();
        }
        //map domain model to dto back

        var regionDto = new RegionDTO()
        {
            id = regionDomainModel.id,
            code = regionDomainModel.code,
            name = regionDomainModel.name,
            RegionImageUrl = regionDomainModel.RegionImageUrl
        };
        return Ok(regionDto);
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var regionDomainModel = await _regionResitory.DeleteAsync(id);
        if (regionDomainModel == null)
        {
            return NotFound();
        }
        //return deleted region back
        //map domain model to dto
        var regionDto = new RegionDTO
        {
            id = regionDomainModel.id,
            code = regionDomainModel.code,
            name = regionDomainModel.name,
            RegionImageUrl = regionDomainModel.RegionImageUrl
        };
        return Ok(regionDto);
    }
}