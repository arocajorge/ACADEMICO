using Core.Data.Base;
using Core.Info.Academico;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = "SELECT * FROM aca_Plantilla p WITH (nolock) "
                    + " WHERE p.IdEmpresa = " + IdEmpresa.ToString() + " and p.IdAnio between "+ IdAnio_ini.ToString() + " and "+ IdAnio_fin.ToString();
                    if (MostrarAnulados == false)
                    {
                        query += " and p.Estado = 1";
                    }
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandTimeout = 0;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new aca_Plantilla_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdAnio = Convert.ToInt32(reader["IdAnio"]),
                            IdPlantilla = Convert.ToInt32(reader["IdPlantilla"]),
                            NomPlantilla = string.IsNullOrEmpty(reader["NomPlantilla"].ToString()) ? null : reader["NomPlantilla"].ToString(),
                            TipoDescuento = string.IsNullOrEmpty(reader["TipoDescuento"].ToString()) ? null : reader["TipoDescuento"].ToString(),
                            IdTipoNota = Convert.ToInt32(reader["IdTipoNota"]),
                            IdTipoPlantilla = string.IsNullOrEmpty(reader["IdTipoPlantilla"].ToString()) ? (int?)null : Convert.ToInt32(reader["IdTipoPlantilla"]),
                            Valor = Convert.ToInt32(reader["Valor"]),
                            AplicaParaTodo = string.IsNullOrEmpty(reader["AplicaParaTodo"].ToString()) ? false : Convert.ToBoolean(reader["AplicaParaTodo"]),
                            Estado = Convert.ToBoolean(reader["Estado"])
                        });
                    }
                    reader.Close();
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
                aca_Plantilla_Info info = new aca_Plantilla_Info();

                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("", connection);
                    command.CommandText = "SELECT * FROM aca_Plantilla p WITH (nolock) "
                    + " WHERE p.IdEmpresa = " + IdEmpresa.ToString() + " and p.IdAnio = " + IdAnio.ToString() + " and p.IdPlantilla = " + IdPlantilla.ToString();
                    var ResultValue = command.ExecuteScalar();

                    if (ResultValue == null)
                        return null;

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        info = new aca_Plantilla_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdPlantilla = Convert.ToInt32(reader["IdPlantilla"]),
                            IdAnio = Convert.ToInt32(reader["IdAnio"]),
                            NomPlantilla = string.IsNullOrEmpty(reader["NomPlantilla"].ToString()) ? null : reader["NomPlantilla"].ToString(),
                            TipoDescuento = string.IsNullOrEmpty(reader["TipoDescuento"].ToString()) ? null : reader["TipoDescuento"].ToString(),
                            IdTipoNota = Convert.ToInt32(reader["IdTipoNota"]),
                            IdTipoPlantilla = string.IsNullOrEmpty(reader["IdTipoPlantilla"].ToString()) ? (int?)null : Convert.ToInt32(reader["IdTipoPlantilla"]),
                            Valor = Convert.ToInt32(reader["Valor"]),
                            AplicaParaTodo = string.IsNullOrEmpty(reader["AplicaParaTodo"].ToString()) ? false : Convert.ToBoolean(reader["AplicaParaTodo"]),
                            Estado = Convert.ToBoolean(reader["Estado"])
                        };
                    }
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
                        TipoDescuento = info.TipoDescuento,
                        IdTipoNota = info.IdTipoNota,
                        IdTipoPlantilla = info.IdTipoPlantilla,
                        Valor = info.Valor,
                        Estado = true,
                        AplicaParaTodo = info.AplicaParaTodo,
                        IdUsuarioCreacion = info.IdUsuarioCreacion,
                        FechaCreacion = DateTime.Now
                    };
                    Context.aca_Plantilla.Add(Entity);

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
                                Total= item.Total,
                                IdTipoNota_descuentoDet = item.IdTipoNota_descuentoDet,
                                TipoDescuento_descuentoDet = item.TipoDescuento_descuentoDet,
                                Valor_descuentoDet = item.Valor_descuentoDet
                            };
                            Context.aca_Plantilla_Rubro.Add(Entity_Det);
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
                    Entity.TipoDescuento = info.TipoDescuento;
                    Entity.IdTipoNota = info.IdTipoNota;
                    Entity.IdTipoPlantilla = info.IdTipoPlantilla;
                    Entity.Valor = info.Valor;
                    Entity.AplicaParaTodo = info.AplicaParaTodo;
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
                                Total = item.Total,
                                IdTipoNota_descuentoDet = item.IdTipoNota_descuentoDet,
                                TipoDescuento_descuentoDet = item.TipoDescuento_descuentoDet,
                                Valor_descuentoDet = item.Valor_descuentoDet
                            };
                            Context.aca_Plantilla_Rubro.Add(Entity_Det);
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
                    Entity.MotivoAnulacion = info.MotivoAnulacion;
                    Entity.FechaAnulacion = DateTime.Now;

                    //var lst_PlantillaDet = Context.aca_Plantilla_Rubro.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdAnio == info.IdAnio && q.IdPlantilla == info.IdPlantilla).ToList();
                    //Context.aca_Plantilla_Rubro.RemoveRange(lst_PlantillaDet);

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
