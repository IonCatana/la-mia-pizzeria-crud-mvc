namespace la_mia_pizzeria_model.Models
{
    public class PizzeCategory
    {
        internal object Category;

        public Pizze Pizze { get; set; }
        public List<Category>? Categories { get; set; }
    }
}
