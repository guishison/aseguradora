﻿Imports BEM = BEntities.Seguridad
Imports BCM = BComponents.Seguridad
Imports BEB = BEntities.Base
Imports BCB = BComponents.Base
Imports BES = BEntities.Seguridad
Imports BCS = BComponents.Seguridad
Imports BCBT = BComponents.Bitacora
Imports BEBT = BEntities.Bitacora
Imports BE = BEntities
Imports Telerik.Web.UI
Imports AsyncLib
Imports AsyncLib.Model.CL.Arguments
Imports System.IO

Public Class ParametroLogin
    Inherits UtilsMethods
    Private bcParametroLogin As New BCM.ParametroLogin
    'Private beCaja As BEM.Caja

    Private bcPersonal As New BCS.Personal
    Private bcClasificadores As New BCB.Clasificadores

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
            Dim ListaParametroLogin As List(Of BEM.ParametroLogin) = bcParametroLogin.ListarTodo(BEM.relParametroLogin.NivelPassword)
            For Each item In ListaParametroLogin
                item.FechaRegistro = MyBase.GetHoraBoliviana(item.FechaRegistro)
            Next
            MyBase.proLoad_RadGrid(rgvGrid, ListaParametroLogin)
        Catch ex As Exception
            ControlarError(MyBase.ObtenerMetodo, If(ex.InnerException IsNot Nothing, ex.InnerException.ToString, ex.Message))
        End Try
    End Sub
    Protected Sub rgvGrid_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles rgvGrid.ItemCommand
        Try
            Dim BePrivilegios As New BEntities.Seguridad.Privilegio
            Dim BcPrivilegios As New BComponents.Seguridad.Privilegio

            BePrivilegios = BcPrivilegios.BuscarPrivilegioPorPersonal(funGet_UserCode(), 2011, funGet_UnidadNegocio())

            If e.CommandName = "EditProvider" Then
                If BePrivilegios.Permiso.Contains("M") Then
                    Dim item As GridDataItem = e.Item
                    Dim ProviderId As String = item.GetDataKeyValue("Id").ToString()
                    LoadProvider(CLng(ProviderId))
                Else
                    ScriptManager.RegisterClientScriptBlock(Page,
                                                            Me.GetType,
                                                            "cerrarpopUp",
                                                            "closeDialog('divDetail'); funShow_Message('" & BePrivilegios.NoUpdate & "', 'ERROR');",
                                                            True)
                End If
            ElseIf e.CommandName = "DeleteProvider" Then
                If BePrivilegios.Permiso.Contains("B") Then
                    Dim item As GridDataItem = e.Item
                    Dim ProviderId As String = item.GetDataKeyValue("Id").ToString()
                    DeleteProvider(CLng(ProviderId))
                        proInit_Form()

                    ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "funShow_Message('Listado Actualizado', 'INFORMATION');", True)

                Else
                    ScriptManager.RegisterClientScriptBlock(Page,
                                                            Me.GetType,
                                                            "cerrarpopUp",
                                                            "closeDialog('divDetail'); funShow_Message('" & BePrivilegios.NoDelete & "', 'ERROR');",
                                                            True)
                End If
            End If
        Catch ex As Exception
            ControlarError(MyBase.ObtenerMetodo, If(ex.InnerException IsNot Nothing, ex.InnerException.ToString, ex.Message))
        End Try
    End Sub

    Protected Sub rgvGrid_PageIndexChanged(sender As Object, e As GridPageChangedEventArgs) Handles rgvGrid.PageIndexChanged
        sender.CurrentPageIndex = e.NewPageIndex
        proInit_Form()
    End Sub


    Protected Sub rbAdd_Click(sender As Object, e As EventArgs) Handles rbAdd.Click
        Try
            Dim BePrivilegios As New BEntities.Seguridad.Privilegio
            Dim BcPrivilegios As New BComponents.Seguridad.Privilegio
            BePrivilegios = BcPrivilegios.BuscarPrivilegioPorPersonal(funGet_UserCode(), 2011, funGet_UnidadNegocio())
            If BePrivilegios.Permiso.Contains("A") Then
                LoadProvider(0)
            Else
                ScriptManager.RegisterClientScriptBlock(Page,
                                                            Me.GetType,
                                                            "cerrarpopUp",
                                                            "closeDialog('divDetail'); funShow_Message('" & BePrivilegios.NoInsert & "', 'ERROR');",
                                                            True)
            End If
        Catch ex As Exception
            ControlarError(MyBase.ObtenerMetodo, If(ex.InnerException IsNot Nothing, ex.InnerException.ToString, ex.Message))
        End Try
    End Sub

    Protected Sub rtbSave_Click(sender As Object, e As EventArgs) Handles rtbSave.Click

        SaveProvider()
        proInit_Form()
    End Sub

    Private Sub LoadProvider(ByVal ParametroLoginId As Long)
        Dim NivelPassword = bcClasificadores.List(BE.TipoClasificadores.NivelesPassword)
        proLoad_RadComboBox(rcbNivelPassword, NivelPassword, "Id", "Nombre")
        If ParametroLoginId = 0 Then
            ViewState("ProviderId") = 0
            ClearFields()
        Else
            Dim beParametroLogin As BEM.ParametroLogin = bcParametroLogin.Search(ParametroLoginId)
            With beParametroLogin
                ViewState("ProviderId") = .Id
                rntLongitudMinima.Value = .LongitudMinima
                rntLongitudMaxima.Value = .LongitudMaxima
                rcbNivelPassword.SelectedValue = .NivelPassword
                rcbExpiracion.SelectedValue = .Expiracion
                rntCantidadIntentosFallidos.Value = .CantidadIntentosFallidos
                rntTiempoBloqueo.Value = .TiempoBloqueo
                rntComprobacionPasswordAnteriores.Value = .ComprobacionesPasswordAnteriores
                'rtbName.Text = .Nombre
                'rcbSala.SelectedValue = .SalaId
                'rcbPersonal.SelectedValue = .PersonalCajaId
            End With
        End If
    End Sub
    Private Sub DeleteProvider(ByVal ProviderId As Long)
        Dim becaja As BEM.ParametroLogin = New BEM.ParametroLogin
        becaja = bcParametroLogin.Search(ProviderId)
        With becaja
            .Id = ProviderId
            .PersonalId = MyBase.funGet_UserCode
            .StatusType = BEntities.StatusType.Delete
            .Estado = BEntities.Estado.Inactivo
        End With
        bcParametroLogin.Guardar(becaja)
        ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "closeDialog('divDetail'); funShow_Message('Listado Actualizado', 'INFORMATION');", True)
    End Sub

    Private Sub SaveProvider()
        Dim beParametroLogin As BEM.ParametroLogin = New BEM.ParametroLogin
        Dim ListParametroLogin As List(Of BEM.ParametroLogin) = New List(Of BEM.ParametroLogin)
        Try
            With beParametroLogin
                If CLng(ViewState("ProviderId")) = 0 Then
                    .StatusType = BEntities.StatusType.Insert
                    .Id = 0
                    .PersonalId = MyBase.funGet_UserCode()
                Else
                    .StatusType = BEntities.StatusType.Insert
                    .Id = CInt(ViewState("ProviderId"))
                End If
                .FechaRegistro = Now
                .LongitudMinima = rntLongitudMinima.Value
                .LongitudMaxima = rntLongitudMaxima.Value
                .NivelPassword = rcbNivelPassword.SelectedValue
                .Expiracion = rcbExpiracion.SelectedValue
                .CantidadIntentosFallidos = rntCantidadIntentosFallidos.Value
                .TiempoBloqueo = rntTiempoBloqueo.Value
                .ComprobacionesPasswordAnteriores = rntComprobacionPasswordAnteriores.Value
                .PersonalModificacionId = MyBase.funGet_UserCode
                .FechaModificacion = Now
                .Estado = BEntities.Estado.Activo
            End With
            Dim modificacion = bcParametroLogin.Listar.FirstOrDefault
            If modificacion IsNot Nothing Then
                With modificacion
                    .StatusType = BE.StatusType.Delete
                    .PersonalModificacionId = funGet_UserCode()
                    .FechaModificacion = Now
                    .Estado = BE.Estado.Inactivo
                End With
                ListParametroLogin.Add(modificacion)
            End If

            ListParametroLogin.Add(beParametroLogin)
            bcParametroLogin.Guardar(ListParametroLogin)
            ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "closeDialog('divDetail'); funShow_Message('Caja Guardado', 'INFORMATION');", True)

                ClearFields()
        Catch ex As Exception
            'ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "closeDialog('divDetail'); funShow_Message('Error al Guardar, verifique lo siguiente: " & ex.Message & "', 'VALIDATION');", True)
            ControlarError(MyBase.ObtenerMetodo, If(ex.InnerException IsNot Nothing, ex.InnerException.ToString, ex.Message))
            ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "closeDialog('divDetail');", True)
        End Try
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