﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Reportes.CuentasPorCobrar
{
    public class CXC_007_Info
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public int IdBodega { get; set; }
        public decimal IdCbteVta { get; set; }
        public System.DateTime vt_fecha { get; set; }
        public string vt_Observacion { get; set; }
        public string vt_NumFactura { get; set; }
        public decimal IdAlumno { get; set; }
        public string CodigoAlumno { get; set; }
        public string pe_nombreCompleto { get; set; }
        public Nullable<int> IdAnio { get; set; }
        public Nullable<int> Periodo { get; set; }
        public decimal Total { get; set; }
        public double TotalPagado { get; set; }
        public Nullable<double> Saldo { get; set; }
        public System.DateTime vt_fech_venc { get; set; }
        public Nullable<int> Plazo { get; set; }
        public Nullable<int> idMes { get; set; }
        public string smes { get; set; }
        public string NomNivel { get; set; }
        public Nullable<int> OrdenNivel { get; set; }
        public string NomJornada { get; set; }
        public Nullable<int> OrdenJornada { get; set; }
        public string NomCurso { get; set; }
        public Nullable<int> OrdenCurso { get; set; }
        public string NomParalelo { get; set; }
        public Nullable<int> OrdenParalelo { get; set; }
        public Nullable<int> IdSede { get; set; }
        public Nullable<int> IdNivel { get; set; }
        public Nullable<int> IdJornada { get; set; }
        public Nullable<int> IdCurso { get; set; }
        public Nullable<int> IdParalelo { get; set; }
        public Nullable<double> VENCIDO_0_30 { get; set; }
        public Nullable<double> VENCIDO_31_60 { get; set; }
        public Nullable<double> VENCIDO_61_90 { get; set; }
        public Nullable<double> VENCIDO_91_180 { get; set; }
        public Nullable<double> VENCIDO_181 { get; set; }
    }
}
