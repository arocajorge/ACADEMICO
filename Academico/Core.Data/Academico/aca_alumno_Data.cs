using Core.Data.Base;
using Core.Info.Academico;
using Core.Info.General;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_Alumno_Data
    {
        public List<aca_Alumno_Info> getList(int IdEmpresa, bool MostrarAnulados)
        {
            try
            {
                List<aca_Alumno_Info> Lista = new List<aca_Alumno_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.vwaca_Alumno.Where(q => q.IdEmpresa == IdEmpresa && q.Estado == (MostrarAnulados ? q.Estado : true)).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_Alumno_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdAlumno = q.IdAlumno,
                            IdPersona = q.IdPersona,
                            Codigo = q.Codigo,
                            IdTipoDocumento = q.IdTipoDocumento,
                            pe_Naturaleza = q.pe_Naturaleza,
                            pe_cedulaRuc = q.pe_cedulaRuc,
                            pe_nombre = q.pe_nombre,
                            pe_apellido = q.pe_apellido,
                            pe_nombreCompleto = q.pe_nombreCompleto,
                            pe_sexo = q.pe_sexo,
                            FechaIngreso = q.FechaIngreso,
                            pe_fechaNacimiento = q.pe_fechaNacimiento,
                            CodCatalogoSangre = q.CodCatalogoSangre,
                            CodCatalogoCONADIS = q.CodCatalogoCONADIS,
                            NumeroCarnetConadis = q.NumeroCarnetConadis,
                            PorcentajeDiscapacidad =q.PorcentajeDiscapacidad,
                            NomCatalogoESTALU = q.NomCatalogoESTALU,
                            NomCatalogoESTMAT = q.NomCatalogoESTMAT,
                            Estado = q.Estado
                        });
                    });
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<aca_Alumno_Info> getList_PeriodoActual(int IdEmpresa)
        {
            try
            {
                List<aca_Alumno_Info> Lista = new List<aca_Alumno_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    odata.Database.CommandTimeout = 5000;
                    var lst = odata.vwaca_Alumno_PeriodoActual.Where(q => q.IdEmpresa == IdEmpresa).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_Alumno_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdAlumno = q.IdAlumno,
                            Codigo = q.Codigo,
                            pe_nombreCompleto = q.NombreAlumno,
                            pe_cedulaRuc = q.pe_cedulaRuc,
                            NomRepEconomico = q.NombreEmiteFactura,
                            correoRepEconomico = q.CorreoEmiteFactura,
                            CelularEmiteFactura = q.CelularEmiteFactura,
                            NomRepLegal = q.NombreRepresentante,
                            CorreoRepLegal =q.CorreoRepresentante,
                            CelularRepresentante = q.CelularRepresentante,
                            IdSede= q.IdSede,
                            IdJornada=q.IdJornada,
                            IdNivel=q.IdNivel,
                            IdCurso = q.IdCurso,
                            IdParalelo = q.IdParalelo,
                            NomSede = q.NomSede,
                            NomJornada = q.NomJornada,
                            NomNivel = q.NomNivel,
                            NomCurso = q.NomCurso,
                            NomParalelo = q.NomParalelo,
                            NomPlantillaTipo = q.NomPlantillaTipo,
                            TelefonoRepresentante = q.TelefonoRepresentante,
                            TelefonoEmiteFactura = q.TelefonoEmiteFactura
                        });
                    });
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public aca_Alumno_Info getInfo_PeriodoActual(int IdEmpresa, decimal IdAlumno)
        {
            try
            {
                aca_Alumno_Info info = new aca_Alumno_Info();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var Entity = odata.vwaca_Alumno_PeriodoActual.Where(q => q.IdEmpresa == IdEmpresa && q.IdAlumno == IdAlumno).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new aca_Alumno_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdAlumno = Entity.IdAlumno,
                        IdMatricula = Entity.IdMatricula,
                        Codigo = Entity.Codigo,
                        pe_nombreCompleto = Entity.NombreAlumno,
                        pe_cedulaRuc = Entity.pe_cedulaRuc,
                        NomRepEconomico = Entity.NombreEmiteFactura,
                        correoRepEconomico = Entity.CorreoEmiteFactura,
                        CelularEmiteFactura = Entity.CelularEmiteFactura,
                        NomRepLegal = Entity.NombreRepresentante,
                        CorreoRepLegal = Entity.CorreoRepresentante,
                        CelularRepresentante = Entity.CelularRepresentante,
                        Saldo = Entity.Saldo,
                        SaldoProntoPago = Entity.SaldoProntoPago,
                        IdSede = Entity.IdSede,
                        IdJornada = Entity.IdJornada,
                        IdNivel = Entity.IdNivel,
                        IdCurso = Entity.IdCurso,
                        IdParalelo = Entity.IdParalelo,
                        NomSede = Entity.NomSede,
                        NomJornada = Entity.NomJornada,
                        NomNivel = Entity.NomNivel,
                        NomCurso = Entity.NomCurso,
                        NomParalelo = Entity.NomParalelo,
                        NomPlantillaTipo = Entity.NomPlantillaTipo,
                        TelefonoRepresentante = Entity.TelefonoRepresentante,
                        TelefonoEmiteFactura = Entity.TelefonoEmiteFactura
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<aca_Alumno_Info> getList_PeriodoActual(int IdEmpresa, int IdAnio, int IdSede, int IdJornada, int IdNivel, int IdCurso, int IdParalelo)
        {
            try
            {
                int IdSedeIni = IdSede;
                int IdSedeFin = IdSede == 0 ? 9999999 : IdSede;

                int IdJornadaIni = IdJornada;
                int IdJornadaFin = IdJornada == 0 ? 9999999 : IdJornada;

                int IdNivelIni = IdNivel;
                int IdNivelFin = IdNivel == 0 ? 9999999 : IdNivel;

                int IdCursoIni = IdCurso;
                int IdCursoFin = IdCurso == 0 ? 9999999 : IdCurso;

                int IdParaleloIni = IdParalelo;
                int IdParaleloFin = IdParalelo == 0 ? 9999999 : IdParalelo;

                List<aca_Alumno_Info> Lista = new List<aca_Alumno_Info>();

                #region Anterior
                /*
                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    odata.Database.CommandTimeout = 5000;
                    var lst = odata.vwaca_Alumno_PeriodoActual.Where(q => q.IdEmpresa == IdEmpresa & q.IdAnio == IdAnio && q.IdSede >= IdSedeIni && q.IdSede <= IdSedeFin
                    && q.IdJornada >= IdJornadaIni && q.IdJornada <= IdJornadaFin && q.IdNivel >= IdNivelIni && q.IdNivel <= IdNivelFin
                    && q.IdCurso >= IdCursoIni && q.IdCurso <= IdCursoFin && q.IdParalelo >= IdParaleloIni && q.IdParalelo <= IdParaleloFin).OrderBy(q=>q.NombreAlumno).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_Alumno_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdAlumno = q.IdAlumno,
                            Codigo = q.Codigo,
                            pe_nombreCompleto = q.NombreAlumno,
                            pe_cedulaRuc = q.pe_cedulaRuc,
                            NomRepEconomico = q.NombreEmiteFactura,
                            correoRepEconomico = q.CorreoEmiteFactura,
                            CelularEmiteFactura = q.CelularEmiteFactura,
                            NomRepLegal = q.NombreRepresentante,
                            CorreoRepLegal = q.CorreoRepresentante,
                            CelularRepresentante = q.CelularRepresentante,
                            IdSede = q.IdSede,
                            IdJornada = q.IdJornada,
                            IdNivel = q.IdNivel,
                            IdCurso = q.IdCurso,
                            IdParalelo = q.IdParalelo,
                            NomSede = q.NomSede,
                            NomJornada = q.NomJornada,
                            NomNivel = q.NomNivel,
                            NomCurso = q.NomCurso,
                            NomParalelo = q.NomParalelo,
                            Saldo = q.Saldo,
                            SaldoProntoPago = q.SaldoProntoPago,
                            NomPlantillaTipo = q.NomPlantillaTipo,
                            TelefonoRepresentante = q.TelefonoRepresentante,
                            TelefonoEmiteFactura = q.TelefonoEmiteFactura
                        });
                    });
                }*/
                #endregion

                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = "select a.IdEmpresa, a.Codigo, a.IdAlumno, b.IdMatricula, c.pe_nombreCompleto, c.pe_cedulaRuc, pfr.pe_nombreCompleto as NombreRepresentante, fr.Correo as CorreoRepresentante,"
                    + " pfc.pe_nombreCompleto NombreFactura, fc.Correo as CorreoEmiteFactura, fr.Celular as CelularRepresentante, fc.Celular as CelularEmiteFactura, fr.Telefono as TelefonoRepresentante, fc.Telefono as TelefonoEmiteFactura,"
                    + " b.IdAnio, b.IdSede, b.IdNivel, b.IdJornada, b.IdCurso, b.IdParalelo,  sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.NomParalelo, cp.OrdenParalelo,"
                    + " isnull(e.Saldo, 0) Saldo, isnull(e.SaldoProntoPago, 0)SaldoProntoPago, isnull(e.CantidadDeudas, 0) CantidadDeudas, pt.NomPlantillaTipo"
                    + " from aca_Alumno as a inner join"
                    + " aca_Matricula as b on a.IdEmpresa = b.IdEmpresa and a.IdAlumno = b.IdAlumno left join"
                    + " tb_persona as c on a.IdPersona = c.IdPersona inner join"
                    + " aca_AnioLectivo as d on b.IdEmpresa = d.IdEmpresa and b.IdAnio = d.IdAnio left join"
                    + " ("
                    + " select az.IdEmpresa, az.IdAlumno, dbo.bankersrounding(sum(az.Saldo), 2) Saldo, sum(az.SaldoProntoPago) SaldoProntoPago, sum(CantidadFacturas) as CantidadDeudas from("
                    + " SELECT ay.IdEmpresa, ay.IdAlumno, sum(ay.Saldo) Saldo, sum(ay.SaldoProntoPago) SaldoProntoPago, count(*) as CantidadFacturas"
                    + " FROM("
                    + " select a1.IdEmpresa, a2.IdAlumno, dbo.bankersrounding(a1.Total - ISNULL(A3.dc_ValorPago, 0), 2) as Saldo,"
                    + " case when a5.FechaProntoPago >= CAST(GETDATE() AS DATE) THEN dbo.bankersrounding(a1.ValorProntoPago - ISNULL(A3.dc_ValorPago, 0), 2) ELSE dbo.bankersrounding(a1.Total - ISNULL(A3.dc_ValorPago, 0), 2) end as SaldoProntoPago"
                    + " from fa_factura_resumen as a1 inner join"
                    + " fa_factura as a2 on a1.IdEmpresa = a2.IdEmpresa and a1.IdSucursal = a2.IdSucursal and a1.IdBodega = a2.IdBodega and a1.IdCbteVta = a2.IdCbteVta left join"
                    + " ("
                    + " select x1.IdEmpresa, x1.IdSucursal, x1.IdBodega_Cbte, x1.IdCbte_vta_nota, x1.dc_TipoDocumento, sum(x1.dc_ValorPago) dc_ValorPago"
                    + " from cxc_cobro_det as x1"
                    + " where x1.IdEmpresa = "+IdEmpresa.ToString()+" and x1.estado = 'A'"
                    + " group by x1.IdEmpresa, x1.IdSucursal, x1.IdBodega_Cbte, x1.IdCbte_vta_nota, x1.dc_TipoDocumento"
                    + " ) as a3 on a2.IdEmpresa = a3.IdEmpresa and a2.IdSucursal = a3.IdSucursal and a2.IdBodega = a3.IdBodega_Cbte and a2.IdCbteVta = a3.IdCbte_vta_nota and a2.vt_tipoDoc = a3.dc_TipoDocumento left join"
                    + " aca_Matricula_Rubro as a4 on a2.IdEmpresa = a4.IdEmpresa and a2.IdSucursal = a4.IdSucursal and a2.IdBodega = a4.IdBodega and a2.IdCbteVta = a4.IdCbteVta left join"
                    + " aca_AnioLectivo_Periodo as a5 on a5.IdEmpresa = a4.IdEmpresa and a5.IdAnio = a4.IdAnio and a5.IdPeriodo = a4.IdPeriodo"
                    + " where a2.IdEmpresa = " + IdEmpresa.ToString() + " and a2.Estado = 'A' and dbo.BankersRounding(a1.Total - isnull(a3.dc_ValorPago, 0), 2) > 0"
                    + " ) ay group by ay.IdEmpresa, ay.IdAlumno"
                    + " UNION ALL"
                    + " select a1.IdEmpresa, a2.IdAlumno, dbo.bankersrounding(sum(a1.Total) - ISNULL(SUM(A3.dc_ValorPago), 0), 2) as Saldo, dbo.bankersrounding(sum(a1.Total) - ISNULL(SUM(A3.dc_ValorPago), 0), 2), count(*) CantidadFacturas"
                    + " from fa_notaCreDeb_resumen as a1 inner join"
                    + " fa_notaCreDeb as a2 on a1.IdEmpresa = a2.IdEmpresa and a1.IdSucursal = a2.IdSucursal and a1.IdBodega = a2.IdBodega and a1.IdNota = a2.IdNota left join"
                    + " ("
                    + " select x1.IdEmpresa, x1.IdSucursal, x1.IdBodega_Cbte, x1.IdCbte_vta_nota, x1.dc_TipoDocumento, sum(x1.dc_ValorPago) dc_ValorPago"
                    + " from cxc_cobro_det as x1"
                    + " where x1.IdEmpresa = " + IdEmpresa.ToString() + " and x1.estado = 'A'"
                    + " group by x1.IdEmpresa, x1.IdSucursal, x1.IdBodega_Cbte, x1.IdCbte_vta_nota, x1.dc_TipoDocumento"
                    + " ) as a3 on a2.IdEmpresa = a3.IdEmpresa and a2.IdSucursal = a3.IdSucursal and a2.IdBodega = a3.IdBodega_Cbte and a2.IdNota = a3.IdCbte_vta_nota and a2.CodDocumentoTipo = a3.dc_TipoDocumento"
                    + " where a2.IdEmpresa = " + IdEmpresa.ToString() + " and a2.Estado = 'A' and a2.CreDeb = 'D' and dbo.BankersRounding(a1.Total - isnull(a3.dc_ValorPago, 0), 2) > 0"
                    + " group by a1.IdEmpresa, a2.IdAlumno"
                    + " ) az group by az.IdEmpresa, az.IdAlumno"
                    + " ) as e on a.IdEmpresa = e.IdEmpresa and a.IdAlumno = e.IdAlumno left join"
                    + " aca_AnioLectivo_Curso_Paralelo as cp on b.IdEmpresa = cp.IdEmpresa and b.IdAnio = cp.IdAnio and b.IdSede = cp.IdSede and b.IdNivel = cp.IdNivel and b.IdJornada = cp.IdJornada and b.IdCurso = cp.IdCurso and b.IdParalelo = cp.IdParalelo left join"
                    + " aca_AnioLectivo_Jornada_Curso as jc on b.IdEmpresa = jc.IdEmpresa and b.IdAnio = jc.IdAnio and b.IdSede = jc.IdSede and b.IdNivel = jc.IdNivel and b.IdJornada = jc.IdJornada and b.IdCurso = jc.IdCurso left join"
                    + " aca_AnioLectivo_NivelAcademico_Jornada as nj on nj.IdEmpresa = b.IdEmpresa and nj.IdAnio = b.IdAnio and nj.IdSede = b.IdSede and nj.IdNivel = b.IdNivel and nj.IdJornada = b.IdJornada left join"
                    + " aca_AnioLectivo_Sede_NivelAcademico as sn on sn.IdEmpresa = b.IdEmpresa and sn.IdAnio = b.IdAnio and sn.IdSede = b.IdSede and sn.IdNivel = b.IdNivel left join"
                    + " aca_Familia as fr on fr.IdEmpresa = a.IdEmpresa and fr.IdAlumno = a.IdAlumno and fr.EsRepresentante = 1 left join"
                    + " tb_persona as pfr on pfr.IdPersona = fr.IdPersona left join"
                    + " aca_Familia as fc on fc.IdEmpresa = a.IdEmpresa and fc.IdAlumno = a.IdAlumno and fc.SeFactura = 1 left join"
                    + " tb_persona as pfc on pfc.IdPersona = fc.IdPersona LEFT JOIN"
                    +" aca_Plantilla as p on b.IdEmpresa = p.IdEmpresa and b.IdPlantilla = p.IdPlantilla left join"
                    +" aca_PlantillaTipo as pt on pt.IdEmpresa = p.IdEmpresa and pt.IdTipoPlantilla = p.IdTipoPlantilla"
                    + " where a.IdEmpresa = "+IdEmpresa.ToString()+" and d.EnCurso = 1 and not exists("
                    + " select x1.IdEmpresa from aca_AlumnoRetiro as x1"
                    + " where x1.IdEmpresa = b.IdEmpresa and x1.IdMatricula = b.IdMatricula and x1.Estado = 1)"
                    + " and b.IdSede between " + IdSedeIni.ToString() + " and " + IdSedeFin.ToString() + " and b.IdNivel between " + IdNivelIni.ToString() + " and " + IdNivelFin.ToString() + " and b.IdJornada between " + IdJornadaIni.ToString() + " and " + IdJornadaFin.ToString() + " and b.IdCurso between " + IdCursoIni.ToString() + " and " + IdCursoFin.ToString() + " and b.IdParalelo between " + IdParaleloIni.ToString() + " and " + IdParaleloFin.ToString();
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new aca_Alumno_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            Codigo = reader["Codigo"].ToString(),
                            IdAlumno = Convert.ToDecimal(reader["IdAlumno"]),
                            IdMatricula = Convert.ToDecimal(reader["IdMatricula"]),
                            pe_nombreCompleto = reader["pe_nombreCompleto"].ToString(),
                            pe_cedulaRuc = reader["pe_cedulaRuc"].ToString(),
                            NomRepEconomico = reader["NombreFactura"].ToString(),
                            correoRepEconomico = reader["CorreoEmiteFactura"].ToString(),
                            NomRepLegal = reader["NombreRepresentante"].ToString(),
                            CorreoRepLegal = reader["CorreoRepresentante"].ToString(),
                            CelularRepresentante = reader["CelularRepresentante"].ToString(),
                            CelularEmiteFactura = reader["CelularEmiteFactura"].ToString(),
                            IdSede = Convert.ToInt32(reader["IdSede"]),
                            IdNivel = Convert.ToInt32(reader["IdNivel"]),
                            IdJornada = Convert.ToInt32(reader["IdJornada"]),
                            IdCurso = Convert.ToInt32(reader["IdCurso"]),
                            IdParalelo = Convert.ToInt32(reader["IdParalelo"]),
                            NomSede = reader["NomSede"].ToString(),
                            NomNivel = reader["NomNivel"].ToString(),
                            NomJornada = reader["NomJornada"].ToString(),
                            NomCurso = reader["NomCurso"].ToString(),
                            NomParalelo = reader["NomParalelo"].ToString(),
                            Saldo = Convert.ToDouble(reader["Saldo"]),
                            SaldoProntoPago = Convert.ToDouble(reader["SaldoProntoPago"]),
                            TelefonoRepresentante = reader["TelefonoRepresentante"].ToString(),
                            TelefonoEmiteFactura = reader["TelefonoEmiteFactura"].ToString(),
                            NomPlantillaTipo = reader["NomPlantillaTipo"].ToString()
                        });
                    }
                    reader.Close();
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public aca_Alumno_Info getInfo(int IdEmpresa, decimal IdAlumno)
        {
            try
            {
                aca_Alumno_Info info;

                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.vwaca_Alumno.Where(q => q.IdEmpresa == IdEmpresa && q.IdAlumno == IdAlumno).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new aca_Alumno_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        pe_nombre = Entity.pe_nombre,
                        pe_apellido = Entity.pe_apellido,
                        pe_nombreCompleto = Entity.pe_nombreCompleto,
                        IdAlumno = Entity.IdAlumno,
                        IdPersona = Entity.IdPersona,
                        IdCatalogoESTALU = Entity.IdCatalogoESTALU,
                        IdCatalogoESTMAT = Entity.IdCatalogoESTMAT,
                        pe_telfono_Contacto = Entity.pe_telfono_Contacto,
                        IdTipoDocumento = Entity.IdTipoDocumento,
                        pe_Naturaleza = Entity.pe_Naturaleza,
                        pe_cedulaRuc = Entity.pe_cedulaRuc,
                        Codigo = Entity.Codigo,
                        Estado = Entity.Estado,
                        Correo = Entity.Correo,
                        Direccion = Entity.Direccion,
                        Celular = Entity.Celular,
                        pe_sexo = Entity.pe_sexo,
                        FechaIngreso = Entity.FechaIngreso,
                        pe_fechaNacimiento = Entity.pe_fechaNacimiento,
                        CodCatalogoSangre = Entity.CodCatalogoSangre,
                        CodCatalogoCONADIS = Entity.CodCatalogoCONADIS,
                        NumeroCarnetConadis = Entity.NumeroCarnetConadis,
                        PorcentajeDiscapacidad = Entity.PorcentajeDiscapacidad,
                        IdPais = Entity.IdPais,
                        Cod_Region = Entity.Cod_Region,
                        IdProvincia = Entity.IdProvincia,
                        IdCiudad = Entity.IdCiudad,
                        IdParroquia = Entity.IdParroquia,
                        Sector = Entity.Sector,
                        LugarNacimiento = Entity.LugarNacimiento,
                        IdReligion = Entity.IdReligion,
                        AsisteCentroCristiano = Entity.AsisteCentroCristiano,
                        IdGrupoEtnico = Entity.IdGrupoEtnico
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public aca_Alumno_Info getInfo_Codigo(int IdEmpresa, string Codigo)
        {
            try
            {
                aca_Alumno_Info info;

                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.vwaca_Alumno.Where(q => q.IdEmpresa == IdEmpresa && q.Codigo == Codigo).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new aca_Alumno_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        pe_nombreCompleto = Entity.pe_nombreCompleto,
                        IdAlumno = Entity.IdAlumno,
                        IdPersona = Entity.IdPersona,
                        Codigo = Entity.Codigo
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public aca_Alumno_Info getInfo(int IdEmpresa, decimal IdAlumno, string pe_cedulaRucFamiliar)
        {
            try
            {
                aca_Alumno_Info info;

                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.vwaca_Alumno.Where(q => q.IdEmpresa == IdEmpresa && q.IdAlumno == IdAlumno && q.pe_cedulaRuc== pe_cedulaRucFamiliar).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new aca_Alumno_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        pe_nombre = Entity.pe_nombre,
                        pe_apellido = Entity.pe_apellido,
                        pe_nombreCompleto = Entity.pe_nombreCompleto,
                        IdAlumno = Entity.IdAlumno,
                        IdPersona = Entity.IdPersona,
                        pe_cedulaRuc = Entity.pe_cedulaRuc,
                        Codigo = Entity.Codigo
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public aca_Alumno_Info get_info_x_num_cedula(int IdEmpresa, string pe_cedulaRuc)
        {
            try
            {
                aca_Alumno_Info info = new aca_Alumno_Info();

                EntitiesGeneral Context_general = new EntitiesGeneral();
                tb_persona Entity_per = Context_general.tb_persona.Where(q => q.pe_cedulaRuc == pe_cedulaRuc).FirstOrDefault();
                if (Entity_per == null)
                {
                    Context_general.Dispose();
                    return info;
                }

                EntitiesAcademico Context_academico = new EntitiesAcademico();
                var Entity_aca = Context_academico.vwaca_Alumno.Where(q => q.IdEmpresa == IdEmpresa && q.IdPersona == Entity_per.IdPersona).FirstOrDefault();
                if (Entity_aca == null)
                {
                    info.IdPersona = Entity_per.IdPersona;
                    info.Direccion = Entity_per.pe_direccion;
                    info.Correo = Entity_per.pe_correo;
                    info.Celular = Entity_per.pe_celular;
                    info.pe_sexo = Entity_per.pe_sexo;
                    info.pe_Naturaleza = Entity_per.pe_Naturaleza;
                    info.IdTipoDocumento = Entity_per.IdTipoDocumento;
                    info.pe_apellido = Entity_per.pe_apellido;
                    info.pe_nombre = Entity_per.pe_nombre;
                    info.pe_nombreCompleto = Entity_per.pe_nombreCompleto;
                    info.pe_telfono_Contacto = Entity_per.pe_telfono_Contacto;
                    info.CodCatalogoSangre = Entity_per.CodCatalogoSangre;
                    info.CodCatalogoCONADIS = Entity_per.CodCatalogoCONADIS;
                    info.NumeroCarnetConadis = Entity_per.NumeroCarnetConadis;
                    info.PorcentajeDiscapacidad = Entity_per.PorcentajeDiscapacidad;
                    info.pe_fechaNacimiento = Entity_per.pe_fechaNacimiento;
                    info.IdReligion = Entity_per.IdReligion;
                    info.AsisteCentroCristiano = Entity_per.AsisteCentroCristiano;
                    info.IdGrupoEtnico = Entity_per.IdGrupoEtnico;

                    Context_general.Dispose();
                    Context_academico.Dispose();
                    return info;
                }

                info = new aca_Alumno_Info
                {
                    IdEmpresa = Entity_aca.IdEmpresa,
                    Codigo = Entity_aca.Codigo,
                    IdAlumno = Entity_aca.IdAlumno,
                    Direccion = Entity_aca.Direccion,
                    Correo = Entity_aca.Correo,
                    Celular = Entity_aca.Celular,
                    IdPersona = Entity_aca.IdPersona,
                    pe_apellido = Entity_aca.pe_apellido,
                    pe_nombre = Entity_aca.pe_nombre,
                    pe_Naturaleza = Entity_aca.pe_Naturaleza,
                    IdTipoDocumento = Entity_aca.IdTipoDocumento,
                    pe_cedulaRuc = Entity_aca.pe_cedulaRuc,
                    pe_nombreCompleto = Entity_aca.pe_nombreCompleto,
                    pe_telfono_Contacto = Entity_aca.pe_telfono_Contacto,
                    CodCatalogoSangre = Entity_aca.CodCatalogoSangre,
                    CodCatalogoCONADIS = Entity_aca.CodCatalogoCONADIS,
                    NumeroCarnetConadis = Entity_aca.NumeroCarnetConadis,
                    PorcentajeDiscapacidad = Entity_aca.PorcentajeDiscapacidad
            };

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public decimal getId(int IdEmpresa)
        {
            try
            {
                decimal ID = 1;

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var cont = Context.aca_Alumno.Where(q => q.IdEmpresa == IdEmpresa).Count();
                    if (cont > 0)
                        ID = Context.aca_Alumno.Where(q => q.IdEmpresa == IdEmpresa).Max(q => q.IdAlumno) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(aca_Alumno_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var anio = info.FechaIngreso.Year;
                    var lista = getList(info.IdEmpresa, true);
                    var ListaAnio = lista.Where(q => q.FechaIngreso.Year == anio).ToList();
                    var NumEstudiante = 1;
                    var Codigo = "";

                    if (ListaAnio.Count == 0)
                    {
                        Codigo = anio + NumEstudiante.ToString("0000");
                    }
                    else
                    {
                        NumEstudiante = Convert.ToInt32(ListaAnio.Max(q => q.Codigo)) + 1;
                        Codigo = NumEstudiante.ToString("0000");
                    }

                    if (info.Codigo=="" || info.Codigo==null)
                    {
                        info.Codigo = Codigo;
                    }

                    aca_Alumno Entity = new aca_Alumno
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdAlumno = info.IdAlumno = getId(info.IdEmpresa),
                        IdPersona = info.IdPersona,
                        Codigo = info.Codigo,
                        Estado = true,
                        Correo = info.Correo,
                        Direccion = info.Direccion,
                        Celular = info.Celular,
                        FechaIngreso = info.FechaIngreso,
                        IdCatalogoESTALU = info.IdCatalogoESTALU,
                        IdCatalogoESTMAT = info.IdCatalogoESTMAT,
                        IdPais = info.IdPais,
                        Cod_Region = info.Cod_Region,
                        IdProvincia = info.IdProvincia,
                        IdCiudad = info.IdCiudad,
                        IdParroquia = info.IdParroquia,
                        Sector = info.Sector,
                        LugarNacimiento = info.LugarNacimiento,
                        IdUsuarioCreacion = info.IdUsuarioCreacion,
                        FechaCreacion = info.FechaCreacion = DateTime.Now
                    };
                    Context.aca_Alumno.Add(Entity);

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(aca_Alumno_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_Alumno Entity = Context.aca_Alumno.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdAlumno == info.IdAlumno);
                    if (Entity == null)
                        return false;
                    Entity.IdUsuarioModificacion = info.IdUsuarioModificacion;
                    Entity.FechaModificacion = info.FechaModificacion = DateTime.Now;
                    Entity.Correo = info.Correo;
                    Entity.Direccion = info.Direccion;
                    Entity.Celular = info.Celular;
                    Entity.IdPais = info.IdPais;
                    Entity.Cod_Region = info.Cod_Region;
                    Entity.IdProvincia = info.IdProvincia;
                    Entity.IdCiudad = info.IdCiudad;
                    Entity.IdParroquia = info.IdParroquia;
                    Entity.Sector = info.Sector;
                    Entity.LugarNacimiento = info.LugarNacimiento;

                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool anularDB(aca_Alumno_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_Alumno Entity = Context.aca_Alumno.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdAlumno == info.IdAlumno);
                    if (Entity == null)
                        return false;
                    Entity.Estado = info.Estado = false;
                    Entity.MotivoAnulacion = info.MotivoAnulacion;
                    Entity.IdUsuarioAnulacion = info.IdUsuarioAnulacion;
                    Entity.FechaAnulacion = info.FechaAnulacion = DateTime.Now;
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool paseAnioDB(aca_Alumno_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_Alumno Entity = Context.aca_Alumno.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdAlumno == info.IdAlumno);
                    if (Entity == null)
                        return false;

                    Entity.IdCatalogoESTMAT = info.IdCatalogoESTMAT;
                    Entity.IdUsuarioModificacion = info.IdUsuarioModificacion;
                    Entity.FechaAnulacion = info.FechaAnulacion = DateTime.Now;
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
