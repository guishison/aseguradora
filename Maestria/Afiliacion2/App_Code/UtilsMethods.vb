'------------------------------------------------------------------------------------------------------------------
' Realizado Por:             Three & One
' Empresa:                   AVATECH
' Version:                   1.1.0.0
' Nombre Proyecto:           Presentacion
' Nombre del Archivo:        clsHelper.vb
' Proposito:                 Clase que contiene todos los metodos base de inicialización/carga a los controles
'------------------------------------------------------------------------------------------------------------------

Imports System
Imports System.Web.UI
Imports System.Data
Imports System.Collections.Generic
Imports System.Threading
Imports System.Reflection
Imports System.Web.UI.WebControls
Imports Microsoft.VisualBasic
Imports Telerik.Web.UI
Imports BCL = BComponents
Imports System.Globalization
Imports System.IO
Imports Microsoft
Imports BComponents
Imports System.Web.Security
Imports System.Web

Public MustInherit Class UtilsMethods
    Inherits System.Web.UI.Page

#Region " Enums "

    ''' <summary>
    ''' Enumerated for the ComboBox
    ''' </summary>
    ''' <remarks></remarks>
    Enum DefaultData
        Unselected = 1
        All = 2
        None = 3
        All2 = 4
        All3 = 5
    End Enum

    Enum enuTypeMessage As Byte

        Confirmation = 1
        Exception = 2
        Information = 3
        Security = 4
        Validation = 5

    End Enum

    Enum enuPermissionLevel As Byte
        Deny
        Read
        Write
    End Enum

#End Region

#Region " Globalization Methods "

    ' ''' <summary>
    ' ''' Gets a custom formated number
    ' ''' </summary>
    ' ''' <param name="p_decNumber">Number to format</param>
    ' ''' <param name="p_enuType">Format type to apply</param>
    ' ''' <param name="p_bitDecimalDigits">[Optional] Number of decimal digits</param>
    ' ''' <returns>Formated string</returns>
    ' ''' <remarks>p_bitDecimalDigits is not supported by all types</remarks>
    'Public Function funGet_Number(ByVal p_decNumber As Decimal, ByVal p_enuType As enuCultureNumberFormat, Optional ByVal p_bitDecimalDigits As Byte = 2) As String
    '    Return CultureConfiguration.funGet_Number(p_decNumber, p_enuType, p_bitDecimalDigits)
    'End Function

    ' ''' <summary>
    ' ''' Gets a custom formated date
    ' ''' </summary>
    ' ''' <param name="p_datDate">Date to format</param>
    ' ''' <param name="p_enuType">Format type to apply</param>
    ' ''' <returns>Formated string</returns>
    ' ''' <remarks></remarks>
    'Public Function funGet_Date(ByVal p_datDate As Date, ByVal p_enuType As enuCultureDateFormat) As String
    '    Return CultureConfiguration.funGet_Date(p_datDate, p_enuType)
    'End Function

    ' ''' <summary>
    ' ''' Gets a custom formated time from a datetime value
    ' ''' </summary>
    ' ''' <param name="p_datTime">Time to format</param>
    ' ''' <param name="p_enuType">Format type to apply </param>
    ' ''' <returns>Formated string</returns>
    ' ''' <remarks></remarks>
    'Public Function funGet_Time(ByVal p_datTime As Date, ByVal p_enuType As enuCultureTimeFormat) As String
    '    Return CultureConfiguration.funGet_Time(p_datTime, p_enuType)
    'End Function

    Protected Function GetHoraBoliviana(ByVal fecha As DateTime) As DateTime
        Dim serverTime As DateTime = fecha
        Dim _localTime As DateTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(serverTime, TimeZoneInfo.Local.Id, "SA Western Standard Time")
        Return _localTime
    End Function
#End Region

