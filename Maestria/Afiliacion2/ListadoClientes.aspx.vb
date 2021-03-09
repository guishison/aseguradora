Imports Telerik.Web.UI
Imports BEM = BEntities.Base
Imports BEC = BComponents.Base
Imports System.IO

Public Class ListadoClientes
    Inherits UtilsMethods

    Private bcRegistroFacturacion As BEC.RegistroFacturacion = New BEC.RegistroFacturacion
    Protected Overloads Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            proInit_Form()
        End If
    End Sub
    Protected Sub rgvGrid_PageIndexChanged(sender As Object, e As GridPageChangedEventArgs) Handles rgvGrid.PageIndexChanged
        sender.CurrentPageIndex = e.NewPageIndex
        'proInit_Form()
        buscartext()
    End Sub
    Private Sub proInit_Form()
        'Dim listMoneda As List(Of BEV.Moneda) = bcMoneda.List()
        'Dim listSala As List(Of BEV.Sala) = bcSala.List()
        'Dim lstProviders As List(Of BEM.Caja) = bcProviders.List(BEntities.Estado.Activo, rgvGrid.MasterTableView.CurrentPageIndex, rgvGrid.MasterTableView.PageSize, BEM.relCaja.Sala)
        'MyBase.proLoad_RadComboBox(rcbSala, listSala, "Id", "Nombre")
        'MyBase.proLoad_RadComboBox(rcbMoneda, listMoneda, "Id", "Nombre")
        'MyBase.proLoad_RadGrid(rgvGrid, lstProviders)
        Try
            buscartext()
        Catch ex As Exception
            MyBase.proShow_Message(ex.Message)
        End Try
    End Sub
    Private Sub buscartext()
        Dim text As String = rtbFilter.Text
        Dim lstRegistroFacturacion As List(Of BEM.RegistroFacturacion) = bcRegistroFacturacion.ListBuscar(text.Trim, rgvGrid.MasterTableView.CurrentPageIndex, rgvGrid.MasterTableView.PageSize)
        rgvGrid.VirtualItemCount = bcRegistroFacturacion.ListBuscarCount(text)
        MyBase.proLoad_RadGrid(rgvGrid, lstRegistroFacturacion)
    End Sub

    Private Sub rbFilter_Click(sender As Object, e As EventArgs) Handles rbFilter.Click
        buscartext()
    End Sub

    Private Sub rtbSave_Click(sender As Object, e As EventArgs) Handles rtbSave.Click
        Dim beRegistroFacturacion = bcRegistroFacturacion.ListById(ViewState("Id"))
        beRegistroFacturacion.StatusType = BEntities.StatusType.Update
        beRegistroFacturacion.EstadoSolicitud = rcbEstadoCliente.SelectedValue
        bcRegistroFacturacion.Save(beRegistroFacturacion)
        ScriptManager.RegisterClientScriptBlock(Page, Me.GetType, "IMAGEN", "closeDialog('divDetail'); funShow_Message('Cambio Guardado', 'INFORMATION');", True)
        buscartext()
    End Sub

    Private Sub rgvGrid_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles rgvGrid.ItemCommand
        Try

            If (e.CommandName <> "Page") Then

                Dim item As GridDataItem = e.Item
                Dim RegistroFacturacionId As String = item.GetDataKeyValue("Id").ToString()
            ViewState("Id") = Int(RegistroFacturacionId)
            Dim beRegistroFacturacion = bcRegistroFacturacion.ListById(RegistroFacturacionId)
                If e.CommandName = "CambioEstado" Then
                    rcbEstadoCliente.SelectedValue = beRegistroFacturacion.EstadoSolicitud
                ElseIf e.CommandName = "VerImagenLogoEmpresa" Then
                    'En caso que que no se encuentren instancias de caja se procede a eliminar la caja estado 1 -> 0
                    AbrirLogoEmpresa(beRegistroFacturacion)
                    'buscartext()
                    'proInit_Form()
                ElseIf e.CommandName = "VerImagenFotoNIT" Then
                    'En caso que que no se encuentren instancias de caja se procede a eliminar la caja estado 1 -> 0
                    'CambiarEstado()
                    'buscartext()
                    AbrirFotoNit(beRegistroFacturacion)
                    'proInit_Form()
                End If
            End If
        Catch ex As Exception
            MyBase.proShow_Message(ex.Message)
        End Try
    End Sub

    Private Sub AbrirFotoNit(beRegistroFacturacion As BEM.RegistroFacturacion)
        Dim virtualPath As String = Hosting.HostingEnvironment.ApplicationVirtualPath
        Dim appName As String = virtualPath.Substring(1)

        Dim url = beRegistroFacturacion.FotoNit
        Dim extencion As String = Path.GetExtension(url)

        Dim Script As String = ""
        'If (extencion.ToLower.Contains("pdf")) Then
        Script = "OpenWindow('" + url.Replace("\", "/") + "');"
        ''Script = "window.radopen('https://i.pinimg.com/originals/6b/4c/ce/6b4cceefef3586a8eea6b80c35747fa4.png', 'RadWindow2');"
        'Script = "OpenWindow('https://i.pinimg.com/originals/6b/4c/ce/6b4cceefef3586a8eea6b80c35747fa4.png');"
        ScriptManager.RegisterClientScriptBlock(Page, Me.GetType, "IMAGEN", Script, True)
    End Sub

    Private Sub AbrirLogoEmpresa(beRegistroFacturacion As BEM.RegistroFacturacion)
        Dim virtualPath As String = Hosting.HostingEnvironment.ApplicationVirtualPath
        Dim appName As String = virtualPath.Substring(1)

        Dim url = beRegistroFacturacion.Logo
        Dim extencion As String = Path.GetExtension(url)

        Dim Script As String = ""
        'If (extencion.ToLower.Contains("pdf")) Then
        'Script = "window.radopen('" + url.Replace("\", "/") + "', 'RadWindow2');"
        Script = "OpenWindow('" + url.Replace("\", "/") + "');"
        ScriptManager.RegisterClientScriptBlock(Page, Me.GetType, "IMAGEN", Script, True)
        'ElseIf (extencion.ToLower.Contains("jpg")) Then
        '    Script = "window.radopen('data:image/jpg;base64," & System.Convert.ToBase64String(url) & "', 'RadWindow2');"
        '    ScriptManager.RegisterClientScriptBlock(Page, Me.GetType, "JPG", Script, True)
        'ElseIf (extencion.ToLower.Contains("gif")) Then
        '    Script = "window.radopen('data:image/gif;base64," & System.Convert.ToBase64String(url) & "', 'RadWindow2');"
        '    ScriptManager.RegisterClientScriptBlock(Page, Me.GetType, "GIF", Script, True)
        'ElseIf (extencion.ToLower.Contains("png")) Then
        '    Script = "window.radopen('data:image/png;base64," & System.Convert.ToBase64String(url) & "', 'RadWindow2');"
        '    ScriptManager.RegisterClientScriptBlock(Page, Me.GetType, "PNG", Script, True)
        'ElseIf (extencion.ToLower.Contains("jpeg")) Then
        '    Script = "window.radopen('data:image/jpeg;base64," & System.Convert.ToBase64String(url) & "', 'RadWindow2');"
        '    ScriptManager.RegisterClientScriptBlock(Page, Me.GetType, "JPEG", Script, True)

        'Else
        '    MyBase.proShow_Message("Formato de archivo no valido")
        'End If
    End Sub

    Private Sub rgvGrid_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles rgvGrid.ItemDataBound
        If TypeOf e.Item Is GridDataItem Then
            Dim row As GridDataItem = CType(e.Item, GridDataItem)

            Try
                Dim dates2 As String = (row("Fecha").Text)
                Dim dates As DateTime = DateTime.Parse(row("Fecha").Text)
                Dim DateExpLabel As Label = CType(row.FindControl("lblexDate"), Label)
                Dim totalmili As Long = (dates.ToUniversalTime() - New DateTime(1970, 1, 1)).TotalMilliseconds
                'DateExpLabel.Text = totalmili.ToString()
                'Dim toto = Convert.ToDateTime(totalmili)

                Dim dateJan1st1970 As New DateTime(1970, 1, 1, 0, 0, 0)
                Dim dateNew As DateTime = dateJan1st1970.AddMilliseconds(totalmili).AddHours(-4)
                'Dim val = (New DateTime(DateTimePicker1.Value.Year, DateTimePicker1.Value.Month, DateTimePicker1.Value.Day, CInt(TextBox1.Text), CInt(TextBox2.Text), 0) - New DateTime(1970, 1, 1, 0, 0, 0)).TotalMilliseconds
                row("Fecha").Text = dateNew.ToString("dd/MM/yyyy HH:mm")

            Catch ex As Exception
                Dim asas = ex.Message
            Finally
            End Try
        End If
    End Sub
End Class