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
Public Class Poliza
    Inherits UtilsMethods
    Private bcDepartamento As New BCB.Departamento
    Private bcVehiculo As New BCA.Vehiculo
    Private bcTasa As New BCA.Tasa
    Private bcCliente As New BCA.Cliente
    Private bcCotizacion As New BCA.Cotizacion
    Private bcPoliza As New BCA.Poliza
    Private bcClasificador As New BCB.Clasificadores
    Private bcCiudad As New BCB.Comuna
    Private bcPersonal As New BCS.Personal
    Private bcFormulario As New BCS.Formulario
    Private beCotizacion As New BEA.Cotizacion
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

            Dim beFormularios = bcFormulario.BuscarPorUnidadNegocio(BE.URLFormularios.Poliza.ToString, funGet_UnidadNegocioPadre)
            BePrivilegios = BcPrivilegios.BuscarPrivilegioPorPersonal(funGet_UserCode(), beFormularios.Id, funGet_UnidadNegocioPadre())

            If e.CommandName = "Editar" Then
                If BePrivilegios.Permiso.Contains("M") Then
                    Dim item As GridDataItem = e.Item
                    Dim VehiculoId As String = item.GetDataKeyValue("Id").ToString()
                    ViewState("PolizaId") = VehiculoId
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
                    Dim PolizaId As String = item.GetDataKeyValue("Id").ToString()
                    EliminarPoliza(CInt(PolizaId))
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

            rcbCliente.Enabled = False
            rcbCiudad.Enabled = False
            rcbVehiculo.Enabled = False
            Dim beFormularios = bcFormulario.BuscarPorUnidadNegocio(BE.URLFormularios.Poliza.ToString, funGet_UnidadNegocioPadre)
            BePrivilegios = BcPrivilegios.BuscarPrivilegioPorPersonal(funGet_UserCode(), beFormularios.Id, funGet_UnidadNegocioPadre())
            If BePrivilegios.Permiso.Contains("A") Then
                'paDetail.Enabled = True
                ViewState("PolizaId") = 0
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
            SavePoliza()
            proInit_Form()
            ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "closeDialog('divDetail'); funShow_Message('Cotizacion guardada', 'CONFIRMATION');", True)
        Catch ex As Exception
            ControlarError(MyBase.ObtenerMetodo, If(ex.InnerException IsNot Nothing, ex.InnerException.ToString, ex.Message))
        End Try
    End Sub
    Private Sub proInit_Form()
        Try
            Dim listPoliza As List(Of BEA.Poliza) = bcPoliza.List(funGet_UnidadNegocio, rgvGrid.MasterTableView.CurrentPageIndex, rgvGrid.MasterTableView.PageSize, BEA.relPoliza.Cotizacion, BEA.relCotizacion.Cliente, BEA.relCotizacion.Ciudad, BEA.relCotizacion.Vehiculo, BEA.relVehiculo.Marca, BEA.relVehiculo.Modelo, BEA.relVehiculo.TipoVehiculo, BEA.relVehiculo.Origen, BEA.relCotizacion.Tasa)
            rgvGrid.VirtualItemCount = bcPoliza.ListCount(funGet_UnidadNegocio)
            MyBase.proLoad_RadGrid(rgvGrid, listPoliza)
        Catch ex As Exception
            ControlarError(MyBase.ObtenerMetodo, If(ex.InnerException IsNot Nothing, ex.InnerException.ToString, ex.Message))
        End Try
    End Sub
    Private Sub LoadCotizacion(ByVal PolizaId As Int32)
        If PolizaId = 0 Then
            ClearFields()
        Else
            Dim bePoliza = bcCotizacion.Search(PolizaId, BEA.relCotizacion.Tasa)
            Dim beVehiculo = bcVehiculo.Search(bePoliza.VehiculoId, BEA.relVehiculo.Marca, BEA.relVehiculo.Modelo, BEA.relVehiculo.Origen, BEA.relVehiculo.TipoVehiculo)
            Dim listaVehiculo As List(Of BEA.Vehiculo) = New List(Of BEA.Vehiculo)
            listaVehiculo.Add(beVehiculo)
            proLoad_RadComboBox(rcbVehiculo, listaVehiculo, "Id", "DetalleVehiculo")
            With bePoliza
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
    Private Sub EliminarPoliza(ByVal PolizaId As Int32)
        Dim bePoliza As BEA.Poliza = bcPoliza.Search(PolizaId)

        Try
            With bePoliza
                .StatusType = BEntities.StatusType.Update
                .Estado = BEntities.Estado.Inactivo
                .FechaModificacion = Now
                .Cotizacion = bcCotizacion.Search(bePoliza.CotizacionId)
                .Cotizacion.StatusType = BE.StatusType.Update
                .Cotizacion.EstadoCotizacionIdc = BE.EstadoCotizacion.PENDIENTE
                .Cotizacion.FechaModificacion = Now
                .Cotizacion.PersonalModificacionId = funGet_UserCode()
            End With
            bcPoliza.Save(bePoliza)
            ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "funShow_Message('Poliza Eliminada, Se procedera a habilitar la cotizacion', 'INFORMATION');", True)
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "closeDialog('divDetail'); funShow_Message('Verifique lo siguiente: " & ex.Message & "', 'VALIDATION');", True)
        End Try
    End Sub

    Private Sub SavePoliza()
        Dim bePoliza = New BEA.Poliza
        Try
            If CLng(ViewState("PolizaId")) = 0 Then
                bePoliza.StatusType = BEntities.StatusType.Insert
                bePoliza.PersonalId = MyBase.funGet_UserCode()
                bePoliza.FechaRegistro = Now
                bePoliza.Estado = BEntities.Estado.Activo
            Else
                bePoliza = bcPoliza.Search(ViewState("PolizaId"))
                bePoliza.StatusType = BEntities.StatusType.Update
            End If
            With bePoliza
                .NumeroPoliza = rtbNumeroPoliza.Text
                .CotizacionId = rtbCotizacion.Text
                .FechaPoliza = rdpFechaPoliza.SelectedDate
                .FechaInicio = rdpFechaInicio.SelectedDate
                .CiudadId = rcbCiudad.SelectedValue
                .PrecioVehiculo = rntPrecioVehiculo.Value
                .MontoAsegurable = rntMontoAsegurado.Value
                .CostoPrima = rntCostoPrima.Value
                .MesesAsegurable = rntTiempo.Value
                .CostoPrima = rntCostoPrima.Value
                .Descuento = rntDescuento.Value
                .CostoTotal = rntCostoTotal.Value
                .UnidadNegocioId = funGet_UnidadNegocio()
                '.Placa = rtbPlaca.Text
                '.Potencia = rtbPotencia.Text
                .PersonalModificacionId = MyBase.funGet_UserCode()
                .FechaModificacion = Now
                .Cotizacion = ViewState("Cotizacion")
                .Cotizacion.StatusType = BE.StatusType.Update
                .Cotizacion.EstadoCotizacionIdc = BE.EstadoCotizacion.COMPLETADA
                .Cotizacion.FechaModificacion = Now
                .Cotizacion.PersonalModificacionId = funGet_UserCode()
            End With
            bcPoliza.Save(bePoliza)
            ClearFields()
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "closeDialog('divDetail'); funShow_Message('Verifique lo siguiente: " & ex.Message & "', 'VALIDATION');", True)
        End Try
    End Sub

    Private Sub ClearFields()
        'rtbPlaca.Text = String.Empty
        rtbPotencia.Text = String.Empty
        rdpFechaPoliza.SelectedDate = Now
        rdpFechaInicio.SelectedDate = Now
        rtbNumeroPoliza.Text = String.Empty
        rtbCotizacion.Text = String.Empty
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
        'rntDescuento.Value = 0
        rntCostoTotal.Value = rntCostoPrima.Value - rntDescuento.Value
        Dim maximoDescuento = bcClasificador.ListClasificadores(BE.TipoClasificadores.ParametroDescuento)
        ViewState("MaximoDescuento") = (rntCostoPrima.Value * maximoDescuento.FirstOrDefault.Valor) / 100
        'If maximoDescuento IsNot Nothing Then
        '    rntDescuento.MaxValue = (rntCostoPrima.Value * maximoDescuento.FirstOrDefault.Valor) / 100
        'End If
    End Sub

    Private Sub rntDescuento_TextChanged(sender As Object, e As EventArgs) Handles rntDescuento.TextChanged
        If rntDescuento.Value > 0 Then
            If rntCostoPrima.Value = 0 Then
                proShow_Message("Primero debe calcular el monto de la prima.", 350, 80, enuTypeMessage.Validation)
                rntDescuento.Value = 0
            Else
                If rntDescuento.Value > (ViewState("MaximoDescuento")) Then
                    proShow_Message("El maximo descuento permitido es " & ViewState("MaximoDescuento"), 350, 80, enuTypeMessage.Validation)
                    rntDescuento.Value = ViewState("MaximoDescuento")
                    rntCostoTotal.Value = rntCostoPrima.Value - rntDescuento.Value
                Else
                    rntCostoTotal.Value = rntCostoPrima.Value - rntDescuento.Value
                End If
            End If
        End If
    End Sub

    Private Sub rgvGridCotizacion_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles rgvGridCotizacion.ItemCommand
        If e.CommandName = "Seleccionar" Then
            Dim item As GridDataItem = e.Item
            Dim Id As String = item("Id").Text.ToString()
            beCotizacion = bcCotizacion.Search(Id, BEA.relCotizacion.Cliente, BEA.relCotizacion.Ciudad, BEA.relCotizacion.Vehiculo, BEA.relVehiculo.Marca, BEA.relVehiculo.Modelo, BEA.relVehiculo.TipoVehiculo, BEA.relVehiculo.Origen, BEA.relCotizacion.Tasa)
            Dim listaVehiculo As List(Of BEA.Vehiculo) = New List(Of BEA.Vehiculo)
            listaVehiculo.Add(beCotizacion.Vehiculo)
            proLoad_RadComboBox(rcbVehiculo, listaVehiculo, "Id", "DetalleVehiculo")
            rdpFechaPoliza.MinDate = Now
            rdpFechaInicio.MinDate = Now
            With beCotizacion
                rtbCotizacion.Text = .Id
                rcbCliente.SelectedValue = .ClienteId
                rcbCliente.Enabled = False
                rtbPotencia.Text = .Vehiculo.Potencia
                rcbCiudad.SelectedValue = .CiudadId
                rcbCiudad.Enabled = False
                rcbVehiculo.SelectedValue = .VehiculoId
                rcbVehiculo.Enabled = False
                rtbMarca.Text = .Vehiculo.Marca.Nombre
                rtbTipoVehiculo.Text = .Vehiculo.TipoVehiculo.Nombre
                rtbModelo.Text = .Vehiculo.Modelo.Nombre
                Origen.Text = .Vehiculo.Origen.Nombre
                rntPrecioVehiculo.Value = .PrecioVehiculo
                rntMontoAsegurado.Value = .MontoAsegurable
                rntTiempo.Value = .MesesAsegurable
                rntTasa.Value = .Tasa.Valor
                rntCostoPrima.Value = .CostoPrima
                rntDescuento.Value = .Descuento
                rntCostoTotal.Value = .CostoTotal
                Dim maximoDescuento = bcClasificador.ListClasificadores(BE.TipoClasificadores.ParametroDescuento)
                ViewState("MaximoDescuento") = CInt(((rntCostoPrima.Value * maximoDescuento.FirstOrDefault.Valor) / 100) * 100) / 100
                ViewState("MinimoTiempo") = rntTiempo.Value
            End With
            ViewState("Cotizacion") = beCotizacion
        End If
    End Sub

    Private Sub rbBuscarCotizacion_Click(sender As Object, e As EventArgs) Handles rbBuscarCotizacion.Click
        rtbFilterCotizacion.Text = String.Empty
        Dim listCotizacion As List(Of BEA.Cotizacion) = bcCotizacion.ListBuscador(rtbFilterCotizacion.Text, funGet_UnidadNegocio, funGet_UserCode, rgvGrid.MasterTableView.CurrentPageIndex, rgvGrid.MasterTableView.PageSize, BEA.relCotizacion.Cliente, BEA.relCotizacion.Ciudad, BEA.relCotizacion.Vehiculo, BEA.relVehiculo.Marca, BEA.relVehiculo.Modelo, BEA.relVehiculo.TipoVehiculo, BEA.relVehiculo.Origen, BEA.relCotizacion.EstadoCotizacion)
        rgvGridCotizacion.VirtualItemCount = bcCotizacion.ListBusquedaCount(rtbFilterCotizacion.Text, funGet_UnidadNegocio)
        MyBase.proLoad_RadGrid(rgvGridCotizacion, listCotizacion)
    End Sub

    Private Sub rntTiempo_TextChanged(sender As Object, e As EventArgs) Handles rntTiempo.TextChanged

        If rntTiempo.Value < ViewState("MinimoTiempo") Then
            proShow_Message("El tiempo minimo de la poliza debe ser de: " & ViewState("MinimoTiempo") & " meses.", 350, 80, enuTypeMessage.Validation)
            rntTiempo.Value = ViewState("MinimoTiempo")
        Else
            rbCalcular_Click(New Object, New EventArgs)
        End If
    End Sub

    Private Sub rbFilterCotizacion_Click(sender As Object, e As EventArgs) Handles rbFilterCotizacion.Click
        Dim listCotizacion As List(Of BEA.Cotizacion) = bcCotizacion.ListBuscador(rtbFilterCotizacion.Text, funGet_UnidadNegocio, funGet_UserCode, rgvGrid.MasterTableView.CurrentPageIndex, rgvGrid.MasterTableView.PageSize, BEA.relCotizacion.Cliente, BEA.relCotizacion.Ciudad, BEA.relCotizacion.Vehiculo, BEA.relVehiculo.Marca, BEA.relVehiculo.Modelo, BEA.relVehiculo.TipoVehiculo, BEA.relVehiculo.Origen, BEA.relCotizacion.EstadoCotizacion)
        rgvGridCotizacion.VirtualItemCount = bcCotizacion.ListBusquedaCount(rtbFilterCotizacion.Text, funGet_UnidadNegocio)
        MyBase.proLoad_RadGrid(rgvGridCotizacion, listCotizacion)
    End Sub
End Class