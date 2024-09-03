namespace dsPortal.Core.Entities;

public class Location : BaseEntity
{
    public string Name { get; set; }
    public string Address { get; set; }
    public List<Office> Offices { get; set; }
}