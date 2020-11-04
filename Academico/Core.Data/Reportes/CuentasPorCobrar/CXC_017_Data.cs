using Core.Info.Helps;
using Core.Info.Reportes.Contabilidad;
using Core.Info.Reportes.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Reportes.Contabilidad
{
    public class CXC_017_Data
    {
        cl_funciones funciones = new cl_funciones();

        public List<CXC_017_Info> GetList(int IdEmpresa, decimal IdAlumno)
        {
            try
            {
                List<CXC_017_Info> Lista = new List<CXC_017_Info>();

                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();
                    string query = string.Empty;
                    query = "SELECT cd.IdEmpresa, c.IdConvenio, c.Fecha, c.Valor, cd.NumCuota, cd.FechaPago, cd.TotalCuota, c.IdAlumno, al.Codigo, pa.pe_nombreCompleto AS Alumno, "
                    + " c.IdPersonaConvenio, u.Nombre Usuario, p.pe_nombreCompleto AS PersonaConvenio, p.pe_cedulaRuc, "
                    + " p.pe_direccion,p.pe_celular,p.pe_correo, c.IdMatricula, m.IdAnio, a.Descripcion, nj.NomJornada, OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.NomParalelo, cp.OrdenParalelo, u.Nombre, c.FechaCreacion "
                    + " FROM     dbo.cxc_Convenio AS c LEFT OUTER JOIN "
                    + " dbo.cxc_Convenio_Det AS cd ON c.IdEmpresa = cd.IdEmpresa and c.IdConvenio = cd.IdConvenio LEFT OUTER JOIN "
                    + " dbo.aca_Alumno AS al ON al.IdEmpresa = c.IdEmpresa AND al.IdAlumno = c.IdAlumno LEFT OUTER JOIN "
                    + " dbo.tb_persona AS pa ON al.IdPersona = pa.IdPersona LEFT OUTER JOIN "
                    + " dbo.tb_persona AS p ON c.IdPersonaConvenio = p.IdPersona LEFT OUTER JOIN "
                    + " dbo.aca_Matricula AS m ON m.IdEmpresa = c.IdEmpresa and m.IdMatricula = c.IdMatricula LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo AS a ON m.IdEmpresa = a.IdEmpresa AND m.IdAnio = a.IdAnio LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON cp.IdEmpresa = m.IdEmpresa AND cp.IdAnio = m.IdAnio AND cp.IdSede = m.IdSede AND cp.IdNivel = m.IdNivel AND cp.IdJornada = m.IdJornada AND cp.IdCurso = m.IdCurso and cp.IdParalelo = m.IdParalelo LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Jornada_Curso AS jc ON jc.IdEmpresa = m.IdEmpresa AND jc.IdAnio = m.IdAnio AND jc.IdSede = m.IdSede AND jc.IdNivel = m.IdNivel AND jc.IdJornada = m.IdJornada AND jc.IdCurso = m.IdCurso LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj ON m.IdEmpresa = nj.IdEmpresa AND m.IdAnio = nj.IdAnio AND m.IdSede = nj.IdSede AND m.IdNivel = nj.IdNivel AND m.IdJornada = nj.IdJornada LEFT OUTER JOIN "
                    + " dbo.seg_usuario AS u ON u.IdUsuario = c.IdUsuarioCreacion "
                     + " where c.IdEmpresa= " + IdEmpresa + " and c.IdAlumno = "+ IdAlumno + " and c.Estado = 1 ";

                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var entero = Convert.ToInt32(Math.Truncate(Convert.ToDouble(reader["Valor"])));
                        var decimales = Convert.ToInt32(Math.Round((Convert.ToDouble(reader["Valor"]) - entero) * 100, 2));

                        Lista.Add(new CXC_017_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdConvenio = Convert.ToInt32(reader["IdConvenio"]),
                            IdAlumno = Convert.ToInt32(reader["IdAlumno"]),
                            IdMatricula = Convert.ToDecimal(reader["IdMatricula"]),
                            IdPersonaConvenio = Convert.ToDecimal(reader["IdPersonaConvenio"]),
                            FechaPago = Convert.ToDateTime(reader["FechaPago"]),
                            Alumno = Convert.ToString(reader["Alumno"]),
                            PersonaConvenio = Convert.ToString(reader["PersonaConvenio"]),
                            pe_cedulaRuc = Convert.ToString(reader["pe_cedulaRuc"]),
                            pe_celular = Convert.ToString(reader["pe_celular"]),
                            pe_direccion = Convert.ToString(reader["pe_direccion"]),
                            pe_correo = Convert.ToString(reader["pe_correo"]),
                            TotalCuota = Convert.ToDouble(reader["TotalCuota"]),
                            Valor = Convert.ToDouble(reader["Valor"]),
                            ValorString = Convert.ToDouble(reader["Valor"]).ToString("C2"),
                            //ValorTexto = entero.ToString() + " con " + decimales.ToString() + "/100",
                            FechaActual = DateTime.Now.ToString("dd' de 'MMMM' de 'yyyy"),
                            Descripcion = Convert.ToString(reader["Descripcion"]),
                            NomCurso = Convert.ToString(reader["NomCurso"]),
                            NomJornada = Convert.ToString(reader["NomJornada"]),
                            Usuario = Convert.ToString(reader["Usuario"]),
                            FechaCreacion = Convert.ToDateTime(reader["FechaCreacion"]),
                            Anio = Convert.ToString( Convert.ToDateTime(reader["FechaCreacion"]).Year),
                            Acta = Convert.ToString(Convert.ToDateTime(reader["FechaCreacion"]).Year) + "-" + Convert.ToInt32(reader["IdConvenio"]).ToString("0000"),
                            OrdenCurso = (string.IsNullOrEmpty(reader["OrdenCurso"].ToString()) ? 0 : Convert.ToInt32(reader["OrdenCurso"])),
                            OrdenJornada = (string.IsNullOrEmpty(reader["OrdenJornada"].ToString()) ? 0 : Convert.ToInt32(reader["OrdenJornada"])),
                            OrdenParalelo = (string.IsNullOrEmpty(reader["OrdenParalelo"].ToString()) ? 0 : Convert.ToInt32(reader["OrdenParalelo"])),
                            NomParalelo = Convert.ToString(reader["NomParalelo"]),
                            NumCuota = Convert.ToInt32(reader["NumCuota"]),
                        });
                    }
                }

                Lista.ForEach(q=>q.ValorTexto = funciones.NumeroALetras_Aca(q.Valor.ToString()));
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
