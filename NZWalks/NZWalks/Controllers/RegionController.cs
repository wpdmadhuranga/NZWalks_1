using Microsoft.AspNetCore.Mvc;
using NZWalks.Data;
using NZWalks.Models.domain1;
using NZWalks.Models.DTO;

namespace NZWalks.Controllers;
    
    [Route("api/[controller]")]
    [ApiController]
public class RegionController : ControllerBase
{
    private readonly NZWalksDbContext1 _dbContext1;

    public RegionController(NZWalksDbContext1 dbContext)
    {
        _dbContext1 = dbContext;
    }
    //Get All Regions
    //Get: https://localhost:7032/api/regions
    [HttpGet]
    public IActionResult GetAll()
    {
        //Get Data from Database-Domain model
        var regionsDomain = _dbContext1.Regions.ToList();
        //Map Domain models to Dtos
        var regionsDTO = new List<RegionDTO>();
        foreach (var regionDomain in regionsDomain)
        {
            regionsDTO.Add(new RegionDTO()
                {
                id=regionDomain.id,
                code=regionDomain.code,
                name=regionDomain.name,
                RegionImageUrl = regionDomain.RegionImageUrl
                    });
        }
        
        return Ok(regionsDTO);
    }

    [HttpGet]
    [Route("{id:guid}")]
    public IActionResult GetById([FromRoute]Guid id)
    {   
        // var region = _dbContext1.Regions.Find(id); // Only for primary key(Find)
        // get region domain model from database 
        var regionDomain = _dbContext1.Regions.FirstOrDefault(x => x.id == id);
        
        
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
}