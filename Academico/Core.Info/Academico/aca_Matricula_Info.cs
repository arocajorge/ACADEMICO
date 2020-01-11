using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Academico
{
    public class aca_Matricula_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        public decimal IdMatricula { get; set; }
        [Required(ErrorMessage = "El campo código es obligatorio")]
        public string Codigo { get; set; }
        [Required(ErrorMessage = "El campo alumno es obligatorio")]
        public decimal IdAlumno { get; set; }
        [Required(ErrorMessage = "El campo año lectivo es obligatorio")]
        public int IdAnio { get; set; }
        public int IdSede { get; set; }
        public int IdNivel { get; set; }
        public int IdJornada { get; set; }
        public int IdCurso { get; set; }
        [Required(ErrorMessage = "El campo curso es obligatorio")]
        public int IdParalelo { get; set; }
        public decimal IdPersonaF { get; set; }
        public decimal IdPersonaR { get; set; }
        [Required(ErrorMessage = "El campo plantilla es obligatorio")]
        public int IdPlantilla { get; set; }
        [Required(ErrorMessage = "El campo mecanismo de pago es obligatorio")]
        public decimal IdMecanismo { get; set; }
        [Required(ErrorMessage = "El campo fecha es obligatorio")]
        public System.DateTime Fecha { get; set; }
        public string Observacion { get; set; }
        public string IdUsuarioCreacion { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string IdUsuarioModificacion { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
        public string IdUsuarioAnulacion { get; set; }
        public Nullable<System.DateTime> FechaAnulacion { get; set; }
        public string MotivoAnulacion { get; set; }

        #region Campos que no existen en la tabla
        public string IdComboCurso { get; set; }
        public List<aca_Matricula_Info> lst_matricula_curso { get; set; }
        public List<aca_AnioLectivo_Curso_Documento_Info> lst_alumno_documentos { get; set; }
        public List<aca_AlumnoDocumento_Info> lst_documentos { get; set; }
        public string pe_cedulaRuc { get; set; }
        public string pe_nombreCompleto { get; set; }
        public string NomSede { get; set; }
        public string NomNivel { get; set; }
        public string NomJornada { get; set; }
        public string NomParalelo { get; set; }
        public string Descripcion { get; set; }
        public string NomCurso { get; set; }
        public string Validar { get; set; }
        public double ValorPlantilla { get; set; }
        public double ValorPlantillaProntoPago { get; set; }
        public List<aca_Matricula_Rubro_Info> lst_MatriculaRubro { get; set; }
        public bool BloquearMatricula { get; set; }
        [Required(ErrorMessage = "El campo mecanismo de pago del rubro es obligatorio")]
        public decimal IdMecanismoDet { get; set; }

        public List<aca_MatriculaCalificacion_Info> lst_calificacion { get; set; }
        public List<aca_MatriculaConducta_Info> lst_conducta { get; set; }
        #endregion
    }
}
