using Core.Data.Base;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Core.Data.Academico
{
    public class aca_AnioLectivo_Curso_Plantilla_Parametrizacion_Data
    {
        public List<aca_AnioLectivo_Curso_Plantilla_Parametrizacion_Info> GetList(int IdEmpresa, int IdAnio)
        {
            try
            {
                List<aca_AnioLectivo_Curso_Plantilla_Parametrizacion_Info> Lista = new List<aca_AnioLectivo_Curso_Plantilla_Parametrizacion_Info>();

                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var lst = db.vwaca_AnioLectivo_Curso_Plantilla_Parametrizacion.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio).ToList();
                    foreach (var item in lst)
                    {
                        Lista.Add(new aca_AnioLectivo_Curso_Plantilla_Parametrizacion_Info
                        {
                            IdEmpresa = item.IdEmpresa,
                            IdAnio = item.IdAnio,
                            IdSede = item.IdSede,
                            IdNivel = item.IdNivel,
                            IdJornada = item.IdJornada,
                            IdCurso = item.IdCurso,
                            IdPlantilla = item.IdPlantilla,
                            IdRubro = item.IdRubro,
                            IdCtaCbleDebe = item.IdCtaCbleDebe,
                            NomJornada = item.NomJornada,
                            OrdenJornada = item.OrdenJornada,
                            NomCurso = item.NomCurso,
                            OrdenCurso = item.OrdenCurso,
                            NomPlantilla = item.NomPlantilla,
                            NomRubro = item.NomRubro,
                            NomNivel = item.NomNivel,
                            NomSede = item.NomSede,
                            IdCtaCbleHaber = item.IdCtaCbleHaber,
                            pc_CuentaDebe = item.pc_CuentaDebe,
                            pc_CuentaHaber = item.pc_CuentaHaber,
                            IdString = item.IdEmpresa.ToString("0000") + item.IdAnio.ToString("0000") + item.IdSede.ToString("0000") + item.IdNivel.ToString("0000") + item.IdJornada.ToString("0000") + item.IdCurso.ToString("0000") + item.IdPlantilla.ToString("0000") + item.IdRubro.ToString("0000")
                        });
                    }

                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ModificarDB(aca_AnioLectivo_Curso_Plantilla_Parametrizacion_Info info)
        {
            try
            {
                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    /*
                    var PK = Regex.Matches(info.IdString, ".{1," + 4 + "}").Cast<Match>().Select(m => m.Value).ToArray();
                    if (PK.Count() != 0)
                    {
                    */
                    /*
                        info.IdEmpresa = Convert.ToInt32(PK[0]);
                        info.IdAnio = Convert.ToInt32(PK[1]);
                        info.IdSede = Convert.ToInt32(PK[2]);
                        info.IdNivel = Convert.ToInt32(PK[3]);
                        info.IdJornada = Convert.ToInt32(PK[4]);
                        info.IdCurso = Convert.ToInt32(PK[5]);
                        info.IdPlantilla = Convert.ToInt32(PK[6]);
                        info.IdRubro = Convert.ToInt32(PK[7]);
                        */
                        var Entity = db.aca_AnioLectivo_Curso_Plantilla_Parametrizacion.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdAnio == info.IdAnio && q.IdSede == info.IdSede && q.IdNivel == info.IdNivel && q.IdJornada == info.IdJornada && q.IdCurso == info.IdCurso && q.IdPlantilla == info.IdPlantilla && q.IdRubro == info.IdRubro).FirstOrDefault();
                        if (Entity == null)
                        {
                            db.aca_AnioLectivo_Curso_Plantilla_Parametrizacion.Add(new aca_AnioLectivo_Curso_Plantilla_Parametrizacion
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdAnio = info.IdAnio,
                                IdSede = info.IdSede,
                                IdNivel = info.IdNivel,
                                IdJornada = info.IdJornada,
                                IdCurso = info.IdCurso,
                                IdPlantilla = info.IdPlantilla,
                                IdRubro = info.IdRubro,
                                IdCtaCbleDebe = info.IdCtaCbleDebe,
                                IdCtaCbleHaber = info.IdCtaCbleHaber
                            });
                        }
                        else
                        {
                            Entity.IdCtaCbleDebe = info.IdCtaCbleDebe;
                            Entity.IdCtaCbleHaber = info.IdCtaCbleHaber;
                        }
                        
                        db.SaveChanges();
                    //}

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
