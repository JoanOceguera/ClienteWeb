using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ServiceReference1;

public partial class ReporteUsuario : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btn_enviar_Click(object sender, EventArgs e)
    {
        PersonaVisual person = Checkear();
        if (person != null)
            Response.Redirect("reporte.aspx?cliente=" + person.Nombre + "&departamento=" + person.Departamento);
    }
    protected void btn_verificar_Click(object sender, EventArgs e)
    {
        Checkear();
    }
    public PersonaVisual Checkear()
    {
        PersonaVisual person = new PersonaVisual();
        try
        {            
            var serv = new Service1Client();
            PersonalRH persona = serv.DamePersonaxExp(Convert.ToInt32(this.txt_expediente.Text));

            this.txt_cliente.Text = "Nombre: " + persona.Nombre + " " + persona.Apellido1 + " " + persona.Apellido2;
            String depart = serv.DameNombreDepartamentoPersonaxExp(Convert.ToInt32(this.txt_expediente.Text));
            this.txt_departamento.Text = "Departamento: " + serv.DameNombreDepartamentoPersonaxExp(Convert.ToInt32(this.txt_expediente.Text));
            person.Nombre = persona.Nombre + " " + persona.Apellido1 + " " + persona.Apellido2;
            person.Departamento = depart;
            serv.Abort();
            return person;
        }
        catch (Exception msg)
        {
            this.txt_cliente.Text = "No fue posible obtener la información para el valor dado: " + this.txt_expediente.Text;
            this.txt_departamento.Text = String.Empty;
            return null;
        }
    }
}