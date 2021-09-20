using MinimalAPI;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<ITokenService>(new TokenService());
builder.Services.AddSingleton<IUserRepositoryService>(new UserRepositoryService());
builder.Services.AddSingleton<IRestaurantService>(new RestaurantService());
builder.Services.AddSingleton<IDishService>(new DishService());
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

await using var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.MapGet("/", (Func<string>)(() => "This a demo for JWT Authentication using Minimalist Web API"));

app.MapPost("/login", [AllowAnonymous] async (HttpContext http, ITokenService tokenService, IUserRepositoryService userRepositoryService) => {
    var userModel = await http.Request.ReadFromJsonAsync<UserModel>();
    var userDto = userRepositoryService.GetUser(userModel);
    if (userDto == null)
    {
        http.Response.StatusCode = 401;
        return;
    }

    var token = tokenService.BuildToken(builder.Configuration["Jwt:Key"], builder.Configuration["Jwt:Issuer"], userDto);
    await http.Response.WriteAsJsonAsync(new { token = token });
    //var userName = tokenService.FindRole(token, "UserName");
    return;
});

app.MapPost("/orderdish", ([Authorize] async 
    (HttpContext http, 
    IDishService dishService, 
    IRestaurantService restaurantService, 
    IUserRepositoryService userRepositoryService  ) => {
    
        var userName = http.User.Identity.Name;

        //upserting users to data base
        await userRepositoryService.UpsertUsers();

        var restaurantsDto = restaurantService.GetRestaurant();
        var dishesDto = dishService.GetDishes();

        var dishModel = await http.Request.ReadFromJsonAsync<DishModel>();
        var dishDto = dishService.GetDish(dishModel, dishesDto);
        if (dishDto == null)
        {
            http.Response.StatusCode = 401;
            return;
        }
        return;
    }));


await app.RunAsync();


