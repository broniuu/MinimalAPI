
using Microsoft.EntityFrameworkCore;

namespace MinimalAPI;
public class DishService : IDishService
{
    public async Task<IEnumerable<DishDto>> GetDishes()
    {
        using (var db = new DishContext())
        {
            return (await db.Dishes.Include(d => d.Restaurant).ToListAsync()).Select(Convert);

        }
        
    }
    private DishDto Convert(Dish dish)
    {
        var name = dish.Restaurant;
        return new DishDto
        {
            Name = dish.Name,
            Price = dish.Price,
            Availability = dish.Availability,
            Restaurant = new RestaurantForDishDto {
                Name = dish.Restaurant.Name
            }
        };
    }

    public DishDto GetDish(DishModel dishModel, Task<IEnumerable<DishDto>> dishDtos)
    {
        return dishDtos.Result.ToList().FirstOrDefault(d => Equals(d.DishID, dishModel.DishId));
    }
}
