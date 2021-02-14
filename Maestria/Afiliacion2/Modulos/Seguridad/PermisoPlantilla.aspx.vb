Imports BEC = BEntities.Seguridad
Imports BE = BEntities
Imports BEB = BEntities.Base
Imports BCB = BComponents.Base
Imports BCC = BComponents.Seguridad
Imports BCBT = BComponents.Bitacora
Imports BEBT = BEntities.Bitacora
Imports Telerik.Web.UI
Public Class PermisoPlantilla
    Inherits UtilsMethods

#Region " Variable "
    Private bcPersonal As New BCC.Personal
    Private bePlantilla As BEC.Plantilla
    Private bePlantillaDetalle As BEC.PlantillaDetalle
    Private bcPlantilla As New BCC.Plantilla
    Private bcPlantillaDetalle As New BCC.PlantillaDetalle
    Private bePersonal As BEC.Personal
    Private bcPrivilegios As New BCC.Privilegio
    Private bePrivilegios As BEC.Privilegio
    Private bcFormulario As New BCC.Formulario
    Private colOptionsInProfile As List(Of BEC.PlantillaDetalle)
    Private bcBitacoraErrores As New BCBT.BitacoraError
    Private beBitacoraErrores As New BEBT.BitacoraError

    'Public Shared Property rgvModulos As RadGrid = New RadGrid

#End Region

