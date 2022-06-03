<%@ Page Title="Pedidos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Pedidos.aspx.cs" Inherits="WebAppDELICIAS.ADM.Pedidos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <hr />
    <div class="row">
        <div class="col-md-12">
            <div class="form-horizontal">
                <div class="form-group">
                    <div class="col-md-12">
                        <%--Cliente--%>
                        <asp:Label ID="Label2" CssClass="col-md-12 col-xs-12" Text="Cliente:" AssociatedControlID="cmbCliente" runat="server"></asp:Label>
                        <div class="col-md-12 col-xs-12">
                            <asp:DropDownList CssClass="form-control" ForeColor="#453750" Font-Bold="true" ID="cmbCliente" runat="server" TabIndex="3">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <%--Fecha MAxima Entrega--%>
                        <asp:Label ID="Label1" CssClass="col-md-12 col-xs-12" Text="Fecha Entrega:" AssociatedControlID="txtFechaEntrega" runat="server"></asp:Label>
                        <div class="col-md-12 col-xs-12">
                            <asp:TextBox ID="txtFechaEntrega" CssClass="form-control" TextMode="Date" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>

            <div class="form-horizontal">
                <asp:UpdatePanel ID="UPMontos" runat="server">
                <ContentTemplate>
                <div class="form-group" style="text-align:center">
                    <div class="col-md-12 col-xs-12">
                        <%--Monto--%>
                        <asp:Label ID="Label7" CssClass="col-md-12 col-xs-12" Text="Monto Total (Referencial): S/. " Font-Size="Small" runat="server"></asp:Label>
                        <asp:Label ID="lblMonto" CssClass="col-md-12 col-xs-12" Text="0" ForeColor="#5cb85c" Font-Size="Large" Font-Bold="true" runat="server"></asp:Label>
                    </div>
                </div>
                
                <div class="form-group" style="text-align:center">
                    <%-- BOTON REGISTRAR VENTA --%>
                    <asp:Label ID="lblInfoRegistrar" CssClass="col-md-12" runat="server" ForeColor="Red"></asp:Label>
                    <div class="col-md-12">
                        <asp:Button ID="btnRegistrarPedido" Enabled="false" ValidationGroup="grupo03" CssClass="btn btn-success" Width="285px" Text="Registrar Pedido" OnClick="btnRegistrarPedido_Click"
                             runat="server"  />
                    </div>
                </div>
                </ContentTemplate>
                </asp:UpdatePanel>
            </div>


            <div class="form-horizontal" style="background-color:#FDCA40; padding:10px;border-radius:6px; color:#CF5C36;">
                    <div class="form-group">
                        <%--Producto--%>
                        <asp:Label ID="Label4" CssClass="control-label col-md-1" Text="Producto:" AssociatedControlID="cmbProducto" runat="server"></asp:Label>
                        <div class="col-md-4">
                            <asp:DropDownList CssClass="form-control" ID="cmbProducto" runat="server">
                            </asp:DropDownList>
                        </div>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                        <%--Cantidad--%>
                        <asp:Label ID="Label5" CssClass="control-label col-md-1 col-xs-12" Text="Cantidad:" AssociatedControlID="txtCantidad" runat="server"></asp:Label>
                        <div class="col-md-2 col-xs-12">
                            <asp:TextBox ID="txtCantidad" CssClass="form-control" TextMode="Number" min="1" runat="server" TabIndex="1" Placeholder="0"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvCanti" ValidationGroup="group1" Display="Dynamic" ForeColor="Red" ControlToValidate="txtCantidad" ErrorMessage="Ingrese Cantidad." runat="server"/>
                        </div>
                        </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div class="form-group">
                        <div class="col-md-offset-1 col-md-3 col-xs-8">
                            <asp:Button ID="btnAgregarP" ValidationGroup="group1" CssClass="btn btn-primary" Text="Agregar &raquo;" OnClick="btnAgregarP_Click" runat="server" TabIndex="2" />
                        </div>
                        <div class="col-md-8 col-xs-3">
                            <asp:Button ID="btnLimpiar" ValidationGroup="groupL" CssClass="btn btn-default" Text="Limpiar" OnClick="btnLimpiar_Click" runat="server"/>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-offset-1 col-md-12" style="overflow:auto;">
                            <asp:GridView ID="grdProductos" CssClass="gridview" runat="server" OnRowCreated="grdProductos_RowCreated" OnRowDeleting="grdProductos_RowDeleting" Caption="Detalle de Venta">                    
                                <Columns>
                                    <asp:CommandField ButtonType="Button" ControlStyle-CssClass="btn btn-xs" DeleteText="X" ControlStyle-BackColor="#453750" ControlStyle-ForeColor="White" ShowDeleteButton="true">
                                    <ControlStyle BackColor="#453750" CssClass="btn btn-xs" ForeColor="White" />
                                    </asp:CommandField>
                                    <asp:TemplateField HeaderText="Item"><ItemTemplate><%# Container.DataItemIndex + 1%> </ItemTemplate></asp:TemplateField>
                                </Columns>
                                <HeaderStyle Font-Bold="True" ForeColor="White" />
                            </asp:GridView>
                        </div>
                    </div>
                </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>


<script type="text/javascript">
        
    function ConfirmacionP(mnt, cnt) {
            return confirm("¿Esta seguro de registrar este Pedido? \n" +
            "Monto Referencial: S/. " + mnt + "\n" + 
            "Cantidad Productos: " + cnt);
    }
    
</script>
</asp:Content>
