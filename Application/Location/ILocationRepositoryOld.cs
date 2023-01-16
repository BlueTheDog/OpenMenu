//using Common.Helpers;
//using Domain.Location;
//using Domain.ResourceParameters;

//namespace Application.Location;

//public interface ILocationRepository
//{
//    Task<LocationEntity> GetLocationAsync(int locationId);
//    Task<PagedList<LocationEntity>> GetLocationsAsync(LocationResourceParameters locationResourceParameters);
//    Task<IEnumerable<LocationEntity>> GetLocationsAsync(IEnumerable<int> locationIds);
//    void AddLocation(LocationEntity Location);
//    void DeleteLocation(LocationEntity Location);
//    Task<bool> LocationExists(int locationId);
//    Task<bool> SaveChangesAsync();
//}