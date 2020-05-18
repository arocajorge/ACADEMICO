using Core.Data.Banco;
using Core.Data.General;
using Core.Info.Banco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Banco
{
    public class ba_ArchivoRecaudacion_Bus
    {
        ba_ArchivoRecaudacion_Data odata = new ba_ArchivoRecaudacion_Data();
        public List<ba_ArchivoRecaudacion_Info> GetList(int IdEmpresa, DateTime fechaini, DateTime fechafin, bool mostrar_anulados)
        {
            try
            {
                return odata.GetList(IdEmpresa, fechaini, fechafin, mostrar_anulados);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ba_ArchivoRecaudacion_Info GetInfo(int IdEmpresa, decimal IdArchivo)
        {
            try
            {
                return odata.GetInfo(IdEmpresa, IdArchivo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool GuardarDB(ba_ArchivoRecaudacion_Info info)
        {
            try
            {
                return odata.GuardarDB(info);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool ModificarDB(ba_ArchivoRecaudacion_Info info)
        {
            try
            {
                return odata.ModificarDB(info);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool ModificarSecuenciaDescargaDB(ba_ArchivoRecaudacion_Info info)
        {
            try
            {
                return odata.ModificarSecuenciaDescargaDB(info);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool AnularDB(ba_ArchivoRecaudacion_Info info)
        {
            try
            {
                return odata.AnularDB(info);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
