namespace SoftJail.DataProcessor.ImportDto
{
    using System.ComponentModel.DataAnnotations;

    public class DepartmentCellDTO
    {
        [Required(AllowEmptyStrings = false)]
        [StringLength(maximumLength: 25, MinimumLength = 3)]
        public string Name { get; set; }

        public CellDTO[] Cells { get; set; }
    }
}
