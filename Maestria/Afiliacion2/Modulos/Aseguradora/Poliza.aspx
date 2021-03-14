<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/assets/master/MasterPage.Master" CodeBehind="Poliza.aspx.vb" Inherits="Afiliacion2.Poliza" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!--Dashboard -->
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>Polizas
                <small>Registro de Polizas.</small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Inicio</a></li>
            <li><a href="#"><i class="fa fa-dashboard"></i>Aseguradora</a></li>
            <li class="active">Poliza</li>
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
                <telerik:AjaxSetting AjaxControlID="rbCalcular">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="paDetail" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="rntDescuento">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="paDetail" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="rcbCiudad">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="paDetail" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="rcbVehiculo">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="paDetail" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="rcbCliente">
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
                        <telerik:AjaxUpdatedControl ControlID="paDetail" UpdatePanelCssClass="" />
                        <telerik:AjaxUpdatedControl ControlID="rgvGrid" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="rbFilter">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="rgvGrid" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="rbBuscarCotizacion">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="paDetail2" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="rbFilterCotizacion">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="paDetail2" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="rgvGridCotizacion">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="paDetail" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="rntTiempo">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="paDetail" />
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
                            <h3 class="box-title">Lista de Polizas</h3>
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
                                        <telerik:RadTextBox ID="rtbFilter" runat="server" Skin="Bootstrap" EmptyMessage="Buscar Polizas">
                                            <ClientEvents OnKeyPress="OnkeyPress" />
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
                                <telerik:RadGrid ID="rgvGrid" CssClass="table-responsive" runat="server" AutoGenerateColumns="False" GridLines="None" AllowPaging="True" VirtualItemCount="15" CellSpacing="0" PageSize="15" Skin="Bootstrap">
                                    <MasterTableView AutoGenerateColumns="False" DataKeyNames="Id" AllowCustomPaging="true" AllowPaging="true" TableLayout="Fixed">
                                        <Columns>
                                            <telerik:GridTemplateColumn HeaderText="Acciones" HeaderStyle-Width="87" HeaderStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <div class="btn-group btn-group-xs" role="group">
                                                        <telerik:RadButton ID="btnEdit" CommandName="Editar" Visible="false" runat="server" ToolTip="Modificar" AutoPostBack="true" Skin="Bootstrap" OnClientClicking="OnEditDatabase">
                                                            <Icon PrimaryIconCssClass="fa fa-pencil" />
                                                        </telerik:RadButton>
                                                        <telerik:RadButton ID="btnDelete" CommandName="Eliminar" runat="server" ToolTip="Eliminar" AutoPostBack="true" Skin="Bootstrap" OnClientClicking="ConfirmDelete">
                                                            <Icon PrimaryIconCssClass="fa fa-trash-o" />
                                                        </telerik:RadButton>
                                                    </div>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridDateTimeColumn DataField="Id" HeaderText="Fecha" UniqueName="Id" Display="false" />
                                            <telerik:GridTemplateColumn HeaderText="Nro." HeaderStyle-Width="50" UniqueName="ItemIndex" ItemStyle-Font-Bold="True">
                                                <ItemTemplate>
                                                    <%# (rgvGrid.MasterTableView.CurrentPageIndex * rgvGrid.MasterTableView.PageSize) + Container.ItemIndex + 1 %>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="Cotizacion.Cliente.NombreCompleto" HeaderText="Cliente" UniqueName="Cliente" HeaderStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Cotizacion.Vehiculo.DetalleVehiculo" HeaderText="Detalle Vehiculo" UniqueName="DetalleVehiculo" HeaderStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Cotizacion.Tasa.Valor" HeaderText="Tasa Valor" UniqueName="Tasa.Valor" HeaderStyle-HorizontalAlign="Center" DataFormatString="{0:###,##0.00}" ItemStyle-HorizontalAlign="Right"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Cotizacion.Ciudad.Ciudad" HeaderText="Ciudad" UniqueName="Ciudad" HeaderStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="MontoAsegurable" HeaderText="Monto Asegurable" UniqueName="MontoAsegurable" HeaderStyle-HorizontalAlign="Center" DataFormatString="{0:###,##0.00}" ItemStyle-HorizontalAlign="Right"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CostoPrima" HeaderText="Costo Prima" UniqueName="CostoPrima" HeaderStyle-HorizontalAlign="Center" DataFormatString="{0:###,##0.00}" ItemStyle-HorizontalAlign="Right"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="MesesAsegurable" HeaderText="Meses Asegurable" UniqueName="MesesAsegurable" HeaderStyle-HorizontalAlign="Center" DataFormatString="{0:###,##0}" ItemStyle-HorizontalAlign="Right"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Descuento" HeaderText="Descuento" UniqueName="Descuento" HeaderStyle-HorizontalAlign="Center" DataFormatString="{0:###,##0.00}" ItemStyle-HorizontalAlign="Right"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CostoTotal" HeaderText="Total" UniqueName="CostoTotal" HeaderStyle-HorizontalAlign="Center" DataFormatString="{0:###,##0.00}" ItemStyle-HorizontalAlign="Right"></telerik:GridBoundColumn>
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
        <div id="divDetail2" class="dialog" style="display: none">
            <asp:Panel ID="paDetail2" runat="server" CssClass="MinWidth467 MaxWidth900">
                
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-6 col-md-push-6">
                                    <div class="input-group">
                                        <telerik:RadTextBox ID="rtbFilterCotizacion" runat="server" Skin="Bootstrap" EmptyMessage="Buscar Cotizacion">
                                            <ClientEvents OnKeyPress="OnkeyPress" />
                                        </telerik:RadTextBox>
                                        <span class="input-group-btn">
                                            <telerik:RadButton ID="rbFilterCotizacion" runat="server" AutoPostBack="true" Text="Buscar" Skin="Bootstrap">
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
                                <telerik:RadGrid ID="rgvGridCotizacion" CssClass="table-responsive" runat="server" AutoGenerateColumns="False" GridLines="None" AllowPaging="True" VirtualItemCount="15" CellSpacing="0" PageSize="15" Skin="Bootstrap">
                                    <MasterTableView AutoGenerateColumns="False" DataKeyNames="Id" AllowCustomPaging="true" AllowPaging="true" TableLayout="Fixed">
                                        <Columns>
                                            <telerik:GridTemplateColumn HeaderText="Acciones" HeaderStyle-Width="87" HeaderStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <div class="btn-group btn-group-xs" role="group">
                                                        <telerik:RadButton ID="btnSelection" CommandName="Seleccionar" runat="server" ToolTip="Seleccionar Cotizacion" AutoPostBack="true" Skin="Bootstrap" OnClientClicking="OnSelection">
                                                            <Icon PrimaryIconCssClass="fa fa-pencil" />
                                                        </telerik:RadButton>
                                                    </div>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridDateTimeColumn DataField="Id" HeaderText="Fecha" UniqueName="Id" Display="false" />
                                            <telerik:GridTemplateColumn HeaderText="Nro." HeaderStyle-Width="50" UniqueName="ItemIndex" ItemStyle-Font-Bold="True">
                                                <ItemTemplate>
                                                    <%# (rgvGridCotizacion.MasterTableView.CurrentPageIndex * rgvGridCotizacion.MasterTableView.PageSize) + Container.ItemIndex + 1 %>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="Cliente.Id" HeaderText="Codigo Cliente" UniqueName="ClienteCodigo" HeaderStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Cliente.CI" HeaderText="CI" UniqueName="CI" HeaderStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Cliente.NombreCompleto" HeaderText="Cliente" UniqueName="Cliente" HeaderStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="FechaCotizacion" HeaderText="Fecha Cotizacion" UniqueName="FechaCotizacion" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="EstadoCotizacion.Nombre" HeaderText="Estado" UniqueName="Estado" HeaderStyle-HorizontalAlign="Center" DataFormatString="{0:###,##0.00}" ItemStyle-HorizontalAlign="Right"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Vehiculo.DetalleVehiculo" HeaderText="Detalle Vehiculo" UniqueName="DetalleVehiculo" HeaderStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                                        </Columns>
                                        <NoRecordsTemplate>
                                            <asp:Label ID="lbEmpty" runat="server" Text="No existen datos." Width="100%"></asp:Label>
                                        </NoRecordsTemplate>
                                        <PagerStyle AlwaysVisible="True" FirstPageToolTip="Primera Pagina" LastPageToolTip="Ultima Pagina" NextPageToolTip="Siguiente" PagerTextFormat="Change page: {4} &amp;nbsp;Pagina &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, Registros &lt;strong&gt;{2}&lt;/strong&gt; al &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." PageSizeControlType="RadDropDownList" PrevPageToolTip="Anterior" />
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </div>
                        </div>
                </asp:Panel>
            </div>
        <!-- Modal -->
        <div id="divDetail" class="dialog" style="display: none">
            <asp:Panel ID="paDetail" runat="server" CssClass="MinWidth467 MaxWidth900">
                <div>
                    <fieldset>
                        <legend>Datos Poliza
                        </legend>
                        <div>
                            <div class="col-lg-2 negrita">Fecha Poliza</div>
                            <div class="col-lg-4">
                                <telerik:RadDatePicker runat="server" ID="rdpFechaPoliza" CssClass="combo100porc" Skin="Bootstrap"></telerik:RadDatePicker>
                            </div>
                            <div class="col-lg-2 negrita">Fecha Inicio</div>
                            <div class="col-lg-4">
                                <telerik:RadDatePicker runat="server" ID="rdpFechaInicio" CssClass="combo100porc" Skin="Bootstrap"></telerik:RadDatePicker>
                            </div>
                        </div>
                        <div>
                            <div class="col-lg-2 negrita">Num. Poliza</div>
                            <div class="col-lg-4">
                                <telerik:RadTextBox runat="server" ID="rtbNumeroPoliza" CssClass="combo100porc" Skin="Bootstrap" ></telerik:RadTextBox>
                            </div>
                            <div class="col-lg-2 negrita">Cotizacion</div>
                            <div class="col-lg-3">
                                <telerik:RadTextBox runat="server" ID="rtbCotizacion" DisabledStyle-BackColor="WhiteSmoke" CssClass="combo100porc" ToolTip="Buscar una Cotizacion" Skin="Bootstrap" ReadOnly="true" Enabled ="false"></telerik:RadTextBox>
                            </div>
                            <div>
                                <telerik:RadButton ID="rbBuscarCotizacion" runat="server" AutoPostBack="true" Skin="Bootstrap"  OnClientClicking="onFindClicking">
                                    <Icon PrimaryIconCssClass="fa fa-search-plus" />
                                </telerik:RadButton>
                            </div>
                        </div>
                    </fieldset>
                    <div class="row"></div>
                    <fieldset>
                        <legend>Automotor
                        </legend>
                        <div>
                            <div class="col-lg-2 negrita">Departamento</div>
                            <div class="col-lg-4">
                                <telerik:RadComboBox ID="rcbCiudad" runat="server" AutoPostBack="true" CssClass="combo100porc" OnClientKeyPressing="radComboKeyPress" MaxHeight="150" EnableTextSelection="true" MarkFirstMatch="true" DropDownAutoWidth="Enabled" Filter="Contains" Skin="Bootstrap"></telerik:RadComboBox>
                            </div>
                            <div class="col-lg-2 negrita">Cliente</div>
                            <div class="col-lg-4">
                                <telerik:RadComboBox ID="rcbCliente" runat="server" CssClass="combo100porc" AutoPostBack="true" OnClientKeyPressing="radComboKeyPress" MaxHeight="150" EnableTextSelection="true" MarkFirstMatch="true" DropDownAutoWidth="Enabled" Filter="Contains" Skin="Bootstrap"></telerik:RadComboBox>
                            </div>
                        </div>
                        <div>
                            <div class="col-lg-2 negrita">Vehiculo</div>
                            <div class="col-lg-4">
                                <telerik:RadComboBox ID="rcbVehiculo"  runat="server" CssClass="combo100porc" AutoPostBack="true" OnClientKeyPressing="radComboKeyPress" MaxHeight="150" EnableTextSelection="true" MarkFirstMatch="true" DropDownAutoWidth="Enabled" Filter="Contains" Skin="Bootstrap"></telerik:RadComboBox>
                            </div>
                            <div class="col-lg-2 negrita">Potencia</div>
                            <div class="col-lg-4">
                                <telerik:RadTextBox Enabled="false" DisabledStyle-BackColor="WhiteSmoke" ID="rtbPotencia" runat="server" CssClass="combo100porc" Skin="Bootstrap"></telerik:RadTextBox>
                            </div>
                        </div>
                        <div>
                            <div class="col-lg-2 negrita">Marca</div>
                            <div class="col-lg-4">
                                <telerik:RadTextBox Enabled="false" ID="rtbMarca" DisabledStyle-BackColor="WhiteSmoke" runat="server" CssClass="combo100porc" Skin="Bootstrap"></telerik:RadTextBox>
                            </div>
                            <div class="col-lg-2 negrita">Tipo Vehiculo</div>
                            <div class="col-lg-4">
                                <telerik:RadTextBox Enabled="false" ID="rtbTipoVehiculo" DisabledStyle-BackColor="WhiteSmoke" runat="server" CssClass="combo100porc" Skin="Bootstrap"></telerik:RadTextBox>
                            </div>
                        </div>
                        <div>
                            <div class="col-lg-2 negrita">Modelo</div>
                            <div class="col-lg-4">
                                <telerik:RadTextBox Enabled="false" ID="rtbModelo" DisabledStyle-BackColor="WhiteSmoke" runat="server" CssClass="combo100porc" Skin="Bootstrap"></telerik:RadTextBox>
                            </div>
                            <div class="col-lg-2 negrita">Origen</div>
                            <div class="col-lg-4">
                                <telerik:RadTextBox Enabled="false" ID="Origen" DisabledStyle-BackColor="WhiteSmoke" runat="server" CssClass="combo100porc" Skin="Bootstrap"></telerik:RadTextBox>
                            </div>
                        </div>
                    </fieldset>
                    <fieldset>
                        <legend>Datos de la Cotizacion
                        </legend>
                        <div>
                            <div class="col-lg-2 negrita">Precio Vehiculo</div>
                            <div class="col-lg-4">
                                <telerik:RadNumericTextBox ID="rntPrecioVehiculo" DisabledStyle-BackColor="WhiteSmoke" runat="server" Enabled="false" CssClass="combo100porc" ClientEvents-OnBlur="Onblur" NumberFormat-DecimalDigits="2" MinValue="0" Skin="Bootstrap"></telerik:RadNumericTextBox>
                            </div>
                            <div class="col-lg-2 negrita">Monto Asegurado</div>
                            <div class="col-lg-4">
                                <telerik:RadNumericTextBox ID="rntMontoAsegurado" DisabledStyle-BackColor="WhiteSmoke" runat="server" Enabled="false" CssClass="combo100porc" ClientEvents-OnBlur="Onblur" NumberFormat-DecimalDigits="2" MinValue="0" Skin="Bootstrap"></telerik:RadNumericTextBox>
                            </div>
                        </div>
                        <div>
                            <div class="col-lg-2 negrita">Tiempo (Meses)</div>
                            <div class="col-lg-4">
                                <telerik:RadNumericTextBox ID="rntTiempo" runat="server" AutoPostBack="true" CssClass="combo100porc" ClientEvents-OnBlur="Onblur" NumberFormat-DecimalDigits="0" MinValue="0" NumberFormat-GroupSeparator="" Skin="Bootstrap"></telerik:RadNumericTextBox>
                            </div>
                            <div class="col-lg-2 negrita">Tasa</div>
                            <div class="col-lg-4">
                                <telerik:RadNumericTextBox ID="rntTasa" runat="server" DisabledStyle-BackColor="WhiteSmoke" CssClass="combo100porc" ClientEvents-OnBlur="Onblur" Enabled="false" NumberFormat-DecimalDigits="2" MinValue="0" Skin="Bootstrap"></telerik:RadNumericTextBox>
                            </div>
                        </div>
                        <div>
                            <div class="col-lg-2 negrita">Costo Prima</div>
                            <div class="col-lg-4">
                                <telerik:RadNumericTextBox ID="rntCostoPrima" DisabledStyle-BackColor="WhiteSmoke" runat="server" CssClass="combo100porc" ClientEvents-OnBlur="Onblur" NumberFormat-DecimalDigits="2" MinValue="0" Skin="Bootstrap"></telerik:RadNumericTextBox>
                            </div>
                            <div class="col-lg-2 negrita">Descuento</div>
                            <div class="col-lg-4">
                                <telerik:RadNumericTextBox ID="rntDescuento" runat="server" AutoPostBack="true" ClientEvents-OnBlur="Onblur" CssClass="combo100porc" NumberFormat-DecimalDigits="2" MinValue="0" Skin="Bootstrap"></telerik:RadNumericTextBox>
                            </div>
                        </div>
                        <div>
                            <div class="col-lg-2 negrita">Costo Total</div>
                            <div class="col-lg-4">
                                <telerik:RadNumericTextBox ID="rntCostoTotal" DisabledStyle-BackColor="WhiteSmoke" Enabled="false" runat="server" ClientEvents-OnBlur="Onblur" CssClass="combo100porc" NumberFormat-DecimalDigits="2" MinValue="0" Skin="Bootstrap"></telerik:RadNumericTextBox>
                            </div>
                            <div class="col-lg-6">
                                <telerik:RadButton ID="rbCalcular" runat="server" Text="Calcular" AutoPostBack="true" OnClientClicking="onCalcular" Skin="Bootstrap">
                                    <Icon PrimaryIconCssClass="fa fa-calculator" PrimaryIconTop="10px" />
                                </telerik:RadButton>
                            </div>
                        </div>
                    </fieldset>
                </div>
                <div class="btn-group pull-right" role="group">
                    <telerik:RadButton ID="rtbSave" runat="server" Text="Guardar" AutoPostBack="true" Skin="Bootstrap" OnClientClicking="onSaveClicking">
                        <Icon PrimaryIconCssClass="fa fa-save" PrimaryIconTop="10px" />
                    </telerik:RadButton>
                    <telerik:RadButton ID="rtbCancel" runat="server" Text="Cancelar" AutoPostBack="true" Skin="Bootstrap" CssClass="btn-danger" OnClientClicking="onCancelClicking">
                        <Icon PrimaryIconCssClass="fa fa-times" PrimaryIconTop="10px" />
                    </telerik:RadButton>
                </div>
            </asp:Panel>
        </div>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script type="text/javascript" src="Scripts/Poliza.js"></script>
</asp:Content>
