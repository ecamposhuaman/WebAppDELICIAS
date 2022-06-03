<%@ Page Title="Reportes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Transacciones.aspx.cs" Inherits="WebAppDELICIAS.ManttoTransacciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <hr />
    <div class="row">
        <div class="col-md-12">

            <div class="row" style="padding-bottom:20px;">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                <div class="col-md-3">
                    <asp:Label ID="Label8" CssClass="col-md-12" Text="Tipo de Transacción:" runat="server"></asp:Label>
                    <div class="col-md-12">
                        <%--Tipo de Transacción--%>
                        <asp:DropDownList CssClass="form-control" ID="cmbVentaFlujo" OnSelectedIndexChanged="cmbVentaFlujo_SelectedIndexChanged" runat="server" AutoPostBack="true">
                            <asp:ListItem Text="Ventas" Value="V"></asp:ListItem>
                            <asp:ListItem Text="Movimiento de Productos" Value="F"></asp:ListItem>
                            <asp:ListItem Text="Pedidos" Value="P"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="Label1" CssClass="col-md-12" Text="Locación:" runat="server"></asp:Label>
                    <div class="col-md-12">
                        <%--Tipo de Almacén--%>
                        <asp:DropDownList CssClass="form-control" ForeColor="#453750" ID="cmbAlmacen" runat="server">
                        </asp:DropDownList>
                    </div>
                </div>
                
                <div class="col-md-3">
                    <asp:Label ID="Label2" CssClass="col-md-12" Text="Fecha:" runat="server"></asp:Label>
                    <div class="col-md-12">
                        <%--Buscar en--%>
                        <asp:TextBox ID="txtFecha" CssClass="form-control" TextMode="Date" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvFecha" ValidationGroup="GBuscar" Display="Dynamic" ForeColor="Red" ControlToValidate="txtFecha" ErrorMessage="Elija Fecha." runat="server"/>
                    </div>
                </div>
                
                <div class="col-md-3">
                    <div class="col-md-12" style="padding-top:20px">
                        <asp:Button ID="btnBuscar" CssClass="btn btn-primary" ValidationGroup="GBuscar" Text="Buscar" OnClick="btnBuscar_Click" runat="server"/>
                    </div>
                </div>
                </ContentTemplate>
                </asp:UpdatePanel>
            </div>
                
            <div class="row">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                <div class="form-group">
                    <div class="col-md-6">
                        <div class="col-md-12 col-xs-12">
                            <div class="col-md-12 col-xs-12">
                                <asp:Label ID="lblInfoTransacciones" runat="server" ForeColor="#e89319"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-12" style="overflow:auto; max-height:400px; overflow-y:auto" > <%--GRIDVIEW--%>
                            <asp:GridView ID="grdTransacciones" CssClass="gridviewBusqueda" ViewStateMode="Enabled" DataKeyNames="Id" runat="server" OnSelectedIndexChanged="grdTransacciones_SelectedIndexChanged" OnRowCreated="grdTransacciones_RowCreated">                    
                                <SelectedRowStyle BackColor="Silver" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-md-6">
                        <div class="col-md-12 col-xs-12">
                            <div class="col-md-9 col-xs-9">
                                <asp:Label ID="lblInfoDetalle" Font-Size="smaller" runat="server" ForeColor="#cccccc"></asp:Label>
                            </div>
                            <div class="col-md-3 col-xs-3" style="padding:15px; text-align:center">
                                <asp:Button ID="btnAnular" Visible="false" CssClass="btn btn-danger pull-left" Text="Anular" OnClick="btnAnular_Click" runat="server"/><br /><br />
                                <asp:Button ID="btnValidarF" Visible="false" CssClass="btn btn-success pull-left" Text="Validar" OnClick="btnValidarF_Click" runat="server"/>
                            </div>
                        </div>
                        <div class="col-md-12" style="overflow:auto;">
                            <asp:GridView ID="grdDetalleTransac" CssClass="gridview" runat="server">                    
                                <Columns>
                                    <asp:TemplateField HeaderText="Item"><ItemTemplate><%# Container.DataItemIndex + 1%> </ItemTemplate></asp:TemplateField>
                                </Columns>
                                <HeaderStyle Font-Bold="True" ForeColor="White" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            
        </div>
    </div>
<%--<script type="text/javascript">
      var xPos, yPos;
      var prm = Sys.WebForms.PageRequestManager.getInstance();
      prm.add_beginRequest(BeginRequestHandler);
      prm.add_endRequest(EndRequestHandler);
      function BeginRequestHandler(sender, args) {
//        xPos = $get('Objeto').scrollLeft;
        yPos = $get('Objeto').scrollTop;
      }
      function EndRequestHandler(sender, args) {
//        $get('Objeto').scrollLeft = xPos;
        $get('Objeto').scrollTop = yPos;
      }
    </script>--%>
</asp:Content>
