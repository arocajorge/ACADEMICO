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
    
        public virtual DbSet<aca_AnioLectivo> aca_AnioLectivo { get; set; }
        public virtual DbSet<aca_AnioLectivo_Curso_Materia> aca_AnioLectivo_Curso_Materia { get; set; }
        public virtual DbSet<aca_AnioLectivo_Curso_Paralelo> aca_AnioLectivo_Curso_Paralelo { get; set; }
        public virtual DbSet<aca_AnioLectivo_Jornada_Curso> aca_AnioLectivo_Jornada_Curso { get; set; }
        public virtual DbSet<aca_AnioLectivo_NivelAcademico_Jornada> aca_AnioLectivo_NivelAcademico_Jornada { get; set; }
        public virtual DbSet<aca_AnioLectivo_Paralelo_Profesor> aca_AnioLectivo_Paralelo_Profesor { get; set; }
        public virtual DbSet<aca_AnioLectivo_Sede_NivelAcademico> aca_AnioLectivo_Sede_NivelAcademico { get; set; }
        public virtual DbSet<aca_Curso> aca_Curso { get; set; }
        public virtual DbSet<aca_Jornada> aca_Jornada { get; set; }
        public virtual DbSet<aca_Materia> aca_Materia { get; set; }
        public virtual DbSet<aca_MateriaGrupo> aca_MateriaGrupo { get; set; }
        public virtual DbSet<aca_NivelAcademico> aca_NivelAcademico { get; set; }
        public virtual DbSet<aca_Paralelo> aca_Paralelo { get; set; }
        public virtual DbSet<aca_Sede> aca_Sede { get; set; }
        public virtual DbSet<aca_Menu> aca_Menu { get; set; }
        public virtual DbSet<aca_Menu_x_aca_Sede> aca_Menu_x_aca_Sede { get; set; }
        public virtual DbSet<aca_Menu_x_seg_usuario> aca_Menu_x_seg_usuario { get; set; }
        public virtual DbSet<aca_alumno> aca_alumno { get; set; }
        public virtual DbSet<aca_Catalogo> aca_Catalogo { get; set; }
        public virtual DbSet<aca_CatalogoTipo> aca_CatalogoTipo { get; set; }
        public virtual DbSet<aca_familia> aca_familia { get; set; }
        public virtual DbSet<aca_Profesor> aca_Profesor { get; set; }
        public virtual DbSet<aca_rubro> aca_rubro { get; set; }
        public virtual DbSet<vwaca_Profesor> vwaca_Profesor { get; set; }
        public virtual DbSet<vwaca_Materia> vwaca_Materia { get; set; }
        public virtual DbSet<vwaca_alumno> vwaca_alumno { get; set; }
        public virtual DbSet<vwaca_familia> vwaca_familia { get; set; }
    
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
