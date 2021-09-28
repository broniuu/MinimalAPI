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
            Restaurant = new RestaurantForDishDto
            {
                Name = dish.Restaurant.Name
            }
        };
    }

    public DishDto GetDish(OrderDto orderDto, Task<IEnumerable<DishDto>> dishDtos)
    {
        return dishDtos.Result.ToList().FirstOrDefault(d => Equals(d.DishID, orderDto.DishId));
    }
    public async Task<IEnumerable<DishDto>> FilterDishes(
        Task<IEnumerable<DishDto>> dishDtos,
        string dishNameReading,
        string dsihIdReading,
        string maxPriceReading,
        string minPriceReading,
        string restaurantNameReading,
        string restaurantIdReading,
        string availabilityReading)
    {
        var filterDishes = dishDtos.Result.ToList().AsEnumerable();
        if (!String.IsNullOrEmpty(dishNameReading))
        {
            filterDishes = filterDishes.Where(fd => fd.Name.Contains(dishNameReading));
        }
    }
    private IEnumerable<DishDto> UseSingielFilter(IEnumerable<DishDto> dishes, string namedishAtribute, string reading)
    {
        if (!String.IsNullOrEmpty(reading))
        {
            return dishes.Where(dishAtribute.Contains(reading));
        }
        return dishes;
    }
}
