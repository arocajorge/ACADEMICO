using Core.Data.Base;
using Core.Info.Reportes.Banco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Reportes.Banco
{
    public class BAN_004_Data
    {
        public List<BAN_004_Info> get_list(int IdEmpresa, int IdArchivo)
        {
            try
            {
                List<BAN_004_Info> Lista = new List<BAN_004_Info>();
                using (EntitiesReportes Context = new EntitiesReportes())
                {
                    //Context.SetCommandTimeOut(5000);
                    Context.Database.CommandTimeout = 5000;
                    var lst = Context.VWBAN_004.Where(q=> q.IdEmpresa ==IdEmpresa && q.IdArchivo == IdArchivo).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new BAN_004_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            Secuencia = q.Secuencia,
                            IdAlumno = q.IdAlumno,
                            pe_cedulaRuc = q.pe_cedulaRuc,
                            pe_nombreCompleto = q.pe_nombreCompleto,
                            Codigo = q.Codigo,
                            ba_descripcion = q.ba_descripcion,
                            ba_Num_Cuenta = q.ba_Num_Cuenta,
                            Estado = q.Estado,
                            Fecha = q.Fecha,
                            IdArchivo = q.IdArchivo,
                            NombreProceso = q.NombreProceso,
                            Observacion = q.Observacion,
                            Valor = q.Valor,
                            ValorProntoPago = q.ValorProntoPago

                        });
                    });
                }
                return Lista;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
