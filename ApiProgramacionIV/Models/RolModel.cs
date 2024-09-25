using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiProgramacionIV.Models
{
    [Table("Roles")]
    public class RolModel
    {
        [Key]
        public int Id_Rol { get; set; }
        public string Nombre_Rol { get; set; }
    }
}
