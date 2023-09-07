using SGE.Data.Entity;
using SGE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Services.ProductServices
{
    public interface IProductServices
    {
        Task<ProductDetailDto> GetAllProduct(string searchString, int pageSize, int pageNumber, string orderBy);
        Task<Product> GetById(int id);
        Task<bool> AddProduct(ProductDto product);
        Task<Product> UpdateProduct(ProductDto product);
        Task<bool> DeleteProduct(int id);
        Task<List<Product>> AllProduct();
    }
}
