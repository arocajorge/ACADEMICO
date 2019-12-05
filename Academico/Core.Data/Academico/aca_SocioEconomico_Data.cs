using Core.Data.Base;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_SocioEconomico_Data
    {
        public int getId(int IdEmpresa)
        {
            try
            {
                int ID = 1;

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var cont = Context.aca_SocioEconomico.Where(q => q.IdEmpresa == IdEmpresa).Count();
                    if (cont > 0)
                        ID = Context.aca_SocioEconomico.Where(q => q.IdEmpresa == IdEmpresa).Max(q => q.IdSocioEconomico) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public aca_SocioEconomico_Info getInfo(int IdEmpresa, int IdSocioEconomico)
        {
            try
            {
                aca_SocioEconomico_Info info;

                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.aca_SocioEconomico.Where(q => q.IdEmpresa == IdEmpresa && q.IdSocioEconomico == IdSocioEconomico).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new aca_SocioEconomico_Info
                    {
                        IdSocioEconomico = Entity.IdSocioEconomico,
                        IdAlumno = Entity.IdAlumno,
                        IdCatalogoFichaVi = Entity.IdCatalogoFichaVi,
                        IdCatalogoFichaTVi = Entity.IdCatalogoFichaTVi,
                        IdCatalogoFichaAg = Entity.IdCatalogoFichaAg,
                        TieneElectricidad = Entity.TieneElectricidad,
                        TieneHermanos = Entity.TieneHermanos,
                        NombreHermanos = Entity.NombreHermanos,
                        SueldoPadre = Entity.SueldoPadre,
                        SueldoMadre = Entity.SueldoMadre,
                        OtroIngreso = Entity.OtroIngreso,
                        GastoAlimentacion = Entity.GastoAlimentacion,
                        GastoEducacion = Entity.GastoEducacion,
                        GastoServicioBasico = Entity.GastoServicioBasico,
                        GastoSalud = Entity.GastoSalud,
                        GastoArriendo = Entity.GastoArriendo,
                        GastoPrestamo = Entity.GastoPrestamo,
                        OtroGasto = Entity.OtroGasto,
                        IdCatalogoFichaMot = Entity.IdCatalogoFichaMot,
                        IdCatalogoFichaIns = Entity.IdCatalogoFichaIns,
                        IdCatalogoFichaFin = Entity.IdCatalogoFichaFin,
                        OtroFinanciamiento = Entity.OtroFinanciamiento,
                        OtroInformacionInst = Entity.OtroInformacionInst,
                        OtroMotivoIngreso = Entity.OtroMotivoIngreso
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public aca_SocioEconomico_Info getInfo_by_Alumno(int IdEmpresa, decimal IdAlumno)
        {
            try
            {
                aca_SocioEconomico_Info info;

                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.aca_SocioEconomico.Where(q => q.IdEmpresa == IdEmpresa && q.IdAlumno == IdAlumno).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new aca_SocioEconomico_Info
                    {
                        IdSocioEconomico = Entity.IdSocioEconomico,
                        IdAlumno = Entity.IdAlumno,
                        IdCatalogoFichaVi = Entity.IdCatalogoFichaVi,
                        IdCatalogoFichaTVi = Entity.IdCatalogoFichaTVi,
                        IdCatalogoFichaAg = Entity.IdCatalogoFichaAg,
                        TieneElectricidad = Entity.TieneElectricidad,
                        TieneHermanos = Entity.TieneHermanos,
                        NombreHermanos = Entity.NombreHermanos,
                        SueldoPadre = Entity.SueldoPadre,
                        SueldoMadre = Entity.SueldoMadre,
                        OtroIngreso = Entity.OtroIngreso,
                        GastoAlimentacion = Entity.GastoAlimentacion,
                        GastoEducacion = Entity.GastoEducacion,
                        GastoServicioBasico = Entity.GastoServicioBasico,
                        GastoSalud = Entity.GastoSalud,
                        GastoArriendo = Entity.GastoArriendo,
                        GastoPrestamo = Entity.GastoPrestamo,
                        OtroGasto = Entity.OtroGasto,
                        IdCatalogoFichaMot = Entity.IdCatalogoFichaMot,
                        IdCatalogoFichaIns = Entity.IdCatalogoFichaIns,
                        IdCatalogoFichaFin = Entity.IdCatalogoFichaFin
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(aca_SocioEconomico_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_SocioEconomico Entity = new aca_SocioEconomico
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdSocioEconomico = info.IdSocioEconomico=getId(info.IdEmpresa),
                        IdAlumno = info.IdAlumno,
                        IdCatalogoFichaVi = info.IdCatalogoFichaVi,
                        IdCatalogoFichaTVi = info.IdCatalogoFichaTVi,
                        IdCatalogoFichaAg = info.IdCatalogoFichaAg,
                        TieneElectricidad = info.TieneElectricidad,
                        TieneHermanos = info.TieneHermanos,
                        NombreHermanos = info.NombreHermanos,
                        SueldoPadre = info.SueldoPadre,
                        SueldoMadre = info.SueldoMadre,
                        OtroIngreso = info.OtroIngreso,
                        GastoAlimentacion = info.GastoAlimentacion,
                        GastoEducacion = info.GastoEducacion,
                        GastoServicioBasico = info.GastoServicioBasico,
                        GastoSalud = info.GastoSalud,
                        GastoArriendo = info.GastoArriendo,
                        GastoPrestamo = info.GastoPrestamo,
                        OtroGasto = info.OtroGasto,
                        IdCatalogoFichaMot = info.IdCatalogoFichaMot,
                        IdCatalogoFichaIns = info.IdCatalogoFichaIns,
                        IdCatalogoFichaFin = info.IdCatalogoFichaFin,
                        OtroFinanciamiento = info.OtroFinanciamiento,
                        OtroInformacionInst = info.OtroInformacionInst,
                        OtroMotivoIngreso = info.OtroMotivoIngreso,
                        Estado =true,
                        IdUsuarioCreacion = info.IdUsuarioCreacion,
                        FechaCreacion = DateTime.Now
                    };
                    Context.aca_SocioEconomico.Add(Entity);

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public bool modificarDB(aca_SocioEconomico_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_SocioEconomico Entity = Context.aca_SocioEconomico.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdSocioEconomico == info.IdSocioEconomico);
                    if (Entity == null)
                        return false;

                    Entity.IdCatalogoFichaVi = info.IdCatalogoFichaVi;
                    Entity.IdCatalogoFichaTVi = info.IdCatalogoFichaTVi;
                    Entity.IdCatalogoFichaAg = info.IdCatalogoFichaAg;
                    Entity.TieneElectricidad = info.TieneElectricidad;
                    Entity.TieneHermanos = info.TieneHermanos;
                    Entity.NombreHermanos = info.NombreHermanos;
                    Entity.SueldoPadre = info.SueldoPadre;
                    Entity.SueldoMadre = info.SueldoMadre;
                    Entity.OtroIngreso = info.OtroIngreso;
                    Entity.GastoAlimentacion = info.GastoAlimentacion;
                    Entity.GastoEducacion = info.GastoEducacion;
                    Entity.GastoServicioBasico = info.GastoServicioBasico;
                    Entity.GastoSalud = info.GastoSalud;
                    Entity.GastoArriendo = info.GastoArriendo;
                    Entity.GastoPrestamo = info.GastoPrestamo;
                    Entity.OtroGasto = info.OtroGasto;
                    Entity.IdCatalogoFichaMot = info.IdCatalogoFichaMot;
                    Entity.IdCatalogoFichaIns = info.IdCatalogoFichaIns;
                    Entity.IdCatalogoFichaFin = info.IdCatalogoFichaFin;
                    Entity.OtroFinanciamiento = info.OtroFinanciamiento;
                    Entity.OtroInformacionInst = info.OtroInformacionInst;
                    Entity.OtroMotivoIngreso = info.OtroMotivoIngreso;
                    Entity.IdUsuarioModificacion = info.IdUsuarioModificacion;
                    Entity.FechaModificacion = info.FechaModificacion = DateTime.Now;

                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
