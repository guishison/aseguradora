<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/assets/master/MasterPage.Master" CodeBehind="Personal.aspx.vb" Inherits="Afiliacion2.Personal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .Base {
            display: inline-block;
            font: 12px/18px "segoe ui",arial,sans-serif;
            height: 20px;
            overflow: hidden;
            text-align: center;
            vertical-align: middle;
            width: 121px;
            color: #fff;
            border: 1px solid #333;
        }

        .L0 {
            border: 0 none;
        }

        .L1 {
            background-color: #ff3933;
        }

        .L2 {
            background-color: #ff6633;
        }

        .L3 {
            background-color: #ff3399;
        }

        .L4 {
            background-color: #cccc33;
        }

        .L5 {
            background-color: #33cc00;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="content-header">
        <h1>Personal
                <small>Usuarios, personal de venta, telemarketing y administrativos</small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-suitcase"></i>Inicio</a></li>
            <li><a href="#"><i class="fa fa-suitcase"></i>Administración</a></li>
            <li><a href="#"><i class="fa fa-suitcase"></i>Usuarios</a></li>
            <li class="active">Personal</li>
        </ol>
    </section>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnRequestStart="AjustarPanel();" UpdatePanelsRenderMode="Inline">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="rbAdd">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="paDetail" UpdatePanelCssClass="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="rbFilter">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="rgvUsersList" UpdatePanelCssClass="" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="rgvUsersList">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="paDetail" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="rcbPosition">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="rcbSupervisor" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="rnbFee" UpdatePanelCssClass="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="rbAccept">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="rgvUsersList" UpdatePanelCssClass="" />
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
                            <h3 class="box-title">Lista de personal</h3>
                            <div class="box-tools pull-right">
                                <div class="btn-group pull-right" role="group">
                                    <telerik:RadButton ID="rbAdd" runat="server" Text="Nuevo" AutoPostBack="true" Skin="Bootstrap" OnClientClicking="onAddClicking">
                                        <Icon PrimaryIconCssClass="fa fa-plus" PrimaryIconTop="10px" />
                                    </telerik:RadButton>
                                </div>
                            </div>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="input-group">
                                        <div class="input-group-btn">
                                            <button class="btn btn-default" type="button">Filtar cargo</button>
                                        </div>
                                        <telerik:RadComboBox ID="rcbPositionFilter" runat="server" CssClass="combo100porc" Skin="Bootstrap" OnClientKeyPressing="radComboKeyPress" MaxHeight="150" EnableTextSelection="true" MarkFirstMatch="true" DropDownAutoWidth="Enabled" Filter="Contains" />
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="input-group">
                                        <div class="input-group-btn">
                                            <button class="btn btn-default" type="button">Filtrar texto</button>
                                        </div>
                                        <telerik:RadTextBox ID="rtbNameFilter1" runat="server" EmptyMessage="Buscar personal" Skin="Bootstrap" >
                                            <ClientEvents OnKeyPress="OnKeyPressEnterSearch" />
                                            </telerik:RadTextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <telerik:RadButton ID="rbFilter" runat="server" Text="Buscar" AutoPostBack="true" Skin="Bootstrap">
                                        <Icon PrimaryIconCssClass="fa fa-search" PrimaryIconTop="10px" />
                                    </telerik:RadButton>
                                </div>
                            </div>
                            <div class="row">
                                <br />
                            </div>
                            <div class="dataTables_wrapper form-inline" role="grid">
                                <telerik:RadAjaxPanel ID="rapList" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
                                    <telerik:RadGrid ID="rgvUsersList" CssClass="table-responsive" runat="server" AutoGenerateColumns="False" PageSize="10" GroupPanelPosition="Top" Skin="Bootstrap" VirtualItemCount="5">
                                        <MasterTableView GroupLoadMode="Client" AutoGenerateColumns="False" DataKeyNames="Id,Nombre,Correo,Login,Password" HierarchyLoadMode="Client" HierarchyDefaultExpanded="False" AllowCustomPaging="True" AllowPaging="True">
                                            <RowIndicatorColumn Visible="False">
                                                <HeaderStyle Width="41px" />
                                            </RowIndicatorColumn>
                                            <ExpandCollapseColumn Created="True">
                                                <HeaderStyle Width="41px" />
                                            </ExpandCollapseColumn>
                                            <Columns>
                                                <telerik:GridTemplateColumn HeaderText="Acciones" UniqueName="Acciones" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <div class="btn-group btn-group-xs" role="group">
                                                            <telerik:RadButton ID="btnEditUser" CommandName="EditUser" ToolTip="Modificar" runat="server" AutoPostBack="true" Skin="Bootstrap" OnClientClicking="OnEditUser">
                                                                <Icon PrimaryIconCssClass="fa fa-pencil" PrimaryIconLeft="15px" PrimaryIconTop="6px"></Icon>
                                                            </telerik:RadButton>
                                                            <%--<telerik:RadButton ID="btnDeleteUser" CommandName="DeleteUser" ToolTip="Eliminar Usuario" runat="server" AutoPostBack="true" Skin="Bootstrap" OnClientClicking="ConfirmDelete">
                                                                <Icon PrimaryIconCssClass="fa fa-trash-o" PrimaryIconLeft="15px" PrimaryIconTop="6px"></Icon>
                                                            </telerik:RadButton>--%>
                                                            <telerik:RadButton ID="rbStateUser" CommandName="StateUser" runat="server" AutoPostBack="true" Skin="Bootstrap" OnClientClicking="ConfirmChangeStateUser">
                                                                <Icon PrimaryIconCssClass="fa fa-check-circle" PrimaryIconLeft="15px" PrimaryIconTop="6px"></Icon>
                                                            </telerik:RadButton>
                                                        </div>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="95px" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn DataField="Id" HeaderText="Code" UniqueName="Id" Display="false">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="PersonalEstado" HeaderText="Estado" UniqueName="Estado" DataType="System.Int32" Display="false">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Login" HeaderText="Login" UniqueName="Login" HeaderStyle-HorizontalAlign="Center" FilterControlAltText="Filter Login column">
                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Nombre" HeaderText="Nombre" UniqueName="Nombre" HeaderStyle-HorizontalAlign="Center" FilterControlAltText="Filter Name column">
                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Rut" HeaderText="Rut" UniqueName="RUT" HeaderStyle-HorizontalAlign="Center" FilterControlAltText="Filter Name column">
                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Cargo.Nombre" HeaderText="Cargo" UniqueName="Cargo" HeaderStyle-HorizontalAlign="Center" FilterControlAltText="Filter Nam column">
                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Supervisor.Nombre" HeaderText="Supervisor" UniqueName="Supervisor" HeaderStyle-HorizontalAlign="Center" FilterControlAltText="Filter Supervisor column">
                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="UNSucursal.Nombre" HeaderText="Unidad Negocio" UniqueName="Email" HeaderStyle-HorizontalAlign="Center" FilterControlAltText="Filter Email column">
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
                                </telerik:RadAjaxPanel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <div id="divDetail" class="dialog" style="display: none;">
            <asp:Panel ID="paDetail" runat="server" CssClass="MinWidth467">
                <table style="width: 100%">
                    <tr>
                        <td>Nombre:
                        </td>
                        <td>
                            <telerik:RadTextBox ID="rtbName" runat="server" EmptyMessage="Ingrese el nombre del personal" CssClass="combo100porc" Skin="Bootstrap">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>RUT:
                        </td>
                        <td>
                            <telerik:RadTextBox ID="rtbRut" runat="server" EmptyMessage="Ingrese el RUT del cliente" CssClass="combo100porc" Skin="Bootstrap">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Email:
                        </td>
                        <td>
                            <telerik:RadTextBox ID="rtbEmail" runat="server" EmptyMessage="Ingrese el email de cliente" CssClass="combo100porc" Skin="Bootstrap">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Teléfono:
                        </td>
                        <td>
                            <telerik:RadTextBox ID="rtbTelefono" runat="server" EmptyMessage="Ingrese el teléfono del personal" Skin="Bootstrap" MaxLength="20" InputType="text">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Dependencia:
                        </td>
                        <td>
                            <telerik:RadComboBox ID="rcbDependencia" runat="server" CssClass="combo100porc" Skin="Bootstrap" OnClientKeyPressing="radComboKeyPress" MaxHeight="150" EnableTextSelection="true" MarkFirstMatch="true" DropDownAutoWidth="Enabled" Filter="Contains">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Unidad Negocio:
                        </td>
                        <td>
                            <telerik:RadComboBox ID="rcbUnidadNegocio" runat="server" CssClass="combo100porc" Skin="Bootstrap" OnClientKeyPressing="radComboKeyPress" MaxHeight="150" EnableTextSelection="true" MarkFirstMatch="true" DropDownAutoWidth="Enabled" Filter="Contains">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Departamento:
                        </td>
                        <td>
                            <telerik:RadComboBox ID="rcbDepartamento" runat="server" CssClass="combo100porc" OnClientKeyPressing="radComboKeyPress" MaxHeight="150" EnableTextSelection="true" MarkFirstMatch="true" DropDownAutoWidth="Enabled" Filter="Contains" Skin="Bootstrap">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Cargo:
                        </td>
                        <td>
                            <telerik:RadComboBox ID="rcbPosition" runat="server" AutoPostBack="True" CssClass="combo100porc" Skin="Bootstrap" OnClientKeyPressing="radComboKeyPress" MaxHeight="150" EnableTextSelection="true" MarkFirstMatch="true" DropDownAutoWidth="Enabled" Filter="Contains">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Supervisor:
                        </td>
                        <td>
                            <telerik:RadComboBox ID="rcbSupervisor" runat="server" CssClass="combo100porc" Skin="Bootstrap" OnClientKeyPressing="radComboKeyPress" MaxHeight="150" EnableTextSelection="true" MarkFirstMatch="true" DropDownAutoWidth="Enabled" Filter="Contains">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Comision:
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="rnbFee" runat="server" Skin="Bootstrap" MaxValue="100" MinValue="0">
                            </telerik:RadNumericTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Login:
                        </td>
                        <td>
                            <telerik:RadTextBox ID="rtbLogin" runat="server" EmptyMessage="Ingrese el login para sesión" CssClass="combo100porc" Skin="Bootstrap">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Cambiar Password:
                        </td>
                        <td>
                            <telerik:RadCheckBox ID="rbCambioPassword" CssClass="btnAgree" runat="server" ></telerik:RadCheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Password:
                        </td>
                        <td>
                            <telerik:RadTextBox ID="rtbPassword" runat="server" EmptyMessage="Ingrese su contraseña" Skin="Bootstrap" TextMode="Password" Style="display: block;" onkeyup="PasswordCheker()" CssClass="combo100porc">
                                <PasswordStrengthSettings ShowIndicator="true" TextStrengthDescriptions="Seguriad 1 de 5;Seguridad 2 de 5; Seguridad 3 de 5;Seguridad 4 de 5;Seguridad 5 de 5"
                                    IndicatorElementBaseStyle="Base" TextStrengthDescriptionStyles="L0;L1;L2;L3;L4;L5"
                                    IndicatorElementID="CustomIndicator" MinimumLowerCaseCharacters="1" MinimumNumericCharacters="1" MinimumSymbolCharacters="1" MinimumUpperCaseCharacters="1" PreferredPasswordLength="6"></PasswordStrengthSettings>
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Confirmar:
                        </td>
                        <td>
                            <telerik:RadTextBox ID="rtbConfirmPassword" runat="server" EmptyMessage="Repita la contraseña" Skin="Bootstrap" TextMode="Password" onkeyup="PasswordCheker()" Width="300">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <span id="CustomIndicator">&nbsp;</span> <span id="PasswordRepeatedIndicator" class="Base L0">&nbsp;</span>
                            <asp:HiddenField ID="hdfPassword" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>Estado:
                        </td>
                        <td>
                            <div class="col-md-12">
                                <div class="radio">
                                    <label class="col-md-5">
                                        <asp:RadioButton ID="rbStateActivated" runat="server" Checked="true" GroupName="StateUser" />
                                        Activado
                                    </label>
                                    <label class="col-md-5">
                                        <asp:RadioButton ID="rbStateDeactivated" runat="server" Checked="false" GroupName="StateUser" />
                                        Desactivado
                                    </label>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div class="btn-group pull-right" role="group">
                                <telerik:RadButton ID="rbAccept" runat="server" Text="Guardar" AutoPostBack="true" Skin="Bootstrap" OnClientClicking="funValidate">
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
    <script type="text/javascript" src="Scripts/Personal.js"></script>
    <style type="text/css">
        #ContentPlaceHolder1_rbStateActivated {
            float: none;
        }

        #ContentPlaceHolder1_rbStateDeactivated {
            float: none;
        }
    </style>
</asp:Content>
