
Imports Telerik.Web.UI.Calendar
Imports BCR = BComponents.Reportes
Imports BCS = BComponents.Sage
Imports BCP = BComponents.Seguridad
Imports BCB = BComponents.Base

Imports BES = BEntities.Sage
Imports BEB = BEntities.Base
Imports BESE = BEntities.Seguridad
Public Class ReporteVentas
    Inherits UtilsMethods


    Dim bcPersonal As New BCP.Personal
    Dim bcConcesionaria As New BCB.UNSucursal
    Dim bcClasificador As New BCB.Clasificadores

    Protected Overloads Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            CargarCombos()
            CargarFechas()
            LoadReporte()
        End If
    End Sub
    Protected Sub CargarFechas()
        Try
            Me.rdpDesde.SelectedDate = Now
            Me.rdpHasta.SelectedDate = Now
        Catch ex As Exception
            MyBase.proShow_Message(ex.Message)
        End Try
    End Sub

    Protected Sub CargarCombos()

        Try
            Dim ListaConsecionarias As New List(Of BEntities.Base.UNSucursal)
            ListaConsecionarias = bcConcesionaria.ListaByUnidadNegocioId(2)
            MyBase.proLoad_RadComboBox(rcbConcesionaria, ListaConsecionarias, "Id", "Nombre")
            If funGet_UnidadNegocio() > 1 Then
                rcbConcesionaria.SelectedValue = funGet_UnidadNegocio()
                rcbConcesionaria.Enabled = False
            Else
                rcbConcesionaria.Enabled = True
            End If

        Catch ex As Exception
            MyBase.proShow_Message(ex.Message)
        End Try
    End Sub

    Protected Sub LoadReporte()

        Dim desde As DateTime = rdpDesde.SelectedDate
        Dim hasta As DateTime = rdpHasta.SelectedDate
        Dim cad As String = desde.Date.ToString
        If hasta.Date >= desde.Date Then

            Dim objReport As New ReporteCotizaciones(rcbConcesionaria.SelectedValue, desde, hasta)
            Dim objInstance As New Telerik.Reporting.InstanceReportSource
            objInstance.ReportDocument = objReport
            ReportViewer.ReportSource = objInstance
        Else
            MyBase.proShow_Message("Verifique las fechas")
            CargarFechas()
        End If

    End Sub

    Private Sub rbGenerar_Click(sender As Object, e As EventArgs) Handles rbGenerar.Click
        Try
            LoadReporte()
        Catch ex As Exception
            MyBase.proShow_Message(ex.Message)
        End Try
    End Sub

End Class