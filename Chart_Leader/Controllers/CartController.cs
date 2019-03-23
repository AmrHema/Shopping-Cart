using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Chart_Leader.Models;

namespace Chart_Leader.Controllers
{
    public class CartController : BaseController
    {
        // GET: Cart

        public ActionResult Index()
        {
            return View(Session["cart"]);
        }

        private int isExist(int Product_id)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            for (int i = 0; i < cart.Count(); i++)
                if (cart[i].product.Product_id == Product_id)
                    return i;
            return -1;

        }
        // Add

        public RedirectToRouteResult Buy(int id)
        {
            if (Session["cart"] == null)
            {
                List<Item> cart = new List<Item>();
                cart.Add(new Item() { product = db.Products.Single(x => x.Product_id == id), quantity = 1 });
                Session["cart"] = cart;
            }
            else
            {
                List<Item> cart = (List<Item>)Session["cart"];
                int index = isExist(id);
                if (index == -1)
                {
                    cart.Add(new Item() { product = db.Products.Single(x => x.Product_id == id), quantity = 1 });
                }
                else
                    cart[index].quantity++;
                Session["cart"] = cart;

            }
             return RedirectToAction("Index", "Home");
            //return Json(new { d = "تم اضافة المنتج" });
        }

        public JsonResult BuyJson(int P_ID)
        {
            if (Session["cart"] == null)
            {
                List<Item> cart = new List<Item>();
                cart.Add(new Item() { product = db.Products.Single(x => x.Product_id == P_ID), quantity = 1 });
                Session["cart"] = cart;
            }
            else
            {
                List<Item> cart = (List<Item>)Session["cart"];
                int index = isExist(P_ID);
                if (index == -1)
                {
                    cart.Add(new Item() { product = db.Products.Single(x => x.Product_id == P_ID), quantity = 1 });
                }
                else
                    cart[index].quantity++;
                Session["cart"] = cart;

            }
            return Json(new {success=true, d = "تم اضافة المنتج" },JsonRequestBehavior.AllowGet);
        }

        // Remove
        public RedirectToRouteResult Remove(int index)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            cart.RemoveAt(index);
            Session["cart"] = cart;
            return RedirectToAction("Index", Session["cart"]);
        }

        //
        // Clear
        public RedirectToRouteResult Clear()
        {
            if (Session["cart"] !=null )
            {
                Session["cart"] = null;
            }
            return RedirectToAction("Index");
        }
        // ADD
        public RedirectToRouteResult changeQTY_Add(int index, int id)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            Item pro = cart.Single(x => x.product.Product_id == id);
            pro.quantity++;
            Session["cart"] = cart;
            return RedirectToAction("Index", Session["cart"]);
        }
        //changeQTY_Mince
        public RedirectToRouteResult changeQTY_Mince(int index, int id)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            Item pro = cart.Single(x => x.product.Product_id == id);
            pro.quantity--;
            if (pro.quantity == 0)
            {
                cart.Remove(pro);
            }
            Session["cart"] = cart;
            return RedirectToAction("Index", Session["cart"]);
        }



        //changeQTY_Input
        //[HttpPost]
        //public RedirectToRouteResult changeQTY_Input(int index, int id, int QTYInput = 0)
        //{
        //    List<Item> cart = (List<Item>)Session["cart"];
        //    Item pro = cart.Single(x => x.product.Product_id == id);
        //    pro.quantity = QTYInput;
        //    if (pro.quantity == 0)
        //    {
        //        cart.Remove(pro);
        //    }
        //    Session["cart"] = cart;
        //    return RedirectToAction("Index", Session["cart"]);
        //}
        //changeQTY_Input
        [HttpPost]
        public RedirectToRouteResult changeQTY_Input(int index, int id, int QTYDropdown = 0)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            Item pro = cart.Single(x => x.product.Product_id == id);
            pro.quantity = QTYDropdown;
            if (pro.quantity == 0)
            {
                cart.Remove(pro);
            }
            Session["cart"] = cart;
            return RedirectToAction("Index", Session["cart"]);
        }
        private Cart GetCart()
        {
            Cart cart = (Cart)Session["cart"];
            if (cart == null)
            {
                cart = new Cart();
                Session["cart"] = cart;
            }
            return cart;
        }

        //
        public JsonResult QuanityChangeddl(int P_ID, int ddlquantity)
        {

            List<Item> cart = (List<Item>)Session["cart"];
            Item pro = cart.Single(x => x.product.Product_id == P_ID);

            if (pro == null)
            {
                return Json(new { d = "0" });
            }
            int QTY = 0;
            pro.quantity = ddlquantity;
            if (pro.quantity == 0)
            {
                cart.Remove(pro);
                QTY = 0;
            }

            else
            {
                QTY = pro.quantity;
            }

            Session["cart"] = cart;
            if (((List<Item>)Session["cart"]).Count<1)
            {
                Session["cart"] = null;
            }

            return Json(new { d = QTY });
        }
        [HttpGet]
        public JsonResult UpdateTotal()
        {
            List<Item> cart = (List<Item>)Session["cart"];
            decimal total;
            try
            {
                total = cart.Sum(p => p.product.Product_Price * p.quantity);
                //total = db.Shopping_Card.Select(p => p.UnitPrice * p.Quantity).Sum();
            }
            catch (Exception) { total = 0; }

            return Json(new { d = String.Format("{0:c}", total) }, JsonRequestBehavior.AllowGet);
        }
        //

    }
}