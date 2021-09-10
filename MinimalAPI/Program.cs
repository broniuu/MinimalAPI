using System;
using Microsoft.AspNetCore.Builder;
using MinimalAPI;

using (var db = new DishContext())
{

}

var builder = WebApplication.CreateBuilder();

var app = builder.Build();

app.MapGet("/getDishes", () => new DishesProvider().GetDishes());

app.Run();
