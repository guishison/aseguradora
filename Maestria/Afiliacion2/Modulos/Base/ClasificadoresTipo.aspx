<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/assets/master/MasterPage.Master" CodeBehind="ClasificadoresTipo.aspx.vb" Inherits="Afiliacion2.ClasificadoresTipo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <section class="content-header">
        <h1>Tipos clasificadores
                <small>Grupos de configuraciones globales del sistema</small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Inicio</a></li>
            <li><a href="#"><i class="fa fa-dashboard"></i>Mantención</a></li>
            <li class="active">Tipos clasificadores</li>
        </ol>
    </section>
    <form id="form" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnRequestStart="AjustarPanel();" UpdatePanelsRenderMode="Inline">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="rbAdd">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="paDetail" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="rgvGrid">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="paDetail" UpdatePanelCssClass="" />
                        <telerik:AjaxUpdatedControl ControlID="rgvGrid" />
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

        <!-- Main content -->
        <section class="content" onload="RemoverPanel()">
            <div id="Bodycito" class="row MyModalPanel"></div>
            <div class="row">
                <div class="col-xs-12">
                    <div class="box">
                        <div class="box-header">
                            <h3 class="box-title">Lista de tipo clasificador
                                <br />
                                <small>Gestione estos parametros con asistencia de un administrador del sistema.</small>
                            </h3>
                            <div class="box-tools pull-right">
                                <div class="btn-group pull-right" role="group">
                                    <telerik:RadButton ID="rbAdd" runat="server" Text="Nuevo" AutoPostBack="true" Skin="Bootstrap" OnClientClicking="onAddClicking">
                                        <Icon PrimaryIconCssClass="fa fa-plus" PrimaryIconTop="10px"></Icon>
                                    </telerik:RadButton>
                                </div>
                            </div>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-6 col-md-push-6">
                                    <div class="input-group">
                                        <telerik:RadTextBox ID="rtbFilter" runat="server" Skin="Bootstrap" Placeholder="Ingrese el nombre del tipo de clasificador" MaxLength="50">
                                            <ClientEvents OnKeyPress="OnkeyPressEnterSearch" />
                                        </telerik:RadTextBox>
                                        <span class="input-group-btn">
                                            <telerik:RadButton ID="rbFilter" runat="server" Text="Buscar" Skin="Bootstrap" Style="position: relative;">
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
                                <telerik:RadGrid ID="rgvGrid" CssClass="table-responsive" runat="server" AutoGenerateColumns="false" GridLines="None" AllowPaging="True" VirtualItemCount="5" CellSpacing="0" Skin="Bootstrap" PageSize="20">
                                    <MasterTableView AutoGenerateColumns="False" DataKeyNames="Id" AllowCustomPaging="true" AllowPaging="true">
                                        <Columns>
                                            <telerik:GridTemplateColumn HeaderText="" HeaderStyle-Width="87" HeaderStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <div class="btn-group btn-group-xs" role="group">
                                                        <telerik:RadButton ID="btnEdit" CommandName="Editar" runat="server" ToolTip="Modificar" AutoPostBack="true" Skin="Bootstrap" OnClientClicking="OnEditClicking">
                                                            <Icon PrimaryIconCssClass="fa fa-pencil" />
                                                        </telerik:RadButton>
                                                        <telerik:RadButton ID="btnDelete" CommandName="Eliminar" runat="server" ToolTip="Eliminar" AutoPostBack="true" Skin="Bootstrap" OnClientClicking="ConfirmDelete">
                                                            <Icon PrimaryIconCssClass="fa fa-trash-o" />
                                                        </telerik:RadButton>
                                                    </div>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Nro." UniqueName="ItemIndex" ItemStyle-Font-Bold="True" HeaderStyle-Width="75px">
                                                <ItemTemplate>
                                                    <%# (rgvGrid.MasterTableView.CurrentPageIndex * rgvGrid.MasterTableView.PageSize) + Container.ItemIndex + 1 %>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridDateTimeColumn DataField="Nombre" HeaderText="Nombre de tipo" UniqueName="NombreTipo" DataType="System.String" HeaderStyle-HorizontalAlign="Left" />
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
        </section>

        <!-- Modal -->
        <div id="divDetail" class="dialog" style="display: none;">
            <asp:Panel ID="paDetail" runat="server" CssClass="MinWidth467">
                <table style="width: 100%">
                    <tr>
                        <td>Nombre:
                        </td>
                        <td>
                            <telerik:RadTextBox ID="rtbNombre" runat="server" EmptyMessage="Nombre" CssClass="combo100porc" Skin="Bootstrap" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <br />
                            <div class="btn-group pull-right" role="group">
                                <telerik:RadButton ID="rtbSave" runat="server" Text="Guardar" AutoPostBack="true" Skin="Bootstrap" OnClientClicking="Validate">
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
    </form>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script type="text/javascript" src="Scripts/ClasificadoresTipo.js"></script>
</asp:Content>
