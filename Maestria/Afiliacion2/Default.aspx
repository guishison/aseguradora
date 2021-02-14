<%@ Page Title="" Language="VB" AutoEventWireup="false" MasterPageFile="~/assets/master/MasterPage.Master" CodeBehind="Default.aspx.vb" Inherits="Afiliacion2._Default" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content0" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" onload="QuitarPanel()" runat="Server">
    <!--Dashboard -->
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>Dashboard
            <small>Panel Central</small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Inicio</a></li>
            <li class="active">Dashboard</li>
        </ol>
    </section>
    <!-- Main content -->
    <section class="content">
    </section>
    <script type="text/javascript">
        function QuitarPanel() {
            var clase = document.getElementById("Bodycito");
            clase.classList.remove("MyModalPanel");
        }
    </script>
</asp:Content>

