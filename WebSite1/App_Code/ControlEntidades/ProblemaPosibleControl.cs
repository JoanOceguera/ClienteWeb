using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReporteDBModel
{
    class ProblemaPosibleControl : Controlador
    {
        public ProblemaPosibleControl() : base() { }

        /// <summary>
        /// Crea y retorna un ProblemaPosible sin idEquipo, por defecto se setea en uso
        /// </summary>
        public ProblemaPosible Crear(String problemaInfo)
        {            
            ProblemaPosible problemPos = new ProblemaPosible() { problemaInfo = problemaInfo, desuso = 0 };
            return problemPos;    
        }

        /// <summary>
        /// Dado un identificador de ProblemaPosible retorna, de existir en la BD, el ProblemaPosible en cuestion.
        /// Retorna null de no existir el ProblemaPosible en la BD.
        /// </summary>
        /// <param name="idEquipo">id del equipo que se desea seleccionar de la BD</param>        
        public ProblemaPosible GetProblemaPosible(int idProblemPosible)
        {
            ProblemaPosible problem = (from problema in Cnx.ProblemaPosible
                            where problema.idProblemaPosible == idProblemPosible
                            select problema).FirstOrDefault();
            return problem;
        }

        public List<ProblemaPosible> GetProblemasPosiblesEnUso()
        {
            List<ProblemaPosible> problem = (from problema in Cnx.ProblemaPosible
                                    where problema.desuso == 0
                                    select problema).ToList();
            return problem;
        }
        public List<ProblemaPosible> GetProblemasPosiblesEnDesUso()
        {
            List<ProblemaPosible> problem = (from problema in Cnx.ProblemaPosible
                                    where problema.desuso == 1
                                    select problema).ToList();
            return problem;
        }

        /// <summary>
        /// Edita un ProblemaPosible dado. Debe pasar un mismo ProblemaPosible seleccionado previamente el cual mantenga su idProblemaPosible
        /// </summary>
        /// <param name="problemaPosible">ProblemaPosible con las modificaciones hechas</param>
        public void Editar(ProblemaPosible problemaPosible)
        {
            try
            {
                ProblemaPosible prob = this.GetProblemaPosible(problemaPosible.idProblemaPosible);
                if (prob != null)
                {
                    Cnx.ProblemaPosible.ApplyCurrentValues(prob);
                    Cnx.SaveChanges();
                }
            }
            catch (Exception msg)
            {
                throw new Exception("Ocurrió un error en el proceso de edición del ProblemaPosible. "  + msg.Message);
            }
        }


        /// <summary>
        /// No borra un problemaPosible de la base de datos. Solamente setea el campo 'desuso' en 1
        /// </summary>
        public void BorrarPorId(int idProblemaPosible)
        {
            try
            {
                ProblemaPosible problem = this.GetProblemaPosible(idProblemaPosible);
                if (problem != null)
                {
                    problem.desuso = 1;
                    Cnx.SaveChanges();
                }
                else throw new Exception("El problemaPosible que intenta borrar no existe en la base de datos");
            }
            catch (Exception msg)
            {
                throw new Exception("Ocurrió un error en el proceso de borrado del problemaPosible. " + msg.Message);
            }
        }
        /// <summary>
        /// No borra un equipo de la base de datos. Solamente setea el campo 'desuso' en 1
        /// </summary>
        /// <param name="idEquipo"></param>
        public void Borrar(ProblemaPosible problemaPosible)
        {
            try
            {
                ProblemaPosible problem = this.GetProblemaPosible(problemaPosible.idProblemaPosible);
                if (problem != null)
                {
                    problem.desuso = 1;
                    Cnx.SaveChanges();
                }
                else throw new Exception("El problemaPosible que intenta borrar no existe en la base de datos");
            }
            catch (Exception msg)
            {
                throw new Exception("Ocurrió un error en el proceso de borrado del problemaPosible. " + msg.Message);
            }
        }



        /***Este codigo funciona, pero se comentarea porke en la practica para esta aplicacion no se puede
         * borrar un ProblemaPosible ya que tiene una relacion con Reporte y un idProblemaPosible puede estar en Reporte 
        /// <summary>
        /// Borra un problema posible dado de la base de datos
        /// </summary>
        /// <param name="problemaPosible"></param>
        public void Borrar(ProblemaPosible problemaPosible)
        {
            this.cnx.DeleteObject(problemaPosible);
            this.cnx.SaveChanges();
        }
        /// <summary>
        /// Borra una lista de problemasPosibles de la BD
        /// </summary>
        /// <param name="problemasPosibles"></param>
        public void Borrar(List<ProblemaPosible> problemasPosibles)
        {
            foreach (ProblemaPosible problem in problemasPosibles)
               this.cnx.DeleteObject(problem);             
            this.cnx.SaveChanges();
        }
        /// <summary>
        /// Borra un problema posible dado su id. Retorna el ProblemaPosible borrado o null de no poder borrarlo
        /// </summary>
        public ProblemaPosible Borrar(int idProblemaPosible)
        { 
            Object problemaPosibleEncontrado;
            if (this.cnx.TryGetObjectByKey(new System.Data.EntityKey("ReporteDBEntities1.ProblemaPosible", "idProblemaPosible", idProblemaPosible), out problemaPosibleEncontrado))// si encuentra el ProblemaPosible con el id pasado por parametro
            {
                try
                {
                    this.cnx.DeleteObject(problemaPosibleEncontrado);
                    this.cnx.SaveChanges();
                }
                catch (System.Data.OptimisticConcurrencyException)//si no se pudo borrar entonces retorna null
                {
                    return null;
                }

            }
            else//si no se pudo encontrar el ProblemaPosible con el id pasado por parametro
            {
                return null;
            }
            return (ProblemaPosible)problemaPosibleEncontrado;
        }*/
    }
}
