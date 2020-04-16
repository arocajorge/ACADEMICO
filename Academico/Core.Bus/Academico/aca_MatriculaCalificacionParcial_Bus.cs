using Core.Data.Academico;
using Core.Info.Academico;
using Core.Info.Helps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Academico
{
    public class aca_MatriculaCalificacionParcial_Bus
    {
        aca_MatriculaCalificacionParcial_Data odata = new aca_MatriculaCalificacionParcial_Data();
        aca_AnioLectivoParcial_Data odata_parcial = new aca_AnioLectivoParcial_Data();
        aca_Alumno_Bus odata_alumno = new aca_Alumno_Bus();
        aca_MatriculaCalificacion_Data odata_calificacion = new aca_MatriculaCalificacion_Data();
        aca_AnioLectivo_Data odata_anio = new aca_AnioLectivo_Data();
        aca_Matricula_Data odata_matricula = new aca_Matricula_Data();

        public List<aca_MatriculaCalificacionParcial_Info> GetList(int IdEmpresa, decimal IdMatricula)
        {
            try
            {
                return odata.getList(IdEmpresa, IdMatricula);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<aca_MatriculaCalificacionParcial_Info> GetList(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, int IdMateria, int IdCatalogoParcial, decimal IdProfesor)
        {
            try
            {
                return odata.getList(IdEmpresa, IdSede, IdAnio, IdNivel, IdJornada, IdCurso, IdParalelo, IdMateria, IdCatalogoParcial, IdProfesor);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool GenerarCalificacion(List<aca_MatriculaCalificacionParcial_Info> lst_parcial)
        {
            try
            {
                return odata.generarCalificacion(lst_parcial);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool ModicarDB(aca_MatriculaCalificacionParcial_Info info)
        {
            try
            {
                if (odata.modificarDB(info))
                {
                    var info_matricula = odata_matricula.getInfo(info.IdEmpresa, info.IdMatricula);
                    var info_parcial = odata_parcial.getInfo(info.IdEmpresa, info_matricula.IdSede, info_matricula.IdAnio, info.IdCatalogoParcial);

                    if (info_parcial != null)
                    {
                        if (info_parcial.ValidaEstadoAlumno == true)
                        {
                            var info_alumno = odata_alumno.GetInfo(info_matricula.IdEmpresa, info_matricula.IdAlumno);
                            var info_calificacion = odata_calificacion.getInfo_modificar(info_matricula.IdEmpresa, info_matricula.IdAnio, info_matricula.IdSede, info_matricula.IdNivel, info_matricula.IdJornada, info_matricula.IdCurso, info_matricula.IdParalelo, info.IdMateria, info_matricula.IdAlumno);
                            var info_anio = odata_anio.getInfo(info.IdEmpresa, info_matricula.IdAnio);

                            decimal PromedioFinal = 0;
                            decimal PromedioFinalTemp = 0;
                            decimal PromedioMinimoPromocion = Math.Round(Convert.ToDecimal(info_anio.PromedioMinimoPromocion), 2, MidpointRounding.AwayFromZero);

                            if(info_calificacion!=null)
                            {
                                string CampoMejoramiento = null;
                                decimal PromedioFinalQ1 = Convert.ToDecimal(info_calificacion.PromedioFinalQ1);
                                decimal PromedioFinalQ2 = Convert.ToDecimal(info_calificacion.PromedioFinalQ2);
                                decimal ExamenMejoramiento = Convert.ToDecimal(info_calificacion.ExamenMejoramiento);
                                decimal ExamenSupletorio = Convert.ToDecimal(info_calificacion.ExamenSupletorio);
                                decimal ExamenRemedial = Convert.ToDecimal(info_calificacion.ExamenRemedial);
                                decimal ExamenGracia = Convert.ToDecimal(info_calificacion.ExamenGracia);
                                PromedioFinalTemp = Math.Round(Convert.ToDecimal((PromedioFinalQ1 + PromedioFinalQ2) / 2), 2, MidpointRounding.AwayFromZero);

                                if (PromedioFinalTemp < PromedioMinimoPromocion)
                                {
                                    if (ExamenSupletorio >= PromedioMinimoPromocion)
                                    {
                                        PromedioFinal = PromedioMinimoPromocion;
                                    }
                                    else if (ExamenRemedial >= PromedioMinimoPromocion)
                                    {
                                        PromedioFinal = PromedioMinimoPromocion;
                                    }
                                    else if (ExamenGracia >= PromedioMinimoPromocion)
                                    {
                                        PromedioFinal = PromedioMinimoPromocion;
                                    }
                                    else
                                    {
                                        PromedioFinal = PromedioFinalTemp;
                                    }

                                }
                                else
                                {
                                    if (ExamenMejoramiento > 0)
                                    {
                                        if (PromedioFinalQ1 < PromedioFinalQ2)
                                        {
                                            CampoMejoramiento = "Q1";
                                            PromedioFinal = Math.Round(Convert.ToDecimal((ExamenMejoramiento + PromedioFinalQ2) / 2), 2, MidpointRounding.AwayFromZero);
                                        }
                                        else if (PromedioFinalQ2 < PromedioFinalQ1)
                                        {
                                            CampoMejoramiento = "Q2";
                                            PromedioFinal = Math.Round(Convert.ToDecimal((PromedioFinalQ1 + ExamenMejoramiento) / 2), 2, MidpointRounding.AwayFromZero);
                                        }
                                        else
                                        {
                                            PromedioFinal = PromedioFinalTemp;
                                        }
                                    }
                                    else
                                    {
                                        PromedioFinal = PromedioFinalTemp;
                                    }
                                }
                                info_calificacion.PromedioFinal = PromedioFinal;
                                info_calificacion.CampoMejoramiento = CampoMejoramiento;

                                odata_calificacion.modicarPaseAnioDB(info_calificacion);

                                if (PromedioFinal < PromedioMinimoPromocion)
                                {
                                    info_alumno.IdCatalogoESTMAT = Convert.ToInt32(cl_enumeradores.eCatalogoAcademicoAlumno.SUPLENCIA);
                                    odata_alumno.PaseAnioDB(info_alumno);
                                }
                            }
                        }
                    }
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
