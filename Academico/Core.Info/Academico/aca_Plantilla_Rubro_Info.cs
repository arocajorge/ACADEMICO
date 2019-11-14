using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Academico
{
    public class aca_Plantilla_Rubro_Info
    {
        public int IdEmpresa { get; set; }
        public int IdAnio { get; set; }
        public int IdPlantilla { get; set; }
        public int IdRubro { get; set; }
        public decimal IdProducto { get; set; }
        public decimal Subtotal { get; set; }
        public string IdCod_Impuesto_Iva { get; set; }
        public decimal Porcentaje { get; set; }
        public decimal ValorIVA { get; set; }
        public decimal Total { get; set; }

        #region Campos que no existen en la base
        public bool seleccionado { get; set; }
        public string NomRubro { get; set; }
        #endregion
    }
}
