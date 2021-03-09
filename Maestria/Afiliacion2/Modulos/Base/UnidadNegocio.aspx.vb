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
Public Class UnidadNegocio
    Inherits UtilsMethods
    Private bcDepartamento As New BCB.Departamento
    Private bcVehiculo As New BCA.Vehiculo
    Private bcCliente As New BCA.Cliente
    Private bcClasificador As New BCB.Clasificadores
    Private bcUnidadNegocio As New BCB.UnidadNegocio
    Private bcUnidadNegocioSucursal As New BCB.UNSucursal
    Private bcPersonal As New BCS.Personal
    Private bcFormulario As New BCS.Formulario
    'Public Shared lsDepartamentoG As List(Of BEB.Departamento)
    Private bcBitacoraErrores As New BCBT.BitacoraError
    Private beBitacoraErrores As New BEBT.BitacoraError

    Protected Overloads Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            proInit_Form()
            rtbFilter.Focus()
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
    Private Sub llenarCombos()
        Dim listaUnidadNegocio As List(Of BEB.UnidadNegocio) = bcUnidadNegocio.List()
        MyBase.proLoad_RadComboBox(rcbUnidadNegocio, listaUnidadNegocio, "Id", "Nombre")
        'MyBase.proLoad_RadComboBox(rcbCliente, listaCliente, "Id", "NombreCompleto")
    End Sub
    Protected Sub rgvGrid_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles rgvGrid.ItemCommand
        Try
            Dim BePrivilegios As New BEntities.Seguridad.Privilegio
            Dim BcPrivilegios As New BComponents.Seguridad.Privilegio

            Dim beFormularios = bcFormulario.BuscarPorUnidadNegocio(BE.URLFormularios.UnidadNegocio.ToString, funGet_UnidadNegocioPadre)
            BePrivilegios = BcPrivilegios.BuscarPrivilegioPorPersonal(funGet_UserCode(), beFormularios.Id, funGet_UnidadNegocioPadre())

            If e.CommandName = "Editar" Then
                If BePrivilegios.Permiso.Contains("M") Then
                    Dim item As GridDataItem = e.Item
                    Dim VehiculoId As String = item.GetDataKeyValue("Id").ToString()
                    ViewState("UnidadNegocioSucursalId") = VehiculoId
                    llenarCombos()
                    LoadVehiculo((VehiculoId))
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
                    Dim VehiculoId As String = item.GetDataKeyValue("Id").ToString()
                    EliminarVehiculo(CInt(VehiculoId))
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

            Dim beFormularios = bcFormulario.BuscarPorUnidadNegocio(BE.URLFormularios.UnidadNegocio.ToString, funGet_UnidadNegocioPadre)
            BePrivilegios = BcPrivilegios.BuscarPrivilegioPorPersonal(funGet_UserCode(), beFormularios.Id, funGet_UnidadNegocioPadre())
            If BePrivilegios.Permiso.Contains("A") Then
                ViewState("UnidadNegocioSucursalId") = 0
                llenarCombos()
                LoadVehiculo(0)
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
            SaveUnidadNegocioSucursal()
            proInit_Form()
            ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "closeDialog('divDetail'); funShow_Message('Vehiculo guardado', 'CONFIRMATION');", True)
        Catch ex As Exception
            ControlarError(MyBase.ObtenerMetodo, If(ex.InnerException IsNot Nothing, ex.InnerException.ToString, ex.Message))
        End Try
    End Sub
    Private Sub proInit_Form()
        Try
            Dim listaUnidadNegocioSucursal As List(Of BEB.UNSucursal) = bcUnidadNegocioSucursal.List(BEB.relUNSucursal.UnidadNegocio)
            rgvGrid.VirtualItemCount = listaUnidadNegocioSucursal.Count 'bcVehiculo.ListCount(funGet_UnidadNegocio)
            MyBase.proLoad_RadGrid(rgvGrid, listaUnidadNegocioSucursal)
        Catch ex As Exception
            ControlarError(MyBase.ObtenerMetodo, If(ex.InnerException IsNot Nothing, ex.InnerException.ToString, ex.Message))
        End Try
    End Sub
    Private Sub LoadVehiculo(ByVal UnidadNegocioSucursalId As Int32)
        If UnidadNegocioSucursalId = 0 Then
            ClearFields()
        Else
            Dim beUnidadNegocioSucursal = bcUnidadNegocioSucursal.Search(UnidadNegocioSucursalId)
            With beUnidadNegocioSucursal
                rcbUnidadNegocio.SelectedValue = .UnidadNegocioId
                rtbNombre.Text = .Nombre
                rntCorrelativo.Value = .Correlativo
                rtbAlias.Text = .AliasUN
            End With
        End If
    End Sub
    Private Sub EliminarVehiculo(ByVal UnidadNegocioSucursalId As Int32)
        Dim beUnidadNegocioSucursalId As BEB.UNSucursal = bcUnidadNegocioSucursal.Search(UnidadNegocioSucursalId)
        Try
            With beUnidadNegocioSucursalId
                .StatusType = BEntities.StatusType.Update
                .Estado = BEntities.Estado.Inactivo
                .FechaActualizacion = Now
            End With
            bcUnidadNegocioSucursal.Save(beUnidadNegocioSucursalId)
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "closeDialog('divDetail'); funShow_Message('Verifique lo siguiente: " & ex.Message & "', 'VALIDATION');", True)
        End Try
    End Sub

    Private Sub SaveUnidadNegocioSucursal()
        Dim beUnidadNegocioSucursal = New BEB.UNSucursal
        Try
            If CLng(ViewState("UnidadNegocioSucursalId")) = 0 Then
                beUnidadNegocioSucursal.StatusType = BEntities.StatusType.Insert
                beUnidadNegocioSucursal.PersonalId = MyBase.funGet_UserCode()
                beUnidadNegocioSucursal.FechaReg = Now
                beUnidadNegocioSucursal.Estado = BEntities.Estado.Activo
            Else
                beUnidadNegocioSucursal = bcUnidadNegocioSucursal.Search(ViewState("UnidadNegocioSucursalId"))
                beUnidadNegocioSucursal.StatusType = BEntities.StatusType.Update
            End If
            With beUnidadNegocioSucursal
                .UnidadNegocioId = rcbUnidadNegocio.SelectedValue
                .Nombre = rtbNombre.Text
                .Correlativo = rntCorrelativo.Value
                .AliasUN = rtbAlias.Text
                '.PersonalModificacionId = MyBase.funGet_UserCode()
                .FechaActualizacion = Now
                .Personal = New BES.Personal
                .Personal.StatusType = BE.StatusType.Insert
                .Personal.PersonalId = 1
                .Personal.SupervisorId = -1
                .Personal.CargoId = 1
                .Personal.MontoComision = 5
                .Personal.Nombre = "admin"
                .Personal.Rut = "123"
                .Personal.Correo = "admin@admin.com"
                .Personal.Login = "admin"
                .Personal.Password = "123"
                .Personal.DepartamentoId = 1
                .Personal.Telefono = "123"
                .Personal.DependenciaIdc = 1299
                .Personal.FechaReg = Now
                .Personal.FechaActualizacion = Now
                .Personal.Estado = 1
                .Personal.PersonalEstado = 1
                .Personal.FechaBloqueo = Nothing
                .Personal.IntentosFallidos = 0
            End With
            Dim ListaPrivilegios As List(Of BES.Privilegio) = New List(Of BES.Privilegio)
            Dim privileg As BES.Privilegio = New BES.Privilegio
            With privileg
                .StatusType = BE.StatusType.Insert
                .PersonalIdL = 1
                .FechaReg = Now
                .PersonalId = 0
                .FormularioId = 3116
                .Permiso = "ABEM"
                .TiempoDias = 10
                .UnidadNegocioId = 2
                .Estado = 1
            End With
            ListaPrivilegios.Add(privileg)
            privileg = New BES.Privilegio
            With privileg
                .StatusType = BE.StatusType.Insert
                .PersonalIdL = 1
                .FechaReg = Now
                .PersonalId = 0
                .FormularioId = 3117
                .Permiso = "ABEM"
                .TiempoDias = 10
                .UnidadNegocioId = 2
                .Estado = 1
            End With
            ListaPrivilegios.Add(privileg)
            beUnidadNegocioSucursal.Personal.CollectionPrivilegios = ListaPrivilegios
            bcUnidadNegocioSucursal.Save(beUnidadNegocioSucursal)
            ClearFields()
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "closeDialog('divDetail'); funShow_Message('Verifique lo siguiente: " & ex.Message & "', 'VALIDATION');", True)
        End Try
    End Sub

    Private Sub ClearFields()
        rtbNombre.Text = String.Empty
        rntCorrelativo.Value = 0
        rcbUnidadNegocio.ClearSelection()
        rtbAlias.Text = String.Empty
    End Sub

    Private Sub rbFilter_Click(sender As Object, e As EventArgs) Handles rbFilter.Click
        rgvGrid.MasterTableView.CurrentPageIndex = 0
        proInit_Form()
        rtbFilter.Focus()
    End Sub
End Class