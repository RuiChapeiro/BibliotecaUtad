using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliotecaUtad.Models
{
    public class Librabry
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? LocAddress { get; set; }

        [Required]
        public string? LocPostalCode { get; set; }

        [Required]
        public string? LocCity { get; set; }

        [Required]
        public string? LocCountry { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Phone { get; set; }

        [Required]
        public string? OpeningHours { get; set; }

    }
}
