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
    
    public partial class vwaca_Matricula
    {
        public int IdEmpresa { get; set; }
        public decimal IdMatricula { get; set; }
        public string Codigo { get; set; }
        public decimal IdAlumno { get; set; }
        public Nullable<decimal> IdPersona { get; set; }
        public string pe_nombreCompleto { get; set; }
        public string pe_cedulaRuc { get; set; }
        public int IdAnio { get; set; }
        public int IdSede { get; set; }
        public int IdNivel { get; set; }
        public int IdJornada { get; set; }
        public int IdCurso { get; set; }
        public int IdParalelo { get; set; }
        public string NomSede { get; set; }
        public string Descripcion { get; set; }
        public string NomNivel { get; set; }
        public Nullable<int> OrdenNivel { get; set; }
        public string NomJornada { get; set; }
        public Nullable<int> OrdenJornada { get; set; }
        public string NomCurso { get; set; }
        public Nullable<int> OrdenCurso { get; set; }
        public string NomParalelo { get; set; }
        public Nullable<int> OrdenParalelo { get; set; }
        public bool BloquearMatricula { get; set; }
        public decimal IdPersonaF { get; set; }
        public decimal IdPersonaR { get; set; }
        public int IdPlantilla { get; set; }
        public System.DateTime Fecha { get; set; }
        public string Observacion { get; set; }
        public decimal IdMecanismo { get; set; }
        public Nullable<int> IdEmpresa_rol { get; set; }
        public Nullable<decimal> IdEmpleado { get; set; }
        public Nullable<bool> EsRetirado { get; set; }
        public string NomPlantilla { get; set; }
    }
}
