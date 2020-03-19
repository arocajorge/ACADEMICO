using Core.Bus.General;
using Core.Data.Academico;
using Core.Data.General;
using Core.Info.Academico;
using Core.Info.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Academico
{
    public class aca_Profesor_Bus
    {
        aca_Profesor_Data odata = new aca_Profesor_Data();
        tb_persona_Data odata_per = new tb_persona_Data();

        public List<aca_Profesor_Info> GetList(int IdEmpresa, bool MostrarAnulados)
        {
            try
            {
                return odata.getList(IdEmpresa, MostrarAnulados);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public aca_Profesor_Info GetInfo(int IdEmpresa, decimal IdProfesor)
        {
            try
            {
                return odata.getInfo(IdEmpresa, IdProfesor);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public aca_Profesor_Info GetInfo_x_Usuario(int IdEmpresa, string IdUsuario)
        {
            try
            {
                return odata.getInfo_x_Usuario(IdEmpresa, IdUsuario);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public decimal GetId(int IdEmpresa)
        {
            try
            {
                return odata.getId(IdEmpresa);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public aca_Profesor_Info get_info_x_num_cedula(int IdEmpresa, string pe_cedulaRuc)
        {
            try
            {
                return odata.get_info_x_num_cedula(IdEmpresa, pe_cedulaRuc);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool GuardarDB(aca_Profesor_Info info)
        {
            try
            {
                tb_persona_Bus bus_persona = new tb_persona_Bus();
                var grabar = false;

                //var persona = new tb_persona_Info
                //{
                //    IdPersona =info.IdPersona,
                //    pe_nombre = info.pe_nombre,
                //    pe_apellido = info.pe_apellido,
                //    pe_nombreCompleto = info.pe_nombreCompleto,
                //    pe_cedulaRuc = info.pe_cedulaRuc,
                //    pe_Naturaleza = info.pe_Naturaleza,
                //    IdTipoDocumento = info.IdTipoDocumento,
                //    pe_razonSocial = info.pe_razonSocial,
                //    pe_direccion = info.Direccion,
                //    pe_telfono_Contacto = info.Telefonos,
                //    pe_celular = info.pe_celular,
                //    pe_correo = info.Correo,
                //    pe_fechaNacimiento = info.pe_fechaNacimiento,
                //    IdEstadoCivil = info.IdEstadoCivil,
                //    pe_sexo = info.pe_sexo,
                //    CodCatalogoCONADIS = info.CodCatalogoCONADIS,
                //    NumeroCarnetConadis = info.NumeroCarnetConadis,
                //    PorcentajeDiscapacidad = info.PorcentajeDiscapacidad
                //};

                if (bus_persona.validar_existe_cedula(info.info_persona.pe_cedulaRuc) == 0)
                {
                    info.info_persona = odata_per.armar_info(info.info_persona);
                    if (odata_per.guardarDB(info.info_persona))
                    {
                        info.IdPersona = info.info_persona.IdPersona;
                        grabar = true;
                    }
                }
                else
                {
                    var data_persona = new tb_persona_Info();
                    if (info.IdPersona == 0)
                    {
                        data_persona = odata_per.get_info_x_num_cedula(info.info_persona.pe_cedulaRuc);
                        info.IdPersona = data_persona.IdPersona;
                    }

                    if (odata_per.modificarDB(info.info_persona))
                    {
                        grabar = true;
                    }
                }


                if (grabar == true)
                {
                    return odata.guardarDB(info);
                }

                return false;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public bool ModificarDB(aca_Profesor_Info info)
        {
            try
            {
                tb_persona_Bus bus_persona = new tb_persona_Bus();
                var grabar = false;

                //var persona = new tb_persona_Info
                //{
                //    IdPersona = info.IdPersona,
                //    pe_nombre = info.pe_nombre,
                //    pe_apellido = info.pe_apellido,
                //    pe_nombreCompleto = info.pe_nombreCompleto,
                //    pe_cedulaRuc = info.pe_cedulaRuc,
                //    pe_Naturaleza = info.pe_Naturaleza,
                //    IdTipoDocumento = info.IdTipoDocumento,
                //    pe_razonSocial = info.pe_razonSocial,
                //    pe_direccion = info.Direccion,
                //    pe_telfono_Contacto = info.Telefonos,
                //    pe_celular = info.pe_celular,
                //    pe_correo = info.Correo,
                //    pe_fechaNacimiento = info.pe_fechaNacimiento,
                //    IdEstadoCivil = info.IdEstadoCivil,
                //    CodCatalogoCONADIS = (info.CodCatalogoCONADIS=="" ? null : info.CodCatalogoCONADIS),
                //    NumeroCarnetConadis = info.NumeroCarnetConadis,
                //    PorcentajeDiscapacidad = info.PorcentajeDiscapacidad,
                //    pe_sexo = info.pe_sexo,
                //    IdProfesion = info.IdProfesion
                //};

                if (odata_per.modificarDB(info.info_persona))
                {
                    grabar = true;
                }

                if (grabar == true)
                {
                    return odata.modificarDB(info);
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool AnularDB(aca_Profesor_Info info)
        {
            try
            {
                return odata.anularDB(info);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
