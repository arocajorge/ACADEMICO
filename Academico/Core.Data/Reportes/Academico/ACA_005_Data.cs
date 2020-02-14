﻿using Core.Data.Base;
using Core.Info.Reportes.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Reportes.Academico
{
    public class ACA_005_Data
    {
        public List<ACA_005_Info> get_list(int IdEmpresa, decimal IdAlumno)
        {
            try
            {
                List<ACA_005_Info> Lista = new List<ACA_005_Info>(); ;
                using (EntitiesReportes Context = new EntitiesReportes())
                {
                    Lista = (from q in Context.SPACA_005(IdEmpresa, IdAlumno)
                             select new ACA_005_Info
                             {
                                NomPadre=q.NomPadre,
                                 DireccionPadre=q.DireccionPadre,
                                 NomEstadoCivilPadre=q.NomEstadoCivilPadre,
                                 CelularPadre=q.CelularPadre,
                                 ProfesionPadre=q.ProfesionPadre,
                                 NomInstruccionPadre=q.NomInstruccionPadre,
                                 CorreoPadre=q.CorreoPadre,
                                 EmpresaTrabajoPadre=q.EmpresaTrabajoPadre,
                                 DireccionTrabajoPadre=q.DireccionTrabajoPadre,
                                 TelefonoTrabajoPadre=q.TelefonoTrabajoPadre,
                                 CargoTrabajoPadre=q.CargoTrabajoPadre,
                                 AniosServicioPadre=q.AniosServicioPadre,
                                IngresoMensualPadre=q.IngresoMensualPadre,
                                 VehiculoPropioPadre=q.VehiculoPropioPadre,
                                 MarcaPadre=q.MarcaPadre,
                                 ModeloPadre=q.ModeloPadre,
                                 AnioVehiculoPadre=q.AnioVehiculoPadre,
                                NomMadre=q.NomMadre,
                                 DireccionMadre=q.DireccionMadre,
                                 NomEstadoCivilMadre=q.NomEstadoCivilMadre,
                                 CelularMadre=q.CelularMadre,
                                 ProfesionMadre=q.ProfesionMadre,
                                 NomInstruccionMadre=q.NomInstruccionMadre,
                                 CorreoMadre=q.CorreoMadre,
                                 EmpresaTrabajoMadre=q.EmpresaTrabajoMadre,
                                 DireccionTrabajoMadre=q.DireccionTrabajoMadre,
                                 TelefonoTrabajoMadre=q.TelefonoTrabajoMadre,
                                 CargoTrabajoMadre=q.CargoTrabajoMadre,
                                 AniosServicioMadre=q.AniosServicioMadre,
                                IngresoMensualMadre=q.IngresoMensualMadre,
                                 VehiculoPropioMadre=q.VehiculoPropioMadre,
                                 MarcaMadre=q.MarcaMadre,
                                 ModeloMadre=q.ModeloMadre,
                                 AnioVehiculoMadre=q.AnioVehiculoMadre,
                                NomRepresentante=q.NomRepresentante,
                                 DireccionRepresentante=q.DireccionRepresentante,
                                 NomEstadoCivilRepresentante=q.NomEstadoCivilRepresentante,
                                 CelularRepresentante=q.CelularRepresentante,
                                 ProfesionRepresentante=q.ProfesionRepresentante,
                                 NomInstruccionRepresentante=q.NomInstruccionRepresentante,
                                 CorreoRepresentante=q.CorreoRepresentante,
                                 EmpresaTrabajoRepresentante=q.EmpresaTrabajoRepresentante,
                                 DireccionTrabajoRepresentante=q.DireccionTrabajoRepresentante,
                                 TelefonoTrabajoRepresentante=q.TelefonoTrabajoRepresentante,
                                 CargoTrabajoRepresentante=q.CargoTrabajoRepresentante,
                                 AniosServicioRepresentante=q.AniosServicioRepresentante,
                                IngresoMensualRepresentante=q.IngresoMensualRepresentante,
                                 VehiculoPropioRepresentante=q.VehiculoPropioRepresentante,
                                 MarcaRepresentante=q.MarcaRepresentante,
                                 ModeloRepresentante=q.ModeloRepresentante,
                                 AnioVehiculoRepresentante=q.AnioVehiculoRepresentante
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
