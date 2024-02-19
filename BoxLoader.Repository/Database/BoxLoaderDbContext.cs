using BoxLoader.Repository.Models.Boxes;
using Microsoft.EntityFrameworkCore;

namespace BoxLoader.Repository.Database;

public class BoxLoaderDbContext : DbContext, IBoxLoaderDbContext
{
	public BoxLoaderDbContext(DbContextOptions options) : base(options)
	{

	}

	public DbSet<Content> Contents { get; set; } = null!;
	public DbSet<Box> Boxes { get; set; } = null!;
}
