//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class aca_Sede
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public aca_Sede()
        {
            this.aca_AnioLectivo_Curso_Materia = new HashSet<aca_AnioLectivo_Curso_Materia>();
            this.aca_AnioLectivo_Curso_Paralelo = new HashSet<aca_AnioLectivo_Curso_Paralelo>();
            this.aca_AnioLectivo_Jornada_Curso = new HashSet<aca_AnioLectivo_Jornada_Curso>();
            this.aca_AnioLectivo_NivelAcademico_Jornada = new HashSet<aca_AnioLectivo_NivelAcademico_Jornada>();
            this.aca_AnioLectivo_Paralelo_Profesor = new HashSet<aca_AnioLectivo_Paralelo_Profesor>();
            this.aca_AnioLectivo_Sede_NivelAcademico = new HashSet<aca_AnioLectivo_Sede_NivelAcademico>();
            this.aca_AnioLectivoParcial = new HashSet<aca_AnioLectivoParcial>();
            this.aca_Menu_x_aca_Sede = new HashSet<aca_Menu_x_aca_Sede>();
            this.aca_Menu_x_seg_usuario = new HashSet<aca_Menu_x_seg_usuario>();
        }
    
        public int IdEmpresa { get; set; }
        public int IdSede { get; set; }
        public int IdSucursal { get; set; }
        public string NomSede { get; set; }
        public string Direccion { get; set; }
        public string NombreRector { get; set; }
        public string TelefonoRector { get; set; }
        public string CelularRector { get; set; }
        public string CorreoRector { get; set; }
        public string NombreSecretaria { get; set; }
        public bool Estado { get; set; }
        public string IdUsuarioCreacion { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string IdUsuarioModificacion { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
        public string IdUsuarioAnulacion { get; set; }
        public Nullable<System.DateTime> FechaAnulacion { get; set; }
        public string MotivoAnulacion { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aca_AnioLectivo_Curso_Materia> aca_AnioLectivo_Curso_Materia { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aca_AnioLectivo_Curso_Paralelo> aca_AnioLectivo_Curso_Paralelo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aca_AnioLectivo_Jornada_Curso> aca_AnioLectivo_Jornada_Curso { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aca_AnioLectivo_NivelAcademico_Jornada> aca_AnioLectivo_NivelAcademico_Jornada { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aca_AnioLectivo_Paralelo_Profesor> aca_AnioLectivo_Paralelo_Profesor { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aca_AnioLectivo_Sede_NivelAcademico> aca_AnioLectivo_Sede_NivelAcademico { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aca_AnioLectivoParcial> aca_AnioLectivoParcial { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aca_Menu_x_aca_Sede> aca_Menu_x_aca_Sede { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aca_Menu_x_seg_usuario> aca_Menu_x_seg_usuario { get; set; }
    }
}
