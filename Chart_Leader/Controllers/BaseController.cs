using Chart_Leader.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Chart_Leader.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        protected leaderEntities db = new leaderEntities();
        protected bool ValidateFile(HttpPostedFileBase file)
        {
            string fileExtension = System.IO.Path.GetExtension(file.FileName).ToLower();
            string[] allowedFileTypes = { ".gif", ".png", ".jpeg", ".jpg" };
            if ((file.ContentLength > 0 && file.ContentLength < 5242880) && allowedFileTypes.Contains(fileExtension))
            {
                return true;
            }
            return false;
        }
        // GET: Error
        public ActionResult FileUploadLimitExceeded()
        {
            return View();
        }
        public ActionResult NotFound()
        {
            return View();
        }
        public BaseController()
        {
            var Data = from q in db.Shopping_Card
                       let count = db.Shopping_Card.Count()
                       let Total = db.Shopping_Card.Sum(x => x.Quantity * x.UnitPrice)
                       let average = Total / count
                       select q;
            ViewBag.CartTotalPrice = CartTotalPrice;
            ViewBag.Cart = Cart;
            ViewBag.CartUnits = Cart.Count;
            ViewBag.category = new SelectList(db.Categories.ToList(), "Cat_id", "Cat_Name");
            //if (Session["cart"]!=null)
            //{
            //    List<Item> CARTBASKET = (List<Item>)Session["cart"];
            //    ViewBag.BASKETsESSION = CARTBASKET;
            //    ViewBag.countBASKET = ((List<Item>)Session["cart"]).Count();
            //    ViewBag.totalbasket = CARTBASKET.Sum(x=>x.quantity*x.product.Product_Price);

            //}
            ViewBag.lastProducts = db.Products.OrderByDescending(x => x.Product_id).Take(5).ToList<Products>();



        }
        private List<Shopping_Card> Cart
        {
            get
            {
                return db.Shopping_Card.ToList();
            }
        }

        private decimal CartTotalPrice
        {
            get
            {
                return Cart.Sum(c => c.Quantity * c.UnitPrice);
            }
        }


    }
}