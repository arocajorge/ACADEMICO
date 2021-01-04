using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Reportes.CuentasPorCobrar
{
    public class CXC_015_Info
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public int IdBodega { get; set; }
        public decimal IdCbteVta { get; set; }
        public string vt_tipoDoc { get; set; }
        public decimal Total { get; set; }
        public double dc_ValorPago { get; set; }
        public Nullable<decimal> Saldo { get; set; }
        public string vt_Observacion { get; set; }
        public System.DateTime vt_fecha { get; set; }
        public Nullable<decimal> IdAlumno { get; set; }
        public string NomNivel { get; set; }
        public string NomJornada { get; set; }
        public string NomCurso { get; set; }
        public string NomParalelo { get; set; }
        public string Alumno { get; set; }
        public string Representante { get; set; }
        public string Codigo { get; set; }
        public Nullable<decimal> ValorProntoPago { get; set; }
        public Nullable<System.DateTime> FechaProntoPago { get; set; }
        public int IdAnio { get; set; }
        public int IdSede { get; set; }
        public int IdNivel { get; set; }
        public int IdJornada { get; set; }
        public int IdCurso { get; set; }
        public int IdParalelo { get; set; }
        public Nullable<int> OrdenJornada { get; set; }
        public Nullable<int> OrdenNivel { get; set; }
        public Nullable<int> OrdenParalelo { get; set; }
        public Nullable<int> OrdenCurso { get; set; }

        #region Campos que no existen en la vista
        public string ValorProntoPagoHasta { get; set; }
        public string ValorHasta { get; set; }
        public string ValorDesde { get; set; }
        public bool MostrarValoresDesdeHasta { get; set; }
        #endregion
    }
}
