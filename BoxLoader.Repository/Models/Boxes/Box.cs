using System.ComponentModel.DataAnnotations;

namespace BoxLoader.Repository.Models.Boxes;

public class Box
{
    [Key]
    public string Identifier { get; set; }

    public string SupplierIdentifier { get; set; }

    public List<Content> Contents { get; set; }
}
