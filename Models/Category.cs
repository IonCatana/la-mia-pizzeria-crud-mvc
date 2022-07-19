using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_model.Models
{
    public class Category
    {
        [Key]
        [Required(ErrorMessage = "Il campo é obbligatorio")]
        public int Id { get; set; }
        [StringLength(75, ErrorMessage = "Il titolo della categoria non puó superare i 75 caratteri")]
        public string NomeCategoria { get; set; }
        public List<Pizze> Pizze { get; set; }

        public Category()
        {

        }
    }
}
