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
    
    public partial class vwba_ArchivoRecaudacion_Archivo
    {
        public int IdEmpresa { get; set; }
        public decimal IdArchivo { get; set; }
        public int Secuencia { get; set; }
        public string Nom_Archivo { get; set; }
        public int IdBanco { get; set; }
        public string ba_Num_Cuenta { get; set; }
        public string CodigoLegal { get; set; }
        public Nullable<decimal> IdMatricula { get; set; }
        public decimal IdAlumno { get; set; }
        public string IdTipoDocumento { get; set; }
        public string pe_cedulaRuc { get; set; }
        public string CodigoAlumno { get; set; }
        public string NomAlumno { get; set; }
        public double Valor { get; set; }
        public double ValorProntoPago { get; set; }
        public int SecuencialDescarga { get; set; }
        public System.DateTime Fecha { get; set; }
        public string Observacion { get; set; }
        public Nullable<System.DateTime> FechaProceso { get; set; }
        public Nullable<System.DateTime> FechaProntoPago { get; set; }
    }
}
