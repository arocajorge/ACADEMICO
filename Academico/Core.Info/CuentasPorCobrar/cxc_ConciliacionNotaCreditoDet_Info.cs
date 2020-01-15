using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.CuentasPorCobrar
{
    public class cxc_ConciliacionNotaCreditoDet_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdConciliacion { get; set; }
        public int Secuencia { get; set; }
        public int IdSucursal { get; set; }
        public int IdBodega { get; set; }
        public decimal IdCbteVtaNota { get; set; }
        public string vt_TipoDoc { get; set; }
        public double Valor { get; set; }
        public string ReferenciaDet { get; set; }
    }
}
