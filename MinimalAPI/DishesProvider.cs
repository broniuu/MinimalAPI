
using Microsoft.EntityFrameworkCore;

namespace MinimalAPI;
public class DishesProvider
{
    public IEnumerable<Dish> GetDishes()
    {
        using (var db = new DishContext())
        {
            foreach (var dish in db.DishDatas)
            {
                yield return new Dish { Name = dish.Name, Price = dish.Price, Availability = dish.Availability };
            }

        }
        
    }
}
