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
    
    public partial class SPCXC_008_Result
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public int IdBodega { get; set; }
        public decimal IdCbteVta { get; set; }
        public System.DateTime vt_fecha { get; set; }
        public string vt_Observacion { get; set; }
        public string vt_NumFactura { get; set; }
        public decimal IdAlumno { get; set; }
        public string CodigoAlumno { get; set; }
        public string pe_nombreCompleto { get; set; }
        public Nullable<int> Periodo { get; set; }
        public decimal Total { get; set; }
        public double TotalPagado { get; set; }
        public Nullable<double> Saldo { get; set; }
        public System.DateTime vt_fech_venc { get; set; }
        public Nullable<int> Plazo { get; set; }
        public Nullable<decimal> IdMatricula { get; set; }
        public Nullable<int> IdAnio { get; set; }
        public Nullable<int> IdSede { get; set; }
        public Nullable<int> IdNivel { get; set; }
        public Nullable<int> IdJornada { get; set; }
        public Nullable<int> IdCurso { get; set; }
        public Nullable<int> IdParalelo { get; set; }
        public Nullable<int> OrdenNivel { get; set; }
        public Nullable<int> OrdenJornada { get; set; }
        public Nullable<int> OrdenCurso { get; set; }
        public Nullable<int> OrdenParalelo { get; set; }
        public string NomSede { get; set; }
        public string NomNivel { get; set; }
        public string NomJornada { get; set; }
        public string NomCurso { get; set; }
        public string NomParalelo { get; set; }
    }
}
