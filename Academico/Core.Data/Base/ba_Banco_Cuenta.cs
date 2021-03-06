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
    
    public partial class ba_Banco_Cuenta
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ba_Banco_Cuenta()
        {
            this.ba_Banco_Cuenta_x_tb_sucursal = new HashSet<ba_Banco_Cuenta_x_tb_sucursal>();
            this.ba_Cbte_Ban = new HashSet<ba_Cbte_Ban>();
            this.ba_Conciliacion = new HashSet<ba_Conciliacion>();
            this.ba_Talonario_cheques_x_banco = new HashSet<ba_Talonario_cheques_x_banco>();
            this.ba_TipoFlujo_Movimiento = new HashSet<ba_TipoFlujo_Movimiento>();
            this.ba_Archivo_Transferencia = new HashSet<ba_Archivo_Transferencia>();
            this.ba_ArchivoRecaudacion = new HashSet<ba_ArchivoRecaudacion>();
        }
    
        public int IdEmpresa { get; set; }
        public int IdBanco { get; set; }
        public string ba_descripcion { get; set; }
        public string ba_Tipo { get; set; }
        public string ba_Num_Cuenta { get; set; }
        public int ba_num_digito_cheq { get; set; }
        public string IdCtaCble { get; set; }
        public string IdUsuario { get; set; }
        public Nullable<System.DateTime> Fecha_Transac { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string Estado { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public Nullable<System.DateTime> Fecha_UltAnu { get; set; }
        public string MotiAnula { get; set; }
        public byte[] ReporteChequeComprobante { get; set; }
        public byte[] ReporteCheque { get; set; }
        public bool Imprimir_Solo_el_cheque { get; set; }
        public int IdBanco_Financiero { get; set; }
        public bool EsFlujoObligatorio { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ba_Banco_Cuenta_x_tb_sucursal> ba_Banco_Cuenta_x_tb_sucursal { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ba_Cbte_Ban> ba_Cbte_Ban { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ba_Conciliacion> ba_Conciliacion { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ba_Talonario_cheques_x_banco> ba_Talonario_cheques_x_banco { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ba_TipoFlujo_Movimiento> ba_TipoFlujo_Movimiento { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ba_Archivo_Transferencia> ba_Archivo_Transferencia { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ba_ArchivoRecaudacion> ba_ArchivoRecaudacion { get; set; }
    }
}
