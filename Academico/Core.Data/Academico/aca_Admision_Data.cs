using Core.Data.Base;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_Admision_Data
    {
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
                        LugarNacimiento_Aspirante = info.LugarNacimiento_Aspirante,
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
    }
}
