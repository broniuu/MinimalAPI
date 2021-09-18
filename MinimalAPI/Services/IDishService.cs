
using MinimalAPI;
public interface IDishService
{
    Task<IEnumerable<DishDto>> GetDishes();
    DishDto GetDish(DishModel dishModel, Task<IEnumerable<DishDto>> dishDtos);


}
