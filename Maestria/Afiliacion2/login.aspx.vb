Option Compare Text
Option Explicit On
Option Infer On
Option Strict On

Imports BE = BEntities
Imports BEC = BEntities.Seguridad
Imports BCC = BComponents.Seguridad
Imports System.Net
Imports System.Management
'Imports System.Management

Public Class login
    Inherits UtilsMethods
    Private Sub Login_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            'Dim ClientIP As String = ""
            'Dim Forwaded As String = ""
            'Dim RealIP As String = ""
            Dim strHostName As String = Dns.GetHostName()

            Dim ipEntry As IPHostEntry = Dns.GetHostEntry(strHostName)
            Dim IPcita As String = Request.UserHostName 'ipEntry.AddressList(1).ToString
            If (IPcita.Equals("::1")) Then
                If ipEntry.AddressList.Length > 0 Then
                    IPcita = ipEntry.AddressList(1).ToString
                End If
            End If
            'For Each i In ipEntry.AddressList
            '    IPcita = IPcita + " " + i.ToString
            'Next
            'ClientIP = Request.ServerVariables("REMOTE_ADDR")
            'If ClientIP <> "" Then
            '    RealIP = ClientIP
            'Else
            '    'El usuario está accediendo a través de un Proxy.
            '    Forwaded = Request.ServerVariables("HTTP_X-Forwarded-For")
            '    If Forwaded <> "" Then RealIP = Forwaded
            'End If
            ViewState("Login") = IPcita
            ViewState("Host") = strHostName
            ControlLogin()
        End If

        Me.rtbUser.Focus()
    End Sub
    'Function Informacion() As String
    '    Dim mensaje = ""
    '    ':::Obtenemos la informacion del Sistema operativo
    '    mensaje += "Nombre del SO: " + My.Computer.Info.OSFullName + "<BR/>"
    '    mensaje += "Versión del SO: " + My.Computer.Info.OSVersion + "<BR/>"

    '    Dim consultaSQLArquitectura As String = "SELECT * FROM Win32_Processor"
    '    'Dim consultaIP As New ManagementObjectSearcher("root\CIMV2", "SELECT * FROM Win32_NetworkAdapterConfiguration where IPEnabled = true")
    '    Dim objMOS As ManagementObjectSearcher

    '    Dim objMOC As Management.ManagementObjectCollection

    '    Dim objMO As Management.ManagementObject

    '    objMOS = New ManagementObjectSearcher("Select * From Win32_NetworkAdapter")

    '    objMOC = objMOS.Get

    '    'Get MAC address from the query result.

    '    For Each objMO In objMOC

    '        mensaje += "MAC ADDRESS: " + (CType(objMO("MACAddress"), String)) + "<BR/>"

    '    Next
    '    Dim objArquitectura As New ManagementObjectSearcher(consultaSQLArquitectura)
    '    Dim ArquitecturaSO As String = ""
    '    For Each info As ManagementObject In objArquitectura.Get()
    '        ArquitecturaSO = info.Properties("AddressWidth").Value.ToString()
    '    Next info
    '    mensaje += "Arquitectura del SO: " + ArquitecturaSO + " Bits" + "<BR/>"

    '    ':::Obtenemos la informacion del Equipo
    '    mensaje += "Nombrel del Equipo: " + My.Computer.Name + "<BR/>"
    '    mensaje += "Nombre del Usuario: " + Security.Principal.WindowsIdentity.GetCurrent().Name + "<BR/>"

    '    ':::Obtenemos el serial del Disco Duro
    '    Dim serialDD As New ManagementObject("Win32_PhysicalMedia='\\.\PHYSICALDRIVE0'")
    '    mensaje += "Serial Disco Duro: " + serialDD.Properties("SerialNumber").Value.ToString + "<BR/>"

    '    ':::Obtenemos el serial de la Board
    '    Dim serial As New ManagementObjectSearcher("root\CIMV2", "SELECT * FROM Win32_BaseBoard")
    '    Dim serialBoard As String = ""
    '    For Each serialB As ManagementObject In serial.Get()
    '        serialBoard = (serialB.GetPropertyValue("SerialNumber").ToString)
    '    Next
    '    mensaje += "Serial Board: " + serialBoard + "<BR/>"
    '    'Dim mac As String = ""
    '    'Dim mensaje2 As StringBuilder = New StringBuilder
    '    'For Each info As ManagementObject In consultaIP.Get()
    '    '    'mac = info.Properties("MACAddress").Value.ToString()
    '    '    For Each it In info.Properties
    '    '        If it.Name = "MACAddress" Then
    '    '            mac = "asasd"
    '    '        End If
    '    '    Next
    '    'Next
    '    'mensaje += "MAC: " + mac + "<BR/>"
    '    Return mensaje
    'End Function
    Protected Sub rbtAcces_Click(ByVal sender As Object, ByVal e As EventArgs) Handles rbtAcces.Click
        Dim BCPersonal As New BCC.Personal
        Dim BEPersonal As BEC.Personal
        Dim BCPrivilegio As New BCC.Privilegio
        Dim NewTicket As FormsAuthenticationTicket
        Dim url As String = HttpContext.Current.Request.Url.AbsoluteUri
        Dim host As String = HttpContext.Current.Request.Url.Host
        Dim virtualPath As String = Hosting.HostingEnvironment.ApplicationVirtualPath
        Dim appName As String = virtualPath.Substring(1)
        Dim encTicket As String = ""
        Me.dvMessage.Visible = True
        Me.lblError.Text = "" ' Informacion()

        Dim _security As New SecurityInformationLapTopLicenJuan()
        'If ValidateSecurityInformation(_security.So, _security.Mac, _security.HardDiskSerial, _security.ArchitectSo) = True Then
        'BEPersonal = BCPersonal.ValidateUser(rtbUser.Text, rtbPassword.Text, BEntities.States.Activo)
        BEPersonal = BCPersonal.ValidarPersonal(rtbUser.Text, rtbPassword.Text, BEntities.Estado.Activo, ViewState("Login").ToString, ViewState("Host").ToString)
        Dim PersonalLogin = BCPersonal.BuscarLogin(rtbUser.Text)
        'BEPersonal = BCPersonal.BuscarPorId(1)
        If (BEPersonal IsNot Nothing) Then
            If BEPersonal.FechaBloqueo IsNot Nothing Then
                If BEPersonal.FechaBloqueo > Now Then
                    Me.dvMessage.Visible = True
                    Me.lblError.Text = "Usuario bloqueado, intente nuevamente a las " & MyBase.GetHoraBoliviana(CDate(BEPersonal.FechaBloqueo)).ToString("yyyy-MM-dd HH:mm:ss")
                    Return
                End If
            End If
            Dim bandera As Boolean = False
            With PersonalLogin
                .StatusType = BE.StatusType.Update
                .FechaBloqueo = Nothing
                '.FechaActualizacion = Now
                .IntentosFallidos = 0
            End With
            BCPersonal.Guardar4(PersonalLogin)

            NewTicket = New FormsAuthenticationTicket(BEPersonal.UNSucursal.UnidadNegocioId, BEPersonal.UnidadNegocioId.ToString, Now, Now.AddMinutes(40), False, Convert.ToString(BEPersonal.Id), FormsAuthentication.FormsCookiePath)
            encTicket = FormsAuthentication.Encrypt(NewTicket)
            'SaveUserLogin(BEPersonal, rtbUser.Text, getSHA1Hash(rtbPassword.Text), NewTicket, BE.loginStatus.Login)
            Response.Cookies.Clear()
            Response.Cookies.Add(New HttpCookie(FormsAuthentication.FormsCookieName, encTicket))
            If PersonalLogin.MontoComision > 0 And PersonalLogin.FechaActualizacion > Now.AddMonths(-1) Then
                bandera = True
            End If
            If bandera Then
                Response.Redirect(FormsAuthentication.GetRedirectUrl(BEPersonal.Login, False))
            Else
                Response.Redirect("/" & appName & "/Modulos/Seguridad/Cuenta.aspx")
            End If
        Else
            'SaveUserLogin(Nothing, rtbUser.Text, getSHA1Hash(rtbPassword.Text), Nothing, BE.loginStatus.Loginfailed)
            Me.dvMessage.Visible = True
            Me.lblError.Text = "Usuario o contraseña incorrectos"
            If PersonalLogin IsNot Nothing Then
                Dim bcParametroLogin As New BCC.ParametroLogin
                Dim beParametroLogin = bcParametroLogin.Listar.FirstOrDefault
                With PersonalLogin
                    .StatusType = BE.StatusType.Update
                    If beParametroLogin.CantidadIntentosFallidos <= .IntentosFallidos + 1 Then
                        .FechaBloqueo = Now.AddSeconds(beParametroLogin.TiempoBloqueo)
                    End If
                    '.FechaActualizacion = Now
                    .IntentosFallidos = .IntentosFallidos + 1
                End With
                BCPersonal.Guardar4(PersonalLogin)
            End If
        End If
        'Else
        '    Me.dvMessage.Visible = True
        '    Me.lblError.Text = "DATOS INCORRECTOS "
        'End If
    End Sub

    Private Function ValidateSecurityInformation(ByVal _So As String, ByVal _Mac As String, ByVal _HardDiskSerial As String, ByVal _ArchitectSo As String) As Boolean
        Dim mensaje = ""
        Dim So As String = My.Computer.Info.OSFullName
        If _So IsNot String.Empty Then
            If _So <> So.Trim() Then
                mensaje += "S.O incorrecto"
            End If
        End If
        Dim consultaSQLArquitectura As String = "SELECT * FROM Win32_Processor"
        Dim objMOS As ManagementObjectSearcher
        Dim objMOC As Management.ManagementObjectCollection
        Dim objMO As Management.ManagementObject
        objMOS = New ManagementObjectSearcher("Select * From Win32_NetworkAdapter")
        objMOC = objMOS.Get
        Dim OkMac As Boolean = False
        If _Mac IsNot String.Empty Then
            For Each objMO In objMOC
                If CType(objMO("MACAddress"), String) IsNot Nothing Then
                    If (CType(objMO("MACAddress"), String).Trim.Length > 0) Then
                        Dim Mac As String = (CType(objMO("MACAddress"), String))
                        If _Mac = Mac.Trim() Then
                            OkMac = True
                        End If
                    End If
                End If
            Next
            If OkMac = False Then
                mensaje += "MAC ADDRESS: incorrecto"
            End If
        End If

        Dim objArquitectura As New ManagementObjectSearcher(consultaSQLArquitectura)
        Dim ArquitecturaSO As String = ""
        For Each info As ManagementObject In objArquitectura.Get()
            ArquitecturaSO = info.Properties("AddressWidth").Value.ToString()
        Next info

        If _ArchitectSo IsNot String.Empty Then
            If _ArchitectSo <> ArquitecturaSO Then
                mensaje += "Arquitectura S.O incorrecta"
            End If
        End If

        Dim serialDD As New ManagementObject("Win32_PhysicalMedia='\\.\PHYSICALDRIVE0'")
        If _HardDiskSerial IsNot String.Empty Then
            If _HardDiskSerial <> serialDD.Properties("SerialNumber").Value.ToString().Trim Then
                mensaje += "Serial Disco Duro incorrecto"
            End If
        End If
        Return If(mensaje.Length = 0, True, False)
    End Function
    Private Class SecurityInformationLapTopLicenJuan
        Public Property So As String = ""
        Public Property Mac As String = String.Empty
        Public Property HardDiskSerial As String = "WD-WXQ1AB7R60A3"
        Public Property ArchitectSo As String = "64"

    End Class
    Private Sub ControlLogin()
        Dim BCUser As New BCC.Personal

        Dim BEUser As BEC.Personal
        If HttpContext.Current.User.Identity.IsAuthenticated Then
            Dim UserIdentity As FormsIdentity = DirectCast(HttpContext.Current.User.Identity, FormsIdentity)
            If UserIdentity IsNot Nothing Then
                BEUser = BCUser.Buscar(CInt(UserIdentity.Ticket.UserData))
                'SaveUserLogin(BEUser, BEUser.Login, BEUser.Password, UserIdentity.Ticket, BE.loginStatus.Logout)
                Session.Abandon()
                FormsAuthentication.SignOut()
            End If
        End If

    End Sub
    'Private Sub SaveUserLogin(ByVal BEUser As BEC.User, ByVal UserName As String, ByVal UserPassword As String, ByVal Ticket As FormsAuthenticationTicket, ByVal LoginStatus As BE.loginStatus)
    '    Dim BCClassifiers As New BCB.Classifiers
    '    Dim BCUserLoginBtc As New BCBI.UserLoginBtc
    '    Dim BEClassifiers As New BEB.Classifiers
    '    Dim BEUserLoginBtc As New BEBI.UserLoginBtc

    '    Dim lstClassifiers As List(Of BEB.Classifiers) = BCClassifiers.List(BE.ClassifierType.LoginStatus)

    '    BEClassifiers = (From Obj In lstClassifiers Where Obj.Id = LoginStatus).FirstOrDefault

    '    With BEUserLoginBtc
    '        If BEUser IsNot Nothing Then
    '            .UserId = BEUser.Id
    '        End If
    '        .UserName = UserName.Trim
    '        .UserPassword = UserPassword.Trim
    '        .UserIpAddress = Me.GetIPPublicAddress
    '        .StateId = LoginStatus
    '        .Description = BEClassifiers.Description
    '        .Registry = Now
    '        If Ticket IsNot Nothing Then
    '            .ExpirationTime = Ticket.Expiration
    '        End If
    '        .StatusType = BE.StatusType.Insert
    '    End With

    '    BCUserLoginBtc.Save(BEUserLoginBtc)

    'End Sub
    Private Function getSHA1Hash(ByVal strToHash As String) As String

        Dim sha1Obj As New System.Security.Cryptography.SHA1CryptoServiceProvider
        Dim bytesToHash() As Byte = System.Text.Encoding.ASCII.GetBytes(strToHash)

        bytesToHash = sha1Obj.ComputeHash(bytesToHash)

        Dim strResult As String = ""

        For Each b As Byte In bytesToHash
            strResult += b.ToString("x2")
        Next

        Return strResult

    End Function

    Private Function GetIpServerClient() As String
        Dim HostName As String = System.Net.Dns.GetHostName
        Dim IpClient As System.Net.IPHostEntry = System.Net.Dns.GetHostEntry(HostName)
        Dim Address As System.Net.IPAddress() = IpClient.AddressList
        Return Address(Address.Length - 1).ToString()
    End Function

    Private Function GetIPPublicAddress() As String
        Dim VisitorsIPAddr As String = String.Empty
        Dim HTTPContext As HttpContext = HttpContext.Current
        If HTTPContext.Request.ServerVariables("HTTP_X_FORWARDED_FOR") IsNot Nothing Then
            VisitorsIPAddr = HttpContext.Current.Request.ServerVariables("HTTP_X_FORWARDED_FOR").ToString()
        ElseIf HttpContext.Current.Request.UserHostAddress.Length.ToString() IsNot "0" Then
            VisitorsIPAddr = HttpContext.Current.Request.UserHostAddress
        End If
        Return VisitorsIPAddr
    End Function

End Class