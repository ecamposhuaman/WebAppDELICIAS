<%@ Page Title="Deudas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Deudas.aspx.cs" Inherits="WebAppDELICIAS.ADM.Deudas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <hr />
    <div class="row">
        <div class="col-md-12">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
            <div class="col-md-6" style="padding:0; padding-bottom:10px">
                <div class="col-md-12" style="padding:0; padding-bottom:10px">
                    <asp:Label ID="Label1" CssClass="col-md-12" AssociatedControlID="cmbClientes" Text="Cliente:" runat="server"></asp:Label>
                    <div class="col-md-12" style="padding:0;">
                        <div class="col-md-9 col-xs-8">
                            <asp:DropDownList CssClass="form-control" ForeColor="#453750" ID="cmbClientes" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="cmbClientes_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Text="- Seleccione un Cliente -" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfv3" runat="server" ValidationGroup="G1" Display="Dynamic" ForeColor="Yellow" ControlToValidate="cmbClientes" InitialValue="0" ErrorMessage="Seleccione un Cliente "/>
                        </div>
                        <div class="col-md-3 col-xs-4">
                            <asp:Button ID="btnBuscar" ValidationGroup="G1" CssClass="btn btn-info" Text="Buscar Deuda" OnClick="btnBuscar_Click" runat="server"/>
                        </div>
                    </div>
                <br /><br />
                </div>
                <div class="col-md-12" style="text-align:center;">
                    <asp:Button ID="btnBuscarTotos" ValidationGroup="G5" CssClass="btn btn-primary" Text="Consultar Todas las Deudas Existentes" OnClick="btnBuscarTotos_Click" runat="server"/>
                </div>
            </div>
            
            <div class="col-md-6" style="padding:0">
                <div class="col-md-12" style="padding:0">
                    <asp:Label ID="Label2" CssClass="col-md-12" Text="Monto de Amortización (S/.):" AssociatedControlID="txtMonto" runat="server"></asp:Label>
                    <div class="col-md-12" style="padding:0">
                        <div class="col-md-4 col-xs-6">
                            <asp:TextBox ID="txtMonto" CssClass="form-control" Width="190px" Placeholder="0.00" TextMode="Number" step="0.1" min="0" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvMonto" ValidationGroup="GPagar" Display="Dynamic" ForeColor="Red" ControlToValidate="txtMonto" ErrorMessage="Ingrese Monto." runat="server"/>
                        </div>
                        <div class="col-md-8 col-xs-6">
                            <asp:Button ID="btnPagarDeuda" Enabled="false" ValidationGroup="GPagar" CssClass="btn btn-success" Text="Amortizar Deudas" OnClick="btnPagarDeuda_Click" runat="server"/>
                        </div>
                    </div><br /><br />
                </div>
                <div class="col-md-12">
                    <asp:Label ID="lblInfoError" CssClass="col-md-12" runat="server" ForeColor="#00cc99"></asp:Label>
                </div>
            </div>
            </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        
        <div class="col-md-12">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
            <div class="form-group">
                <div class="col-md-12">
                    <div class="col-md-12" style="overflow:auto; max-height:400px; overflow-y:auto" > <%--GRIDVIEW--%>
                        <asp:GridView ID="grdDeudas" CssClass="gridview" EmptyDataText="Ningún resultado encontrado" ViewStateMode="Enabled" runat="server">                    
                            <SelectedRowStyle BackColor="Silver" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
                
            </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

</asp:Content>
