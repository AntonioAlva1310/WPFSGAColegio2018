using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace SGA2018.Model
{
    public class Clase
    {
        [Key]
        public int ClaseId { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaCreacion { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }  
        public int GrupoAcademicoId { get; set; }
        public int SalonId { get; set; }
        public virtual Salon Salon { get; set; }
        public virtual GrupoAcademico GrupoAcademico { get; set; }
        public virtual ICollection<ClaseAlumno> ClaseAlumnos { get; set; }
    }
}
