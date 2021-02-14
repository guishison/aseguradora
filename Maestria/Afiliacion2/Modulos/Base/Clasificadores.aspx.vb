Imports BEM = BEntities.Base
Imports BCM = BComponents.Base
Imports BE = BEntities
Imports Telerik.Web.UI
Imports BCBT = BComponents.Bitacora
Imports BEBT = BEntities.Bitacora
Imports BCB = BComponents.Base

Public Class Clasificadores
    Inherits UtilsMethods

    Private bcClasificadores As New BCM.Clasificadores
    Private beClasificadores As BEM.Clasificadores
    Private bcBitacoraErrores As New BCBT.BitacoraError
    Private beBitacoraErrores As New BEBT.BitacoraError
    'Shared Property ListaGlobal As List(Of BEM.Clasificadores) = Nothing

    Private Overloads Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            proInit_Form()
            ViewState("ListaGlobal") = New List(Of BEM.Clasificadores)
            ViewState("ListaGlobal") = Nothing
            rcbTipoClasificador.EnableTextSelection = True
            rcbTipoClasificador.MarkFirstMatch = True
            rcbTipoClasifica.Filter = RadComboBoxFilter.Contains
            rcbTipoClasificador.Filter = RadComboBoxFilter.Contains
            rcbTipoClasifica.DropDownAutoWidth = RadComboBoxDropDownAutoWidth.Enabled
            rcbTipoClasificador.DropDownAutoWidth = RadComboBoxDropDownAutoWidth.Enabled
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

            BePrivilegios = BcPrivilegios.BuscarPrivilegioPorPersonal(funGet_UserCode(), BE.FormularioDeModulos.Clasificadores, funGet_UnidadNegocio())

            If e.CommandName = "Editar" Then
                If BePrivilegios.Permiso.Contains("M") Then
                    Dim item As GridDataItem = e.Item
                    Dim Id As String = item.GetDataKeyValue("Id").ToString()
                    LoadClasificadores(CLng(Id))
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
                    DeleteClasficadores(CLng(Id))
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

            BePrivilegios = BcPrivilegios.BuscarPrivilegioPorPersonal(funGet_UserCode(), BE.FormularioDeModulos.Clasificadores, funGet_UnidadNegocio())
            If BePrivilegios.Permiso.Contains("A") Then

                LoadClasificadores(0)
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
                beClasificadores = New BEM.Clasificadores()
            End If
            If (result) Then
                SaveClasificadores()
                rcbTipoClasifica.SelectedValue = rcbTipoClasificador.SelectedValue 'Aumentado
                proInit_Form()
                ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "closeDialog('divDetail'); funShow_Message('Registro guardados', 'INFORMATION');", True)
            End If
        Catch ex As Exception
            ControlarError(MyBase.ObtenerMetodo, If(ex.InnerException IsNot Nothing, ex.InnerException.ToString, ex.Message))
        End Try
    End Sub

    Private Sub proInit_Form()
        Try
            llenarClasificador()
            Dim List_Clasificadores As New List(Of BEM.Clasificadores)



            '%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%Cambiado 
            Dim dalClasificadoresTipo As BComponents.Base.ClasificadoresTipo = New BCM.ClasificadoresTipo
            If rcbTipoClasifica.SelectedValue <> "" Then
                List_Clasificadores = bcClasificadores.ListClasificadores(rcbTipoClasifica.SelectedValue, BEM.relClasificadores.ClasificadoresTipo)
                'For Each x In List_Clasificadores
                '    x.ClasificadoresTipo = dalClasificadoresTipo.Search(x.TipoClasificadorId)
                'Next
                ViewState("ListaGlobal") = List_Clasificadores
            End If
            Me.rgvGrid.VirtualItemCount = List_Clasificadores.Count
            MyBase.proLoad_RadGrid(rgvGrid, List_Clasificadores)

            '%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
            'Dim List_Clasificadores As List(Of BEM.Clasificadores) = bcClasificadores.List(rtbFilter.Text.Trim,
            '                                                                               BEntities.Estado.Activo,
            '                                                                               rgvGrid.MasterTableView.CurrentPageIndex,
            '                                                                               rgvGrid.MasterTableView.PageSize, BEntities.Base.relClasificadores.ClasificadoresTipo)


            'rgvGrid.VirtualItemCount = bcClasificadores.Count(rtbFilter.Text.Trim, BEntities.Base.relClasificadores.ClasificadoresTipo).Count
            'MyBase.proLoad_RadGrid(rgvGrid, List_Clasificadores)

            'Dim dalClasificadoresTipo As BComponents.Base.ClasificadoresTipo = New BCM.ClasificadoresTipo()
            MyBase.proLoad_RadComboBox(rcbTipoClasificador, dalClasificadoresTipo.List(), "Id", "Nombre")

        Catch ex As Exception
            ControlarError(MyBase.ObtenerMetodo, If(ex.InnerException IsNot Nothing, ex.InnerException.ToString, ex.Message))
        End Try
    End Sub

    Private Sub llenarClasificador()
        Dim dalClasificadoresTipo As BComponents.Base.ClasificadoresTipo = New BCM.ClasificadoresTipo()
        'MyBase.proLoad_RadComboBox(rcbTipoClasifica, dalClasificadoresTipo.List(), "Id", "Nombre")
        rcbTipoClasifica.Items.Clear()
        rcbTipoClasifica.DataTextField = "Nombre"
        rcbTipoClasifica.DataValueField = "Id"
        rcbTipoClasifica.DataSource = dalClasificadoresTipo.List()
        rcbTipoClasifica.DataBind()

    End Sub

    Private Sub LoadClasificadores(ByVal Id As Long)

        If Id = 0 Then
            ViewState("Id") = 0
            rcbTipoClasificador.SelectedValue = rcbTipoClasifica.SelectedValue
            rcbTipoClasificador.Enabled = False
            ClearFields()
        Else
            beClasificadores = bcClasificadores.Search(Id)
            With beClasificadores
                ViewState("Id") = .Id
                rtbNombre.Text = .Nombre
                rtbValor.Text = .Valor
                rcbTipoClasificador.SelectedValue = beClasificadores.TipoClasificadorId
            End With
        End If
        rtbNombre.Focus()
    End Sub

    Private Sub DeleteClasficadores(ByVal Id As Long)

        beClasificadores = New BEM.Clasificadores
        beClasificadores = bcClasificadores.Search(Id)

        If beClasificadores IsNot Nothing Then
            With beClasificadores
                .Id = Id
                .PersonalId = MyBase.funGet_UserCode
                .StatusType = BEntities.StatusType.Delete
                .Estado = BEntities.Estado.Inactivo
            End With
            bcClasificadores.Save(beClasificadores)

            ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "closeDialog('divDetail'); funShow_Message('Lista actualizada', 'INFORMATION');", True)
        End If
    End Sub

    Private Sub SaveClasificadores()
        beClasificadores = New BEM.Clasificadores()
        Try
            With beClasificadores
                If CLng(ViewState("Id")) = 0 Then
                    .StatusType = BEntities.StatusType.Insert
                    .Id = 0
                    .FechaReg = Now
                Else
                    .StatusType = BEntities.StatusType.Update
                    .Id = CInt(ViewState("Id"))
                    .FechaReg = bcClasificadores.Search(CInt(ViewState("Id"))).FechaReg
                End If
                .Nombre = rtbNombre.Text
                .TipoClasificadorId = rcbTipoClasificador.SelectedValue
                .Valor = rtbValor.Text
                .PersonalId = MyBase.funGet_UserCode()
                .FechaActualizacion = Now
                .Estado = BEntities.Estado.Activo
            End With
            bcClasificadores.Save(beClasificadores)
            ClearFields()
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "closeDialog('divDetail'); funShow_Message('Error al guardar, verifique lo siguiente: " & ex.Message & "', 'VALIDATION');", True)
        End Try
    End Sub

    Private Sub ClearFields()
        rtbNombre.Text = String.Empty
        rtbValor.Text = String.Empty
    End Sub

    Private Sub rbFilter_Click(sender As Object, e As EventArgs) Handles rbFilter.Click
        Try
            rgvGrid.MasterTableView.CurrentPageIndex = 0
            'proInit_Form()
            buscartext()
            rtbFilter.Text = String.Empty
            rtbFilter.Focus()
        Catch ex As Exception
            ControlarError(MyBase.ObtenerMetodo, If(ex.InnerException IsNot Nothing, ex.InnerException.ToString, ex.Message))
        End Try
    End Sub

    Private Sub buscartext()
        If ViewState("ListaGlobal") IsNot Nothing Then
            Dim lista = (From x In CType(ViewState("ListaGlobal"), List(Of BEM.Clasificadores))
                         Where x.Nombre.ToLower().Contains(Me.rtbFilter.Text.ToLower()) Or
                             x.Valor.ToLower.Contains(Me.rtbFilter.Text.ToLower)).ToList()
            Me.rgvGrid.VirtualItemCount = lista.Count
            MyBase.proLoad_RadGrid(rgvGrid, lista)
        End If

    End Sub

    Private Sub rcbTipoClasifica_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles rcbTipoClasifica.SelectedIndexChanged
        Try
            Dim List_Clasificadores As New List(Of BEM.Clasificadores)
            Dim dalClasificadoresTipo As BComponents.Base.ClasificadoresTipo = New BCM.ClasificadoresTipo
            If rcbTipoClasifica.SelectedValue <> "" Then
                List_Clasificadores = bcClasificadores.ListClasificadores(rcbTipoClasifica.SelectedValue, BEM.relClasificadores.ClasificadoresTipo)
                'For Each x In List_Clasificadores
                '    x.ClasificadoresTipo = dalClasificadoresTipo.Search(x.TipoClasificadorId)
                'Next
                ViewState("ListaGlobal") = List_Clasificadores
            End If
            Me.rgvGrid.VirtualItemCount = List_Clasificadores.Count
            MyBase.proLoad_RadGrid(rgvGrid, List_Clasificadores)
        Catch ex As Exception
            ControlarError(MyBase.ObtenerMetodo, If(ex.InnerException IsNot Nothing, ex.InnerException.ToString, ex.Message))
        End Try
    End Sub
End Class