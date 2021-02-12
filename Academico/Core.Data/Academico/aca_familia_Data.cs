using Core.Data.Base;
using Core.Info.Academico;
using Core.Info.General;
using Core.Info.Helps;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_Familia_Data
    {
        aca_AnioLectivo_Data odata_anio = new aca_AnioLectivo_Data();
        aca_Matricula_Data odata_matricula = new aca_Matricula_Data();
        public List<aca_Familia_Info> getList(int IdEmpresa, int IdAlumno)
        {
            try
            {
                List<aca_Familia_Info> Lista = new List<aca_Familia_Info>();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = "SELECT f.IdEmpresa, f.IdAlumno, f.IdCatalogoPAREN, c.NomCatalogo, f.IdPersona, p.pe_Naturaleza, p.IdTipoDocumento, p.pe_cedulaRuc, p.pe_apellido, p.pe_nombre, p.pe_nombreCompleto, f.Direccion, f.Celular, f.Correo, f.SeFactura, p.pe_sexo, "
                    + " p.IdEstadoCivil, p.pe_fechaNacimiento, p.CodCatalogoSangre, p.CodCatalogoCONADIS, p.PorcentajeDiscapacidad, p.NumeroCarnetConadis, p.pe_telfono_Contacto, f.Secuencia, f.EsRepresentante, p.pe_razonSocial, p.IdProfesion, "
                    + " f.IdCatalogoFichaInst, f.EmpresaTrabajo, f.DireccionTrabajo, f.TelefonoTrabajo, f.CargoTrabajo, f.AniosServicio, f.IngresoMensual, f.VehiculoPropio, f.Marca, f.Modelo, f.CasaPropia, f.AnioVehiculo, p.IdReligion, p.AsisteCentroCristiano, "
                    + " f.EstaFallecido, p.IdGrupoEtnico, f.IdPais, f.Cod_Region, f.IdProvincia, f.IdCiudad, f.IdParroquia, f.Sector, f.Estado, f.Telefono "
                    + " FROM dbo.tb_persona AS p INNER JOIN "
                    + " dbo.aca_Familia AS f ON p.IdPersona = f.IdPersona LEFT OUTER JOIN "
                    + " dbo.aca_Catalogo AS c ON f.IdCatalogoPAREN = c.IdCatalogo "
                    + " WHERE f.IdEmpresa = " + IdEmpresa.ToString() + "and f.IdAlumno = " + IdAlumno.ToString();
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandTimeout = 0;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new aca_Familia_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdAlumno = Convert.ToDecimal(reader["IdAlumno"]),
                            Secuencia = Convert.ToInt32(reader["Secuencia"]),
                            IdPersona = Convert.ToDecimal(reader["IdPersona"]),
                            IdCatalogoPAREN = Convert.ToInt32(reader["IdCatalogoPAREN"]),
                            NomCatalogo = string.IsNullOrEmpty(reader["NomCatalogo"].ToString()) ? null : reader["NomCatalogo"].ToString(),
                            Direccion = string.IsNullOrEmpty(reader["Direccion"].ToString()) ? null : reader["Direccion"].ToString(),
                            Telefono = string.IsNullOrEmpty(reader["Telefono"].ToString()) ? null : reader["Telefono"].ToString(),
                            Celular = string.IsNullOrEmpty(reader["Celular"].ToString()) ? null : reader["Celular"].ToString(),
                            Correo = string.IsNullOrEmpty(reader["Correo"].ToString()) ? null : reader["Correo"].ToString(),
                            SeFactura = Convert.ToBoolean(reader["SeFactura"]),
                            EsRepresentante = Convert.ToBoolean(reader["EsRepresentante"]),
                            IdCatalogoFichaInst = string.IsNullOrEmpty(reader["IdCatalogoFichaInst"].ToString()) ? (int?)null : Convert.ToInt32(reader["IdCatalogoFichaInst"]),
                            EmpresaTrabajo = string.IsNullOrEmpty(reader["EmpresaTrabajo"].ToString()) ? null : reader["EmpresaTrabajo"].ToString(),
                            DireccionTrabajo = string.IsNullOrEmpty(reader["DireccionTrabajo"].ToString()) ? null : reader["DireccionTrabajo"].ToString(),
                            TelefonoTrabajo = string.IsNullOrEmpty(reader["TelefonoTrabajo"].ToString()) ? null : reader["TelefonoTrabajo"].ToString(),
                            CargoTrabajo = string.IsNullOrEmpty(reader["CargoTrabajo"].ToString()) ? null : reader["CargoTrabajo"].ToString(),
                            AniosServicio = string.IsNullOrEmpty(reader["AniosServicio"].ToString()) ? (int?)null : Convert.ToInt32(reader["AniosServicio"]),
                            IngresoMensual = string.IsNullOrEmpty(reader["IngresoMensual"].ToString()) ? (double?)null : Convert.ToDouble(reader["IngresoMensual"]),
                            VehiculoPropio = Convert.ToBoolean(reader["VehiculoPropio"]),
                            Marca = string.IsNullOrEmpty(reader["Marca"].ToString()) ? null : reader["Marca"].ToString(),
                            Modelo = string.IsNullOrEmpty(reader["Modelo"].ToString()) ? null : reader["Modelo"].ToString(),
                            CasaPropia = Convert.ToBoolean(reader["CasaPropia"]),
                            IdTipoDocumento = reader["IdTipoDocumento"].ToString(),
                            pe_Naturaleza = reader["pe_Naturaleza"].ToString(),
                            pe_cedulaRuc = reader["pe_cedulaRuc"].ToString(),
                            pe_nombre = string.IsNullOrEmpty(reader["pe_nombre"].ToString()) ? null : reader["pe_nombre"].ToString(),
                            pe_apellido = string.IsNullOrEmpty(reader["pe_apellido"].ToString()) ? null : reader["pe_apellido"].ToString(),
                            pe_nombreCompleto = reader["pe_nombreCompleto"].ToString(),
                            pe_sexo = string.IsNullOrEmpty(reader["pe_sexo"].ToString()) ? null : reader["pe_sexo"].ToString(),
                            IdEstadoCivil = string.IsNullOrEmpty(reader["IdEstadoCivil"].ToString()) ? null : reader["IdEstadoCivil"].ToString(),
                            pe_fechaNacimiento = string.IsNullOrEmpty(reader["pe_fechaNacimiento"].ToString()) ? (DateTime?)null : Convert.ToDateTime(reader["pe_fechaNacimiento"]),
                            EstaFallecido = Convert.ToBoolean(reader["EstaFallecido"]),
                            Estado = Convert.ToBoolean(reader["Estado"])
                        });
                    }
                    reader.Close();
                }
                /*
                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.vwaca_Familia.Where(q => q.IdEmpresa == IdEmpresa && q.IdAlumno == IdAlumno).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_Familia_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdAlumno = q.IdAlumno,
                            Secuencia = q.Secuencia,
                            IdPersona = q.IdPersona,
                            IdCatalogoPAREN = q.IdCatalogoPAREN,
                            NomCatalogo = q.NomCatalogo,
                            Direccion = q.Direccion,
                            Telefono = q.Telefono,
                            Celular =q.Celular,
                            Correo =q.Correo,
                            SeFactura = q.SeFactura,
                            EsRepresentante =q.EsRepresentante,
                            IdCatalogoFichaInst = q.IdCatalogoFichaInst??0,
                            EmpresaTrabajo = q.EmpresaTrabajo,
                            DireccionTrabajo = q.DireccionTrabajo,
                            TelefonoTrabajo = q.TelefonoTrabajo,
                            CargoTrabajo = q.CargoTrabajo,
                            AniosServicio = q.AniosServicio,
                            IngresoMensual = q.IngresoMensual,
                            VehiculoPropio = q.VehiculoPropio,
                            Marca = q.Marca,
                            Modelo = q.Modelo,
                            CasaPropia = q.CasaPropia,
                            IdTipoDocumento = q.IdTipoDocumento,
                            pe_Naturaleza = q.pe_Naturaleza,
                            pe_cedulaRuc = q.pe_cedulaRuc,
                            pe_nombre = q.pe_nombre,
                            pe_apellido = q.pe_apellido,
                            pe_nombreCompleto = q.pe_nombreCompleto,
                            pe_sexo = q.pe_sexo,
                            IdEstadoCivil = q.IdEstadoCivil,
                            pe_fechaNacimiento = q.pe_fechaNacimiento,
                            EstaFallecido = q.EstaFallecido,
                            Estado = q.Estado
                        });
                    });
                }
                */
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public aca_Familia_Info getListTipo(int IdEmpresa, decimal IdAlumno, int IdCatalogoPAREN)
        {
            try
            {
                aca_Familia_Info info_familia = new aca_Familia_Info();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("", connection);
                    command.CommandText = "SELECT f.IdEmpresa, f.IdAlumno, f.IdCatalogoPAREN, c.NomCatalogo, f.IdPersona, p.pe_Naturaleza, p.IdTipoDocumento, p.pe_cedulaRuc, p.pe_apellido, p.pe_nombre, p.pe_nombreCompleto, f.Direccion, f.Celular, f.Correo, f.SeFactura, p.pe_sexo, "
                    + " p.IdEstadoCivil, p.pe_fechaNacimiento, p.CodCatalogoSangre, p.CodCatalogoCONADIS, p.PorcentajeDiscapacidad, p.NumeroCarnetConadis, p.pe_telfono_Contacto, f.Secuencia, f.EsRepresentante, p.pe_razonSocial, p.IdProfesion, "
                    + " f.IdCatalogoFichaInst, f.EmpresaTrabajo, f.DireccionTrabajo, f.TelefonoTrabajo, f.CargoTrabajo, f.AniosServicio, f.IngresoMensual, f.VehiculoPropio, f.Marca, f.Modelo,f.AnioVehiculo, f.CasaPropia, f.AnioVehiculo, p.IdReligion, p.AsisteCentroCristiano, "
                    + " f.EstaFallecido, p.IdGrupoEtnico, f.IdPais, f.Cod_Region, f.IdProvincia, f.IdCiudad, f.IdParroquia, f.Sector, f.Estado, f.Telefono, p.IdReligion "
                    + " FROM dbo.tb_persona AS p INNER JOIN "
                    + " dbo.aca_Familia AS f ON p.IdPersona = f.IdPersona LEFT OUTER JOIN "
                    + " dbo.aca_Catalogo AS c ON f.IdCatalogoPAREN = c.IdCatalogo "
                    + " WHERE f.IdEmpresa = " + IdEmpresa.ToString() + "and f.IdAlumno = " + IdAlumno.ToString() + "and f.IdCatalogoPAREN = " + IdCatalogoPAREN.ToString() + "and f.Estado = 1 ";
                    var ResultValue = command.ExecuteScalar();

                    if (ResultValue == null)
                        return null;

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        info_familia = new aca_Familia_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdAlumno = Convert.ToDecimal(reader["IdAlumno"]),
                            Secuencia = Convert.ToInt32(reader["Secuencia"]),
                            IdPersona = Convert.ToDecimal(reader["IdPersona"]),
                            IdCatalogoPAREN = Convert.ToInt32(reader["IdCatalogoPAREN"]),
                            NomCatalogo = string.IsNullOrEmpty(reader["NomCatalogo"].ToString()) ? null : reader["NomCatalogo"].ToString(),
                            Direccion = string.IsNullOrEmpty(reader["Direccion"].ToString()) ? null : reader["Direccion"].ToString(),
                            Telefono = string.IsNullOrEmpty(reader["Telefono"].ToString()) ? null : reader["Telefono"].ToString(),
                            Celular = string.IsNullOrEmpty(reader["Celular"].ToString()) ? null : reader["Celular"].ToString(),
                            Correo = string.IsNullOrEmpty(reader["Correo"].ToString()) ? null : reader["Correo"].ToString(),
                            SeFactura = Convert.ToBoolean(reader["SeFactura"]),
                            EsRepresentante = Convert.ToBoolean(reader["EsRepresentante"]),
                            IdCatalogoFichaInst = string.IsNullOrEmpty(reader["IdCatalogoFichaInst"].ToString()) ? (int?)null : Convert.ToInt32(reader["IdCatalogoFichaInst"]),
                            EmpresaTrabajo = string.IsNullOrEmpty(reader["EmpresaTrabajo"].ToString()) ? null : reader["EmpresaTrabajo"].ToString(),
                            DireccionTrabajo = string.IsNullOrEmpty(reader["DireccionTrabajo"].ToString()) ? null : reader["DireccionTrabajo"].ToString(),
                            TelefonoTrabajo = string.IsNullOrEmpty(reader["TelefonoTrabajo"].ToString()) ? null : reader["TelefonoTrabajo"].ToString(),
                            CargoTrabajo = string.IsNullOrEmpty(reader["CargoTrabajo"].ToString()) ? null : reader["CargoTrabajo"].ToString(),
                            AniosServicio = string.IsNullOrEmpty(reader["AniosServicio"].ToString()) ? (int?)null : Convert.ToInt32(reader["AniosServicio"]),
                            IngresoMensual = string.IsNullOrEmpty(reader["IngresoMensual"].ToString()) ? (double?)null : Convert.ToDouble(reader["IngresoMensual"]),
                            VehiculoPropio = Convert.ToBoolean(reader["VehiculoPropio"]),
                            Marca = string.IsNullOrEmpty(reader["Marca"].ToString()) ? null : reader["Marca"].ToString(),
                            Modelo = string.IsNullOrEmpty(reader["Modelo"].ToString()) ? null : reader["Modelo"].ToString(),
                            CasaPropia = Convert.ToBoolean(reader["CasaPropia"]),
                            IdTipoDocumento = reader["IdTipoDocumento"].ToString(),
                            pe_Naturaleza = reader["pe_Naturaleza"].ToString(),
                            pe_cedulaRuc = reader["pe_cedulaRuc"].ToString(),
                            pe_nombre = string.IsNullOrEmpty(reader["pe_nombre"].ToString()) ? null : reader["pe_nombre"].ToString(),
                            pe_apellido = string.IsNullOrEmpty(reader["pe_apellido"].ToString()) ? null : reader["pe_apellido"].ToString(),
                            pe_nombreCompleto = reader["pe_nombreCompleto"].ToString(),
                            pe_sexo = string.IsNullOrEmpty(reader["pe_sexo"].ToString()) ? null : reader["pe_sexo"].ToString(),
                            IdEstadoCivil = string.IsNullOrEmpty(reader["IdEstadoCivil"].ToString()) ? null : reader["IdEstadoCivil"].ToString(),
                            pe_fechaNacimiento = string.IsNullOrEmpty(reader["pe_fechaNacimiento"].ToString()) ? (DateTime?)null : Convert.ToDateTime(reader["pe_fechaNacimiento"]),
                            EstaFallecido = Convert.ToBoolean(reader["EstaFallecido"]),
                            Estado = Convert.ToBoolean(reader["Estado"]),
                            IdPais = string.IsNullOrEmpty(reader["IdPais"].ToString()) ? null : reader["IdPais"].ToString(),
                            IdProvincia = string.IsNullOrEmpty(reader["IdProvincia"].ToString()) ? null : reader["IdProvincia"].ToString(),
                            IdCiudad = string.IsNullOrEmpty(reader["IdCiudad"].ToString()) ? null : reader["IdCiudad"].ToString(),
                            IdParroquia = string.IsNullOrEmpty(reader["IdParroquia"].ToString()) ? null : reader["IdParroquia"].ToString(),
                            Cod_Region = string.IsNullOrEmpty(reader["Cod_Region"].ToString()) ? null : reader["Cod_Region"].ToString(),
                            Sector = string.IsNullOrEmpty(reader["Sector"].ToString()) ? null : reader["Sector"].ToString(),
                            IdReligion = string.IsNullOrEmpty(reader["IdReligion"].ToString()) ? (int?)null : Convert.ToInt32(reader["IdReligion"]),
                            IdProfesion = string.IsNullOrEmpty(reader["IdProfesion"].ToString()) ? (int?)null : Convert.ToInt32(reader["IdProfesion"]),
                            AnioVehiculo = string.IsNullOrEmpty(reader["AnioVehiculo"].ToString()) ? (int?)null : Convert.ToInt32(reader["AnioVehiculo"]),
                        };
                    }
                }
                /*
                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var Entity = odata.vwaca_Familia.Where(q => q.IdEmpresa == IdEmpresa && q.IdAlumno == IdAlumno && q.IdCatalogoPAREN == IdCatalogoPAREN && q.Estado==true).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info_familia = new aca_Familia_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdAlumno = Entity.IdAlumno,
                        IdPersona = Entity.IdPersona,
                        IdCatalogoPAREN = Entity.IdCatalogoPAREN,
                        Direccion = Entity.Direccion,
                        Telefono = Entity.Telefono,
                        Celular = Entity.Celular,
                        Correo = Entity.Correo,
                        SeFactura = Entity.SeFactura,
                        EsRepresentante = Entity.EsRepresentante,
                        IdTipoDocumento = Entity.IdTipoDocumento,
                        pe_Naturaleza = Entity.pe_Naturaleza,
                        pe_cedulaRuc = Entity.pe_cedulaRuc,
                        pe_nombre = Entity.pe_nombre,
                        pe_apellido = Entity.pe_apellido,
                        pe_nombreCompleto = Entity.pe_nombreCompleto,
                        pe_razonSocial = Entity.pe_razonSocial,
                        pe_sexo = Entity.pe_sexo,
                        IdProfesion = Entity.IdProfesion ?? 0,
                        IdCatalogoFichaInst = Entity.IdCatalogoFichaInst??0,
                        EmpresaTrabajo = Entity.EmpresaTrabajo,
                        DireccionTrabajo = Entity.DireccionTrabajo,
                        TelefonoTrabajo = Entity.TelefonoTrabajo,
                        CargoTrabajo = Entity.CargoTrabajo,
                        AniosServicio = Entity.AniosServicio,
                        IngresoMensual = Entity.IngresoMensual,
                        VehiculoPropio = Entity.VehiculoPropio,
                        Marca = Entity.Marca,
                        Modelo = Entity.Modelo,
                        AnioVehiculo = Entity.AnioVehiculo,
                        CasaPropia = Entity.CasaPropia,
                        IdEstadoCivil = Entity.IdEstadoCivil,
                        pe_fechaNacimiento = Entity.pe_fechaNacimiento,
                        CodCatalogoCONADIS = Entity.CodCatalogoCONADIS,
                        NumeroCarnetConadis = Entity.NumeroCarnetConadis,
                        PorcentajeDiscapacidad = Entity.PorcentajeDiscapacidad,
                        pe_telfono_Contacto = Entity.pe_telfono_Contacto,
                        IdReligion = Entity.IdReligion,
                        AsisteCentroCristiano = Entity.AsisteCentroCristiano,
                        IdPais = Entity.IdPais,
                        IdProvincia = Entity.IdProvincia,
                        IdCiudad = Entity.IdCiudad,
                        IdParroquia = Entity.IdParroquia,
                        Cod_Region = Entity.Cod_Region,
                        Sector = Entity.Sector,
                        EstaFallecido = Entity.EstaFallecido,
                        Estado = Entity.Estado
                    };
                }
                */
                return info_familia;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public aca_Familia_Info getInfo_Representante(int IdEmpresa, decimal IdAlumno, string Tipo)
        {
            try
            {
                aca_Familia_Info info_familia = new aca_Familia_Info();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("", connection);
                    command.CommandText = "SELECT f.IdEmpresa, f.IdAlumno, f.IdCatalogoPAREN, c.NomCatalogo, f.IdPersona, p.pe_Naturaleza, p.IdTipoDocumento, p.pe_cedulaRuc, p.pe_apellido, p.pe_nombre, p.pe_nombreCompleto, f.Direccion, f.Celular, f.Correo, f.SeFactura, p.pe_sexo, "
                    + " p.IdEstadoCivil, p.pe_fechaNacimiento, p.CodCatalogoSangre, p.CodCatalogoCONADIS, p.PorcentajeDiscapacidad, p.NumeroCarnetConadis, p.pe_telfono_Contacto, f.Secuencia, f.EsRepresentante, p.pe_razonSocial, p.IdProfesion, "
                    + " f.IdCatalogoFichaInst, f.EmpresaTrabajo, f.DireccionTrabajo, f.TelefonoTrabajo, f.CargoTrabajo, f.AniosServicio, f.IngresoMensual, f.VehiculoPropio, f.Marca, f.Modelo, f.CasaPropia, f.AnioVehiculo, p.IdReligion, p.AsisteCentroCristiano, "
                    + " f.EstaFallecido, p.IdGrupoEtnico, f.IdPais, f.Cod_Region, f.IdProvincia, f.IdCiudad, f.IdParroquia, f.Sector, f.Estado, f.Telefono, p.IdReligion "
                    + " FROM dbo.tb_persona AS p INNER JOIN "
                    + " dbo.aca_Familia AS f ON p.IdPersona = f.IdPersona LEFT OUTER JOIN "
                    + " dbo.aca_Catalogo AS c ON f.IdCatalogoPAREN = c.IdCatalogo "
                    + " WHERE f.IdEmpresa = " + IdEmpresa.ToString() + "and f.IdAlumno = " + IdAlumno.ToString() + "and f.Estado = 1 ";
                    if (Tipo == cl_enumeradores.eTipoRepresentante.ECON.ToString())
                    {
                        command.CommandText += "and f.SeFactura = 1";
                    }
                    else
                    {
                        command.CommandText += "and f.EsRepresentante = 1";
                    }
                    var ResultValue = command.ExecuteScalar();

                    if (ResultValue == null)
                        return null;

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        info_familia = new aca_Familia_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdAlumno = Convert.ToDecimal(reader["IdAlumno"]),
                            Secuencia = Convert.ToInt32(reader["Secuencia"]),
                            IdPersona = Convert.ToDecimal(reader["IdPersona"]),
                            IdCatalogoPAREN = Convert.ToInt32(reader["IdCatalogoPAREN"]),
                            NomCatalogo = string.IsNullOrEmpty(reader["NomCatalogo"].ToString()) ? null : reader["NomCatalogo"].ToString(),
                            Direccion = string.IsNullOrEmpty(reader["Direccion"].ToString()) ? null : reader["Direccion"].ToString(),
                            Telefono = string.IsNullOrEmpty(reader["Telefono"].ToString()) ? null : reader["Telefono"].ToString(),
                            Celular = string.IsNullOrEmpty(reader["Celular"].ToString()) ? null : reader["Celular"].ToString(),
                            Correo = string.IsNullOrEmpty(reader["Correo"].ToString()) ? null : reader["Correo"].ToString(),
                            SeFactura = Convert.ToBoolean(reader["SeFactura"]),
                            EsRepresentante = Convert.ToBoolean(reader["EsRepresentante"]),
                            IdCatalogoFichaInst = string.IsNullOrEmpty(reader["IdCatalogoFichaInst"].ToString()) ? (int?)null : Convert.ToInt32(reader["IdCatalogoFichaInst"]),
                            EmpresaTrabajo = string.IsNullOrEmpty(reader["EmpresaTrabajo"].ToString()) ? null : reader["EmpresaTrabajo"].ToString(),
                            DireccionTrabajo = string.IsNullOrEmpty(reader["DireccionTrabajo"].ToString()) ? null : reader["DireccionTrabajo"].ToString(),
                            TelefonoTrabajo = string.IsNullOrEmpty(reader["TelefonoTrabajo"].ToString()) ? null : reader["TelefonoTrabajo"].ToString(),
                            CargoTrabajo = string.IsNullOrEmpty(reader["CargoTrabajo"].ToString()) ? null : reader["CargoTrabajo"].ToString(),
                            AniosServicio = string.IsNullOrEmpty(reader["AniosServicio"].ToString()) ? (int?)null : Convert.ToInt32(reader["AniosServicio"]),
                            IngresoMensual = string.IsNullOrEmpty(reader["IngresoMensual"].ToString()) ? (double?)null : Convert.ToDouble(reader["IngresoMensual"]),
                            VehiculoPropio = Convert.ToBoolean(reader["VehiculoPropio"]),
                            Marca = string.IsNullOrEmpty(reader["Marca"].ToString()) ? null : reader["Marca"].ToString(),
                            Modelo = string.IsNullOrEmpty(reader["Modelo"].ToString()) ? null : reader["Modelo"].ToString(),
                            CasaPropia = Convert.ToBoolean(reader["CasaPropia"]),
                            IdTipoDocumento = reader["IdTipoDocumento"].ToString(),
                            pe_Naturaleza = reader["pe_Naturaleza"].ToString(),
                            pe_cedulaRuc = reader["pe_cedulaRuc"].ToString(),
                            pe_nombre = string.IsNullOrEmpty(reader["pe_nombre"].ToString()) ? null : reader["pe_nombre"].ToString(),
                            pe_apellido = string.IsNullOrEmpty(reader["pe_apellido"].ToString()) ? null : reader["pe_apellido"].ToString(),
                            pe_nombreCompleto = reader["pe_nombreCompleto"].ToString(),
                            pe_razonSocial = string.IsNullOrEmpty(reader["pe_razonSocial"].ToString()) ? null : reader["pe_razonSocial"].ToString(),
                            pe_sexo = string.IsNullOrEmpty(reader["pe_sexo"].ToString()) ? null : reader["pe_sexo"].ToString(),
                            IdEstadoCivil = string.IsNullOrEmpty(reader["IdEstadoCivil"].ToString()) ? null : reader["IdEstadoCivil"].ToString(),
                            pe_fechaNacimiento = string.IsNullOrEmpty(reader["pe_fechaNacimiento"].ToString()) ? (DateTime?)null : Convert.ToDateTime(reader["pe_fechaNacimiento"]),
                            EstaFallecido = Convert.ToBoolean(reader["EstaFallecido"]),
                            CodCatalogoCONADIS = string.IsNullOrEmpty(reader["CodCatalogoCONADIS"].ToString()) ? null : reader["CodCatalogoCONADIS"].ToString(),
                            NumeroCarnetConadis = string.IsNullOrEmpty(reader["NumeroCarnetConadis"].ToString()) ? null : reader["NumeroCarnetConadis"].ToString(),
                            PorcentajeDiscapacidad = string.IsNullOrEmpty(reader["PorcentajeDiscapacidad"].ToString()) ? (double?)null : Convert.ToDouble(reader["CodCatalogoCONADIS"]),
                            pe_telfono_Contacto = string.IsNullOrEmpty(reader["pe_telfono_Contacto"].ToString()) ? null : reader["pe_telfono_Contacto"].ToString(),
                            IdProfesion = string.IsNullOrEmpty(reader["IdProfesion"].ToString()) ? (int?)null : Convert.ToInt32(reader["IdProfesion"]),
                            Estado = Convert.ToBoolean(reader["Estado"]),
                            IdPais = string.IsNullOrEmpty(reader["IdPais"].ToString()) ? null : reader["IdPais"].ToString(),
                            IdProvincia = string.IsNullOrEmpty(reader["IdProvincia"].ToString()) ? null : reader["IdProvincia"].ToString(),
                            IdCiudad = string.IsNullOrEmpty(reader["IdCiudad"].ToString()) ? null : reader["IdCiudad"].ToString(),
                            IdParroquia = string.IsNullOrEmpty(reader["IdParroquia"].ToString()) ? null : reader["IdParroquia"].ToString(),
                            Cod_Region = string.IsNullOrEmpty(reader["Cod_Region"].ToString()) ? null : reader["Cod_Region"].ToString(),
                            Sector = string.IsNullOrEmpty(reader["Sector"].ToString()) ? null : reader["Sector"].ToString(),
                            IdReligion = string.IsNullOrEmpty(reader["IdReligion"].ToString()) ? (int?)null : Convert.ToInt32(reader["IdReligion"]),
                        };
                    }
                }
                /*
                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var Entity = odata.vwaca_Familia.Where(q => q.IdEmpresa == IdEmpresa && q.IdAlumno == IdAlumno && q.Estado==true 
                    && ((Tipo == cl_enumeradores.eTipoRepresentante.ECON.ToString() ? q.SeFactura == true : q.EsRepresentante==true))).FirstOrDefault();

                    if (Entity == null)
                        return null;

                    info_familia = new aca_Familia_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdAlumno = Entity.IdAlumno,
                        Secuencia = Entity.Secuencia,
                        IdPersona = Entity.IdPersona,
                        IdCatalogoPAREN = Entity.IdCatalogoPAREN,
                        Direccion = Entity.Direccion,
                        Telefono = Entity.Telefono,
                        Celular = Entity.Celular,
                        Correo = Entity.Correo,
                        SeFactura = Entity.SeFactura,
                        IdTipoDocumento = Entity.IdTipoDocumento,
                        pe_Naturaleza = Entity.pe_Naturaleza,
                        pe_cedulaRuc = Entity.pe_cedulaRuc,
                        pe_nombre = Entity.pe_nombre,
                        pe_apellido = Entity.pe_apellido,
                        pe_nombreCompleto = Entity.pe_nombreCompleto,
                        pe_razonSocial = Entity.pe_razonSocial,
                        pe_sexo = Entity.pe_sexo,
                        IdEstadoCivil = Entity.IdEstadoCivil,
                        IdProfesion = Entity.IdProfesion ?? 0,
                        IdCatalogoFichaInst = Entity.IdCatalogoFichaInst??0,
                        EmpresaTrabajo = Entity.EmpresaTrabajo,
                        DireccionTrabajo = Entity.DireccionTrabajo,
                        TelefonoTrabajo = Entity.TelefonoTrabajo,
                        CargoTrabajo = Entity.CargoTrabajo,
                        AniosServicio = Entity.AniosServicio,
                        IngresoMensual = Entity.IngresoMensual,
                        VehiculoPropio = Entity.VehiculoPropio,
                        Marca = Entity.Marca,
                        Modelo = Entity.Modelo,
                        CasaPropia = Entity.CasaPropia,
                        pe_fechaNacimiento = Entity.pe_fechaNacimiento,
                        CodCatalogoCONADIS = Entity.CodCatalogoCONADIS,
                        NumeroCarnetConadis = Entity.NumeroCarnetConadis,
                        PorcentajeDiscapacidad = Entity.PorcentajeDiscapacidad,
                        pe_telfono_Contacto = Entity.pe_telfono_Contacto,
                        IdReligion = Entity.IdReligion,
                        AsisteCentroCristiano = Entity.AsisteCentroCristiano,
                        IdPais = Entity.IdPais,
                        IdProvincia = Entity.IdProvincia,
                        IdCiudad = Entity.IdCiudad,
                        IdParroquia = Entity.IdParroquia,
                        Cod_Region = Entity.Cod_Region,
                        Sector = Entity.Sector,
                        EstaFallecido = Entity.EstaFallecido,
                        Estado = Entity.Estado
                    };
                }
                */
                return info_familia;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public aca_Familia_Info getInfo(int IdEmpresa, int IdAlumno, int Secuencia)
        {
            try
            {
                aca_Familia_Info info_familia = new aca_Familia_Info();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("", connection);
                    command.CommandText = "SELECT f.IdEmpresa, f.IdAlumno, f.IdCatalogoPAREN, c.NomCatalogo, f.IdPersona, p.pe_Naturaleza, p.IdTipoDocumento, p.pe_cedulaRuc, p.pe_apellido, p.pe_nombre, p.pe_nombreCompleto, f.Direccion, f.Celular, f.Correo, f.SeFactura, p.pe_sexo, "
                    + " p.IdEstadoCivil, p.pe_fechaNacimiento, p.CodCatalogoSangre, p.CodCatalogoCONADIS, p.PorcentajeDiscapacidad, p.NumeroCarnetConadis, p.pe_telfono_Contacto, f.Secuencia, f.EsRepresentante, p.pe_razonSocial, p.IdProfesion, "
                    + " f.IdCatalogoFichaInst, f.EmpresaTrabajo, f.DireccionTrabajo, f.TelefonoTrabajo, f.CargoTrabajo, f.AniosServicio, f.IngresoMensual, f.VehiculoPropio, f.Marca, f.Modelo, f.CasaPropia, f.AnioVehiculo, p.IdReligion, p.AsisteCentroCristiano, "
                    + " f.EstaFallecido, p.IdGrupoEtnico, f.IdPais, f.Cod_Region, f.IdProvincia, f.IdCiudad, f.IdParroquia, f.Sector, f.Estado, f.Telefono "
                    + " FROM dbo.tb_persona AS p INNER JOIN "
                    + " dbo.aca_Familia AS f ON p.IdPersona = f.IdPersona LEFT OUTER JOIN "
                    + " dbo.aca_Catalogo AS c ON f.IdCatalogoPAREN = c.IdCatalogo "
                    + " WHERE f.IdEmpresa = " + IdEmpresa.ToString() + "and f.IdAlumno = " + IdAlumno.ToString() + "and f.Secuencia = " + Secuencia.ToString() + "and f.Estado = 1 ";
                    var ResultValue = command.ExecuteScalar();

                    if (ResultValue == null)
                        return null;

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        info_familia = new aca_Familia_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdAlumno = Convert.ToDecimal(reader["IdAlumno"]),
                            Secuencia = Convert.ToInt32(reader["Secuencia"]),
                            IdPersona = Convert.ToDecimal(reader["IdPersona"]),
                            IdCatalogoPAREN = Convert.ToInt32(reader["IdCatalogoPAREN"]),
                            NomCatalogo = string.IsNullOrEmpty(reader["NomCatalogo"].ToString()) ? null : reader["NomCatalogo"].ToString(),
                            Direccion = string.IsNullOrEmpty(reader["Direccion"].ToString()) ? null : reader["Direccion"].ToString(),
                            Telefono = string.IsNullOrEmpty(reader["Telefono"].ToString()) ? null : reader["Telefono"].ToString(),
                            Celular = string.IsNullOrEmpty(reader["Celular"].ToString()) ? null : reader["Celular"].ToString(),
                            Correo = string.IsNullOrEmpty(reader["Correo"].ToString()) ? null : reader["Correo"].ToString(),
                            SeFactura = Convert.ToBoolean(reader["SeFactura"]),
                            EsRepresentante = Convert.ToBoolean(reader["EsRepresentante"]),
                            IdCatalogoFichaInst = string.IsNullOrEmpty(reader["IdCatalogoFichaInst"].ToString()) ? (int?)null : Convert.ToInt32(reader["IdCatalogoFichaInst"]),
                            EmpresaTrabajo = string.IsNullOrEmpty(reader["EmpresaTrabajo"].ToString()) ? null : reader["EmpresaTrabajo"].ToString(),
                            DireccionTrabajo = string.IsNullOrEmpty(reader["DireccionTrabajo"].ToString()) ? null : reader["DireccionTrabajo"].ToString(),
                            TelefonoTrabajo = string.IsNullOrEmpty(reader["TelefonoTrabajo"].ToString()) ? null : reader["TelefonoTrabajo"].ToString(),
                            CargoTrabajo = string.IsNullOrEmpty(reader["CargoTrabajo"].ToString()) ? null : reader["CargoTrabajo"].ToString(),
                            AniosServicio = string.IsNullOrEmpty(reader["AniosServicio"].ToString()) ? (int?)null : Convert.ToInt32(reader["AniosServicio"]),
                            IngresoMensual = string.IsNullOrEmpty(reader["IngresoMensual"].ToString()) ? (double?)null : Convert.ToDouble(reader["IngresoMensual"]),
                            VehiculoPropio = Convert.ToBoolean(reader["VehiculoPropio"]),
                            Marca = string.IsNullOrEmpty(reader["Marca"].ToString()) ? null : reader["Marca"].ToString(),
                            AnioVehiculo = string.IsNullOrEmpty(reader["AnioVehiculo"].ToString()) ? (int?)null : Convert.ToInt32(reader["AnioVehiculo"]),
                            Modelo = string.IsNullOrEmpty(reader["Modelo"].ToString()) ? null : reader["Modelo"].ToString(),
                            CasaPropia = Convert.ToBoolean(reader["CasaPropia"]),
                            IdTipoDocumento = reader["IdTipoDocumento"].ToString(),
                            pe_Naturaleza = reader["pe_Naturaleza"].ToString(),
                            pe_cedulaRuc = reader["pe_cedulaRuc"].ToString(),
                            pe_nombre = string.IsNullOrEmpty(reader["pe_nombre"].ToString()) ? null : reader["pe_nombre"].ToString(),
                            pe_apellido = string.IsNullOrEmpty(reader["pe_apellido"].ToString()) ? null : reader["pe_apellido"].ToString(),
                            pe_nombreCompleto = reader["pe_nombreCompleto"].ToString(),
                            pe_sexo = string.IsNullOrEmpty(reader["pe_sexo"].ToString()) ? null : reader["pe_sexo"].ToString(),
                            IdEstadoCivil = string.IsNullOrEmpty(reader["IdEstadoCivil"].ToString()) ? null : reader["IdEstadoCivil"].ToString(),
                            pe_fechaNacimiento = string.IsNullOrEmpty(reader["pe_fechaNacimiento"].ToString()) ? (DateTime?)null : Convert.ToDateTime(reader["pe_fechaNacimiento"]),
                            EstaFallecido = Convert.ToBoolean(reader["EstaFallecido"]),
                            IdPais = string.IsNullOrEmpty(reader["IdPais"].ToString()) ? null : reader["IdPais"].ToString(),
                            IdCiudad = string.IsNullOrEmpty(reader["IdCiudad"].ToString()) ? null : reader["IdCiudad"].ToString(),
                            IdProvincia = string.IsNullOrEmpty(reader["IdProvincia"].ToString()) ? null : reader["IdProvincia"].ToString(),
                            Cod_Region = string.IsNullOrEmpty(reader["Cod_Region"].ToString()) ? null : reader["Cod_Region"].ToString(),
                            IdParroquia = string.IsNullOrEmpty(reader["IdParroquia"].ToString()) ? null : reader["IdParroquia"].ToString(),
                            Sector = string.IsNullOrEmpty(reader["Sector"].ToString()) ? null : reader["Sector"].ToString(), 
                             
                            Estado = Convert.ToBoolean(reader["Estado"])
                        };
                    }
                }
                /*
                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var Entity = odata.vwaca_Familia.Where(q => q.IdEmpresa == IdEmpresa && q.IdAlumno == IdAlumno && q.Secuencia == Secuencia && q.Estado==true).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info_familia = new aca_Familia_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdAlumno = Entity.IdAlumno,
                        Secuencia = Entity.Secuencia,
                        IdPersona = Entity.IdPersona,
                        IdCatalogoPAREN = Entity.IdCatalogoPAREN,
                        Direccion = Entity.Direccion,
                        Telefono = Entity.Telefono,
                        Celular = Entity.Celular,
                        Correo = Entity.Correo,
                        SeFactura = Entity.SeFactura,
                        EsRepresentante = Entity.EsRepresentante,
                        IdTipoDocumento = Entity.IdTipoDocumento,
                        pe_Naturaleza = Entity.pe_Naturaleza,
                        pe_cedulaRuc = Entity.pe_cedulaRuc,
                        pe_nombre = Entity.pe_nombre,
                        pe_apellido = Entity.pe_apellido,
                        pe_nombreCompleto = Entity.pe_nombreCompleto,
                        pe_razonSocial = Entity.pe_razonSocial,
                        pe_sexo = Entity.pe_sexo,
                        IdEstadoCivil = Entity.IdEstadoCivil,
                        IdProfesion = Entity.IdProfesion ?? 0,
                        IdCatalogoFichaInst = Entity.IdCatalogoFichaInst??0,
                        EmpresaTrabajo = Entity.EmpresaTrabajo,
                        DireccionTrabajo = Entity.DireccionTrabajo,
                        TelefonoTrabajo = Entity.TelefonoTrabajo,
                        CargoTrabajo = Entity.CargoTrabajo,
                        AniosServicio = Entity.AniosServicio,
                        IngresoMensual = Entity.IngresoMensual,
                        VehiculoPropio = Entity.VehiculoPropio,
                        Marca = Entity.Marca,
                        Modelo = Entity.Modelo,
                        AnioVehiculo = Entity.AnioVehiculo,
                        CasaPropia = Entity.CasaPropia,
                        pe_fechaNacimiento = Entity.pe_fechaNacimiento,
                        CodCatalogoCONADIS = Entity.CodCatalogoCONADIS,
                        NumeroCarnetConadis = Entity.NumeroCarnetConadis,
                        PorcentajeDiscapacidad = Entity.PorcentajeDiscapacidad,
                        pe_telfono_Contacto = Entity.pe_telfono_Contacto,
                        IdReligion = Entity.IdReligion,
                        AsisteCentroCristiano = Entity.AsisteCentroCristiano,
                        EstaFallecido = Entity.EstaFallecido,
                        IdPais = Entity.IdPais,
                        IdProvincia = Entity.IdProvincia,
                        IdCiudad = Entity.IdCiudad,
                        IdParroquia = Entity.IdParroquia,
                        Cod_Region = Entity.Cod_Region,
                        Sector = Entity.Sector,
                        Estado = Entity.Estado
                    };
                }
                */
                return info_familia;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public aca_Familia_Info getInfo_ExistePersonaParentezco(int IdEmpresa, decimal IdAlumno, decimal IdPersona, int IdCatalogoPAREN)
        {
            try
            {
                aca_Familia_Info info_familia = new aca_Familia_Info();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("", connection);
                    command.CommandText = "SELECT f.IdEmpresa, f.IdAlumno, f.IdCatalogoPAREN, c.NomCatalogo, f.IdPersona, p.pe_Naturaleza, p.IdTipoDocumento, p.pe_cedulaRuc, p.pe_apellido, p.pe_nombre, p.pe_nombreCompleto, f.Direccion, f.Celular, f.Correo, f.SeFactura, p.pe_sexo, "
                    + " p.IdEstadoCivil, p.pe_fechaNacimiento, p.CodCatalogoSangre, p.CodCatalogoCONADIS, p.PorcentajeDiscapacidad, p.NumeroCarnetConadis, p.pe_telfono_Contacto, f.Secuencia, f.EsRepresentante, p.pe_razonSocial, p.IdProfesion, "
                    + " f.IdCatalogoFichaInst, f.EmpresaTrabajo, f.DireccionTrabajo, f.TelefonoTrabajo, f.CargoTrabajo, f.AniosServicio, f.IngresoMensual, f.VehiculoPropio, f.Marca, f.Modelo, f.CasaPropia, f.AnioVehiculo, p.IdReligion, p.AsisteCentroCristiano, "
                    + " f.EstaFallecido, p.IdGrupoEtnico, f.IdPais, f.Cod_Region, f.IdProvincia, f.IdCiudad, f.IdParroquia, f.Sector, f.Estado, f.Telefono "
                    + " FROM dbo.tb_persona AS p INNER JOIN "
                    + " dbo.aca_Familia AS f ON p.IdPersona = f.IdPersona LEFT OUTER JOIN "
                    + " dbo.aca_Catalogo AS c ON f.IdCatalogoPAREN = c.IdCatalogo "
                    + " WHERE f.IdEmpresa = " + IdEmpresa.ToString() + "and f.IdAlumno = " + IdAlumno.ToString() + "and f.IdPersona = " + IdPersona.ToString() + "and f.IdCatalogoPAREN = " + IdCatalogoPAREN.ToString() + "and f.Estado = 1 ";
                    var ResultValue = command.ExecuteScalar();

                    if (ResultValue == null)
                        return null;

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        info_familia = new aca_Familia_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdAlumno = Convert.ToDecimal(reader["IdAlumno"]),
                            Secuencia = Convert.ToInt32(reader["Secuencia"]),
                            IdPersona = Convert.ToDecimal(reader["IdPersona"]),
                            IdCatalogoPAREN = Convert.ToInt32(reader["IdCatalogoPAREN"]),
                            NomCatalogo = string.IsNullOrEmpty(reader["NomCatalogo"].ToString()) ? null : reader["NomCatalogo"].ToString(),
                            Direccion = string.IsNullOrEmpty(reader["Direccion"].ToString()) ? null : reader["Direccion"].ToString(),
                            Telefono = string.IsNullOrEmpty(reader["Telefono"].ToString()) ? null : reader["Telefono"].ToString(),
                            Celular = string.IsNullOrEmpty(reader["Celular"].ToString()) ? null : reader["Celular"].ToString(),
                            Correo = string.IsNullOrEmpty(reader["Correo"].ToString()) ? null : reader["Correo"].ToString(),
                            SeFactura = Convert.ToBoolean(reader["SeFactura"]),
                            EsRepresentante = Convert.ToBoolean(reader["EsRepresentante"]),
                            IdCatalogoFichaInst = string.IsNullOrEmpty(reader["IdCatalogoFichaInst"].ToString()) ? (int?)null : Convert.ToInt32(reader["IdCatalogoFichaInst"]),
                            EmpresaTrabajo = string.IsNullOrEmpty(reader["EmpresaTrabajo"].ToString()) ? null : reader["EmpresaTrabajo"].ToString(),
                            DireccionTrabajo = string.IsNullOrEmpty(reader["DireccionTrabajo"].ToString()) ? null : reader["DireccionTrabajo"].ToString(),
                            TelefonoTrabajo = string.IsNullOrEmpty(reader["TelefonoTrabajo"].ToString()) ? null : reader["TelefonoTrabajo"].ToString(),
                            CargoTrabajo = string.IsNullOrEmpty(reader["CargoTrabajo"].ToString()) ? null : reader["CargoTrabajo"].ToString(),
                            AniosServicio = string.IsNullOrEmpty(reader["AniosServicio"].ToString()) ? (int?)null : Convert.ToInt32(reader["AniosServicio"]),
                            IngresoMensual = string.IsNullOrEmpty(reader["IngresoMensual"].ToString()) ? (double?)null : Convert.ToDouble(reader["IngresoMensual"]),
                            VehiculoPropio = Convert.ToBoolean(reader["VehiculoPropio"]),
                            Marca = string.IsNullOrEmpty(reader["Marca"].ToString()) ? null : reader["Marca"].ToString(),
                            Modelo = string.IsNullOrEmpty(reader["Modelo"].ToString()) ? null : reader["Modelo"].ToString(),
                            CasaPropia = Convert.ToBoolean(reader["CasaPropia"]),
                            IdTipoDocumento = reader["IdTipoDocumento"].ToString(),
                            pe_Naturaleza = reader["pe_Naturaleza"].ToString(),
                            pe_cedulaRuc = reader["pe_cedulaRuc"].ToString(),
                            pe_nombre = string.IsNullOrEmpty(reader["pe_nombre"].ToString()) ? null : reader["pe_nombre"].ToString(),
                            pe_apellido = string.IsNullOrEmpty(reader["pe_apellido"].ToString()) ? null : reader["pe_apellido"].ToString(),
                            pe_nombreCompleto = reader["pe_nombreCompleto"].ToString(),
                            pe_sexo = string.IsNullOrEmpty(reader["pe_sexo"].ToString()) ? null : reader["pe_sexo"].ToString(),
                            IdEstadoCivil = string.IsNullOrEmpty(reader["IdEstadoCivil"].ToString()) ? null : reader["IdEstadoCivil"].ToString(),
                            pe_fechaNacimiento = string.IsNullOrEmpty(reader["pe_fechaNacimiento"].ToString()) ? (DateTime?)null : Convert.ToDateTime(reader["pe_fechaNacimiento"]),
                            EstaFallecido = Convert.ToBoolean(reader["EstaFallecido"]),
                            Estado = Convert.ToBoolean(reader["Estado"])
                        };
                    }
                }
                /*
                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var Entity = odata.vwaca_Familia.Where(q => q.IdEmpresa == IdEmpresa && q.IdAlumno == IdAlumno && q.IdPersona == IdPersona 
                    && q.IdCatalogoPAREN == IdCatalogoPAREN && q.Estado ==true).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info_familia = new aca_Familia_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdAlumno = Entity.IdAlumno,
                        Secuencia = Entity.Secuencia,
                        IdPersona = Entity.IdPersona,
                        IdCatalogoPAREN = Entity.IdCatalogoPAREN,
                        Direccion = Entity.Direccion,
                        Telefono = Entity.Telefono,
                        Celular = Entity.Celular,
                        Correo = Entity.Correo,
                        SeFactura = Entity.SeFactura,
                        IdTipoDocumento = Entity.IdTipoDocumento,
                        pe_Naturaleza = Entity.pe_Naturaleza,
                        pe_cedulaRuc = Entity.pe_cedulaRuc,
                        pe_nombre = Entity.pe_nombre,
                        pe_apellido = Entity.pe_apellido,
                        pe_nombreCompleto = Entity.pe_nombreCompleto,
                        pe_razonSocial =Entity.pe_razonSocial,
                        pe_sexo = Entity.pe_sexo,
                        IdEstadoCivil = Entity.IdEstadoCivil,
                        IdProfesion = Entity.IdProfesion??0,
                        IdCatalogoFichaInst = Entity.IdCatalogoFichaInst??0,
                        EmpresaTrabajo = Entity.EmpresaTrabajo,
                        DireccionTrabajo = Entity.DireccionTrabajo,
                        TelefonoTrabajo = Entity.TelefonoTrabajo,
                        CargoTrabajo = Entity.CargoTrabajo,
                        AniosServicio = Entity.AniosServicio,
                        IngresoMensual = Entity.IngresoMensual,
                        VehiculoPropio = Entity.VehiculoPropio,
                        Marca = Entity.Marca,
                        Modelo = Entity.Modelo,
                        AnioVehiculo = Entity.AnioVehiculo,
                        CasaPropia = Entity.CasaPropia,
                        pe_fechaNacimiento = Entity.pe_fechaNacimiento,
                        CodCatalogoCONADIS = Entity.CodCatalogoCONADIS,
                        NumeroCarnetConadis = Entity.NumeroCarnetConadis,
                        PorcentajeDiscapacidad = Entity.PorcentajeDiscapacidad,
                        pe_telfono_Contacto = Entity.pe_telfono_Contacto,
                        IdReligion = Entity.IdReligion,
                        AsisteCentroCristiano = Entity.AsisteCentroCristiano,
                        IdPais = Entity.IdPais,
                        IdProvincia = Entity.IdProvincia,
                        IdCiudad = Entity.IdCiudad,
                        IdParroquia = Entity.IdParroquia,
                        Cod_Region = Entity.Cod_Region,
                        Sector = Entity.Sector,
                        EstaFallecido = Entity.EstaFallecido,
                    };
                }
                */
                return info_familia;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public aca_Familia_Info get_info_x_num_cedula(int IdEmpresa, decimal IdAlumno, string pe_cedulaRuc)
        {
            try
            {
                aca_Familia_Info info = new aca_Familia_Info();

                EntitiesGeneral Context_general = new EntitiesGeneral();
                var Entity_per = Context_general.tb_persona.Where(q => q.pe_cedulaRuc == pe_cedulaRuc).FirstOrDefault();
                if (Entity_per == null)
                {
                    Context_general.Dispose();
                    return info;
                }

                EntitiesAcademico Context_academico = new EntitiesAcademico();
                var Entity_fam = Context_academico.vwaca_Familia.Where(q => q.IdEmpresa == IdEmpresa && q.IdAlumno == IdAlumno && q.IdPersona == Entity_per.IdPersona && q.Estado == true).FirstOrDefault();

                if (Entity_fam == null)
                {
                    info.IdPersona = Entity_per.IdPersona;
                    info.Direccion = Entity_per.pe_direccion;
                    info.Correo = Entity_per.pe_correo;
                    info.Celular = Entity_per.pe_celular;
                    info.pe_sexo = Entity_per.pe_sexo;
                    info.pe_Naturaleza = Entity_per.pe_Naturaleza;
                    info.IdTipoDocumento = Entity_per.IdTipoDocumento;
                    info.pe_apellido = Entity_per.pe_apellido;
                    info.pe_nombre = Entity_per.pe_nombre;
                    info.pe_nombreCompleto = Entity_per.pe_nombreCompleto;
                    info.pe_razonSocial = Entity_per.pe_razonSocial;
                    info.pe_telfono_Contacto = Entity_per.pe_telfono_Contacto;
                    info.CodCatalogoSangre = Entity_per.CodCatalogoSangre;
                    info.CodCatalogoCONADIS = Entity_per.CodCatalogoCONADIS;
                    info.NumeroCarnetConadis = Entity_per.NumeroCarnetConadis;
                    info.PorcentajeDiscapacidad = Entity_per.PorcentajeDiscapacidad;
                    info.pe_fechaNacimiento = Entity_per.pe_fechaNacimiento;
                    info.IdEstadoCivil = Entity_per.IdEstadoCivil;
                    info.IdProfesion = Entity_per.IdProfesion ?? 0;
                    info.IdReligion = Entity_per.IdReligion;
                    info.AsisteCentroCristiano = Entity_per.AsisteCentroCristiano;
                    info.Secuencia = 0;
                    Context_general.Dispose();
                    Context_academico.Dispose();
                    return info;
                }

                info = new aca_Familia_Info
                {
                    IdEmpresa = Entity_fam.IdEmpresa,
                    IdAlumno = Entity_fam.IdAlumno,
                    Secuencia = Entity_fam.Secuencia,
                    IdCatalogoPAREN = Entity_fam.IdCatalogoPAREN,
                    Direccion = Entity_fam.Direccion,
                    Correo = Entity_fam.Correo,
                    Telefono = Entity_fam.Telefono,
                    Celular = Entity_fam.Celular,
                    SeFactura = Entity_fam.SeFactura,
                    IdPersona = Entity_fam.IdPersona,
                    pe_apellido = Entity_fam.pe_apellido,
                    pe_nombre = Entity_fam.pe_nombre,
                    pe_razonSocial = Entity_fam.pe_razonSocial,
                    pe_Naturaleza = Entity_fam.pe_Naturaleza,
                    IdTipoDocumento = Entity_fam.IdTipoDocumento,
                    pe_cedulaRuc = Entity_fam.pe_cedulaRuc,
                    pe_sexo = Entity_fam.pe_sexo,
                    pe_fechaNacimiento = Entity_fam.pe_fechaNacimiento,
                    IdEstadoCivil = Entity_fam.IdEstadoCivil,
                    IdProfesion = Entity_fam.IdProfesion??0,
                    IdCatalogoFichaInst = Entity_fam.IdCatalogoFichaInst??0,
                    EmpresaTrabajo = Entity_fam.EmpresaTrabajo,
                    DireccionTrabajo = Entity_fam.DireccionTrabajo,
                    TelefonoTrabajo = Entity_fam.TelefonoTrabajo,
                    CargoTrabajo = Entity_fam.CargoTrabajo,
                    AniosServicio = Entity_fam.AniosServicio,
                    IngresoMensual = Entity_fam.IngresoMensual,
                    VehiculoPropio = Entity_fam.VehiculoPropio,
                    Marca = Entity_fam.Marca,
                    Modelo = Entity_fam.Modelo,
                    AnioVehiculo = Entity_fam.AnioVehiculo,
                    CasaPropia = Entity_fam.CasaPropia,
                    pe_nombreCompleto = Entity_fam.pe_nombreCompleto,
                    pe_telfono_Contacto = Entity_fam.pe_telfono_Contacto,
                    CodCatalogoSangre = Entity_fam.CodCatalogoSangre,
                    CodCatalogoCONADIS = Entity_fam.CodCatalogoCONADIS,
                    NumeroCarnetConadis = Entity_fam.NumeroCarnetConadis,
                    IdReligion = Entity_fam.IdReligion,
                    AsisteCentroCristiano = Entity_fam.AsisteCentroCristiano,
                    PorcentajeDiscapacidad = Entity_fam.PorcentajeDiscapacidad,
                    IdPais = Entity_fam.IdPais,
                    IdProvincia = Entity_fam.IdProvincia,
                    IdCiudad = Entity_fam.IdCiudad,
                    IdParroquia = Entity_fam.IdParroquia,
                    Cod_Region = Entity_fam.Cod_Region,
                    Sector = Entity_fam.Sector,
                    EstaFallecido = Entity_fam.EstaFallecido
                };

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int getSecuencia(int IdEmpresa, decimal IdAlumno)
        {
            try
            {
                int ID = 1;

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var cont = Context.aca_Familia.Where(q => q.IdEmpresa == IdEmpresa && q.IdAlumno==IdAlumno).Count();
                    if (cont > 0)
                        ID = Context.aca_Familia.Where(q => q.IdEmpresa == IdEmpresa && q.IdAlumno == IdAlumno).Max(q => q.Secuencia) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(aca_Familia_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var info_anio = odata_anio.getInfo_AnioEnCurso(info.IdEmpresa, 0);
                    var infoMatricula = odata_matricula.getInfo_ExisteMatricula(info.IdEmpresa, info_anio.IdAnio, info.IdAlumno);
                    var IdMatricula = (infoMatricula == null ? 0 : infoMatricula.IdMatricula);

                    var lst_familia = Context.aca_Familia.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdAlumno == info.IdAlumno).ToList();
                    if (info.SeFactura == true)
                    {
                        aca_Matricula EntityMatricula = Context.aca_Matricula.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdMatricula == IdMatricula && q.IdAlumno == info.IdAlumno);
                        if (EntityMatricula!=null)
                        {
                            EntityMatricula.IdPersonaF = info.IdPersona;
                        }

                        if (lst_familia.Count>0)
                        {
                            foreach (var item in lst_familia)
                            {
                                item.SeFactura = false;
                            }
                            Context.SaveChanges();
                        }      
                    }

                    if (info.EsRepresentante == true)
                    {
                        aca_Matricula EntityMatricula = Context.aca_Matricula.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdMatricula == IdMatricula && q.IdAlumno == info.IdAlumno);
                        if (EntityMatricula != null)
                        {
                            EntityMatricula.IdPersonaR = info.IdPersona;
                        }

                        if (lst_familia.Count > 0)
                        {
                            foreach (var item in lst_familia)
                            {
                                item.EsRepresentante = false;
                            }
                            Context.SaveChanges();
                        }
                    }

                    aca_Familia Entity = new aca_Familia
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdAlumno = info.IdAlumno,
                        Secuencia = info.Secuencia=getSecuencia(info.IdEmpresa, info.IdAlumno),
                        IdCatalogoPAREN = info.IdCatalogoPAREN,
                        IdPersona = info.IdPersona,
                        Direccion = info.Direccion,
                        Telefono = info.Telefono,
                        Celular = info.Celular,
                        Correo = info.Correo,
                        SeFactura = info.SeFactura,
                        IdCatalogoFichaInst = (info.IdCatalogoFichaInst == 0 ? null : info.IdCatalogoFichaInst),
                        EmpresaTrabajo = info.EmpresaTrabajo,
                        DireccionTrabajo = info.DireccionTrabajo,
                        TelefonoTrabajo = info.TelefonoTrabajo,
                        CargoTrabajo = info.CargoTrabajo,
                        AniosServicio = info.AniosServicio,
                        IngresoMensual = info.IngresoMensual,
                        VehiculoPropio = info.VehiculoPropio,
                        Marca = info.Marca,
                        Modelo = info.Modelo,
                        AnioVehiculo = info.AnioVehiculo,
                        CasaPropia = info.CasaPropia,
                        EsRepresentante = info.EsRepresentante,
                        EstaFallecido = info.EstaFallecido,
                        IdPais = info.IdPais,
                        IdProvincia = info.IdProvincia,
                        IdCiudad = info.IdCiudad,
                        IdParroquia = info.IdParroquia,
                        Cod_Region = info.Cod_Region,
                        Sector = info.Sector,
                        Estado = true,
                        IdUsuarioCreacion = info.IdUsuarioCreacion,
                        FechaCreacion = info.FechaCreacion = DateTime.Now
                    };
                    Context.aca_Familia.Add(Entity);

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public bool modificarDB(aca_Familia_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var info_anio = odata_anio.getInfo_AnioEnCurso(info.IdEmpresa, 0);
                    var infoMatricula = odata_matricula.getInfo_ExisteMatricula(info.IdEmpresa, info_anio.IdAnio, info.IdAlumno);
                    var IdMatricula = (infoMatricula==null ? 0 : infoMatricula.IdMatricula);

                    var lst_familia = Context.aca_Familia.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdAlumno == info.IdAlumno).ToList();
                    if (info.SeFactura == true)
                    {
                        aca_Matricula EntityMatricula = Context.aca_Matricula.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdMatricula == IdMatricula && q.IdAlumno == info.IdAlumno);
                        if (EntityMatricula != null)
                        {
                            EntityMatricula.IdPersonaF = info.IdPersona;
                        }

                        if (lst_familia.Count > 0)
                        {
                            foreach (var item in lst_familia)
                            {
                                aca_Familia Entity_Update = Context.aca_Familia.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdAlumno == info.IdAlumno && q.Secuencia == item.Secuencia);
                                Entity_Update.SeFactura = false;
                            }

                        }
                    }

                    if (info.EsRepresentante == true)
                    {
                        aca_Matricula EntityMatricula = Context.aca_Matricula.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdMatricula == IdMatricula && q.IdAlumno == info.IdAlumno);
                        if (EntityMatricula != null)
                        {
                            EntityMatricula.IdPersonaR = info.IdPersona;
                        }

                        if (lst_familia.Count > 0)
                        {
                            foreach (var item in lst_familia)
                            {
                                aca_Familia Entity_Update = Context.aca_Familia.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdAlumno == info.IdAlumno && q.Secuencia == item.Secuencia);
                                Entity_Update.EsRepresentante = false;
                            }
                        }
                    }

                    aca_Familia Entity = Context.aca_Familia.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdAlumno == info.IdAlumno && q.Secuencia == info.Secuencia);

                    if (Entity == null)
                    return false;

                    Entity.IdCatalogoPAREN = info.IdCatalogoPAREN;
                    Entity.IdPersona = info.IdPersona;
                    Entity.Direccion = info.Direccion;
                    Entity.Telefono = info.Telefono;
                    Entity.Celular = info.Celular;
                    Entity.Correo = info.Correo;
                    Entity.SeFactura = info.SeFactura;
                    Entity.EsRepresentante = info.EsRepresentante;
                    Entity.IdCatalogoFichaInst = (info.IdCatalogoFichaInst==0 ? null : info.IdCatalogoFichaInst);
                    Entity.EmpresaTrabajo = info.EmpresaTrabajo;
                    Entity.DireccionTrabajo = info.DireccionTrabajo;
                    Entity.TelefonoTrabajo = info.TelefonoTrabajo;
                    Entity.CargoTrabajo = info.CargoTrabajo;
                    Entity.AniosServicio = info.AniosServicio;
                    Entity.IngresoMensual = info.IngresoMensual;
                    Entity.VehiculoPropio = info.VehiculoPropio;
                    Entity.Marca = info.Marca;
                    Entity.Modelo = info.Modelo;
                    Entity.AnioVehiculo = info.AnioVehiculo;
                    Entity.CasaPropia = info.CasaPropia;
                    Entity.EstaFallecido = info.EstaFallecido;
                    Entity.IdPais = info.IdPais;
                    Entity.IdProvincia = info.IdProvincia;
                    Entity.IdCiudad = info.IdCiudad;
                    Entity.IdParroquia = info.IdParroquia;
                    Entity.Cod_Region = info.Cod_Region;
                    Entity.Sector = info.Sector;
                    Entity.IdUsuarioModificacion = info.IdUsuarioModificacion;
                    Entity.FechaModificacion = info.FechaModificacion = DateTime.Now;

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public bool anularDB(aca_Familia_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_Familia Entity = Context.aca_Familia.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdAlumno == info.IdAlumno && q.Secuencia == info.Secuencia);

                    if (Entity == null)
                        return false;

                    Entity.IdUsuarioAnulacion = info.IdUsuarioAnulacion;
                    Entity.FechaAnulacion = info.FechaAnulacion = DateTime.Now;
                    Entity.Estado = false;
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

        public aca_Familia_Info existe_familia(int IdEmpresa, decimal IdAlumno, string pe_cedulaRuc)
        {
            try
            {
                aca_Familia_Info info = new aca_Familia_Info();

                EntitiesAcademico Context_academico = new EntitiesAcademico();
                var Entity_fam = Context_academico.vwaca_Familia.Where(q => q.IdEmpresa == IdEmpresa && q.IdAlumno == IdAlumno && q.pe_cedulaRuc==pe_cedulaRuc && q.Estado == true).FirstOrDefault();

                if (Entity_fam == null)
                {
                    return null;
                }

                info = new aca_Familia_Info
                {
                    IdEmpresa = Entity_fam.IdEmpresa,
                    IdAlumno = Entity_fam.IdAlumno,
                    Secuencia = Entity_fam.Secuencia,
                    IdCatalogoPAREN = Entity_fam.IdCatalogoPAREN,
                    NomCatalogo = Entity_fam.NomCatalogo,
                    Direccion = Entity_fam.Direccion,
                    Correo = Entity_fam.Correo,
                    Telefono = Entity_fam.Telefono,
                    Celular = Entity_fam.Celular,
                    SeFactura = Entity_fam.SeFactura,
                    IdPersona = Entity_fam.IdPersona,
                    pe_apellido = Entity_fam.pe_apellido,
                    pe_nombre = Entity_fam.pe_nombre,
                    pe_razonSocial = Entity_fam.pe_razonSocial,
                    pe_Naturaleza = Entity_fam.pe_Naturaleza,
                    IdTipoDocumento = Entity_fam.IdTipoDocumento,
                    pe_cedulaRuc = Entity_fam.pe_cedulaRuc,
                    pe_nombreCompleto = Entity_fam.pe_nombreCompleto
                };

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
