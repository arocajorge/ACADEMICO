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
    public class aca_MatriculaCondicional_Det_Data
    {
        public List<aca_MatriculaCondicional_Det_Info> getList(int IdEmpresa, decimal IdMatriculaCondicional)
        {
            try
            {
                List<aca_MatriculaCondicional_Det_Info> Lista = new List<aca_MatriculaCondicional_Det_Info>();

                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = "SELECT det.IdEmpresa, det.IdMatriculaCondicional, det.Secuencia, parr.IdCatalogoCONDIC, c.NomCatalogo, det.IdParrafo, parr.Nombre"
                    + " FROM dbo.aca_MatriculaCondicional_Det AS det WITH (nolock)INNER JOIN "
                    + " dbo.aca_MatriculaCondicionalParrafo AS parr WITH(nolock) ON det.IdEmpresa = parr.IdEmpresa AND det.IdParrafo = parr.IdParrafo INNER JOIN "
                    + " dbo.aca_Catalogo AS c WITH(nolock) ON parr.IdCatalogoCONDIC = c.IdCatalogo"
                    + " WHERE det.IdEmpresa = " + IdEmpresa.ToString() + " and det.IdMatriculaCondicional = " + IdMatriculaCondicional.ToString();
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new aca_MatriculaCondicional_Det_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdMatriculaCondicional = Convert.ToDecimal(reader["IdMatriculaCondicional"]),
                            IdParrafo = Convert.ToInt32(reader["IdParrafo"]),
                            Secuencia = Convert.ToInt32(reader["Secuencia"]),
                            Nombre = string.IsNullOrEmpty(reader["Nombre"].ToString()) ? null : reader["Nombre"].ToString(),
                            NomCatalogo = string.IsNullOrEmpty(reader["NomCatalogo"].ToString()) ? null : reader["NomCatalogo"].ToString(),
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

        public List<aca_MatriculaCondicional_Det_Info> getList_x_Tipo(int IdEmpresa, int IdCatalogoCONDIC)
        {
            try
            {
                List<aca_MatriculaCondicional_Det_Info> Lista = new List<aca_MatriculaCondicional_Det_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.aca_MatriculaCondicionalParrafo.Where(q => q.IdEmpresa == IdEmpresa && q.IdCatalogoCONDIC == IdCatalogoCONDIC).ToList();
                    var Secuencia = 1;
                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_MatriculaCondicional_Det_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            Secuencia = Secuencia++,
                            IdParrafo = q.IdParrafo,
                            Nombre = q.Nombre
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
    }
}
