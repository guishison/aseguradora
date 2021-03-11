Imports BEM = BEntities.Base
Imports BCM = BComponents.Base
Imports BCBT = BComponents.Bitacora
Imports BEBT = BEntities.Bitacora
Imports BCS = BComponents.Seguridad
Imports BE = BEntities
Imports Telerik.Web.UI
Imports BCB = BComponents.Base

Public Class ClasificadoresTipo
    Inherits UtilsMethods
    Private bcBitacoraErrores As New BCBT.BitacoraError
    Private beBitacoraErrores As New BEBT.BitacoraError
    Private bcClasificadoresTipos As New BCM.ClasificadoresTipo
    Private beClasificadoresTipo As BEM.ClasificadoresTipo
    Private bcFormulario As New BCS.Formulario

    Private Overloads Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            proInit_Form()

        End If
    End Sub

    Private Sub ControlarError(ByVal Metodo As String, ByVal Sms As String)
        beBitacoraErrores = New BEBT.BitacoraError
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
        MyBase.proShow_Message("Verifique lo siguiente: " & Sms)
    End Sub

    Protected Sub rgvGrid_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles rgvGrid.ItemCommand
        Try
            Dim BePrivilegios As New BEntities.Seguridad.Privilegio
            Dim BcPrivilegios As New BComponents.Seguridad.Privilegio

            Dim beFormularios = bcFormulario.BuscarPorUnidadNegocio(BE.URLFormularios.TipoClasificadores.ToString, funGet_UnidadNegocioPadre)
            BePrivilegios = BcPrivilegios.BuscarPrivilegioPorPersonal(funGet_UserCode(), beFormularios.Id, funGet_UnidadNegocioPadre())
            If e.CommandName = "Editar" Then
                If BePrivilegios.Permiso.Contains("M") Then
                    Dim item As GridDataItem = e.Item

                    Dim Id As String = item.GetDataKeyValue("Id").ToString()
                    LoadClasificadoresTipo(CLng(Id))
                Else
                    ScriptManager.RegisterClientScriptBlock(Page,
                                                            Me.GetType,
                                                            "cerrarpopUp",
                                                            "closeDialog('divDetail'); funShow_Message('" & BePrivilegios.NoUpdate & "', 'ERROR');",
                                                            True)
                End If

            ElseIf e.CommandName = "Eliminar" Then
                If BePrivilegios.Permiso.Contains("B") Then
                    Dim item As GridDataItem = e.Item
                    Dim Id As String = item.GetDataKeyValue("Id").ToString()

                    DeleteClasficadoresTipo(CLng(Id))
                    proInit_Form()
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

            Dim beFormularios = bcFormulario.BuscarPorUnidadNegocio(BE.URLFormularios.TipoClasificadores.ToString, funGet_UnidadNegocioPadre)
            BePrivilegios = BcPrivilegios.BuscarPrivilegioPorPersonal(funGet_UserCode(), beFormularios.Id, funGet_UnidadNegocioPadre())
            If BePrivilegios.Permiso.Contains("A") Then
                LoadClasificadoresTipo(0)
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
        Try
            Dim result As Boolean = True
            If CInt(ViewState("Id")) = 0 Then
                beClasificadoresTipo = New BEM.ClasificadoresTipo
            End If
            If (result) Then
                SaveClasificadoresTipo()
                proInit_Form()
                ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "closeDialog('divDetail'); funShow_Message('Registro guardados', 'INFORMATION');", True)
            End If
        Catch ex As Exception
            ControlarError(MyBase.ObtenerMetodo, If(ex.InnerException IsNot Nothing, ex.InnerException.ToString, ex.Message))
        End Try
    End Sub

    Private Sub proInit_Form()
        Try
            Dim List_TipoClasificadores As List(Of BEM.ClasificadoresTipo) = bcClasificadoresTipos.List(rtbFilter.Text.Trim, BEntities.Estado.Activo, rgvGrid.MasterTableView.CurrentPageIndex, rgvGrid.MasterTableView.PageSize)

            rgvGrid.VirtualItemCount = bcClasificadoresTipos.Count(rtbFilter.Text.Trim).Count

            MyBase.proLoad_RadGrid(rgvGrid, List_TipoClasificadores)
        Catch ex As Exception
            ControlarError(MyBase.ObtenerMetodo, If(ex.InnerException IsNot Nothing, ex.InnerException.ToString, ex.Message))
        End Try
    End Sub

    Private Sub LoadClasificadoresTipo(ByVal Id As Long)

        If Id = 0 Then
            ViewState("Id") = 0
            ClearFields()
        Else
            beClasificadoresTipo = bcClasificadoresTipos.SearchPorId(Id)
            With beClasificadoresTipo
                ViewState("Id") = .Id
                rtbNombre.Text = .Nombre
            End With
        End If
        'rtbNombre.Focus()
    End Sub

    Private Sub DeleteClasficadoresTipo(ByVal Id As Long)

        beClasificadoresTipo = New BEM.ClasificadoresTipo
        beClasificadoresTipo = bcClasificadoresTipos.SearchPorId(Id)

        If beClasificadoresTipo IsNot Nothing Then
            With beClasificadoresTipo
                .Id = Id
                .PersonalId = MyBase.funGet_UserCode
                .StatusType = BEntities.StatusType.Delete
                .Estado = BEntities.Estado.Inactivo
            End With
            bcClasificadoresTipos.Save(beClasificadoresTipo)

            ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "closeDialog('divDetail'); funShow_Message('Lista actualizado', 'INFORMATION');", True)
        End If
    End Sub

    Private Sub SaveClasificadoresTipo()
        beClasificadoresTipo = New BEM.ClasificadoresTipo()
        Try
            With beClasificadoresTipo
                If CLng(ViewState("Id")) = 0 Then
                    .StatusType = BEntities.StatusType.Insert
                    .Id = 0
                    .Fechareg = Now
                Else
                    .StatusType = BEntities.StatusType.Update
                    .Id = CInt(ViewState("Id"))
                    .Fechareg = bcClasificadoresTipos.SearchPorId(CInt(ViewState("Id"))).Fechareg
                End If
                .Nombre = rtbNombre.Text
                .PersonalId = MyBase.funGet_UserCode()
                .FechaActualizacion = Now
                .Estado = BEntities.Estado.Activo
            End With
            bcClasificadoresTipos.Save(beClasificadoresTipo)
            ClearFields()
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "closeDialog('divDetail'); funShow_Message('Error al Guardar, verifique lo siguiente: " & ex.Message & "', 'VALIDATION');", True)
        End Try
    End Sub

    Private Sub ClearFields()
        rtbNombre.Text = String.Empty
    End Sub

    Private Sub rbFilter_Click(sender As Object, e As EventArgs) Handles rbFilter.Click
        Try
            rgvGrid.MasterTableView.CurrentPageIndex = 0
            Dim List_TipoClasificadores As List(Of BEM.ClasificadoresTipo) = bcClasificadoresTipos.List(rtbFilter.Text.Trim, BEntities.Estado.Activo, rgvGrid.MasterTableView.CurrentPageIndex, rgvGrid.MasterTableView.PageSize)
            rgvGrid.VirtualItemCount = bcClasificadoresTipos.Count(rtbFilter.Text.Trim).Count
            MyBase.proLoad_RadGrid(rgvGrid, List_TipoClasificadores)
            rtbFilter.Focus()
        Catch ex As Exception
            ControlarError(MyBase.ObtenerMetodo, If(ex.InnerException IsNot Nothing, ex.InnerException.ToString, ex.Message))
        End Try
    End Sub

End Class