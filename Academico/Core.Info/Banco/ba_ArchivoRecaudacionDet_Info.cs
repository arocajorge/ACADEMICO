using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Banco
{
    public class ba_ArchivoRecaudacionDet_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdArchivo { get; set; }
        public int Secuencia { get; set; }
        public Nullable<decimal> IdMatricula { get; set; }
        public decimal IdAlumno { get; set; }
        public double Valor { get; set; }
        public double ValorProntoPago { get; set; }
        public Nullable<System.DateTime> FechaProceso { get; set; }

        #region Campos que no existen en la tabla
        public string CodigoAlumno { get; set; }
        public string pe_nombreCompleto { get; set; }
        public System.DateTime Fecha { get; set; }
        public decimal Saldo { get; set; }
        public decimal SaldoProntoPago { get; set; }

        public string Nom_Archivo { get; set; }
        public int IdBanco { get; set; }
        public string ba_Num_Cuenta { get; set; }
        public string CodigoLegal { get; set; }
        public string IdTipoDocumento { get; set; }
        public string pe_cedulaRuc { get; set; }
        public int SecuencialDescarga { get; set; }
        public string Observacion { get; set; }
        #endregion
    }
}
