<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/assets/master/MasterPage.Master" CodeBehind="CargoJerarquia.aspx.vb" Inherits="Afiliacion2.CargoJerarquia" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--Dashboard -->
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>Cargos y jerarquías
            <small>Definición de cargos para el personal</small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Inicio</a></li>
            <li><a href="#"><i class="fa fa-dashboard"></i>Mantención</a></li>
            <li class="active">Cargos y jerarquías</li>
        </ol>
    </section>
    <form id="form" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelsRenderMode="Inline">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="rbAdd">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="paDetail" UpdatePanelCssClass="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="rgvGrid">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="paDetail" UpdatePanelCssClass="" />
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
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
            Cargando...
        </telerik:RadAjaxLoadingPanel>
        <!-- Main content -->
        <section class="content">
            <div class="row">
                <div class="col-xs-12">
                    <div class="box">
                        <div class="box-header">
                            <h3 class="box-title">Lista de cargos</h3>
                            <div class="box-tools pull-right">
                                <div class="btn-group pull-right" role="group">
                                    <telerik:RadButton ID="rbAdd" runat="server" Text="Nuevo" AutoPostBack="true" OnClientClicking="onAddClicking" Skin="Bootstrap">
                                        <Icon PrimaryIconCssClass="fa fa-plus" PrimaryIconTop="12px"></Icon>
                                    </telerik:RadButton>
                                </div>
                            </div>
                        </div>
                        <div class="box-body table-responsive">
                            <div class="row">
                                <div class="col-md-6 col-md-push-6">
                                    <div class="input-group">
                                        <telerik:RadTextBox ID="rtbFilter" runat="server" Skin="Bootstrap" EmptyMessage="Buscar cargo" >
                                            <ClientEvents OnKeyPress="OnkeyPressEnterSearch" />
                                        </telerik:RadTextBox>
                                        <span class="input-group-btn">
                                            <telerik:RadButton ID="rbFilter" runat="server" Text="Buscar" UseSubmitBehavior="true" Skin="Bootstrap" />
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
                                <telerik:RadGrid ID="rgvGrid" runat="server" AutoGenerateColumns="false" GridLines="None" AllowPaging="True" VirtualItemCount="5"   CellSpacing="0" Skin="Bootstrap" PageSize="15">
                                    <MasterTableView AutoGenerateColumns="False" DataKeyNames="Id" AllowCustomPaging="true" AllowPaging="true" AllowMultiColumnSorting="true">
                                        <Columns>
                                            <telerik:GridTemplateColumn HeaderText="Acciones" HeaderStyle-Width="90" HeaderStyle-HorizontalAlign="Center" >
                                                <ItemStyle HorizontalAlign="Center" /> 
                                                <ItemTemplate>                                                    
                                                    <div class="btn-group btn-group-xs" role="group" >
                                                        <telerik:RadButton ID="btnEdit" CommandName="EditPosition" runat="server" AutoPostBack="true" OnClientClicking="OnEditPosition" Skin="Bootstrap" >
                                                            <Icon PrimaryIconCssClass="fa fa-pencil" PrimaryIconLeft="15px" PrimaryIconTop="6px"></Icon>
                                                        </telerik:RadButton>
                                                        <telerik:RadButton ID="btnDelete" CommandName="DeletePosition" runat="server" AutoPostBack="true" OnClientClicking="ConfirmDelete" Skin="Bootstrap">
                                                            <Icon PrimaryIconCssClass="fa fa-trash-o" PrimaryIconLeft="15px" PrimaryIconTop="6px"></Icon>
                                                        </telerik:RadButton>                                                      
                                                    </div>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            
                                               <telerik:GridTemplateColumn HeaderText="Nro." UniqueName="ItemIndex" ItemStyle-Font-Bold="True" HeaderStyle-Width="75px">
                                                    <ItemTemplate >
                                                        <%# (rgvGrid.MasterTableView.CurrentPageIndex * rgvGrid.MasterTableView.PageSize) + Container.ItemIndex + 1 %>
                                                    </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="Id" HeaderText="Code" UniqueName="Id" Display="false" />
                                            <telerik:GridBoundColumn DataField="Nombre" HeaderText="Nombre del cargo" UniqueName="Description" HeaderStyle-HorizontalAlign="Center" />
                                            <telerik:GridBoundColumn DataField="Cargo.Nombre" HeaderText="Cargo superior" UniqueName="PositionId" HeaderStyle-HorizontalAlign="Center" />
                                        </Columns>
                                        <NoRecordsTemplate>
                                            <asp:Label ID="lbEmpty" runat="server" Text="No existen datos." Width="100%"></asp:Label>
                                        </NoRecordsTemplate>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </div>
                            <div id="divDetail" class="dialog" style="display: none;">
                                <asp:Panel ID="paDetail" runat="server">
                                    <table style="width: 100%">
                                        <tr>
                                            <td>Nombre:
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="rtbDescription" runat="server" EmptyMessage="Ingrese el nombre del cargo" Skin="Bootstrap" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Superior:
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="rcbParent" runat="server" Placeholder="Seleccione el cargo superior" OnClientKeyPressing="radComboKeyPress" MaxHeight="150" EnableTextSelection="true" MarkFirstMatch="true" DropDownAutoWidth="Enabled" Filter="Contains" Skin="Bootstrap"/>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Comisión:
                                            </td>
                                            <td>
                                                <telerik:RadNumericTextBox ID="rnbBaseFee" runat="server" Skin="Bootstrap" MaxValue="100" MinValue="0" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td>
                                                <br />
                                                <div class="btn-group pull-right" role="group">
                                                    <telerik:RadButton ID="rtbSave" runat="server" Text="Guardar" AutoPostBack="true" OnClientClicking="funValidate" Skin="Bootstrap">
                                                        <Icon PrimaryIconCssClass="fa fa-save" PrimaryIconTop="12px"></Icon>
                                                    </telerik:RadButton>
                                                    <telerik:RadButton ID="rtbCancel" runat="server" Text="Cancelar" AutoPostBack="true" OnClientClicking="onCancelClicking" Skin="Bootstrap" CssClass="btn-danger">
                                                        <Icon PrimaryIconCssClass="fa fa-times" PrimaryIconTop="12px"></Icon>
                                                    </telerik:RadButton>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
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
    <script type="text/javascript" src="Scripts/CargoJerarquia.js"></script>
</asp:Content>