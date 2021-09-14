namespace MinimalAPI
{
    public class DishDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Status Availability { get; set; }
        public RestaurantForDishDto Restaurant { get; set; }
    }
}