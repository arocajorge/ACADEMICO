using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Academico
{
    public class aca_MatriculaCalificacionParcial_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        public decimal IdMatricula { get; set; }
        public int IdMateria { get; set; }
        public int IdCatalogoParcial { get; set; }
        public Nullable<decimal> IdProfesor { get; set; }
        public Nullable<decimal> Calificacion1 { get; set; }
        public Nullable<decimal> Calificacion2 { get; set; }
        public Nullable<decimal> Calificacion3 { get; set; }
        public Nullable<decimal> Calificacion4 { get; set; }
        public Nullable<decimal> Evaluacion { get; set; }
        public Nullable<decimal> Remedial1 { get; set; }
        public Nullable<decimal> Remedial2 { get; set; }
        [Required(ErrorMessage = "El campo conducta es obligatorio")]
        public Nullable<int> Conducta { get; set; }
        public string MotivoCalificacion { get; set; }
        public string MotivoConducta { get; set; }
        public string AccionRemedial { get; set; }
        public string IdUsuarioCreacion { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string IdUsuarioModificacion { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }


        #region Campos que no existen en la tabla
        public int IdAnio { get; set; }
        public int IdSede { get; set; }
        public int IdNivel { get; set; }
        public int IdJornada { get; set; }
        public int IdCurso { get; set; }
        public int IdParalelo { get; set; }
        public decimal IdAlumno { get; set; }
        public string Codigo { get; set; }
        public string pe_nombreCompleto { get; set; }
        public decimal PromedioParcial { get; set; }
        #endregion
    }
}
