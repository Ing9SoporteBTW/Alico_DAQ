using EpicorBTWRest.DataAccess.DataContext;
using EpicorBTWRest.DataAccess.Models.TimeReport;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SharedService.Functions;

namespace EpicorBTWRest.Rules.Delegates
{
    public static class DelagadoProcesosMaquina
    {
        public static async Task<bool> InicializarTrabajos(IntermediaContext context,List<TimeReport> initializedTimes, ILogger log) 
        {
            #region Inicialización de atributos

            List<Epicor_UD05> ud05 = new List<Epicor_UD05>();
            List<Epicor_Labor> labor = new List<Epicor_Labor>();
            List<ControlPerdidos> control = new List<ControlPerdidos>();
            List<Epicor_UD05> Cierreud05 = new List<Epicor_UD05>();
            List<Epicor_Labor> Cierrelabor = new List<Epicor_Labor>();

            ProcessData Procesar = new ProcessData();

            #region Consulta en la Base de datos

            //CONSULTAR SI EL TRABAJO TIENE MAS REGISTROS.
            //-- Para la tabla LaborDtl
            var tiemposBaseDeDatos = await context.Epicor_Labors
                    .AsNoTracking()
                        .Where(x => x.jobnum == initializedTimes[0].TimeReport_Trabajo ).ToListAsync();

           
            //-- Para la tabla UD05
            var tiemposPerdidosBaseDeDatos = await context.Epicor_UD05s
                    .AsNoTracking()
                        .Where(x => x.jobnum == initializedTimes[0].TimeReport_Trabajo).ToListAsync();
            
            //-- Para el control de los tiempos perdidos
            var controlPerdidos = await context.ControlPerdidos
                          .AsNoTracking()
                        .Where(x => x.jobnum == initializedTimes[0].TimeReport_Trabajo).ToListAsync();

            //CONSULTA DE TRABAJOS QUE NO ESTAN CERRADOS
            var TrabajosCierreLabor = await context.Epicor_Labors
                       .AsNoTracking()
                           .Where(x => x.Closed == false).ToListAsync();

            var TrabajosCierreUD05 = await context.Epicor_UD05s
               .AsNoTracking()
                   .Where(x => x.Closed == false).ToListAsync();

            #endregion
            DateTime Sistema = DateTime.Today;
            decimal valCierre = Convert.ToDecimal(24.00);
            int i = tiemposBaseDeDatos.Count > 0 ? ((tiemposBaseDeDatos.Count) - 1) : 0,
                j = tiemposPerdidosBaseDeDatos.Count > 0 ? ((tiemposPerdidosBaseDeDatos.Count) - 1) : 0,
                k = controlPerdidos.Count > 0 ? ((controlPerdidos.Count)-1) : 0,
                cont = (i > j && i > k) ? i : (j > i && j > k) ? j : (k > i && k > j) ? k: (k == i || k==j) ? k :0;
            string Qtyfinalanterior = string.Empty;

            #region Inicialización de Listas 
            /*Se inicializan las listas con el ultimo valor registrado en la 
             *base de datos, ocurre solo cuando existe un historico del trabajo*/
            if (i == ((tiemposBaseDeDatos.Count) - 1))
            {
                var ultimo_registro = tiemposBaseDeDatos[i];
                labor.Add(ultimo_registro);
                log.LogInformation($"Inicialización Labor { JsonConvert.SerializeObject(labor, Formatting.Indented) }");

            }

            if (j == ((tiemposPerdidosBaseDeDatos.Count) - 1))
            {
                var ultimo_registroPerdido = tiemposPerdidosBaseDeDatos[j];
                ud05.Add(ultimo_registroPerdido);
                log.LogInformation($"Inicialización UD { JsonConvert.SerializeObject(ud05, Formatting.Indented) }");

            }

      #endregion
      #endregion
      #region Cierre de Día

      // Cierre de tiempos en la tabla Labor
      foreach (var CierreLab in TrabajosCierreLabor)
            {
                int evaluar = DateTime.Compare(CierreLab.ClockInDate, Sistema);

                if (evaluar < 0)
                {
                    CierreLab.Closed = true;
                    CierreLab.OnlyCreated = false;
                    CierreLab.ClockOutTime = valCierre;
                    CierreLab.DspClockOutTime = "24:00";
                    var op = CierreLab.ClockinTime;
                    var horas = valCierre - CierreLab.ClockinTime;
                    CierreLab.laborhrs = horas;
                    CierreLab.OnlyUpdate = false;

                    Cierrelabor.Add(CierreLab);
                }
                
            }

            // Cierre de tiempos en la tabla UD05
            foreach (var CierreUD in TrabajosCierreUD05)
            {
                int evaluar = DateTime.Compare(CierreUD.Final, Sistema);
                if (evaluar < 0)
                {
                    CierreUD.Closed = true;
                    CierreUD.OnlyCreated = false;
                    CierreUD.Final = Sistema;
                    Cierreud05.Add(CierreUD);
                }
            }

            #endregion
            #region Control de Tiempos
            var grouptimes = Procesar.GenerateGroups(initializedTimes,50);

            foreach (var times in grouptimes)
            {


                foreach (var t in times)
                {
                    decimal DHoraInicial = 0, DHoraFinal = 0, TiempoHrs = 0;
                    string HoraI = string.Empty, HoraF = string.Empty;

                    Functions.MetodosPropios.cargaVariablesLocales(t.TimeReport_FechaInicial, out DHoraInicial, out DHoraFinal, out TiempoHrs, out HoraI, out HoraF);

                    #region Coordinación de tiempos para la tabla Labor
                    log.LogInformation($"i => {i}");
                    if (i > 0)
                    {

                        // Control de lo que existe en la base de datos y lo que llega 
                        //tener encuenta que si el registro que llega va para una tabla diferente se debe actualizar el tiempo de la otra tabla 
                        if (i == ((tiemposBaseDeDatos.Count) - 1))
                        {// && x.id != t.TimeReport_Id
                            var timepoAnterior = labor.Where(x => x.jobnum == t.TimeReport_Trabajo).FirstOrDefault();
                            log.LogInformation($"Tiempo Anterior Labor { JsonConvert.SerializeObject(timepoAnterior, Formatting.Indented) }");


                            if ((timepoAnterior != null) && (timepoAnterior.Closed == false))
                            {
                                timepoAnterior.Closed = true;
                                log.LogWarning($" idTrabajo {t.TimeReport_Id}  DHoraFinal{DHoraFinal} ");
                                log.LogWarning($" idTrabajoConsulta {timepoAnterior.id} ClockInTimeConsulta{timepoAnterior.ClockinTime}  ClockOutTimeConsulta{timepoAnterior.ClockOutTime} ");


                                if (DHoraFinal < timepoAnterior.ClockinTime)
                                {
                                    timepoAnterior.ClockinTime = decimal.Round(DHoraFinal, 2);
                                    timepoAnterior.DspClockInTime = HoraF;
                                    var horas = timepoAnterior.ClockOutTime - DHoraFinal;
                                    timepoAnterior.laborhrs = decimal.Round(horas, 2);
                                    timepoAnterior.OnlyUpdate = false;
                                    log.LogInformation($"Por evaluación creadada Forzada DB {JsonConvert.SerializeObject(timepoAnterior, Formatting.Indented)}");

                                }
                                else
                                {
                                    timepoAnterior.ClockOutTime = decimal.Round(DHoraFinal, 2);
                                    timepoAnterior.DspClockOutTime = HoraF;
                                    //var op = timepoAnterior.ClockinTime;
                                    var horas = DHoraFinal - timepoAnterior.ClockinTime;
                                    timepoAnterior.laborhrs = decimal.Round(horas, 2);
                                    timepoAnterior.OnlyUpdate = false;
                                    log.LogInformation($"Por evaluación creadada DB {JsonConvert.SerializeObject(timepoAnterior, Formatting.Indented)}");

                                }


                                i = 0;
                                labor[i] = timepoAnterior;
                                log.LogWarning($"***Labor Actualizar el registro de la base de datos { JsonConvert.SerializeObject(labor[i], Formatting.Indented)}");
                            }

                        }
                        else
                        {
                            if (i > 0)
                            {
                                var timepoAnterior = labor.Where(x => x.Posicion == i - 1).FirstOrDefault();
                                log.LogInformation($"**Tiempo Anterior Labor** { JsonConvert.SerializeObject(timepoAnterior, Formatting.Indented) }");


                                if ((timepoAnterior != null) && (timepoAnterior.Closed == false)
                                    && (!await context.Epicor_Labors.AnyAsync(x => x.id == timepoAnterior.id)))
                                {
                                    timepoAnterior.Closed = true;
                                    if (timepoAnterior.ClockinTime > DHoraFinal)
                                    {
                                        timepoAnterior.ClockinTime = decimal.Round(DHoraFinal, 2);
                                        timepoAnterior.DspClockInTime = HoraF;
                                        var horas = timepoAnterior.ClockOutTime - DHoraFinal;
                                        timepoAnterior.laborhrs = decimal.Round(horas, 2);
                                        timepoAnterior.OnlyUpdate = false;
                                        log.LogInformation($"Por evaluación creadada {JsonConvert.SerializeObject(timepoAnterior, Formatting.Indented)}");

                                    }
                                    else
                                    {
                                        timepoAnterior.ClockOutTime = decimal.Round(DHoraFinal, 2);
                                        timepoAnterior.DspClockOutTime = HoraF;
                                        var horas = DHoraFinal - timepoAnterior.ClockinTime;
                                        timepoAnterior.laborhrs = decimal.Round(horas, 2);
                                        timepoAnterior.OnlyUpdate = false;
                                        log.LogInformation($"DHoraFinal {DHoraFinal}");
                                        log.LogInformation($"Por evaluación original {JsonConvert.SerializeObject(timepoAnterior, Formatting.Indented)}");
                                    }
                                    i = (labor.Count) - 1;
                                    labor[i] = timepoAnterior;
                                    log.LogInformation($"***Labor actualizar el registro del bloque de trabajo {JsonConvert.SerializeObject(labor[i], Formatting.Indented)}");
                                }

                            }

                        }
                    }
                    #endregion
                    #region Coordinación de tiempos para la tabla UD05

                    if (j >= 0)
                    {
                        if (j == ((tiemposPerdidosBaseDeDatos.Count) - 1))
                        {
                            var tiempoTablaUD05 = ud05.Where(x => x.jobnum == t.TimeReport_Trabajo).FirstOrDefault();

                            if (tiempoTablaUD05 != null)
                            {
                                Qtyfinalanterior = tiempoTablaUD05.QtlyFinal;

                                if (Qtyfinalanterior == t.TimeReport_QtyFinal
                                    && tiempoTablaUD05.Closed == false)
                                {
                                    tiempoTablaUD05.Closed = false;
                                    //tiempoTablaUD05.OnlyCreated = false;
                                    tiempoTablaUD05.Final = t.TimeReport_FechaInicial;
                                    tiempoTablaUD05.OnlyUpdate = false;
                                    j = 0;
                                    ud05[j] = tiempoTablaUD05;
                                    log.LogInformation($"ud con el mismo Qtyfinalanterior{ JsonConvert.SerializeObject(tiempoTablaUD05, Formatting.Indented) }");


                                }
                                else
                                {
                                    if (tiempoTablaUD05.Closed == false)
                                    {
                                        tiempoTablaUD05.Closed = true;
                                        // tiempoTablaUD05.OnlyCreated = false;
                                        tiempoTablaUD05.Final = t.TimeReport_FechaInicial;
                                        tiempoTablaUD05.OnlyUpdate = false;
                                        j = 0;
                                        ud05[j] = tiempoTablaUD05;
                                        log.LogInformation($"ud con el diferente Qtyfinalanterior{ JsonConvert.SerializeObject(tiempoTablaUD05, Formatting.Indented) }");

                                    }
                                }

                            }
                        }
                        else
                        {
                            var timepoAnterior = ud05.Where(x => x.Posicion1 == j - 1).FirstOrDefault();

                            if ((timepoAnterior != null) && (timepoAnterior.Closed != true) &&
                                (!await context.Epicor_UD05s.AnyAsync(x => x.id == timepoAnterior.id)))
                            {
                                j = (ud05.Count) - 1;
                                if ((timepoAnterior.QtlyFinal != Qtyfinalanterior) || (t.TimeReport_TiempoPerdido != "T"))
                                {
                                    timepoAnterior.Closed = true;
                                    //timepoAnterior.OnlyCreated = false;
                                    timepoAnterior.Final = t.TimeReport_FechaInicial;
                                    timepoAnterior.OnlyUpdate = false;
                                }
                                else
                                {
                                    timepoAnterior.Closed = false;
                                    //timepoAnterior.OnlyCreated = false;
                                    timepoAnterior.OnlyUpdate = false;
                                    timepoAnterior.Sincronizado = true;
                                }
                                ud05[j] = timepoAnterior;
                                log.LogInformation($"ud en el mismo bloque { JsonConvert.SerializeObject(timepoAnterior, Formatting.Indented) }");

                            }
                        }

                    }

                    #endregion
                    #region Tiempos a reportar 
                    if ((!await context.Epicor_Labors.AnyAsync(x => x.id == t.TimeReport_Id)) &&
                        (!await context.Epicor_UD05s.AnyAsync(x => x.id == t.TimeReport_Id)))
                    {

                        if ((t.TimeReport_TiempoPerdido != "T"))
                        {
                            labor.Add(new Epicor_Labor()
                            {
                                id = t.TimeReport_Id,
                                jobnum = t.TimeReport_Trabajo,
                                EmployeeNum = t.TimeReport_Empleado,
                                laborhrs = TiempoHrs,
                                LaborNote = t.TimeReport_Trabajo,
                                ClockinTime = DHoraInicial,
                                ClockOutTime = DHoraFinal,
                                DspClockInTime = HoraI,
                                DspClockOutTime = HoraF,
                                OprSeq = t.TimeReport_Operacion,
                                ResourceID = t.TimeReport_Recurso,
                                LaborType = t.TimeReport_TipoLabor,
                                ClockInDate = t.TimeReport_FechaInicial.Date,
                                CodigoEjecucion = t.TimeReport_CodigoEjecucion,
                                CodigoParo = t.TimeReport_CodigoParo,
                                Ensamble = t.TimeReport_Ensamble,
                                Maquina = t.TimeReport_Maquina,
                                Labor_QtyFinal = t.TimeReport_QtyFinal,
                                TiempoPerdido = t.TimeReport_TiempoPerdido,
                                GrupoRecurso = t.TimeReport_GrupoRecurso,
                                Sincronizado = false,
                                OnlyCreated = i == 0 ? true : false,
                                OnlyUpdate = false,
                                Closed = false,
                                Posicion = i,
                                Consecutivo = cont++
                            });
                            Qtyfinalanterior = string.Empty;
                            i++;
                            log.LogInformation($"Nuevo registro Labor{ JsonConvert.SerializeObject(labor, Formatting.Indented) }");

                        }//Casos de tiempo perdido
                        else
                        {
                            if ((Qtyfinalanterior != t.TimeReport_QtyFinal)
                                && (!await context.ControlPerdidos.AnyAsync(x => x.id == t.TimeReport_Id)))
                            {

                                ud05.Add(new Epicor_UD05()
                                {
                                    id = t.TimeReport_Id,
                                    jobnum = t.TimeReport_Trabajo,
                                    EmployeeNum = t.TimeReport_Empleado,
                                    Entrada = t.TimeReport_FechaInicial,
                                    Final = t.TimeReport_FechaFinal,
                                    Oper = t.TimeReport_Operacion,
                                    ResourceID = t.TimeReport_Recurso,
                                    Codi_Ejecu = t.TimeReport_CodigoEjecucion,
                                    Codi_Paro = t.TimeReport_CodigoParo,
                                    QtlyFinal = t.TimeReport_QtyFinal,
                                    Ensamble = t.TimeReport_Ensamble,
                                    GrupoRecurso = t.TimeReport_GrupoRecurso,
                                    Maquina = t.TimeReport_Maquina,
                                    TiempoPerdido = t.TimeReport_TiempoPerdido,
                                    TipoLabor = t.TimeReport_TipoLabor,
                                    Sincronizado = false,
                                    OnlyCreated = j == 0 ? true : false,
                                    OnlyUpdate = false,
                                    Closed = false,
                                    Posicion1 = j,
                                    Consecutivo = cont++

                                });
                                Qtyfinalanterior = t.TimeReport_QtyFinal;
                                j++;
                                log.LogInformation($"Nuevo registro ud{ JsonConvert.SerializeObject(ud05, Formatting.Indented) }");


                            }
                            control.Add(new ControlPerdidos()
                            {
                                id = t.TimeReport_Id,
                                jobnum = t.TimeReport_Trabajo,
                                EmployeeNum = t.TimeReport_Empleado,
                                Entrada = t.TimeReport_FechaInicial,
                                Oper = t.TimeReport_Operacion,
                                ResourceID = t.TimeReport_Recurso,
                                Codi_Ejecu = t.TimeReport_CodigoEjecucion,
                                Codi_Paro = t.TimeReport_CodigoParo,
                                QtlyFinal = t.TimeReport_QtyFinal,
                                posicion = j,
                                total = cont++

                            });
                            log.LogInformation($"nuevo control{ JsonConvert.SerializeObject(controlPerdidos, Formatting.Indented) }");


                        }

                    }

                }
                #endregion
                #endregion
            }
            #region Guardar información en Base de Datos
            //GUARDAR EN BASE DE DATOS 
            if (Cierrelabor.Count > 0)
            {
                foreach (var Cierre_lab in Cierrelabor)
                {
                    //Evaluar que no exista el id del trabajo de la maquina, evitando registros repetidos
                    if (!await context.Epicor_Labors.AnyAsync(x => x.id == Cierre_lab.id))
                    {
                        context.Epicor_Labors.Add(Cierre_lab);
                        if (await context.SaveChangesAsync() <= 0)
                        {
                            log.LogInformation("Error al guardar CierreLabor");
                            log.LogInformation($"Contenido a guardar{Cierre_lab}");
                        }
                    } 
                    else
                    {
                        var newCierre_lab = await context.Epicor_Labors.Where(x => x.id == Cierre_lab.id).FirstOrDefaultAsync();

                        context.Epicor_Labors.Update(newCierre_lab);
                        if (await context.SaveChangesAsync() <= 0)
                        {
                            log.LogInformation("Error al guardar CierreLabor");
                            log.LogInformation($"Contenido a guardar{newCierre_lab}");
                        }
                    }
                }
                
                
            }
            if (Cierreud05.Count > 0)
            {
                foreach (var Cierre_ud in Cierreud05)
                {
                    //Evaluar que no exista el id del trabajo de la maquina, evitando registros repetidos
                    if (!await context.Epicor_UD05s.AnyAsync(x => x.id == Cierre_ud.id))
                    {
                        context.Epicor_UD05s.Add(Cierre_ud);
                        if (await context.SaveChangesAsync() <= 0)
                        {
                            log.LogInformation("Error al guardar Cierreud05");
                        }
                    }
                    else
                    {
                        context.Epicor_UD05s.Update(Cierre_ud);
                        if (await context.SaveChangesAsync() <= 0)
                        {
                            log.LogInformation("Error al guardar Cierreud05");
                        }
                    }
                }
                log.LogInformation($"Contenido a guardar ud {Cierreud05}");
                
            }
            if (labor.Count > 0)
            {
                log.LogInformation($"***Labor lo que guardaré en base de datos {JsonConvert.SerializeObject(labor, Formatting.Indented)}");

                foreach (var lab in labor)
                {
                    //Evaluar que no exista el id del trabajo de la maquina, evitando registros repetidos
                    if (!await context.Epicor_Labors.AnyAsync(x => x.id == lab.id))
                    {
                        context.Epicor_Labors.Add(lab);
                        if (await context.SaveChangesAsync() <= 0)
                        {
                            log.LogInformation($"Error al guardar Labor{JsonConvert.SerializeObject(lab, Formatting.Indented)}");
                        }
                    }   
                    else 
                    {
                        var newlab = await context.Epicor_Labors.Where(x => x.id == lab.id).FirstOrDefaultAsync();
                        log.LogInformation($"Valor de newlab{newlab}");
                        context.Epicor_Labors.Update(newlab);
                        if (await context.SaveChangesAsync() <= 0)
                        {
                            log.LogInformation($"Error al guardar Labor{JsonConvert.SerializeObject(newlab, Formatting.Indented)}");
                        }
                    }
                   
                   // log.LogInformation($"labor Final{ JsonConvert.SerializeObject(labor, Formatting.Indented) }");

                }

                
            }
            if (ud05.Count > 0)
            {
                log.LogInformation($"ud final{ JsonConvert.SerializeObject(ud05, Formatting.Indented) }");

                foreach (var ud in ud05)
                {
                    if (!await context.Epicor_UD05s.AnyAsync(x => x.id == ud.id))
                    {
                        context.Epicor_UD05s.Add(ud);
                        if (await context.SaveChangesAsync() <= 0)
                        {
                            log.LogInformation($"Error al guardar en la  ud05 {ud}");
                        }

                    }
                    else
                    {
                        var neud05 = await context.Epicor_UD05s.Where(x => x.id == ud.id).FirstOrDefaultAsync();
                        //if (!await context.Epicor_UD05s.AnyAsync(x => x.Closed == false))
                            context.Epicor_UD05s.Update(neud05);
                        if (await context.SaveChangesAsync() <= 0)
                        {
                            log.LogInformation($"Error al guardar en la ud05 {neud05}");
                        }
                    }
                   
                }
               
            }
            if (control.Count > 0)
            {
                foreach (var conta in control)
                {
                    log.LogInformation("",conta);
                    //Evaluar que no exista el id del trabajo de la maquina, evitando registros repetidos
                    if (!await context.ControlPerdidos.AnyAsync(x => x.id == conta.id))
                        context.ControlPerdidos.Add(conta);
                    log.LogInformation($"control final{ JsonConvert.SerializeObject(control, Formatting.Indented) }");

                }

                if (await context.SaveChangesAsync() <= 0)
                {
                    log.LogInformation($"Error al guardar Conrtol {control} ");
                }
            }

            
            return true;
            # endregion
        }
    }
}
