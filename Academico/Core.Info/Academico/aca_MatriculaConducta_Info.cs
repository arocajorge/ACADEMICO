using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Academico
{
    public class aca_MatriculaConducta_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        public decimal IdMatricula { get; set; }
        public Nullable<int> PromedioP1 { get; set; }
        public Nullable<int> PromedioFinalP1 { get; set; }
        public Nullable<int> PromedioP2 { get; set; }
        public Nullable<int> PromedioFinalP2 { get; set; }
        public Nullable<int> PromedioP3 { get; set; }
        public Nullable<int> PromedioFinalP3 { get; set; }
        public Nullable<int> PromedioQ1 { get; set; }
        public Nullable<int> PromedioFinalQ1 { get; set; }
        public Nullable<int> PromedioP4 { get; set; }
        public Nullable<int> PromedioFinalP4 { get; set; }
        public Nullable<int> PromedioP5 { get; set; }
        public Nullable<int> PromedioFinalP5 { get; set; }
        public Nullable<int> PromedioP6 { get; set; }
        public Nullable<int> PromedioFinalP6 { get; set; }
        public Nullable<int> PromedioQ2 { get; set; }
        public Nullable<int> PromedioFinalQ2 { get; set; }
        public Nullable<int> PromedioGeneral { get; set; }
        public Nullable<int> PromedioFinal { get; set; }

        #region Campos que no existen en la tabla
        public int IdAnio { get; set; }
        public int IdSede { get; set; }
        public int IdNivel { get; set; }
        public int IdJornada { get; set; }
        public int IdCurso { get; set; }
        public int IdParalelo { get; set; }
        public int IdCatalogoParcial { get; set; }
        public decimal IdAlumno { get; set; }
        public string pe_nombreCompleto { get; set; }
        public string Descripcion { get; set; }
        public string NomSede { get; set; }
        public string NomNivel { get; set; }
        public string NomJornada { get; set; }
        public string NomCurso { get; set; }
        public string NomParalelo { get; set; }
        public int OrdenNivel { get; set; }
        public int OrdenJornada { get; set; }
        public int OrdenCurso { get; set; }
        public int OrdenParalelo { get; set; }
        public int PromedioParcialFinal { get; set; }
        public int PromedioParcial { get; set; }
        #endregion
    }
}
