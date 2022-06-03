<%@ Page Title="Contacto" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="WebAppDELICIAS.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <h3>Su apartado de Contacto.</h3>
    <address>
        Parra del Riego 164 Int. ALTO<br />
        El Tambo - Huancayo - Junín<br />
        <abbr title="Teléfono Celular">C:</abbr>
        987654321
    </address>

    <address>
        <strong>Support:</strong>   <a href="mailto:47667981@continental.edu.pe">47667981@continental.edu.pe</a><br />
        <strong>Marketing:</strong> <a href="mailto:Marketing@example.com">Marketing@ejemplo.com</a>
    </address>
</asp:Content>
