using System;
using Microsoft.AspNetCore.Builder;
using MinimalAPI;



var builder = WebApplication.CreateBuilder();

var app = builder.Build();

app.MapGet("/getRestaurant", () => new RestaurantProvider().GetRestaurant());

app.MapGet("/getDishes", () => new DishService().GetDishes());

app.Run();
