public record OrderDto
{
    [Required]
    public int DishId {  get; set; }

    [Required]
    public int Amount {  get; set; }
}
