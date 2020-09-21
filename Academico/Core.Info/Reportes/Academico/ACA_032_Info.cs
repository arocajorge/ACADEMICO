using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Reportes.Academico
{
    public class ACA_032_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdMatricula { get; set; }
        public Nullable<int> IdAnio { get; set; }
        public Nullable<int> IdSede { get; set; }
        public Nullable<int> IdNivel { get; set; }
        public Nullable<int> IdJornada { get; set; }
        public Nullable<int> IdCurso { get; set; }
        public Nullable<int> IdParalelo { get; set; }
        public Nullable<decimal> IdAlumno { get; set; }
        public string Codigo { get; set; }
        public string pe_nombreCompleto { get; set; }
        public Nullable<bool> EsRetirado { get; set; }
        public string EsRetiradoString { get; set; }
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
        public Nullable<int> SecuenciaQ1 { get; set; }
        public string LetraQ1 { get; set; }
        public string MotivoPromedioFinalQ1 { get; set; }
        public Nullable<int> SecuenciaQ2 { get; set; }
        public string LetraQ2 { get; set; }
        public string MotivoPromedioFinalQ2 { get; set; }
        public Nullable<int> SecuenciaPF { get; set; }
        public string LetraPF { get; set; }
        public string MotivoPromedioFinal { get; set; }
        public Nullable<decimal> IdProfesorInspector { get; set; }
        public string NombreInspector { get; set; }



        public int Num { get; set; }
        public string Letra { get; set; }
        public string Motivo { get; set; }
    }
}
