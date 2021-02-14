<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/assets/master/MasterPage.Master" CodeBehind="Cuenta.aspx.vb" Inherits="Afiliacion2.Cuenta" %>

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

    <!--Dashboard -->
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>Perfil de Usuario
                <small>Edicón de datos personales y contraseña</small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Inicio</a></li>
            <li class="active">Perfil de Usuario</li>
        </ol>
    </section>
    <form id="form" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelsRenderMode="Inline">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="rtbSave">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="paDetail" />
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
                <div class="col-md-6 col-md-push-3">
                    <div class="box">
                        <div class="box-body table-responsive">
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Panel ID="paDetail" runat="server">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td>Nombre Completo:
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="rtbName" runat="server" Width="400" ReadOnly="true" Skin="Bootstrap" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Correo Electrónico:
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="rtbEmail" runat="server" Width="400" ReadOnly="true" Skin="Bootstrap" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Usuario:
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="rtbLogin" runat="server" Width="400" ReadOnly="true" Skin="Bootstrap" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Clave Actual:
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="rtbPassword" TextMode="Password" runat="server" Width="200" Skin="Bootstrap" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Nueva Clave:
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="rtbNewPassword" runat="server" Skin="Bootstrap" TextMode="Password" Style="display: block;" onkeyup="PasswordCheker()" Width="400">
                                                        <PasswordStrengthSettings ShowIndicator="true" TextStrengthDescriptions="Seguriad 1 de 5;Seguridad 2 de 5; Seguridad 3 de 5;Seguridad 4 de 5;Seguridad 5 de 5"
                                                            IndicatorElementBaseStyle="Base" TextStrengthDescriptionStyles="L0;L1;L2;L3;L4;L5"
                                                            IndicatorElementID="CustomIndicator" MinimumLowerCaseCharacters="1" MinimumNumericCharacters="1" MinimumSymbolCharacters="1" MinimumUpperCaseCharacters="1" PreferredPasswordLength="6"></PasswordStrengthSettings>
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Confirmar Nueva Clave:
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="rtbConfirmNewPassword" runat="server" Skin="Bootstrap" TextMode="Password" onkeyup="PasswordCheker()" Width="300">
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td>
                                                    <span id="CustomIndicator">&nbsp;</span> <span id="PasswordRepeatedIndicator" class="Base L0">&nbsp;</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td>
                                                    <br />
                                                    <div class="btn-group pull-right" role="group">
                                                        <telerik:RadButton ID="rtbSave" runat="server" Text="Guardar" AutoPostBack="true" Skin="Bootstrap" OnClientClicking="onSaveClicking">
                                                            <Icon PrimaryIconCssClass="fa fa-save" PrimaryIconTop="10px" />
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
            </div>
        </section>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script type="text/javascript" src="Scripts/Cuenta.js"></script>
</asp:Content>
