using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Academico
{
    public class aca_AnioLectivo_Curso_Materia_Info
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
        public int IdMateria { get; set; }
        [StringLength(500, MinimumLength = 1, ErrorMessage = "el campo materia debe tener mínimo 1 caracter y máximo 500")]
        [Required(ErrorMessage = "El campo materia es obligatorio")]
        public string NomMateria { get; set; }
        [StringLength(500, MinimumLength = 1, ErrorMessage = "el campo área de materia debe tener mínimo 1 caracter y máximo 500")]
        [Required(ErrorMessage = "El campo área de materia es obligatorio")]
        public string NomMateriaArea { get; set; }
        [StringLength(500, MinimumLength = 1, ErrorMessage = "el campo grupo de materia debe tener mínimo 1 caracter y máximo 500")]
        [Required(ErrorMessage = "El campo grupo de materia es obligatorio")]
        public string NomMateriaGrupo { get; set; }
        public bool EsObligatorio { get; set; }
        public int OrdenMateria { get; set; }
        public Nullable<int> OrdenMateriaGrupo { get; set; }
        public Nullable<int> OrdenMateriaArea { get; set; }

        #region Campos que no existen en la tabla
        public bool seleccionado { get; set; }
        #endregion
    }
}
