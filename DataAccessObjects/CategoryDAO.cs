using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects
{
    public class CategoryDAO
    {
        //private readonly MystoreContext _context;
        //public CategoryDAO(MystoreContext context)
        //{
        //    _context = context;
        //}
        MystoreContext _context;

        public List<Category> GetCategories() 
        {
            _context = new();
            var listCatergoies = new List<Category>();
            try
            {
                listCatergoies = _context.Categories.ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return listCatergoies;
        }
        




    }
}
