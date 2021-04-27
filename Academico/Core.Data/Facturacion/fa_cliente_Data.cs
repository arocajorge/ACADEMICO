using Core.Data.Base;
using Core.Info.Facturacion;
using Core.Info.General;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Facturacion
{
    public class fa_cliente_Data
    {
        public List<fa_cliente_Info> get_list(int IdEmpresa, bool mostrar_anulados)
        {
            try
            {
                List<fa_cliente_Info> Lista = new List<fa_cliente_Info>();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = "SELECT c.IdEmpresa, c.IdCliente, c.Codigo, c.IdPersona, p.pe_nombreCompleto, c.Idtipo_cliente, p.pe_cedulaRuc, c.Estado, ct.Descripcion_tip_cliente "
                    + " FROM dbo.tb_persona AS p RIGHT OUTER JOIN "
                    + " dbo.fa_cliente AS c ON p.IdPersona = c.IdPersona LEFT OUTER JOIN "
                    + " dbo.fa_cliente_tipo AS ct ON c.IdEmpresa = ct.IdEmpresa AND c.Idtipo_cliente = ct.Idtipo_cliente "
                    + " WHERE c.IdEmpresa=" + IdEmpresa.ToString();
                    if (mostrar_anulados==false)
                    {
                        query += " and c.Estado == 'A' ";
                    }
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandTimeout = 0;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new fa_cliente_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            Codigo = string.IsNullOrEmpty(reader["Codigo"].ToString()) ? null : reader["Codigo"].ToString(),
                            Estado = string.IsNullOrEmpty(reader["Estado"].ToString()) ? null : reader["Estado"].ToString(),
                            IdCliente = Convert.ToDecimal(reader["IdCliente"]),
                            Descripcion_tip_cliente = string.IsNullOrEmpty(reader["Descripcion_tip_cliente"].ToString()) ? null : reader["Descripcion_tip_cliente"].ToString(),
                            info_persona = new tb_persona_Info
                            {
                                IdPersona = Convert.ToDecimal(reader["IdPersona"]),
                                pe_nombreCompleto = string.IsNullOrEmpty(reader["pe_nombreCompleto"].ToString()) ? null : reader["pe_nombreCompleto"].ToString(),
                                pe_cedulaRuc = string.IsNullOrEmpty(reader["pe_cedulaRuc"].ToString()) ? null : reader["pe_cedulaRuc"].ToString(),
                            },
                            EstadoBool = string.IsNullOrEmpty(reader["Estado"].ToString()) ? false : (reader["Estado"].ToString() == "A" ? true : false),                           
                        });
                    }
                    reader.Close();
                }
                /*
                using (EntitiesFacturacion Context = new EntitiesFacturacion())
                {
                    if (mostrar_anulados)
                        Lista = (from q in Context.vwfa_cliente_consulta
                                 join t in Context.fa_cliente_tipo
                                 on new { q.IdEmpresa, q.Idtipo_cliente} equals new { t.IdEmpresa, t.Idtipo_cliente}
                                 where q.IdEmpresa == IdEmpresa
                                 select new fa_cliente_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     Codigo = q.Codigo,
                                     Estado = q.Estado,
                                     IdCliente = q.IdCliente,
                                     Descripcion_tip_cliente = t.Descripcion_tip_cliente,
                                     info_persona = new Info.General.tb_persona_Info
                                     {
                                         IdPersona = q.IdPersona,
                                         pe_nombreCompleto = q.pe_nombreCompleto,
                                         pe_cedulaRuc = q.pe_cedulaRuc
                                     },

                                     EstadoBool = q.Estado == "A" ? true : false
                                 }).ToList();
                    else

                        Lista = (from q in Context.vwfa_cliente_consulta
                                 join t in Context.fa_cliente_tipo
                                 on new { q.IdEmpresa, q.Idtipo_cliente } equals new { t.IdEmpresa, t.Idtipo_cliente }
                                 where q.IdEmpresa == IdEmpresa
                                 && q.Estado == "A"
                                 select new fa_cliente_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     Codigo = q.Codigo,
                                     Estado = q.Estado,
                                     IdCliente = q.IdCliente,
                                     Descripcion_tip_cliente = t.Descripcion_tip_cliente,
                                     info_persona = new Info.General.tb_persona_Info
                                     {
                                         IdPersona = q.IdPersona,
                                         pe_nombreCompleto = q.pe_nombreCompleto,
                                         pe_cedulaRuc = q.pe_cedulaRuc
                                     },

                                     EstadoBool = q.Estado == "A" ? true : false
                                 }).ToList();
                }
                */
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public fa_cliente_Info get_info(int IdEmpresa, decimal IdCliente)
        {
            try
            {
                fa_cliente_Info info = new fa_cliente_Info();
                EntitiesFacturacion Context_f = new EntitiesFacturacion();
                EntitiesGeneral Context_g = new EntitiesGeneral();

                fa_cliente Entity = Context_f.fa_cliente.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdCliente == IdCliente);
                if (Entity == null) return null;

                info = new fa_cliente_Info
                {
                    IdEmpresa = Entity.IdEmpresa,
                    cl_Cupo = Entity.cl_Cupo,
                    cl_plazo = Entity.cl_plazo,
                    Codigo = Entity.Codigo,
                    Estado = Entity.Estado,
                    es_empresa_relacionada = Entity.es_empresa_relacionada,
                    IdCliente = Entity.IdCliente,
                    FormaPago = Entity.FormaPago,
                    IdCtaCble_cxc_Credito = Entity.IdCtaCble_cxc_Credito,
                    IdPersona = Entity.IdPersona,
                    IdTipoCredito = Entity.IdTipoCredito,
                    Idtipo_cliente = Entity.Idtipo_cliente,
                    IdNivel = Entity.IdNivel,
                    EsClienteExportador = Entity.EsClienteExportador
                };

                fa_cliente_contactos Entity_contacto = Context_f.fa_cliente_contactos.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdCliente == info.IdCliente).FirstOrDefault();
                info.IdContacto = Entity_contacto.IdContacto;                
                info.Correo = Entity_contacto.Correo;
                info.Direccion = Entity_contacto.Direccion;
                info.Telefono = Entity_contacto.Telefono;
                info.Celular = Entity_contacto.Celular;
                info.IdCiudad = Entity_contacto.IdCiudad;
                info.IdParroquia = Entity_contacto.IdParroquia;

                tb_ciudad Entity_ciudad = Context_g.tb_ciudad.Where(q => q.IdCiudad == info.IdCiudad).FirstOrDefault();
                info.Descripcion_Ciudad = Entity_ciudad.Descripcion_Ciudad;

                tb_persona Entity_p = Context_g.tb_persona.Where(q => q.IdPersona == info.IdPersona).FirstOrDefault();
                info.info_persona = new Info.General.tb_persona_Info
                {
                    IdPersona = Entity_p.IdPersona,
                    pe_apellido = Entity_p.pe_apellido,
                    pe_nombre = Entity_p.pe_nombre,
                    pe_cedulaRuc = Entity_p.pe_cedulaRuc,
                    pe_nombreCompleto = Entity_p.pe_nombreCompleto,
                    pe_razonSocial = Entity_p.pe_razonSocial,
                    pe_Naturaleza = Entity_p.pe_Naturaleza,
                    IdTipoDocumento = Entity_p.IdTipoDocumento
                };

                Context_f.Dispose();
                Context_g.Dispose();
                return info;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public fa_cliente_Info get_info_x_num_cedula(int IdEmpresa, string pe_cedulaRuc)
        {
            try
            {
                fa_cliente_Info info = new fa_cliente_Info
                {
                    info_persona = new Info.General.tb_persona_Info()
                };

                EntitiesGeneral Context_general = new EntitiesGeneral();
                tb_persona Entity_p = Context_general.tb_persona.Where(q => q.pe_cedulaRuc == pe_cedulaRuc).FirstOrDefault();
                if (Entity_p == null)
                {
                    Context_general.Dispose();
                    return info;
                }

                EntitiesFacturacion Context_facturacion = new EntitiesFacturacion();
                fa_cliente Entity_c = Context_facturacion.fa_cliente.Where(q => q.IdEmpresa == IdEmpresa && q.IdPersona == Entity_p.IdPersona).FirstOrDefault();
                if (Entity_c == null)
                {
                    info.IdPersona = Entity_p.IdPersona;
                    info.info_persona = new Info.General.tb_persona_Info
                    {
                        IdPersona = Entity_p.IdPersona,
                        pe_apellido = Entity_p.pe_apellido,
                        pe_nombre = Entity_p.pe_nombre,
                        pe_cedulaRuc = Entity_p.pe_cedulaRuc,
                        pe_nombreCompleto = Entity_p.pe_nombreCompleto,
                        pe_razonSocial = Entity_p.pe_razonSocial,
                        pe_celular = Entity_p.pe_celular,
                        pe_telfono_Contacto = Entity_p.pe_telfono_Contacto,
                        pe_correo = Entity_p.pe_correo,
                        pe_direccion = Entity_p.pe_direccion                        
                    };
                    Context_general.Dispose();
                    Context_facturacion.Dispose();
                    return info;
                }

                info = new fa_cliente_Info
                {
                    IdEmpresa = Entity_c.IdEmpresa,
                    IdCliente = Entity_c.IdCliente,
                    IdPersona = Entity_p.IdPersona,
                    info_persona = new Info.General.tb_persona_Info
                    {
                        IdPersona = Entity_p.IdPersona,
                        pe_apellido = Entity_p.pe_apellido,
                        pe_nombre = Entity_p.pe_nombre,
                        pe_cedulaRuc = Entity_p.pe_cedulaRuc,
                        pe_nombreCompleto = Entity_p.pe_nombreCompleto,
                        pe_razonSocial = Entity_p.pe_razonSocial,
                        pe_celular = Entity_p.pe_celular,
                        pe_telfono_Contacto = Entity_p.pe_telfono_Contacto,
                        pe_correo = Entity_p.pe_correo,
                        pe_direccion = Entity_p.pe_direccion
                    }
            };

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }
        private decimal get_id(int IdEmpresa)
        {
            try
            {
                decimal ID = 1;

                using (EntitiesFacturacion Context = new EntitiesFacturacion())
                {
                    var lst = from q in Context.fa_cliente
                              where q.IdEmpresa == IdEmpresa
                              select q;

                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdCliente)+1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(fa_cliente_Info info)
        {
            try
            {
                using (EntitiesFacturacion Context = new EntitiesFacturacion())
                {
                    fa_cliente Entity = new fa_cliente
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdCliente = info.IdCliente = get_id(info.IdEmpresa),
                        cl_Cupo = info.cl_Cupo,
                        cl_plazo = info.cl_plazo,
                        Codigo = info.Codigo,
                        Estado = info.Estado = "A",
                        es_empresa_relacionada = info.es_empresa_relacionada,
                        FormaPago = info.FormaPago,
                        IdCtaCble_cxc_Credito = info.IdCtaCble_cxc_Credito,
                        IdPersona = info.IdPersona,
                        IdTipoCredito = info.IdTipoCredito,
                        Idtipo_cliente = info.Idtipo_cliente,
                        IdNivel = info.IdNivel,
                        EsClienteExportador = info.EsClienteExportador,
                        IdUsuario = info.IdUsuario,
                        Fecha_Transac = DateTime.Now
                    };
                    Context.fa_cliente.Add(Entity);
                    //foreach (var item in info.lst_fa_cliente_contactos)
                    //{
                        fa_cliente_contactos Entity_det = new fa_cliente_contactos
                        {
                            IdEmpresa = Entity.IdEmpresa,
                            IdCliente = Entity.IdCliente,
                            IdContacto = 1,
                            IdCiudad = info.IdCiudad,
                            IdParroquia = info.IdParroquia,
                            Celular = info.Celular,
                            Correo = info.Correo,
                            Direccion = info.Direccion,
                            Nombres = info.info_persona.pe_nombreCompleto,
                            Telefono = info.Telefono
                        };
                        Context.fa_cliente_contactos.Add(Entity_det);
                    //}

                    foreach (var item in info.Lst_fa_cliente_x_fa_Vendedor_x_sucursal)
                    {
                        fa_cliente_x_fa_Vendedor_x_sucursal det = new fa_cliente_x_fa_Vendedor_x_sucursal
                        {
                            IdEmpresa = info.IdEmpresa,
                            IdSucursal = item.IdSucursal,
                            IdCliente = info.IdCliente,
                            IdVendedor = item.IdVendedor,
                            observacion = item.observacion
                        };
                        Context.fa_cliente_x_fa_Vendedor_x_sucursal.Add(det);
                    }
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool modificarDB(fa_cliente_Info info)
        {
            try
            {
                using (EntitiesFacturacion Context = new EntitiesFacturacion())
                {
                    fa_cliente Entity = Context.fa_cliente.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdCliente == info.IdCliente);
                    if (Entity == null) return false;

                    Entity.cl_Cupo = info.cl_Cupo;
                    Entity.cl_plazo = info.cl_plazo;
                    Entity.Codigo = info.Codigo;
                    Entity.es_empresa_relacionada = info.es_empresa_relacionada;
                    Entity.FormaPago = info.FormaPago;
                    Entity.IdCtaCble_cxc_Credito = info.IdCtaCble_cxc_Credito;
                    Entity.IdTipoCredito = info.IdTipoCredito;
                    Entity.Idtipo_cliente = info.Idtipo_cliente;
                    Entity.IdNivel = info.IdNivel;
                    Entity.EsClienteExportador = info.EsClienteExportador;

                    Entity.IdUsuarioUltMod = info.IdUsuarioUltMod;
                    Entity.Fecha_UltMod = DateTime.Now;

                    var lst = Context.fa_cliente_x_fa_Vendedor_x_sucursal.Where(q =>  q.IdEmpresa == info.IdEmpresa && q.IdCliente == info.IdCliente).ToList();
                    foreach (var item in lst)
                    {
                        Context.fa_cliente_x_fa_Vendedor_x_sucursal.Remove(item);
                    }
                    foreach (var item in info.Lst_fa_cliente_x_fa_Vendedor_x_sucursal)
                    {
                        fa_cliente_x_fa_Vendedor_x_sucursal det = new fa_cliente_x_fa_Vendedor_x_sucursal
                        {
                            IdEmpresa = info.IdEmpresa,
                            IdSucursal = item.IdSucursal,
                            IdCliente = info.IdCliente,
                            IdVendedor = item.IdVendedor,
                            observacion = item.observacion
                        };
                        Context.fa_cliente_x_fa_Vendedor_x_sucursal.Add(det);
                    }

                    fa_cliente_contactos Entity_Contacto = Context.fa_cliente_contactos.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdCliente == info.IdCliente && q.IdContacto==1);
                    if (Entity_Contacto != null)
                    {
                        Entity_Contacto.IdParroquia = info.IdParroquia;
                        Entity_Contacto.IdCiudad = info.IdCiudad;
                    }

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool anularDB(fa_cliente_Info info)
        {
            try
            {
                using (EntitiesFacturacion Context = new EntitiesFacturacion())
                {
                    fa_cliente Entity = Context.fa_cliente.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdCliente == info.IdCliente);
                    if (Entity == null) return false;

                    Entity.Estado = info.Estado = "I";

                    Entity.IdUsuarioUltAnu = info.IdUsuarioUltAnu;
                    Entity.Fecha_UltAnu = DateTime.Now;
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //public bool ValidarCupoCreditoCliente(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdCbteVta, string vt_tipoDoc, decimal IdCliente, double Total, ref string mensaje)
        //{
        //    Entities_cuentas_por_cobrar db_cxc = new Entities_cuentas_por_cobrar();
        //    EntitiesFacturacion db_fac = new EntitiesFacturacion();
        //    try
        //    {

        //        #region variables
        //        double ValorDocumento = 0;
        //        double SaldoPorCobrar = 0;
        //        #endregion

        //        var cliente = db_fac.fa_cliente.Where(q => q.IdEmpresa == IdEmpresa && q.IdCliente == IdCliente).FirstOrDefault();
        //        if (cliente == null)
        //        {
        //            db_cxc.Dispose();
        //            db_fac.Dispose();
        //            mensaje = "Seleccione el cliente";
        //            return false;
        //        }

        //        if (cliente.cl_Cupo == 0)
        //        {
        //            db_cxc.Dispose();
        //            db_fac.Dispose();
        //            return true;
        //        }

        //        if (IdCbteVta != 0)
        //        {
        //            if (vt_tipoDoc == "FACT")
        //            {
        //                var fac = db_fac.vwfa_factura.Where(q => q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal && q.IdBodega == IdBodega && q.IdCbteVta == IdCbteVta).FirstOrDefault();
        //                if (fac != null)
        //                    ValorDocumento = fac.vt_total == null ? 0 : Convert.ToDouble(fac.vt_total);
        //            }
        //            else
        //            {
        //                var fac = db_fac.vwfa_notaCreDeb.Where(q => q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal && q.IdBodega == IdBodega && q.IdNota == IdCbteVta).FirstOrDefault();
        //                if (fac != null)
        //                    ValorDocumento = fac.sc_total == null ? 0 : Convert.ToDouble(fac.sc_total);
        //            }
        //        }

        //        var cartera = db_cxc.vwcxc_cartera_x_cobrar.Where(q => q.IdEmpresa == IdEmpresa && q.IdCliente == IdCliente && q.Estado == "A");
        //        if (cartera.Count() > 0)
        //            SaldoPorCobrar = Convert.ToDouble(cartera.Sum(q => q.Saldo));

        //        if (Math.Round(Total + SaldoPorCobrar - ValorDocumento,2,MidpointRounding.AwayFromZero) > cliente.cl_Cupo)
        //        {
        //            mensaje = "El cliente ha sobrepasado su cupo de crédito de: $"+Math.Round(cliente.cl_Cupo,2,MidpointRounding.AwayFromZero)+", Actualmente tiene un crédito en documentos de : $"+Math.Round(SaldoPorCobrar,2,MidpointRounding.AwayFromZero)+ " El total del documento actual es de $"+Math.Round(Total,2,MidpointRounding.AwayFromZero);
        //            db_cxc.Dispose();
        //            db_fac.Dispose();
        //            return false;
        //        }

        //        db_cxc.Dispose();
        //        db_fac.Dispose();

        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        db_cxc.Dispose();
        //        db_fac.Dispose();
        //        throw;
        //    }
        //}
    }
}
