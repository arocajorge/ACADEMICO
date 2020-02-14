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
    public class aca_Paralelo_Bus
    {
        aca_Paralelo_Data odata = new aca_Paralelo_Data();
        aca_AnioLectivo_Data odata_anio = new aca_AnioLectivo_Data();
        aca_AnioLectivo_Curso_Paralelo_Data odata_curso_paralelo = new aca_AnioLectivo_Curso_Paralelo_Data();

        public List<aca_Paralelo_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args, int IdEmpresa, int IdAnio, int IdSede, int IdNivel, int IdJornada, int IdCurso)
        {
            try
            {
                return odata.get_list_bajo_demanda(args, IdEmpresa, IdAnio, IdSede, IdNivel, IdJornada, IdCurso);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public aca_Paralelo_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args, int IdEmpresa)
        {
            try
            {
                return odata.get_info_bajo_demanda(args, IdEmpresa);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<aca_Paralelo_Info> GetList(int IdEmpresa, bool MostrarAnulados)
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
        public List<aca_Paralelo_Info> GetList(int IdEmpresa, int IdAnio, int IdSede, int IdNivel, int IdJornada, int IdCurso)
        {
            try
            {
                return odata.getList(IdEmpresa, IdAnio, IdSede, IdNivel, IdJornada, IdCurso);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public aca_Paralelo_Info GetInfo(int IdEmpresa, int IdParalelo)
        {
            try
            {
                return odata.getInfo(IdEmpresa, IdParalelo);
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

        public aca_Paralelo_Info ExisteCodigo(int IdEmpresa, string CodigoParalelo)
        {
            try
            {
                return odata.existeCodigo(IdEmpresa, CodigoParalelo);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool GuardarDB(aca_Paralelo_Info info)
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

        public bool ModificarDB(aca_Paralelo_Info info)
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
                            var lst_curso_paralelo = odata_curso_paralelo.getList_Update(info.IdEmpresa, item.IdAnio, info.IdParalelo);
                            if (lst_curso_paralelo.Count > 0)
                            {
                                foreach (var info_curso_paralelo in lst_curso_paralelo)
                                {
                                    info_curso_paralelo.CodigoParalelo = info.CodigoParalelo;
                                    info_curso_paralelo.NomParalelo = info.NomParalelo;
                                    info_curso_paralelo.OrdenParalelo = info.OrdenParalelo;
                                }

                                return (odata_curso_paralelo.modificarDB(lst_curso_paralelo));
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

        public bool AnularDB(aca_Paralelo_Info info)
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
