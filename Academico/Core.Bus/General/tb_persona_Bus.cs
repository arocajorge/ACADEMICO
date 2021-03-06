﻿using Core.Data.General;
using Core.Info.General;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.General
{
    public class tb_persona_Bus
    {
        tb_persona_Data odata = new tb_persona_Data();
        public decimal validar_existe_cedula(string pe_CedulaRuc)
        {
            try
            {
                return odata.validar_existe_cedula(pe_CedulaRuc);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public decimal validar_existe_cedula(string IdTipoDocumento, string pe_CedulaRuc, decimal IdPersona)
        {
            try
            {
                return odata.validar_existe_cedula(IdTipoDocumento, pe_CedulaRuc, IdPersona);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<tb_persona_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args, int IdEmpresa, string IdTipoPersona)
        {
            return odata.get_list_bajo_demanda(args, IdEmpresa, IdTipoPersona);
        }

        public tb_persona_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args, int IdEmpresa, string IdTipoPersona)
        {
            return odata.get_info_bajo_demanda(args, IdEmpresa, IdTipoPersona);
        }

        public tb_persona_Info get_info(int IdEmpresa, string IdTipoPersona, decimal IdEntidad)
        {
            return odata.get_info(IdEmpresa, IdTipoPersona, IdEntidad);
        }

        public List<tb_persona_Info> get_list(bool mostrar_anulados)
        {
            try
            {
                return odata.get_list(mostrar_anulados);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public tb_persona_Info get_info(decimal IdPersona)
        {
            try
            {
                return odata.get_info(IdPersona);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public tb_persona_Info get_info_x_num_cedula(string pe_cedulaRuc)
        {
            try
            {
                return odata.get_info_x_num_cedula(pe_cedulaRuc);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool guardarDB(tb_persona_Info info)
        {
            try
            {
                return odata.guardarDB(info);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool modificarDB(tb_persona_Info info)
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

        public bool anularDB(tb_persona_Info info)
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
