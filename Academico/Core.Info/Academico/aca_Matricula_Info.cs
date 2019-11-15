using System;
using System.Collections.Generic;
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
        public string Codigo { get; set; }
        public decimal IdAlumno { get; set; }
        public int IdAnio { get; set; }
        public int IdSede { get; set; }
        public int IdNivel { get; set; }
        public int IdJornada { get; set; }
        public int IdCurso { get; set; }
        public int IdParalelo { get; set; }
        public decimal IdPersonaF { get; set; }
        public decimal IdPersonaR { get; set; }
        public int IdPlantilla { get; set; }
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
        public string pe_cedulaRuc { get; set; }
        public string pe_nombreCompleto { get; set; }
        public string NomSede { get; set; }
        public string NomNivel { get; set; }
        public string NomJornada { get; set; }
        public string NomParalelo { get; set; }
        public string Descripcion { get; set; }
        public string NomCurso { get; set; }
        #endregion
    }
}
