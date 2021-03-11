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
    Private bcTasa As New BCA.Tasa
    Private bcCliente As New BCA.Cliente
    Private bcCotizacion As New BCA.Cotizacion
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
            rntCostoPrima.Enabled = False
            rtbFilter.Focus()
            ClearFields()
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
        Dim listaCliente As List(Of BEA.Cliente) = bcCliente.ListadoUN(funGet_UnidadNegocio)
        Dim listaComuna As List(Of BEB.Comuna) = bcCiudad.ListaCompleta(funGet_UnidadNegocio)
        MyBase.proLoad_RadComboBox(rcbCliente, listaCliente, "Id", "NombreCompleto")
        MyBase.proLoad_RadComboBox(rcbCiudad, listaComuna, "Id", "Ciudad")
    End Sub
    Protected Sub rgvGrid_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles rgvGrid.ItemCommand
        Try
            Dim BePrivilegios As New BEntities.Seguridad.Privilegio
            Dim BcPrivilegios As New BComponents.Seguridad.Privilegio

            Dim beFormularios = bcFormulario.BuscarPorUnidadNegocio(BE.URLFormularios.Cotizacion.ToString, funGet_UnidadNegocioPadre)
            BePrivilegios = BcPrivilegios.BuscarPrivilegioPorPersonal(funGet_UserCode(), beFormularios.Id, funGet_UnidadNegocioPadre())

            If e.CommandName = "Editar" Then
                If BePrivilegios.Permiso.Contains("M") Then
                    Dim item As GridDataItem = e.Item
                    Dim VehiculoId As String = item.GetDataKeyValue("Id").ToString()
                    ViewState("CotizacionId") = VehiculoId
                    llenarCombos()
                    LoadCotizacion((VehiculoId))
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

            Dim beFormularios = bcFormulario.BuscarPorUnidadNegocio(BE.URLFormularios.Cotizacion.ToString, funGet_UnidadNegocioPadre)
            BePrivilegios = BcPrivilegios.BuscarPrivilegioPorPersonal(funGet_UserCode(), beFormularios.Id, funGet_UnidadNegocioPadre())
            If BePrivilegios.Permiso.Contains("A") Then
                'paDetail.Enabled = True
                ViewState("CotizacionId") = 0
                llenarCombos()
                LoadCotizacion(0)
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
            SaveCotizacion()
            proInit_Form()
            ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "closeDialog('divDetail'); funShow_Message('Cotizacion guardada', 'CONFIRMATION');", True)
        Catch ex As Exception
            ControlarError(MyBase.ObtenerMetodo, If(ex.InnerException IsNot Nothing, ex.InnerException.ToString, ex.Message))
        End Try
    End Sub
    Private Sub proInit_Form()
        Try
            Dim listaCotizacion As List(Of BEA.Cotizacion) = bcCotizacion.List(funGet_UnidadNegocio, rgvGrid.MasterTableView.CurrentPageIndex, rgvGrid.MasterTableView.PageSize, BEA.relCotizacion.Cliente, BEA.relCotizacion.Ciudad, BEA.relCotizacion.Vehiculo, BEA.relVehiculo.Marca, BEA.relVehiculo.Modelo, BEA.relVehiculo.TipoVehiculo, BEA.relVehiculo.Origen, BEA.relCotizacion.Tasa)
            rgvGrid.VirtualItemCount = bcCotizacion.ListCount(funGet_UnidadNegocio)
            MyBase.proLoad_RadGrid(rgvGrid, listaCotizacion)
        Catch ex As Exception
            ControlarError(MyBase.ObtenerMetodo, If(ex.InnerException IsNot Nothing, ex.InnerException.ToString, ex.Message))
        End Try
    End Sub
    Private Sub LoadCotizacion(ByVal CotizacionId As Int32)
        If CotizacionId = 0 Then
            ClearFields()
        Else
            Dim beCotizacion = bcCotizacion.Search(CotizacionId, BEA.relCotizacion.Tasa)
            Dim beVehiculo = bcVehiculo.Search(beCotizacion.VehiculoId, BEA.relVehiculo.Marca, BEA.relVehiculo.Modelo, BEA.relVehiculo.Origen, BEA.relVehiculo.TipoVehiculo)
            Dim listaVehiculo As List(Of BEA.Vehiculo) = New List(Of BEA.Vehiculo)
            listaVehiculo.Add(beVehiculo)
            proLoad_RadComboBox(rcbVehiculo, listaVehiculo, "Id", "DetalleVehiculo")
            With beCotizacion
                rcbCliente.SelectedValue = .ClienteId
                rtbPotencia.Text = beVehiculo.Potencia
                rcbCiudad.SelectedValue = .CiudadId
                rcbVehiculo.SelectedValue = .VehiculoId
                rtbMarca.Text = beVehiculo.Marca.Nombre
                rtbTipoVehiculo.Text = beVehiculo.TipoVehiculo.Nombre
                rtbModelo.Text = beVehiculo.Modelo.Nombre
                Origen.Text = beVehiculo.Origen.Nombre
                rntPrecioVehiculo.Value = .PrecioVehiculo
                rntMontoAsegurado.Value = .MontoAsegurable
                rntTiempo.Value = .MesesAsegurable
                rntTasa.Value = .Tasa.Valor
                rntCostoPrima.Value = .CostoPrima
                rntDescuento.Value = .Descuento
                rntCostoTotal.Value = .CostoTotal
            End With
            paDetail.Enabled = False
        End If
    End Sub
    Private Sub EliminarVehiculo(ByVal DepartamentoID As Int32)
        'Dim bedepartamento As BEB.Departamento = bcDepartamento.Search(DepartamentoID)
        'Dim lspersonal As List(Of BES.Personal) = bcPersonal.ListByDepartamentoCount("", bedepartamento.Id, BEntities.Estado.Activo)
        'If lspersonal.Count() > 0 Then
        '    ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "funShow_Message('No se puede eliminar, existe personal en el departamento', 'INFORMATION');", True)
        'Else
        '    Try
        '        With bedepartamento
        '            .StatusType = BEntities.StatusType.Update
        '            .Estado = BEntities.Estado.Inactivo
        '            .FechaActualizacion = Now
        '        End With
        '        bcDepartamento.Save(bedepartamento)
        '    Catch ex As Exception
        '        ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "closeDialog('divDetail'); funShow_Message('Verifique lo siguiente: " & ex.Message & "', 'VALIDATION');", True)
        '    End Try
        'End If
    End Sub

    Private Sub SaveCotizacion()
        Dim beCotizacion = New BEA.Cotizacion
        Try
            If CLng(ViewState("CotizacionId")) = 0 Then
                beCotizacion.StatusType = BEntities.StatusType.Insert
                beCotizacion.PersonalId = MyBase.funGet_UserCode()
                beCotizacion.FechaRegistro = Now
                beCotizacion.Estado = BEntities.Estado.Activo
            Else
                beCotizacion = bcCotizacion.Search(ViewState("CotizacionId"))
                beCotizacion.StatusType = BEntities.StatusType.Update
            End If
            With beCotizacion
                .ClienteId = rcbCliente.SelectedValue
                .VehiculoId = rcbVehiculo.SelectedValue
                .TasaId = bcTasa.Search(funGet_UnidadNegocio(), rcbCiudad.SelectedValue, bcVehiculo.Search(rcbVehiculo.SelectedValue).TipoVehiculoIdc).Valor
                .FechaCotizacion = Now
                .CiudadId = rcbCiudad.SelectedValue
                .PrecioVehiculo = rntPrecioVehiculo.Value
                .MontoAsegurable = rntMontoAsegurado.Value
                .CostoPrima = rntCostoPrima.Value
                .MesesAsegurable = rntTiempo.Value
                .TasaId = rntTasa.Value
                .CostoPrima = rntCostoPrima.Value
                .Descuento = rntDescuento.Value
                .CostoTotal = rntCostoTotal.Value
                .UnidadNegocioId = funGet_UnidadNegocio()
                '.Placa = rtbPlaca.Text
                '.Potencia = rtbPotencia.Text
                .PersonalModificacionId = MyBase.funGet_UserCode()
                .EstadoCotizacionIdc = BE.EstadoCotizacion.PENDIENTE
                .FechaModificacion = Now
            End With
            bcCotizacion.Save(beCotizacion)
            ClearFields()
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "closeDialog('divDetail'); funShow_Message('Verifique lo siguiente: " & ex.Message & "', 'VALIDATION');", True)
        End Try
    End Sub

    Private Sub ClearFields()
        'rtbPlaca.Text = String.Empty
        rtbPotencia.Text = String.Empty
        rdpDesde.SelectedDate = Now
        rdpHasta.SelectedDate = Now
        rcbCiudad.ClearSelection()
        rcbCliente.ClearSelection()
        rcbVehiculo.ClearSelection()
        rtbMarca.Text = String.Empty
        rtbTipoVehiculo.Text = String.Empty
        rtbModelo.Text = String.Empty
        Origen.Text = String.Empty
        rntPrecioVehiculo.Value = 0
        rntMontoAsegurado.Value = 0
        rntTiempo.Value = 0
        rntTasa.Value = 0
        rntCostoPrima.Value = 0
        rntDescuento.Value = 0
        rntCostoTotal.Value = 0
    End Sub

    Private Sub rbFilter_Click(sender As Object, e As EventArgs) Handles rbFilter.Click
        rgvGrid.MasterTableView.CurrentPageIndex = 0
        proInit_Form()
        rtbFilter.Focus()
    End Sub

    Private Sub rcbCliente_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles rcbCliente.SelectedIndexChanged
        If rcbCliente.SelectedValue > 0 Then
            Dim listaVehiculo As List(Of BEA.Vehiculo) = bcVehiculo.ListByClienteId(rcbCliente.SelectedValue, funGet_UnidadNegocio, BEA.relVehiculo.Marca, BEA.relVehiculo.Modelo, BEA.relVehiculo.TipoVehiculo, BEA.relVehiculo.Origen)
            MyBase.proLoad_RadComboBox(rcbVehiculo, listaVehiculo, "Id", "DetalleVehiculo")
        End If
    End Sub

    Private Sub rcbVehiculo_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles rcbVehiculo.SelectedIndexChanged
        If rcbVehiculo.SelectedValue <> "" And rcbVehiculo.SelectedValue <> "-1" Then
            Dim beVehiculo As BEA.Vehiculo = bcVehiculo.Search(rcbVehiculo.SelectedValue, BEA.relVehiculo.Marca, BEA.relVehiculo.Modelo, BEA.relVehiculo.TipoVehiculo, BEA.relVehiculo.Origen)
            Dim ListParametroOrigen = bcClasificador.ListClasificadores(BE.TipoClasificadores.ParametroOrigen)
            For Each i In ListParametroOrigen
                If beVehiculo.Origen.Nombre = i.Nombre Then
                    proShow_Message("Vehiculo no asegurable por origen: " & i.Nombre, 350, 80, enuTypeMessage.Validation)
                    rcbVehiculo.ClearSelection()
                    Return
                End If
            Next
            If rcbCiudad.SelectedValue > 0 Then
                    'Dim bevehiculocombo = bcVehiculo.Search(rcbVehiculo.SelectedValue)
                    Dim beTasa = bcTasa.Search(funGet_UnidadNegocio(), rcbCiudad.SelectedValue, beVehiculo.TipoVehiculoIdc)
                    If beTasa IsNot Nothing Then
                        rntTasa.Value = beTasa.Valor
                    Else
                        rntTasa.Value = 0
                    End If
                Else
                    rntTasa.Value = 0
                End If
                With beVehiculo
                    rtbPotencia.Text = .Potencia
                    rtbMarca.Text = .Marca.Nombre
                    rtbTipoVehiculo.Text = .TipoVehiculo.Nombre
                    rtbModelo.Text = .Modelo.Nombre
                    Origen.Text = .Origen.Nombre
                End With
            Else
            rtbPotencia.Text = String.Empty
            rtbMarca.Text = String.Empty
            rtbTipoVehiculo.Text = String.Empty
            rtbModelo.Text = String.Empty
            Origen.Text = String.Empty
        End If
    End Sub

    Private Sub rcbCiudad_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles rcbCiudad.SelectedIndexChanged
        If rcbCiudad.SelectedValue > 0 Then
            If rcbVehiculo.SelectedValue <> "" And rcbVehiculo.SelectedValue <> "-1" Then
                Dim bevehiculocombo = bcVehiculo.Search(rcbVehiculo.SelectedValue)
                Dim beTasa = bcTasa.Search(funGet_UnidadNegocio(), rcbCiudad.SelectedValue, bevehiculocombo.TipoVehiculoIdc)
                If beTasa IsNot Nothing Then
                    rntTasa.Value = beTasa.Valor
                Else
                    rntTasa.Value = 0
                End If
            Else
                rntTasa.Value = 0
            End If
        End If
    End Sub

    Private Sub rbCalcular_Click(sender As Object, e As EventArgs) Handles rbCalcular.Click
        rntCostoPrima.Value = (((rntTasa.Value * rntMontoAsegurado.Value) / 100) / 12) * rntTiempo.Value
        rntDescuento.Value = 0
        rntCostoTotal.Value = rntCostoPrima.Value
        Dim maximoDescuento = bcClasificador.ListClasificadores(BE.TipoClasificadores.ParametroDescuento)
        ViewState("MaximoDescuento") = (rntCostoPrima.Value * maximoDescuento.FirstOrDefault.Valor) / 100
        'If maximoDescuento IsNot Nothing Then
        '    rntDescuento.MaxValue = (rntCostoPrima.Value * maximoDescuento.FirstOrDefault.Valor) / 100
        'End If
    End Sub

    Private Sub rntDescuento_TextChanged(sender As Object, e As EventArgs) Handles rntDescuento.TextChanged
        If rntCostoPrima.Value = 0 Then
            proShow_Message("Primero debe calcular el monto de la prima.", 350, 80, enuTypeMessage.Validation)
            rntDescuento.Value = 0
        Else
            If rntDescuento.Value > ViewState("MaximoDescuento") Then
                proShow_Message("El maximo descuento permitido es: " & ViewState("MaximoDescuento"), 350, 80, enuTypeMessage.Validation)
                rntDescuento.Value = ViewState("MaximoDescuento")
                rntCostoTotal.Value = rntCostoPrima.Value - rntDescuento.Value
            Else
                rntCostoTotal.Value = rntCostoPrima.Value - rntDescuento.Value
            End If
        End If
    End Sub
End Class