namespace Application.Interfaces.Property;

public interface IPropertyCheckerService
{
    public bool TypeHasProperties<T>(string? fields);
}