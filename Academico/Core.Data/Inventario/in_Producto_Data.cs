using Core.Data.Base;
using Core.Info.Helps;
using Core.Info.Inventario;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Inventario
{
    public class in_Producto_Data
    {
        #region metodo baja demanda

        public List<in_Producto_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args, int IdEmpresa, cl_enumeradores.eTipoBusquedaProducto Busqueda, cl_enumeradores.eModulo Modulo, int IdSucursal, int IdBodega)
        {
            var skip = args.BeginIndex;
            var take = args.EndIndex - args.BeginIndex + 1;
            List<in_Producto_Info> Lista = new List<in_Producto_Info>();
            switch (Busqueda)
            {
                case cl_enumeradores.eTipoBusquedaProducto.PORMODULO:
                    Lista = get_list(Modulo, IdEmpresa, skip, take, args.Filter);
                    break;
            }
            return Lista;
        }

        public in_Producto_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args, int IdEmpresa)
        {
            decimal id;
            if (args.Value == null || !decimal.TryParse(args.Value.ToString(), out id))
                return null;
            return get_info_demanda(IdEmpresa, Convert.ToDecimal(args.Value));
        }

        public in_Producto_Info get_info_demanda(int IdEmpresa, decimal IdProducto)
        {
            in_Producto_Info info = new in_Producto_Info();

            using (EntitiesInventario Contex = new EntitiesInventario())
            {
                info = (from q in Contex.in_Producto
                        join p in Contex.in_presentacion
                        on new { q.IdEmpresa, q.IdPresentacion } equals new { p.IdEmpresa, p.IdPresentacion }
                        where q.IdEmpresa == IdEmpresa && q.IdProducto == IdProducto
                        select new in_Producto_Info
                        {
                            IdProducto = q.IdProducto,
                            pr_descripcion = q.pr_descripcion,
                            pr_descripcion_2 = q.pr_descripcion_2,
                            pr_codigo = q.pr_codigo,
                            lote_num_lote = q.lote_num_lote,
                            lote_fecha_vcto = q.lote_fecha_vcto,
                            nom_presentacion = p.nom_presentacion
                        }).FirstOrDefault();

            }
            if (info != null)
                info.pr_descripcion_combo = info.pr_descripcion;
            else
                info = new in_Producto_Info();

            return info;
        }

        public List<in_Producto_Info> get_list(int IdEmpresa, int skip, int take, string filter)
        {
            try
            {
                List<in_Producto_Info> Lista = new List<in_Producto_Info>();

                EntitiesInventario Context = new EntitiesInventario();

                var lst = (from
                          p in Context.in_Producto
                           join c in Context.in_categorias
                           on new { p.IdEmpresa, p.IdCategoria } equals new { c.IdEmpresa, c.IdCategoria }
                           join pr in Context.in_presentacion
                           on new { p.IdEmpresa, p.IdPresentacion } equals new { pr.IdEmpresa, pr.IdPresentacion }
                           where
                            p.IdEmpresa == IdEmpresa
                            && c.IdEmpresa == IdEmpresa
                            && pr.IdEmpresa == IdEmpresa
                            && p.Estado == "A"
                            && (p.IdProducto.ToString() + " " + p.pr_descripcion).Contains(filter)
                           select new
                           {
                               p.IdEmpresa,
                               p.IdProducto,
                               p.pr_descripcion,
                               p.pr_descripcion_2,
                               p.pr_codigo,
                               p.lote_num_lote,
                               p.lote_fecha_vcto,
                               c.ca_Categoria,
                               pr.nom_presentacion
                           })
                             .OrderBy(p => p.IdProducto)
                             .Skip(skip)
                             .Take(take)
                             .ToList();


                foreach (var q in lst)
                {
                    Lista.Add(new in_Producto_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdProducto = q.IdProducto,
                        pr_descripcion = q.pr_descripcion,
                        pr_descripcion_2 = q.pr_descripcion_2,
                        pr_codigo = q.pr_codigo,
                        lote_num_lote = q.lote_num_lote,
                        lote_fecha_vcto = q.lote_fecha_vcto,
                        nom_categoria = q.ca_Categoria,
                        nom_presentacion = q.nom_presentacion
                    });
                }

                Context.Dispose();
                Lista = get_list_nombre_combo(Lista);
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<in_Producto_Info> get_list(cl_enumeradores.eModulo Modulo, int IdEmpresa, int skip, int take, string filter)
        {
            try
            {
                List<in_Producto_Info> Lista = new List<in_Producto_Info>();

                using (EntitiesInventario Context = new EntitiesInventario())
                {
                    switch (Modulo)
                    {
                        case cl_enumeradores.eModulo.ACA:
                            Lista = (from p in Context.in_Producto
                                     join c in Context.in_categorias
                                     on new { p.IdEmpresa, p.IdCategoria } equals new { c.IdEmpresa, c.IdCategoria }
                                     join pr in Context.in_presentacion
                                     on new { p.IdEmpresa, p.IdPresentacion } equals new { pr.IdEmpresa, pr.IdPresentacion }
                                     where
                                      p.IdEmpresa == IdEmpresa
                                      && c.IdEmpresa == IdEmpresa
                                      && pr.IdEmpresa == IdEmpresa
                                      && p.Estado == "A"
                                      && (p.IdProducto.ToString() + " " + p.pr_descripcion + " " + p.lote_num_lote).Contains(filter)
                                     select new in_Producto_Info
                                     {
                                         IdEmpresa = p.IdEmpresa,
                                         IdProducto = p.IdProducto,
                                         pr_descripcion = p.pr_descripcion,
                                         lote_num_lote = p.lote_num_lote,
                                         lote_fecha_vcto = p.lote_fecha_vcto,
                                         nom_categoria = c.ca_Categoria,
                                         nom_presentacion = pr.nom_presentacion,
                                         precio_1 = p.precio_1??0
                                     })
                                    .OrderBy(p => p.IdProducto)
                                    .Skip(skip)
                                    .Take(take)
                                    .ToList();
                            break;
                    }
                }
                Lista = get_list_nombre_combo(Lista);
                return Lista;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<in_Producto_Info> get_list_nombre_combo(List<in_Producto_Info> Lista)
        {
            int OrdenVcto = 1;
            Lista.ForEach(V => {
                V.pr_descripcion = V.pr_descripcion;
                V.pr_descripcion_combo = V.pr_descripcion;
                V.OrdenVcto = OrdenVcto++;
            });
            return Lista;
        }
        #endregion

        public in_Producto_Info get_info(int IdEmpresa, decimal IdProducto)
        {
            try
            {
                in_Producto_Info info = new in_Producto_Info();

                using (EntitiesInventario Context = new EntitiesInventario())
                {
                    in_Producto Entity = Context.in_Producto.Include("in_presentacion").Include("in_categorias").Include("in_ProductoTipo").FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdProducto == IdProducto);
                    if (Entity == null) return null;
                    info = new in_Producto_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdProducto = Entity.IdProducto,
                        pr_codigo = Entity.pr_codigo,
                        pr_codigo2 = Entity.pr_codigo2,
                        pr_descripcion = Entity.pr_descripcion,
                        pr_descripcion_2 = Entity.pr_descripcion_2,
                        IdProductoTipo = Entity.IdProductoTipo,
                        IdMarca = Entity.IdMarca,
                        IdPresentacion = Entity.IdPresentacion,
                        IdCategoria = Entity.IdCategoria,
                        IdLinea = Entity.IdLinea,
                        IdGrupo = Entity.IdGrupo,
                        IdSubGrupo = Entity.IdSubGrupo,
                        IdUnidadMedida = Entity.IdUnidadMedida,
                        IdUnidadMedida_Consumo = Entity.IdUnidadMedida_Consumo,
                        pr_codigo_barra = Entity.pr_codigo_barra,
                        pr_observacion = Entity.pr_observacion,
                        Estado = Entity.Estado,
                        IdCod_Impuesto_Iva = Entity.IdCod_Impuesto_Iva,
                        Aparece_modu_Ventas = Entity.Aparece_modu_Ventas,
                        Aparece_modu_Compras = Entity.Aparece_modu_Compras,
                        Aparece_modu_Inventario = Entity.Aparece_modu_Inventario,
                        Aparece_modu_Activo_F = Entity.Aparece_modu_Activo_F,
                        IdProducto_padre = Entity.IdProducto_padre,
                        lote_fecha_fab = Entity.lote_fecha_fab,
                        lote_fecha_vcto = Entity.lote_fecha_vcto,
                        lote_num_lote = Entity.lote_num_lote,
                        precio_1 = Entity.precio_1 == null ? 0 : Convert.ToDouble(Entity.precio_1),
                        se_distribuye = Entity.se_distribuye == null ? false : Convert.ToBoolean(Entity.se_distribuye),
                        pr_imagen = Entity.pr_imagen,
                        IdCtaCtble_Inve = Entity.in_categorias.IdCtaCtble_Inve,
                        pr_descripcion_combo = Entity.pr_descripcion,
                        ca_descripcion = Entity.in_categorias.ca_Categoria,
                        tp_ManejaInven = Entity.in_ProductoTipo.tp_ManejaInven
                    };
                }

                return info;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