#Region " EVENTS "

    Private Sub Profile_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            proInit_Form()
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
    Public Sub LimpiarCampos()
        rtbProfile.Text = String.Empty
    End Sub
    Protected Sub rgvProfilesList_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles rgvProfileList.ItemCommand
        Try
            Dim beFormularios = bcFormulario.BuscarPorUnidadNegocio(BE.URLFormularios.PermisoPlantilla.ToString, funGet_UnidadNegocioPadre)
            bePrivilegios = bcPrivilegios.BuscarPrivilegioPorPersonal(funGet_UserCode(), beFormularios.Id, funGet_UnidadNegocioPadre())

            If e.CommandName = "EditProfile" Then
                If bePrivilegios.Permiso.Contains("M") Then
                    Dim item As GridDataItem = e.Item
                    Dim IdPlantilla As String = item.GetDataKeyValue("Id").ToString()
                    ViewState("PlantillaId") = IdPlantilla
                    LimpiarCampos()
                    LoadPlantilla(CLng(IdPlantilla))
                    rtbProfile.Text = bcPlantilla.Search(IdPlantilla).Nombre
                Else
                    ScriptManager.RegisterClientScriptBlock(Page,
                                                        Me.GetType,
                                                        "cerrarpopUp",
                                                        "closeDialog('divDetail'); funShow_Message('" & bePrivilegios.NoUpdate & "', 'ERROR');",
                                                        True)
                End If


            ElseIf e.CommandName = "DeleteProfile" Then
                If bePrivilegios.Permiso.Contains("B") Then
                    Try

                        Dim item As GridDataItem = e.Item
                        Dim IdProfile As String = item.GetDataKeyValue("Id").ToString()
                        DeleteProfile(CLng(IdProfile))
                        proInit_Form()
                        ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "closeDialog('divDetail'); funShow_Message('Perfil eliminado', 'INFORMATION');", True)
                    Catch ex As BComponents.BCException
                        'ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "closeDialog('divDetail'); funShow_Message('" & bcPersonal.ErrorMessage(0).Trim & "', 'ERROR');", True)
                        ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "closeDialog('divDetail'); funShow_Message('" & Me.ExceptionMessage(ex) & "', 'ERROR');", True)
                    Catch ex As Exception
                        ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "closeDialog('divDetail'); funShow_Message('" & ex.Message & "', 'ERROR');", True)
                    End Try
                Else
                    ScriptManager.RegisterClientScriptBlock(Page,
                                                        Me.GetType,
                                                        "cerrarpopUp",
                                                        "closeDialog('divDetail'); funShow_Message('" & bePrivilegios.NoDelete & "', 'ERROR');",
                                                        True)
                End If

            End If
        Catch ex As Exception
            ControlarError(MyBase.ObtenerMetodo, If(ex.InnerException IsNot Nothing, ex.InnerException.ToString, ex.Message))
        End Try
        'Catch ex As Exception
        '    ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "closeDialog('divDetail'); funShow_Message('" & ex.Message & "', 'ERROR');", True)
        'End Try
    End Sub

    Private Sub rgvUsersList_PageIndexChanged(sender As Object, e As Telerik.Web.UI.GridPageChangedEventArgs) Handles rgvProfileList.PageIndexChanged
        sender.CurrentPageIndex = e.NewPageIndex
    End Sub

    Private Sub rgvUsersList_PageSizeChanged(sender As Object, e As Telerik.Web.UI.GridPageSizeChangedEventArgs) Handles rgvProfileList.PageSizeChanged
        ' proLoadGrid(rcbType.SelectedValue)
    End Sub

    Private Sub rbNuevo_Click(sender As Object, e As EventArgs) Handles rbNuevo.Click
        Try
            Dim beFormularios = bcFormulario.BuscarPorUnidadNegocio(BE.URLFormularios.PermisoPlantilla.ToString, funGet_UnidadNegocioPadre)
            bePrivilegios = bcPrivilegios.BuscarPrivilegioPorPersonal(funGet_UserCode(), beFormularios.Id, funGet_UnidadNegocioPadre())

            If bePrivilegios.Permiso.Contains("A") Then
                rtbNombrePlantilla.Text = String.Empty
                ViewState("IdProfile") = 0
                ViewState("PlantillaId") = 0
            Else
                ScriptManager.RegisterClientScriptBlock(Page,
                                                            Me.GetType,
                                                            "cerrarpopUp",
                                                            "closeDialog('divDetail2'); funShow_Message('" & bePrivilegios.NoInsert & "', 'ERROR');",
                                                            True)
            End If
        Catch ex As Exception
            ControlarError(MyBase.ObtenerMetodo, If(ex.InnerException IsNot Nothing, ex.InnerException.ToString, ex.Message))
        End Try
    End Sub

    Protected Sub rbAccept_Click(sender As Object, e As EventArgs) Handles rbAccept.Click
        Try
            SaveProfile()
            proInit_Form()
            ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "closeDialog('divDetail'); funShow_Message('Perfil guardado', 'INFORMATION');", True)
        Catch ex As BComponents.BCException
            ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "closeDialog('divDetail'); funShow_Message('" & bcPersonal.ErrorMessage(0).Trim & "', 'ERROR');", True)
        Catch ex As Exception
            'ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "closeDialog('divDetail'); funShow_Message('" & Me.ExceptionMessage(ex) & "', 'ERROR');", True)
            'ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "closeDialog('divDetail'); funShow_Message('" & ex.Message & "', 'ERROR');", True)
            ControlarError(MyBase.ObtenerMetodo, If(ex.InnerException IsNot Nothing, ex.InnerException.ToString, ex.Message))
            ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "closeDialog('divDetail');", True)
        End Try
    End Sub

    Protected Sub rbFilter_Click(sender As Object, e As EventArgs) Handles rbFilter.Click
        Try
            Dim bepersonalBuscar As New List(Of BEC.Plantilla)

            bepersonalBuscar = bcPlantilla.Listar(rtbProfileFilter1.Text, funGet_UnidadNegocioPadre)
            MyBase.proLoad_RadGrid(rgvProfileList, bepersonalBuscar)
        Catch ex As Exception
            ControlarError(MyBase.ObtenerMetodo, If(ex.InnerException IsNot Nothing, ex.InnerException.ToString, ex.Message))
        End Try
    End Sub

    Private Sub rgvModule_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs)
        If TypeOf e.Item Is GridDataItem Then
            If TypeOf e.Item.DataItem Is BEC.Formulario Then
                Dim BEOption As BEC.Formulario = DirectCast(e.Item.DataItem, BEC.Formulario)
                Dim dataItem As GridDataItem = DirectCast(e.Item, GridDataItem)
                Dim chkBox As WebControls.CheckBox = DirectCast(dataItem("gcbcSelected").Controls(0), WebControls.CheckBox)
                Dim chkBox1 As WebControls.CheckBox = DirectCast(dataItem("gcbcSelected1").Controls(0), WebControls.CheckBox)
                Dim chkBox2 As WebControls.CheckBox = DirectCast(dataItem("gcbcSelected2").Controls(0), WebControls.CheckBox)
                Dim chkBox3 As WebControls.CheckBox = DirectCast(dataItem("gcbcSelected3").Controls(0), WebControls.CheckBox)


                chkBox.Enabled = True
                chkBox.InputAttributes.Add("value", BEOption.Id)

                chkBox1.Enabled = True
                chkBox1.InputAttributes.Add("value", BEOption.Id)

                chkBox2.Enabled = True
                chkBox2.InputAttributes.Add("value", BEOption.Id)


                chkBox3.Enabled = True
                chkBox3.InputAttributes.Add("value", BEOption.Id)


                If colOptionsInProfile IsNot Nothing AndAlso (From OptionProfile In colOptionsInProfile
                                                              Where OptionProfile.FormularioId = BEOption.Id And OptionProfile.Permiso.Contains("A")                                                          '
                                                                  ).Any Then


                    chkBox.Checked = True
                End If


                If colOptionsInProfile IsNot Nothing AndAlso (From OptionProfile In colOptionsInProfile
                                                              Where OptionProfile.FormularioId = BEOption.Id And OptionProfile.Permiso.Contains("M")                                                          '
                                                                  ).Any Then


                    chkBox1.Checked = True
                End If


                If colOptionsInProfile IsNot Nothing AndAlso (From OptionProfile In colOptionsInProfile
                                                              Where OptionProfile.FormularioId = BEOption.Id And OptionProfile.Permiso.Contains("B")                                                          '
                                                                  ).Any Then


                    chkBox2.Checked = True
                End If


                If colOptionsInProfile IsNot Nothing AndAlso (From OptionProfile In colOptionsInProfile
                                                              Where OptionProfile.FormularioId = BEOption.Id And OptionProfile.Permiso.Contains("E")                                                          '
                                                                  ).Any Then


                    chkBox3.Checked = True
                End If


            End If
        End If
    End Sub

    Protected Sub rbLimpiar_Click(sender As Object, e As EventArgs) Handles rbLimpiar.Click
        rtbProfileFilter1.Text = ""
        proInit_Form()
    End Sub

