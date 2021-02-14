<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/assets/master/MasterPage.Master" CodeBehind="PermisoFormulario.aspx.vb" Inherits="Afiliacion2.PermisoFormulario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--Dashboard -->
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>Perfiles
                <small>Listado de Perfiles</small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Inicio</a></li>
            <li class="active">Perfiles</li>
        </ol>
    </section>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="rbAdd">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="paDetail" UpdatePanelCssClass="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="rbFilter">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="rgvProfileList" UpdatePanelCssClass="" LoadingPanelID="RadAjaxLoadingPanel1" />
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
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
            Cargando...
        </telerik:RadAjaxLoadingPanel>
        <section class="content">
            <div class="row">
                <div class="col-xs-12">
                    <div class="box">
                        <div class="box-header">
                            <h3 class="box-title">Listado de Perfiles</h3>
                            <div class="box-tools pull-right">
                                <div class="btn-group pull-right" role="group">
                                    <telerik:RadButton ID="rbAdd" runat="server" Text="Nuevo" AutoPostBack="true" Skin="Bootstrap" OnClientClicking="onAddClicking">
                                        <Icon PrimaryIconCssClass="fa fa-plus" PrimaryIconTop="12px"></Icon>
                                    </telerik:RadButton>
                                </div>
                            </div>
                        </div>
                        <div class="box-body table-responsive">
                            <div class="row">
                                <div class="col-md-6 col-md-push-6">
                                    <div class="input-group">
                                        <telerik:RadTextBox ID="rtbProfileFilter1" runat="server" laceHolder="Ingrese el texto..." Skin="Bootstrap" />
                                        <span class="input-group-btn">
                                            <telerik:RadButton ID="rbFilter" runat="server" Text="Buscar" AutoPostBack="true" Skin="Bootstrap">
                                                <Icon PrimaryIconCssClass="fa fa-search" PrimaryIconTop="10px" />
                                            </telerik:RadButton>
                                            <telerik:RadButton ID="rbLimpiar" runat="server" Text="Limpiar" AutoPostBack="true" Skin="Bootstrap">
                                                <Icon PrimaryIconCssClass="fa fa-eraser" PrimaryIconTop="10px" />
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
                                <telerik:RadGrid ID="rgvProfileList" runat="server" AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True" PageSize="15" GroupPanelPosition="Top" Skin="Bootstrap">
                                    <MasterTableView GroupLoadMode="Client" AutoGenerateColumns="False" DataKeyNames="Id" HierarchyLoadMode="Client" HierarchyDefaultExpanded="False">
                                        <RowIndicatorColumn Visible="False">
                                        </RowIndicatorColumn>
                                        <ExpandCollapseColumn Created="True">
                                        </ExpandCollapseColumn>
                                        <Columns>
                                            <telerik:GridTemplateColumn HeaderText="Acciones" HeaderStyle-Width="120" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <div class="btn-group btn-group-xs" role="group">
                                                        <telerik:RadButton ID="btnEditProfile" CommandName="EditProfile" runat="server" AutoPostBack="true" Skin="Bootstrap" OnClientClicking="OnEditProfile">
                                                            <Icon PrimaryIconCssClass="fa fa-pencil" PrimaryIconLeft="15px" PrimaryIconTop="6px"></Icon>
                                                        </telerik:RadButton>
                                                        <telerik:RadButton ID="btnDeleteProfile" CommandName="DeleteProfile" runat="server" AutoPostBack="true" Skin="Bootstrap" OnClientClicking="ConfirmDelete">
                                                            <Icon PrimaryIconCssClass="fa fa-trash-o" PrimaryIconLeft="15px" PrimaryIconTop="6px"></Icon>
                                                        </telerik:RadButton>
                                                    </div>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="Id" HeaderText="Code" UniqueName="Id" Display="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Nombre" HeaderText="Nombre Hotel" UniqueName="Profile" HeaderStyle-HorizontalAlign="Center" FilterControlAltText="Filter Profile column">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                        <NoRecordsTemplate>
                                            <asp:Label ID="lbEmpty" runat="server" Text="No existen datos." Width="100%"></asp:Label>
                                        </NoRecordsTemplate>
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
        <div id="divDetail" class="dialog">
            <asp:Panel ID="paDetail" runat="server">
                <table style="width: 100%">
                    <tr>
                        <td>Perfil:
                        </td>
                        <td>
                            <telerik:RadTextBox ID="rtbProfile" runat="server" Width="400" Skin="Bootstrap">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
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
        <asp:HiddenField ID="hdnOptionIds" runat="server" />
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script type="text/javascript" src="Scripts/PermisoFormulario.js"></script>
</asp:Content>
