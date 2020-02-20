using Core.Data.Academico;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Academico
{
    public class aca_MatriculaCondicionalParrafo_Bus
    {
        aca_MatriculaCondicionalParrafo_Data odata = new aca_MatriculaCondicionalParrafo_Data();

        public List<aca_MatriculaCondicionalParrafo_Info> GetList()
        {
            try
            {
                return odata.GetList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public aca_MatriculaCondicionalParrafo_Info GetInfo(int Id)
        {
            try
            {
                return odata.GetInfo(Id); 
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool GuardarDB(aca_MatriculaCondicionalParrafo_Info info)
        {
            try
            {
                return odata.GuardarDB(info);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ModificarDB(aca_MatriculaCondicionalParrafo_Info info)
        {
            try
            {
                return odata.ModificarDB(info);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool AnularDB(aca_MatriculaCondicionalParrafo_Info info)
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
