using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Academico
{
    public class aca_AnioLectivo_Curso_Materia_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        public int IdAnio { get; set; }
        public int IdSede { get; set; }
        public int IdNivel { get; set; }
        public int IdJornada { get; set; }
        public int IdCurso { get; set; }
        public int IdMateria { get; set; }
        public string NomMateria { get; set; }
        public bool EsObligatorio { get; set; }
        public int OrdenMateria { get; set; }

        #region Campos que no existen en la tabla
        public bool seleccionado { get; set; }
        #endregion
    }
}
