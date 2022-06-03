<%@ Page Title="Empleados" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Empleados.aspx.cs" Inherits="WebAppDELICIAS.ADM.Empleados" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div>
        <div class="row">
            <div class="form-group">
                <div class="col-md-12 col-xs-12">
                    <asp:Label AssociatedControlID="lblEmpleado" ForeColor="#5cb85c" Font-Size="Large" CssClass="col-md-2 col-xs-5 control-label" runat="server">ID Empleado:</asp:Label>
                    <asp:Label ID="lblEmpleado" CssClass="col-md-10 col-xs-7 control-label" ForeColor="#5cb85c" Font-Size="Large" Font-Bold="true" runat="server"></asp:Label>
                </div>
                <div class="col-md-5">
                    <asp:Label AssociatedControlID="txtDNI" CssClass="col-md-2 control-label" runat="server">DNI:</asp:Label>
                    <div class="col-md-10">
                        <asp:TextBox ID="txtDNI" CssClass="form-control" runat="server" ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvDNI" Display="Dynamic" ForeColor="Yellow" ControlToValidate="txtDNI" ErrorMessage="* DNI necesario." ValidationGroup="G1" runat="server"/>
                    </div>
                </div>
                <div class="col-md-7">
                    <asp:Label AssociatedControlID="txtApNombres" CssClass="col-md-2 control-label" runat="server" >Apellidos y Nombres:</asp:Label>
                    <div class="col-md-10">
                        <asp:TextBox ID="txtApNombres" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvApNom" Display="Dynamic" ForeColor="Yellow" ControlToValidate="txtApNombres" ErrorMessage="* A. y Nombres necesario." ValidationGroup="G1" runat="server"/>
                    </div>
                </div>
                <div class="col-md-5">
                    <asp:Label AssociatedControlID="txtTelefono" CssClass="col-md-2 control-label" runat="server" >Teléfono:</asp:Label>
                    <div class="col-md-10">
                        <asp:TextBox ID="txtTelefono" CssClass="form-control" TextMode="Number" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvTelef" Display="Dynamic" ForeColor="Yellow" ControlToValidate="txtTelefono" ErrorMessage="* Teléfono necesario." ValidationGroup="G1" runat="server"/>
                    </div>
                </div>
                <div class="col-md-7">
                    <asp:Label AssociatedControlID="txtEmail" CssClass="col-md-2 control-label" runat="server">Email:</asp:Label>
                    <div class="col-md-10">
                        <asp:TextBox ID="txtEmail" CssClass="form-control" TextMode="Email" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvEmail" Display="Dynamic" ForeColor="Yellow" ControlToValidate="txtEmail" ErrorMessage="* Email necesario." ValidationGroup="G1" runat="server"/>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="form-group">
                <div class="col-md-5">
                    <asp:Label runat="server" CssClass="col-md-2 control-label" AssociatedControlID="txtCargo">Cargo:</asp:Label>
                    <div class="col-md-10">
                        <asp:TextBox runat="server" ID="txtCargo" CssClass="form-control"/>
                    </div>
                </div>
                <div class="col-md-3">
                    <asp:Label runat="server" CssClass="col-md-4 control-label" AssociatedControlID="txtLimiteVenta">Ración Semanal:</asp:Label>
                    <div class="col-md-8">
                        <asp:TextBox runat="server" ID="txtLimiteVenta" TextMode="Number" step="0.1" min="0" CssClass="form-control"/>
                        <asp:RequiredFieldValidator ID="rfvLimVnta" Display="Dynamic" ForeColor="Yellow" ControlToValidate="txtLimiteVenta" ErrorMessage="* Ración necesario." ValidationGroup="G1" runat="server"/>
                    </div>
                </div>
                <div class="col-md-4">
                    <asp:Label runat="server" CssClass="col-md-5 control-label" AssociatedControlID="cmbRol">Cuenta de Inicio de Sesión:</asp:Label>
                    <div class="col-md-7">
                        <asp:DropDownList ID="cmbRol" CssClass="form-control"  runat="server">
                            <asp:ListItem Text="Sin cuenta" Value="nnn"></asp:ListItem>
                            <asp:ListItem Text="Con cuenta de Usuario" Value="usu"></asp:ListItem>
                            <asp:ListItem Text="Con cuenta de Administrador" Value="adm"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <section class="row">
            <div class="col-md-4 col-xs-10">
                 <asp:Label ID="Label5" runat="server" Text="Buscar Empleado: "></asp:Label>
                 <div class="input-group">
                     <asp:TextBox ID="txtbuscar" CssClass="form-control" runat="server" placeholder="Nombre de Empleado..."></asp:TextBox>
                     <span class="input-group-btn">
                         <asp:Button ID="btnbuscar" CssClass="btn btn-info" ValidationGroup="fff" runat="server" Text="Buscar" OnClick="btnbuscar_Click"/>
                     </span>
                 </div>
           </div>
            <div class="col-md-6 col-xs-12">
                <div class="row" style="margin:0px; padding:0px; height:20px"><asp:Label ForeColor="#99ccff" runat="server" ID="lblinfo1"></asp:Label></div>
                <div class="btn-group botones" role="group" style="margin:0px;">
                    <asp:Button ID="btnnuevo" cssclass="btn btn-primary" OnClick="btnnuevo_Click" runat="server" Text="Nuevo" ToolTip="Ingresar un nuevo Cliente." />
                    <asp:Button ID="btnguardar" cssclass="btn btn-success" OnClick="btnguardar_Click" runat="server" Text="Guardar" Enabled="False" ValidationGroup="G1" ToolTip="Guardar datos del Cliente preparado."/>
                    <asp:Button ID="btnmodificar" cssclass="btn btn-warning" OnClick="btnmodificar_Click" runat="server" Text="Modificar Datos"  ValidationGroup="G1" ToolTip="Permite editar los datos del Cliente seleccionado. Luego de Editar los datos debe pulsar en Guardar para registrar los cambios."/>
                </div>
            </div>
        </section>
        <br />
        <div class="row">
             <div class="col-md-12" style="overflow:auto; max-height:400px; overflow-y:auto" >
                 <asp:Label ID="lbloperacion" runat="server" Visible="False"></asp:Label>
                 <asp:GridView ID="grdEmpleados" runat="server" OnSelectedIndexChanged="grdEmpleados_SelectedIndexChanged" DataKeyNames="ID" 
                     CssClass="gridview">                    
                     
                 </asp:GridView>
             </div>
       </div>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
    
    <br />

</asp:Content>
