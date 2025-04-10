

using Microsoft.EntityFrameworkCore;
using PatientManager.Domain.Entities.administracion;
using PatientManager.Domain.Entities.atencionmedica;
using PatientManager.Domain.Entities.laboratorio;

namespace PatientManager.Persistance.Context
{
    public partial class PatientManagerContext : DbContext
    {
        public PatientManagerContext(DbContextOptions<PatientManagerContext> options) : base(options)
        {

        }

        #region
        public DbSet<Consultorios> Consultorios { get; set; }
        public DbSet<Estados> Estados { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }
        #endregion

        #region
        public DbSet<Citas> Citas { get; set; }
        public DbSet<Medicos> Medicos { get; set; }
        public DbSet<Pacientes> Pacientes { get; set; }
        #endregion

        #region
        public DbSet<PruebasLaboratorio> PruebasLaboratorios { get; set; }
        public DbSet<ResultadosLaboratorio> ResultadosLaboratorios { get; set; }
        #endregion
    }
}
