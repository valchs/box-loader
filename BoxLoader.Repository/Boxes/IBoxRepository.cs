using BoxLoader.Models.Boxes;

namespace BoxLoader.Repository.Boxes;

public interface IBoxRepository
{
	Task<BoxModel> Get(string identifier);
	Task<BoxModel> Insert(BoxModel box);
	Task<BoxModel> Update(BoxModel box);
	Task<List<ContentModel>> GetContentsById(string[] ids);
}
