using MinimalAPI;

public interface IOrderService
{
    Task InsertOrder(DishDto dishDto, string userName, OrderDto orderDto);

    IEnumerable<OrderInformation> GetOrderInformations(PageParameters pageParameters);
}