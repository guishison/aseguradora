Imports BEB = BEntities.Base
Imports BCB = BComponents.Base
Imports BCS = BComponents.Seguridad
Imports BE = BEntities
Imports Telerik.Web.UI
Imports BCCO = BComponents.Cobranzas
Imports BECO = BEntities.Cobranzas
Imports System.Web.Services
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Web.Script.Services
Imports AjaxControlToolkit
Imports BEH = BEntities.AyudaUsuario
Imports BCH = BComponents.AyudaUsuario
Imports BCBT = BComponents.Bitacora
Imports BEBT = BEntities.Bitacora
Imports System.Net.Mail

Public Class HelpDesk
    Inherits UtilsMethods

    'Public Shared imagen As Byte() = Nothing
    Private chau As String = ""
    Private bcModulo As New BCS.Modulo
    Private bcFormulario As New BCS.Formulario
    Private bcClasificadores As New BCB.Clasificadores
    Private bcHelpDeskDetalle As New BCH.HelpDeskDetalle
    Private bcHelpDesk As New BCH.HelpDesk
    'Dim bcCarta As New BCCO.Carta
    Private bcBitacoraErrores As New BCBT.BitacoraError
    Private beBitacoraErrores As New BEBT.BitacoraError
    'Private Shared Property BePrivilegios As BEntities.Seguridad.Privilegio
    Dim BcPrivilegios As New BComponents.Seguridad.Privilegio
    Private beHelpDeskDetalle As BEH.HelpDeskDetalle = New BEH.HelpDeskDetalle
    Private beHelpDesk As BEH.HelpDesk = New BEH.HelpDesk
    Private bcPersonal As BCS.Personal = New BCS.Personal
    'Public Shared Property listaHelpDeskDetalle As List(Of BEH.HelpDeskDetalle) = New List(Of BEH.HelpDeskDetalle)
    'Public Shared Property NuevaListaCargadaAdjuntos As List(Of BEH.HelpDeskDetalle) = New List(Of BEH.HelpDeskDetalle)
    'Public Shared Property MensajeError As String = ""

    Protected Overloads Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            Session("imagen") = New Byte
            Session("imagen") = Nothing
            Session("listaHelpDeskDetalle") = New List(Of BEH.HelpDeskDetalle)
            Session("NuevaListaCargadaAdjuntos") = New List(Of BEH.HelpDeskDetalle)
            Session("MensajeError") = ""
            Session("BePrivilegios") = New BEntities.Seguridad.Privilegio
            Session("BePrivilegios") = BcPrivilegios.BuscarPrivilegioPorPersonal(funGet_UserCode(), 3078, funGet_UnidadNegocio())
            ''Dim sb As StringBuilder = New StringBuilder
            ''sb.Append("html2canvas(document.body, {")
            ''sb.AppendLine()
            ''sb.Append("onrendered(canvas) {")
            ''sb.AppendLine()
            ''sb.Append("var link = document.getElementById('Img1');")
            ''sb.AppendLine()
            ''sb.Append("var image = canvas.toDataURL();")
            ''sb.AppendLine()
            ''sb.Append("link.src = Image;")
            ''sb.AppendLine()
            ''sb.Append("}")
            ''sb.AppendLine()
            ''sb.Append("});")
            ''ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "probando", sb.ToString, True)
            'Dim virtualPath As String = Hosting.HostingEnvironment.ApplicationVirtualPath
            'Dim appName As String = virtualPath.Substring(1)
            'chau = HttpContext.Current.Request.QueryString("ModuloId")
            'Dim url As String = "localhost/" + appName + "/Modulos/Caja/Caja.aspx"
            'Dim Imagencita = DirectCast(Me.Master.FindControl("Img1"), HtmlImage)
            ''Dim ImagencitaByte As Byte() = System.Convert.FromBase64String(Imagencita.Src)
            ''SuperImagen2.DataValue = ImagencitaByte
            'If (chau IsNot Nothing) Then
            '    'Dim navegador As New WebBrowser()
            '    'navegador.Size = New Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height)
            '    '    navegador.ScrollBarsEnabled = False
            '    '    navegador.Navigate(url)
            '    '    Dim tamano As Rectangle = New Rectangle(0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height)
            '    '    Dim bitMap As Bitmap = New Bitmap(tamano.Width, tamano.Height)
            '    '    navegador.DrawToBitmap(bitMap, tamano)

            '    '    Dim thumbnail As Bitmap = New Bitmap(tamano.Width, tamano.Height)

            '    '    Dim gfx As Graphics = Graphics.FromImage(thumbnail)
            '    '    gfx.DrawImage(bitMap, tamano, tamano, GraphicsUnit.Pixel)
            '    'Using stream As New System.IO.MemoryStream
            '    '    thumbnail.Save(stream, thumbnail.RawFormat)
            '    '    imagen = stream.ToArray
            '    'End Using

            '    'Dim hola = imagen
            '    'SuperImagen.DataValue = imagen
            'End If

            rdpInicial.SelectedDate = Now
            rdpFinal.SelectedDate = Now
            llenarCombos()
            proLoadGrid()
            rcbEstadoBusqueda.Filter = RadComboBoxFilter.Contains
            rcbEstadoHelpDesk.Filter = RadComboBoxFilter.Contains
            rcbFormulario.Filter = RadComboBoxFilter.Contains
            rcbModulo.Filter = RadComboBoxFilter.Contains
            rcbPrioridad.Filter = RadComboBoxFilter.Contains
            rcbTipoTicket.Filter = RadComboBoxFilter.Contains
            Dim hola As String = HttpContext.Current.Request.QueryString("ModuloId")
            'Dim beModulos = bcModulo.BuscarPorNombre(hola)
            Dim formularios = bcFormulario.ListByUrlContains(String.Concat("/", hola))
            If (formularios.Count > 0) Then
                rcbModulo.SelectedValue = formularios.ElementAt(0).ModuloId
                Dim eventito As New RadComboBoxSelectedIndexChangedEventArgs(hola, hola, formularios.ElementAt(0).ModuloId, formularios.ElementAt(0).ModuloId)
                Call rcbModulo_SelectedIndexChanged(Nothing, eventito)
            End If
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
    'Public Function GetScreenCapture() As Image
    '    Dim objTecla As SendKeys ' variable que guarda la pulsación de la tecla
    '    ' envía la pulsación de la tecla especificada (Impr Pant)
    '    objTecla.SendWait("{PRTSC 2}") ' 2 pulsaciones de Impr Pant para capturar la pantalla entera
    '    ' variable de tipo IDataObject que contiene el portapapeles
    '    Dim objClipboard As IDataObject = Clipboard.GetDataObject() ' método de IDataObject
    '    ' devolver el portapapeles como mapa de bits
    '    Return objClipboard.GetData(DataFormats.Bitmap)
    'End Function
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function CargarHelpDesk(Imagencita As Byte()) As Boolean
        Dim aquiestalaimagen = Imagencita
        Session("imagen") = Imagencita
        Dim imagenes As Image = Nothing
        Dim FSize As Size = Screen.PrimaryScreen.Bounds.Size
        Dim bmpScreenshot As New Bitmap(FSize.Width, FSize.Height, PixelFormat.Format32bppArgb)
        Dim gfxScreenshot As Graphics = Graphics.FromImage(bmpScreenshot)
        'gfxScreenshot.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, FSize, CopyPixelOperation.SourceCopy)
        gfxScreenshot.DrawImage(imagenes, 0, 0, 0, 0)
        'bmpScreenshot.Save(FileName, ImageFormat.Png)
        Dim bitmapBytes As Byte()
        Dim stream2 As New MemoryStream
        Dim aquinuevo As New Bitmap(FSize.Width, FSize.Height, PixelFormat.Format32bppArgb)
        aquinuevo.Save(stream2, aquinuevo.RawFormat)
        Using stream As New MemoryStream
            bmpScreenshot.Save(stream, bmpScreenshot.RawFormat)
            bitmapBytes = stream.ToArray
        End Using
        Dim virtualPath As String = Hosting.HostingEnvironment.ApplicationVirtualPath
        Dim appName As String = virtualPath.Substring(1)
        Session("imagen") = bitmapBytes

        'SuperImagen.Src = Image.FromStream(stream2, True)
        Return True
        'Response.BufferOutput = True
        'Response.Redirect("/" + appName + "/HelpDesk.aspx?ModuloId=")
    End Function
    Protected Sub rgvGrid_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles rgvGrid.ItemCommand

        Try
            If (e.CommandName <> "Page") Then

                Dim item2 As GridDataItem = e.Item
            Dim beHelpDeskValidacion = bcHelpDesk.Search(CInt(item2.GetDataKeyValue("Id").ToString()))
                If e.CommandName = "EditPosition" Then
                    If CType(Session("BePrivilegios"), BEntities.Seguridad.Privilegio).Permiso.Contains("M") Or beHelpDeskValidacion.PersonalSolicitanteId = MyBase.funGet_UserCode() Then
                        Dim item As GridDataItem = e.Item
                        Dim HelpDeskId As String = item.GetDataKeyValue("Id").ToString()
                        Session("HelpDeskId") = HelpDeskId
                        rcbEstadoHelpDesk.Enabled = True
                        CargarDatos(CLng(HelpDeskId))
                        Activar(False)
                        If (beHelpDeskValidacion.PersonalSolicitanteId = MyBase.funGet_UserCode() And rcbEstadoHelpDesk.SelectedValue = BE.EstadoHelpDesk.Abierto) Then
                            rcbEstadoHelpDesk.Enabled = True
                        End If
                    Else
                        ScriptManager.RegisterClientScriptBlock(Page,
                                                                Me.GetType,
                                                                "cerrarpopUp",
                                                                "closeDialog('divDetail'); funShow_Message('" & Session("BePrivilegios").NoUpdate & "', 'ERROR');",
                                                                True)
                    End If

                ElseIf e.CommandName = "DeletePosition" Then
                    If Session("BePrivilegios").Permiso.Contains("B") Then
                        Dim item As GridDataItem = e.Item
                        Dim IdUser As String = item.GetDataKeyValue("Id").ToString()
                        'DeletePosition(CLng(IdUser))
                        proLoadGrid()
                    Else
                        ScriptManager.RegisterClientScriptBlock(Page,
                                                                Me.GetType,
                                                                "cerrarpopUp",
                                                                "closeDialog('divDetail'); funShow_Message('" & Session("BePrivilegios").NoDelete & "', 'ERROR');",
                                                                True)
                    End If

                End If
            End If
        Catch ex As Exception
            ControlarError(MyBase.ObtenerMetodo, If(ex.InnerException IsNot Nothing, ex.InnerException.ToString, ex.Message))
        End Try
    End Sub

    Private Sub CargarDatos(ByVal HelpDeskId As Long)
        Dim beHelpDesk = bcHelpDesk.Search(HelpDeskId)
        With beHelpDesk
            rtbComentario.Text = .ProblemaDescripcion
            rcbPrioridad.SelectedValue = .PrioridadIdc
            rcbModulo.SelectedValue = .ModuloId
            Dim eventito As New RadComboBoxSelectedIndexChangedEventArgs("", "", .ModuloId, .ModuloId)
            Call rcbModulo_SelectedIndexChanged(Nothing, eventito)
            rcbFormulario.SelectedValue = .FormularioId
            rcbTipoTicket.SelectedValue = .TipoTicketIdc
            rcbEstadoHelpDesk.SelectedValue = .EstadoHelpDeskIdc
        End With
        Session("listaHelpDeskDetalle") = bcHelpDeskDetalle.ListByHelpDeskId(HelpDeskId)
        rgvAdjuntos.MasterTableView.VirtualItemCount = CType(Session("listaHelpDeskDetalle"), List(Of BEH.HelpDeskDetalle)).Count
        MyBase.proLoad_RadGrid(rgvAdjuntos, CType(Session("listaHelpDeskDetalle"), List(Of BEH.HelpDeskDetalle)))
    End Sub
    Private Sub EnviarCorreo(ByVal Mensaje As String)

        Dim SmtpServer As New SmtpClient()
        Dim mail As New MailMessage()
        SmtpServer.UseDefaultCredentials = False
        SmtpServer.Credentials = New Net.NetworkCredential("enjoyvacationcobranzas@gmail.com", "enjoy123")
        SmtpServer.Port = 587
        SmtpServer.Host = "smtp.gmail.com"
        'SmtpServer.EnableSsl = True
        'SmtpServer.ServicePoint.MaxIdleTime = 1
        SmtpServer.EnableSsl = True

        mail = New MailMessage()
        mail.From = New MailAddress("enjoyvacationcobranzas@gmail.com")


        mail.To.Add("lgarcia@dte.com.bo")



        Dim CorreoSujeto As String
        Dim CorreoBody As String

        Dim auxContador As Int32 = bcHelpDesk.Listita().Count
        CorreoSujeto = String.Concat("Nuevo Help Desk ", (auxContador + 1))

        Dim MensajeCompleto As StringBuilder = New StringBuilder
        MensajeCompleto.Append("<br/>Enviado por: ")
        MensajeCompleto.Append(bcPersonal.BuscarPorId(funGet_UserCode()).Nombre)
        MensajeCompleto.AppendLine()
        MensajeCompleto.Append("<br/>Modulo: ")
        MensajeCompleto.Append(rcbModulo.Text)
        MensajeCompleto.Append("<br/>Formulario: ")
        MensajeCompleto.Append(rcbFormulario.Text)
        MensajeCompleto.Append("<br/>Tipo de ticket: ")
        MensajeCompleto.Append(rcbTipoTicket.Text)
        MensajeCompleto.Append("<br/>Prioridad: ")
        MensajeCompleto.Append(rcbPrioridad.Text)
        MensajeCompleto.Append("<br/>Estado: ")
        MensajeCompleto.Append(rcbEstadoHelpDesk.Text)
        MensajeCompleto.AppendLine()
        MensajeCompleto.Append("<br/>Contenido del mensaje:")
        MensajeCompleto.AppendLine()
        MensajeCompleto.Append("<br/>" + Mensaje)
        MensajeCompleto.AppendLine()
        MensajeCompleto.Append("<br/>Hora de envio :" + Now.ToString("dd/MM/yyyy HH:mm:ss"))
        MensajeCompleto.AppendLine()
        MensajeCompleto.Append("<br/>Contestar lo antes posible.<br/>")
        CorreoBody = String.Concat(MensajeCompleto.ToString)




        mail.Subject = CorreoSujeto
        For Each item In CType(Session("listaHelpDeskDetalle"), List(Of BEH.HelpDeskDetalle))
            Dim streams As Stream = New MemoryStream(item.Archivo)
            Dim adjuntos As Attachment = New Attachment(streams, item.NombreArchivo)
            mail.Attachments.Add(adjuntos)
        Next
        'Dim stream As Stream = New MemoryStream(CType(Session("listaHelpDeskDetalle"), List(Of BEH.HelpDeskDetalle)).ElementAt(0).Archivo)
        'Dim adjuntos As Attachment = New Attachment(stream, "Imagen Adjunta")
        'mail.Attachments.Add(adjuntos)

        mail.Body = CorreoBody

        mail.IsBodyHtml = True

        Try
            SmtpServer.Send(mail)

        Catch ex As Exception
            ControlarError(MyBase.ObtenerMetodo, If(ex.InnerException IsNot Nothing, ex.InnerException.ToString, ex.Message))
            'proShow_Message(ex.ToString)
            'ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "closeDialog('divDetail'); funShow_Message('Error al momento de Enviar Correo', 'INFORMATION');", True)

        End Try
    End Sub

    Private Sub EnviarCorreoRespuesta(ByVal HelpDesk As BEH.HelpDesk)

        Dim SmtpServer As New SmtpClient()
        Dim mail As New MailMessage()
        SmtpServer.UseDefaultCredentials = False
        SmtpServer.Credentials = New Net.NetworkCredential("enjoyvacationcobranzas@gmail.com", "enjoy123")
        SmtpServer.Port = 587
        SmtpServer.Host = "smtp.gmail.com"
        'SmtpServer.EnableSsl = True
        'SmtpServer.ServicePoint.MaxIdleTime = 1
        SmtpServer.EnableSsl = True

        mail = New MailMessage()
        mail.From = New MailAddress("enjoyvacationcobranzas@gmail.com")


        mail.To.Add("lgarcia@dte.com.bo")



        Dim CorreoSujeto As String
        Dim CorreoBody As String

        Dim auxContador As Int32 = HelpDesk.Id
        CorreoSujeto = String.Concat("Nueva Respuesta del Help Desk  ", (auxContador))

        Dim MensajeCompleto As StringBuilder = New StringBuilder
        MensajeCompleto.Append("<br/>Enviado por: ")
        MensajeCompleto.Append(bcPersonal.BuscarPorId(funGet_UserCode()).Nombre)
        MensajeCompleto.AppendLine()
        MensajeCompleto.Append("<br/>Modulo: ")
        MensajeCompleto.Append(rcbModulo.Text)
        MensajeCompleto.Append("<br/>Formulario: ")
        MensajeCompleto.Append(rcbFormulario.Text)
        MensajeCompleto.Append("<br/>Tipo de ticket: ")
        MensajeCompleto.Append(rcbTipoTicket.Text)
        MensajeCompleto.Append("<br/>Prioridad: ")
        MensajeCompleto.Append(rcbPrioridad.Text)
        MensajeCompleto.Append("<br/>Estado: ")
        MensajeCompleto.Append(rcbEstadoHelpDesk.Text)
        MensajeCompleto.AppendLine()
        MensajeCompleto.Append("<br/>Contenido del mensaje:")
        MensajeCompleto.AppendLine()
        MensajeCompleto.Append("<br/>" + CType(Session("listaHelpDeskDetalle"), List(Of BEH.HelpDeskDetalle)).LastOrDefault().Descripcion)
        MensajeCompleto.AppendLine()
        MensajeCompleto.Append("<br/>Hora de Envio :" + Now.ToString("dd/MM/yyyy HH:mm:ss"))
        MensajeCompleto.AppendLine()
        MensajeCompleto.Append("<br/>Contestar lo antes posible.<br/>")
        CorreoBody = String.Concat(MensajeCompleto.ToString)




        mail.Subject = CorreoSujeto
        'For Each item In CType(Session("listaHelpDeskDetalle"), List(Of BEH.HelpDeskDetalle))
        '    Dim streams As Stream = New MemoryStream(item.Archivo)
        '    Dim adjuntos As Attachment = New Attachment(streams, item.NombreArchivo)
        '    mail.Attachments.Add(adjuntos)
        'Next
        'Dim stream As Stream = New MemoryStream(CType(Session("listaHelpDeskDetalle"), List(Of BEH.HelpDeskDetalle)).ElementAt(0).Archivo)
        'Dim adjuntos As Attachment = New Attachment(stream, "Imagen Adjunta")
        'mail.Attachments.Add(adjuntos)

        mail.Body = CorreoBody

        mail.IsBodyHtml = True

        Try
            SmtpServer.Send(mail)

        Catch ex As Exception
            ControlarError(MyBase.ObtenerMetodo, If(ex.InnerException IsNot Nothing, ex.InnerException.ToString, ex.Message))
            'proShow_Message(ex.ToString)
            'ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "closeDialog('divDetail'); funShow_Message('Error al momento de Enviar Correo', 'INFORMATION');", True)

        End Try
    End Sub
    Protected Sub rgvGrid_PageIndexChanged(sender As Object, e As GridPageChangedEventArgs) Handles rgvGrid.PageIndexChanged

        sender.CurrentPageIndex = e.NewPageIndex
        Dim listaHelpDesk As List(Of BEH.HelpDesk) = bcHelpDesk.Listas(rtbFilter.Text, rdpInicial.SelectedDate, rdpFinal.SelectedDate, rcbEstadoBusqueda.SelectedValue,
                                                                       rgvGrid.MasterTableView.CurrentPageIndex, rgvGrid.MasterTableView.PageSize, BEH.relHelpDesk.EstadoHelpDesk,
                                                                       BEH.relHelpDesk.Formulario, BEH.relHelpDesk.HelpDeskDetalle, BEH.relHelpDesk.Modulo, BEH.relHelpDesk.Personal,
                                                                       BEH.relHelpDesk.Prioridad, BEH.relHelpDesk.TipoTicket)
        rgvGrid.VirtualItemCount = bcHelpDesk.ListasCount(rtbFilter.Text, rdpInicial.SelectedDate, rdpFinal.SelectedDate, rcbEstadoBusqueda.SelectedValue).Count
        MyBase.proLoad_RadGrid(rgvGrid, listaHelpDesk)
    End Sub

    Protected Sub rbAdd_Click(sender As Object, e As EventArgs) Handles rbAdd.Click
        Try
            Activar(True)
            If Session("BePrivilegios").Permiso.Contains("A") Then
                ClearFields()
                Session("HelpDeskId") = 0
                Dim nuevalista = New List(Of BEH.HelpDeskDetalle)
                Session("listaHelpDeskDetalle") = nuevalista
                rgvAdjuntos.MasterTableView.VirtualItemCount = 0
                MyBase.proLoad_RadGrid(rgvAdjuntos, nuevalista)
                'LoadPosition(0)
            Else
                ScriptManager.RegisterClientScriptBlock(Page,
                                                            Me.GetType,
                                                            "cerrarpopUp",
                                                            "closeDialog('divDetail'); funShow_Message('" & CType(Session("BePrivilegios"), BEntities.Seguridad.Privilegio).NoInsert & "', 'ERROR');",
                                                            True)
            End If
        Catch ex As Exception
            ControlarError(MyBase.ObtenerMetodo, If(ex.InnerException IsNot Nothing, ex.InnerException.ToString, ex.Message))
        End Try
    End Sub

    'Protected Sub rtbSave_Click(sender As Object, e As EventArgs) Handles rbGuardarAdjunto.Click
    '    'SavePosition()
    '    proLoadGrid()
    '    ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "closeDialog('divDetail'); funShow_Message('Cargo Guardado', 'INFORMATION');", True)
    'End Sub

    Private Sub proLoadGrid()
        Try
            Dim listaHelpDesk As List(Of BEH.HelpDesk) = bcHelpDesk.Listas(rtbFilter.Text, rdpInicial.SelectedDate, rdpFinal.SelectedDate, rcbEstadoBusqueda.SelectedValue,
                                                                       rgvGrid.MasterTableView.CurrentPageIndex, rgvGrid.MasterTableView.PageSize, BEH.relHelpDesk.EstadoHelpDesk,
                                                                       BEH.relHelpDesk.Formulario, BEH.relHelpDesk.HelpDeskDetalle, BEH.relHelpDesk.Modulo, BEH.relHelpDesk.Personal,
                                                                       BEH.relHelpDesk.Prioridad, BEH.relHelpDesk.TipoTicket)
            rgvGrid.VirtualItemCount = bcHelpDesk.ListasCount(rtbFilter.Text, rdpInicial.SelectedDate, rdpFinal.SelectedDate, rcbEstadoBusqueda.SelectedValue).Count
            MyBase.proLoad_RadGrid(rgvGrid, listaHelpDesk)
        Catch ex As Exception
            ControlarError(MyBase.ObtenerMetodo, If(ex.InnerException IsNot Nothing, ex.InnerException.ToString, ex.Message))
        End Try
    End Sub

    Private Sub llenarCombos()
        Try
            Dim beModulo = bcModulo.Listar(BEntities.UnidadNegocio.Yotau)
            Dim TipoTicket = bcClasificadores.List(BEntities.TipoClasificadores.TipoTicket)
            Dim Prioridad = bcClasificadores.List(BEntities.TipoClasificadores.PrioridadHelpDesk)
            Dim EstadoHelpDesk = bcClasificadores.List(BEntities.TipoClasificadores.EstadoHelpDesk)
            MyBase.proLoad_RadComboBox(rcbModulo, beModulo, "Id", "Nombre")
            MyBase.proLoad_RadComboBox(rcbTipoTicket, TipoTicket, "Id", "Nombre")
            MyBase.proLoad_RadComboBox(rcbPrioridad, Prioridad, "Id", "Nombre")
            MyBase.proLoad_RadComboBox(rcbEstadoHelpDesk, EstadoHelpDesk, "Id", "Nombre")
            MyBase.proLoad_RadComboBox(rcbEstadoBusqueda, EstadoHelpDesk, "Id", "Nombre")
        Catch ex As Exception
            ControlarError(MyBase.ObtenerMetodo, If(ex.InnerException IsNot Nothing, ex.InnerException.ToString, ex.Message))
        End Try
    End Sub
    'Private Sub proLoadParent(ByVal PositionId As Long)
    '    Dim lstPositions As List(Of BEB.Cargo) = bcPositions.Listas(BEB.relCargo.Cargo)
    '    Dim lst As List(Of BEB.Cargo) = (From item In lstPositions Where item.Id <> PositionId Select item).ToList
    '    MyBase.proLoad_RadComboBox(rcbPrioridad, lst, "Id", "Nombre")
    'End Sub


    'Private Sub DeletePosition(ByVal PositionId As Long)
    '    bePosition = New BEB.Cargo
    '    With bePosition
    '        .Id = PositionId
    '        .FechaActualizacion = Now
    '        .FechaReg = Now

    '        '.Nombre = rtbDescription.Text
    '        '.PositionId = rcbParent.SelectedValue
    '        '.OrganizationChart = 1
    '        '.BaseFee = rnbBaseFee.Value
    '        .StatusType = BEntities.StatusType.Delete
    '        .Estado = BEntities.Estado.Inactivo
    '    End With
    '    bcPositions.Save(bePosition)
    'End Sub

    'Private Sub SavePosition()
    '    bePosition = New BEB.Cargo
    '    With bePosition
    '        If CLng(ViewState("CargoId")) = 0 Then
    '            .StatusType = BEntities.StatusType.Insert
    '            .Id = 0
    '        Else
    '            .StatusType = BEntities.StatusType.Update
    '            .Id = CLng(ViewState("CargoId"))
    '        End If
    '        .Nombre = rtbDescription.Text
    '        .CargoId = rcbParent.SelectedValue
    '        .ComisionBase = rnbBaseFee.Value
    '        .FechaReg = Now
    '        .FechaActualizacion = Now
    '        If .CargoId = -1 Then
    '            .CargoId = 0
    '        End If
    '        .OrgGrafica = 1
    '        .Estado = BEntities.Estado.Activo
    '    End With
    '    bcPositions.Save(bePosition)
    '    ClearFields()
    'End Sub

    Private Sub ClearFields()
        rtbComentario.Text = String.Empty
        rcbPrioridad.ClearSelection()
        rcbTipoTicket.ClearSelection()
        rcbEstadoHelpDesk.ClearSelection()
        rcbFormulario.ClearSelection()
        If (HttpContext.Current.Request.QueryString("ModuloId") Is Nothing) Then
            rcbModulo.ClearSelection()
        End If
    End Sub

    Private Sub rbFilter_Click(sender As Object, e As EventArgs) Handles rbFilter.Click
        rgvGrid.MasterTableView.CurrentPageIndex = 0
        proLoadGrid()
    End Sub

    Private Sub rcbModulo_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles rcbModulo.SelectedIndexChanged
        Try
            If (CInt(rcbModulo.SelectedValue) > 0) Then
                Dim beFormulario = bcFormulario.ListByModulo(CInt(rcbModulo.SelectedValue))
                MyBase.proLoad_RadComboBox(rcbFormulario, beFormulario, "Id", "Nombre")
            End If
        Catch ex As Exception
            ControlarError(MyBase.ObtenerMetodo, If(ex.InnerException IsNot Nothing, ex.InnerException.ToString, ex.Message))
        End Try
    End Sub

    Public Sub OnUploadComplete(sender As Object, e As AjaxFileUploadEventArgs) Handles AjaxFileUpload11.UploadComplete
        Dim fileName As String = Path.GetFileName(e.FileName)
        'AjaxFileUpload11.SaveAs(Server.MapPath(Convert.ToString("~/assets/") & fileName))
        Try
            If Path.GetExtension(e.FileName).ToLower.Equals(".jpeg") Or Path.GetExtension(e.FileName).ToLower.Equals(".jpg") Or Path.GetExtension(e.FileName).ToLower.Equals(".pdf") Or Path.GetExtension(e.FileName).ToLower.Equals(".png") Or Path.GetExtension(e.FileName).ToLower.Equals(".gif") Then
                Dim imagenes As Byte()
                imagenes = e.GetContents
                'Dim archivo As Byte() = imagenes.ToArray
                Dim BEHelpDeskDetalle As BEH.HelpDeskDetalle = New BEH.HelpDeskDetalle
                With BEHelpDeskDetalle
                    .StatusType = BEntities.StatusType.Insert
                    .Id = 0
                    .HelpDeskId = 0
                    .Url = e.FileName
                    .NombreArchivo = e.FileName
                    .Archivo = imagenes
                    .Estado = BEntities.Estado.Activo
                    .FechaReg = Now
                    .FechaActualizacion = Now
                    .PersonalId = funGet_UserCode()
                End With
                If Session("NuevaListaCargadaAdjuntos") Is Nothing Then
                    Session("NuevaListaCargadaAdjuntos") = New List(Of BEH.HelpDeskDetalle)
                End If

                CType(Session("NuevaListaCargadaAdjuntos"), List(Of BEH.HelpDeskDetalle)).Add(BEHelpDeskDetalle)
                Session("ListaGlobal") = Session("listaHelpDeskDetalle")
                'Me.proLoad_RadGrid(rgvAdjuntos, listaHotelesAdjuntos)
                'bcHotelAdjunto.Save(listaHotelesAdjuntos.ElementAt(0))
            Else
                Session("MensajeError") = If(CType(Session("MensajeError"), String).Length = 0, Path.GetExtension(e.FileName), If(CType(Session("MensajeError"), String).Contains(Path.GetExtension(e.FileName)), Session("MensajeError"), String.Concat(Session("MensajeError"), ", ", Path.GetExtension(e.FileName))))
                'MyBase.proShow_Message("La importacion no permite el tipo de extension *" & Path.GetExtension(e.FileName))
            End If
        Catch ex As Exception
            MyBase.proShow_Message("La importacion no permite el tipo de extensión *" & Path.GetExtension(e.FileName))
        End Try
    End Sub

    Private Sub AbrirArchivo(v As Integer, index As Integer)
        'Dim strFilePath As String = listaHotelesAdjuntos.ElementAt(index + (rgvAdjuntos.MasterTableView.CurrentPageIndex * rgvAdjuntos.MasterTableView.PageSize)).Url
        'Dim fs As FileStream
        'If File.Exists(strFilePath) Then
        '_FileName = Report_file_name.Value
        'fs = File.Open(strFilePath, FileMode.Open)
        'Else
        '    '_FileName = Server.MapPath("EmptyFile.txt")
        '    fs = File.Open(strFilePath, FileMode.Open, FileAccess.Read)
        'End If
        'Dim bytBytes(fs.Length) As Byte
        'fs.Read(bytBytes, 0, fs.Length)
        'fs.Close()
        'fs.Dispose()


        Dim virtualPath As String = Hosting.HostingEnvironment.ApplicationVirtualPath
        Dim appName As String = virtualPath.Substring(1)

        Dim url = CType(Session("listaHelpDeskDetalle"), List(Of BEH.HelpDeskDetalle)).ElementAt(index + (rgvAdjuntos.MasterTableView.CurrentPageIndex * rgvAdjuntos.MasterTableView.PageSize)).Archivo
        Dim extencion As String = Path.GetExtension(CType(Session("listaHelpDeskDetalle"), List(Of BEH.HelpDeskDetalle)).ElementAt(index + (rgvAdjuntos.MasterTableView.CurrentPageIndex * rgvAdjuntos.MasterTableView.PageSize)).Url)
        'rbiImagen.DataValue = url

        ''Create a new window add it dynamically 
        ''The window will inherit the default settings of parent WindowManager 
        'Dim newWindow As New Telerik.Web.UI.RadWindow()
        'newWindow.NavigateUrl = "data:application/pdf;base64," & System.Text.Encoding.UTF8.GetString(url)
        ''Top and Left can be used in conjunction with the OffsetElementId (if no OffsetElementId is specified, the top left corner of the browser window is used 
        'newWindow.Top = Unit.Pixel(22)
        'newWindow.Left = Unit.Pixel(0)
        ''OPTION 1 
        ''Add the newly created RadWindow to the RadWindowManager's collection 
        ''WindowManager.Windows.Add(newWindow) 
        ''OPTION 2 
        ''since in the for ASP.NET AJAX version you can have a RadWindow outside of a RadWindowManager 
        ''as a separate control, you can add the newly created RadWindow directly to the form's Controls collection 
        'form1.Controls.Add(newWindow)
        'RadWindow2.NavigateUrl = "data:application/pdf;base64," & System.Text.Encoding.UTF8.GetString(url)
        ''Dim aqui As String = "Open('data:application/pdf;base64," & System.Text.Encoding.UTF8.GetString(url) & "');"
        'ScriptManager.RegisterClientScriptBlock(Page, Me.GetType, "cerrarpopUp", "Open();", True)
        'Page.Response.BinaryWrite(listaHotelesAdjuntos.ElementAt(index + (rgvAdjuntos.MasterTableView.CurrentPageIndex * rgvAdjuntos.MasterTableView.PageSize)).Archivo)
        'Page.Response.Flush()
        'Page.Response.End()


        'Response.Clear()
        'Response.ContentType = "application/pdf"
        'Response.BinaryWrite(url)
        'Response.Flush()
        Dim Script As String = ""
        If (extencion.ToLower.Contains("pdf")) Then
            Script = "window.radopen('data:application/pdf;base64," & System.Convert.ToBase64String(url) & "', 'RadWindow2');"
            ScriptManager.RegisterClientScriptBlock(Page, Me.GetType, "PDF", Script, True)
        ElseIf (extencion.ToLower.Contains("jpg")) Then
            Script = "window.radopen('data:image/jpg;base64," & System.Convert.ToBase64String(url) & "', 'RadWindow2');"
            ScriptManager.RegisterClientScriptBlock(Page, Me.GetType, "JPG", Script, True)
        ElseIf (extencion.ToLower.Contains("gif")) Then
            Script = "window.radopen('data:image/gif;base64," & System.Convert.ToBase64String(url) & "', 'RadWindow2');"
            ScriptManager.RegisterClientScriptBlock(Page, Me.GetType, "GIF", Script, True)
        ElseIf (extencion.ToLower.Contains("png")) Then
            Script = "window.radopen('data:image/png;base64," & System.Convert.ToBase64String(url) & "', 'RadWindow2');"
            ScriptManager.RegisterClientScriptBlock(Page, Me.GetType, "PNG", Script, True)
        ElseIf (extencion.ToLower.Contains("jpeg")) Then
            Script = "window.radopen('data:image/jpeg;base64," & System.Convert.ToBase64String(url) & "', 'RadWindow2');"
            ScriptManager.RegisterClientScriptBlock(Page, Me.GetType, "JPEG", Script, True)
            'ElseIf (extencion.Contains("doc")) Then
            '    'Script = "window.radopen('data:application/msword;base64," & System.Convert.ToBase64String(url) & "', 'RadWindow2');"
            '    'ScriptManager.RegisterClientScriptBlock(Page, Me.GetType, "Word", Script, True)
            '    'Me.Application.Documents.Open(FileName:="C:\Test\NewDocument.docx", ReadOnly:=True)
            '    Dim words As Document = New Document
            '    words.Application.Documents.Open(url, [ReadOnly]:=True).PrintPreview()
            'ElseIf (extencion.Contains("xls")) Then
            '    'Script = "window.radopen('data:application/msword;base64," & System.Convert.ToBase64String(url) & "', 'RadWindow2');"
            '    'ScriptManager.RegisterClientScriptBlock(Page, Me.GetType, "Word", Script, True)
            '    'Me.Application.Documents.Open(FileName:="C:\Test\NewDocument.docx", ReadOnly:=True)
            '    'Dim excels As Microsoft.Office.Interop.Excel.Application = New Microsoft.Office.Interop.Excel.Application
            '    'excels.Application.Documents.Open(url, [ReadOnly]:=True).PrintPreview()


            '    Dim xlApp As Microsoft.Office.Interop.Excel.Application
            '    Dim xlWorkBook As Microsoft.Office.Interop.Excel.Workbook
            '    Dim xlWorkSheet As Microsoft.Office.Interop.Excel.Worksheet

            '    xlApp = New Excel.Application

            '    xlWorkBook = xlApp.Workbooks.Open("data:application/vnd.ms-excel;base64," & System.Convert.ToBase64String(url) & "'", [ReadOnly]:=True)
            '    xlWorkSheet = xlWorkBook.Worksheets("Sheet1")
            '    xlApp.Visible = True

            '    '~~> Print Preview
            '    xlWorkBook.PrintPreview()

            '    MsgBox("Wait")

            '    xlWorkBook.Close(SaveChanges:=False)
            '    xlApp.Quit()

            '    'releaseObject(xlApp)
            '    'releaseObject(xlWorkBook)
            '    'releaseObject(xlWorkSheet)

        Else
            MyBase.proShow_Message("Formato de archivo no valido")
        End If

    End Sub
    Private Sub ModificarDescripcion(v As Integer, index As Integer)
        rtbDescripcionModificar.Text = CType(Session("listaHelpDeskDetalle"), List(Of BEH.HelpDeskDetalle)).ElementAt(index + (rgvAdjuntos.MasterTableView.CurrentPageIndex * rgvAdjuntos.MasterTableView.PageSize)).Descripcion
    End Sub
    Private Sub DeleteHotelAdjunto(v As Int32, index As Int32)
        If (v = 0) Then
            CType(Session("listaHelpDeskDetalle"), List(Of BEH.HelpDeskDetalle)).RemoveAt(index + (rgvAdjuntos.MasterTableView.CurrentPageIndex * rgvAdjuntos.MasterTableView.PageSize))
        Else
            beHelpDeskDetalle = New BEH.HelpDeskDetalle
            beHelpDeskDetalle = CType(Session("listaHelpDeskDetalle"), List(Of BEH.HelpDeskDetalle)).ElementAt(index + (rgvAdjuntos.MasterTableView.CurrentPageIndex * rgvAdjuntos.MasterTableView.PageSize))
            With beHelpDeskDetalle
                .Id = v
                .PersonalId = MyBase.funGet_UserCode
                .StatusType = BEntities.StatusType.Delete
                .Estado = BEntities.Estado.Inactivo
                .FechaActualizacion = Now
            End With
            bcHelpDeskDetalle.Save(beHelpDeskDetalle)
            CType(Session("listaHelpDeskDetalle"), List(Of BEH.HelpDeskDetalle)).RemoveAt(index + (rgvAdjuntos.MasterTableView.CurrentPageIndex * rgvAdjuntos.MasterTableView.PageSize))
        End If
        rgvAdjuntos.VirtualItemCount = CType(Session("listaHelpDeskDetalle"), List(Of BEH.HelpDeskDetalle)).Count
        MyBase.proLoad_RadGrid(rgvAdjuntos, CType(Session("listaHelpDeskDetalle"), List(Of BEH.HelpDeskDetalle)))
        ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "funShow_Message('Adjunto Eliminado', 'INFORMATION');", True)
    End Sub

    Private Sub rgvAdjuntos_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles rgvAdjuntos.ItemCommand
        Try
            If (e.CommandName <> "Page") Then

                If e.CommandName = "EliminarAdjunto" Then

                If CType(Session("BePrivilegios"), BEntities.Seguridad.Privilegio).Permiso.Contains("B") Then
                    Dim item As GridDataItem = e.Item
                    Dim index As Int32 = item.ItemIndex
                    Dim AdjuntoId As String = item.GetDataKeyValue("Id").ToString()
                    DeleteHotelAdjunto(CInt(AdjuntoId), index)
                    'proInit_Form()
                Else
                    ScriptManager.RegisterClientScriptBlock(Page,
                                                            Me.GetType,
                                                            "cerrarpopUp",
                                                            "funShow_Message('" & CType(Session("BePrivilegios"), BEntities.Seguridad.Privilegio).NoUpdate & "', 'ERROR');",
                                                            True)
                End If
            End If

            Dim item2 As GridDataItem = e.Item
            Dim beHelpDeskValidacion = bcHelpDesk.Search(CInt(item2.GetDataKeyValue("Id").ToString()))
            If e.CommandName = "EditarDescripcion" Then
                If CType(Session("BePrivilegios"), BEntities.Seguridad.Privilegio).Permiso.Contains("M") Or If(beHelpDeskValidacion IsNot Nothing, beHelpDeskValidacion.PersonalSolicitanteId = MyBase.funGet_UserCode(), True) Then
                    Dim item As GridDataItem = e.Item
                    Dim index As Int32 = item.ItemIndex
                    Dim AdjuntoId As String = item.GetDataKeyValue("Id").ToString()
                    Session("IdAdjunto") = index + (rgvAdjuntos.MasterTableView.CurrentPageIndex * rgvAdjuntos.MasterTableView.PageSize)
                    If (CType(Session("listaHelpDeskDetalle"), List(Of BEH.HelpDeskDetalle)).ElementAt(Session("IdAdjunto")).PersonalId = MyBase.funGet_UserCode) Then
                        If (CType(Session("listaHelpDeskDetalle"), List(Of BEH.HelpDeskDetalle)).ElementAt(Session("IdAdjunto")).FechaReg.Date = Now.Date) Then
                            ModificarDescripcion(CInt(AdjuntoId), index)
                        Else
                            ScriptManager.RegisterClientScriptBlock(Page,
                                                    Me.GetType,
                                                    "cerrarpopUp",
                                                    "closeDialog('divDescripcion'); funShow_Message('Solo se pueden modificar comentarios en la misma fecha en que se hicieron.', 'ERROR');",
                                                    True)
                        End If
                    Else
                        ScriptManager.RegisterClientScriptBlock(Page,
                                                    Me.GetType,
                                                    "cerrarpopUp",
                                                    "closeDialog('divDescripcion'); funShow_Message('Solo puede modificar su propio mensaje', 'ERROR');",
                                                    True)
                    End If
                    'proInit_Form()
                Else
                    ScriptManager.RegisterClientScriptBlock(Page,
                                                    Me.GetType,
                                                    "cerrarpopUp",
                                                    "funShow_Message('" & CType(Session("BePrivilegios"), BEntities.Seguridad.Privilegio).NoUpdate & "', 'ERROR');",
                                                    True)
                End If
            End If
                If e.CommandName = "Abrir" Then
                    'If BePrivilegios.Permiso.Contains("E") Then
                    Dim item As GridDataItem = e.Item
                    Dim index As Int32 = item.ItemIndex
                    Dim AdjuntoId As String = item.GetDataKeyValue("Id").ToString()
                    Session("IdAdjunto") = index + (rgvAdjuntos.MasterTableView.CurrentPageIndex * rgvAdjuntos.MasterTableView.PageSize)
                    AbrirArchivo(CInt(AdjuntoId), index)

                    'Else

                    'End If
                End If
            End If
        Catch ex As Exception
            ControlarError(MyBase.ObtenerMetodo, If(ex.InnerException IsNot Nothing, ex.InnerException.ToString, ex.Message))
        End Try
    End Sub

    Private Sub rbActualizarTabla_Click(sender As Object, e As EventArgs) Handles rbActualizarTabla.Click
        Try
            If (rtbDescripcionAdjuntos.Text.Trim.Equals("")) Then
                MyBase.proShow_Message("Ingrese una descripcion")
                rtbDescripcionAdjuntos.Focus()
            Else
                For Each item In CType(Session("NuevaListaCargadaAdjuntos"), List(Of BEH.HelpDeskDetalle))
                    item.Descripcion = rtbDescripcionAdjuntos.Text
                    CType(Session("listaHelpDeskDetalle"), List(Of BEH.HelpDeskDetalle)).Add(item)
                Next
                If (CType(Session("NuevaListaCargadaAdjuntos"), List(Of BEH.HelpDeskDetalle)).Count = 0) Then
                    Dim BEHelpDeskDetalle As BEH.HelpDeskDetalle = New BEH.HelpDeskDetalle
                    With BEHelpDeskDetalle
                        .StatusType = BEntities.StatusType.Insert
                        .Id = 0
                        .HelpDeskId = 0
                        .Url = "Solo Comentario"
                        .NombreArchivo = "Solo Comentario"
                        .Descripcion = rtbDescripcionAdjuntos.Text
                        .Archivo = Nothing
                        .Estado = BEntities.Estado.Activo
                        .FechaReg = Now
                        .FechaActualizacion = Now
                        .PersonalId = funGet_UserCode()
                    End With
                    CType(Session("listaHelpDeskDetalle"), List(Of BEH.HelpDeskDetalle)).Add(BEHelpDeskDetalle)
                End If
                Session("NuevaListaCargadaAdjuntos") = New List(Of BEH.HelpDeskDetalle)
                rgvAdjuntos.MasterTableView.VirtualItemCount = CType(Session("listaHelpDeskDetalle"), List(Of BEH.HelpDeskDetalle)).Count
                MyBase.proLoad_RadGrid(rgvAdjuntos, CType(Session("listaHelpDeskDetalle"), List(Of BEH.HelpDeskDetalle)))
                rtbDescripcionAdjuntos.Text = String.Empty
                If (CType(Session("MensajeError"), String).Length > 0) Then
                    MyBase.proShow_Message("La importacion no permite el tipo de extension *" & Session("MensajeError"))
                End If
                Session("MensajeError") = ""
            End If
        Catch ex As Exception
            ControlarError(MyBase.ObtenerMetodo, If(ex.InnerException IsNot Nothing, ex.InnerException.ToString, ex.Message))
        End Try
    End Sub
    Public Sub Activar(ByVal posi As Boolean)
        rtbComentario.Enabled = posi
        rcbPrioridad.Enabled = posi
        rcbModulo.Enabled = posi
        rcbFormulario.Enabled = posi
        rcbTipoTicket.Enabled = posi
        rcbEstadoHelpDesk.Enabled = posi
    End Sub
    Private Sub rbGuardarDescripcion_Click(sender As Object, e As EventArgs) Handles rbGuardarDescripcion.Click
        Try
            CType(Session("listaHelpDeskDetalle"), List(Of BEH.HelpDeskDetalle)).ElementAt(Session("IdAdjunto")).Descripcion = rtbDescripcionModificar.Text
            CType(Session("listaHelpDeskDetalle"), List(Of BEH.HelpDeskDetalle)).ElementAt(Session("IdAdjunto")).StatusType = If(CType(Session("listaHelpDeskDetalle"), List(Of BEH.HelpDeskDetalle)).ElementAt(Session("IdAdjunto")).Id = 0, BEntities.StatusType.Insert, BE.StatusType.Update)
            rgvAdjuntos.VirtualItemCount = CType(Session("listaHelpDeskDetalle"), List(Of BEH.HelpDeskDetalle)).Count
            MyBase.proLoad_RadGrid(rgvAdjuntos, CType(Session("listaHelpDeskDetalle"), List(Of BEH.HelpDeskDetalle)))
            ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp2", "closeDialog('divDescripcion'); funShow_Message('Descripcion Actualizada', 'INFORMATION');", True)
        Catch ex As Exception
            ControlarError(MyBase.ObtenerMetodo, If(ex.InnerException IsNot Nothing, ex.InnerException.ToString, ex.Message))
        End Try
    End Sub
    ''' <summary>
    ''' os
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub rbGuardarAdjunto_Click(sender As Object, e As EventArgs) Handles rbGuardarAdjunto.Click
        Try
            If (rcbEstadoHelpDesk.Enabled = False And rcbEstadoHelpDesk.SelectedValue = BE.EstadoHelpDesk.Cerrado) Then
                ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp13", "funShow_Message('La insidencia ya fue solucionada.', 'ERROR');", True)
            Else
                If (Session("HelpDeskId") = 0) Then
                    beHelpDesk = New BEH.HelpDesk
                    beHelpDesk.Id = 0
                    beHelpDesk.StatusType = BE.StatusType.Insert
                    beHelpDesk.FechaReg = Now
                    beHelpDesk.PersonalSolicitanteId = funGet_UserCode()
                Else
                    beHelpDesk = bcHelpDesk.Search(Session("HelpDeskId"), BEH.relHelpDesk.HelpDeskDetalle)
                    beHelpDesk.StatusType = BE.StatusType.Update
                End If
                With beHelpDesk
                    .StatusType = If(Session("HelpDeskId") = 0, BE.StatusType.Insert, BE.StatusType.Update)
                    .ModuloId = rcbModulo.SelectedValue
                    .FormularioId = rcbFormulario.SelectedValue
                    .ProblemaDescripcion = rtbComentario.Text
                    .TipoTicketIdc = rcbTipoTicket.SelectedValue
                    .PrioridadIdc = rcbPrioridad.SelectedValue
                    .EstadoHelpDeskIdc = rcbEstadoHelpDesk.SelectedValue
                    .Estado = BE.Estado.Activo
                    .FechaActualizacion = Now
                    .PersonalId = funGet_UserCode()
                    .ListaDetalleHelpDesk = CType(Session("listaHelpDeskDetalle"), List(Of BEH.HelpDeskDetalle))
                End With
                If (beHelpDesk.Id = 0 And CType(Session("listaHelpDeskDetalle"), List(Of BEH.HelpDeskDetalle)).Count > 0) Then
                    If (CType(Session("listaHelpDeskDetalle"), List(Of BEH.HelpDeskDetalle)).ElementAt(0).Archivo IsNot Nothing) Then
                        bcHelpDesk.Save(beHelpDesk)
                        EnviarCorreo(rtbComentario.Text)
                        proLoadGrid()
                        ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp10", "closeDialog('divDetail'); funShow_Message('Documentos adjuntos guardados', 'INFORMATION');", True)
                    Else
                        ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp12", "funShow_Message('Debe ingresar una imagen de la insidencia', 'ERROR');", True)
                    End If
                Else
                    If (beHelpDesk.Id > 0 And CType(Session("listaHelpDeskDetalle"), List(Of BEH.HelpDeskDetalle)).Count > 1) Then
                        bcHelpDesk.Save(beHelpDesk)
                        EnviarCorreoRespuesta(beHelpDesk)
                        proLoadGrid()
                        ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp10", "closeDialog('divDetail'); funShow_Message('Documentos adjuntos guardados', 'INFORMATION');", True)
                    Else
                        ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp11", "funShow_Message('Debe ingresar una descripcion', 'ERROR');", True)
                    End If
                End If

            End If
        Catch ex As Exception
            ControlarError(MyBase.ObtenerMetodo, If(ex.InnerException IsNot Nothing, ex.InnerException.ToString, ex.Message))
        End Try
    End Sub

    Private Sub rcbEstadoBusqueda_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles rcbEstadoBusqueda.SelectedIndexChanged
        rgvGrid.MasterTableView.CurrentPageIndex = 0
        proLoadGrid()
    End Sub

    Private Sub rgvAdjuntos_PageIndexChanged(sender As Object, e As GridPageChangedEventArgs) Handles rgvAdjuntos.PageIndexChanged
        rgvAdjuntos.CurrentPageIndex = e.NewPageIndex
        rgvAdjuntos.MasterTableView.VirtualItemCount = CType(Session("listaHelpDeskDetalle"), List(Of BEH.HelpDeskDetalle)).Count
        MyBase.proLoad_RadGrid(rgvAdjuntos, CType(Session("listaHelpDeskDetalle"), List(Of BEH.HelpDeskDetalle)))
    End Sub
End Class