﻿using Core.Data.Base;
using Core.Info.Reportes.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Core.Data.Reportes.CuentasPorCobrar
{
    public class CXC_004_Data
    {
        public List<CXC_004_Info> Getlist(int IdEmpresa, string IdUsuario, DateTime FechaCorte)
        {
            try
            {
                FechaCorte = FechaCorte.Date;

                List <CXC_004_Info> Lista = new List<CXC_004_Info>();

                using (EntitiesReportes db = new EntitiesReportes())
                {
                    var lst = db.SPCXC_004(IdEmpresa, IdUsuario, FechaCorte).ToList();

                    foreach (var item in lst)
                    {
                        Lista.Add(new CXC_004_Info
                        {
                            IdEmpresa = item.IdEmpresa,
                            IdAlumno = item.IdAlumno,
                            IdAnio = item.IdAnio,
                            IdUsuario = item.IdUsuario,
                            NomAnio = item.NomAnio,
                            CodigoAlumno = item.CodigoAlumno,
                            NombreAlumno = item.NombreAlumno,
                            IdJornada = item.IdJornada,
                            NombreJornada = item.NombreJornada,
                            SaldoDeudor = item.SaldoDeudor,
                            SaldoAcreedor = item.SaldoAcreedor,
                            SaldoFinal = item.SaldoFinal,

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

        public List<CXC_004_Info> Getlist(int IdEmpresa, string IdUsuario)
        {
            try
            {
                List<CXC_004_Info> Lista = new List<CXC_004_Info>();

                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandText = "select IdEmpresa,  IdAlumno, IdAnio, IdUsuario, NomAnio, CodigoAlumno, NombreAlumno, IdJornada, NombreJornada, SaldoDeudor, SaldoAcreedor, SaldoFinal"
                                        +" from[Academico].[cxc_SPCXC_004]"
                                        +" where IdEmpresa = "+IdEmpresa.ToString()+" and IdUsuario = '"+IdUsuario+"'";
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new CXC_004_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdAlumno = Convert.ToDecimal(reader["IdAlumno"]),
                            IdUsuario = Convert.ToString(reader["IdUsuario"]),
                            NomAnio = Convert.ToString(reader["NomAnio"]),
                            CodigoAlumno = Convert.ToString(reader["CodigoAlumno"]),
                            NombreAlumno = Convert.ToString(reader["NombreAlumno"]),
                            SaldoDeudor = Convert.ToDecimal(reader["SaldoDeudor"]),
                            SaldoAcreedor = Convert.ToDecimal(reader["SaldoAcreedor"]),
                            SaldoFinal = Convert.ToDecimal(reader["SaldoFinal"]),
                            NombreJornada = Convert.ToString(reader["NombreJornada"])
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
    }
}
