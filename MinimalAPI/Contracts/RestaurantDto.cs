
namespace MinimalAPI;
public class RestaurantDto
{
    public string Name {  get; set; }
    public List<DishDto> Dishes { get; } = new List<DishDto>();

}
