using Core.Data.Base;
using Core.Info.Academico;
using Core.Info.Helps;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_SocioEconomico_Data
    {
        aca_Familia_Data odata_fam = new aca_Familia_Data();
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
                aca_SocioEconomico_Info info = new aca_SocioEconomico_Info();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("", connection);
                    command.CommandText = "SELECT * FROM aca_SocioEconomico"
                    + " WHERE IdEmpresa = " + IdEmpresa.ToString() + " and IdSocioEconomico = " + IdSocioEconomico.ToString();
                    var ResultValue = command.ExecuteScalar();

                    if (ResultValue == null)
                        return null;

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        info = new aca_SocioEconomico_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdSocioEconomico = Convert.ToInt32(reader["IdSocioEconomico"]),
                            IdAlumno = Convert.ToDecimal(reader["IdAlumno"]),
                            IdCatalogoFichaVi = Convert.ToInt32(reader["IdCatalogoFichaVi"]),
                            IdCatalogoFichaTVi = Convert.ToInt32(reader["IdCatalogoFichaTVi"]),
                            IdCatalogoFichaAg = Convert.ToInt32(reader["IdCatalogoFichaAg"]),
                            TieneElectricidad = Convert.ToBoolean(reader["TieneElectricidad"]),
                            TieneHermanos = Convert.ToBoolean(reader["TieneHermanos"]),
                            CantidadHermanos = string.IsNullOrEmpty(reader["CantidadHermanos"].ToString()) ? (int?)null : Convert.ToInt32(reader["CantidadHermanos"]),
                            SueldoPadre = Convert.ToDouble(reader["SueldoPadre"]),
                            SueldoMadre = Convert.ToDouble(reader["SueldoMadre"]),
                            OtroIngresoMadre = Convert.ToDouble(reader["OtroIngresoMadre"]),
                            OtroIngresoPadre = Convert.ToDouble(reader["OtroIngresoPadre"]),
                            GastoAlimentacion = Convert.ToDouble(reader["GastoAlimentacion"]),
                            GastoEducacion = Convert.ToDouble(reader["GastoEducacion"]),
                            GastoServicioBasico = Convert.ToDouble(reader["GastoServicioBasico"]),
                            GastoSalud = Convert.ToDouble(reader["GastoSalud"]),
                            GastoArriendo = Convert.ToDouble(reader["GastoArriendo"]),
                            GastoPrestamo = Convert.ToDouble(reader["GastoPrestamo"]),
                            OtroGasto = Convert.ToDouble(reader["OtroGasto"]),
                            IdCatalogoFichaMot = Convert.ToInt32(reader["IdCatalogoFichaMot"]),
                            IdCatalogoFichaIns = Convert.ToInt32(reader["IdCatalogoFichaIns"]),
                            IdCatalogoFichaFin = Convert.ToInt32(reader["IdCatalogoFichaFin"]),
                            IdCatalogoFichaVive = Convert.ToInt32(reader["IdCatalogoFichaVive"]),
                            OtroFinanciamiento = string.IsNullOrEmpty(reader["OtroFinanciamiento"].ToString()) ? null : reader["OtroFinanciamiento"].ToString(),
                            OtroInformacionInst = string.IsNullOrEmpty(reader["OtroInformacionInst"].ToString()) ? null : reader["OtroInformacionInst"].ToString(),
                            OtroMotivoIngreso = string.IsNullOrEmpty(reader["OtroMotivoIngreso"].ToString()) ? null : reader["OtroMotivoIngreso"].ToString()
                        };
                    }
                }
                /*
                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.aca_SocioEconomico.Where(q => q.IdEmpresa == IdEmpresa && q.IdSocioEconomico == IdSocioEconomico).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new aca_SocioEconomico_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdSocioEconomico = Entity.IdSocioEconomico,
                        IdAlumno = Entity.IdAlumno,
                        IdCatalogoFichaVi = Entity.IdCatalogoFichaVi,
                        IdCatalogoFichaTVi = Entity.IdCatalogoFichaTVi,
                        IdCatalogoFichaAg = Entity.IdCatalogoFichaAg,
                        TieneElectricidad = Entity.TieneElectricidad,
                        TieneHermanos = Entity.TieneHermanos,
                        CantidadHermanos = Entity.CantidadHermanos,
                        SueldoPadre = Entity.SueldoPadre,
                        SueldoMadre = Entity.SueldoMadre,
                        OtroIngresoMadre = Entity.OtroIngresoMadre,
                        OtroIngresoPadre = Entity.OtroIngresoPadre,
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
                        IdCatalogoFichaVive = Entity.IdCatalogoFichaVive,
                        OtroFinanciamiento = Entity.OtroFinanciamiento,
                        OtroInformacionInst = Entity.OtroInformacionInst,
                        OtroMotivoIngreso = Entity.OtroMotivoIngreso
                    };
                }
                */
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
                aca_SocioEconomico_Info info = new aca_SocioEconomico_Info();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("", connection);
                    command.CommandText = "SELECT * FROM aca_SocioEconomico"
                    + " WHERE IdEmpresa = " + IdEmpresa.ToString() + " and IdAlumno = " + IdAlumno.ToString();
                    var ResultValue = command.ExecuteScalar();

                    if (ResultValue == null)
                        return null;

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        info = new aca_SocioEconomico_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdSocioEconomico = Convert.ToInt32(reader["IdSocioEconomico"]),
                            IdAlumno = Convert.ToDecimal(reader["IdAlumno"]),
                            IdCatalogoFichaVi = Convert.ToInt32(reader["IdCatalogoFichaVi"]),
                            IdCatalogoFichaTVi = Convert.ToInt32(reader["IdCatalogoFichaTVi"]),
                            IdCatalogoFichaAg = Convert.ToInt32(reader["IdCatalogoFichaAg"]),
                            TieneElectricidad = Convert.ToBoolean(reader["TieneElectricidad"]),
                            TieneHermanos = Convert.ToBoolean(reader["TieneHermanos"]),
                            CantidadHermanos = string.IsNullOrEmpty(reader["CantidadHermanos"].ToString()) ? (int?)null : Convert.ToInt32(reader["CantidadHermanos"]),
                            SueldoPadre = Convert.ToDouble(reader["SueldoPadre"]),
                            SueldoMadre = Convert.ToDouble(reader["SueldoMadre"]),
                            OtroIngresoMadre = Convert.ToDouble(reader["OtroIngresoMadre"]),
                            OtroIngresoPadre = Convert.ToDouble(reader["OtroIngresoPadre"]),
                            GastoAlimentacion = Convert.ToDouble(reader["GastoAlimentacion"]),
                            GastoEducacion = Convert.ToDouble(reader["GastoEducacion"]),
                            GastoServicioBasico = Convert.ToDouble(reader["GastoServicioBasico"]),
                            GastoSalud = Convert.ToDouble(reader["GastoSalud"]),
                            GastoArriendo = Convert.ToDouble(reader["GastoArriendo"]),
                            GastoPrestamo = Convert.ToDouble(reader["GastoPrestamo"]),
                            OtroGasto = Convert.ToDouble(reader["OtroGasto"]),
                            IdCatalogoFichaMot = Convert.ToInt32(reader["IdCatalogoFichaMot"]),
                            IdCatalogoFichaIns = Convert.ToInt32(reader["IdCatalogoFichaIns"]),
                            IdCatalogoFichaFin = Convert.ToInt32(reader["IdCatalogoFichaFin"]),
                            IdCatalogoFichaVive = Convert.ToInt32(reader["IdCatalogoFichaVive"]),
                            OtroFinanciamiento = string.IsNullOrEmpty(reader["OtroFinanciamiento"].ToString()) ? null : reader["OtroFinanciamiento"].ToString(),
                            OtroInformacionInst = string.IsNullOrEmpty(reader["OtroInformacionInst"].ToString()) ? null : reader["OtroInformacionInst"].ToString(),
                            OtroMotivoIngreso = string.IsNullOrEmpty(reader["OtroMotivoIngreso"].ToString()) ? null : reader["OtroMotivoIngreso"].ToString()
                        };
                    }
                }
                /*
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
                        IdCatalogoFichaVive = Entity.IdCatalogoFichaVive,
                        TieneElectricidad = Entity.TieneElectricidad,
                        TieneHermanos = Entity.TieneHermanos,
                        CantidadHermanos = Entity.CantidadHermanos,
                        SueldoPadre = Entity.SueldoPadre,
                        SueldoMadre = Entity.SueldoMadre,
                        OtroIngresoMadre = Entity.OtroIngresoMadre,
                        OtroIngresoPadre = Entity.OtroIngresoPadre,
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
                */
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
                        CantidadHermanos = info.CantidadHermanos,
                        SueldoPadre = info.SueldoPadre,
                        SueldoMadre = info.SueldoMadre,
                        OtroIngresoMadre = info.OtroIngresoMadre,
                        OtroIngresoPadre = info.OtroIngresoPadre,
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
                        IdCatalogoFichaVive = info.IdCatalogoFichaVive,
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
            catch (Exception EX)
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
                    Entity.CantidadHermanos = info.CantidadHermanos;
                    Entity.SueldoPadre = info.SueldoPadre;
                    Entity.SueldoMadre = info.SueldoMadre;
                    Entity.OtroIngresoMadre = info.OtroIngresoMadre;
                    Entity.OtroIngresoPadre = info.OtroIngresoPadre;
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
                    Entity.IdCatalogoFichaVive = info.IdCatalogoFichaVive;
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

        public List<aca_Matricula_Info> GetListHermanos(int IdEmpresa, decimal IdAlumno)
        {
            try
            {
                List<aca_Matricula_Info> Lista = new List<aca_Matricula_Info>();

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var info_padre = odata_fam.getListTipo(IdEmpresa, IdAlumno, Convert.ToInt32(cl_enumeradores.eTipoParentezco.PAPA));
                    var info_madre = odata_fam.getListTipo(IdEmpresa, IdAlumno, Convert.ToInt32(cl_enumeradores.eTipoParentezco.PAPA));

                    var IdPersonaPadre = (info_padre == null ? 0 : info_padre.IdPersona);
                    var IdPersonaMadre = (info_madre == null ? 0 : info_madre.IdPersona);

                    Lista = Context.vwaca_Hermanos.Where(q => q.IdEmpresa == IdEmpresa && (q.IdPersona == IdPersonaPadre || q.IdPersona==IdPersonaMadre) && q.IdAlumno!=IdAlumno).OrderByDescending(q=>q.IdCurso).GroupBy(q => new { q.IdEmpresa, q.IdAlumno,q.pe_nombreCompleto, q.IdCurso, q.NomCurso, q.NomParalelo }).Select(q => new aca_Matricula_Info
                    {
                        IdEmpresa = q.Key.IdEmpresa,
                        IdAlumno = q.Key.IdAlumno,
                        pe_nombreCompleto = q.Key.pe_nombreCompleto,
                        IdCurso = q.Key.IdCurso??0,
                        NomCurso = q.Key.NomCurso,
                        NomParalelo = q.Key.NomParalelo
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
