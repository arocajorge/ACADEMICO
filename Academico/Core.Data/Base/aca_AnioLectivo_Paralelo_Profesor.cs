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
    
    public partial class aca_AnioLectivo_Paralelo_Profesor
    {
        public int IdEmpresa { get; set; }
        public int IdAnio { get; set; }
        public int IdSede { get; set; }
        public int IdNivel { get; set; }
        public int IdJornada { get; set; }
        public int IdCurso { get; set; }
        public int IdMateria { get; set; }
        public int IdParalelo { get; set; }
        public Nullable<decimal> IdProfesor { get; set; }
    
        public virtual aca_Jornada aca_Jornada { get; set; }
        public virtual aca_Materia aca_Materia { get; set; }
        public virtual aca_NivelAcademico aca_NivelAcademico { get; set; }
        public virtual aca_Paralelo aca_Paralelo { get; set; }
        public virtual aca_Sede aca_Sede { get; set; }
        public virtual aca_AnioLectivo aca_AnioLectivo { get; set; }
        public virtual aca_Profesor aca_Profesor { get; set; }
        public virtual aca_Curso aca_Curso { get; set; }
    }
}
