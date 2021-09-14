
using Microsoft.EntityFrameworkCore;

namespace MinimalAPI;
public class RestaurantService
{
    public async Task<IEnumerable<RestaurantDto>> GetRestaurant()
    {
        using (var db = new DishContext())
        {

            var enumerable = (await db.Restaurants.Include(r => r.Dishes)
                .ToListAsync()).Select(Convert);
            return enumerable;
        }
    }
    private RestaurantDto Convert(Restaurant restaurant)
    {
        var restaurantDto = new RestaurantDto
        {
            Name = restaurant.Name,
            Dishes = restaurant.Dishes.Select(ConvertDish)
        };

        return restaurantDto;
    }

    private DishForRestaurantDto ConvertDish(Dish dish)
    {
        return new DishForRestaurantDto { Name = dish.Name} ;
    }
}
