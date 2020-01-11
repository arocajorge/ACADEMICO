using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Academico
{
    public class aca_AnioLectivo_Paralelo_Profesor_Info
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
        [Required(ErrorMessage = "El campo curso es obligatorio")]
        public int IdCurso { get; set; }
        [Required(ErrorMessage = "El campo materia es obligatorio")]
        public int IdMateria { get; set; }
        [Required(ErrorMessage = "El campo paralelo es obligatorio")]
        public int IdParalelo { get; set; }
        public Nullable<decimal> IdProfesor { get; set; }

        #region Campos que no existen en la tabla
        public bool seleccionado { get; set; }
        public string pe_nombreCompleto { get; set; }
        public string Codigo { get; set; }
        public string NomMateria { get; set; }
        #endregion
    }
}
