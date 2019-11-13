﻿using Core.Bus.General;
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
    public class aca_Alumno_Bus
    {
        tb_persona_Bus bus_persona = new tb_persona_Bus();
        aca_Alumno_Data odata = new aca_Alumno_Data();
        tb_persona_Data odata_per = new tb_persona_Data();
        aca_Familia_Data odata_fam = new aca_Familia_Data();
        public List<aca_Alumno_Info> GetList(int IdEmpresa, bool MostrarAnulados)
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

        public aca_Alumno_Info get_info_x_num_cedula(int IdEmpresa, string pe_cedulaRuc)
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

        public aca_Alumno_Info GetInfo(int IdEmpresa, int IdProfesor)
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

        public bool GuardarDB(aca_Alumno_Info info)
        {
            try
            {
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
                    odata_per.modificarDB(info.info_persona_alumno);
                    grabar_alumno = true;
                }


                if (grabar_alumno == true)
                {
                    if (odata.guardarDB(info))
                    {
                        grabar_padre = true;

                    }
                }

                if (grabar_padre == true)
                {
                    if (bus_persona.validar_existe_cedula(info.info_persona_padre.pe_cedulaRuc) == 0)
                    {
                        info.info_persona_padre = odata_per.armar_info(info.info_persona_padre);
                        if (odata_per.guardarDB(info.info_persona_padre))
                        {
                            info.info_persona_padre.IdPersona = info.info_persona_padre.IdPersona;
                        }
                    }
                    else
                    {
                        if (odata_per.modificarDB(info.info_persona_padre))
                        {
                            info.info_persona_padre.IdPersona = info.info_persona_padre.IdPersona;
                        }
                    }

                    var info_fam_padre = new aca_Familia_Info
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdAlumno = info.IdAlumno,
                        IdCatalogoPAREN = Convert.ToInt32(cl_enumeradores.eTipoParentezco.PAPA),
                        IdPersona = info.info_persona_padre.IdPersona,
                        Direccion = info.Direccion_padre,
                        Celular = info.Celular_padre,
                        Correo = info.Correo_padre,
                        SeFactura = info.SeFactura_padre,
                        EsRepresentante = info.EsRepresentante_padre,
                        IdUsuarioCreacion = info.IdUsuarioCreacion,
                        FechaCreacion = info.FechaCreacion = DateTime.Now
                    };

                    if (odata_fam.guardarDB(info_fam_padre))
                    {
                        grabar_madre = true;
                    }
                }     

                if (grabar_madre == true)
                {
                    if (bus_persona.validar_existe_cedula(info.info_persona_madre.pe_cedulaRuc) == 0)
                    {
                        info.info_persona_madre = odata_per.armar_info(info.info_persona_madre);
                        if (odata_per.guardarDB(info.info_persona_madre))
                        {
                            info.info_persona_madre.IdPersona = info.info_persona_madre.IdPersona;
                        }
                    }
                    else
                    {
                        if (odata_per.modificarDB(info.info_persona_padre))
                        {
                            info.info_persona_madre.IdPersona = info.info_persona_madre.IdPersona;
                        }
                    }

                    var info_fam_madre = new aca_Familia_Info
                        {
                            IdEmpresa = info.IdEmpresa,
                            IdAlumno = info.IdAlumno,
                            IdCatalogoPAREN = Convert.ToInt32(cl_enumeradores.eTipoParentezco.MAMA),
                            IdPersona = info.info_persona_madre.IdPersona,
                            Direccion = info.Direccion_madre,
                            Celular = info.Celular_madre,
                            Correo = info.Correo_madre,
                            SeFactura = info.SeFactura_madre,
                            EsRepresentante = info.EsRepresentante_madre,
                            IdUsuarioCreacion = info.IdUsuarioCreacion,
                            FechaCreacion = info.FechaCreacion = DateTime.Now
                        };

                    if (odata_fam.modificarDB(info_fam_madre))
                    {
                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public bool ModificarDB(aca_Alumno_Info info)
        {
            try
            {
                var grabar_alumno = false;
                var grabar_padre = false;
                var grabar_madre = false;


                if (odata_per.modificarDB(info.info_persona_alumno))
                {
                    grabar_alumno = true;
                }

                if (grabar_alumno == true)
                {
                    if (odata.modificarDB(info))
                    {
                        grabar_padre = true;
                    }
                }

                if (grabar_padre == true)
                {
                    if (bus_persona.validar_existe_cedula(info.info_persona_padre.pe_cedulaRuc) == 0)
                    {
                        info.info_persona_padre = odata_per.armar_info(info.info_persona_padre);
                        if (odata_per.guardarDB(info.info_persona_padre))
                        {
                            info.info_persona_padre.IdPersona = info.info_persona_padre.IdPersona;
                        }
                    }
                    else
                    {
                        if (odata_per.modificarDB(info.info_persona_padre))
                        {
                            info.info_persona_padre.IdPersona = info.info_persona_padre.IdPersona;
                        }
                    }

                    var info_fam_padre = new aca_Familia_Info
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdAlumno = info.IdAlumno,
                        IdCatalogoPAREN = Convert.ToInt32(cl_enumeradores.eTipoParentezco.PAPA),
                        IdPersona = info.info_persona_padre.IdPersona,
                        Direccion = info.Direccion_padre,
                        Celular = info.Celular_padre,
                        Correo = info.Correo_padre,
                        SeFactura = info.SeFactura_padre,
                        EsRepresentante = info.EsRepresentante_padre,
                        IdUsuarioCreacion = info.IdUsuarioCreacion,
                        FechaCreacion = info.FechaCreacion = DateTime.Now
                    };

                    //var existe_padre = odata_fam.getInfo();
                    if (odata_fam.modificarDB(info_fam_padre))
                    {
                        grabar_madre = true;
                    }
                }

                if (grabar_madre == true)
                {
                    if (bus_persona.validar_existe_cedula(info.info_persona_madre.pe_cedulaRuc) == 0)
                    {
                        info.info_persona_madre = odata_per.armar_info(info.info_persona_madre);
                        if (odata_per.guardarDB(info.info_persona_madre))
                        {
                            info.info_persona_madre.IdPersona = info.info_persona_madre.IdPersona;
                        }
                    }
                    else
                    {
                        if (odata_per.modificarDB(info.info_persona_madre))
                        {
                            info.info_persona_madre.IdPersona = info.info_persona_madre.IdPersona;
                        }
                    }

                    var info_fam_madre = new aca_Familia_Info
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdAlumno = info.IdAlumno,
                        IdCatalogoPAREN = Convert.ToInt32(cl_enumeradores.eTipoParentezco.MAMA),
                        IdPersona = info.info_persona_madre.IdPersona,
                        Direccion = info.Direccion_madre,
                        Celular = info.Celular_madre,
                        Correo = info.Correo_madre,
                        SeFactura = info.SeFactura_madre,
                        EsRepresentante = info.EsRepresentante_madre,
                        IdUsuarioCreacion = info.IdUsuarioCreacion,
                        FechaCreacion = info.FechaCreacion = DateTime.Now
                    };

                    if (odata_fam.modificarDB(info_fam_madre))
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public bool AnularDB(aca_Alumno_Info info)
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
