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
    
    public partial class aca_Alumno
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public aca_Alumno()
        {
            this.aca_AlumnoRetiro = new HashSet<aca_AlumnoRetiro>();
            this.aca_Matricula = new HashSet<aca_Matricula>();
            this.aca_PermisoMatricula = new HashSet<aca_PermisoMatricula>();
            this.aca_AnioLectivoCalificacionHistorico = new HashSet<aca_AnioLectivoCalificacionHistorico>();
            this.aca_AlumnoDocumento = new HashSet<aca_AlumnoDocumento>();
            this.aca_SocioEconomico = new HashSet<aca_SocioEconomico>();
            this.aca_Familia = new HashSet<aca_Familia>();
        }
    
        public int IdEmpresa { get; set; }
        public decimal IdAlumno { get; set; }
        public string Codigo { get; set; }
        public decimal IdPersona { get; set; }
        public string Direccion { get; set; }
        public string Celular { get; set; }
        public string Correo { get; set; }
        public Nullable<bool> Estado { get; set; }
        public int IdCatalogoESTMAT { get; set; }
        public Nullable<int> IdCurso { get; set; }
        public int IdCatalogoESTALU { get; set; }
        public System.DateTime FechaIngreso { get; set; }
        public string LugarNacimiento { get; set; }
        public string IdPais { get; set; }
        public string Cod_Region { get; set; }
        public string IdProvincia { get; set; }
        public string IdCiudad { get; set; }
        public string IdParroquia { get; set; }
        public string Sector { get; set; }
        public string IdUsuarioCreacion { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string IdUsuarioModificacion { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
        public string IdUsuarioAnulacion { get; set; }
        public Nullable<System.DateTime> FechaAnulacion { get; set; }
        public string MotivoAnulacion { get; set; }
    
        public virtual aca_Catalogo aca_Catalogo { get; set; }
        public virtual aca_Catalogo aca_Catalogo1 { get; set; }
        public virtual aca_Curso aca_Curso { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aca_AlumnoRetiro> aca_AlumnoRetiro { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aca_Matricula> aca_Matricula { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aca_PermisoMatricula> aca_PermisoMatricula { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aca_AnioLectivoCalificacionHistorico> aca_AnioLectivoCalificacionHistorico { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aca_AlumnoDocumento> aca_AlumnoDocumento { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aca_SocioEconomico> aca_SocioEconomico { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aca_Familia> aca_Familia { get; set; }
    }
}
