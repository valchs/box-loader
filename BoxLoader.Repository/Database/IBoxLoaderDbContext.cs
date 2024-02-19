﻿using BoxLoader.Repository.Models.Boxes;
using Microsoft.EntityFrameworkCore;

namespace BoxLoader.Repository.Database;

public interface IBoxLoaderDbContext
{
	DbSet<Content> Contents { get; set; }
	DbSet<Box> Boxes { get; set; }
	Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
