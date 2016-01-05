using System.Linq;
using Microsoft.AspNet.Mvc;
using JohnsFirstApp.Models;
using Microsoft.Data.Entity;
using Microsoft.AspNet.Mvc.Rendering;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace JohnsFirstApp.Controllers
{
    public class ItemsController : Controller
    {
        // GET: /<controller>/
        private JohnsFirstAppContext db = new JohnsFirstAppContext();
        public IActionResult Index()
        {
            return View(db.Items.Include(x => x.Category).ToList());
        }
        public IActionResult Details(int id)
        {
            Item item = db.Items.FirstOrDefault(x => x.ItemId == id);
            return View(item);
        }
        public IActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Item item)
        {
            db.Items.Add(item);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            Item item = db.Items.FirstOrDefault(d => d.ItemId == id);
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name");
            return View(item);
        }

        [HttpPost]
        public IActionResult Edit(Item item)
        {
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            Item item = db.Items.FirstOrDefault(x => x.ItemId == id);
            return View(item);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            Item item = db.Items.FirstOrDefault(x => x.ItemId == id);
            db.Items.Remove(item);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
