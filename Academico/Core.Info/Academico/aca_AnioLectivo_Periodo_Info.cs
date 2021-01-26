using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Academico
{
    public class aca_AnioLectivo_Periodo_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        public int IdPeriodo { get; set; }
        public int IdAnio { get; set; }
        public int IdMes { get; set; }
        public System.DateTime FechaDesde { get; set; }
        public System.DateTime FechaHasta { get; set; }
        public Nullable<System.DateTime> FechaProntoPago { get; set; }
        public bool Estado { get; set; }
        public string IdUsuarioCreacion { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string IdUsuarioModificacion { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
        public string IdUsuarioAnulacion { get; set; }
        public Nullable<System.DateTime> FechaAnulacion { get; set; }
        public string MotivoAnulacion { get; set; }
        public Nullable<int> IdSucursal { get; set; }
        public Nullable<int> IdPuntoVta { get; set; }
        public Nullable<bool> Procesado { get; set; }
        public Nullable<System.DateTime> FechaProceso { get; set; }
        public Nullable<decimal> TotalAlumnos { get; set; }
        public Nullable<decimal> TotalValorFacturado { get; set; }

        #region Campos que no existen en la tabla
        public List<aca_AnioLectivo_Periodo_Info> lst_detalle { get; set; }
        public string Descripcion { get; set; }
        public int NumPeriodos { get; set; }
        public string NomPeriodo { get; set; }
        public List<aca_Matricula_Rubro_Info> lst_det_fact_masiva { get; set; }
        public int Orden { get; set; }
        public int IdSede { get; set; }
        public int IdJornada { get; set; }
        public int IdNivel { get; set; }
        public int IdCurso { get; set; }
        public int IdParalelo { get; set; }
        #endregion
    }
}
