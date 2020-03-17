using System;

namespace Core.Info.Reportes.Facturacion
{
    public class FAC_005_Info
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public int IdBodega { get; set; }
        public decimal IdNota { get; set; }
        public System.DateTime no_fecha { get; set; }
        public string CreDeb { get; set; }
        public string NaturalezaNota { get; set; }
        public string Estado { get; set; }
        public string NumeroNota { get; set; }
        public decimal SubtotalConDscto { get; set; }
        public decimal ValorIVA { get; set; }
        public decimal Total { get; set; }
        public double Valor_Aplicado { get; set; }
        public Nullable<double> Saldo { get; set; }
        public string NombreCliente { get; set; }
        public string NombreAlumno { get; set; }
        public int IdTipoNota { get; set; }
        public string No_Descripcion { get; set; }
        public double Cruce { get; set; }
        public double Aplicacion { get; set; }
        public string Tipo { get; set; }
    }
}
