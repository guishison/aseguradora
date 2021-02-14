<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/assets/master/MasterPage.Master" CodeBehind="Bitacora.aspx.vb" Inherits="Afiliacion2.Bitacora" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content-header">
        <h1>Parametro de Login
                <small>Definicion de los parametros del login</small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-suitcase"></i>Inicio</a></li>
            <li><a href="#"><i class="fa fa-suitcase"></i>Caja</a></li>
            <li class="active">Lista de Parametros de Login</li>
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
                <telerik:AjaxSetting AjaxControlID="rgvGrid">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="paDetail" UpdatePanelCssClass="" />
                        <telerik:AjaxUpdatedControl ControlID="rgvGrid" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="grid2">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="grid2" />
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
                
                <telerik:AjaxSetting AjaxControlID="rtbFilter">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="rgvGrid" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="SuperBoton">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="SuperBoton" />
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
                            <h3 class="box-title">Lista de cajas</h3>
                            <div class="box-tools pull-right">
                                <div class="btn-group pull-right" role="group">
                                    <telerik:RadButton ID="rbAdd" runat="server" Text="Nuevo" AutoPostBack="true" Skin="Bootstrap" OnClientClicking="onAddClicking">
                                        <Icon PrimaryIconCssClass="fa fa-plus" PrimaryIconTop="10px"></Icon>
                                    </telerik:RadButton>
                                </div>
                            </div>
                        </div>
                        <div class="box-body table-responsive">
                            <div class="row">
                                <br />
                            </div>
                            <div class="dataTables_wrapper form-inline" role="grid">
                                <telerik:RadGrid ID="rgvGrid" runat="server" CssClass="table-responsive" AutoGenerateColumns="False" GridLines="None" AllowPaging="True" CellSpacing="0" VirtualItemCount="5" PageSize="20" Skin="Bootstrap">
                                    <MasterTableView AutoGenerateColumns="False" DataKeyNames="Id" AllowCustomPaging="false" AllowPaging="true">
                                        <Columns>
                                            <telerik:GridTemplateColumn HeaderText="Nro." UniqueName="ItemIndex" ItemStyle-Font-Bold="True">
                                                <ItemTemplate>
                                                    <%# (rgvGrid.MasterTableView.CurrentPageIndex * rgvGrid.MasterTableView.PageSize) + Container.ItemIndex + 1 %>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="Id" HeaderText="Nº" UniqueName="Id" Display="false" />
                                            <telerik:GridBoundColumn DataField="Campos" HeaderText="Campos" UniqueName="LongitudMaxima" />
                                            <telerik:GridBoundColumn DataField="IPCliente" HeaderText="IP Cliente" UniqueName="LongitudMinima" HeaderStyle-HorizontalAlign="Center" />
                                            <telerik:GridBoundColumn DataField="MaquinaCliente" HeaderText="Maquina Cliente" UniqueName="NivelPassword" HeaderStyle-HorizontalAlign="Center" />
                                            <telerik:GridBoundColumn DataField="Accion" HeaderText="Accion" UniqueName="Expiracion" HeaderStyle-HorizontalAlign="Center" />
                                            <telerik:GridBoundColumn DataField="Transaccion" HeaderText="Transaccion" UniqueName="CantidadIntentosFallidos" HeaderStyle-HorizontalAlign="Center" />
                                            <telerik:GridBoundColumn DataField="FechaReg" HeaderText="Fecha Registro" UniqueName="TiempoBloqueo" DataType="System.DateTime" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" HeaderStyle-HorizontalAlign="Center" />
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
        <div id="divDetail" class="box-body" style="display: none;">
            <asp:Panel ID="paDetail" CssClass="MinWidth560" runat="server">
                <table class="combo100porc">
                    <tr>
                        <td class="combo20porc">Longitud Mínima:
                        </td>
                        <td class="combo80porc">
                            <%--<telerik:RadNumericTextBox ID="RadTextBox1" runat="server" Placeholder="Nombre de Caja" Skin="Bootstrap" />--%>
                            <telerik:RadNumericTextBox ID="rntLongitudMinima" runat="server" CssClass="combo100porc" NumberFormat-DecimalDigits="0" MinValue="3" NumberFormat-GroupSeparator="" EmptyMessage="Ingrese una longitud mínima para la contraseña" Skin="Bootstrap" />
                        </td>
                    </tr>
                    <tr>
                        <td class="combo20porc">Longitud Máxima:
                        </td>
                        <td class="combo80porc">
                            <%--<telerik:RadNumericTextBox ID="RadTextBox1" runat="server" Placeholder="Nombre de Caja" Skin="Bootstrap" />--%>
                            <telerik:RadNumericTextBox ID="rntLongitudMaxima" runat="server" CssClass="combo100porc" NumberFormat-DecimalDigits="0" MinValue="3" NumberFormat-GroupSeparator="" EmptyMessage="Ingrese una longitud máxima para la contraseña" Skin="Bootstrap" />
                        </td>
                    </tr>
                    <tr>
                        <td class="combo20porc">Nivel Password:
                        </td>
                        <td class="combo80porc">
                            <telerik:RadComboBox ID="rcbNivelPassword" runat="server" CssClass="combo100porc" OnClientKeyPressing="radComboKeyPress" MaxHeight="150" EnableTextSelection="true" MarkFirstMatch="true" DropDownAutoWidth="Enabled" Filter="Contains" Skin="Bootstrap">
                                <Items>
                                    <telerik:RadComboBoxItem runat="server" Value="1" Text="Sala1" />
                                    <telerik:RadComboBoxItem runat="server" Value="2" Text="Sala2" />
                                    <telerik:RadComboBoxItem runat="server" Value="3" Text="Sala3" />
                                    <telerik:RadComboBoxItem runat="server" Value="4" Text="Sala4" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="combo20porc">Expiración:
                        </td>
                        <td class="combo80porc">
                            
                            <telerik:RadComboBox ID="rcbExpiracion" runat="server" CssClass="combo100porc" OnClientKeyPressing="radComboKeyPress" MaxHeight="150" EnableTextSelection="true" MarkFirstMatch="true" DropDownAutoWidth="Enabled" Filter="Contains" Skin="Bootstrap">
                                <Items>
                                    <telerik:RadComboBoxItem runat="server" Value="-1" Text="Seleccione..." />
                                    <telerik:RadComboBoxItem runat="server" Value="1" Text="SI" />
                                    <telerik:RadComboBoxItem runat="server" Value="0" Text="NO" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    
                    <tr>
                        <td class="combo20porc">Cantidad Intentos Fallidos:
                        </td>
                        <td class="combo80porc">
                            <%--<telerik:RadNumericTextBox ID="RadTextBox1" runat="server" Placeholder="Nombre de Caja" Skin="Bootstrap" />--%>
                            <telerik:RadNumericTextBox ID="rntCantidadIntentosFallidos" runat="server" CssClass="combo100porc" NumberFormat-DecimalDigits="0" MinValue="0" NumberFormat-GroupSeparator="" EmptyMessage="Ingrese la cantidad de intentos fllidoss" Skin="Bootstrap" />
                        </td>
                    </tr>
                    <tr>
                        <td class="combo20porc">Tiempo Bloqueo:
                        </td>
                        <td class="combo80porc">
                            <%--<telerik:RadNumericTextBox ID="RadTextBox1" runat="server" Placeholder="Nombre de Caja" Skin="Bootstrap" />--%>
                            <telerik:RadNumericTextBox ID="rntTiempoBloqueo" runat="server" CssClass="combo100porc" NumberFormat-DecimalDigits="0" MinValue="0" NumberFormat-GroupSeparator="" EmptyMessage="Ingrese un tiempo de bloqueo en segundos" Skin="Bootstrap" />
                        </td>
                    </tr>
                    <tr>
                        <td class="combo20porc">Comprobacion Password Anteriores:
                        </td>
                        <td class="combo80porc">
                            <%--<telerik:RadNumericTextBox ID="RadTextBox1" runat="server" Placeholder="Nombre de Caja" Skin="Bootstrap" />--%>
                            <telerik:RadNumericTextBox ID="rntComprobacionPasswordAnteriores" runat="server" CssClass="combo100porc" NumberFormat-DecimalDigits="0" MinValue="0" NumberFormat-GroupSeparator="" EmptyMessage="Ingrese la cantidad de historicos de contraseñas guardadas" Skin="Bootstrap" />
                        </td>
                    </tr>
                    <%--<tr>
                        <td>Monto Inicial:
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID ="rtbMontoInicial" runat="server" Placeholder="" Skin="Bootstrap"/>
                        </td>
                    </tr>--%>
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

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script type="text/javascript" src="Scripts/ParametroLogin.js"></script>
</asp:Content>
