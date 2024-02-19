using BoxLoader.Repository.Models.Boxes;
using Microsoft.EntityFrameworkCore;

namespace BoxLoader.Repository.Database;

public class BoxLoaderDbContext : DbContext, IBoxLoaderDbContext
{
	public DbSet<Content> Contents { get; set; } = null!;
	public DbSet<Box> Boxes { get; set; } = null!;

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlServer("connString");
	}
}
