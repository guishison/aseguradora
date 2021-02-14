Imports System.Net
Imports BCBT = BComponents.Bitacora
Imports BCS = BComponents.Seguridad
Imports BEBT = BEntities.Bitacora
Imports BE = BEntities
Imports BES = BEntities.Seguridad
Public Class Cuenta
    Inherits UtilsMethods
    Private bcBitacoraErrores As New BCBT.BitacoraError
    Private beBitacoraErrores As New BEBT.BitacoraError
    Private bcParametroLogin As New BCS.ParametroLogin
    Private beParametroLogin As New BES.ParametroLogin
    Protected Overloads Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
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
    Private Sub proInit_Form()
        Try
            Dim bc = New BComponents.Seguridad.Personal()
            Dim beUser = bc.Buscar(funGet_UserCode())
            rtbName.Text = beUser.Nombre
            rtbEmail.Text = beUser.Correo
            rtbLogin.Text = beUser.Login
            'If beParametroLogin IsNot Nothing Then
            '    rtbNewPassword.MaxLength = beParametroLogin.LongitudMaxima
            'End If
        Catch ex As Exception
            ControlarError(MyBase.ObtenerMetodo, If(ex.InnerException IsNot Nothing, ex.InnerException.ToString, ex.Message))
        End Try
    End Sub

    Protected Sub rtbSave_Click(sender As Object, e As EventArgs) Handles rtbSave.Click
        Try
            Dim bc = New BComponents.Seguridad.Personal()
            Dim beUser = bc.Buscar(funGet_UserCode())
            Dim flag = True
            beUser.Nombre = rtbName.Text
            beUser.Correo = rtbEmail.Text
            beUser.Login = rtbLogin.Text
            beUser.FechaActualizacion = Now
            beUser.StatusType = BEntities.StatusType.Update
            beUser.MontoComision = 5
            If (rtbNewPassword.Text.Length > 0) Then
                Dim BEUser2 = bc.ValidarPersonal(rtbLogin.Text, rtbPassword.Text, BEntities.Estado.Activo, ViewState("Login").ToString, ViewState("Host").ToString)
                If (BEUser2 Is Nothing) Then
                    'If (Not rtbPassword.Text.Equals(beUser.Password)) Then
                    flag = False
                    ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "funShow_Message('La clave actual no coincide con la registrada en el sistema, intente nuevamente!!!', 'INFORMATION');", True)
                Else
                    If (rtbNewPassword.Text.Equals(rtbConfirmNewPassword.Text)) Then
                        beUser.Password = rtbNewPassword.Text
                        beUser.PersonalId = MyBase.funGet_UserCode
                    Else
                        flag = False
                    End If
                End If
            End If
            If (rtbPassword.Text.Equals(rtbNewPassword.Text)) Then
                flag = False
                ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "funShow_Message('La nueva clave no debe ser la misma que la anterior, intente nuevamente!!!', 'INFORMATION');", True)
            End If

            If (flag) Then
                beParametroLogin = bcParametroLogin.Listar().FirstOrDefault

                Dim bcPersonalPassword = New BCS.PersonalPassword
                Dim ListPersonalPassword As List(Of BES.PersonalPassword) = New List(Of BES.PersonalPassword)
                Dim resultado = ""

                If rtbNewPassword.Text.Contains(beUser.Login) Then
                    resultado = resultado & "- El password no debe contener el Login del usuario.<br/>"
                End If
                ListPersonalPassword = bcPersonalPassword.VerificarPasswordAnteriores(rtbNewPassword.Text, CInt(MyBase.funGet_UserCode), beParametroLogin.ComprobacionesPasswordAnteriores)
                If ListPersonalPassword.Count > 0 Then
                    resultado = resultado & "- No puede ingresar un password que haya utilizado anteriormente. La cantidad de Password guardados es de " & beParametroLogin.ComprobacionesPasswordAnteriores & ".<br/>"
                End If
                If beParametroLogin IsNot Nothing Then
                    If rtbNewPassword.Text.Length > beParametroLogin.LongitudMaxima Then
                        resultado = resultado & "- El password no debe exeder los " & beParametroLogin.LongitudMaxima & " caracteres.<br/>"
                    End If
                    If rtbNewPassword.Text.Length < beParametroLogin.LongitudMinima Then
                        resultado = resultado & "- El password no debe tener menos de " & beParametroLogin.LongitudMinima & " caracteres.<br/>"
                    End If
                    Dim regexBasico As Regex = New Regex("(?=.*[A-Z])")
                    Dim regexMedio As Regex = New Regex("(?=.*[A-Z])(?=.*[a-z])")
                    Dim regexFuerte As Regex = New Regex("(?=.*[0-9])(?=.*[A-Z])(?=.*[a-z])")
                    Dim regexMuyFuerte As Regex = New Regex("(?=.*[0-9])(?=.*\W)(?=.*[A-Z])(?=.*[a-z])")
                    '@#%&-+()/*"':;!?~|{}[]_
                    If beParametroLogin.NivelPassword = BE.NivelesPassword.BASICO Then
                        If Not regexBasico.IsMatch(rtbNewPassword.Text) Then
                            resultado = resultado & "- - El password debe contener al menos 1 caracter mayúscula..<br/>"
                        End If
                    ElseIf beParametroLogin.NivelPassword = BE.NivelesPassword.MEDIO Then
                        If Not regexMedio.IsMatch(rtbNewPassword.Text) Then
                            resultado = resultado & "- El password debe contener al menos 1 caracter mayúscula, 1 caracter minúscula.<br/>"
                        End If
                    ElseIf beParametroLogin.NivelPassword = BE.NivelesPassword.FUERTE Then
                        If Not regexFuerte.IsMatch(rtbNewPassword.Text) Then
                            resultado = resultado & "- El password debe contener al menos 1 caracter mayúscula, 1 caracter minúscula, 1 numero.<br/>"
                        End If
                    ElseIf beParametroLogin.NivelPassword = BE.NivelesPassword.MUYFUERTE Then
                        If Not regexMuyFuerte.IsMatch(rtbNewPassword.Text) Then
                            resultado = resultado & "- El password debe contener al menos 1 caracter mayúscula, 1 caracter minúscula, 1 numero y 1 caracter especial.<br/>"
                        End If
                    End If
                    If resultado.Length > 0 Then
                        ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "funShow_Message('Revise lo siguiente:<br/> " & resultado & "', 'INFORMATION');", True)
                    Else
                        bc.Guardar3(beUser)
                        ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "funShow_Message('Cambios realizados con éxito', 'INFORMATION');", True)
                    End If
                End If
            End If
            rtbPassword.Text = Nothing
            rtbNewPassword.Text = Nothing
            rtbConfirmNewPassword.Text = Nothing
        Catch ex As Exception
            ControlarError(MyBase.ObtenerMetodo, If(ex.InnerException IsNot Nothing, ex.InnerException.ToString, ex.Message))
        End Try
    End Sub

End Class