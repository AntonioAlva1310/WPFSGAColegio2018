using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace SGA2018.Model
{
    public class ClaseAlumno
    {
        //[Key,Column(Order = 0)]
        //[ForeignKey("Alumno")]
        public int AlumnoId { get; set; }
        //[Key,Column(Order = 1)]
        //[ForeignKey("Clase")]
        public int ClaseId { get; set; }
        public DateTime FechaAsignacion { get; set; }
        public virtual Alumno Alumno { get; set; }
        public virtual Clase Clase { get; set; }
    }
}
