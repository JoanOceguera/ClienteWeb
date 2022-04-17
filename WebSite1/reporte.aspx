<%@ Page EnableEventValidation="false" Language="C#" AutoEventWireup="true" CodeFile="reporte.aspx.cs" Inherits="Default"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reportes</title>
    <link rel="stylesheet" type="text/css" href="styles/style.css" media="screen" />
    <style type="text/css">
        table
        {
            color: Gray;
            font-family:Tahoma;
            font-size:small;
        }
        .style1
        {
            width: 98%;
            margin-right: 0px;
        }
        .style2
        {
            width: 120px;
        }
        .style5
        {
            width: 237px;
        }
        .style8
        {
            width: 120px;
            height: 32px;
        }
        .style9
        {
            width: 237px;
            height: 32px;
        }
        .style11
        {
            height: 32px;
        }
        .style12
        {
            width: 120px;
            height: 42px;
        }
        .style13
        {
            width: 237px;
            height: 42px;
        }
        .style15
        {
            height: 42px;
        }
               
        
        .style16
        {
            height: 42px;
            width: 5px;
        }
        .style17
        {
            width: 5px;
        }
        .style18
        {
            height: 32px;
            width: 5px;
        }
        .style19
        {
            color: #FF6666;
        }
               
        
        .style20
        {
            width: 120px;
            height: 26px;
        }
        .style21
        {
            width: 237px;
            height: 26px;
        }
        .style22
        {
            width: 5px;
            height: 26px;
        }
        .style23
        {
            height: 26px;
        }
         #lbl_nombreCliente
         {
            padding: 5px;    
         }      
         #salir
         {
             padding-top:80px;
             height:13px;
             width:40px;
             float:inherit;
             margin-left: 200px;
         }
         #lbl_fecha_hora
         {
             float:right;
             color:#F7FAFE;
             padding: 3px 3px 3px 3px;
             padding-right: 5px;
          }
        </style>
</head>


