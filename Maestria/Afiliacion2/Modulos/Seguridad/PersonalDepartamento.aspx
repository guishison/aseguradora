<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/assets/master/MasterPage.Master" CodeBehind="PersonalDepartamento.aspx.vb" Inherits="Afiliacion2.PersonalDepartamento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!--Dashboard -->
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>Personal departamento
                <small>Información de contacto de todo el personal de la empresa</small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Inicio</a></li>
            <li><a href="#"><i class="fa fa-dashboard"></i>Intranet</a></li>
            <li class="active">Personal departamento</li>
        </ol>
    </section>
    <form id="form" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelsRenderMode="Inline">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="rgvGrid">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="paTarjeta" UpdatePanelCssClass="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="rgvGrid">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="rgvGrid" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
        </telerik:RadWindowManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
        </telerik:RadAjaxLoadingPanel>
        <!-- Main content -->
        <section class="content">
            <div class="row">
                <div class="col-xs-12">
                    <div class="box">
                        <div class="box-header">
                            <h3 class="box-title">Lista de personal</h3>
                        </div>
                        <div class="box-body table-responsive">
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="input-group">
                                        <div class="input-group-btn">
                                            <telerik:RadLabel ID="rlDepartamento" Text="Departamento" runat="server" Skin="Bootstrap"></telerik:RadLabel>
                                        </div>
                                        <telerik:RadComboBox runat="server" ID="rcbDepartamento" CssClass="combo100porc" AutoPostBack="true" Skin="Bootstrap" OnClientKeyPressing="radComboKeyPress" MaxHeight="150" EnableTextSelection="true" MarkFirstMatch="true" DropDownAutoWidth="Enabled" Filter="Contains"></telerik:RadComboBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="input-group">
                                        <telerik:RadTextBox ID="rtbFilter" runat="server" Skin="Bootstrap" Placeholder="Ingrese el texto...">
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
                                <telerik:RadGrid ID="rgvGrid" runat="server" AutoGenerateColumns="False" GridLines="None" AllowPaging="True" VirtualItemCount="15" CellSpacing="0" PageSize="15" Skin="Bootstrap">
                                    <MasterTableView AutoGenerateColumns="False" DataKeyNames="Id" AllowCustomPaging="true" AllowPaging="true" TableLayout="Fixed">
                                        <Columns>
                                            <telerik:GridDateTimeColumn DataField="Id" HeaderText="Fecha" UniqueName="Id" Display="false" />
                                            <telerik:GridTemplateColumn HeaderText="Nro." HeaderStyle-Width="50" UniqueName="ItemIndex" ItemStyle-Font-Bold="True">
                                                <ItemTemplate>
                                                    <%# (rgvGrid.MasterTableView.CurrentPageIndex * rgvGrid.MasterTableView.PageSize) + Container.ItemIndex + 1 %>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="Nombre" HeaderText="Nombre" UniqueName="Nombre" HeaderStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Departamento.Nombre" HeaderText="Departamento" UniqueName="Departamento" HeaderStyle-HorizontalAlign="Center" />
                                            <telerik:GridBoundColumn DataField="Cargo.Nombre" HeaderText="Cargo" UniqueName="Cargo" HeaderStyle-HorizontalAlign="Center" />
                                            <telerik:GridBoundColumn DataField="Correo" HeaderText="Correo electrónico" UniqueName="Correo" HeaderStyle-HorizontalAlign="Center" />
                                            <telerik:GridBoundColumn DataField="Telefono" HeaderText="Teléfono de contacto" UniqueName="Telefono" HeaderStyle-HorizontalAlign="Center" />
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
        <div id="divTarjeta" class="dialog" style="display: none">
            <asp:Panel ID="paTarjeta" runat="server">
                <%--<table style="width: 100%">
                    <tr>
                        <td>Cliente:
                        </td>
                        <td>
                            <telerik:RadComboBox ID="rcbCliente" runat="server" CssClass="combo100porc" Skin="Bootstrap" AutoPostBack="true"></telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Contrato:
                        </td>
                        <td>
                            <telerik:RadComboBox ID="rcbContrato" runat="server" CssClass="combo100porc" Skin="Bootstrap" AutoPostBack="true"></telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Nro. Tarjeta</td>
                        <td>
                            <telerik:RadTextBox ID="rtbNroTarjeta" runat="server" Skin="Bootstrap" EmptyMessage="Numero de Tarjeta" MaxLength="30"></telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <br />
                            <div class="btn-group pull-right" role="group">
                                <telerik:RadButton ID="rtbSave" runat="server" Text="Guardar" AutoPostBack="true" Skin="Bootstrap" SingleClick="true" OnClientClicking="">
                                    <Icon PrimaryIconCssClass="fa fa-save" PrimaryIconTop="10px" />
                                </telerik:RadButton>
                                <telerik:RadButton ID="rtbCancel" runat="server" Text="Cancelar" AutoPostBack="true" Skin="Bootstrap" CssClass="btn-danger" OnClientClicking="onCancelClicking">
                                    <Icon PrimaryIconCssClass="fa fa-times" PrimaryIconTop="10px" />
                                </telerik:RadButton>
                            </div>
                        </td>
                    </tr>
                </table>--%>
            </asp:Panel>
        </div>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <%--<script type="text/javascript" src="Scripts/PersonalDepartamento.js"></script>--%>
</asp:Content>
