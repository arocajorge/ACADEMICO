using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.General
{
    public class tb_ColaCorreo_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        public decimal IdCorreo { get; set; }
        public string Codigo { get; set; }
        public string Destinatarios { get; set; }
        public string Asunto { get; set; }
        public string Cuerpo { get; set; }
        public string Parametros { get; set; }
        public string IdUsuarioCreacion { get; set; }
        public string Error { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaEnvio { get; set; }

        #region Campos que no existen en la tabla
        public tb_ColaCorreoParametros_Info ParametroInfo { get; set; }
        public decimal IdAlumno { get; set; }
        public bool RepLegal { get; set; }
        public bool RepEconomico { get; set; }
        public int IdAnio { get; set; }
        public int IdSede { get; set; }
        public int IdNivel { get; set; }
        public int IdJornada { get; set; }
        public int IdCurso { get; set; }
        public int IdParalelo { get; set; }
        public string Copia { get; set; }
        public List<aca_AnioLectivo_Curso_Paralelo_Info> lst_correo_masivo { get; set; }
        #endregion
    }
}
