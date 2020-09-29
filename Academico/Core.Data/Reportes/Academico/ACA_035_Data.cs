﻿using Core.Data.Base;
using Core.Info.Helps;
using Core.Info.Reportes.Academico;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Reportes.Academico
{
    public class ACA_035_Data
    {
        public List<ACA_035_Info> get_list(int IdEmpresa, int IdAnio, int IdSede, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, decimal IdAlumno)
        {
            try
            {
                int IdSedeIni = IdSede;
                int IdSedeFin = IdSede == 0 ? 9999999 : IdSede;

                int IdJornadaIni = IdJornada;
                int IdJornadaFin = IdJornada == 0 ? 9999999 : IdJornada;

                int IdNivelIni = IdNivel;
                int IdNivelFin = IdNivel == 0 ? 9999999 : IdNivel;

                int IdCursoIni = IdCurso;
                int IdCursoFin = IdCurso == 0 ? 9999999 : IdCurso;

                int IdParaleloIni = IdParalelo;
                int IdParaleloFin = IdParalelo == 0 ? 9999999 : IdParalelo;

                List<ACA_035_Info> Lista = new List<ACA_035_Info>();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = "SELECT a.IdEmpresa, a.Codigo, a.IdAlumno, e.IdMatricula, b.pe_nombreCompleto AS NombreAlumno, b.pe_cedulaRuc, e.IdAnio, e.IdSede, e.IdNivel, e.IdJornada, e.IdCurso, e.IdParalelo, f.Descripcion, sn.NomSede, sn.NomNivel, nj.NomJornada, "
                    + " jc.NomCurso, cp.NomParalelo, ISNULL(g.Saldo, 0) AS Saldo, ISNULL(g.SaldoProntoPago, 0) AS SaldoProntoPago, ISNULL(g.CantDeudas, 0) AS CantDeudas, nj.OrdenJornada, sn.OrdenNivel, jc.OrdenCurso, cp.OrdenParalelo "
                    + " FROM dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn RIGHT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Jornada_Curso AS jc ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Curso_Paralelo AS cp RIGHT OUTER JOIN "
                    + " (SELECT IdEmpresa, IdAlumno, SUM(Saldo)AS Saldo, SUM(ValorProntoPago - TotalxCobrado) AS SaldoProntoPago, COUNT(*) AS CantDeudas "
                    + " FROM      dbo.vwcxc_cartera_x_cobrar AS xy "
                    + " GROUP BY IdEmpresa, IdAlumno) AS g RIGHT OUTER JOIN "
                    + " dbo.aca_AnioLectivo AS f INNER JOIN "
                        + " dbo.aca_Matricula AS e ON f.IdEmpresa = e.IdEmpresa AND f.IdAnio = e.IdAnio INNER  JOIN"
                        + " dbo.aca_Alumno AS a INNER JOIN "
                        + " dbo.tb_persona AS b ON a.IdPersona = b.IdPersona ON e.IdEmpresa = a.IdEmpresa AND e.IdAlumno = a.IdAlumno ON g.IdEmpresa = a.IdEmpresa AND g.IdAlumno = a.IdAlumno ON cp.IdEmpresa = e.IdEmpresa AND "
                        + " cp.IdAnio = e.IdAnio AND cp.IdSede = e.IdSede AND cp.IdNivel = e.IdNivel AND cp.IdJornada = e.IdJornada AND cp.IdCurso = e.IdCurso AND cp.IdParalelo = e.IdParalelo ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND "
                        + " jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso "
                    + " WHERE "
                    + " e.IdEmpresa = "+IdEmpresa.ToString()
                    + " and e.IdSede between " + IdSedeIni.ToString() + " and " + IdSedeFin.ToString() 
                    + " and e.IdNivel between " + IdNivelIni.ToString() + " and " + IdNivelFin.ToString() 
                    + " and e.IdJornada between " + IdJornadaIni.ToString() + " and " + IdJornadaFin.ToString() 
                    + " and e.IdCurso between " + IdCursoIni.ToString() + " and " + IdCursoFin.ToString() 
                    + " and e.IdParalelo between " + IdParaleloIni.ToString() + " and " + IdParaleloFin.ToString()
                    +" and(f.Estado = 1) AND(a.Estado = 1) AND(NOT EXISTS "
                        + " (SELECT IdEmpresa "
                            + " FROM      dbo.aca_AlumnoRetiro AS xx "
                            + " WHERE(e.IdEmpresa = IdEmpresa) AND(e.IdMatricula = IdMatricula) AND(Estado = 1)))";
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new ACA_035_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            Codigo = reader["Codigo"].ToString(),
                            IdAlumno = Convert.ToDecimal(reader["IdAlumno"]),
                            IdMatricula = Convert.ToDecimal(reader["IdMatricula"]),
                            NombreAlumno = reader["NombreAlumno"].ToString(),
                            pe_cedulaRuc = reader["pe_cedulaRuc"].ToString(),
                            IdAnio = Convert.ToInt32(reader["IdAnio"]),
                            IdSede = Convert.ToInt32(reader["IdSede"]),
                            IdNivel = Convert.ToInt32(reader["IdNivel"]),
                            IdJornada = Convert.ToInt32(reader["IdJornada"]),
                            IdCurso = Convert.ToInt32(reader["IdCurso"]),
                            IdParalelo = Convert.ToInt32(reader["IdParalelo"]),
                            Descripcion = reader["Descripcion"].ToString(),
                            NomSede = reader["NomSede"].ToString(),
                            NomNivel = reader["NomNivel"].ToString(),
                            NomJornada = reader["NomJornada"].ToString(),
                            NomCurso = reader["NomCurso"].ToString(),
                            NomParalelo = reader["NomParalelo"].ToString(),
                            Saldo = Convert.ToDouble(reader["Saldo"]),
                            SaldoProntoPago = Convert.ToDouble(reader["SaldoProntoPago"]),
                            CantDeudas = Convert.ToInt32(reader["CantDeudas"]),
                            OrdenNivel = Convert.ToInt32(reader["OrdenNivel"]),
                            OrdenJornada = Convert.ToInt32(reader["OrdenJornada"]),
                            OrdenCurso = Convert.ToInt32(reader["OrdenCurso"]),
                            OrdenParalelo = Convert.ToInt32(reader["OrdenParalelo"]),
                            FechaActual = DateTime.Now.ToString("d' de 'MMMM' de 'yyyy"),
                            DescripcionAdeudar = (Convert.ToInt32(reader["CantDeudas"])==0 ? "no adeuda valor alguno ": "tiene valores pendientes de pago")
                        });
                    }
                    reader.Close();
                }
                
                return Lista;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
