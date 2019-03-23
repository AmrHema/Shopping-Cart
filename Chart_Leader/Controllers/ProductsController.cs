using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Chart_Leader.Models;

namespace Chart_Leader.Controllers
{
    [Authorize(Roles = "admin")]
    [HandleError]
    public class ProductsController : BaseController
    {
        [AllowAnonymous]
        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Categories);
            return View(products.ToList());
        }
        // GET: Products Searche && Pagination
        public ActionResult ProductDatatable()
        {
            var products = db.Products.Include(p => p.Categories);
            return View(products.ToList());
        }
        public ActionResult GetProductsPrint()
        {
            var products = db.Products.ToList<Products>();
            return Json(new { data = products }, JsonRequestBehavior.AllowGet);
        }
        // GET: Products
        public ActionResult Last4Products()
        {
            var products = db.Products.Include(p => p.Categories).OrderBy(x => x.Product_id).Take(3);
            return View(products.ToList());
        }
        [AllowAnonymous]
        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.Cat_id = new SelectList(db.Categories, "Cat_id", "Cat_Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Products products, HttpPostedFileBase ImageUpload)
        {

            int idProduct = db.Products.OrderByDescending(x => x.Product_id).Select(x => x.Product_id).FirstOrDefault() + 1;
            if (ImageUpload != null)
            {
                if (ValidateFile(ImageUpload))
                {
                    try
                    {
                        string imgProductName = products.Cat_id + "-" + idProduct + ".jpg";
                        products.Product_Image = "~/Common/Images/" + imgProductName;
                        ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Common/Images/"), imgProductName));
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError("FileName", "Sorry an error occurred saving the file to disk, please try again");

                    }
                }
                else
                {
                    ModelState.AddModelError("FileName", "The file must be gif, png, jpeg or jpg and less than 5MB in size");
                }
            }
            else
            {
                products.Product_Image = "~/Common/Images/uploade.jpg";
                //if the user has not entered a file return an error message
                // ModelState.AddModelError("FileName", "Please choose a file");
            }


            if (ModelState.IsValid)
            {
                db.Products.Add(products);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Cat_id = new SelectList(db.Categories, "Cat_id", "Cat_Name", products.Cat_id);
            return View(products);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Find(id);

            if (products == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cat_id = new SelectList(db.Categories, "Cat_id", "Cat_Name", products.Cat_id);
            return View(products);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Products products, HttpPostedFileBase ImageUpload)
        {
            if (ImageUpload != null)
            {
                string imgProductName = products.Cat_id + "-" + products.Product_id + ".jpg";
                products.Product_Image = "~/Common/Images/" + imgProductName;
                ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Common/Images/"), imgProductName));
            }
            else
            {
                products.Product_Image = "~/Common/Images/uploade.jpg";
            }
            if (ModelState.IsValid)
            {
                db.Entry(products).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Cat_id = new SelectList(db.Categories, "Cat_id", "Cat_Name", products.Cat_id);
            return View(products);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Products products = db.Products.Find(id);
            db.Products.Remove(products);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        public ActionResult caresoul()
        {
            var last3products = db.Products.OrderByDescending(x => x.Product_id).Take(3).ToList<Products>();
            return View(last3products);
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
