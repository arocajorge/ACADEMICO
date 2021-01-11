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
    public class aca_Admision_Data
    {
        public List<aca_Admision_Info> getList(int IdEmpresa, int IdAnio)
        {
            try
            {
                List<aca_Admision_Info> Lista = new List<aca_Admision_Info>();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();
                    #region Query
                    string query = "SELECT * FROM aca_Admision a "
                    + " WHERE a.IdEmpresa = " + IdEmpresa.ToString()
                    + " a.IdAnio = " + IdAnio.ToString();
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandTimeout = 0;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new aca_Admision_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdAdmision = Convert.ToDecimal(reader["IdAdmision"]),
                            IdSede = Convert.ToInt32(reader["IdSede"]),
                            IdAnio = Convert.ToInt32(reader["IdAnio"]),
                            IdJornada = Convert.ToInt32(reader["IdJornada"]),
                            IdNivel = Convert.ToInt32(reader["IdNivel"]),
                            IdCurso = Convert.ToInt32(reader["IdCurso"]),
                            Naturaleza_Aspirante = reader["Naturaleza_Aspirante"].ToString(),
                            IdTipoDocumento_Aspirante = reader["IdTipoDocumento_Aspirante"].ToString(),
                            CedulaRuc_Aspirante = reader["CedulaRuc_Aspirante"].ToString(),
                            Nombres_Aspirante = string.IsNullOrEmpty(reader["Nombres_Aspirante"].ToString()) ? null : reader["Nombres_Aspirante"].ToString(),
                            Apellidos_Aspirante = string.IsNullOrEmpty(reader["Apellidos_Aspirante"].ToString()) ? null : reader["Apellidos_Aspirante"].ToString(),
                            NombreCompleto_Aspirante = string.IsNullOrEmpty(reader["NombreCompleto_Aspirante"].ToString()) ? null : reader["NombreCompleto_Aspirante"].ToString(),
                            Direccion_Aspirante = string.IsNullOrEmpty(reader["Direccion_Aspirante"].ToString()) ? null : reader["Direccion_Aspirante"].ToString(),
                            Telefono_Aspirante = string.IsNullOrEmpty(reader["Telefono_Aspirante"].ToString()) ? null : reader["Telefono_Aspirante"].ToString(),
                            Celular_Aspirante = string.IsNullOrEmpty(reader["Celular_Aspirante"].ToString()) ? null : reader["Celular_Aspirante"].ToString(),
                            Correo_Aspirante = string.IsNullOrEmpty(reader["Correo_Aspirante"].ToString()) ? null : reader["Correo_Aspirante"].ToString(),
                            Sexo_Aspirante = string.IsNullOrEmpty(reader["Sexo_Aspirante"].ToString()) ? null : reader["Sexo_Aspirante"].ToString(),
                            FechaNacimiento_Aspirante = string.IsNullOrEmpty(reader["FechaNacimiento_Aspirante"].ToString()) ? (DateTime?)null : Convert.ToDateTime(reader["FechaNacimiento_Aspirante"]),
                            CodCatalogoSangre_Aspirante = string.IsNullOrEmpty(reader["Sexo_Aspirante"].ToString()) ? null : reader["Sexo_Aspirante"].ToString(),
                            CodCatalogoCONADIS_Aspirante = string.IsNullOrEmpty(reader["CodCatalogoCONADIS_Aspirante"].ToString()) ? null : reader["CodCatalogoCONADIS_Aspirante"].ToString(),
                            PorcentajeDiscapacidad_Aspirante = string.IsNullOrEmpty(reader["PorcentajeDiscapacidad_Aspirante"].ToString()) ? (double?)null : Convert.ToDouble(reader["PorcentajeDiscapacidad_Aspirante"]),
                            NumeroCarnetConadis_Aspirante = string.IsNullOrEmpty(reader["NumeroCarnetConadis_Aspirante"].ToString()) ? null : reader["NumeroCarnetConadis_Aspirante"].ToString(),
                            IdGrupoEtnico_Aspirante = string.IsNullOrEmpty(reader["IdGrupoEtnico_Aspirante"].ToString()) ? (int?)null : Convert.ToInt32(reader["IdGrupoEtnico_Aspirante"]),
                            IdReligion_Aspirante = string.IsNullOrEmpty(reader["IdReligion_Aspirante"].ToString()) ? (int?)null : Convert.ToInt32(reader["IdReligion_Aspirante"]),
                            AsisteCentroCristiano_Aspirante = string.IsNullOrEmpty(reader["AsisteCentroCristiano_Aspirante"].ToString()) ? false : Convert.ToBoolean(reader["AsisteCentroCristiano_Aspirante"]),
                            LugarNacimiento_Aspirante = string.IsNullOrEmpty(reader["LugarNacimiento_Aspirante"].ToString()) ? null : reader["LugarNacimiento_Aspirante"].ToString(),
                            IdPais_Aspirante = string.IsNullOrEmpty(reader["IdPais_Aspirante"].ToString()) ? null : reader["IdPais_Aspirante"].ToString(),
                            Cod_Region_Aspirante = string.IsNullOrEmpty(reader["Cod_Region_Aspirante"].ToString()) ? null : reader["Cod_Region_Aspirante"].ToString(),
                            IdProvincia_Aspirante = string.IsNullOrEmpty(reader["IdProvincia_Aspirante"].ToString()) ? null : reader["IdProvincia_Aspirante"].ToString(),
                            IdCiudad_Aspirante = string.IsNullOrEmpty(reader["IdCiudad_Aspirante"].ToString()) ? null : reader["IdCiudad_Aspirante"].ToString(),
                            IdParroquia_Aspirante = string.IsNullOrEmpty(reader["IdParroquia_Aspirante"].ToString()) ? null : reader["IdParroquia_Aspirante"].ToString(),
                            Sector_Aspirante = string.IsNullOrEmpty(reader["Sector_Aspirante"].ToString()) ? null : reader["Sector_Aspirante"].ToString(),
                            FechaIngreso_Aspirante = Convert.ToDateTime(reader["FechaIngreso_Aspirante"]),
                            IdCatalogoFichaTipoViv_Aspirante = Convert.ToInt32(reader["IdCatalogoFichaTipoViv_Aspirante"]),
                            IdCatalogoFichaViv_Aspirante = Convert.ToInt32(reader["IdCatalogoFichaViv_Aspirante"]),
                            IdCatalogoFichaAgua_Aspirante = Convert.ToInt32(reader["IdCatalogoFichaAgua_Aspirante"]),
                            TieneElectricidad_Aspirante = string.IsNullOrEmpty(reader["TieneHermanos_Aspirante"].ToString()) ? false : Convert.ToBoolean(reader["TieneHermanos_Aspirante"]),
                            TieneHermanos_Aspirante = string.IsNullOrEmpty(reader["TieneHermanos_Aspirante"].ToString()) ? false : Convert.ToBoolean(reader["TieneHermanos_Aspirante"]),
                            CantidadHermanos = string.IsNullOrEmpty(reader["CantidadHermanos"].ToString()) ? (int?)null : Convert.ToInt32(reader["CantidadHermanos"]),
                            IdCatalogoFichaMotivo_Aspirante = Convert.ToInt32(reader["IdCatalogoFichaMotivo_Aspirante"]),
                            IdCatalogoFichaInst_Aspirante = Convert.ToInt32(reader["IdCatalogoFichaInst_Aspirante"]),
                            IdCatalogoFichaFinanc_Aspirante = Convert.ToInt32(reader["IdCatalogoFichaFinanc_Aspirante"]),
                            IdCatalogoFichaVive_Aspirante = Convert.ToInt32(reader["IdCatalogoFichaVive_Aspirante"]),
                            OtroMotivoIngreso_Aspirante = string.IsNullOrEmpty(reader["OtroMotivoIngreso_Aspirante"].ToString()) ? null : reader["OtroMotivoIngreso_Aspirante"].ToString(),
                            OtroInformacionInst_Aspirante = string.IsNullOrEmpty(reader["OtroInformacionInst_Aspirante"].ToString()) ? null : reader["OtroInformacionInst_Aspirante"].ToString(),
                            OtroFinanciamiento_Aspirante = string.IsNullOrEmpty(reader["OtroFinanciamiento_Aspirante"].ToString()) ? null : reader["OtroFinanciamiento_Aspirante"].ToString(),
                            Estado = string.IsNullOrEmpty(reader["Estado"].ToString()) ? false : Convert.ToBoolean(reader["Estado"]),
                            IdCatalogoESTADM = Convert.ToInt32(reader["IdCatalogoESTADM"]),
                            /*Naturaleza_Padre = info.Naturaleza_Padre,
                            IdTipoDocumento_Padre = info.IdTipoDocumento_Padre,
                            CedulaRuc_Padre = info.CedulaRuc_Padre,
                            Nombres_Padre = info.Nombres_Padre,
                            Apellidos_Padre = info.Apellidos_Padre,
                            NombreCompleto_Padre = info.NombreCompleto_Padre,
                            RazonSocial_Padre = info.RazonSocial_Padre,
                            Direccion_Padre = info.Direccion_Padre,
                            Telefono_Padre = info.Telefono_Padre,
                            Celular_Padre = info.Celular_Padre,
                            Correo_Padre = info.Correo_Padre,
                            Sexo_Padre = (info.Sexo_Padre == "" ? null : info.Sexo_Padre),
                            FechaNacimiento_Padre = info.FechaNacimiento_Padre,
                            CodCatalogoCONADIS_Padre = (info.CodCatalogoCONADIS_Padre == "" ? null : info.CodCatalogoCONADIS_Padre),
                            PorcentajeDiscapacidad_Padre = info.PorcentajeDiscapacidad_Padre,
                            NumeroCarnetConadis_Padre = info.NumeroCarnetConadis_Padre,
                            IdGrupoEtnico_Padre = (info.IdGrupoEtnico_Padre == 0 ? (int?)null : info.IdGrupoEtnico_Padre),
                            IdReligion_Padre = (info.IdReligion_Padre == 0 ? (int?)null : info.IdReligion_Padre),
                            IdEstadoCivil_Padre = (info.IdEstadoCivil_Padre == "" ? null : info.IdEstadoCivil_Padre),
                            AsisteCentroCristiano_Padre = info.AsisteCentroCristiano_Padre,
                            IdPais_Padre = (info.IdPais_Padre == "" ? null : info.IdPais_Padre),
                            Cod_Region_Padre = (info.Cod_Region_Padre == "" ? null : info.Cod_Region_Padre),
                            IdProvincia_Padre = (info.IdProvincia_Padre == "" ? null : info.IdProvincia_Padre),
                            IdCiudad_Padre = (info.IdCiudad_Padre == "" ? null : info.IdCiudad_Padre),
                            IdParroquia_Padre = (info.IdParroquia_Padre == "" ? null : info.IdParroquia_Padre),
                            Sector_Padre = info.Sector_Padre,
                            IdCatalogoPAREN_Padre = info.IdCatalogoPAREN_Padre,
                            IdCatalogoFichaInst_Padre = (info.IdCatalogoFichaInst_Padre == 0 ? (int?)null : info.IdCatalogoFichaInst_Padre),
                            EmpresaTrabajo_Padre = info.EmpresaTrabajo_Padre,
                            IdProfesion_Padre = (info.IdProfesion_Padre == 0 ? (int?)null : info.IdCatalogoFichaInst_Padre),
                            DireccionTrabajo_Padre = info.DireccionTrabajo_Padre,
                            TelefonoTrabajo_Padre = info.TelefonoTrabajo_Padre,
                            CargoTrabajo_Padre = info.CargoTrabajo_Padre,
                            AniosServicio_Padre = info.AniosServicio_Padre,
                            IngresoMensual_Padre = info.IngresoMensual_Padre,
                            VehiculoPropio_Padre = info.VehiculoPropio_Padre,
                            Marca_Padre = info.Marca_Padre,
                            Modelo_Padre = info.Modelo_Padre,
                            AnioVehiculo_Padre = info.AnioVehiculo_Padre,
                            CasaPropia_Padre = info.CasaPropia_Padre,
                            EstaFallecido_Padre = info.EstaFallecido_Padre,
                            Naturaleza_Madre = info.Naturaleza_Madre,
                            IdTipoDocumento_Madre = info.IdTipoDocumento_Madre,
                            CedulaRuc_Madre = info.CedulaRuc_Madre,
                            Nombres_Madre = info.Nombres_Madre,
                            Apellidos_Madre = info.Apellidos_Madre,
                            NombreCompleto_Madre = info.NombreCompleto_Madre,
                            RazonSocial_Madre = info.RazonSocial_Madre,
                            Direccion_Madre = info.Direccion_Madre,
                            Telefono_Madre = info.Telefono_Madre,
                            Celular_Madre = info.Celular_Madre,
                            Correo_Madre = info.Correo_Madre,
                            Sexo_Madre = (info.Sexo_Madre == "" ? null : info.Sexo_Madre),
                            FechaNacimiento_Madre = info.FechaNacimiento_Madre,
                            CodCatalogoCONADIS_Madre = (info.CodCatalogoCONADIS_Madre == "" ? null : info.CodCatalogoCONADIS_Madre),
                            PorcentajeDiscapacidad_Madre = info.PorcentajeDiscapacidad_Madre,
                            NumeroCarnetConadis_Madre = info.NumeroCarnetConadis_Madre,
                            IdGrupoEtnico_Madre = (info.IdGrupoEtnico_Madre == 0 ? (int?)null : info.IdGrupoEtnico_Madre),
                            IdReligion_Madre = (info.IdReligion_Madre == 0 ? (int?)null : info.IdReligion_Madre),
                            IdEstadoCivil_Madre = (info.IdEstadoCivil_Madre == "" ? null : info.IdEstadoCivil_Madre),
                            AsisteCentroCristiano_Madre = info.AsisteCentroCristiano_Madre,
                            IdPais_Madre = (info.IdPais_Madre == "" ? null : info.IdPais_Madre),
                            Cod_Region_Madre = (info.Cod_Region_Madre == "" ? null : info.Cod_Region_Madre),
                            IdProvincia_Madre = (info.IdProvincia_Madre == "" ? null : info.IdProvincia_Madre),
                            IdCiudad_Madre = (info.IdCiudad_Madre == "" ? null : info.IdCiudad_Madre),
                            IdParroquia_Madre = (info.IdParroquia_Madre == "" ? null : info.IdParroquia_Madre),
                            Sector_Madre = info.Sector_Madre,
                            IdCatalogoPAREN_Madre = info.IdCatalogoPAREN_Madre,
                            IdCatalogoFichaInst_Madre = (info.IdCatalogoFichaInst_Madre == 0 ? null : info.IdCatalogoFichaInst_Madre),
                            EmpresaTrabajo_Madre = info.EmpresaTrabajo_Madre,
                            IdProfesion_Madre = (info.IdProfesion_Madre == 0 ? (int?)null : info.IdProfesion_Madre),
                            DireccionTrabajo_Madre = info.DireccionTrabajo_Madre,
                            TelefonoTrabajo_Madre = info.TelefonoTrabajo_Madre,
                            CargoTrabajo_Madre = info.CargoTrabajo_Madre,
                            AniosServicio_Madre = info.AniosServicio_Madre,
                            IngresoMensual_Madre = info.IngresoMensual_Madre,
                            VehiculoPropio_Madre = info.VehiculoPropio_Madre,
                            Marca_Madre = info.Marca_Madre,
                            Modelo_Madre = info.Modelo_Madre,
                            AnioVehiculo_Madre = info.AnioVehiculo_Madre,
                            CasaPropia_Madre = info.CasaPropia_Madre,
                            EstaFallecido_Madre = info.EstaFallecido_Madre,
                            Naturaleza_Representante = info.Naturaleza_Representante,
                            IdTipoDocumento_Representante = info.IdTipoDocumento_Representante,
                            CedulaRuc_Representante = info.CedulaRuc_Representante,
                            Nombres_Representante = info.Nombres_Representante,
                            Apellidos_Representante = info.Apellidos_Representante,
                            NombreCompleto_Representante = info.NombreCompleto_Representante,
                            RazonSocial_Representante = info.RazonSocial_Representante,
                            Direccion_Representante = info.Direccion_Representante,
                            Telefono_Representante = info.Telefono_Representante,
                            Celular_Representante = info.Celular_Representante,
                            Correo_Representante = info.Correo_Representante,
                            Sexo_Representante = (info.Sexo_Representante == "" ? null : info.Sexo_Representante),
                            FechaNacimiento_Representante = info.FechaNacimiento_Representante,
                            CodCatalogoCONADIS_Representante = (info.CodCatalogoCONADIS_Representante == "" ? null : info.CodCatalogoCONADIS_Representante),
                            PorcentajeDiscapacidad_Representante = info.PorcentajeDiscapacidad_Representante,
                            NumeroCarnetConadis_Representante = info.NumeroCarnetConadis_Representante,
                            IdGrupoEtnico_Representante = (info.IdGrupoEtnico_Representante == 0 ? (int?)null : info.IdGrupoEtnico_Representante),
                            IdReligion_Representante = (info.IdReligion_Representante == 0 ? (int?)null : info.IdReligion_Representante),
                            IdEstadoCivil_Representante = (info.IdEstadoCivil_Representante == "" ? null : info.IdEstadoCivil_Representante),
                            AsisteCentroCristiano_Representante = info.AsisteCentroCristiano_Representante,
                            IdPais_Representante = (info.IdPais_Representante == "" ? null : info.IdPais_Representante),
                            Cod_Region_Representante = (info.Cod_Region_Representante == "" ? null : info.Cod_Region_Representante),
                            IdProvincia_Representante = (info.IdProvincia_Representante == "" ? null : info.IdProvincia_Representante),
                            IdCiudad_Representante = (info.IdCiudad_Representante == "" ? null : info.IdCiudad_Representante),
                            IdParroquia_Representante = (info.IdParroquia_Representante == "" ? null : info.IdParroquia_Representante),
                            Sector_Representante = info.Sector_Representante,
                            IdCatalogoPAREN_Representante = info.IdCatalogoPAREN_Representante,
                            IdCatalogoFichaInst_Representante = (info.IdCatalogoFichaInst_Representante == 0 ? (int?)null : info.IdCatalogoFichaInst_Representante),
                            EmpresaTrabajo_Representante = info.EmpresaTrabajo_Representante,
                            IdProfesion_Representante = (info.IdProfesion_Representante == 0 ? (int?)null : info.IdCatalogoFichaInst_Representante),
                            DireccionTrabajo_Representante = info.DireccionTrabajo_Representante,
                            TelefonoTrabajo_Representante = info.TelefonoTrabajo_Representante,
                            CargoTrabajo_Representante = info.CargoTrabajo_Representante,
                            AniosServicio_Representante = info.AniosServicio_Representante,
                            IngresoMensual_Representante = info.IngresoMensual_Representante,
                            VehiculoPropio_Representante = info.VehiculoPropio_Representante,
                            Marca_Representante = info.Marca_Representante,
                            Modelo_Representante = info.Modelo_Representante,
                            AnioVehiculo_Representante = info.AnioVehiculo_Representante,
                            CasaPropia_Representante = info.CasaPropia_Representante,
                            EstaFallecido_Representante = info.EstaFallecido_Representante,
                            SueldoPadre = info.SueldoPadre,
                            SueldoMadre = info.SueldoMadre,
                            OtroIngresoPadre = info.OtroIngresoPadre,
                            OtroIngresoMadre = info.OtroIngresoMadre,
                            GastoAlimentacion = info.GastoAlimentacion,
                            GastoEducacion = info.GastoEducacion,
                            GastoServicioBasico = info.GastoServicioBasico,
                            GastoSalud = info.GastoSalud,
                            GastoArriendo = info.GastoArriendo,
                            GastoPrestamo = info.GastoPrestamo,
                            OtroGasto = info.OtroGasto,
                            FechaCreacion = info.FechaCreacion = DateTime.Now*/
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
        public decimal getId(int IdEmpresa)
        {
            try
            {
                decimal ID = 1;

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var cont = Context.aca_Admision.Where(q => q.IdEmpresa == IdEmpresa).Count();
                    if (cont > 0)
                        ID = Context.aca_Admision.Where(q => q.IdEmpresa == IdEmpresa).Max(q => q.IdAdmision) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(aca_Admision_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_Admision Entity = new aca_Admision
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdAdmision = info.IdAdmision = getId(info.IdEmpresa),
                        IdSede = info.IdSede,
                        IdAnio = info.IdAnio,
                        IdJornada = info.IdJornada,
                        IdNivel = info.IdNivel,
                        IdCurso = info.IdCurso,
                        Naturaleza_Aspirante = info.Naturaleza_Aspirante,
                        IdTipoDocumento_Aspirante = info.IdTipoDocumento_Aspirante,
                        CedulaRuc_Aspirante = info.CedulaRuc_Aspirante,
                        Nombres_Aspirante = info.Nombres_Aspirante,
                        Apellidos_Aspirante = info.Apellidos_Aspirante,
                        NombreCompleto_Aspirante = info.NombreCompleto_Aspirante,
                        Direccion_Aspirante = info.Direccion_Aspirante,
                        Telefono_Aspirante = info.Telefono_Aspirante,
                        Celular_Aspirante = info.Celular_Aspirante,
                        Correo_Aspirante = info.Correo_Aspirante,
                        Sexo_Aspirante = (info.Sexo_Aspirante== "" ? null : info.Sexo_Aspirante),
                        FechaNacimiento_Aspirante = info.FechaNacimiento_Aspirante??(DateTime?)null,
                        CodCatalogoSangre_Aspirante = (info.CodCatalogoSangre_Aspirante == "" ? null : info.CodCatalogoSangre_Aspirante),
                        CodCatalogoCONADIS_Aspirante = (info.CodCatalogoCONADIS_Aspirante == "" ? null : info.CodCatalogoCONADIS_Aspirante),
                        PorcentajeDiscapacidad_Aspirante = info.PorcentajeDiscapacidad_Aspirante,
                        NumeroCarnetConadis_Aspirante = info.NumeroCarnetConadis_Aspirante,
                        IdGrupoEtnico_Aspirante = (info.IdGrupoEtnico_Aspirante==0 ? (int?)null : info.IdGrupoEtnico_Aspirante),
                        IdReligion_Aspirante = (info.IdReligion_Aspirante == 0 ? (int?)null : info.IdReligion_Aspirante),
                        AsisteCentroCristiano_Aspirante = info.AsisteCentroCristiano_Aspirante??false,
                        LugarNacimiento_Aspirante = string.IsNullOrEmpty(info.LugarNacimiento_Aspirante)?null : info.LugarNacimiento_Aspirante,
                        IdPais_Aspirante = (info.IdPais_Aspirante == "" ? null : info.IdPais_Aspirante),
                        Cod_Region_Aspirante = (info.Cod_Region_Aspirante == "" ? null : info.Cod_Region_Aspirante),
                        IdProvincia_Aspirante = (info.IdProvincia_Aspirante == "" ? null : info.IdProvincia_Aspirante),
                        IdCiudad_Aspirante = (info.IdCiudad_Aspirante == "" ? null : info.IdCiudad_Aspirante),
                        IdParroquia_Aspirante = (info.IdParroquia_Aspirante == "" ? null : info.IdParroquia_Aspirante),
                        Sector_Aspirante = info.Sector_Aspirante,
                        FechaIngreso_Aspirante = DateTime.Now,
                        IdCatalogoFichaTipoViv_Aspirante = info.IdCatalogoFichaTipoViv_Aspirante,
                        IdCatalogoFichaViv_Aspirante = info.IdCatalogoFichaViv_Aspirante,
                        IdCatalogoFichaAgua_Aspirante = info.IdCatalogoFichaAgua_Aspirante,
                        TieneElectricidad_Aspirante = info.TieneElectricidad_Aspirante,
                        TieneHermanos_Aspirante = info.TieneHermanos_Aspirante,
                        CantidadHermanos = info.CantidadHermanos,
                        IdCatalogoFichaMotivo_Aspirante = info.IdCatalogoFichaMotivo_Aspirante,
                        IdCatalogoFichaInst_Aspirante = info.IdCatalogoFichaInst_Aspirante,
                        IdCatalogoFichaFinanc_Aspirante = info.IdCatalogoFichaFinanc_Aspirante,
                        IdCatalogoFichaVive_Aspirante = info.IdCatalogoFichaVive_Aspirante,
                        OtroMotivoIngreso_Aspirante = info.OtroMotivoIngreso_Aspirante,
                        OtroInformacionInst_Aspirante = info.OtroInformacionInst_Aspirante,
                        OtroFinanciamiento_Aspirante = info.OtroFinanciamiento_Aspirante,
                        Naturaleza_Padre = info.Naturaleza_Padre,
                        IdTipoDocumento_Padre = info.IdTipoDocumento_Padre,
                        CedulaRuc_Padre = info.CedulaRuc_Padre,
                        Nombres_Padre = info.Nombres_Padre,
                        Apellidos_Padre = info.Apellidos_Padre,
                        NombreCompleto_Padre = info.NombreCompleto_Padre,
                        RazonSocial_Padre = info.RazonSocial_Padre,
                        Direccion_Padre = info.Direccion_Padre,
                        Telefono_Padre = info.Telefono_Padre,
                        Celular_Padre = info.Celular_Padre,
                        Correo_Padre = info.Correo_Padre,
                        Sexo_Padre = (info.Sexo_Padre=="" ? null : info.Sexo_Padre),
                        FechaNacimiento_Padre = info.FechaNacimiento_Padre,
                        CodCatalogoCONADIS_Padre = (info.CodCatalogoCONADIS_Padre == "" ? null : info.CodCatalogoCONADIS_Padre),
                        PorcentajeDiscapacidad_Padre = info.PorcentajeDiscapacidad_Padre,
                        NumeroCarnetConadis_Padre = info.NumeroCarnetConadis_Padre,
                        IdGrupoEtnico_Padre = (info.IdGrupoEtnico_Padre == 0 ? (int?)null : info.IdGrupoEtnico_Padre),
                        IdReligion_Padre = (info.IdReligion_Padre == 0 ? (int?)null : info.IdReligion_Padre),
                        IdEstadoCivil_Padre = (info.IdEstadoCivil_Padre == "" ? null : info.IdEstadoCivil_Padre),
                        AsisteCentroCristiano_Padre = info.AsisteCentroCristiano_Padre,
                        IdPais_Padre = (info.IdPais_Padre == "" ? null : info.IdPais_Padre),
                        Cod_Region_Padre = (info.Cod_Region_Padre == "" ? null : info.Cod_Region_Padre),
                        IdProvincia_Padre = (info.IdProvincia_Padre == "" ? null : info.IdProvincia_Padre),
                        IdCiudad_Padre = (info.IdCiudad_Padre == "" ? null : info.IdCiudad_Padre),
                        IdParroquia_Padre = (info.IdParroquia_Padre == "" ? null : info.IdParroquia_Padre),
                        Sector_Padre = info.Sector_Padre,
                        IdCatalogoPAREN_Padre = info.IdCatalogoPAREN_Padre,
                        IdCatalogoFichaInst_Padre = (info.IdCatalogoFichaInst_Padre == 0 ? (int?)null : info.IdCatalogoFichaInst_Padre),
                        EmpresaTrabajo_Padre = info.EmpresaTrabajo_Padre,
                        IdProfesion_Padre = (info.IdProfesion_Padre == 0 ? (int?)null : info.IdCatalogoFichaInst_Padre),
                        DireccionTrabajo_Padre = info.DireccionTrabajo_Padre,
                        TelefonoTrabajo_Padre = info.TelefonoTrabajo_Padre,
                        CargoTrabajo_Padre = info.CargoTrabajo_Padre,
                        AniosServicio_Padre = info.AniosServicio_Padre,
                        IngresoMensual_Padre = info.IngresoMensual_Padre,
                        VehiculoPropio_Padre = info.VehiculoPropio_Padre,
                        Marca_Padre = info.Marca_Padre,
                        Modelo_Padre = info.Modelo_Padre,
                        AnioVehiculo_Padre = info.AnioVehiculo_Padre,
                        CasaPropia_Padre = info.CasaPropia_Padre,
                        EstaFallecido_Padre = info.EstaFallecido_Padre,
                        Naturaleza_Madre = info.Naturaleza_Madre,
                        IdTipoDocumento_Madre = info.IdTipoDocumento_Madre,
                        CedulaRuc_Madre = info.CedulaRuc_Madre,
                        Nombres_Madre = info.Nombres_Madre,
                        Apellidos_Madre = info.Apellidos_Madre,
                        NombreCompleto_Madre = info.NombreCompleto_Madre,
                        RazonSocial_Madre = info.RazonSocial_Madre,
                        Direccion_Madre = info.Direccion_Madre,
                        Telefono_Madre = info.Telefono_Madre,
                        Celular_Madre = info.Celular_Madre,
                        Correo_Madre = info.Correo_Madre,
                        Sexo_Madre = (info.Sexo_Madre=="" ? null : info.Sexo_Madre),
                        FechaNacimiento_Madre = info.FechaNacimiento_Madre,
                        CodCatalogoCONADIS_Madre = (info.CodCatalogoCONADIS_Madre == "" ? null : info.CodCatalogoCONADIS_Madre),
                        PorcentajeDiscapacidad_Madre = info.PorcentajeDiscapacidad_Madre,
                        NumeroCarnetConadis_Madre = info.NumeroCarnetConadis_Madre,
                        IdGrupoEtnico_Madre = (info.IdGrupoEtnico_Madre == 0 ? (int?)null : info.IdGrupoEtnico_Madre),
                        IdReligion_Madre = (info.IdReligion_Madre == 0 ? (int?)null : info.IdReligion_Madre),
                        IdEstadoCivil_Madre = (info.IdEstadoCivil_Madre == "" ? null : info.IdEstadoCivil_Madre),
                        AsisteCentroCristiano_Madre = info.AsisteCentroCristiano_Madre,
                        IdPais_Madre = (info.IdPais_Madre == "" ? null : info.IdPais_Madre),
                        Cod_Region_Madre = (info.Cod_Region_Madre == "" ? null : info.Cod_Region_Madre),
                        IdProvincia_Madre = (info.IdProvincia_Madre == "" ? null : info.IdProvincia_Madre),
                        IdCiudad_Madre = (info.IdCiudad_Madre == "" ? null : info.IdCiudad_Madre),
                        IdParroquia_Madre = (info.IdParroquia_Madre == "" ? null : info.IdParroquia_Madre),
                        Sector_Madre = info.Sector_Madre,
                        IdCatalogoPAREN_Madre = info.IdCatalogoPAREN_Madre,
                        IdCatalogoFichaInst_Madre = (info.IdCatalogoFichaInst_Madre == 0 ? null : info.IdCatalogoFichaInst_Madre),
                        EmpresaTrabajo_Madre = info.EmpresaTrabajo_Madre,
                        IdProfesion_Madre = (info.IdProfesion_Madre == 0 ? (int?)null : info.IdProfesion_Madre),
                        DireccionTrabajo_Madre = info.DireccionTrabajo_Madre,
                        TelefonoTrabajo_Madre = info.TelefonoTrabajo_Madre,
                        CargoTrabajo_Madre = info.CargoTrabajo_Madre,
                        AniosServicio_Madre = info.AniosServicio_Madre,
                        IngresoMensual_Madre = info.IngresoMensual_Madre,
                        VehiculoPropio_Madre = info.VehiculoPropio_Madre,
                        Marca_Madre = info.Marca_Madre,
                        Modelo_Madre = info.Modelo_Madre,
                        AnioVehiculo_Madre = info.AnioVehiculo_Madre,
                        CasaPropia_Madre = info.CasaPropia_Madre,
                        EstaFallecido_Madre = info.EstaFallecido_Madre,
                        Naturaleza_Representante = info.Naturaleza_Representante,
                        IdTipoDocumento_Representante = info.IdTipoDocumento_Representante,
                        CedulaRuc_Representante = info.CedulaRuc_Representante,
                        Nombres_Representante = info.Nombres_Representante,
                        Apellidos_Representante = info.Apellidos_Representante,
                        NombreCompleto_Representante = info.NombreCompleto_Representante,
                        RazonSocial_Representante = info.RazonSocial_Representante,
                        Direccion_Representante = info.Direccion_Representante,
                        Telefono_Representante = info.Telefono_Representante,
                        Celular_Representante = info.Celular_Representante,
                        Correo_Representante = info.Correo_Representante,
                        Sexo_Representante = (info.Sexo_Representante == "" ? null : info.Sexo_Representante),
                        FechaNacimiento_Representante = info.FechaNacimiento_Representante,
                        CodCatalogoCONADIS_Representante = (info.CodCatalogoCONADIS_Representante == "" ? null : info.CodCatalogoCONADIS_Representante),
                        PorcentajeDiscapacidad_Representante = info.PorcentajeDiscapacidad_Representante,
                        NumeroCarnetConadis_Representante = info.NumeroCarnetConadis_Representante,
                        IdGrupoEtnico_Representante = (info.IdGrupoEtnico_Representante == 0 ? (int?)null : info.IdGrupoEtnico_Representante),
                        IdReligion_Representante = (info.IdReligion_Representante == 0 ? (int?)null : info.IdReligion_Representante),
                        IdEstadoCivil_Representante = (info.IdEstadoCivil_Representante == "" ? null : info.IdEstadoCivil_Representante),
                        AsisteCentroCristiano_Representante = info.AsisteCentroCristiano_Representante,
                        IdPais_Representante = (info.IdPais_Representante == "" ? null : info.IdPais_Representante),
                        Cod_Region_Representante = (info.Cod_Region_Representante == "" ? null : info.Cod_Region_Representante),
                        IdProvincia_Representante = (info.IdProvincia_Representante == "" ? null : info.IdProvincia_Representante),
                        IdCiudad_Representante = (info.IdCiudad_Representante == "" ? null : info.IdCiudad_Representante),
                        IdParroquia_Representante = (info.IdParroquia_Representante == "" ? null : info.IdParroquia_Representante),
                        Sector_Representante = info.Sector_Representante,
                        IdCatalogoPAREN_Representante = info.IdCatalogoPAREN_Representante,
                        IdCatalogoFichaInst_Representante = (info.IdCatalogoFichaInst_Representante == 0 ? (int?)null : info.IdCatalogoFichaInst_Representante),
                        EmpresaTrabajo_Representante = info.EmpresaTrabajo_Representante,
                        IdProfesion_Representante = (info.IdProfesion_Representante == 0 ? (int?)null : info.IdCatalogoFichaInst_Representante),
                        DireccionTrabajo_Representante = info.DireccionTrabajo_Representante,
                        TelefonoTrabajo_Representante = info.TelefonoTrabajo_Representante,
                        CargoTrabajo_Representante = info.CargoTrabajo_Representante,
                        AniosServicio_Representante = info.AniosServicio_Representante,
                        IngresoMensual_Representante = info.IngresoMensual_Representante,
                        VehiculoPropio_Representante = info.VehiculoPropio_Representante,
                        Marca_Representante = info.Marca_Representante,
                        Modelo_Representante = info.Modelo_Representante,
                        AnioVehiculo_Representante = info.AnioVehiculo_Representante,
                        CasaPropia_Representante = info.CasaPropia_Representante,
                        EstaFallecido_Representante = info.EstaFallecido_Representante,
                        SueldoPadre = info.SueldoPadre,
                        SueldoMadre = info.SueldoMadre,
                        OtroIngresoPadre = info.OtroIngresoPadre,
                        OtroIngresoMadre = info.OtroIngresoMadre,
                        GastoAlimentacion = info.GastoAlimentacion,
                        GastoEducacion = info.GastoEducacion,
                        GastoServicioBasico = info.GastoServicioBasico,
                        GastoSalud = info.GastoSalud,
                        GastoArriendo = info.GastoArriendo,
                        GastoPrestamo = info.GastoPrestamo,
                        OtroGasto = info.OtroGasto,
                        IdCatalogoESTADM = info.IdCatalogoESTADM,
                        Estado = true,
                        FechaCreacion = info.FechaCreacion = DateTime.Now
                    };
                    Context.aca_Admision.Add(Entity);

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception EX)
            {

                throw;
            }
        }
        public aca_Admision_Info getInfo_CedulaAspirante(int IdEmpresa, string CedulaRuc_Aspirante)
        {
            try
            {
                aca_Admision_Info info = new aca_Admision_Info();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("", connection);
                    command.CommandText = "SELECT * FROM aca_Admision a "
                    + " WHERE a.IdEmpresa = " + IdEmpresa.ToString() + " and a.CedulaRuc_Aspirante = " + CedulaRuc_Aspirante.ToString();
                    var ResultValue = command.ExecuteScalar();

                    if (ResultValue == null)
                        return null;

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        info = new aca_Admision_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdAdmision = Convert.ToDecimal(reader["IdAdmision"]),
                            CedulaRuc_Aspirante = reader["CedulaRuc_Aspirante"].ToString(),
                            Nombres_Aspirante = reader["Nombres_Aspirante"].ToString(),
                            Apellidos_Aspirante = reader["Apellidos_Aspirante"].ToString(),
                            NombreCompleto_Aspirante = reader["NombreCompleto_Aspirante"].ToString(),
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
        public aca_Admision_Info consultaAdmision(int IdEmpresa, int IdAnio, string CedulaRuc_Aspirante)
        {
            try
            {
                aca_Admision_Info info = new aca_Admision_Info();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("", connection);
                    command.CommandText = "SELECT a.IdEmpresa, a.IdAdmision, a.NombreCompleto_Aspirante, a.CedulaRuc_Aspirante, a.FechaIngreso_Aspirante, b.NomCatalogo EstadoAdmision FROM aca_Admision a "
                    + " left join aca_Catalogo b on a.IdCatalogoESTADM=b.IdCatalogo"
                    + " WHERE a.IdEmpresa = " + IdEmpresa.ToString() + " and a.CedulaRuc_Aspirante = " + CedulaRuc_Aspirante.ToString();
                    var ResultValue = command.ExecuteScalar();

                    if (ResultValue == null)
                        return null;

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        info = new aca_Admision_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdAdmision = Convert.ToDecimal(reader["IdAdmision"]),
                            CedulaRuc_Aspirante = reader["CedulaRuc_Aspirante"].ToString(),
                            NombreCompleto_Aspirante = reader["NombreCompleto_Aspirante"].ToString(),
                            FechaIngreso_Aspirante = Convert.ToDateTime(reader["FechaIngreso_Aspirante"]),
                            EstadoAdmision = reader["EstadoAdmision"].ToString(),
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
    }
}
