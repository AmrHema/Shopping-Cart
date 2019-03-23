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
    [HandleError]
    [Authorize(Roles = "admin")]

    public class CategoriesController : BaseController
    {
        // GET: Categories
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }
        [AllowAnonymous]
        // GET: Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categories categories = db.Categories.Find(id);
            if (categories == null)
            {
                return HttpNotFound();
            }
            return View(categories);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Categories categories, HttpPostedFileBase ImageUpload)
        {
            int idCat = db.Categories.OrderByDescending(x => x.Cat_id).Select(x => x.Cat_id).FirstOrDefault() + 1;
            if (ImageUpload != null)
            {
                if (ValidateFile(ImageUpload))
                {
                    try
                    {
                        string imgCatName = idCat + ".jpg";
                        categories.Cat_Image = "~/Common/CatImages/" + imgCatName;
                        ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Ccommon/CatImages/"), imgCatName));
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

                categories.Cat_Image = "~/Common/Images/uploade.jpg";
                //if the user has not entered a file return an error message
                // ModelState.AddModelError("FileName", "Please choose a file");
            }
            if (ModelState.IsValid)
            {
                db.Categories.Add(categories);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(categories);
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categories categories = db.Categories.Find(id);
            if (categories == null)
            {
                return HttpNotFound();
            }
            return View(categories);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Cat_id,Cat_Name,Cat_Description,Cat_Image")] Categories categories, HttpPostedFileBase ImageUpload)
        {
            string imgName = categories.Cat_id + ".jpg";
            categories.Cat_Image = "~/Common/CatImages/" + categories.Cat_id + ".jpg";
            ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Common/CatImages/"), imgName));
            if (ModelState.IsValid)
            {
                db.Entry(categories).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categories);
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categories categories = db.Categories.Find(id);
            if (categories == null)
            {
                return HttpNotFound();
            }
            return View(categories);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Categories categories = db.Categories.Find(id);
            db.Categories.Remove(categories);
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
