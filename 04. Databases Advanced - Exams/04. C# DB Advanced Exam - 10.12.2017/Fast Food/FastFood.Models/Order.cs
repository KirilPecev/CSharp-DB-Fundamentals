namespace FastFood.Models
{
    using Enums;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public class Order
    {
        public Order()
        {
            this.OrderItems = new List<OrderItem>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Customer { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public OrderType Type { get; set; }

        [NotMapped]
        public decimal TotalPrice => this.OrderItems.Sum(i => i.Item.Price * i.Quantity);

        [Required]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }

    }
}
