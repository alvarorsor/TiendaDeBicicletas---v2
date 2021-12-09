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
    public class stocksController : Controller
    {
        private bicitucdbEntities db = new bicitucdbEntities();

        // GET: stocks
        public ActionResult Index(string searchString, string searchString2)
        {
        

  var stocks = db.stocks.Include(s => s.products).Include(s => s.stores);


            if (!String.IsNullOrEmpty(searchString))
            {
                stocks = stocks.Where(s => s.products.product_name.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(searchString2))
            {
                stocks = stocks.Where(s => s.stores.store_name.Contains(searchString2));
            }

            stocks = from stock in stocks
                     orderby stock.quantity
                     select stock;

            stocks.OrderByDescending(stock => stock.quantity);


            return View(stocks.ToList());
        }

        // GET: stocks/Details/5
        public ActionResult Details(int? id1, int? id2)
        {
            if ((id1 & id2) == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // stocks stocks = db.stocks.Find(id);

            stocks stocks = db.stocks.Find(id1, id2);
            if (stocks == null)
            {
                return HttpNotFound();
            }
            return View(stocks);
        }

        // GET: stocks/Create
        public ActionResult Create()
        {
            ViewBag.product_id = new SelectList(db.products, "product_id", "product_name");
            ViewBag.store_id = new SelectList(db.stores, "store_id", "store_name");
            return View();
        }

        // POST: stocks/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "store_id,product_id,quantity")] stocks stocks)
        {
            if (ModelState.IsValid)
            {
                db.stocks.Add(stocks);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.product_id = new SelectList(db.products, "product_id", "product_name", stocks.product_id);
            ViewBag.store_id = new SelectList(db.stores, "store_id", "store_name", stocks.store_id);
            return View(stocks);
        }

        // GET: stocks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            stocks stocks = db.stocks.Find(id);
            if (stocks == null)
            {
                return HttpNotFound();
            }
            ViewBag.product_id = new SelectList(db.products, "product_id", "product_name", stocks.product_id);
            ViewBag.store_id = new SelectList(db.stores, "store_id", "store_name", stocks.store_id);
            return View(stocks);
        }

        // POST: stocks/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "store_id,product_id,quantity")] stocks stocks)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stocks).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.product_id = new SelectList(db.products, "product_id", "product_name", stocks.product_id);
            ViewBag.store_id = new SelectList(db.stores, "store_id", "store_name", stocks.store_id);
            return View(stocks);
        }

        // GET: stocks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            stocks stocks = db.stocks.Find(id);
            if (stocks == null)
            {
                return HttpNotFound();
            }
            return View(stocks);
        }

        // POST: stocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            stocks stocks = db.stocks.Find(id);
            db.stocks.Remove(stocks);
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

/*
        // GET: stocks/CalcularStock
        public ActionResult CalcularStock()
        {


            var stocks = db.stocks.Include(s => s.products).Include(s => s.stores);

       
           
            
                stocks = stocks.Where(s => s.products.product_name.Contains(searchString));
            

            stocks = from stock in stocks
                     count stock.quantity
                     select stock;

            


            return View(stocks.ToList());
        }*/
    }
}
