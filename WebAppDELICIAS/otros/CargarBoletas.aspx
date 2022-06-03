<%@ Page Title="Cargar Boletas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CargarBoletas.aspx.cs" Inherits="WebAppDELICIAS.CargarBoletas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <asp:Button ID="btnMisBoletas" Width="220px" class="btn btn-success" runat="server" Text="Ir a Mis Boletas &raquo;" OnClick="btnMisBoletas_Click"/>
    
    <div class="jumbotron" style="color:black; padding-top:3px;">
        <h2>Informes.</h2>
        <div class="row">
            <asp:Button ID="btnNuevo" CssClass="btn btn-info btn-lg" runat="server" Text="Agregar Boleta &raquo;" OnClick="btnNuevo_Click"/>
        </div>
        <div class="row">
            <div class="col-md-12" style="overflow:auto; max-height:400px; overflow-y:auto" >
                <asp:Label ID="lbloperacion" runat="server" Visible="False"></asp:Label>
                <asp:GridView ID="grdBoletas" runat="server" OnSelectedIndexChanged="grdBoletas_SelectedIndexChanged" DataKeyNames="IdBoleta"
                    CssClass="gridviewBusqueda">                    
                     
                </asp:GridView>
            </div>
        </div>

        <div class="modal fade" id="ActualizarEliminar" tabindex="-1">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <asp:Label ID="Label1" runat="server" ForeColor="#808080" Text="Titulo de ModalPopUP"></asp:Label>
                    </div>
                    <div class="modal-body">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <asp:Label runat="server" AssociatedControlID="txtIdBoleta" CssClass="col-md-2 control-label">IdBoleta:</asp:Label>
                                <div class="col-md-10">
                                    <asp:TextBox ID="txtIdBoleta" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <asp:Label runat="server" AssociatedControlID="cmbNombre" CssClass="col-md-2 control-label">Nombre:</asp:Label>
                                <div class="col-md-10">
                                    <asp:DropDownList ID="cmbNombre" CssClass="form-control"  runat="server">
                                        <asp:ListItem Text="Elmer Campos" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Sammir Araujo" Value="120"></asp:ListItem>
                                        <asp:ListItem Text="-Nuevo Empleado-" Value="121"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnActualizar" Width="220px" CssClass="btn btn-info btn-lg" ValidationGroup="grupo04" runat="server" Text="Actualizar &raquo;" OnClick="btnActualizar_Click"/>
                        <asp:Button ID="btnEliminar" Width="220px" CssClass="btn btn-info btn-lg" ValidationGroup="grupo04" runat="server" Text="Eliminar &raquo;" OnClick="btnEliminar_Click"/>
                        <input id="Button1" type="button" data-dismiss="modal" Class="btn btn-success btn-lg" value="Cancelar" />
                    </div>
                </div>
            </div>
        </div>

         <div class="modal fade" id="Nuevo" tabindex="-1">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <asp:Label ID="Label2" runat="server" ForeColor="#808080" Text="Titulo de ModalPopUP"></asp:Label>
                    </div>
                    <div class="modal-body">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <asp:Label runat="server" AssociatedControlID="cmbNombre" CssClass="col-md-2 control-label">Nombre:</asp:Label>
                                <div class="col-md-10">
                                    <asp:DropDownList ID="DropDownList1" CssClass="form-control"  runat="server">
                                        <asp:ListItem Text="Elmer Campos" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Sammir Araujo" Value="520"></asp:ListItem>
                                        <asp:ListItem Text="-Nuevo Empleado-" Value="521"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <asp:Label runat="server" AssociatedControlID="fupArchivo" CssClass="col-md-2 control-label">Boleta:</asp:Label>
                                <div class="btn-group btn-group-justified">
                                    <asp:Label ID="StatusLabel" runat="server"></asp:Label>
                                    <div>
                                        <div style="margin-bottom:5px">
                                            <asp:Label ID="lblnombreimagen" runat="server" ForeColor="#808080" Text="No se ha seleccionado ningún archivo"></asp:Label>
                                        </div>
                                        <label class="file-upload btn-info btn-lg" style="text-align: center;">
                                            <asp:Label ID="lblFoto"  runat="server" ToolTip="Elija el archivo en formato PDF" Text="Examinar"></asp:Label>
                                            <asp:FileUpload  ID="fupArchivo" runat="server" Width="0" Height="0" onchange="showimagepreview(this)" CssClass="oculto"/> 
                                        </label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ForeColor="Red" ValidationGroup="grupo01" ControlToValidate="fupArchivo" ErrorMessage="Elija un Archivo PDF. "></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnCrear" Width="220px" CssClass="btn btn-info btn-lg" ValidationGroup="grupo05" runat="server" Text="Registrar &raquo;" OnClick="btnCrear_Click"/>
                        <input id="Button2" type="button" data-dismiss="modal" Class="btn btn-success btn-lg" value="Cancelar" />
                    </div>
                </div>
            </div>
        </div>
    </div>
        

<style type="text/css">
    .peq{clear: both; overflow: hidden; height: 1%}
    .oculto{ visibility: hidden }
    .AlineadoDerecha{text-align:right;}
</style>
<script>   
    $(document).ready(function () {
    $(document).on('change', '#<%= fupArchivo.ClientID %>', function (e) {
        $('#<%= lblFoto.ClientID %>').text("Cambiar"/*e.target.files[0].name*/);
        $('#<%= lblnombreimagen.ClientID %>').text(e.target.files[0].name /*+ "algo mas"*/);
    });
    });

    function AbrirModalNuevo(){
        $("#Nuevo").modal('toggle')
    }
    
    function AbrirModalActElim(){
        $("#ActualizarEliminar").modal('toggle')
    }
</script>
 
</asp:Content>
