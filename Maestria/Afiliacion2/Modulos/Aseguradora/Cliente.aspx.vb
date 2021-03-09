Imports BE = BEntities
Imports BEB = BEntities.Base
Imports BEA = BEntities.Aseguradora
Imports BCB = BComponents.Base
Imports BCA = BComponents.Aseguradora
Imports BES = BEntities.Seguridad
Imports BCS = BComponents.Seguridad
Imports BCBT = BComponents.Bitacora
Imports BEBT = BEntities.Bitacora
Imports Telerik.Web.UI
Public Class Cliente
    Inherits UtilsMethods
    Private bcDepartamento As New BCB.Departamento
    Private bcVehiculo As New BCA.Vehiculo
    Private bcCliente As New BCA.Cliente
    Private bcClasificador As New BCB.Clasificadores
    Private bcPersonal As New BCS.Personal
    Private bcFormulario As New BCS.Formulario
    'Public Shared lsDepartamentoG As List(Of BEB.Departamento)
    Private bcBitacoraErrores As New BCBT.BitacoraError
    Private beBitacoraErrores As New BEBT.BitacoraError

    Protected Overloads Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            rdpFechaNacimiento.MinDate = Now().AddYears(-120)
            proInit_Form()
            rtbFilter.Focus()
        End If
    End Sub

    Private Sub proInit_Form()
        Try
            Dim listaVehiculo As List(Of BEA.Cliente) = bcCliente.List(funGet_UnidadNegocio, rgvGrid.MasterTableView.CurrentPageIndex, rgvGrid.MasterTableView.PageSize, BEA.relCliente.Expedido)
            rgvGrid.VirtualItemCount = bcCliente.ListCount(funGet_UnidadNegocio)
            MyBase.proLoad_RadGrid(rgvGrid, listaVehiculo)
        Catch ex As Exception
            ControlarError(MyBase.ObtenerMetodo, If(ex.InnerException IsNot Nothing, ex.InnerException.ToString, ex.Message))
        End Try
    End Sub
    Private Sub ClearFields()
        rtbNombre.Text = String.Empty
        rtbApellido.Text = String.Empty
        rdpFechaNacimiento.SelectedDate = Now
        rntCI.Value = 0
        rcbExpedido.ClearSelection()
        rntTelefono.Value = 0
        rtbDireccion.Text = String.Empty
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
    Private Sub rbFilter_Click(sender As Object, e As EventArgs) Handles rbFilter.Click
        rgvGrid.MasterTableView.CurrentPageIndex = 0
        proInit_Form()
        rtbFilter.Focus()
    End Sub
    Protected Sub rgvGrid_PageIndexChanged(sender As Object, e As GridPageChangedEventArgs) Handles rgvGrid.PageIndexChanged
        sender.CurrentPageIndex = e.NewPageIndex
        proInit_Form()
    End Sub
    Private Sub llenarCombos()
        Dim listaExpedido As List(Of BEB.Clasificadores) = bcClasificador.ListClasificadores(BE.TipoClasificadores.Expedido)
        MyBase.proLoad_RadComboBox(rcbExpedido, listaExpedido, "Id", "Valor")
    End Sub
    Protected Sub rgvGrid_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles rgvGrid.ItemCommand
        Try
            Dim BePrivilegios As New BEntities.Seguridad.Privilegio
            Dim BcPrivilegios As New BComponents.Seguridad.Privilegio

            Dim beFormularios = bcFormulario.BuscarPorUnidadNegocio(BE.URLFormularios.Cliente.ToString, funGet_UnidadNegocioPadre)
            BePrivilegios = BcPrivilegios.BuscarPrivilegioPorPersonal(funGet_UserCode(), beFormularios.Id, funGet_UnidadNegocioPadre())

            If e.CommandName = "Editar" Then
                If BePrivilegios.Permiso.Contains("M") Then
                    Dim item As GridDataItem = e.Item
                    Dim ClienteId As String = item.GetDataKeyValue("Id").ToString()
                    ViewState("ClienteId") = ClienteId
                    llenarCombos()
                    LoadCliente((ClienteId))
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
                    Dim ClienteId As String = item.GetDataKeyValue("Id").ToString()
                    EliminarCliente(CInt(ClienteId))
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

            Dim beFormularios = bcFormulario.BuscarPorUnidadNegocio(BE.URLFormularios.Cliente.ToString, funGet_UnidadNegocioPadre)
            BePrivilegios = BcPrivilegios.BuscarPrivilegioPorPersonal(funGet_UserCode(), beFormularios.Id, funGet_UnidadNegocioPadre())
            If BePrivilegios.Permiso.Contains("A") Then
                ViewState("ClienteId") = 0
                llenarCombos()
                LoadCliente(0)
                'rtbNombre.Focus()
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
            SaveCliente()
            proInit_Form()
            ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "closeDialog('divDetail'); funShow_Message('Cliente guardado', 'CONFIRMATION');", True)
        Catch ex As Exception
            ControlarError(MyBase.ObtenerMetodo, If(ex.InnerException IsNot Nothing, ex.InnerException.ToString, ex.Message))
        End Try
    End Sub
    Private Sub LoadCliente(ByVal ClienteId As Int32)
        If ClienteId = 0 Then
            ClearFields()
        Else
            Dim beCliente = bcCliente.Search(ClienteId)
            With beCliente
                rtbNombre.Text = .Nombre
                rtbApellido.Text = .Apellido
                rntCI.Value = .CI
                rcbExpedido.SelectedValue = .ExpedidoIdc
                rntTelefono.Value = .Telefono
                rdpFechaNacimiento.SelectedDate = .FechaNacimiento
                rtbDireccion.Text = .Direccion
            End With
        End If
    End Sub
    Private Sub EliminarCliente(ByVal ClienteID As Int32)
        Dim beCliente As BEA.Cliente = bcCliente.Search(ClienteID)
        'Dim lspersonal As List(Of BES.Personal) = bcPersonal.ListByDepartamentoCount("", beCliente.Id, BEntities.Estado.Activo)
        'If lspersonal.Count() > 0 Then
        '    ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "funShow_Message('No se puede eliminar, existe personal en el departamento', 'INFORMATION');", True)
        'Else
        Try
                With beCliente
                .StatusType = BEntities.StatusType.Delete
                .Estado = BEntities.Estado.Inactivo
                .FechaModificacion = Now
            End With
            bcCliente.Save(beCliente)
        Catch ex As Exception
                ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "closeDialog('divDetail'); funShow_Message('Verifique lo siguiente: " & ex.Message & "', 'VALIDATION');", True)
            End Try
        'End If
    End Sub

    Private Sub SaveCliente()
        Dim beCliente = New BEA.Cliente
        Try
            If CLng(ViewState("ClienteId")) = 0 Then
                beCliente.StatusType = BEntities.StatusType.Insert
                beCliente.PersonalId = MyBase.funGet_UserCode()
                beCliente.FechaRegistro = Now
                beCliente.Estado = BEntities.Estado.Activo
            Else
                beCliente = bcCliente.Search(ViewState("ClienteId"))
                beCliente.StatusType = BEntities.StatusType.Update
            End If
            With beCliente
                .Nombre = rtbNombre.Text
                .Apellido = rtbApellido.Text
                .CI = rntCI.Value
                .ExpedidoIdc = rcbExpedido.SelectedValue
                .Telefono = rntTelefono.Value
                .FechaNacimiento = rdpFechaNacimiento.SelectedDate
                .Direccion = rtbDireccion.Text
                .UnidadNegocioId = funGet_UnidadNegocio()
                .PersonalModificacionId = MyBase.funGet_UserCode()
                .FechaModificacion = Now
            End With
            bcCliente.Save(beCliente)
            ClearFields()
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "closeDialog('divDetail'); funShow_Message('Verifique lo siguiente: " & ex.Message & "', 'VALIDATION');", True)
        End Try
    End Sub

End Class