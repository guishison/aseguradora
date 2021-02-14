<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="login.aspx.vb" Inherits="Afiliacion2.login" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html>
<html class="bg-green" xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <title>Sistema</title>
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport" />
    <!-- bootstrap 3.0.2 -->
    <link href="~/assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link rel="icon" href="~/assets/image/logo2.ico" />
    <!-- font Awesome -->
    <link href="~/assets/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <!-- Theme style -->
    <link href="~/assets/css/adminlte.css" rel="stylesheet" type="text/css" />
    <link href="~/assets/css/hacks.css" rel="stylesheet" type="text/css" />
</head>
<body class="bg-green">
    <form id="form1" name="formul" runat="server" defaultbutton="rbtAcces">
        <telerik:RadScriptManager ID="rsmManager" runat="server" />
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnRequestStart="AjustarPanel();" UpdatePanelsRenderMode="Inline">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="rbtAcces">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="LoginPanel" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="abrir">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="LoginPanel" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" ForeColor="#555555" IsSticky="true" Transparency="30" CssClass="MyModalPanel">
        </telerik:RadAjaxLoadingPanel>

        <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
            <script type="text/javascript">
                function AjustarPanel() {
                   <%-- $get("<%= RadAjaxLoadingPanel1.ClientID %>").style.height = document.documentElement.scrollHeight + "px";--%>
                    var loadingPanel = document.getElementById("<%= RadAjaxLoadingPanel1.ClientID %>");
                    var altoPagina = document.documentElement.scrollHeight;
                    var vistaAltura = document.documentElement.clientHeight;

                    if (altoPagina > vistaAltura) {
                        loadingPanel.style.height = altoPagina + "px";
                    }

                    var anchoPagina = document.documentElement.scrollWidth;
                    var vistaAnchura = document.documentElement.clientWidth;

                    if (anchoPagina > vistaAnchura) {
                        loadingPanel.style.width = anchoPagina + "px";
                    }

                    var scrollArriba = document.documentElement.scrollTop;
                    var scrollIzquierda = document.documentElement.scrollLeft;
                    var loadingImageWidth = 55;
                    var loadingImageHeight = 55;

                    loadingPanel.style.backgroundPosition = (parseInt(scrollIzquierda) +
                        parseInt(vistaAnchura / 2) - parseInt(loadingImageWidth / 2)) + "px "
                        + (parseInt(scrollArriba) + parseInt(vistaAltura / 2) -
                            parseInt(loadingImageHeight / 2)) + "px";

                    // por si algun soquete pone ids iguales dentro de los contenedores para sobreescribirlos

                    if (loadingPanel.nextSibling.className == loadingPanel.className) // IE, Opera
                    {
                        loadingPanel.nextSibling.style.backgroundPosition = (parseInt(scrollIzquierda) +
                            parseInt(vistaAnchura / 2) - parseInt(loadingImageWidth / 2)) + "px " +
                            (parseInt(scrollArriba) + parseInt(vistaAltura / 2) -
                                parseInt(loadingImageHeight / 2)) + "px";
                    }
                    else if (document.getElementsByClassName) // Firefox
                    {
                        var panels = document.getElementsByClassName("MyModalPanel");
                        for (var j = 0; j < panels.length; j++) {
                            panels[j].style.backgroundPosition = (parseInt(scrollIzquierda) +
                                parseInt(vistaAnchura / 2) - parseInt(loadingImageWidth / 2)) +
                                "px " + (parseInt(scrollArriba) + parseInt(vistaAltura / 2) -
                                    parseInt(loadingImageHeight / 2)) + "px";
                        }
                    }
                }
            </script>
        </telerik:RadScriptBlock>
        <section onload="RemoverPanel()">
            <div id="Bodycito" class="row MyModalPanel"></div>
            <div class="form-box" id="login-box">
                <div class="header">Acceso al Sistema</div>
                <asp:Panel ID="LoginPanel" runat="server">
                    <div class="body bg-gray">
                        <div class="form-group" runat="server" id="dvMessage" visible="false">
                            <div class="callout callout-danger">
                                <asp:Label ID="lblError" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="form-group">
                            <telerik:RadTextBox ID="rtbUser" runat="server" Skin="Bootstrap" Placeholder="Nombre de Usuario" />
                        </div>
                        <div class="form-group">
                            <telerik:RadTextBox ID="rtbPassword" runat="server" Skin="Bootstrap" TextMode="Password" Placeholder="Contraseña" />
                        </div>
                    </div>
                    <div class="footer text-right">
                        <telerik:RadButton ID="rbtAcces" runat="server" Text="Ingresar" UseSubmitBehavior="true" Skin="Bootstrap" />
                    </div>
                </asp:Panel>
            </div>
        </section>
    </form>
    <aside class="right-side">
        <section class="contenido">
            <div class="row">
                <div class="col-md-12 box-footer text-right">
                    <asp:HyperLink runat="server" NavigateUrl="~/login.aspx" class="logo4">
                        <img src="~/assets/image/logoV2.png" height="200" id="logodte" runat="server" />
                    </asp:HyperLink>
                    <br />
                    Desarrollado 2020 Ver. 2.4.3
                </div>
            </div>
        </section>
    </aside>
    <script src="<%= ResolveClientUrl("~/assets/js/jquery-2.2.3.min.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/assets/js/jquery-ui.min.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/assets/js/jquery.ui.custom.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/assets/js/bootstrap.min.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/assets/js/scripts.js") %>" type="text/javascript"></script>
</body>

</html>
