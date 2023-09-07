using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SGE.Data.Entity;
using SGE.Models;
using SGE.Services.ProductServices;

namespace SGE.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductServices _productServices;

        public ProductController(IProductServices productServices)
        {
            _productServices = productServices;
        }

        [Authorize(Roles = "Admin, User")]
        [HttpGet("GetProduct")]
        public async Task<ActionResult<List<Product>>> GetProduct(string? searchString = "", int pageSize = 3, int pageNumber = 1, string orderBy = "id")
        {
            var response = new ApiResponse();
            try
            {
                var result = await _productServices.GetAllProduct(searchString, pageSize, pageNumber, orderBy);
                if (result != null)
                {
                    response.Success = true;
                    response.Data = result;
                    return Ok(response);
                }
                throw new Exception("something went wrong");

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return Ok(response);
            }

        }

        [Authorize(Roles = "Admin, User")]
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<List<Product>>> GetById(int id)
        {
            var response = new ApiResponse();
            try
            {
                var result = await _productServices.GetById(id);
                if (result != null)
                {
                    response.Success = true;
                    response.Data = result;
                    return Ok(response);
                }
                throw new Exception("id not found");
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return Ok(response);
            }

        }

        [Authorize(Roles = "Admin")]
        [HttpPost("AddProduct")]
        public async Task<ActionResult> AddProduct([FromForm] ProductDto product)
        {
            var response = new ApiResponse();
            try
            {
                bool result = await _productServices.AddProduct(product);
                if (result)
                {
                    response.Success = true;
                    return Ok(response);
                }
                throw new Exception("Item is not added");
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return Ok(response);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateProduct/{id}")]
        public async Task<ActionResult<Product>> UpdateProduct([FromForm] ProductDto product)
        {
            var response = new ApiResponse();
            try
            {
                var result = await _productServices.UpdateProduct(product);
                if (result != null)
                {
                    response.Success = true;
                    response.Data = result;
                    return Ok(response);
                }
                throw new Exception("item not updated");
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return Ok(response);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteProduct/{id}")]
        public async Task<ActionResult<List<Product>>> DeleteProduct(int id)
        {
            var response = new ApiResponse();
            try
            {
                bool result = await _productServices.DeleteProduct(id);
                if (result)
                {
                    response.Success = true;
                    return Ok(response);
                }
                throw new Exception("item not deleted");
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return Ok(response);
            }
        }
        
    }
}
