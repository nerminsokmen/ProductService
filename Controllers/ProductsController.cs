using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsService.DataLayer;
using ProductsService.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading;
using Google.Cloud.Diagnostics.Common;

namespace ProductsService.Controllers
{
    [System.Web.Http.Cors.EnableCors("*", "*", "*")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
   
        private DataContext _context;
        public ProductsController(DataContext context)
        {
            _context = context;

            if (_context.Products.Count() == 0)
            {
                // Create a new ProductItem if collection is empty,
                // which means you can't delete all Customer.
                _context.Products.Add(new Products { ID = 1, ProductName = "Polo", ProductType = "Sports Car", ProductVolume = 150, ProductPrice = 30000 });
                _context.SaveChanges();
                _context.Products.Add(new Products { ID = 2, ProductName = "Golf", ProductType = "Sports Car", ProductVolume = 120, ProductPrice = 38000 });
                _context.SaveChanges();
                _context.Products.Add(new Products { ID = 3, ProductName = "Arteon", ProductType = "Sports Car", ProductVolume = 110, ProductPrice = 48000 });
                _context.SaveChanges();
                _context.Products.Add(new Products { ID = 4, ProductName = "Passat", ProductType = "Famiy Car", ProductVolume = 161, ProductPrice = 34000 });
                _context.SaveChanges();
                _context.Products.Add(new Products { ID = 5, ProductName = "Tiguan", ProductType = "Land", ProductVolume = 112, ProductPrice = 50000 });
                _context.SaveChanges();
                _context.Products.Add(new Products { ID = 6, ProductName = "Bugatti Type 46", ProductType = "Classical", ProductVolume = 7, ProductPrice = 65000 });
                _context.SaveChanges();
                _context.Products.Add(new Products { ID = 7, ProductName = "Alfa Romeo 6C 2300B", ProductType = "Classical", ProductVolume = 2, ProductPrice = 68000 });
                _context.SaveChanges();
                _context.Products.Add(new Products { ID = 8, ProductName = "Chevrolet 3100 Pickup", ProductType = "Classical", ProductVolume = 12, ProductPrice = 15000 });
                _context.SaveChanges();
                _context.Products.Add(new Products { ID = 9, ProductName = "ISUZU D-Max", ProductType = "Land", ProductVolume = 25, ProductPrice = 25000 });
                _context.SaveChanges();
            }
        }

        // GET api/Products
        [HttpGet]
        public async Task<ActionResult> GetProducts()
        {
            var products = await _context.Products.ToListAsync();
            if (products == null)
            {
                return NotFound("Product Not Found");

            }
            else
            {
                return Ok(products);
            }
        
        }

        // GET api/Products/5
        [HttpGet("{ID}")]
        public async Task<ActionResult> GetProduct(int ID)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.ID == ID);

            if (product == null)
            {
                return NotFound("Product Not Found");

            }
            else
            {
                return Ok(product);
            }

        }

    }
}
