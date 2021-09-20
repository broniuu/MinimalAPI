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
}