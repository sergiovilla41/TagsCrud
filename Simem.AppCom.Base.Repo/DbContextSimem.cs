using Microsoft.EntityFrameworkCore;
using Simem.AppCom.Base.Utils;
using Simem.AppCom.Datos.Dominio;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using static Simem.AppCom.Base.Repo.DbContextSimem;


namespace Simem.AppCom.Base.Repo
{
    [ExcludeFromCodeCoverage]
    public partial class DbContextSimem : DbContext
    {
        public DbContextSimem() : base()
        {
            MegaMenu = Set<MegaMenu>();
            Categoria = Set<Categoria>();
            CategoriaNovedad = Set<CategoriaNovedad>();

            Granularidad = Set<Granularidad>();
            TipoVista = Set<TipoVista>();
            Etiqueta = Set<Etiqueta>();
            GeneracionArchivo = Set<GeneracionArchivo>();
            GeneracionArchivoEtiqueta = Set<GeneracionArchivoEtiqueta>();
            Periodicidad = Set<ConfiguracionPeriodicidad>();

            PreguntaFrecuente = Set<PreguntaFrecuente>();
            PreguntaFrecuenteEtiqueta = Set<PreguntaFrecuenteEtiqueta>();
            EnlaceInteres = Set<EnlaceInteres>();
            EnlaceInteresEtiqueta = Set<EnlaceInteresEtiqueta>();
            ColumnaConjuntoDato = Set<ColumnaConjuntoDato>();
            ColumnaDatoConjuntoDato = Set<ColumnaDatoConjuntoDato>();
            EstandarizacionRegistros = Set<EstandarizacionRegistros>();
            GeneracionArchivoCategoria = Set<GeneracionArchivoCategoria>();

            Novedad = Set<Novedad>();
            IdGeneracionArchivos = Set<IdGeneracionArchivos>();

            ConfiguracionVariableGeneracionArchivo = Set<ConfiguracionVariableGeneracionArchivo>();
            ConfiguracionVariable = Set<ConfiguracionVariable>();

            Contacto = Set<Contacto>();
            ContactoCodigo = Set<ContactoCodigo>();
            ContactoEmpresa = Set<ContactoEmpresa>();
            ContactoPais = Set<ContactoPais>();
            ContactoSolicitante = Set<ContactoSolicitante>();
            ContactoTipoDocumento = Set<ContactoTipoDocumento>();
            ContactoTipoSolicitud = Set<ContactoTipoSolicitud>();
            ContactoConfiguracionAdjunto = Set<ContactoConfiguracionAdjunto>();
            ConfiguracionVariablePrcResult = Set<ConfiguracionVariablePrcResult>();

            Capacitacion = Set<Capacitacion>();
            CapacitacionEtiquetas = Set<CapacitacionEtiqueta>();

            UsuarioRol = Set<UsuarioRol>();
            Rol = Set<Rol>();
            Usuario = Set<Usuario>();
            GeneracionArchivo = Set<GeneracionArchivo>();
            RolConfiguracionGeneracionArchivos = Set<RolConfiguracionGeneracionArchivos>();
            NovedadEtiquetas = Set<NovedadEtiqueta>();

            BuscadorGeneralConjuntoDatosPrcResult = Set<BuscadorGeneralConjuntoDatosPrcResult>();
            BuscadorGeneralPrcResult = Set<BuscadorGeneralPrcResult>();
            CategoriasHijosPrcResult = Set<CategoriasHijosPrcResult>();

            ResumenConjuntoDato = Set<ResumenConjuntoDato>();
            ResumenConjuntoDatoEtiqueta = Set<ResumenConjuntoDatoEtiqueta>();
            ResumenConjuntoDatoColumna = Set<ResumenConjuntoDatoColumna>();
            Counts = Set<Count>();
            MigaPanPrcResult = Set<MigaPanPrcResult>();

            Archivo = Set<Archivo>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string simmenConnectionValue = KeyVaultManager.GetSecretValue(KeyVaultTypes.SimemConnection);
            optionsBuilder.UseSqlServer(simmenConnectionValue);
        }

        public DbSet<MegaMenu> MegaMenu { get; set; }

        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<CategoriaNovedad> CategoriaNovedad { get; set; }
        public DbSet<Granularidad> Granularidad { get; set; }
        public DbSet<GeneracionArchivoCategoria> GeneracionArchivoCategoria { get; set; }
        public DbSet<NovedadEtiqueta> NovedadEtiquetas { get; set; }

