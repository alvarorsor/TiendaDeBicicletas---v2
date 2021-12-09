using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TiendaDeBicicletas;

namespace TiendaDeBicicletas.Controllers
{
    public class ordersController : Controller
    {
        private bicitucdbEntities db = new bicitucdbEntities();

        // GET: orders
        public ActionResult Index(string searchString, string searchString2, string searchString3)
        {
            var orders = db.orders.Include(o => o.customers).Include(o => o.staffs).Include(o => o.stores);

            if (!String.IsNullOrEmpty(searchString))
            {
                orders = orders.Where(s => s.customers.first_name.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(searchString2))
            {
                orders = orders.Where(s => s.stores.store_name.Contains(searchString2));
            }
            

            return View(orders.ToList());
        }

        // GET: orders
        /*  public ActionResult Index()
          {
              var orders = db.orders.Include(o => o.customers).Include(o => o.staffs).Include(o => o.stores);
              return View(orders.ToList());
    }*/

        // GET: orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            orders orders = db.orders.Find(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            return View(orders);
        }

        // GET: orders/Create
        public ActionResult Create()
        {
            ViewBag.customer_id = new SelectList(db.customers, "customer_id", "first_name");
            ViewBag.staff_id = new SelectList(db.staffs, "staff_id", "first_name");
            ViewBag.store_id = new SelectList(db.stores, "store_id", "store_name");
            return View();
        }

        // POST: orders/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "order_id,customer_id,order_status,order_date,required_date,shipped_date,store_id,staff_id")] orders orders)
        {
            if (ModelState.IsValid)
            {
                db.orders.Add(orders);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.customer_id = new SelectList(db.customers, "customer_id", "first_name", orders.customer_id);
            ViewBag.staff_id = new SelectList(db.staffs, "staff_id", "first_name", orders.staff_id);
            ViewBag.store_id = new SelectList(db.stores, "store_id", "store_name", orders.store_id);
            return View(orders);
        }

        // GET: orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            orders orders = db.orders.Find(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            ViewBag.customer_id = new SelectList(db.customers, "customer_id", "first_name", orders.customer_id);
            ViewBag.staff_id = new SelectList(db.staffs, "staff_id", "first_name", orders.staff_id);
            ViewBag.store_id = new SelectList(db.stores, "store_id", "store_name", orders.store_id);
            return View(orders);
        }

        // POST: orders/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "order_id,customer_id,order_status,order_date,required_date,shipped_date,store_id,staff_id")] orders orders)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orders).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.customer_id = new SelectList(db.customers, "customer_id", "first_name", orders.customer_id);
            ViewBag.staff_id = new SelectList(db.staffs, "staff_id", "first_name", orders.staff_id);
            ViewBag.store_id = new SelectList(db.stores, "store_id", "store_name", orders.store_id);
            return View(orders);
        }

        // GET: orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            orders orders = db.orders.Find(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            return View(orders);
        }

        // POST: orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            orders orders = db.orders.Find(id);
            db.orders.Remove(orders);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
