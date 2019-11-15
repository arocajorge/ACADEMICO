﻿using Core.Data.Base;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_Matricula_Data
    {
        public List<aca_Matricula_Info> getList(int IdEmpresa, bool MostrarAnulados)
        {
            try
            {
                List<aca_Matricula_Info> Lista = new List<aca_Matricula_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.aca_Matricula.Where(q => q.IdEmpresa == IdEmpresa).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_Matricula_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdMatricula = q.IdMatricula,
                            IdAlumno = q.IdAlumno,
                            IdAnio = q.IdAnio,
                            IdSede = q.IdSede,
                            IdNivel = q.IdNivel,
                            IdJornada = q.IdJornada,
                            IdCurso = q.IdCurso,
                            IdParalelo = q.IdParalelo
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

        public aca_Matricula_Info getInfo(int IdEmpresa, int IdMatricula)
        {
            try
            {
                aca_Matricula_Info info;

                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.aca_Matricula.Where(q => q.IdEmpresa == IdEmpresa && q.IdMatricula == IdMatricula).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new aca_Matricula_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdMatricula = Entity.IdMatricula,
                        Codigo =Entity.Codigo,
                        IdAlumno = Entity.IdAlumno,
                        IdAnio = Entity.IdAnio,
                        IdSede = Entity.IdSede,
                        IdNivel = Entity.IdNivel,
                        IdJornada = Entity.IdJornada,
                        IdCurso = Entity.IdCurso,
                        IdParalelo = Entity.IdParalelo,
                        IdPersonaF = Entity.IdPersonaF,
                        IdPersonaR = Entity.IdPersonaR,
                        IdPlantilla = Entity.IdPlantilla
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public decimal getId(int IdEmpresa)
        {
            try
            {
                decimal ID = 1;

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var cont = Context.aca_Matricula.Where(q => q.IdEmpresa == IdEmpresa).Count();
                    if (cont > 0)
                        ID = Context.aca_Matricula.Where(q => q.IdEmpresa == IdEmpresa).Max(q => q.IdMatricula) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(aca_Matricula_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_Matricula Entity = new aca_Matricula
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdMatricula = info.IdMatricula = getId(info.IdEmpresa),
                        Codigo = info.Codigo,
                        IdAlumno = info.IdAlumno,
                        IdAnio = info.IdAnio,
                        IdSede = info.IdSede,
                        IdNivel = info.IdNivel,
                        IdJornada = info.IdJornada,
                        IdCurso = info.IdCurso,
                        IdParalelo = info.IdParalelo,
                        IdPersonaF = info.IdPersonaF,
                        IdPersonaR = info.IdPersonaR,
                        IdPlantilla = info.IdPlantilla
                    };
                    Context.aca_Matricula.Add(Entity);

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(aca_Matricula_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_Matricula Entity = Context.aca_Matricula.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdMatricula == info.IdMatricula);
                    if (Entity == null)
                        return false;
                    Entity.IdEmpresa = info.IdEmpresa;
                    Entity.Codigo = info.Codigo;

                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool anularDB(aca_Matricula_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_Matricula Entity = Context.aca_Matricula.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdMatricula == info.IdMatricula);
                    if (Entity == null)
                        return false;
                    //Entity.Estado = info.Estado = false;
                    //Entity.MotivoAnulacion = info.MotivoAnulacion;
                    //Entity.IdUsuarioAnulacion = info.IdUsuarioAnulacion;
                    //Entity.FechaAnulacion = info.FechaAnulacion = DateTime.Now;
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