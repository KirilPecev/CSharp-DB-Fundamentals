﻿namespace Cinema.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Customer
    {
        public Customer()
        {
            this.Tickets = new List<Ticket>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 3)]
        public string LastName { get; set; }

        [Required]
        [Range(12, 110)]
        public int Age { get; set; }

        [Required]
        [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
        public decimal Balance { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
    }
}
