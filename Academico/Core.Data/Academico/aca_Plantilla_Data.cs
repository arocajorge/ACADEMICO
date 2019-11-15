﻿using Core.Data.Base;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_Plantilla_Data
    {
        public List<aca_Plantilla_Info> getList(int IdEmpresa, int IdAnio, bool MostrarAnulados)
        {
            try
            {
                var IdAnio_ini = IdAnio;
                var IdAnio_fin = (IdAnio == 0 ? 99999999999 : IdAnio);
                List<aca_Plantilla_Info> Lista = new List<aca_Plantilla_Info>();

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    Lista = Context.aca_Plantilla.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio >= IdAnio_ini && q.IdAnio <= IdAnio_fin).Select(q => new aca_Plantilla_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdAnio = q.IdAnio,
                        IdPlantilla = q.IdPlantilla,
                        NomPlantilla = q.NomPlantilla,
                        Valor = q.Valor,
                        Estado = q.Estado
                    }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public aca_Plantilla_Info getInfo(int IdEmpresa, int IdAnio, int IdPlantilla)
        {
            try
            {
                aca_Plantilla_Info info;

                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.aca_Plantilla.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdPlantilla == IdPlantilla).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new aca_Plantilla_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdPlantilla = Entity.IdPlantilla,
                        IdAnio = Entity.IdAnio,
                        NomPlantilla = Entity.NomPlantilla,
                        Valor = Entity.Valor
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int getId(int IdEmpresa)
        {
            try
            {
                int ID = 1;

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var cont = Context.aca_Plantilla.Where(q => q.IdEmpresa == IdEmpresa).Count();
                    if (cont > 0)
                        ID = Context.aca_Plantilla.Where(q => q.IdEmpresa == IdEmpresa).Max(q => q.IdPlantilla) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(aca_Plantilla_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_Plantilla Entity = new aca_Plantilla
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdAnio = info.IdAnio,
                        IdPlantilla = info.IdPlantilla = getId(info.IdEmpresa),
                        NomPlantilla = info.NomPlantilla,
                        Valor = info.Valor,
                        Estado = true,
                        IdUsuarioCreacion = info.IdUsuarioCreacion,
                        FechaCreacion = DateTime.Now
                    };
                    Context.aca_Plantilla.Add(Entity);

                    var lst_PlantillaDet = Context.aca_Plantilla_Rubro.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdAnio == info.IdAnio && q.IdPlantilla == info.IdPlantilla).ToList();
                    Context.aca_Plantilla_Rubro.RemoveRange(lst_PlantillaDet);

                    if (info.lst_Plantilla_Rubro.Count > 0)
                    {
                        foreach (var item in info.lst_Plantilla_Rubro)
                        {
                            aca_Plantilla_Rubro Entity_Det = new aca_Plantilla_Rubro
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdAnio = info.IdAnio,
                                IdPlantilla = info.IdPlantilla,
                                IdRubro = item.IdRubro,
                                IdProducto = item.IdProducto,
                                Subtotal = item.Subtotal,
                                IdCod_Impuesto_Iva = item.IdCod_Impuesto_Iva,
                                Porcentaje = item.Porcentaje,
                                Total= item.Total
                            };
                            Context.aca_Plantilla_Rubro.Add(Entity_Det);
                        }
                    }

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public bool modificarDB(aca_Plantilla_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_Plantilla Entity = Context.aca_Plantilla.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdAnio == info.IdAnio && q.IdPlantilla == info.IdPlantilla);
                    if (Entity == null)
                        return false;

                    Entity.IdAnio = info.IdAnio;
                    Entity.NomPlantilla = info.NomPlantilla;
                    Entity.Valor = info.Valor;
                    Entity.IdUsuarioModificacion = info.IdUsuarioModificacion;
                    Entity.FechaModificacion = DateTime.Now;

                    var lst_PlantillaDet = Context.aca_Plantilla_Rubro.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdAnio == info.IdAnio && q.IdPlantilla == info.IdPlantilla).ToList();
                    Context.aca_Plantilla_Rubro.RemoveRange(lst_PlantillaDet);

                    if (info.lst_Plantilla_Rubro.Count > 0)
                    {
                        foreach (var item in info.lst_Plantilla_Rubro)
                        {
                            aca_Plantilla_Rubro Entity_Det = new aca_Plantilla_Rubro
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdAnio = info.IdAnio,
                                IdPlantilla = info.IdPlantilla,
                                IdRubro = item.IdRubro,
                                IdProducto = item.IdProducto,
                                Subtotal = item.Subtotal,
                                IdCod_Impuesto_Iva = item.IdCod_Impuesto_Iva,
                                Porcentaje = item.Porcentaje,
                                Total = item.Total
                            };
                            Context.aca_Plantilla_Rubro.Add(Entity_Det);
                        }
                    }

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public bool anularDB(aca_Plantilla_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_Plantilla Entity = Context.aca_Plantilla.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdAnio == info.IdAnio && q.IdPlantilla == info.IdPlantilla);
                    if (Entity == null)
                        return false;

                    Entity.Estado = false;
                    Entity.IdUsuarioAnulacion = info.IdUsuarioAnulacion;
                    Entity.FechaAnulacion = DateTime.Now;

                    var lst_PlantillaDet = Context.aca_Plantilla_Rubro.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdAnio == info.IdAnio && q.IdPlantilla == info.IdPlantilla).ToList();
                    Context.aca_Plantilla_Rubro.RemoveRange(lst_PlantillaDet);

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}