using MinimalAPI;
public interface IRestaurantService
{
    Task<IEnumerable<RestaurantDto>> GetRestaurant();

}
