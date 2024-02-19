using BoxLoader.Models.Boxes;
using BoxLoader.Repository.Boxes;

namespace BoxLoader.Service.Boxes;

public class BoxService : IBoxService
{
	private readonly IBoxRepository _boxRepository;

	public BoxService(IBoxRepository boxRepository)
	{
		_boxRepository = boxRepository ?? throw new ArgumentNullException(nameof(boxRepository));
	}

	public async Task<BoxModel> Get(string identifier)
	{
		return await _boxRepository.Get(identifier);
	}

	public async Task<BoxModel> Insert(BoxModel box)
	{
		return await _boxRepository.Insert(box);
	}
}
