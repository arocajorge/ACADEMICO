using Core.Data.Base;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_Plantilla_Rubro_Data
    {
        public List<aca_Plantilla_Rubro_Info> get_list_asignacion(int IdEmpresa, int IdAnio)
        {
            try
            {
                List<aca_Plantilla_Rubro_Info> Lista;

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    Lista = (from q in Context.aca_Plantilla_Rubro
                             join j in Context.aca_AnioLectivo_Rubro on new { q.IdEmpresa,q.IdAnio, q.IdRubro } equals new { j.IdEmpresa, IdAnio, j.IdRubro }
                             where q.IdEmpresa == IdEmpresa
                             && q.IdAnio == IdAnio
                             select new aca_Plantilla_Rubro_Info
                             {
                                 seleccionado = true,
                                 IdEmpresa = q.IdEmpresa,
                                 IdAnio = q.IdAnio,
                                 IdRubro = q.IdRubro,
                                 NomRubro = j.NomRubro,
                                 IdProducto= j.IdProducto,
                                 IdCod_Impuesto_Iva = j.IdCod_Impuesto_Iva,
                                 Subtotal = j.Subtotal,
                                 Porcentaje = j.Porcentaje,
                                 Total = j.Total
                             }).ToList();


                    Lista.AddRange((from j in Context.aca_AnioLectivo_Rubro
                                    where !Context.aca_Plantilla_Rubro.Any(n => n.IdRubro == j.IdRubro && n.IdEmpresa == IdEmpresa && n.IdAnio == IdAnio)
                                    where j.IdEmpresa == IdEmpresa
                                    && j.IdAnio == IdAnio
                                    select new aca_Plantilla_Rubro_Info
                                    {
                                        seleccionado = false,
                                        IdEmpresa = IdEmpresa,
                                        IdAnio = IdAnio,
                                        IdRubro = j.IdRubro,
                                        NomRubro = j.NomRubro,
                                        IdProducto = j.IdProducto,
                                        IdCod_Impuesto_Iva = j.IdCod_Impuesto_Iva,
                                        Subtotal = j.Subtotal,
                                        Porcentaje = j.Porcentaje,
                                        Total = j.Total
                                    }).ToList());
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
