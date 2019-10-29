using Core.Data.Base;
using Core.Info.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.General
{
    public class tb_parroquia_Data
    {
        public List<tb_parroquia_Info> get_list(string IdCiudad, bool mostrar_anulados)
        {
            try
            {
                List<tb_parroquia_Info> Lista;

                using (EntitiesGeneral Context = new EntitiesGeneral())
                {
                    if (mostrar_anulados)
                    {
                        if (!string.IsNullOrEmpty(IdCiudad))
                            Lista = (from q in Context.tb_parroquia
                                     where q.IdCiudad_Canton == IdCiudad
                                     select new tb_parroquia_Info
                                     {
                                         IdCiudad_Canton = q.IdCiudad_Canton,
                                         IdParroquia = q.IdParroquia,
                                         cod_parroquia = q.cod_parroquia,
                                         nom_parroquia = q.nom_parroquia,
                                         estado = q.estado
                                     }).ToList();
                        else
                            Lista = (from q in Context.tb_parroquia
                                     select new tb_parroquia_Info
                                     {
                                         IdCiudad_Canton = q.IdCiudad_Canton,
                                         IdParroquia = q.IdParroquia,
                                         cod_parroquia = q.cod_parroquia,
                                         nom_parroquia = q.nom_parroquia,
                                         estado = q.estado
                                     }).ToList();
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(IdCiudad))
                            Lista = (from q in Context.tb_parroquia
                                     where q.IdCiudad_Canton == IdCiudad
                                     && q.estado == true
                                     select new tb_parroquia_Info
                                     {
                                         IdCiudad_Canton = q.IdCiudad_Canton,
                                         IdParroquia = q.IdParroquia,
                                         cod_parroquia = q.cod_parroquia,
                                         nom_parroquia = q.nom_parroquia,
                                         estado = q.estado,
                                     }).ToList();
                        else
                            Lista = (from q in Context.tb_parroquia
                                     where q.estado == true
                                     select new tb_parroquia_Info
                                     {
                                         IdCiudad_Canton = q.IdCiudad_Canton,
                                         IdParroquia = q.IdParroquia,
                                         cod_parroquia = q.cod_parroquia,
                                         nom_parroquia = q.nom_parroquia,
                                         estado = q.estado,
                                     }).ToList();
                    }
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
