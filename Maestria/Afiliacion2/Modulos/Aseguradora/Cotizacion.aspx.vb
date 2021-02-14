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
Public Class Cotizacion
    Inherits UtilsMethods
    Private bcDepartamento As New BCB.Departamento
    Private bcVehiculo As New BCA.Vehiculo
    Private bcCliente As New BCA.Cliente
    Private bcClasificador As New BCB.Clasificadores
    Private bcCiudad As New BCB.Comuna
    Private bcPersonal As New BCS.Personal
    Private bcFormulario As New BCS.Formulario
    'Public Shared lsDepartamentoG As List(Of BEB.Departamento)
    Private bcBitacoraErrores As New BCBT.BitacoraError
    Private beBitacoraErrores As New BEBT.BitacoraError

    Protected Overloads Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            proInit_Form()
            rdpDesde.SelectedDate = Now
            rdpHasta.SelectedDate = Now
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
        Dim listaCliente As List(Of BEA.Cliente) = bcCliente.ListadoUN(funGet_UnidadNegocioPadre)
        Dim listaComuna As List(Of BEB.Comuna) = bcCiudad.ListaCompleta(funGet_UnidadNegocioPadre)
        MyBase.proLoad_RadComboBox(rcbCliente, listaCliente, "Id", "NombreCompleto")
        MyBase.proLoad_RadComboBox(rcbCiudad, listaComuna, "Id", "Ciudad")
    End Sub
    Protected Sub rgvGrid_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles rgvGrid.ItemCommand
        Try
            Dim BePrivilegios As New BEntities.Seguridad.Privilegio
            Dim BcPrivilegios As New BComponents.Seguridad.Privilegio

            Dim beFormularios = bcFormulario.BuscarPorUnidadNegocio(BE.URLFormularios.Vehiculo.ToString, funGet_UnidadNegocioPadre)
            BePrivilegios = BcPrivilegios.BuscarPrivilegioPorPersonal(funGet_UserCode(), beFormularios.Id, funGet_UnidadNegocioPadre())

            If e.CommandName = "Editar" Then
                If BePrivilegios.Permiso.Contains("M") Then
                    Dim item As GridDataItem = e.Item
                    Dim VehiculoId As String = item.GetDataKeyValue("Id").ToString()
                    ViewState("VehiculoId") = VehiculoId
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

            Dim beFormularios = bcFormulario.BuscarPorUnidadNegocio(BE.URLFormularios.Vehiculo.ToString, funGet_UnidadNegocioPadre)
            BePrivilegios = BcPrivilegios.BuscarPrivilegioPorPersonal(funGet_UserCode(), beFormularios.Id, funGet_UnidadNegocioPadre())
            If BePrivilegios.Permiso.Contains("A") Then
                ViewState("VehiculoId") = 0
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
            SaveVehiculo()
            proInit_Form()
            ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "closeDialog('divDetail'); funShow_Message('Vehiculo guardado', 'INFORMATION');", True)
        Catch ex As Exception
            ControlarError(MyBase.ObtenerMetodo, If(ex.InnerException IsNot Nothing, ex.InnerException.ToString, ex.Message))
        End Try
    End Sub
    Private Sub proInit_Form()
        Try
            Dim listaVehiculo As List(Of BEA.Vehiculo) = bcVehiculo.List(funGet_UnidadNegocioPadre, rgvGrid.MasterTableView.CurrentPageIndex, rgvGrid.MasterTableView.PageSize, BEA.relVehiculo.Cliente, BEA.relVehiculo.Marca, BEA.relVehiculo.Modelo, BEA.relVehiculo.TipoVehiculo, BEA.relVehiculo.Origen)
            rgvGrid.VirtualItemCount = bcVehiculo.ListCount(funGet_UnidadNegocioPadre)
            MyBase.proLoad_RadGrid(rgvGrid, listaVehiculo)
        Catch ex As Exception
            ControlarError(MyBase.ObtenerMetodo, If(ex.InnerException IsNot Nothing, ex.InnerException.ToString, ex.Message))
        End Try
    End Sub
    Private Sub LoadVehiculo(ByVal VehiculoId As Int32)
        If VehiculoId = 0 Then
            ClearFields()
        Else
            Dim beVehiculo = bcVehiculo.Search(VehiculoId)
            With beVehiculo
                rcbCliente.SelectedValue = .ClienteId
                rtbPotencia.Text = .Potencia
            End With
        End If
    End Sub
    Private Sub EliminarVehiculo(ByVal DepartamentoID As Int32)
        Dim bedepartamento As BEB.Departamento = bcDepartamento.Search(DepartamentoID)
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

    Private Sub SaveVehiculo()
        Dim beVehiculo = New BEA.Vehiculo
        Try
            If CLng(ViewState("VehiculoId")) = 0 Then
                beVehiculo.StatusType = BEntities.StatusType.Insert
                beVehiculo.PersonalId = MyBase.funGet_UserCode()
                beVehiculo.FechaRegistro = Now
                beVehiculo.Estado = BEntities.Estado.Activo
            Else
                beVehiculo = bcVehiculo.Search(ViewState("VehiculoId"))
                beVehiculo.StatusType = BEntities.StatusType.Update
            End If
            With beVehiculo
                .ClienteId = rcbCliente.SelectedValue
                '.MarcaIdc = rcbMarca.SelectedValue
                '.ModeloIdc = rcbModelo.SelectedValue
                '.TipoVehiculoIdc = rcbTipoVehiculo.SelectedValue
                '.OrigenIdc = rcbOrigen.SelectedValue
                .UnidadNegocioId = funGet_UnidadNegocioPadre()
                '.Placa = rtbPlaca.Text
                .Potencia = rtbPotencia.Text
                .PersonalModificacionId = MyBase.funGet_UserCode()
                .FechaModificacion = Now
            End With
            bcVehiculo.Save(beVehiculo)
            ClearFields()
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "closeDialog('divDetail'); funShow_Message('Verifique lo siguiente: " & ex.Message & "', 'VALIDATION');", True)
        End Try
    End Sub

    Private Sub ClearFields()
        'rtbPlaca.Text = String.Empty
        rtbPotencia.Text = String.Empty
        'rdpFecha.Enabled = True
        'rdpFecha.SelectedDate = Now
        'rdpFecha.Enabled = False
    End Sub

    Private Sub rbFilter_Click(sender As Object, e As EventArgs) Handles rbFilter.Click
        rgvGrid.MasterTableView.CurrentPageIndex = 0
        proInit_Form()
        rtbFilter.Focus()
    End Sub

    Private Sub rcbCliente_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles rcbCliente.SelectedIndexChanged
        If rcbCliente.SelectedValue > 0 Then
            Dim listaVehiculo As List(Of BEA.Vehiculo) = bcVehiculo.ListByClienteId(rcbCliente.SelectedValue, funGet_UnidadNegocioPadre, BEA.relVehiculo.Marca, BEA.relVehiculo.Modelo, BEA.relVehiculo.TipoVehiculo, BEA.relVehiculo.Origen)
            MyBase.proLoad_RadComboBox(rcbVehiculo, listaVehiculo, "Id", "DetalleVehiculo")
        End If
    End Sub

    Private Sub rcbVehiculo_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles rcbVehiculo.SelectedIndexChanged
        If rcbVehiculo.SelectedValue > 0 Then
            Dim beVehiculo As BEA.Vehiculo = bcVehiculo.Search(rcbCliente.SelectedValue, BEA.relVehiculo.Marca, BEA.relVehiculo.Modelo, BEA.relVehiculo.TipoVehiculo, BEA.relVehiculo.Origen)
            rtbPotencia.Text = beVehiculo.Potencia
            rtbMarca.Text = beVehiculo.Marca.Nombre
            rtbTipoVehiculo.Text = beVehiculo.TipoVehiculo.Nombre
            rtbModelo.Text = beVehiculo.Modelo.Nombre
            Origen.Text = beVehiculo.Origen.Nombre
        Else
            rtbPotencia.Text = String.Empty
            rtbMarca.Text = String.Empty
            rtbTipoVehiculo.Text = String.Empty
            rtbModelo.Text = String.Empty
            Origen.Text = String.Empty
        End If
    End Sub
End Class