<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebAppDELICIAS._Default" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Home Panel</h1>
    <div class="row" style="padding:5px;">
        <asp:Panel ID="PanelData" Visible="false" runat="server">
            <div class="col-md-4 col-xs-6" style="padding:0">  
                <div class="col-md-6 col-xs-12" style="padding:0;">
                    <asp:Label ID="Label4" ForeColor="#B88A18" CssClass="col-md-12 col-xs-12" runat="server" Text="VENTAS (del día)" Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblVentas" ForeColor="#EDD772" CssClass="col-md-12 col-xs-12 col-md-offset-1 col-xs-offset-1" runat="server" Font-Size="Small"></asp:Label>
                </div>
                <div class="col-md-6 col-xs-12" style="padding:0;">
                    <asp:Label ID="Label6" ForeColor="#B88A18" CssClass="col-md-12 col-xs-12" runat="server" Text="PEDIDOS" Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblPedidos" ForeColor="#EDD772" CssClass="col-md-12 col-xs-12 col-md-offset-1 col-xs-offset-1" runat="server" Font-Size="Small"></asp:Label>
                </div>
            </div>
            <div class="col-md-8 col-xs-6" style="padding:0">
                <div class="col-md-3 col-xs-12" style="padding:0;">
                    <asp:Label ID="Label10" ForeColor="#B88A18" CssClass="col-md-12 col-xs-12" runat="server" Text="MOVIMIENTOS" Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblMovimientos" ForeColor="#EDD772" CssClass="col-md-12 col-xs-12 col-md-offset-1 col-xs-offset-1" runat="server" Font-Size="Small"></asp:Label>
                </div>
                <div class="col-md-3 col-xs-12" style="padding:0;">
                    <asp:Label ID="Label8" ForeColor="#B88A18" CssClass="col-md-12 col-xs-12" runat="server" Text="DEUDAS" Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblDeudas" ForeColor="#EDD772" CssClass="col-md-12 col-xs-12 col-md-offset-1 col-xs-offset-1" runat="server" Font-Size="Small"></asp:Label>
                </div>
                <div class="col-md-3 col-xs-12" style="padding:0;">
                    <asp:Label ID="Label14" ForeColor="#B88A18" CssClass="col-md-12 col-xs-12" runat="server" Text="CLIENTES" Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblClientes" ForeColor="#EDD772" CssClass="col-md-12 col-xs-12 col-md-offset-1 col-xs-offset-1" runat="server" Font-Size="Small"></asp:Label>
                </div>
                <div class="col-md-3 col-xs-12" style="padding:0;">
                    <asp:Label ID="Label18" ForeColor="#B88A18" CssClass="col-md-12 col-xs-12" runat="server" Text="PRODUCTOS" Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblProductos" ForeColor="#EDD772" CssClass="col-md-12 col-xs-12 col-md-offset-1 col-xs-offset-1" runat="server" Font-Size="Small"></asp:Label>
                </div>
            </div>
        </asp:Panel>
    </div>
    <div class="row" style="padding:5px; padding-bottom:0" >
        <div class="row col-md-12" style="padding-bottom:5px">
            <%--<div class="col-md-3 col-xs-6" style="padding:2px">
                <div class="col-md-12 col-xs-12 btn-success" style="padding:0; border-radius:5px">
                    <asp:LinkButton ID="linkVentas" OnClick="linkVentas_Click" runat="server">
                        <div class="col-md-12 col-xs-12" style="padding:0; color:burlywood;">
                            <div class="col-md-8 col-xs-8" style="padding:0; margin-top:45px;">
                                <asp:Label ID="Label3" CssClass="input-lg" runat="server" Text="VENTAS"></asp:Label>
                            </div>
                            <div class="col-md-4 col-xs-4" style="padding:0;">
                                <asp:Image ID="Image2" runat="server" ImageUrl="imgs/Ventas.png" CssClass="img-responsive"/>
                            </div>
                        </div>
                        <asp:Label ID="Label30" CssClass="col-md-12 col-xs-12" Text="Registrar <br/> Nueva Venta." runat="server"></asp:Label>
                    </asp:LinkButton>
                </div>
            </div>--%>
            <div class="col-md-3 col-xs-6" style="padding:2px">
                <div class="col-md-12 col-xs-12 btn-warning" style="padding:0; border-radius:7px;">
                    <asp:LinkButton ID="linkVentas" OnClick="linkVentas_Click" runat="server">
                        <div class="col-md-12 col-xs-12" style="padding:0; position:relative;">
                            <asp:Image ID="Image12" runat="server" ImageUrl="imgs/Ventas.png" CssClass="img-responsive"/>
                        </div>
                        <div class="col-md-12 col-xs-12" style="padding:0; position:absolute;">
                            <div class="col-md-12 col-xs-12" style="padding:0; margin-top:15px; margin-bottom:5px">
                                <asp:Label ID="Label3" ForeColor="#cc3300" CssClass="input-lg" runat="server" Text="VENTAS"></asp:Label>
                            </div>
                            <asp:Label ID="Label30" ForeColor="#cc3300" CssClass="col-md-12 col-xs-12" Text="Registrar <p style='line-height:8px'> Nueva Venta. </p>" runat="server"></asp:Label>
                        </div>
                    </asp:LinkButton>
                </div>
            </div>
            <div class="col-md-3 col-xs-6" style="padding:2px">
                <div class="col-md-12 col-xs-12 btn-success" style="padding:0; border-radius:7px;">
                    <asp:LinkButton ID="linkMovimientos" OnClick="linkMovimientos_Click" runat="server">
                        <div class="col-md-12 col-xs-12" style="padding:0; position:relative;">
                            <asp:Image ID="Image1" runat="server" ImageUrl="imgs/Movim.png" CssClass="img-responsive"/>
                        </div>
                        <div class="col-md-12 col-xs-12" style="padding:0; color:burlywood; position:absolute;">
                            <div class="col-md-12 col-xs-12" style="padding:0; margin-top:15px; margin-bottom:5px">
                                <asp:Label ID="Label12" ForeColor="#3518B8" CssClass="input-lg" runat="server" Text="MOVIMIENTOS"></asp:Label>
                            </div>
                            <asp:Label ID="Label31" ForeColor="#3518B8" CssClass="col-md-12 col-xs-12" Text="Registrar Nuevo <p style='line-height:8px'> Movimiento de Productos. </p>" runat="server"></asp:Label>
                        </div>
                    </asp:LinkButton>
                </div>
            </div>
            <div class="col-md-3 col-xs-6" style="padding:2px">
                <div class="col-md-12 col-xs-12 btn-primary" style="padding:0; border-radius:7px;">
                    <asp:LinkButton ID="linkPedidos" OnClick="linkPedidos_Click" runat="server">
                        <div class="col-md-12 col-xs-12" style="padding:0; position:relative;">
                            <asp:Image ID="Image3" runat="server" ImageUrl="imgs/Pedidos.png" CssClass="img-responsive"/>
                        </div>
                        <div class="col-md-12 col-xs-12" style="padding:0; color:burlywood; position:absolute;">
                            <div class="col-md-12 col-xs-12" style="padding:0; margin-top:15px; margin-bottom:5px">
                                <asp:Label ID="Label5" ForeColor="White" CssClass="input-lg" runat="server" Text="PEDIDOS"></asp:Label>
                            </div>
                            <asp:Label ID="Label21" ForeColor="White" CssClass="col-md-12 col-xs-12" Text="Registrar <p style='line-height:8px'> Nuevo Pedido. </p>" runat="server"></asp:Label>
                        </div>
                    </asp:LinkButton>
                </div>
            </div>
            <div class="col-md-3 col-xs-6" style="padding:3px">
                <div class="col-md-12 col-xs-12 btn-info" style="padding:0; border-radius:7px">
                    <asp:LinkButton ID="linkReportes" OnClick="linkReportes_Click" runat="server">
                        <div class="col-md-12 col-xs-12" style="padding:0; position:relative;">
                            <asp:Image ID="Image2" runat="server" ImageUrl="imgs/Report.png" CssClass="img-responsive"/>
                        </div>
                        <div class="col-md-12 col-xs-12" style="padding:0; color:burlywood; position:absolute;">
                            <div class="col-md-12 col-xs-12" style="padding:0; margin-top:15px; margin-bottom:5px">
                                <asp:Label ID="Label7" ForeColor="#7D9399" CssClass="input-lg" runat="server" Text="REPORTES"></asp:Label>
                            </div>
                            <asp:Label ID="Label22" ForeColor="#7D9399" CssClass="col-md-12 col-xs-12" Text="Registro de Ventas, <p style='line-height:8px'> Movimientos y Pedidos. </p>" runat="server"></asp:Label>
                        </div>
                    </asp:LinkButton>
                </div>
            </div>       
        </div>


        <div class="row col-md-12" style="padding-bottom:5px">
            <div class="col-md-3 col-xs-6" style="padding:2px">
                <div class="col-md-12 col-xs-12 btn-danger" style="padding:0; border-radius:7px">
                    <asp:LinkButton ID="linkDeudas" OnClick="linkDeudas_Click" runat="server">
                        <div class="col-md-12 col-xs-12" style="padding:0; position:relative;">
                            <asp:Image ID="Image4" runat="server" ImageUrl="imgs/Deuda.png" CssClass="img-responsive"/>
                        </div>
                        <div class="col-md-12 col-xs-12" style="padding:0; color:burlywood; position:absolute;">
                            <div class="col-md-12 col-xs-12" style="padding:0; margin-top:15px; margin-bottom:5px">
                                <asp:Label ID="Label13" ForeColor="#ffcc66" CssClass="input-lg" runat="server" Text="DEUDAS"></asp:Label>
                            </div>
                            <asp:Label ID="Label25" ForeColor="#ffcc66" CssClass="col-md-12 col-xs-12" Text="Revisión y Pago <p style='line-height:8px'> de Deudas por Cliente. </p>" runat="server"></asp:Label>
                        </div>
                    </asp:LinkButton>
                </div>
            </div>
            <div class="col-md-3 col-xs-6" style="padding:2px">
                <div class="col-md-12 col-xs-12 btn-success" style="padding:0; border-radius:7px">
                    <asp:LinkButton ID="linkInventario" OnClick="linkInventario_Click" runat="server">
                        <div class="col-md-12 col-xs-12" style="padding:0; position:relative;">
                            <asp:Image ID="Image13" runat="server" ImageUrl="imgs/Kardex.png" CssClass="img-responsive"/>
                        </div>
                        <div class="col-md-12 col-xs-12" style="padding:0; color:burlywood; position:absolute;">
                            <div class="col-md-12 col-xs-12" style="padding:0; margin-top:15px; margin-bottom:5px">
                                <asp:Label ID="Label11" ForeColor="#B4EB36" CssClass="input-lg" runat="server" Text="KARDEX"></asp:Label>
                            </div>
                            <asp:Label ID="Label24" ForeColor="#B4EB36" CssClass="col-md-12 col-xs-12" Text="Revisión de <p style='line-height:8px'> Kardex (Inventariado). </p>" runat="server"></asp:Label>
                        </div>
                    </asp:LinkButton>
                </div>
            </div>
            <div class="col-md-3 col-xs-6" style="padding:2px">
                <div class="col-md-12 col-xs-12 btn-primary" style="padding:0; border-radius:7px">
                    <asp:LinkButton ID="linkCierreTurno" OnClick="linkCierreTurno_Click" runat="server">
                        <div class="col-md-12 col-xs-12" style="padding:0; position:relative;">
                            <asp:Image ID="Image14" runat="server" ImageUrl="imgs/CierreT.png" CssClass="img-responsive"/>
                        </div>
                        <div class="col-md-12 col-xs-12" style="padding:0; color:burlywood; position:absolute;">
                            <div class="col-md-12 col-xs-12" style="padding:0; margin-top:15px; margin-bottom:5px">
                                <asp:Label ID="Label2" ForeColor="#36EBDF" CssClass="input-lg" runat="server" Text="CERRAR TURNO"></asp:Label>
                            </div>
                            <asp:Label ID="Label29" ForeColor="#36EBDF" CssClass="col-md-12 col-xs-12" Text="Gestión de <p style='line-height:8px'> Cierres de Turno. </p>" runat="server"></asp:Label>
                        </div>
                    </asp:LinkButton>
                </div>
            </div>
            <div class="col-md-3 col-xs-6" style="padding:2px">
                <div class="col-md-12 col-xs-12 btn-default" style="padding:0; border-radius:7px">
                    <asp:LinkButton ID="linkClientes" OnClick="linkClientes_Click" runat="server">
                        <div class="col-md-12 col-xs-12" style="padding:0; position:relative;">
                            <asp:Image ID="Image6" runat="server" ImageUrl="imgs/Clientes.png" CssClass="img-responsive"/>
                        </div>
                        <div class="col-md-12 col-xs-12" style="padding:0; position:absolute;">
                            <div class="col-md-12 col-xs-12" style="padding:0; margin-top:15px; margin-bottom:5px">
                                <asp:Label ID="Label9" ForeColor="#EB36EB" CssClass="input-lg" runat="server" Text="CLIENTES"></asp:Label>
                            </div>
                            <asp:Label ID="Label23" ForeColor="#EB36EB" CssClass="col-md-12 col-xs-12" Text="Mantenimiento <p style='line-height:8px'> de Clientes. </p>" runat="server"></asp:Label>
                        </div>
                    </asp:LinkButton>
                </div>
            </div>
        </div>


        <div class="row col-md-12">
            <div class="col-md-3 col-xs-6" style="padding:2px">
                <div class="col-md-12 col-xs-12 btn-default" style="padding:0; border-radius:7px">
                    <asp:LinkButton ID="linkProductos" OnClick="linkProductos_Click" runat="server">
                        <div class="col-md-12 col-xs-12" style="padding:0; position:relative;">
                            <asp:Image ID="Image7" runat="server" ImageUrl="imgs/Productos.png" CssClass="img-responsive"/>
                        </div>
                        <div class="col-md-12 col-xs-12" style="padding:0; position:absolute;">
                            <div class="col-md-12 col-xs-12" style="padding:0; margin-top:15px; margin-bottom:5px">
                                <asp:Label ID="Label15" ForeColor="#855454" CssClass="input-lg" runat="server" Text="PRODUCTOS"></asp:Label>
                            </div>
                            <asp:Label ID="Label26" ForeColor="#855454" CssClass="col-md-12 col-xs-12" Text="Mantenimiento <p style='line-height:8px'> de Productos. </p>" runat="server"></asp:Label>
                        </div>
                    </asp:LinkButton>
                </div>
            </div>    
            <div class="col-md-3 col-xs-6" style="padding:2px">
                <div class="col-md-12 col-xs-12 btn-default" style="padding:0; border-radius:7px">
                    <asp:LinkButton ID="linkEmpleados" OnClick="linkEmpleados_Click" runat="server">
                        <div class="col-md-12 col-xs-12" style="padding:0; position:relative;">
                            <asp:Image ID="Image11" runat="server" ImageUrl="imgs/Empleados.png" CssClass="img-responsive"/>
                        </div>
                        <div class="col-md-12 col-xs-12" style="padding:0; position:absolute;">
                            <div class="col-md-12 col-xs-12" style="padding:0; margin-top:15px; margin-bottom:5px">
                                <asp:Label ID="Label17" ForeColor="#EB36EB" CssClass="input-lg" runat="server" Text="EMPLEADOS"></asp:Label>
                            </div>
                            <asp:Label ID="Label27" ForeColor="#EB36EB" CssClass="col-md-12 col-xs-12" Text="Mantenimiento <p style='line-height:8px'> de Empleados. </p>" runat="server"></asp:Label>
                        </div>
                    </asp:LinkButton>
                </div>
            </div>
            <div class="col-md-3 col-xs-6" style="padding:2px">  
                <div class="col-md-12 col-xs-12 btn-default" style="padding:0; border-radius:7px">
                    <asp:LinkButton ID="linkAlmacenes" OnClick="linkAlmacenes_Click" runat="server">
                        <div class="col-md-12 col-xs-12" style="padding:0; position:relative;">
                            <asp:Image ID="Image15" runat="server" ImageUrl="imgs/Almacenes.png" CssClass="img-responsive"/>
                        </div>
                        <div class="col-md-12 col-xs-12" style="padding:0; position:absolute;">
                            <div class="col-md-12 col-xs-12" style="padding:0; margin-top:15px; margin-bottom:5px">
                                <asp:Label ID="Label19" ForeColor="#B8B518" CssClass="input-lg" runat="server" Text="ALMACENES"></asp:Label>
                            </div>
                            <asp:Label ID="Label28" ForeColor="#B8B518" CssClass="col-md-12 col-xs-12" Text="Mantenimiento <p style='line-height:8px'> de Almacenes." runat="server"></asp:Label>
                        </div>
                    </asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
