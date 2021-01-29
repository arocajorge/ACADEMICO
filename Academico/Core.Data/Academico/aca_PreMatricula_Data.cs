using Core.Data.Base;
using Core.Info.Academico;
using Core.Info.Helps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_PreMatricula_Data
    {
        public decimal getId(int IdEmpresa)
        {
            try
            {
                decimal ID = 1;

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var cont = Context.aca_PreMatricula.Where(q => q.IdEmpresa == IdEmpresa).Count();
                    if (cont > 0)
                        ID = Context.aca_PreMatricula.Where(q => q.IdEmpresa == IdEmpresa).Max(q => q.IdPreMatricula) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(aca_PreMatricula_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_PreMatricula Entity = new aca_PreMatricula
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdPreMatricula = info.IdPreMatricula = getId(info.IdEmpresa),
                        IdAdmision = info.IdAdmision,
                        Codigo = info.IdPreMatricula.ToString("00000"),
                        IdAlumno = info.IdAlumno,
                        IdAnio = info.IdAnio,
                        IdSede = info.IdSede,
                        IdNivel = info.IdNivel,
                        IdJornada = info.IdJornada,
                        IdCurso = info.IdCurso,
                        IdParalelo = info.IdParalelo,
                        IdPersonaF = info.IdPersonaF,
                        IdPersonaR = info.IdPersonaR,
                        IdPlantilla = info.IdPlantilla,
                        IdMecanismo = info.IdMecanismo,
                        Observacion = info.Observacion,
                        IdCatalogoESTPREMAT = info.IdCatalogoESTPREMAT,
                        IdUsuarioCreacion = info.IdUsuarioCreacion,
                        FechaCreacion = DateTime.Now,
                        Fecha = info.Fecha,
                        IdEmpresa_rol = info.IdEmpresa_rol,
                        IdEmpleado = info.IdEmpleado,
                        EsPatrocinado = info.EsPatrocinado
                    };
                    Context.aca_PreMatricula.Add(Entity);

                    if (info.lst_PreMatriculaRubro.Count > 0)
                    {
                        foreach (var item in info.lst_PreMatriculaRubro)
                        {
                            aca_PreMatricula_Rubro Entity_Det = new aca_PreMatricula_Rubro
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdPreMatricula = info.IdPreMatricula,
                                IdPeriodo = item.IdPeriodo,
                                IdRubro = item.IdRubro,
                                IdMecanismo = item.IdMecanismo,
                                IdProducto = item.IdProducto,
                                Subtotal = item.Subtotal,
                                IdCod_Impuesto_Iva = item.IdCod_Impuesto_Iva,
                                Porcentaje = item.Porcentaje,
                                ValorIVA = item.ValorIVA,
                                Total = item.Total,
                                FechaFacturacion = null,
                                EnMatricula = item.EnMatricula,
                                IdPlantilla = item.IdPlantilla,
                                IdAnio = item.IdAnio,
                                IdSede = item.IdSede,
                                IdNivel = item.IdNivel,
                                IdJornada = item.IdJornada,
                                IdCurso = item.IdCurso,
                                IdParalelo = item.IdParalelo
                            };
                            Context.aca_PreMatricula_Rubro.Add(Entity_Det);
                        }
                    }

                    aca_Admision Entity_Admision = Context.aca_Admision.FirstOrDefault(q => q.IdEmpresa == info.IdAdmision);
                    if (Entity_Admision == null)
                        return false;

                    Entity_Admision.FechaPreMatriculacion = info.FechaModificacion = DateTime.Now;
                    Entity_Admision.IdCatalogoESTADM = Convert.ToInt32(cl_enumeradores.eTipoCatalogoAdmision.PREMATRICULADO);
                    Entity_Admision.IdUsuarioModificacion = info.IdUsuarioCreacion;
                    Entity_Admision.FechaModificacion = info.FechaModificacion = DateTime.Now;
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception EX)
            {

                throw;
            }
        }
    }
}
