using Microsoft.EntityFrameworkCore;
namespace MinimalAPI;

public class FilterService : IFilterService
{
    public Filter SetFilterParameters(
        string dishNamePattern,
        string maxPricePattern,
        string minPricePattern,
        string restaurantNamePattern,
        string availabilityPattern, 
        HttpContext http)
    {
        string dishNameReading = http.Request.Query[dishNamePattern].ToString();
        string maxPriceReading = http.Request.Query[maxPricePattern].ToString();
        string minPriceReading = http.Request.Query[minPricePattern].ToString();
        string restaurantNameReading = http.Request.Query[restaurantNamePattern].ToString();
        string availabilityReading = http.Request.Query[availabilityPattern].ToString();

        var filter = new Filter();
        if (!String.IsNullOrEmpty(dishNameReading))
        {
            filter.DishName = dishNameReading;
        }
        if (!String.IsNullOrEmpty(maxPriceReading))
        {
            try
            {
                filter.MaxPrice = System.Convert.ToDecimal(maxPriceReading);
            }
            catch (FormatException)
            {
                filter.MaxPrice = 0;
            }
        }
        if (!String.IsNullOrEmpty(minPriceReading))
        {
            try
            {
                filter.MinPrice = System.Convert.ToDecimal(minPriceReading);
            }
            catch (FormatException)
            {
                filter.MinPrice = 0;
            }
        }
        if (!String.IsNullOrEmpty(restaurantNameReading))
        {
            filter.RestaurantName = restaurantNameReading;
        }
        if (!String.IsNullOrEmpty(availabilityReading))
        {
            filter.AvailabilityReading = availabilityReading;
        }
        return filter;
    }

    public async Task<IEnumerable<DishDto>> FilterDishes(
        string dishNameReading,
        string maxPriceReading,
        string minPriceReading,
        string restaurantNameReading,
        string availabilityReading)
    {
        using (var db = new DishContext())
        {
            //       var dishes = System.Linq.Queryable<DishDto>.Where(db.Dishes, FilterDish);
            return (await db.Dishes.Include(d => d.Restaurant)
                .ToListAsync())
                .Where(d => FilterDish(d, dishNameReading, maxPriceReading, minPriceReading, restaurantNameReading, availabilityReading))
                .Select(new DishService().Convert)
                .ToList();
        }
        //var filterDishes = dishDtos;
        //if (!String.IsNullOrEmpty(dishNameReading))
        //{
        //    filterDishes = filterDishes.Where(fd => fd.Name.Contains(dishNameReading));
        //}

        //if (!String.IsNullOrEmpty(dishIdReading))
        //{
        //    int dishId;
        //    try
        //    {
        //        dishId = Int32.Parse(dishIdReading);
        //    }
        //    catch (FormatException)
        //    {
        //        dishId = 0;
        //    }
        //    filterDishes = filterDishes.Where(fd => fd.DishID.Equals(dishId));
        //}

        //if (!(String.IsNullOrEmpty(maxPriceReading) && String.IsNullOrEmpty(minPriceReading)))
        //{
        //    decimal maxPrice = System.Convert.ToDecimal(maxPriceReading);
        //    decimal minPrice = System.Convert.ToDecimal(minPriceReading);
        //    try
        //    {
        //        maxPrice = System.Convert.ToDecimal(maxPriceReading);
        //        minPrice = System.Convert.ToDecimal(minPriceReading);
        //    }
        //    catch (FormatException)
        //    {
        //        maxPrice = 0;
        //        minPrice = 0;
        //    }

        //    filterDishes = filterDishes.Where(fd => fd.Price.Equals(maxPrice));
        //}
        //if (!String.IsNullOrEmpty(restaurantNameReading))
        //{
        //    filterDishes = filterDishes.Where(fd => fd.Restaurant.Name.Contains(restaurantNameReading));
        //}

        //if (!String.IsNullOrEmpty(availabilityReading))
        //{
        //    var availability = (Status)Enum.Parse(typeof(Status), availabilityReading);
        //    filterDishes = filterDishes.Where(fd => fd.Restaurant.Name.Contains(restaurantNameReading));
        //}
        //return filterDishes;
    }
    // TODO: Check work of FilterDish metod 
    private bool FilterDish(Dish dish,
        string dishNameReading,
        string maxPriceReading,
        string minPriceReading,
        string restaurantNameReading,
        string availabilityReading)
    {
        if (!String.IsNullOrEmpty(dishNameReading))
        {
            if (dish.Name.Contains(dishNameReading))
            {
                return true;
            }
        }
        if (!String.IsNullOrEmpty(restaurantNameReading))
        {
            if (dish.Restaurant.Name.Contains(restaurantNameReading))
            {
                return true;
            }
        }
        if (!String.IsNullOrEmpty(minPriceReading))
        {
            decimal minPrice = System.Convert.ToDecimal(minPriceReading);
            if (dish.Price >= minPrice)
            {
                return true;
            }
        }
        if (!String.IsNullOrEmpty(availabilityReading))
        {
            //if (availabilityReading == "avalible")
            //if (availabilityReading == "unavalible")
            //if (availabilityReading == "avalibleAtSelectedTimes")
            //if (availabilityReading == "temporarilyUnavailable")

        }
        return false;
    }
}