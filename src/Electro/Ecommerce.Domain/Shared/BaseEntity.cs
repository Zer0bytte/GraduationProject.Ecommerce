namespace Ecommerce.Domain.Shared;

public class BaseEntity
{
    public Guid Id { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.Now;
    public DateTime ModifiedOn { get; set; } = DateTime.Now;

    public void MarkAsDeleted()
    {
        IsDeleted = true;
        ModifiedOn = DateTime.UtcNow;
    }

}