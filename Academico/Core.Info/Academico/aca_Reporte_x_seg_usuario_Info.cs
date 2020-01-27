using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Academico
{
    public class aca_Reporte_x_seg_usuario_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public string IdUsuario { get; set; }
        public int IdEmpresa { get; set; }
        public string CodReporte { get; set; }

        #region Campos que no existen en la tabla
        public string nom_reporte { get; set; }
        public bool seleccionado { get; set; }
        public object observacion { get; set; }
        public string mvc_accion { get; set; }
        public string mvc_controlador { get; set; }
        public string mvc_area { get; set; }
        #endregion
    }
}
