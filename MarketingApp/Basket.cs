using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MarketingApp.İndex;

namespace MarketingApp
{
    public class Basket
    {
        public List<Products> Product { get; set; } = new List<Products>();
        public Basket()
        {
            Product = new List<Products>();

        }

        public void AddOrUpdateProduct(Products product)
        {
            Products existingProduct = Product.FirstOrDefault(p => p.UrunId == product.UrunId);

            if (existingProduct != null)
            {
                existingProduct.Miktar += product.Miktar;
            }
            else
            {
                Product.Add(product);            }
        }
    }
}
