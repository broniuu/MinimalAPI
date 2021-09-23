using MinimalAPI;

public interface IRestaurantService
{
    Task<IEnumerable<RestaurantDto>> GetRestaurants(PageParameters pageParameters);
}