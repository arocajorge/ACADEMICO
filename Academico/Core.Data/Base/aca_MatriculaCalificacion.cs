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
    
    public partial class aca_MatriculaCalificacion
    {
        public int IdEmpresa { get; set; }
        public decimal IdMatricula { get; set; }
        public int IdMateria { get; set; }
        public Nullable<decimal> IdProfesor { get; set; }
        public Nullable<decimal> CalificacionP1 { get; set; }
        public Nullable<int> IdEquivalenciaPromedioP1 { get; set; }
        public Nullable<decimal> CalificacionP2 { get; set; }
        public Nullable<int> IdEquivalenciaPromedioP2 { get; set; }
        public Nullable<decimal> CalificacionP3 { get; set; }
        public Nullable<int> IdEquivalenciaPromedioP3 { get; set; }
        public Nullable<decimal> PromedioQ1 { get; set; }
        public Nullable<decimal> ExamenQ1 { get; set; }
        public Nullable<int> IdEquivalenciaPromedioEQ1 { get; set; }
        public Nullable<decimal> PromedioFinalQ1 { get; set; }
        public Nullable<int> IdEquivalenciaPromedioQ1 { get; set; }
        public string CausaQ1 { get; set; }
        public string ResolucionQ1 { get; set; }
        public Nullable<decimal> CalificacionP4 { get; set; }
        public Nullable<int> IdEquivalenciaPromedioP4 { get; set; }
        public Nullable<decimal> CalificacionP5 { get; set; }
        public Nullable<int> IdEquivalenciaPromedioP5 { get; set; }
        public Nullable<decimal> CalificacionP6 { get; set; }
        public Nullable<int> IdEquivalenciaPromedioP6 { get; set; }
        public Nullable<decimal> PromedioQ2 { get; set; }
        public Nullable<decimal> ExamenQ2 { get; set; }
        public Nullable<int> IdEquivalenciaPromedioEQ2 { get; set; }
        public Nullable<decimal> PromedioFinalQ2 { get; set; }
        public Nullable<int> IdEquivalenciaPromedioQ2 { get; set; }
        public string CausaQ2 { get; set; }
        public string ResolucionQ2 { get; set; }
        public Nullable<decimal> PromedioQuimestres { get; set; }
        public Nullable<decimal> ExamenMejoramiento { get; set; }
        public string CampoMejoramiento { get; set; }
        public Nullable<decimal> ExamenSupletorio { get; set; }
        public Nullable<decimal> ExamenRemedial { get; set; }
        public Nullable<decimal> ExamenGracia { get; set; }
        public Nullable<decimal> PromedioFinal { get; set; }
        public Nullable<int> IdEquivalenciaPromedioPF { get; set; }
    
        public virtual aca_Matricula aca_Matricula { get; set; }
    }
}
