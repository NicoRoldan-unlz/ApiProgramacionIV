using ApiProgramacionIV.Data;
using ApiProgramacionIV.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace ApiProgramacionIV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly DataBaseContext _context;

        public UsuariosController(DataBaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsuarios()
        {
            try
            {
                List<UsuarioModel> usuarios = await _context.Usuarios
                    .Include(usuario => usuario.Rol)
                    .ToListAsync();
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return BadRequest($"Hubo un problema, error: {ex.Message}");
            }
        }

        [HttpGet("{id_Usuario}")]
        public async Task<IActionResult> GetUsuariosById(int id_Usuario)
        {
            try
            {
                UsuarioModel usuario = await _context.Usuarios
                    .Include(usuario => usuario.Rol)
                    .FirstOrDefaultAsync(u => u.Id_Usuario == id_Usuario);

                if (usuario is null)
                {
                    Log.Error($"No existe el usuario con el id {id_Usuario}");
                    return NotFound("No existe el usuario indicado.");
                }

                Log.Information("Se llamó al endpoint GetUsuariosById");
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                Log.Error($"Hubo un problema en GetUsuariosById, error: {ex.Message}");
                return BadRequest($"Hubo un problema, error: {ex.Message}");
            }
        }

        [HttpGet("UsuariosPorRoles")]
        public async Task<IActionResult> GetUsuariosPorRoles()
        {
            try
            {
                var usuariosPorRoles = await _context.Usuarios
                    .Include(u => u.Rol)
                    .GroupBy(r => new { r.Edad, r.Rol.Nombre_Rol })
                    .Select(s => new
                    {
                        Edad = s.Key.Edad,
                        Nombre_Rol = s.Key.Nombre_Rol,
                        Cantidad_Usuarios = s.Count()
                    })
                    .OrderByDescending(order => order.Cantidad_Usuarios)
                        .ThenByDescending(order => order.Edad)
                    .ToListAsync();
                return Ok(usuariosPorRoles);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpGet("UsuariosPorRol")]
        public async Task<IActionResult> GetUsuariosPorRol(string nombre_Rol)
        {
            try
            {
                RolModel roles = await _context.Roles
                    .FirstOrDefaultAsync(r => r.Nombre_Rol == nombre_Rol);

                if (roles is null)
                    return NotFound($"No existe el rol indicado {nombre_Rol}");

                List<UsuarioModel> usuarios = await _context.Usuarios
                    .Include(usuario => usuario.Rol)
                    .Where(r => r.Rol.Nombre_Rol == nombre_Rol)
                    .ToListAsync();

                if (!usuarios.Any())
                    return NotFound($"No existen usuarios del tipo {nombre_Rol}");

                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateUsuario(UsuarioModel usuarioModel)
        {
            try
            {
                _context.Usuarios.Add(usuarioModel);
                await _context.SaveChangesAsync();
                return Ok(usuarioModel);
            }
            catch (Exception ex)
            {
                return BadRequest($"Hubo un problema, error: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUsuario(UsuarioModel usuarioModel)
        {
            UsuarioModel usuario = await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(u => u.Id_Usuario == usuarioModel.Id_Usuario);

            if(usuario is null)
            {
                return NotFound("No existe el usuario indicado");
            }

            try
            {
                _context.Entry(usuarioModel).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok("Se modificaron los datos del usuario.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpDelete("{id_Usuario}")]
        public async Task<IActionResult> DeleteUsuario(int id_Usuario)
        {
            UsuarioModel usuario = await _context.Usuarios
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id_Usuario == id_Usuario);

            if (usuario is null)
                return NotFound($"No existe el usuario con ID: {id_Usuario}");

            try
            {
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();

                return Ok("Se eliminó el registro indicado.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}
