using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Reportes.Academico
{
    public class ACA_022_Info
    {
        public Nullable<int> IdEmpresa { get; set; }
        public Nullable<decimal> IdMatricula { get; set; }
        public Nullable<int> IdAnio { get; set; }
        public Nullable<int> IdSede { get; set; }
        public Nullable<int> IdNivel { get; set; }
        public Nullable<int> IdJornada { get; set; }
        public Nullable<int> IdCurso { get; set; }
        public Nullable<int> IdParalelo { get; set; }
        public Nullable<decimal> IdAlumno { get; set; }
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
        public Nullable<bool> EsRetirado { get; set; }
        public Nullable<int> FInjustificadaP1 { get; set; }
        public Nullable<int> FJustificadaP1 { get; set; }
        public Nullable<int> AtrasosP1 { get; set; }
        public Nullable<int> FInjustificadaP2 { get; set; }
        public Nullable<int> FJustificadaP2 { get; set; }
        public Nullable<int> AtrasosP2 { get; set; }
        public Nullable<int> FInjustificadaP3 { get; set; }
        public Nullable<int> FJustificadaP3 { get; set; }
        public Nullable<int> AtrasosP3 { get; set; }
        public Nullable<int> FInjustificadaP4 { get; set; }
        public Nullable<int> FJustificadaP4 { get; set; }
        public Nullable<int> AtrasosP4 { get; set; }
        public Nullable<int> FInjustificadaP5 { get; set; }
        public Nullable<int> FJustificadaP5 { get; set; }
        public Nullable<int> AtrasosP5 { get; set; }
        public Nullable<int> FInjustificadaP6 { get; set; }
        public Nullable<int> FJustificadaP6 { get; set; }
        public Nullable<int> AtrasosP6 { get; set; }
        public Nullable<decimal> IdProfesorInspector { get; set; }
        public string NombreInspector { get; set; }





        public int Num { get; set; }
        public Nullable<int> FaltasInjustificadas { get; set; }
        public Nullable<int> FaltasJustificadas { get; set; }
        public Nullable<int> Atrasos { get; set; }
    }
}
