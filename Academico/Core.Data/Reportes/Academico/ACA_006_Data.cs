using Core.Data.Base;
using Core.Info.Reportes.Academico;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Data.Reportes.Academico
{
    public class ACA_006_Data
    {
        public List<ACA_006_Info> Getlist(int IdEmpresa, int IdSede, int IdAnio, int IdJornada, int IdNivel, int IdCurso, int IdParalelo, DateTime fecha_ini, DateTime fecha_fin, bool MostrarAlumnosRetirados)
        {
            try
            {
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

                List<ACA_006_Info> Lista = new List<ACA_006_Info>();
                using (EntitiesReportes db = new EntitiesReportes())
                {
                    Lista = db.VWACA_006.Where(q => q.IdEmpresa == IdEmpresa &&
                    q.IdSede >= IdSedeIni && q.IdSede <= IdSedeFin &&
                    q.IdAnio >= IdAnioIni && q.IdAnio <= IdAnioFin &&
                    q.IdNivel >= IdNivelIni && q.IdNivel <= IdNivelFin &&
                    q.IdJornada >= IdJornadaIni && q.IdJornada <= IdJornadaFin &&
                    q.IdCurso >= IdCursoIni && q.IdCurso <= IdCursoFin &&
                    q.IdParalelo >= IdParaleloIni && q.IdParalelo <= IdParaleloFin &&
                    q.Fecha >= fecha_ini.Date && q.Fecha <= fecha_fin.Date
                    && (MostrarAlumnosRetirados == true ? q.EsRetirado == q.EsRetirado : q.EsRetirado == false)).Select(q => new ACA_006_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdAnio = q.IdAnio,
                        IdSede = q.IdSede,
                        IdNivel = q.IdNivel,
                        IdJornada = q.IdJornada,
                        IdCurso = q.IdCurso,
                        NomSede = q.NomSede,
                        NomJornada = q.NomJornada,
                        NomCurso = q.NomCurso,
                        NomNivel = q.NomNivel,
                        OrdenJornada = q.OrdenJornada??0,
                        OrdenNivel = q.OrdenNivel??0,
                        OrdenCurso = q.OrdenCurso??0,
                        OrdenParalelo = q.OrdenParalelo??0,
                        CodigoParalelo = q.CodigoParalelo,
                        IdMatricula = q.IdMatricula,
                        IdParalelo = q.IdParalelo,
                        NomParalelo = q.NomParalelo,
                        pe_sexo = q.pe_sexo,
                        NomSexo=q.NomSexo,
                        Cantidad = q.Cantidad,
                        Descripcion = q.Descripcion,
                        Fecha = q.Fecha

                    }).ToList();
                }

                    return Lista;
             }
            catch (Exception EX)
            {
                throw;
            }
        }
            
    }
}

