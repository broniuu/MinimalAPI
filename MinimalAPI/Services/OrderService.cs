using Microsoft.EntityFrameworkCore;
using MinimalAPI;
using System.Globalization;

public class OrderService : IOrderService
{
    public Task InsertOrder(DishDto dishDto, string userName, OrderDto orderDto)
    {   
        using (var db = new DishContext())
        {
            var user = db.Users.FirstOrDefault(u => u.Name == userName);
            var order = db.Add(entity: new Order
            {
                UserId = user.UserId,
                DishId = dishDto.DishID,
                Amount = orderDto.Amount,
                Date = DateTime.Now
            }
                ).Entity;
            db.SaveChanges();
        }
        return Task.CompletedTask;
    }

    public IEnumerable<OrderInformation> GiveOrderInformations()
    {
        var orderInformations = new List<OrderInformation>();
        using (var db = new DishContext())
        { 
            var orders = db.Orders.Where(o => o.Date.Day.Equals(DateTime.Today.Day) 
            && o.Date.Month.Equals(DateTime.Today.Month)
            && o.Date.Year.Equals(DateTime.Today.Year));
            foreach(var order in orders)
            {
                var userId = order.UserId;
                var userName = db.Users.FirstOrDefault(u => userId == u.UserId).Name;
                var dishId = order.DishId;
                var currentDish = db.Dishes.FirstOrDefault(d => dishId == d.DishId);
                var dishName = currentDish.Name;
                var price = currentDish.Price;
                var restaurantId = currentDish.RestaurantId;
                var restaurantName = db.Restaurants.FirstOrDefault(r => restaurantId == r.RestaurantId).Name;

                orderInformations.Add( new OrderInformation()
                {
                    UserId = userId,
                    UserName = userName,
                    DishId = dishId,
                    DishName = dishName,
                    Price = price,
                    RestaurantId = restaurantId,
                    RestaurantName = restaurantName,

                }
                    );
            }

        }
        return orderInformations;
    }
}