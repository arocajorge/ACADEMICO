﻿using Core.Data.General;
using Core.Info.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.General
{
   public class tb_visor_video_aca_Bus
    {
        tb_visor_video_aca_Data odata = new tb_visor_video_aca_Data();
        public List<tb_visor_video_aca_Info> get_list(bool mostrar_anulados)
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

        public tb_visor_video_aca_Info get_info(string Cod_video)
        {
            try
            {
                return odata.get_info(Cod_video);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(tb_visor_video_aca_Info info)
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
        public bool si_existe(string Cod_video)
        {
            try
            {
                return odata.si_existe(Cod_video);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(tb_visor_video_aca_Info info)
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

        public bool anularDB(tb_visor_video_aca_Info info)
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
