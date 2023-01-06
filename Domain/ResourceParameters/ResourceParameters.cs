namespace Domain.ResourceParameters;

abstract public class ResourceParameters
{
    public const int maxPageSize = 20;
    private int _pageSize = 5;
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > maxPageSize ? maxPageSize : value;
    }
    public int PageNumber { get; set; } = 1;
    public string? SearchQuery { get; set; }
    public string OrderBy { get; set; } = "Name";
    public string? Fields { get; set; }
}
