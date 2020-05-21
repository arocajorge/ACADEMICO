using Core.Data.Academico;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Academico
{
    public class aca_Jornada_Bus
    {
        aca_Jornada_Data odata = new aca_Jornada_Data();
        aca_AnioLectivo_Data odata_anio = new aca_AnioLectivo_Data();
        aca_AnioLectivo_NivelAcademico_Jornada_Data odata_nivel_jornada = new aca_AnioLectivo_NivelAcademico_Jornada_Data();
        public List<aca_Jornada_Info> GetList(int IdEmpresa, bool MostrarAnulados)
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

        public List<aca_Jornada_Info> GetList(int IdEmpresa, int IdAnio, int IdSede, int IdNivel)
        {
            try
            {
                return odata.getList(IdEmpresa, IdAnio, IdSede, IdNivel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<aca_Jornada_Info> GetList_Combos(int IdEmpresa, int IdAnio, int IdSede)
        {
            try
            {
                return odata.getList_Combos(IdEmpresa, IdAnio, IdSede);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public aca_Jornada_Info GetInfo(int IdEmpresa, int IdJornada)
        {
            try
            {
                return odata.getInfo(IdEmpresa, IdJornada);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int GetOrden(int IdEmpresa)
        {
            try
            {
                return odata.getOrden(IdEmpresa);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool GuardarDB(aca_Jornada_Info info)
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

        public bool ModificarDB(aca_Jornada_Info info)
        {
            try
            {
                var lst_anios = odata_anio.getList_update(info.IdEmpresa);
                if (odata.modificarDB(info))
                {
                    if (lst_anios.Count > 0)
                    {
                        foreach (var item in lst_anios)
                        {
                            var lst_nivel_jornada = odata_nivel_jornada.GetList_Update(info.IdEmpresa, item.IdAnio, info.IdJornada);
                            if (lst_nivel_jornada.Count > 0)
                            {
                                foreach (var info_nivel_jornada in lst_nivel_jornada)
                                {
                                    info_nivel_jornada.NomJornada = info.NomJornada;
                                }

                                return (odata_nivel_jornada.modificarDB(lst_nivel_jornada));
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool AnularDB(aca_Jornada_Info info)
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
