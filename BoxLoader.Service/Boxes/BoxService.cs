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

	public async Task<BoxModel> Upsert(BoxModel box)
	{
		try
		{
			var existingBox = await this.Get(box.Identifier);

			if (existingBox == null)
			{
				await this.RemoveExisitingContents(box);
				return await _boxRepository.Insert(box);
			}

			return await _boxRepository.Update(box);
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Exception occured while saving the box: {ex.Message}");
			return new BoxModel();
		}

	}

	private async Task<BoxModel> Get(string identifier)
	{
		return await _boxRepository.Get(identifier);
	}

	/// <summary>
	/// NOTE: Temporary method to remove duplicate contents, wouldn't do it in production because it would cause loss of data,
	/// instead I would update the exisitng content or change primary key/use composite primary key so there are no duplicates
	/// </summary>
	/// <param name="box"></param>
	/// <returns></returns>
	private async Task RemoveExisitingContents(BoxModel box)
	{
		var contentIds = box.Contents.Select(x => x.Isbn).ToArray();
		var existingContents = await _boxRepository.GetContentsById(contentIds);
		var exsitingContentIds = existingContents.Select(x => x.Isbn).ToArray();

		box.Contents = box.Contents
			.Where(x => !exsitingContentIds.Contains(x.Isbn))
			.ToList();
	}
}
