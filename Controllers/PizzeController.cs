using la_mia_pizzeria_model.Models;
using la_mia_pizzeria_model.Data;
using Microsoft.AspNetCore.Mvc;
namespace la_mia_pizzeria_model.Controllers
{
    public class PizzeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {               
            List<Pizze> pizze = new List<Pizze>();

            using (PizzeContext db = new PizzeContext())
            {
                pizze = db.Pizze.ToList<Pizze>();
            }

            return View("HomePage", pizze);
        }

        [HttpGet]

        public IActionResult Details(int id)
        {
            Pizze? pizzaTrovata = null;

            using (PizzeContext db = new PizzeContext())
            {
                pizzaTrovata = db.Pizze.Where(pizze => pizze.Id == id).FirstOrDefault();
            }

            if (pizzaTrovata != null)
            {
                return View("Details", pizzaTrovata);
            }
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
