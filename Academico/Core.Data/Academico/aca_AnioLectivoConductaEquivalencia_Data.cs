using Core.Data.Base;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_AnioLectivoConductaEquivalencia_Data
    {
        public List<aca_AnioLectivoConductaEquivalencia_Info> getList(int IdEmpresa,int IdAnio, bool MostrarAnulados)
        {
            try
            {
                List<aca_AnioLectivoConductaEquivalencia_Info> Lista = new List<aca_AnioLectivoConductaEquivalencia_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.vwaca_AnioLectivoConductaEquivalencia.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio ==IdAnio).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_AnioLectivoConductaEquivalencia_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdAnio = q.IdAnio,
                            Secuencia = q.Secuencia,
                            Letra = q.Letra,
                            Calificacion = q.Calificacion,
                            Descripcion = q.Descripcion
                        });
                    });
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public aca_AnioLectivoConductaEquivalencia_Info getInfo(int IdEmpresa, int IdAnio, int Secuencia)
        {
            try
            {
                aca_AnioLectivoConductaEquivalencia_Info info;

                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.aca_AnioLectivoConductaEquivalencia.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.Secuencia == Secuencia).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new aca_AnioLectivoConductaEquivalencia_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdAnio = Entity.IdAnio,
                        Secuencia = Entity.Secuencia,
                        Letra = Entity.Letra,
                        Calificacion = Entity.Calificacion
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public aca_AnioLectivoConductaEquivalencia_Info getInfo_X_PromConducta(int IdEmpresa, int IdAnio, decimal PromedioConducta)
        {
            try
            {
                aca_AnioLectivoConductaEquivalencia_Info info;

                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.aca_AnioLectivoConductaEquivalencia.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio ==IdAnio && q.Calificacion == PromedioConducta).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new aca_AnioLectivoConductaEquivalencia_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdAnio = Entity.IdAnio,
                        Secuencia = Entity.Secuencia,
                        Letra = Entity.Letra,
                        Calificacion = Entity.Calificacion
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int getId(int IdEmpresa)
        {
            try
            {
                int ID = 1;

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var cont = Context.aca_AnioLectivoConductaEquivalencia.Where(q => q.IdEmpresa == IdEmpresa).Count();
                    if (cont > 0)
                        ID = Context.aca_AnioLectivoConductaEquivalencia.Where(q => q.IdEmpresa == IdEmpresa).Max(q => q.Secuencia) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(aca_AnioLectivoConductaEquivalencia_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_AnioLectivoConductaEquivalencia Entity = new aca_AnioLectivoConductaEquivalencia
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdAnio = info.IdAnio,
                        Secuencia = info.Secuencia=getId(info.IdEmpresa),
                        Letra = info.Letra,
                        Calificacion = info.Calificacion
                    };
                    Context.aca_AnioLectivoConductaEquivalencia.Add(Entity);

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(aca_AnioLectivoConductaEquivalencia_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_AnioLectivoConductaEquivalencia Entity = Context.aca_AnioLectivoConductaEquivalencia.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.Secuencia == info.Secuencia);
                    if (Entity == null)
                        return false;
                    Entity.IdAnio = info.IdAnio;
                    Entity.Letra = info.Letra;
                    Entity.Calificacion = info.Calificacion;

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
