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
        public List<ACA_007_Info> get_list(int IdEmpresa, DateTime fecha_ini, DateTime fecha_fin)
        {
            try
            {
                List<ACA_007_Info> Lista;
                using (EntitiesReportes Context = new EntitiesReportes())
                {
                    Lista = Context.VWACA_007.Where(q => q.IdEmpresa == IdEmpresa && q.Fecha >= fecha_ini && q.Fecha<=fecha_fin).Select(q => new ACA_007_Info
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
            catch (Exception)
            {

                throw;
            }
        }
    }
}
