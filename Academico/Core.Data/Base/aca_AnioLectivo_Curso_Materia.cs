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
    
    public partial class aca_AnioLectivo_Curso_Materia
    {
        public int IdEmpresa { get; set; }
        public int IdAnio { get; set; }
        public int IdSede { get; set; }
        public int IdNivel { get; set; }
        public int IdJornada { get; set; }
        public int IdCurso { get; set; }
        public int IdMateria { get; set; }
        public string NomMateria { get; set; }
        public string NomMateriaArea { get; set; }
        public string NomMateriaGrupo { get; set; }
        public Nullable<int> IdCatalogoTipoCalificacion { get; set; }
        public bool EsObligatorio { get; set; }
        public int OrdenMateria { get; set; }
        public Nullable<int> OrdenMateriaGrupo { get; set; }
        public Nullable<int> OrdenMateriaArea { get; set; }
        public Nullable<bool> PromediarGrupo { get; set; }
    
        public virtual aca_Curso aca_Curso { get; set; }
        public virtual aca_Jornada aca_Jornada { get; set; }
        public virtual aca_Materia aca_Materia { get; set; }
        public virtual aca_NivelAcademico aca_NivelAcademico { get; set; }
        public virtual aca_Sede aca_Sede { get; set; }
        public virtual aca_AnioLectivo aca_AnioLectivo { get; set; }
    }
}
