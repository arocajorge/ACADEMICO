using Core.Data.Base;
using Core.Data.General;
using Core.Info.Banco;
using Core.Info.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Banco
{
    public class ba_ArchivoRecaudacion_Data
    {
        public List<ba_ArchivoRecaudacion_Info> GetList(int IdEmpresa, DateTime fechaini, DateTime fechafin, bool mostrar_anulados)
        {
            try
            {
                List<ba_ArchivoRecaudacion_Info> Lista= new List<ba_ArchivoRecaudacion_Info>();
                using (EntitiesBanco Context = new EntitiesBanco())
                {
                    var lst = Context.ba_ArchivoRecaudacion.Where(q => q.IdEmpresa == IdEmpresa && fechaini <= q.Fecha && q.Fecha <= fechafin && q.Estado == (mostrar_anulados == true ? q.Estado : true)).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new ba_ArchivoRecaudacion_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdArchivo = q.IdArchivo,
                            IdBanco = q.IdBanco,
                            IdProceso_bancario = q.IdProceso_bancario,
                            Fecha = q.Fecha,
                            Nom_Archivo = q.Nom_Archivo,
                            Observacion = q.Observacion,
                            Estado = q.Estado,
                            Valor = q.Valor,
                            ValorProntoPago = q.ValorProntoPago
                        });
                    });
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ba_ArchivoRecaudacion_Info GetInfo(int IdEmpresa, decimal IdArchivo)
        {
            try
            {
                ba_ArchivoRecaudacion_Info info = new ba_ArchivoRecaudacion_Info();
                using (EntitiesBanco Context = new EntitiesBanco())
                {
                    ba_ArchivoRecaudacion Entity = Context.ba_ArchivoRecaudacion.Where(q => q.IdEmpresa == IdEmpresa && q.IdArchivo == IdArchivo).FirstOrDefault();
                    if (Entity == null) return null;
                    info = new ba_ArchivoRecaudacion_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdArchivo = Entity.IdArchivo,
                        IdBanco = Entity.IdBanco,
                        IdProceso_bancario = Entity.IdProceso_bancario,
                        SecuencialDescarga = Entity.SecuencialDescarga,
                        Nom_Archivo = Entity.Nom_Archivo,
                        Estado = Entity.Estado,
                        Fecha = Entity.Fecha,
                        Observacion = Entity.Observacion,
                        Valor = Entity.Valor,
                        ValorProntoPago = Entity.ValorProntoPago
                    };
                }
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private decimal GetId(int IdEmpresa)
        {
            try
            {
                decimal Id = 1;
                using (EntitiesBanco Context = new EntitiesBanco())
                {
                    var lst = from q in Context.ba_ArchivoRecaudacion
                              where q.IdEmpresa == IdEmpresa
                              select q;
                    if (lst.Count() > 0)
                        Id = lst.Max(q => q.IdArchivo) + 1;
                }
                return Id;
            }
            catch (Exception)
            {

                throw;
            }
        }
        private int GetSecuencia(int IdEmpresa)
        {
            try
            {
                int Secuencia = 1;
                using (EntitiesBanco Context = new EntitiesBanco())
                {
                    var lst = from q in Context.ba_ArchivoRecaudacion
                              where q.IdEmpresa == IdEmpresa
                              select q;
                    if (lst.Count() > 0)
                        Secuencia = lst.Max(q => q.SecuencialDescarga) + 1;
                }
                return Secuencia;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool GuardarDB(ba_ArchivoRecaudacion_Info info)
        {
            try
            {
                using (EntitiesBanco Context = new EntitiesBanco())
                {
                    Context.ba_ArchivoRecaudacion.Add(new ba_ArchivoRecaudacion
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdArchivo = info.IdArchivo = GetId(info.IdEmpresa),
                        Estado = true,
                        Fecha = info.Fecha.Date,
                        IdBanco = info.IdBanco,
                        IdProceso_bancario = info.IdProceso_bancario,
                        Nom_Archivo = info.Nom_Archivo,
                        Valor = info.Valor,
                        ValorProntoPago = info.ValorProntoPago,
                        Observacion = info.Observacion,
                        IdUsuarioCreacion = info.IdUsuarioCreacion,
                        FechaCreacion = DateTime.Now,
                        SecuencialDescarga = 1
                    });

                    int Secuencia = 1;
                    if (info.Lst_det.Count() > 0)
                    {
                        foreach (var item in info.Lst_det)
                        {
                            Context.ba_ArchivoRecaudacionDet.Add(new ba_ArchivoRecaudacionDet
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdArchivo = info.IdArchivo,
                                Secuencia = Secuencia++,
                                IdMatricula = item.IdMatricula,
                                IdAlumno = item.IdAlumno,
                                FechaProceso = item.FechaProceso,
                                Valor = item.Valor,
                                ValorProntoPago = item.ValorProntoPago,
                                FechaProntoPago = item.FechaProntoPago
                            });
                        }
                    }

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                tb_LogError_Data LogData = new tb_LogError_Data();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "ba_ArchivoRecaudacion_Data", Metodo = "GuardarDB", IdUsuario = info.IdUsuarioCreacion });
                return false;
            }
        }

        public bool ModificarDB(ba_ArchivoRecaudacion_Info info)
        {
            try
            {
                using (EntitiesBanco Context = new EntitiesBanco())
                {
                    ba_ArchivoRecaudacion Entity = Context.ba_ArchivoRecaudacion.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdArchivo == info.IdArchivo).FirstOrDefault();
                    if (Entity == null) return false;

                    Entity.Fecha = info.Fecha;
                    Entity.Valor = info.Valor;
                    Entity.ValorProntoPago = info.ValorProntoPago;
                    Entity.Observacion = info.Observacion;
                    Entity.IdUsuarioModificacion = info.IdUsuarioModificacion;
                    Entity.FechaModificacion = DateTime.Now;

                    var Lst_det = Context.ba_ArchivoRecaudacionDet.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdArchivo == info.IdArchivo).ToList();
                    Context.ba_ArchivoRecaudacionDet.RemoveRange(Lst_det);

                    int Secuencia = 1;
                    if (info.Lst_det.Count() > 0)
                    {
                        foreach (var item in info.Lst_det)
                        {
                            Context.ba_ArchivoRecaudacionDet.Add(new ba_ArchivoRecaudacionDet
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdArchivo = info.IdArchivo,
                                Secuencia = Secuencia++,
                                IdMatricula = item.IdMatricula,
                                IdAlumno = item.IdAlumno,
                                FechaProceso = item.FechaProceso,
                                Valor = item.Valor,
                                ValorProntoPago = item.ValorProntoPago
                            });
                        }
                    }

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                tb_LogError_Data LogData = new tb_LogError_Data();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "ba_ArchivoRecaudacion_Data", Metodo = "ModificarDB", IdUsuario = info.IdUsuarioModificacion });
                return false;
            }
        }

        public bool ModificarSecuenciaDescargaDB(ba_ArchivoRecaudacion_Info info)
        {
            try
            {
                using (EntitiesBanco Context = new EntitiesBanco())
                {
                    ba_ArchivoRecaudacion Entity = Context.ba_ArchivoRecaudacion.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdArchivo == info.IdArchivo).FirstOrDefault();
                    if (Entity == null) return false;

                    Entity.SecuencialDescarga = info.SecuencialDescarga+1;
                    Entity.IdUsuarioModificacion = info.IdUsuarioModificacion;
                    Entity.FechaModificacion = DateTime.Now;
                    
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                tb_LogError_Data LogData = new tb_LogError_Data();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "ba_ArchivoRecaudacion_Data", Metodo = "ModificarDB", IdUsuario = info.IdUsuarioModificacion });
                return false;
            }
        }

        public bool AnularDB(ba_ArchivoRecaudacion_Info info)
        {
            try
            {
                using (EntitiesBanco Context = new EntitiesBanco())
                {
                    //var Lst_det = Context.ba_ArchivoRecaudacionDet.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdArchivo == info.IdArchivo).ToList();
                    //Context.ba_ArchivoRecaudacionDet.RemoveRange(Lst_det);

                    //Context.ba_ArchivoRecaudacion.Remove(Context.ba_ArchivoRecaudacion.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdArchivo == info.IdArchivo).FirstOrDefault());
                    ba_ArchivoRecaudacion Entity = Context.ba_ArchivoRecaudacion.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdArchivo == info.IdArchivo).FirstOrDefault();
                    if (Entity == null) return false;

                    Entity.Estado = false;
                    Entity.IdUsuarioAnulacion = info.IdUsuarioModificacion;
                    Entity.FechaAnulacion = DateTime.Now;
                    Entity.MotivoAnulacion = info.MotivoAnulacion;

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
