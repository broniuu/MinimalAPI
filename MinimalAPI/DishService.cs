
using Microsoft.EntityFrameworkCore;

namespace MinimalAPI;
public class DishService
{
    public async Task<IEnumerable<DishDto>> GetDishes()
    {
        using (var db = new DishContext())
        { 
            return (await db.DishDatas.ToListAsync()).Select(Convert);
        }
        
    }
    private DishDto Convert(DishData dish)
    {
        return new DishDto
        {
            Name = dish.Name,
            Price = dish.Price,
            Availability = dish.Availability,
            Restaurant = new RestaurantDto
            {
                Name = dish.Restaurant.Name
            }
        };
    }
}
