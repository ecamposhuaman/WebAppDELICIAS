<%@ Page Title="Clientes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="WebAppDELICIAS.Clientes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div>
        <div class="row">
            <div class="form-group">
                <div class="col-md-12 col-xs-12">
                    <asp:Label AssociatedControlID="lblIdCliente" ForeColor="#5cb85c" Font-Size="Large" CssClass="col-md-2 col-xs-4 control-label" runat="server">ID Cliente:</asp:Label>
                    <asp:Label ID="lblIdCliente" CssClass="col-md-10 col-xs-8 control-label" ForeColor="#5cb85c" Font-Size="Large" Font-Bold="true" runat="server"></asp:Label>
                </div>
                <div class="col-md-5">
                    <asp:Label AssociatedControlID="txtDNI" CssClass="col-md-2 control-label" runat="server">DNI:</asp:Label>
                    <div class="col-md-10">
                        <asp:TextBox ID="txtDNI" CssClass="form-control" runat="server" ></asp:TextBox>
                        <%--<asp:RequiredFieldValidator ID="rfvDNI" Display="Dynamic" ForeColor="Yellow" ControlToValidate="txtDNI" ErrorMessage="* DNI necesario." ValidationGroup="G1" runat="server"/>--%>
                    </div>
                </div>
                <div class="col-md-7">
                    <asp:Label AssociatedControlID="txtApNombres" CssClass="col-md-2 control-label" runat="server" >Apellidos y Nombres:</asp:Label>
                    <div class="col-md-10">
                        <asp:TextBox ID="txtApNombres" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvApNom" Display="Dynamic" ForeColor="Yellow" ControlToValidate="txtApNombres" ErrorMessage="* Apellidos y Nombres necesario." ValidationGroup="G1" runat="server"/>
                    </div>
                </div>
                <div class="col-md-5">
                    <asp:Label AssociatedControlID="txtTelefono" CssClass="col-md-2 control-label" runat="server" >Teléfono:</asp:Label>
                    <div class="col-md-10">
                        <asp:TextBox ID="txtTelefono" CssClass="form-control" TextMode="Number" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-7">
                    <asp:Label AssociatedControlID="txtEmail" CssClass="col-md-2 control-label" runat="server">Email:</asp:Label>
                    <div class="col-md-10">
                        <asp:TextBox ID="txtEmail" CssClass="form-control" TextMode="Email" runat="server"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="form-group">
                <div class="col-md-3 col-xs-6">
                    <asp:Label CssClass="col-md-4 col-xs-12 control-label" AssociatedControlID="cmbCategCli"  runat="server">Categoría:</asp:Label>
                    <div class="col-md-8 col-xs-12">
                        <asp:DropDownList ID="cmbCategCli" Enabled="false" CssClass="form-control"  runat="server">
                            <asp:ListItem Text="A" Value="1"></asp:ListItem>
                            <asp:ListItem Text="B" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Empl" Value="5"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-3 col-xs-6">
                    <asp:Label runat="server" CssClass="col-md-6 col-xs-12 control-label" AssociatedControlID="txtDiasVisita">Días de Visita:</asp:Label>
                    <div class="col-md-6 col-xs-12">
                        <asp:TextBox runat="server" ID="txtDiasVisita" style="text-transform:uppercase" CssClass="form-control" Placeholder="LMXJVSD" />
                    </div>
                </div>
                <div class="col-md-6 col-xs-12">
                    <asp:Label runat="server" CssClass="col-md-2 col-xs-12 control-label" AssociatedControlID="txtDireccion">Dirección:</asp:Label>
                    <div class="col-md-10 col-xs-12">
                        <asp:TextBox runat="server" ID="txtDireccion" CssClass="form-control"/>
                        <asp:RequiredFieldValidator ID="rfvDirec" Display="Dynamic" ForeColor="Yellow" ControlToValidate="txtDireccion" ErrorMessage="* Dirección necesario." ValidationGroup="G1" runat="server"/>
                    </div>
                </div>
                <%--<div class="col-md-6 col-xs-6" style="padding-top:5px">
                    <asp:Label runat="server" CssClass="col-md-2 control-label" AssociatedControlID="cmbSector">Sector:</asp:Label>
                    <div class="col-md-8">
                        <asp:DropDownList ID="cmbSector" CssClass="form-control"  runat="server">
                            <asp:ListItem Text="Sector 1" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Sector 2" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Sector 3" Value="3"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>--%>
                <div class="col-md-6 col-xs-12"  style="padding-top:5px">
                    <asp:Label runat="server" CssClass="col-md-2 control-label" AssociatedControlID="cmbDistrito" >Distrito:</asp:Label>
                    <div class="col-md-8">
                        <asp:DropDownList ID="cmbDistrito" CssClass="form-control"  runat="server">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <section class="row">
            <div class="col-md-4 col-xs-10">
                 <asp:Label ID="Label5" runat="server" Text="Buscar Cliente: "></asp:Label>
                 <div class="input-group">
                     <asp:TextBox ID="txtbuscar" CssClass="form-control" runat="server" placeholder="Nombre de Cliente..."></asp:TextBox>
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
                 <asp:GridView ID="grdClientes" runat="server" OnSelectedIndexChanged="grdClientes_SelectedIndexChanged" DataKeyNames="ID" 
                     CssClass="gridview">                    
                     
                 </asp:GridView>
             </div>
       </div>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
    
    <br />

</asp:Content>
