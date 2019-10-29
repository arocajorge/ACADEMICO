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

        public aca_Profesor_Info GetInfo(int IdEmpresa, int IdProfesor)
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
                    grabar = true;
                    //grabar = odata_per.modificarDB(info.info_persona);
                }


                if (grabar == true)
                {
                    return odata.guardarDB(info);
                }

                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ModificarDB(aca_Profesor_Info info)
        {
            try
            {
                return odata.modificarDB(info);
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
