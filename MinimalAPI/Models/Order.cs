
using System;

namespace MinimalAPI
{
    public class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int DishId { get; set; }
        public int amount { get; set; }
        public DateTime date { get; set; }
    }
}