using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA2018.Model
{
    class Curso
    {
        [Key]
        public int CursoId { get; set; }
        public string Descripcion { get; set; }
    }
}
