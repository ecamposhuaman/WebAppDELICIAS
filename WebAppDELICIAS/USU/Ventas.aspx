<%@ Page Title="Ventas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Ventas.aspx.cs" Inherits="WebAppDELICIAS.Ventas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <hr />
    <div class="row">
        <div class="col-md-12">
            <div class="form-horizontal">
                <div class="form-group"><div class="col-md-2"></div>
                    <div class="col-md-4 col-xs-12">
                        <%--Almacén--%>
                        <asp:Label ID="Label1" CssClass="col-md-12 col-xs-12" Text="Punto de Venta:" AssociatedControlID="cmbPuntoVenta" runat="server"></asp:Label>
                        <div class="col-md-12 col-xs-12">
                            <asp:DropDownList CssClass="form-control" ForeColor="#453750" Font-Bold="true" ID="cmbPuntoVenta" runat="server">
                            </asp:DropDownList>
                            <%--<asp:RequiredFieldValidator ID="rfv1" Display="Dynamic" ForeColor="Yellow" ControlToValidate="cmbPuntoVenta" ErrorMessage="Elija una opción" runat="server"/>--%>
                        </div>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                    <div class="col-md-6 col-xs-12">
                        <%--Cliente--%>
                        <asp:Label ID="Label2" CssClass="col-md-12 col-xs-12" AssociatedControlID="cmbCliente" Text="Cliente:" runat="server"></asp:Label>
                        <div class="col-md-8 col-xs-12" style="padding-bottom:5px">
                            <asp:DropDownList CssClass=" form-control" ForeColor="#453750" Font-Bold="true" ID="cmbCliente" runat="server" TabIndex="3" OnSelectedIndexChanged="cmbCliente_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                        
                        <%--<div class="autocomplete" style="width:300px;">--%>
                            <%--<asp:TextBox ID="txtCliente" CssClass="form-control" runat="server"></asp:TextBox>--%>
                            <%--<input id="txtCliente" type="text" name="myclient" placeholder="Cliente..." runat="server">--%>
                            <%--<asp:Button ID="Button1" ValidationGroup="grupo08" CssClass="btn btn-success" Text="verifique" OnClick="Button1_Click" runat="server"/>
                            <asp:Label ID="lblpruebas" Text="rpta:" runat="server"></asp:Label>--%>
                        <%--</div>--%>
                        <div class="col-md-4 col-xs-12">
                            <asp:DropDownList CssClass="form-control"  Visible="false" ID="cmbPedidos" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="cmbPedidos_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Value="0">Pedido Pendiente</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-12 col-xs-12" style="text-align:center; color:#EB6B36">
                            <asp:CheckBox CssClass="checkbox" Text="Filtrar sólo faltantes" Font-Strikeout="true" Checked="true" ID="chkAutomaticClient" runat="server" OnCheckedChanged="chkAutomaticClient_CheckedChanged" AutoPostBack="true" />
                        </div>
                    </div>
                    </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>

            <div class="form-horizontal">
                <asp:UpdatePanel ID="UPMontos" runat="server">
                <ContentTemplate>
                <div class="form-group" style="text-align:center">
                    <div class="col-md-6 col-xs-6">
                        <%--Monto--%>
                        <asp:Label ID="Label7" CssClass="col-md-12 col-xs-12" Text="SubTotal: S/. " Font-Size="Small" runat="server"></asp:Label>
                        <asp:Label ID="lblMonto" CssClass="col-md-12 col-xs-12" Text="0" ForeColor="#5cb85c" Font-Size="Large" Font-Bold="true" runat="server"></asp:Label>
                    </div>
                    <div class="col-md-6 col-xs-6">
                        <%--MontoDescontado--%>
                        <asp:Label ID="Label3" CssClass="col-md-12 col-xs-12" Text="Total: S/. " Font-Size="Small" runat="server"></asp:Label>
                        <asp:Label ID="lblMontoDsctado" CssClass="col-md-12 col-xs-12" ForeColor="#5cb85c" Text="0"  Font-Size="Large" Font-Bold="true" runat="server"></asp:Label>
                    </div>
                </div>
                </ContentTemplate>
                </asp:UpdatePanel>

                <div class="form-group" style="text-align:center">
                    <div class="col-md-12 col-xs-12" style="padding:15px; text-align:center">
                       <input type="button" id="btnAplDscto" class="btn btn-info" value="Insertar Descuento" onclick="hideShowDIV();" />
                    </div>
                </div>

                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                <div id="divdinamico">
                    <div class="form-group">
                        <div class="col-md-6 col-xs-6" style="text-align:right">
                            <%--Descuento--%>
                            <asp:Label ID="lblDescuento" CssClass="col-md-12 col-xs-12" Text="Descuento: S/." AssociatedControlID="txtDscto" runat="server"></asp:Label>
                            <div class="col-md-5 col-md-offset-7 col-xs-12">
                                <asp:TextBox ID="txtDscto" CssClass="form-control" ForeColor="#5cb85c" Font-Bold="true" TextMode="Number" step="0.1" min="0" OnTextChanged="txtDscto_TextChanged" AutoPostBack="true" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6 col-xs-6" style="text-align:left">
                            <div class="col-md-6 col-xs-12">
                                <%--Descripción Descuento--%>
                                <asp:TextBox ID="txtDescripDscto" Height="60px" CssClass="form-control" TextMode="MultiLine" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="form-group">
                    <div class="col-md-6 col-xs-6" style="text-align:right">
                        <%--PAGO AL contado o Credito--%>
                        <asp:Label ID="Label8" CssClass="col-md-12 col-xs-12" Text="Forma de Pago:" AssociatedControlID="cmbContdCred" runat="server"></asp:Label>
                        <div class="col-md-5 col-md-offset-7 col-xs-12">
                            <asp:DropDownList CssClass="form-control" ID="cmbContdCred" OnSelectedIndexChanged="cmbContdCred_SelectedIndexChanged" AutoPostBack="true" runat="server" TabIndex="4">
                                <asp:ListItem Text="Al Contado" Value="0"></asp:ListItem>
                                <asp:ListItem Text="A Crédito" Value="1"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-6 col-xs-6" style="text-align:left">
                        <%--Efectivo--%>
                        <asp:Label ID="lblLabelEfectivo" CssClass="col-md-12 col-xs-12" Text="Efectivo:" AssociatedControlID="txtEfectivo" runat="server"></asp:Label>
                        <div class="col-md-5 col-xs-12">
                            <asp:TextBox ID="txtEfectivo" CssClass="form-control" Font-Bold="true" ForeColor="#F79824" TextMode="Number" step="0.1" min="0" OnTextChanged="txtEfectivo_TextChanged" AutoPostBack="true" runat="server" TabIndex="5"></asp:TextBox>
                        </div>
                    </div>
                </div>
                
                <div class="form-group" style="text-align:center">
                    <div class="col-md-12 col-xs-12">
                        <%--Deuda/Vuelto--%>
                        <asp:Label ID="lblLabelDeudaVuelto" CssClass="control-label" Font-Bold="true" ForeColor="#5cb85c" runat="server"></asp:Label>
                        <asp:Label ID="lblDeudaVuelto" CssClass="control-label" Font-Bold="true" ForeColor="#5cb85c" runat="server"></asp:Label>
                    </div>
                </div>
                
                <div class="form-group" style="text-align:center">
                    <%-- BOTON REGISTRAR VENTA --%>
                    <asp:Label ID="lblInfoRegistrar" CssClass="col-md-12" runat="server" ForeColor="Red"></asp:Label>
                    <div class="col-md-12">
                        <asp:Button ID="btnRegistrarVenta" Enabled="false" ValidationGroup="grupo03" CssClass="btn btn-success" Width="285px" Text="Registrar Venta" OnClick="btnRegistrarVenta_Click" 
                             runat="server" TabIndex="5" />
                    </div>
                </div>
                </ContentTemplate>
                </asp:UpdatePanel>
            </div>

            <div class="modal fade" id="AgregarProductos" tabindex="-1">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4>Detalle de Venta</h4>
                        </div>
                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                        <ContentTemplate>
                        <div class="modal-body" >
                         
                            <%--CONTENIDO DEL MODAL--%>
                            <div class="w3-responsive">
                                <asp:GridView ID="grdContact" CssClass="gridview" runat="server" AutoGenerateColumns="false"
                                    DataKeyNames="IdProducto" OnRowCancelingEdit="grdContact_RowCancelingEdit"
                                    OnRowDataBound="grdContact_RowDataBound" OnRowEditing="grdContact_RowEditing" 
                                    OnRowUpdating="grdContact_RowUpdating" OnRowCommand="grdContact_RowCommand" 
                                    ShowFooter="True" FooterStyle-BackColor="#ff3399" OnRowDeleting="grdContact_RowDeleting"
                                    > 
                                            
                                <Columns> 
                                    <asp:TemplateField HeaderText="ID"> 
                                        <EditItemTemplate> 
                                            <asp:Label ID="lblId" runat="server" Text='<%# Bind("IdProducto") %>'></asp:Label>
                                        </EditItemTemplate> 
                                        <ItemTemplate> 
                                            <asp:Label ID="lblId" runat="server" Text='<%# Bind("IdProducto") %>'></asp:Label> 
                                        </ItemTemplate> 
                                    </asp:TemplateField> 
                                    <asp:TemplateField HeaderText="Producto"> <%--ItemStyle-Width="90px"--%>
                                        <EditItemTemplate> 
                                            <asp:Label ID="lblName" runat="server" Text='<%# Bind("NombreProd") %>'></asp:Label>
                                        </EditItemTemplate> 
                                        <ItemTemplate> 
                                            <asp:Label ID="lblName" runat="server" Text='<%# Bind("NombreProd") %>'></asp:Label> 
                                        </ItemTemplate> 
                                    </asp:TemplateField> 
                                    <asp:TemplateField HeaderText="Precio">
                                        <ItemTemplate> 
                                            <asp:Label ID="lblPrecio" runat="server" Text='<%# Bind("Precio") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Venta"> 
                                        <ItemTemplate> 
                                            <asp:TextBox ID="txtCantVN" runat="server" Width="50px" Text='<%# Bind("CantVN") %>' CssClass="form-control input-sm" TextMode="Number"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField> 
                                    <asp:TemplateField HeaderText="Bonif."> 
                                        <ItemTemplate> 
                                            <asp:TextBox ID="txtCantBnf" runat="server" Width="50px" Text='<%# Bind("CantBnf") %>' CssClass="form-control input-sm" TextMode="Number"></asp:TextBox>
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Recambio Vencidos"> 
                                        <ItemTemplate> 
                                            <asp:TextBox ID="txtCantRcV" runat="server" Width="50px" Text='<%# Bind("CantRcV") %>' CssClass="form-control input-sm" TextMode="Number"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Recambio Mal Est.">
                                        <ItemTemplate> 
                                            <asp:TextBox ID="txtCantRcM" runat="server" Width="50px" Text='<%# Bind("CantRcM") %>' CssClass="form-control input-sm" TextMode="Number"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                            
                                    <%--<asp:TemplateField HeaderText="Editar" ShowHeader="False"> 
                                        <ItemTemplate> 
                                            <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="False" CommandName="Edit" Text="Editar"></asp:LinkButton> 
                                        </ItemTemplate> 
                                        <EditItemTemplate> 
                                            <asp:LinkButton ID="lbkUpdate" runat="server" CausesValidation="True" CommandName="Update" Text="Actualizar"></asp:LinkButton> 
                                            <asp:LinkButton ID="lnkCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancelar"></asp:LinkButton> 
                                        </EditItemTemplate> 
                                        <FooterTemplate> 
                                            <asp:LinkButton ID="lnkAdd" runat="server" CausesValidation="False" CommandName="Insert" Text="Insertar"></asp:LinkButton> 
                                        </FooterTemplate>
                                    </asp:TemplateField> 

                                    <asp:CommandField HeaderText="Eliminar" ShowDeleteButton="true" ShowHeader="True" /> --%>

                                </Columns> 
                                </asp:GridView> 
                            </div>
                        </div>
                        </ContentTemplate>
                        </asp:UpdatePanel>
                        <div class="modal-footer">
                            <asp:Button ID="BtnAniadirProductos" ValidationGroup="group10" CssClass="btn btn-primary" Text="Agregar Items &raquo;" OnClick="BtnAniadirProductos_Click" runat="server"/>
                            <input id="Button1" type="button" data-dismiss="modal" Class="btn btn-success" value="Cancelar" />
                            <div class="row"><p><br /><br /><br /></p></div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="form-horizontal" style="background-color:#f0ad4e; padding:10px;border-radius:6px; color:#857054;">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                <div class="form-group">
                    <div class="col-md-3 col-xs-8">
                        <asp:Button ID="btnModal" CssClass="btn btn-info" runat="server" Text="Seleccionar Productos &raquo;" OnClick="btnModal_Click"/>
                    </div>
                    <div class="col-md-9 col-xs-4">
                        <asp:Button ID="btnLimpiar" Visible="false" ValidationGroup="groupL" CssClass="btn btn-default" Text="Limpiar" OnClick="btnLimpiar_Click" runat="server"/>
                    </div>
                    <asp:Label ID="lblCantProd" CssClass="col-md-offset-1 col-md-12" ForeColor="#453750" runat="server"></asp:Label>
                </div>
                
                <div class="form-group">
                    <div class="col-md-offset-1 col-md-12" style="overflow:auto;">
                        <asp:GridView Caption="Detalle de Productos Seleccionados" ID="grdProductos" CssClass="gridview" runat="server" OnRowCreated="grdProductos_RowCreated" OnRowDeleting="grdProductos_RowDeleting">                    
                            <Columns>
                                <asp:CommandField ButtonType="Button" ControlStyle-CssClass="btn btn-xs" DeleteText="X" ControlStyle-BackColor="#453750" ControlStyle-ForeColor="White" ShowDeleteButton="true"/>
                                <asp:TemplateField HeaderText="✓"><ItemTemplate><%# Container.DataItemIndex + 1%> </ItemTemplate></asp:TemplateField>
                                
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

