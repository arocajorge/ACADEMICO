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
    
    public partial class aca_AnioLectivo_Sede_NivelAcademico
    {
        public int IdEmpresa { get; set; }
        public int IdAnio { get; set; }
        public int IdSede { get; set; }
        public int IdNivel { get; set; }
        public string NomSede { get; set; }
        public string NomNivel { get; set; }
        public int OrdenNivel { get; set; }
    
        public virtual aca_NivelAcademico aca_NivelAcademico { get; set; }
        public virtual aca_Sede aca_Sede { get; set; }
        public virtual aca_AnioLectivo aca_AnioLectivo { get; set; }
    }
}
