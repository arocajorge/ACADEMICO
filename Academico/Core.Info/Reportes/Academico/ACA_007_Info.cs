using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Reportes.Academico
{
    public class ACA_007_Info
    {
        public string NomSede { get; set; }
        public string NomNivel { get; set; }
        public Nullable<int> OrdenNivel { get; set; }
        public string NomJornada { get; set; }
        public Nullable<int> OrdenJornada { get; set; }
        public int OrdenCurso { get; set; }
        public string NomCurso { get; set; }
        public string CodigoParalelo { get; set; }
        public string NomParalelo { get; set; }
        public Nullable<int> OrdenParalelo { get; set; }
        public string pe_sexo { get; set; }
        public int Cantidad { get; set; }
        public Nullable<int> IdEmpresa { get; set; }
        public Nullable<decimal> IdMatricula { get; set; }
        public Nullable<int> IdAnio { get; set; }
        public Nullable<int> IdSede { get; set; }
        public Nullable<int> IdNivel { get; set; }
        public Nullable<int> IdJornada { get; set; }
        public Nullable<int> IdCurso { get; set; }
        public Nullable<int> IdParalelo { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }
        public string NomPlantilla { get; set; }
        public Nullable<int> IdPlantilla { get; set; }
        public string Descripcion { get; set; }
        public Nullable<int> IdTipoPlantilla { get; set; }
        public string NomPlantillaTipo { get; set; }
        public Nullable<bool> EsRetirado { get; set; }
        public string EsRetiradoString { get; set; }
    }
}
