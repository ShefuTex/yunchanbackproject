using System.Net;
using Microsoft.AspNetCore.Mvc;
using yunchanbackproject.Models;
using yunchanbackproject.Entities;
using Microsoft.EntityFrameworkCore;

namespace yunchanbackproject.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsuariosController : ControllerBase
    {

        private readonly DBContext DBContext;

        public UsuariosController(DBContext DBContext)
        {
            this.DBContext = DBContext;
        }


        [HttpGet("mail/{mail}/pass/{pass}")]
        public async Task<ActionResult<UsuariosModel>> loguinUser(string mail, string pass)
        {
            var User = await DBContext.Users.Select(
                s => new UsuariosModel
                {
                    id = s.id,
                    nombre = s.nombre,
                    apellido = s.apellido,
                    correo = s.correo,
                    password = s.password,
                    telefono = s.telefono,
                    perfil = s.perfil
                }
            ).FirstOrDefaultAsync(s => s.correo == mail && s.password == pass);

            if (User == null)
            {
                return NotFound();
            }
            else
            {
                return User;
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<UsuariosModel>>> getAll()
        {
            var List = await DBContext.Users.Select(
                s => new UsuariosModel
                {
                    id = s.id,
                    nombre = s.nombre,
                    apellido = s.apellido,
                    correo = s.correo,
                    password = s.password,
                    telefono = s.telefono,
                    perfil = s.perfil
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

        [HttpPost]
        public async Task<ActionResult<UsuariosModel>> InsertUser(UsuariosModel user)
        {
            int lastUser = DBContext.Users.Max(u => u.id);
            var entity = new User
            {
                id = lastUser+1,
                nombre = user.nombre,
                apellido = user.apellido,
                correo = user.correo,
                password = user.password,
                telefono = user.telefono,
                perfil = user.perfil
            };

            DBContext.Users.Add(entity);
            await DBContext.SaveChangesAsync();
            var User = await DBContext.Users.Select(
               s => new UsuariosModel
               {
                   id = s.id,
                   nombre = s.nombre,
                   apellido = s.apellido,
                   correo = s.correo,
                   password = s.password,
                   telefono = s.telefono,
                   perfil = s.perfil,
               }
           ).FirstOrDefaultAsync(s => s.correo == user.correo && s.password == user.password);
            if (User == null)
            {
                return new UsuariosModel();
            }
            return User;
        }

        [HttpPut]
        public async Task<int> update(UsuariosModel user){
            var entitySave = await DBContext.Users.FirstOrDefaultAsync(p => p.id == user.id);
            entitySave.apellido = user.apellido;
            entitySave.correo = user.correo;
            entitySave.nombre = user.nombre;
            entitySave.password = user.password;
            entitySave.perfil = user.perfil;
            entitySave.telefono = user.telefono;
            await DBContext.SaveChangesAsync();
            return 0;
        }
    }
}