using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Data.Entity
{
    public class Product:BaseClass
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter Product Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter image Url")]
        [DataType(DataType.ImageUrl)]
        public string Image { get; set; }
        [Required(ErrorMessage = "Enter Product Description")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Enter Category")]
        public string Category { get; set; }
        [Required(ErrorMessage = "Enter product quantity")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "Enter Product Price")]
        public decimal Price { get; set; }
    }
}
