using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ReporteDBModel;

public partial class Default : System.Web.UI.Page
{
    private ReporteDBEntities _cnx;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.Params["cliente"] != null)
        {
            Session["cliente"] = Request.Params["cliente"];
            this.lbl_nombreCliente.Text = Session["cliente"].ToString();
        }
        if (Request.Params["departamento"] != null)
        {
            Session["departamento"] = Request.Params["departamento"];
        }
        //if (IsPostBack)//si no es un refresh
        //{           
        LLenarDataView();
        //}
        this.lbl_fecha_hora.Text = String.Format("{0:d/M/yyyy HH:mm:ss}", DateTime.Now);
    }
    public ReporteDBEntities Cnx
    {
        get { return _cnx ?? (_cnx = new ReporteDBEntities()); }
        set { _cnx = value; }
    }


    /// <summary>
    /// se pasa true en empty para crear un datatable con solo 1 columna
    /// </summary>
    /// <param name="empty"></param>
    /// <returns></returns>
    public DataTable CrearDataTable(bool empty)
    {
        DataTable dt = new DataTable();
        try
        {
            if (!empty)
            {
                dt.Columns.Add("Número", Type.GetType("System.String"));
                dt.Columns.Add("Situación", Type.GetType("System.String"));
                dt.Columns.Add("Fecha", Type.GetType("System.String"));
                dt.Columns.Add("Estado", Type.GetType("System.String"));
                dt.Columns.Add("Técnico", Type.GetType("System.String"));
            }
            else dt.Columns.Add("_", Type.GetType("System.String"));
        }
        catch (Exception)
        {
            return null;
        }
        return dt;
    }
    public void LLenarDataView()
    {
        ReporteDBEntities context = Cnx;
        String cliente = Session["cliente"].ToString().ToUpper();

        List<Reporte> reportes = (from report in context.Reporte
                                  orderby report.fecha_hora descending
                                  where report.nombreCliente.ToUpper() == cliente
                                  select report).Take(5).ToList();

        ReporteControl control = new ReporteControl();
        DataTable dt = null;
        if (reportes.Count > 0)//si hay reportes pendientes
        {
            dt = CrearDataTable(false);
            if (dt != null)
            {
                String estado = "Pendiente";
                foreach (Reporte repo in reportes)
                {
                    //insertar el registro en el datatable
                    String fechaHora = String.Format("{0:dd/MM/yyy hh:mm}", repo.fecha_hora);

                    if (repo.estado == Estados.SiendoDefectado)
                        estado = "Siendo defectado";
                    else if (repo.estado == Estados.PendienteADefectar)
                        estado = "Pendiente";

                    String tecnico = "--";
                    if (repo.Administrador != null)
                        tecnico = repo.Administrador.nombre;
                    dt.Rows.Add(repo.numero, repo.Equipo.nombre + " - " + repo.ProblemaPosible.problemaInfo, fechaHora, estado,tecnico);
                }
                dtg_sinSolucionar.DataSource = dt;
                dtg_sinSolucionar.DataBind();
            }
        }
        else//si no hay reportes pendientes
        {
            dt = CrearDataTable(true);
            if (dt != null) 
            {
                dt.Rows.Add("No hay reportes por solucionar.");
                dtg_sinSolucionar.DataSource = dt;
                dtg_sinSolucionar.DataBind();
            }

        }


    }

    public bool RealizarReporte()
    {
        try
        {
            //Entorno entorno = this.GetEntornoPorId(Convert.ToInt32(this.drop_entorno.SelectedValue));
            Equipo equipo = this.GetEquipoPorId(Convert.ToInt32(this.idEquipo.SelectedValue));
            ProblemaPosible problema = this.GetProblemaPosiblePorId(Convert.ToInt32(this.list_problemas.SelectedValue));

            String nombrePc = txt_nombrepc.Text == String.Empty ? null : txt_nombrepc.Text;
            String observacion = txt_observacion.Text == String.Empty ? null : txt_observacion.Text;
        
            String nombreCliente = "sin_nombre";
            if(Session["cliente"] != null) 
             nombreCliente = Session["cliente"].ToString();

            String departamento = "sin_departamento";
            if(Session["departamento"] != null)
                departamento = Session["departamento"].ToString();

            String numero = GetAndSetNextNumero();      
        
            ReporteControl control = new ReporteControl();
            Reporte reporte = control.Crear(null, equipo, nombreCliente, departamento, problema, nombrePc, observacion, numero);
        
            control.Adicionar(reporte,Cnx);     
        }
        catch (Exception)
        {
            return false;
        }
        return true;
    }

    protected void btn_enviar_Click(object sender, EventArgs e)
    {
        if(RealizarReporte())
        {
            MostrarMensajeFinalizado();
            LLenarDataView();
            LimpiarCampos();
            Response.Redirect("reporte.aspx?cliente=" + Session["cliente"] + "&departamento=" + Session["departamento"]);
        }       
    }
    public Entorno GetEntornoPorId(int idEntorno)
    {
        ReporteDBEntities context = Cnx;
        Entorno entorno = (from entorn in context.Entorno
                           where entorn.idEntorno == idEntorno
                           select entorn).FirstOrDefault();
        return entorno;
    }
    public Equipo GetEquipoPorId(int idEquipo)
    {
        //ReporteDBEntities cnx = new ReporteDBEntities();
        Equipo equip = (from equipo in Cnx.Equipo
                        where equipo.idEquipo == idEquipo
                        select equipo).FirstOrDefault();
        return equip;
    }
    public ProblemaPosible GetProblemaPosiblePorId(int idProblemPosible)
    {
        // ReporteDBEntities cnx = new ReporteDBEntities();
        ProblemaPosible problem = (from problema in Cnx.ProblemaPosible
                                   where problema.idProblemaPosible == idProblemPosible
                                   select problema).FirstOrDefault();
        return problem;
    }

    public String GetAndSetNumeroReporte()
    {
        return string.Empty;
    }

    public Consecutivo GetValorActualConsecutivo()
    {
        Consecutivo consec = (from consecutivo in Cnx.Consecutivo
                              select consecutivo).FirstOrDefault();
        if (consec == null)
        {
            consec = (new Consecutivo() { consecutivoSecuencia = 0 });
            this.Adicionar(consec);
        }
        return consec;
    }
    private void Adicionar(Consecutivo consecutivo)
    {
        try
        {
            Cnx.Consecutivo.AddObject(consecutivo);
            Cnx.SaveChanges();
        }
        catch (Exception msg)
        {
            throw new Exception("Ocurrió un error adicionando el consecutivo en la BD: " + consecutivo.consecutivoSecuencia + ". " + msg.Message);
        }
    }
    public Consecutivo GetAndSetNext()
    {
        int incremento = 1;
        Consecutivo conActual = this.GetValorActualConsecutivo();
        if (conActual.consecutivoSecuencia >= 999)
            incremento = conActual.consecutivoSecuencia * -1;
        conActual.consecutivoSecuencia += incremento;
        Cnx.SaveChanges();

        return conActual;
    }
    public String GetAndSetNextNumero()
    {        
        Consecutivo cons = GetAndSetNext();
        String dtn = DateTime.Now.ToString("MMyy");
        String numero = cons.consecutivoSecuencia.ToString() + dtn.Substring(0, 2) + dtn.Substring(2, 2);

        return numero;
    }
    public void MostrarMensajeFinalizado()
    {
        String mensaje = "Reporte enviado exitosamente.";
        this.Page.Response.Write("<script language='JavaScript'>window.alert('" + mensaje + "');</script>");
    }
    public void LimpiarCampos()
    {
        this.txt_nombrepc.Text = String.Empty;
        this.txt_observacion.Text = string.Empty;
        this.idEquipo.SelectedIndex = 0;
        //this.list_problemas.SelectedIndex = 0;
        this.drop_entorno.SelectedIndex = 0;
    }
    public void Cerrar()
    {
        Session.Clear();
        Session.RemoveAll();
        Session.Abandon();
        Response.Redirect("reporteUsuario.aspx");
    }
    protected void salirLink_Click(object sender, EventArgs e)
    {
        Cerrar();        
    }
    protected void UpdateTimer_Tick(object sender, EventArgs e)
    {
        lbl_fecha_hora.Text = DateTime.Now.ToString("d/M/yyyy HH:mm:ss");
    }
}