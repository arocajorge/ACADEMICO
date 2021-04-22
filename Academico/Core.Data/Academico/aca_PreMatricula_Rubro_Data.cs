using Core.Data.Base;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_PreMatricula_Rubro_Data
    {
        public List<aca_PreMatricula_Rubro_Info> getListPreMatricula(int IdEmpresa, int IdAnio, int IdPlantilla)
        {
            try
            {
                List<aca_PreMatricula_Rubro_Info> Lista = new List<aca_PreMatricula_Rubro_Info>();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = "SELECT dbo.aca_Plantilla_Rubro.IdEmpresa, dbo.aca_Plantilla_Rubro.IdAnio, dbo.aca_Plantilla_Rubro.IdPlantilla, dbo.aca_Plantilla_Rubro.IdRubro, dbo.aca_AnioLectivo_Rubro.NomRubro, arp.IdPeriodo, "
                    + " dbo.aca_AnioLectivo_Periodo.FechaDesde, dbo.aca_AnioLectivo_Periodo.FechaHasta, dbo.aca_Plantilla_Rubro.IdProducto, dbo.aca_Plantilla_Rubro.Subtotal, dbo.aca_Plantilla_Rubro.IdCod_Impuesto_Iva, "
                    + " dbo.aca_Plantilla_Rubro.Porcentaje, dbo.aca_Plantilla_Rubro.ValorIVA, dbo.aca_Plantilla_Rubro.Total, pro.pr_descripcion, dbo.aca_AnioLectivo_Rubro.AplicaProntoPago, CASE WHEN aca_AnioLectivo_Rubro.AplicaProntoPago = 1 AND "
                    + " dbo.aca_AnioLectivo_Periodo.FechaProntoPago > CAST(getdate() AS date) THEN CAST(dbo.aca_Plantilla_Rubro.Subtotal AS float) "
                    + " - (CASE WHEN dbo.aca_Plantilla.AplicaParaTodo = 1 THEN(CASE WHEN dbo.aca_Plantilla.TipoDescuento = '$' THEN CAST(dbo.aca_Plantilla.Valor AS FLOAT) ELSE ROUND(CAST(dbo.aca_Plantilla_Rubro.Subtotal AS float) "
                    + " * (dbo.aca_Plantilla.Valor / 100), 2) END) ELSE(CASE WHEN dbo.aca_Plantilla_Rubro.TipoDescuento_descuentoDet = '$' THEN CAST(dbo.aca_Plantilla_Rubro.Valor_descuentoDet AS FLOAT) "
                    + " ELSE ROUND(CAST(dbo.aca_Plantilla_Rubro.Subtotal AS float) * (dbo.aca_Plantilla_Rubro.Valor_descuentoDet / 100), 2) END) END) ELSE CAST(dbo.aca_Plantilla_Rubro.Subtotal AS float)  "
                    + " + CAST(dbo.aca_Plantilla_Rubro.ValorIVA AS FLOAT) END + dbo.aca_Plantilla_Rubro.ValorIVA AS ValorProntoPago, dbo.aca_AnioLectivo_Periodo.FechaProntoPago "
                    + " FROM dbo.aca_AnioLectivo_Rubro_Periodo AS arp WITH (nolock) INNER JOIN "
                    + " dbo.aca_Plantilla_Rubro WITH (nolock) ON arp.IdEmpresa = dbo.aca_Plantilla_Rubro.IdEmpresa AND arp.IdAnio = dbo.aca_Plantilla_Rubro.IdAnio AND arp.IdRubro = dbo.aca_Plantilla_Rubro.IdRubro INNER JOIN "
                    + " dbo.aca_AnioLectivo_Periodo WITH (nolock) ON arp.IdEmpresa = dbo.aca_AnioLectivo_Periodo.IdEmpresa AND arp.IdPeriodo = dbo.aca_AnioLectivo_Periodo.IdPeriodo INNER JOIN "
                    + " dbo.aca_AnioLectivo_Rubro WITH (nolock) ON arp.IdEmpresa = dbo.aca_AnioLectivo_Rubro.IdEmpresa AND arp.IdAnio = dbo.aca_AnioLectivo_Rubro.IdAnio AND arp.IdRubro = dbo.aca_AnioLectivo_Rubro.IdRubro INNER JOIN "
                    + " dbo.in_Producto AS pro WITH (nolock) ON dbo.aca_Plantilla_Rubro.IdEmpresa = pro.IdEmpresa AND dbo.aca_Plantilla_Rubro.IdProducto = pro.IdProducto INNER JOIN "
                    + " dbo.aca_Plantilla WITH (nolock) ON dbo.aca_Plantilla_Rubro.IdEmpresa = dbo.aca_Plantilla.IdEmpresa AND dbo.aca_Plantilla_Rubro.IdAnio = dbo.aca_Plantilla.IdAnio AND dbo.aca_Plantilla_Rubro.IdPlantilla = dbo.aca_Plantilla.IdPlantilla "
                    + " WHERE dbo.aca_Plantilla_Rubro.IdEmpresa = " + IdEmpresa.ToString() + " and dbo.aca_Plantilla_Rubro.IdAnio = " + IdAnio.ToString() + " and dbo.aca_Plantilla_Rubro.IdPlantilla = " + IdPlantilla.ToString()
                    + " ORDER BY arp.IdPeriodo ";
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandTimeout = 0;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new aca_PreMatricula_Rubro_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdAnio = Convert.ToInt32(reader["IdAnio"]),
                            IdPlantilla = Convert.ToInt32(reader["IdPlantilla"]),
                            IdPeriodo = Convert.ToInt32(reader["IdPeriodo"]),
                            IdRubro = Convert.ToInt32(reader["IdRubro"]),
                            IdProducto = Convert.ToDecimal(reader["IdProducto"]),
                            Subtotal = Convert.ToDecimal(reader["Subtotal"]),
                            IdCod_Impuesto_Iva = reader["IdCod_Impuesto_Iva"].ToString(),
                            ValorIVA = Convert.ToDecimal(reader["ValorIVA"]),
                            Porcentaje = Convert.ToDecimal(reader["Porcentaje"]),
                            Total = Convert.ToDecimal(reader["Total"]),
                            NomRubro = reader["NomRubro"].ToString(),
                            FechaDesde = Convert.ToDateTime(reader["FechaDesde"]),
                            pr_descripcion = reader["pr_descripcion"].ToString(),
                            AplicaProntoPago = Convert.ToBoolean(reader["AplicaProntoPago"]),
                            ValorProntoPago = string.IsNullOrEmpty(reader["ValorProntoPago"].ToString()) ? 0 : Convert.ToDecimal(reader["ValorProntoPago"]),
                            FechaProntoPago = string.IsNullOrEmpty(reader["FechaProntoPago"].ToString()) ? DateTime.Now.Date : Convert.ToDateTime(reader["FechaProntoPago"])
                        });
                    }
                    reader.Close();
                }

                Lista.ForEach(v => { v.Periodo = v.FechaDesde.Year.ToString("0000") + v.FechaDesde.Month.ToString("00"); });
                Lista.ForEach(q => q.IdString = q.IdEmpresa.ToString("0000000") + q.IdPlantilla.ToString("0000000") + q.IdPeriodo.ToString("0000000") + q.IdRubro.ToString("0000000"));
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<aca_PreMatricula_Rubro_Info> getList(int IdEmpresa, decimal IdPreMatricula)
        {
            try
            {
                List<aca_PreMatricula_Rubro_Info> Lista = new List<aca_PreMatricula_Rubro_Info>();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = "SELECT mr.IdEmpresa, mr.IdPreMatricula, mr.IdPeriodo, ap.FechaDesde, mr.IdRubro, ar.NomRubro, mr.IdProducto, mr.Subtotal, "
                    + " mr.IdCod_Impuesto_Iva, mr.Porcentaje, mr.ValorIVA, mr.Total, p.pr_descripcion, mr.IdSucursal, mr.IdBodega,  mr.IdCbteVta, "
                    + " mr.FechaFacturacion, mr.IdMecanismo, mr.EnMatricula, mr.IdAnio, mr.IdPlantilla, mr.IdSede, mr.IdNivel, mr.IdJornada, "
                    + " mr.IdCurso,  mr.IdParalelo, ar.AplicaProntoPago, ap.FechaProntoPago, "
                    + " CASE WHEN ar.AplicaProntoPago = 1 AND ap.FechaProntoPago > CAST(getdate() AS date) "
                    + " THEN CAST(mr.Subtotal AS float)  -(CASE WHEN pl.AplicaParaTodo = 1 "
                    + " THEN(CASE WHEN pl.TipoDescuento = '$' THEN CAST(pl.Valor AS FLOAT) ELSE "
                    + " ROUND(CAST(mr.Subtotal AS float) * (pl.Valor / 100), 2) END) "
                    + " ELSE(CASE WHEN pr.TipoDescuento_descuentoDet = '$' THEN CAST(pr.Valor_descuentoDet AS FLOAT) "
                    + " ELSE ROUND(CAST(pr.Subtotal AS float) * (pr.Valor_descuentoDet / 100), 2) END) END) ELSE "
                    + " CAST(pr.Subtotal AS float) + CAST(pr.ValorIVA AS FLOAT) END + pr.ValorIVA AS ValorProntoPago "
                    + " FROM dbo.aca_AnioLectivo_Rubro AS ar WITH (nolock) "
                    + " INNER JOIN dbo.aca_AnioLectivo_Rubro_Periodo AS arp WITH (nolock) ON ar.IdEmpresa = arp.IdEmpresa AND ar.IdAnio = arp.IdAnio AND ar.IdRubro = arp.IdRubro "
                    + " INNER JOIN  dbo.aca_AnioLectivo_Periodo AS ap WITH (nolock) ON arp.IdEmpresa = ap.IdEmpresa and arp.IdAnio = ar.IdAnio AND arp.IdPeriodo = ap.IdPeriodo "
                    + " INNER JOIN  dbo.aca_PreMatricula_Rubro AS mr WITH (nolock) ON arp.IdEmpresa = mr.IdEmpresa AND arp.IdPeriodo = mr.IdPeriodo AND arp.IdRubro = mr.IdRubro "
                    + " inner join aca_Plantilla pl WITH (nolock) on pl.IdEmpresa = mr.IdEmpresa and pl.IdAnio = ar.IdAnio and pl.IdPlantilla = mr.IdPlantilla "
                    + " INNER JOIN aca_Plantilla_Rubro pr WITH (nolock) on pr.IdEmpresa = pl.IdEmpresa AND pr.IdAnio = pl.IdAnio AND pr.IdPlantilla = pl.IdPlantilla and pr.IdRubro = ar.IdRubro "
                    + " INNER JOIN  dbo.in_Producto AS p WITH (nolock) ON mr.IdEmpresa = p.IdEmpresa AND mr.IdProducto = p.IdProducto  "
                    + " WHERE mr.IdEmpresa = " + IdEmpresa.ToString() + " and mr.IdPreMatricula = " + IdPreMatricula.ToString()
                    + " ORDER BY mr.IdPeriodo ";
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandTimeout = 0;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new aca_PreMatricula_Rubro_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdPreMatricula = Convert.ToDecimal(reader["IdPreMatricula"]),
                            IdPeriodo = Convert.ToInt32(reader["IdPeriodo"]),
                            FechaDesde = Convert.ToDateTime(reader["FechaDesde"]),
                            IdRubro = Convert.ToInt32(reader["IdRubro"]),
                            NomRubro = reader["NomRubro"].ToString(),
                            IdProducto = Convert.ToDecimal(reader["IdProducto"]),
                            Subtotal = Convert.ToDecimal(reader["Subtotal"]),
                            IdCod_Impuesto_Iva = reader["IdCod_Impuesto_Iva"].ToString(),
                            ValorIVA = Convert.ToDecimal(reader["ValorIVA"]),
                            Porcentaje = Convert.ToDecimal(reader["Porcentaje"]),
                            Total = Convert.ToDecimal(reader["Total"]),
                            pr_descripcion = reader["pr_descripcion"].ToString(),
                            IdSucursal = string.IsNullOrEmpty(reader["IdSucursal"].ToString()) ? (int?)null : Convert.ToInt32(reader["IdSucursal"]),
                            IdBodega = string.IsNullOrEmpty(reader["IdBodega"].ToString()) ? (int?)null : Convert.ToInt32(reader["IdBodega"]),
                            IdCbteVta = string.IsNullOrEmpty(reader["IdCbteVta"].ToString()) ? (decimal?)null : Convert.ToInt32(reader["IdCbteVta"]),
                            FechaFacturacion = string.IsNullOrEmpty(reader["FechaFacturacion"].ToString()) ? (DateTime?)null : Convert.ToDateTime(reader["FechaFacturacion"]),
                            IdMecanismo = Convert.ToInt32(reader["IdMecanismo"]),
                            EnMatricula = Convert.ToBoolean(reader["EnMatricula"]),
                            IdAnio = Convert.ToInt32(reader["IdAnio"]),
                            IdPlantilla = Convert.ToInt32(reader["IdPlantilla"]),
                            IdSede = string.IsNullOrEmpty(reader["IdSede"].ToString()) ? (int?)null : Convert.ToInt32(reader["IdSede"]),
                            IdJornada = string.IsNullOrEmpty(reader["IdJornada"].ToString()) ? (int?)null : Convert.ToInt32(reader["IdJornada"]),
                            IdNivel = string.IsNullOrEmpty(reader["IdNivel"].ToString()) ? (int?)null : Convert.ToInt32(reader["IdNivel"]),
                            IdCurso = string.IsNullOrEmpty(reader["IdCurso"].ToString()) ? (int?)null : Convert.ToInt32(reader["IdCurso"]),
                            IdParalelo = string.IsNullOrEmpty(reader["IdParalelo"].ToString()) ? (int?)null : Convert.ToInt32(reader["IdParalelo"]),
                            AplicaProntoPago = string.IsNullOrEmpty(reader["AplicaProntoPago"].ToString()) ? false : Convert.ToBoolean(reader["AplicaProntoPago"]),
                            ValorProntoPago = string.IsNullOrEmpty(reader["ValorProntoPago"].ToString()) ? 0 : Convert.ToDecimal(reader["ValorProntoPago"]),
                            FechaProntoPago = string.IsNullOrEmpty(reader["FechaProntoPago"].ToString()) ? DateTime.Now.Date : Convert.ToDateTime(reader["FechaProntoPago"])
                        });
                        Lista.ForEach(q => q.IdString = q.IdEmpresa.ToString("0000000") + q.IdPlantilla.ToString("0000000") + q.IdPeriodo.ToString("0000000") + q.IdRubro.ToString("0000000"));
                    }
                    reader.Close();
                }

                Lista.ForEach(v => { v.Periodo = v.FechaDesde.Year.ToString("0000") + v.FechaDesde.Month.ToString("00"); });
                Lista.ForEach(q => q.IdString = q.IdEmpresa.ToString("0000")+ q.IdPreMatricula.ToString("000000") + q.IdPeriodo.ToString("0000") + q.IdRubro.ToString("000000"));
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public aca_PreMatricula_Rubro_Info getInfo(int IdEmpresa, decimal IdPreMatricula, int IdPeriodo, int IdRubro)
        {
            try
            {
                aca_PreMatricula_Rubro_Info info = new aca_PreMatricula_Rubro_Info();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("", connection);
                    command.CommandText = "SELECT mr.IdEmpresa, mr.IdPreMatricula, mr.IdPeriodo, ap.FechaDesde, mr.IdRubro, ar.NomRubro, mr.IdProducto, mr.Subtotal, mr.IdCod_Impuesto_Iva, mr.Porcentaje, mr.ValorIVA, mr.Total, p.pr_descripcion, mr.IdSucursal, mr.IdBodega, "
                    + " mr.IdCbteVta, mr.FechaFacturacion, mr.IdMecanismo, mr.EnPreMatricula, mr.IdAnio, mr.IdPlantilla, mr.IdSede, mr.IdNivel, mr.IdJornada, mr.IdCurso, mr.IdParalelo "
                    + " FROM     dbo.aca_AnioLectivo_Rubro AS ar WITH (nolock) INNER JOIN "
                    + " dbo.aca_AnioLectivo_Rubro_Periodo AS arp WITH (nolock) ON ar.IdEmpresa = arp.IdEmpresa AND ar.IdAnio = arp.IdAnio AND ar.IdRubro = arp.IdRubro INNER JOIN "
                    + " dbo.aca_AnioLectivo_Periodo AS ap WITH (nolock) ON arp.IdEmpresa = ap.IdEmpresa AND arp.IdPeriodo = ap.IdPeriodo INNER JOIN "
                    + " dbo.aca_PreMatricula_Rubro AS mr WITH (nolock) ON arp.IdEmpresa = mr.IdEmpresa AND arp.IdPeriodo = mr.IdPeriodo AND arp.IdRubro = mr.IdRubro INNER JOIN "
                    + " dbo.in_Producto AS p WITH (nolock) ON mr.IdEmpresa = p.IdEmpresa AND mr.IdProducto = p.IdProducto "
                    + " WHERE mr.IdEmpresa = " + IdEmpresa.ToString() + " and mr.IdPreMatricula = " + IdPreMatricula.ToString() + " and mr.IdPeriodo = " + IdPeriodo.ToString() + " and mr.IdRubro = " + IdRubro.ToString();
                    var ResultValue = command.ExecuteScalar();

                    if (ResultValue == null)
                        return null;

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        info = new aca_PreMatricula_Rubro_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdPreMatricula = Convert.ToDecimal(reader["IdPreMatricula"]),
                            IdPeriodo = Convert.ToInt32(reader["IdPeriodo"]),
                            FechaDesde = Convert.ToDateTime(reader["FechaDesde"]),
                            IdRubro = Convert.ToInt32(reader["IdRubro"]),
                            NomRubro = reader["NomRubro"].ToString(),
                            IdProducto = Convert.ToDecimal(reader["IdProducto"]),
                            Subtotal = Convert.ToDecimal(reader["Subtotal"]),
                            IdCod_Impuesto_Iva = reader["IdCod_Impuesto_Iva"].ToString(),
                            ValorIVA = Convert.ToDecimal(reader["ValorIVA"]),
                            Porcentaje = Convert.ToDecimal(reader["Porcentaje"]),
                            Total = Convert.ToDecimal(reader["Total"]),
                            pr_descripcion = reader["pr_descripcion"].ToString(),
                            IdSucursal = string.IsNullOrEmpty(reader["IdSucursal"].ToString()) ? (int?)null : Convert.ToInt32(reader["IdSucursal"]),
                            IdBodega = string.IsNullOrEmpty(reader["IdBodega"].ToString()) ? (int?)null : Convert.ToInt32(reader["IdBodega"]),
                            IdCbteVta = string.IsNullOrEmpty(reader["IdCbteVta"].ToString()) ? (decimal?)null : Convert.ToInt32(reader["IdCbteVta"]),
                            FechaFacturacion = string.IsNullOrEmpty(reader["FechaFacturacion"].ToString()) ? (DateTime?)null : Convert.ToDateTime(reader["FechaFacturacion"]),
                            IdMecanismo = Convert.ToDecimal(reader["IdMecanismo"]),
                            EnMatricula = Convert.ToBoolean(reader["EnMatricula"]),
                            IdAnio = Convert.ToInt32(reader["IdAnio"]),
                            IdPlantilla = Convert.ToInt32(reader["IdPlantilla"]),
                            IdSede = string.IsNullOrEmpty(reader["IdSede"].ToString()) ? (int?)null : Convert.ToInt32(reader["IdSede"]),
                            IdJornada = string.IsNullOrEmpty(reader["IdJornada"].ToString()) ? (int?)null : Convert.ToInt32(reader["IdJornada"]),
                            IdNivel = string.IsNullOrEmpty(reader["IdNivel"].ToString()) ? (int?)null : Convert.ToInt32(reader["IdNivel"]),
                            IdCurso = string.IsNullOrEmpty(reader["IdCurso"].ToString()) ? (int?)null : Convert.ToInt32(reader["IdCurso"]),
                            IdParalelo = string.IsNullOrEmpty(reader["IdParalelo"].ToString()) ? (int?)null : Convert.ToInt32(reader["IdParalelo"]),
                        };
                    }
                }
                /*
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var Entity = Context.vwaca_PreMatricula_Rubro.Where(q => q.IdEmpresa == IdEmpresa && q.IdPreMatricula == IdPreMatricula && q.IdPeriodo == IdPeriodo && q.IdRubro == IdRubro).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new aca_PreMatricula_Rubro_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdPreMatricula = Entity.IdPreMatricula,
                        IdPeriodo = Entity.IdPeriodo,
                        IdRubro = Entity.IdRubro,
                        IdProducto = Entity.IdProducto,
                        Subtotal = Entity.Subtotal,
                        IdCod_Impuesto_Iva = Entity.IdCod_Impuesto_Iva,
                        ValorIVA = Entity.ValorIVA,
                        Porcentaje = Entity.Porcentaje,
                        Total = Entity.Total,
                        NomRubro = Entity.NomRubro,
                        FechaDesde = Entity.FechaDesde,
                        FechaFacturacion = Entity.FechaFacturacion,
                        pr_descripcion = Entity.pr_descripcion,
                        IdMecanismo = Entity.IdMecanismo,
                        EnPreMatricula = Entity.EnPreMatricula,
                        IdAnio = Entity.IdAnio,
                        IdPlantilla = Entity.IdPlantilla,
                        IdSede = Entity.IdSede,
                        IdJornada = Entity.IdJornada,
                        IdNivel = Entity.IdNivel,
                        IdCurso = Entity.IdCurso,
                        IdParalelo = Entity.IdParalelo
                    };
                }
                */
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(aca_PreMatricula_Rubro_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_PreMatricula_Rubro Entity = Context.aca_PreMatricula_Rubro.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdPreMatricula == info.IdPreMatricula && q.IdPeriodo==info.IdPeriodo && q.IdRubro==info.IdRubro);
                    if (Entity == null)
                        return false;

                    Entity.IdSucursal = info.IdSucursal;
                    Entity.IdBodega = info.IdBodega;
                    Entity.IdCbteVta = info.IdCbteVta;
                    Entity.FechaFacturacion = info.FechaFacturacion;

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
