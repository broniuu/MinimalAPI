
using MinimalAPI;
public interface IDishService
{
    Task<IEnumerable<DishDto>> GetDishes();
    DishDto GetDish(OrderDto orderDto, Task<IEnumerable<DishDto>> dishDtos);


}
