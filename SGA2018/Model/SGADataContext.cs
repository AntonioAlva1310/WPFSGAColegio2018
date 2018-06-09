using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
namespace SGA2018.Model
{
    public class SGADataContext : DbContext
    {
        public DbSet<Alumno> Alumnos { get; set; }
        public DbSet<Carrera> Carreras { get; set; }
        public DbSet<Salon> Salones { get; set; }
        public DbSet<ClaseAlumno> ClasesAlumnos { get; set; }
        public DbSet<GrupoAcademico> GrupoAcademicos { get; set; }
        public DbSet<Clase> Clases { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<Carrera>()
                .ToTable("Carreras");
            modelBuilder.Entity<Salon>()
                .ToTable("Salones");
            modelBuilder.Entity<Alumno>()
                .ToTable("Alumnos")
                .Property(c => c.Carne)
                .IsRequired()
                .HasMaxLength(7);
            modelBuilder.Entity<Alumno>()
                .ToTable("Alumnos")
                .Property(n => n.Nombres)
                .IsRequired()
                .HasMaxLength(128);
            modelBuilder.Entity<Alumno>()
                .ToTable("Alumnos")
                .Property(n => n.Apellidos)
                .IsRequired()
                .HasMaxLength(128);
            modelBuilder.Entity<GrupoAcademico>()
                .ToTable("GruposAcademicos");
            modelBuilder.Entity<Clase>()
                .ToTable("Clases");                
            modelBuilder.Entity<ClaseAlumno>()
                .ToTable("ClasesAlumnos")
                .HasKey(x => new {x.AlumnoId,x.ClaseId });
        }
    }
}
