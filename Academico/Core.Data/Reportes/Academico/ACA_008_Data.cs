using Core.Data.Base;
using Core.Info.Reportes.Academico;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Data.Reportes.Academico
{
    public class ACA_008_Data
    {
        public List<ACA_008_Info> GetList(int IdEmpresa, int IdAnio, int IdSede, int IdNivel, int IdJornada, int IdCurso,int IdParalelo, bool MostarPlantilla, bool MostrarAlumnosRetirados)
        {
            try
            {
                int IdSedeIni = IdSede;
                int IdSedeFin = IdSede == 0 ? 9999999 : IdSede;

                int IdNivelIni = IdNivel;
                int IdNivelFin = IdNivel == 0 ? 9999999 : IdNivel;

                int IdJornadaIni = IdJornada;
                int IdJornadaFin = IdJornada == 0 ? 9999999 : IdJornada;

                int IdCursoIni = IdCurso;
                int IdCursoFin = IdCurso == 0 ? 9999999 : IdCurso;

                int IdParaleloIni = IdParalelo;
                int IdParaleloFin = IdParalelo == 0 ? 9999999 : IdParalelo;

                List<ACA_008_Info> Lista = new List<ACA_008_Info>();
                using (EntitiesReportes Context = new EntitiesReportes())
                {
                    //Context.SetCommandTimeOut(5000);

                    var lst = Context.VWACA_008.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio &&
                    IdSedeIni <= q.IdSede && q.IdSede <= IdSedeFin &&
                    IdNivelIni <= q.IdNivel && q.IdNivel <= IdNivelFin &&
                    IdJornadaIni <= q.IdJornada && q.IdJornada <= IdJornadaFin &&
                    IdCursoIni <= q.IdCurso && q.IdCurso <= IdCursoFin &&
                    IdParaleloIni <= q.IdParalelo && q.IdParalelo <= IdParaleloFin
                    && (MostrarAlumnosRetirados == true ? q.IdRetiro == q.IdRetiro : q.IdRetiro == 0)).ToList();
                    foreach (var item in lst)
                    {
                        Lista.Add(new ACA_008_Info
                        {
                            Num=item.Num,
                            IdEmpresa = item.IdEmpresa,
                            NomSede = item.NomSede,
                            NomNivel = item.NomNivel,
                            OrdenNivel = item.OrdenNivel,
                            NomJornada = item.NomJornada,
                            OrdenJornada = item.OrdenJornada,
                            OrdenCurso = item.OrdenCurso,
                            NomCurso = item.NomCurso,
                            CodigoParalelo = item.CodigoParalelo,
                            NomParalelo = item.NomParalelo,
                            OrdenParalelo = item.OrdenParalelo,
                            pe_sexo = item.pe_sexo,
                            Cantidad = item.Cantidad,
                            IdMatricula = item.IdMatricula,
                            IdAnio = item.IdAnio,
                            IdSede = item.IdSede,
                            IdNivel = item.IdNivel,
                            IdJornada = item.IdJornada,
                            IdCurso = item.IdCurso,
                            IdParalelo = item.IdParalelo,
                            Fecha = item.Fecha,
                            NomPlantilla = (MostarPlantilla == true ? item.NomPlantilla : ""),
                            IdPlantilla = item.IdPlantilla,
                            Descripcion = item.Descripcion,
                            pe_nombreCompleto = item.pe_nombreCompleto,
                            CodigoAlumno = item.CodigoAlumno,
                            IdTipoPlantilla = item.IdTipoPlantilla,
                            NomPlantillaTipo = (MostarPlantilla == true ? item.NomPlantillaTipo : ""),
                            EstaRetirado = item.EstaRetirado
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
