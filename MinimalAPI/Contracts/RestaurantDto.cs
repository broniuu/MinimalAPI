
namespace MinimalAPI;
public class RestaurantDto
{
    public string Name {  get; set; }
    public IEnumerable<DishForRestaurantDto> Dishes { get; set; }

}
