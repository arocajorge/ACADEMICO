﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Core.Data.Base
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class EntitiesReportes : DbContext
    {
        public EntitiesReportes()
            : base("name=EntitiesReportes")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<VWACA_002> VWACA_002 { get; set; }
        public virtual DbSet<VWACA_003> VWACA_003 { get; set; }
        public virtual DbSet<VWCONTA_001> VWCONTA_001 { get; set; }
        public virtual DbSet<VWCXC_002> VWCXC_002 { get; set; }
        public virtual DbSet<VWCXC_002_diario> VWCXC_002_diario { get; set; }
        public virtual DbSet<VWCXC_003> VWCXC_003 { get; set; }
    
        public virtual ObjectResult<SPACA_001_Result> SPACA_001(Nullable<int> idEmpresa, Nullable<decimal> idAlumno)
        {
            var idEmpresaParameter = idEmpresa.HasValue ?
                new ObjectParameter("IdEmpresa", idEmpresa) :
                new ObjectParameter("IdEmpresa", typeof(int));
    
            var idAlumnoParameter = idAlumno.HasValue ?
                new ObjectParameter("IdAlumno", idAlumno) :
                new ObjectParameter("IdAlumno", typeof(decimal));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPACA_001_Result>("SPACA_001", idEmpresaParameter, idAlumnoParameter);
        }
    
        public virtual ObjectResult<SPCXC_001_Result> SPCXC_001(Nullable<int> idEmpresa, Nullable<int> idSucursalIni, Nullable<int> idSucursalFin, Nullable<decimal> idAlumnoIni, Nullable<decimal> idAlumnoFin, Nullable<System.DateTime> fechaCorte, Nullable<bool> mostrarSaldo0)
        {
            var idEmpresaParameter = idEmpresa.HasValue ?
                new ObjectParameter("IdEmpresa", idEmpresa) :
                new ObjectParameter("IdEmpresa", typeof(int));
    
            var idSucursalIniParameter = idSucursalIni.HasValue ?
                new ObjectParameter("IdSucursalIni", idSucursalIni) :
                new ObjectParameter("IdSucursalIni", typeof(int));
    
            var idSucursalFinParameter = idSucursalFin.HasValue ?
                new ObjectParameter("IdSucursalFin", idSucursalFin) :
                new ObjectParameter("IdSucursalFin", typeof(int));
    
            var idAlumnoIniParameter = idAlumnoIni.HasValue ?
                new ObjectParameter("IdAlumnoIni", idAlumnoIni) :
                new ObjectParameter("IdAlumnoIni", typeof(decimal));
    
            var idAlumnoFinParameter = idAlumnoFin.HasValue ?
                new ObjectParameter("IdAlumnoFin", idAlumnoFin) :
                new ObjectParameter("IdAlumnoFin", typeof(decimal));
    
            var fechaCorteParameter = fechaCorte.HasValue ?
                new ObjectParameter("FechaCorte", fechaCorte) :
                new ObjectParameter("FechaCorte", typeof(System.DateTime));
    
            var mostrarSaldo0Parameter = mostrarSaldo0.HasValue ?
                new ObjectParameter("MostrarSaldo0", mostrarSaldo0) :
                new ObjectParameter("MostrarSaldo0", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPCXC_001_Result>("SPCXC_001", idEmpresaParameter, idSucursalIniParameter, idSucursalFinParameter, idAlumnoIniParameter, idAlumnoFinParameter, fechaCorteParameter, mostrarSaldo0Parameter);
        }
    
        public virtual ObjectResult<SPCXC_004_Result> SPCXC_004(Nullable<int> idEmpresa, string idUsuario, Nullable<System.DateTime> fechaCorte)
        {
            var idEmpresaParameter = idEmpresa.HasValue ?
                new ObjectParameter("IdEmpresa", idEmpresa) :
                new ObjectParameter("IdEmpresa", typeof(int));
    
            var idUsuarioParameter = idUsuario != null ?
                new ObjectParameter("IdUsuario", idUsuario) :
                new ObjectParameter("IdUsuario", typeof(string));
    
            var fechaCorteParameter = fechaCorte.HasValue ?
                new ObjectParameter("FechaCorte", fechaCorte) :
                new ObjectParameter("FechaCorte", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPCXC_004_Result>("SPCXC_004", idEmpresaParameter, idUsuarioParameter, fechaCorteParameter);
        }
    }
}
