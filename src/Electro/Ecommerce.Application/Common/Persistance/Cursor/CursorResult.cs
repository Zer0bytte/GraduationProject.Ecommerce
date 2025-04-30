namespace Ecommerce.Application.Common.Persistance.Cursor;
public class CursorResult<TModel>
{
    public IList<TModel> Items { get; set; } = [];
    public string? Cursor { get; set; }
    public bool HasMore { get; set; }
}