using System;
using Microsoft.AspNetCore.Builder;
using MinimalAPI;



var builder = WebApplication.CreateBuilder(args);

await using var app = builder.Build();



app.MapGet("/getRestaurant", () => new RestaurantService().GetRestaurant());

app.MapGet("/getDishes", () => new DishService().GetDishes());

app.Run();
