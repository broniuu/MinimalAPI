
namespace MinimalAPI;
public interface IDishService
{
    Task<IEnumerable<DishDto>> GetDishes();
    DishDto GetDish(DishModel dishModel, IEnumerable<DishDto> dishDtos);


}
