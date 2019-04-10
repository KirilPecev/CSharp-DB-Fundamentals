namespace Cinema.Data.Models
{
    using Enums;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Movie
    {
        public Movie()
        {
            this.Projections = new List<Projection>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 3)]
        public string Title { get; set; }

        [Required]
        public Genre Genre { get; set; }

        [Required]
        public TimeSpan Duration { get; set; }

        [Required]
        [Range(1, 10)]
        public double Rating { get; set; }

        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 3)]
        public string Director { get; set; }

        public ICollection<Projection> Projections { get; set; }
    }
}
