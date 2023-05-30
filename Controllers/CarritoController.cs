using Microsoft.AspNetCore.Mvc;
using yunchanbackproject.Models;
using yunchanbackproject.Entities;
using Microsoft.EntityFrameworkCore;
namespace yunchanbackproject.Controllers
{

    [ApiController]
    [Route("api/v1/[controller]")]
    public class CarritoController : ControllerBase
    {


        private readonly DBContext DBContext;

        public CarritoController(DBContext DBContext)
        {
            this.DBContext = DBContext;
        }
        [HttpPost]
        public async Task<CarritoModel> saveProductoInCarrito(CarritoModel productoAAgregar)
        {
            var entitySave = await DBContext.Carrito.FirstOrDefaultAsync(p => p.id == productoAAgregar.id && p.usuarioId == productoAAgregar.usuarioId && p.comprado == 1);
            if (entitySave == null)
            {
                entitySave = new Carrito()
                {
                    id = productoAAgregar.id,
                    nombre = productoAAgregar.nombre,
                    precio = productoAAgregar.precio,
                    imagen = productoAAgregar.imagen,
                    cantidad = productoAAgregar.cantidad,
                    usuarioId = productoAAgregar.usuarioId,
                    nombreUsuario = productoAAgregar.nombreUsuario,
                    comprado = 1
                };
                DBContext.Carrito.Add(entitySave);
            }
            else
            {
                entitySave.cantidad = productoAAgregar.cantidad;
            }

            await DBContext.SaveChangesAsync();
            CarritoModel carrito = await DBContext.Carrito.Select(
                   s => new CarritoModel
                   {
                       id = s.id,
                       nombre = s.nombre,
                       precio = s.precio,
                       imagen = s.imagen,
                       cantidad = s.cantidad,
                       usuarioId = s.usuarioId,
                       nombreUsuario = s.nombreUsuario,

                   })
               .FirstOrDefaultAsync(s => s.id == productoAAgregar.id && s.usuarioId == productoAAgregar.usuarioId);
            return carrito;
        }

        [HttpGet("by-user-id/{userId}")]
        public async Task<List<CarritoModel>> getCarrtioByUser(int userId)
        {
            var List = await DBContext.Carrito.Select(
               s => new CarritoModel
               {
                   id = s.id,
                   nombre = s.nombre,
                   precio = s.precio,
                   imagen = s.imagen,
                   cantidad = s.cantidad,
                   usuarioId = s.usuarioId,
                   nombreUsuario = s.nombreUsuario,
                   comprado = s.comprado

               }
           ).Where(s => s.usuarioId == userId && s.comprado == 1)
           .ToListAsync();
            if (List.Count < 0)
            {
                return null;
            }
            else
            {
                return List;
            }
        }

        [HttpDelete("by-user-id/{userId}/producto/{productId}")]
        public async Task<int> quitarDelCarrito(int userId, int productId)
        {
            var entity = new Carrito()
            {
                id = productId,
                usuarioId = userId,
                comprado = 1
            };
            DBContext.Carrito.Attach(entity);
            DBContext.Carrito.Remove(entity);
            await DBContext.SaveChangesAsync();
            var entityToValidate = DBContext.Carrito.Select(s => new CarritoModel
            {
                id = s.id,
                nombre = s.nombre,
                precio = s.precio,
                imagen = s.imagen,
                cantidad = s.cantidad,
                usuarioId = s.usuarioId,
                nombreUsuario = s.nombreUsuario,
            }).FirstOrDefaultAsync(s => s.id == productId && s.usuarioId == userId);
            if (entityToValidate == null)
            {
                return 1;
            }
            return 0;
        }

        [HttpGet("comprar/by-user-id/{userId}")]
        public async Task<int> comprarCarrito(int userId)
        {
            var listaDeCompras = await DBContext.Carrito.Select(
                s => new Carrito
                {
                    id = s.id,
                    nombre = s.nombre,
                    precio = s.precio,
                    imagen = s.imagen,
                    cantidad = s.cantidad,
                    usuarioId = s.usuarioId,
                    nombreUsuario = s.nombreUsuario,
                    comprado = s.comprado
                }
            ).Where(p => p.usuarioId == userId && p.comprado == 1)
            .ToListAsync();
            await DBContext.SaveChangesAsync();
            var encontrarProductosNoComprados = await DBContext.Carrito.Select(
                s => new Carrito
                {
                    id = s.id,
                    nombre = s.nombre,
                    precio = s.precio,
                    imagen = s.imagen,
                    cantidad = s.cantidad,
                    usuarioId = s.usuarioId,
                    nombreUsuario = s.nombreUsuario,
                    comprado = s.comprado
                }
            ).Where(p => p.usuarioId == userId && p.comprado == 1).ToListAsync();
            if (encontrarProductosNoComprados != null && encontrarProductosNoComprados.Count > 0)
            {
                return 1;
            }
            return 0;
        }
    }
}