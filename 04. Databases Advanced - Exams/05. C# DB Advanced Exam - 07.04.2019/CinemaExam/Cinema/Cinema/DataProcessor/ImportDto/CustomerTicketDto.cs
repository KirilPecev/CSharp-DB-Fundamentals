namespace Cinema.DataProcessor.ImportDto
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    [XmlType("Customer")]
    public class CustomerTicketDto
    {

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

        [XmlArray]
        public TicketDto[] Tickets { get; set; }
    }
}
