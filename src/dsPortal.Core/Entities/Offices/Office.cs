namespace dsPortal.Core.Entities;

public class Office : BaseEntity
{
    public string Name { get; set; }
    public List<FlexDesk> Desks { get; set; }
}