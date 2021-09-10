
namespace MinimalAPI;
public class DishesProvider
{
    public IEnumerable<Dish> GetDishes()
    {
        yield return new Dish { Name = "Dish1", Price = 12 };

        yield return new Dish { Name = "Dish2", Price = 14.12M };
    }
}
