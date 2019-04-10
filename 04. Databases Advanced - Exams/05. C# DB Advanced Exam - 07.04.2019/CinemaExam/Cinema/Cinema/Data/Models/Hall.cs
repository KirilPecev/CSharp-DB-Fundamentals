namespace Cinema.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Hall
    {
        public Hall()
        {
            this.Projections = new List<Projection>();
            this.Seats = new List<Seat>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        public bool Is4Dx { get; set; }

        [Required]
        public bool Is3D { get; set; }

        public ICollection<Projection> Projections { get; set; }

        public ICollection<Seat> Seats { get; set; }
    }
}
