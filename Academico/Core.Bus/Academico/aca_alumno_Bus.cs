using Core.Bus.General;
using Core.Data.Academico;
using Core.Data.General;
using Core.Info.Academico;
using Core.Info.Helps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Academico
{
    public class aca_alumno_Bus
    {
        aca_alumno_Data odata = new aca_alumno_Data();
        tb_persona_Data odata_per = new tb_persona_Data();
        aca_familia_Data odata_fam = new aca_familia_Data();
        public List<aca_alumno_Info> GetList(int IdEmpresa, bool MostrarAnulados)
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

        public aca_alumno_Info get_info_x_num_cedula(int IdEmpresa, string pe_cedulaRuc)
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

        public aca_alumno_Info GetInfo(int IdEmpresa, int IdProfesor)
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

        public bool GuardarDB(aca_alumno_Info info)
        {
            try
            {
                tb_persona_Bus bus_persona = new tb_persona_Bus();
                var grabar_alumno = false;
                var grabar_padre = false;
                var grabar_madre = false;

                if (bus_persona.validar_existe_cedula(info.pe_cedulaRuc) == 0)
                {
                    info.info_persona_alumno = odata_per.armar_info(info.info_persona_alumno);
                    if (odata_per.guardarDB(info.info_persona_alumno))
                    {
                        info.IdPersona = info.info_persona_alumno.IdPersona;
                        grabar_alumno = true;
                    }
                }
                else
                {
                    grabar_alumno = true;
                }


                if (grabar_alumno == true)
                {
                    if (odata.guardarDB(info))
                    {
                        if (bus_persona.validar_existe_cedula(info.info_persona_padre.pe_cedulaRuc) == 0)
                        {
                            info.info_persona_padre = odata_per.armar_info(info.info_persona_padre);
                            if (odata_per.guardarDB(info.info_persona_padre))
                            {
                                info.info_persona_padre.IdPersona = info.info_persona_padre.IdPersona;
                                grabar_padre = true;
                            }
                        }
                        else
                        {
                            grabar_padre = true;
                        }

                        if (grabar_padre == true)
                        {
                            var info_fam_padre = new aca_familia_Info
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdAlumno = info.IdAlumno,
                                IdCatalogoPAREN = Convert.ToInt32(cl_enumeradores.Parentezco.PAPA),
                                IdPersona = info.info_persona_padre.IdPersona,
                                Direccion = info.Direccion_padre,
                                Celular = info.Celular_padre,
                                Correo = info.Correo_padre,
                                //SeFactura = info.SeFactura,
                                IdUsuarioCreacion = info.IdUsuarioCreacion,
                                FechaCreacion = info.FechaCreacion = DateTime.Now
                            };

                            if (odata_fam.guardarDB(info_fam_padre))
                            {
                                if (bus_persona.validar_existe_cedula(info.info_persona_madre.pe_cedulaRuc) == 0)
                                {
                                    info.info_persona_madre = odata_per.armar_info(info.info_persona_madre);
                                    if (odata_per.guardarDB(info.info_persona_madre))
                                    {
                                        info.info_persona_madre.IdPersona = info.info_persona_madre.IdPersona;
                                        grabar_madre = true;
                                    }
                                }
                                else
                                {
                                    grabar_madre = true;
                                }
                            }

                            if (grabar_madre == true)
                            {
                                var info_fam_madre = new aca_familia_Info
                                {
                                    IdEmpresa = info.IdEmpresa,
                                    IdAlumno = info.IdAlumno,
                                    IdCatalogoPAREN = Convert.ToInt32(cl_enumeradores.Parentezco.MAMA),
                                    IdPersona = info.info_persona_padre.IdPersona,
                                    Direccion = info.Direccion_madre,
                                    Celular = info.Celular_madre,
                                    Correo = info.Correo_madre,
                                    //SeFactura = info.SeFactura,
                                    IdUsuarioCreacion = info.IdUsuarioCreacion,
                                    FechaCreacion = info.FechaCreacion = DateTime.Now
                                };

                                return odata_fam.guardarDB(info_fam_madre);
                            }
                        }
                    }
                }

                return false;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public bool ModificarDB(aca_alumno_Info info)
        {
            try
            {
                tb_persona_Bus bus_persona = new tb_persona_Bus();
                var grabar = false;

                if (odata_per.modificarDB(info.info_persona_alumno))
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

        public bool AnularDB(aca_alumno_Info info)
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
