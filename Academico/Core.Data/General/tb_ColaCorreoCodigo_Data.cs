using Core.Data.Base;
using Core.Info.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.General
{
    public class tb_ColaCorreoCodigo_Data
    {
        public List<tb_ColaCorreoCodigo_Info> GetList(int IdEmpresa)
        {
            try
            {
                List<tb_ColaCorreoCodigo_Info> Lista = new List<tb_ColaCorreoCodigo_Info>();

                using (EntitiesGeneral db = new EntitiesGeneral())
                {
                    var lst = db.tb_ColaCorreoCodigo.Where(q => q.IdEmpresa == IdEmpresa).ToList();
                    foreach (var item in lst)
                    {
                        Lista.Add(new tb_ColaCorreoCodigo_Info
                        {
                            IdEmpresa = item.IdEmpresa,
                            Codigo = item.Codigo,
                            ApareceSeguimientoCobranza = item.ApareceSeguimientoCobranza,
                            Asunto = item.Asunto,
                            Cuerpo = item.Cuerpo,
                            CantidadFin = item.CantidadFin,
                            CantidadIni = item.CantidadIni
                        });
                    }
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<tb_ColaCorreoCodigo_Info> GetList_Academico(int IdEmpresa)
        {
            try
            {
                List<tb_ColaCorreoCodigo_Info> Lista = new List<tb_ColaCorreoCodigo_Info>();

                using (EntitiesGeneral db = new EntitiesGeneral())
                {
                    var lst = db.tb_ColaCorreoCodigo.Where(q => q.IdEmpresa == IdEmpresa && q.ApareceSeguimientoCobranza == false).ToList();
                    foreach (var item in lst)
                    {
                        Lista.Add(new tb_ColaCorreoCodigo_Info
                        {
                            IdEmpresa = item.IdEmpresa,
                            Codigo = item.Codigo,
                            ApareceSeguimientoCobranza = item.ApareceSeguimientoCobranza,
                            Asunto = item.Asunto,
                            Cuerpo = item.Cuerpo,
                            CantidadFin = item.CantidadFin,
                            CantidadIni = item.CantidadIni
                        });
                    }
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<tb_ColaCorreoCodigo_Info> GetList_Seguimiento(int IdEmpresa)
        {
            try
            {
                List<tb_ColaCorreoCodigo_Info> Lista = new List<tb_ColaCorreoCodigo_Info>();

                using (EntitiesGeneral db = new EntitiesGeneral())
                {
                    var lst = db.tb_ColaCorreoCodigo.Where(q => q.IdEmpresa == IdEmpresa && q.ApareceSeguimientoCobranza==true).ToList();
                    foreach (var item in lst)
                    {
                        Lista.Add(new tb_ColaCorreoCodigo_Info
                        {
                            IdEmpresa = item.IdEmpresa,
                            Codigo = item.Codigo,
                            ApareceSeguimientoCobranza = item.ApareceSeguimientoCobranza,
                            Asunto = item.Asunto,
                            Cuerpo = item.Cuerpo,
                            CantidadFin = item.CantidadFin,
                            CantidadIni = item.CantidadIni
                        });
                    }
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public tb_ColaCorreoCodigo_Info GetInfo(int IdEmpresa, string Codigo)
        {
            try
            {
                tb_ColaCorreoCodigo_Info info = new tb_ColaCorreoCodigo_Info();

                using (EntitiesGeneral db = new EntitiesGeneral())
                {
                    var Entity = db.tb_ColaCorreoCodigo.Where(q => q.IdEmpresa == IdEmpresa && q.Codigo == Codigo).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new tb_ColaCorreoCodigo_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        Codigo = Entity.Codigo,
                        ApareceSeguimientoCobranza = Entity.ApareceSeguimientoCobranza,
                        Asunto = Entity.Asunto,
                        Cuerpo = Entity.Cuerpo,
                        CantidadIni = Entity.CantidadIni,
                        CantidadFin = Entity.CantidadFin
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Existe_codigo(int IdEmpresa, string Codigo)
        {
            try
            {
                tb_ColaCorreoCodigo_Info info = new tb_ColaCorreoCodigo_Info();

                using (EntitiesGeneral db = new EntitiesGeneral())
                {
                    var Entity = db.tb_ColaCorreoCodigo.Where(q => q.IdEmpresa == IdEmpresa && q.Codigo == Codigo).FirstOrDefault();
                    if (Entity == null)
                        return false;

                    return true;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(tb_ColaCorreoCodigo_Info info)
        {
            try
            {
                using (EntitiesGeneral Context = new EntitiesGeneral())
                {
                    tb_ColaCorreoCodigo Entity = new tb_ColaCorreoCodigo
                    {
                        IdEmpresa = info.IdEmpresa,
                        Codigo = info.Codigo,
                        ApareceSeguimientoCobranza = (info.ApareceSeguimientoCobranza == null ? false : info.ApareceSeguimientoCobranza),
                        Asunto = info.Asunto,
                        Cuerpo = info.Cuerpo,
                        CantidadIni=info.CantidadIni,
                        CantidadFin=info.CantidadFin
                    };
                    Context.tb_ColaCorreoCodigo.Add(Entity);

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(tb_ColaCorreoCodigo_Info info)
        {
            try
            {
                using (EntitiesGeneral Context = new EntitiesGeneral())
                {
                    tb_ColaCorreoCodigo Entity = Context.tb_ColaCorreoCodigo.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.Codigo == info.Codigo);
                    if (Entity == null)
                        return false;

                    Entity.Asunto = info.Asunto;
                    Entity.Cuerpo = info.Cuerpo;
                    Entity.ApareceSeguimientoCobranza = (info.ApareceSeguimientoCobranza == null ? false : info.ApareceSeguimientoCobranza);
                    Entity.CantidadFin = info.CantidadFin;
                    Entity.CantidadIni = info.CantidadIni;
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