<body>
<form id="form1" runat="server">
<div id="main_container">
	<div id="header">    
    	
        <div id="menu">
                <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <asp:Timer runat="server" id="UpdateTimer" interval="1000" ontick="UpdateTimer_Tick" />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger controlid="UpdateTimer" eventname="Tick" />
            </Triggers>
            <ContentTemplate>                
                <asp:Label ID="lbl_fecha_hora" runat="server" Text="fecha"></asp:Label>                          
            </ContentTemplate>
        </asp:UpdatePanel>            
        </div>
    </div>
    
    <div class="green_box">
    	<div class="clock">
        <img src="images/logoRobot.png" alt="" title="" 
                style="height: 121px; width: 167px" />             
        </div>
        <div class="text_content">
        <h1>Sistema de reportes. Informática y comunicaciones.</h1>
        <p class="green" style="text-align: left">
            Reporte su problema al grupo de Informática y comunicaciones y con gusto se 
            atenderá.</p>
        
        </div> 
                <div class = "salir_class">
                    <asp:LinkButton ID="salirLink" runat="server" onclick="salirLink_Click" 
                        CausesValidation="False" Height="16px" Width="57px">Salir</asp:LinkButton>
                </div>
        
    </div><!--end of green box-->
    


    <div id="main_content">
   
    <div id="left_content">
    <div id = "cabecera">
        <asp:Label ID="lbl_nombreCliente" runat="server" BackColor="#3B5967" 
            BorderStyle="Solid" ForeColor="White" style="margin-bottom: 0px" Text="Nombre"></asp:Label>
        </div>
    <div id = "cuerpo">

        <table class="style1">
            <tr>
                <td class="style12" id="E">
                    <span class="style19">*</span> Elemento:</td>
                <td class="style13">
                    <asp:DropDownList ID="idEquipo" runat="server" Width="240px" 
                        AutoPostBack="True" DataSourceID="SqlDataSource1" DataTextField="nombre" 
                        DataValueField="idEquipo" ForeColor="#666666" AppendDataBoundItems="True" 
                        ToolTip="Seleccione un elemento con problema">
                        <asp:ListItem Value="-1" Selected="True">.</asp:ListItem>
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:ReporteDBConnectionString %>" 
                        SelectCommand="SELECT [idEquipo], [nombre], [desuso] FROM [Equipo] WHERE ([desuso] = @desuso)">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="0" Name="desuso" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
                <td class="style16" colspan="0">
                    &nbsp;</td>
                <td class="style15">
                    <asp:CompareValidator ID="CompareValidator1" runat="server" 
                        ControlToValidate="idEquipo" ErrorMessage="Requerido" ForeColor="#FF6666" 
                        Operator="NotEqual" ValueToCompare="-1"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    <span class="style19">*</span>
                    Problema:</td>
                <td class="style5">

                    <asp:DropDownList ID="list_problemas" runat="server" 
                        DataSourceID="SqlDataSource2" DataTextField="problemaInfo" 
                        DataValueField="idProblemaPosible" ForeColor="#666666" Width="240px" 
                        ToolTip="Seleccione el problema que presenta el elemento seleccionado anteriormente.">
                        <asp:ListItem Selected="True" Value="-1">--seleccione--</asp:ListItem>
                    </asp:DropDownList>

                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:ReporteDBConnectionString %>" 
                        
                        SelectCommand="SELECT idProblemaPosible, idEquipo, problemaInfo, desuso FROM ProblemaPosible WHERE (idEquipo = @idEquipo) AND (desuso = @desuso)">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="idEquipo" Name="idEquipo" 
                                PropertyName="SelectedValue" Type="Int32" />
                            <asp:Parameter DefaultValue="0" Name="desuso" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
                <td class="style17" colspan="0">
                    &nbsp;</td>
                <td>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" 
                        ControlToValidate="list_problemas" ErrorMessage="Requerido" ForeColor="#FF6666" 
                        Operator="NotEqual" ValueToCompare="-1"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="style8">
                    Nombre PC:</td>
                <td class="style9">
                    <asp:TextBox ID="txt_nombrepc" runat="server" Width="240px" ForeColor="#666666" 
                        ToolTip="Escriba el nombre de la PC frente a la cual está sentado llenando este formulario."></asp:TextBox>
                </td>
                <td class="style18" colspan="0">
                    </td>
                <td class="style11">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style2">
                     Observación:</td>
                <td class="style5">
                    <asp:TextBox ID="txt_observacion" runat="server" Height="68px" Width="240px" 
                        TextMode="MultiLine" ForeColor="#666666" 
                        ToolTip="Sea libre de escribir en este campo cualquier información adicional sobre este reporte."></asp:TextBox>
                </td>
                <td class="style17" colspan="0">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style2">
                     &nbsp;</td>
                <td class="style5">
                    <asp:SqlDataSource ID="SqlDataSource4" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:ReporteDBConnectionString %>" 
                        SelectCommand="SELECT * FROM [Entorno] WHERE ([desuso] = @desuso)">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="0" Name="desuso" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:DropDownList ID="drop_entorno" runat="server" Width="240px" 
                        AutoPostBack="True" DataSourceID="SqlDataSource4" DataTextField="infoEntorno" 
                        DataValueField="idEntorno" ForeColor="#666666" AppendDataBoundItems="True" 
                        Visible="False">
                        <asp:ListItem Value="-1" Selected="True">.</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="style17" colspan="0">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style20">
                    &nbsp;</td>
                <td class="style21" align="right">
                    <asp:Button ID="btn_enviar" runat="server" Text="Reportar" 
                        onclick="btn_enviar_Click" BackColor="#666666" BorderColor="#666666" 
                        BorderStyle="Solid" BorderWidth="5px" Font-Size="Medium" ForeColor="White" 
                        Height="40px" ToolTip="Envia el reporte al departamento de informática" />
                </td>
                <td class="style22" colspan="0">
                    &nbsp;</td>
                <td class="style23">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style20">
                    </td>
                <td class="style21" align="right">
                    &nbsp;</td>
                <td class="style22" colspan="0">
                    </td>
                <td class="style23">
                    </td>
            </tr>
        </table>

    </div>
        </div><!--end of left content-->

    <div id = "right_content" style="background-color: #EEEEEE">
    <h2 id = "cabecera_titulo">Reportes sin solucionar</h2>

        <asp:GridView ID="dtg_sinSolucionar" runat="server" CellSpacing="5" 
            BorderStyle="Dashed" CaptionAlign="Bottom" CellPadding="8" 
            EnableTheming="True" GridLines="Horizontal" 
            ToolTip="Muestra el estado de sus últimos reportes.">
            <AlternatingRowStyle BackColor="#F7FAFE" />
            <HeaderStyle BackColor="Silver" />
        </asp:GridView>
    </div>
    </div>
</div>

</form>

<div style=" clear:both;">
    <div id="footer">
        <img src="images/logo.png" alt="" title="" style="height: 37px; width: 76px" />
    </div>
</div>
</body>
</html>






<!--formulario-->
