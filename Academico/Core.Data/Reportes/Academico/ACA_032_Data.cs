using Core.Data.Base;
using Core.Info.Helps;
using Core.Info.Reportes.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Reportes.Academico
{
    public class ACA_032_Data
    {
        public List<ACA_032_Info> get_list(int IdEmpresa, int IdAnio, int IdSede, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, string IdCatalogoParcialTipo, bool MostrarRetirados)
        {
            try
            {
                List<ACA_032_Info> Lista = new List<ACA_032_Info>();
                using (EntitiesReportes Context = new EntitiesReportes())
                {
                    Context.Database.CommandTimeout = 5000;
                    var lst = Context.SPACA_032(IdEmpresa, IdAnio, IdSede, IdNivel, IdJornada, IdCurso, IdParalelo, MostrarRetirados).ToList();

                    foreach (var q in lst)
                    {
                        Lista.Add(new ACA_032_Info
                        {
                            Num = 1,
                            IdEmpresa = q.IdEmpresa,
                            IdMatricula = q.IdMatricula,
                            IdAlumno = q.IdAlumno,
                            pe_nombreCompleto = q.pe_nombreCompleto,
                            NombreInspector = q.NombreInspector,
                            IdProfesorInspector = q.IdProfesorInspector,
                            Codigo = q.Codigo,
                            IdAnio = q.IdAnio,
                            IdSede = q.IdSede,
                            IdNivel = q.IdNivel,
                            IdJornada = q.IdJornada,
                            IdCurso = q.IdCurso,
                            IdParalelo = q.IdParalelo,
                            Descripcion = q.Descripcion,
                            NomSede = q.NomSede,
                            NomNivel = q.NomNivel,
                            NomJornada = q.NomJornada,
                            NomCurso = q.NomCurso,
                            NomParalelo = q.NomParalelo,
                            OrdenNivel = q.OrdenNivel,
                            OrdenJornada = q.OrdenJornada,
                            OrdenCurso = q.OrdenCurso,
                            OrdenParalelo = q.OrdenParalelo,
                            CodigoParalelo = q.CodigoParalelo,
                            Letra = ((IdCatalogoParcialTipo == cl_enumeradores.eTipoCatalogoAcademicoConductaFinal.QUIMESTRE_1.ToString()) ? q.LetraQ1 :
                                                (IdCatalogoParcialTipo == cl_enumeradores.eTipoCatalogoAcademicoConductaFinal.QUIMESTRE_2.ToString()) ? q.LetraQ2 :
                                                (IdCatalogoParcialTipo == cl_enumeradores.eTipoCatalogoAcademicoConductaFinal.PROMEDIOFINAL.ToString()) ? q.LetraPF :
                                                null
                                                ),
                            Motivo = ((IdCatalogoParcialTipo == cl_enumeradores.eTipoCatalogoAcademicoConductaFinal.QUIMESTRE_1.ToString()) ? q.MotivoPromedioFinalQ1 :
                                                (IdCatalogoParcialTipo == cl_enumeradores.eTipoCatalogoAcademicoConductaFinal.QUIMESTRE_2.ToString()) ? q.MotivoPromedioFinalQ2 :
                                                (IdCatalogoParcialTipo == cl_enumeradores.eTipoCatalogoAcademicoConductaFinal.PROMEDIOFINAL.ToString()) ? q.MotivoPromedioFinal :
                                                null
                                                ),
                            EsRetirado=q.EsRetirado,
                            EsRetiradoString = q.EsRetiradoString
                        });
                    }
                }

                return Lista;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
