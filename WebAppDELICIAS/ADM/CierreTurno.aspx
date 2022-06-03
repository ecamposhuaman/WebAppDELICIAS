<%@ Page Title="Cierre_Turno" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CierreTurno.aspx.cs" Inherits="WebAppDELICIAS.CierreTurno" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Cierre de Turno.</h2>
    <hr />
    <div class="row" style="padding-bottom:10px">
        <div class="col-md-12">
            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
            <ContentTemplate>
            <div class="col-md-5">
                <asp:Label ID="Label1" AssociatedControlID="cmbAlmacen" CssClass="col-md-2" Text="Almacén:" runat="server"></asp:Label>
                <div class="col-md-10">
                    <%--Tipo de Almacén--%>
                    <asp:DropDownList CssClass="form-control" ID="cmbAlmacen" runat="server" OnSelectedIndexChanged="cmbAlmacen_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                </div>
            </div>
            
            <div class="col-md-4" style="padding-bottom:10px">
                <asp:Label ID="Label2" AssociatedControlID="txtFecha" CssClass="col-md-2" Text="Fecha:" runat="server"></asp:Label>
                <%--FECHA--%>
                <div class="col-md-10">
                    <asp:TextBox ID="txtFecha" CssClass="form-control" TextMode="Date" runat="server" OnTextChanged="txtFecha_TextChanged" AutoPostBack="true"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvfecha" ValidationGroup="grp01" Display="Dynamic" ForeColor="Red" ControlToValidate="txtFecha" ErrorMessage="Elija una Fecha." runat="server"/>
                </div>
            </div>
            
            <div class="col-md-3">
                <div class="col-md-6 col-xs-6" style="text-align:center">
                    <asp:Button ID="btnBuscar" CssClass="btn btn-info" Text="Mostrar" ValidationGroup="grp01" OnClick="btnBuscar_Click" runat="server"/>
                </div>
                <div class="col-md-6 col-xs-6" style="text-align:center">
                    <asp:Button ID="btnGuardar" Enabled="false" CssClass="btn btn-success" Text="Guardar" ValidationGroup="grp01" OnClick="btnGuardar_Click" runat="server"/>
                </div>
            </div>
            </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12" style="color:burlywood;">
            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
            <div class="col-md-12">
                <asp:Label ID="lblInforme" ForeColor="#cc66ff" runat="server" Font-Italic="true"></asp:Label>
            </div>
            <div class="col-md-12">
                <asp:Label ID="lblSaldo" ForeColor="#00cc66" runat="server" Font-Bold="true"></asp:Label>
            </div>
            <div class="col-md-6" style="padding:0">
                <asp:Label ID="Label3" CssClass="col-md-6 col-xs-8" Text="MOVIMIENTO DE PRODUCTOS" Font-Bold="true" runat="server"></asp:Label>
                <asp:Label ID="lblMOVPROD" CssClass="col-md-6 col-xs-4" Text="S/. 0.00" Font-Bold="true" runat="server"></asp:Label>
                <div class="col-md-12 col-xs-12">
                    <asp:Label ID="Label5" CssClass="col-md-6 col-xs-7" Text="+ Valor Total de Entradas: " runat="server"></asp:Label>
                    <asp:Label ID="lblVTENT" CssClass="col-md-3 col-xs-4" Text="S/. 0.00" runat="server"></asp:Label>
                    <div class="col-md-3 col-xs-1" style="padding:0">
                        <asp:Button ID="btnVerEntradas" Enabled="false" CssClass="btn btn-success btn-xs pull-left" Text="Ver" ValidationGroup="ggg" OnClick="btnVerEntradas_Click" runat="server"/>
                    </div>
                </div>
                <div class="col-md-12 col-xs-12">
                    <asp:Label ID="Label7" CssClass="col-md-6 col-xs-7" Text="- Valor Total de Salidas: " runat="server"></asp:Label>
                    <asp:Label ID="lblVTSAL" CssClass="col-md-3 col-xs-4" Text="S/. 0.00" runat="server"></asp:Label>
                    <div class="col-md-3 col-xs-1" style="padding:0">
                        <asp:Button ID="btnVerSalidas" Enabled="false" CssClass="btn btn-success btn-xs pull-left" Text="Ver" ValidationGroup="ggg" OnClick="btnVerSalidas_Click" runat="server"/>
                    </div>
                </div>
                <div class="col-md-12 col-xs-12">
                    <asp:Label ID="Label9" CssClass="col-md-6 col-xs-7" Text="- Valor Stock Final: " runat="server"></asp:Label>
                    <asp:Label ID="lblSTKACT" CssClass="col-md-6 col-xs-5" Text="S/. 0.00" runat="server"></asp:Label>
                </div>
                <asp:GridView ID="grdMovimientos" CssClass="gridview" runat="server" EmptyDataText="Ningún resultado encontrado" ViewStateMode="Enabled" >                    
                </asp:GridView>
            </div>
            

            <div class="col-md-6" style="padding:0">
                <asp:Label ID="Label4" CssClass="col-md-6 col-xs-8" Text="VENTAS" Font-Bold="true" runat="server"></asp:Label>
                <asp:Label ID="lblVENTAS" CssClass="col-md-6 col-xs-4" Text="S/. 0.00" Font-Bold="true" runat="server"></asp:Label>
                <div class="col-md-12">
                    <asp:Label ID="Label13" CssClass="col-md-6 col-xs-7" Text="+ Valor en Deudas: " runat="server"></asp:Label>
                    <asp:Label ID="lblVDeudas" CssClass="col-md-6 col-xs-5" Text="S/. 0.00" runat="server"></asp:Label>
                </div>
                <div class="col-md-12">
                    <asp:Label ID="Label15" CssClass="col-md-6 col-xs-7" Text="+ Valor en Descuentos: " runat="server"></asp:Label>
                    <asp:Label ID="lblVDescuentos" CssClass="col-md-6 col-xs-5" Text="S/. 0.00" runat="server"></asp:Label>
                </div>
                <div class="col-md-12">
                    <asp:Label ID="Label17" CssClass="col-md-6 col-xs-7" Text="+ Valor en Bonificaciones: " runat="server"></asp:Label>
                    <asp:Label ID="lblVBonif" CssClass="col-md-6 col-xs-5" Text="S/. 0.00" runat="server"></asp:Label>
                </div>
                <div class="col-md-12" style="padding:0;">
                    <div class="col-md-6 col-xs-6" style="padding:0">
                        <asp:Label ID="Label11" CssClass="col-md-6 col-xs-12" Text="+ Efectivo: " runat="server"></asp:Label>
                        <asp:Label ID="lblTotalEfectivo" CssClass="col-md-6 col-xs-12" Text="S/. 0,00" runat="server"></asp:Label>
                        <div class="col-md-12 col-xs-12" style="color:lightgray">
                            <asp:Label ID="Label21" CssClass="col-xs-7" Text="S/. 200" runat="server"></asp:Label>
                            <div class="col-xs-5" style="padding:0">
                                <asp:TextBox ID="txt200" CssClass="form-control input-sm" TextMode="Number" Text="0" step="1" min="0" runat="server" OnTextChanged="txt200_TextChanged" AutoPostBack="true"></asp:TextBox>
                            </div>
                            <asp:Label ID="Label22" CssClass="col-xs-7" Text="S/. 100" runat="server"></asp:Label>
                            <div class="col-xs-5" style="padding:0">
                                <asp:TextBox ID="txt100" CssClass="form-control input-sm" TextMode="Number" Text="0" step="1" min="0" runat="server" OnTextChanged="txt100_TextChanged" AutoPostBack="true"></asp:TextBox>
                            </div>
                            <asp:Label ID="Label23" CssClass="col-xs-7" Text="S/. 50" runat="server"></asp:Label>
                            <div class="col-xs-5" style="padding:0">
                                <asp:TextBox ID="txt50" CssClass="form-control input-sm" TextMode="Number" Text="0" step="1" min="0" runat="server" OnTextChanged="txt50_TextChanged" AutoPostBack="true"></asp:TextBox>
                            </div>
                            <asp:Label ID="Label24" CssClass="col-xs-7" Text="S/. 20" runat="server"></asp:Label>
                            <div class="col-xs-5" style="padding:0">
                                <asp:TextBox ID="txt20" CssClass="form-control input-sm" TextMode="Number" Text="0" step="1" min="0" runat="server" OnTextChanged="txt20_TextChanged" AutoPostBack="true"></asp:TextBox>
                            </div>
                            <asp:Label ID="Label25" CssClass="col-xs-7" Text="S/. 10" runat="server"></asp:Label>
                            <div class="col-xs-5" style="padding:0">
                                <asp:TextBox ID="txt10" CssClass="form-control input-sm" TextMode="Number" Text="0" step="1" min="0" runat="server" OnTextChanged="txt10_TextChanged" AutoPostBack="true"></asp:TextBox>
                            </div>
                            <asp:Label ID="Label26" CssClass="col-xs-7" Text="S/. 5" runat="server"></asp:Label>
                            <div class="col-xs-5" style="padding:0">
                                <asp:TextBox ID="txt5" CssClass="form-control input-sm" TextMode="Number" Text="0" step="1" min="0" runat="server" OnTextChanged="txt5_TextChanged" AutoPostBack="true"></asp:TextBox>
                            </div>
                            <asp:Label ID="Label27" CssClass="col-xs-7" Text="S/. 2" runat="server"></asp:Label>
                            <div class="col-xs-5" style="padding:0">
                                <asp:TextBox ID="txt2" CssClass="form-control input-sm" TextMode="Number" Text="0" step="1" min="0" runat="server" OnTextChanged="txt2_TextChanged" AutoPostBack="true"></asp:TextBox>
                            </div>
                            <asp:Label ID="Label28" CssClass="col-xs-7" Text="S/. 1" runat="server"></asp:Label>
                            <div class="col-xs-5" style="padding:0">
                                <asp:TextBox ID="txt1" CssClass="form-control input-sm" TextMode="Number" Text="0" step="1" min="0" runat="server" OnTextChanged="txt1_TextChanged" AutoPostBack="true"></asp:TextBox>
                            </div>
                            <asp:Label ID="Label29" CssClass="col-xs-7" Text="S/. 0.50" runat="server"></asp:Label>
                            <div class="col-xs-5" style="padding:0">
                                <asp:TextBox ID="txt05" CssClass="form-control input-sm" TextMode="Number" Text="0" step="1" min="0" runat="server" OnTextChanged="txt05_TextChanged" AutoPostBack="true"></asp:TextBox>
                            </div>
                            <asp:Label ID="Label30" CssClass="col-xs-7" Text="S/. 0.20" runat="server"></asp:Label>
                            <div class="col-xs-5" style="padding:0">
                                <asp:TextBox ID="txt02" CssClass="form-control input-sm" TextMode="Number" Text="0" step="1" min="0" runat="server" OnTextChanged="txt02_TextChanged" AutoPostBack="true"></asp:TextBox>
                            </div>
                            <asp:Label ID="Label31" CssClass="col-xs-7" Text="S/. 0.10" runat="server"></asp:Label>
                            <div class="col-xs-5" style="padding:0">
                                <asp:TextBox ID="txt01" CssClass="form-control input-sm" TextMode="Number" Text="0" step="1" min="0" runat="server" OnTextChanged="txt01_TextChanged" AutoPostBack="true"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-6 col-xs-6" style="padding:0">
                        <asp:Label ID="Label19" CssClass="col-md-6 col-xs-12" Text="+ Gastos:" runat="server"></asp:Label>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lblTotalGastos" CssClass="col-md-6 col-xs-12" Text="S/. 0,00" runat="server"></asp:Label>
                        </ContentTemplate>
                        </asp:UpdatePanel>
                        <div class="col-md-12 col-xs-12" style="padding:0; color:lightgray">
                            <div class="col-xs-12" style="padding:0">
                                <asp:Label ID="Label32" CssClass="col-xs-12" Text="Listar Gastos" runat="server"></asp:Label>
                            </div>
                            <div class="col-xs-12" style="padding-bottom:5px">
                                <asp:TextBox ID="txtNombreGasto" CssClass="form-control input-sm" placeholder="Nombre de gasto..." TextMode="MultiLine" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvNombre" ValidationGroup="group1" Display="Dynamic" ForeColor="Red" ControlToValidate="txtNombreGasto" ErrorMessage="Ingrese Nombre de gasto." runat="server"/>
                            </div>
                            <div class="col-xs-12" style="padding-bottom:5px">
                                <asp:TextBox ID="txtMontoGasto" CssClass="form-control input-sm" placeholder="0.00 [Monto S/.]" TextMode="Number" step="0.1" min="0" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvmonto" ValidationGroup="group1" Display="Dynamic" ForeColor="Red" ControlToValidate="txtMontoGasto" ErrorMessage="Ingrese Monto en soles." runat="server"/>
                            </div>
                        
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                    <div class="col-xs-12" style="text-align:center">
                                        <asp:Button ID="btnAgregar" Enabled="false" ValidationGroup="group1" CssClass="btn btn-primary" Text="Agregar &raquo;" OnClick="btnAgregar_Click" runat="server"/>
                                    </div>
                                    <div class="col-xs-12" style="overflow:auto;">
                                        <asp:GridView Caption="Detalle de Gastos" ID="grdGastos" CssClass="gridview" runat="server" OnRowCreated="grdGastos_RowCreated" OnRowDeleting="grdGastos_RowDeleting">                    
                                            <Columns>
                                                <asp:CommandField ButtonType="Button" ControlStyle-CssClass="btn btn-xs" DeleteText="X" ControlStyle-BackColor="#453750" ControlStyle-ForeColor="White" ShowDeleteButton="true"/>
                                                <asp:TemplateField HeaderText="Item"><ItemTemplate><%# Container.DataItemIndex + 1%> </ItemTemplate></asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle Font-Bold="True" ForeColor="White" />
                                        </asp:GridView>
                                    </div>
                    
                            </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>

            </ContentTemplate>
            </asp:UpdatePanel>
            <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
            <div class="form-group">
                <div class="col-md-6">
                    <div class="col-md-12 col-xs-12">
                        <div class="col-md-5 col-xs-7">
                            <asp:Label ID="lblInfoTransacciones" runat="server" ForeColor="#e89319"></asp:Label>
                        </div>
                    </div>
                    <div class="col-md-12" style="overflow:auto; max-height:400px; overflow-y:auto" >
                        <asp:GridView ID="grdTransacciones" CssClass="gridviewBusqueda" ViewStateMode="Enabled" DataKeyNames="Id" runat="server" >                    
                            <SelectedRowStyle BackColor="Silver" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-6">
                    <div class="col-md-12 col-xs-12">
                        <div class="col-md-9 col-xs-9">
                            <asp:Label ID="lblInfoDetalle" runat="server" ForeColor="#e89319"></asp:Label>
                        </div>
                        <div class="col-md-3 col-xs-3" style="padding:15px; text-align:center">
                            <asp:Button ID="btnAnular" Visible="false" CssClass="btn btn-danger pull-left" Text="Anular" runat="server"/><br /><br />
                            <asp:Button ID="btnValidarF" Visible="false" CssClass="btn btn-success pull-left" Text="Validar"  runat="server"/>
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
            </asp:UpdatePanel>--%>
           
        </div>
    </div>

<script type="text/javascript">
        
    function Confirmacion(stk, saldo) {
            return confirm("¿Está seguro(a) de Guardar este Cierre de Turno? \n" +
            "Stock Final: S/. " + stk + "\n" + 
            "Saldo:  S/. " + saldo);
        
    }
</script>
</asp:Content>
