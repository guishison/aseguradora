

Imports BEM = BEntities.AtencionAlSocio
Imports BCM = BComponents.AtencionAlSocio
Imports BECO = BEntities.Contratos
Imports BCCO = BComponents.Contratos
Imports BEV = BEntities.Venta
Imports BCV = BComponents.Venta
Imports BEB = BEntities.Base
Imports BCB = BComponents.Base
Imports BEI = BEntities.Inventario
Imports BCI = BComponents.Inventario
Imports BES = BEntities.Seguridad
Imports BCS = BComponents.Seguridad
Imports BCBT = BComponents.Bitacora
Imports BEBT = BEntities.Bitacora
Imports Telerik.Web.UI
Imports Telerik.Web.UI.Calendar

Public Class PersonalDepartamento



    Inherits UtilsMethods
    'Private bcCliente As New BCCO.Cliente
    'Private bcContrato As New BCCO.Contrato
    Private bcClasificadores As New BCB.Clasificadores
    'Private bcServicio As New BCB.Servicios
    Private bcDepartamento As New BCB.Departamento
    'Private bcAtencionSocioDetalle As New BCM.AtencionSocioDetalle
    Private bcPersonal As New BCS.Personal
    Private bcCargo As New BCB.Cargo
    Private bcBitacoraErrores As New BCBT.BitacoraError
    Private beBitacoraErrores As New BEBT.BitacoraError


    Private Overloads Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            LlenarcomboDepartamento()
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
        MyBase.proShow_Message("Error, verifique lo siguiente: " & Sms)
    End Sub
    Private Sub LlenarcomboDepartamento()
        Try
            Dim lsdeoartamento As List(Of BEB.Departamento) = bcDepartamento.List()
            MyBase.proLoad_RadComboBox(rcbDepartamento, lsdeoartamento, "Id", "Nombre")
        Catch ex As Exception
            ControlarError(MyBase.ObtenerMetodo, If(ex.InnerException IsNot Nothing, ex.InnerException.ToString, ex.Message))
        End Try

    End Sub

    Protected Sub rgvGrid_PageIndexChanged(sender As Object, e As GridPageChangedEventArgs) Handles rgvGrid.PageIndexChanged
        sender.CurrentPageIndex = e.NewPageIndex
        proInit_Form()
    End Sub
    Private Sub proInit_Form()
        Try
            Dim lspersonal As List(Of BES.Personal) = bcPersonal.ListByDepartamento(rtbFilter.Text.Trim, rcbDepartamento.SelectedValue, BEntities.Estado.Activo, rgvGrid.MasterTableView.CurrentPageIndex, rgvGrid.MasterTableView.PageSize)
            rgvGrid.VirtualItemCount = bcPersonal.ListByDepartamentoCount(rtbFilter.Text.Trim, rcbDepartamento.SelectedValue, BEntities.Estado.Activo).Count()
            For Each item In lspersonal
                item.Cargo = bcCargo.Search(item.CargoId)
                item.Departamento = bcDepartamento.Search(item.DepartamentoId)
            Next
            MyBase.proLoad_RadGrid(rgvGrid, lspersonal)
        Catch ex As Exception
            ControlarError(MyBase.ObtenerMetodo, If(ex.InnerException IsNot Nothing, ex.InnerException.ToString, ex.Message))
        End Try

    End Sub

    Protected Sub rgvGrid_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles rgvGrid.ItemCommand
        Try
            Dim BePrivilegios As New BEntities.Seguridad.Privilegio
            Dim BcPrivilegios As New BComponents.Seguridad.Privilegio

            BePrivilegios = BcPrivilegios.BuscarPrivilegioPorPersonal(funGet_UserCode(), 3083, funGet_UnidadNegocio())

            If e.CommandName = "deleteDatabase" Then
                If BePrivilegios.Permiso.Contains("M") Then

                    Dim item As GridDataItem = e.Item
                    Dim MultaId As String = item.GetDataKeyValue("Id").ToString()
                    ViewState("MultaId") = MultaId
                    rgvGrid.MasterTableView.CurrentPageIndex = 0
                    proInit_Form()
                Else
                    ScriptManager.RegisterClientScriptBlock(Page,
                                                            Me.GetType,
                                                            "cerrarpopUp",
                                                            "closeDialog('divDetail'); funShow_Message('" & BePrivilegios.NoUpdate & "', 'ERROR');",
                                                            True)
                End If
            End If
        Catch ex As Exception
            ControlarError(MyBase.ObtenerMetodo, If(ex.InnerException IsNot Nothing, ex.InnerException.ToString, ex.Message))
        End Try

    End Sub

    Private Sub rcbDepartamento_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles rcbDepartamento.SelectedIndexChanged
        rtbFilter.Text = String.Empty
        rgvGrid.MasterTableView.CurrentPageIndex = 0
        proInit_Form()
    End Sub

    Private Sub rbFilter_Click(sender As Object, e As EventArgs) Handles rbFilter.Click
        rcbDepartamento.ClearSelection()
        rgvGrid.MasterTableView.CurrentPageIndex = 0
        proInit_Form()
    End Sub
End Class