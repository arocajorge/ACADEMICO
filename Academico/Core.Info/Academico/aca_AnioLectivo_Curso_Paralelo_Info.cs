using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Academico
{
    public class aca_AnioLectivo_Curso_Paralelo_Info
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
        public int IdParalelo { get; set; }
        public string CodigoParalelo { get; set; }
        public string NomParalelo { get; set; }
        public int OrdenParalelo { get; set; }
        public Nullable<decimal> IdProfesorTutor { get; set; }
        public Nullable<decimal> IdProfesorInspector { get; set; }

        #region Campos que no existen en la tabla
        public bool seleccionado { get; set; }
        public decimal IdAlumno { get; set; }
        public string NomSede { get; set; }
        public string Descripcion { get; set; }
        public string NomNivel { get; set; }
        public string NomJornada { get; set; }
        public string NomCurso { get; set; }
        public string NomTutor { get; set; }
        public string NomInspector { get; set; }
        public decimal IdMatricula { get; set; }
        public List<aca_AnioLectivo_Curso_Paralelo_Info> lst_detalle { get; set; }
        public string Validar { get; set; }

        /*correo*/
        public int OrdenJornada { get; set; }
        public int OrdenNivel { get; set; }
        public int OrdenCurso { get; set; }
        public string IdString { get; set; }
        #endregion
    }
}
