using AutoMapper;
using BoxLoader.Models.Boxes;
using BoxLoader.Repository.Database;
using BoxLoader.Repository.Models.Boxes;
using Microsoft.EntityFrameworkCore;

namespace BoxLoader.Repository.Boxes
{
	public class BoxRepository : IBoxRepository
	{
		private readonly IBoxLoaderDbContext _dbContext;
		private readonly IMapper _mapper;

		public BoxRepository(IBoxLoaderDbContext dbContext, IMapper mapper)
		{
			_dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
		}

		public async Task<BoxModel> Get(string identifier)
		{
			var result = await _dbContext.Boxes
							.Include(x => x.Contents)
							.AsQueryable()
							.AsNoTracking()
							.SingleOrDefaultAsync(x => x.Identifier == identifier);

			return _mapper.Map<BoxModel>(result);
		}

		public async Task<BoxModel> Insert(BoxModel box)
		{
			if (box == null)
			{
				throw new ArgumentNullException(nameof(box));
			}

			var result = await _dbContext.Boxes.AddAsync(_mapper.Map<Box>(box));
			await _dbContext.SaveChangesAsync();
			this.DetachEntities();

			return _mapper.Map<BoxModel>(result.Entity);
		}

		public async Task<BoxModel> Update(BoxModel box)
		{
			var boxToUpdate = _dbContext.Boxes
				.AsQueryable()
				.AsNoTracking()
				.SingleOrDefault(x => x.Identifier == box.Identifier);

			if (boxToUpdate == null)
			{
				throw new ArgumentNullException(nameof(box));
			}

			var result = _dbContext.Boxes.Update(_mapper.Map<Box>(box));
			await _dbContext.SaveChangesAsync();
			this.DetachEntities();

			return _mapper.Map<BoxModel>(result.Entity);
		}

		// NOTE: Should move it to ContentRepository, keeping here for simplicity
		public async Task<List<ContentModel>> GetContentsById(string[] ids)
		{
			var result = await _dbContext.Contents
							.AsQueryable()
							.Where(x => ids.Contains(x.Isbn))
							.ToListAsync();

			return _mapper.Map<List<ContentModel>>(result);
		}

		private void DetachEntities()
		{
			var trackedEntries = _dbContext.ChangeTracker.Entries().ToList();
			foreach (var trackedEntry in trackedEntries)
			{
				trackedEntry.State = EntityState.Detached;
			}
		}
	}
}
