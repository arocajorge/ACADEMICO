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
    
    public partial class SPACA_029_PromediosBajos_Result
    {
        public int IdEmpresa { get; set; }
        public decimal IdMatricula { get; set; }
        public Nullable<int> IdAnio { get; set; }
        public Nullable<int> IdSede { get; set; }
        public Nullable<int> IdNivel { get; set; }
        public Nullable<int> IdJornada { get; set; }
        public Nullable<int> IdCurso { get; set; }
        public Nullable<int> IdParalelo { get; set; }
        public Nullable<decimal> IdAlumno { get; set; }
        public string Codigo { get; set; }
        public string pe_nombreCompleto { get; set; }
        public int IdMateria { get; set; }
        public string NomMateria { get; set; }
        public Nullable<int> OrdenMateriaGrupo { get; set; }
        public Nullable<int> OrdenMateria { get; set; }
        public Nullable<decimal> PromedioFinalQ1 { get; set; }
        public string CausaQ1 { get; set; }
        public string ResolucionQ1 { get; set; }
        public Nullable<decimal> PromedioFinalQ2 { get; set; }
        public string CausaQ2 { get; set; }
        public string ResolucionQ2 { get; set; }
        public Nullable<double> PromedioMinimoPromocion { get; set; }
    }
}
