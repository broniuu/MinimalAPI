
using Microsoft.EntityFrameworkCore;

using MinimalAPI;
public class RestaurantService : IRestaurantService
{
    public async Task<IEnumerable<RestaurantDto>> GetRestaurants()
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
