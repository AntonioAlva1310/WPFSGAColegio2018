using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA2018.Model
{
    public  class Puesto
    {
        [Key]
        public int PuestoId { get; set; }
        public string Descripcion { get; set; }
        public override string ToString()
        {
            return $" {this.PuestoId} | { this.Descripcion }";
        }

    }
}
