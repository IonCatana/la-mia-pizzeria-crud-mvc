using la_mia_pizzeria_model.Models;
using la_mia_pizzeria_model.Data;
using Microsoft.AspNetCore.Mvc;
namespace la_mia_pizzeria_model.Controllers
{
    public class PizzeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {   // Aggiungo il nuovo sistema che abbiamo imparato con i DB            
            List<Pizze> pizze = new List<Pizze>();

            using (PizzeContext db = new PizzeContext())
            {
                pizze = db.Pizze.ToList<Pizze>();
            }
            //Il controller dice le liste e il modello
            //Il controller si chiama la lista delle pizze con il metodo GetPizze()
            //Poi passo un razor, quindi insertisco
            return View("HomePage", pizze);
        }
        //Creo una nuova pagina, quindi per prima cosa creo un metodo nuovo
        [HttpGet]
        //Il metodo servira per una determinata pizza descrizione quindi inserisco l'id, il parametro
        public IActionResult Details(int id)
        {
            //dichiaro un oggetto temporaneo che sará null per ora
            Pizze? pizzaTrovata = null;

            using (PizzeContext db = new PizzeContext())
            {
                pizzaTrovata = db.Pizze.Where(pizze => pizze.Id == id).FirstOrDefault();
            }
            //Se la pizza é stata trovata e quindi e diverso da null, faccio un return vista "Details" 
            if (pizzaTrovata != null)
            {
                return View("Details", pizzaTrovata);
            }
            //altrimenti se non é stata trovata inivio un messaggio di errore che segna anche l'id
            else
            {
                return NotFound("La pizza con l'id " + id + "non è stato trovato");
            }
        }
        //Creo un metodo per aggiungere pizze alla mia pizzeria da parte dell'utente

        [HttpGet]
        public IActionResult Create()
        {
            return View("FormPizze");
        }

        //Inserisco Il HttpPost e inserisco il validation per evitare gli hacker
        [HttpPost]
        [ValidateAntiForgeryToken]
        //Creo un metodo chiamato nuovaPizza per aggiungere le pizze, aggiungere il model
        public IActionResult Create(Pizze nuovaPizza)
        {
            //se il modello non é valido ritorniamo una view
            if (!ModelState.IsValid)
            {
                return View("FormPizze", nuovaPizza);
            }

            using (PizzeContext db = new PizzeContext())
            {
                Pizze pizzaDaCreare = new Pizze(nuovaPizza.Immagine, nuovaPizza.Nome, nuovaPizza.Descrizione, nuovaPizza.Prezzo);
                //Se il modello é coretto prendiamo la lista Pizze e il metodo get che aggiungerá questa pizza alla lista
                db.Pizze.Add(nuovaPizza);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            Pizze pizzaDaModificare = null;

            using (PizzeContext db = new PizzeContext())
            {
                pizzaDaModificare = db.Pizze.Where(pizze => pizze.Id == id).FirstOrDefault();
            }

            if (pizzaDaModificare == null)
            {
                return NotFound();
            }
            else
            {
                return View("Update", pizzaDaModificare);
            }

        }

        [HttpPost]
        public IActionResult Update(int id, Pizze model)
        {
            if (!ModelState.IsValid)
            {
                return View("Update", model);
            }

            Pizze pizzaDaModificare = null;

            using (PizzeContext db = new PizzeContext())
            {
                pizzaDaModificare = db.Pizze.Where(pizze => pizze.Id == id).FirstOrDefault();


                if (pizzaDaModificare != null)
                {
                    pizzaDaModificare.Nome = model.Nome;
                    pizzaDaModificare.Descrizione = model.Descrizione;
                    pizzaDaModificare.Immagine = model.Immagine;
                    pizzaDaModificare.Prezzo = model.Prezzo;

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound();
                }
            }

        }


        [HttpPost]
        public IActionResult Delete(int id)
        {
            using (PizzeContext db = new PizzeContext())
            {
                Pizze pizzaDaEliminare = db.Pizze.Where(pizze => pizze.Id == id).FirstOrDefault();

                if (pizzaDaEliminare != null)
                {
                    db.Pizze.Remove(pizzaDaEliminare);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound();
                }
            }


        }

    }

}
