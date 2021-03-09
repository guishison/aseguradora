Imports BEC = BEntities.Seguridad
Imports BEB = BEntities.Base
Imports BE = BEntities
Imports BCB = BComponents.Base
Imports BCC = BComponents.Seguridad
Imports BCBT = BComponents.Bitacora
Imports BEBT = BEntities.Bitacora
Imports Telerik.Web.UI
Imports System.Net

Public Class Personal
    Inherits UtilsMethods

#Region " Variable "
    Private bcPersonal As New BCC.Personal
    Private bePersonal As BEC.Personal

    Private bcSupervisorPersonal As New BCC.PersonalSupervisor
    Private bcFormulario As New BCC.Formulario
    Private BePrivilegios As New BEntities.Seguridad.Privilegio
    Private BcPrivilegios As New BComponents.Seguridad.Privilegio
    Private beSupervisorPersonal As BEC.PersonalSupervisor
    Private bcDepartamento As New BCB.Departamento
    Private bcBitacoraErrores As New BCBT.BitacoraError
    Private beBitacoraErrores As New BEBT.BitacoraError
#End Region

#Region " EVENTS "
    Private Sub Users_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            proInit_Form()
            rcbPosition.EnableTextSelection = True
            rcbPosition.MarkFirstMatch = True
            rcbDepartamento.Filter = RadComboBoxFilter.Contains
            rcbDependencia.Filter = RadComboBoxFilter.Contains
            rcbPosition.Filter = RadComboBoxFilter.Contains
            rcbPositionFilter.Filter = RadComboBoxFilter.Contains
            rcbSupervisor.Filter = RadComboBoxFilter.Contains
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
    Protected Sub rgvUsersList_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles rgvUsersList.ItemCommand
        Try

            Dim beFormularios = bcFormulario.BuscarPorUnidadNegocio(BE.URLFormularios.PermisoaPersonales.ToString, funGet_UnidadNegocioPadre)
            BePrivilegios = BcPrivilegios.BuscarPrivilegioPorPersonal(funGet_UserCode(), beFormularios.Id, funGet_UnidadNegocioPadre())

            Select Case e.CommandName
                Case "EditUser"
                    If BePrivilegios.Permiso.Contains("M") Then
                        Dim item As GridDataItem = e.Item
                        Dim IdPersonal As String = item.GetDataKeyValue("Id").ToString()
                        LoadUser(CInt(IdPersonal))
                        rbCambioPassword.Enabled = True
                        ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cargarPassword", "setPassword();", True)
                    Else
                        ScriptManager.RegisterClientScriptBlock(Page,
                                                            Me.GetType,
                                                            "cerrarpopUp",
                                                            "closeDialog('divDetail'); funShow_Message('" & BePrivilegios.NoUpdate & "', 'ERROR');",
                                                            True)
                    End If

                Case "DeleteUser"
                    If BePrivilegios.Permiso.Contains("B") Then
                        Try
                            Dim item As GridDataItem = e.Item
                            Dim IdPersonal As String = item.GetDataKeyValue("Id").ToString()
                            DeleteUser(CLng(IdPersonal))
                            'proInit_Form()
                            loadGrid()
                            ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "closeDialog('divDetail'); funShow_Message('Usuario eliminado', 'INFORMATION');", True)
                        Catch ex As BComponents.BCException
                            ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "closeDialog('divDetail'); funShow_Message('" & Me.ExceptionMessage(ex) & "', 'ERROR');", True)
                        Catch ex As Exception
                            ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "closeDialog('divDetail'); funShow_Message('" & ex.Message & "', 'ERROR');", True)
                        End Try
                    Else
                        ScriptManager.RegisterClientScriptBlock(Page,
                                                            Me.GetType,
                                                            "cerrarpopUp",
                                                            "closeDialog('divDetail'); funShow_Message('" & BePrivilegios.NoDelete & "', 'ERROR');",
                                                            True)
                    End If

                Case "StateUser"
                    If BePrivilegios.Permiso.Contains("M") Then
                        Dim rbStateUser As RadButton = e.CommandSource
                        rbStateUser.Icon.PrimaryIconCssClass = If(rbStateUser.Checked, "fa fa-times-circle", "fa fa-check-circle")
                        rbStateUser.ToolTip = If(rbStateUser.Checked, "Habilitar", "Deshabilitar")
                        rbStateUser.Checked = Not rbStateUser.Checked
                        Dim item As GridDataItem = e.Item
                        Dim IdPersonal As String = item.GetDataKeyValue("Id").ToString()
                        ChangeStateUser(CLng(IdPersonal))
                    Else
                        ScriptManager.RegisterClientScriptBlock(Page,
                                                            Me.GetType,
                                                            "cerrarpopUp",
                                                            "closeDialog('divDetail'); funShow_Message('" & BePrivilegios.NoUpdate & "', 'ERROR');",
                                                            True)
                    End If

            End Select
        Catch ex As Exception
            ControlarError(MyBase.ObtenerMetodo, If(ex.InnerException IsNot Nothing, ex.InnerException.ToString, ex.Message))
        End Try
    End Sub
    Private Sub rgvUsersList_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles rgvUsersList.ItemDataBound
        If TypeOf e.Item Is GridDataItem Then
            Dim Item As GridDataItem = e.Item
            Dim rbStateUser As RadButton = Item("Acciones").FindControl("rbStateUser")
            If Not (Item("Estado").Text = "0") Then
                rbStateUser.Icon.PrimaryIconCssClass = "fa fa-check-circle"
                rbStateUser.ToolTip = "Usuario habilitado"
                rbStateUser.Checked = True
            Else
                rbStateUser.Icon.PrimaryIconCssClass = "fa fa-times-circle"
                rbStateUser.ToolTip = "Usuario deshabilitado"
                rbStateUser.Checked = False
            End If
        End If
    End Sub
    Private Sub rgvUsersList_PageIndexChanged(sender As Object, e As Telerik.Web.UI.GridPageChangedEventArgs) Handles rgvUsersList.PageIndexChanged
        sender.CurrentPageIndex = e.NewPageIndex
        Dim lstPersonal = bcPersonal.Listar(rtbNameFilter1.Text, funGet_UnidadNegocio, rcbPositionFilter.SelectedValue, "", rgvUsersList.MasterTableView.CurrentPageIndex,
                                    rgvUsersList.MasterTableView.PageSize, BEC.relPersonal.Cargo, BEC.relPerfilPersonal.Perfil, BEC.relPersonal.Supervisor, BEC.relPersonal.UNSucursal)
        Me.rgvUsersList.VirtualItemCount = bcPersonal.Count(rtbNameFilter1.Text, "", rcbPositionFilter.SelectedValue, funGet_UnidadNegocio)
        Me.rgvUsersList.DataSource = lstPersonal
    End Sub

    Private Sub rgvUsersList_PageSizeChanged(sender As Object, e As Telerik.Web.UI.GridPageSizeChangedEventArgs) Handles rgvUsersList.PageSizeChanged
        Dim lstUsers = bcPersonal.Listar(rtbNameFilter1.Text, funGet_UnidadNegocio, rcbPositionFilter.SelectedValue, "", rgvUsersList.MasterTableView.CurrentPageIndex,
                                    rgvUsersList.MasterTableView.PageSize, BEC.relPersonal.Cargo, BEC.relPerfilPersonal.Perfil, BEC.relPersonal.Supervisor, BEC.relPersonal.UNSucursal)
        Me.rgvUsersList.VirtualItemCount = bcPersonal.Count(rtbNameFilter1.Text, "", rcbPositionFilter.SelectedValue, funGet_UnidadNegocio)
        Me.rgvUsersList.DataSource = lstUsers
    End Sub

    Protected Sub rgvUsersList_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles rgvUsersList.NeedDataSource
        Dim lstUsers = bcPersonal.Listar(rtbNameFilter1.Text, funGet_UnidadNegocio, rcbPositionFilter.SelectedValue, "", rgvUsersList.MasterTableView.CurrentPageIndex,
                                    rgvUsersList.MasterTableView.PageSize, BEC.relPersonal.Cargo, BEC.relPerfilPersonal.Perfil, BEC.relPersonal.Supervisor, BEC.relPersonal.UNSucursal)
        Me.rgvUsersList.VirtualItemCount = bcPersonal.Count(rtbNameFilter1.Text, "", rcbPositionFilter.SelectedValue, funGet_UnidadNegocio)
        Me.rgvUsersList.DataSource = lstUsers
    End Sub

    Private Sub rbAdd_Click(sender As Object, e As EventArgs) Handles rbAdd.Click
        Try
            Dim beFormularios = bcFormulario.BuscarPorUnidadNegocio(BE.URLFormularios.PermisoaPersonales.ToString, funGet_UnidadNegocioPadre)
            BePrivilegios = BcPrivilegios.BuscarPrivilegioPorPersonal(funGet_UserCode(), beFormularios.Id, funGet_UnidadNegocioPadre())
            If BePrivilegios.Permiso.Contains("A") Then
                proLoadComboBox()
                ClearFields()
                ViewState("IdPersonal") = 0
                rbCambioPassword.Enabled = True
                rbCambioPassword.Checked = True
                rbCambioPassword.Enabled = False
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

    Protected Sub rbAccept_Click(sender As Object, e As EventArgs) Handles rbAccept.Click
        Try
            SaveUser()
        Catch ex As BComponents.BCException
            'ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "closeDialog('divDetail'); funShow_Message('" & Me.ExceptionMessage(ex) & "', 'ERROR');", True)
            ControlarError(MyBase.ObtenerMetodo, If(ex.InnerException IsNot Nothing, ex.InnerException.ToString, Me.ExceptionMessage(ex)))
            ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "closeDialog('divDetail');", True)
        Catch ex As Exception
            'ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "closeDialog('divDetail'); funShow_Message('" & bcPersonal.ErrorMessage(0).Trim & "', 'ERROR');", True)
            ControlarError(MyBase.ObtenerMetodo, If(ex.InnerException IsNot Nothing, ex.InnerException.ToString, bcPersonal.ErrorMessage(0).Trim))
            ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "closeDialog('divDetail');", True)
        End Try
    End Sub

    Protected Sub rbFilter_Click(sender As Object, e As EventArgs) Handles rbFilter.Click
        Try
            Dim lstUsers As New List(Of BEC.Personal)

            rgvUsersList.MasterTableView.CurrentPageIndex = 0

            lstUsers = bcPersonal.Listar(rtbNameFilter1.Text, funGet_UnidadNegocio, rcbPositionFilter.SelectedValue, "", rgvUsersList.MasterTableView.CurrentPageIndex,
                                rgvUsersList.MasterTableView.PageSize, BEC.relPersonal.Cargo, BEC.relPerfilPersonal.Perfil, BEC.relPersonal.Supervisor, BEC.relPersonal.UNSucursal)

            Me.rgvUsersList.VirtualItemCount = bcPersonal.Count(rtbNameFilter1.Text, "", rcbPositionFilter.SelectedValue, funGet_UnidadNegocio)

            MyBase.proLoad_RadGrid(rgvUsersList, lstUsers)
        Catch ex As Exception
            ControlarError(MyBase.ObtenerMetodo, If(ex.InnerException IsNot Nothing, ex.InnerException.ToString, ex.Message))
        End Try

    End Sub

    Protected Sub rcbPosition_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles rcbPosition.SelectedIndexChanged
        Try
            Dim beCargo As BEB.Cargo
            Dim bcCargo As New BCB.Cargo

            beCargo = bcCargo.Search(CInt(e.Value))
            'lstSupervisors = bcPersonal.ListarPersonalPorCargo(beCargo.CargoId)
            Dim lstSupervisors As List(Of BEC.Personal)
            lstSupervisors = bcPersonal.ListarPersonalPorCargo2(-1, funGet_UnidadNegocio)
            'Validacion para evitar que se supervice a si mismo 
            If ViewState("IdPersonal").ToString <> "" AndAlso IsNumeric(ViewState("IdPersonal")) Then
                lstSupervisors = (From Supervisors In lstSupervisors Where Supervisors.Id <> CLng(ViewState("IdPersonal"))).ToList
            End If

            rnbFee.Value = beCargo.ComisionBase
            MyBase.proLoad_RadComboBox(rcbSupervisor, lstSupervisors, "Id", "Nombre")
        Catch ex As Exception
            ControlarError(MyBase.ObtenerMetodo, If(ex.InnerException IsNot Nothing, ex.InnerException.ToString, ex.Message))
        End Try

    End Sub

