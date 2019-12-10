using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Helps
{
    public class cl_filtros_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public int IdSede { get; set; }
        public int IdAnio { get; set; }
        public int IdNivel { get; set; }
        public int IdJornada { get; set; }
        public int IdCurso { get; set; }
        public int IdParalelo { get; set; }
        [Required(ErrorMessage = "El campo fecha inicio es obligatorio")]
        [DataType(DataType.Date, ErrorMessage = "El campo fecha inicio debe ser una fecha en formato dd/MM/yyyy")]
        public DateTime fecha_ini { get; set; }
        [Required(ErrorMessage = "El campo fecha fin es obligatorio")]
        [DataType(DataType.Date, ErrorMessage = "El campo fecha fin debe ser una fecha en formato dd/MM/yyyy")]
        public DateTime fecha_fin { get; set; }

        public cl_filtros_Info()
        {
            fecha_ini = DateTime.Now.Date.AddMonths(-1);
            fecha_fin = DateTime.Now.Date;
        }

        public class cl_filtros_facturacion_Info
        {
            public int IdEmpresa { get; set; }
            public DateTime fecha_fin { get; set; }
            public decimal? IdProducto { get; set; }
            public decimal? IdCliente { get; set; }
            public int IdClienteContacto { get; set; }
            public int IdVendedor { get; set; }
            public decimal? IdProductoPadre { get; set; }
            public decimal? IdEntidad { get; set; }
            public DateTime fecha_ini { get; set; }
            public int IdSucursal { get; set; }
            public bool Check1 { get; set; }
            public bool Check2 { get; set; }
            public DateTime fecha_corte { get; set; }
            public int IdContacto { get; set; }

            public decimal IdProforma { get; set; }
            public bool formato_hoja_membretada { get; set; }

            public int IdMarca { get; set; }
            public string IdCategoria { get; set; }
            public int IdLinea { get; set; }
            public int IdGrupo { get; set; }
            public int IdSubGrupo { get; set; }
            public bool mostrarSaldo0 { get; set; }
            public bool mostrarSoloVencido { get; set; }

            public decimal IdLiquidacion { get; set; }
            public bool mostrarAnulados { get; set; }
            public bool mostrar_observacion_completa { get; set; }
            public int Idtipo_cliente { get; set; }
            public string IdCatalogo_FormaPago { get; set; }

            public int IdAnio { get; set; }
            public string IdCobro_tipo { get; set; }
            public int[] IntArray { get; set; }
            public int IdTipoNota { get; set; }
            public string CreDeb { get; set; }


            public cl_filtros_facturacion_Info()
            {
                fecha_ini = DateTime.Now.Date.AddMonths(-1);
                fecha_fin = DateTime.Now.Date;
                fecha_corte = DateTime.Now.Date;
                IdCatalogo_FormaPago = "";
            }
        }
    }
}
