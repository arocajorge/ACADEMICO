using Core.Data.Base;
using Core.Info.Reportes.Academico;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Reportes.Academico
{
    public class ACA_003_Data
    {
        public List<ACA_003_Info> get_list(int IdEmpresa, decimal IdAlumno)
        {
            try
            {
                List<ACA_003_Info> Lista;
                using (EntitiesReportes Context = new EntitiesReportes())
                {
                    Lista = Context.VWACA_003.Where(q => q.IdEmpresa == IdEmpresa && q.IdAlumno == IdAlumno).Select(q => new ACA_003_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdMatricula = q.IdMatricula,
                        CedulaAlumno = q.CedulaAlumno,
                        CedulaRepresentante = q.CedulaRepresentante,
                        CedulaSeFactura = q.CedulaSeFactura,
                        DescripcionActual = q.DescripcionActual,
                        DescripcionAnterior = q.DescripcionAnterior,
                        DescripcionPensiones = q.DescripcionPensiones,
                        Direccion= q.Direccion,
                        IdAlumno = q.IdAlumno,
                        NomAlumno = q.NomAlumno,
                        NomCurso = q.NomCurso,
                        NomRepresentante = q.NomRepresentante,
                        NomSeFactura =q.NomSeFactura

                    }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ACA_003_Info getInfo(int IdEmpresa, int IdAnio, decimal IdAlumno)
        {
            try
            {
                ACA_003_Info info = new ACA_003_Info();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("", connection);
                    command.CommandText = "SELECT m.IdEmpresa, m.IdMatricula, m.IdAlumno, Representante.pe_nombreCompleto AS NomRepresentante, Representante.pe_cedulaRuc AS CedulaRepresentante, Factura.pe_nombreCompleto AS NomSeFactura, "
                    + " Factura.pe_cedulaRuc AS CedulaSeFactura, p.pe_nombreCompleto AS NomAlumno, p.pe_nombre AS CedulaAlumno, dbo.aca_AnioLectivo_Jornada_Curso.NomCurso, AnioActual.Descripcion AS DescripcionActual, "
                    + " AnioAnterior.Descripcion AS DescripcionAnterior, dbo.aca_Familia.Direccion, MesInicio.smes + ' de ' + CAST(YEAR(AnioActual.FechaDesde) AS varchar) + ' a ' + MesFin.smes + ' de ' + CAST(YEAR(AnioActual.FechaHasta) AS varchar) "
                    + " AS DescripcionPensiones, FFactura.Correo AS CorreoSeFactura, FRepresentante.Correo AS CorreoRepresentante, Representante.pe_direccion AS DireccionRepresentante, dbo.tb_pais.Nacionalidad AS NacionalidadRepresentante, "
                    + " FRepresentante.Sector AS SectorRepresentante, FRepresentante.Celular AS CelularRepresentante, m.IdAnio "
                    + " FROM     dbo.aca_Familia AS FRepresentante RIGHT OUTER JOIN "
                    + " dbo.tb_pais ON FRepresentante.IdPais = dbo.tb_pais.IdPais RIGHT OUTER JOIN "
                    + " dbo.tb_persona AS p INNER JOIN "
                    + " dbo.aca_Matricula AS m INNER JOIN "
                    + " dbo.aca_Alumno AS a ON m.IdEmpresa = a.IdEmpresa AND m.IdAlumno = a.IdAlumno INNER JOIN "
                    + " dbo.aca_AnioLectivo AS AnioActual ON m.IdEmpresa = AnioActual.IdEmpresa AND m.IdAnio = AnioActual.IdAnio ON p.IdPersona = a.IdPersona INNER JOIN "
                    + " dbo.tb_persona AS Factura ON m.IdPersonaF = Factura.IdPersona INNER JOIN "
                    + " dbo.tb_persona AS Representante ON m.IdPersonaR = Representante.IdPersona INNER JOIN "
                    + " dbo.aca_AnioLectivo_Jornada_Curso ON m.IdEmpresa = dbo.aca_AnioLectivo_Jornada_Curso.IdEmpresa AND m.IdAnio = dbo.aca_AnioLectivo_Jornada_Curso.IdAnio AND m.IdSede = dbo.aca_AnioLectivo_Jornada_Curso.IdSede AND "
                    + " m.IdNivel = dbo.aca_AnioLectivo_Jornada_Curso.IdNivel AND m.IdJornada = dbo.aca_AnioLectivo_Jornada_Curso.IdJornada AND m.IdCurso = dbo.aca_AnioLectivo_Jornada_Curso.IdCurso INNER JOIN "
                    + " dbo.aca_Familia ON a.IdEmpresa = dbo.aca_Familia.IdEmpresa AND a.IdAlumno = dbo.aca_Familia.IdAlumno AND Representante.IdPersona = dbo.aca_Familia.IdPersona LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo AS AnioAnterior ON AnioActual.IdEmpresa = AnioAnterior.IdEmpresa AND AnioActual.IdAnioLectivoAnterior = AnioAnterior.IdAnio INNER JOIN "
                    + " dbo.tb_mes AS MesInicio ON MONTH(AnioActual.FechaDesde) = MesInicio.idMes INNER JOIN "
                    + " dbo.tb_mes AS MesFin ON MONTH(AnioActual.FechaHasta) = MesFin.idMes ON FRepresentante.IdPersona = m.IdPersonaR AND FRepresentante.IdEmpresa = m.IdEmpresa AND FRepresentante.IdAlumno = m.IdAlumno LEFT OUTER JOIN "
                    + " dbo.aca_Familia AS FFactura ON m.IdEmpresa = FFactura.IdEmpresa AND m.IdAlumno = FFactura.IdAlumno AND m.IdPersonaR = FFactura.IdPersona "
                    + " WHERE a.IdEmpresa = " + IdEmpresa.ToString() + "and a.IdAlumno = " + IdAlumno.ToString() + "and m.IdAnio = " + IdAnio.ToString();
                    var ResultValue = command.ExecuteScalar();

                    if (ResultValue == null)
                        return null;

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        info = new ACA_003_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdMatricula = Convert.ToInt32(reader["IdMatricula"]),
                            CedulaAlumno = string.IsNullOrEmpty(reader["CedulaAlumno"].ToString()) ? null : reader["CedulaAlumno"].ToString(),
                            CedulaRepresentante = string.IsNullOrEmpty(reader["CedulaRepresentante"].ToString()) ? null : reader["CedulaRepresentante"].ToString(),
                            CedulaSeFactura = string.IsNullOrEmpty(reader["CedulaSeFactura"].ToString()) ? null : reader["CedulaSeFactura"].ToString(),
                            DescripcionActual = string.IsNullOrEmpty(reader["DescripcionActual"].ToString()) ? null : reader["DescripcionActual"].ToString(),
                            DescripcionAnterior = string.IsNullOrEmpty(reader["DescripcionAnterior"].ToString()) ? null : reader["DescripcionAnterior"].ToString(),
                            DescripcionPensiones = string.IsNullOrEmpty(reader["DescripcionPensiones"].ToString()) ? null : reader["DescripcionPensiones"].ToString(),
                            Direccion = string.IsNullOrEmpty(reader["Direccion"].ToString()) ? null : reader["Direccion"].ToString(),
                            IdAlumno = Convert.ToDecimal(reader["IdAlumno"]),
                            NomAlumno = string.IsNullOrEmpty(reader["NomAlumno"].ToString()) ? null : reader["NomAlumno"].ToString(),
                            NomCurso = string.IsNullOrEmpty(reader["NomCurso"].ToString()) ? null : reader["NomCurso"].ToString(),
                            NomRepresentante = string.IsNullOrEmpty(reader["NomRepresentante"].ToString()) ? null : reader["NomRepresentante"].ToString(),
                            NomSeFactura = string.IsNullOrEmpty(reader["NomSeFactura"].ToString()) ? null : reader["NomSeFactura"].ToString(),
                            DireccionRepresentante = string.IsNullOrEmpty(reader["DireccionRepresentante"].ToString()) ? null : reader["DireccionRepresentante"].ToString(),
                            NacionalidadRepresentante = string.IsNullOrEmpty(reader["NacionalidadRepresentante"].ToString()) ? null : reader["NacionalidadRepresentante"].ToString(),
                            SectorRepresentante = string.IsNullOrEmpty(reader["SectorRepresentante"].ToString()) ? null : reader["SectorRepresentante"].ToString(),
                            CorreoRepresentante = string.IsNullOrEmpty(reader["CorreoRepresentante"].ToString()) ? null : reader["CorreoRepresentante"].ToString(),
                            CelularRepresentante = string.IsNullOrEmpty(reader["CelularRepresentante"].ToString()) ? null : reader["CelularRepresentante"].ToString(),
                            CorreoSeFactura = string.IsNullOrEmpty(reader["CorreoSeFactura"].ToString()) ? null : reader["CorreoSeFactura"].ToString(),
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
        
        public ACA_003_Info get_info(int IdEmpresa, decimal IdAlumno)
        {
            try
            {
                ACA_003_Info Info = new ACA_003_Info(); ;
                using (EntitiesReportes Context = new EntitiesReportes())
                {
                    Info = Context.VWACA_003.Where(q => q.IdEmpresa == IdEmpresa && q.IdAlumno == IdAlumno).Select(q => new ACA_003_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdMatricula = q.IdMatricula,
                        CedulaAlumno = q.CedulaAlumno,
                        CedulaRepresentante = q.CedulaRepresentante,
                        CedulaSeFactura = q.CedulaSeFactura,
                        DescripcionActual = q.DescripcionActual,
                        DescripcionAnterior = q.DescripcionAnterior,
                        DescripcionPensiones = q.DescripcionPensiones,
                        Direccion = q.Direccion,
                        IdAlumno = q.IdAlumno,
                        NomAlumno = q.NomAlumno,
                        NomCurso = q.NomCurso,
                        NomRepresentante = q.NomRepresentante,
                        NomSeFactura = q.NomSeFactura,
                        DireccionRepresentante = q.DireccionRepresentante,
                        NacionalidadRepresentante = q.NacionalidadRepresentante,
                        SectorRepresentante = q.SectorRepresentante,
                        CorreoRepresentante = q.CorreoRepresentante,
                        CelularRepresentante= q.CelularRepresentante,
                        CorreoSeFactura = q.CorreoSeFactura

                    }).FirstOrDefault();
                }
                return Info;
            }
            catch (Exception)
            {

                throw;
            }
        }
        
    }
}
