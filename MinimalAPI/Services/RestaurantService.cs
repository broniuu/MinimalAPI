
using Microsoft.EntityFrameworkCore;

using MinimalAPI;
public class RestaurantService : IRestaurantService
{
    public async Task<IEnumerable<RestaurantDto>> GetRestaurants(PageParameters pageParameters)
    {
        using (var db = new DishContext())
        {

            var enumerable = (await db.Restaurants
                .Skip((pageParameters.PageNumber - 1) * pageParameters.PageSize)
                .Take(pageParameters.PageSize)
                .Include(r => r.Dishes)
                .ToListAsync())
                .Select(Convert);
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
