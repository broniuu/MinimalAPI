
using Microsoft.EntityFrameworkCore;

namespace MinimalAPI;
public class RestaurantProvider
{
    public async Task<IEnumerable<RestaurantDto>> GetRestaurant()
    {
        using (var db = new DishContext())
        {
            return (await db.Restaurants.ToListAsync()).Select(Convert);    
        }
    }
    private RestaurantDto Convert(Restaurant restaurant)
    {
        return new RestaurantDto
        {
            Name = restaurant.Name
        };
    }
}
