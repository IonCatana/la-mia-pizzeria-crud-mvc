using la_mia_pizzeria_model.Models;
using la_mia_pizzeria_model.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            using (PizzeContext db = new PizzeContext())
            {
                try
                {
                    Pizze pizzaTrovata = db.Pizze
                             .Where(pizze => pizze.Id == id).Include(Pizze => Pizze.Category)
                             .FirstOrDefault();

                    return View("Details", pizzaTrovata);

                }
                catch (InvalidOperationException ex)
                {
                    return NotFound("La pizza con id " + id + " non è stata trovata");
                }
                catch (Exception ex)
                {
                    return BadRequest();
                }
            }
        }
        //Creo un metodo per aggiungere pizze alla mia pizzeria da parte dell'utente

        [HttpGet]
        public IActionResult Create()
        {
            using (PizzeContext db = new PizzeContext())
            {
                List<Category> categorie = db.Category.ToList();

                PizzeCategorie model = new PizzeCategorie();
                model.Pizze = new Pizze();
                model.Categorie = categorie;
                return View("FormPizze", model);
            }
        }

        //Inserisco Il HttpPost e inserisco il validation per evitare gli hacker
        [HttpPost]
        [ValidateAntiForgeryToken]
        //Creo un metodo chiamato nuovaPizza per aggiungere le pizze, aggiungere il model
        public IActionResult Create(PizzeCategorie data)
        {
            //se il modello non é valido ritorniamo una view
            if (!ModelState.IsValid)
            {
                using (PizzeContext db = new PizzeContext())
                {
                    List<Category> categorie = db.Category.ToList();
                    data.Categorie = categorie;
                }
                return View("FormPizze", data);
            }

            using (PizzeContext db = new PizzeContext())
            {
                Pizze nuovaPizza = new Pizze();
                nuovaPizza.Nome = data.Pizze.Nome;
                nuovaPizza.Descrizione = data.Pizze.Descrizione;
                nuovaPizza.Immagine = data.Pizze.Immagine;
                nuovaPizza.Prezzo = data.Pizze.Prezzo;
                nuovaPizza.CategoryId = data.Pizze.CategoryId;

                db.Pizze.Add(nuovaPizza);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            Pizze? pizzaDaModificare = null;
            List<Category> categorie = new List<Category>();

            using (PizzeContext db = new PizzeContext())
            {
                pizzaDaModificare = db.Pizze.Where(pizze => pizze.Id == id).FirstOrDefault();
                categorie = db.Category.ToList<Category>();
            }

            if (pizzaDaModificare == null)
            {
                return NotFound();
            }
            else
            {
                PizzeCategorie model = new PizzeCategorie();
                model.Pizze = pizzaDaModificare;
                model.Categorie = categorie;
                return View("Update", model);
            }

        }

        [HttpPost]
        public IActionResult Update(int id, PizzeCategorie model)
        {
            if (!ModelState.IsValid)
            {
                using (PizzeContext db = new PizzeContext())
                {
                    List<Category> categorie = db.Category.ToList();
                    model.Categorie = categorie;
                }
                return View("Update", model);
            }

            Pizze? pizzaDaModificare = null;

            using (PizzeContext db = new PizzeContext())
            {
                pizzaDaModificare = db.Pizze.Where(pizze => pizze.Id == id).FirstOrDefault();


                if (pizzaDaModificare != null)
                {
                    pizzaDaModificare.Nome = model.Pizze.Nome;
                    pizzaDaModificare.Descrizione = model.Pizze.Descrizione;
                    pizzaDaModificare.Immagine = model.Pizze.Immagine;
                    pizzaDaModificare.Prezzo = model.Pizze.Prezzo;
                    pizzaDaModificare.CategoryId = model.Pizze.CategoryId;

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

                    return RedirectToAction("Index", "Pizze");
                }
                else
                {
                    return NotFound();
                }
            }


        }

    }

}
