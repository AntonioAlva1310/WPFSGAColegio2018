using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace SGA2018.Model
{
    public class GrupoAcademico
    {
        [Key]
        public int GrupoAcademicoId { get; set; }
        public string Descripcion { get; set; }
        public virtual ICollection<Clase> Clases { get; set; }
    }
}
