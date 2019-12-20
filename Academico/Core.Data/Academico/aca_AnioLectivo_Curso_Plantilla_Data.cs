using Core.Data.Base;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_AnioLectivo_Curso_Plantilla_Data
    {
        public List<aca_AnioLectivo_Curso_Plantilla_Info> get_list_asignacion(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, int IdJornada, int IdCurso)
        {
            try
            {
                List<aca_AnioLectivo_Curso_Plantilla_Info> Lista;

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    Lista = (from q in Context.aca_AnioLectivo_Curso_Plantilla
                             join c in Context.aca_Plantilla
                             on new { q.IdEmpresa, q.IdAnio, q.IdPlantilla } equals new { c.IdEmpresa, c.IdAnio, c.IdPlantilla }
                             where q.IdEmpresa == IdEmpresa
                             && q.IdSede == IdSede
                             && q.IdAnio == IdAnio
                             && q.IdNivel == IdNivel
                             && q.IdJornada == IdJornada
                             && q.IdCurso == IdCurso
                             && c.Estado == true
                             select new aca_AnioLectivo_Curso_Plantilla_Info
                             {
                                 seleccionado = true,
                                 IdEmpresa = q.IdEmpresa,
                                 IdSede = q.IdSede,
                                 IdAnio = q.IdAnio,
                                 IdNivel = q.IdNivel,
                                 IdJornada = q.IdJornada,
                                 IdCurso = q.IdCurso,
                                 IdPlantilla = q.IdPlantilla,
                                 NomPlantilla = c.NomPlantilla
                             }).ToList();

                    Lista.AddRange((from j in Context.aca_Plantilla
                                    where !Context.aca_AnioLectivo_Curso_Plantilla.Any(n => n.IdPlantilla == j.IdPlantilla && n.IdEmpresa == IdEmpresa && n.IdSede == IdSede && n.IdAnio == IdAnio && n.IdNivel == IdNivel && n.IdJornada == IdJornada && n.IdCurso == IdCurso)
                                    && j.Estado == true
                                    && j.IdAnio == IdAnio
                                    select new aca_AnioLectivo_Curso_Plantilla_Info
                                    {
                                        seleccionado = false,
                                        IdEmpresa = IdEmpresa,
                                        IdSede = IdSede,
                                        IdAnio = IdAnio,
                                        IdNivel = IdNivel,
                                        IdJornada = IdJornada,
                                        IdCurso = IdCurso,
                                        IdPlantilla = j.IdPlantilla,
                                        NomPlantilla = j.NomPlantilla
                                    }).ToList());
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public List<aca_AnioLectivo_Curso_Plantilla_Info> getList(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, int IdJornada, int IdCurso)
        {
            try
            {
                List<aca_AnioLectivo_Curso_Plantilla_Info> Lista = new List<aca_AnioLectivo_Curso_Plantilla_Info>();

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    Lista = Context.aca_AnioLectivo_Curso_Plantilla.Include("aca_Plantilla").Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdSede == IdSede && q.IdNivel == IdNivel && q.IdJornada == IdJornada && q.IdCurso == IdCurso && q.aca_Plantilla.Estado == true).Select(q => new aca_AnioLectivo_Curso_Plantilla_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdAnio = q.IdAnio,
                        IdSede = q.IdSede,
                        IdJornada = q.IdJornada,
                        IdCurso = q.IdCurso,
                        IdNivel = q.IdNivel,
                        IdPlantilla = q.IdPlantilla,
                        NomPlantilla = q.aca_Plantilla.NomPlantilla,
                        TipoDescuento = q.aca_Plantilla.TipoDescuento,
                        Valor = q.aca_Plantilla.Valor
                    }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool guardarDB(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, int IdJornada, int IdCurso, List<aca_AnioLectivo_Curso_Plantilla_Info> lista)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var lst_PlantillaPorCurso = Context.aca_AnioLectivo_Curso_Plantilla.Where(q => q.IdEmpresa == IdEmpresa && q.IdSede == IdSede && q.IdAnio == IdAnio && q.IdNivel == IdNivel && q.IdJornada == IdJornada && q.IdCurso == IdCurso).ToList();
                    Context.aca_AnioLectivo_Curso_Plantilla.RemoveRange(lst_PlantillaPorCurso);

                    if (lista.Count > 0)
                    {
                        foreach (var info in lista)
                        {
                            aca_AnioLectivo_Curso_Plantilla Entity = new aca_AnioLectivo_Curso_Plantilla
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdAnio = info.IdAnio,
                                IdSede = info.IdSede,
                                IdNivel = info.IdNivel,
                                IdJornada = info.IdJornada,
                                IdCurso = info.IdCurso,
                                IdPlantilla = info.IdPlantilla
                            };
                            Context.aca_AnioLectivo_Curso_Plantilla.Add(Entity);
                        }
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
