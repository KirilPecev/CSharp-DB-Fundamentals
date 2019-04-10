namespace PetClinic.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AnimalAid
    {
        public AnimalAid()
        {
            this.AnimalAidProcedures = new List<ProcedureAnimalAid>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [Range(0.01, Double.MaxValue)]
        public decimal Price { get; set; }

        public ICollection<ProcedureAnimalAid> AnimalAidProcedures { get; set; }
    }
}
