using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BikeDistributor;
using BikeDistributor.Models;
using System.Data.Entity.Validation;

namespace BikeDistributor.Controllers
{
    public class PlaceOrderController : Controller
    {
        private BikeDistEntities db = new BikeDistEntities();

        public SelectListItem[] Bikes { get; private set; }

        // GET: PlaceOrderViewModels
        public ActionResult Index()
        {
            return View(db.PlaceOrderViewModels.ToList());
        }

        // GET: PlaceOrderViewModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlaceOrderViewModel placeOrderViewModel = db.PlaceOrderViewModels.Find(id);
            if (placeOrderViewModel == null)
            {
                return HttpNotFound();
            }
            return View(placeOrderViewModel);
        }

        // GET: PlaceOrderViewModels/Create
        public ActionResult Create()
        {
            //Create db context object here 
           BikeDistEntities dbContext = new BikeDistEntities();
            //Get the value from database and then set it to ViewBag to pass it View
            IEnumerable<SelectListItem> brandsInStock = dbContext.Bike.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Brand

            });
            ViewBag.ListOfBrands = new SelectList(brandsInStock,"Value", "Text");
            var placeOrderViewModel = new PlaceOrderViewModel();
            return View(placeOrderViewModel);
        }

        public ActionResult GetBikeModelsFromBrand(string brand)
        {
            BikeDistEntities dbContext = new BikeDistEntities();
            IEnumerable<Bike> modelsInStock = dbContext.Bike.Where(x => x.Brand == brand);
            var listOfModels = new List<string>();
            foreach(var model in modelsInStock)
            {
                listOfModels.Add(model.Model);
            }
            return Json(listOfModels, JsonRequestBehavior.AllowGet);
        }

        public string GetPriceFromModel(string model)
        {
            BikeDistEntities dbContext = new BikeDistEntities();
            Bike bikeModel = dbContext.Bike.Where(x => x.Model == model).FirstOrDefault();

            return bikeModel.Price.ToString();
        }

        // POST: PlaceOrderViewModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Model,Brand,BikeSize,PaymentMethod,Price,Quantity")] PlaceOrderViewModel placeOrderViewModel)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    placeOrderViewModel.Order.Company.Name = placeOrderViewModel.Brand;
                    placeOrderViewModel.Order.Company.PhoneNumber = "555-555-1212";
                    placeOrderViewModel.Order.Company.PrimaryContact = "Test Primary Contact";

                    placeOrderViewModel.Order.PaymentMethod = placeOrderViewModel.PaymentMethod;
                    placeOrderViewModel.Order.Receipt = placeOrderViewModel.Order.GetReceipt(placeOrderViewModel, placeOrderViewModel.Quantity);

                    placeOrderViewModel.Receipt = null;
                    placeOrderViewModel.Receipt = placeOrderViewModel.Order.Receipt;
                    placeOrderViewModel.Receipt.OrderNumber = placeOrderViewModel.Order.OrderNumber;
                    db.PlaceOrderViewModels.Add(placeOrderViewModel);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                var errors = ModelState.Values.SelectMany(v => v.Errors);
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }



            return View(placeOrderViewModel);
        }

        // GET: PlaceOrderViewModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlaceOrderViewModel placeOrderViewModel = db.PlaceOrderViewModels.Find(id);
            if (placeOrderViewModel == null)
            {
                return HttpNotFound();
            }
            return View(placeOrderViewModel);
        }

        // POST: PlaceOrderViewModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id")] PlaceOrderViewModel placeOrderViewModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(placeOrderViewModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(placeOrderViewModel);
        }

        // GET: PlaceOrderViewModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlaceOrderViewModel placeOrderViewModel = db.PlaceOrderViewModels.Find(id);
            if (placeOrderViewModel == null)
            {
                return HttpNotFound();
            }
            return View(placeOrderViewModel);
        }

        // POST: PlaceOrderViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PlaceOrderViewModel placeOrderViewModel = db.PlaceOrderViewModels.Find(id);
            db.PlaceOrderViewModels.Remove(placeOrderViewModel);
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
