﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.CuentasPorCobrar
{
    public class cxc_LiquidacionTarjeta_x_ba_TipoFlujo_Info
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public decimal IdLiquidacion { get; set; }
        public int Secuencia { get; set; }
        public decimal IdTipoFlujo { get; set; }
        public double Porcentaje { get; set; }
        public double Valor { get; set; }
    }
}