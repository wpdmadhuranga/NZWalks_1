using Microsoft.AspNetCore.Mvc;
using NZWalks.Models.domain1;

namespace NZWalks.Controllers;
    
    [Route("api/[controller]")]
    [ApiController]
public class RegionController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        var regions = new List<Region>
        {
            new Region()
            {
                id = Guid.NewGuid(),
                name = "Auckland Region",
                code = "AKL",
                RegionImageUrl = "asdasdas"
                
            },
            new Region()
            {
                id = Guid.NewGuid(),
                name = "Welington Region",
                code = "WLN",
                RegionImageUrl = "asdasdas"
                
            }
        };
            
        return Ok();
    }
}