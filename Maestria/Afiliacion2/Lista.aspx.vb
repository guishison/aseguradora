
Imports Telerik.Web.UI
Imports BCB = BComponents.Base
Imports BEB = BEntities.Base
Imports BEntities
Imports BE = BEntities
Imports System.Text.StringBuilder
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.IO
Imports AjaxControlToolkit
Imports BCBT = BComponents.Bitacora
Imports BCS = BComponents.Seguridad
Imports BEBT = BEntities.Bitacora
Imports System.Data.SqlClient

Public Class Lista
    Inherits UtilsMethods

    Private bcBitacoraErrores As New BCBT.BitacoraError
    Private beBitacoraErrores As New BEBT.BitacoraError
    Private bcFoto As New BCB.Foto
    Private beFoto As BEB.Foto
    Private beAlbum As BEB.Album
    Private bcAlbum As BCB.Album = New BCB.Album
    Private bcFormulario As New BCS.Formulario
    Dim IdHotel = 0

    'Dim BcPrivilegios As New BComponents.Seguridad.Privilegio

    'Public Shared Property ListHotelPrograma As List(Of BEB.HotelPrograma) = New List(Of BEB.HotelPrograma)
    'Public Shared Property ContProgramas As Int32 = 0
    'Public Shared Property MensajeError As String = ""
    'Public Shared Property listaHotelesAdjuntos As List(Of BEB.HotelAdjunto) = New List(Of BEB.HotelAdjunto)
    'Public Shared Property NuevaListaCargadaAdjuntos As List(Of BEB.HotelAdjunto) = New List(Of BEB.HotelAdjunto)
    Private Overloads Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If AjaxFileUpload11.IsInFileUploadPostBack Then
        'Else
        If Not Page.IsPostBack Then
            '%%%%%%%Iniciar Variables%%%%%%%%%%%%%%
            'Session("ListHotelPrograma") = New List(Of BEB.HotelPrograma)
            'Session("ContProgramas") = 0
            'Session("MensajeError") = ""
            'Session("listaFotos") = New List(Of BEB.HotelAdjunto) ' Session("listaFotos") = New List(Of BEB.HotelAdjunto)

            'Session("NuevaListaCargadaAdjuntos") = New List(Of BEB.HotelAdjunto) 'Session("NuevaListaCargadaAdjuntos") = New List(Of BEB.HotelAdjunto)



            '%%%%%%% End Iniciar Variables%%%%%%%%%%%%%%
            'Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-ES")
            'Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-ES")

            'rbAdd.Attributes.Add("onkeypress", "return (event.keyCode!=13);")
            'Session("ListHotelPrograma") = New List(Of BEB.HotelPrograma) 'ListHotelPrograma.Clear()

            proInit_Form()
            'listaHotelesAdjuntos = New List(Of BEB.HotelAdjunto)
            Session("archivo") = 0
            'Session("ListaGlobal") = ""
            'Session("IsOpenDialog") = False
            'Dim sortExpr As New GridSortExpression()
            'sortExpr.SortOrder = GridSortOrder.Descending
            'rgvGrid.MasterTableView.SortExpressions.AddSortExpression(sortExpr)
        End If
        'End If

    End Sub
    'Private Sub ControlarError(ByVal Metodo As String, ByVal Sms As String)
    '    beBitacoraErrores = New BEBT.BitacoraError
    '    With beBitacoraErrores
    '        .StatusType = BEntities.StatusType.Insert
    '        .Evento = Metodo
    '        .Url = MyBase.ObtenerUrl()
    '        .MensajeError = Sms
    '        .Version = MyBase.ObtenerVersion()
    '        .SistemaOperativo = MyBase.ObtenerSistemaOperativo()
    '        .Navegador = MyBase.ObtenerNavegador()
    '        .PersonalId = funGet_UserCode()
    '        .FechaReg = Now
    '    End With
    '    bcBitacoraErrores.Save(beBitacoraErrores)
    '    MyBase.proShow_Message("Error, verifique lo siguiente: " & Sms)
    'End Sub


    'Protected Sub rgvGrid_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles rgvGrid.ItemCommand
    '    Try

    'Dim BePrivilegios As New BEntities.Seguridad.Privilegio
    'Dim BcPrivilegios As New BComponents.Seguridad.Privilegio

    'Dim beFormularios = bcFormulario.BuscarPorUnidadNegocio(BE.URLFormularios.Albunes.ToString, funGet_UnidadNegocioPadre)
    'BePrivilegios = BcPrivilegios.BuscarPrivilegioPorPersonal(funGet_UserCode(), beFormularios.Id, funGet_UnidadNegocioPadre())
    '        If e.CommandName = "Editar" Then

    '            If BePrivilegios.Permiso.Contains("M") Then
    '                Dim item As GridDataItem = e.Item
    '                Dim HotelId As String = item.GetDataKeyValue("Id").ToString()

    '                LoadHotel(CLng(HotelId))
    '            Else
    '                ScriptManager.RegisterClientScriptBlock(Page,
    '                                                        Me.GetType,
    '                                                        "cerrarpopUp",
    '                                                        "closeDialog('divDetail'); funShow_Message('" & BePrivilegios.NoUpdate & "', 'ERROR');",
    '                                                        True)
    '            End If

    '        ElseIf e.CommandName = "Eliminar" Then

    '            If BePrivilegios.Permiso.Contains("B") Then
    '                Dim item As GridDataItem = e.Item
    '                Dim HotelId As String = item.GetDataKeyValue("Id").ToString()
    '                DeleteHotel(CLng(HotelId))
    '                proInit_Form()
    '            Else
    '                ScriptManager.RegisterClientScriptBlock(Page,
    '                                                        Me.GetType,
    '                                                        "cerrarpopUp",
    '                                                        "closeDialog('divDetail'); funShow_Message('" & BePrivilegios.NoDelete & "', 'ERROR');",
    '                                                        True)
    '            End If
    '        ElseIf e.CommandName = "Galeria" Then
    '            If BePrivilegios.Permiso.Contains("E") Then
    '                Dim item As GridDataItem = e.Item
    '                Dim index As Int32 = item.ItemIndex
    '                Dim AdjuntoId As String = item.GetDataKeyValue("Id").ToString()
    '                Session("listaFotos") = bcFoto.ListByAlbumId(AdjuntoId) ' listaHotelesAdjuntos = bcHotelAdjunto.ListByHotel(AdjuntoId, rgvGrid.MasterTableView.CurrentPageIndex, rgvGrid.MasterTableView.PageSize)
    '                'AbrirArchivo(CInt(AdjuntoId), index)
    '                If (CType(Session("listaFotos"), List(Of BEB.Foto)).Count > 0) Then ' If (listaHotelesAdjuntos.Count > 0) Then
    '                    CrearGaleria2(CType(Session("listaFotos"), List(Of BEB.Foto))) ' CrearGaleria2(listaHotelesAdjuntos)

    '                Else
    '                    MyBase.proShow_Message("El hotel seleccionado no tiene imagenes adjuntas")
    '                End If
    '            Else
    '                ScriptManager.RegisterClientScriptBlock(Page,
    '                                                        Me.GetType,
    '                                                        "cerrarpopUp",
    '                                                        "closeDialog('divDetail'); funShow_Message('" & BePrivilegios.NoExport & "', 'ERROR');",
    '                                                        True)
    '            End If
    '        ElseIf e.CommandName = "AddHotelPrograma" Then
    '            If BePrivilegios.Permiso.Contains("A") Then

    '                Dim item As GridDataItem = e.Item
    '                Dim HotelId As String = item.GetDataKeyValue("Id").ToString()

    '                Session("AlbumId") = HotelId
    '                IdHotel = HotelId

    '                'Mostrar el nombre del hotel seleccionado'
    '                'Session("IsOpenDialog") = True
    '            Else
    '                ScriptManager.RegisterClientScriptBlock(Page,
    '                                                            Me.GetType,
    '                                                            "cerrarpopUp",
    '                                                            "closeDialog('divDetailHotelPrograma'); funShow_Message('" & BePrivilegios.NoInsert & "', 'ERROR');",
    '                                                            True)
    '            End If
    '        End If
    '    Catch ex As Exception
    '        proShow_Message(If(ex.InnerException IsNot Nothing, ex.InnerException.ToString, ex.Message))
    '        'ControlarError(MyBase.ObtenerMetodo, If(ex.InnerException IsNot Nothing, ex.InnerException.ToString, ex.Message))
    '    End Try
    'End Sub
    Private Sub CrearGaleria2(listaAdjuntos As List(Of BEB.Foto))

        Dim sb As StringBuilder = New StringBuilder

        sb.Append("$(document).ready(function () {")
        sb.AppendLine()
        sb.Append("var gallery = $('#divDetail3').lightGallery({")
        sb.AppendLine()
        sb.Append("dynamic: true,")
        sb.AppendLine()
        sb.Append("html: true,")
        sb.AppendLine()
        sb.Append("mobileSrc: true,")
        sb.AppendLine()
        sb.Append("dynamicEl: [")
        'Dim aquisito As String = ""
        'Dim pathcito = Server.MapPath("~")
        'Dim fileNames = My.Computer.FileSystem.GetFiles(pathcito.Replace("\", "/") +
        '"/assets/image/Hotel_Pucon/", FileIO.SearchOption.SearchTopLevelOnly, "*.jpg")
        listaAdjuntos = (From item In listaAdjuntos Where Path.GetExtension(item.Foto.ToLower).Contains(".jpg") Or Path.GetExtension(item.Foto.ToLower).Contains(".jpeg") Or Path.GetExtension(item.Foto.ToLower).Contains(".png") Or Path.GetExtension(item.Foto.ToLower).Contains(".gif")).ToList
        For Each imagenes In listaAdjuntos
            'aquisito = imagenes
            'Dim go As String = Split(imagenes, appName,, CompareMethod.Text)(1)
            Dim url = imagenes.Foto
            sb.AppendLine()
            If (Path.GetExtension(imagenes.Foto.ToLower).Equals(".jpg") Or Path.GetExtension(imagenes.Foto.ToLower).Equals(".jpeg") Or Path.GetExtension(imagenes.Foto.ToLower).Equals(".png") Or Path.GetExtension(imagenes.Foto.ToLower).Equals(".gif")) Then
                sb.Append("{ 'src': ' " & imagenes.Foto & "', 'thumb': ' " & imagenes.Foto & "', 'Sub-html': " & Chr(34) & "<div Class='custom-html'><h4>Custom HTML</h4></div>" & Chr(34) & ", 'mobileSrc':' " & imagenes.Foto & "'},")
            End If

        Next
        'Dim hello As String = String.Concat("$(document).ready(function () { alert('", pathcito.Replace("\", "/"), " ---- ", Split(aquisito, appName,, CompareMethod.Text)(1).Replace("\", "/"), "'); });")
        'ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", hello, True)

        sb.AppendLine()
        sb.Append("]")
        sb.AppendLine()
        sb.Append("});")
        sb.AppendLine()
        sb.Append("gallery.on('onCloseAfter.lg', function (event) {
console.log($('#divDetail3').data('lightGallery'));
if($('#divDetail3').data('lightGallery') === undefined){}else{
    $('#divDetail3').data('lightGallery').destroy(true)}
})")
        sb.AppendLine()
        sb.Append("});")
        sb.AppendLine()
        sb.Append("")
        'Ocultito.Value = StringUrls
        If (listaAdjuntos.Count > 0) Then
            ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", sb.ToString, True)
        End If
        'ClientScript.RegisterStartupScript(Page.GetType, "script", sb.ToString)
        'Page.ClientScript.RegisterStartupScript(Me.GetType(), "prueba", sb.ToString)
    End Sub
    'Private Sub CrearGaleria()
    '    Dim virtualPath As String = Hosting.HostingEnvironment.ApplicationVirtualPath
    '    Dim appName As String = virtualPath.Substring(1)
    '    'Dim dt As New DataTable()
    '    'dt.Columns.AddRange(New DataColumn() {New DataColumn("ProductName"), New DataColumn("ImageUrl"), New DataColumn("BigImageUrl")})

    '    Dim sb As StringBuilder = New StringBuilder
    '    Dim sb2 As StringBuilder = New StringBuilder
    '    sb.Append("<script language='javascript'>")
    '    sb.AppendLine()
    '    sb.Append("showDialog('divDetail3');")
    '    sb.AppendLine()
    '    sb.Append("var holass = $('#divDetail3');")
    '    sb.AppendLine()
    '    sb.Append("console.log(holass);")
    '    Dim generadorHtml = ""
    '    '    Dim generadorHtml2 = " < Script type='text/javascript'> showDialog('divDetail3');
    '    'var holass = $('#divDetail3');
    '    'console.log(holass);"
    '    Dim fileNames = My.Computer.FileSystem.GetFiles(
    '    "C:\Users\programador5\Desktop\repositorio\testFundatio\Crm_Sage\Afiliacion2\assets\image\Hotel_Pucon\", FileIO.SearchOption.SearchTopLevelOnly, "*.jpg")
    '    For Each imagenes In fileNames
    '        sb2.Append("<a href='" & imagenes.Replace("C:\Users\programador5\Desktop\repositorio\testFundatio\Crm_Sage\Afiliacion2\assets\image\Hotel_Pucon\", "/" + appName + "/assets/image/Hotel_Pucon/") & "'>" _
    '            & "<img class='img-responsive' src='" & imagenes.Replace("C:\Users\programador5\Desktop\repositorio\testFundatio\Crm_Sage\Afiliacion2\assets\image\Hotel_Pucon\", "/" + appName + "/assets/image/Hotel_Pucon/") & "' alt='hola' />" _
    '        & "</a>")
    '        sb2.AppendLine()
    '        'generadorHtml2 = generadorHtml2 & "$('#divDetail3').html('<a href='" & imagenes.Replace("C:\Users\programador5\Desktop\repositorio\testFundatio\Crm_Sage\Afiliacion2\assets\image\Hotel_Pucon\", "/" + appName + "/assets/image/Hotel_Pucon/") & "'>" _
    '        '    & "<img class='img-responsive' src='" & imagenes.Replace("C:\Users\programador5\Desktop\repositorio\testFundatio\Crm_Sage\Afiliacion2\assets\image\Hotel_Pucon\", "/" + appName + "/assets/image/Hotel_Pucon/") & "' alt='hola'/>" _
    '        '& "</a>');"
    '        'dt.Rows.Add(imagenes.Replace("C:\Users\programador5\Desktop\repositorio\testFundatio\Crm_Sage\Afiliacion2\assets\image\Hotel_Pucon\", "/" + appName + "/assets/image/Hotel_Pucon/"), imagenes.Replace("C:\Users\programador5\Desktop\repositorio\testFundatio\Crm_Sage\Afiliacion2\assets\image\Hotel_Pucon\", "/" + appName + "/assets/image/Hotel_Pucon/"), imagenes.Replace("C:\Users\programador5\Desktop\repositorio\testFundatio\Crm_Sage\Afiliacion2\assets\image\Hotel_Pucon\", "/" + appName + "/assets/image/Hotel_Pucon/"))
    '    Next
    '    sb.Append(" $('#divDetail3').html('" & sb2.ToString & "');")
    '    sb.AppendLine()
    '    sb.Append("$('#divDetail3').lightGallery();")
    '    sb.AppendLine()
    '    sb.Append("</script>")
    '    'divDetail3.InnerHtml = generadorHtml
    '    'ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "abrirgaleria('hola');", True)
    '    ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "holaaaaaa", sb.ToString, False)
    '    'rptImages.DataSource = dt
    '    'rptImages.DataBind()
    'End Sub
    'Protected Sub rgvGrid_PageIndexChanged(sender As Object, e As GridPageChangedEventArgs) Handles rgvGrid.PageIndexChanged
    '    sender.CurrentPageIndex = e.NewPageIndex
    '    proInit_Form()
    'End Sub
    'Protected Sub rgvAdjuntos_PageIndexChanged(sender As Object, e As GridPageChangedEventArgs) Handles rgvAdjuntos.PageIndexChanged
    '    sender.CurrentPageIndex = e.NewPageIndex
    '    rgvAdjuntos.VirtualItemCount = CType(Session("listaFotos"), List(Of BEB.Foto)).Count 'listaHotelesAdjuntos.Count
    '    MyBase.proLoad_RadGrid(rgvAdjuntos, CType(Session("listaFotos"), List(Of BEB.Foto))) ' MyBase.proLoad_RadGrid(rgvAdjuntos, listaHotelesAdjuntos)
    'End Sub
    'Protected Sub rbAdd_Click(sender As Object, e As EventArgs) Handles rbAdd.Click
    '    Try

    '        Dim BePrivilegios As New BEntities.Seguridad.Privilegio
    '        Dim BcPrivilegios As New BComponents.Seguridad.Privilegio

    '        Dim beFormularios = bcFormulario.BuscarPorUnidadNegocio(BE.URLFormularios.Albunes.ToString, funGet_UnidadNegocioPadre)
    '        BePrivilegios = BcPrivilegios.BuscarPrivilegioPorPersonal(funGet_UserCode(), beFormularios.Id, funGet_UnidadNegocioPadre())
    '        If BePrivilegios.Permiso.Contains("A") Then
    '            LoadHotel(0)
    '        Else
    '            ScriptManager.RegisterClientScriptBlock(Page,
    '                                                    Me.GetType,
    '                                                    "cerrarpopUp",
    '                                                    "closeDialog('divDetail'); funShow_Message('" & BePrivilegios.NoInsert & "', 'ERROR');",
    '                                                    True)
    '        End If

    '        'If Session("IsOpenDialog") = False Then
    '        'Else
    '        '    ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "closeDialog('divDetail');", True)
    '        'End If
    '    Catch ex As Exception
    '        proShow_Message(If(ex.InnerException IsNot Nothing, ex.InnerException.ToString, ex.Message))
    '    End Try
    'End Sub
    'Private Function ValidarCamposHotel() As String
    '    Dim result = String.Empty
    '    If Me.rtbNombre.Text = String.Empty Or Me.rtbNombre.Text.Length < 3 Or Me.rtbNombre.Text.Length > 150 Then
    '        result += "<br />- El nombre del hotel debe tener como mínimo 3 caracteres y como máximo 150 caracteres."
    '    End If

    '    If Me.rtbDescripcionAlbum.Text = String.Empty Then
    '        result += "<br />- Ingrese una descripcion del album."
    '    End If

    '    beAlbum = (From x In bcAlbum.ListBuscar("", 0, 1100) Where x.Nombre.ToLower().Equals(rtbNombre.Text.ToLower()) And x.Id <> Session("AlbumId") Select x).SingleOrDefault
    '    If beAlbum IsNot Nothing Then
    '        result += "<br />- Ya existe un hotel con el mismo nombre."
    '    End If

    '    Return result
    'End Function
    'Protected Sub rtbSave_Click(sender As Object, e As EventArgs) Handles rtbSave.Click
    '    Dim result = ValidarCamposHotel()
    '    If result.Length = 0 Then
    '        SaveHotel()
    '        ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "closeDialog('divDetail'); funShow_Message('Registro guardado', 'INFORMATION');", True)
    '        proInit_Form()
    '    Else
    '        MyBase.proShow_Message("Revise los siguientes datos:\n" + result)
    '    End If

    'End Sub
    Protected Sub RadImageGallery2_NeedDataSource(sender As Object, e As ImageGalleryNeedDataSourceEventArgs)
        'RadImageGallery2.DataSource = GetDataTable() 'bcAlbum.ListByAlbum("select distinct a.Nombre as Nombre, a.Descripcion as Descripcion, max(f.Foto) as url from crm_foto_album a inner join crm_foto_foto f on a.Id = f.AlbumId where a.Estado = 1 group by a.Nombre, a.Descripcion")
    End Sub

    Public Function GetDataTable(query As String) As DataView


        Dim ConnString As [String] = ConfigurationManager.ConnectionStrings("Negocio").ConnectionString
        Dim conn As New SqlConnection(ConnString)
        Dim adapter As New SqlDataAdapter()
        adapter.SelectCommand = New SqlCommand(query, conn)

        Dim myDataTable As New DataTable()

        conn.Open()
        Try
            adapter.Fill(myDataTable)
        Finally
            conn.Close()
        End Try

        Dim dataView As New DataView(myDataTable)
        dataView.Sort = "Foto DESC"
        Return dataView
    End Function
    Private Sub proInit_Form()
        Try
            'ClearFields()
            '<telerik:RadImageGallery RenderMode="Lightweight" ID="RadImageGallery2" runat="server" OnNeedDataSource="RadImageGallery2_NeedDataSource" DataDescriptionField="Descripcion" DataImageField="url"
            'DataTitleField = "Nombre" Width="600px" Height="480px" LoopItems="true">
            '    <ThumbnailsAreaSettings ThumbnailWidth = "120px" ThumbnailHeight="80px" Height="80px" />
            '   <ImageAreaSettings Height = "400px" />
            '</telerik:RadImageGallery>
            Dim bcClasificadores As New BCB.Clasificadores
            Dim listalbum = bcAlbum.ListBuscar("", 0, 1000)
            For Each item In listalbum

                Dim galeria As RadImageGallery = New RadImageGallery
                galeria.RenderMode = RenderMode.Lightweight
                galeria.ID = item.Nombre
                galeria.DataDescriptionField = "Descripcion"
                galeria.DataImageField = "Foto"
                galeria.DataTitleField = "Nombre"
                galeria.Width = 600
                galeria.Height = 400
                galeria.CssClass = "margencito"
                galeria.LoopItems = True
                galeria.ThumbnailsAreaSettings.ThumbnailWidth = 120
                galeria.ThumbnailsAreaSettings.ThumbnailHeight = 80
                galeria.ThumbnailsAreaSettings.Height = 80
                galeria.ImageAreaSettings.Height = 400
                galeria.DataSource = GetDataTable("select Nombre,Descripcion,Foto from crm_foto_foto where AlbumId = " & item.Id.ToString)
                Dim lblTitulo As RadLabel = New RadLabel
                lblTitulo.Text = item.Nombre
                lblTitulo.CssClass = "margencito2"
                Dim oDiv As HtmlGenericControl = New HtmlGenericControl("div")
                oDiv.Attributes("class") = "margencito3"
                oDiv.Controls.Add(lblTitulo)
                oDiv.Controls.Add(galeria)
                'Page.Form.Controls.Add(oDiv)
                'Page.Form.Controls.Add(lblTitulo)
                Page.Form.Controls.Add(oDiv)
                'Dim query As String = "select distinct a.Nombre as Nombre, a.Descripcion as Descripcion, max(f.Foto) as url from crm_foto_album a inner join crm_foto_foto f on a.Id = f.AlbumId where a.Estado = 1 group by a.Nombre, a.Descripcion"
            Next
            'Dim listalbum = bcAlbum.ListByAlbum("select distinct a.Nombre as Nombre, a.Descripcion as Descripcion, max(f.Foto) as url from crm_foto_album a inner join crm_foto_foto f on a.Id = f.AlbumId where a.Estado = 1 group by a.Nombre, a.Descripcion")

            'rgvGrid.VirtualItemCount = bcAlbum.ListBuscarCount(rtbFilter.Text.Trim)
            'MyBase.proLoad_RadGrid(rgvGrid, ListAlbum)


        Catch ex As Exception
            proShow_Message(If(ex.InnerException IsNot Nothing, ex.InnerException.ToString, ex.Message))
        End Try
    End Sub
    'Private Sub LoadHotel(ByVal HotelId As Long)
    '    'Dim listAdjunto As List(Of BEB.HotelAdjunto) = New List(Of BEB.HotelAdjunto)
    '    Dim bcClasificadores As New BCB.Clasificadores
    '    Session("MensajeError") = "" 'MensajeError = ""
    '    If HotelId = 0 Then
    '        Session("AlbumId") = 0
    '        Session("NombreAlbum") = ""
    '        Session("DescripcionAlbum") = ""
    '        ClearFields()
    '        Session("listaFotos") = New List(Of BEB.Foto) ' listaHotelesAdjuntos = New List(Of BEB.HotelAdjunto)
    '        rgvAdjuntos.VirtualItemCount = CType(Session("listaFotos"), List(Of BEB.Foto)).Count 'rgvAdjuntos.VirtualItemCount = listaHotelesAdjuntos.Count
    '        Me.proLoad_RadGrid(rgvAdjuntos, CType(Session("listaFotos"), List(Of BEB.Foto))) 'Me.proLoad_RadGrid(rgvAdjuntos, listaHotelesAdjuntos)
    '    Else
    '        beAlbum = bcAlbum.ListById(HotelId)
    '        Session("listaFotos") = bcFoto.ListByAlbumId(HotelId) 'listaHotelesAdjuntos = bcHotelAdjunto.ListByHotel(HotelId, rgvAdjuntos.MasterTableView.CurrentPageIndex, rgvAdjuntos.MasterTableView.PageSize)
    '        With beAlbum
    '            Session("AlbumId") = .Id
    '            rtbNombre.Text = .Nombre
    '            rtbDescripcionAlbum.Text = .Descripcion
    '            Session("NombreAlbum") = .Nombre
    '            Session("DescripcionAlbum") = .Descripcion
    '            'If (Session("ListHotelPrograma") Is Nothing) Then
    '            '    Session("ListHotelPrograma") = New List(Of BEB.HotelPrograma)
    '            'End If
    '            'If (From it In CType(Session("ListHotelPrograma"), List(Of BEB.HotelPrograma)) Where it.HotelId = beAlbum.Id).ToList().Count = 0 Then 'If (From it In ListHotelPrograma Where it.HotelId = beHotel.Id).ToList().Count = 0 Then

    '            '    rcbCategoria.Enabled = True
    '            'Else
    '            '    rcbCategoria.Enabled = False
    '            'End If

    '            'rcbCategoria.SelectedValue = .CategoriaIdc
    '            'rcbSala.SelectedValue = .SalaId
    '            'rntDormitorio.Value = .Dormitorio
    '            'rntBano.Value = .Bano
    '            'rcbEstado.SelectedValue = beAlbum.EstadoHotel
    '            'rntMetrosCuadrados.Value = .MetroCuadrado
    '            'rntCamas.Value = .Cama
    '            'rtbServicioAdicional.Text = .ServicioAdicional
    '            'rcbEstado.SelectedValue = beHotel.EstadoHotel
    '        End With
    '        rgvAdjuntos.VirtualItemCount = CType(Session("listaFotos"), List(Of BEB.Foto)).Count ' rgvAdjuntos.VirtualItemCount = listaHotelesAdjuntos.Count
    '        Me.proLoad_RadGrid(rgvAdjuntos, CType(Session("listaFotos"), List(Of BEB.Foto))) 'Me.proLoad_RadGrid(rgvAdjuntos, listaHotelesAdjuntos)
    '    End If
    '    rtbNombre.Focus()
    'End Sub
    'Private Sub DeleteHotel(ByVal HotelId As Long)
    '    beAlbum = bcAlbum.ListById(HotelId)
    '    With beAlbum
    '        .FechaModificacion = Now
    '        .PersonalModificacionId = MyBase.funGet_UserCode
    '        .StatusType = BEntities.StatusType.Delete
    '        .Estado = BEntities.Estado.Inactivo
    '    End With
    '    bcAlbum.Save(beAlbum)
    '    ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "closeDialog('divDetail'); funShow_Message('Listado Actualizado', 'INFORMATION');", True)
    'End Sub
    'Private Sub SaveHotel()
    '    bcAlbum = New BCB.Album()
    '    beAlbum = bcAlbum.ListById(Session("AlbumId"))
    '    Try
    '        If (beAlbum Is Nothing) Then
    '            beAlbum = New BEB.Album()
    '        End If
    '        With beAlbum
    '            If CLng(Session("AlbumId")) = 0 Then
    '                .StatusType = BEntities.StatusType.Insert
    '                .Id = 0
    '                .FechaRegistro = Now
    '                .FechaModificacion = Now
    '                .PersonalId = MyBase.funGet_UserCode()
    '                .FechaRegistro = Now
    '            Else
    '                .StatusType = BEntities.StatusType.Update
    '                .Id = CInt(Session("AlbumId"))
    '                .FechaModificacion = Now
    '            End If

    '            .Nombre = rtbNombre.Text
    '            .Descripcion = rtbDescripcionAlbum.Text
    '            .PersonalModificacionId = funGet_UserCode()
    '            .Estado = BEntities.Estado.Activo

    '            .ListaFotos = CType(Session("listaFotos"), List(Of BEB.Foto)) '.ListadoAdjuntosHotel = listaHotelesAdjuntos
    '        End With


    '        Dim host As String = HttpContext.Current.Request.Url.Host
    '        Dim virtualPath As String = Hosting.HostingEnvironment.ApplicationVirtualPath
    '        Dim folderPath As String = virtualPath + "/assets/File/Fotos/" + rtbNombre.Text
    '        Dim folderPath2 As String = Server.MapPath("~/assets/File/Fotos/") + rtbNombre.Text

    '        If Not Directory.Exists(folderPath2) Then
    '            'If Directory (Folder) does not exists. Create it.
    '            Directory.CreateDirectory(folderPath2)
    '        End If
    '        For Each item In beAlbum.ListaFotos
    '            If item.Archivo IsNot Nothing Then

    '                Dim direccion = item.Foto

    '                Dim Bin As New MemoryStream(item.Archivo)
    '                'con el método FroStream de Image obtenemos imagen
    '                Dim Resultado As Image = Image.FromStream(Bin)

    '                File.WriteAllBytes(Server.MapPath(direccion), item.Archivo)
    '                Bin.Dispose()
    '            End If
    '        Next
    '        'RadAjaxPanel1.UploadedFiles(0).SaveAs(direccion, True)
    '        bcAlbum.Save(beAlbum)
    '        ClearFields()
    '    Catch ex As Exception
    '        'ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "closeDialog('divDetail'); funShow_Message('Error al Guardar, verifique lo siguiente: " & ex.Message & "', 'VALIDATION');", True)
    '        proShow_Message(If(ex.InnerException IsNot Nothing, ex.InnerException.ToString, ex.Message))
    '        ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "closeDialog('divDetail');", True)
    '    End Try
    'End Sub
    'Private Sub ClearFields()
    '    rtbNombre.Text = String.Empty
    '    rtbDescripcionAlbum.Text = String.Empty
    'End Sub
    'Private Sub rbFilter_Click(sender As Object, e As EventArgs) Handles rbFilter.Click
    '    rgvGrid.MasterTableView.CurrentPageIndex = 0
    '    proInit_Form()
    'End Sub

    'Private Sub rtbCancel_Click(sender As Object, e As EventArgs) Handles rtbCancel.Click
    '    ClearFields()
    '    Session("AlbumId") = 0
    '    Return
    'End Sub
    'Protected Sub rgvGrid_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles rgvGrid.NeedDataSource
    '    Try
    '        Dim bcClasificadores As New BCB.Clasificadores
    '        Dim ListHotel = bcAlbum.ListBuscar(rtbFilter.Text.Trim, rgvGrid.MasterTableView.CurrentPageIndex, rgvGrid.MasterTableView.PageSize)

    '        rgvGrid.DataSource = ListHotel

    '    Catch ex As Exception
    '        proShow_Message(If(ex.InnerException IsNot Nothing, ex.InnerException.ToString, ex.Message))
    '    End Try

    'End Sub


    'Public Sub OnUploadComplete(sender As Object, e As AjaxFileUploadEventArgs) Handles AjaxFileUpload11.UploadComplete
    '    Dim fileName As String = Path.GetFileName(e.FileName)
    '    'AjaxFileUpload11.SaveAs(Server.MapPath(Convert.ToString("~/assets/") & fileName))

    '    Dim host As String = HttpContext.Current.Request.Url.Host
    '    Dim virtualPath As String = Hosting.HostingEnvironment.ApplicationVirtualPath
    '    Dim folderPath As String = virtualPath + "/assets/File/Fotos/" + Session("NombreAlbum")
    '    Dim folderPath2 As String = Server.MapPath("~/assets/File/Fotos/") + Session("NombreAlbum")
    '    Try
    '        If Path.GetExtension(e.FileName).ToLower.Equals(".jpeg") Or Path.GetExtension(e.FileName).ToLower.Equals(".jpg") Or Path.GetExtension(e.FileName).ToLower.Equals(".pdf") Or Path.GetExtension(e.FileName).ToLower.Equals(".png") Or Path.GetExtension(e.FileName).ToLower.Equals(".gif") Then
    '            Dim imagenes As Byte()
    '            imagenes = e.GetContents
    '            'Dim archivo As Byte() = imagenes.ToArray
    '            Dim BEHotelesAdjuntos As BEB.Foto = New BEB.Foto
    '            With BEHotelesAdjuntos
    '                .StatusType = BEntities.StatusType.Insert
    '                .Id = 0
    '                .Nombre = e.FileName
    '                .Descripcion = rtbDescripcionAdjuntos.Text
    '                .Foto = folderPath & "/" & e.FileName
    '                .Archivo = imagenes
    '                .Estado = BEntities.Estado.Activo
    '                .FechaRegistro = Now
    '                .FechaModificacion = Now
    '                .PersonalId = funGet_UserCode()
    '                .PersonalModificacionId = funGet_UserCode()
    '            End With
    '            If Session("NuevaListaCargadaAdjuntos") Is Nothing Then
    '                Session("NuevaListaCargadaAdjuntos") = New List(Of BEB.Foto)
    '            End If
    '            CType(Session("NuevaListaCargadaAdjuntos"), List(Of BEB.Foto)).Add(BEHotelesAdjuntos) 'NuevaListaCargadaAdjuntos.Add(BEHotelesAdjuntos)
    '            If Session("ListaGlobal") Is Nothing Then
    '                Session("ListaGlobal") = New List(Of BEB.Foto)
    '            End If
    '            Session("ListaGlobal") = CType(Session("listaFotos"), List(Of BEB.Foto)) ' Session("ListaGlobal") = listaHotelesAdjuntos
    '            'Me.proLoad_RadGrid(rgvAdjuntos, listaHotelesAdjuntos)
    '            'bcHotelAdjunto.Save(listaHotelesAdjuntos.ElementAt(0))
    '        Else
    '            Session("MensajeError") = If(Session("MensajeError").ToString.Length = 0, Path.GetExtension(e.FileName), If(Session("MensajeError").ToString.Contains(Path.GetExtension(e.FileName)), Session("MensajeError").ToString, String.Concat(Session("MensajeError").ToString, ", ", Path.GetExtension(e.FileName)))) 'MensajeError = If(MensajeError.Length = 0, Path.GetExtension(e.FileName), If(MensajeError.Contains(Path.GetExtension(e.FileName)), MensajeError, String.Concat(MensajeError, ", ", Path.GetExtension(e.FileName))))
    '            'MyBase.proShow_Message("La importacion no permite el tipo de extension *" & Path.GetExtension(e.FileName))
    '        End If
    '    Catch ex As Exception
    '        MyBase.proShow_Message("La importacion no permite el tipo de extension *" & Path.GetExtension(e.FileName))
    '    End Try
    'End Sub

    'Private Sub rgvAdjuntos_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles rgvAdjuntos.ItemCommand
    '    Try

    '        Dim BePrivilegios As New BEntities.Seguridad.Privilegio
    '        Dim BcPrivilegios As New BComponents.Seguridad.Privilegio

    '        Dim beFormularios = bcFormulario.BuscarPorUnidadNegocio(BE.URLFormularios.Albunes.ToString, funGet_UnidadNegocioPadre)
    '        BePrivilegios = BcPrivilegios.BuscarPrivilegioPorPersonal(funGet_UserCode(), beFormularios.Id, funGet_UnidadNegocioPadre())
    '        If e.CommandName = "EliminarAdjunto" Then

    '            If BePrivilegios.Permiso.Contains("B") Then
    '                Dim item As GridDataItem = e.Item
    '                Dim index As Int32 = item.ItemIndex
    '                Dim AdjuntoId As String = item.GetDataKeyValue("Id").ToString()
    '                DeleteHotelAdjunto(CInt(AdjuntoId), index)
    '                'proInit_Form()
    '            Else
    '                ScriptManager.RegisterClientScriptBlock(Page,
    '                                                    Me.GetType,
    '                                                    "cerrarpopUp",
    '                                                    "funShow_Message('" & BePrivilegios.NoDelete & "', 'ERROR');",
    '                                                    True)
    '            End If
    '        End If
    '        If e.CommandName = "EditarDescripcion" Then

    '            If BePrivilegios.Permiso.Contains("M") Then
    '                Dim item As GridDataItem = e.Item
    '                Dim index As Int32 = item.ItemIndex
    '                Dim AdjuntoId As String = item.GetDataKeyValue("Id").ToString()
    '                Session("IdAdjunto") = index + (rgvAdjuntos.MasterTableView.CurrentPageIndex * rgvAdjuntos.MasterTableView.PageSize)
    '                ModificarDescripcion(CInt(AdjuntoId), index)
    '                'proInit_Form()
    '            Else
    '                ScriptManager.RegisterClientScriptBlock(Page,
    '                                                    Me.GetType,
    '                                                    "cerrarpopUp",
    '                                                    "funShow_Message('" & BePrivilegios.NoUpdate & "', 'ERROR');",
    '                                                    True)
    '            End If
    '        End If
    '        If e.CommandName = "Abrir" Then
    '            If BePrivilegios.Permiso.Contains("E") Then
    '                Dim item As GridDataItem = e.Item
    '                Dim index As Int32 = item.ItemIndex
    '                Dim AdjuntoId As String = item.GetDataKeyValue("Id").ToString()
    '                Session("IdAdjunto") = index + (rgvAdjuntos.MasterTableView.CurrentPageIndex * rgvAdjuntos.MasterTableView.PageSize)
    '                AbrirArchivo(CInt(AdjuntoId), index)

    '            Else
    '                ScriptManager.RegisterClientScriptBlock(Page,
    '                                                    Me.GetType,
    '                                                    "cerrarpopUp",
    '                                                    "funShow_Message('" & BePrivilegios.NoExport & "', 'ERROR');",
    '                                                    True)
    '            End If
    '        End If

    '    Catch ex As Exception
    '        proShow_Message(If(ex.InnerException IsNot Nothing, ex.InnerException.ToString, ex.Message))
    '    End Try

    'End Sub

    'Private Sub AbrirArchivo(v As Integer, index As Integer)
    '    'Dim strFilePath As String = listaHotelesAdjuntos.ElementAt(index + (rgvAdjuntos.MasterTableView.CurrentPageIndex * rgvAdjuntos.MasterTableView.PageSize)).Url
    '    'Dim fs As FileStream
    '    'If File.Exists(strFilePath) Then
    '    '_FileName = Report_file_name.Value
    '    'fs = File.Open(strFilePath, FileMode.Open)
    '    'Else
    '    '    '_FileName = Server.MapPath("EmptyFile.txt")
    '    '    fs = File.Open(strFilePath, FileMode.Open, FileAccess.Read)
    '    'End If
    '    'Dim bytBytes(fs.Length) As Byte
    '    'fs.Read(bytBytes, 0, fs.Length)
    '    'fs.Close()
    '    'fs.Dispose()


    '    Dim virtualPath As String = Hosting.HostingEnvironment.ApplicationVirtualPath
    '    Dim appName As String = virtualPath.Substring(1)
    '    Dim url = CType(Session("listaFotos"), List(Of BEB.Foto)).ElementAt(index + (rgvAdjuntos.MasterTableView.CurrentPageIndex * rgvAdjuntos.MasterTableView.PageSize)).Foto ' Dim url = listaHotelesAdjuntos.ElementAt(index + (rgvAdjuntos.MasterTableView.CurrentPageIndex * rgvAdjuntos.MasterTableView.PageSize)).Archivo
    '    Dim extencion As String = Path.GetExtension(CType(Session("listaFotos"), List(Of BEB.Foto)).ElementAt(index + (rgvAdjuntos.MasterTableView.CurrentPageIndex * rgvAdjuntos.MasterTableView.PageSize)).Foto) 'Dim extencion As String = Path.GetExtension(listaHotelesAdjuntos.ElementAt(index + (rgvAdjuntos.MasterTableView.CurrentPageIndex * rgvAdjuntos.MasterTableView.PageSize)).Url)
    '    'rbiImagen.DataValue = url

    '    ''Create a new window add it dynamically 
    '    ''The window will inherit the default settings of parent WindowManager 
    '    'Dim newWindow As New Telerik.Web.UI.RadWindow()
    '    'newWindow.NavigateUrl = "data:application/pdf;base64," & System.Text.Encoding.UTF8.GetString(url)
    '    ''Top and Left can be used in conjunction with the OffsetElementId (if no OffsetElementId is specified, the top left corner of the browser window is used 
    '    'newWindow.Top = Unit.Pixel(22)
    '    'newWindow.Left = Unit.Pixel(0)
    '    ''OPTION 1 
    '    ''Add the newly created RadWindow to the RadWindowManager's collection 
    '    ''WindowManager.Windows.Add(newWindow) 
    '    ''OPTION 2 
    '    ''since in the for ASP.NET AJAX version you can have a RadWindow outside of a RadWindowManager 
    '    ''as a separate control, you can add the newly created RadWindow directly to the form's Controls collection 
    '    'form1.Controls.Add(newWindow)
    '    'RadWindow2.NavigateUrl = "data:application/pdf;base64," & System.Text.Encoding.UTF8.GetString(url)
    '    ''Dim aqui As String = "Open('data:application/pdf;base64," & System.Text.Encoding.UTF8.GetString(url) & "');"
    '    'ScriptManager.RegisterClientScriptBlock(Page, Me.GetType, "cerrarpopUp", "Open();", True)
    '    'Page.Response.BinaryWrite(listaHotelesAdjuntos.ElementAt(index + (rgvAdjuntos.MasterTableView.CurrentPageIndex * rgvAdjuntos.MasterTableView.PageSize)).Archivo)
    '    'Page.Response.Flush()
    '    'Page.Response.End()


    '    'Response.Clear()
    '    'Response.ContentType = "application/pdf"
    '    'Response.BinaryWrite(url)
    '    'Response.Flush()
    '    Dim Script As String = ""
    '    If (extencion.Contains("pdf")) Then
    '        Script = "window.radopen('" & url & "', 'RadWindow2');"
    '        ScriptManager.RegisterClientScriptBlock(Page, Me.GetType, "PDF", Script, True)
    '    ElseIf (extencion.Contains("jpg")) Then
    '        Script = "window.radopen('" & url & "', 'RadWindow2');"
    '        ScriptManager.RegisterClientScriptBlock(Page, Me.GetType, "JPG", Script, True)
    '    ElseIf (extencion.Contains("gif")) Then
    '        Script = "window.radopen('" & url & "', 'RadWindow2');"
    '        ScriptManager.RegisterClientScriptBlock(Page, Me.GetType, "GIF", Script, True)
    '    ElseIf (extencion.Contains("png")) Then
    '        Script = "window.radopen('" & url & "', 'RadWindow2');"
    '        ScriptManager.RegisterClientScriptBlock(Page, Me.GetType, "PNG", Script, True)
    '    ElseIf (extencion.Contains("jpeg")) Then
    '        Script = "window.radopen('" & url & "', 'RadWindow2');"
    '        ScriptManager.RegisterClientScriptBlock(Page, Me.GetType, "JPEG", Script, True)
    '        'ElseIf (extencion.Contains("doc")) Then
    '        '    'Script = "window.radopen('data:application/msword;base64," & System.Convert.ToBase64String(url) & "', 'RadWindow2');"
    '        '    'ScriptManager.RegisterClientScriptBlock(Page, Me.GetType, "Word", Script, True)
    '        '    'Me.Application.Documents.Open(FileName:="C:\Test\NewDocument.docx", ReadOnly:=True)
    '        '    Dim words As Document = New Document
    '        '    words.Application.Documents.Open(url, [ReadOnly]:=True).PrintPreview()
    '        'ElseIf (extencion.Contains("xls")) Then
    '        '    'Script = "window.radopen('data:application/msword;base64," & System.Convert.ToBase64String(url) & "', 'RadWindow2');"
    '        '    'ScriptManager.RegisterClientScriptBlock(Page, Me.GetType, "Word", Script, True)
    '        '    'Me.Application.Documents.Open(FileName:="C:\Test\NewDocument.docx", ReadOnly:=True)
    '        '    'Dim excels As Microsoft.Office.Interop.Excel.Application = New Microsoft.Office.Interop.Excel.Application
    '        '    'excels.Application.Documents.Open(url, [ReadOnly]:=True).PrintPreview()


    '        '    Dim xlApp As Microsoft.Office.Interop.Excel.Application
    '        '    Dim xlWorkBook As Microsoft.Office.Interop.Excel.Workbook
    '        '    Dim xlWorkSheet As Microsoft.Office.Interop.Excel.Worksheet

    '        '    xlApp = New Excel.Application

    '        '    xlWorkBook = xlApp.Workbooks.Open("data:application/vnd.ms-excel;base64," & System.Convert.ToBase64String(url) & "'", [ReadOnly]:=True)
    '        '    xlWorkSheet = xlWorkBook.Worksheets("Sheet1")
    '        '    xlApp.Visible = True

    '        '    '~~> Print Preview
    '        '    xlWorkBook.PrintPreview()

    '        '    MsgBox("Wait")

    '        '    xlWorkBook.Close(SaveChanges:=False)
    '        '    xlApp.Quit()

    '        '    'releaseObject(xlApp)
    '        '    'releaseObject(xlWorkBook)
    '        '    'releaseObject(xlWorkSheet)

    '    Else
    '        MyBase.proShow_Message("Formato de archivo no valido")
    '    End If

    'End Sub

    'Private Sub ModificarDescripcion(v As Integer, index As Integer)
    '    rtbDescripcionModificar.Text = CType(Session("listaFotos"), List(Of BEB.Foto)).ElementAt(index + (rgvAdjuntos.MasterTableView.CurrentPageIndex * rgvAdjuntos.MasterTableView.PageSize)).Descripcion ' rtbDescripcionModificar.Text = listaHotelesAdjuntos.ElementAt(index + (rgvAdjuntos.MasterTableView.CurrentPageIndex * rgvAdjuntos.MasterTableView.PageSize)).Descripcion
    'End Sub

    'Private Sub DeleteHotelAdjunto(v As Int32, index As Int32)
    '    If (v = 0) Then
    '        CType(Session("listaFotos"), List(Of BEB.Foto)).RemoveAt(index + (rgvAdjuntos.MasterTableView.CurrentPageIndex * rgvAdjuntos.MasterTableView.PageSize)) ' listaHotelesAdjuntos.RemoveAt(index + (rgvAdjuntos.MasterTableView.CurrentPageIndex * rgvAdjuntos.MasterTableView.PageSize))
    '    Else
    '        beFoto = New BEB.Foto
    '        beFoto = CType(Session("listaFotos"), List(Of BEB.Foto)).ElementAt(index + (rgvAdjuntos.MasterTableView.CurrentPageIndex * rgvAdjuntos.MasterTableView.PageSize)) 'beHotelAdjunto = listaHotelesAdjuntos.ElementAt(index + (rgvAdjuntos.MasterTableView.CurrentPageIndex * rgvAdjuntos.MasterTableView.PageSize))
    '        With beFoto
    '            .Id = v
    '            .PersonalId = MyBase.funGet_UserCode
    '            .StatusType = BEntities.StatusType.Delete
    '            .Estado = BEntities.Estado.Inactivo
    '            .FechaModificacion = Now
    '        End With
    '        bcFoto.Save(beFoto)
    '        CType(Session("listaFotos"), List(Of BEB.Foto)).RemoveAt(index + (rgvAdjuntos.MasterTableView.CurrentPageIndex * rgvAdjuntos.MasterTableView.PageSize)) ' listaHotelesAdjuntos.RemoveAt(index + (rgvAdjuntos.MasterTableView.CurrentPageIndex * rgvAdjuntos.MasterTableView.PageSize))
    '    End If
    '    rgvAdjuntos.VirtualItemCount = CType(Session("listaFotos"), List(Of BEB.Foto)).Count ' rgvAdjuntos.VirtualItemCount = listaHotelesAdjuntos.Count
    '    MyBase.proLoad_RadGrid(rgvAdjuntos, CType(Session("listaFotos"), List(Of BEB.Foto))) 'MyBase.proLoad_RadGrid(rgvAdjuntos, listaHotelesAdjuntos)
    '    ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", "funShow_Message('Adjunto eliminado', 'INFORMATION');", True)
    'End Sub

    'Private Sub rbActualizarTabla_Click(sender As Object, e As EventArgs) Handles rbActualizarTabla.Click
    '    Try
    '        If (Session("NuevaListaCargadaAdjuntos") Is Nothing) Then
    '            Session("NuevaListaCargadaAdjuntos") = New List(Of BEB.Foto)
    '        End If

    '        For Each item In CType(Session("NuevaListaCargadaAdjuntos"), List(Of BEB.Foto)) ' For Each item In NuevaListaCargadaAdjuntos
    '            item.Descripcion = rtbDescripcionAdjuntos.Text
    '            CType(Session("listaFotos"), List(Of BEB.Foto)).Add(item) 'listaHotelesAdjuntos.Add(item)
    '        Next
    '        Session("NuevaListaCargadaAdjuntos") = New List(Of BEB.Foto) ' NuevaListaCargadaAdjuntos = New List(Of BEB.HotelAdjunto)
    '        rgvAdjuntos.VirtualItemCount = CType(Session("listaFotos"), List(Of BEB.Foto)).Count 'rgvAdjuntos.VirtualItemCount = listaHotelesAdjuntos.Count
    '        MyBase.proLoad_RadGrid(rgvAdjuntos, CType(Session("listaFotos"), List(Of BEB.Foto))) ' MyBase.proLoad_RadGrid(rgvAdjuntos, listaHotelesAdjuntos)
    '        rtbDescripcionAdjuntos.Text = String.Empty
    '        If (Session("MensajeError").ToString.Length > 0) Then 'If (MensajeError.Length > 0) Then
    '            MyBase.proShow_Message("La importacion no permite el tipo de extension *" & Session("MensajeError").ToString) 'MyBase.proShow_Message("La importacion no permite el tipo de extension *" & MensajeError)
    '        End If
    '        Session("MensajeError") = "" 'MensajeError = ""

    '    Catch ex As Exception
    '        proShow_Message(If(ex.InnerException IsNot Nothing, ex.InnerException.ToString, ex.Message))
    '    End Try

    'End Sub

    'Private Sub rbGuardarDescripcion_Click(sender As Object, e As EventArgs) Handles rbGuardarDescripcion.Click
    '    Try
    '        CType(Session("listaFotos"), List(Of BEB.Foto)).ElementAt(Session("IdAdjunto")).Descripcion = rtbDescripcionModificar.Text 'listaHotelesAdjuntos.ElementAt(Session("IdAdjunto")).Descripcion = rtbDescripcionModificar.Text
    '        CType(Session("listaFotos"), List(Of BEB.Foto)).ElementAt(Session("IdAdjunto")).StatusType = BEntities.StatusType.Update 'listaHotelesAdjuntos.ElementAt(Session("IdAdjunto")).StatusType = BEntities.StatusType.Update
    '        rgvAdjuntos.VirtualItemCount = CType(Session("listaFotos"), List(Of BEB.Foto)).Count 'rgvAdjuntos.VirtualItemCount = listaHotelesAdjuntos.Count
    '        MyBase.proLoad_RadGrid(rgvAdjuntos, CType(Session("listaFotos"), List(Of BEB.Foto))) ' MyBase.proLoad_RadGrid(rgvAdjuntos, listaHotelesAdjuntos)
    '        ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp2", "closeDialog('divDescripcion'); funShow_Message('Descripción actualizada', 'INFORMATION');", True)

    '    Catch ex As Exception
    '        proShow_Message(If(ex.InnerException IsNot Nothing, ex.InnerException.ToString, ex.Message))
    '    End Try

    'End Sub

    'Private Sub rtbNombre_TextChanged(sender As Object, e As EventArgs) Handles rtbNombre.TextChanged
    '    Session("NombreAlbum") = rtbNombre.Text
    'End Sub
End Class