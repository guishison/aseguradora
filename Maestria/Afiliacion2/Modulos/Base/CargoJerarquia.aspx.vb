Imports BEB = BEntities.Base
Imports BE = BEntities
Imports BCB = BComponents.Base
Imports BES = BEntities.Seguridad
Imports BCS = BComponents.Seguridad
Imports BCBT = BComponents.Bitacora
Imports BEBT = BEntities.Bitacora
Imports Telerik.Web.UI
Public Class CargoJerarquia
    Inherits UtilsMethods

    Private bcPositions As New BCB.Cargo
    Private bePosition As BEB.Cargo
    Private bcFormulario As New BCS.Formulario
    Private bcBitacoraErrores As New BCBT.BitacoraError
    Private beBitacoraErrores As New BEBT.BitacoraError
    Private bcPersonal As New BCS.Personal

    Protected Overloads Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            proLoadGrid()
            rcbParent.Filter = RadComboBoxFilter.Contains
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

            Dim beFormularios = bcFormulario.BuscarPorUnidadNegocio(BE.URLFormularios.CargosyJerarquias.ToString, funGet_UnidadNegocioPadre)
            BePrivilegios = BcPrivilegios.BuscarPrivilegioPorPersonal(funGet_UserCode(), beFormularios.Id, funGet_UnidadNegocioPadre())

            If e.CommandName = "EditPosition" Then
                If BePrivilegios.Permiso.Contains("M") Then
                    Dim item As GridDataItem = e.Item
                    Dim IdUser As String = item.GetDataKeyValue("Id").ToString()
                    LoadPosition(CLng(IdUser))
                Else
                    ScriptManager.RegisterClientScriptBlock(Page,
                                                            Me.GetType,
                                                            "cerrarpopUp",
                                                            "closeDialog('divDetail'); funShow_Message('" & BePrivilegios.NoUpdate & "', 'ERROR');",
                                                            True)
                End If

            ElseIf e.CommandName = "DeletePosition" Then
                If BePrivilegios.Permiso.Contains("B") Then
                    Dim item As GridDataItem = e.Item
                    Dim IdUser As String = item.GetDataKeyValue("Id").ToString()
                    DeletePosition(CLng(IdUser))
                    proLoadGrid()
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
        proLoadGrid()
    End Sub

    Protected Sub rbAdd_Click(sender As Object, e As EventArgs) Handles rbAdd.Click
        Try
            Dim BePrivilegios As New BEntities.Seguridad.Privilegio
            Dim BcPrivilegios As New BComponents.Seguridad.Privilegio

            Dim beFormularios = bcFormulario.BuscarPorUnidadNegocio(BE.URLFormularios.CargosyJerarquias.ToString, funGet_UnidadNegocioPadre)
            BePrivilegios = BcPrivilegios.BuscarPrivilegioPorPersonal(funGet_UserCode(), beFormularios.Id, funGet_UnidadNegocioPadre())
            If BePrivilegios.Permiso.Contains("A") Then

                ClearFields()
                LoadPosition(0)
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
            SavePosition()
            proLoadGrid()
            ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "closeDialog('divDetail'); funShow_Message('Cargo guardado', 'INFORMATION');", True)
        Catch ex As Exception
            ControlarError(MyBase.ObtenerMetodo, If(ex.InnerException IsNot Nothing, ex.InnerException.ToString, ex.Message))
        End Try
    End Sub

    Private Sub proLoadGrid()
        Dim lstPositions As List(Of BEB.Cargo) = bcPositions.Listar(rtbFilter.Text, BEntities.Estado.Activo, rgvGrid.MasterTableView.CurrentPageIndex, rgvGrid.MasterTableView.PageSize, BEB.relCargo.Cargo)
        rgvGrid.VirtualItemCount = bcPositions.Count(rtbFilter.Text, BEB.relCargo.Cargo).Count
        MyBase.proLoad_RadGrid(rgvGrid, lstPositions)
    End Sub

    Private Sub proLoadParent(ByVal PositionId As Long)
        Dim lstPositions As List(Of BEB.Cargo) = bcPositions.Listas(BEB.relCargo.Cargo)
        Dim lst As List(Of BEB.Cargo) = (From item In lstPositions Where item.Id <> PositionId Select item).ToList
        MyBase.proLoad_RadComboBox(rcbParent, lst, "Id", "Nombre")
    End Sub

    Private Sub LoadPosition(ByVal PositionId As Long)

        proLoadParent(PositionId)

        If PositionId = 0 Then
            rtbDescription.Enabled = True
            rcbParent.Enabled = True
            rnbBaseFee.Enabled = True
            rtbSave.Enabled = True
            ViewState("CargoId") = 0
            ClearFields()
        Else
            bePosition = bcPositions.Search(PositionId)
            Dim lspersonal As List(Of BES.Personal) = bcPersonal.ListarPersonalPorCargo(bePosition.Id)
            If lspersonal.Count() > 0 Then
                rtbDescription.Enabled = False
                rcbParent.Enabled = False
                rnbBaseFee.Enabled = False
                rtbSave.Enabled = False
            Else
                rtbDescription.Enabled = True
                rcbParent.Enabled = True
                rnbBaseFee.Enabled = True
                rtbSave.Enabled = True
            End If
            With bePosition
                ViewState("CargoId") = .Id
                rtbDescription.Text = .Nombre
                rcbParent.SelectedValue = .CargoId
                rnbBaseFee.Value = .ComisionBase
            End With
        End If

        rtbDescription.Focus()

    End Sub

    Private Sub DeletePosition(ByVal PositionId As Long)
        bePosition = bcPositions.Search(PositionId)
        Dim lspersonal As List(Of BES.Personal) = bcPersonal.ListarPersonalPorCargo(bePosition.Id)
        If lspersonal.Count() > 0 Then
            ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "closeDialog('divDetail'); funShow_Message('El Cargo está en uso, no se puede eliminar', 'INFORMATION');", True)
        Else
            With bePosition
                .Id = PositionId
                .FechaActualizacion = Now
                '.FechaReg = Now
                .StatusType = BEntities.StatusType.Delete
                .Estado = BEntities.Estado.Inactivo
            End With
            bcPositions.Save(bePosition)
        End If
    End Sub

    Private Sub SavePosition()
        bePosition = New BEB.Cargo
        With bePosition
            If CLng(ViewState("CargoId")) = 0 Then
                .StatusType = BEntities.StatusType.Insert
                .Id = 0
                .FechaReg = Now
            Else
                .StatusType = BEntities.StatusType.Update
                .Id = CLng(ViewState("CargoId"))
            End If
            .Nombre = rtbDescription.Text
            .CargoId = rcbParent.SelectedValue
            .ComisionBase = rnbBaseFee.Value
            .PersonalId = MyBase.funGet_UserCode
            .FechaActualizacion = Now
            If .CargoId = -1 Then
                .CargoId = 0
            End If
            .OrgGrafica = 1
            .Estado = BEntities.Estado.Activo
        End With
        bcPositions.Save(bePosition)
        ClearFields()
    End Sub

    Private Sub ClearFields()
        rtbDescription.Text = String.Empty
        rcbParent.SelectedIndex = -1
        rnbBaseFee.Value = 0
    End Sub

    Private Sub rbFilter_Click(sender As Object, e As EventArgs) Handles rbFilter.Click
        Try
            rgvGrid.MasterTableView.CurrentPageIndex = 0
            Dim lstPositions As List(Of BEB.Cargo) = bcPositions.Listar(rtbFilter.Text, BEntities.Estado.Activo, rgvGrid.MasterTableView.CurrentPageIndex, rgvGrid.MasterTableView.PageSize, BEB.relCargo.Cargo)
            rgvGrid.VirtualItemCount = bcPositions.Count(rtbFilter.Text, BEB.relCargo.Cargo).Count
            MyBase.proLoad_RadGrid(rgvGrid, lstPositions)
            rtbFilter.Focus()
        Catch ex As Exception
            ControlarError(MyBase.ObtenerMetodo, If(ex.InnerException IsNot Nothing, ex.InnerException.ToString, ex.Message))
        End Try
    End Sub


End Class