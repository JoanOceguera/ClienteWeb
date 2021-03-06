using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReporteDBModel
{
    /// <summary>
    /// Control de los reportes ke no se han completado
    /// </summary>
    public class ReporteControl : Controlador
    {
        public ReporteControl() : base() { }

        /// <summary>
        /// Crea y retorna un Reporte
        /// </summary>
        public Reporte Crear(String numero, Equipo equipo, DateTime fechaHora, String nombreCliente, String departamento, 
                              String estado, String observacion, ProblemaPosible problemaPosible, Administrador administradorDefectando )
        {
            Reporte reporte = new Reporte() { numero = numero, Equipo = equipo, fecha_hora = fechaHora, nombreCliente = nombreCliente, Administrador = administradorDefectando,
                                              departamento = departamento, estado = estado, observacion = observacion, ProblemaPosible = problemaPosible };
            return reporte;
        }
        /// <summary>
        /// Crea y retorna un Reporte sin haberle seteado el Administrador
        /// </summary>
        public Reporte Crear(String numero, Equipo equipo, DateTime fechaHora, String nombreCliente, String departamento,
                              String estado, String observacion, ProblemaPosible problemaPosible)
        {
            Reporte reporte = new Reporte()
            {
                numero = numero,
                Equipo = equipo,
                fecha_hora = fechaHora,
                nombreCliente = nombreCliente,
                departamento = departamento,
                estado = estado,
                observacion = observacion,
                ProblemaPosible = problemaPosible
            };
            return reporte;
        }
        /// <summary>
        /// Crea y retorna un Reporte sin haberle seteado el campo 'observacion'
        /// </summary>
        public Reporte Crear(String numero, Equipo equipo, DateTime fechaHora, String nombreCliente, String departamento,
                              String estado, ProblemaPosible problemaPosible, Administrador administradorDefectando)
        {
            Reporte reporte = new Reporte()
            {
                numero = numero,
                Equipo = equipo,
                fecha_hora = fechaHora,
                nombreCliente = nombreCliente,
                departamento = departamento,
                estado = estado,
                Administrador = administradorDefectando,
                ProblemaPosible = problemaPosible
            };
            return reporte;
        }
        /// <summary>
        /// Crea y retorna un Reporte sin haberle seteado los campos 'observacion', 'Administrador' (se refiere al administradorDefectando)
        /// </summary>
        public Reporte Crear(String numero, Equipo equipo, DateTime fechaHora, String nombreCliente, String departamento,
                              String estado, ProblemaPosible problemaPosible)
        {
            Reporte reporte = new Reporte()
            {
                numero = numero,
                Equipo = equipo,
                fecha_hora = fechaHora,
                nombreCliente = nombreCliente,
                departamento = departamento,
                estado = estado,
                ProblemaPosible = problemaPosible
            };
            return reporte;
        }
        /// <summary>
        /// Crea y retorna un Reporte. Utilizado para el reporte ke crea el usuario, se setean algunos valores por defecto
        /// </summary>
        public Reporte Crear(Entorno entorno, Equipo equipo, String nombreCliente, String departamento, ProblemaPosible problemaPosible, String nombrePc, String observacion, String numero)
        {            
            Reporte reporte = new Reporte()
            {                
                //idEntorno = entorno.idEntorno,
                Entorno = entorno,
                //idEquipo = equipo.idEquipo,
                Equipo = equipo,
                nombreCliente = nombreCliente,
                departamento = departamento,
                ProblemaPosible = problemaPosible,
                //idProblemaPosible = problemaPosible.idProblemaPosible,
                nombrePC = nombrePc,
                observacion = observacion,
                numero = numero,
                estado = Estados.PendienteADefectar,
                fecha_hora = DateTime.Now
            };
            return reporte;
        }

        /// <summary>
        /// Retorna una lista ordenada por fecha descendientemente (las mas recientes en las ultimas posiciones en la lista) 
        /// de reportes ke no se han completado, obtenidos de la BD. 
        /// Propiedad de solo lectura
        /// </summary>
        public List<Reporte> Reportes
        {
            get
            {
                List<Reporte> reportes = (from report in Cnx.Reporte
                                          orderby report.fecha_hora ascending
                                          select report).ToList();
                return reportes;
            }
        }

        /// <summary>
        /// Dado un identificador de reporte retorna, de existir en la BD, el reporte en cuestion.
        /// Retorna null de no existir el reporte en la BD.
        /// </summary>
        /// <param name="idEquipo">id del reporte que se desea seleccionar de la BD</param>        
        public Reporte GetReporte(int idReporte)
        {
            Reporte reporte = (from report in Cnx.Reporte
                            where report.idReporte == idReporte
                            select report).FirstOrDefault();
            return reporte;
        }

        /// <summary>
        /// Dado un numero de reporte retorna, de existir en la BD, el reporte en cuestion.
        /// Retorna null de no existir el reporte en la BD.
        /// </summary>
        /// <param name="idEquipo">id del equipo que se desea seleccionar de la BD</param>        
        public Reporte GetReportePorNumero(String numero)
        {
            Reporte reporte = (from report in Cnx.Reporte
                               where report.numero == numero
                               select report).FirstOrDefault();
            return reporte;
        }

        /// <summary>
        /// Retorna todos los reportes que pertenecen al cliente 'nombreCliente' pasado por parametro
        /// </summary>
        /// <param name="nombreCliente"></param>
        /// <returns></returns>
        public List<Reporte> GetReportesPorNombreCliente(String nombreCliente)
        {
            List<Reporte> reportes = (from report in Cnx.Reporte
                                     where report.nombreCliente.ToUpper() == nombreCliente.ToUpper()
                                     select report).ToList();
            return reportes;
        }

        /// <summary>
        /// Retorna una lista de reportes que esten pendientes por Defectar. 
        /// De no haber ninguno en este estado, retorna la lista vacia
        /// </summary>
        /// <returns></returns>
        public List<Reporte> GetReportesPendienteADefectar()
        {
            return this.GetReportesPorEstado(Estados.PendienteADefectar);
        }

        /// <summary>
        /// Retorna una lista de reportes que esten siendo defectados. 
        /// De no haber ninguno en este estado, retorna la lista vacia
        /// </summary>
        /// <returns></returns>
        public List<Reporte> GetReportesSiendoDefectado()
        {
            return this.GetReportesPorEstado(Estados.SiendoDefectado);
        }

        public List<Reporte> GetReportesPorEstado(String estado)
        {
            List<Reporte> reportes = (from report in Cnx.Reporte
                                      where report.estado.ToLower() == estado.ToLower()
                                      select report).ToList();
            return reportes;
        }

        /// <summary>
        /// Adiciona a la BD un reporte dado, sobre el contexto igualmente pasado
        /// </summary>
        /// <param name="reporte">reporte que se pretende adicionar a la BD</param>
        public void Adicionar(Reporte reporte, ReporteDBEntities cnx)
        {
            try
            {                
                cnx.Reporte.AddObject(reporte);
                cnx.SaveChanges();
            }
            catch (Exception msg)
            {
                throw new Exception("Ocurrió un error adicionando el reporte en la BD: " + reporte.numero + ". " + msg.Message);
            }
        }
        /// <summary>
        /// Setea en la BD un reporte dado que debe haber sido editado en el contexto. Debe pasar un mismo reporte seleccionado previamente el cual mantenga su idReporte
        /// </summary>
        /// <param name="reporte">reporte con las modificaciones hechas</param>
        public void Editar(Reporte reporte)
        {
            try
            {
                Reporte report = this.GetReporte(reporte.idReporte);
                if (report != null)
                {
                    Cnx.Reporte.ApplyCurrentValues(report);
                    Cnx.SaveChanges();
                }
            }
            catch (Exception msg)
            {
                throw new Exception("Ocurrió un error en el proceso de edición del reporte: " + reporte.numero + ". " + msg.Message);
            }
        }

        /// <summary>
        /// Cambia el administrador al reporte pasado por parametro. Si se pasa null en administrador, entonces
        /// borra dicha relacion de la base de datos.
        /// </summary>
        public void CambiarAdministrador(Reporte reporteAModificar, Administrador administrador)
        {
            if (administrador == null)
                reporteAModificar.Administrador = null;
            reporteAModificar.Administrador = administrador;
            Cnx.SaveChanges();
        }

        /// <summary>
        /// Retorna la ultima cantidad de reportes que se pase por parametro en 'cantidad' de la persona 'nombreCliente', ordenados por fecha.
        /// </summary>
        /// <param name="cantidad"></param>
        /// <returns></returns>
        public List<Reporte> GetLastDelCliente(int cantidad, String nombreCliente)
        {
            List<Reporte> reportes = (from report in Cnx.Reporte
                                      orderby report.fecha_hora descending
                                      where report.nombreCliente.ToUpper() == nombreCliente.ToUpper()
                                      select report).Take(cantidad).ToList();
            return reportes;                           
        }

        /// <summary>
        /// Retorna la ultima cantidad de reportes que se pase por parametro en 'cantidad', ordenados por fecha.
        /// </summary>
        /// <param name="cantidad"></param>
        /// <returns></returns>
        public List<Reporte> GetLast(int cantidad)
        {
            List<Reporte> reportes = (from report in Cnx.Reporte
                                      orderby report.fecha_hora descending
                                      select report).Take(cantidad).ToList();
            return reportes;
        }

        /// <summary>
        /// Borra un reporte de la tabla de Reporte de la base de datos
        /// </summary>
        public void Borrar(Reporte reporte)
        {
            try
            {
                Reporte rborrar = this.GetReporte(reporte.idReporte);
                this.Cnx.DeleteObject(rborrar);                
                this.Cnx.SaveChanges();
            }
            catch (Exception msg)
            {
                throw new Exception("Ocurrió un error borrando el reporte: " + reporte.numero + ". " + msg.Message);
            }

        }
    }
}
