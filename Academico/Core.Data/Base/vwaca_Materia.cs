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
    
    public partial class vwaca_Materia
    {
        public int IdEmpresa { get; set; }
        public int IdMateria { get; set; }
        public Nullable<int> IdMateriaGrupo { get; set; }
        public string NomMateriaGrupo { get; set; }
        public int OrdenMateriaGrupo { get; set; }
        public string NomMateria { get; set; }
        public bool EsObligatorio { get; set; }
        public int OrdenMateria { get; set; }
        public bool Estado { get; set; }
        public Nullable<int> IdMateriaArea { get; set; }
    }
}
