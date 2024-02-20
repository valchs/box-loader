using System.ComponentModel.DataAnnotations;

namespace BoxLoader.Repository.Models.Boxes;

public class Content
{
    [Key]
    public string Isbn { get; set; }
    public string PoNumber { get; set; }
    public int Quantity { get; set; }
	public Box Box { get; set; }
}
