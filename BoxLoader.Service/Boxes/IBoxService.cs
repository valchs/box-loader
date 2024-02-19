using BoxLoader.Models.Boxes;

namespace BoxLoader.Service.Boxes;

public interface IBoxService
{
	Task<BoxModel> Get(string identifier);
	Task<BoxModel> Insert(BoxModel box);
}
