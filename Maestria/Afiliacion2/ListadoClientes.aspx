<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ListadoClientes.aspx.vb" Inherits="FacturacionRegistro.ListadoClientes" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="icon" type="image/gif" href="login.gif" />
    <link href="Content/jquery.ui.custom.css" rel="stylesheet" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/adminlte.css" rel="stylesheet" />
    <link href="Content/hacks.css" rel="stylesheet" />
    <%--<script src="Scripts/jquery-3.4.1.js"></script>--%>
    <%--    <script src="Scripts/jquery-3.4.1.min.js"></script>
    <script src="Scripts/jquery-3.4.1.slim.js"></script>--%>
    <script src="Scripts/jquery.1.11.2.js"></script>
    <script src="Scripts/jquery-2.2.3.min.js"></script>
    <script src="Scripts/jquery.linq.js"></script>
    <%--<script src="Scripts/jquery-ui.js"></script>--%>
    <script src="Scripts/jquery.ui.custom.js"></script>
    <script src="Scripts/scripts.js"></script>
    <%--<link href="Content/font-awesome.min.css" rel="stylesheet" />--%>
    <script src="https://kit.fontawesome.com/d793205f4e.js" crossorigin="anonymous"></script>
    <style type="text/css">
        .RadWindow RadWindow_Black .rwShadow{
            width: 100% !important;
            height: 100% !important;
            position: center !important;
        }
        .rwContent .rwExternalContent{            
            width: 100% !important;
            height: 100% !important;
            position: center !important;
        }
    </style>

    <title>Listado de Clientes</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelsRenderMode="Inline">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="rtbSave">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="rcbTipoClasifica" />
                        <telerik:AjaxUpdatedControl ControlID="rgvGrid" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="rbFilter">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="rgvGrid" UpdatePanelCssClass="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="rtbSave">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="rgvGrid" UpdatePanelCssClass="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="rgvGrid">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="rgvGrid" UpdatePanelCssClass="" />
                        <telerik:AjaxUpdatedControl ControlID="rcbEstadoCliente" UpdatePanelCssClass="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="rauFotoNIT">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="rauFotoNIT" UpdatePanelCssClass="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="rauLogoEmpresa">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="rauLogoEmpresa" UpdatePanelCssClass="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadWindowManager ID="RadWindowManager2"
            runat="server" EnableShadow="true">
        </telerik:RadWindowManager>
        
        <%--<div id="window-container">--%>
            <telerik:RadWindowManager ID="RadWindowManager1" CssClass="textalignCenterMargin0" OnClientAutoSizeEnd="OnClientAutoSizeEnd" runat="server">
               <%-- <Windows>
                    <telerik:RadWindow ID="RadWindow2" runat="server" Behaviors="Close, Move, Minimize, Maximize" InitialBehaviors="Maximize" AutoSizeBehaviors="HeightProportional" KeepInScreenBounds="true" VisibleStatusbar="false" ViewStateMode="Enabled" Modal="true" VisibleTitlebar="true" EnableViewState="true" RenderMode="Auto" Title="Foto NIT" Skin="Office2010Black" CssClass="combo100porcheightToo" AutoSize="False">
                    </telerik:RadWindow>
                </Windows>--%>
            </telerik:RadWindowManager>
        <%--</div>--%>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
        </telerik:RadAjaxLoadingPanel>
        <div class="content">
            <div class="box">
                <div class="row">
                    <div class="col-md-12">
                        <h3>Listado de Clientes</h3>

                        <div class="row">
                            <br />
                        </div>

                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-6"></div>
                                <div class="col-md-6">
                                    <div class="combo100porc">
                                        <div class="col-md-10">
                                            <telerik:RadTextBox ID="rtbFilter" runat="server" Skin="Bootstrap" EmptyMessage="Buscar cliente" ClientEvents-OnKeyPress="OnKeyPressEnterSearch" />
                                        </div>
                                        <div class="col-md-2">
                                            <telerik:RadButton ID="rbFilter" RenderMode="Lightweight" runat="server" Text="Buscar" Skin="Office2010Black">
                                                <ContentTemplate>
                                                    <i class="fas fa-search fa-1x"></i><span style="padding-left: 3px; font-size: 18px;"><b>Buscar</b></span>
                                                </ContentTemplate>
                                            </telerik:RadButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="dataTables_wrapper form-inline" role="grid">
                                <telerik:RadGrid ID="rgvGrid" runat="server" CssClass="table-responsive" AutoGenerateColumns="False" GridLines="None" AllowPaging="True" CellSpacing="0" VirtualItemCount="5" PageSize="10" Skin="Bootstrap">
                                    <MasterTableView AutoGenerateColumns="False" DataKeyNames="Id" AllowCustomPaging="true" AllowPaging="true">
                                        <Columns>
                                            <telerik:GridTemplateColumn HeaderText="Acciones" HeaderStyle-Width="120" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <div class="btn-group btn-group-xs" role="group">
                                                        <telerik:RadButton ID="rbCambioEstado" CommandName="CambioEstado" runat="server" ToolTip="Cambiar Estado" AutoPostBack="true" Skin="Bootstrap" OnClientClicking="OnEditProvider">
                                                            <ContentTemplate>
                                                                <i class="fas fa-sync-alt"></i>
                                                            </ContentTemplate>
                                                            <Icon PrimaryIconCssClass="fa fa-pencil" PrimaryIconLeft="15px" PrimaryIconTop="6px" />
                                                        </telerik:RadButton>
                                                        <telerik:RadButton ID="rbVerImagenLogoEmpresa" CommandName="VerImagenLogoEmpresa" runat="server" ToolTip="Ver Imagen del Logo de la Empresa" AutoPostBack="true" Skin="Bootstrap" >
                                                            <ContentTemplate>
                                                                <i class="far fa-image"></i>
                                                            </ContentTemplate>
                                                        </telerik:RadButton>
                                                        <telerik:RadButton ID="rbVerImagenFotoNIT" CommandName="VerImagenFotoNIT" runat="server" ToolTip="Ver imagen de la foto del NIT" AutoPostBack="true" Skin="Bootstrap" >
                                                            <ContentTemplate>
                                                                <i class="fas fa-image"></i>
                                                            </ContentTemplate>
                                                        </telerik:RadButton>
                                                    </div>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Nro." HeaderStyle-Width="70" UniqueName="ItemIndex" ItemStyle-Font-Bold="True">
                                                <ItemTemplate>
                                                    <%# (rgvGrid.MasterTableView.CurrentPageIndex * rgvGrid.MasterTableView.PageSize) + Container.ItemIndex + 1 %>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="Id" HeaderText="Nº" UniqueName="Id" Display="false" />
                                            <telerik:GridBoundColumn DataField="NombreCompleto" HeaderText="Nombre Completo" UniqueName="NombreCompleto" />
                                            <telerik:GridBoundColumn DataField="RazonSocial" HeaderText="Razon Social" UniqueName="RazonSocial" HeaderStyle-HorizontalAlign="Center" />
                                            <telerik:GridBoundColumn DataField="NIT" HeaderText="NIT" UniqueName="NIT" HeaderStyle-HorizontalAlign="Center" />
                                            <telerik:GridBoundColumn DataField="Telefono" HeaderText="Telefono" UniqueName="Telefono" HeaderStyle-HorizontalAlign="Center" />
                                            <telerik:GridBoundColumn DataField="Direccion" HeaderText="Direccion" UniqueName="Telefono" HeaderStyle-HorizontalAlign="Center" />
                                            <telerik:GridBoundColumn DataField="NumeroPatronal" HeaderText="Numero Patronal" UniqueName="Email" HeaderStyle-HorizontalAlign="Center" />
                                            <telerik:GridBoundColumn DataField="Email" HeaderText="Email" UniqueName="Email" HeaderStyle-HorizontalAlign="Center" />
                                            <telerik:GridBoundColumn DataField="Ciudad" HeaderText="Ciudad" UniqueName="Ciudad" HeaderStyle-HorizontalAlign="Center" />
                                            <telerik:GridBoundColumn DataField="Fecha" HeaderText="Fecha" UniqueName="Fecha" HeaderStyle-HorizontalAlign="Center" />
                                            <telerik:GridBoundColumn DataField="RepresentanteLegal" HeaderText="Representante Legal" UniqueName="Ciudad" HeaderStyle-HorizontalAlign="Center" />
                                            <telerik:GridBoundColumn DataField="EstadoClienteGrid" HeaderText="Estado" UniqueName="Estado" HeaderStyle-HorizontalAlign="Center" />
                                        </Columns>
                                        <NoRecordsTemplate>
                                            <asp:Label ID="lbEmpty" runat="server" Text="No existen datos." Width="100%"></asp:Label>
                                        </NoRecordsTemplate>
                                        <PagerStyle AlwaysVisible="True" FirstPageToolTip="Primera Pagina" LastPageToolTip="Ultima Pagina" NextPageToolTip="Siguiente" PagerTextFormat="Change page: {4} &amp;nbsp;Pagina &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, Registros &lt;strong&gt;{2}&lt;/strong&gt; al &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." PageSizeControlType="RadDropDownList" PrevPageToolTip="Anterior" />
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <br />
            <div style="width: auto; color: white; height: 75px; text-align: center; font-size: 14px">
                © Copyright 2020 "Sagecv", Todos los derechos reservados<br />
                Vers.3.1.0 - Desarrollado por D.T.E srl
            <br />
                Teléfono 71391723 -3226078<br />
                <a rel="nofollow" target="_blank" href="http://www.dte.com.bo">www.dte.com.bo</a>

            </div>
            <br />
        </div>
        <div id="divDetail" class="box-body" style="display: none;">
            <asp:Panel ID="paDetail" CssClass="MinWidth560" runat="server">
                <table class="combo100porc">
                    <tr>
                        <td class="combo20porc">Estado Cliente:
                        </td>
                        <td class="combo80porc">
                            <telerik:RadComboBox ID="rcbEstadoCliente" runat="server" CssClass="combo100porc" OnClientKeyPressing="radComboKeyPress" MaxHeight="150" EnableTextSelection="true" MarkFirstMatch="true" DropDownAutoWidth="Enabled" Filter="Contains" Skin="Bootstrap">
                                <Items>
                                    <telerik:RadComboBoxItem runat="server" Value="1" Text="CREADO" />
                                    <telerik:RadComboBoxItem runat="server" Value="2" Text="EN PROCESO" />
                                    <telerik:RadComboBoxItem runat="server" Value="3" Text="REALIZADO" />
                                    <telerik:RadComboBoxItem runat="server" Value="4" Text="ANULADO" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="combo100porc">
                            <br />
                            <div class="btn-group pull-right" role="group">
                                <telerik:RadButton ID="rtbSave" runat="server" Text="Guardar" AutoPostBack="true" Skin="Bootstrap" OnClientClicking="funValidate">
                                    <Icon PrimaryIconCssClass="fa fa-save" PrimaryIconTop="10px"></Icon>
                                </telerik:RadButton>
                                <telerik:RadButton ID="rtbCancel" runat="server" Text="Cancelar" AutoPostBack="true" Skin="Bootstrap" OnClientClicking="onCancelClicking" CssClass="btn-danger">
                                    <Icon PrimaryIconCssClass="fa fa-times" PrimaryIconTop="10px"></Icon>
                                </telerik:RadButton>
                            </div>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
