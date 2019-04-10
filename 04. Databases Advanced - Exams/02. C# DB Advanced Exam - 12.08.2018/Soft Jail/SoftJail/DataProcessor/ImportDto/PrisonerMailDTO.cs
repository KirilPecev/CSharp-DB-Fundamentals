namespace SoftJail.DataProcessor.ImportDto
{
    using System.ComponentModel.DataAnnotations;

    public class PrisonerMailDTO
    {
        [Required(AllowEmptyStrings = false)]
        [StringLength(maximumLength: 20, MinimumLength = 3)]
        public string FullName { get; set; }

        [Required]
        [RegularExpression("^The ([A-Z]{1}[a-z]+)$")]
        public string Nickname { get; set; }

        [Required]
        [Range(18, 65)]
        public int Age { get; set; }

        [Required]
        public string IncarcerationDate { get; set; }

        public string ReleaseDate { get; set; }

        [Range(0.0, double.MaxValue)]
        public decimal? Bail { get; set; }

        public int? CellId { get; set; }

        public MailDTO[] Mails { get; set; }
    }
}
