
using Microsoft.EntityFrameworkCore;

namespace MinimalAPI;
public class DishService : IDishService
{
    public async Task<IEnumerable<DishDto>> GetDishes(PageParameters pageParameters)
    {
        using (var db = new DishContext())
        {
            return (await db.Dishes.Include(d => d.Restaurant)
                .Skip((pageParameters.PageNumber - 1) * pageParameters.PageSize)
                .Take(pageParameters.PageSize)
                .ToListAsync()).Select(Convert)
                .ToList();

        }
        
    }
    public async Task<IEnumerable<DishDto>> GetAllDishes()
    {
        using (var db = new DishContext())
        {
            return (await db.Dishes.Include(d => d.Restaurant)
                .ToListAsync()).Select(Convert);
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
            DishID = dish.DishId,
            Restaurant = new RestaurantForDishDto {
                Name = dish.Restaurant.Name
            }
        };
    }

    public DishDto GetDish(OrderDto orderDto, Task<IEnumerable<DishDto>> dishDtos)
    {
        return dishDtos.Result.ToList().FirstOrDefault(d => Equals(d.DishID, orderDto.DishId));
    }
}
