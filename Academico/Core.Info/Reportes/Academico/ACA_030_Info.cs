using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Reportes.Academico
{
    public class ACA_030_Info
    {
        public int IdEmpresa { get; set; }
        public int IdAnio { get; set; }
        public int IdSede { get; set; }
        public int IdNivel { get; set; }
        public int IdJornada { get; set; }
        public int IdCurso { get; set; }
        public int IdParalelo { get; set; }
        public decimal IdAlumno { get; set; }
        public decimal IdMatricula { get; set; }
        public int IdMateria { get; set; }
        public string NomMateria { get; set; }
        public string NomMateriaArea { get; set; }
        public string NomMateriaGrupo { get; set; }
        public Nullable<bool> EsObligatorio { get; set; }
        public int OrdenMateria { get; set; }
        public int OrdenMateriaGrupo { get; set; }
        public int OrdenMateriaArea { get; set; }
        public string Codigo { get; set; }
        public string pe_nombreCompleto { get; set; }
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
        public string NombreInspector { get; set; }
        public string NombreTutor { get; set; }
        public string CalificacionP1 { get; set; }
        public string CalificacionP2 { get; set; }
        public string CalificacionP3 { get; set; }
        public string PromedioQ1 { get; set; }
        public string PorcentajeQ1 { get; set; }
        public string ExamenQ1 { get; set; }
        public string PorcentajeEQ1 { get; set; }
        public string PromedioFinalQ1 { get; set; }
        public string CalificacionP4 { get; set; }
        public string CalificacionP5 { get; set; }
        public string CalificacionP6 { get; set; }
        public string PromedioQ2 { get; set; }
        public string PorcentajeQ2 { get; set; }
        public string ExamenQ2 { get; set; }
        public string PorcentajeEQ2 { get; set; }
        public Nullable<decimal> PromedioFinalQ2 { get; set; }
        public string PromedioQuimestralFinal { get; set; }
        public string ExamenMejoramiento { get; set; }
        public string CampoMejoramiento { get; set; }
        public string ExamenSupletorio { get; set; }
        public string ExamenRemedial { get; set; }
        public string ExamenGracia { get; set; }
        public string PromedioFinal { get; set; }

        #region MyRegion
        public string P1 { get; set; }
        public string P2 { get; set; }
        public string P3 { get; set; }
        public string PROM80 { get; set; }
        public string EXAMEN { get; set; }
        public string EXA20 { get; set; }
        public string PROMFINAL { get; set; }
        #endregion
    }
}
