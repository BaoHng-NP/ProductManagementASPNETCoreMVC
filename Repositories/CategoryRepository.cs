using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessObjects;

namespace Repositories
{   

    public class CategoryRepository : ICategoryRepository
    {
        private readonly CategoryDAO _categoryDAO;
        public CategoryRepository(CategoryDAO categoryDAO)
        {
            _categoryDAO = categoryDAO;
        }
        public List<Category> GetCategories() => _categoryDAO.GetCategories();

    }
}