#Region " Telerik Controls Methods "

    ''' <summary>
    '''  Metodo que carga un RadGrid con un Listado de Entidades
    ''' </summary>
    ''' <param name="p_objGrid">Recibe el RadGrid al que se cargarán los datos</param>
    ''' <param name="p_lstItems">Recibe el Objeto que contiene el listado de entidades</param>
    ''' <remarks></remarks>
    ''' 

    Public Sub proLoad_RadGrid3(Of T)(ByRef p_objGrid As RadGrid, ByVal p_lstItems As List(Of T), ByVal fecha As DateTime)
        p_objGrid.DataSource = p_lstItems

        p_objGrid.MasterTableView.Columns.FindByUniqueName("UPSAnt").HeaderText = fecha.Year - 1
        p_objGrid.MasterTableView.Columns.FindByUniqueName("UPSNow").HeaderText = fecha.Year

        p_objGrid.MasterTableView.Columns.FindByUniqueName("QAnt").HeaderText = fecha.Year - 1
        p_objGrid.MasterTableView.Columns.FindByUniqueName("QNow").HeaderText = fecha.Year


        p_objGrid.MasterTableView.Columns.FindByUniqueName("NQAnt").HeaderText = fecha.Year - 1
        p_objGrid.MasterTableView.Columns.FindByUniqueName("NQNow").HeaderText = fecha.Year



        p_objGrid.MasterTableView.Columns.FindByUniqueName("NTNIAnt").HeaderText = fecha.Year - 1
        p_objGrid.MasterTableView.Columns.FindByUniqueName("NTNINow").HeaderText = fecha.Year



        p_objGrid.MasterTableView.Columns.FindByUniqueName("MCAnt").HeaderText = fecha.Year - 1
        p_objGrid.MasterTableView.Columns.FindByUniqueName("MCNow").HeaderText = fecha.Year




        'p_objGrid.MasterTableView.ColumnGroups.FindGroupByName("DIA1").HeaderText = fecha.ToString("yyyy-MM-dd")
        'p_objGrid.MasterTableView.ColumnGroups.FindGroupByName("DIA2").HeaderText = fecha.Add(New TimeSpan(1, 0, 0, 0)).ToString("yyyy-MM-dd")
        'p_objGrid.MasterTableView.ColumnGroups.FindGroupByName("DIA3").HeaderText = fecha.Add(New TimeSpan(2, 0, 0, 0)).ToString("yyyy-MM-dd")
        'p_objGrid.MasterTableView.ColumnGroups.FindGroupByName("DIA4").HeaderText = fecha.Add(New TimeSpan(3, 0, 0, 0)).ToString("yyyy-MM-dd")
        'p_objGrid.MasterTableView.ColumnGroups.FindGroupByName("DIA5").HeaderText = fecha.Add(New TimeSpan(4, 0, 0, 0)).ToString("yyyy-MM-dd")
        'p_objGrid.MasterTableView.ColumnGroups.FindGroupByName("DIA6").HeaderText = fecha.Add(New TimeSpan(5, 0, 0, 0)).ToString("yyyy-MM-dd")
        'p_objGrid.MasterTableView.ColumnGroups.FindGroupByName("DIA7").HeaderText = fecha.Add(New TimeSpan(6, 0, 0, 0)).ToString("yyyy-MM-dd")
        'p_objGrid.MasterTableView.ColumnGroups.FindGroupByName("DIA8").HeaderText = fecha.Add(New TimeSpan(7, 0, 0, 0)).ToString("yyyy-MM-dd")
        'p_objGrid.MasterTableView.ColumnGroups.FindGroupByName("DIA9").HeaderText = fecha.Add(New TimeSpan(8, 0, 0, 0)).ToString("yyyy-MM-dd")
        'p_objGrid.MasterTableView.ColumnGroups.FindGroupByName("DIA10").HeaderText = fecha.Add(New TimeSpan(9, 0, 0, 0)).ToString("yyyy-MM-dd")
        'p_objGrid.MasterTableView.ColumnGroups.FindGroupByName("DIA11").HeaderText = fecha.Add(New TimeSpan(10, 0, 0, 0)).ToString("yyyy-MM-dd")
        'p_objGrid.MasterTableView.ColumnGroups.FindGroupByName("DIA12").HeaderText = fecha.Add(New TimeSpan(11, 0, 0, 0)).ToString("yyyy-MM-dd")
        'p_objGrid.MasterTableView.ColumnGroups.FindGroupByName("DIA13").HeaderText = fecha.Add(New TimeSpan(12, 0, 0, 0)).ToString("yyyy-MM-dd")
        'p_objGrid.MasterTableView.ColumnGroups.FindGroupByName("DIA14").HeaderText = fecha.Add(New TimeSpan(13, 0, 0, 0)).ToString("yyyy-MM-dd")
        'p_objGrid.MasterTableView.ColumnGroups.FindGroupByName("DIA15").HeaderText = fecha.Add(New TimeSpan(14, 0, 0, 0)).ToString("yyyy-MM-dd")



        ' p_objGrid.MasterTableView.Columns.FindByUniqueName("CantPersonas").Display = False
        p_objGrid.DataBind()
    End Sub

    Public Sub proLoad_RadGrid4(Of T)(ByRef p_objGrid As RadGrid, ByVal p_lstItems As List(Of T), ByVal fecha As DateTime)
        p_objGrid.DataSource = p_lstItems

        p_objGrid.MasterTableView.Columns.FindByUniqueName("AntQUPs").HeaderText = fecha.Year - 1
        p_objGrid.MasterTableView.Columns.FindByUniqueName("QUPSNow").HeaderText = fecha.Year

        p_objGrid.MasterTableView.Columns.FindByUniqueName("AntVentaQ").HeaderText = fecha.Year - 1
        p_objGrid.MasterTableView.Columns.FindByUniqueName("VentaQNow").HeaderText = fecha.Year


        p_objGrid.MasterTableView.Columns.FindByUniqueName("AntVentaUPS").HeaderText = fecha.Year - 1
        p_objGrid.MasterTableView.Columns.FindByUniqueName("VentaUPSNow").HeaderText = fecha.Year


        p_objGrid.DataBind()
    End Sub




    Public Sub proLoad_RadGrid(Of T)(ByRef p_objGrid As RadGrid, ByVal p_lstItems As List(Of T))
        p_objGrid.DataSource = p_lstItems
        p_objGrid.DataBind()
    End Sub

    ''' <summary>
    ''' Metodo que carga un control RadComboBox con un Listado de Entidades
    ''' </summary>
    ''' <param name="p_objComboBox">Recibe el control RadComboBox por referencia</param>
    ''' <param name="p_lstItems">Recibe el Objeto que contiene el listado de entidades</param>
    ''' <param name="p_strTextField">Recibe el Nombre del Texto</param>
    ''' <param name="p_strValueField">Recibe el Nombre del ID</param>
    ''' <remarks></remarks>
    Public Sub proLoad_RadComboBox(Of T)(ByRef p_objComboBox As RadComboBox, ByVal p_lstItems As List(Of T), ByVal p_strValueField As String, ByVal p_strTextField As String,
                                         Optional ByVal enuFirstField As DefaultData = DefaultData.Unselected)

        p_objComboBox.Items.Clear()
        p_objComboBox.DataTextField = p_strTextField
        p_objComboBox.DataValueField = p_strValueField
        p_objComboBox.ClearSelection()
        p_objComboBox.DataSource = p_lstItems
        p_objComboBox.DataBind()
        Dim liItem As New RadComboBoxItem
        liItem.Value = -1
        Select Case enuFirstField
            Case DefaultData.Unselected
                liItem.Text = "Seleccione..."
                liItem.Selected = True
                p_objComboBox.Items.Insert(0, liItem)
            Case DefaultData.All
                liItem.Text = "Todos"
                p_objComboBox.Items.Insert(0, liItem)
            Case DefaultData.None
                'Does not add a first item
            Case DefaultData.All2
                liItem.Text = "Seleccione..."
                p_objComboBox.Items.Insert(0, liItem)
                liItem.Value = 0
                Dim liItem2 As New RadComboBoxItem
                liItem2.Text = "Todos"
                liItem2.Value = 1
                p_objComboBox.Items.Insert(1, liItem2)
            Case DefaultData.All3
                liItem.Text = "Seleccione..."
                p_objComboBox.Items.Insert(0, liItem)
                liItem.Value = -1
                Dim liItem2 As New RadComboBoxItem
                liItem2.Text = "All"
                liItem2.Value = 0
                p_objComboBox.Items.Insert(1, liItem2)
        End Select
        'Set to the default value
        p_objComboBox.SelectedIndex = 0
    End Sub
    Public Function ObtenerNavegador() As String
        Dim s As String = ""
        s = Request.UserHostAddress() & vbCrLf
        With HttpContext.Current.Request.Browser
            s &= HttpContext.Current.Request.ServerVariables("HTTP_USER_AGENT") & vbCrLf
            s &= "Capacidades del Browser" & vbCrLf
            s &= "Tipo = " & .Type & vbCrLf
            s &= "Nombre = " & .Browser & vbCrLf
            s &= "Version = " & .Version & vbCrLf
            s &= "Mayor Version = " & .MajorVersion & vbCrLf
            s &= "Menor Version = " & .MinorVersion & vbCrLf
            s &= "Plataforma = " & .Platform & vbCrLf
            s &= "Es Beta = " & .Beta & vbCrLf
            s &= "Es Crawler = " & .Crawler & vbCrLf
            s &= "Es AOL = " & .AOL & vbCrLf
            s &= "Es Win16 = " & .Win16 & vbCrLf
            s &= "Es Win32 = " & .Win32 & vbCrLf
            s &= "Soporta Frames = " & .Frames & vbCrLf
            s &= "Soporta Tablas = " & .Tables & vbCrLf
            s &= "Soporta Cookies = " & .Cookies & vbCrLf
            s &= "Soporta VBScript = " & .VBScript & vbCrLf
            s &= "Soporta JavaScript = " &
                .EcmaScriptVersion.ToString() & vbCrLf
            s &= "Soporta Java Applets = " & .JavaApplets & vbCrLf
            s &= "Soporta ActiveX Controls = " & .ActiveXControls &
                vbCrLf
        End With
        Return s
    End Function
    Public Function ObtenerSistemaOperativo() As String
        'Environment.OSVersion.ToString()
        Dim SO As String = HttpContext.Current.Request.Browser.Platform + " Logeado con: " + HttpContext.Current.Request.LogonUserIdentity.Name + " ++ " + HttpContext.Current.Request.LogonUserIdentity.AuthenticationType + " ++ " + HttpContext.Current.Request.LogonUserIdentity.User.Value
        Return SO
    End Function
    Public Function ObtenerUrl() As String
        Dim Url As String = HttpContext.Current.Request.Url.AbsoluteUri
        Return Url
    End Function
    Public Function ObtenerVersion() As String
        Dim Versions = DirectCast(Me.Master.FindControl("lblVersion"), Label)
        Dim lblComentario = DirectCast(Me.Master.FindControl("lblComentario"), Label)
        Return Versions.Text + ". Comentario: " + lblComentario.Text
    End Function

    Public Function ObtenerMetodo() As String
        Dim st As New System.Diagnostics.StackTrace()
        Dim nombreMetodo As String = st.GetFrame(1).GetMethod().Name
        Return nombreMetodo
    End Function

    Public Shared Function GetStatusMessage(ByVal offset As Integer, ByVal total As Integer) As String
        If total <= 0 Then
            Return "No hay coincidencias"
        End If
        Return String.Format("Items <b>1</b>-<b>{0}</b> de <b>{1}</b>", offset, total)
    End Function

    ''' <summary>
    ''' Metodo que carga un control RadComboBox con un DataTable
    ''' </summary>
    ''' <param name="p_objComboBox">Recibe el control RadComboBox por referencia</param>
    ''' <param name="p_lstItems">Recibe el DataTable</param>
    ''' <param name="p_strTextField">Recibe el Nombre del Texto</param>
    ''' <param name="p_strValueField">Recibe el Nombre del ID</param>
    ''' <remarks></remarks>
    Public Sub proLoad_RadComboBox(ByRef p_objComboBox As RadComboBox, ByVal p_lstItems As DataTable, ByVal p_strValueField As String, ByVal p_strTextField As String,
                                         Optional ByVal enuFirstField As DefaultData = DefaultData.Unselected)

        p_objComboBox.Items.Clear()
        p_objComboBox.DataTextField = p_strTextField
        p_objComboBox.DataValueField = p_strValueField

        p_objComboBox.ClearSelection()

        p_objComboBox.DataSource = p_lstItems
        p_objComboBox.DataBind()

        Dim liItem As New RadComboBoxItem
        liItem.Value = -1
        Select Case enuFirstField
            Case DefaultData.Unselected
                liItem.Text = "Seleccione..."
                liItem.Selected = True
                p_objComboBox.Items.Insert(0, liItem)
            Case DefaultData.All
                liItem.Text = "Todos"
                p_objComboBox.Items.Insert(0, liItem)
            Case DefaultData.None
                'Does not add a first item
            Case DefaultData.All2
                liItem.Text = "Seleccione..."
                p_objComboBox.Items.Insert(0, liItem)
                liItem.Value = 0

                Dim liItem2 As New RadComboBoxItem
                liItem2.Text = "Todos"
                liItem2.Value = 1
                p_objComboBox.Items.Insert(1, liItem2)
            Case DefaultData.All3
                liItem.Text = "Seleccione..."
                p_objComboBox.Items.Insert(0, liItem)
                liItem.Value = -1

                Dim liItem2 As New RadComboBoxItem
                liItem2.Text = "All"
                liItem2.Value = 0
                p_objComboBox.Items.Insert(1, liItem2)
        End Select

        'Set to the default value
        p_objComboBox.SelectedIndex = 0

    End Sub

    ''' <summary>
    ''' Metodo que carga un control RadComboBox con un Listado de Entidades
    ''' </summary>
    ''' <param name="p_objListBox">Recibe el control RadComboBox por referencia</param>
    ''' <param name="p_lstItems">Recibe el Objeto que contiene el listado de entidades</param>
    ''' <param name="p_strTextField">Recibe el Nombre del Texto</param>
    ''' <param name="p_strValueField">Recibe el Nombre del ID</param>
    ''' <remarks></remarks>
    Public Sub proLoad_RadListBox(Of T)(ByRef p_objListBox As RadListBox, ByVal p_lstItems As List(Of T),
                                        ByVal p_strValueField As String, ByVal p_strTextField As String)

        p_objListBox.Items.Clear()
        p_objListBox.DataTextField = p_strTextField
        p_objListBox.DataValueField = p_strValueField

        p_objListBox.ClearSelection()

        p_objListBox.DataSource = p_lstItems
        p_objListBox.DataBind()

    End Sub

#End Region

#Region " ASP.NET Controls Methods "

    ''' <summary>
    ''' Metodo que carga un control RadComboBox con un Listado de Entidades
    ''' </summary>
    ''' <param name="p_objComboBox">Recibe el control RadComboBox por referencia</param>
    ''' <param name="p_lstItems">Recibe el Objeto que contiene el listado de entidades</param>
    ''' <param name="p_strTextField">Recibe el Nombre del Texto</param>
    ''' <param name="p_strValueField">Recibe el Nombre del ID</param>
    ''' <remarks></remarks>
    Public Sub proLoad_RadioButtonList(Of T)(ByRef p_objComboBox As RadioButtonList, ByVal p_lstItems As List(Of T), ByVal p_strValueField As String, ByVal p_strTextField As String,
                                             Optional ByVal enuFirstField As DefaultData = DefaultData.Unselected)

        p_objComboBox.Items.Clear()
        p_objComboBox.DataTextField = p_strTextField
        p_objComboBox.DataValueField = p_strValueField

        p_objComboBox.ClearSelection()

        p_objComboBox.DataSource = p_lstItems
        p_objComboBox.DataBind()

        Dim liItem As New ListItem
        liItem.Value = -1
        Select Case enuFirstField
            Case DefaultData.Unselected
                liItem.Text = "Seleccione un Valor..."
                p_objComboBox.Items.Insert(0, liItem)
            Case DefaultData.All
                liItem.Text = "Todos"
                p_objComboBox.Items.Insert(0, liItem)
            Case DefaultData.None
                'Does not add a first item
            Case DefaultData.All2
                liItem.Text = "Seleccione un Valor..."
                p_objComboBox.Items.Insert(0, liItem)
                liItem.Value = 0

                Dim liItem2 As New ListItem
                liItem2.Text = "TODOS"
                liItem2.Value = 1
                p_objComboBox.Items.Insert(1, liItem2)
        End Select

        'Set to the default value
        p_objComboBox.SelectedIndex = 0

    End Sub

    ''' <summary>
    ''' Metodo que carga un control RadComboBox con un Listado de Entidades
    ''' </summary>
    ''' <param name="p_objComboBox">Recibe el control RadComboBox por referencia</param>
    ''' <param name="p_lstItems">Recibe el Objeto que contiene el listado de entidades</param>
    ''' <param name="p_strTextField">Recibe el Nombre del Texto</param>
    ''' <param name="p_strValueField">Recibe el Nombre del ID</param>
    ''' <remarks></remarks>
    Public Sub proLoad_CheckBoxList(Of T)(ByRef p_objComboBox As CheckBoxList, ByVal p_lstItems As List(Of T), ByVal p_strValueField As String, ByVal p_strTextField As String)

        p_objComboBox.Items.Clear()
        p_objComboBox.DataTextField = p_strTextField
        p_objComboBox.DataValueField = p_strValueField

        p_objComboBox.ClearSelection()

        p_objComboBox.DataSource = p_lstItems
        p_objComboBox.DataBind()

    End Sub

    ''' <summary>
    ''' Metodo que carga un control RadComboBox con un Listado de Entidades
    ''' </summary>
    ''' <param name="p_objComboBox">Recibe el control RadComboBox por referencia</param>
    ''' <param name="p_lstItems">Recibe el Objeto que contiene el listado de entidades</param>
    ''' <param name="p_strTextField">Recibe el Nombre del Texto</param>
    ''' <param name="p_strValueField">Recibe el Nombre del ID</param>
    ''' <remarks></remarks>
    Public Sub proLoad_ListBox(Of T)(ByRef p_objComboBox As ListBox, ByVal p_lstItems As List(Of T), ByVal p_strValueField As String, ByVal p_strTextField As String)

        p_objComboBox.Items.Clear()
        p_objComboBox.DataTextField = p_strTextField
        p_objComboBox.DataValueField = p_strValueField
        p_objComboBox.ClearSelection()

        p_objComboBox.DataSource = p_lstItems
        p_objComboBox.DataBind()

    End Sub

    ''' <summary>
    ''' Metodo que selecciona un Item en particular del ComboBox
    ''' </summary>
    ''' <param name="objComboBox"></param>
    ''' <param name="strCodigo"></param>
    ''' <param name="strTexto"></param>
    ''' <remarks></remarks>
    Protected Sub proSeleccionar_ItemComboBox(ByRef objComboBox As DropDownList, Optional ByVal strCodigo As String = Nothing,
                                              Optional ByVal strTexto As String = Nothing)

        If Not (strTexto = Nothing) Then
            If (objComboBox.Items.FindByText(strTexto) Is Nothing) Then Exit Sub
            objComboBox.SelectedItem.Selected = False
            objComboBox.Items.FindByText(strTexto).Selected = True
        Else
            If Not (strCodigo = Nothing) Then
                If objComboBox.Items.FindByValue(strCodigo) Is Nothing Then Exit Sub
                objComboBox.SelectedItem.Selected = False
                objComboBox.Items.FindByValue(strCodigo).Selected = True
            End If
        End If

    End Sub

