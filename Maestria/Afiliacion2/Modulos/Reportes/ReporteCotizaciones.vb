Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports Telerik.Reporting
Imports Telerik.Reporting.Drawing

Partial Public Class ReporteCotizaciones
    Inherits Telerik.Reporting.Report
    Public Sub New(ByVal UnidadNegocioId As Int32, ByVal Desde As DateTime, ByVal Hasta As DateTime)
        InitializeComponent()
        Me.SqlReporteVentas.Parameters(0).Value = UnidadNegocioId
        Me.SqlReporteVentas.Parameters(1).Value = Desde
        Me.SqlReporteVentas.Parameters(2).Value = Hasta

        txtDesde.Value = Desde.ToShortDateString
        txtHasta.Value = Hasta.ToShortDateString

    End Sub
End Class