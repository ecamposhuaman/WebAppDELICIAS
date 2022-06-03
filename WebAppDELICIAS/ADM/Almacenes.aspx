<%@ Page Title="Almacén" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Almacenes.aspx.cs" Inherits="WebAppDELICIAS.Almacenes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div>
        <div class="row">
            <div class="form-group">
                <div class="col-md-12 col-xs-12">
                    <asp:Label AssociatedControlID="lblIdAlmacen" ForeColor="#5cb85c" Font-Size="Large" CssClass="col-md-2 col-xs-4 control-label" runat="server">ID Almacén:</asp:Label>
                    <asp:Label ID="lblIdAlmacen" CssClass="col-md-10 col-xs-8 control-label" ForeColor="#5cb85c" Font-Size="Large" Font-Bold="true" runat="server"></asp:Label>
                </div>
                <div class="col-md-6">
                    <asp:Label AssociatedControlID="txtNombreAlm" CssClass="col-md-3 control-label" runat="server" >Nombre:</asp:Label>
                    <div class="col-md-9" style="padding-bottom:10px">
                        <asp:TextBox ID="txtNombreAlm" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvNP" Display="Dynamic" ForeColor="Yellow" ControlToValidate="txtNombreAlm" ErrorMessage="* Nombre necesario." ValidationGroup="G1" runat="server"/>
                    </div>
                </div>
                <div class="col-md-6">
                    <asp:Label AssociatedControlID="cmbTipo" CssClass="col-md-3 control-label" runat="server" >Tipo:</asp:Label>
                    <div class="col-md-9" style="padding-bottom:10px">
                        <asp:DropDownList ID="cmbTipo" CssClass="form-control"  runat="server">
                            <asp:ListItem Text="Fijo" Value="Fijo"></asp:ListItem>
                            <asp:ListItem Text="Móvil" Value="Móvil"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-12">
                    <asp:Label CssClass="col-md-4 control-label" AssociatedControlID="cmbResponsable"  runat="server">Responsable:</asp:Label>
                    <div class="col-md-8">
                        <asp:DropDownList ID="cmbResponsable" CssClass="form-control"  runat="server">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-3"></div>
            </div>
        </div>
        
        <br />
        <section class="row">
            <div class="col-md-4 col-xs-10">
                 <asp:Label ID="Label5" runat="server" Text="Buscar Almacén: "></asp:Label>
                 <div class="input-group">
                     <asp:TextBox ID="txtbuscar" CssClass="form-control" runat="server" placeholder="Nombre de Almacén..."></asp:TextBox>
                     <span class="input-group-btn">
                         <asp:Button ID="btnbuscar" CssClass="btn btn-info" ValidationGroup="fff" runat="server" Text="Buscar" OnClick="btnbuscar_Click"/>
                     </span>
                 </div>
           </div>
            <div class="col-md-6 col-xs-12">
                <div class="row" style="margin:0px; padding:0px; height:20px"><asp:Label ForeColor="#99ccff" runat="server" ID="lblinfo1"></asp:Label></div>
                <div class="btn-group botones" role="group" style="margin:0px;">
                    <asp:Button ID="btnnuevo" cssclass="btn btn-primary" OnClick="btnnuevo_Click" runat="server" Text="Nuevo" ToolTip="Ingresar un nuevo Almacén." />
                    <asp:Button ID="btnguardar" cssclass="btn btn-success" OnClick="btnguardar_Click" runat="server" Text="Guardar" Enabled="False" ValidationGroup="G1" ToolTip="Guardar datos del Almacén preparado."/>
                    <asp:Button ID="btnmodificar" cssclass="btn btn-warning" OnClick="btnmodificar_Click" runat="server" Text="Modificar Datos"  ValidationGroup="G1" ToolTip="Permite editar los datos del Almacén seleccionado. Luego de Editar los datos debe pulsar en Guardar para registrar los cambios."/>
                </div>
            </div>
        </section>
        <br />
        <div class="row">
             <div class="col-md-12" style="overflow:auto; max-height:400px; overflow-y:auto" >
                 <asp:Label ID="lbloperacion" runat="server" Visible="False"></asp:Label>
                 <asp:GridView ID="grdAlmacenes" runat="server" OnSelectedIndexChanged="grdClientes_SelectedIndexChanged" DataKeyNames="ID" 
                     CssClass="gridview">
                 </asp:GridView>
             </div>
       </div>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
