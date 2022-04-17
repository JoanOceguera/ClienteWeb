<%@ Page Language="C#" AutoEventWireup="true" CodeFile="reporteUsuario.aspx.cs" Inherits="ReporteUsuario" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
            width: 100%;
        }
        .style2
        {
            width: 92px;
        }
        .style3
        {
            width: 455px;
            text-align: center;
            background-color: #C0C0C0;
            height: 26px;            
        }
        .style4
        {
            text-align: center;
            color: white;
            background-color: #416271;
            width: 455px;
        }
        .style5
        {
            width: 455px;
            text-align: center;
            background-color: #FFFFFF;
        }
        .style6
        {
            width: 92px;
            height: 26px;
        }
        .style7
        {
            height: 26px;
        }
        #btn_verificar
        {
            margin: 5px 5px 5px 5px; 
            margin: 5px 5px 5px 5px;  
             
        }
        #txt_expediente
        {
            margin-bottom: 5px;    
        }
        .style8
        {
            text-align: justify;
        }
        .style9
        {
            width: 166px;
        }
        .style10
        {
            width: 166px;
            height: 26px;
        }
    </style>
</head>
<body>
        
<div id="main_container">
	<div id="header">    	
        <div id="menu"></div>
    </div>
    
    <div class="green_box">
    	<div class="clock">
        <img src="images/logoRobot.png" alt="" title="" 
                style="height: 128px; width: 180px" />             
        </div>
        <div class="text_content">
        <h1>Sistema de reportes. Informática y comunicaciones.</h1>
        <p class="green" style="text-align: left">
            Reporte su problema al grupo de Informática y comunicaciones y con gusto se 
            atenderá.</p>
        
        </div> 
    </div>

    <div id="main_content">
    <form id="form1" runat="server">
    <div id="cabecera">    
        <br />
    </div>

    <div id = "cuerpo"> 
        
        <table class="style1">
            <tr>
                <td class="style2">
                    &nbsp;</td>
                <td class="style9">
                    &nbsp;</td>
                <td style="font-size: small; " class="style8">
                    <strong>Escriba su número de solapín y presione el botón Entrar.</strong> Puede pinchar el botón 
                    verificar después de haber escrito su número de solapín y de esa forma comprobar si 
                    se corresponde con su nombre y departamento.</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style2">
                    &nbsp;</td>
                <td class="style9">
                    &nbsp;</td>
                <td style="font-size: small; font-weight: 700;">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style2">
                    &nbsp;</td>
                <td class="style9">
                    &nbsp;</td>
                <td class="style4" style="font-size: small; font-weight: 700;" rowspan="1">
                    Introduzca su número de solapín</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style6">
                    </td>
                <td class="style10">
                    </td>
                <td class="style3" rowspan="1">
                    <asp:TextBox ID="txt_expediente" runat="server" ForeColor="#333333" 
            Width="145px" style="text-align: center"></asp:TextBox>
                    <asp:Button ID="btn_verificar" runat="server" BackColor="#666666" 
                        BorderStyle="None" ForeColor="White" Height="22px" Text="verificar" 
                        onclick="btn_verificar_Click" UseSubmitBehavior="False" />
                </td>
                <td class="style7">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="txt_expediente" ErrorMessage="Escriba su número de solapín" 
                        ForeColor="#FF5050"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    &nbsp;</td>
                <td class="style9">
                    &nbsp;</td>
                <td class="style5" rowspan="1">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style2">
                    &nbsp;</td>
                <td class="style9">
                    &nbsp;</td>
                <td class="style5" rowspan="1">
                    <asp:TextBox ID="txt_cliente" runat="server" ForeColor="#333333" Width="442px" 
                        BorderStyle="None" ReadOnly="True"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style2">
                    &nbsp;</td>
                <td class="style9">
                    &nbsp;</td>
                <td class="style5" rowspan="1">
                    <asp:TextBox ID="txt_departamento" runat="server" ForeColor="#333333" 
                        Width="442px" BorderStyle="None" ReadOnly="True"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style2">
                    &nbsp;</td>
                <td class="style9">
                    &nbsp;</td>
                <td class="style5" rowspan="1">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style2">
                    &nbsp;</td>
                <td class="style9">
                    &nbsp;</td>
                <td align="center" bgcolor="#CCCCCC" rowspan="1">
                    <asp:Button ID="btn_enviar" runat="server" Text="Entrar" 
                        onclick="btn_enviar_Click" BackColor="#666666" BorderColor="#666666" 
                        BorderStyle="Solid" BorderWidth="5px" Font-Size="Medium" ForeColor="White" 
                        Height="40px" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    </div>
        <div style="clear: both;">
            <br />
            <br />
            <br />
            <br />
            <br />
        </div>

    </form>

    </div>
    <div style=" clear:both;">
        <div id="footer">

            <img src="images/logo.png" alt="" title="" style="height: 37px; width: 76px" /></div>
    </div>
</div>
</body>
</html>
