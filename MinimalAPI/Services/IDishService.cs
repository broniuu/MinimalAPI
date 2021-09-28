using MinimalAPI;

public interface IDishService
{
    Task<IEnumerable<DishDto>> GetDishes(PageParameters pageParameters);

    Task<IEnumerable<DishDto>> GetAllDishes();

    DishDto GetDish(OrderDto orderDto, Task<IEnumerable<DishDto>> dishDtos);

}