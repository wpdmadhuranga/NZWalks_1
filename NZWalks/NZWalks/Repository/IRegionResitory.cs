using NZWalks.Models.domain1;

namespace NZWalks.Repository;

public interface IRegionResitory
{
    Task<List<Region>> GetAllAsync();
    
    Task<Region?> GetByIdAsync(Guid id);
    
    Task<Region> AddAsync(Region region);
    
    Task<Region> UpdateAsync(Guid id,Region region);
    
    Task<Region?> DeleteAsync(Guid id);
}