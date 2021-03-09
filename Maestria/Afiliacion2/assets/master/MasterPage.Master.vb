Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Web.Services
Imports System.Windows.Forms

Public Class MasterPage
    Inherits System.Web.UI.MasterPage
    Public Shared Version As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("myapplication.language") = "en-Us"


        'Response.Redirect("/Afiliacion2/login.aspx")

        Dim virtualPath As String = Hosting.HostingEnvironment.ApplicationVirtualPath
        Dim appName As String = virtualPath.Substring(1)
        Version = lblVersion.Text + " " + lblComentario.Text
        'HelpDeskImg.Attributes.Add("onclick", "Clickcito")
        'Img1.Attributes.Add("onclick", "Clickcito")
        closeSession.NavigateUrl = "/" & appName & "/login.aspx"
        editProfile.NavigateUrl = "/" & appName & "/Modulos/Seguridad/Cuenta.aspx"
        logo.Src = "/" + appName + "/assets/image/logo.png"
        'logodte.Src = "/" + appName + "/assets/image/logoV2.png"
        ImgHelpDesk.NavigateUrl = "/" & appName & "/Modulos/HelpDesk/HelpDesk.aspx"
        'construyendo el menu
        If (Me.funGet_UserCode() > 0) Then
            If (Me.hasAccess()) Then
                loadMenu()
                loadUser()
            End If
        End If

    End Sub
    '<WebMethod>
    'Public Sub CargarHelpDesk()
    '    Dim bmpScreenshot As New Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb)
    '    Dim gfxScreenshot As Graphics = Graphics.FromImage(bmpScreenshot)
    '    gfxScreenshot.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy)
    '    'bmpScreenshot.Save(FileName, ImageFormat.Png)
    '    Dim bitmapBytes As Byte()

    '    Using stream As New System.IO.MemoryStream
    '        bmpScreenshot.Save(stream, bmpScreenshot.RawFormat)
    '        bitmapBytes = stream.ToArray
    '    End Using
    '    Dim virtualPath As String = Hosting.HostingEnvironment.ApplicationVirtualPath
    '    Dim appName As String = virtualPath.Substring(1)
    '    Server.Transfer("Servicios.aspx?ModiloId=" & appName, True)
    '    'Response.BufferOutput = True
    '    'Response.Redirect("/" + appName + "/HelpDesk.aspx?ModuloId=")
    'End Sub
    Private Function hasAccess() As Boolean
        'url params
        Dim url As String = HttpContext.Current.Request.Url.AbsoluteUri
        Dim host As String = HttpContext.Current.Request.Url.Host
        Dim virtualPath As String = Hosting.HostingEnvironment.ApplicationVirtualPath
        Dim appName As String = virtualPath.Substring(1)
        'consultas
        Dim bc = New BComponents.Seguridad.Formulario
        Dim bcPersonal = New BComponents.Seguridad.Personal
        Dim bePersonal = New BEntities.Seguridad.Personal
        Dim lstFormulario = New List(Of BEntities.Seguridad.Formulario)

        url = url.ToLower()

        If (url.Contains("default.aspx") Or url.Contains("cuenta.aspx")) Then
            Return True
        Else
            bePersonal = bcPersonal.Buscar(Me.funGet_UserCode())
            lstFormulario = bc.ListMenuOptions(bePersonal.Id, funGet_UnidadNegocioPadre)

            Dim beOption = (From obj In lstFormulario Where url.Contains(obj.Url.ToLower()) Select obj).FirstOrDefault
            If (beOption IsNot Nothing) Then
                Return True
            End If
        End If
        'show message
        ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "showmessage", "alert('No posee acceso a este módulo!'); location.href='http://" & host & "/" & appName & "/default.aspx';", True)
        Return False
    End Function

    Private Sub loadUser()
        Dim bc = New BComponents.Seguridad.Personal
        Dim bePersonal = bc.Buscar(Me.funGet_UserCode())
        userName.InnerHtml = bePersonal.Nombre
        Dim bcUnidadNegocio = New BComponents.Base.UNSucursal
        lblConcesionaria.InnerHtml = bcUnidadNegocio.Search(bePersonal.UnidadNegocioId).Nombre
    End Sub

    Private Sub loadMenu()
        Dim bcPersonal = New BComponents.Seguridad.Personal
        Dim bc = New BComponents.Seguridad.Formulario
        Dim bcModulo = New BComponents.Seguridad.Modulo
        Dim bePersonal = New BEntities.Seguridad.Personal
        Dim beModulo = New BEntities.Seguridad.Modulo
        Dim lstFormularios = New List(Of BEntities.Seguridad.Formulario)
        Dim lstModulos = New List(Of BEntities.Seguridad.Modulo)
        Dim lstModuloId = New List(Of Int32)
        Dim url As String = HttpContext.Current.Request.Url.AbsoluteUri
        Dim host As String = HttpContext.Current.Request.Url.Host
        Dim virtualPath As String = Hosting.HostingEnvironment.ApplicationVirtualPath
        Dim appName As String = virtualPath.Substring(1)
        Dim menu As String = ""
        Dim item As String = ""
        Dim selected As String = ""
        bePersonal = bcPersonal.Buscar(Me.funGet_UserCode())
        lstFormularios = bc.ListMenuOptions(bePersonal.Id, funGet_UnidadNegocioPadre)
        Dim modulos = bcModulo.ListarPadresSinHijos(Me.funGet_UserCode)
        lstModuloId = (From obj In lstFormularios Select obj.ModuloId).Distinct.ToList
        'Dim agregas As Boolean = False
        'For Each ite In modulos

        '    For Each it In lstModuloId
        '        If (ite.sub) Then

        '        End If
        '    Next
        'Next
        For Each id As Long In lstModuloId
            beModulo = bcModulo.Buscar(id)
            beModulo.ColeccionFormulario = (From obj In lstFormularios Where obj.ModuloId = id Select obj Order By obj.Orden).ToList

            lstModulos.Add(beModulo)
        Next
        lstModulos = (From obj In lstModulos Select obj Order By obj.Orden).ToList

        selected = IIf("http://" & host & "/" & appName & "/default.aspx" = url, "active", "")

        menu = menu & "<li class='" & selected & "'><a href='/" & appName & "/default.aspx'><i class='fa fa-dashboard'></i><span>Dashboard</span></a></li>"
        Dim lstModulos2 = (From items In lstModulos Where items.SubModuloId = 0).ToList
        Dim lstSubmodulo = (From items In lstModulos Where items.SubModuloId > 0).ToList
        Dim cont As Int32 = 0
        Dim moduloIds As Int32 = 0
        For Each it In modulos
            it.ColeccionFormulario = (From obj In lstFormularios Where obj.ModuloId = it.Id Select obj Order By obj.Orden).ToList
            lstModulos2.Add(it)
        Next
        For Each objModule As BEntities.Seguridad.Modulo In lstModulos2
            cont = 1
            moduloIds = objModule.Id
            selected = IIf((From obj In objModule.ColeccionFormulario Where "http://" & host & "/" & appName & obj.Url = url Select obj).ToList.Count > 0, "active", "")

            item = "<li class='treeview " & selected & "'><a href='#'><i class='fa " & objModule.Icono & "'></i><span>" & objModule.Nombre & "</span><i class='fa fa-angle-left pull-right'></i></a><ul class='treeview-menu'>"
            For Each objModule2 As BEntities.Seguridad.Modulo In lstSubmodulo
                If (objModule.Id = objModule2.SubModuloId) Then
                    cont = cont + 1
                    selected = IIf((From obj In objModule2.ColeccionFormulario Where "http://" & host & "/" & appName & obj.Url = url Select obj).ToList.Count > 0, "active", "")

                    item = item + "<li class='treeview " & selected & "'><a href='#'><i class='fa " & objModule2.Icono & "'></i><span>" & objModule2.Nombre & "</span><i class='fa fa-angle-left pull-right'></i></a><ul class='treeview-menu'>"

                    For Each objOption3 As BEntities.Seguridad.Formulario In objModule2.ColeccionFormulario
                        selected = IIf("http://" & host & "/" & appName & objOption3.Url = url, "active", "")
                        item = item & "<li class='" & selected & "'><a href='/" & appName & objOption3.Url & "'><i class='fa " & objOption3.Icono & "" & "'></i>" & objOption3.Nombre & "</a></li>"

                    Next
                    item = item & "</ul>"
                    'menu = menu + item
                End If
            Next
            For Each objOption As BEntities.Seguridad.Formulario In objModule.ColeccionFormulario
                selected = IIf("http://" & host & "/" & appName & objOption.Url = url, "active", "")
                'If (Not objOption.Url.Contains("HelpDesk")) Then
                item = item & "<li class='" & selected & "'><a href='/" & appName & objOption.Url & "'><i class='fa " & objOption.Icono & "'></i>" & objOption.Nombre & "</a></li>"

                'Else
                '    'item = item & "<li class='" & selected & "'><a href='/" & appName & objOption.NavigateUrl & "'><i class='fa " & objOption.Icon & "'></i>" & objOption.Name & "</a></li>"
                'End If
            Next
            item = item & "</ul></li>"
            'If (item.Contains("HelpDesk")) Then
            '    Dim hola As String = item & "<li class='" & selected & "'><a href='/" & appName & "'><i class='fa " & "'></i>" & "</a></li>"
            'End If
            menu = menu + item
        Next
        menu = menu & "</li>"
        sidebarMenu.InnerHtml = menu
    End Sub

    Private Function funGet_UserCode() As Int32
        Dim UserIdentity As FormsIdentity = DirectCast(Page.User.Identity, FormsIdentity)
        Dim lngResult As Int32 = 0

        If UserIdentity IsNot Nothing Then
            Dim UserTicket As FormsAuthenticationTicket = UserIdentity.Ticket
            lngResult = UserTicket.UserData
        End If

        Return lngResult
    End Function
    Private Function funGet_UnidadNegocio() As Int32
        Dim UserIdentity As FormsIdentity = DirectCast(Page.User.Identity, FormsIdentity)
        Dim lngResult As Int32 = 0

        If UserIdentity IsNot Nothing Then
            Dim UserTicket As FormsAuthenticationTicket = UserIdentity.Ticket
            lngResult = CInt(UserTicket.Name)
        End If

        Return lngResult
    End Function
    Private Function funGet_UnidadNegocioPadre() As Int32
        Dim UserIdentity As FormsIdentity = DirectCast(Page.User.Identity, FormsIdentity)
        Dim lngResult As Int32 = 0

        If UserIdentity IsNot Nothing Then
            Dim UserTicket As FormsAuthenticationTicket = UserIdentity.Ticket
            lngResult = UserTicket.Version
        End If

        Return lngResult
    End Function

    'Private Sub botoncito_Click(sender As Object, e As EventArgs) Handles botoncito.Click
    '    Response.Redirect("http:\\www.dte.com.bo")
    'End Sub
End Class