#End Region

#Region " PRIVATE METHODS "

    Private Sub proInit_Form()

        Try

            'Dim lstProfile As New List(Of BEC.Perfil)
            'Dim BCProfile As New BCC.Perfil
            Dim lstCargo As List(Of BEB.Cargo)
            Dim bcCargo As New BCB.Cargo
            lstCargo = bcCargo.Listas()
            Me.rgvUsersList.VirtualItemCount = bcPersonal.Count("", "-1", "-1", funGet_UnidadNegocio)
            'lstProfile = BCProfile.Listar()
            'MyBase.proLoad_RadComboBox(rcbProfileFilter, lstProfile, "Id", "Nombre")
            MyBase.proLoad_RadComboBox(rcbPositionFilter, lstCargo, "Id", "Nombre")
            ViewState.Add("IdPersonal", 0)
        Catch ex As BComponents.BCException
            ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "closeDialog('divDetail'); funShow_Message('" & Me.ExceptionMessage(ex) & "', 'ERROR');", True)
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "closeDialog('divDetail'); funShow_Message('" & bcPersonal.ErrorMessage(0).Trim & "', 'ERROR');", True)
        End Try


    End Sub

    Private Sub loadGrid()
        Dim lstUsers As New List(Of BEC.Personal)

        lstUsers = bcPersonal.Listar(rtbNameFilter1.Text, funGet_UnidadNegocio, rcbPositionFilter.SelectedValue, "", rgvUsersList.MasterTableView.CurrentPageIndex,
                                rgvUsersList.MasterTableView.PageSize, BEC.relPersonal.Cargo, BEC.relPerfilPersonal.Perfil, BEC.relPersonal.Supervisor, BEC.relPersonal.UNSucursal)
        Me.rgvUsersList.VirtualItemCount = bcPersonal.Count(rtbNameFilter1.Text, "", rcbPositionFilter.SelectedValue, funGet_UnidadNegocio)
        'bcPersonal.Count(rtbNameFilter1.Text, rcbProfileFilter.SelectedValue, rcbPositionFilter.SelectedValue, "")

        MyBase.proLoad_RadGrid(rgvUsersList, lstUsers)
    End Sub

    Private Sub LoadUser(ByVal IdPersonal As Int32)
        Dim lstSupervisors As List(Of BEC.Personal)
        Dim beCargo As BEB.Cargo
        Dim bcCargo As New BCB.Cargo

        proLoadComboBox()
        bePersonal = bcPersonal.Buscar(IdPersonal)

        With bePersonal
            ViewState("IdPersonal") = .Id
            rtbName.Text = .Nombre
            rtbEmail.Text = .Correo
            rtbTelefono.Text = .Telefono
            rtbLogin.Text = .Login
            rtbRut.Text = .Rut
            hdfPassword.Value = String.Empty
            rcbDepartamento.SelectedValue = .DepartamentoId
            rcbUnidadNegocio.SelectedValue = .UnidadNegocioId
            rcbPosition.SelectedValue = .CargoId
            rcbDependencia.SelectedValue = .DependenciaIdc
            beCargo = bcCargo.Search(.CargoId)
            'lstSupervisors = bcPersonal.ListarPersonalPorCargo(beCargo.CargoId)
            lstSupervisors = bcPersonal.ListarPersonalPorCargo2(-1, funGet_UnidadNegocio)
            rnbFee.Value = .MontoComision
            If ViewState("IdPersonal").ToString <> "" AndAlso IsNumeric(ViewState("IdPersonal")) Then
                lstSupervisors = (From Supervisors In lstSupervisors Where Supervisors.Id <> CLng(ViewState("IdPersonal"))).ToList
            End If
            MyBase.proLoad_RadComboBox(rcbSupervisor, lstSupervisors, "Id", "Nombre")
            If .SupervisorId = -1 Then
                rcbSupervisor.SelectedIndex = -1
            Else
                rcbSupervisor.SelectedValue = .SupervisorId
            End If
            If .Estado = 1 Then
                rbStateDeactivated.Checked = False
                rbStateActivated.Checked = True
            Else
                rbStateActivated.Checked = False
                rbStateDeactivated.Checked = True
            End If
        End With
        rtbName.Focus()
    End Sub

    Private Sub DeleteUser(ByVal IdPersonal As Long)
        bePersonal = New BEC.Personal
        Dim ListaSupervisorPersonal = bcSupervisorPersonal.Listar
        Dim Contador = (From item In ListaSupervisorPersonal Where item.Estado = 1 And item.SupervisorId = IdPersonal).Count
        If (Contador <= 0) Then
            bePersonal = bcPersonal.BuscarPorId(IdPersonal)
            With bePersonal
                .Estado = BEntities.Estado.Inactivo
                .PersonalId = MyBase.funGet_UserCode()
                .StatusType = BEntities.StatusType.Delete
                .FechaActualizacion = Now
            End With
            bcPersonal.Guardar2(bePersonal)
        Else
            ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "closeDialog('divDetail'); funShow_Message('Es Supervisora. No se puede eliminar. No Insista!!!!!!', 'INFORMATION');", True)

        End If

    End Sub
    Private Sub ChangeStateUser(ByVal IdPersonal As Int32)
        bePersonal = New BEC.Personal
        bePersonal = bcPersonal.Buscar(IdPersonal)
        With bePersonal
            '.Id = IdPersonal
            '.Estado = If(.Estado = 1, 2, 1)
            .PersonalEstado = If(.PersonalEstado = 1, 0, 1)
            .PersonalId = MyBase.funGet_UserCode()
            '.FechaReg = Now
            .FechaActualizacion = Now
            .StatusType = BEntities.StatusType.Update
        End With
        bcPersonal.Guardar2(bePersonal)
    End Sub

    Private Sub SaveUser()
        Dim ClientIP As String = ""
        Dim Forwaded As String = ""
        Dim RealIP As String = ""
        Dim strHostName As String = Dns.GetHostName()

        ClientIP = Request.ServerVariables("REMOTE_ADDR")
        If ClientIP <> "" Then
            RealIP = ClientIP
        Else
            'El usuario está accediendo a través de un Proxy.
            Forwaded = Request.ServerVariables("HTTP_X-Forwarded-For")
            If Forwaded <> "" Then RealIP = Forwaded
        End If
        ViewState("Login") = RealIP
        ViewState("Host") = strHostName
        bePersonal = New BEC.Personal
        Dim bcParametroLogin = New BCC.ParametroLogin
        Dim bcPersonalPassword = New BCC.PersonalPassword
        Dim beParametroLogin = bcParametroLogin.Listar.FirstOrDefault
        Dim ListPersonalPassword As List(Of BEC.PersonalPassword) = New List(Of BEC.PersonalPassword)
        beSupervisorPersonal = New BEC.PersonalSupervisor
        Dim resultado = ""
        If CLng(ViewState("IdPersonal")) > 0 Then
            bePersonal = bcPersonal.Buscar(CInt(ViewState("IdPersonal")))
            ListPersonalPassword = bcPersonalPassword.VerificarPasswordAnteriores(rtbPassword.Text, CInt(ViewState("IdPersonal")), beParametroLogin.ComprobacionesPasswordAnteriores)
            If ListPersonalPassword.Count > 0 Then
                MyBase.proShow_Message("No puede ingresar un password que haya utilizado anteriormente. La cantidad de Password guardados es de " & beParametroLogin.ComprobacionesPasswordAnteriores)
                Return
            End If
            If rtbPassword.Text.Contains(bePersonal.Login) Then
                resultado = resultado & "- El password no debe contener el Login del usuario.<br/>"
            End If
        End If
        Dim BEUser2 = bcPersonal.ValidarPersonal(rtbLogin.Text, rtbPassword.Text, BEntities.Estado.Activo, ViewState("Login").ToString, ViewState("Host").ToString)
        If (BEUser2 IsNot Nothing) Then
            resultado = resultado & "- El password no debe ser el mismo que el actual en el sistema.<br/>"
        End If
        If beParametroLogin IsNot Nothing Then
            If rtbPassword.Text.Length > beParametroLogin.LongitudMaxima Then
                resultado = resultado & "- El password no debe exeder los " & beParametroLogin.LongitudMaxima & " caracteres.<br/>"
            End If
            If rtbPassword.Text.Length < beParametroLogin.LongitudMinima Then
                resultado = resultado & "- El password no debe tener menos de " & beParametroLogin.LongitudMinima & " caracteres.<br/>"
            End If
            Dim regexBasico As Regex = New Regex("(?=.*[A-Z])")
            Dim regexMedio As Regex = New Regex("(?=.*[A-Z])(?=.*[a-z])")
            Dim regexFuerte As Regex = New Regex("(?=.*[0-9])(?=.*[A-Z])(?=.*[a-z])")
            Dim regexMuyFuerte As Regex = New Regex("(?=.*[0-9])(?=.*\W)(?=.*[A-Z])(?=.*[a-z])")
            '@#%&-+()/*"':;!?~|{}[]_
            If beParametroLogin.NivelPassword = BE.NivelesPassword.BASICO Then
                If Not regexBasico.IsMatch(rtbPassword.Text) Then
                    resultado = resultado & "- - El password debe contener al menos 1 caracter mayúscula..<br/>"
                End If
            ElseIf beParametroLogin.NivelPassword = BE.NivelesPassword.MEDIO Then
                If Not regexMedio.IsMatch(rtbPassword.Text) Then
                    resultado = resultado & "- El password debe contener al menos 1 caracter mayúscula, 1 caracter minúscula.<br/>"
                End If
            ElseIf beParametroLogin.NivelPassword = BE.NivelesPassword.FUERTE Then
                If Not regexFuerte.IsMatch(rtbPassword.Text) Then
                    resultado = resultado & "- El password debe contener al menos 1 caracter mayúscula, 1 caracter minúscula, 1 numero.<br/>"
                End If
            ElseIf beParametroLogin.NivelPassword = BE.NivelesPassword.MUYFUERTE Then
                If Not regexMuyFuerte.IsMatch(rtbPassword.Text) Then
                    resultado = resultado & "- El password debe contener al menos 1 caracter mayúscula, 1 caracter minúscula, 1 numero y 1 caracter especial.<br/>"
                End If
            End If
        End If
        If resultado.Length > 0 Then
            ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "funShow_Message('Revise lo siguiente:<br/> " & resultado & "', 'INFORMATION');", True)
            Return
        End If
        With bePersonal
            If (.Id > 0) Then
                .StatusType = BEntities.StatusType.Update
            Else
                .StatusType = BEntities.StatusType.Insert
                .PersonalEstado = BEntities.Estado.Activo

            End If
            .Nombre = rtbName.Text
            .Correo = rtbEmail.Text
            '.Telefono = rnbTelefono.Text
            .Telefono = rtbTelefono.Text
            .Rut = rtbRut.Text
            .DepartamentoId = rcbDepartamento.SelectedValue
            .Login = rtbLogin.Text
            .DependenciaIdc = (rcbDependencia.SelectedValue)
            If rbCambioPassword.Checked Then
                .Password = rtbPassword.Text
                .FechaBloqueo = Nothing
            End If
            .MontoComision = rnbFee.Value
            .UnidadNegocioId = rcbUnidadNegocio.SelectedValue
            .CargoId = (rcbPosition.SelectedValue)
            .SupervisorId = (rcbSupervisor.SelectedValue)
            .PersonalEstado = If(rbStateDeactivated.Checked, 0, 1)
            .PersonalId = MyBase.funGet_UserCode()
            .Estado = BEntities.Estado.Activo
            If (ViewState("IdPersonal") = 0) Then
                .FechaReg = Now
            End If
            .FechaActualizacion = Now
            .IntentosFallidos = beParametroLogin.CantidadIntentosFallidos
            .PersonalPassword = New BEC.PersonalPassword
            .PersonalPassword.Password = rtbPassword.Text
            '.PerfilPersonal = New BEC.PerfilPersonal
            '.PerfilPersonal.StatusType = BEntities.StatusType.Insert
            '.PerfilPersonal.PerfilId = rcbProfile.SelectedValue
            '.PerfilPersonal.SupervisorId = rcbSupervisor.SelectedValue
            '.PerfilPersonal.UnidadNegocioId = rcbPlace.SelectedValue
            '.PerfilPersonal.PersonalIdL = funGet_UserCode()
            '.PerfilPersonal.PersonalId = If(ViewState("IdPersonal") = 0, 0, ViewState("IdPersonal"))
            '.PerfilPersonal.FechaReg = Now
            '.PerfilPersonal.FechaActualizacion = Now
            '.PerfilPersonal.Estado = BEntities.Estado.Activo
        End With


        'If bePersonal.StatusType = BEntities.StatusType.Insert Then

        If rcbPosition.SelectedValue <> -1 Then
            If bePersonal.StatusType = BEntities.StatusType.Update And bePersonal.SupervisorId <> rcbSupervisor.SelectedValue Then


            Else
                With beSupervisorPersonal
                    '.PersonalId = bePersonal.Id
                    .SupervisorId = rcbSupervisor.SelectedValue
                    .PersonalIdL = funGet_UserCode()
                    .FechaReg = Now
                    .FechaActualizacion = Now
                    .Estado = BEntities.Estado.Activo
                    .StatusType = BEntities.StatusType.Insert
                End With


            End If


        End If
        'Else

        'With beSupervisorPersonal
        '    .StatusType = BEntities.StatusType.Update
        '    .SupervisorId = rcbPosition.SelectedValue
        '    .PersonalIdL = funGet_UserCode()
        '    .FechaReg = Now
        '    .FechaActualizacion = Now
        '    .Estado = BEntities.Estado.Inactivo
        'End With
        'beSupervisorPersonal.Estado = BEntities.Estado.Inactivo
        'End If

        bePersonal.SupervisorPersonal = beSupervisorPersonal
        bcPersonal.Guardar(bePersonal)
        ClearFields()
        loadGrid()

        proInit_Form()
        ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "closeDialog('divDetail'); funShow_Message('Usuario guardado', 'INFORMATION');", True)
    End Sub

    Private Sub ClearFields()
        rtbName.Text = ""
        rtbEmail.Text = ""
        rtbRut.Text = ""
        rtbLogin.Text = ""
        rtbTelefono.Text = ""
        rtbPassword.Text = ""
        rcbDepartamento.ClearSelection()
        rcbPosition.ClearSelection()
        rcbSupervisor.ClearSelection()
        rcbDependencia.ClearSelection()
        rnbFee.Value = 0
        rbStateActivated.Checked = True
        rbStateDeactivated.Checked = False
    End Sub

    Private Sub proLoadComboBox()
        Dim lstPerfil As List(Of BEC.Perfil)
        Dim beCargo As List(Of BEB.Cargo)
        Dim bcPerfil As New BCC.Perfil
        Dim bcCargo As New BCB.Cargo
        Dim bcDependencia As New BCB.Clasificadores
        Dim lsdepartamento As List(Of BEB.Departamento) = bcDepartamento.List()
        lstPerfil = bcPerfil.Listar()
        beCargo = bcCargo.Listas()
        Dim bcUnidadesNegocioSuc As New BCB.UNSucursal
        Dim listUnidadesNegocioSuc As List(Of BEB.UNSucursal) = bcUnidadesNegocioSuc.List
        MyBase.proLoad_RadComboBox(rcbUnidadNegocio, listUnidadesNegocioSuc, "Id", "Nombre")
        MyBase.proLoad_RadComboBox(rcbDepartamento, lsdepartamento, "Id", "Nombre")
        MyBase.proLoad_RadComboBox(rcbPosition, beCargo, "Id", "Nombre")
        MyBase.proLoad_RadComboBox(rcbDependencia, bcDependencia.List(BEntities.TipoClasificadores.DependenciaPersonal), "Id", "Nombre")
    End Sub

#End Region

End Class