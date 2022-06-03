<%@ Page Title="Movimientos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Flujos.aspx.cs" Inherits="WebAppDELICIAS.Flujos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <hr />
    <div class="row">
        <div class="col-md-12">
            <div class="form-horizontal">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="form-group">
                        <%--Almacén Origen--%>
                        <asp:Label ID="Label1" CssClass="control-label col-md-1 col-xs-3" Text="Origen:" AssociatedControlID="cmbAlmacenOrig" runat="server"></asp:Label>
                        <div class="col-md-4 col-xs-9">
                            <asp:DropDownList CssClass="form-control" ForeColor="#453750" Font-Bold="true" ID="cmbAlmacenOrig" OnSelectedIndexChanged="cmbAlmacenOrig_SelectedIndexChanged" AutoPostBack="true" runat="server">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <%--Almacén Destino--%>
                        <asp:Label ID="Label2" CssClass="control-label col-md-1 col-xs-3" Text="Destino:" AssociatedControlID="cmbAlmacenDest" runat="server"></asp:Label>
                        <div class="col-md-4 col-xs-9">
                            <asp:DropDownList CssClass="form-control" ForeColor="#453750" Font-Bold="true" ID="cmbAlmacenDest" runat="server">
                            </asp:DropDownList>
                        </div>
                    </div>
                </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="form-horizontal">
                <div class="form-group" style="text-align:center"> <%-- BOTON REGISTRAR --%>
                    <asp:Label ID="lblInfoRegistrar" CssClass="col-md-12"  runat="server" ForeColor="Red"></asp:Label>
                    <div class="col-md-12" >
                        <asp:Button ID="btnRegistrarFlujo" ValidationGroup="grupo02" Enabled="false" CssClass="btn btn-success" Width="200px" Text="Registrar" OnClick="btnRegistrarFlujo_Click" runat="server"/>
                    </div>
                </div>
            </div>
        

            <div class="modal fade" id="AgregarProductos" tabindex="-1">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4>Detalle de Movimiento</h4>
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
                                            <%--<asp:TextBox ID="txtName" runat="server" Text='<%# Bind("NombreProd") %>' CssClass="form-control input-sm"></asp:TextBox> --%>
                                        </EditItemTemplate> 
                                        <ItemTemplate> 
                                            <asp:Label ID="lblName" runat="server" Text='<%# Bind("NombreProd") %>'></asp:Label> 
                                        </ItemTemplate> 
                                        <%--<FooterTemplate> 
                                            <asp:TextBox ID="txtNewName" runat="server" CssClass="form-control input-sm"></asp:TextBox> 
                                        </FooterTemplate>--%>
                                    </asp:TemplateField> 
                                    <asp:TemplateField HeaderText="En Buen Estado" > 
                                        <%--<EditItemTemplate> 
                                            <asp:TextBox ID="txtCantA" runat="server" Text='<%# Bind("CantA") %>' CssClass="form-control input-sm"></asp:TextBox> 
                                        </EditItemTemplate>--%> 
                                        <ItemTemplate> 
                                            <asp:TextBox ID="txtCantA" runat="server" Text='<%# Bind("CantA") %>' CssClass="form-control input-sm" TextMode="Number"></asp:TextBox>
                                            <%--<asp:Label ID="lblCantA" runat="server" Text='<%# Bind("CantA") %>'></asp:Label> --%>
                                        </ItemTemplate> 
                                        <%--<FooterTemplate> 
                                            <asp:TextBox ID="txttxtNewCantA" runat="server" CssClass="form-control input-sm"></asp:TextBox> 
                                        </FooterTemplate>--%>
                                    </asp:TemplateField> 
                                    <asp:TemplateField HeaderText="Vencidos"> 
                                        <%--<EditItemTemplate> 
                                            <asp:TextBox ID="txtCantB" runat="server" Text='<%# Bind("CantB") %>' CssClass="form-control input-sm"></asp:TextBox> 
                                        </EditItemTemplate> --%>
                                        <ItemTemplate> 
                                            <asp:TextBox ID="txtCantB" runat="server" Text='<%# Bind("CantB") %>' CssClass="form-control input-sm" TextMode="Number"></asp:TextBox> 
                                            <%--<asp:Label ID="lblCantB" runat="server" Text='<%# Bind("CantB") %>'></asp:Label> --%>
                                        </ItemTemplate> 
                                        <%--<FooterTemplate> 
                                            <asp:TextBox ID="txttxtNewCantB" runat="server" CssClass="form-control input-sm"></asp:TextBox> 
                                        </FooterTemplate>--%>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="En Mal Estado"> 
                                        <%--<EditItemTemplate> 
                                            <asp:TextBox ID="txtCantC" runat="server" Text='<%# Bind("CantC") %>' CssClass="form-control input-sm"></asp:TextBox> 
                                        </EditItemTemplate> --%>
                                        <ItemTemplate> 
                                            <asp:TextBox ID="txtCantC" runat="server" Text='<%# Bind("CantC") %>' CssClass="form-control input-sm" TextMode="Number"></asp:TextBox> 
                                            <%--<asp:Label ID="lblCantC" runat="server" Text='<%# Bind("CantC") %>'></asp:Label> --%>
                                        </ItemTemplate> 
                                        <%--<FooterTemplate> 
                                            <asp:TextBox ID="txttxtNewCantC" runat="server" CssClass="form-control input-sm"></asp:TextBox> 
                                        </FooterTemplate>--%>
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
                <div class="form-group">
                    <div class="col-md-3 col-xs-8">
                        <asp:Button ID="btnModal" CssClass="btn btn-info" runat="server" Text="Seleccionar Productos &raquo;" OnClick="btnModal_Click"/>
                    </div>
                    <div class="col-md-9 col-xs-4">
                        <asp:Button ID="btnLimpiar" Visible="false" ValidationGroup="groupL" CssClass="btn btn-default" Text="Limpiar" OnClick="btnLimpiar_Click" runat="server"/>
                    </div>
                    <asp:Label ID="lblCantProd" CssClass="col-md-offset-1 col-md-12" ForeColor="#453750" runat="server"></asp:Label>
                </div>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
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
 
    function ConfirmacionF(items, cnt) {
            return confirm("¿Está seguro de registrar este Movimiento de Productos? \n" +
            "Total de Items: " + items + "\n" + 
            "Total de Unidades: " + cnt);
        
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
