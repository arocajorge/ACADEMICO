using Core.Bus.Facturacion;
using Core.Bus.Inventario;
using Core.Info.Facturacion;
using Core.Info.Inventario;
using Core.Web.Helps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Web.Areas.Inventario.Controllers
{
    public class ProductoController : Controller
    {
        
    }
    public class Producto_imagen
    {
        public static byte[] pr_imagen { get; set; }
        public static DevExpress.Web.UploadControlValidationSettings UploadValidationSettings = new DevExpress.Web.UploadControlValidationSettings()
        {
            AllowedFileExtensions = new string[] { ".jpg", ".jpeg" },
            MaxFileSize = 4000000
        };
        public static void FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
        {
            if (e.UploadedFile.IsValid)
            {
                pr_imagen = e.UploadedFile.FileBytes;
            }
        }
    }
    public class UploadControlSettings
    {
        public static DevExpress.Web.UploadControlValidationSettings UploadValidationSettings = new DevExpress.Web.UploadControlValidationSettings()
        {
            AllowedFileExtensions = new string[] { ".xlsx" },
            MaxFileSize = 40000000
        };
        //public static void FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
        //{
        //    #region Variables

        //    in_categorias_List Lista_Categoria = new in_categorias_List();
        //    in_linea_List Lista_Linea = new in_linea_List();
        //    in_grupo_List Lista_Grupo = new in_grupo_List();
        //    in_subgrupo_List Lista_Subgrupo = new in_subgrupo_List();
        //    in_presentacion_List Lista_Presentacion = new in_presentacion_List();
        //    in_Marca_List Lista_Marca = new in_Marca_List();
        //    in_Prod_List Lista_Producto = new in_Prod_List();

        //    List<in_categorias_Info> ListaCategoria = new List<in_categorias_Info>();
        //    List<in_linea_Info> ListaLinea = new List<in_linea_Info>();
        //    List<in_grupo_Info> ListaGrupo = new List<in_grupo_Info>();
        //    List<in_subgrupo_Info> ListaSubgrupo = new List<in_subgrupo_Info>();
        //    List<in_presentacion_Info> ListaPresentacion = new List<in_presentacion_Info>();
        //    List<in_Marca_Info> ListaMarca = new List<in_Marca_Info>();
        //    List<in_Producto_Info> ListaProducto = new List<in_Producto_Info>();
        //    in_Producto_Bus bus_producto = new in_Producto_Bus();
        //    int cont = 0;
        //    decimal IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
        //    int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
        //    #endregion

        //    Stream stream = new MemoryStream(e.UploadedFile.FileBytes);
        //    if (stream.Length > 0)
        //    {
        //        IExcelDataReader reader = null;
        //        reader = ExcelReaderFactory.CreateOpenXmlReader(stream);

        //        #region Categoria                
        //        while (reader.Read())
        //        {
        //            if (!reader.IsDBNull(0) && cont > 0)
        //            {
        //                in_subgrupo_Info info = new in_subgrupo_Info
        //                {
        //                    IdEmpresa = IdEmpresa,
        //                    IdCategoria = Convert.ToString(reader.GetValue(0)).Trim(),
        //                    NomCategoria = Convert.ToString(reader.GetValue(1)).Trim(),
        //                    IdLinea = Convert.ToInt32(reader.GetValue(2)),
        //                    NomLinea = Convert.ToString(reader.GetValue(3)).Trim(),
        //                    IdGrupo = Convert.ToInt32(reader.GetValue(4)),
        //                    NomGrupo = Convert.ToString(reader.GetValue(5)).Trim(),
        //                    IdSubgrupo = Convert.ToInt32(reader.GetValue(6)),
        //                    nom_subgrupo = Convert.ToString(reader.GetValue(7)).Trim(),
        //                    IdUsuario = SessionFixed.IdUsuario
        //                };
        //                ListaSubgrupo.Add(info);
        //            }
        //            else
        //                cont++;
        //        }
        //        Lista_Subgrupo.set_list(ListaSubgrupo, IdTransaccionSession);

        //        #endregion

        //        cont = 0;
        //        //Para avanzar a la siguiente hoja de excel
        //        reader.NextResult();


        //        #region Presentacion                
        //        while (reader.Read())
        //        {
        //            if (!reader.IsDBNull(0) && cont > 0)
        //            {
        //                in_presentacion_Info info = new in_presentacion_Info
        //                {
        //                    IdEmpresa = IdEmpresa,
        //                    IdPresentacion = Convert.ToString(reader.GetValue(0)),
        //                    nom_presentacion = Convert.ToString(reader.GetValue(1))
        //                };
        //                ListaPresentacion.Add(info);
        //            }
        //            else
        //                cont++;
        //        }
        //        Lista_Presentacion.set_list(ListaPresentacion, IdTransaccionSession);
        //        #endregion

        //        cont = 0;
        //        reader.NextResult();

        //        #region Marca                
        //        while (reader.Read())
        //        {
        //            if (!reader.IsDBNull(0) && cont > 0)
        //            {
        //                in_Marca_Info info = new in_Marca_Info
        //                {
        //                    IdEmpresa = IdEmpresa,
        //                    IdMarca = Convert.ToInt32(reader.GetValue(0)),
        //                    Descripcion = Convert.ToString(reader.GetValue(1)),
        //                    IdUsuario = SessionFixed.IdUsuario
        //                };

        //                ListaMarca.Add(info);
        //            }
        //            else
        //                cont++;
        //        }
        //        Lista_Marca.set_list(ListaMarca, IdTransaccionSession);
        //        #endregion

        //        cont = 0;
        //        reader.NextResult();

        //        #region Producto   
        //        while (reader.Read())
        //        {
        //            if (!reader.IsDBNull(0) && cont > 0)
        //            {
        //                if (!bus_producto.ValidarCodigoExists(IdEmpresa, Convert.ToString(reader.GetValue(1)).Trim()))
        //                {
        //                    in_Producto_Info info = new in_Producto_Info
        //                    {
        //                        IdEmpresa = IdEmpresa,
        //                        IdProducto = Convert.ToInt32(reader.GetValue(0)),
        //                        pr_codigo = Convert.ToString(reader.GetValue(1)).Trim(),
        //                        pr_descripcion = string.IsNullOrEmpty(Convert.ToString(reader.GetValue(2))) ? null : Convert.ToString(reader.GetValue(2)).Trim(),
        //                        pr_descripcion_2 = string.IsNullOrEmpty(Convert.ToString(reader.GetValue(2))) ? null : Convert.ToString(reader.GetValue(2)).Trim(),
        //                        IdMarca = Convert.ToInt32(reader.GetValue(3)),
        //                        IdPresentacion = Convert.ToString(reader.GetValue(4)),
        //                        IdCategoria = Convert.ToString(reader.GetValue(5)),
        //                        IdLinea = Convert.ToInt32(reader.GetValue(6)),
        //                        IdGrupo = Convert.ToInt32(reader.GetValue(7)),
        //                        IdSubGrupo = Convert.ToInt32(reader.GetValue(8)),
        //                        IdCod_Impuesto_Iva = Convert.ToString(reader.GetValue(9)),
        //                        IdUnidadMedida = Convert.ToString(reader.GetValue(10)),
        //                        IdUnidadMedida_Consumo = Convert.ToString(reader.GetValue(11)),
        //                        precio_1 = Convert.ToDouble(reader.GetValue(12)),
        //                        IdProductoTipo = 1,
        //                    };
        //                    ListaProducto.Add(info);
        //                }
        //            }
        //            else
        //                cont++;
        //        }
        //        Lista_Producto.set_list(ListaProducto, IdTransaccionSession);
        //        #endregion

        //        cont = 0;
        //        reader.NextResult();

        //    }
        //}
    }

    public class in_Producto_List
    {
        string Variable = "in_producto_x_tb_bodega_Info";
        public List<in_Producto_Info> get_list()
        {
            if (HttpContext.Current.Session[Variable] == null)
            {
                List<in_Producto_Info> list = new List<in_Producto_Info>();

                HttpContext.Current.Session[Variable] = list;
            }
            return (List<in_Producto_Info>)HttpContext.Current.Session[Variable];
        }

        public void set_list(List<in_Producto_Info> list)
        {
            HttpContext.Current.Session[Variable] = list;
        }

        public void AddRow(in_Producto_Info info_det)
        {
            List<in_Producto_Info> list = get_list();
            if (list.Where(q => q.IdProducto == info_det.IdProducto).Count() == 0)
                list.Add(info_det);
        }

        public void DeleteRow(decimal IdProducto)
        {
            List<in_Producto_Info> list = get_list();
            list.Remove(list.Where(m => m.IdProducto == IdProducto).First());
        }
    }

    public class in_Producto_x_fa_NivelDescuesto_List
    {
        in_Producto_Bus bus_producto = new in_Producto_Bus();
        fa_NivelDescuento_Bus bus_nivel_desc = new fa_NivelDescuento_Bus();
        string Variable = "in_Producto_x_fa_NivelDescuento_Info";

        public List<in_Producto_x_fa_NivelDescuento_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<in_Producto_x_fa_NivelDescuento_Info> list = new List<in_Producto_x_fa_NivelDescuento_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<in_Producto_x_fa_NivelDescuento_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<in_Producto_x_fa_NivelDescuento_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }

        public void AddRow(in_Producto_x_fa_NivelDescuento_Info info_det, decimal IdTransaccionSession)
        {
            List<in_Producto_x_fa_NivelDescuento_Info> list = get_list(IdTransaccionSession);
            fa_NivelDescuento_Info info_nivel = bus_nivel_desc.GetInfo(Convert.ToInt32(SessionFixed.IdEmpresa), info_det.IdNivel);

            info_det.Secuencia = list.Count == 0 ? 1 : list.Max(q => q.Secuencia) + 1;
            info_det.Descripcion = info_nivel.Descripcion;

            list.Add(info_det);
        }

        public void UpdateRow(in_Producto_x_fa_NivelDescuento_Info info_det, decimal IdTransaccionSession)
        {
            in_Producto_x_fa_NivelDescuento_Info edited_info = get_list(IdTransaccionSession).Where(m => m.Secuencia == info_det.Secuencia).First();
            if (edited_info.IdNivel != info_det.IdNivel)
            {
                fa_NivelDescuento_Info info_nivel = bus_nivel_desc.GetInfo(Convert.ToInt32(SessionFixed.IdEmpresa), info_det.IdNivel);
                edited_info.Descripcion = info_nivel.Descripcion;
            }
            edited_info.IdNivel = info_det.IdNivel;
            edited_info.Porcentaje = info_det.Porcentaje;
        }

        public void DeleteRow(int secuencia, decimal IdTransaccionSession)
        {
            List<in_Producto_x_fa_NivelDescuento_Info> list = get_list(IdTransaccionSession);
            list.Remove(list.Where(m => m.Secuencia == secuencia).First());
        }
    }

    public class in_Prod_List
    {
        string Variable = "in_Producto_Info";
        public List<in_Producto_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<in_Producto_Info> list = new List<in_Producto_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<in_Producto_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<in_Producto_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }
}