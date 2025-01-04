using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliotecaUtad.Models
{
    public class BookViewModel
    {
        [Key]
        [RegularExpression(@"^\d+$", ErrorMessage = "ISBN deve conter apenas números.")]
        public string ISBN { get; set; }

        [Required(ErrorMessage = "O campo Título é obrigatório.")]
        [StringLength(100, ErrorMessage = "O campo Título deve ter no máximo 100 caracteres.")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "O campo Autor é obrigatório.")]
        [StringLength(100, ErrorMessage = "O campo Autor deve ter no máximo 100 caracteres.")]
        public string? Author { get; set; }

        [Required(ErrorMessage = "O campo Editora é obrigatório.")]
        [StringLength(100, ErrorMessage = "O campo Editora deve ter no máximo 100 caracteres.")]
        public string? Editor { get; set; }

        [Required(ErrorMessage = "O campo Nº Exemplares é obrigatório.")]
        public int? N_Copies { get; set; }

        [Required(ErrorMessage = "O campo Imagem de Capa é obrigatório.")]
        public IFormFile? CoverImage { get; set; }

        [StringLength(1000, ErrorMessage = "O campo Sínopse deve ter no máximo 1000 caracteres.")]
        public string? Summary { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? LaunchDate { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime AquisitionDate { get; set; }

        // Propriedades de navegação para Gender e Subgender
        [ForeignKey("Gender")]
        public int GenderId { get; set; }
        public Gender? Gender { get; set; }

        [ForeignKey("Subgender")]
        public int SubGenderId { get; set; }
        public Subgender? Subgender { get; set; }
    }
}
