﻿namespace PetClinic.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class Procedure
    {
        public Procedure()
        {
            this.ProcedureAnimalAids = new List<ProcedureAnimalAid>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public int AnimalId { get; set; }
        public Animal Animal { get; set; }

        [Required]
        public int VetId { get; set; }
        public Vet Vet { get; set; }

        public decimal Cost => this.ProcedureAnimalAids.Sum(p => p.AnimalAid.Price);

        [Required]
        public DateTime DateTime { get; set; }
        public ICollection<ProcedureAnimalAid> ProcedureAnimalAids { get; set; }
    }
}
