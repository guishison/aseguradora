Imports System.IO
Public Class Download
    Inherits UtilsMethods

    Protected Overloads Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Session("DownLoadFIle") Is Nothing Then
                If (Session("Path") Is Nothing) Then
                    Session("Path") = ""
                End If
                If Session("Path") <> "" Then

                    Dim strPath As String = Session("Path")
                    'Dim strPath As String = Hosting.HostingEnvironment.ApplicationVirtualPath
                    Session.Remove("Path")
                    'Dim path As Path
                    Dim fullpath = Path.GetFullPath(strPath)
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
                        Case ".csv", ".xls", ".xlsx"
                            type = "Application/x-msexcel"
                        Case ".odt"
                            type = "application/vnd.oasis.opendocument.text"
                        Case ".ods"
                            type = "application/vnd.oasis.opendocument.spreadsheet"
                        Case Else
                            type = "text/plain"
                    End Select
                    Response.AppendHeader("content-disposition", "attachment; filename=" + name)
                    If type <> "" Then
                        Response.ContentType = type
                    End If
                    Response.WriteFile(fullpath)
                    Response.End()
                End If
            Else
                    Dim objFileData As Byte() = Session("DownLoadFIle")
                Session.Remove("DownLoadFIle")
                Response.Clear()
                Response.AddHeader("Content-Type", "binary/octet-stream")
                Response.AddHeader("Content-Length", objFileData.Length.ToString())
                Response.AddHeader("Content-Disposition", "attachment; filename=" & Session("DownLoadFIleName") & ";")
                Response.BinaryWrite(objFileData)
            End If

        End If
    End Sub

End Class