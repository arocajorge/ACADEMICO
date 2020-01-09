using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Academico
{
    public class aca_AnioLectivo_Curso_Plantilla_Parametrizacion_Info
    {
        public int IdEmpresa { get; set; }
        public int IdAnio { get; set; }
        public int IdSede { get; set; }
        public int IdNivel { get; set; }
        public int IdJornada { get; set; }
        public int IdCurso { get; set; }
        public int IdPlantilla { get; set; }
        public int IdRubro { get; set; }
        public string IdCtaCble { get; set; }

        #region Campos que no existen en la tabla
        public string NomJornada { get; set; }
        public Nullable<int> OrdenJornada { get; set; }
        public string NomCurso { get; set; }
        public Nullable<int> OrdenCurso { get; set; }
        public string NomPlantilla { get; set; }
        public string NomRubro { get; set; }
        public string NomNivel { get; set; }
        public string NomSede { get; set; }
        #endregion
    }
}
