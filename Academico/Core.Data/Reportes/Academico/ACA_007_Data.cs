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
        public List<ACA_007_Info> get_list(int IdEmpresa, int IdSede, int IdAnio, int IdJornada, int IdNivel, int IdCurso, int IdParalelo, DateTime fecha_ini, DateTime fecha_fin)
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

                List<ACA_007_Info> Lista;
                using (EntitiesReportes Context = new EntitiesReportes())
                {
                    Lista = Context.VWACA_007.Where(q => q.IdEmpresa == IdEmpresa && 
                    q.IdSede >= IdSedeIni && q.IdSede<=IdSedeFin &&
                    q.IdAnio >= IdAnioIni && q.IdAnio <= IdAnioFin &&
                    q.IdNivel >= IdNivelIni && q.IdNivel <= IdNivelFin &&
                    q.IdJornada >= IdJornadaIni && q.IdJornada <= IdJornadaFin &&
                    q.IdCurso >= IdCursoIni && q.IdCurso <= IdCursoFin &&
                    q.IdParalelo >= IdParaleloIni && q.IdParalelo <= IdParaleloFin &&
                    q.Fecha >= fecha_ini.Date && q.Fecha<=fecha_fin.Date).OrderBy(q=> new {q.OrdenNivel, q.OrdenJornada, q.OrdenCurso, q.OrdenParalelo }).Select(q => new ACA_007_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdMatricula = q.IdMatricula,
                        Cantidad = q.Cantidad,
                        CodigoParalelo = q.CodigoParalelo,
                        Fecha = q.Fecha,
                        IdAnio= q.IdAnio,
                        IdCurso=q.IdCurso,
                        IdJornada=q.IdJornada,
                        IdNivel=q.IdNivel,
                        IdParalelo=q.IdParalelo,
                        IdPlantilla=q.IdPlantilla,
                        IdTipoPlantilla = q.IdTipoPlantilla,
                        NomPlantillaTipo = q.NomPlantillaTipo,
                        OrdenCurso=q.OrdenCurso,
                        OrdenJornada=q.OrdenJornada,
                        OrdenNivel=q.OrdenNivel,
                        IdSede = q.IdSede,
                        NomJornada=q.NomJornada,
                        NomNivel=q.NomNivel,
                        NomParalelo=q.NomParalelo,
                        NomPlantilla=q.NomPlantilla,
                        NomSede=q.NomSede,
                        OrdenParalelo = q.OrdenParalelo,
                        pe_sexo =q.pe_sexo,
                        NomCurso = q.NomCurso,
                        Descripcion=q.Descripcion

                    }).ToList();
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
