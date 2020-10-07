using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Reportes.Academico
{
    public class ACA_047_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdMatricula { get; set; }
        public int IdMateria { get; set; }
        public string Codigo { get; set; }
        public string NombreAlumno { get; set; }
        public int IdSede { get; set; }
        public int IdAnio { get; set; }
        public int IdNivel { get; set; }
        public int IdJornada { get; set; }
        public int IdCurso { get; set; }
        public int IdParalelo { get; set; }
        public decimal IdAlumno { get; set; }
        public string Descripcion { get; set; }
        public string NomSede { get; set; }
        public string NomNivel { get; set; }
        public Nullable<int> OrdenNivel { get; set; }
        public string NomJornada { get; set; }
        public Nullable<int> OrdenJornada { get; set; }
        public string NomCurso { get; set; }
        public Nullable<int> OrdenCurso { get; set; }
        public string CodigoParalelo { get; set; }
        public string NomParalelo { get; set; }
        public Nullable<int> OrdenParalelo { get; set; }
        public string NomMateria { get; set; }
        public string NomMateriaArea { get; set; }
        public string NomMateriaGrupo { get; set; }
        public bool EsObligatorio { get; set; }
        public int OrdenMateria { get; set; }
        public Nullable<int> OrdenMateriaGrupo { get; set; }
        public Nullable<int> OrdenMateriaArea { get; set; }
        public Nullable<decimal> IdProfesor { get; set; }
        public string NombreProfesor { get; set; }
        public string CalificacionP1 { get; set; }
        public string CalificacionP2 { get; set; }
        public string CalificacionP3 { get; set; }
        public string PromedioFinalQ1 { get; set; }
        public string CalificacionP4 { get; set; }
        public string CalificacionP5 { get; set; }
        public string CalificacionP6 { get; set; }
        public string PromedioFinalQ2 { get; set; }
        public string PromedioFinal { get; set; }

        #region Campos que no existen
        public int Num { get; set; }
        #endregion
    }
}
