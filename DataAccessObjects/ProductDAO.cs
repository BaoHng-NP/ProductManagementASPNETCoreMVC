using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Microsoft.EntityFrameworkCore;
namespace DataAccessObjects
{
    public class ProductDAO
    {
        //private readonly MystoreContext _context;
        //public ProductDAO(MystoreContext context)
        //{
        //    _context = context;
        //}
        MystoreContext _context;

        public List<Product> GetProducts()
        {

            var listProducts = new List<Product>();
            try
            {
                _context = new();
                listProducts = _context.Products.Include(p => p.Category).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return listProducts;
        }

        public void SaveProduct(Product product)
        {
            try
            {
                _context=new();
                _context.Products.Add(product);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void UpdateProduct(Product product)
        {
            try
            {
                //_context.Products.Update(product);
                //_context.Entry<Product>(product).State = EntityState.Modified;
                _context = new();
                _context.Entry(product).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void DeleteProduct(Product product)
        {
            try
            {
                _context = new();
                var p1 = _context.Products.SingleOrDefault(p => p.ProductId == product.ProductId);
                _context.Products.Remove(p1);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Product GetProductById(int id)
        {
            Product product = new Product();
            try
            {
                _context = new();
                product = _context.Products.FirstOrDefault(p => p.ProductId == id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return product;
        }



    }
}
