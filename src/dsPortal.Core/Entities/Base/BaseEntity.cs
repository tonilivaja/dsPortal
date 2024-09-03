namespace dsPortal.Core.Entities;

public abstract class BaseEntity : IEntity
{
    public Guid Id { get; set; }
}