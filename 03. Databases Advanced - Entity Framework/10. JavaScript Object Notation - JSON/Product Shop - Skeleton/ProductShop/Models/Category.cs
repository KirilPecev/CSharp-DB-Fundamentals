namespace ProductShop.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Category
    {
        public Category()
        {
            this.CategoryProducts = new List<CategoryProduct>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [Range(3,15)]
        public string Name { get; set; }

        public ICollection<CategoryProduct> CategoryProducts { get; set; }
    }
}
