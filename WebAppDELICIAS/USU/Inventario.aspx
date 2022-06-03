<%@ Page Title="Kardex" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Inventario.aspx.cs" Inherits="WebAppDELICIAS.ADM.Inventario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <hr />
    <div class="row">
        <div class="col-md-12">
            <div class="form-group">
                <div class="col-md-5">
                    <asp:Label ID="Label1" CssClass="col-md-12" AssociatedControlID="cmbAlmacen" Text="Almacén:" runat="server"></asp:Label>
                    <div class="col-md-12">
                        <%--Almacén--%>
                        <asp:DropDownList CssClass="form-control" ForeColor="#453750" ID="cmbAlmacen" runat="server">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-4">
                    
                </div>
                <%--<div class="col-md-3">
                    <asp:Label ID="Label2" CssClass="col-md-12" Text="Buscar en:" runat="server"></asp:Label>
                    <div class="col-md-12">
                        <asp:DropDownList CssClass="form-control" ID="cmbDiasbusqueda" runat="server">
                            <asp:ListItem Text="Esta Jornada" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Última hora" Value="1"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>--%>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                <div class="col-md-3">
                    <div class="col-md-12" style="padding-top:20px">
                        <asp:Button ID="btnBuscar" CssClass="btn btn-success" Width="160px" Text="Inventariar" OnClick="btnBuscar_Click" runat="server"/>
                    </div>
                </div>
                </ContentTemplate>
                </asp:UpdatePanel>
            </div>
                
            <div class="row">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                <div class="form-group">
                    <div class="col-md-12">
                        <div class="col-md-12" style="overflow:auto;"> <%--GRIDVIEW--%>
                            <asp:GridView ID="grdResultados" CssClass="gridview" ViewStateMode="Enabled" runat="server" OnSelectedIndexChanged="grdResultados_SelectedIndexChanged">                    
                                <SelectedRowStyle BackColor="Silver" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                
                </ContentTemplate>
                </asp:UpdatePanel>
            </div>

        </div>
    </div>

</asp:Content>
