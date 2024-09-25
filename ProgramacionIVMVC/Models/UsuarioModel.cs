using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProgramacionIVMVC.Models
{
    public class UsuarioModel
    {
        [Key]
        public int Id_Usuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Edad { get; set; }
        public int Dni { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int Id_Rol { get; set; }

        [ForeignKey("Id_Rol")]
        public virtual RolModel Rol { get; set; }
    }
}
