using Core.Data.Academico;
using Core.Info.Academico;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Academico
{
    public class aca_Sede_Bus
    {
        aca_Sede_Data odata = new aca_Sede_Data();
        aca_AnioLectivo_Data odata_anio = new aca_AnioLectivo_Data();
        aca_AnioLectivo_Sede_NivelAcademico_Data odata_sede_nivel = new aca_AnioLectivo_Sede_NivelAcademico_Data();

        public List<aca_Sede_Info> GetList(int IdEmpresa, bool MostrarAnulados)
        {
            try
            {
                return odata.GetList(IdEmpresa, MostrarAnulados);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<aca_Sede_Info> GetList(int IdEmpresa, int IdAnio)
        {
            try
            {
                return odata.GetList(IdEmpresa, IdAnio);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public aca_Sede_Info GetInfo(int IdEmpresa, int IdSede)
        {
            try
            {
                return odata.GetInfo(IdEmpresa, IdSede);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<aca_Sede_Info> GetListSinEmpresa(bool MostrarAnulados)
        {
            try
            {
                return odata.GetListSinEmpresa(MostrarAnulados);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<aca_Sede_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args, int IdEmpresa)
        {
            return odata.get_list_bajo_demanda(args, IdEmpresa);
        }

        public aca_Sede_Info get_info_bajo_demanda(int IdEmpresa, ListEditItemRequestedByValueEventArgs args)
        {
            return odata.get_info_bajo_demanda(IdEmpresa, args);
        }

        public bool guardarDB(aca_Sede_Info info)
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

        public bool modificarDB(aca_Sede_Info info)
        {
            try
            {
                var lst_anios = odata_anio.getList_update(info.IdEmpresa);
                if (odata.modificarDB(info))
                {                   
                    if (lst_anios.Count >0)
                    {
                        foreach (var item in lst_anios)
                        {
                            var lst_sede_nivel = odata_sede_nivel.GetList_Update(info.IdEmpresa, item.IdAnio, info.IdSede);
                            if (lst_sede_nivel.Count > 0)
                            {
                                foreach (var info_sede_nivel in lst_sede_nivel)
                                {
                                    info_sede_nivel.NomSede = info.NomSede;                                  
                                }

                                return (odata_sede_nivel.modificarDB(lst_sede_nivel));
                            }
                        }
                    }
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool anularDB(aca_Sede_Info info)
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
