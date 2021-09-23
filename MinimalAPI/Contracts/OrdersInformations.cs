
namespace MinimalAPI;
public class OrderInformation
{
    public int UserId { get; set; }
    public string UserName {  get; set; }
    public int DishId {  get; set; }
    public string DishName {  get; set;}
    public Decimal Price { get; set; }
    public int RestaurantId { get; set; }
    public string RestaurantName {  get; set; }
}
