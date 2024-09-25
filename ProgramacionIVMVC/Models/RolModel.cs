using System.ComponentModel.DataAnnotations;

namespace ProgramacionIVMVC.Models
{
    public class RolModel
    {
        [Key]
        public int Id_Rol { get; set; }
        public string Nombre_Rol { get; set; }
    }
}
