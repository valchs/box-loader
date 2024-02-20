using BoxLoader.Models.Boxes;

namespace BoxLoader.Service.Boxes;

public interface IBoxService
{
	Task<BoxModel> Upsert(BoxModel box);
}
