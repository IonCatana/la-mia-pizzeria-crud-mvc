using la_mia_pizzeria_model.Models;

namespace la_mia_pizzeria_model.Utilities
{
    public class PizzeData
    {
        private static List<Pizze> pizze;


        public static List<Pizze> GetPizze()
        {
            if (PizzeData.pizze != null)
            {
                return PizzeData.pizze;
            }
            List<Pizze> nuovaListaPizze = new List<Pizze>();

            for (int i = 0; i < nuovaListaPizze.Count; i++)
            {
                Pizze Margherita = new Pizze("~/img/margherita.ipg" + i, "Titolo Pizza: " + i, "Pomodoro San Marzano D.O.P., fior di latte di Agerola, basilico e olio evo.", "prezzo");                
                nuovaListaPizze.Add(Margherita);
                
                
            }
            PizzeData.pizze = nuovaListaPizze;
            return PizzeData.pizze;
        }
    }
}
