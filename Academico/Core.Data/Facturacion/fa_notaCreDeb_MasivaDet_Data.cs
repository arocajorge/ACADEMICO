using Core.Data.Base;
using Core.Data.General;
using Core.Info.Facturacion;
using Core.Info.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Facturacion
{
    public class fa_notaCreDeb_MasivaDet_Data
    {
        public List<fa_notaCreDeb_MasivaDet_Info> get_list(int IdEmpresa, decimal IdNCMasivo)
        {
            try
            {
                List<fa_notaCreDeb_MasivaDet_Info> Lista;

                using (EntitiesFacturacion Context = new EntitiesFacturacion())
                {
                    Lista = (from q in Context.vwfa_notaCreDeb_MasivaDet
                             where q.IdEmpresa == IdEmpresa
                             && q.IdNCMasivo == IdNCMasivo
                             select new fa_notaCreDeb_MasivaDet_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdSucursal = q.IdSucursal,
                                 IdBodega = q.IdBodega,
                                 IdNCMasivo = q.IdNCMasivo,
                                 IdNota = q.IdNota,
                                 Secuencia = q.Secuencia,
                                 Subtotal = q.Subtotal,
                                 IVA = q.IVA,
                                 Total = q.Total,
                                 IdCod_Impuesto_Iva = q.IdCod_Impuesto_Iva,
                                 ObservacionDet = q.ObservacionDet,
                                 pe_cedulaRuc = q.pe_cedulaRuc,
                                 pe_nombreCompleto = q.pe_nombreCompleto,
                                 Codigo = q.Codigo,
                                 IdAlumno = q.IdAlumno,
                                 IdCliente= q.IdCliente
                             }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool modificarDB(fa_notaCreDeb_MasivaDet_Info info)
        {
            try
            {
                using (EntitiesFacturacion db_f = new EntitiesFacturacion())
                {
                    #region Detalle
                    var entity = db_f.fa_notaCreDeb_MasivaDet.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdNCMasivo == info.IdNCMasivo && q.Secuencia == info.Secuencia).FirstOrDefault();
                    if (entity == null) return false;

                    entity.IdSucursal = info.IdSucursal;
                    entity.IdBodega = info.IdBodega;
                    entity.IdNota = info.IdNota;

                    #endregion

                    db_f.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
