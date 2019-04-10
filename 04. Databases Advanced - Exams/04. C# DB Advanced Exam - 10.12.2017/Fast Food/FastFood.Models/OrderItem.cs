namespace FastFood.Models
{
    using System.ComponentModel.DataAnnotations;

    public class OrderItem
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int ItemId { get; set; }
        public Item Item { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