#End Region

#Region " Miscelaneos Methods "

    Public Sub DownloadFile(ByVal fname As String, ByVal forceDownload As Boolean)
        'Dim path As Path
        Dim fullpath = Path.GetFullPath(fname)
        Dim name = Path.GetFileName(fullpath)
        Dim ext = Path.GetExtension(fullpath)
        Dim type As String = ""

        If Not IsDBNull(ext) Then
            ext = LCase(ext)
        End If

        Select Case ext
            Case ".htm", ".html"
                type = "text/HTML"
            Case ".txt"
                type = "text/plain"
            Case ".doc", ".rtf"
                type = "Application/msword"
            Case ".csv", ".xls"
                type = "Application/x-msexcel"
            Case Else
                type = "text/plain"
        End Select

        If (forceDownload) Then
            Response.AppendHeader("content-disposition", "attachment; filename=" + name)
        End If
        If type <> "" Then
            Response.ContentType = type
        End If

        Response.WriteFile(fullpath)
        Response.End()

    End Sub

    ''' <summary>
    ''' Generates a temp code for the item
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function funGet_TempCodeString() As String
        Dim strCode As String = String.Empty

        'If there anything on the ViewState I get it and increment it,
        'if not initilizes it
        If Me.ViewState.Item("Temp_CodeString") Is Nothing Then
            strCode = "TEMP1"
        Else
            strCode = Me.ViewState.Item("Temp_CodeString")
            strCode = "TEMP" & (CInt(strCode.Substring(4)) + 1)
        End If

        'Asigns the resulting code to the ViewState and returns it
        Me.ViewState.Add("Temp_CodeString", strCode)
        Return strCode
    End Function

    ''' <summary>
    ''' Generates a temp code for the item
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function funGet_TempCodeInteger() As Integer
        Dim intCode As Integer = -1

        'If there anything on the ViewState I get it and increment it,
        'if not initilizes it
        If Me.ViewState.Item("Temp_CodeInteger") Is Nothing Then
            intCode = -1
        Else
            intCode = Me.ViewState.Item("Temp_CodeInteger") - 1
        End If

        'Asigns the resulting code to the ViewState and returns it
        Me.ViewState.Add("Temp_CodeInteger", intCode)
        Return intCode
    End Function

    ' ''' <summary>
    ' ''' Method that load the requested mesnsaje
    ' ''' </summary>
    ' ''' <param name="p_strModuleCode">Receives the module code</param>
    ' ''' <param name="p_intMessageCode">Receives the message code</param>
    ' ''' <remarks></remarks>
    'Protected Sub proGet_XMLMessage(ByVal p_strModuleCode As String, ByVal p_intMessageCode As Integer, ByRef p_strMessage As String, ByRef p_intTypeMessage As Integer)

    '    Dim objXML As XDocument = XDocument.Load(Request.PhysicalApplicationPath & "Resources\XML\Messages.xml")
    '    Dim objResult = From xmlMessages In objXML.Descendants("Message") _
    '                    Where xmlMessages.Element("ModuleCode") = p_strModuleCode _
    '                    And xmlMessages.Element("MessageCode") = CStr(p_intMessageCode) _
    '                    Select Message = xmlMessages.Element("SpanishMessage").Value, _
    '                       TypeMessage = xmlMessages.Element("TypeMessage").Value
    '    'Set the values
    '    p_strMessage = objResult(0).Message
    '    p_intTypeMessage = CInt(objResult(0).TypeMessage)

    'End Sub

    ''' <summary>
    ''' Shows a message to the user
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub proShow_Message(ByVal p_strMessage As String, Optional ByVal p_intWith As Integer = 350, Optional ByVal p_intHeight As Integer = 160, Optional ByVal ptypemessage As enuTypeMessage = enuTypeMessage.Information)

        Dim bolAsynchronous As Boolean
        Dim strMSG As String = String.Empty
        Dim myMessageType As enuTypeMessage = ptypemessage

        If Request.Headers("X-MicrosoftAjax") IsNot Nothing Then
            If Request.Headers("X-MicrosoftAjax") = "Delta=true" Then
                bolAsynchronous = True
            Else
                bolAsynchronous = False
            End If
        Else
            bolAsynchronous = False
        End If

        'Get the message from XML file
        'Me.proGet_XMLMessage(p_strModuleCode, p_intMessageCode, strMSG, myMessageType)
        'If (strMSG Is Nothing) Then
        '    strMSG = "NO SE PUDO ENCONTRAR EL MENSAJE"
        '    myMessageType = enuTypeMessage.Exception
        'End If

        'Set personal message
        If (p_strMessage IsNot Nothing) Then
            strMSG = p_strMessage
        End If

        Dim strMessage As String = "<div class=""{0}"">" & strMSG & "</div>"
        Dim strTitle As String = String.Empty
        Select Case myMessageType
            Case enuTypeMessage.Information
                strTitle = "INFORMATION"
                strMessage = String.Format(strMessage, "message_info")

            Case enuTypeMessage.Confirmation
                strTitle = "CONFIRMATION"
                strMessage = String.Format(strMessage, "message_confirm")

            Case enuTypeMessage.Exception
                strTitle = "ERROR"
                strMessage = String.Format(strMessage, "message_error")

            Case enuTypeMessage.Security
                strTitle = "SECURITY"
                strMessage = String.Format(strMessage, "message_security")

            Case enuTypeMessage.Validation
                strTitle = "VALIDATION"
                strMessage = String.Format(strMessage, "message_validation")
            Case Else
                strTitle = "ERROR"
                strMessage = String.Format(strMessage = strMessage.Replace("'", ""), "message_error")
        End Select

        strMessage = strMessage.Replace("'", Chr(34))
        strMessage = strMessage.Replace(Chr(13), "")
        strMessage = strMessage.Replace(Chr(10), "")

        Dim intWidth As Integer = p_intWith
        Dim intHeight As Integer = p_intHeight

        If bolAsynchronous Then
            Dim strScript As String = "radalert('" & strMessage & "', " & intWidth & ", " & intHeight & ", '" & strTitle & "');"

            ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "RadAlert", strScript, True)
        Else
            Dim strScript As String =
            "<script language=JavaScript>" &
            "   (function(){ " &
            "       var f = function(){" &
            "           radalert('" & strMessage & "', " & intWidth & ", " & intHeight & ", '" & strTitle & "');" &
            "           Sys.Application.remove_load(f);" &
            "       };" &
            "   Sys.Application.add_load(f);" &
            "   })()" &
            "</script>"

            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Mensajes", strScript)
        End If

    End Sub


    Public Sub ClosePopupWithMessage(ByVal Div As String, ByVal Msg As String, ByVal MsgType As enuTypeMessage)

        Dim strType As String = String.Empty

        Select Case MsgType
            Case enuTypeMessage.Confirmation : strType = "CONFIRMATION"
            Case enuTypeMessage.Exception : strType = "ERROR"
            Case enuTypeMessage.Information : strType = "INFORMATION"
            Case enuTypeMessage.Validation : strType = "VALIDATION"
            Case enuTypeMessage.Security : strType = "SECURITY"
        End Select

        Dim strScript As String = "closeDialog('" & Div & "'); funShow_Message('" & Msg & "', '" & strType & "');"

        ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "cerrarpopUp", strScript, True)

    End Sub

    Public Sub ShowMessageFromClient(ByVal Msg As String, ByVal MsgType As enuTypeMessage)

        Dim strType As String = String.Empty

        Select Case MsgType
            Case enuTypeMessage.Confirmation : strType = "CONFIRMATION"
            Case enuTypeMessage.Exception : strType = "ERROR"
            Case enuTypeMessage.Information : strType = "INFORMATION"
            Case enuTypeMessage.Validation : strType = "VALIDATION"
            Case enuTypeMessage.Security : strType = "SECURITY"
        End Select

        Dim strScript As String = "funShow_Message('" & Msg & "', '" & strType & "');"

        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "showMessage", strScript, True)

    End Sub


    Public Shared Function DegreeToRadian(ByVal degree As Double) As Double
        Return (Math.PI / 180) * degree
    End Function

    Public Shared Function DistanciaPuntos(ByVal Lat1 As Double, ByVal Lon1 As Double, ByVal Lat2 As Double, ByVal Lon2 As Double) As Double
        Const EarthRadius As Integer = 6378
        Dim P As Double
        Dim decDistancia As Double

        P = Math.Sin(DegreeToRadian(Lat1)) * Math.Sin(DegreeToRadian(Lat2)) +
        Math.Cos(DegreeToRadian(Lat1)) * Math.Cos(DegreeToRadian(Lat2)) * Math.Cos(DegreeToRadian(Lon1) - DegreeToRadian(Lon2))

        decDistancia = Math.Acos(P) * EarthRadius

        'Metros
        decDistancia = decDistancia * 1000

        Return decDistancia
    End Function

    Public Function ExceptionMessage(ByVal Exception As BCException) As String
        Dim strMessage As String = ""

        strMessage = Exception.Message & "</br>"

        If Exception.InnerException IsNot Nothing Then
            strMessage &= "<p>" & Exception.InnerException.Message & "</p>"
            If Exception.InnerException.InnerException IsNot Nothing Then
                strMessage &= "<p>" & Exception.InnerException.InnerException.Message & "</p>"
            End If
        End If

        For Each ExceptionError In Exception.ErrorCollection
            strMessage &= ExceptionError & "<br>"
        Next
        Return strMessage
    End Function
