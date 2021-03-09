<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/assets/master/MasterPage.Master" CodeBehind="Albunes.aspx.vb" Inherits="Afiliacion2.Albunes" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<link href="../../assets/css/lightgallery.css" rel="stylesheet" type="text/css" />--%>
    <link href="../../assets/css/lightgallery.min.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content-header">
        <h1>Configuración Albumes
                <small>Registro para ver galeria de imagenes</small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Inicio</a></li>
            <li><a href="#"><i class="fa fa-briefcase"></i>Album</a></li>
            <li class="active">Albumes</li>
        </ol>
    </section>
    <form id="form" runat="server">
        <%--<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
            <script type="text/javascript">
                function realPostBack(eventTarget, eventArgument) {
                    $find("<%= RadAjaxPanel1.ClientID%>").__doPostBack(eventTarget, eventArgument);
                    //showDialog("divDetail");
                }
            </script>
        </telerik:RadCodeBlock>--%>
        <!--Dashboard -->
        <!-- Content Header (Page header) -->
        <%--        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
            <Scripts>
        <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
        <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
        <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
        <asp:ScriptReference Path="../../assets/js/blockUI.js" />
        </Scripts>
      </asp:ToolkitScriptManager>--%>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnRequestStart="AjustarPanel();" UpdatePanelsRenderMode="Inline">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="rbAdd">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="paDetail" />
                        <telerik:AjaxUpdatedControl ControlID="rntMetrosCuadrados" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="rbFilter">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="rgvGrid" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="rbActualizarTabla">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="rgvAdjuntos" />
                        <telerik:AjaxUpdatedControl ControlID="rtbDescripcionAdjuntos" />
                        <%--<telerik:AjaxUpdatedControl ControlID="rntMetrosCuadrados" />--%>
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="rbGuardarDescripcion">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="rgvAdjuntos" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="rbAddHotelPrograma">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="paDetailHotelPrograma" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="CargadorArchivo">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="paDetailHotelPrograma" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="rgvGrid">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="paDetail" UpdatePanelCssClass="" />
                        <telerik:AjaxUpdatedControl ControlID="Ocultito" UpdatePanelCssClass="" />
                        <%--<telerik:AjaxUpdatedControl ControlID="rptImages" UpdatePanelCssClass="" />--%>
                        <telerik:AjaxUpdatedControl ControlID="paDetailHotelPrograma" UpdatePanelCssClass="" />
                        <telerik:AjaxUpdatedControl ControlID="rgvGridHotelPrograma" UpdatePanelCssClass="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <%--   <telerik:AjaxSetting AjaxControlID="AjaxFileUpload11">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="rgvAdjuntos" UpdatePanelCssClass="" />
                        <telerik:AjaxUpdatedControl ControlID="paDetail" UpdatePanelCssClass="" />
                        <telerik:AjaxUpdatedControl ControlID="paDetail" UpdatePanelCssClass="" />
                    </UpdatedControls>                    
                </telerik:AjaxSetting>--%>
                <%--                <telerik:AjaxSetting AjaxControlID="paDetail">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="rgvAdjuntos" UpdatePanelCssClass="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>--%>
                <telerik:AjaxSetting AjaxControlID="rgvAdjuntos">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="rgvAdjuntos" UpdatePanelCssClass="" />
                        <telerik:AjaxUpdatedControl ControlID="rtbDescripcionModificar" UpdatePanelCssClass="" />
                        <telerik:AjaxUpdatedControl ControlID="rbiImagen" UpdatePanelCssClass="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <%--       <telerik:AjaxSetting AjaxControlID="RadCodeBlock1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadCodeBlock1" UpdatePanelCssClass="" />
                        <telerik:AjaxUpdatedControl ControlID="RadAjaxPanel1" UpdatePanelCssClass="" />
                        <telerik:AjaxUpdatedControl ControlID="rgvAdjunto" UpdatePanelCssClass="" />
                        <telerik:AjaxUpdatedControl ControlID="paDetail" UpdatePanelCssClass="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="RadAjaxPanel1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadCodeBlock1" UpdatePanelCssClass="" />
                        <telerik:AjaxUpdatedControl ControlID="RadAjaxPanel1" UpdatePanelCssClass="" />
                        <telerik:AjaxUpdatedControl ControlID="rgvAdjunto" UpdatePanelCssClass="" />
                        <telerik:AjaxUpdatedControl ControlID="paDetail" UpdatePanelCssClass="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="rbAdjuntar">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadCodeBlock1" UpdatePanelCssClass="" />
                        <telerik:AjaxUpdatedControl ControlID="RadAjaxPanel1" UpdatePanelCssClass="" />
                        <telerik:AjaxUpdatedControl ControlID="rgvAdjunto" UpdatePanelCssClass="" />
                        <telerik:AjaxUpdatedControl ControlID="paDetail" UpdatePanelCssClass="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>--%>
                <telerik:AjaxSetting AjaxControlID="rgvGridHotelPrograma">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="paDetailHotelPrograma" UpdatePanelCssClass="" />
                        <telerik:AjaxUpdatedControl ControlID="rntValorPuntos" />
                        <telerik:AjaxUpdatedControl ControlID="rntValor" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="paDetailHotelPrograma">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="rntValorPuntos" />
                        <telerik:AjaxUpdatedControl ControlID="rntValor" />
                        <telerik:AjaxUpdatedControl ControlID="rtbCancelHotelPrograma" UpdatePanelCssClass="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="rtbSave">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="rgvGrid" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="rtbCancel">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="rgvGrid" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="rtbSaveHotelPrograma">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="rgvGridHotelPrograma" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="rgvGrid">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="rgvGridHotelPrograma" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="rgvGrid">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="rgvGrid" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="rgvGridHotelPrograma">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="rgvGrid" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="lbNombreHotel">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="lbNombreHotel" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadWindowManager ID="RadWindowManager1" CssClass="textalignCenterMargin0" runat="server">
            <Windows>
                <telerik:RadWindow ID="RadWindow2" runat="server" Animation="Resize" Behaviors="Close, Move, Minimize, Maximize" KeepInScreenBounds="true" VisibleStatusbar="false" ViewStateMode="Enabled" Modal="true" VisibleTitlebar="true" EnableViewState="true" RenderMode="Auto" Skin="Bootstrap" CssClass="combo100porcheightToo" AutoSize="False">
                </telerik:RadWindow>
            </Windows>
        </telerik:RadWindowManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" IsSticky="true" Transparency="30" CssClass="MyModalPanel">
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
        <!-- Main content -->

        <section class="content" onload="RemoverPanel()">
            <div id="Bodycito" class="row MyModalPanel"></div>
            <div class="row">
                <div class="col-xs-12">
                    <div class="box">
                        <div class="box-header">
                            <h3 class="box-title">Listado de Albumes</h3>
                            <div class="box-tools pull-right">
                                <div class="btn-group pull-right" role="group">
                                    <telerik:RadButton ID="rbAdd" runat="server" Text="Nuevo" AutoPostBack="true" Skin="Bootstrap" OnClientClicking="onAddClicking" PressedCssClass="pace-inactive">
                                        <Icon PrimaryIconCssClass="fa fa-plus" PrimaryIconTop="10px"></Icon>
                                    </telerik:RadButton>
                                </div>
                            </div>
                        </div>
                        <div class="combo100porc">
                            <div class="row box-body">
                                <div class="col-md-6 col-md-push-6">
                                    <div class="input-group">
                                        <telerik:RadTextBox ID="rtbFilter" runat="server" Skin="Bootstrap" EmptyMessage="Buscar Album">
                                            <%--<ClientEvents OnKeyPress="OnkeyPress" />--%>
                                        </telerik:RadTextBox>
                                        <span class="input-group-btn">
                                            <telerik:RadButton ID="rbFilter" runat="server" AutoPostBack="true" Text="Buscar" Skin="Bootstrap">
                                                <Icon PrimaryIconCssClass="fa fa-list-alt" PrimaryIconTop="10px"></Icon>
                                            </telerik:RadButton>
                                        </span>
                                    </div>
                                </div>
                                <div class="col-md-6 text-right">
                                </div>
                            </div>
                            <div class="row">
                                <br />
                            </div>
                            <div class="dataTables_wrapper form-inline" role="grid">
                                <telerik:RadGrid ID="rgvGrid" runat="server" CssClass="table-responsive" AutoGenerateColumns="False" AllowPaging="True" VirtualItemCount="15" PageSize="15" Skin="Bootstrap" Culture="es-ES">
                                    <ExportSettings>
                                        <Pdf PageWidth=""></Pdf>
                                    </ExportSettings>
                                    <MasterTableView AutoGenerateColumns="False" DataKeyNames="Id" AllowCustomPaging="true" AllowPaging="true">
                                        <Columns>
                                            <telerik:GridTemplateColumn HeaderText="Acciones" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <div class="btn-group btn-group-xs" role="group">
                                                        <telerik:RadButton ID="btnEdit" CommandName="Editar" runat="server" ToolTip="Modificar" AutoPostBack="true" Skin="Bootstrap" OnClientClicking="OnEditClicking">
                                                            <Icon PrimaryIconCssClass="fa fa-pencil" />
                                                        </telerik:RadButton>
