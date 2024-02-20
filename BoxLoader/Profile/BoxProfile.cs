using BoxLoader.Models.Boxes;
using BoxLoader.Repository.Models.Boxes;

namespace BoxLoader.Host.Profile;

public class BoxProfile : AutoMapper.Profile
{
	public BoxProfile()
	{
		CreateMap<Box, BoxModel>().ReverseMap();
		CreateMap<Content, ContentModel>().ReverseMap();
	}
}