#End Region

#Region " PRIVATE METHODS "

    Private Sub proInit_Form()
        Try
            'rbManual.Checked = True
            'Dim lstPersonal As New List(Of BEC.Personal)
            'lstPersonal = bcPersonal.ListarCompleto(BEC.relPersonal.Cargo, BEC.relPersonal.Supervisor, BEC.relPersonal.UNSucursal)
            Dim lstPlantilla As New List(Of BEC.Plantilla)
            lstPlantilla = bcPlantilla.Listar("", funGet_UnidadNegocioPadre)
            Me.rgvProfileList.VirtualItemCount = lstPlantilla.Count

            MyBase.proLoad_RadGrid(rgvProfileList, lstPlantilla)
        Catch ex As Exception
            ControlarError(MyBase.ObtenerMetodo, If(ex.InnerException IsNot Nothing, ex.InnerException.ToString, ex.Message))
        End Try


        'Dim lstProfile As New List(Of BEC.Perfil)

        'lstProfile = bcPersonal.List("Profile")
        'MyBase.proLoad_RadGrid(rgvProfileList, lstProfile)
    End Sub

    Private Sub LoadPlantilla(ByVal IdProfile As Int32)

        bePlantilla = bcPlantilla.Search(IdProfile, BEC.relPlanilla.PlantillaDetalle)
        ViewState("UnidadNegocioId") = bePlantilla.UnidadNegocioId

        colOptionsInProfile = bePlantilla.ColeccionPlantillaDetalle
        With bePlantilla
            ViewState("IdProfile") = .Id
        End With
        proLoadModules()

        'rtbProfile.Focus()
        'bePersonal = bcPersonal.Search(IdProfile, BEC.relPerfil.Option)

        'colOptionsInProfile = bePersonal.CollectionOption

        'With bePersonal
        '    ViewState("IdProfile") = .Id
        '    rtbProfile.Text = .Profile
        'End With

        'proLoadModules()

        'rtbProfile.Focus()

    End Sub
    Private Sub LoadPersonal2(ByVal IdProfile As Int32)
        Dim bePlantilla As BEC.Plantilla = New BEC.Plantilla
        bePlantilla = bcPlantilla.Search(IdProfile, BEC.relPlanilla.PlantillaDetalle)


        colOptionsInProfile = bePlantilla.ColeccionPlantillaDetalle
        With bePlantilla
            ViewState("IdProfile") = .Id
        End With
        proLoadModules()

        'rtbProfile.Focus()
        'bePersonal = bcPersonal.Search(IdProfile, BEC.relPerfil.Option)

        'colOptionsInProfile = bePersonal.CollectionOption

        'With bePersonal
        '    ViewState("IdProfile") = .Id
        '    rtbProfile.Text = .Profile
        'End With

        'proLoadModules()

        'rtbProfile.Focus()

    End Sub

    Private Sub DeleteProfile(ByVal IdProfile As Long)
        'bcPersonal.Delete(IdProfile)
    End Sub

    Private Sub SaveProfile()

        'Dim bePrivilegiosaux As BEC.Privilegio
        'bePrivilegiosaux = New BEC.Privilegio
        ''Dim tab1 As RadTab = rtsModules.SelectedTab


        ''Dim rgvtext As RadGrid = TryCast(rpvTabPages.PageViews.Item(0).Controls.Item(0), RadGrid)

        ''Dim tab As RadTab = rtsModules.Tabs(1)


        ''For Each item As GridDataItem In rgvModulos.MasterTableView.Items

        ''    Dim Formulario As String = item.GetDataKeyValue("Id").ToString

        ''    Dim rckColumnInsert As Boolean = TryCast(item("gcbcSelected").Controls(0), CheckBox).Checked
        ''    Dim rckColumnUpdate As Boolean = TryCast(item("gcbcSelected1").Controls(0), CheckBox).Checked
        ''    Dim rckColumnDelete As Boolean = TryCast(item("gcbcSelected2").Controls(0), CheckBox).Checked
        ''    Dim rckColumnExport As Boolean = TryCast(item("gcbcSelected3").Controls(0), CheckBox).Checked

        ''    Dim idpersonal As Integer = ViewState("IdProfile")
        ''    If rckColumnInsert Or rckColumnUpdate Or rckColumnDelete Or rckColumnExport Then

        ''        Dim permisoaux As String = If(rckColumnInsert, "A", "") + If(rckColumnUpdate, "B", "") + If(rckColumnDelete, "B", "") + If(rckColumnExport, "E", "")

        ''        If colOptionsInProfile IsNot Nothing AndAlso (From OptionProfile In colOptionsInProfile
        ''                                                      Where OptionProfile.FormularioId = Formulario).Any Then

        ''update
        'bePrivilegiosaux.StatusType = BEntities.StatusType.Update
        '            With bePrivilegiosaux
        '                .Id = (From OptionProfile In colOptionsInProfile
        '                       Where OptionProfile.FormularioId = Formulario
        '                       Select OptionProfile.Id).First

        '                .PersonalId = idpersonal
        '                .FormularioId = Formulario
        '                .Permiso = permisoaux
        '            End With
        '            bcPrivilegios.Guardar(bePrivilegiosaux)

        '        Else
        '            'insert 

        '            bePrivilegiosaux.StatusType = BEntities.StatusType.Insert
        '            With bePrivilegiosaux
        '                .PersonalIdL = MyBase.funGet_UserCode()
        '                .FechaReg = Now
        '                .PersonalId = idpersonal
        '                .FormularioId = Formulario
        '                .Permiso = permisoaux
        '                .TiempoDias = 10
        '                .UnidadNegocioId = MyBase.funGet_UnidadNegocio
        '                .Estado = 1
        '            End With
        '            bcPrivilegios.Guardar(bePrivilegiosaux)
        'chkBox.Checked = True
        '    End If


        'End If

        'bePeriodoSemanal = New BEM.PeriodoSemanal
        'With bePeriodoSemanal
        '    If CLng(ViewState("PeriodoSemanlId")) = 0 Then
        '        .StatusType = BEntities.StatusType.Update
        '        .Id = CInt(item.GetDataKeyValue("Id").ToString)
        '        .FechaReg = Now
        '    Else
        '        .StatusType = BEntities.StatusType.Update
        '        .Id = CInt(item.GetDataKeyValue("Id").ToString)
        '        .FechaReg = bcPeriodoSemanal.Search(CInt(ViewState("PeriodoSemanlId"))).FechaReg
        '    End If

        '    .Semana = CInt(item.GetDataKeyValue("Semana").ToString)
        '    Dim rcbTemporada As RadComboBox = CType(item.FindControl("rcbTemporada"), RadComboBox)
        '    .TemporadaId = rcbTemporada.SelectedValue
        '    .FechaActualizacion = Now
        '    .PersonalId = MyBase.funGet_UserCode
        '    .Estado = BEntities.Estado.Activo
        '    listaPeriodoSemanal.Add(bePeriodoSemanal)
        'End With
        'Next


        'bePrivilegiosaux.StatusType = BEntities.StatusType.Update
        '            With bePrivilegiosaux
        '                .Id = (From OptionProfile In colOptionsInProfile
        '                       Where OptionProfile.FormularioId = Formulario
        '                       Select OptionProfile.Id).First

        '                .PersonalId = idpersonal
        '                .FormularioId = Formulario
        '                .Permiso = permisoaux
        '            End With
        '            bcPrivilegios.Guardar(bePrivilegiosaux)

        '        Else
        '            'insert 

        '            bePrivilegiosaux.StatusType = BEntities.StatusType.Insert
        '            With bePrivilegiosaux
        '                .PersonalIdL = MyBase.funGet_UserCode()
        '                .FechaReg = Now
        '                .PersonalId = idpersonal
        '                .FormularioId = Formulario
        '                .Permiso = permisoaux
        '                .TiempoDias = 10
        '                .UnidadNegocioId = MyBase.funGet_UnidadNegocio
        '                .Estado = 1
        '            End With
        '            bcPrivilegios.Guardar(bePrivilegiosaux)
        'chkBox.Checked = True
        '    End If




        Dim intOptionIDs As String() = Nothing

        bePlantillaDetalle = New BEC.PlantillaDetalle
        Dim listOrdenada As List(Of String) = New List(Of String)()
        If hdnOptionIds.Value.Trim.Length > 0 Then
            intOptionIDs = hdnOptionIds.Value.Split(",")
        End If
        If (intOptionIDs Is Nothing) Then
            listOrdenada = New List(Of String)()
        Else
            listOrdenada.AddRange(intOptionIDs)
            Dim intOptionIDs2 As String() = Nothing
            listOrdenada.Sort()
        End If
        Dim aux As String = ""
        Dim listanueva As List(Of String) = New List(Of String)
        Dim privile As String = ""
        For Each item In listOrdenada
            If (aux = item.Split("-")(0)) Then
                privile = listanueva.ElementAt(listanueva.Count - 1)
                Dim valor1 As String = privile.Split("-")(0)
                Dim valor2 As String = privile.Split("-")(1)
                valor2 = String.Concat(valor2, item.Split("-")(1))
                privile = String.Concat(valor1, "-", valor2)
                listanueva.RemoveAt(listanueva.Count - 1)
            Else
                privile = item
            End If
            aux = item.Split("-")(0)
            listanueva.Add(privile)
        Next

        Dim beplantilla2 = bcPlantilla.Search(ViewState("PlantillaId"), BEC.relPlanilla.PlantillaDetalle)
        Dim listaDetallePlanilla As List(Of BEC.PlantillaDetalle) = New List(Of BEC.PlantillaDetalle)
        Dim accionesFiltradas As String = ""
        Dim accionesFiltradasTotal As String = ""
        For Each it In listanueva
            accionesFiltradasTotal = ""
            accionesFiltradas = ""
            bePlantillaDetalle = New BEC.PlantillaDetalle
            Dim PlantiDet As BEC.PlantillaDetalle = bcPlantillaDetalle.BuscarPlantillaDetallePorPersonal(ViewState("PlantillaId"), it.Split("-")(0), ViewState("UnidadNegocioId"))


            With bePlantillaDetalle
                If (PlantiDet IsNot Nothing) Then
                    .StatusType = BEntities.StatusType.Update
                    .Id = PlantiDet.Id
                Else
                    .StatusType = BEntities.StatusType.Insert
                End If

                '.PersonalIdL = rtbProfile.Text
                .PersonalId = MyBase.funGet_UserCode()
                .PlantillaId = ViewState("PlantillaId")
                .FechaReg = Now
                .FormularioId = it.Split("-")(0)
                accionesFiltradas = it.Split("-")(1)
                For it2 As Int32 = 0 To accionesFiltradas.Length - 1
                    If it2 > 0 Then
                        If accionesFiltradas.Chars(it2 - 1).Equals(accionesFiltradas.Chars(it2)) Then
                            accionesFiltradasTotal = accionesFiltradasTotal & accionesFiltradas.ElementAt(it2)
                        End If
                    End If
                Next
                .Permiso = If(accionesFiltradasTotal.Length = 0, accionesFiltradas, accionesFiltradasTotal)
                .TiempoDias = 10
                .UnidadNegocioId = beplantilla2.UnidadNegocioId
                .Estado = BEntities.Estado.Activo
            End With
            listaDetallePlanilla.Add(bePlantillaDetalle)
        Next

        Dim resultado = From b In beplantilla2.ColeccionPlantillaDetalle Group Join c In listanueva On c.Split("-")(0) Equals b.FormularioId Into g = Group From r In g.DefaultIfEmpty Select New With {b, r}

        For Each result In resultado
            accionesFiltradasTotal = ""
            accionesFiltradas = ""
            If (result.r Is Nothing) Then
                bePlantillaDetalle = New BEC.PlantillaDetalle
                With bePlantillaDetalle
                    .StatusType = BEntities.StatusType.Delete
                    .Id = result.b.Id
                    .PlantillaId = ViewState("PlantillaId")
                    .PersonalId = MyBase.funGet_UserCode()
                    .FechaReg = Now
                    .PersonalId = funGet_UserCode()
                    .FormularioId = result.b.FormularioId
                    For it2 As Int32 = 0 To result.b.Permiso.Length - 1
                        If it2 > 0 Then
                            If result.b.Permiso.Chars(it2 - 1).Equals(result.b.Permiso.Chars(it2)) Then
                                accionesFiltradasTotal = accionesFiltradasTotal & result.b.Permiso.ElementAt(it2)
                            End If
                        End If
                    Next
                    .Permiso = If(accionesFiltradasTotal.Length = 0, result.b.Permiso, accionesFiltradasTotal)
                    .TiempoDias = 10
                    .UnidadNegocioId = MyBase.funGet_UnidadNegocioPadre
                    .Estado = BEntities.Estado.Inactivo
                End With
                listaDetallePlanilla.Add(bePlantillaDetalle)
            End If
        Next
        bcPlantillaDetalle.Guardar(listaDetallePlanilla)
        MessageInClientSide()
        ClearFields()

    End Sub

    Private Sub ClearFields()
        rtbProfile.Text = ""
    End Sub
    Private Sub rgvProfileList_PageIndexChanged(sender As Object, e As Telerik.Web.UI.GridPageChangedEventArgs) Handles rgvProfileList.PageIndexChanged
        proInit_Form()
    End Sub
    Private Sub proLoadModules()
        Dim BCModules As New BCC.Modulo
        Dim lstModules As List(Of BEC.Modulo)
        Dim rtTab As RadTab
        Dim rpvPageTab As RadPageView

        lstModules = BCModules.Listar(If(MyBase.funGet_UnidadNegocio = BEntities.UnidadNegocio.Yotau, BEntities.UnidadNegocio.Yotau, CInt(ViewState("UnidadNegocioId"))), BEC.relModulo.Formulario)
        rpvTabPages.PageViews.Clear()
        rtsModules.Tabs.Clear()

        For Each BEModule In lstModules
            rtTab = New RadTab(BEModule.Nombre)
            rpvPageTab = New RadPageView

            rpvPageTab.ID = "rpv" & BEModule.Id
            rtTab.PageViewID = rpvPageTab.ID
            'rtTab.Text = BEModule.Nombre


            Dim rgvModule As New RadGrid

            rgvModule.ID = "rgvModule_" & BEModule.Nombre
            rgvModule.Skin = "Bootstrap"
            'rgvModule.Width = Unit.Pixel(410)
            rgvModule.CssClass = "combo100porc"
            rgvModule.MasterTableView.NoMasterRecordsText = "No existen opciones registradas."
            rgvModule.MasterTableView.AutoGenerateColumns = False
            rgvModule.MasterTableView.BatchEditingSettings.EditType = GridBatchEditingType.Cell
            rgvModule.MasterTableView.DataKeyNames = {"Id"}

            Dim rgcColumn As New Telerik.Web.UI.GridBoundColumn
            rgcColumn.DataField = "Id"
            rgcColumn.UniqueName = "Id"
            rgcColumn.Display = False
            rgvModule.MasterTableView.Columns.Add(rgcColumn)

            rgcColumn = New GridBoundColumn
            'rgcColumn.DataField = "Name"
            'rgcColumn.UniqueName = "Name"

            rgcColumn.DataField = "Nombre"
            rgcColumn.UniqueName = "Nombre"

            rgcColumn.HeaderText = "Opcion"
            rgcColumn.ReadOnly = True
            rgcColumn.HeaderStyle.Width = Unit.Pixel(200)
            rgvModule.MasterTableView.Columns.Add(rgcColumn)

            Dim rckColumn As New Telerik.Web.UI.GridCheckBoxColumn
            rckColumn.DataField = "Selected"
            rgcColumn.UniqueName = "Selected"
            rckColumn.UniqueName = "gcbcSelected"
            rckColumn.HeaderText = "Insertar"
            rgcColumn.ReadOnly = False
            rckColumn.HeaderStyle.Width = Unit.Pixel(16)
            rckColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Center
            rgvModule.MasterTableView.Columns.Add(rckColumn)

            Dim rckColumn1 As New Telerik.Web.UI.GridCheckBoxColumn
            rckColumn1.DataField = "Selected"
            rgcColumn.UniqueName = "Selected1"
            rckColumn1.UniqueName = "gcbcSelected1"
            rckColumn1.HeaderText = "Actualizar"
            rgcColumn.ReadOnly = False
            rckColumn1.HeaderStyle.Width = Unit.Pixel(16)
            rckColumn1.ItemStyle.HorizontalAlign = HorizontalAlign.Center
            rgvModule.MasterTableView.Columns.Add(rckColumn1)

            Dim rckColumn2 As New Telerik.Web.UI.GridCheckBoxColumn
            rckColumn2.DataField = "Selected"
            rgcColumn.UniqueName = "Selected2"
            rckColumn2.UniqueName = "gcbcSelected2"
            rckColumn2.HeaderText = "Eliminar"
            rgcColumn.ReadOnly = False
            rckColumn2.HeaderStyle.Width = Unit.Pixel(16)
            rckColumn2.ItemStyle.HorizontalAlign = HorizontalAlign.Center
            rgvModule.MasterTableView.Columns.Add(rckColumn2)

            Dim rckColumn3 As New Telerik.Web.UI.GridCheckBoxColumn
            rckColumn3.DataField = "Selected"
            rgcColumn.UniqueName = "Selected"
            rckColumn3.UniqueName = "gcbcSelected3"
            rckColumn3.HeaderText = "Exportar"
            rgcColumn.ReadOnly = False
            rckColumn3.HeaderStyle.Width = Unit.Pixel(16)
            rckColumn3.ItemStyle.HorizontalAlign = HorizontalAlign.Center
            rgvModule.MasterTableView.Columns.Add(rckColumn3)

            AddHandler rgvModule.ItemDataBound, AddressOf rgvModule_ItemDataBound

            MyBase.proLoad_RadGrid(rgvModule, BEModule.ColeccionFormulario)

            rpvPageTab.Controls.Add(rgvModule)
            'rgvModulos = rgvModule
            rpvTabPages.PageViews.Add(rpvPageTab)

            rtsModules.Tabs.Add(rtTab)
        Next

        rpvTabPages.SelectedIndex = 0
        rtsModules.SelectedIndex = 0

    End Sub

    Private Sub RecreateOptionControls()



        Dim BCModules As New BCC.Modulo
        Dim lstModules As List(Of BEC.Modulo)

        If rtsModules.Tabs.Count > 0 Then

            lstModules = BCModules.Listar(If(MyBase.funGet_UnidadNegocio = BEntities.UnidadNegocio.Yotau, BEntities.UnidadNegocio.Yotau, CInt(ViewState("UnidadNegocioId"))), BEC.relModulo.Formulario)
            rtsModules.Tabs.Clear()
            rpvTabPages.PageViews.Clear()

            Dim rtTab As RadTab
            Dim rpvPageTab As RadPageView

            For Each BEModule In lstModules
                rtTab = New RadTab(BEModule.Nombre)
                rpvPageTab = New RadPageView
                rpvPageTab.ID = "rpv" & BEModule.Id
                rtTab.PageViewID = rpvPageTab.ID

                Dim rgvModule As New RadGrid
                rgvModule.ID = "rgvModule_" & BEModule.Nombre
                rgvModule.DataBind()

                rpvPageTab.Controls.Add(rgvModule)
                rpvTabPages.PageViews.Add(rpvPageTab)
                rtsModules.Tabs.Add(rtTab)
            Next
        End If
    End Sub

    Private Sub MessageInClientSide()
        ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "closeDialog('divDetail'); funShow_Message('Plantilla guardada', 'INFORMATION');", True)
    End Sub

    Private Sub rtbSave_Click(sender As Object, e As EventArgs) Handles rtbSave.Click

        Try
            bePlantilla = bcPlantilla.existe(rtbNombrePlantilla.Text)
            If (bePlantilla Is Nothing) Then
                bePlantilla = New BEC.Plantilla
                With bePlantilla
                    .Id = 0
                    .StatusType = BEntities.StatusType.Insert
                    .PersonalId = MyBase.funGet_UserCode
                    .FechaReg = Now
                    .Nombre = rtbNombrePlantilla.Text.Trim
                    .UnidadNegocioId = funGet_UnidadNegocioPadre()
                    .Estado = BEntities.Estado.Activo
                End With
                bcPlantilla.Guardar(bePlantilla)
                proInit_Form()
                ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "closeDialog('divDetail2'); funShow_Message('Plantilla guardada', 'INFORMATION');", True)
            Else
                MyBase.proShow_Message("El nombre de la Plantilla ya existe.")
            End If

        Catch ex As Exception
            ControlarError(MyBase.ObtenerMetodo, If(ex.InnerException IsNot Nothing, ex.InnerException.ToString, ex.Message))
        End Try
    End Sub


#End Region


End Class