<%--                                                        <telerik:RadButton ID="btnAddHotelPrograma" CommandName="AddHotelPrograma" runat="server" ToolTip="Asignar programas" AutoPostBack="true" Skin="Bootstrap" OnClientClicking="onAddClickingHotelPrograma">
                                                            <Icon PrimaryIconCssClass="fa fa-cube" />
                                                        </telerik:RadButton>--%>
                                                        <telerik:RadButton ID="btnDelete" CommandName="Eliminar" runat="server" ToolTip="Eliminar" AutoPostBack="true" Skin="Bootstrap" OnClientClicking="ConfirmDelete">
                                                            <Icon PrimaryIconCssClass="fa fa-trash-o" />
                                                        </telerik:RadButton>
                                                        <telerik:RadButton ID="btnGaleria" CommandName="Galeria" runat="server" ToolTip="Ver Imagenes" AutoPostBack="true" Skin="Bootstrap">
                                                            <Icon PrimaryIconCssClass="fa fa-picture-o" />
                                                        </telerik:RadButton>
                                                    </div>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Nro." UniqueName="ItemIndex" ItemStyle-Font-Bold="True">
                                                <ItemTemplate>
                                                    <%# (rgvGrid.MasterTableView.CurrentPageIndex * rgvGrid.MasterTableView.PageSize) + Container.ItemIndex + 1 %>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridDateTimeColumn DataField="Nombre" HeaderText="Album" UniqueName="Nombre" HeaderStyle-HorizontalAlign="Center" />
                                            <telerik:GridBoundColumn DataField="Descripcion" HeaderText="Descripcion" UniqueName="Descripcion" HeaderStyle-HorizontalAlign="Center" />
                                        </Columns>
                                        <NoRecordsTemplate>
                                            <asp:Label ID="lbEmpty" runat="server" Text="No existen datos." Width="100%"></asp:Label>
                                        </NoRecordsTemplate>
                                        <PagerStyle AlwaysVisible="True" FirstPageToolTip="Primera Pagina" LastPageToolTip="Ultima Pagina" NextPageToolTip="Siguiente" PagerTextFormat="Change page: {4} &amp;nbsp;Pagina &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, Registros &lt;strong&gt;{2}&lt;/strong&gt; al &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." PageSizeControlType="RadDropDownList" PrevPageToolTip="Anterior" />
                                    </MasterTableView>
                                </telerik:RadGrid>
                                <asp:HiddenField runat="server" ID="Ocultito" Value="" />
                                <%-- <telerik:RadBinaryImage ID="rbiImagen" runat="server"/>
                                <asp:AjaxFileUpload ID="file" runat="server" Visible="false" />
                                <telerik:RadUpload ID="file2" runat="server" Visible="false" ></telerik:RadUpload>
                                <asp:FileUpload ID="file3" runat="server" Visible="false" />--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <!-- Modal Registrar Hotel -->
        <div id="divDetail" class="dialog" style="display: none;">
            <asp:Panel ID="paDetail" runat="server">
                <table style="width: 100%">
                    <tr>
                        <td class="combo25porc">Nombre del Album
                        </td>
                        <td>
                            <telerik:RadTextBox ID="rtbNombre" AutoPostBack="true" CssClass="combo100porc" runat="server" MaxLength="150" EmptyMessage="Ingrese el nombre del album" Skin="Bootstrap" />
                        </td>
                    </tr>
                    <tr>
                        <td class="combo25porc">Descripcion del Album
                        </td>
                        <td>
                            <telerik:RadTextBox ID="rtbDescripcionAlbum" CssClass="combo100porc" runat="server" MaxLength="150" EmptyMessage="Ingrese una descripcion del album" Skin="Bootstrap" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div class="row"></div>
                            <div class="btn-group pull-left combo100porc" role="group">
                                <div class="form-group combo100porc">
                                    <%--<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">--%>
                                    <%--  <telerik:RadLabel runat="server" ID="rlbAdjuntar" CssClass="paddingtop4pixl" Text="Adjuntar Archivo"></telerik:RadLabel>
                                            <asp:FileUpload ID="CargadorArchivo" runat="server" EnableViewState="false" ></asp:FileUpload>
                                    <asp:FileUpload id="aqui" runat="server" ></asp:FileUpload>
                                    <asp:FileUpload ID="FileUpload1" runat="server" />
                                <hr />
                                <asp:Button ID="btnUpload" Text="Upload" runat="server" OnClick="UploadFile" />
                                <br />
                                <asp:Label ID="lblMessage" ForeColor="Green" runat="server" />
                                    <telerik:RadButton ID="rbAdjuntar" runat="server" Text="Agregar" CssClass="floatleft" Skin="Bootstrap" OnClick="rbAdjuntar_Click">
                                        <Icon PrimaryIconCssClass="fa fa-save" PrimaryIconTop="10px" />
                                    </telerik:RadButton>--%>
                                    <asp:AjaxFileUpload ID="AjaxFileUpload11" EnableTheming="true" runat="server" MaximumNumberOfFiles="10000"
                                        CssClass="combo100porc" OnUploadComplete="OnUploadComplete"></asp:AjaxFileUpload>
                                    <telerik:RadTextBox runat="server" ID="rtbDescripcionAdjuntos" Width="100%" EmptyMessage="Ingrese la descripcion de los archivos a adjuntar" TextMode="MultiLine" ToolTip="Descripcion de archicos adjuntos" RenderMode="Lightweight"></telerik:RadTextBox>
                                    <%--</div>--%>
                                    <%--  <telerik: RenderMode="Lightweight" CssClass="floatleft" runat="server" ID="CargadorArchivo" TargetFolder="../../assets/img"
                        MaxFileInputsCount="1" AllowedFileExtensions=".jpg"  PostbackTriggers="RadButton1" Skin="Default" >
                    </telerik:>--%>
                                    <%--</telerik:RadAjaxPanel>--%>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <%--Radgrid donde se colocaran los datos adjuntos--%>
                            <div class="dataTables_wrapper form-inline" role="grid">
                                <telerik:RadGrid ID="rgvAdjuntos" runat="server" CssClass="table-responsive" AutoGenerateColumns="False" GridLines="None" VirtualItemCount="5" CellSpacing="0" Skin="Bootstrap" PageSize="5">
                                    <MasterTableView AutoGenerateColumns="False" DataKeyNames="Id" AllowCustomPaging="True" AllowPaging="True">
                                        <Columns>
                                            <telerik:GridTemplateColumn HeaderText="" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <div class="btn-group btn-group-xs" role="group">
                                                        <telerik:RadButton ID="btnDelete" CommandName="EliminarAdjunto" runat="server" ToolTip="Eliminar adjunto" AutoPostBack="true" Skin="Bootstrap" OnClientClicking="ConfirmDeleteAdjunto">
                                                            <Icon PrimaryIconCssClass="fa fa-trash-o" />
                                                        </telerik:RadButton>
                                                        <telerik:RadButton ID="btnEditar" CommandName="EditarDescripcion" runat="server" ToolTip="Modificar descripción" AutoPostBack="true" Skin="Bootstrap" OnClientClicking="AbrirDescripcion">
                                                            <Icon PrimaryIconCssClass="fa fa-pencil" />
                                                        </telerik:RadButton>
                                                        <telerik:RadButton ID="btnAbrirArchivo" CommandName="Abrir" runat="server" ToolTip="Abrir adjunto" AutoPostBack="true" Skin="Bootstrap">
                                                            <Icon PrimaryIconCssClass="fa fa-picture-o" />
                                                        </telerik:RadButton>
                                                    </div>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Nro." UniqueName="ItemIndex" ItemStyle-Font-Bold="True">
                                                <ItemTemplate>
                                                    <%# (rgvAdjuntos.MasterTableView.CurrentPageIndex * rgvAdjuntos.MasterTableView.PageSize) + Container.ItemIndex + 1 %>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridDateTimeColumn DataField="Descripcion" HeaderText="Descripción" UniqueName="Descripcion" HeaderStyle-HorizontalAlign="Center" />
                                            <telerik:GridBoundColumn DataField="Url" HeaderText="Archivo adjunto" UniqueName="Url" HeaderStyle-HorizontalAlign="Center" />
                                            <%-- <telerik:GridBoundColumn DataField="Archivo" HeaderText="Archivo" UniqueName="Archivo2" Display="false" HeaderStyle-HorizontalAlign="Center" />
                                            <telerik:GridAttachmentColumn HeaderText="Archivo" AttachmentDataField="BinaryData" AttachmentKeyFields="Archivo" FileNameTextField="Archivo" MaxFileSize="100048576" UniqueName="Archivo"></telerik:GridAttachmentColumn>--%>
                                        </Columns>
                                        <NoRecordsTemplate>
                                            <asp:Label ID="lbEmpty" runat="server" Text="No existen datos." Width="100%"></asp:Label>
                                        </NoRecordsTemplate>
                                        <PagerStyle AlwaysVisible="True" FirstPageToolTip="Primera página" LastPageToolTip="Ultima página" NextPageToolTip="Siguiente" PagerTextFormat="Change page: {4} &amp;nbsp;Pagina &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, Registros &lt;strong&gt;{2}&lt;/strong&gt; al &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." PageSizeControlType="RadDropDownList" PrevPageToolTip="Anterior" />
                                    </MasterTableView>
                                </telerik:RadGrid>
                                <asp:HiddenField runat="server" ID="ocultin" />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="combo100porc">
                            <br />
                            <div class="btn-group pull-right combo100porc" role="group">
                                <telerik:RadButton ID="rbActualizarTabla" runat="server" Text="Actualizar tabla" CssClass="btn-extra" AutoPostBack="true" Skin="Bootstrap" OnClientClicking="LimpiarMyFileUpload">
                                    <Icon PrimaryIconCssClass="fa fa-refresh" PrimaryIconTop="10px" />
                                </telerik:RadButton>
                                <telerik:RadButton ID="rtbSave" runat="server" Text="Guardar" AutoPostBack="true" Skin="Bootstrap">
                                    <Icon PrimaryIconCssClass="fa fa-save" PrimaryIconTop="10px" />
                                </telerik:RadButton>
                                <telerik:RadButton ID="rtbCancel" runat="server" Text="Cancelar" AutoPostBack="true" Skin="Bootstrap" CssClass="btn-danger" OnClientClicking="onCancelClicking">
                                    <Icon PrimaryIconCssClass="fa fa-times" PrimaryIconTop="10px" />
                                </telerik:RadButton>
                            </div>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
        <!-- Modal Registrar Hotel Programa -->
        
        <div id="divDescripcion" class="dialog combo100porc" style="display: none;">
            <section class="content-header">
                <h1>
                    <asp:Label ID="Label1" runat="server" Text="Ingrese una descripcion" Width="100%"></asp:Label>
                </h1>
            </section>
            <asp:Panel ID="paDetailDescripcion" runat="server">
                <div>
                    <telerik:RadTextBox runat="server" CssClass="combo100porc" Label="Descripcion" ID="rtbDescripcionModificar" TextMode="MultiLine" Width="100%" RenderMode="Lightweight"></telerik:RadTextBox>
                </div>
                <div class="btn-group pull-right" role="group">
                    <telerik:RadButton ID="rbGuardarDescripcion" runat="server" Text="Guardar" AutoPostBack="true" Skin="Bootstrap">
                        <Icon PrimaryIconCssClass="fa fa-save" PrimaryIconTop="10px" />
                    </telerik:RadButton>
                    <telerik:RadButton ID="rbCancelarDescripcion" runat="server" Text="Cancelar" AutoPostBack="true" Skin="Bootstrap" CssClass="btn-danger" OnClientClicking="CerrarDescripcion">
                        <Icon PrimaryIconCssClass="fa fa-times" PrimaryIconTop="10px" />
                    </telerik:RadButton>
                </div>
            </asp:Panel>
        </div>
        <%--<div class="demo-container no-bg" id="offsetElement">
            <telerik:RadSkinManager ID="RadSkinManager1" runat="server" Skin="Glow" />
        <script type="text/javascript">            
                Sys.Application.add_load(function () {
                    $autoSizeDemo.galleryWin = $find($("[id$='GalleryWindow']").attr("id"));
                    $autoSizeDemo.previewWin = $find($("[id$='PreviewWindow']").attr("id")); 
                //get a reference to the image tag in the preview window
                $autoSizeDemo.imgHolder = $get("imageHolder");
                //add onload event for the image in the preview window
                $addHandler($autoSizeDemo.imgHolder, "load", sizePreviewWindow);
            })
        </script>
            <telerik:RadWindow RenderMode="Lightweight" ID="PreviewWindow"  runat="server" VisibleStatusbar="false" VisibleTitlebar="true"
                OffsetElementID="offsetElement" Top="-10" Left="10" Title="Hotel Pucon"
                AutoSize="true" KeepInScreenBounds="false" Skin="Sunset">
            <ContentTemplate>
                <img src="javascript:void(0);" alt="Image holder" id="imageHolder" />
            </ContentTemplate>
        </telerik:RadWindow>
        <telerik:RadWindow RenderMode="Lightweight" ID="GalleryWindow" VisibleOnPageLoad="true" runat="server" Title="Visor de Imagenes"
            VisibleStatusbar="false" AutoSize="true" Top="10" Left="10" AutoSizeBehaviors="Height, Width" OffsetElementID="offsetElement"
            IconUrl="~/assets/image/Hotel_Pucon/enjoy2.gif" >
            <ContentTemplate>
                <div style="width: 310px">
                    <h1 id="ThumbsTitle">Fotos del Hotel</h1>
                    <p id="ThumbsDescr">
                        Clic sobre la imagen para su tamaño real.
                    </p>
                    <ul class="thumbPane">
                        <li>
                            <img src="../../assets/image/Hotel_Pucon/1.jpg" alt="imagen 1" onclick="openWin(this); return false;" width="92" height="92" />
                        </li>
                        <li>
                            <img src="../../assets/image/Hotel_Pucon/3.jpg" alt="imagen 2" onclick="openWin(this); return false;" width="92" height="92" />
                        </li>
                        <li>
                            <img src="../../assets/image/Hotel_Pucon/2.jpg" alt="imagen 3" onclick="openWin(this); return false;" width="92" height="92" />
                        </li>
                        <li>
                            <img src="../../assets/image/Hotel_Pucon/5.jpg" alt="imagen 4" onclick="openWin(this); return false;" width="92" height="92" />
                        </li>
                        <li>
                            <img src="../../assets/image/Hotel_Pucon/4.jpg" alt="imagen 5" onclick="openWin(this); return false;" width="92" height="92" />
                        </li>
                        <li>
                            <img src="../../assets/image/Hotel_Pucon/6.jpg" alt="imagen 6" onclick="openWin(this); return false;" width="92" height="92" />
                        </li>
                        <li>
                            <img src="../../assets/image/Hotel_Pucon/7.jpg" alt="imagen 7" onclick="openWin(this); return false;" width="92" height="92" />
                        </li>
                        <li>
                            <img src="../../assets/image/Hotel_Pucon/10.jpg" alt="imagen 8" onclick="openWin(this); return false;" width="92" height="92" />
                        </li>
                        <li>
                            <img src="../../assets/image/Hotel_Pucon/8.jpg" alt="imagen 9" onclick="openWin(this); return false;" width="92" height="92" />
                        </li>
                    </ul>
                    <div id="hiddenPane" style="display: none">
                        <ul class="thumbPane">
                            <li>
                                <img src="../../assets/image/Hotel_Pucon/11.jpg" alt="imagen 10" onclick="openWin(this); return false;" width="92" height="92" />
                            </li>
                            <li>
                                <img src="../../assets/image/Hotel_Pucon/9.jpg" alt="imagen 11" onclick="openWin(this); return false;" width="92" height="92" />
                            </li>
                            <li>
                                <img src="../../assets/image/Hotel_Pucon/12.jpg" alt="imagen 12" onclick="openWin(this); return false;" width="92" height="92" />
                            </li>
                            <li>
                                <img src="../../assets/image/Hotel_Pucon/13.jpg" alt="imagen 13" onclick="openWin(this); return false;" width="92" height="92" />
                            </li>
                            <li>
                                <img src="../../assets/image/Hotel_Pucon/14.jpg" alt="imagen 14" onclick="openWin(this); return false;" width="92" height="92" />
                            </li>
                            <li>
                                <img src="../../assets/image/Hotel_Pucon/15.jpg" alt="imagen 15" onclick="openWin(this); return false;" width="92" height="92" />
                            </li>
                        </ul>
                    </div>
                    <div style="clear: both; padding: 5px 5px 10px 5px;">
                        <a id="toggleLink" href="javascript:void(0);" class="toggleLink" onclick="toggleExpand(this); return false;">Mostrar Todos</a>
                    </div>
                </div>
            </ContentTemplate>
        </telerik:RadWindow>
    </div>--%>
        <%--        <div id="lightgallery2">
            <div id="aqui" class="dialog">
            <asp:HyperLink runat="server" NavigateUrl="../../assets/image/Hotel_Pucon/10.jpg">
                <img class="img-responsive" src="../../assets/image/Hotel_Pucon/10.jpg" />
            </asp:HyperLink>
            <asp:HyperLink runat="server" NavigateUrl="../../assets/image/Hotel_Pucon/11.jpg">
                <img class="img-responsive" src="../../assets/image/Hotel_Pucon/11.jpg" />
            </asp:HyperLink>
            <asp:HyperLink runat="server" NavigateUrl="../../assets/image/Hotel_Pucon/12.jpg">
                <img class="img-responsive" src="../../assets/image/Hotel_Pucon/12.jpg" />
            </asp:HyperLink>
            </div>
        </div>--%>
        <div id="divDetail3">
        </div>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script type="text/javascript" src="Scripts/HotelPrograma.js"></script>
    <%--<script type="text/javascript" src="Scripts/Hotel.js"></script>--%>
    <%--<script type="text/javascript" src="Scripts/HotelPrograma.js"></script>--%>
    <%--   <script type="text/javascript" src="../../assets/js/jquery-ui.min.js"></script>
    <script type="text/javascript" src="../../assets/js/jquery.1.11.2.js"></script>
    <script type="text/javascript" src="../../assets/js/jquery.min.js"></script>
    <script type="text/javascript" src="../../assets/js/jquery.slimscroll.min.js"></script>
    <script type="text/javascript" src="../../assets/js/jquery.ui.custom.js"></script>
    <script type="text/javascript" src="../../assets/js/app.js"></script>--%>
    <%--<script type="text/javascript" src="../../assets/jsPlugin/lightgalery.js"></script>--%>
    <script type="text/javascript" src="../../assets/jsPlugin/lightgallery.js"></script>
    <script type="text/javascript" src="../../assets/jsPlugin/picturefill.js"></script>
    <script type="text/javascript" src="../../assets/jsPlugin/mousewheel.min.js"></script>
    <script type="text/javascript" src="../../assets/jsPlugin/lg-fullscreen.min.js"></script>
    <script type="text/javascript" src="../../assets/jsPlugin/lg-thumbnail.min.js"></script>
    <script type="text/javascript" src="../../assets/jsPlugin/lg-video.min.js"></script>
    <script type="text/javascript" src="../../assets/jsPlugin/lg-autoplay.min.js"></script>
    <script type="text/javascript" src="../../assets/jsPlugin/lg-zoom.min.js"></script>
    <script type="text/javascript" src="../../assets/jsPlugin/lg-hash.min.js"></script>
    <script type="text/javascript" src="../../assets/jsPlugin/lg-pager.min.js"></script>
    <script type="text/javascript">
</script>
</asp:Content>
