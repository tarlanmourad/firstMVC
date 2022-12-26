using Bootstrap.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bootstrap.Controllers
{
    public class HomeController : Controller
    {
        NorthwindEntities db = new NorthwindEntities();
        // GET: Home
        public ActionResult Index()
        {
            var model = db.Orders.ToList();
            return View(model);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpGet]
        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult New(Orders newOrder)
        {
            if (newOrder.OrderID == 0) db.Orders.Add(newOrder);

            else
            {
                var updateName = db.Orders.Find(newOrder.OrderID);

                if (updateName == null) return HttpNotFound();

                updateName.ShipName = newOrder.ShipName;
            }
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Update(int id)
        {
            var model = db.Orders.Find(id);

            if(model == null) return HttpNotFound();

            return View("New", model);
        }

        public ActionResult Delete(int id)
        {
            var deleteName = db.Orders.Find(id);

            if(deleteName == null) return HttpNotFound();

            db.Orders.Remove(deleteName);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}