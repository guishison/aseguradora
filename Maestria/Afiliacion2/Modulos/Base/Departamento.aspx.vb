Imports BEM = BEntities.Base
Imports BCM = BComponents.Base
Imports BES = BEntities.Seguridad
Imports BCS = BComponents.Seguridad
Imports BEI = BEntities.Inventario
Imports BCI = BComponents.Inventario
Imports BCBT = BComponents.Bitacora
Imports BEBT = BEntities.Bitacora
Imports Telerik.Web.UI
Public Class Departamento

    Inherits UtilsMethods
    Private bcDepartamento As New BCM.Departamento
    Private bcPersonal As New BCS.Personal
    'Public Shared lsDepartamentoG As List(Of BEM.Departamento)
    Private bcBitacoraErrores As New BCBT.BitacoraError
    Private beBitacoraErrores As New BEBT.BitacoraError

    Private Overloads Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            proInit_Form()
            rtbFilter.Focus()
            rcbDepartamentoSup.Filter = RadComboBoxFilter.Contains
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
    Protected Sub rgvGrid_PageIndexChanged(sender As Object, e As GridPageChangedEventArgs) Handles rgvGrid.PageIndexChanged
        sender.CurrentPageIndex = e.NewPageIndex
        proInit_Form()
    End Sub
    Private Sub llenarComboDepartamentoSup()
        Dim lsdepartamento As List(Of BEM.Departamento) = bcDepartamento.List()
        MyBase.proLoad_RadComboBox(rcbDepartamentoSup, lsdepartamento, "Id", "Nombre")
    End Sub
    Protected Sub rgvGrid_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles rgvGrid.ItemCommand
        Try
            Dim BePrivilegios As New BEntities.Seguridad.Privilegio
            Dim BcPrivilegios As New BComponents.Seguridad.Privilegio

            BePrivilegios = BcPrivilegios.BuscarPrivilegioPorPersonal(funGet_UserCode(), BEntities.FormularioDeModulos.Departamento, funGet_UnidadNegocio())

            If e.CommandName = "editDatabase" Then
                If BePrivilegios.Permiso.Contains("M") Then
                    Dim item As GridDataItem = e.Item
                    Dim DepartamentoId As String = item.GetDataKeyValue("Id").ToString()
                    ViewState("DepartamentoId") = DepartamentoId
                    llenarComboDepartamentoSup()
                    LoadDepartamento(CLng(DepartamentoId))
                Else
                    ScriptManager.RegisterClientScriptBlock(Page,
                                                            Me.GetType,
                                                            "cerrarpopUp",
                                                            "closeDialog('divDetail'); funShow_Message('" & BePrivilegios.NoUpdate & "', 'ERROR');",
                                                            True)
                End If
            ElseIf e.CommandName = "deleteDatabase" Then
                If BePrivilegios.Permiso.Contains("B") Then
                    Dim item As GridDataItem = e.Item
                    Dim DepartamentoId As String = item.GetDataKeyValue("Id").ToString()
                    DeleteDepartamento(CInt(DepartamentoId))
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

    Protected Sub rbAdd_Click(sender As Object, e As EventArgs) Handles rbAdd.Click
        Try
            Dim BePrivilegios As New BEntities.Seguridad.Privilegio
            Dim BcPrivilegios As New BComponents.Seguridad.Privilegio
            BePrivilegios = BcPrivilegios.BuscarPrivilegioPorPersonal(funGet_UserCode(), BEntities.FormularioDeModulos.Departamento, funGet_UnidadNegocio())
            If BePrivilegios.Permiso.Contains("A") Then
                ViewState("DepartamentoId") = 0
                llenarComboDepartamentoSup()
                LoadDepartamento(0)
                rtbNombre.Focus()
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
            If CInt(ViewState("DepartamentoId")) = 0 Then
                Dim lsdepartamento As List(Of BEM.Departamento) = bcDepartamento.List()
                lsdepartamento = (From item In lsdepartamento
                                  Where item.Nombre.ToLower.Trim = rtbNombre.Text.ToLower.Trim).ToList()
                If (lsdepartamento.Count > 0) Then
                    ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "funShow_Message('Nombre de departamento " & rtbNombre.Text & " ya existente ', 'INFORMATION');", True)
                Else
                    SaveDepartamento()
                    proInit_Form()
                    ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "closeDialog('divDetail'); funShow_Message('Departamento guardado', 'INFORMATION');", True)
                End If
            Else
                Dim bedepartamento As BEM.Departamento = bcDepartamento.Search(ViewState("DepartamentoId"))
                If rtbNombre.Text.ToLower.Trim <> bedepartamento.Nombre.ToLower.Trim Then
                    Dim lsdepartamento As List(Of BEM.Departamento) = bcDepartamento.List()
                    lsdepartamento = (From item In lsdepartamento
                                      Where item.Nombre.ToLower.Trim = rtbNombre.Text.ToLower.Trim).ToList()
                    If (lsdepartamento.Count > 0) Then
                        ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "funShow_Message('Nombre de departamento " & rtbNombre.Text & " ya existente ', 'INFORMATION');", True)
                    Else
                        SaveDepartamento()
                        proInit_Form()
                        ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "closeDialog('divDetail'); funShow_Message('Departamento actualizado', 'INFORMATION');", True)
                    End If
                Else
                    SaveDepartamento()
                    proInit_Form()
                    ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "closeDialog('divDetail'); funShow_Message('Departamento actualizado', 'INFORMATION');", True)
                End If

            End If
        Catch ex As Exception
            ControlarError(MyBase.ObtenerMetodo, If(ex.InnerException IsNot Nothing, ex.InnerException.ToString, ex.Message))
        End Try
    End Sub
    Private Sub proInit_Form()
        Try
            Dim lsdepartamento As List(Of BEM.Departamento) = bcDepartamento.List(rtbFilter.Text.Trim, BEntities.Estado.Activo, rgvGrid.MasterTableView.CurrentPageIndex, rgvGrid.MasterTableView.PageSize)
            rgvGrid.VirtualItemCount = bcDepartamento.List().Count
            MyBase.proLoad_RadGrid(rgvGrid, lsdepartamento)
        Catch ex As Exception
            ControlarError(MyBase.ObtenerMetodo, If(ex.InnerException IsNot Nothing, ex.InnerException.ToString, ex.Message))
        End Try
    End Sub
    Private Sub LoadDepartamento(ByVal DepartamentoId As Long)
        If DepartamentoId = 0 Then
            ClearFields()
            rtbNombre.Enabled = True
            rcbDepartamentoSup.Enabled = True
        Else
            Dim bedepartamento = bcDepartamento.Search(DepartamentoId)
            With bedepartamento
                rtbNombre.Text = .Nombre
                rcbDepartamentoSup.SelectedValue = .DepartamentoSuperiorId
            End With
            Dim lspersonal As List(Of BES.Personal) = bcPersonal.ListByDepartamentoCount("", bedepartamento.Id, BEntities.Estado.Activo)
            If lspersonal.Count() > 0 Then
                rtbNombre.Enabled = False
                rcbDepartamentoSup.Enabled = False
            Else
                rtbNombre.Enabled = True
                rcbDepartamentoSup.Enabled = True
            End If
        End If
    End Sub
    Private Sub DeleteDepartamento(ByVal DepartamentoID As Int32)
        Dim bedepartamento As BEM.Departamento = bcDepartamento.Search(DepartamentoID)
        Dim lspersonal As List(Of BES.Personal) = bcPersonal.ListByDepartamentoCount("", bedepartamento.Id, BEntities.Estado.Activo)
        If lspersonal.Count() > 0 Then
            ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "funShow_Message('No se puede eliminar, existe personal en el departamento', 'INFORMATION');", True)
        Else
            Try
                With bedepartamento
                    .StatusType = BEntities.StatusType.Update
                    .Estado = BEntities.Estado.Inactivo
                    .FechaActualizacion = Now
                End With
                bcDepartamento.Save(bedepartamento)
            Catch ex As Exception
                ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "closeDialog('divDetail'); funShow_Message('Verifique lo siguiente: " & ex.Message & "', 'VALIDATION');", True)
            End Try
        End If
    End Sub

    Private Sub SaveDepartamento()
        Dim bedepartamento = New BEM.Departamento
        Try
            With bedepartamento
                If CLng(ViewState("DepartamentoId")) = 0 Then
                    .StatusType = BEntities.StatusType.Insert
                    .Id = 0
                    .Nombre = rtbNombre.Text
                    If rcbDepartamentoSup.SelectedValue <> -1 Then
                        .DepartamentoSuperiorId = rcbDepartamentoSup.SelectedValue
                    End If
                    .PersonalId = MyBase.funGet_UserCode()
                    .FechaReg = Now
                    .FechaActualizacion = Now
                    .Estado = BEntities.Estado.Activo
                Else
                    bedepartamento = bcDepartamento.Search(ViewState("DepartamentoId"))
                    With bedepartamento
                        .StatusType = BEntities.StatusType.Update
                        .Nombre = rtbNombre.Text
                        If rcbDepartamentoSup.SelectedValue <> -1 Then
                            .DepartamentoSuperiorId = rcbDepartamentoSup.SelectedValue
                        Else
                            .DepartamentoSuperiorId = Nothing
                        End If
                        .PersonalId = MyBase.funGet_UserCode()
                        .FechaActualizacion = Now
                    End With
                End If
            End With
            bcDepartamento.Save(bedepartamento)
            ClearFields()
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "closeDialog('divDetail'); funShow_Message('Verifique lo siguiente: " & ex.Message & "', 'VALIDATION');", True)
        End Try
    End Sub

    Private Sub ClearFields()
        rtbNombre.Text = String.Empty
        rcbDepartamentoSup.ClearSelection()
    End Sub

    Private Sub rbFilter_Click(sender As Object, e As EventArgs) Handles rbFilter.Click
        rgvGrid.MasterTableView.CurrentPageIndex = 0
        proInit_Form()
        rtbFilter.Focus()
    End Sub
End Class