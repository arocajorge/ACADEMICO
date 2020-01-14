using System;
using System.Collections.Generic;

namespace Core.Info.CuentasPorCobrar
{
    public class cxc_ConciliacionNotaCredito_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdConciliacion { get; set; }
        public int IdSucursal { get; set; }
        public int IdBodega { get; set; }
        public decimal IdNota { get; set; }
        public decimal IdCobro { get; set; }
        public System.DateTime Fecha { get; set; }
        public double Valor { get; set; }
        public string Observacion { get; set; }
        public bool Estado { get; set; }


        #region Auditoria
        public string IdUsuarioCreacion { get; set; }
        public string MotivoAnulacion { get; set; }
        #endregion

        #region Campos que no existen en la tabla
        public string pe_nombreCompleto { get; set; }
        public string Referencia { get; set; }
        public List<cxc_ConciliacionNotaCreditoDet_Info> ListaDet { get; set; }
        #endregion

    }
}
