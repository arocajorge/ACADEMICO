using Core.Data.Base;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_MatriculaCambios_Data
    {
        public int getSecuenciaByMatricula(int IdEmpresa, decimal IdMatricula)
        {
            try
            {
                int Secuencia = 1;

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var cont = Context.aca_MatriculaCambios.Where(q => q.IdEmpresa == IdEmpresa && q.IdMatricula==IdMatricula).Count();
                    if (cont > 0)
                        Secuencia = Context.aca_MatriculaCambios.Where(q => q.IdEmpresa == IdEmpresa && q.IdMatricula == IdMatricula).Max(q => q.Secuencia) + 1;
                }

                return Secuencia;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public aca_MatriculaCambios_Info getInfo_UltimoCambioParalelo(int IdEmpresa, decimal IdMatricula)
        {
            try
            {
                aca_MatriculaCambios_Info info = new aca_MatriculaCambios_Info();

                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var MaximaSecuencia = 0;
                    var Cambios = db.aca_MatriculaCambios.Where(q => q.IdEmpresa == IdEmpresa && q.IdMatricula == IdMatricula && q.TipoCambio == "CURSOPARALELO").ToList();
                    if (Cambios.Count > 0)
                    {
                        MaximaSecuencia = Cambios.Max(q => q.Secuencia);
                    }

                    var Entity = db.aca_MatriculaCambios.Where(q => q.IdEmpresa == IdEmpresa && q.IdMatricula == IdMatricula && q.TipoCambio == "CURSOPARALELO" && q.Secuencia== MaximaSecuencia).FirstOrDefault();
                    if (Entity == null)
                        return new aca_MatriculaCambios_Info();

                    info = new aca_MatriculaCambios_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdMatricula = Entity.IdMatricula,
                        IdAnio = Entity.IdAnio,
                        IdSede = Entity.IdSede,
                        IdNivel = Entity.IdNivel,
                        IdJornada = Entity.IdJornada,
                        IdCurso = Entity.IdCurso,
                        IdParalelo = Entity.IdParalelo,
                        IdPlantilla = Entity.IdPlantilla,
                        Observacion = Entity.Observacion,
                        FechaCreacion = Entity.FechaCreacion,
                        IdUsuarioCreacion = Entity.IdUsuarioCreacion,
                        Secuencia = Entity.Secuencia,
                        TipoCambio = Entity.TipoCambio
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public aca_MatriculaCambios_Info getInfo_UltimoCambioPlantilla(int IdEmpresa, decimal IdMatricula)
        {
            try
            {
                aca_MatriculaCambios_Info info = new aca_MatriculaCambios_Info();

                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var MaximaSecuencia = 0;
                    var Cambios = db.aca_MatriculaCambios.Where(q => q.IdEmpresa == IdEmpresa && q.IdMatricula == IdMatricula && q.TipoCambio == "PLANTILLA").ToList();
                    if (Cambios.Count>0)
                    {
                        MaximaSecuencia = Cambios.Max(q => q.Secuencia);
                    }

                    var Entity = db.aca_MatriculaCambios.Where(q => q.IdEmpresa == IdEmpresa && q.IdMatricula == IdMatricula && q.TipoCambio == "PLANTILLA" && q.Secuencia== MaximaSecuencia).FirstOrDefault();
                    if (Entity == null)
                        return new aca_MatriculaCambios_Info();

                    info = new aca_MatriculaCambios_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdMatricula = Entity.IdMatricula,
                        IdAnio = Entity.IdAnio,
                        IdSede = Entity.IdSede,
                        IdNivel = Entity.IdNivel,
                        IdJornada = Entity.IdJornada,
                        IdCurso = Entity.IdCurso,
                        IdParalelo = Entity.IdParalelo,
                        IdPlantilla = Entity.IdPlantilla,
                        Observacion = Entity.Observacion,
                        FechaCreacion = Entity.FechaCreacion,
                        IdUsuarioCreacion = Entity.IdUsuarioCreacion,
                        Secuencia = Entity.Secuencia,
                        TipoCambio = Entity.TipoCambio
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
