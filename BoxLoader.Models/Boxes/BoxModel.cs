namespace BoxLoader.Models.Boxes;

public class BoxModel
{
    public string Identifier { get; set; }
    public string SupplierIdentifier { get; set; }
    public List<ContentModel> Contents { get; set; } = new List<ContentModel>();
}