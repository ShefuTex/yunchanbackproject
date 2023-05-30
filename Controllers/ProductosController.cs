using System.Net;
using Microsoft.AspNetCore.Mvc;
using yunchanbackproject.Models;
using yunchanbackproject.Entities;
using Microsoft.EntityFrameworkCore;
namespace yunchanbackproject.Controllers
{

    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly DBContext DBContext;

        public ProductosController(DBContext DBContext)
        {
            this.DBContext = DBContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductModel>>> getAll()
        {
            var List = await DBContext.Products.Select(
                s => new ProductModel
                {
                    id = s.id,
                    nombre = s.nombre,
                    descriopcion = s.descriopcion,
                    precio = s.precio,
                    imagen = s.imagen,
                    stok = s.stok
                }
            ).ToListAsync();

            if (List.Count < 0)
            {
                return NotFound();
            }
            else
            {
                return List;
            }
        }

        [HttpGet("by-id/{productId}")]
        public async Task<ActionResult<ProductModel>> getAll(int productId)
        {
            ProductModel Product = await DBContext.Products.Select(
                   s => new ProductModel
                   {
                       id = s.id,
                       nombre = s.nombre,
                       descriopcion = s.descriopcion,
                       precio = s.precio,
                       imagen = s.imagen,
                       stok = s.stok
                   })
               .FirstOrDefaultAsync(s => s.id == productId);

            if (Product == null)
            {
                return NotFound();
            }
            else
            {
                return Product;
            }
        }

        [HttpPost]
        public async Task<int> saveProduct(ProductModel producto)
        {

            var entity = new Product()
            {
                id = producto.id,
                nombre = producto.nombre,
                descriopcion = producto.descriopcion,
                precio = producto.precio,
                imagen = producto.imagen,
                stok = producto.stok
            };

            DBContext.Products.Add(entity);
            await DBContext.SaveChangesAsync();
            ProductModel Product =await DBContext.Products.Select(
                   s => new ProductModel
                   {
                       id = s.id,
                       nombre = s.nombre,
                       descriopcion = s.descriopcion,
                       precio = s.precio,
                       imagen = s.imagen,
                       stok = s.stok
                   })
               .FirstOrDefaultAsync(s => s.id == producto.id);
            if (Product == null){
                return 1;
            }
            return 0;
        }
    
        [HttpPut]
        public async Task<int> updateProducto(ProductModel producto){
            var entitySave = await DBContext.Products.FirstOrDefaultAsync(p => p.id == producto.id);
            entitySave.imagen = producto.imagen;
            entitySave.descriopcion = producto.descriopcion;
            entitySave.nombre = producto.nombre;
            entitySave.precio = producto.precio;
            entitySave.stok = producto.stok;
            await DBContext.SaveChangesAsync();
            return 0;
        }
    }
}