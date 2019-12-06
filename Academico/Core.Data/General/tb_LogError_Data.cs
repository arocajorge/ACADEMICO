using Core.Data.Base;
using Core.Info.General;
using System;

namespace Core.Data.General
{
    public class tb_LogError_Data
    {
        public bool GuardarDB(tb_LogError_Info info)
        {
            try
            {
                using (EntitiesGeneral Context = new EntitiesGeneral())
                {
                    Context.tb_LogError.Add(new tb_LogError
                    {
                        Clase = info.Clase,
                        Descripcion = info.Descripcion,
                        Fecha = DateTime.Now,
                        IdUsuario = info.IdUsuario,
                        Metodo = info.Metodo,
                        InnerException = info.InnerException
                    });
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
