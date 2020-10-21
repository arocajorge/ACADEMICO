using Core.Data.Base;
using Core.Info.Academico;
using Core.Info.Helps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_MatriculaCalificacion_Data
    {
        aca_Matricula_Data odata_matricula = new aca_Matricula_Data();
        aca_AnioLectivoEquivalenciaPromedio_Data odata_promedio_equivalencia = new aca_AnioLectivoEquivalenciaPromedio_Data();
        public List<aca_MatriculaCalificacion_Info> getList(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, int IdMateria)
        {
            try
            {
                List<aca_MatriculaCalificacion_Info> Lista = new List<aca_MatriculaCalificacion_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.vwaca_MatriculaCalificacion.Where(q => q.IdEmpresa == IdEmpresa && q.IdSede == IdSede
                    && q.IdAnio == IdAnio && q.IdNivel == IdNivel && q.IdJornada == IdJornada && q.IdCurso == IdCurso && q.IdParalelo == IdParalelo
                    && q.IdMateria == IdMateria).OrderBy(q=>q.pe_nombreCompletoAlumno).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_MatriculaCalificacion_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdMatricula = q.IdMatricula,
                            IdMateria = q.IdMateria,
                            IdProfesor = q.IdProfesor,
                            IdAlumno = q.IdAlumno,
                            Codigo = q.Codigo,
                            pe_nombreCompletoAlumno = q.pe_nombreCompletoAlumno,
                            pe_nombreCompleto = q.pe_nombreCompleto
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

        public List<aca_MatriculaCalificacion_Info> getList_PaseAnio(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, decimal IdAlumno)
        {
            try
            {
                List<aca_MatriculaCalificacion_Info> Lista = new List<aca_MatriculaCalificacion_Info>();
                int IdNivelIni = IdNivel;
                int IdNivelFin = IdNivel == 0 ? 9999999 : IdNivel;

                int IdJornadaIni = IdJornada;
                int IdJornadaFin = IdJornada == 0 ? 9999999 : IdJornada;

                int IdCursoIni = IdCurso;
                int IdCursoFin = IdCurso == 0 ? 9999999 : IdCurso;

                int IdParaleloIni = IdParalelo;
                int IdParaleloFin = IdParalelo == 0 ? 9999999 : IdParalelo;

                decimal IdAlumnoIni = IdAlumno;
                decimal IdAlumnoFin = IdAlumno == 0 ? 9999999 : IdAlumno;

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.vwaca_MatriculaCalificacion.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdSede == IdSede
                    && q.IdNivel >= IdNivelIni && q.IdNivel <= IdNivelFin && q.IdJornada >= IdJornadaIni && q.IdJornada <= IdJornadaFin
                    && q.IdCurso >= IdCursoIni && q.IdCurso <= IdCursoFin && q.IdParalelo >= IdParaleloIni && q.IdParalelo <= IdParaleloFin
                    && q.IdAlumno >= IdAlumnoIni && q.IdAlumno <= IdAlumnoFin).OrderBy(q => q.pe_nombreCompletoAlumno).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_MatriculaCalificacion_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdMatricula = q.IdMatricula,
                            IdMateria = q.IdMateria,
                            IdProfesor = q.IdProfesor,
                            IdAlumno = q.IdAlumno,
                            Codigo = q.Codigo,
                            pe_nombreCompletoAlumno = q.pe_nombreCompletoAlumno,
                            pe_nombreCompleto = q.pe_nombreCompleto,
                            RegistroValido = true,
                            CalificacionP1 = q.CalificacionP1,
                            CalificacionP2 = q.CalificacionP2,
                            CalificacionP3 = q.CalificacionP3,
                            PromedioQ1 = q.PromedioQ1,
                            ExamenQ1 = q.ExamenQ1,
                            PromedioFinalQ1 = q.PromedioFinalQ1,
                            CalificacionP4 = q.CalificacionP4,
                            CalificacionP5 = q.CalificacionP5,
                            CalificacionP6 = q.CalificacionP6,
                            PromedioQ2 = q.PromedioQ2,
                            ExamenQ2 = q.ExamenQ2,
                            PromedioFinalQ2 = q.PromedioFinalQ2,
                            ExamenMejoramiento = q.ExamenMejoramiento,
                            CampoMejoramiento = q.CampoMejoramiento,
                            ExamenSupletorio = q.ExamenSupletorio,
                            ExamenRemedial = q.ExamenRemedial,
                            ExamenGracia = q.ExamenGracia,
                            PromedioFinal = q.PromedioFinal,
                            CodigoEquivalencia= q.CodigoEquivalencia,
                            DescripcionEquivalencia = q.Descripcion,
                            IdEquivalenciaPromedioP1 = q.IdEquivalenciaPromedioP1,
                            IdEquivalenciaPromedioP2 = q.IdEquivalenciaPromedioP2,
                            IdEquivalenciaPromedioP3 = q.IdEquivalenciaPromedioP2,
                            IdEquivalenciaPromedioEQ1 = q.IdEquivalenciaPromedioEQ1,
                            IdEquivalenciaPromedioQ1 = q.IdEquivalenciaPromedioQ1,
                            IdEquivalenciaPromedioP4 = q.IdEquivalenciaPromedioP4,
                            IdEquivalenciaPromedioP5 = q.IdEquivalenciaPromedioP5,
                            IdEquivalenciaPromedioP6 = q.IdEquivalenciaPromedioP6,
                            IdEquivalenciaPromedioEQ2 = q.IdEquivalenciaPromedioEQ2,
                            IdEquivalenciaPromedioQ2 = q.IdEquivalenciaPromedioQ2,
                            IdEquivalenciaPromedioPF = q.IdEquivalenciaPromedioPF
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

        public aca_MatriculaCalificacion_Info getInfo_modificar(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, int IdMateria, decimal IdAlumno)
        {
            try
            {
                aca_MatriculaCalificacion_Info info = new aca_MatriculaCalificacion_Info();
                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var Entity = odata.vwaca_MatriculaCalificacion.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdSede == IdSede
                    && q.IdNivel == IdNivel && q.IdJornada == IdJornada && q.IdCurso == IdCurso && q.IdParalelo == IdParalelo && q.IdMateria == IdMateria
                    && q.IdAlumno == IdAlumno).OrderBy(q => q.pe_nombreCompletoAlumno).FirstOrDefault();

                    if (Entity == null)
                        return null;

                    info = new aca_MatriculaCalificacion_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdMatricula = Entity.IdMatricula,
                        IdMateria = Entity.IdMateria,
                        IdProfesor = Entity.IdProfesor,
                        IdAlumno = Entity.IdAlumno,
                        Codigo = Entity.Codigo,
                        pe_nombreCompletoAlumno = Entity.pe_nombreCompletoAlumno,
                        pe_nombreCompleto = Entity.pe_nombreCompleto,
                        RegistroValido = true,
                        CalificacionP1 = Entity.CalificacionP1,
                        CalificacionP2 = Entity.CalificacionP2,
                        CalificacionP3 = Entity.CalificacionP3,
                        PromedioQ1 = Entity.PromedioQ1,
                        ExamenQ1 = Entity.ExamenQ1,
                        PromedioFinalQ1 = Entity.PromedioFinalQ1,
                        CalificacionP4 = Entity.CalificacionP4,
                        CalificacionP5 = Entity.CalificacionP5,
                        CalificacionP6 = Entity.CalificacionP6,
                        PromedioQ2 = Entity.PromedioQ2,
                        ExamenQ2 = Entity.ExamenQ2,
                        PromedioFinalQ2 = Entity.PromedioFinalQ2,
                        ExamenMejoramiento = Entity.ExamenMejoramiento,
                        CampoMejoramiento = Entity.CampoMejoramiento,
                        ExamenSupletorio = Entity.ExamenSupletorio,
                        ExamenRemedial = Entity.ExamenRemedial,
                        ExamenGracia = Entity.ExamenGracia,
                        PromedioFinal = Entity.PromedioFinal,
                        CodigoEquivalencia = Entity.CodigoEquivalencia,
                        DescripcionEquivalencia = Entity.Descripcion,
                        IdEquivalenciaPromedioP1 = Entity.IdEquivalenciaPromedioP1,
                        IdEquivalenciaPromedioP2 = Entity.IdEquivalenciaPromedioP2,
                        IdEquivalenciaPromedioP3 = Entity.IdEquivalenciaPromedioP2,
                        IdEquivalenciaPromedioEQ1 = Entity.IdEquivalenciaPromedioEQ1,
                        IdEquivalenciaPromedioQ1 = Entity.IdEquivalenciaPromedioQ1,
                        IdEquivalenciaPromedioP4 = Entity.IdEquivalenciaPromedioP4,
                        IdEquivalenciaPromedioP5 = Entity.IdEquivalenciaPromedioP5,
                        IdEquivalenciaPromedioP6 = Entity.IdEquivalenciaPromedioP6,
                        IdEquivalenciaPromedioEQ2 = Entity.IdEquivalenciaPromedioEQ2,
                        IdEquivalenciaPromedioQ2 = Entity.IdEquivalenciaPromedioQ2,
                        IdEquivalenciaPromedioPF = Entity.IdEquivalenciaPromedioPF
                    };
                }

                return info;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<aca_MatriculaCalificacion_Info> getList_x_Profesor(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, int IdMateria, decimal IdProfesor)
        {
            try
            {
                List<aca_MatriculaCalificacion_Info> Lista = new List<aca_MatriculaCalificacion_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.vwaca_MatriculaCalificacion.Where(q => q.IdEmpresa == IdEmpresa && q.IdSede == IdSede
                    && q.IdAnio == IdAnio && q.IdNivel == IdNivel && q.IdJornada == IdJornada && q.IdCurso == IdCurso && q.IdParalelo == IdParalelo
                    && q.IdMateria == IdMateria && q.IdProfesor == IdProfesor).OrderBy(q => q.pe_nombreCompletoAlumno).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_MatriculaCalificacion_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdMatricula = q.IdMatricula,
                            IdMateria = q.IdMateria,
                            IdProfesor = q.IdProfesor,
                            IdAlumno = q.IdAlumno,
                            Codigo = q.Codigo,
                            pe_nombreCompletoAlumno = q.pe_nombreCompletoAlumno,
                            pe_nombreCompleto = q.pe_nombreCompleto,
                            CalificacionP1 = q.CalificacionP1,
                            CalificacionP2 = q.CalificacionP2,
                            CalificacionP3 = q.CalificacionP3,
                            CalificacionP4 = q.CalificacionP4,
                            CalificacionP5 = q.CalificacionP5,
                            CalificacionP6 = q.CalificacionP6,
                            ExamenQ1 = q.ExamenQ1,
                            PromedioFinalQ1=q.PromedioFinalQ1,
                            CausaQ1 = q.CausaQ1,
                            ResolucionQ1 = q.ResolucionQ1,
                            ExamenQ2 = q.ExamenQ2,
                            PromedioFinalQ2= q.PromedioFinalQ2,
                            CausaQ2 = q.CausaQ2,
                            ResolucionQ2 = q.ResolucionQ2,
                            ExamenMejoramiento = q.ExamenMejoramiento,
                            CampoMejoramiento = q.CampoMejoramiento,
                            ExamenSupletorio = q.ExamenSupletorio,
                            ExamenRemedial = q.ExamenRemedial,
                            ExamenGracia = q.ExamenGracia,
                            PromedioFinal = q.PromedioFinal,
                            RegistroValido = true,
                            CodigoEquivalencia = q.CodigoEquivalencia,
                            DescripcionEquivalencia = q.Descripcion,
                            IdEquivalenciaPromedioP1 = q.IdEquivalenciaPromedioP1,
                            IdEquivalenciaPromedioP2 = q.IdEquivalenciaPromedioP2,
                            IdEquivalenciaPromedioP3 = q.IdEquivalenciaPromedioP2,
                            IdEquivalenciaPromedioEQ1 = q.IdEquivalenciaPromedioEQ1,
                            IdEquivalenciaPromedioQ1 = q.IdEquivalenciaPromedioQ1,
                            IdEquivalenciaPromedioP4 = q.IdEquivalenciaPromedioP4,
                            IdEquivalenciaPromedioP5 = q.IdEquivalenciaPromedioP5,
                            IdEquivalenciaPromedioP6 = q.IdEquivalenciaPromedioP6,
                            IdEquivalenciaPromedioEQ2 = q.IdEquivalenciaPromedioEQ2,
                            IdEquivalenciaPromedioQ2 = q.IdEquivalenciaPromedioQ2,
                            IdEquivalenciaPromedioPF = q.IdEquivalenciaPromedioPF,
                            PromedioQuimestres = q.PromedioQuimestres
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

        public List<aca_MatriculaCalificacion_Info> getList_SuperAdmin(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, int IdMateria)
        {
            try
            {
                List<aca_MatriculaCalificacion_Info> Lista = new List<aca_MatriculaCalificacion_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.vwaca_MatriculaCalificacion.Where(q => q.IdEmpresa == IdEmpresa && q.IdSede == IdSede
                    && q.IdAnio == IdAnio && q.IdNivel == IdNivel && q.IdJornada == IdJornada && q.IdCurso == IdCurso && q.IdParalelo == IdParalelo
                    && q.IdMateria == IdMateria).OrderBy(q => q.pe_nombreCompletoAlumno).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_MatriculaCalificacion_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdMatricula = q.IdMatricula,
                            IdMateria = q.IdMateria,
                            IdProfesor = q.IdProfesor,
                            IdAlumno = q.IdAlumno,
                            Codigo = q.Codigo,
                            pe_nombreCompletoAlumno = q.pe_nombreCompletoAlumno,
                            pe_nombreCompleto = q.pe_nombreCompleto,
                            CalificacionP1 = q.CalificacionP1,
                            CalificacionP2 = q.CalificacionP2,
                            CalificacionP3 = q.CalificacionP3,
                            CalificacionP4 = q.CalificacionP4,
                            CalificacionP5 = q.CalificacionP5,
                            CalificacionP6 = q.CalificacionP6,
                            ExamenQ1 = q.ExamenQ1,
                            PromedioFinalQ1 = q.PromedioFinalQ1,
                            CausaQ1 = q.CausaQ1,
                            ResolucionQ1 = q.ResolucionQ1,
                            ExamenQ2 = q.ExamenQ2,
                            PromedioFinalQ2 = q.PromedioFinalQ2,
                            CausaQ2 = q.CausaQ2,
                            ResolucionQ2 = q.ResolucionQ2,
                            ExamenMejoramiento = q.ExamenMejoramiento,
                            CampoMejoramiento = q.CampoMejoramiento,
                            ExamenSupletorio = q.ExamenSupletorio,
                            ExamenRemedial = q.ExamenRemedial,
                            ExamenGracia = q.ExamenGracia,
                            PromedioFinal = q.PromedioFinal,
                            RegistroValido = true,
                            CodigoEquivalencia = q.CodigoEquivalencia,
                            DescripcionEquivalencia = q.Descripcion,
                            IdEquivalenciaPromedioP1 = q.IdEquivalenciaPromedioP1,
                            IdEquivalenciaPromedioP2 = q.IdEquivalenciaPromedioP2,
                            IdEquivalenciaPromedioP3 = q.IdEquivalenciaPromedioP2,
                            IdEquivalenciaPromedioEQ1 = q.IdEquivalenciaPromedioEQ1,
                            IdEquivalenciaPromedioQ1 = q.IdEquivalenciaPromedioQ1,
                            IdEquivalenciaPromedioP4 = q.IdEquivalenciaPromedioP4,
                            IdEquivalenciaPromedioP5 = q.IdEquivalenciaPromedioP5,
                            IdEquivalenciaPromedioP6 = q.IdEquivalenciaPromedioP6,
                            IdEquivalenciaPromedioEQ2 = q.IdEquivalenciaPromedioEQ2,
                            IdEquivalenciaPromedioQ2 = q.IdEquivalenciaPromedioQ2,
                            IdEquivalenciaPromedioPF = q.IdEquivalenciaPromedioPF,
                            PromedioQuimestres = q.PromedioQuimestres
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

        public aca_MatriculaCalificacion_Info getInfo(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, int IdMateria, decimal IdAlumno)
        {
            try
            {
                aca_MatriculaCalificacion_Info info = new aca_MatriculaCalificacion_Info();

                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.vwaca_MatriculaCalificacion.Where(q => q.IdEmpresa == IdEmpresa && q.IdSede == IdSede
                    && q.IdAnio == IdAnio && q.IdNivel == IdNivel && q.IdJornada == IdJornada && q.IdCurso == IdCurso && q.IdParalelo == IdParalelo
                    && q.IdMateria == IdMateria && q.IdAlumno == IdAlumno).FirstOrDefault();

                    if (Entity == null)
                        return null;
                    info = new aca_MatriculaCalificacion_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdMatricula = Entity.IdMatricula,
                        IdMateria = Entity.IdMateria,
                        IdProfesor = Entity.IdProfesor,
                        IdAlumno = Entity.IdAlumno,
                        Codigo = Entity.Codigo,
                        pe_nombreCompletoAlumno = Entity.pe_nombreCompletoAlumno,
                        CalificacionP1 = Entity.CalificacionP1,
                        CalificacionP2 = Entity.CalificacionP2,
                        CalificacionP3 = Entity.CalificacionP3,
                        CalificacionP4 = Entity.CalificacionP4,
                        CalificacionP5 = Entity.CalificacionP5,
                        CalificacionP6 = Entity.CalificacionP6,
                        ExamenQ1 = Entity.ExamenQ1,
                        ExamenQ2 = Entity.ExamenQ2,
                        ExamenMejoramiento = Entity.ExamenMejoramiento,
                        CampoMejoramiento = Entity.CampoMejoramiento,
                        ExamenSupletorio = Entity.ExamenSupletorio,
                        ExamenRemedial = Entity.ExamenRemedial,
                        ExamenGracia = Entity.ExamenGracia,
                        PromedioFinal = Entity.PromedioFinal,
                        CodigoEquivalencia = Entity.CodigoEquivalencia,
                        DescripcionEquivalencia = Entity.Descripcion,
                        IdEquivalenciaPromedioP1 = Entity.IdEquivalenciaPromedioP1,
                        IdEquivalenciaPromedioP2 = Entity.IdEquivalenciaPromedioP2,
                        IdEquivalenciaPromedioP3 = Entity.IdEquivalenciaPromedioP2,
                        IdEquivalenciaPromedioEQ1 = Entity.IdEquivalenciaPromedioEQ1,
                        IdEquivalenciaPromedioQ1 = Entity.IdEquivalenciaPromedioQ1,
                        IdEquivalenciaPromedioP4 = Entity.IdEquivalenciaPromedioP4,
                        IdEquivalenciaPromedioP5 = Entity.IdEquivalenciaPromedioP5,
                        IdEquivalenciaPromedioP6 = Entity.IdEquivalenciaPromedioP6,
                        IdEquivalenciaPromedioEQ2 = Entity.IdEquivalenciaPromedioEQ2,
                        IdEquivalenciaPromedioQ2 = Entity.IdEquivalenciaPromedioQ2,
                        IdEquivalenciaPromedioPF = Entity.IdEquivalenciaPromedioPF
                    };
                }

                return info;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public aca_MatriculaCalificacion_Info getInfo_X_Matricula(int IdEmpresa, decimal IdMatricula, decimal IdMateria)
        {
            try
            {
                aca_MatriculaCalificacion_Info info = new aca_MatriculaCalificacion_Info();

                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.aca_MatriculaCalificacion.Where(q => q.IdEmpresa == IdEmpresa && q.IdMatricula == IdMatricula && q.IdMateria == IdMateria).FirstOrDefault();

                    if (Entity == null)
                        return null;
                    info = new aca_MatriculaCalificacion_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdMatricula = Entity.IdMatricula,
                        IdMateria = Entity.IdMateria,
                        IdProfesor = Entity.IdProfesor,
                        CalificacionP1 = Entity.CalificacionP1,
                        CalificacionP2 = Entity.CalificacionP2,
                        CalificacionP3 = Entity.CalificacionP3,
                        CalificacionP4 = Entity.CalificacionP4,
                        CalificacionP5 = Entity.CalificacionP5,
                        CalificacionP6 = Entity.CalificacionP6,
                        PromedioQ1=Entity.PromedioQ1,
                        ExamenQ1 = Entity.ExamenQ1,
                        CausaQ1 = Entity.CausaQ1,
                        ResolucionQ1 = Entity.ResolucionQ1,
                        PromedioQ2 = Entity.PromedioQ2,
                        ExamenQ2 = Entity.ExamenQ2,
                        CausaQ2 = Entity.CausaQ2,
                        ResolucionQ2 = Entity.ResolucionQ2,
                        PromedioQuimestres = Entity.PromedioQuimestres,
                        ExamenMejoramiento = Entity.ExamenMejoramiento,
                        CampoMejoramiento = Entity.CampoMejoramiento,
                        ExamenSupletorio = Entity.ExamenSupletorio,
                        ExamenRemedial = Entity.ExamenRemedial,
                        ExamenGracia = Entity.ExamenGracia,
                        PromedioFinalQ1 = Entity.PromedioFinalQ1,
                        PromedioFinalQ2 = Entity.PromedioFinalQ2,
                        PromedioFinal = Entity.PromedioFinal,
                        IdEquivalenciaPromedioP1 = Entity.IdEquivalenciaPromedioP1,
                        IdEquivalenciaPromedioP2 = Entity.IdEquivalenciaPromedioP2,
                        IdEquivalenciaPromedioP3 = Entity.IdEquivalenciaPromedioP2,
                        IdEquivalenciaPromedioEQ1 = Entity.IdEquivalenciaPromedioEQ1,
                        IdEquivalenciaPromedioQ1 = Entity.IdEquivalenciaPromedioQ1,
                        IdEquivalenciaPromedioP4 = Entity.IdEquivalenciaPromedioP4,
                        IdEquivalenciaPromedioP5 = Entity.IdEquivalenciaPromedioP5,
                        IdEquivalenciaPromedioP6 = Entity.IdEquivalenciaPromedioP6,
                        IdEquivalenciaPromedioEQ2 = Entity.IdEquivalenciaPromedioEQ2,
                        IdEquivalenciaPromedioQ2 = Entity.IdEquivalenciaPromedioQ2,
                        IdEquivalenciaPromedioPF = Entity.IdEquivalenciaPromedioPF
                    };
                }

                return info;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool guardarDB(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, int IdMateria, List<aca_MatriculaCalificacion_Info> lista)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var info_anio_curso = Context.aca_AnioLectivo.Where(q => q.IdEmpresa == IdEmpresa && q.EnCurso == true).FirstOrDefault();
                    if (info_anio_curso != null)
                    {
                        var lst_matricula = Context.aca_Matricula.Where(q => q.IdEmpresa == IdEmpresa && q.IdSede == IdSede && q.IdAnio == IdAnio && q.IdNivel == IdNivel && q.IdJornada == IdJornada && q.IdCurso == IdCurso && q.IdParalelo == IdParalelo).ToList();
                        if (lst_matricula != null && lst_matricula.Count > 0)
                        {
                            foreach (var item in lista)
                            {
                                var info_matricula = lst_matricula.Where(q => q.IdEmpresa == IdEmpresa && q.IdSede == IdSede && q.IdAnio == IdAnio && q.IdNivel == IdNivel && q.IdJornada == IdJornada && q.IdCurso == IdCurso && q.IdParalelo == IdParalelo && q.IdAlumno == item.IdAlumno).FirstOrDefault();
                                if (info_matricula!=null)
                                {
                                    var lst_MatriculaCalificacionParcial = Context.aca_MatriculaCalificacionParcial.Where(q => q.IdEmpresa == IdEmpresa && q.IdMatricula == info_matricula.IdMatricula && q.IdMateria == IdMateria).ToList();
                                    if (lst_MatriculaCalificacionParcial.Count > 0)
                                    {
                                        lst_MatriculaCalificacionParcial.ForEach(q => q.IdProfesor = item.IdProfesor);
                                    }

                                    var lst_MatriculaCalificacion = Context.aca_MatriculaCalificacion.Where(q => q.IdEmpresa == IdEmpresa && q.IdMatricula == item.IdMatricula && q.IdMateria == IdMateria).ToList();
                                    if (lst_MatriculaCalificacion.Count > 0)
                                    {
                                        lst_MatriculaCalificacion.ForEach(q => q.IdProfesor = item.IdProfesor);
                                    }
                                }
                                
                            }
                        }
                    }
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<aca_MatriculaCalificacion_Info> getList(int IdEmpresa, decimal IdMatricula)
        {
            try
            {
                List<aca_MatriculaCalificacion_Info> Lista = new List<aca_MatriculaCalificacion_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.aca_MatriculaCalificacion.Where(q => q.IdEmpresa == IdEmpresa && q.IdMatricula== IdMatricula).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_MatriculaCalificacion_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdMatricula = q.IdMatricula,
                            IdMateria = q.IdMateria,
                            IdProfesor = q.IdProfesor,
                            CalificacionP1 = q.CalificacionP1,
                            CalificacionP2 = q.CalificacionP2,
                            CalificacionP3=q.CalificacionP3,
                            CalificacionP4 = q.CalificacionP4,
                            CalificacionP5 = q.CalificacionP5,
                            CalificacionP6=q.CalificacionP6,
                            ExamenQ1=q.ExamenQ1,
                            ExamenQ2=q.ExamenQ2,
                            ExamenRemedial=q.ExamenRemedial,
                            ExamenMejoramiento = q.ExamenMejoramiento,
                            CampoMejoramiento = q.CampoMejoramiento,
                            ExamenSupletorio = q.ExamenSupletorio,
                            ExamenGracia=q.ExamenGracia,
                            PromedioQ1 = q.PromedioQ1,
                            PromedioQ2=q.PromedioQ2,
                            PromedioFinalQ1=q.PromedioFinalQ1,
                            PromedioFinalQ2=q.PromedioFinalQ2,
                            PromedioFinal =q.PromedioFinal,
                            IdEquivalenciaPromedioP1 = q.IdEquivalenciaPromedioP1,
                            IdEquivalenciaPromedioP2 = q.IdEquivalenciaPromedioP2,
                            IdEquivalenciaPromedioP3 = q.IdEquivalenciaPromedioP2,
                            IdEquivalenciaPromedioEQ1 = q.IdEquivalenciaPromedioEQ1,
                            IdEquivalenciaPromedioQ1 = q.IdEquivalenciaPromedioQ1,
                            IdEquivalenciaPromedioP4 = q.IdEquivalenciaPromedioP4,
                            IdEquivalenciaPromedioP5 = q.IdEquivalenciaPromedioP5,
                            IdEquivalenciaPromedioP6 = q.IdEquivalenciaPromedioP6,
                            IdEquivalenciaPromedioEQ2 = q.IdEquivalenciaPromedioEQ2,
                            IdEquivalenciaPromedioQ2 = q.IdEquivalenciaPromedioQ2,
                            IdEquivalenciaPromedioPF = q.IdEquivalenciaPromedioPF
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

        public bool generarCalificacion(List<aca_MatriculaCalificacion_Info> lst_calificacion)
        {
            try
            {
                List<aca_MatriculaCalificacion_Info> Lista = new List<aca_MatriculaCalificacion_Info>();

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var lst_matricula = (from q in lst_calificacion
                                         group q by new
                                         {
                                             q.IdEmpresa,
                                             q.IdMatricula
                                         } into mat
                                         select new aca_Matricula_Info
                                         {
                                             IdEmpresa = mat.Key.IdEmpresa,
                                             IdMatricula = mat.Key.IdMatricula
                                         }).ToList();

                    foreach (var item in lst_matricula)
                    {
                        var lista_calificacion = Context.aca_MatriculaCalificacion.Where(q => q.IdEmpresa == item.IdEmpresa && q.IdMatricula == item.IdMatricula).ToList();
                        Context.aca_MatriculaCalificacion.RemoveRange(lista_calificacion);

                        var lst_x_matricula = lst_calificacion.Where(q => q.IdEmpresa == item.IdEmpresa && q.IdMatricula == item.IdMatricula).ToList();

                        if (lst_x_matricula != null)
                        {
                            foreach (var info in lst_x_matricula)
                            {
                                aca_MatriculaCalificacion Entity = new aca_MatriculaCalificacion
                                {
                                    IdEmpresa = info.IdEmpresa,
                                    IdMatricula = info.IdMatricula,
                                    IdMateria = info.IdMateria,
                                    IdProfesor = info.IdProfesor,
                                    CalificacionP1 = info.CalificacionP1,
                                    CalificacionP2= info.CalificacionP2,
                                    CalificacionP3 = info.CalificacionP3,
                                    CalificacionP4 = info.CalificacionP4,
                                    CalificacionP5 = info.CalificacionP5,
                                    CalificacionP6 = info.CalificacionP6,
                                    PromedioQ1 = info.PromedioQ1,
                                    ExamenQ1 = info.ExamenQ1,
                                    PromedioFinalQ1 = info.PromedioFinalQ1,
                                    PromedioQ2 = info.PromedioQ2,
                                    ExamenQ2 = info.ExamenQ2,
                                    PromedioFinalQ2 = info.PromedioFinalQ2,
                                    PromedioQuimestres =info.PromedioQuimestres,
                                    ExamenMejoramiento = info.ExamenMejoramiento,
                                    CampoMejoramiento = info.CampoMejoramiento,
                                    ExamenSupletorio = info.ExamenSupletorio,
                                    ExamenRemedial = info.ExamenRemedial,
                                    ExamenGracia = info.ExamenGracia,
                                    PromedioFinal = info.PromedioFinal,
                                    IdEquivalenciaPromedioP1 = info.IdEquivalenciaPromedioP1,
                                    IdEquivalenciaPromedioP2 = info.IdEquivalenciaPromedioP2,
                                    IdEquivalenciaPromedioP3 = info.IdEquivalenciaPromedioP2,
                                    IdEquivalenciaPromedioEQ1 = info.IdEquivalenciaPromedioEQ1,
                                    IdEquivalenciaPromedioQ1 = info.IdEquivalenciaPromedioQ1,
                                    IdEquivalenciaPromedioP4 = info.IdEquivalenciaPromedioP4,
                                    IdEquivalenciaPromedioP5 = info.IdEquivalenciaPromedioP5,
                                    IdEquivalenciaPromedioP6 = info.IdEquivalenciaPromedioP6,
                                    IdEquivalenciaPromedioEQ2 = info.IdEquivalenciaPromedioEQ2,
                                    IdEquivalenciaPromedioQ2 = info.IdEquivalenciaPromedioQ2,
                                    IdEquivalenciaPromedioPF = info.IdEquivalenciaPromedioPF,
                                };

                                Context.aca_MatriculaCalificacion.Add(Entity);
                            }
                        }
                        Context.SaveChanges();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<aca_MatriculaCalificacion_Info> getList_Combos(int IdEmpresa, int IdAnio, int IdSede)
        {
            try
            {
                List<aca_MatriculaCalificacion_Info> Lista = new List<aca_MatriculaCalificacion_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    odata.Database.CommandTimeout = 5000;
                    var lst = odata.vwaca_AnioLectivo_Paralelo_Profesor_Calificaciones.Where(q => q.IdEmpresa == IdEmpresa
                    && q.IdAnio == IdAnio && q.IdSede== IdSede).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_MatriculaCalificacion_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdMatricula = q.IdMatricula,
                            IdMateria = q.IdMateria,
                            IdProfesor = q.IdProfesor,
                            IdAnio = q.IdAnio,
                            IdSede = q.IdSede,
                            IdNivel = q.IdNivel,
                            IdJornada = q.IdJornada,
                            IdCurso = q.IdCurso,
                            IdParalelo = q.IdParalelo,
                            Descripcion = q.Descripcion,
                            NomSede = q.NomSede,
                            NomNivel = q.NomNivel,
                            OrdenNivel = q.OrdenNivel ?? 0,
                            NomJornada = q.NomJornada,
                            OrdenJornada = q.OrdenJornada ?? 0,
                            NomCurso = q.NomCurso,
                            OrdenCurso = q.OrdenCurso ?? 0,
                            NomParalelo = q.NomParalelo,
                            OrdenParalelo = q.OrdenParalelo ?? 0,
                            CodigoParalelo = q.CodigoParalelo,
                            IdProfesorTutor = q.IdProfesorTutor ?? 0,
                            IdProfesorInspector = q.IdProfesorInspector ?? 0,
                            NomMateria = q.NomMateria,
                            OrdenMateria = q.OrdenMateria,
                            EsObligatorio = q.EsObligatorio
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
        public List<aca_MatriculaCalificacion_Info> getList_Combos(int IdEmpresa, int IdAnio, int IdSede, decimal IdProfesor, bool EsSuperAdmin)
        {
            try
            {
                List<aca_MatriculaCalificacion_Info> Lista = new List<aca_MatriculaCalificacion_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    odata.Database.CommandTimeout = 5000;
                    var lst = odata.vwaca_AnioLectivo_Paralelo_Profesor_Calificaciones.Where(q => q.IdEmpresa == IdEmpresa
                    && q.IdAnio == IdAnio && q.IdSede == IdSede && q.IdProfesor == (EsSuperAdmin==true ? q.IdProfesor : IdProfesor) ).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_MatriculaCalificacion_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdMatricula = q.IdMatricula,
                            IdMateria = q.IdMateria,
                            IdProfesor=q.IdProfesor,
                            IdAnio = q.IdAnio,
                            IdSede = q.IdSede,
                            IdNivel=q.IdNivel,
                            IdJornada = q.IdJornada,
                            IdCurso = q.IdCurso,
                            IdParalelo = q.IdParalelo,
                            Descripcion = q.Descripcion,
                            NomSede = q.NomSede,
                            NomNivel = q.NomNivel,
                            OrdenNivel = q.OrdenNivel??0,
                            NomJornada = q.NomJornada,
                            OrdenJornada = q.OrdenJornada??0,
                            NomCurso = q.NomCurso,
                            OrdenCurso = q.OrdenCurso??0,
                            NomParalelo = q.NomParalelo,
                            OrdenParalelo = q.OrdenParalelo??0,
                            CodigoParalelo = q.CodigoParalelo,
                            IdProfesorTutor = q.IdProfesorTutor??0,
                            IdProfesorInspector = q.IdProfesorInspector??0,
                            NomMateria = q.NomMateria,
                            OrdenMateria = q.OrdenMateria,
                            EsObligatorio = q.EsObligatorio
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

        public List<aca_MatriculaCalificacion_Info> getList_Combos_Tutor(int IdEmpresa, int IdAnio, int IdSede, decimal IdProfesor, bool EsSuperAdmin)
        {
            try
            {
                List<aca_MatriculaCalificacion_Info> Lista = new List<aca_MatriculaCalificacion_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.vwaca_AnioLectivo_Paralelo_Profesor_Calificaciones.Where(q => q.IdEmpresa == IdEmpresa
                    && q.IdAnio == IdAnio && q.IdSede == IdSede && q.IdProfesorTutor == (EsSuperAdmin == true ? q.IdProfesorTutor : IdProfesor)).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_MatriculaCalificacion_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdMatricula = q.IdMatricula,
                            IdMateria = q.IdMateria,
                            IdProfesor = q.IdProfesor,
                            IdAnio = q.IdAnio,
                            IdSede = q.IdSede,
                            IdNivel = q.IdNivel,
                            IdJornada = q.IdJornada,
                            IdCurso = q.IdCurso,
                            IdParalelo = q.IdParalelo,
                            Descripcion = q.Descripcion,
                            NomSede = q.NomSede,
                            NomNivel = q.NomNivel,
                            OrdenNivel = q.OrdenNivel ?? 0,
                            NomJornada = q.NomJornada,
                            OrdenJornada = q.OrdenJornada ?? 0,
                            NomCurso = q.NomCurso,
                            OrdenCurso = q.OrdenCurso ?? 0,
                            NomParalelo = q.NomParalelo,
                            OrdenParalelo = q.OrdenParalelo ?? 0,
                            CodigoParalelo = q.CodigoParalelo,
                            IdProfesorTutor = q.IdProfesorTutor ?? 0,
                            IdProfesorInspector = q.IdProfesorInspector ?? 0,
                            NomMateria = q.NomMateria,
                            OrdenMateria = q.OrdenMateria,
                            EsObligatorio = q.EsObligatorio
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

        public List<aca_MatriculaCalificacion_Info> getList_Combos_Inspector(int IdEmpresa, int IdAnio, int IdSede, decimal IdProfesor, bool EsSuperAdmin)
        {
            try
            {
                List<aca_MatriculaCalificacion_Info> Lista = new List<aca_MatriculaCalificacion_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.vwaca_AnioLectivo_Paralelo_Profesor_Calificaciones.Where(q => q.IdEmpresa == IdEmpresa
                    && q.IdAnio == IdAnio && q.IdSede == IdSede && q.IdProfesorInspector == (EsSuperAdmin == true ? q.IdProfesorInspector : IdProfesor)).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_MatriculaCalificacion_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdMatricula = q.IdMatricula,
                            IdMateria = q.IdMateria,
                            IdProfesor = q.IdProfesor,
                            IdAnio = q.IdAnio,
                            IdSede = q.IdSede,
                            IdNivel = q.IdNivel,
                            IdJornada = q.IdJornada,
                            IdCurso = q.IdCurso,
                            IdParalelo = q.IdParalelo,
                            Descripcion = q.Descripcion,
                            NomSede = q.NomSede,
                            NomNivel = q.NomNivel,
                            OrdenNivel = q.OrdenNivel ?? 0,
                            NomJornada = q.NomJornada,
                            OrdenJornada = q.OrdenJornada ?? 0,
                            NomCurso = q.NomCurso,
                            OrdenCurso = q.OrdenCurso ?? 0,
                            NomParalelo = q.NomParalelo,
                            OrdenParalelo = q.OrdenParalelo ?? 0,
                            CodigoParalelo = q.CodigoParalelo,
                            IdProfesorTutor = q.IdProfesorTutor ?? 0,
                            IdProfesorInspector = q.IdProfesorInspector ?? 0,
                            NomMateria = q.NomMateria,
                            OrdenMateria = q.OrdenMateria,
                            EsObligatorio = q.EsObligatorio
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
        public bool modificarDB(aca_MatriculaCalificacion_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var info_matricula = odata_matricula.getInfo(info.IdEmpresa, info.IdMatricula);
                    aca_MatriculaCalificacion EntityCalificacion = Context.aca_MatriculaCalificacion.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa
                    && q.IdMatricula == info.IdMatricula && q.IdProfesor == info.IdProfesor && q.IdMateria == info.IdMateria);
                    if (EntityCalificacion == null)
                        return false;

                    if (info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoExamen.EXQUI1))
                    {
                        EntityCalificacion.ExamenQ1 = info.CalificacionExamen;
                        EntityCalificacion.PromedioFinalQ1 = info.Promedio;
                        EntityCalificacion.IdEquivalenciaPromedioQ1 = info.IdEquivalenciaPromedio;
                        EntityCalificacion.CausaQ1 = info.Causa;
                        EntityCalificacion.ResolucionQ1 = info.Resolucion;
                        EntityCalificacion.IdEquivalenciaPromedioEQ1 = info.IdEquivalenciaCalificacionExamen;

                        var PromedioQuimestral = (decimal?)null;
                        if (EntityCalificacion.PromedioFinalQ1 != null && EntityCalificacion.PromedioFinalQ2 != null)
                        {
                            PromedioQuimestral = Math.Round(((Convert.ToDecimal(EntityCalificacion.PromedioFinalQ1 + EntityCalificacion.PromedioFinalQ2)) / 2), 2, MidpointRounding.AwayFromZero);
                        }

                        EntityCalificacion.PromedioQuimestres = PromedioQuimestral;
                    }
                        
                    if (info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoExamen.EXQUI2))
                    {
                        EntityCalificacion.ExamenQ2 = info.CalificacionExamen;
                        EntityCalificacion.PromedioFinalQ2 = info.Promedio;
                        EntityCalificacion.IdEquivalenciaPromedioQ2 = info.IdEquivalenciaPromedio;
                        EntityCalificacion.CausaQ2 = info.Causa;
                        EntityCalificacion.ResolucionQ2 = info.Resolucion;
                        EntityCalificacion.IdEquivalenciaPromedioEQ2 = info.IdEquivalenciaCalificacionExamen;

                        var PromedioQuimestral = (decimal?)null;
                        if (EntityCalificacion.PromedioFinalQ1!= null && EntityCalificacion.PromedioFinalQ2!=null)
                        {
                            PromedioQuimestral = Math.Round(((Convert.ToDecimal(EntityCalificacion.PromedioFinalQ1 + EntityCalificacion.PromedioFinalQ2)) / 2), 2, MidpointRounding.AwayFromZero);
                        }

                        EntityCalificacion.PromedioQuimestres = PromedioQuimestral; 
                    }

                    if (info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoExamen.EXMEJ))
                    {
                        EntityCalificacion.ExamenMejoramiento = info.CalificacionExamen;
                        EntityCalificacion.PromedioFinal = info.Promedio;
                        EntityCalificacion.IdEquivalenciaPromedioPF = info.IdEquivalenciaPromedio;
                    }
                        

                    if (info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoExamen.EXSUP))
                    {
                        EntityCalificacion.ExamenSupletorio = info.CalificacionExamen;
                        EntityCalificacion.PromedioFinal = info.Promedio;
                        EntityCalificacion.IdEquivalenciaPromedioPF = info.IdEquivalenciaPromedio;
                    }

                    if (info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoExamen.EXREM))
                    {
                        EntityCalificacion.ExamenRemedial = info.CalificacionExamen;
                        EntityCalificacion.PromedioFinal = info.Promedio;
                        EntityCalificacion.IdEquivalenciaPromedioPF = info.IdEquivalenciaPromedio;
                    }

                    if (info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoExamen.EXGRA))
                    {
                        EntityCalificacion.ExamenGracia = info.CalificacionExamen;
                        EntityCalificacion.PromedioFinal = info.Promedio;
                        EntityCalificacion.IdEquivalenciaPromedioPF = info.IdEquivalenciaPromedio;
                    }

                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool modicarPaseAnioDB(aca_MatriculaCalificacion_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_MatriculaCalificacion EntityCalificacion = Context.aca_MatriculaCalificacion.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa
                    && q.IdMatricula == info.IdMatricula && q.IdProfesor == info.IdProfesor && q.IdMateria == info.IdMateria);
                    if (EntityCalificacion == null)
                        return false;

                    EntityCalificacion.PromedioQuimestres = info.PromedioQuimestres;
                    EntityCalificacion.CampoMejoramiento = info.CampoMejoramiento;
                    EntityCalificacion.PromedioFinal = info.PromedioFinal;
                    EntityCalificacion.IdEquivalenciaPromedioPF = info.IdEquivalenciaPromedioPF;

                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
