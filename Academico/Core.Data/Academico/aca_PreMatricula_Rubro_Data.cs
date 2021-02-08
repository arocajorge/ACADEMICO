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
                    + " FROM dbo.aca_AnioLectivo_Rubro_Periodo AS arp INNER JOIN "
                    + " dbo.aca_Plantilla_Rubro ON arp.IdEmpresa = dbo.aca_Plantilla_Rubro.IdEmpresa AND arp.IdAnio = dbo.aca_Plantilla_Rubro.IdAnio AND arp.IdRubro = dbo.aca_Plantilla_Rubro.IdRubro INNER JOIN "
                    + " dbo.aca_AnioLectivo_Periodo ON arp.IdEmpresa = dbo.aca_AnioLectivo_Periodo.IdEmpresa AND arp.IdPeriodo = dbo.aca_AnioLectivo_Periodo.IdPeriodo INNER JOIN "
                    + " dbo.aca_AnioLectivo_Rubro ON arp.IdEmpresa = dbo.aca_AnioLectivo_Rubro.IdEmpresa AND arp.IdAnio = dbo.aca_AnioLectivo_Rubro.IdAnio AND arp.IdRubro = dbo.aca_AnioLectivo_Rubro.IdRubro INNER JOIN "
                    + " dbo.in_Producto AS pro ON dbo.aca_Plantilla_Rubro.IdEmpresa = pro.IdEmpresa AND dbo.aca_Plantilla_Rubro.IdProducto = pro.IdProducto INNER JOIN "
                    + " dbo.aca_Plantilla ON dbo.aca_Plantilla_Rubro.IdEmpresa = dbo.aca_Plantilla.IdEmpresa AND dbo.aca_Plantilla_Rubro.IdAnio = dbo.aca_Plantilla.IdAnio AND dbo.aca_Plantilla_Rubro.IdPlantilla = dbo.aca_Plantilla.IdPlantilla "
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
                /*
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var lst = Context.vwaca_Plantilla_Rubro_PreMatricula.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdPlantilla == IdPlantilla).OrderBy(q => q.IdPeriodo).ToList();
                    foreach (var q in lst)
                    {
                        Lista.Add(new aca_PreMatricula_Rubro_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdAnio = q.IdAnio,
                            IdPlantilla = q.IdPlantilla,
                            IdPeriodo = q.IdPeriodo,
                            IdRubro = q.IdRubro,
                            IdProducto = q.IdProducto,
                            Subtotal = q.Subtotal,
                            IdCod_Impuesto_Iva = q.IdCod_Impuesto_Iva,
                            ValorIVA = q.ValorIVA,
                            Porcentaje = q.Porcentaje,
                            Total = q.Total,
                            NomRubro = q.NomRubro,
                            FechaDesde = q.FechaDesde,
                            pr_descripcion = q.pr_descripcion,
                            AplicaProntoPago = q.AplicaProntoPago,
                            ValorProntoPago = Convert.ToDecimal(q.ValorProntoPago ?? 0),
                            FechaProntoPago = q.FechaProntoPago ?? DateTime.Now.Date
                        });
                    }    
                }
                */
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
                    string query = "SELECT mr.IdEmpresa, mr.IdPreMatricula, mr.IdPeriodo, ap.FechaDesde, mr.IdRubro, ar.NomRubro, mr.IdProducto, mr.Subtotal, mr.IdCod_Impuesto_Iva, mr.Porcentaje, mr.ValorIVA, mr.Total, p.pr_descripcion, mr.IdSucursal, mr.IdBodega, "
                    + " mr.IdCbteVta, mr.FechaFacturacion, mr.IdMecanismo, mr.EnPreMatricula, mr.IdAnio, mr.IdPlantilla, mr.IdSede, mr.IdNivel, mr.IdJornada, mr.IdCurso, mr.IdParalelo "
                    + " FROM     dbo.aca_AnioLectivo_Rubro AS ar INNER JOIN "
                    + " dbo.aca_AnioLectivo_Rubro_Periodo AS arp ON ar.IdEmpresa = arp.IdEmpresa AND ar.IdAnio = arp.IdAnio AND ar.IdRubro = arp.IdRubro INNER JOIN "
                    + " dbo.aca_AnioLectivo_Periodo AS ap ON arp.IdEmpresa = ap.IdEmpresa AND arp.IdPeriodo = ap.IdPeriodo INNER JOIN "
                    + " dbo.aca_PreMatricula_Rubro AS mr ON arp.IdEmpresa = mr.IdEmpresa AND arp.IdPeriodo = mr.IdPeriodo AND arp.IdRubro = mr.IdRubro INNER JOIN "
                    + " dbo.in_Producto AS p ON mr.IdEmpresa = p.IdEmpresa AND mr.IdProducto = p.IdProducto "
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
                        });
                    }
                    reader.Close();
                }
                /*
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    Lista = Context.vwaca_PreMatricula_Rubro.Where(q => q.IdEmpresa == IdEmpresa && q.IdPreMatricula == IdPreMatricula).OrderBy(q=>q.IdPeriodo).Select(q => new aca_PreMatricula_Rubro_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdPreMatricula = q.IdPreMatricula,
                        IdPeriodo = q.IdPeriodo,
                        IdRubro = q.IdRubro,
                        IdProducto = q.IdProducto,
                        Subtotal = q.Subtotal,
                        IdCod_Impuesto_Iva = q.IdCod_Impuesto_Iva,
                        ValorIVA = q.ValorIVA,
                        Porcentaje = q.Porcentaje,
                        Total = q.Total,
                        NomRubro = q.NomRubro,
                        FechaDesde = q.FechaDesde,
                        FechaFacturacion = q.FechaFacturacion,
                        pr_descripcion = q.pr_descripcion,
                        IdMecanismo = q.IdMecanismo,
                        EnPreMatricula = q.EnPreMatricula,
                        IdAnio = q.IdAnio,
                        IdPlantilla = q.IdPlantilla,
                        IdCbteVta = q.IdCbteVta,
                        IdBodega = q.IdBodega,
                        IdSucursal = q.IdSucursal,
                        IdSede = q.IdSede,
                        IdJornada = q.IdJornada,
                        IdNivel = q.IdNivel,
                        IdCurso=q.IdCurso,
                        IdParalelo = q.IdParalelo
                    }).ToList();
                }
                */
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
                    + " FROM     dbo.aca_AnioLectivo_Rubro AS ar INNER JOIN "
                    + " dbo.aca_AnioLectivo_Rubro_Periodo AS arp ON ar.IdEmpresa = arp.IdEmpresa AND ar.IdAnio = arp.IdAnio AND ar.IdRubro = arp.IdRubro INNER JOIN "
                    + " dbo.aca_AnioLectivo_Periodo AS ap ON arp.IdEmpresa = ap.IdEmpresa AND arp.IdPeriodo = ap.IdPeriodo INNER JOIN "
                    + " dbo.aca_PreMatricula_Rubro AS mr ON arp.IdEmpresa = mr.IdEmpresa AND arp.IdPeriodo = mr.IdPeriodo AND arp.IdRubro = mr.IdRubro INNER JOIN "
                    + " dbo.in_Producto AS p ON mr.IdEmpresa = p.IdEmpresa AND mr.IdProducto = p.IdProducto "
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

        public List<aca_PreMatricula_Rubro_Info> getList_FactMasiva(int IdEmpresa, int IdAnio, int IdPeriodo, int IdSede, int IdJornada, int IdNivel, int IdCurso, int IdParalelo)
        {
            try
            {
                List<aca_PreMatricula_Rubro_Info> Lista = new List<aca_PreMatricula_Rubro_Info>();

                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query_PorFacturar = "SELECT a.IdEmpresa, a.IdPreMatricula, a.IdAnio, a.IdPeriodo, a.IdPlantilla, a.IdRubro, a.IdProducto, a.Subtotal, a.IdCod_Impuesto_Iva, a.Porcentaje, a.ValorIVA, a.Total, CASE WHEN j.AplicaProntoPago = 1 AND d .FechaProntoPago > CAST(getdate() "
                    + " AS date) THEN CAST(k.Subtotal AS float) -(CASE WHEN l.AplicaParaTodo = 1 THEN(CASE WHEN l.TipoDescuento = '$' THEN CAST(l.Valor AS FLOAT) ELSE ROUND(CAST(k.Subtotal AS float) * (l.Valor / 100), 2) END) "
                    + " ELSE(CASE WHEN k.TipoDescuento_descuentoDet = '$' THEN CAST(k.Valor_descuentoDet AS FLOAT) ELSE ROUND(CAST(k.Subtotal AS float) * (k.Valor_descuentoDet / 100), 2) END) END) ELSE CAST(k.Subtotal AS float) "
                    + " + CAST(k.ValorIVA AS FLOAT) END + k.ValorIVA AS ValorProntoPago, c.NomRubro + CASE WHEN c.NumeroCuotas > 1 THEN + ' ' + CAST(e.Secuencia AS varchar) + '/' + CAST(c.NumeroCuotas AS varchar) "
                    + " ELSE '' END + ' ' + f.smes + ' ' + CAST(YEAR(d.FechaDesde) AS varchar) AS Observacion, h.IdAlumno, d.FechaDesde, d.FechaProntoPago, b.IdTerminoPago, i.IdCliente, m.Codigo, n.pe_nombreCompleto AS Alumno, h.IdEmpresa_rol, "
                    + " h.IdEmpleado, h.IdSede, h.IdNivel, h.IdJornada, h.IdCurso, h.IdParalelo "
                    + " FROM     dbo.aca_PreMatricula_Rubro AS a INNER JOIN "
                    + " dbo.aca_MecanismoDePago AS b ON a.IdEmpresa = b.IdEmpresa AND a.IdMecanismo = b.IdMecanismo LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Rubro AS c ON a.IdAnio = c.IdAnio AND a.IdEmpresa = c.IdEmpresa AND a.IdRubro = c.IdRubro AND a.IdRubro = c.IdRubro LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Periodo AS d ON a.IdEmpresa = d.IdEmpresa AND a.IdAnio = d.IdAnio AND a.IdPeriodo = d.IdPeriodo LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Rubro_Periodo AS e ON a.IdEmpresa = e.IdEmpresa AND a.IdRubro = e.IdRubro AND a.IdPeriodo = e.IdPeriodo AND a.IdAnio = e.IdAnio LEFT OUTER JOIN "
                    + " dbo.tb_mes AS f ON d.IdMes = f.idMes LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo AS g ON a.IdEmpresa = g.IdEmpresa AND a.IdAnio = g.IdAnio INNER JOIN "
                    + " dbo.aca_PreMatricula AS h ON a.IdEmpresa = h.IdEmpresa AND a.IdPreMatricula = h.IdPreMatricula LEFT OUTER JOIN "
                        + " (SELECT IdEmpresa, IdPersona, MAX(IdCliente) AS IdCliente "
                        + " FROM      dbo.fa_cliente "
                        + " GROUP BY IdEmpresa, IdPersona) AS i ON h.IdPersonaF = i.IdPersona AND h.IdEmpresa = i.IdEmpresa LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Rubro AS j ON j.IdEmpresa = a.IdEmpresa AND j.IdAnio = a.IdAnio AND j.IdRubro = a.IdRubro LEFT OUTER JOIN "
                    + " dbo.aca_Plantilla_Rubro AS k ON a.IdEmpresa = k.IdEmpresa AND a.IdAnio = k.IdAnio AND a.IdPlantilla = k.IdPlantilla AND a.IdRubro = k.IdRubro LEFT OUTER JOIN "
                    + " dbo.aca_Plantilla AS l ON l.IdEmpresa = k.IdEmpresa AND l.IdAnio = k.IdAnio AND l.IdPlantilla = k.IdPlantilla LEFT OUTER JOIN "
                    + " dbo.aca_Alumno AS m ON h.IdEmpresa = m.IdEmpresa AND h.IdAlumno = m.IdAlumno LEFT OUTER JOIN "
                    + " dbo.tb_persona AS n ON m.IdPersona = n.IdPersona "
                    + " WHERE(a.FechaFacturacion IS NULL) AND(NOT EXISTS "
                    + " (SELECT IdEmpresa "
                        + " FROM      dbo.aca_AlumnoRetiro AS ret "
                        + " WHERE(IdEmpresa = a.IdEmpresa) AND(IdPreMatricula = a.IdPreMatricula) AND(Estado = 1))) "
                        + " and a.IdEmpresa= " + IdEmpresa.ToString()
                        + " and a.IdPeriodo= " + IdPeriodo.ToString();

                    if (IdAnio > 0)
                        query_PorFacturar += " and a.IdAnio= " + IdAnio.ToString();
                    if (IdSede > 0)
                        query_PorFacturar += " and a.IdSede= " + IdSede.ToString();
                    if (IdNivel > 0)
                        query_PorFacturar += " and a.IdNivel= " + IdNivel.ToString();
                    if (IdJornada > 0)
                        query_PorFacturar += " and a.IdJornada= " + IdJornada.ToString();
                    if (IdCurso > 0)
                        query_PorFacturar += " and a.IdCurso= " + IdCurso.ToString();
                    if (IdParalelo > 0)
                        query_PorFacturar += " and a.IdParalelo= " + IdParalelo.ToString();
                    
                    #endregion

                    SqlCommand command_PorFacturar = new SqlCommand(query_PorFacturar, connection);
                    command_PorFacturar.CommandTimeout = 0;
                    SqlDataReader reader_PorFacturar = command_PorFacturar.ExecuteReader();
                    while (reader_PorFacturar.Read())
                    {
                        Lista.Add(new aca_PreMatricula_Rubro_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader_PorFacturar["IdEmpresa"]),
                            IdPreMatricula = Convert.ToDecimal(reader_PorFacturar["IdPreMatricula"]),
                            IdAnio = Convert.ToInt32(reader_PorFacturar["IdAnio"]),
                            IdPeriodo = Convert.ToInt32(reader_PorFacturar["IdPeriodo"]),
                            IdPlantilla = Convert.ToInt32(reader_PorFacturar["IdPlantilla"]),
                            IdRubro = Convert.ToInt32(reader_PorFacturar["IdRubro"]),
                            IdProducto = Convert.ToDecimal(reader_PorFacturar["IdProducto"]),
                            Subtotal = Convert.ToDecimal(reader_PorFacturar["Subtotal"]),
                            ValorIVA = Convert.ToDecimal(reader_PorFacturar["ValorIVA"]),
                            Porcentaje = Convert.ToDecimal(reader_PorFacturar["Porcentaje"]),
                            IdAlumno = Convert.ToDecimal(reader_PorFacturar["IdAlumno"]),
                            pe_nombreCompleto = string.IsNullOrEmpty(reader_PorFacturar["Alumno"].ToString()) ? null : reader_PorFacturar["Alumno"].ToString(),
                            Codigo = string.IsNullOrEmpty(reader_PorFacturar["Codigo"].ToString()) ? null : reader_PorFacturar["Codigo"].ToString(),
                            Total = Convert.ToDecimal(reader_PorFacturar["Total"]),
                            IdCod_Impuesto_Iva = string.IsNullOrEmpty(reader_PorFacturar["IdCod_Impuesto_Iva"].ToString()) ? null : reader_PorFacturar["IdCod_Impuesto_Iva"].ToString(),
                            ValorProntoPago = Convert.ToDecimal(reader_PorFacturar["ValorProntoPago"]),
                            vt_Observacion = string.IsNullOrEmpty(reader_PorFacturar["Observacion"].ToString()) ? null : reader_PorFacturar["Observacion"].ToString(),
                            Procesado = false,
                            FechaDesde = Convert.ToDateTime(reader_PorFacturar["FechaDesde"]),
                            FechaProntoPago = Convert.ToDateTime(reader_PorFacturar["FechaProntoPago"]),
                            IdTerminoPago = string.IsNullOrEmpty(reader_PorFacturar["IdTerminoPago"].ToString()) ? null : reader_PorFacturar["IdTerminoPago"].ToString(),
                            IdCliente = Convert.ToDecimal(reader_PorFacturar["IdCliente"]),
                            IdEmpresa_rol = string.IsNullOrEmpty(reader_PorFacturar["IdEmpresa_rol"].ToString()) ? (int?)null : Convert.ToInt32(reader_PorFacturar["IdEmpresa_rol"]),
                            IdEmpleado = string.IsNullOrEmpty(reader_PorFacturar["IdEmpleado"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader_PorFacturar["IdEmpleado"]),
                        });
                    }
                    reader_PorFacturar.Close();


                    #region Query
                    string query_FacturaMasiva = "SELECT a.IdEmpresa, a.IdAnio, a.IdPeriodo, a.IdSucursal, a.IdBodega, a.IdCbteVta, b.IdAlumno, d.pe_nombreCompleto, a.Total, f.Correo, e.vt_autorizacion, b.IdSede, b.IdNivel, b.IdJornada, b.IdCurso, b.IdParalelo "
                    + " FROM dbo.aca_PreMatricula_Rubro AS a INNER JOIN "
                    + " dbo.aca_PreMatricula AS b ON a.IdEmpresa = b.IdEmpresa AND a.IdPreMatricula = b.IdPreMatricula INNER JOIN "
                    + " dbo.aca_Alumno AS c ON b.IdEmpresa = c.IdEmpresa AND b.IdAlumno = c.IdAlumno INNER JOIN "
                    + " dbo.tb_persona AS d ON c.IdPersona = d.IdPersona INNER JOIN "
                    + " dbo.fa_factura AS e ON e.IdEmpresa = a.IdEmpresa AND e.IdSucursal = a.IdSucursal AND e.IdBodega = a.IdBodega AND e.IdCbteVta = a.IdCbteVta INNER JOIN "
                    + " dbo.fa_cliente_contactos AS f ON e.IdEmpresa = f.IdEmpresa AND e.IdCliente = f.IdCliente "
                    + " WHERE(e.AplicacionMasiva = 1) AND(NOT EXISTS "
                    + " (SELECT IdEmpresa "
                    + " FROM      dbo.aca_AlumnoRetiro AS z "
                    + " WHERE(a.IdEmpresa = IdEmpresa) AND(a.IdPreMatricula = IdPreMatricula))) "
                    + " and a.IdEmpresa= " + IdEmpresa.ToString()
                    + " and a.IdPeriodo= " + IdPeriodo.ToString();

                    if (IdAnio > 0)
                        query_PorFacturar += " and a.IdAnio= " + IdAnio.ToString();
                    if (IdSede > 0)
                        query_PorFacturar += " and a.IdSede= " + IdSede.ToString();
                    if (IdNivel > 0)
                        query_PorFacturar += " and a.IdNivel= " + IdNivel.ToString();
                    if (IdJornada > 0)
                        query_PorFacturar += " and a.IdJornada= " + IdJornada.ToString();
                    if (IdCurso > 0)
                        query_PorFacturar += " and a.IdCurso= " + IdCurso.ToString();
                    if (IdParalelo > 0)
                        query_PorFacturar += " and a.IdParalelo= " + IdParalelo.ToString();
                    #endregion
                    SqlCommand command_FacturaMasiva = new SqlCommand(query_FacturaMasiva, connection);
                    command_FacturaMasiva.CommandTimeout = 0;
                    SqlDataReader reader_FacturaMasiva = command_FacturaMasiva.ExecuteReader();
                    while (reader_FacturaMasiva.Read())
                    {
                        Lista.Add(new aca_PreMatricula_Rubro_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader_FacturaMasiva["IdEmpresa"]),
                            IdAnio = Convert.ToInt32(reader_FacturaMasiva["IdAnio"]),
                            IdPeriodo = Convert.ToInt32(reader_FacturaMasiva["IdPeriodo"]),
                            IdAlumno = Convert.ToDecimal(reader_FacturaMasiva["IdAlumno"]),
                            pe_nombreCompleto = string.IsNullOrEmpty(reader_FacturaMasiva["pe_nombreCompleto"].ToString()) ? null : reader_FacturaMasiva["pe_nombreCompleto"].ToString(),
                            Total = Convert.ToDecimal(reader_FacturaMasiva["Total"]),
                            IdSucursal = string.IsNullOrEmpty(reader_FacturaMasiva["IdSucursal"].ToString()) ? (int?)null : Convert.ToInt32(reader_FacturaMasiva["IdSucursal"]),
                            IdBodega = string.IsNullOrEmpty(reader_FacturaMasiva["IdBodega"].ToString()) ? (int?)null : Convert.ToInt32(reader_FacturaMasiva["IdBodega"]),
                            IdCbteVta = string.IsNullOrEmpty(reader_FacturaMasiva["IdCbteVta"].ToString()) ? (int?)null : Convert.ToInt32(reader_FacturaMasiva["IdCbteVta"]),
                            Correo = string.IsNullOrEmpty(reader_FacturaMasiva["Correo"].ToString()) ? null : reader_FacturaMasiva["Correo"].ToString(),
                            vt_autorizacion = string.IsNullOrEmpty(reader_FacturaMasiva["vt_autorizacion"].ToString()) ? null : reader_FacturaMasiva["vt_autorizacion"].ToString(),
                            Procesado = true
                        });
                    }
                    reader_FacturaMasiva.Close();

                }
                /*
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var lst = Context.vwaca_PreMatricula_Rubro_PorFacturarMasiva.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdPeriodo == IdPeriodo).ToList();

                    foreach (var q in lst)
                    {
                        var info = new aca_PreMatricula_Rubro_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdPreMatricula = q.IdPreMatricula,
                            IdAnio = q.IdAnio,
                            IdPeriodo = q.IdPeriodo,
                            IdPlantilla = q.IdPlantilla,
                            IdRubro = q.IdRubro,
                            IdProducto = q.IdProducto,
                            Subtotal = q.Subtotal,
                            IdCod_Impuesto_Iva = q.IdCod_Impuesto_Iva,
                            ValorIVA = q.ValorIVA,
                            Porcentaje = q.Porcentaje,
                            Total = q.Total,
                            ValorProntoPago = Convert.ToDecimal(q.ValorProntoPago),
                            vt_Observacion = q.Observacion,
                            IdAlumno = q.IdAlumno,
                            FechaDesde = Convert.ToDateTime(q.FechaDesde),
                            FechaProntoPago = Convert.ToDateTime(q.FechaProntoPago),
                            IdTerminoPago = q.IdTerminoPago,
                            IdCliente = q.IdCliente ?? 0,
                            Codigo = q.Codigo,
                            pe_nombreCompleto = q.Alumno,
                            Procesado = false,
                            IdEmpresa_rol = q.IdEmpresa_rol,
                            IdEmpleado = q.IdEmpleado,
                        };
                        Lista.Add(info);
                    }

                    var lstProcesado = Context.vwaca_PreMatricula_Rubro_FacturaMasiva.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdPeriodo == IdPeriodo).ToList();
                    foreach (var item in lstProcesado)
                    {
                        Lista.Add(new aca_PreMatricula_Rubro_Info
                        {
                            IdEmpresa = item.IdEmpresa,
                            IdAnio = item.IdAnio,
                            IdPeriodo = item.IdPeriodo,
                            IdAlumno = item.IdAlumno,
                            pe_nombreCompleto = item.pe_nombreCompleto,
                            Total = item.Total,
                            IdSucursal = item.IdSucursal,
                            IdBodega = item.IdBodega,
                            IdCbteVta = item.IdCbteVta,
                            Correo = item.Correo,
                            vt_autorizacion = item.vt_autorizacion,
                            Procesado = true
                        });
                    }
                }
                */
                Lista.ForEach(q => q.IdString = q.IdEmpresa.ToString("0000") + q.IdPreMatricula.ToString("000000") + q.IdPeriodo.ToString("0000") + q.IdRubro.ToString("000000"));
                return Lista;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
