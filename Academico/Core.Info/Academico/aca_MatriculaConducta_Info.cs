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
        public Nullable<int> SecuenciaPromedioP1 { get; set; }
        public Nullable<double> PromedioP1 { get; set; }
        public Nullable<int> SecuenciaPromedioFinalP1 { get; set; }
        public Nullable<double> PromedioFinalP1 { get; set; }
        public string MotivoPromedioFinalP1 { get; set; }
        public Nullable<int> SecuenciaPromedioP2 { get; set; }
        public Nullable<double> PromedioP2 { get; set; }
        public Nullable<int> SecuenciaPromedioFinalP2 { get; set; }
        public Nullable<double> PromedioFinalP2 { get; set; }
        public string MotivoPromedioFinalP2 { get; set; }
        public Nullable<int> SecuenciaPromedioP3 { get; set; }
        public Nullable<double> PromedioP3 { get; set; }
        public Nullable<int> SecuenciaPromedioFinalP3 { get; set; }
        public Nullable<double> PromedioFinalP3 { get; set; }
        public string MotivoPromedioFinalP3 { get; set; }
        public Nullable<int> SecuenciaPromedioQ1 { get; set; }
        public Nullable<double> PromedioQ1 { get; set; }
        public Nullable<int> SecuenciaPromedioFinalQ1 { get; set; }
        public Nullable<double> PromedioFinalQ1 { get; set; }
        public string MotivoPromedioFinalQ1 { get; set; }
        public Nullable<int> SecuenciaPromedioP4 { get; set; }
        public Nullable<double> PromedioP4 { get; set; }
        public Nullable<int> SecuenciaPromedioFinalP4 { get; set; }
        public Nullable<double> PromedioFinalP4 { get; set; }
        public string MotivoPromedioFinalP4 { get; set; }
        public Nullable<int> SecuenciaPromedioP5 { get; set; }
        public Nullable<double> PromedioP5 { get; set; }
        public Nullable<int> SecuenciaPromedioFinalP5 { get; set; }
        public Nullable<double> PromedioFinalP5 { get; set; }
        public string MotivoPromedioFinalP5 { get; set; }
        public Nullable<int> SecuenciaPromedioP6 { get; set; }
        public Nullable<double> PromedioP6 { get; set; }
        public Nullable<int> SecuenciaPromedioFinalP6 { get; set; }
        public Nullable<double> PromedioFinalP6 { get; set; }
        public string MotivoPromedioFinalP6 { get; set; }
        public Nullable<int> SecuenciaPromedioQ2 { get; set; }
        public Nullable<double> PromedioQ2 { get; set; }
        public Nullable<int> SecuenciaPromedioFinalQ2 { get; set; }
        public Nullable<double> PromedioFinalQ2 { get; set; }
        public string MotivoPromedioFinalQ2 { get; set; }
        public Nullable<int> SecuenciaPromedioGeneral { get; set; }
        public Nullable<double> PromedioGeneral { get; set; }
        public Nullable<int> SecuenciaPromedioFinal { get; set; }
        public Nullable<double> PromedioFinal { get; set; }
        public string MotivoPromedioFinal { get; set; }

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
        public decimal IdProfesorTutor { get; set; }
        public decimal IdProfesorInspector { get; set; }
        public string CodigoParalelo { get; set; }

        public int SecuenciaConductaPromedioParcial { get; set; }
        public double ConductaPromedioParcial { get; set; }
        public int SecuenciaConductaPromedioParcialFinal { get; set; }
        public double ConductaPromedioParcialFinal { get; set; }
        public string MotivoPromedioParcialFinal { get; set; }
        public string IdPromedioFinal { get; set; }
        public int IdCatalogoTipo { get; set; }
        public int IdMateria { get; set; }
        #endregion
    }
}
