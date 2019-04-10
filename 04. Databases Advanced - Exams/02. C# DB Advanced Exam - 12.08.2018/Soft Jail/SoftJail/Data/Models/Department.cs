namespace SoftJail.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Department
    {
        public Department()
        {
            this.Cells = new HashSet<Cell>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength:25, MinimumLength = 3)]
        public string Name { get; set; }

        public ICollection<Cell> Cells { get; set; }
    }
}
