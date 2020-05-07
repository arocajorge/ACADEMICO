using Core.Data.Base;
using Core.Info.Reportes.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Reportes.Academico
{
    public class ACA_007_Data
    {
        public List<ACA_007_Info> get_list(int IdEmpresa, int IdSede, int IdAnio, int IdJornada, int IdNivel, int IdCurso, int IdParalelo, DateTime fecha_ini, DateTime fecha_fin, bool MostrarAlumnosRetirados)
        {
            try
            {
                fecha_ini = fecha_ini.Date;
                fecha_fin = fecha_fin.Date;

                int IdSedeIni = IdSede;
                int IdSedeFin = IdSede == 0 ? 9999999 : IdSede;

                int IdAnioIni = IdAnio;
                int IdAnioFin = IdAnio == 0 ? 9999999 : IdAnio;

                int IdNivelIni = IdNivel;
                int IdNivelFin = IdNivel == 0 ? 9999999 : IdNivel;

                int IdJornadaIni = IdJornada;
                int IdJornadaFin = IdJornada == 0 ? 9999999 : IdJornada;

                int IdCursoIni = IdCurso;
                int IdCursoFin = IdCurso == 0 ? 9999999 : IdCurso;

                int IdParaleloIni = IdParalelo;
                int IdParaleloFin = IdParalelo == 0 ? 9999999 : IdParalelo;

                List<ACA_007_Info> Lista = new List<ACA_007_Info>();
                using (EntitiesReportes Context = new EntitiesReportes())
                {
                    Context.Database.CommandTimeout = 5000;
                    //Context.SetCommandTimeOut(5000);
                    var lst = Context.SPACA_007(IdEmpresa, IdAnio, IdSede, IdNivel, IdJornada, IdCurso, IdParalelo, fecha_ini, fecha_fin, MostrarAlumnosRetirados).ToList();

                    foreach (var q in lst)
                    {
                        if (q.EsRetirado == (MostrarAlumnosRetirados == true ? q.EsRetirado : false))
                        {
                            Lista.Add(new ACA_007_Info
                            {
                                IdEmpresa = q.IdEmpresa,
                                IdMatricula = q.IdMatricula,
                                Cantidad = q.Cantidad,
                                CodigoParalelo = q.CodigoParalelo,
                                Fecha = q.Fecha,
                                IdAnio = q.IdAnio,
                                IdCurso = q.IdCurso,
                                IdJornada = q.IdJornada,
                                IdNivel = q.IdNivel,
                                IdParalelo = q.IdParalelo,
                                IdPlantilla = q.IdPlantilla,
                                IdTipoPlantilla = q.IdTipoPlantilla,
                                NomPlantillaTipo = q.NomPlantillaTipo,
                                OrdenCurso = q.OrdenCurso ?? 0,
                                OrdenJornada = q.OrdenJornada,
                                OrdenNivel = q.OrdenNivel,
                                IdSede = q.IdSede,
                                NomJornada = q.NomJornada,
                                NomNivel = q.NomNivel,
                                NomParalelo = q.NomParalelo,
                                NomPlantilla = q.NomPlantilla,
                                NomSede = q.NomSede,
                                OrdenParalelo = q.OrdenParalelo,
                                pe_sexo = q.pe_sexo,
                                NomCurso = q.NomCurso,
                                Descripcion = q.Descripcion,
                                EsRetirado = q.EsRetirado,
                                EsRetiradoString = q.EsRetiradoString

                            });

                        }
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
