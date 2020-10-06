using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Academico
{
    public class aca_MatriculaCalificacionParticipacion_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        public decimal IdAlumno { get; set; }
        public Nullable<decimal> IdMatricula { get; set; }
        public int IdTematica { get; set; }
        public int IdCampoAccion { get; set; }
        public Nullable<decimal> IdProfesor { get; set; }
        public Nullable<decimal> CalificacionP1 { get; set; }
        public Nullable<decimal> CalificacionP2 { get; set; }
        public Nullable<decimal> PromedioQ1 { get; set; }
        public Nullable<decimal> CalificacionP3 { get; set; }
        public Nullable<decimal> CalificacionP4 { get; set; }
        public Nullable<decimal> PromedioQ2 { get; set; }
        public string PromedioFinal { get; set; }
        public string IdUsuarioCreacion { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string IdUsuarioModificacion { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }

        #region Campos que no existen en la tabla
        public string NombreProfesor { get; set; }
        public int IdAnio { get; set; }
        public int IdSede { get; set; }
        public int IdNivel { get; set; }
        public int IdJornada { get; set; }
        public int IdCurso { get; set; }
        public int IdParalelo { get; set; }
        public string NomParalelo { get; set; }
        public int OrdenParalelo { get; set; }
        public Nullable<int> IdTematicaParticipacion { get; set; }
        public List<aca_MatriculaCalificacionParticipacion_Info> lst_detalle { get; set; }
        public string NombreCampoAccion { get; set; }
        public string NombreTematica { get; set; }
        public string IdString { get; set; }
        #endregion
    }
}
