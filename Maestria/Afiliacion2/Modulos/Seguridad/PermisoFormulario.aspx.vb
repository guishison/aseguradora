Imports BEC = BEntities.Seguridad
Imports BCC = BComponents.Seguridad
Imports BEI = BEntities.Inventario
Imports BCI = BComponents.Inventario
Imports BEB = BEntities.Base
Imports BCB = BComponents.Base
Imports BCBT = BComponents.Bitacora
Imports BEBT = BEntities.Bitacora
Imports Telerik.Web.UI

Public Class PermisoFormulario
    Inherits UtilsMethods

#Region " Variable "
    Private bcFormulario As New BCC.Formulario
    Private beFormulario As BEC.Formulario
    Private bcModulo As New BCC.Modulo
    Private beModulo As BEC.Modulo
    Private bcPrivilegio As New BCC.Privilegio
    Private bePrivilegio As BEC.Privilegio
    'Private Shared Property beHotel As BEI.Hotel = New BEI.Hotel
    'Private bcHotel As New BCI.Hotel
    'Private bcProfiles As New BCC.Profile
    'Private beProfile As BEC.Profile
    Private bcBitacoraErrores As New BCBT.BitacoraError
    Private beBitacoraErrores As New BEBT.BitacoraError
    Private colOptionsInProfile As List(Of BEC.Formulario)
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
        MyBase.proShow_Message("Error, verifique lo siguiente: " & Sms)
    End Sub

    Protected Sub rgvProfilesList_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles rgvProfileList.ItemCommand
        Try
            If e.CommandName = "EditProfile" Then
                Dim item As GridDataItem = e.Item
                Dim UnidadNegocioId As String = item.GetDataKeyValue("Id").ToString()
                ViewState("UnidadNegocioId") = UnidadNegocioId
                LoadHotel(UnidadNegocioId)
            ElseIf e.CommandName = "DeleteProfile" Then
                Try
                    Dim item As GridDataItem = e.Item
                    Dim IdProfile As String = item.GetDataKeyValue("Id").ToString()
                    'DeleteProfile(CLng(IdProfile))
                    proInit_Form()
                    ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "closeDialog('divDetail'); funShow_Message('Perfil Eliminado', 'INFORMATION');", True)
                Catch ex As BComponents.BCException
                    'ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "closeDialog('divDetail'); funShow_Message('" & bcProfiles.ErrorMessage(0).Trim & "', 'ERROR');", True)
                    ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "closeDialog('divDetail'); funShow_Message('" & Me.ExceptionMessage(ex) & "', 'ERROR');", True)
                Catch ex As Exception
                    ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "closeDialog('divDetail'); funShow_Message('" & ex.Message & "', 'ERROR');", True)
                End Try
            End If
        Catch ex As Exception
            ControlarError(MyBase.ObtenerMetodo, If(ex.InnerException IsNot Nothing, ex.InnerException.ToString, ex.Message))
        End Try
    End Sub

    Private Sub rgvUsersList_PageIndexChanged(sender As Object, e As Telerik.Web.UI.GridPageChangedEventArgs) Handles rgvProfileList.PageIndexChanged
        sender.CurrentPageIndex = e.NewPageIndex
    End Sub

    Private Sub rgvUsersList_PageSizeChanged(sender As Object, e As Telerik.Web.UI.GridPageSizeChangedEventArgs) Handles rgvProfileList.PageSizeChanged
        ' proLoadGrid(rcbType.SelectedValue)
    End Sub

    Private Sub rbAdd_Click(sender As Object, e As EventArgs) Handles rbAdd.Click
        Try
            proLoadModules()
            ClearFields()
            ViewState("UnidadNegocioId") = 0
        Catch ex As Exception
            ControlarError(MyBase.ObtenerMetodo, If(ex.InnerException IsNot Nothing, ex.InnerException.ToString, ex.Message))
        End Try
    End Sub

    Protected Sub rbAccept_Click(sender As Object, e As EventArgs) Handles rbAccept.Click
        Try
            SaveProfile()
            proInit_Form()
            ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "closeDialog('divDetail'); funShow_Message('Perfil Guardado', 'INFORMATION');", True)
        Catch ex As BComponents.BCException
            ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "closeDialog('divDetail'); funShow_Message('" & bcFormulario.ErrorMessage(0).Trim & "', 'ERROR');", True)
        Catch ex As Exception
            'ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "closeDialog('divDetail'); funShow_Message('" & Me.ExceptionMessage(ex) & "', 'ERROR');", True)
            ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "closeDialog('divDetail'); funShow_Message('" & ex.Message & "', 'ERROR');", True)
        End Try
    End Sub

    Protected Sub rbFilter_Click(sender As Object, e As EventArgs) Handles rbFilter.Click
        Try
            'Dim lstProfile As New List(Of BEI.Hotel)

            'lstProfile = bcHotel.List(rtbProfileFilter1.Text, BEntities.Estado.Activo, rgvProfileList.MasterTableView.CurrentPageIndex, rgvProfileList.MasterTableView.PageSize)
            'MyBase.proLoad_RadGrid(rgvProfileList, lstProfile)
        Catch ex As Exception
            ControlarError(MyBase.ObtenerMetodo, If(ex.InnerException IsNot Nothing, ex.InnerException.ToString, ex.Message))
        End Try
    End Sub

    Private Sub rgvModule_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs)
        Try
            If TypeOf e.Item Is GridDataItem Then
                If TypeOf e.Item.DataItem Is BEC.Formulario Then
                    Dim BEOption As BEC.Formulario = DirectCast(e.Item.DataItem, BEC.Formulario)
                    Dim dataItem As GridDataItem = DirectCast(e.Item, GridDataItem)
                    Dim chkBox As WebControls.CheckBox = DirectCast(dataItem("gcbcSelected").Controls(0), WebControls.CheckBox)
                    chkBox.Enabled = True
                    chkBox.InputAttributes.Add("value", BEOption.Id)
                    If colOptionsInProfile IsNot Nothing AndAlso (From OptionProfile In colOptionsInProfile
                                                                  Where OptionProfile.Url = BEOption.Url).Any Then
                        chkBox.Checked = True
                    End If
                End If
            End If
        Catch ex As Exception
            ControlarError(MyBase.ObtenerMetodo, If(ex.InnerException IsNot Nothing, ex.InnerException.ToString, ex.Message))
        End Try
    End Sub

    Protected Sub rbLimpiar_Click(sender As Object, e As EventArgs) Handles rbLimpiar.Click
        rtbProfileFilter1.Text = ""
        proInit_Form()
    End Sub

