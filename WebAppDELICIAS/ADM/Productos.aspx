<%@ Page Title="Productos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="WebAppDELICIAS.ADM.Productos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div>
        <div class="row">
            <div class="form-group">
                <div class="col-md-12 col-xs-12">
                    <asp:Label AssociatedControlID="lblProducto" ForeColor="#5cb85c" Font-Size="Large" CssClass="col-md-2 col-xs-4 control-label" runat="server">ID Producto:</asp:Label>
                    <asp:Label ID="lblProducto" CssClass="col-md-10 col-xs-8 control-label" ForeColor="#5cb85c" Font-Size="Large" Font-Bold="true" runat="server"></asp:Label>
                </div>
                <div class="col-md-6">
                    <asp:Label AssociatedControlID="txtNombreProd" CssClass="col-md-3 control-label" runat="server" >Nombre:</asp:Label>
                    <div class="col-md-9" style="padding-bottom:10px">
                        <asp:TextBox ID="txtNombreProd" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvNP" Display="Dynamic" ForeColor="Yellow" ControlToValidate="txtNombreProd" ErrorMessage="* Nombre necesario." ValidationGroup="G1" runat="server"/>
                    </div>
                </div>
                <div class="col-md-6">
                    <asp:Label AssociatedControlID="txtDescripcion" CssClass="col-md-3 control-label" runat="server" >Descripción:</asp:Label>
                    <div class="col-md-9" style="padding-bottom:10px">
                        <asp:TextBox ID="txtDescripcion" CssClass="form-control" TextMode="SingleLine" runat="server"></asp:TextBox>
                    </div>
                </div>
                
                <div class="col-md-3">
                    <asp:Label CssClass="col-md-4 control-label" AssociatedControlID="cmbCategProd"  runat="server">Categoría:</asp:Label>
                    <div class="col-md-8">
                        <asp:DropDownList ID="cmbCategProd" CssClass="form-control"  runat="server">
                            <asp:ListItem Text="Pastelería" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Panadería" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Cafetería" Value="3"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-3 col-xs-6">
                    <asp:Label CssClass="col-md-6 col-xs-12 control-label" AssociatedControlID="txtStockMinimo"  runat="server">Stock Mínimo:</asp:Label>
                    <div class="col-md-6 col-xs-12">
                        <asp:TextBox ID="txtStockMinimo" CssClass="form-control" TextMode="Number" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvSM" Display="Dynamic" ForeColor="Yellow" ControlToValidate="txtStockMinimo" ErrorMessage="* Campo necesario." ValidationGroup="G1" runat="server"/>
                    </div>
                </div>
            
                <div class="col-md-3 col-xs-6">
                    <asp:Label CssClass="col-md-4 col-xs-12 control-label" AssociatedControlID="txtPrecio"  runat="server">Precio:</asp:Label>
                    <div class="col-md-8 col-xs-12">
                        <asp:TextBox ID="txtPrecio" CssClass="form-control" TextMode="Number" step="0.1" min="0" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvPrecio" Display="Dynamic" ForeColor="Yellow" ControlToValidate="txtPrecio" ErrorMessage="* Precio necesario." ValidationGroup="G1" runat="server"/>
                    </div>
                </div>
                <div class="col-md-3"></div>
            </div>
        </div>
        
        <br />
        <section class="row">
            <div class="col-md-4 col-xs-10">
                 <asp:Label ID="Label5" runat="server" Text="Buscar Producto: "></asp:Label>
                 <div class="input-group">
                     <asp:TextBox ID="txtbuscar" CssClass="form-control" runat="server" placeholder="Nombre de Producto..."></asp:TextBox>
                     <span class="input-group-btn">
                         <asp:Button ID="btnbuscar" CssClass="btn btn-info" ValidationGroup="fff" runat="server" Text="Buscar" OnClick="btnbuscar_Click"/>
                     </span>
                 </div>
           </div>
            <div class="col-md-6 col-xs-12">
                <div class="row" style="margin:0px; padding:0px; height:20px"><asp:Label ForeColor="#99ccff" runat="server" ID="lblinfo1"></asp:Label></div>
                <div class="btn-group botones" role="group" style="margin:0px;">
                    <asp:Button ID="btnnuevo" cssclass="btn btn-primary" OnClick="btnnuevo_Click" runat="server" Text="Nuevo" ToolTip="Ingresar un nuevo Producto." />
                    <asp:Button ID="btnguardar" cssclass="btn btn-success" OnClick="btnguardar_Click" runat="server" Text="Guardar" Enabled="False" ValidationGroup="G1" ToolTip="Guardar datos del Producto preparado."/>
                    <asp:Button ID="btnmodificar" cssclass="btn btn-warning" OnClick="btnmodificar_Click" runat="server" Text="Modificar Datos"  ValidationGroup="G1" ToolTip="Permite editar los datos del Producto seleccionado. Luego de Editar los datos debe pulsar en Guardar para registrar los cambios."/>
                </div>
            </div>
        </section>
        <br />
        <div class="row">
             <div class="col-md-12" style="overflow:auto; max-height:400px; overflow-y:auto" >
                 <asp:Label ID="lbloperacion" runat="server" Visible="False"></asp:Label>
                 <asp:GridView ID="grdProductos" runat="server" OnSelectedIndexChanged="grdClientes_SelectedIndexChanged" DataKeyNames="ID" 
                     CssClass="gridview">                    
                     
                 </asp:GridView>
             </div>
       </div>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
    
    <br />

</asp:Content>
