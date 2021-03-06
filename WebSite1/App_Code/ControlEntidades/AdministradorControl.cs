using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReporteDBModel
{
    class AdministradorControl : Controlador
    {
        public AdministradorControl() : base() { }


        /// <summary>
        /// Crea y retorna un administrador en uso
        /// </summary>
    /*    public Administrador Crear(String nombreAdministrador, String foto)
        {
            Administrador administ = new Administrador() { nombre = nombreAdministrador, foto = foto, desuso = 0 };
            return administ;
        }

        public Administrador Crear(String nombreAdministrador, String foto, int desuso)
        {
            Administrador administ = new Administrador() { nombre = nombreAdministrador, foto = foto, desuso = desuso };
            return administ;
        }*/

        /// <summary>
        /// Retorna una lista de administradores obtenidos de la BD. 
        /// Propiedad de solo lectura
        /// </summary>
        public List<Administrador> Administradores
        {
            get
            {
                List<Administrador> administ = (from admin in Cnx.Administrador
                                         select admin).ToList();
                return administ;
            }
        }

        /// <summary>
        /// Dado un identificador de administrador retorna, de existir en la BD, el administrador en cuestion.
        /// Retorna null de no existir el administrador en la BD.
        /// </summary>
        /// <param name="idAdministrador">id del equipo que se desea seleccionar de la BD</param>        
        public Administrador GetAdministrador(int idAdministrador)
        {/* lo comentariado funciona perfectamente, es solo otra variante de implementacion
            Administrador administ = (from admin in cnx.Administrador
                            where admin.idAdministrador == idAdministrador
                            select admin).FirstOrDefault();*/
            Administrador administ = this.Cnx.Administrador.Single(a => a.idAdministrador == idAdministrador);
            return administ;
        }

        /// <summary>
        /// retorna una lista con los administradores en uso
        /// </summary>
        /// <returns></returns>
        public List<Administrador> GetAdministradorEnUso()
        {
            List<Administrador> administ = (from admin in Cnx.Administrador
                                    where admin.desuso == 0
                                    select admin).ToList();
            return administ;
        }
        public List<Administrador> GetAdministradorEnDesUso()
        {
            List<Administrador> administ = (from admin in Cnx.Administrador
                                    where admin.desuso == 1
                                    select admin).ToList();
            return administ;
        }
        /// <summary>
        /// Adiciona a la BD un administrador dado
        /// </summary>
        /// <param name="administrador">administrador que se pretende adicionar a la BD</param>
        public void Adicionar(Administrador administrador)
        {
            try
            {
                Cnx.Administrador.AddObject(administrador);
                Cnx.SaveChanges();
            }
            catch (Exception msg)
            {
                throw new Exception("Ocurrió un error adicionando el administrador en la BD: " + administrador.nombre + ". " + msg.Message);
            }
        }
        /// <summary>
        /// Edita un administrador dado. Debe pasar un mismo administrador seleccionado previamente el cual mantenga su idAdministrador
        /// </summary>
        /// <param name="administrador">administrador con las modificaciones hechas</param>
        public void Editar(Administrador administrador)
        {
            try
            {
                Administrador administ = this.GetAdministrador(administrador.idAdministrador);
                if (administ != null)
                {
                    Cnx.Administrador.ApplyCurrentValues(administ);
                    Cnx.SaveChanges();
                }
            }
            catch (Exception msg)
            {
                throw new Exception("Ocurrió un error en el proceso de edición del administrador: " + administrador.nombre + ". " + msg.Message);
            }
        }

        /// <summary>
        /// No borra realmente un administrador de la base de datos. Solamente setea el campo 'desuso' en 1
        /// </summary>
        public void BorrarPorId(int idAdministrador)
        {
            try
            {
                Administrador administ = this.GetAdministrador(idAdministrador);
                if (administ != null)
                {
                    administ.desuso = 1;
                    Cnx.SaveChanges();
                }
                else throw new Exception("El administrador que intenta borrar no existe en la base de datos");
            }
            catch (Exception msg)
            {
                throw new Exception("Ocurrió un error en el proceso de borrado del administrador. " + msg.Message);
            }
        }

        /// <summary>
        /// No borra un administrador de la base de datos. Solamente setea el campo 'desuso' en 1
        /// </summary>
        /// <param name="administrador"></param>
        public void Borrar(Administrador administrador)
        {
            try
            {
                Administrador administ = this.GetAdministrador(administrador.idAdministrador);
                if (administ != null)
                {
                    administ.desuso = 1;
                    Cnx.SaveChanges();
                }
                else throw new Exception("El administrador que intenta borrar no existe en la base de datos");
            }
            catch (Exception msg)
            {
                throw new Exception("Ocurrió un error en el proceso de borrado del administrador. " + msg.Message);
            }
        }
        /// <summary>
        /// Retorna true de no existir el administrador en la base de datos o estar marcado como desuso, esto es si el campo
        /// 'desuso' esta seteado en 1.
        /// </summary>
        public bool EstaBorradoPorId(int idAdministrador)
        {
            Administrador administ = this.GetAdministrador(idAdministrador);
            if (administ != null)
                return (administ.desuso == 1);

            return true;
        }

        /// <summary>
        /// Retorna true de no existir el equipo en la base de datos o estar marcado como desuso, esto es si el campo
        /// 'desuso' esta seteado en 1.
        /// </summary>
        public bool EstaBorrado(Administrador administrador)
        {
            Administrador administ = this.GetAdministrador(administrador.idAdministrador);
            if (administ != null)
                return (administ.desuso == 1);

            return true;
        }
    }
}
