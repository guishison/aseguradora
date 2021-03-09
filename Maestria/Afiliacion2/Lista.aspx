<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Lista.aspx.vb" Inherits="Afiliacion2.Lista" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">    
    <link rel="icon" href="assets/image/logo2.ico" />
    <link href="assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/css/font-awesome2.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/css/ionicons2.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/css/jquery.ui.custom.css" rel="stylesheet" type="text/css" />
    <link href="assets/css/lightgallery.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/css/stylonuevo.css"  rel="stylesheet"  type="text/css" />
    <script type="text/javascript" src="assets/js/jquery-2.2.3.min.js"></script>
    <script type="text/javascript" src="assets/js/jquery-ui.min.js"></script>
    <script type="text/javascript" src="assets/js/jquery.min.js"></script>
    <script type="text/javascript" src="assets/js/jquery.ui.custom.js"></script>
    <script type="text/javascript" src="assets/js/bootstrap.min.js"></script>
    <script type="text/javascript" src="assets/js/app.js"></script>
    <script type="text/javascript" src="assets/js/jquery.slimscroll.min.js"></script>
    <script type="text/javascript" src="assets/js/scripts.js"></script>
    <script type="text/javascript" src="assets/js/jquery.linq.js"></script>
    <script type="text/javascript" src="assets/js/icheck.min.js"></script>
    <script type="text/javascript" src="assets/js/html2canvas.js"></script>
    <script type="text/javascript" src="assets/jsPlugin/lightgallery.js"></script>
    <script type="text/javascript" src="assets/jsPlugin/picturefill.js"></script>
    <script type="text/javascript" src="assets/jsPlugin/mousewheel.min.js"></script>
    <script type="text/javascript" src="assets/jsPlugin/lg-fullscreen.min.js"></script>
    <script type="text/javascript" src="assets/jsPlugin/lg-thumbnail.min.js"></script>
    <script type="text/javascript" src="assets/jsPlugin/lg-video.min.js"></script>
    <script type="text/javascript" src="assets/jsPlugin/lg-autoplay.min.js"></script>
    <script type="text/javascript" src="assets/jsPlugin/lg-zoom.min.js"></script>
    <script type="text/javascript" src="assets/jsPlugin/lg-hash.min.js"></script>
    <script type="text/javascript" src="assets/jsPlugin/lg-pager.min.js"></script>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
    <telerik:RadSkinManager ID="RadSkinManager1" runat="server" ShowChooser="false" />
  
    <div class="floatleft">
                    
        <div id="divDetail4" runat="server">
        </div>
        </div>
    </div>
        <div id="divDetail3">
        </div>
    </form>
</body>
    
</html>
