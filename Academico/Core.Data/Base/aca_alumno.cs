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
            this.aca_Familia = new HashSet<aca_Familia>();
            this.aca_Matricula = new HashSet<aca_Matricula>();
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
        public string MotivoNoMatricula { get; set; }
        public System.DateTime FechaIngreso { get; set; }
        public string IdUsuarioCreacion { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string IdUsuarioModificacion { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
        public string IdUsuarioAnulacion { get; set; }
        public Nullable<System.DateTime> FechaAnulacion { get; set; }
        public string MotivoAnulacion { get; set; }
    
        public virtual aca_Catalogo aca_Catalogo { get; set; }
        public virtual aca_Catalogo aca_Catalogo1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aca_Familia> aca_Familia { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aca_Matricula> aca_Matricula { get; set; }
        public virtual aca_Curso aca_Curso { get; set; }
    }
}
