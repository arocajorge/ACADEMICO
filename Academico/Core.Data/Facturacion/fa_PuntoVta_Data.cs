﻿using Core.Data.Base;
using Core.Info.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Data.Facturacion
{
    public class fa_PuntoVta_Data
    {

        public List<fa_PuntoVta_Info> get_list(int IdEmpresa, int IdSucursal,string CodDocumentoTipo, bool mostrar_anulados )
        {
            try
            {
                int IdSucursalIni = IdSucursal;
                int IdSucursalFin = IdSucursal == 0 ? 9999 : IdSucursal;

                List<fa_PuntoVta_Info> Lista;
                using (EntitiesFacturacion Context = new EntitiesFacturacion())
                {
                    Lista = Context.vwfa_PuntoVta.Where(q=> q.IdEmpresa == IdEmpresa
                    && IdSucursalIni <= q.IdSucursal
                    && q.IdSucursal <= IdSucursalFin
                    && q.codDocumentoTipo == (string.IsNullOrEmpty(CodDocumentoTipo) ? q.codDocumentoTipo : CodDocumentoTipo)
                    && q.estado == (mostrar_anulados ? q.estado : true)).Select(q=> new fa_PuntoVta_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdSucursal = q.IdSucursal,
                                 IdPuntoVta = q.IdPuntoVta,
                                 cod_PuntoVta = q.cod_PuntoVta,
                                 nom_PuntoVta = q.nom_PuntoVta,
                                 estado = q.estado,
                                 Su_Descripcion = q.Su_Descripcion,
                                 CobroAutomatico = q.CobroAutomatico    ,
                                 EsElectronico  = q.EsElectronico,
                                 Descripcion=q.Descripcion

                    }).ToList();
                   
                }
                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<fa_PuntoVta_Info> get_list(int IdEmpresa, int IdSucursal, bool mostrar_anulados)
        {
            try
            {
                List<fa_PuntoVta_Info> Lista;
                using (EntitiesFacturacion Context = new EntitiesFacturacion())
                {
                    if (!mostrar_anulados)
                        Lista = (from q in Context.fa_PuntoVta
                                 where q.IdEmpresa == IdEmpresa
                                 && q.IdSucursal == IdSucursal
                                 && q.estado == true
                                 select new fa_PuntoVta_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdSucursal = q.IdSucursal,
                                     IdBodega = q.IdBodega,
                                     IdPuntoVta = q.IdPuntoVta,
                                     cod_PuntoVta = q.cod_PuntoVta,
                                     nom_PuntoVta = q.nom_PuntoVta,
                                     estado = q.estado,
                                     CobroAutomatico = q.CobroAutomatico

                                 }).ToList();
                    else
                        Lista = (from q in Context.fa_PuntoVta
                                 where q.IdEmpresa == IdEmpresa
                                 && q.IdSucursal == IdSucursal
                                 select new fa_PuntoVta_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdSucursal = q.IdSucursal,
                                     IdBodega = q.IdBodega,
                                     IdPuntoVta = q.IdPuntoVta,
                                     cod_PuntoVta = q.cod_PuntoVta,
                                     nom_PuntoVta = q.nom_PuntoVta,
                                     estado = q.estado,
                                     CobroAutomatico = q.CobroAutomatico
                                 }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<fa_PuntoVta_Info> GetListUsuario(int IdEmpresa, int IdSucursal, bool mostrar_anulados, string IdUsuario, bool EsContador, string codDocumentoTipo)
        {
            try
            {
                List<fa_PuntoVta_Info> Lista;
                using (EntitiesFacturacion Context = new EntitiesFacturacion())
                {
                    if (EsContador)
                    {
                        var lst = Context.fa_PuntoVta.Where(q => q.IdEmpresa == IdEmpresa && q.estado == (mostrar_anulados ? q.estado : true)
                        && q.IdSucursal == (IdSucursal == 0 ? q.IdSucursal : IdSucursal) && q.codDocumentoTipo == codDocumentoTipo
                        ).ToList();
                        Lista = lst.Select(q => new fa_PuntoVta_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            estado = q.estado,
                            IdCaja = q.IdCaja,
                            nom_PuntoVta = q.nom_PuntoVta,
                            IdSucursal = q.IdSucursal,
                            cod_PuntoVta = q.cod_PuntoVta,
                            IdBodega = q.IdBodega,
                            IdPuntoVta = q.IdPuntoVta
                        }).ToList();
                    }
                    else
                    {
                        var lst = Context.fa_PuntoVta_x_seg_usuario.Include("fa_PuntoVta").Where(q => q.IdEmpresa == IdEmpresa && q.IdUsuario.ToLower() == IdUsuario.ToLower() && q.fa_PuntoVta.estado == (mostrar_anulados ? q.fa_PuntoVta.estado : true) 
                        && q.fa_PuntoVta.IdSucursal == IdSucursal && q.fa_PuntoVta.codDocumentoTipo == codDocumentoTipo).ToList();
                        Lista = lst.Select(q => new fa_PuntoVta_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdPuntoVta =q.IdPuntoVta,
                            estado = q.fa_PuntoVta.estado,
                            IdCaja = q.fa_PuntoVta.IdCaja,
                            nom_PuntoVta = q.fa_PuntoVta.nom_PuntoVta,
                            IdSucursal = q.fa_PuntoVta.IdSucursal,
                            cod_PuntoVta = q.fa_PuntoVta.cod_PuntoVta,
                            IdBodega = q.fa_PuntoVta.IdBodega
                        }).ToList();
                    }
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public fa_PuntoVta_Info get_info(int IdEmpresa, int IdSucursal, int IdPuntoVta)
        {
            try
            {
                fa_PuntoVta_Info info = new fa_PuntoVta_Info();
                using (EntitiesFacturacion Context = new EntitiesFacturacion())
                {
                    var Entity = Context.vwfa_PuntoVta.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal  && q.IdPuntoVta == IdPuntoVta);
                    if (Entity == null) return null;
                    info = new fa_PuntoVta_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdSucursal = Entity.IdSucursal,
                        IdBodega = Entity.IdBodega,
                        IdPuntoVta = Entity.IdPuntoVta,
                        cod_PuntoVta = Entity.cod_PuntoVta,
                        nom_PuntoVta = Entity.nom_PuntoVta,
                        estado = Entity.estado,
                        Su_CodigoEstablecimiento = Entity.Su_CodigoEstablecimiento,
                        IdCaja = Entity.IdCaja,
                        IPImpresora = Entity.IPImpresora,
                        NumCopias = Entity.NumCopias,
                        CobroAutomatico = Entity.CobroAutomatico,
                        EsElectronico = Entity.EsElectronico,
                        codDocumentoTipo = Entity.codDocumentoTipo,
                                              

                    };
                }
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private int get_id(int IdEmpresa, int IdSucursal)
        {

            try
            {
                int ID = 1;
                using (EntitiesFacturacion Context = new EntitiesFacturacion())
                {
                    var lst = from q in Context.fa_PuntoVta
                              where q.IdEmpresa == IdEmpresa
                              where q.IdSucursal == IdSucursal
                              select q;

                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdPuntoVta) + 1;
                }
                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(fa_PuntoVta_Info info)
        {
            try
            {
                using (EntitiesFacturacion Context = new EntitiesFacturacion())
                {
                    fa_PuntoVta Entity = new fa_PuntoVta
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdSucursal = info.IdSucursal,
                        IdBodega = info.IdBodega,
                        IdPuntoVta = info.IdPuntoVta = get_id(info.IdEmpresa, info.IdSucursal),
                        cod_PuntoVta = info.cod_PuntoVta,
                         nom_PuntoVta = info.nom_PuntoVta,
                        estado = info.estado = true,
                        IdCaja = info.IdCaja,
                        IPImpresora = info.IPImpresora,
                        NumCopias = info.NumCopias,
                        CobroAutomatico = info.CobroAutomatico,
                        EsElectronico = info.EsElectronico,
                        codDocumentoTipo = info.codDocumentoTipo,
                        IdUsuarioCreacion = info.IdUsuarioCreacion,
                        FechaCreacion = DateTime.Now
                    };
                    Context.fa_PuntoVta.Add(Entity);

                    if (info.lst_usuarios!= null || info.lst_usuarios.Count>0)
                    {
                        int Secuencia = 1;

                        foreach (var item in info.lst_usuarios)
                        {
                            Context.fa_PuntoVta_x_seg_usuario.Add(new fa_PuntoVta_x_seg_usuario
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdPuntoVta = info.IdPuntoVta,
                                IdSucursal = info.IdSucursal,
                                Secuencia = Secuencia++,
                                IdUsuario = item.IdUsuario
                            });

                        }
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

        public bool modificarDB(fa_PuntoVta_Info info)
        {
            try
            {
                using (EntitiesFacturacion Context = new EntitiesFacturacion())
                {
                    fa_PuntoVta Entity = Context.fa_PuntoVta.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdSucursal == info.IdSucursal  && q.IdPuntoVta == info.IdPuntoVta);
                    if (Entity == null) return false;
                    
                    Entity.cod_PuntoVta = info.cod_PuntoVta;
                    Entity.nom_PuntoVta = info.nom_PuntoVta;
                    Entity.IdCaja = info.IdCaja;
                    Entity.IPImpresora = info.IPImpresora;
                    Entity.NumCopias = info.NumCopias;
                    Entity.CobroAutomatico = info.CobroAutomatico;
                    Entity.EsElectronico = info.EsElectronico;
                    Entity.codDocumentoTipo = info.codDocumentoTipo;

                    Entity.IdUsuarioModificacion = info.IdUsuarioModificacion;
                    Entity.FechaModificacion = DateTime.Now;

                    var lst_Usuarios = Context.fa_PuntoVta_x_seg_usuario.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdPuntoVta == info.IdPuntoVta).ToList();
                    Context.fa_PuntoVta_x_seg_usuario.RemoveRange(lst_Usuarios);
                    if (info.lst_usuarios != null || info.lst_usuarios.Count > 0)
                    {
                        int Secuencia = 1;

                        foreach (var item in info.lst_usuarios)
                        {
                            Context.fa_PuntoVta_x_seg_usuario.Add(new fa_PuntoVta_x_seg_usuario
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdPuntoVta = info.IdPuntoVta,
                                IdSucursal = info.IdSucursal,
                                Secuencia = Secuencia++,
                                IdUsuario = item.IdUsuario
                            });

                        }
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

        public bool anularDB(fa_PuntoVta_Info info)
        {
            try
            {
                using (EntitiesFacturacion Context = new EntitiesFacturacion())
                {
                    fa_PuntoVta Entity = Context.fa_PuntoVta.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdSucursal == info.IdSucursal && q.IdBodega == info.IdBodega && q.IdPuntoVta == info.IdPuntoVta);
                    if (Entity == null) return false;

                    Entity.estado = Entity.estado = false;

                    Entity.IdUsuarioAnulacion = info.IdUsuarioAnulacion;
                    Entity.MotivoAnulacion = info.MotivoAnulacion;
                    Entity.FechaAnulacion = DateTime.Now;
                    Context.SaveChanges();

                }
                return true; 
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<fa_PuntoVta_Info> get_list_x_tipo_doc(int IdEmpresa, int IdSucursal, string codDocumentoTipo)
        {
            try
            {
                List<fa_PuntoVta_Info> Lista;
                using (EntitiesFacturacion Context = new EntitiesFacturacion())
                {
                    Lista = (from q in Context.fa_PuntoVta
                                where q.IdEmpresa == IdEmpresa
                                && q.IdSucursal == IdSucursal
                                && q.codDocumentoTipo == codDocumentoTipo
                                && q.estado == true
                                select new fa_PuntoVta_Info
                                {
                                    IdEmpresa = q.IdEmpresa,
                                    IdSucursal = q.IdSucursal,
                                    IdBodega = q.IdBodega,
                                    IdPuntoVta = q.IdPuntoVta,
                                    cod_PuntoVta = q.cod_PuntoVta,
                                    nom_PuntoVta = q.nom_PuntoVta,
                                    estado = q.estado,
                                    CobroAutomatico = q.CobroAutomatico

                                }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
