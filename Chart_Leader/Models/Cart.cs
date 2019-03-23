using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chart_Leader.Models
{
    public class Cart
    {
        public static List<CartILine> LineCollection = new List<CartILine>();
        public void AddItem(int productID, int qty = 1)
        {
            CartILine line = LineCollection.Where(x => x.product.Product_id == productID)
                                .FirstOrDefault();
            if (line == null)
            {
                LineCollection.Add(new CartILine() { product_id = productID, QTY = qty });
            }
            else
                line.QTY += qty;
        }//End Add

        public void RemoveLine(int product_id)
        {
            LineCollection.RemoveAll(x => x.product.Product_id== product_id);
        }//End Remove
        public decimal ComputeTotal()
        {
           
            return LineCollection.Sum(x=>x.product.Product_Price * x.QTY);
        }//End ComputeTotal
        public void ClearCollection()
        {
            LineCollection.Clear();
        }//End ComputeTotal

        public IEnumerable<CartILine>Lines
        {
            get { return LineCollection; }
        }
    }
    public class CartILine
    {
        public int product_id;
        public Products product;

        public decimal QTY;
    }
}