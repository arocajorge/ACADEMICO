using Core.Data.Base;
using Core.Info.Reportes.Academico;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Data.Reportes.Academico
{
    public class ACA_008_Data
    {
        public List<ACA_008_Info> GetList(int IdEmpresa, int IdAnio, int IdSede, int IdNivel, int IdJornada, int IdCurso,int IdParalelo, bool MostarPlantilla)
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
                    Lista = Context.VWACA_008.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio &&
                    IdSedeIni <= q.IdSede && q.IdSede <= IdSedeFin &&
                    IdNivelIni <= q.IdNivel && q.IdNivel <= IdNivelFin &&
                    IdJornadaIni <= q.IdJornada && q.IdJornada <= IdJornadaFin &&
                    IdCursoIni <= q.IdCurso && q.IdCurso <= IdCursoFin &&
                    IdParaleloIni <= q.IdCurso && q.IdCurso <= IdParaleloFin
                 ).Select(q => new ACA_008_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        NomSede = q.NomSede,
                        NomNivel = q.NomNivel,
                        OrdenNivel = q.OrdenNivel,
                        NomJornada = q.NomJornada,
                        OrdenJornada = q.OrdenJornada,
                        OrdenCurso = q.OrdenCurso,
                        NomCurso = q.NomCurso,
                        CodigoParalelo = q.CodigoParalelo,
                        NomParalelo = q.NomParalelo,
                        OrdenParalelo = q.OrdenParalelo,
                        pe_sexo = q.pe_sexo,
                        Cantidad = q.Cantidad,
                        IdMatricula = q.IdMatricula,
                        IdAnio = q.IdAnio,
                        IdSede = q.IdSede,
                        IdNivel = q.IdNivel,
                        IdJornada = q.IdJornada,
                        IdCurso = q.IdCurso,
                        IdParalelo = q.IdParalelo,
                        Fecha = q.Fecha,
                        NomPlantilla =(MostarPlantilla==true ? q.NomPlantilla: ""),
                        IdPlantilla = q.IdPlantilla,
                        Descripcion = q.Descripcion,
                        pe_nombreCompleto = q.pe_nombreCompleto,
                        CodigoAlumno = q.CodigoAlumno


                    }).ToList();
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
