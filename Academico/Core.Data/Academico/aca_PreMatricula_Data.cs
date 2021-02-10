using Core.Data.Base;
using Core.Info.Academico;
using Core.Info.Helps;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_PreMatricula_Data
    {
        aca_AlumnoDocumento_Data odata_AlumnoDocumento = new aca_AlumnoDocumento_Data();
        
        public decimal getId(int IdEmpresa)
        {
            try
            {
                decimal ID = 1;

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var cont = Context.aca_PreMatricula.Where(q => q.IdEmpresa == IdEmpresa).Count();
                    if (cont > 0)
                        ID = Context.aca_PreMatricula.Where(q => q.IdEmpresa == IdEmpresa).Max(q => q.IdPreMatricula) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(aca_PreMatricula_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_PreMatricula Entity = new aca_PreMatricula
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdPreMatricula = info.IdPreMatricula = getId(info.IdEmpresa),
                        IdAdmision = info.IdAdmision,
                        Codigo = info.IdPreMatricula.ToString("00000"),
                        IdAlumno = info.IdAlumno,
                        IdAnio = info.IdAnio,
                        IdSede = info.IdSede,
                        IdNivel = info.IdNivel,
                        IdJornada = info.IdJornada,
                        IdCurso = info.IdCurso,
                        IdParalelo = info.IdParalelo,
                        IdPersonaF = info.IdPersonaF,
                        IdPersonaR = info.IdPersonaR,
                        IdPlantilla = info.IdPlantilla,
                        IdMecanismo = info.IdMecanismo,
                        Observacion = info.Observacion,
                        IdCatalogoESTPREMAT = info.IdCatalogoESTPREMAT,
                        IdUsuarioCreacion = info.IdUsuarioCreacion,
                        FechaCreacion = DateTime.Now,
                        Fecha = info.Fecha,
                        IdEmpresa_rol = info.IdEmpresa_rol,
                        IdEmpleado = info.IdEmpleado,
                        EsPatrocinado = info.EsPatrocinado,
                        IdSucursal = info.IdSucursal,
                        IdPuntoVta =info.IdPuntoVta,
                        Valor=info.Valor,
                        ValorProntoPago=info.ValorProntoPago
                    };
                    Context.aca_PreMatricula.Add(Entity);

                    if (info.lst_PreMatriculaRubro.Count > 0)
                    {
                        foreach (var item in info.lst_PreMatriculaRubro)
                        {
                            aca_PreMatricula_Rubro Entity_Det = new aca_PreMatricula_Rubro
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdPreMatricula = info.IdPreMatricula,
                                IdPeriodo = item.IdPeriodo,
                                IdRubro = item.IdRubro,
                                IdMecanismo = item.IdMecanismo,
                                IdProducto = item.IdProducto,
                                Subtotal = item.Subtotal,
                                IdCod_Impuesto_Iva = item.IdCod_Impuesto_Iva,
                                Porcentaje = item.Porcentaje,
                                ValorIVA = item.ValorIVA,
                                Total = item.Total,
                                FechaFacturacion = null,
                                EnMatricula = item.seleccionado,
                                IdPlantilla = item.IdPlantilla,
                                IdAnio = item.IdAnio,
                                IdSede = item.IdSede,
                                IdNivel = item.IdNivel,
                                IdJornada = item.IdJornada,
                                IdCurso = item.IdCurso,
                                IdParalelo = item.IdParalelo
                            };
                            Context.aca_PreMatricula_Rubro.Add(Entity_Det);
                        }
                    }

                    #region Documentos por alumno
                    //Obtengo lista de documentos por curso
                    var Secuencia = odata_AlumnoDocumento.getSecuencia(info.IdEmpresa, info.IdAlumno);
                    var lstDocPorCurso = Context.aca_AnioLectivo_Curso_Documento.Where(q => q.IdEmpresa == info.IdEmpresa
                  && q.IdSede == info.IdSede
                  && q.IdAnio == info.IdAnio
                  && q.IdNivel == info.IdNivel
                  && q.IdJornada == info.IdJornada
                  && q.IdCurso == info.IdCurso).ToList();

                    //Recorro lista de documentos por curso
                    foreach (var item in lstDocPorCurso)
                    {
                        //Valido si en la lista de los seleccionados existe el documento
                        var Documento = info.lst_Documentos.Where(q => q.IdDocumento == item.IdDocumento).FirstOrDefault();
                        //Si no existe como seleccionado
                        if (Documento == null)
                        {
                            //Valido si existe en la lista de documentos por alumno
                            var DocumentoAlumno = Context.aca_AlumnoDocumento.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdAlumno == info.IdAlumno && q.IdDocumento == item.IdDocumento).FirstOrDefault();
                            if (DocumentoAlumno == null)
                            {
                                //Si no existe lo agrego con estado false
                                Context.aca_AlumnoDocumento.Add(new aca_AlumnoDocumento
                                {
                                    IdEmpresa = info.IdEmpresa,
                                    IdAlumno = info.IdAlumno,
                                    IdDocumento = item.IdDocumento,
                                    Secuencia = Secuencia++,
                                    EnArchivo = false
                                });
                            }
                            else
                            {
                                //Si existe lo modifico y le pongo estado false
                                DocumentoAlumno.EnArchivo = false;
                            }
                        }
                        else
                        {
                            //Si existe como seleccionado valido si existe en la tabla de documentos por alumno
                            var DocumentoAlumnoE = Context.aca_AlumnoDocumento.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdAlumno == info.IdAlumno && q.IdDocumento == item.IdDocumento).FirstOrDefault();
                            if (DocumentoAlumnoE == null)
                            {
                                //Si no existe lo agrego con estado true
                                Context.aca_AlumnoDocumento.Add(new aca_AlumnoDocumento
                                {
                                    IdEmpresa = info.IdEmpresa,
                                    IdAlumno = info.IdAlumno,
                                    IdDocumento = item.IdDocumento,
                                    Secuencia = Secuencia++,
                                    EnArchivo = true
                                });
                            }
                            else
                            {
                                //Si existe lo modifico y le pongo estado true
                                DocumentoAlumnoE.EnArchivo = true;
                            }
                        }
                    }
                    #endregion

                    aca_Admision Entity_Admision = Context.aca_Admision.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdAdmision == info.IdAdmision);
                    if (Entity_Admision == null)
                        return false;

                    Entity_Admision.FechaPreMatriculacion = info.FechaModificacion = DateTime.Now;
                    Entity_Admision.IdCatalogoESTADM = Convert.ToInt32(cl_enumeradores.eTipoCatalogoAdmision.PREMATRICULADO);
                    Entity_Admision.IdUsuarioModificacion = info.IdUsuarioCreacion;
                    Entity_Admision.FechaModificacion = info.FechaModificacion = DateTime.Now;

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception EX)
            {

                throw;
            }
        }
        public aca_PreMatricula_Info getInfo_PorIdAdmision(int IdEmpresa, decimal IdAdmision)
        {
            try
            {
                aca_PreMatricula_Info info = new aca_PreMatricula_Info();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("", connection);
                    command.CommandText = "SELECT pm.IdEmpresa, pm.IdPreMatricula, pm.IdAdmision, pm.Codigo, pm.IdAlumno, pm.IdAnio, pm.IdSede, pm.IdNivel, pm.IdJornada, "
                    + " pm.IdCurso, pm.IdParalelo, pm.IdPersonaF, pm.IdPersonaR, pm.IdPlantilla, pm.IdMecanismo, pm.IdCatalogoESTPREMAT, pm.Fecha, pm.Observacion, a.Codigo AS CodigoAlumno, p.pe_cedulaRuc, p.pe_nombreCompleto, "
                    + "pm.IdSucursal, pm.IdPuntoVta, pm.Valor, pm.ValorProntoPago"
                    + " FROM     dbo.aca_PreMatricula AS pm LEFT OUTER JOIN "
                    + " dbo.aca_Alumno AS a ON pm.IdEmpresa = a.IdEmpresa AND pm.IdAlumno = a.IdAlumno LEFT OUTER JOIN "
                    + " dbo.tb_persona AS p ON a.IdPersona = p.IdPersona "
                    + " WHERE pm.IdEmpresa = " + IdEmpresa.ToString() + " and pm.IdAdmision = '" + IdAdmision.ToString() + "'";
                    var ResultValue = command.ExecuteScalar();

                    if (ResultValue == null)
                        return null;

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        info = new aca_PreMatricula_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdPreMatricula = Convert.ToDecimal(reader["IdPreMatricula"]),
                            IdAdmision = Convert.ToDecimal(reader["IdAdmision"]),
                            IdAlumno = Convert.ToDecimal(reader["IdAlumno"]),
                            IdSede = Convert.ToInt32(reader["IdSede"]),
                            IdAnio = Convert.ToInt32(reader["IdAnio"]),
                            IdJornada = Convert.ToInt32(reader["IdJornada"]),
                            IdNivel = Convert.ToInt32(reader["IdNivel"]),
                            IdCurso = Convert.ToInt32(reader["IdCurso"]),
                            IdParalelo = Convert.ToInt32(reader["IdParalelo"]),
                            Codigo = string.IsNullOrEmpty(reader["Codigo"].ToString()) ? null : reader["Codigo"].ToString(),
                            CodigoAlumno = string.IsNullOrEmpty(reader["CodigoAlumno"].ToString()) ? null : reader["CodigoAlumno"].ToString(),
                            pe_cedulaRuc = string.IsNullOrEmpty(reader["pe_cedulaRuc"].ToString()) ? null : reader["pe_cedulaRuc"].ToString(),
                            pe_nombreCompleto = string.IsNullOrEmpty(reader["pe_nombreCompleto"].ToString()) ? null : reader["pe_nombreCompleto"].ToString(),
                            IdPersonaF = Convert.ToInt32(reader["IdPersonaF"]),
                            IdPersonaR = Convert.ToInt32(reader["IdPersonaR"]),
                            IdPlantilla = Convert.ToInt32(reader["IdPlantilla"]),
                            IdMecanismo = Convert.ToInt32(reader["IdMecanismo"]),
                            IdCatalogoESTPREMAT = Convert.ToInt32(reader["IdCatalogoESTPREMAT"]),
                            Fecha = Convert.ToDateTime(reader["Fecha"]),
                            Observacion = string.IsNullOrEmpty(reader["Observacion"].ToString()) ? null : reader["Observacion"].ToString(),
                            IdSucursal = Convert.ToInt32(reader["IdSucursal"]),
                            IdPuntoVta = Convert.ToInt32(reader["IdPuntoVta"]),
                            Valor = Convert.ToDecimal(reader["Valor"]),
                            ValorProntoPago = Convert.ToDecimal(reader["ValorProntoPago"]),
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

        public List<aca_PreMatricula_Info> getList_Procesar(int IdEmpresa, int IdSede, int IdAnio)
        {
            try
            {
                List<aca_PreMatricula_Info> Lista = new List<aca_PreMatricula_Info>();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();
                    #region Query
                    string query = "SELECT pm.IdEmpresa, pm.IdPreMatricula, pm.IdAdmision, pm.Codigo, pm.IdAlumno, pm.IdAnio, pm.IdSede, pm.IdNivel, pm.IdJornada, pm.IdCurso, pm.IdParalelo, pm.IdPersonaF, pm.IdPersonaR, pm.IdPlantilla, pm.IdMecanismo, "
                    + " pm.IdCatalogoESTPREMAT, pm.Fecha, pm.Observacion, a.Codigo AS CodigoAlumno, p.pe_cedulaRuc, p.pe_nombreCompleto, pm.IdSucursal, pm.IdPuntoVta, pm.Valor, pm.ValorProntoPago, m.IdMatricula "
                    + " FROM     dbo.aca_PreMatricula AS pm LEFT OUTER JOIN "
                    + " dbo.aca_Matricula AS m ON pm.IdAlumno = m.IdAlumno AND pm.IdParalelo = m.IdParalelo AND pm.IdCurso = m.IdCurso AND pm.IdJornada = m.IdJornada AND pm.IdNivel = m.IdNivel AND pm.IdSede = m.IdSede AND "
                    + " pm.IdAnio = m.IdAnio AND m.IdEmpresa = pm.IdEmpresa LEFT OUTER JOIN "
                    + " dbo.aca_Alumno AS a ON pm.IdEmpresa = a.IdEmpresa AND pm.IdAlumno = a.IdAlumno LEFT OUTER JOIN "
                    + " dbo.tb_persona AS p ON a.IdPersona = p.IdPersona "
                    + " WHERE pm.IdEmpresa = " + IdEmpresa.ToString()
                    + " and pm.IdSede = " + IdSede.ToString()
                    + " and pm.IdAnio = " + IdAnio.ToString()
                    + " and m.IdMatricula is null";
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandTimeout = 0;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new aca_PreMatricula_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdPreMatricula = Convert.ToDecimal(reader["IdPreMatricula"]),
                            IdAdmision = Convert.ToDecimal(reader["IdAdmision"]),
                            IdAlumno = Convert.ToDecimal(reader["IdAlumno"]),
                            IdSede = Convert.ToInt32(reader["IdSede"]),
                            IdAnio = Convert.ToInt32(reader["IdAnio"]),
                            IdJornada = Convert.ToInt32(reader["IdJornada"]),
                            IdNivel = Convert.ToInt32(reader["IdNivel"]),
                            IdCurso = Convert.ToInt32(reader["IdCurso"]),
                            IdParalelo = Convert.ToInt32(reader["IdParalelo"]),
                            Codigo = string.IsNullOrEmpty(reader["Codigo"].ToString()) ? null : reader["Codigo"].ToString(),
                            CodigoAlumno = string.IsNullOrEmpty(reader["CodigoAlumno"].ToString()) ? null : reader["CodigoAlumno"].ToString(),
                            pe_cedulaRuc = string.IsNullOrEmpty(reader["pe_cedulaRuc"].ToString()) ? null : reader["pe_cedulaRuc"].ToString(),
                            pe_nombreCompleto = string.IsNullOrEmpty(reader["pe_nombreCompleto"].ToString()) ? null : reader["pe_nombreCompleto"].ToString(),
                            IdPersonaF = Convert.ToInt32(reader["IdPersonaF"]),
                            IdPersonaR = Convert.ToInt32(reader["IdPersonaR"]),
                            IdPlantilla = Convert.ToInt32(reader["IdPlantilla"]),
                            IdMecanismo = Convert.ToInt32(reader["IdMecanismo"]),
                            IdCatalogoESTPREMAT = Convert.ToInt32(reader["IdCatalogoESTPREMAT"]),
                            Fecha = Convert.ToDateTime(reader["Fecha"]),
                            Observacion = string.IsNullOrEmpty(reader["Observacion"].ToString()) ? null : reader["Observacion"].ToString(),
                            IdSucursal = Convert.ToInt32(reader["IdSucursal"]),
                            IdPuntoVta = Convert.ToInt32(reader["IdPuntoVta"]),
                            Valor = Convert.ToDecimal(reader["Valor"]),
                            ValorProntoPago = Convert.ToDecimal(reader["ValorProntoPago"]),
                        });
                    }
                    reader.Close();
                }
                Lista.ForEach(q => q.IdString = q.IdEmpresa.ToString("0000")+q.IdAdmision.ToString("000000"));
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarEstado(aca_PreMatricula_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_PreMatricula Entity = Context.aca_PreMatricula.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdPreMatricula == info.IdPreMatricula);
                    if (Entity == null) return false;

                    Entity.IdUsuarioModificacion = info.IdUsuarioModificacion;
                    Entity.FechaModificacion = DateTime.Now;
                    Entity.IdCatalogoESTPREMAT = info.IdCatalogoESTPREMAT;

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
