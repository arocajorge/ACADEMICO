﻿using Core.Data.General;
using Core.Info.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.General
{
    public class tb_GrupoEtnico_Bus
    {
        tb_GrupoEtnico_Data odata = new tb_GrupoEtnico_Data();
        public List<tb_GrupoEtnico_Info> GetList(bool MostrarAnulados)
        {
            try
            {
                return odata.getList(MostrarAnulados);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public tb_GrupoEtnico_Info GetInfo(int IdGrupoEtnico)
        {
            try
            {
                return odata.getInfo(IdGrupoEtnico);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool GuardarDB(tb_GrupoEtnico_Info info)
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

        public bool ModificarDB(tb_GrupoEtnico_Info info)
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

        public bool AnularDB(tb_GrupoEtnico_Info info)
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
