using Core.Data.Base;
using Core.Info.Reportes.Academico;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Reportes.Academico
{
    public class ACA_001_Data
    {
        public List<ACA_001_Info> get_list(int IdEmpresa, decimal IdAlumno, int IdAnio)
        {
            try
            {
                var Ruta = @"..\\Content\imagenes\alumnos\" + IdEmpresa.ToString("000") + IdAlumno.ToString("000000") + ".jpg";
                List<ACA_001_Info> Lista = new List<ACA_001_Info>(); ;
                using (EntitiesReportes Context = new EntitiesReportes())
                {
                    Lista = (from q in Context.SPACA_001(IdEmpresa, IdAlumno)
                             select new ACA_001_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdMatricula = q.IdMatricula,
                                 Fecha = q.Fecha,
                                 CodigoMatricula = q.CodigoMatricula,
                                 CodigoAlumno = q.CodigoAlumno,
                                 IdAlumno = q.IdAlumno,
                                 pe_apellido = q.pe_apellido,
                                 pe_nombre = q.pe_nombre,
                                 NomAnio = q.NomAnio,
                                 NomSede = q.NomSede,
                                 NomNivel = q.NomNivel,
                                 NomJornada = q.NomJornada,
                                 NomCurso = q.NomCurso,
                                 NomParalelo = q.NomParalelo,
                                 pe_sexo = q.pe_sexo,
                                 IdUsuarioCreacion = q.IdUsuarioCreacion,
                                 LugarNacimiento = q.LugarNacimiento,
                                 pe_fechaNacimiento = q.pe_fechaNacimiento,
                                 Direccion = q.Direccion,
                                 AnioVehiculo = q.AnioVehiculo,
                                 Nacionalidad = q.Nacionalidad,
                                 TieneConadis = q.TieneConadis,
                                 CedulaRucFamiliar = q.CedulaRucFamiliar,
                                 TieneElectricidad = q.TieneElectricidad,
                                 NomAgua = q.NomAgua,
                                 AntiguaInstitucion = q.AntiguaInstitucion,
                                 CantidadHermanos = q.CantidadHermanos,
                                 TieneHermanos = q.TieneHermanos,
                                 NivelProcedencia = q.NivelProcedencia,
                                 NomTipoVivienda = q.NomTipoVivienda,
                                 EmpresaTrabajo = q.EmpresaTrabajo,
                                 CodigoParalelo = q.CodigoParalelo,
                                 DireccionFamiliar = q.DireccionFamiliar,
                                 Celular = q.Celular,
                                 CelularFamiliar = q.CelularFamiliar,
                                Conducta = q.Conducta,
                                Correo = q.Correo,
                                CorreoFamiliar = q.CorreoFamiliar,
                                DocumentosCompletos = q.DocumentosCompletos,
                                IngresoMensual = q.IngresoMensual,
                                Marca = q.Marca,
                                NomConadis = q.NomConadis,
                                NomEstadoCivil = q.NomEstadoCivil,
                                NomFamiliar = q.NomFamiliar,
                                Modelo = q.Modelo,
                                NomGrupoEtnico = q.NomGrupoEtnico,
                                NomInstruccion = q.NomInstruccion,
                                NomPlantilla = q.NomPlantilla,
                                NomProfesion =q.NomProfesion,
                                NomViveCon = q.NomViveCon,
                                NomVivienda = q.NomVivienda,
                                Observacion = q.Observacion,
                                OtrosIngresos = q.OtrosIngresos,
                                Promedio = q.Promedio,
                                Titulo = q.Titulo,
                                TotalGastos = q.TotalGastos,
                                VehiculoPropio = q.VehiculoPropio,
                                ImageUrlString = Ruta
                        }).ToList();
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
