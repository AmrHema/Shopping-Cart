using Chart_Leader.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Chart_Leader.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }
        //AddToCart
        public ActionResult AddToCart(int id)
        {
            addToCart(id);
            return RedirectToAction("Index");
        }

        private void addToCart(int Product_id)
        {
            // check if product is valid
            Products product = db.Products.FirstOrDefault(p => p.Product_id == Product_id);
            if (product != null && product.Product_QTY > 0)
            {
                // check if product already existed
                Shopping_Card cart = db.Shopping_Card.FirstOrDefault(c => c.P_ID == Product_id);
                if (cart != null)
                {
                    cart.Quantity++;
                }
                else
                {

                    cart = new Shopping_Card
                    {
                        P_ID = product.Product_id,
                        P_Name = product.Product_Name,
                        UnitPrice = product.Product_Price,
                        Quantity = 1
                    };

                    db.Shopping_Card.Add(cart);
                }
                product.Product_QTY--;
                db.SaveChanges();
            }
        }


        public ActionResult Category(string catName)
        {
            List<Products> products;
            if (catName == "")
            {
                products = db.Products.ToList<Products>();
            }
            else
            {
                products = db.Products.Where(p => p.Categories.Cat_Name == catName).ToList<Products>();
            }
            return View("Index", products);
        }


        // GET: Products
        public ActionResult Cat(int id)
        {
            var products = db.Products.Where(x=>x.Cat_id==id);
            return View(products.ToList());
        }


        // GET: Products/Details/5
        public ActionResult Product(int? id)
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

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        
    }
}