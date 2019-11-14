using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Academico
{
    public class aca_AnioLectivo_Jornada_Curso_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        [Required(ErrorMessage = "El campo año lectivo es obligatorio")]
        public int IdAnio { get; set; }
        [Required(ErrorMessage = "El campo sede es obligatorio")]
        public int IdSede { get; set; }
        [Required(ErrorMessage = "El campo nivel es obligatorio")]
        public int IdNivel { get; set; }
        [Required(ErrorMessage = "El campo jornada es obligatorio")]
        public int IdJornada { get; set; }
        public int IdCurso { get; set; }
        public string NomCurso { get; set; }
        public int OrdenCurso { get; set; }

        #region Campos que no existen en la base de datos
        public bool seleccionado { get; set; }
        public string NomSede { get; set; }
        public string NomJornada { get; set; }
        public string NomNivel { get; set; }
        public string ComboCurso { get; set; }
        public string IdComboCurso { get; set; }
        #endregion
    }
}