#End Region

#Region " PRIVATE METHODS "

    Private Sub proInit_Form()
        'Dim lstProfile As New List(Of BEI.Hotel)

        'lstProfile = bcHotel.ListTodo()
        'MyBase.proLoad_RadGrid(rgvProfileList, lstProfile)
    End Sub

    Private Sub LoadHotel(ByVal HotelId As Int32)

        'beHotel = bcHotel.Search(HotelId, BEI.relHotel.Formulario, BEC.relFormulario.Modulo)

        'colOptionsInProfile = beHotel.ColeccionFormularios

        'With beHotel
        '    ViewState("IdProfile") = .Id
        '    rtbProfile.Text = .Nombre
        'End With

        'proLoadModules()

        'rtbProfile.Focus()

    End Sub

    'Private Sub DeleteProfile(ByVal IdProfile As Long)
    '    bcHotel.Delete(IdProfile)
    'End Sub

    Private Sub SaveProfile()
        'Dim intOptionIDs As String() = Nothing

        'beFormulario = New BEC.Formulario

        'If hdnOptionIds.Value.Trim.Length > 0 Then
        '    intOptionIDs = hdnOptionIds.Value.Substring(0, hdnOptionIds.Value.Length - 1).Split(",")
        'End If
        'Dim IdsFormulario As IEnumerable(Of String) = intOptionIDs
        ''beHotel.ColeccionFormularios

        'Dim listFormularios As List(Of BEC.Formulario) = New List(Of BEC.Formulario)


        'Dim formularios As List(Of BEC.Formulario) = bcFormulario.ReturnListaChild(IdsFormulario)

        'If intOptionIDs IsNot Nothing Then
        '    For Each item In formularios

        '        beFormulario = bcFormulario.BuscarPorUnidadNegocio(item.Url, CInt(ViewState("UnidadNegocioId")))
        '        If (beFormulario Is Nothing) Then
        '            beFormulario = New BEC.Formulario
        '        End If

        '        With beFormulario
        '            If beFormulario IsNot Nothing Then
        '                .StatusType = BEntities.StatusType.Insert
        '            Else
        '                .StatusType = BEntities.StatusType.Update
        '            End If
        '            .Url = item.Url
        '            .Nombre = item.Nombre
        '            .TipoFormulario = item.TipoFormulario
        '            .ModuloId = item.ModuloId
        '            .Icono = item.Icono
        '            .Orden = item.Orden
        '            .Estado = BEntities.Estado.Activo
        '            .Acciones = item.Acciones
        '            .UnidadNegocioId = ViewState("UnidadNegocioId")
        '        End With
        '        listFormularios.Add(beFormulario)
        '    Next
        'End If

        'If formularios.Count > 0 And beHotel.ColeccionFormularios.Count > 0 Then
        '    Dim resultado = From b In beHotel.ColeccionFormularios Group Join c In formularios On c.Url Equals b.Url Into g = Group From r In g.DefaultIfEmpty Select New With {b, r}
        '    For Each result In resultado
        '        If (result.r Is Nothing) Then
        '            beFormulario = New BEC.Formulario
        '            With beFormulario
        '                .StatusType = BEntities.StatusType.Delete
        '                .Id = result.b.Id
        '                .Url = result.b.Url
        '                .Nombre = result.b.Nombre
        '                .TipoFormulario = result.b.TipoFormulario
        '                .ModuloId = result.b.ModuloId
        '                .Icono = result.b.Icono
        '                .Orden = result.b.Orden
        '                .Estado = BEntities.Estado.Inactivo
        '                .Acciones = result.b.Acciones
        '                .UnidadNegocioId = ViewState("UnidadNegocioId")
        '            End With
        '            listFormularios.Add(beFormulario)
        '        End If
        '    Next
        'Else

        'End If


        'bcFormulario.Guardar(listFormularios)
        'MessageInClientSide()
        'ClearFields()

    End Sub

    Private Sub ClearFields()
        rtbProfile.Text = ""
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

            Dim rgvModule As New RadGrid
            rgvModule.ID = "rgvModule_" & BEModule.Nombre
            rgvModule.Skin = "Bootstrap"
            rgvModule.Width = Unit.Pixel(410)
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
            rgcColumn.DataField = "Nombre"
            rgcColumn.UniqueName = "Nombre"
            rgcColumn.HeaderText = "Formulario"
            rgcColumn.ReadOnly = True
            rgcColumn.HeaderStyle.Width = Unit.Pixel(200)
            rgvModule.MasterTableView.Columns.Add(rgcColumn)

            Dim rckColumn As New Telerik.Web.UI.GridCheckBoxColumn
            rckColumn.DataField = "Selected"
            rgcColumn.UniqueName = "Selected"
            rckColumn.UniqueName = "gcbcSelected"
            rckColumn.HeaderText = "&nbsp;"
            rgcColumn.ReadOnly = False
            rckColumn.HeaderStyle.Width = Unit.Pixel(16)
            rgvModule.MasterTableView.Columns.Add(rckColumn)

            AddHandler rgvModule.ItemDataBound, AddressOf rgvModule_ItemDataBound

            MyBase.proLoad_RadGrid(rgvModule, BEModule.ColeccionFormulario)

            rpvPageTab.Controls.Add(rgvModule)
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
        ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "closeDialog('divDetail'); funShow_Message('Perfil guardado', 'INFORMATION');", True)
    End Sub

#End Region


End Class