#End Region

#Region " Security "

    'Private Property GrantedPages() As Hashtable
    '    Get
    '        If Session("GrantedPages") Is Nothing Then
    '            Session("GrantedPages") = New Hashtable()
    '        End If
    '        Return DirectCast(Session("GrantedPages"), Hashtable)
    '    End Get
    '    Set(ByVal value As Hashtable)
    '        Session("GrantedPages") = value
    '    End Set
    'End Property

    'Protected Property soSystem() As AVATech.Security.SecurityProvider.GrantedSystem
    '    Get
    '        If Session("SO_System") Is Nothing Then
    '            Session("SO_System") = New AVATech.Security.SecurityProvider.GrantedSystem()
    '        End If
    '        Return DirectCast(Session("SO_System"), AVATech.Security.SecurityProvider.GrantedSystem)
    '    End Get
    '    Set(ByVal value As AVATech.Security.SecurityProvider.GrantedSystem)
    '        Session("SO_System") = value
    '    End Set
    'End Property

    'Protected Property soUser() As AVATech.Security.SecurityProvider.User
    '    Get
    '        If Session("SO_User") Is Nothing Then
    '            Session("SO_User") = New AVATech.Security.SecurityProvider.User()
    '        End If
    '        Return DirectCast(Session("SO_User"), AVATech.Security.SecurityProvider.User)
    '    End Get
    '    Set(ByVal value As AVATech.Security.SecurityProvider.User)
    '        Session("SO_User") = value
    '    End Set
    'End Property

    'Protected Function AVASEC_GetUserCode() As String
    '    Return soUser.Info.UserCode
    'End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Not soUser.IsAuthenticated Then

        '    Session("SuccessLogOnRedirectURL") = Page.AppRelativeVirtualPath & "?" & Page.ClientQueryString
        '    Response.Redirect("~/Login.aspx", True)
        'End If
        'If Not (Page.AppRelativeVirtualPath = "~/LogOff.aspx" Or GrantedPages.ContainsKey(Page.AppRelativeVirtualPath) Or Page.AppRelativeVirtualPath = Session("SuccessLogOnRedirectURL")) Then
        '    '   Response.Redirect("~/Denied.aspx")
        'End If
    End Sub

    'Protected Function AVASEC_GetPermissionLevel(ByVal p_strActivitieCode As String) As AVATech.Security.enLevelAccess
    '    If Not soUser.IsAuthenticated Then
    '        Return AVATech.Security.enLevelAccess.Denied
    '    Else
    '        Return AVATech.Security.UserHandler.fun_GetPermissionLevel(soUser, p_strActivitieCode)
    '    End If

    'End Function

    'Protected Sub proLogOFF()
    '    Session("SuccessLogOnRedirectURL") = "~/Default.aspx"
    '    soUser = Nothing
    'End Sub

    ''TODO USUARIO ACTUAL DE LA BD
    'Public Function funGet_UserCode() As String
    '    If Session("UserCode") IsNot Nothing Then
    '        Return Me.Session.Item("UserCode")
    '    Else
    '        FormsAuthentication.RedirectToLoginPage()
    '    End If
    '    Return String.Empty
    'End Function
    Public Shared Function funGet_UserCode() As Int32

        Dim UserIdentity As FormsIdentity = DirectCast(HttpContext.Current.User.Identity, FormsIdentity)
        Dim lngResult As Int32 = 0

        If UserIdentity IsNot Nothing Then
            Dim UserTicket As FormsAuthenticationTicket = UserIdentity.Ticket
            lngResult = UserTicket.UserData
        End If

        Return lngResult
    End Function
    Public Shared Function funGet_UnidadNegocio() As Int32

        Dim UserIdentity As FormsIdentity = DirectCast(HttpContext.Current.User.Identity, FormsIdentity)
        Dim lngResult As Int32 = 0

        If UserIdentity IsNot Nothing Then
            Dim UserTicket As FormsAuthenticationTicket = UserIdentity.Ticket
            lngResult = CInt(UserTicket.Name)
        End If

        Return lngResult
    End Function
    Public Shared Function funGet_UnidadNegocioPadre() As Int32

        Dim UserIdentity As FormsIdentity = DirectCast(HttpContext.Current.User.Identity, FormsIdentity)
        Dim lngResult As Int32 = 0

        If UserIdentity IsNot Nothing Then
            Dim UserTicket As FormsAuthenticationTicket = UserIdentity.Ticket
            lngResult = UserTicket.Version
        End If

        Return lngResult
    End Function
#End Region

#Region " Page Events "

    'Protected Overrides Sub InitializeCulture()
    '    CultureConfiguration.CurrentCulture = New Bolivia

    '    Thread.CurrentThread.CurrentCulture = CultureConfiguration.CurrentCultureInfo
    '    Thread.CurrentThread.CurrentUICulture = CultureConfiguration.CurrentCultureInfo
    '    MyBase.InitializeCulture()
    'End Sub

#End Region

End Class