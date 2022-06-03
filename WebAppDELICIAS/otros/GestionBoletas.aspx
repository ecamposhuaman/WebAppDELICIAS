<%@ Page Title="Mis Boletas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GestionBoletas.aspx.cs" Inherits="WebAppDELICIAS.GestionBoletas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <asp:Button ID="btnCargarBoletas" Width="220px" class="btn btn-success" runat="server" Text="Ir a Cargar Boletas &raquo;" OnClick="btnCargarBoletas_Click"/>
    
    <div>
        <section class="row">
            <div class="col-md-12 col-xs-12">
                     <span class="input-group-btn">
                         <asp:Button ID="btnbuscar" CssClass="btn btn-info" runat="server" Text="Buscar" OnClick="btnbuscar_Click"/>
                     </span>
           </div>
        </section>
        <br />
        <div class="row">
             <div class="col-md-12" style="overflow:auto; max-height:400px; overflow-y:auto" >
                 <asp:Label ID="lbloperacion" runat="server" Visible="False"></asp:Label>
                 <asp:GridView ID="grdBoletas" runat="server" OnSelectedIndexChanged="grdBoletas_SelectedIndexChanged" DataKeyNames="IdBoleta"
                     CssClass="gridview">                    
                     
                 </asp:GridView>
             </div>
       </div>
    </div>
    
    <br />
    
</asp:Content>
