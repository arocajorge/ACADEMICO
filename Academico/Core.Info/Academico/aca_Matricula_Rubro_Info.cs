using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Academico
{
    public class aca_Matricula_Rubro_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdMatricula { get; set; }
        public int IdPeriodo { get; set; }
        public int IdRubro { get; set; }
        public bool EnMatricula { get; set; }
        public decimal IdMecanismo { get; set; }
        public decimal IdProducto { get; set; }
        public decimal Subtotal { get; set; }
        public string IdCod_Impuesto_Iva { get; set; }
        public decimal Porcentaje { get; set; }
        public decimal ValorIVA { get; set; }
        public decimal Total { get; set; }
        public Nullable<int> IdSucursal { get; set; }
        public Nullable<int> IdBodega { get; set; }
        public Nullable<decimal> IdCbteVta { get; set; }
        public Nullable<System.DateTime> FechaFacturacion { get; set; }
        public Nullable<int> IdSede { get; set; }
        public Nullable<int> IdNivel { get; set; }
        public Nullable<int> IdJornada { get; set; }
        public Nullable<int> IdCurso { get; set; }
        public Nullable<int> IdParalelo { get; set; }

        #region Campos que no existen en la tabla
        public bool seleccionado { get; set; }
        public string NomRubro { get; set; }
        public DateTime FechaDesde { get; set; }
        public string Periodo { get; set; }
        public string IdString { get; set; }
        public int IdAnio { get; set; }
        public int IdPlantilla { get; set; }
        public string pr_descripcion { get; set; }
        public bool AplicaProntoPago { get; set; }
        public DateTime FechaProntoPago { get; set; }
        public decimal ValorProntoPago { get; set; }
        public decimal IdAlumno { get; set; }
        public decimal IdCliente { get; set; }
        public string vt_Observacion { get; set; }
        public string pe_nombreCompleto { get; set; }
        public string Codigo { get; set; }
        public string IdTerminoPago { get; set; }
        public bool Procesado { get; set; }
        public string vt_autorizacion { get; set; }
        public string Correo { get; set; }
        #endregion
    }
}
