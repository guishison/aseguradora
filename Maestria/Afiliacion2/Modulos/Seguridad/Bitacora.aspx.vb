Imports BEM = BEntities.Bitacora
Imports BCM = BComponents.Bitacora
Imports BEB = BEntities.Base
Imports BCB = BComponents.Base
Imports BES = BEntities.Seguridad
Imports BCS = BComponents.Bitacora
Imports BCBT = BComponents.Bitacora
Imports BEBT = BEntities.Bitacora
Imports BE = BEntities
Imports Telerik.Web.UI
Imports AsyncLib
Imports AsyncLib.Model.CL.Arguments
Imports System.IO

Public Class Bitacora
    Inherits UtilsMethods
    Private bcParametroLogin As New BCM.BitacoraGeneral
    'Private beCaja As BEM.Caja


    Private bcBitacoraErrores As New BCBT.BitacoraError


    Protected Overloads Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            proInit_Form()

            'rcbPersonal.Filter = RadComboBoxFilter.Contains
            'rcbSala.Filter = RadComboBoxFilter.Contains

        End If
    End Sub

    Private Sub ControlarError(ByVal Metodo As String, ByVal Sms As String)
        Dim beBitacoraErrores As BEBT.BitacoraError = New BEBT.BitacoraError
        With beBitacoraErrores
            .StatusType = BEntities.StatusType.Insert
            .Evento = Metodo
            .Url = MyBase.ObtenerUrl()
            .MensajeError = Sms
            .Version = MyBase.ObtenerVersion()
            .SistemaOperativo = MyBase.ObtenerSistemaOperativo()
            .Navegador = MyBase.ObtenerNavegador()
            .PersonalId = funGet_UserCode()
            .FechaReg = Now
        End With
        bcBitacoraErrores.Save(beBitacoraErrores)
        MyBase.proShow_Message("Error, verifique lo siguiente: " & Sms)
    End Sub
    Private Sub proInit_Form()
        Try
            'buscartext()
            Dim ListaParametroLogin As List(Of BEM.BitacoraGeneral) = bcParametroLogin.Listas()
            For Each item In ListaParametroLogin
                item.FechaReg = MyBase.GetHoraBoliviana(item.FechaReg)
            Next
            MyBase.proLoad_RadGrid(rgvGrid, ListaParametroLogin)
        Catch ex As Exception
            ControlarError(MyBase.ObtenerMetodo, If(ex.InnerException IsNot Nothing, ex.InnerException.ToString, ex.Message))
        End Try
    End Sub

    Protected Sub rgvGrid_PageIndexChanged(sender As Object, e As GridPageChangedEventArgs) Handles rgvGrid.PageIndexChanged
        sender.CurrentPageIndex = e.NewPageIndex
        proInit_Form()
    End Sub


    Private Sub ClearFields()
        'rtbName.Text = String.Empty
        'rcbSala.Text = String.Empty
        'rcbMoneda.Text = String.Empty
        'rtbMontoInicial.Text = String.Empty

    End Sub


    'Private Sub grid2_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles grid2.ItemDataBound
    '    If TypeOf e.Item Is GridDataItem Then
    '        Dim dataItem As GridDataItem = DirectCast(e.Item, GridDataItem)
    '        Dim item As GridDataItem = e.Item
    '        'Dim currentRow As DataRowView = DirectCast(e.Item.DataItem, DataRowView)
    '        For Each col As GridColumn In grid2.Columns
    '            If col.UniqueName = "FullName" Then
    '                'Dim strTxt As String = currentRow.Row("FullName").ToString
    '                Dim ubicacion As String = dataItem("FullName").Text
    '                CType(dataItem("FullName2").Controls(0), HyperLink).NavigateUrl = dataItem("FullName").Text
    '                'CType(dataItem("FullName").Controls(0), HyperLink). = ubicacion
    '            End If

    '        Next
    '        'Dim CuotaID As String = item.GetDataKeyValue("Operacion").ToString()
    '        'If (CuotaID.Equals("Egreso")) Then
    '        '    dataItem("MontoConvertido").ForeColor = Color.Red
    '        '    dataItem("MontoConvertido").Font.Bold = True
    '        '    dataItem("MontoConvertido").Text = CInt(Math.Round(CDec(item.GetDataKeyValue("MontoConvertido").ToString()) * -1, 2)).ToString("#,##0")
    '        'End If
    '    End If
    'End Sub

    'Private Sub SuperBoton_Click(sender As Object, e As EventArgs) Handles SuperBoton.Click
    '    Try

    '        Dim aqui = Environment.GetEnvironmentVariable("C:\")
    '        Dim aqui2 = Environment.GetEnvironmentVariable("WINDIR")
    '        Dim aqui3 = aqui2 + "\explorer.exe"
    '        aqui = "C:\D"
    '        aqui2 = aqui2
    '        Process.Start("www.google.com.bo")
    '        System.Diagnostics.Process.Start(aqui2 + "\explorer.exe", "www.google.com.bo")
    '        'Dim objShell = CreateObject("Shell.Application")
    '    Catch ex As Exception
    '        MyBase.proShow_Message(ex.Message)
    '    End Try

    'End Sub
End Class