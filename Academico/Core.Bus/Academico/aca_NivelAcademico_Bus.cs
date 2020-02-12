using Core.Data.Academico;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Academico
{
    public class aca_NivelAcademico_Bus
    {
        aca_NivelAcademico_Data odata = new aca_NivelAcademico_Data();
        aca_AnioLectivo_Data odata_anio = new aca_AnioLectivo_Data();
        aca_AnioLectivo_Sede_NivelAcademico_Data odata_sede_nivel = new aca_AnioLectivo_Sede_NivelAcademico_Data();

        public List<aca_NivelAcademico_Info> GetList(int IdEmpresa, bool MostrarAnulados)
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

        public List<aca_NivelAcademico_Info> GetList(int IdEmpresa, int IdAnio, int IdSede)
        {
            try
            {
                return odata.getList(IdEmpresa, IdAnio, IdSede);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public aca_NivelAcademico_Info GetInfo(int IdEmpresa, int IdNivel)
        {
            try
            {
                return odata.getInfo(IdEmpresa, IdNivel);
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

        public bool GuardarDB(aca_NivelAcademico_Info info)
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

        public bool ModificarDB(aca_NivelAcademico_Info info)
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
                            var lst_sede_nivel = odata_sede_nivel.GetList_Update_Nivel(info.IdEmpresa, item.IdAnio, info.IdNivel);
                            if (lst_sede_nivel.Count > 0)
                            {
                                foreach (var info_sede_nivel in lst_sede_nivel)
                                {
                                    info_sede_nivel.NomNivel = info.NomNivel;
                                    info_sede_nivel.OrdenNivel = info.Orden;
                                }

                                return (odata_sede_nivel.modificarDB(lst_sede_nivel));
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

        public bool AnularDB(aca_NivelAcademico_Info info)
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
