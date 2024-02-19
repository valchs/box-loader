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
			var boxes = _dbContext.Boxes.AsQueryable();
			var result = await BoxInclude(boxes).FirstOrDefaultAsync(x => x.Identifier == identifier);

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

			return _mapper.Map<BoxModel>(result.Entity);
		}

		private IQueryable<Box> BoxInclude(IQueryable<Box> boxes)
		{
			boxes = boxes.Include(x => x.Contents);

			return boxes;
		}
	}
}
