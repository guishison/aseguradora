<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/assets/master/MasterPage.Master" CodeBehind="HelpDesk.aspx.vb" Inherits="Afiliacion2.HelpDesk" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!--Dashboard -->
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>Registro de incidencias
            <small>Atención y soporte a usuarios</small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Inicio</a></li>
            <li><a href="#"><i class="fa fa-dashboard"></i>Help desk</a></li>
            <li class="active">Registro de incidencias
            </li>
        </ol>
    </section>
    <form id="form" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnRequestStart="AjustarPanel();" UpdatePanelsRenderMode="Inline">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="rbAdd">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="paDetail" UpdatePanelCssClass="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="rbGuardarAdjunto">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="rgvGrid" UpdatePanelCssClass="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="rgvGrid">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="paDetail" UpdatePanelCssClass="" />
                        <telerik:AjaxUpdatedControl ControlID="rgvAdjuntos" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="rgvAdjuntos">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="rtbDescripcionModificar" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="rcbEstadoBusqueda">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="rgvGrid" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="rbGuardarDescripcion">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="rgvAdjuntos" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="rtbSave">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="rgvGrid" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="rbFilter">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="rgvGrid" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="rcbModulo">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="rcbFormulario" UpdatePanelCssClass="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="SuperImagen">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="SuperImagen" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <div id="window-container">
            <telerik:RadWindowManager ID="RadWindowManager1" CssClass="textalignCenterMargin0" runat="server">
                <Windows>
                    <telerik:RadWindow ID="RadWindow2" runat="server" Behaviors="Close, Move, Minimize, Maximize" KeepInScreenBounds="true" VisibleStatusbar="false" ViewStateMode="Enabled" Modal="true" VisibleTitlebar="true" EnableViewState="true" OffsetElementID="window-container" RenderMode="Auto" Title="HelpDesk.png" Skin="Bootstrap" CssClass="combo100porcheightToo" AutoSize="False">
                    </telerik:RadWindow>
                </Windows>
            </telerik:RadWindowManager>
        </div>
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
                            <h3 class="box-title">Lista de incidencias
                                <br />
                                <small>Utilice los filtros para buscar incidencias de fechas pasadas.</small>
                            </h3>
                            <div class="box-tools pull-right">
                                <div class="btn-group pull-right" role="group">
                                    <telerik:RadButton ID="rbAdd" runat="server" Text="Nuevo" AutoPostBack="true" OnClientClicking="onAddClicking" Skin="Bootstrap">
                                        <Icon PrimaryIconCssClass="fa fa-plus" PrimaryIconTop="12px"></Icon>
                                    </telerik:RadButton>
                                </div>
                            </div>
                        </div>
                        <asp:HiddenField ID="SuperOculto" runat="server" />
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="input-group">
                                        <div class="input-group-btn">
                                            <telerik:RadLabel runat="server" ID="lblIniciall" Text="Desde:"></telerik:RadLabel>
                                        </div>
                                        <telerik:RadDatePicker ID="rdpInicial" runat="server" CssClass="combo100porc" Skin="Bootstrap"></telerik:RadDatePicker>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="input-group">
                                        <div class="input-group-btn">
                                            <telerik:RadLabel runat="server" ID="lblFinal" Text="Hasta:"></telerik:RadLabel>
                                        </div>
                                        <telerik:RadDatePicker ID="rdpFinal" runat="server" CssClass="combo100porc" Skin="Bootstrap"></telerik:RadDatePicker>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="input-group">
                                        <div class="input-group-btn">
                                            <telerik:RadLabel runat="server" ID="lblEstadoCombo" Text="Filtrar estado:"></telerik:RadLabel>
                                        </div>
                                        <telerik:RadComboBox ID="rcbEstadoBusqueda" runat="server" AutoPostBack="true" CssClass="combo100porc" OnClientKeyPressing="radComboKeyPress" MaxHeight="150" EnableTextSelection="true" MarkFirstMatch="true" DropDownAutoWidth="Enabled" Filter="Contains" Skin="Bootstrap"></telerik:RadComboBox>
                                    </div>
                                </div>
                                <div class="col-md-3 text-right">
                                    <div class="input-group">
                                        <telerik:RadTextBox ID="rtbFilter" runat="server" Skin="Bootstrap" Placeholder="Ingrese el texto...">
                                        </telerik:RadTextBox>
                                        <span class="input-group-btn">
                                            <telerik:RadButton ID="rbFilter" runat="server" Text="Buscar" UseSubmitBehavior="true" Skin="Bootstrap" />
                                        </span>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <br />
                            </div>
                            <telerik:RadBinaryImage runat="server" ID="SuperImagen2" />
                            <img runat="server" id="SuperImagen" alt="imagen prueba" visible="false" src="~/assets/image/HelpDesk.png" />
                            <div class="dataTables_wrapper form-inline" role="grid">
                                <telerik:RadGrid ID="rgvGrid" CssClass="table-responsive" runat="server" AutoGenerateColumns="false" GridLines="None" VirtualItemCount="5" CellSpacing="0" Skin="Bootstrap" PageSize="10">
                                    <MasterTableView AutoGenerateColumns="False" DataKeyNames="Id" AllowCustomPaging="true" AllowPaging="true">
                                        <Columns>
                                            <telerik:GridTemplateColumn HeaderText="Acciones" HeaderStyle-Width="60" HeaderStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <div class="btn-group btn-group-xs" role="group">
                                                        <telerik:RadButton ID="btnEdit" CommandName="EditPosition" runat="server" AutoPostBack="true" ToolTip="Responder" OnClientClicking="OnEditPosition" Skin="Bootstrap">
                                                            <Icon PrimaryIconCssClass="fa fa-pencil" PrimaryIconLeft="15px" PrimaryIconTop="6px"></Icon>
                                                        </telerik:RadButton>
                                                        <%--<telerik:RadButton ID="btnDelete" CommandName="DeletePosition" runat="server" AutoPostBack="true" OnClientClicking="ConfirmDelete" Skin="Bootstrap">
                                                            <Icon PrimaryIconCssClass="fa fa-trash-o" PrimaryIconLeft="15px" PrimaryIconTop="6px"></Icon>
                                                        </telerik:RadButton>--%>
                                                    </div>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Nro." UniqueName="ItemIndex" ItemStyle-Font-Bold="True" HeaderStyle-Width="75px">
                                                <ItemTemplate>
                                                    <%# (rgvGrid.MasterTableView.CurrentPageIndex * rgvGrid.MasterTableView.PageSize) + Container.ItemIndex + 1 %>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="Id" HeaderText="Code" UniqueName="Id" Display="false" />
                                            <telerik:GridBoundColumn DataField="FechaReg" HeaderText="Emisión" UniqueName="Fecha" DataType="System.DateTime" DataFormatString="{0:dd MMMM yyyy HH:mm}" HeaderStyle-HorizontalAlign="Center" />
                                            <telerik:GridBoundColumn DataField="Personal.Nombre" HeaderText="Reportado por" UniqueName="Personal" HeaderStyle-HorizontalAlign="Center" />
                                            <telerik:GridBoundColumn DataField="TipoTicket.Nombre" HeaderText="Tipo ticket" UniqueName="TipoTicket" HeaderStyle-HorizontalAlign="Center" />
                                            <telerik:GridBoundColumn DataField="Modulo.Nombre" HeaderText="Módulo" UniqueName="Modulo" HeaderStyle-HorizontalAlign="Center" />
                                            <telerik:GridBoundColumn DataField="Formulario.Nombre" HeaderText="Formulario" UniqueName="Formulario" HeaderStyle-HorizontalAlign="Center" />
                                            <telerik:GridBoundColumn DataField="Prioridad.Nombre" HeaderText="Prioridad" UniqueName="Prioridad" HeaderStyle-HorizontalAlign="Center" />
                                            <telerik:GridBoundColumn DataField="EstadoHelpDesk.Nombre" HeaderText="Estado" UniqueName="EstadoHelpDesk" HeaderStyle-HorizontalAlign="Center" />
                                        </Columns>
                                        <NoRecordsTemplate>
                                            <asp:Label ID="lbEmpty" runat="server" Text="No existen datos." Width="100%"></asp:Label>
                                        </NoRecordsTemplate>
                                        <PagerStyle AlwaysVisible="True" FirstPageToolTip="Primera Pagina" LastPageToolTip="Ultima Pagina" NextPageToolTip="Siguiente" PagerTextFormat="Change page: {4} &amp;nbsp;Pagina &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, Registros &lt;strong&gt;{2}&lt;/strong&gt; al &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." PageSizeControlType="RadDropDownList" PrevPageToolTip="Anterior" />
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </div>
                            <div id="divDescripcion" class="dialog" style="display: none;">
                                <asp:Panel ID="paDetailDescripcion" runat="server">
                                    <table style="width: 100%">
                                        <tr>
                                            <td>
                                                <telerik:RadLabel ID="rlDescripcionAdjunto" Text="Ingrese una Descripcion" runat="server" Skin="Bootstrap"></telerik:RadLabel>
                                            </td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                    </table>
                                    <div>
                                        <telerik:RadTextBox runat="server" Label="Descripcion" ID="rtbDescripcionModificar" TextMode="MultiLine" Width="100%" RenderMode="Lightweight"></telerik:RadTextBox>
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
                            <div id="divDetail" class="dialog" style="display: none;">
                                <asp:Panel ID="paDetail" runat="server">
                                    <table style="width: 100%">
                                        <tr>
                                            <td>Módulo:
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="rcbModulo" runat="server" AutoPostBack="true" CssClass="combo100porc" Skin="Bootstrap" OnClientKeyPressing="radComboKeyPress" MaxHeight="150" EnableTextSelection="true" MarkFirstMatch="true" DropDownAutoWidth="Enabled" Filter="Contains" />
                                            </td>
                                            <td>Formulario:
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="rcbFormulario" runat="server" CssClass="combo100porc" Skin="Bootstrap" OnClientKeyPressing="radComboKeyPress" MaxHeight="150" EnableTextSelection="true" MarkFirstMatch="true" DropDownAutoWidth="Enabled" Filter="Contains" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Tipo de ticket:
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="rcbTipoTicket" runat="server" CssClass="combo100porc" Skin="Bootstrap" OnClientKeyPressing="radComboKeyPress" MaxHeight="150" EnableTextSelection="true" MarkFirstMatch="true" DropDownAutoWidth="Enabled" Filter="Contains" />
                                            </td>
                                            <td>Prioridad:
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="rcbPrioridad" runat="server" Skin="Bootstrap" CssClass="combo100porc" OnClientKeyPressing="radComboKeyPress" MaxHeight="150" EnableTextSelection="true" MarkFirstMatch="true" DropDownAutoWidth="Enabled" Filter="Contains" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Estado:
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="rcbEstadoHelpDesk" runat="server" Skin="Bootstrap" CssClass="combo100porc" OnClientKeyPressing="radComboKeyPress" MaxHeight="150" EnableTextSelection="true" MarkFirstMatch="true" DropDownAutoWidth="Enabled" Filter="Contains" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Comentario:
                                            </td>
                                            <td colspan="3">
                                                <telerik:RadTextBox ID="rtbComentario" runat="server" Skin="Bootstrap" TextMode="MultiLine" RenderMode="Lightweight" />
                                            </td>
                                        </tr>
                                    </table>
                                    <div class="combo100porc">
                                        <asp:AjaxFileUpload ID="AjaxFileUpload11" EnableTheming="true" runat="server" MaximumNumberOfFiles="10"
                                            CssClass="combo100porc" OnUploadComplete="OnUploadComplete"></asp:AjaxFileUpload>
                                        <telerik:RadTextBox runat="server" ID="rtbDescripcionAdjuntos" Width="100%" EmptyMessage="Ingrese la descripcion de los archivos a adjuntar" TextMode="MultiLine" ToolTip="Descripcion de Datos Adjuntos" RenderMode="Lightweight"></telerik:RadTextBox>
                                    </div>
                                    <div class="dataTables_wrapper form-inline" role="grid">
                                        <telerik:RadGrid ID="rgvAdjuntos" runat="server" CssClass="table-responsive" AutoGenerateColumns="False" GridLines="None" VirtualItemCount="5" CellSpacing="0" Skin="Bootstrap" PageSize="5">
                                            <MasterTableView AutoGenerateColumns="False" DataKeyNames="Id" AllowCustomPaging="True" AllowPaging="True">
                                                <Columns>
                                                    <telerik:GridTemplateColumn HeaderText="" HeaderStyle-Width="70" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <div class="btn-group btn-group-xs" role="group">
                                                                <%--<telerik:RadButton ID="btnDelete" CommandName="EliminarAdjunto" runat="server" ToolTip="Eliminar" AutoPostBack="true" Skin="Bootstrap" OnClientClicking="ConfirmDeleteAdjunto">
                                                                    <Icon PrimaryIconCssClass="fa fa-trash-o" />
                                                                </telerik:RadButton>--%>
                                                                <telerik:RadButton ID="btnEditar" CommandName="EditarDescripcion" runat="server" ToolTip="Modificar" AutoPostBack="true" Skin="Bootstrap" OnClientClicking="AbrirDescripcion">
                                                                    <Icon PrimaryIconCssClass="fa fa-pencil" />
                                                                </telerik:RadButton>
                                                                <telerik:RadButton ID="btnAbrirArchivo" CommandName="Abrir" runat="server" ToolTip="Abrir archivo" AutoPostBack="true" Skin="Bootstrap">
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
                                                    <telerik:GridDateTimeColumn DataField="NombreArchivo" HeaderText="Nombre archivo" UniqueName="Nombre" HeaderStyle-HorizontalAlign="Center" />
                                                    <telerik:GridDateTimeColumn DataField="Descripcion" HeaderText="Descripción" UniqueName="Descripcion" HeaderStyle-HorizontalAlign="Center" />
                                                    <telerik:GridBoundColumn DataField="Url" HeaderText="Url" UniqueName="Url" HeaderStyle-HorizontalAlign="Center" />
                                                    <telerik:GridBoundColumn DataField="Fechareg" HeaderText="Fecha y Hora" UniqueName="Fecha" DataType="System.DateTime" DataFormatString="{0:dd/MM/yyyy HH:mm}" HeaderStyle-HorizontalAlign="Center" />
                                                    <%-- <telerik:GridBoundColumn DataField="Archivo" HeaderText="Archivo" UniqueName="Archivo2" Display="false" HeaderStyle-HorizontalAlign="Center" />
                                            <telerik:GridAttachmentColumn HeaderText="Archivo" AttachmentDataField="BinaryData" AttachmentKeyFields="Archivo" FileNameTextField="Archivo" MaxFileSize="100048576" UniqueName="Archivo"></telerik:GridAttachmentColumn>--%>
                                                </Columns>
                                                <NoRecordsTemplate>
                                                    <asp:Label ID="lbEmpty" runat="server" Text="No existen datos." Width="100%"></asp:Label>
                                                </NoRecordsTemplate>
                                                <PagerStyle AlwaysVisible="True" FirstPageToolTip="Primera Pagina" LastPageToolTip="Ultima Pagina" NextPageToolTip="Siguiente" PagerTextFormat="Change page: {4} &amp;nbsp;Pagina &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, Registros &lt;strong&gt;{2}&lt;/strong&gt; al &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." PageSizeControlType="RadDropDownList" PrevPageToolTip="Anterior" />
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                        <asp:HiddenField runat="server" ID="ocultin" />
                                    </div>
                                    <div class="btn-group pull-right" role="group">
                                        <telerik:RadButton ID="rbActualizarTabla" runat="server" Text="Actualizar tabla" CssClass="btn-extra" SingleClick="true" AutoPostBack="true" Skin="Bootstrap" OnClientClicking="LimpiarMyFileUpload">
                                            <Icon PrimaryIconCssClass="fa fa-refresh" PrimaryIconTop="10px" />
                                        </telerik:RadButton>
                                        <telerik:RadButton ID="rbGuardarAdjunto" runat="server" Text="Guardar" AutoPostBack="true" Skin="Bootstrap" SingleClick="true" OnClientClicking="funValidate">
                                            <Icon PrimaryIconCssClass="fa fa-save" PrimaryIconTop="10px" />
                                        </telerik:RadButton>
                                        <telerik:RadButton ID="rbCancelarAdjunto" runat="server" Text="Cancelar" AutoPostBack="true" Skin="Bootstrap" CssClass="btn-danger" OnClientClicking="onCancelAdjunto">
                                            <Icon PrimaryIconCssClass="fa fa-times" PrimaryIconTop="10px" />
                                        </telerik:RadButton>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script type="text/javascript" src="Scripts/ScriptGlobal.js"></script>
    <%--<script type="text/javascript" src="../../assets/js/bililiteRange.js"></script>--%>
</asp:Content>
