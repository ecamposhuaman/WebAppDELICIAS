<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Empresa.aspx.cs" Inherits="WebAppDELICIAS.otros.Empresa" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

        
    <!-- Sidebar/menu -->
<nav class="w3-sidebar w3-collapse w3-white w3-animate-left" style="z-index:3;width:300px; height:450PX; " id="mySidebar"><br>
  <div class="w3-container">
    <a href="#" onclick="w3_close()" class="w3-hide-large w3-right w3-jumbo w3-padding w3-hover-grey" title="close menu">
      <i class="fa fa-remove"></i>
    </a>
    <img src="/w3images/avatar_g2.jpg" style="width:45%;" class="w3-round"><br><br>
    <h4><b>HOME</b></h4>
    <p class="w3-text-grey">...Todo a un solo Click</p>
  </div>
  <div class="w3-bar-block">
    <a href="#portfolio" onclick="w3_close()" class="w3-bar-item w3-button w3-padding w3-text-teal"><i class="fa fa-th-large fa-fw w3-margin-right"></i>Inicio</a> 
    <a href="#portfolio2" onclick="w3_close()" class="w3-bar-item w3-button w3-padding "><i class="fa fa-user fa-fw w3-margin-right"></i>ACERCA DE NOSOTROS</a> 
    <a href="#contact" onclick="w3_close()" class="w3-bar-item w3-button w3-padding"><i class="fa fa-envelope fa-fw w3-margin-right"></i>CONTACTANOS</a>
  </div>
  <div class="w3-panel w3-large">
    <i class="fa fa-facebook-official w3-hover-opacity"></i>
    <i class="fa fa-instagram w3-hover-opacity"></i>
    <i class="fa fa-snapchat w3-hover-opacity"></i>
    <i class="fa fa-pinterest-p w3-hover-opacity"></i>
    <i class="fa fa-twitter w3-hover-opacity"></i>
    <i class="fa fa-linkedin w3-hover-opacity"></i>
  </div>
</nav>

    <!-- Overlay effect when opening sidebar on small screens -->
<div class="w3-overlay w3-hide-large w3-animate-opacity" onclick="w3_close()" style="cursor:pointer" title="close side menu" id="myOverlay"></div>

<!-- !PAGE CONTENT! -->
<div class="w3-main" style="margin-left:320px">

  <!-- Header -->
  <header id="portfolio2">
    
    <span class="w3-button w3-hide-large w3-xxlarge w3-hover-text-grey" onclick="w3_open()"><i class="fa fa-bars"></i></span>
    <div class="w3-container">
    <h1><b>Sedapar</b></h1>
    <div class="w3-section w3-bottombar w3-padding-16">
              <span class="w3-margin-right">Ver:</span> 
              <button class="w3-button w3-black">INTRANET</button>
              
              <button class="w3-button w3-white w3-hide-small"><i class="fa fa-photo w3-margin-right"></i>WebMail</button>
              <button class="w3-button w3-white w3-hide-small"><i class="fa fa-map-pin w3-margin-right"></i>Accesibilidad</button>
                <button class="w3-button w3-white"><i class="fa fa-diamond w3-margin-right"></i>Transparencia</button>
            </div>
    </div>
  </header>
    
    <div class="jumbotron" style="color: royalblue; background-color:white; padding-top:3px; ">


        <header id="portafolio2">
            

      </header>
    </div>

  </div>



</asp:Content>
