using Microsoft.EntityFrameworkCore;
using NZWalks.Models.domain1;

namespace NZWalks.Data;

public class NZWalksDbContext1 : DbContext

{
    public NZWalksDbContext1(DbContextOptions dbContextOptions) : base(dbContextOptions) { }
    
    public DbSet<Difficulty> Difficulties { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<Walk> Walks { get; set; }   
    
}