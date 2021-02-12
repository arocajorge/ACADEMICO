using Core.Data.Base;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_parametro_Data
    {
        public aca_parametro_Info get_info(int IdEmpresa)
        {
            try
            {
                aca_parametro_Info info = new aca_parametro_Info();
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_parametro Entity = Context.aca_parametro.FirstOrDefault(q => q.IdEmpresa == IdEmpresa);
                    if (Entity == null) return null;
                    info = new aca_parametro_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        RutaImagen_Alumno = Entity.RutaImagen_Alumno,
                        RutaImagen_Profesor = Entity.RutaImagen_Profesor,
                        RutaImagen_Seguimiento = Entity.RutaImagen_Seguimiento,
                        FtpPassword = Entity.FtpPassword,
                        FtpUrl = Entity.FtpUrl,
                        FtpUser = Entity.FtpUser
                    };
                }
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(aca_parametro_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_parametro Entity = Context.aca_parametro.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa);
                    if (Entity == null)
                    {
                        Entity = new aca_parametro
                        {

                            IdEmpresa = info.IdEmpresa,
                            RutaImagen_Seguimiento = info.RutaImagen_Seguimiento,
                            RutaImagen_Profesor = info.RutaImagen_Profesor,
                            RutaImagen_Alumno = info.RutaImagen_Alumno,
                            FtpPassword = info.FtpPassword,
                            FtpUrl = info.FtpUrl,
                            FtpUser = info.FtpUser

                        };
                        Context.aca_parametro.Add(Entity);
                    }
                    else
                    {
                        Entity.RutaImagen_Seguimiento = info.RutaImagen_Seguimiento;
                        Entity.RutaImagen_Profesor = info.RutaImagen_Profesor;
                        Entity.RutaImagen_Alumno = info.RutaImagen_Alumno;
                        Entity.FtpPassword = info.FtpPassword;
                        Entity.FtpUrl = info.FtpUrl;
                        Entity.FtpUser = info.FtpUser;
                    }
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
