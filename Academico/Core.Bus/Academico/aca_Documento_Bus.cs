using Core.Data.Academico;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Academico
{
    public class aca_Documento_Bus
    {
        aca_Documento_Data odata = new aca_Documento_Data();
        aca_AnioLectivo_Data odata_anio = new aca_AnioLectivo_Data();
        aca_AnioLectivo_Curso_Documento_Data odata_curso_documento = new aca_AnioLectivo_Curso_Documento_Data();
        public List<aca_Documento_Info> GetList(int IdEmpresa, bool MostrarAnulados)
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

        public aca_Documento_Info GetInfo(int IdEmpresa, int IdCurso)
        {
            try
            {
                return odata.getInfo(IdEmpresa, IdCurso);
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

        public bool GuardarDB(aca_Documento_Info info)
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

        public bool ModificarDB(aca_Documento_Info info)
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
                            var lst_curso_documento = odata_curso_documento.getList_Update(info.IdEmpresa, item.IdAnio, info.IdDocumento);
                            if (lst_curso_documento.Count > 0)
                            {
                                foreach (var info_curso_documento in lst_curso_documento)
                                {
                                    info_curso_documento.NomDocumento = info.NomDocumento;
                                    info_curso_documento.OrdenDocumento = info.OrdenDocumento;
                                }

                                return (odata_curso_documento.modificarDB(lst_curso_documento));
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

        public bool AnularDB(aca_Documento_Info info)
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
