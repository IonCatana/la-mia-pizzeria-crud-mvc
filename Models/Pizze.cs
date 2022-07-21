using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace la_mia_pizzeria_model.Models
{
    public class Pizze
    {
        internal object CategoryId;

        //Inserisco required che obbliga all'utente di inserire quel campo o una determinata informazione, se non li rispetta non puó andare avanti
        [Key]
        public int Id { get; internal set; }

        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [Url(ErrorMessage = "L'Url inserito non è valido")]
        public string Immagine { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [StringLength(20, ErrorMessage = "Il nome non può avere più di 20 caratteri")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [StringLength(100, ErrorMessage = "Il nome non può avere più di 100 caratteri")]        
        public string Descrizione { get; set; }

        [Required(ErrorMessage = "Il campo Prezzo è obbligatorio")]
        [Range(0.01,30, ErrorMessage = "Inserisci un prezzo Valido, il minimo é 0.01€ e massimo é 30€")]
        public string Prezzo { get; set; }
        public int? CategoriId { get; set; }
        public Category? Category { get; set; }        

        public Pizze()
        {

        }       
        
        public Pizze(string immagine, string nome, string descrizione, string prezzo)
        {
            this.Immagine = immagine;
            this.Nome = nome;
            this.Descrizione = descrizione;
            this.Prezzo = prezzo;
        }
    }
}