        public DbSet<TipoVista> TipoVista { get; set; }
        public DbSet<Etiqueta> Etiqueta { get; set; }
        public DbSet<GeneracionArchivo> GeneracionArchivo { get; set; }
        public DbSet<GeneracionArchivoEtiqueta> GeneracionArchivoEtiqueta { get; set; }
        public DbSet<ConfiguracionPeriodicidad> Periodicidad { get; set; }

        public DbSet<PreguntaFrecuente> PreguntaFrecuente { get; set; }
        public DbSet<PreguntaFrecuenteEtiqueta> PreguntaFrecuenteEtiqueta { get; set; }
        public DbSet<Capacitacion> Capacitacion { get; set; }
        public DbSet<CapacitacionEtiqueta> CapacitacionEtiquetas { get; set; }
        public DbSet<EnlaceInteres> EnlaceInteres { get; set; }
        public DbSet<EnlaceInteresEtiqueta> EnlaceInteresEtiqueta { get; set; }
        public DbSet<ColumnaConjuntoDato> ColumnaConjuntoDato { get; set; }
        public DbSet<ColumnaDatoConjuntoDato> ColumnaDatoConjuntoDato { get; set; }
        public DbSet<EstandarizacionRegistros> EstandarizacionRegistros { get; set; }

        public DbSet<Novedad> Novedad { get; set; }
        public DbSet<IdGeneracionArchivos> IdGeneracionArchivos { get; set; }

        public DbSet<ConfiguracionVariableGeneracionArchivo> ConfiguracionVariableGeneracionArchivo { get; set; }
        public DbSet<ConfiguracionVariable> ConfiguracionVariable { get; set; }

        public DbSet<Contacto> Contacto { get; set; }
        public DbSet<ContactoCodigo> ContactoCodigo { get; set; }
        public DbSet<ContactoEmpresa> ContactoEmpresa { get; set; }
        public DbSet<ContactoPais> ContactoPais { get; set; }
        public DbSet<ContactoSolicitante> ContactoSolicitante { get; set; }
        public DbSet<ContactoTipoDocumento> ContactoTipoDocumento { get; set; }
        public DbSet<ContactoTipoSolicitud> ContactoTipoSolicitud { get; set; }
        public DbSet<ContactoConfiguracionAdjunto> ContactoConfiguracionAdjunto { get; set; }

        public DbSet<ConfiguracionVariablePrcResult> ConfiguracionVariablePrcResult { get; set; }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Rol> Rol { get; set; }
        public DbSet<UsuarioRol> UsuarioRol { get; set; }
        public DbSet<RolConfiguracionGeneracionArchivos> RolConfiguracionGeneracionArchivos { get; set; }
        public DbSet<BuscadorGeneralConjuntoDatosPrcResult> BuscadorGeneralConjuntoDatosPrcResult { get; set; }
        public DbSet<BuscadorGeneralPrcResult> BuscadorGeneralPrcResult { get; set; }
        public DbSet<CategoriasHijosPrcResult> CategoriasHijosPrcResult { get; set; }

        public DbSet<ResumenConjuntoDato> ResumenConjuntoDato { get; set; }
        public DbSet<ResumenConjuntoDatoEtiqueta> ResumenConjuntoDatoEtiqueta { get; set; }
        public DbSet<ResumenConjuntoDatoColumna> ResumenConjuntoDatoColumna { get; set; }
        public DbSet<MigaPanPrcResult> MigaPanPrcResult { get; set; }

        public DbSet<Count> Counts { get; set; }

        public DbSet<Archivo> Archivo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Count>().HasNoKey();
            modelBuilder.Entity<ConfiguracionVariablePrcResult>().HasNoKey();
            modelBuilder.Entity<BuscadorGeneralConjuntoDatosPrcResult>().HasNoKey();
            modelBuilder.Entity<BuscadorGeneralPrcResult>().HasNoKey();
            modelBuilder.Entity<CategoriasHijosPrcResult>().HasNoKey();
            modelBuilder.Entity<GeneracionArchivoCategoria>().HasNoKey();
            modelBuilder.Entity<MigaPanPrcResult>().HasNoKey();
        }

    }
}
