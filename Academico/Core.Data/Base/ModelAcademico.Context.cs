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
    
    public partial class EntitiesAcademico : DbContext
    {
        public EntitiesAcademico()
            : base("name=EntitiesAcademico")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<aca_AnioLectivo_Curso_Plantilla> aca_AnioLectivo_Curso_Plantilla { get; set; }
        public virtual DbSet<aca_AnioLectivo_Jornada_Curso> aca_AnioLectivo_Jornada_Curso { get; set; }
        public virtual DbSet<aca_AnioLectivo_NivelAcademico_Jornada> aca_AnioLectivo_NivelAcademico_Jornada { get; set; }
        public virtual DbSet<aca_Catalogo> aca_Catalogo { get; set; }
        public virtual DbSet<aca_CatalogoTipo> aca_CatalogoTipo { get; set; }
        public virtual DbSet<aca_Jornada> aca_Jornada { get; set; }
        public virtual DbSet<aca_MateriaGrupo> aca_MateriaGrupo { get; set; }
        public virtual DbSet<aca_Menu> aca_Menu { get; set; }
        public virtual DbSet<aca_Menu_x_aca_Sede> aca_Menu_x_aca_Sede { get; set; }
        public virtual DbSet<aca_NivelAcademico> aca_NivelAcademico { get; set; }
        public virtual DbSet<aca_Paralelo> aca_Paralelo { get; set; }
        public virtual DbSet<aca_Sede> aca_Sede { get; set; }
        public virtual DbSet<vwaca_AnioLectivo_Rubro> vwaca_AnioLectivo_Rubro { get; set; }
        public virtual DbSet<aca_AnioLectivo_Curso_Documento> aca_AnioLectivo_Curso_Documento { get; set; }
        public virtual DbSet<aca_Curso> aca_Curso { get; set; }
        public virtual DbSet<vwaca_AnioLectivo_Periodo> vwaca_AnioLectivo_Periodo { get; set; }
        public virtual DbSet<aca_Documento> aca_Documento { get; set; }
        public virtual DbSet<aca_AnioLectivo_Curso_Paralelo> aca_AnioLectivo_Curso_Paralelo { get; set; }
        public virtual DbSet<aca_Menu_x_seg_usuario> aca_Menu_x_seg_usuario { get; set; }
        public virtual DbSet<aca_PermisoMatricula> aca_PermisoMatricula { get; set; }
        public virtual DbSet<aca_Rubro> aca_Rubro { get; set; }
        public virtual DbSet<vwaca_AnioLectivo_Jornada_Curso> vwaca_AnioLectivo_Jornada_Curso { get; set; }
        public virtual DbSet<vwaca_PermisoMatricula> vwaca_PermisoMatricula { get; set; }
        public virtual DbSet<aca_CatalogoFicha> aca_CatalogoFicha { get; set; }
        public virtual DbSet<aca_CatalogoTipoFicha> aca_CatalogoTipoFicha { get; set; }
        public virtual DbSet<aca_Alumno> aca_Alumno { get; set; }
        public virtual DbSet<aca_AnioLectivo_Paralelo_Profesor> aca_AnioLectivo_Paralelo_Profesor { get; set; }
        public virtual DbSet<aca_AnioLectivo_Curso_Plantilla_Parametrizacion> aca_AnioLectivo_Curso_Plantilla_Parametrizacion { get; set; }
        public virtual DbSet<vwaca_AnioLectivo_Curso_Plantilla_Parametrizacion> vwaca_AnioLectivo_Curso_Plantilla_Parametrizacion { get; set; }
        public virtual DbSet<vwaca_AnioLectivo_Paralelo_Profesor_NoAsignados> vwaca_AnioLectivo_Paralelo_Profesor_NoAsignados { get; set; }
        public virtual DbSet<vwaca_AnioLectivoConductaEquivalencia> vwaca_AnioLectivoConductaEquivalencia { get; set; }
        public virtual DbSet<aca_AnioLectivo_Rubro> aca_AnioLectivo_Rubro { get; set; }
        public virtual DbSet<aca_AnioLectivo_Rubro_Periodo> aca_AnioLectivo_Rubro_Periodo { get; set; }
        public virtual DbSet<aca_AnioLectivoCalificacionHistorico> aca_AnioLectivoCalificacionHistorico { get; set; }
        public virtual DbSet<aca_AlumnoDocumento> aca_AlumnoDocumento { get; set; }
        public virtual DbSet<vwaca_AlumnoDocumento> vwaca_AlumnoDocumento { get; set; }
        public virtual DbSet<vwaca_Hermanos> vwaca_Hermanos { get; set; }
        public virtual DbSet<vwaca_Alumno> vwaca_Alumno { get; set; }
        public virtual DbSet<aca_Reporte_x_seg_usuario> aca_Reporte_x_seg_usuario { get; set; }
        public virtual DbSet<aca_Reporte_x_tb_empresa> aca_Reporte_x_tb_empresa { get; set; }
        public virtual DbSet<aca_Reporte> aca_Reporte { get; set; }
        public virtual DbSet<aca_SocioEconomico> aca_SocioEconomico { get; set; }
        public virtual DbSet<vwaca_AnioLectivoCalificacionHistorico> vwaca_AnioLectivoCalificacionHistorico { get; set; }
        public virtual DbSet<aca_AnioLectivo_Curso_Materia> aca_AnioLectivo_Curso_Materia { get; set; }
        public virtual DbSet<aca_Materia> aca_Materia { get; set; }
        public virtual DbSet<aca_MateriaArea> aca_MateriaArea { get; set; }
        public virtual DbSet<vwaca_Materia> vwaca_Materia { get; set; }
        public virtual DbSet<aca_Familia> aca_Familia { get; set; }
        public virtual DbSet<vwaca_Familia> vwaca_Familia { get; set; }
        public virtual DbSet<aca_AnioLectivo_Sede_NivelAcademico> aca_AnioLectivo_Sede_NivelAcademico { get; set; }
        public virtual DbSet<aca_MatriculaCondicionalParrafo> aca_MatriculaCondicionalParrafo { get; set; }
        public virtual DbSet<aca_MatriculaCondicional> aca_MatriculaCondicional { get; set; }
        public virtual DbSet<vwaca_MatriculaCondicional> vwaca_MatriculaCondicional { get; set; }
        public virtual DbSet<aca_MatriculaCondicional_Det> aca_MatriculaCondicional_Det { get; set; }
        public virtual DbSet<vwaca_MatriculaCondicional_Det> vwaca_MatriculaCondicional_Det { get; set; }
        public virtual DbSet<aca_MecanismoDePago> aca_MecanismoDePago { get; set; }
        public virtual DbSet<vwaca_MecanismoDePago> vwaca_MecanismoDePago { get; set; }
        public virtual DbSet<aca_Matricula> aca_Matricula { get; set; }
        public virtual DbSet<aca_PlantillaTipo> aca_PlantillaTipo { get; set; }
        public virtual DbSet<aca_MatriculaCalificacionParcial> aca_MatriculaCalificacionParcial { get; set; }
        public virtual DbSet<aca_Profesor> aca_Profesor { get; set; }
        public virtual DbSet<vwaca_AnioLectivo_Paralelo_Profesor> vwaca_AnioLectivo_Paralelo_Profesor { get; set; }
        public virtual DbSet<vwaca_Profesor> vwaca_Profesor { get; set; }
        public virtual DbSet<vwaca_AnioLectivo_Paralelo_Profesor_Calificaciones> vwaca_AnioLectivo_Paralelo_Profesor_Calificaciones { get; set; }
        public virtual DbSet<aca_AnioLectivoConductaEquivalencia> aca_AnioLectivoConductaEquivalencia { get; set; }
        public virtual DbSet<aca_AnioLectivo> aca_AnioLectivo { get; set; }
        public virtual DbSet<aca_Plantilla_Rubro> aca_Plantilla_Rubro { get; set; }
        public virtual DbSet<vwaca_Plantilla_Rubro> vwaca_Plantilla_Rubro { get; set; }
        public virtual DbSet<aca_Plantilla> aca_Plantilla { get; set; }
        public virtual DbSet<vwaca_Matricula_Rubro_PorFacturar> vwaca_Matricula_Rubro_PorFacturar { get; set; }
        public virtual DbSet<vwaca_AnioLectivo_Curso_Paralelo> vwaca_AnioLectivo_Curso_Paralelo { get; set; }
        public virtual DbSet<vwaca_Plantilla_Rubro_Matricula> vwaca_Plantilla_Rubro_Matricula { get; set; }
        public virtual DbSet<vwaca_AnioLectivo_Paralelo_Conducta> vwaca_AnioLectivo_Paralelo_Conducta { get; set; }
        public virtual DbSet<aca_MatriculaConducta> aca_MatriculaConducta { get; set; }
        public virtual DbSet<vwaca_MatriculaConducta> vwaca_MatriculaConducta { get; set; }
        public virtual DbSet<aca_AnioLectivoParcial> aca_AnioLectivoParcial { get; set; }
        public virtual DbSet<vwaca_AnioLectivoParcial> vwaca_AnioLectivoParcial { get; set; }
        public virtual DbSet<vwaca_MatriculaCalificacionParcial> vwaca_MatriculaCalificacionParcial { get; set; }
        public virtual DbSet<aca_AnioLectivoEquivalenciaPromedio> aca_AnioLectivoEquivalenciaPromedio { get; set; }
        public virtual DbSet<vwaca_AnioLectivoEquivalenciaPromedio> vwaca_AnioLectivoEquivalenciaPromedio { get; set; }
        public virtual DbSet<aca_AlumnoRetiro> aca_AlumnoRetiro { get; set; }
        public virtual DbSet<vwaca_Matricula_AlumnosPorParalelo> vwaca_Matricula_AlumnosPorParalelo { get; set; }
        public virtual DbSet<aca_MatriculaCambios> aca_MatriculaCambios { get; set; }
        public virtual DbSet<aca_Matricula_Rubro> aca_Matricula_Rubro { get; set; }
        public virtual DbSet<vwaca_Matricula_Rubro> vwaca_Matricula_Rubro { get; set; }
        public virtual DbSet<vwaca_AnioLectivo_Curso_Materia> vwaca_AnioLectivo_Curso_Materia { get; set; }
        public virtual DbSet<aca_MatriculaCalificacion> aca_MatriculaCalificacion { get; set; }
        public virtual DbSet<vwaca_MatriculaCalificacion> vwaca_MatriculaCalificacion { get; set; }
        public virtual DbSet<vwaca_Matricula> vwaca_Matricula { get; set; }
        public virtual DbSet<vwaca_AlumnoRetiro> vwaca_AlumnoRetiro { get; set; }
        public virtual DbSet<aca_AnioLectivo_Periodo> aca_AnioLectivo_Periodo { get; set; }
        public virtual DbSet<vwaca_Matricula_Rubro_PorFacturarMasiva> vwaca_Matricula_Rubro_PorFacturarMasiva { get; set; }
    
        public virtual int spaca_corregir_menu(Nullable<int> idEmpresa, Nullable<int> idSede, string idUsuario)
        {
            var idEmpresaParameter = idEmpresa.HasValue ?
                new ObjectParameter("IdEmpresa", idEmpresa) :
                new ObjectParameter("IdEmpresa", typeof(int));
    
            var idSedeParameter = idSede.HasValue ?
                new ObjectParameter("IdSede", idSede) :
                new ObjectParameter("IdSede", typeof(int));
    
            var idUsuarioParameter = idUsuario != null ?
                new ObjectParameter("IdUsuario", idUsuario) :
                new ObjectParameter("IdUsuario", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spaca_corregir_menu", idEmpresaParameter, idSedeParameter, idUsuarioParameter);
        }
    }
}
