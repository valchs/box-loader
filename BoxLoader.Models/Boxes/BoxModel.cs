namespace BoxLoader.Models.Boxes;

public class BoxModel
{
    public string Identifier { get; set; }
    public string SupplierIdentifier { get; set; }

    public IReadOnlyCollection<ContentModel> Contents { get; set; }
}