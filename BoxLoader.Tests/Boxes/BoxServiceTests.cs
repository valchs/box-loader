using AutoFixture;
using BoxLoader.Models.Boxes;
using BoxLoader.Repository.Boxes;
using BoxLoader.Service.Boxes;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace BoxLoader.Tests.Boxes;

public class BoxServiceTests
{
    private MockRepository _mockRepository;
    private Fixture _fixture;

    private Mock<IBoxRepository> _boxRepository;

    private BoxService _boxService;

    [SetUp]
    public void SetUp()
    {
        _mockRepository = new MockRepository(MockBehavior.Loose);

        _boxRepository = _mockRepository.Create<IBoxRepository>();

        _boxService = new BoxService(
            _boxRepository.Object);
        _fixture = new Fixture();
    }

    [Test]
    public async Task Upsert_ChecksExistingBox()
    {
        // Arrange
        var box = _fixture.Create<BoxModel>();

        // Act
        await _boxService.Upsert(box);

        // Assert
        _boxRepository.Verify(x => x.Get(It.IsAny<string>()), Times.Once);
    }

    [Test]
    public async Task Upsert_BoxDoesNotExist_InsertsNewBox()
    {
        // Arrange
        var box = _fixture.Create<BoxModel>();

        // Act
         await _boxService.Upsert(box);

        // Assert
        _boxRepository.Verify(x => x.Insert(It.IsAny<BoxModel>()), Times.Once);
    }

    [Test]
    public async Task Upsert_BoxExists_UpdatesBox()
    {
        // Arrange
        var box = _fixture.Create<BoxModel>();
        _boxRepository.Setup(x => x.Get(It.IsAny<string>())).ReturnsAsync(box);

        // Act
        var result = await _boxService.Upsert(box);

        // Assert
        _boxRepository.Verify(x => x.Update(It.IsAny<BoxModel>()), Times.Once);
    }
}
