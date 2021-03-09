<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/assets/master/MasterPage.Master" CodeBehind="ReporteCotizacion.aspx.vb" Inherits="Afiliacion2.ReporteVentas" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="Telerik.ReportViewer.WebForms" Namespace="Telerik.ReportViewer.WebForms" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <section class="content-header">
        <h1>REPORTE DE COTIZACIONES
                
        </h1>
    </section>

    <form id="form" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

        <section class="content">
            <div class="row">
                <div class="col-xs-12">
                    <div class="box">
                        <div class="box-header">
                            <h3 class="box-title">Parámetros de búsqueda</h3>
                            <div class="box-tools pull-right">
                            </div>
                        </div>

                        <div class="box-body table-responsive">
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="input-group">
                                        <div class="input-group-btn">
                                            <button class="btn btn-default" type="button">DESDE</button>
                                        </div>

                                        <telerik:RadDatePicker ID="rdpDesde" CssClass="combo100porc" runat="server" AutoPostBack="false" Skin="Bootstrap">
                                        </telerik:RadDatePicker>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="input-group">
                                        <div class="input-group-btn">
                                            <button class="btn btn-default" type="button">HASTA</button>
                                        </div>
                                        <telerik:RadDatePicker ID="rdpHasta" CssClass="combo100porc" runat="server" AutoPostBack="false" Skin="Bootstrap">
                                        </telerik:RadDatePicker>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="input-group">
                                        <div class="input-group-btn">
                                            <button class="btn btn-default" type="button">CONCESIONARIAS</button>
                                        </div>

                                        <telerik:RadComboBox ID="rcbConcesionaria" runat="server" CssClass="combo100porc" OnClientKeyPressing="radComboKeyPress" MaxHeight="150" EnableTextSelection="true" MarkFirstMatch="true" DropDownAutoWidth="Enabled" Filter="Contains" Skin="Bootstrap">
                                        </telerik:RadComboBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="input-group">

                                        <span class="input-group-btn">
                                            <telerik:RadButton ID="rbGenerar" runat="server" AutoPostBack="true" Text="Generar" Skin="Bootstrap">
                                                <Icon PrimaryIconCssClass="fa fa-list-alt" PrimaryIconTop="10px"></Icon>
                                            </telerik:RadButton>
                                        </span>
                                    </div>
                                </div>
                                <div class="col-md-2 text-right">
                                </div>

                            </div>

                            <div class="row">
                                <telerik:ReportViewer runat="server" ID="ReportViewer" Width="100%" Height="900px" ProgressText="Generando Reporte..."></telerik:ReportViewer>
                                <script type="text/javascript">
                                    ReportViewer.prototype.PrintReport = function () {
                                        switch (this.defaultPrintFormat) {
                                            case "Default":
                                                this.DefaultPrint();
                                                break;
                                            case "PDF":
                                                this.PrintAs("PDF");
                                                previewFrame = document.getElementById(this.previewFrameID);
                                                previewFrame.onload = function () { previewFrame.contentDocument.execCommand("print", true, null); }
                                                break;
                                        }
                                    };
                                </script>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>

        <%--  <section>
            
           

        </section>--%>
    </form>



</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
