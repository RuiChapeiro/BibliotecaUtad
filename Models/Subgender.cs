using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliotecaUtad.Models
{
    public class Subgender
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubGenderId { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public string? SubGenderName { get; set; }

        // Chave estrangeira dos géneros
        [ForeignKey("Gender")]
        public int GenderId { get; set; }

        // Propriedade de navegação
        public Gender? Gender { get; set; }
    }
}
