using Core.Bus.General;
using Core.Data.Academico;
using Core.Data.General;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Academico
{
    public class aca_Familia_Bus
    {
        aca_Familia_Data odata = new aca_Familia_Data();
        tb_persona_Bus bus_persona = new tb_persona_Bus();
        tb_persona_Data odata_per = new tb_persona_Data();
        public List<aca_Familia_Info> GetList(int IdEmpresa, int IdAlumno)
        {
            try
            {
                return odata.getList(IdEmpresa, IdAlumno);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public aca_Familia_Info GetListTipo(int IdEmpresa, int IdAlumno, int IdCatalogoPAREN)
        {
            try
            {
                return odata.getListTipo(IdEmpresa, IdAlumno, IdCatalogoPAREN);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public aca_Familia_Info GetInfo(int IdEmpresa, int IdAlumno, int Secuencia)
        {
            try
            {
                return odata.getInfo(IdEmpresa, IdAlumno, Secuencia);
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
                return odata.get_info_x_num_cedula(IdEmpresa, IdAlumno, pe_cedulaRuc);
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
                var grabar = false;

                if (bus_persona.validar_existe_cedula(info.pe_cedulaRuc) == 0)
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
                    odata_per.modificarDB(info.info_persona);
                    grabar = true;
                }

                if (grabar == true)
                {
                    if (odata.guardarDB(info))
                    {
                        return true;

                    }
                }

                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool modificarDB(aca_Familia_Info info)
        {
            try
            {
                var grabar = false;

                if (bus_persona.validar_existe_cedula(info.pe_cedulaRuc) == 0)
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
                    odata_per.modificarDB(info.info_persona);
                    grabar = true;
                }

                if (grabar == true)
                {
                    if (odata.modificarDB(info))
                    {
                        return true;

                    }
                }

                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool anularDB(aca_Familia_Info info)
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