<style>  
    .productList {  
        border: 1px solid #6c757d;  
        margin-bottom: 10px;  
        padding: 15px;  
        border-radius: 3px;  
    }  
</style>

<script type="text/javascript">
        
    function ConfirmacionV(md, dsc, deu) {
        if (deu == "0") {
            return confirm("¿Está seguro de registrar esta Venta? \n" +
            "Venta Total: S/. " + md + "\n" + 
            "Descuento aplicado: S/. " + dsc + "\n" +
            "Venta CANCELADA");
        } else {
            return confirm("¿Está seguro de registrar esta Venta? \n" +
            "Venta Total: S/. " + md + "\n" + 
            "Descuento aplicado: S/. " + dsc + "\n" +
            "Pago a Crédito - Deuda por generarse: S/. " + deu );
        }
    }

    function NotificarVentaEmpleado(md, dsc, deu, deudaAct, rpta) {
        if (deu == "0") {
            if (rpta == "1") {
                return confirm("¿Está seguro de registrar esta Venta? \n" +
                    "Venta dirigida a un Empleado de la Organización." + "\n" +
                    "Deuda Actual: S/. " + deudaAct + "\n" + 
                    "Total Venta : S/. " + md + "\n" + 
                    "Descuento aplicado: S/. " + dsc +  "\n" +
                    "Venta CANCELADA. No se generará ninguna deuda adicional.");
            } else {
                return confirm("¿Está seguro de registrar esta Venta? \n" +
                    "Venta dirigida a un Empleado de la Organización." + "\n" +
                    "Deuda Actual: S/. " + deudaAct  + "\n" + 
                    "Total Venta : S/. " + md +  "\n" + 
                    "Descuento aplicado: S/. " + dsc  + "\n" +
                    "Venta CANCELADA. No se generará ninguna deuda adicional.");
            }
        } else {
            if (rpta == "1") {
                return confirm("¿Está seguro de registrar esta Venta? \n" +
                    "Venta dirigida a un Empleado de la Organización." + "\n" +
                    "Deuda Actual: S/. " + deudaAct  + "\n" + 
                    "Total Venta: S/. " + md +  "\n" + 
                    "Descuento aplicado: S/. " + dsc + "\n" +
                    "Pago a Crédito - Deuda adicional por generarse: S/. " + deu );
            } else {
                return confirm("¿Está seguro de registrar esta Venta? \n" +
                    "Venta dirigida a un Empleado de la Organización." + "\n" +
                    "Deuda Actual: S/. " + deudaAct  + "\n" + 
                    "Total Venta: S/. " + md + "\n" + 
                    "Descuento aplicado: S/. " + dsc  + "\n" +
                    "Pago a Crédito - Deuda adicional por generarse: S/. " + deu + "\n" +
                    "SE RECOMIENDA CANCELAR ESTA VENTA, LA RACIÓN SERÁ SUPERADA.");
            }
        }
    }
    
    function hideShowDIV(){
            <%--var aspNetWayOfResolvingId = $("#<%= _btnOk.ClientID %>");--%> // use this if clientidmode is not static
        var iddivdinamico = $("#divdinamico");
        var idbtn = $("#btnAplDscto");
        if (iddivdinamico.is(":visible")) {
            iddivdinamico.hide();
            idbtn.val("Insertar Descuento");
        }
        else {
            iddivdinamico.show();
            idbtn.val("Ocultar Descuento");
        }
    }

    function noabrirDscto() {
        var iddivdinamico2 = $("#divdinamico");
        var idbtn2 = $("#btnAplDscto");
        if (iddivdinamico2.is(":visible")) {
            iddivdinamico2.hide();
            idbtn2.val("Insertar Descuento");
        } 
    }
    
    function AbrirModal(){
        $("#AgregarProductos").modal('toggle')
    }

    // MANTENER SESION TODO EL TIEMPO
    setInterval('MantenSesion()', <%= (int) (0.9 * (Session.Timeout * 60000)) %>);

    function MantenSesion()
    {                
        var CONTROLADOR = "refresh_session.ashx";
        var head = document.getElementsByTagName('head').item(0);            
        script = document.createElement('script');            
        script.src = CONTROLADOR ;
        script.setAttribute('type', 'text/javascript');
        script.defer = true;
        head.appendChild(script);
    } 

    // ------ FUNCION PARA VERIFICAR SESION
    //(function () {
    //    setInterval(function () {
    //        $.post("@Url.Content("~/Acceso/ComprobarSesion/")", function (data) {
    //            $("#existeSesion").html(data);
    //            if(data=="Sin sesion")
    //                 document.location.href = "~/Account/Login.aspx";
    //        });
    //    },5000);
    //})();


</script>
</asp:Content>
