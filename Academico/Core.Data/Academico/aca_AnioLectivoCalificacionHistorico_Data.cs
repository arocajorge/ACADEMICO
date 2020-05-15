using Core.Data.Base;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_AnioLectivoCalificacionHistorico_Data
    {
        public List<aca_AnioLectivoCalificacionHistorico_Info> getList(int IdEmpresa, decimal IdAlumno, bool MostrarAnulados)
        {
            try
            {
                List<aca_AnioLectivoCalificacionHistorico_Info> Lista = new List<aca_AnioLectivoCalificacionHistorico_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.vwaca_AnioLectivoCalificacionHistorico.Where(q => q.IdEmpresa == IdEmpresa && q.IdAlumno == IdAlumno).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_AnioLectivoCalificacionHistorico_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdAnio = q.IdAnio,
                            IdAlumno = q.IdAlumno,
                            IdCurso = q.IdCurso,
                            IdNivel = q.IdNivel,
                            Promedio = q.Promedio,
                            Conducta = q.Conducta,
                            Descripcion = q.Descripcion,
                            NomNivel=q.NomNivel,
                            NomCurso= q.NomCurso,
                            Letra = q.Letra,
                            AntiguaInstitucion = q.AntiguaInstitucion,
                            pe_nombreCompleto = q.pe_nombreCompleto
                        });
                    });
                }
                Lista.ForEach(q=>q.IdString = IdEmpresa.ToString("000")+ q.IdAnio.ToString("0000") + q.IdAlumno.ToString("000000") + q.IdNivel.ToString("000") + q.IdCurso.ToString("000") );
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public aca_AnioLectivoCalificacionHistorico_Info getInfo(int IdEmpresa, int IdAnio, decimal IdAlumno)
        {
            try
            {
                aca_AnioLectivoCalificacionHistorico_Info info;

                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.aca_AnioLectivoCalificacionHistorico.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdAlumno== IdAlumno).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new aca_AnioLectivoCalificacionHistorico_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdAnio = Entity.IdAnio,
                        IdAlumno = Entity.IdAlumno,
                        IdNivel = Entity.IdNivel,
                        IdCurso = Entity.IdCurso,
                        AntiguaInstitucion = Entity.AntiguaInstitucion,
                        Promedio = Entity.Promedio,
                        Conducta = Entity.Conducta
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(aca_AnioLectivoCalificacionHistorico_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_AnioLectivoCalificacionHistorico Entity = new aca_AnioLectivoCalificacionHistorico
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdAnio = info.IdAnio,
                        IdAlumno = info.IdAlumno,
                        IdNivel=info.IdNivel,
                        IdCurso = info.IdCurso,
                        AntiguaInstitucion= info.AntiguaInstitucion,
                        Promedio = info.Promedio,
                        Conducta = info.Conducta
                    };
                    Context.aca_AnioLectivoCalificacionHistorico.Add(Entity);

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public bool modificarDB(aca_AnioLectivoCalificacionHistorico_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_AnioLectivoCalificacionHistorico Entity = Context.aca_AnioLectivoCalificacionHistorico.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdAnio == info.IdAnio && q.IdAlumno == info.IdAlumno);
                    if (Entity == null)
                        return false;

                    Entity.IdNivel = info.IdNivel;
                    Entity.IdCurso = info.IdCurso;
                    Entity.AntiguaInstitucion = info.AntiguaInstitucion;
                    Entity.Promedio = info.Promedio;
                    Entity.Conducta = info.Conducta;

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
