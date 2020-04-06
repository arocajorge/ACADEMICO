using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Academico
{
    public class aca_MatriculaCalificacion_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        public decimal IdMatricula { get; set; }
        public int IdMateria { get; set; }
        public Nullable<decimal> IdProfesor { get; set; }
        public Nullable<decimal> CalificacionP1 { get; set; }
        public Nullable<decimal> CalificacionP2 { get; set; }
        public Nullable<decimal> CalificacionP3 { get; set; }
        public Nullable<decimal> PromedioQ1 { get; set; }
        public Nullable<decimal> ExamenQ1 { get; set; }
        public Nullable<decimal> PromedioFinalQ1 { get; set; }
        public Nullable<decimal> CalificacionP4 { get; set; }
        public Nullable<decimal> CalificacionP5 { get; set; }
        public Nullable<decimal> CalificacionP6 { get; set; }
        public Nullable<decimal> PromedioQ2 { get; set; }
        public Nullable<decimal> ExamenQ2 { get; set; }
        public Nullable<decimal> PromedioFinalQ2 { get; set; }
        public Nullable<decimal> ExamenMejoramiento { get; set; }
        public Nullable<decimal> ExamenSupletorio { get; set; }
        public Nullable<decimal> ExamenRemedial { get; set; }
        public Nullable<decimal> ExamenGracia { get; set; }
        public Nullable<decimal> PromedioFinal { get; set; }

        #region Campos que no existen en la tabla
        public int IdAnio { get; set; }
        public int IdSede { get; set; }
        public int IdNivel { get; set; }
        public int IdJornada { get; set; }
        public int IdCurso { get; set; }
        public int IdParalelo { get; set; }
        public decimal IdAlumno { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string NomSede { get; set; }
        public string NomNivel { get; set; }
        public string NomJornada { get; set; }
        public string NomCurso { get; set; }
        public string NomParalelo { get; set; }
        public string CodigoParalelo { get; set; }
        public string NomMateria { get; set; }
        public int OrdenNivel { get; set; }
        public int OrdenJornada { get; set; }
        public int OrdenCurso { get; set; }
        public int OrdenParalelo { get; set; }
        public int OrdenMateria { get; set; }
        public decimal IdProfesorTutor { get; set; }
        public decimal IdProfesorInspector { get; set; }
        public bool EsObligatorio { get; set; }
        public string pe_nombreCompletoAlumno { get; set; }
        public string pe_nombreCompleto { get; set; }
        public decimal CalificacionExamen { get; set; }
        public int IdCatalogoParcial { get; set; }
        #endregion
    }
}
