
namespace MinimalAPI;
public interface IFilterService
{
    Filter SetFilterParameters(
        string dishNameReading,
        string maxPriceReading,
        string minPriceReading,
        string restaurantNameReading,
        string availabilityReading, HttpContext http);
    Task<IEnumerable<DishDto>> FilterDishes(
    string dishNameReading,
    string maxPriceReading,
    string minPriceReading,
    string restaurantNameReading,
    string availabilityReading);

}
