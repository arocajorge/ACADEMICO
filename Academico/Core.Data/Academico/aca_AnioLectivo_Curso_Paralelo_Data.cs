using Core.Data.Base;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_AnioLectivo_Curso_Paralelo_Data
    {
        public List<aca_AnioLectivo_Curso_Paralelo_Info> get_list(int IdEmpresa, int IdSede, int IdAnio)
        {
            try
            {
                List<aca_AnioLectivo_Curso_Paralelo_Info> Lista;

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    Lista = (from q in Context.vwaca_AnioLectivo_Curso_Paralelo
                             where q.IdEmpresa == IdEmpresa
                             && q.IdSede == IdSede
                             && q.IdAnio == IdAnio
                             group q by new { q.IdEmpresa, q.IdAnio, q.IdSede, q.IdNivel, q.IdJornada, q.IdCurso,
                                 q.NomSede, q.Descripcion,q.NomNivel, q.NomJornada, q.NomCurso } into g
                             select new aca_AnioLectivo_Curso_Paralelo_Info
                             {
                                 IdEmpresa = g.Key.IdEmpresa,
                                 IdSede = g.Key.IdSede,
                                 IdAnio = g.Key.IdAnio,
                                 IdNivel = g.Key.IdNivel,
                                 IdJornada = g.Key.IdJornada,
                                 IdCurso = g.Key.IdCurso,
                                 NomSede = g.Key.NomSede,
                                 Descripcion = g.Key.Descripcion,
                                 NomNivel = g.Key.NomNivel,
                                 NomJornada = g.Key.NomJornada,
                                 NomCurso = g.Key.NomCurso
                             }).ToList();
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<aca_AnioLectivo_Curso_Paralelo_Info> get_list(int IdEmpresa, int IdSede, int IdAnio, decimal IdProfesor)
        {
            try
            {
                List<aca_AnioLectivo_Curso_Paralelo_Info> Lista;

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    Lista = (from q in Context.vwaca_AnioLectivo_Curso_Paralelo
                             where q.IdEmpresa == IdEmpresa
                             && q.IdSede == IdSede
                             && q.IdAnio == IdAnio
                             && q.IdProfesorTutor == IdProfesor
                             group q by new
                             {
                                 q.IdEmpresa,
                                 q.IdAnio,
                                 q.IdSede,
                                 q.IdNivel,
                                 q.IdJornada,
                                 q.IdCurso,
                                 q.NomSede,
                                 q.Descripcion,
                                 q.NomNivel,
                                 q.NomJornada,
                                 q.NomCurso,
                                 q.OrdenParalelo
                             } into g
                             select new aca_AnioLectivo_Curso_Paralelo_Info
                             {
                                 IdEmpresa = g.Key.IdEmpresa,
                                 IdSede = g.Key.IdSede,
                                 IdAnio = g.Key.IdAnio,
                                 IdNivel = g.Key.IdNivel,
                                 IdJornada = g.Key.IdJornada,
                                 IdCurso = g.Key.IdCurso,
                                 NomSede = g.Key.NomSede,
                                 Descripcion = g.Key.Descripcion,
                                 NomNivel = g.Key.NomNivel,
                                 NomJornada = g.Key.NomJornada,
                                 NomCurso = g.Key.NomCurso,
                                 OrdenParalelo = g.Key.OrdenParalelo

                             }).ToList();
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<TreeList_Info> get_list_CorreoMasivo(int IdEmpresa, int IdAnio)
        {
            try
            {
                List<TreeList_Info> Lista = new List<TreeList_Info>();
                var ListaSede = new List<vwaca_AnioLectivo_Curso_Paralelo>(); ;
                var ListaJornada = new List<vwaca_AnioLectivo_Curso_Paralelo>();
                var ListaNivel = new List<vwaca_AnioLectivo_Curso_Paralelo>();
                var ListaCurso = new List<vwaca_AnioLectivo_Curso_Paralelo>();
                var ListaParalelo = new List<vwaca_AnioLectivo_Curso_Paralelo>();

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var lst_sede = new List<TreeList_Info>();
                    var lst_jornada = new List<TreeList_Info>();
                    var lst_nivel = new List<TreeList_Info>();
                    var lst_curso = new List<TreeList_Info>();
                    var lst_paralelo = new List<TreeList_Info>();

                    ListaSede = Context.vwaca_AnioLectivo_Curso_Paralelo.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio).ToList();
                    lst_sede = (from q in ListaSede
                                group q by new
                                {
                                    q.IdEmpresa,
                                    q.IdAnio,
                                    q.IdSede,
                                    q.NomSede
                                } into s
                                select new TreeList_Info
                                {
                                    IdString = s.Key.IdSede.ToString("000"),
                                    IdStringPadre = null,
                                    Descripcion = s.Key.NomSede,
                                    Orden = 0
                                }).OrderBy(q => q.Orden).ToList();

                    Lista.AddRange(lst_sede);

                    foreach (var item_sede in lst_sede)
                    {
                        var IdSede = Convert.ToInt32(item_sede.IdString);
                        ListaJornada = Context.vwaca_AnioLectivo_Curso_Paralelo.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdSede == IdSede).ToList();

                        lst_jornada = (from q in ListaJornada
                                        group q by new
                                        {
                                            q.IdEmpresa,
                                            q.IdAnio,
                                            q.IdSede,
                                            q.IdJornada,
                                            q.NomJornada,
                                            q.OrdenJornada
                                        } into s
                                        select new TreeList_Info
                                        {
                                            IdString = s.Key.IdSede.ToString("000") + s.Key.IdJornada.ToString("000"),
                                            IdStringPadre = s.Key.IdSede.ToString("000"),
                                            Descripcion = s.Key.NomJornada,
                                            Orden = s.Key.OrdenJornada
                                        }).OrderBy(q => q.Orden).ToList();

                        Lista.AddRange(lst_jornada);

                        foreach (var item_jornada in lst_jornada)
                        {
                            //var IdSede = Convert.ToInt32(item_jornada.IdString.Substring(0, 3));
                            var IdJornada = Convert.ToInt32(item_jornada.IdString.Substring(3, 3));
                            ListaNivel = Context.vwaca_AnioLectivo_Curso_Paralelo.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdSede == IdSede && q.IdJornada == IdJornada).ToList();

                            lst_nivel = (from q in ListaNivel
                                            group q by new
                                            {
                                                q.IdEmpresa,
                                                q.IdAnio,
                                                q.IdSede,
                                                q.IdJornada,
                                                q.IdNivel,
                                                q.NomNivel,
                                                q.OrdenNivel
                                            } into s
                                            select new TreeList_Info
                                            {
                                                IdString = s.Key.IdSede.ToString("000") + s.Key.IdJornada.ToString("000") + s.Key.IdNivel.ToString("000"),
                                                IdStringPadre = s.Key.IdSede.ToString("000") + s.Key.IdJornada.ToString("000"),
                                                Descripcion = s.Key.NomNivel,
                                                Orden = s.Key.OrdenNivel
                                            }).OrderBy(q => q.Orden).ToList();

                            Lista.AddRange(lst_nivel);

                            foreach (var item_nivel in lst_nivel)
                            {
                                //var IdSede = Convert.ToInt32(item_nivel.IdString.Substring(0, 3));
                                //var IdJornada = Convert.ToInt32(item_nivel.IdString.Substring(3, 3));
                                var IdNivel = Convert.ToInt32(item_nivel.IdString.Substring(6, 3));
                                ListaCurso = Context.vwaca_AnioLectivo_Curso_Paralelo.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdSede == IdSede && q.IdJornada == IdJornada && q.IdNivel == IdNivel).ToList();

                                lst_curso = (from q in ListaCurso
                                             group q by new
                                             {
                                                 q.IdEmpresa,
                                                 q.IdAnio,
                                                 q.IdSede,
                                                 q.IdJornada,
                                                 q.IdNivel,
                                                 q.IdCurso,
                                                 q.NomCurso,
                                                 q.OrdenCurso
                                             } into s
                                             select new TreeList_Info
                                             {
                                                 IdString = s.Key.IdSede.ToString("000") + s.Key.IdJornada.ToString("000") + s.Key.IdNivel.ToString("000") + s.Key.IdCurso.ToString("000"),
                                                 IdStringPadre = s.Key.IdSede.ToString("000") + s.Key.IdJornada.ToString("000") + s.Key.IdNivel.ToString("000"),
                                                 Descripcion = s.Key.NomCurso,
                                                 Orden = s.Key.OrdenCurso
                                             }).OrderBy(q => q.Orden).ToList();

                                Lista.AddRange(lst_curso);

                                foreach (var item_curso in lst_curso)
                                {
                                    //var IdSede = Convert.ToInt32(item_curso.IdString.Substring(0, 3));
                                    //var IdJornada = Convert.ToInt32(item_curso.IdString.Substring(3, 3));
                                    //var IdNivel = Convert.ToInt32(item_curso.IdString.Substring(6, 3));
                                    var IdCurso = Convert.ToInt32(item_curso.IdString.Substring(9, 3));

                                    ListaCurso = Context.vwaca_AnioLectivo_Curso_Paralelo.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdSede == IdSede && q.IdJornada == IdJornada && q.IdNivel == IdNivel && q.IdCurso == IdCurso).ToList();

                                    lst_paralelo = (from q in ListaCurso
                                                    group q by new
                                                    {
                                                        q.IdEmpresa,
                                                        q.IdAnio,
                                                        q.IdSede,
                                                        q.IdJornada,
                                                        q.IdNivel,
                                                        q.IdCurso,
                                                        q.IdParalelo,
                                                        q.NomParalelo,
                                                        q.OrdenParalelo
                                                    } into s
                                                    select new TreeList_Info
                                                    {
                                                        IdString = s.Key.IdSede.ToString("000") + s.Key.IdJornada.ToString("000") + s.Key.IdNivel.ToString("000") + s.Key.IdCurso.ToString("000") + s.Key.IdParalelo.ToString("000"),
                                                        IdStringPadre = s.Key.IdSede.ToString("000") + s.Key.IdJornada.ToString("000") + s.Key.IdNivel.ToString("000") + s.Key.IdCurso.ToString("000"),
                                                        Descripcion = s.Key.NomParalelo,
                                                        Orden = s.Key.OrdenParalelo
                                                    }).OrderBy(q => q.Orden).ToList();

                                    Lista.AddRange(lst_paralelo);
                                }
                            }
                        }
                    }
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<TreeList_Info> get_list_CorreoMasivoAcademico(int IdEmpresa, int IdAnio)
        {
            try
            {
                List<TreeList_Info> Lista = new List<TreeList_Info>();
                List<aca_Alumno_Info> ListaGeneral = new List<aca_Alumno_Info>();
                var ListaSede = new List<aca_Alumno_Info>(); ;
                var ListaJornada = new List<aca_Alumno_Info>();
                var ListaNivel = new List<aca_Alumno_Info>();
                var ListaCurso = new List<aca_Alumno_Info>();
                var ListaParalelo = new List<aca_Alumno_Info>();
                var ListaAlumno = new List<aca_Alumno_Info>();

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
                    + " where x1.IdEmpresa = " + IdEmpresa.ToString() + " and x1.estado = 'A'"
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
                    + " aca_Plantilla as p on b.IdEmpresa = p.IdEmpresa and b.IdPlantilla = p.IdPlantilla left join"
                    + " aca_PlantillaTipo as pt on pt.IdEmpresa = p.IdEmpresa and pt.IdTipoPlantilla = p.IdTipoPlantilla"
                    + " where a.IdEmpresa = " + IdEmpresa.ToString() + " and d.EnCurso = 1 and not exists("
                    + " select x1.IdEmpresa from aca_AlumnoRetiro as x1"
                    + " where x1.IdEmpresa = b.IdEmpresa and x1.IdMatricula = b.IdMatricula and x1.Estado = 1)";
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        ListaGeneral.Add(new aca_Alumno_Info
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
                            IdAnio = Convert.ToInt32(reader["IdAnio"]),
                            IdNivel = Convert.ToInt32(reader["IdNivel"]),
                            IdJornada = Convert.ToInt32(reader["IdJornada"]),
                            IdCurso = Convert.ToInt32(reader["IdCurso"]),
                            IdParalelo = Convert.ToInt32(reader["IdParalelo"]),
                            NomSede = reader["NomSede"].ToString(),
                            NomNivel = reader["NomNivel"].ToString(),
                            OrdenNivel = Convert.ToInt32(reader["OrdenNivel"]),
                            NomJornada = reader["NomJornada"].ToString(),
                            OrdenJornada = Convert.ToInt32(reader["OrdenJornada"]),
                            NomCurso = reader["NomCurso"].ToString(),
                            OrdenCurso = Convert.ToInt32(reader["OrdenCurso"]),
                            NomParalelo = reader["NomParalelo"].ToString(),
                            OrdenParalelo = Convert.ToInt32(reader["OrdenParalelo"]),
                            Saldo = Convert.ToDouble(reader["Saldo"]),
                            SaldoProntoPago = Convert.ToDouble(reader["SaldoProntoPago"]),
                            TelefonoRepresentante = reader["TelefonoRepresentante"].ToString(),
                            TelefonoEmiteFactura = reader["TelefonoEmiteFactura"].ToString(),
                            NomPlantillaTipo = reader["NomPlantillaTipo"].ToString()
                        });
                    }
                    reader.Close();
                }

                var lst_sede = new List<TreeList_Info>();
                var lst_jornada = new List<TreeList_Info>();
                var lst_nivel = new List<TreeList_Info>();
                var lst_curso = new List<TreeList_Info>();
                var lst_paralelo = new List<TreeList_Info>();
                var lst_alumno = new List<TreeList_Info>();

                ListaSede = ListaGeneral.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio).ToList();

                lst_sede = (from q in ListaSede
                            group q by new
                            {
                                q.IdEmpresa,
                                q.IdAnio,
                                q.IdSede,
                                q.NomSede
                            } into s
                            select new TreeList_Info
                            {
                                IdString = s.Key.IdSede.ToString("000"),
                                IdStringPadre = null,
                                Descripcion = s.Key.NomSede,
                                Orden = 0
                            }).OrderBy(q => q.Orden).ToList();

                Lista.AddRange(lst_sede);

                foreach (var item_sede in lst_sede)
                {
                    var IdSede = Convert.ToInt32(item_sede.IdString);
                    ListaJornada = ListaGeneral.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdSede == IdSede).ToList();

                    lst_jornada = (from q in ListaJornada
                                   group q by new
                                   {
                                       q.IdEmpresa,
                                       q.IdAnio,
                                       q.IdSede,
                                       q.IdJornada,
                                       q.NomJornada,
                                       q.OrdenJornada
                                   } into s
                                   select new TreeList_Info
                                   {
                                       IdString = s.Key.IdSede.ToString("000") + s.Key.IdJornada.ToString("000"),
                                       IdStringPadre = s.Key.IdSede.ToString("000"),
                                       Descripcion = s.Key.NomJornada,
                                       Orden = s.Key.OrdenJornada
                                   }).OrderBy(q => q.Orden).ToList();

                    Lista.AddRange(lst_jornada);

                    foreach (var item_jornada in lst_jornada)
                    {
                        var IdJornada = Convert.ToInt32(item_jornada.IdString.Substring(3, 3));
                        ListaNivel = ListaGeneral.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdSede == IdSede && q.IdJornada == IdJornada).ToList();

                        lst_nivel = (from q in ListaNivel
                                     group q by new
                                     {
                                         q.IdEmpresa,
                                         q.IdAnio,
                                         q.IdSede,
                                         q.IdJornada,
                                         q.IdNivel,
                                         q.NomNivel,
                                         q.OrdenNivel
                                     } into s
                                     select new TreeList_Info
                                     {
                                         IdString = s.Key.IdSede.ToString("000") + s.Key.IdJornada.ToString("000") + s.Key.IdNivel.ToString("000"),
                                         IdStringPadre = s.Key.IdSede.ToString("000") + s.Key.IdJornada.ToString("000"),
                                         Descripcion = s.Key.NomNivel,
                                         Orden = s.Key.OrdenNivel
                                     }).OrderBy(q => q.Orden).ToList();

                        Lista.AddRange(lst_nivel);

                        foreach (var item_nivel in lst_nivel)
                        {
                            var IdNivel = Convert.ToInt32(item_nivel.IdString.Substring(6, 3));
                            ListaCurso = ListaGeneral.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdSede == IdSede && q.IdJornada == IdJornada && q.IdNivel == IdNivel).ToList();

                            lst_curso = (from q in ListaCurso
                                         group q by new
                                         {
                                             q.IdEmpresa,
                                             q.IdAnio,
                                             q.IdSede,
                                             q.IdJornada,
                                             q.IdNivel,
                                             q.IdCurso,
                                             q.NomCurso,
                                             q.OrdenCurso
                                         } into s
                                         select new TreeList_Info
                                         {
                                             IdString = s.Key.IdSede.ToString("000") + s.Key.IdJornada.ToString("000") + s.Key.IdNivel.ToString("000") + Convert.ToInt32(s.Key.IdCurso).ToString("000"),
                                             IdStringPadre = s.Key.IdSede.ToString("000") + s.Key.IdJornada.ToString("000") + s.Key.IdNivel.ToString("000"),
                                             Descripcion = s.Key.NomCurso,
                                             Orden = s.Key.OrdenCurso
                                         }).OrderBy(q => q.Orden).ToList();

                            Lista.AddRange(lst_curso);

                            foreach (var item_curso in lst_curso)
                            {
                                var IdCurso = Convert.ToInt32(item_curso.IdString.Substring(9, 3));

                                ListaCurso = ListaGeneral.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdSede == IdSede && q.IdJornada == IdJornada && q.IdNivel == IdNivel && q.IdCurso == IdCurso).ToList();

                                lst_paralelo = (from q in ListaCurso
                                                group q by new
                                                {
                                                    q.IdEmpresa,
                                                    q.IdAnio,
                                                    q.IdSede,
                                                    q.IdJornada,
                                                    q.IdNivel,
                                                    q.IdCurso,
                                                    q.IdParalelo,
                                                    q.NomParalelo,
                                                    q.OrdenParalelo
                                                } into s
                                                select new TreeList_Info
                                                {
                                                    IdString = s.Key.IdSede.ToString("000") + s.Key.IdJornada.ToString("000") + s.Key.IdNivel.ToString("000") + Convert.ToInt32(s.Key.IdCurso).ToString("000") + s.Key.IdParalelo.ToString("000"),
                                                    IdStringPadre = s.Key.IdSede.ToString("000") + s.Key.IdJornada.ToString("000") + s.Key.IdNivel.ToString("000") + Convert.ToInt32(s.Key.IdCurso).ToString("000"),
                                                    Descripcion = s.Key.NomParalelo,
                                                    Orden = s.Key.OrdenParalelo
                                                }).OrderBy(q => q.Orden).ToList();

                                Lista.AddRange(lst_paralelo);

                                foreach (var item_paralelo in lst_paralelo)
                                {
                                    var IdParalelo = Convert.ToInt32(item_paralelo.IdString.Substring(12, 3));

                                    ListaAlumno = ListaGeneral.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdSede == IdSede && q.IdJornada == IdJornada && q.IdNivel == IdNivel && q.IdCurso == IdCurso && q.IdParalelo == IdParalelo).OrderBy(q => q.pe_nombreCompleto).ToList();

                                    foreach (var item in ListaAlumno)
                                    {
                                        var info = new TreeList_Info
                                        {
                                            IdString = IdSede.ToString("000") + IdJornada.ToString("000") + IdNivel.ToString("000") + IdCurso.ToString("000") + IdParalelo.ToString("000") + item.IdAlumno.ToString("000000"),
                                            IdStringPadre = IdSede.ToString("000") + IdJornada.ToString("000") + IdNivel.ToString("000") + IdCurso.ToString("000") + IdParalelo.ToString("000"),
                                            Descripcion = item.Codigo + " - " + item.pe_nombreCompleto,
                                            Orden = 0,
                                            CorreoEmiteFactura = item.correoRepEconomico,
                                            CorreoRepresentante = item.CorreoRepLegal
                                        };
                                        Lista.Add(info);
                                    }
                                }
                            }
                        }
                    }
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<TreeList_Info> get_list_CorreoMasivoDeudores(int IdEmpresa, int IdAnio, int CantidadIni, int CantidadFin)
        {
            try
            {
                List<TreeList_Info> Lista = new List<TreeList_Info>();
                List<aca_Alumno_Info> ListaGeneralPorCantidadFacturas = new List<aca_Alumno_Info>();
                var ListaSede = new List<aca_Alumno_Info>(); ;
                var ListaJornada = new List<aca_Alumno_Info>();
                var ListaNivel = new List<aca_Alumno_Info>();
                var ListaCurso = new List<aca_Alumno_Info>();
                var ListaParalelo = new List<aca_Alumno_Info>();
                var ListaAlumno = new List<aca_Alumno_Info>();

                int CantidadIniConsulta = CantidadIni;
                int CantidadFinConsulta = CantidadIni == 0 ? 9999999 : CantidadFin;

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
                    + " case when a5.FechaProntoPago <= CAST(GETDATE() AS DATE) THEN dbo.bankersrounding(a1.ValorProntoPago - ISNULL(A3.dc_ValorPago, 0), 2) ELSE dbo.bankersrounding(a1.Total - ISNULL(A3.dc_ValorPago, 0), 2) end as SaldoProntoPago"
                    + " from fa_factura_resumen as a1 inner join"
                    + " fa_factura as a2 on a1.IdEmpresa = a2.IdEmpresa and a1.IdSucursal = a2.IdSucursal and a1.IdBodega = a2.IdBodega and a1.IdCbteVta = a2.IdCbteVta left join"
                    + " ("
                    + " select x1.IdEmpresa, x1.IdSucursal, x1.IdBodega_Cbte, x1.IdCbte_vta_nota, x1.dc_TipoDocumento, sum(x1.dc_ValorPago) dc_ValorPago"
                    + " from cxc_cobro_det as x1"
                    + " where x1.IdEmpresa = " + IdEmpresa.ToString() + " and x1.estado = 'A'"
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
                    + " aca_Plantilla as p on b.IdEmpresa = p.IdEmpresa and b.IdPlantilla = p.IdPlantilla left join"
                    + " aca_PlantillaTipo as pt on pt.IdEmpresa = p.IdEmpresa and pt.IdTipoPlantilla = p.IdTipoPlantilla"
                    + " where a.IdEmpresa = " + IdEmpresa.ToString() + " and d.EnCurso = 1 and not exists("
                    + " select x1.IdEmpresa from aca_AlumnoRetiro as x1"
                    + " where x1.IdEmpresa = b.IdEmpresa and x1.IdMatricula = b.IdMatricula and x1.Estado = 1)"
                    + " and b.IdEmpresa = " + IdEmpresa.ToString() + "and b.IdAnio = " + IdAnio.ToString() + " and CantidadDeudas between " + CantidadIniConsulta.ToString() + " and " + CantidadFinConsulta.ToString();
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        ListaGeneralPorCantidadFacturas.Add(new aca_Alumno_Info
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
                            IdAnio = Convert.ToInt32(reader["IdAnio"]),
                            IdNivel = Convert.ToInt32(reader["IdNivel"]),
                            IdJornada = Convert.ToInt32(reader["IdJornada"]),
                            IdCurso = Convert.ToInt32(reader["IdCurso"]),
                            IdParalelo = Convert.ToInt32(reader["IdParalelo"]),
                            NomSede = reader["NomSede"].ToString(),
                            NomNivel = reader["NomNivel"].ToString(),
                            OrdenNivel = Convert.ToInt32(reader["OrdenNivel"]),
                            NomJornada = reader["NomJornada"].ToString(),
                            OrdenJornada = Convert.ToInt32(reader["OrdenJornada"]),
                            NomCurso = reader["NomCurso"].ToString(),
                            OrdenCurso = Convert.ToInt32(reader["OrdenCurso"]),
                            NomParalelo = reader["NomParalelo"].ToString(),
                            OrdenParalelo = Convert.ToInt32(reader["OrdenParalelo"]),
                            Saldo = Convert.ToDouble(reader["Saldo"]),
                            SaldoProntoPago = Convert.ToDouble(reader["SaldoProntoPago"]),
                            TelefonoRepresentante = reader["TelefonoRepresentante"].ToString(),
                            TelefonoEmiteFactura = reader["TelefonoEmiteFactura"].ToString(),
                            NomPlantillaTipo = reader["NomPlantillaTipo"].ToString()
                        });
                    }
                    reader.Close();
                }

                var lst_sede = new List<TreeList_Info>();
                var lst_jornada = new List<TreeList_Info>();
                var lst_nivel = new List<TreeList_Info>();
                var lst_curso = new List<TreeList_Info>();
                var lst_paralelo = new List<TreeList_Info>();
                var lst_alumno = new List<TreeList_Info>();

                ListaSede = ListaGeneralPorCantidadFacturas.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio).ToList();

                lst_sede = (from q in ListaSede
                            group q by new
                            {
                                q.IdEmpresa,
                                q.IdAnio,
                                q.IdSede,
                                q.NomSede
                            } into s
                            select new TreeList_Info
                            {
                                IdString = s.Key.IdSede.ToString("000"),
                                IdStringPadre = null,
                                Descripcion = s.Key.NomSede,
                                Orden = 0
                            }).OrderBy(q => q.Orden).ToList();

                Lista.AddRange(lst_sede);

                foreach (var item_sede in lst_sede)
                {
                    var IdSede = Convert.ToInt32(item_sede.IdString);
                    ListaJornada = ListaGeneralPorCantidadFacturas.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdSede == IdSede).ToList();

                    lst_jornada = (from q in ListaJornada
                                   group q by new
                                   {
                                       q.IdEmpresa,
                                       q.IdAnio,
                                       q.IdSede,
                                       q.IdJornada,
                                       q.NomJornada,
                                       q.OrdenJornada
                                   } into s
                                   select new TreeList_Info
                                   {
                                       IdString = s.Key.IdSede.ToString("000") + s.Key.IdJornada.ToString("000"),
                                       IdStringPadre = s.Key.IdSede.ToString("000"),
                                       Descripcion = s.Key.NomJornada,
                                       Orden = s.Key.OrdenJornada
                                   }).OrderBy(q => q.Orden).ToList();

                    Lista.AddRange(lst_jornada);

                    foreach (var item_jornada in lst_jornada)
                    {
                        var IdJornada = Convert.ToInt32(item_jornada.IdString.Substring(3, 3));
                        ListaNivel = ListaGeneralPorCantidadFacturas.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdSede == IdSede && q.IdJornada == IdJornada).ToList();

                        lst_nivel = (from q in ListaNivel
                                     group q by new
                                     {
                                         q.IdEmpresa,
                                         q.IdAnio,
                                         q.IdSede,
                                         q.IdJornada,
                                         q.IdNivel,
                                         q.NomNivel,
                                         q.OrdenNivel
                                     } into s
                                     select new TreeList_Info
                                     {
                                         IdString = s.Key.IdSede.ToString("000") + s.Key.IdJornada.ToString("000") + s.Key.IdNivel.ToString("000"),
                                         IdStringPadre = s.Key.IdSede.ToString("000") + s.Key.IdJornada.ToString("000"),
                                         Descripcion = s.Key.NomNivel,
                                         Orden = s.Key.OrdenNivel
                                     }).OrderBy(q => q.Orden).ToList();

                        Lista.AddRange(lst_nivel);

                        foreach (var item_nivel in lst_nivel)
                        {
                            var IdNivel = Convert.ToInt32(item_nivel.IdString.Substring(6, 3));
                            ListaCurso = ListaGeneralPorCantidadFacturas.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdSede == IdSede && q.IdJornada == IdJornada && q.IdNivel == IdNivel).ToList();

                            lst_curso = (from q in ListaCurso
                                         group q by new
                                         {
                                             q.IdEmpresa,
                                             q.IdAnio,
                                             q.IdSede,
                                             q.IdJornada,
                                             q.IdNivel,
                                             q.IdCurso,
                                             q.NomCurso,
                                             q.OrdenCurso
                                         } into s
                                         select new TreeList_Info
                                         {
                                             IdString = s.Key.IdSede.ToString("000") + s.Key.IdJornada.ToString("000") + s.Key.IdNivel.ToString("000") + Convert.ToInt32(s.Key.IdCurso).ToString("000"),
                                             IdStringPadre = s.Key.IdSede.ToString("000") + s.Key.IdJornada.ToString("000") + s.Key.IdNivel.ToString("000"),
                                             Descripcion = s.Key.NomCurso,
                                             Orden = s.Key.OrdenCurso
                                         }).OrderBy(q => q.Orden).ToList();

                            Lista.AddRange(lst_curso);

                            foreach (var item_curso in lst_curso)
                            {
                                var IdCurso = Convert.ToInt32(item_curso.IdString.Substring(9, 3));

                                ListaCurso = ListaGeneralPorCantidadFacturas.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdSede == IdSede && q.IdJornada == IdJornada && q.IdNivel == IdNivel && q.IdCurso == IdCurso).ToList();

                                lst_paralelo = (from q in ListaCurso
                                                group q by new
                                                {
                                                    q.IdEmpresa,
                                                    q.IdAnio,
                                                    q.IdSede,
                                                    q.IdJornada,
                                                    q.IdNivel,
                                                    q.IdCurso,
                                                    q.IdParalelo,
                                                    q.NomParalelo,
                                                    q.OrdenParalelo
                                                } into s
                                                select new TreeList_Info
                                                {
                                                    IdString = s.Key.IdSede.ToString("000") + s.Key.IdJornada.ToString("000") + s.Key.IdNivel.ToString("000") + Convert.ToInt32(s.Key.IdCurso).ToString("000") + s.Key.IdParalelo.ToString("000"),
                                                    IdStringPadre = s.Key.IdSede.ToString("000") + s.Key.IdJornada.ToString("000") + s.Key.IdNivel.ToString("000") + Convert.ToInt32(s.Key.IdCurso).ToString("000"),
                                                    Descripcion = s.Key.NomParalelo,
                                                    Orden = s.Key.OrdenParalelo
                                                }).OrderBy(q => q.Orden).ToList();

                                Lista.AddRange(lst_paralelo);

                                foreach (var item_paralelo in lst_paralelo)
                                {
                                    var IdParalelo = Convert.ToInt32(item_paralelo.IdString.Substring(12, 3));

                                    ListaAlumno = ListaGeneralPorCantidadFacturas.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdSede == IdSede && q.IdJornada == IdJornada && q.IdNivel == IdNivel && q.IdCurso == IdCurso && q.IdParalelo == IdParalelo).OrderBy(q => q.pe_nombreCompleto).ToList();

                                    foreach (var item in ListaAlumno)
                                    {
                                        var info = new TreeList_Info
                                        {
                                            IdString = IdSede.ToString("000") + IdJornada.ToString("000") + IdNivel.ToString("000") + IdCurso.ToString("000") + IdParalelo.ToString("000") + item.IdAlumno.ToString("000000"),
                                            IdStringPadre = IdSede.ToString("000") + IdJornada.ToString("000") + IdNivel.ToString("000") + IdCurso.ToString("000") + IdParalelo.ToString("000"),
                                            Descripcion = item.Codigo + " - " + item.pe_nombreCompleto,
                                            Orden = 0,
                                            CorreoEmiteFactura = item.correoRepEconomico,
                                            CorreoRepresentante = item.CorreoRepLegal
                                        };
                                        Lista.Add(info);
                                    }
                                }
                            }
                        }
                    }
                }
                return Lista;
                #region Anterior
                /*
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var lst_sede = new List<TreeList_Info>();
                    var lst_jornada = new List<TreeList_Info>();
                    var lst_nivel = new List<TreeList_Info>();
                    var lst_curso = new List<TreeList_Info>();
                    var lst_paralelo = new List<TreeList_Info>();
                    var lst_alumno = new List<TreeList_Info>();

                    Context.Database.CommandTimeout = 5000;
                    var ListaGeneralPorCantidadFacturas = Context.vwaca_Alumno_PeriodoActual.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio 
                    && q.CantDeudas>= CantidadIniConsulta && q.CantDeudas<=CantidadFin).ToList();

                    
                    ListaSede = ListaGeneralPorCantidadFacturas.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio).ToList();
                    
                    lst_sede = (from q in ListaSede
                                group q by new
                                {
                                    q.IdEmpresa,
                                    q.IdAnio,
                                    q.IdSede,
                                    q.NomSede
                                } into s
                                select new TreeList_Info
                                {
                                    IdString = s.Key.IdSede.ToString("000"),
                                    IdStringPadre = null,
                                    Descripcion = s.Key.NomSede,
                                    Orden = 0
                                }).OrderBy(q => q.Orden).ToList();

                    Lista.AddRange(lst_sede);

                    foreach (var item_sede in lst_sede)
                    {
                        var IdSede = Convert.ToInt32(item_sede.IdString);
                        ListaJornada = ListaGeneralPorCantidadFacturas.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdSede == IdSede).ToList();

                        lst_jornada = (from q in ListaJornada
                                       group q by new
                                       {
                                           q.IdEmpresa,
                                           q.IdAnio,
                                           q.IdSede,
                                           q.IdJornada,
                                           q.NomJornada,
                                           q.OrdenJornada
                                       } into s
                                       select new TreeList_Info
                                       {
                                           IdString = s.Key.IdSede.ToString("000") + s.Key.IdJornada.ToString("000"),
                                           IdStringPadre = s.Key.IdSede.ToString("000"),
                                           Descripcion = s.Key.NomJornada,
                                           Orden = s.Key.OrdenJornada??0
                                       }).OrderBy(q => q.Orden).ToList();

                        Lista.AddRange(lst_jornada);

                        foreach (var item_jornada in lst_jornada)
                        {
                            //var IdSede = Convert.ToInt32(item_jornada.IdString.Substring(0, 3));
                            var IdJornada = Convert.ToInt32(item_jornada.IdString.Substring(3, 3));
                            ListaNivel = ListaGeneralPorCantidadFacturas.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdSede == IdSede && q.IdJornada == IdJornada).ToList();

                            lst_nivel = (from q in ListaNivel
                                         group q by new
                                         {
                                             q.IdEmpresa,
                                             q.IdAnio,
                                             q.IdSede,
                                             q.IdJornada,
                                             q.IdNivel,
                                             q.NomNivel,
                                             q.OrdenNivel
                                         } into s
                                         select new TreeList_Info
                                         {
                                             IdString = s.Key.IdSede.ToString("000") + s.Key.IdJornada.ToString("000") + s.Key.IdNivel.ToString("000"),
                                             IdStringPadre = s.Key.IdSede.ToString("000") + s.Key.IdJornada.ToString("000"),
                                             Descripcion = s.Key.NomNivel,
                                             Orden = s.Key.OrdenNivel??0
                                         }).OrderBy(q => q.Orden).ToList();

                            Lista.AddRange(lst_nivel);

                            foreach (var item_nivel in lst_nivel)
                            {
                                //var IdSede = Convert.ToInt32(item_nivel.IdString.Substring(0, 3));
                                //var IdJornada = Convert.ToInt32(item_nivel.IdString.Substring(3, 3));
                                var IdNivel = Convert.ToInt32(item_nivel.IdString.Substring(6, 3));
                                ListaCurso = ListaGeneralPorCantidadFacturas.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdSede == IdSede && q.IdJornada == IdJornada && q.IdNivel == IdNivel).ToList();

                                lst_curso = (from q in ListaCurso
                                             group q by new
                                             {
                                                 q.IdEmpresa,
                                                 q.IdAnio,
                                                 q.IdSede,
                                                 q.IdJornada,
                                                 q.IdNivel,
                                                 q.IdCurso,
                                                 q.NomCurso,
                                                 q.OrdenCurso
                                             } into s
                                             select new TreeList_Info
                                             {
                                                 IdString = s.Key.IdSede.ToString("000") + s.Key.IdJornada.ToString("000") + s.Key.IdNivel.ToString("000") + s.Key.IdCurso.ToString("000"),
                                                 IdStringPadre = s.Key.IdSede.ToString("000") + s.Key.IdJornada.ToString("000") + s.Key.IdNivel.ToString("000"),
                                                 Descripcion = s.Key.NomCurso,
                                                 Orden = s.Key.OrdenCurso??0
                                             }).OrderBy(q => q.Orden).ToList();

                                Lista.AddRange(lst_curso);

                                foreach (var item_curso in lst_curso)
                                {
                                    //var IdSede = Convert.ToInt32(item_curso.IdString.Substring(0, 3));
                                    //var IdJornada = Convert.ToInt32(item_curso.IdString.Substring(3, 3));
                                    //var IdNivel = Convert.ToInt32(item_curso.IdString.Substring(6, 3));
                                    var IdCurso = Convert.ToInt32(item_curso.IdString.Substring(9, 3));

                                    ListaCurso = ListaGeneralPorCantidadFacturas.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdSede == IdSede && q.IdJornada == IdJornada && q.IdNivel == IdNivel && q.IdCurso == IdCurso).ToList();

                                    lst_paralelo = (from q in ListaCurso
                                                    group q by new
                                                    {
                                                        q.IdEmpresa,
                                                        q.IdAnio,
                                                        q.IdSede,
                                                        q.IdJornada,
                                                        q.IdNivel,
                                                        q.IdCurso,
                                                        q.IdParalelo,
                                                        q.NomParalelo,
                                                        q.OrdenParalelo
                                                    } into s
                                                    select new TreeList_Info
                                                    {
                                                        IdString = s.Key.IdSede.ToString("000") + s.Key.IdJornada.ToString("000") + s.Key.IdNivel.ToString("000") + s.Key.IdCurso.ToString("000") + s.Key.IdParalelo.ToString("000"),
                                                        IdStringPadre = s.Key.IdSede.ToString("000") + s.Key.IdJornada.ToString("000") + s.Key.IdNivel.ToString("000") + s.Key.IdCurso.ToString("000"),
                                                        Descripcion = s.Key.NomParalelo,
                                                        Orden = s.Key.OrdenParalelo??0
                                                    }).OrderBy(q => q.Orden).ToList();

                                    Lista.AddRange(lst_paralelo);

                                    foreach (var item_paralelo in lst_paralelo)
                                    {
                                        var IdParalelo = Convert.ToInt32(item_paralelo.IdString.Substring(12, 3));

                                        ListaAlumno= ListaGeneralPorCantidadFacturas.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdSede == IdSede && q.IdJornada == IdJornada && q.IdNivel == IdNivel && q.IdCurso == IdCurso && q.IdParalelo==IdParalelo).OrderBy(q=>q.NombreAlumno).ToList();

                                        foreach (var item in ListaAlumno)
                                        {
                                            var info = new TreeList_Info
                                            {
                                                IdString = IdSede.ToString("000") + IdJornada.ToString("000") + IdNivel.ToString("000") + IdCurso.ToString("000") + IdParalelo.ToString("000") + item.IdAlumno.ToString("000000"),
                                                IdStringPadre = IdSede.ToString("000") + IdJornada.ToString("000") + IdNivel.ToString("000") + IdCurso.ToString("000") + IdParalelo.ToString("000"),
                                                Descripcion = item.Codigo + " - " + item.NombreAlumno,
                                                Orden = 0,
                                                CorreoEmiteFactura = item.CorreoEmiteFactura,
                                                CorreoRepresentante = item.CorreoRepresentante
                                            };
                                            Lista.Add(info);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }*/
                #endregion
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        
        public List<aca_AnioLectivo_Curso_Paralelo_Info> get_list(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, int IdJornada, int IdCurso)
        {
            try
            {
                List<aca_AnioLectivo_Curso_Paralelo_Info> Lista;

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    Lista = Context.vwaca_AnioLectivo_Curso_Paralelo.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdSede == IdSede
                    && q.IdNivel == IdNivel && q.IdJornada == IdJornada && q.IdCurso == IdCurso).Select(q => new aca_AnioLectivo_Curso_Paralelo_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdSede = q.IdSede,
                        IdAnio = q.IdAnio,
                        IdNivel = q.IdNivel,
                        IdJornada = q.IdJornada,
                        IdCurso = q.IdCurso,
                        IdParalelo = q.IdParalelo,
                        NomParalelo = q.NomParalelo,
                        OrdenParalelo = q.OrdenParalelo,
                        IdProfesorInspector = q.IdProfesorInspector,
                        IdProfesorTutor = q.IdProfesorTutor,
                        NomTutor = q.NomTutor,
                        NomInspector = q.NomInspector
                    }).ToList();
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<aca_AnioLectivo_Curso_Paralelo_Info> get_list_asignacion(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, int IdJornada, int IdCurso)
        {
            try
            {
                List<aca_AnioLectivo_Curso_Paralelo_Info> Lista;

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    Lista = (from q in Context.aca_AnioLectivo_Curso_Paralelo
                             join c in Context.aca_Paralelo
                             on new { q.IdEmpresa, q.IdParalelo } equals new { c.IdEmpresa, c.IdParalelo }
                             where q.IdEmpresa == IdEmpresa
                             && q.IdSede == IdSede
                             && q.IdAnio == IdAnio
                             && q.IdNivel == IdNivel
                             && q.IdJornada == IdJornada
                             && q.IdCurso == IdCurso
                             && c.Estado == true
                             select new aca_AnioLectivo_Curso_Paralelo_Info
                             {
                                 seleccionado = true,
                                 IdEmpresa = q.IdEmpresa,
                                 IdSede = q.IdSede,
                                 IdAnio = q.IdAnio,
                                 IdNivel = q.IdNivel,
                                 IdJornada = q.IdJornada,
                                 IdCurso = q.IdCurso,
                                 IdParalelo = q.IdParalelo,
                                 NomParalelo = q.NomParalelo,
                                 OrdenParalelo = q.OrdenParalelo
                             }).ToList();

                    Lista.AddRange((from j in Context.aca_Paralelo
                                    where !Context.aca_AnioLectivo_Curso_Paralelo.Any(n => n.IdParalelo == j.IdParalelo && n.IdEmpresa == IdEmpresa && n.IdSede == IdSede && n.IdAnio == IdAnio && n.IdNivel == IdNivel && n.IdJornada == IdJornada && n.IdCurso == IdCurso)
                                    && j.Estado == true
                                    select new aca_AnioLectivo_Curso_Paralelo_Info
                                    {
                                        seleccionado = false,
                                        IdEmpresa = IdEmpresa,
                                        IdSede = IdSede,
                                        IdAnio = IdAnio,
                                        IdNivel = IdNivel,
                                        IdJornada = IdJornada,
                                        IdCurso = IdCurso,
                                        IdParalelo = j.IdParalelo,
                                        NomParalelo = j.NomParalelo,
                                        OrdenParalelo = j.OrdenParalelo
                                    }).ToList());
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, int IdJornada, int IdCurso, List<aca_AnioLectivo_Curso_Paralelo_Info> lista)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var lst_ParaleloPorCurso = Context.aca_AnioLectivo_Curso_Paralelo.Where(q => q.IdEmpresa == IdEmpresa && q.IdSede == IdSede && q.IdAnio == IdAnio && q.IdNivel == IdNivel && q.IdJornada == IdJornada && q.IdCurso == IdCurso).ToList();
                    Context.aca_AnioLectivo_Curso_Paralelo.RemoveRange(lst_ParaleloPorCurso);

                    if (lista.Count > 0)
                    {
                        foreach (var info in lista)
                        {
                            aca_AnioLectivo_Curso_Paralelo Entity = new aca_AnioLectivo_Curso_Paralelo
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdAnio = info.IdAnio,
                                IdSede = info.IdSede,
                                IdNivel = info.IdNivel,
                                IdJornada = info.IdJornada,
                                IdCurso = info.IdCurso,
                                IdParalelo = info.IdParalelo,
                                CodigoParalelo = info.CodigoParalelo,
                                NomParalelo = info.NomParalelo,
                                OrdenParalelo = info.OrdenParalelo
                            };
                            Context.aca_AnioLectivo_Curso_Paralelo.Add(Entity);
                        }
                    }
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<aca_AnioLectivo_Curso_Paralelo_Info> getList_Update(int IdEmpresa, int IdAnio, int IdParalelo)
        {
            try
            {
                List<aca_AnioLectivo_Curso_Paralelo_Info> Lista;

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    Lista = Context.aca_AnioLectivo_Curso_Paralelo.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdParalelo == IdParalelo).Select(q => new aca_AnioLectivo_Curso_Paralelo_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdAnio = q.IdAnio,
                        IdSede = q.IdSede,
                        IdNivel = q.IdNivel,
                        IdJornada = q.IdJornada,
                        IdCurso = q.IdCurso,
                        IdParalelo = q.IdParalelo,
                        CodigoParalelo = q.CodigoParalelo,
                        NomParalelo = q.NomParalelo,
                        OrdenParalelo = q.OrdenParalelo
                    }).ToList();
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(List<aca_AnioLectivo_Curso_Paralelo_Info> lista)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    if (lista.Count > 0)
                    {
                        foreach (var item in lista)
                        {
                            aca_AnioLectivo_Curso_Paralelo Entity = Context.aca_AnioLectivo_Curso_Paralelo.FirstOrDefault(q => q.IdEmpresa == item.IdEmpresa
                            && q.IdSede == item.IdSede && q.IdAnio == item.IdAnio && q.IdNivel == item.IdNivel && q.IdJornada == item.IdJornada && q.IdCurso == item.IdCurso && q.IdParalelo == item.IdParalelo);
                            if (Entity == null)
                                return false;

                            Entity.NomParalelo = item.NomParalelo;
                            Entity.OrdenParalelo = item.OrdenParalelo;
                            Entity.CodigoParalelo = item.CodigoParalelo;
                        }
                        Context.SaveChanges();
                    }
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ModificarDB(aca_AnioLectivo_Curso_Paralelo_Info info)
        {
            try
            {
                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.aca_AnioLectivo_Curso_Paralelo.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdAnio == info.IdAnio && q.IdNivel == info.IdNivel && q.IdJornada == info.IdJornada && q.IdSede == info.IdSede && q.IdCurso == info.IdCurso && q.IdParalelo == info.IdParalelo).FirstOrDefault();
                    if (Entity != null)
                    {
                        Entity.IdProfesorInspector = info.IdProfesorInspector;
                        Entity.IdProfesorTutor = info.IdProfesorTutor;
                    }
                    db.SaveChanges();
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
