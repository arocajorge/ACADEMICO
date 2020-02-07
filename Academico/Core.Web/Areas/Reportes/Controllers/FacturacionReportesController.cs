using Core.Bus.Facturacion;
using Core.Bus.General;
using Core.Bus.Inventario;
using Core.Info.General;
using Core.Info.Helps;
using Core.Info.Inventario;
using Core.Web.Helps;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Web.Areas.Reportes.Controllers
{
    public class FacturacionReportesController : Controller
    {
        tb_persona_Bus bus_persona = new tb_persona_Bus();
        in_Producto_Bus bus_producto = new in_Producto_Bus();
        fa_factura_Bus bus_factura = new fa_factura_Bus();
        fa_catalogo_Bus bus_catalogo = new fa_catalogo_Bus();
        tb_sis_reporte_x_tb_empresa_Bus bus_rep_x_emp = new tb_sis_reporte_x_tb_empresa_Bus();
        tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
        in_Marca_Bus bus_marca = new in_Marca_Bus();
        string RootReporte = System.IO.Path.GetTempPath() + "Rpt_Facturacion.repx";

        #region Metodos ComboBox bajo demanda
        public ActionResult CmbCliente_Facturacion()
        {
            cl_filtros_facturacion_Info model = new cl_filtros_facturacion_Info();
            return PartialView("_CmbCliente_Facturacion", model);
        }
        public List<tb_persona_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_persona.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.CLIENTE.ToString());
        }
        public tb_persona_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_persona.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.CLIENTE.ToString());
        }

        public ActionResult CmbProductoHijo_Facturacion()
        {
            SessionFixed.IdProducto_padre_dist = (!string.IsNullOrEmpty(Request.Params["IdProductoPadre"])) ? Request.Params["IdProductoPadre"].ToString() : "-1";
            cl_filtros_facturacion_Info model = new cl_filtros_facturacion_Info();
            return PartialView("_CmbProductoHijo_Facturacion", model);
        }
        public List<in_Producto_Info> get_list_ProductoHijo_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_producto.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoBusquedaProducto.PORSUCURSAL, cl_enumeradores.eModulo.INV, 0, Convert.ToInt32(SessionFixed.IdSucursal));
        }
        public in_Producto_Info get_info_producto_bajo_demanda(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_producto.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }
        #endregion
    }
}