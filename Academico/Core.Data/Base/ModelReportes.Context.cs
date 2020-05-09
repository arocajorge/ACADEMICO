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
    
        public virtual DbSet<VWCONTA_001> VWCONTA_001 { get; set; }
        public virtual DbSet<VWCXC_003> VWCXC_003 { get; set; }
        public virtual DbSet<VWFAC_003> VWFAC_003 { get; set; }
        public virtual DbSet<VWFAC_003_aplicaciones> VWFAC_003_aplicaciones { get; set; }
        public virtual DbSet<VWFAC_004> VWFAC_004 { get; set; }
        public virtual DbSet<VWACA_003> VWACA_003 { get; set; }
        public virtual DbSet<VWCXC_002> VWCXC_002 { get; set; }
        public virtual DbSet<VWACA_004> VWACA_004 { get; set; }
        public virtual DbSet<VWCXC_002_Aplicaciones> VWCXC_002_Aplicaciones { get; set; }
        public virtual DbSet<VWFAC_001> VWFAC_001 { get; set; }
        public virtual DbSet<VWFAC_002> VWFAC_002 { get; set; }
        public virtual DbSet<VWACA_002> VWACA_002 { get; set; }
        public virtual DbSet<VWCAJ_001> VWCAJ_001 { get; set; }
        public virtual DbSet<VWCAJ_002> VWCAJ_002 { get; set; }
        public virtual DbSet<VWCAJ_002_ingresos> VWCAJ_002_ingresos { get; set; }
        public virtual DbSet<VWCAJ_002_ValesNoConciliados> VWCAJ_002_ValesNoConciliados { get; set; }
        public virtual DbSet<VWBAN_001> VWBAN_001 { get; set; }
        public virtual DbSet<VWBAN_001_cancelaciones> VWBAN_001_cancelaciones { get; set; }
        public virtual DbSet<VWBAN_002> VWBAN_002 { get; set; }
        public virtual DbSet<VWBAN_002_cancelaciones> VWBAN_002_cancelaciones { get; set; }
        public virtual DbSet<VWBAN_003> VWBAN_003 { get; set; }
        public virtual DbSet<VWACA_009> VWACA_009 { get; set; }
        public virtual DbSet<VWCXC_005> VWCXC_005 { get; set; }
        public virtual DbSet<VWCXC_005_Cobros> VWCXC_005_Cobros { get; set; }
        public virtual DbSet<VWCXC_005_Diario> VWCXC_005_Diario { get; set; }
        public virtual DbSet<VWFAC_0031> VWFAC_0031 { get; set; }
        public virtual DbSet<VWFAC_007> VWFAC_007 { get; set; }
        public virtual DbSet<VWACA_012> VWACA_012 { get; set; }
        public virtual DbSet<VWACA_011> VWACA_011 { get; set; }
        public virtual DbSet<VWFAC_008> VWFAC_008 { get; set; }
        public virtual DbSet<VWACA_006> VWACA_006 { get; set; }
        public virtual DbSet<VWACA_008> VWACA_008 { get; set; }
        public virtual DbSet<VWACA_013_EquivalenciaPromedio> VWACA_013_EquivalenciaPromedio { get; set; }
    
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
    
        public virtual ObjectResult<SPACA_005_Result> SPACA_005(Nullable<int> idEmpresa, Nullable<decimal> idAlumno)
        {
            var idEmpresaParameter = idEmpresa.HasValue ?
                new ObjectParameter("IdEmpresa", idEmpresa) :
                new ObjectParameter("IdEmpresa", typeof(int));
    
            var idAlumnoParameter = idAlumno.HasValue ?
                new ObjectParameter("IdAlumno", idAlumno) :
                new ObjectParameter("IdAlumno", typeof(decimal));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPACA_005_Result>("SPACA_005", idEmpresaParameter, idAlumnoParameter);
        }
    
        public virtual ObjectResult<SPFAC_015_Result> SPFAC_015(Nullable<int> idEmpresa, Nullable<int> idSucursal, Nullable<System.DateTime> fechaIni, Nullable<System.DateTime> fechaFin)
        {
            var idEmpresaParameter = idEmpresa.HasValue ?
                new ObjectParameter("IdEmpresa", idEmpresa) :
                new ObjectParameter("IdEmpresa", typeof(int));
    
            var idSucursalParameter = idSucursal.HasValue ?
                new ObjectParameter("IdSucursal", idSucursal) :
                new ObjectParameter("IdSucursal", typeof(int));
    
            var fechaIniParameter = fechaIni.HasValue ?
                new ObjectParameter("FechaIni", fechaIni) :
                new ObjectParameter("FechaIni", typeof(System.DateTime));
    
            var fechaFinParameter = fechaFin.HasValue ?
                new ObjectParameter("FechaFin", fechaFin) :
                new ObjectParameter("FechaFin", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPFAC_015_Result>("SPFAC_015", idEmpresaParameter, idSucursalParameter, fechaIniParameter, fechaFinParameter);
        }
    
        public virtual ObjectResult<SPFAC_006_Result> SPFAC_006(Nullable<int> idEmpresa, Nullable<int> idSucursalIni, Nullable<int> idSucursalFin, Nullable<decimal> idAlumnoIni, Nullable<decimal> idAlumnoFin, Nullable<System.DateTime> fechaIni, Nullable<System.DateTime> fechaFin, Nullable<bool> mostrarAnulados)
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
    
            var fechaIniParameter = fechaIni.HasValue ?
                new ObjectParameter("FechaIni", fechaIni) :
                new ObjectParameter("FechaIni", typeof(System.DateTime));
    
            var fechaFinParameter = fechaFin.HasValue ?
                new ObjectParameter("FechaFin", fechaFin) :
                new ObjectParameter("FechaFin", typeof(System.DateTime));
    
            var mostrarAnuladosParameter = mostrarAnulados.HasValue ?
                new ObjectParameter("MostrarAnulados", mostrarAnulados) :
                new ObjectParameter("MostrarAnulados", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPFAC_006_Result>("SPFAC_006", idEmpresaParameter, idSucursalIniParameter, idSucursalFinParameter, idAlumnoIniParameter, idAlumnoFinParameter, fechaIniParameter, fechaFinParameter, mostrarAnuladosParameter);
        }
    
        public virtual ObjectResult<SPFAC_005_Result> SPFAC_005(Nullable<int> idEmpresa, Nullable<System.DateTime> fechaDesde, Nullable<System.DateTime> fechaHasta, string creDeb, string naturaleza)
        {
            var idEmpresaParameter = idEmpresa.HasValue ?
                new ObjectParameter("IdEmpresa", idEmpresa) :
                new ObjectParameter("IdEmpresa", typeof(int));
    
            var fechaDesdeParameter = fechaDesde.HasValue ?
                new ObjectParameter("FechaDesde", fechaDesde) :
                new ObjectParameter("FechaDesde", typeof(System.DateTime));
    
            var fechaHastaParameter = fechaHasta.HasValue ?
                new ObjectParameter("FechaHasta", fechaHasta) :
                new ObjectParameter("FechaHasta", typeof(System.DateTime));
    
            var creDebParameter = creDeb != null ?
                new ObjectParameter("CreDeb", creDeb) :
                new ObjectParameter("CreDeb", typeof(string));
    
            var naturalezaParameter = naturaleza != null ?
                new ObjectParameter("Naturaleza", naturaleza) :
                new ObjectParameter("Naturaleza", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPFAC_005_Result>("SPFAC_005", idEmpresaParameter, fechaDesdeParameter, fechaHastaParameter, creDebParameter, naturalezaParameter);
        }
    
        public virtual ObjectResult<SPCXC_006_Result> SPCXC_006(Nullable<int> idEmpresa, Nullable<int> idSucursal, Nullable<System.DateTime> fechaIni, Nullable<System.DateTime> fechaFin)
        {
            var idEmpresaParameter = idEmpresa.HasValue ?
                new ObjectParameter("IdEmpresa", idEmpresa) :
                new ObjectParameter("IdEmpresa", typeof(int));
    
            var idSucursalParameter = idSucursal.HasValue ?
                new ObjectParameter("IdSucursal", idSucursal) :
                new ObjectParameter("IdSucursal", typeof(int));
    
            var fechaIniParameter = fechaIni.HasValue ?
                new ObjectParameter("FechaIni", fechaIni) :
                new ObjectParameter("FechaIni", typeof(System.DateTime));
    
            var fechaFinParameter = fechaFin.HasValue ?
                new ObjectParameter("FechaFin", fechaFin) :
                new ObjectParameter("FechaFin", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPCXC_006_Result>("SPCXC_006", idEmpresaParameter, idSucursalParameter, fechaIniParameter, fechaFinParameter);
        }
    
        public virtual ObjectResult<SPCXC_008_Result> SPCXC_008(Nullable<int> idEmpresa, Nullable<System.DateTime> fechaCorte, Nullable<int> idAnio, Nullable<int> idSede, Nullable<int> idNivel, Nullable<int> idJornada, Nullable<int> idCurso, Nullable<int> idParalelo, Nullable<decimal> idAlumno)
        {
            var idEmpresaParameter = idEmpresa.HasValue ?
                new ObjectParameter("IdEmpresa", idEmpresa) :
                new ObjectParameter("IdEmpresa", typeof(int));
    
            var fechaCorteParameter = fechaCorte.HasValue ?
                new ObjectParameter("FechaCorte", fechaCorte) :
                new ObjectParameter("FechaCorte", typeof(System.DateTime));
    
            var idAnioParameter = idAnio.HasValue ?
                new ObjectParameter("IdAnio", idAnio) :
                new ObjectParameter("IdAnio", typeof(int));
    
            var idSedeParameter = idSede.HasValue ?
                new ObjectParameter("IdSede", idSede) :
                new ObjectParameter("IdSede", typeof(int));
    
            var idNivelParameter = idNivel.HasValue ?
                new ObjectParameter("IdNivel", idNivel) :
                new ObjectParameter("IdNivel", typeof(int));
    
            var idJornadaParameter = idJornada.HasValue ?
                new ObjectParameter("IdJornada", idJornada) :
                new ObjectParameter("IdJornada", typeof(int));
    
            var idCursoParameter = idCurso.HasValue ?
                new ObjectParameter("IdCurso", idCurso) :
                new ObjectParameter("IdCurso", typeof(int));
    
            var idParaleloParameter = idParalelo.HasValue ?
                new ObjectParameter("IdParalelo", idParalelo) :
                new ObjectParameter("IdParalelo", typeof(int));
    
            var idAlumnoParameter = idAlumno.HasValue ?
                new ObjectParameter("IdAlumno", idAlumno) :
                new ObjectParameter("IdAlumno", typeof(decimal));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPCXC_008_Result>("SPCXC_008", idEmpresaParameter, fechaCorteParameter, idAnioParameter, idSedeParameter, idNivelParameter, idJornadaParameter, idCursoParameter, idParaleloParameter, idAlumnoParameter);
        }
    
        public virtual ObjectResult<SPCXC_007_Result> SPCXC_007(Nullable<int> idEmpresa, Nullable<System.DateTime> fechaCorte)
        {
            var idEmpresaParameter = idEmpresa.HasValue ?
                new ObjectParameter("IdEmpresa", idEmpresa) :
                new ObjectParameter("IdEmpresa", typeof(int));
    
            var fechaCorteParameter = fechaCorte.HasValue ?
                new ObjectParameter("FechaCorte", fechaCorte) :
                new ObjectParameter("FechaCorte", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPCXC_007_Result>("SPCXC_007", idEmpresaParameter, fechaCorteParameter);
        }
    
        public virtual ObjectResult<SPACA_007_Result> SPACA_007(Nullable<int> idEmpresa, Nullable<int> idAnio, Nullable<int> idSede, Nullable<int> idNivel, Nullable<int> idJornada, Nullable<int> idCurso, Nullable<int> idParalelo, Nullable<System.DateTime> fechaIni, Nullable<System.DateTime> fechaFin, Nullable<bool> mostrarRetirados)
        {
            var idEmpresaParameter = idEmpresa.HasValue ?
                new ObjectParameter("IdEmpresa", idEmpresa) :
                new ObjectParameter("IdEmpresa", typeof(int));
    
            var idAnioParameter = idAnio.HasValue ?
                new ObjectParameter("IdAnio", idAnio) :
                new ObjectParameter("IdAnio", typeof(int));
    
            var idSedeParameter = idSede.HasValue ?
                new ObjectParameter("IdSede", idSede) :
                new ObjectParameter("IdSede", typeof(int));
    
            var idNivelParameter = idNivel.HasValue ?
                new ObjectParameter("IdNivel", idNivel) :
                new ObjectParameter("IdNivel", typeof(int));
    
            var idJornadaParameter = idJornada.HasValue ?
                new ObjectParameter("IdJornada", idJornada) :
                new ObjectParameter("IdJornada", typeof(int));
    
            var idCursoParameter = idCurso.HasValue ?
                new ObjectParameter("IdCurso", idCurso) :
                new ObjectParameter("IdCurso", typeof(int));
    
            var idParaleloParameter = idParalelo.HasValue ?
                new ObjectParameter("IdParalelo", idParalelo) :
                new ObjectParameter("IdParalelo", typeof(int));
    
            var fechaIniParameter = fechaIni.HasValue ?
                new ObjectParameter("FechaIni", fechaIni) :
                new ObjectParameter("FechaIni", typeof(System.DateTime));
    
            var fechaFinParameter = fechaFin.HasValue ?
                new ObjectParameter("FechaFin", fechaFin) :
                new ObjectParameter("FechaFin", typeof(System.DateTime));
    
            var mostrarRetiradosParameter = mostrarRetirados.HasValue ?
                new ObjectParameter("MostrarRetirados", mostrarRetirados) :
                new ObjectParameter("MostrarRetirados", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPACA_007_Result>("SPACA_007", idEmpresaParameter, idAnioParameter, idSedeParameter, idNivelParameter, idJornadaParameter, idCursoParameter, idParaleloParameter, fechaIniParameter, fechaFinParameter, mostrarRetiradosParameter);
        }
    
        public virtual ObjectResult<SPACA_010_Result> SPACA_010(Nullable<int> idEmpresa, Nullable<int> idAnio, Nullable<int> idSede, Nullable<int> idNivel, Nullable<int> idJornada, Nullable<int> idCurso, Nullable<int> idParalelo, Nullable<int> idParcial, Nullable<int> idMateria)
        {
            var idEmpresaParameter = idEmpresa.HasValue ?
                new ObjectParameter("IdEmpresa", idEmpresa) :
                new ObjectParameter("IdEmpresa", typeof(int));
    
            var idAnioParameter = idAnio.HasValue ?
                new ObjectParameter("IdAnio", idAnio) :
                new ObjectParameter("IdAnio", typeof(int));
    
            var idSedeParameter = idSede.HasValue ?
                new ObjectParameter("IdSede", idSede) :
                new ObjectParameter("IdSede", typeof(int));
    
            var idNivelParameter = idNivel.HasValue ?
                new ObjectParameter("IdNivel", idNivel) :
                new ObjectParameter("IdNivel", typeof(int));
    
            var idJornadaParameter = idJornada.HasValue ?
                new ObjectParameter("IdJornada", idJornada) :
                new ObjectParameter("IdJornada", typeof(int));
    
            var idCursoParameter = idCurso.HasValue ?
                new ObjectParameter("IdCurso", idCurso) :
                new ObjectParameter("IdCurso", typeof(int));
    
            var idParaleloParameter = idParalelo.HasValue ?
                new ObjectParameter("IdParalelo", idParalelo) :
                new ObjectParameter("IdParalelo", typeof(int));
    
            var idParcialParameter = idParcial.HasValue ?
                new ObjectParameter("IdParcial", idParcial) :
                new ObjectParameter("IdParcial", typeof(int));
    
            var idMateriaParameter = idMateria.HasValue ?
                new ObjectParameter("IdMateria", idMateria) :
                new ObjectParameter("IdMateria", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPACA_010_Result>("SPACA_010", idEmpresaParameter, idAnioParameter, idSedeParameter, idNivelParameter, idJornadaParameter, idCursoParameter, idParaleloParameter, idParcialParameter, idMateriaParameter);
        }
    
        public virtual ObjectResult<SPACA_013_Result> SPACA_013(Nullable<int> idEmpresa, Nullable<int> idAnio, Nullable<int> idSede, Nullable<int> idNivel, Nullable<int> idJornada, Nullable<int> idCurso, Nullable<int> idParalelo, Nullable<int> idParcial)
        {
            var idEmpresaParameter = idEmpresa.HasValue ?
                new ObjectParameter("IdEmpresa", idEmpresa) :
                new ObjectParameter("IdEmpresa", typeof(int));
    
            var idAnioParameter = idAnio.HasValue ?
                new ObjectParameter("IdAnio", idAnio) :
                new ObjectParameter("IdAnio", typeof(int));
    
            var idSedeParameter = idSede.HasValue ?
                new ObjectParameter("IdSede", idSede) :
                new ObjectParameter("IdSede", typeof(int));
    
            var idNivelParameter = idNivel.HasValue ?
                new ObjectParameter("IdNivel", idNivel) :
                new ObjectParameter("IdNivel", typeof(int));
    
            var idJornadaParameter = idJornada.HasValue ?
                new ObjectParameter("IdJornada", idJornada) :
                new ObjectParameter("IdJornada", typeof(int));
    
            var idCursoParameter = idCurso.HasValue ?
                new ObjectParameter("IdCurso", idCurso) :
                new ObjectParameter("IdCurso", typeof(int));
    
            var idParaleloParameter = idParalelo.HasValue ?
                new ObjectParameter("IdParalelo", idParalelo) :
                new ObjectParameter("IdParalelo", typeof(int));
    
            var idParcialParameter = idParcial.HasValue ?
                new ObjectParameter("IdParcial", idParcial) :
                new ObjectParameter("IdParcial", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPACA_013_Result>("SPACA_013", idEmpresaParameter, idAnioParameter, idSedeParameter, idNivelParameter, idJornadaParameter, idCursoParameter, idParaleloParameter, idParcialParameter);
        }
    }
}