<footer>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#divDetail").dialog({
                autoOpen: false,
                draggable: true,
                resizable: false,
                fluid: true,
                modal: true,
                width: "auto",
                maxWidth: window.innerWidth,
                maxHeight: window.innerHeight,
                overflow: "scroll",
                position: ["center", "top"],
                title: "Cambio de Estado",
                open: function (type, data) { $(this).parent().appendTo("form"); }
            });
        });

        // on window resize run function
        $(window).resize(function () {
            fluidDialog();
        });

        // catch dialog if opened within a viewport smaller than the dialog width
        $(document).on("dialogopen", ".ui-dialog", function (event, ui) {
            fluidDialog();
        });

        function fluidDialog() {
            var $visible = $(".ui-dialog:visible");
            // each open dialog
            $visible.each(function () {
                var $this = $(this);
                var dialog = $this.find(".ui-dialog-content").data("ui-dialog");
                // if fluid option == true
                if (dialog.options.fluid) {
                    var wWidth = $(window).width();
                    // check window width against dialog width
                    if (wWidth < (parseInt(dialog.options.maxWidth) + 50)) {
                        // keep dialog from filling entire screen
                        $this.css("max-width", "90%");
                    } else {
                        // fix maxWidth bug
                        $this.css("max-width", dialog.options.maxWidth + "px");
                    }
                    //reposition dialog
                    dialog.option("position", dialog.options.position);
                }
            });

        }

        function onAddClicking(sender, args) {
            showDialog("divDetail");
        }

        function OnEditProvider() {
            showDialog("divDetail");
        }

        function onCancelClicking(sender, args) {
            closeDialog("divDetail");
            args.set_cancel(true);
        }

        function funValidate(sender, args) {
            var strMessage = "";

            if (strMessage == "") {
                //closeDialog("divDetail");
                //proShow_Validation("Por Favor revise los siguientes ítems: <br />" + strMessage, "VALIDACION");
            } else {
                proShow_Validation("Por Favor revise los siguientes ítems: <br />" + strMessage, "VALIDACION");
                args.set_cancel(true);
            }
        }


        function validateEmail(email) {
            var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

            return re.test(email);
        }

        function OnKeyPressEnterSearch(seder, args) {
            var rtbFilter = $find($("[id$='rbFilter']").attr("id"));
            if (args.get_keyCode() == 13) {
                rtbFilter.click();
                args.set_cancel(true)
            }
        }
        function ConfirmDelete(sender, args) {

            var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
                if (shouldSubmit) {
                    //initiate the origianal postback again
                    this.click();
                }
            });

            var text = "¿Está seguro que desea eliminar la caja seleccionada?";
            radconfirm(text, callBackFunction, 300, 160, null, "CONFIRMACION");

            args.set_cancel(true);

        }

        function OnClientAutoSizeEnd(sender, args) {
            var win = $find("<%=RadWindowManager1.ClientID%>");
            win.autoSize(true);
        }
        function OpenWindow(sender) {
            var link = sender;
            var wnd = window.radopen(link, null, window.innerWidth,window.innerHeight - 18);
            setTimeout(function () {
                wnd.set_status("");
            }, 0);
        }


    </script>
</footer>
