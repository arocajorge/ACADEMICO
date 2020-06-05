using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Facturacion
{
    public class fa_notaCreDeb_Masiva_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        public decimal IdNCMasivo { get; set; }
        public int IdSucursal { get; set; }
        public int IdBodega { get; set; }
        public int IdPuntoVta { get; set; }
        public string CreDeb { get; set; }
        public System.DateTime no_fecha { get; set; }
        public System.DateTime no_fecha_venc { get; set; }
        public int IdTipoNota { get; set; }
        public string NaturalezaNota { get; set; }
        public string sc_observacion { get; set; }
        public bool Estado { get; set; }
        public string IdUsuarioCreacion { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string IdUsuarioModificacion { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
        public string IdUsuarioAnulacion { get; set; }
        public Nullable<System.DateTime> FechaAnulacion { get; set; }
        public string MotivoAnulacion { get; set; }

        #region Campos que no existen en la tabla
        public List<fa_notaCreDeb_MasivaDet_Info> lst_det { get; set; }
        public string IdCtaCble_TipoNota { get; set; }
        #endregion
    }
}
