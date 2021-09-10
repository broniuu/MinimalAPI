using System;
using Microsoft.AspNetCore.Builder;
using MinimalAPI;



var builder = WebApplication.CreateBuilder();

var app = builder.Build();

app.MapGet("/getDishes", () => new DishesProvider().GetDishes());

app.Run();
