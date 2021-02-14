<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/assets/master/MasterPage.Master" CodeBehind="PermisoPlantilla.aspx.vb" Inherits="Afiliacion2.PermisoPlantilla" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--Dashboard -->
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>Permiso plantilla
                <small>Plantillas de permisos preconfigurdas para aplicación rapida</small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Inicio</a></li>
            <li><a href="#"><i class="fa fa-dashboard"></i>Administración</a></li>
            <li><a href="#"><i class="fa fa-dashboard"></i>Usuarios</a></li>
            <li class="active">Permiso plantilla</li>
        </ol>
    </section>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnRequestStart="AjustarPanel();" UpdatePanelsRenderMode="Inline">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="rbNuevo">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="paDetail2" UpdatePanelCssClass="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="rbFilter">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="rgvProfileList" UpdatePanelCssClass="" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="rtbSave">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="rgvProfileList" UpdatePanelCssClass="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="rbLimpiar">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="rgvProfileList" UpdatePanelCssClass="" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="rgvProfileList">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="rgvProfileList" />
                        <telerik:AjaxUpdatedControl ControlID="paDetail" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="rbAccept">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="rgvProfileList" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
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
        <section class="content" onload="RemoverPanel()">
            <div id="Bodycito" class="row MyModalPanel"></div>
            <div class="row">
                <div class="col-xs-12">
                    <div class="box">
                        <div class="box-header">
                            <h3 class="box-title">Lista de plantillas</h3>
                            <div class="box-tools pull-right">
                                <div class="btn-group pull-right" role="group">
                                    <telerik:RadButton ID="rbAdd" runat="server" Visible="false" Text="Nuevo" AutoPostBack="true" Skin="Bootstrap" OnClientClicking="onAddClicking">
                                        <Icon PrimaryIconCssClass="fa fa-plus" PrimaryIconTop="12px"></Icon>
                                    </telerik:RadButton>
                                </div>
                            </div>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-6 col-md-push-6">
                                    <div class="input-group">
                                        <telerik:RadTextBox ID="rtbProfileFilter1" runat="server" EmptyMessage="Buscar plantilla" Skin="Bootstrap" />
                                        <span class="input-group-btn">
                                            <telerik:RadButton ID="rbFilter" runat="server" Text="Buscar" AutoPostBack="true" Skin="Bootstrap">
                                                <Icon PrimaryIconCssClass="fa fa-search" PrimaryIconTop="10px" />
                                            </telerik:RadButton>
                                            <telerik:RadButton ID="rbLimpiar" runat="server" Text="Limpiar" AutoPostBack="true" Skin="Bootstrap">
                                                <Icon PrimaryIconCssClass="fa fa-eraser" PrimaryIconTop="10px" />
                                            </telerik:RadButton>
                                            <telerik:RadButton ID="rbNuevo" runat="server" Text="Nuevo" AutoPostBack="true" Skin="Bootstrap" OnClientClicking="onAddClicking">
                                                <Icon PrimaryIconCssClass="fa fa-plus" PrimaryIconTop="10px" />
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
                                <telerik:RadGrid ID="rgvProfileList" CssClass="table-responsive" runat="server" AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True" PageSize="15" GroupPanelPosition="Top" Skin="Bootstrap">
                                    <MasterTableView GroupLoadMode="Client" AutoGenerateColumns="False" DataKeyNames="Id,Nombre" HierarchyLoadMode="Client" HierarchyDefaultExpanded="False">
                                        <RowIndicatorColumn Visible="False">
                                        </RowIndicatorColumn>
                                        <ExpandCollapseColumn Created="True">
                                        </ExpandCollapseColumn>
                                        <Columns>
                                            <telerik:GridTemplateColumn HeaderText="Acciones" HeaderStyle-Width="120" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <div class="btn-group btn-group-xs" role="group">
                                                        <telerik:RadButton ID="btnEditProfile" CommandName="EditProfile" ToolTip="Modificar" runat="server" AutoPostBack="true" Skin="Bootstrap" OnClientClicking="OnEditProfile">
                                                            <Icon PrimaryIconCssClass="fa fa-pencil" PrimaryIconLeft="15px" PrimaryIconTop="6px"></Icon>
                                                        </telerik:RadButton>
                                                        <telerik:RadButton ID="btnDeleteProfile" CommandName="DeleteProfile" Visible="false" runat="server" AutoPostBack="true" Skin="Bootstrap" OnClientClicking="ConfirmDelete">
                                                            <Icon PrimaryIconCssClass="fa fa-trash-o" PrimaryIconLeft="15px" PrimaryIconTop="6px"></Icon>
                                                        </telerik:RadButton>
                                                    </div>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="Id" HeaderText="Code" UniqueName="Id" Display="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Nombre" HeaderText="Personal" UniqueName="Nombre" HeaderStyle-HorizontalAlign="Center" FilterControlAltText="Filter Profile column">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="FechaReg" HeaderText="Fecha creacion" UniqueName="Login" HeaderStyle-HorizontalAlign="Center" DataType="System.DateTime" DataFormatString="{0:dd MMMM yyyy}" FilterControlAltText="Filter Profile column">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                        <NoRecordsTemplate>
                                            <asp:Label ID="lbEmpty" runat="server" Text="No existen datos." Width="100%"></asp:Label>
                                        </NoRecordsTemplate>
                                        <PagerStyle AlwaysVisible="True" FirstPageToolTip="Primera Pagina" LastPageToolTip="Ultima Pagina" NextPageToolTip="Siguiente" PagerTextFormat="Change page: {4} &amp;nbsp;Pagina &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, Registros &lt;strong&gt;{2}&lt;/strong&gt; al &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." PageSizeControlType="RadDropDownList" PrevPageToolTip="Anterior" />
                                    </MasterTableView>
                                    <ClientSettings>
                                        <ClientEvents OnRowMouseOver="RowMouseOver" />
                                    </ClientSettings>
                                </telerik:RadGrid>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </section>
        <div id="divDetail" class="dialog" style="display: none;">
            <asp:Panel ID="paDetail" runat="server" CssClass="MaxWidth560">
                <table style="width: 100%">
                    <tr>
                        <td>Plantilla:
                        </td>
                        <td>
                            <telerik:RadLabel ID="rtbProfile" runat="server" Skin="Bootstrap">
                            </telerik:RadLabel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <telerik:RadTabStrip ID="rtsModules" runat="server" MultiPageID="rpvTabPages" Skin="Bootstrap">
                            </telerik:RadTabStrip>
                            <telerik:RadMultiPage ID="rpvTabPages" runat="server">
                            </telerik:RadMultiPage>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <br />
                            <div class="btn-group pull-right" role="group">
                                <telerik:RadButton ID="rbAccept" runat="server" Text="Guardar" AutoPostBack="true" Skin="Bootstrap" OnClientClicking="funValidate">
                                    <Icon PrimaryIconCssClass="fa fa-save" PrimaryIconTop="12px"></Icon>
                                </telerik:RadButton>
                                <telerik:RadButton ID="rtbCancel" runat="server" Text="Cancelar" AutoPostBack="true" Skin="Bootstrap" CssClass="btn-danger" OnClientClicking="onCancelClicking">
                                    <Icon PrimaryIconCssClass="fa fa-times" PrimaryIconTop="12px"></Icon>
                                </telerik:RadButton>
                            </div>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
        <div id="divDetail2" class="box-body" style="display: none">
            <asp:Panel ID="paDetail2" runat="server" CssClass="MinWidth467 MaxWidth560">
                <table class="combo100porc">
                    <tr>
                        <td>Nombre plantilla:
                        </td>
                        <td>
                            <telerik:RadTextBox ID="rtbNombrePlantilla" runat="server" EmptyMessage="Ingrese el nombre de la plantilla" Skin="Bootstrap" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <br />
                            <div class="btn-group pull-right" role="group">
                                <telerik:RadButton ID="rtbSave" runat="server" Text="Guardar" AutoPostBack="true" Skin="Bootstrap" OnClientClicking="funValidate2">
                                    <Icon PrimaryIconCssClass="fa fa-save" PrimaryIconTop="10px"></Icon>
                                </telerik:RadButton>
                                <telerik:RadButton ID="rtbCancelar" runat="server" Text="Cancelar" AutoPostBack="true" Skin="Bootstrap" OnClientClicking="onCancelClicking2" CssClass="btn-danger">
                                    <Icon PrimaryIconCssClass="fa fa-times" PrimaryIconTop="10px"></Icon>
                                </telerik:RadButton>
                            </div>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
        <asp:HiddenField ID="hdnOptionIds" runat="server" />
        <asp:HiddenField ID="hdnOptionIds2" runat="server" />
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script type="text/javascript" src="Scripts/PermisoPlantilla.js"></script>
</asp:Content>
