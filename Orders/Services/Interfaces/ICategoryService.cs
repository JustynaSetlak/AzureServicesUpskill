using Orders.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<bool> InsertCategory(Category newCategory);
    }
}
