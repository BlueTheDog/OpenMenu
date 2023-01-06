namespace Common.Exceptions;
public class DataShapingCustomException : Exception
{
    public string Type { get; set; } = "data-shaping-exception";
    public string Detail { get; set; } = "Not all requested data shaping fields exist on the resource.";
    public string Title { get; set; } = "DataShaping Exception";
    public DataShapingCustomException()
    {
    }
}