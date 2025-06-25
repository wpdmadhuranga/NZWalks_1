using Microsoft.EntityFrameworkCore;
using NZWalks.Data;
using NZWalks.Models.domain1;
using NZWalks.Models.DTO;

namespace NZWalks.Repository;

public class MySQLRegionReository: IRegionResitory 
{
    private readonly NZWalksDbContext1 _dbContext;

    public MySQLRegionReository(NZWalksDbContext1 dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<List<Region>> GetAllAsync()
    {
        return await _dbContext.Regions.ToListAsync();
    }

    public async Task<Region?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Regions.FirstOrDefaultAsync(x =>x.id == id);
    }

    public async Task<Region> AddAsync(Region region)
    {
         await _dbContext.Regions.AddAsync(region);
         await _dbContext.SaveChangesAsync();
         return region;
    }

    public async Task<Region?> UpdateAsync(Guid id, Region region)
    {
        var existingregion = await _dbContext.Regions.FirstOrDefaultAsync(x => x.id == id);

        if ((existingregion == null))
        {
            return null;
        }
        existingregion.code = region.code;
        existingregion.name = region.name;
        existingregion.RegionImageUrl = region.RegionImageUrl;
        
        await _dbContext.SaveChangesAsync();
        return existingregion;
    }

    public async Task<Region?> DeleteAsync(Guid id)
    {
        var existingRegion =  await _dbContext.Regions.FirstOrDefaultAsync(x => x.id == id);
        if (existingRegion == null)
        {
            return null;
        }
        _dbContext.Regions.Remove(existingRegion);
        await _dbContext.SaveChangesAsync();
       return existingRegion;
    }
}