using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProgramacionIVMVC.Data;
using ProgramacionIVMVC.Models;

namespace ProgramacionIVMVC.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly DataBaseContext _context;

        public UsuarioController(DataBaseContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var usuarios = _context.Usuarios
                .Include(u => u.Rol)
                .ToList();
            return View(usuarios);
        }


        public IActionResult Create()
        {
            ViewData["Id_Rol"] = new SelectList(_context.Roles, "Id_Rol", "Nombre_Rol");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Nombre, Apellido, Edad, Dni, FechaNacimiento, Id_Rol")] UsuarioModel usuario)
        {
            _context.Add(usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}
