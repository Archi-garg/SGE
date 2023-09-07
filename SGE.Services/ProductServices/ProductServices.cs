using Microsoft.AspNetCore.Hosting;
using SGE.Data.Repository;
using SGE.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGE.Data.Entity;
using SGE.Models;
using Microsoft.AspNetCore.Mvc;

namespace SGE.Services.ProductServices
{
    public class ProductServices:IProductServices
    {
        public readonly IGenericRepository<Product> _genericCrud;
        public readonly DataContext _dataContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductServices(IGenericRepository<Product> genericCrud, DataContext context, IWebHostEnvironment webHostEnvironment)
        {
            _genericCrud = genericCrud;
            _dataContext = context;
            _webHostEnvironment = webHostEnvironment;

        }

        public async Task<ProductDetailDto> GetAllProduct(string? searchString, int pageSize, int pageNumber, string orderBy)
        {
            try
            {
                string search = searchString != null ? searchString.ToLower() : "";
                var itemList = await _genericCrud.GetAll(x => x.IsDeleted == false);
                var SearchedItem = itemList.Where(item => item.Name.ToLower().StartsWith(search) ||
                                                   item.Description.ToLower().StartsWith(search) ||
                                                   item.Category.ToLower().StartsWith(search) ||
                                                   item.Quantity.ToString().StartsWith(search) ||
                                                   item.Price.ToString().StartsWith(search));

                if (orderBy == "descendingId")
                {
                    SearchedItem = SearchedItem.OrderByDescending(x => x.Id);
                }
                else if (orderBy == "name")
                {
                    SearchedItem = SearchedItem.OrderBy(x => x.Name);
                }
                else if (orderBy == "descendingName")
                {
                    SearchedItem = SearchedItem.OrderByDescending(x => x.Name);
                }
                else if (orderBy == "description")
                {
                    SearchedItem = SearchedItem.OrderBy(x => x.Description);
                }
                else if (orderBy == "descendingDescription")
                {
                    SearchedItem = SearchedItem.OrderByDescending(x => x.Description);
                }
                else if (orderBy == "category")
                {
                    SearchedItem = SearchedItem.OrderBy(x => x.Category);
                }
                else if (orderBy == "descendingCategory")
                {
                    SearchedItem = SearchedItem.OrderByDescending(x => x.Category);
                }
                else if (orderBy == "price")
                {
                    SearchedItem = SearchedItem.OrderBy(x => x.Price);
                }
                else if (orderBy == "descendingPrice")
                {
                    SearchedItem = SearchedItem.OrderByDescending(x => x.Price);
                }
                else if (orderBy == "quantity")
                {
                    SearchedItem = SearchedItem.OrderBy(x => x.Quantity);
                }
                else if (orderBy == "descendingQuantity")
                {
                    SearchedItem = SearchedItem.OrderByDescending(x => x.Quantity);
                }
                else
                {
                    SearchedItem = SearchedItem.OrderBy(x => x.Id);
                }

                var selectedItem = SearchedItem.Skip(((pageNumber) - 1) * (pageSize)).Take(pageSize).ToList();
                var addResult = new ProductDetailDto() { item = (List<Product>)selectedItem, count = SearchedItem.Count() };
                return addResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<bool> AddProduct([FromForm] ProductDto product)
        {
            try
            {
                var data = new Product()
                {
                    Name = product.Name,
                    Description = product.Description,
                    Quantity = product.Quantity,
                    Price = product.Price,
                    Category = product.Category,
                };

                if (product.Image.Length > 0)
                {

                    var uploadsFolderPath = Path.Combine(_webHostEnvironment.WebRootPath, "Image");

                    if (!Directory.Exists(uploadsFolderPath))
                    {
                        Directory.CreateDirectory(uploadsFolderPath);
                    }

                    string uniqueFileName = Path.GetFileName(product.Image.FileName);
                    var filePath = Path.Combine(uploadsFolderPath, uniqueFileName);

                    using (Stream filestream = new FileStream(filePath, FileMode.Create))
                    {
                        await product.Image.CopyToAsync(filestream);
                        data.Image = "https://localhost:7148/Image/" + uniqueFileName;
                    }

                    await _genericCrud.Insert(data);
                    var res = await _genericCrud.Save();
                    return (int)res > 0;
                }
                return false;

            }
            catch (Exception ex) { throw ex; }

        }

        public async Task<Product> GetById(int id)
        {
            try
            {
                return await _genericCrud.GetById(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Product> UpdateProduct(ProductDto product)
        {
            try
            {
                var data = await _genericCrud.GetById(product.Id);
                data.Name = product.Name;
                data.Description = product.Description;
                data.Price = product.Price;
                data.Category = product.Category;
                data.Quantity = product.Quantity;

                if (product.Image != null && product.Image.Length > 0)
                {

                    var uploadsFolderPath = Path.Combine(_webHostEnvironment.WebRootPath, "Image");

                    if (!Directory.Exists(uploadsFolderPath))
                    {
                        Directory.CreateDirectory(uploadsFolderPath);
                    }

                    string uniqueFileName = Path.GetFileName(product.Image.FileName);
                    var filePath = Path.Combine(uploadsFolderPath, uniqueFileName);

                    using (Stream filestream = new FileStream(filePath, FileMode.Create))
                    {
                        await product.Image.CopyToAsync(filestream);
                        data.Image = "https://localhost:7148/Image/" + uniqueFileName;
                    }
                }
                await _genericCrud.Update(data);
                var res = await _genericCrud.Save();
                if ((int)res > 0)
                {
                    return data;
                }
                return data;
            }
            catch (Exception ex) { throw ex; }


        }

        public async Task<bool> DeleteProduct(int id)
        {
            try
            {
                var product = _dataContext.products.Find(id);
                product.IsDeleted = true;
                await _genericCrud.Update(product);
                //_genericCrud.Delete(id);
                var res = await _genericCrud.Save();
                return (int)res > 0;
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<List<Product>> AllProduct()
        {
            var itemList = await _genericCrud.GetAll(x => x.IsDeleted == false);
            return itemList.ToList();
        }
    }
}
