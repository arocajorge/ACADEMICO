using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Academico
{
    public class aca_AnioLectivo_Paralelo_Profesor_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        public int IdAnio { get; set; }
        public int IdSede { get; set; }
        public int IdNivel { get; set; }
        public int IdJornada { get; set; }
        public int IdCurso { get; set; }
        public int IdMateria { get; set; }
        public int IdParalelo { get; set; }
        public Nullable<decimal> IdProfesor { get; set; }

        #region Campos que no existen en la tabla
        public bool seleccionado { get; set; }
        public string pe_nombreCompleto { get; set; }
        public string Codigo { get; set; }
        #endregion
    }
}
