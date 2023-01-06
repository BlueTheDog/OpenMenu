namespace Common.Exceptions;
public class OrderByCustomException : Exception
{
    public string Type { get; set; } = "order-by-exception";
    public string Detail { get; set; } = "The OrderBy value is not accepted.";
    public string Title { get; set; } = "OrderBy Exception";
    public OrderByCustomException()
    {
    }
}
