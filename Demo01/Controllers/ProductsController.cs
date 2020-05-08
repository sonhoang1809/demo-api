using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Demo01.Models;

namespace Demo01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductContext _context;

        public ProductsController(ProductContext context)
        {
            _context = context;
            if (!ProductExists(101))
            {
                _context.Products.Add(new Product
                {

                    ProductId = 101,
                    Name = "Adidas - Superstar",
                    Description = "The best seller",
                    ImgBase64 = Support.Support.GetBase64ForImage("./assets/img/Adidas - Superstar.jpg"),
                    Price = 100,
                    Quantity = 12
                }) ;
            }
            if (!ProductExists(97))
            {
                _context.Products.Add(new Product
                {
                    ProductId = 97,
                    Name = "Adidas - Stan Smith",
                    Description = "The big star from Adidas",
                    ImgBase64 = Support.Support.GetBase64ForImage("./assets/img/Adidas - Stan Smith.jpg"),
                    Price = 100,
                    Quantity = 14
                });
            }
            if (!ProductExists(99))
            {
                _context.Products.Add(new Product
                {
                    ProductId = 99,
                    Name = "Adidas - Alphaboost",
                    Description = "With a specialized design for athletes who want to improve their skills",
                    ImgBase64 = Support.Support.GetBase64ForImage("./assets/img/Adidas - Alphaboost.jpg"),
                    Price = 120,
                    Quantity = 17
                });
            }
            if (!ProductExists(80))
            {
                _context.Products.Add(new Product
                {
                    ProductId = 80,
                    Name = "Adidas - Yeezy Boost 350",
                    Description = "Yeezy adds another colorway of its most popular design with the adidas Yeezy 350 Cinder",
                    ImgBase64 = Support.Support.GetBase64ForImage("./assets/img/Adidas - Yeezy Boost 350.jpg"),
                    Price = 310,
                    Quantity = 20
                });
            }
            if (!ProductExists(67))
            {
                _context.Products.Add(new Product
                {
                    ProductId = 67,
                    Name = "Nike - Nike Air Max 2090",
                    Description = "Bring the past into the future with the Nike Air Max 2090",
                    ImgBase64 = Support.Support.GetBase64ForImage("./assets/img/Nike - Nike Air Max 2090.jpg"),
                    Price = 210,
                    Quantity = 12
                });
            }
            if (!ProductExists(95))
            {
                _context.Products.Add(new Product
                {
                    ProductId = 95,
                    Name = "Nike - Nike Air Max 97",
                    Description = "The Nike Air Max 97 reimagines an iconic running shoe into everyday kicks.",
                    ImgBase64 = Support.Support.GetBase64ForImage("./assets/img/Nike - Nike Air Max 97.jpg"),
                    Price = 180,
                    Quantity = 12
                });
            }
            
            _context.SaveChanges();
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }

            var prod = await _context.Products.FindAsync(id);
            if(prod == null)
            {
                return NotFound();
            }
            prod.Name = product.Name;
            prod.Description = product.Description;
            prod.Price = product.Price;
            prod.Quantity = product.Quantity;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.ProductId }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return product;
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
