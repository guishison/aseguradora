Imports System
Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

Public Class DALBase

#Region " Enumerator "
    Protected Enum ErrorPolicy As Byte
        DALReplace = 1
        DALWrap = 2
        DALNew = 3
        DALThrow = 4
    End Enum
#End Region

    Protected DBPFactory As DatabaseProviderFactory
    Protected DBFactory As Database = Nothing

    Sub New()
        Try
            DBPFactory = New DatabaseProviderFactory()
            DBFactory = DBPFactory.Create("Negocio")
        Catch ex As Exception
            Me.ErrorHandler(ex, ErrorPolicy.DALWrap)
        End Try
    End Sub

    Public Sub OpenConnection(ByRef Conn As DbConnection, ByRef tran As DbTransaction)
        Conn = DBFactory.CreateConnection()
        Conn.Open()
        tran = Conn.BeginTransaction
    End Sub

    Public Sub DisposeConnection(ByRef Conn As DbConnection, ByRef tran As DbTransaction)
        Try
            If Conn IsNot Nothing Then
                If Conn.State <> ConnectionState.Closed Then
                    Conn.Close()
                End If
                tran.Dispose()
                Conn.Dispose()
            End If
        Catch ex As Exception
            Me.ErrorHandler(ex, ErrorPolicy.DALWrap)
        End Try
        tran = Nothing
        Conn = Nothing
    End Sub

    Protected Sub ErrorHandler(ByVal exnException As Exception, ByVal Policy As ErrorPolicy)
        Dim rethrow As Boolean = ExceptionPolicy.HandleException(exnException, Policy.ToString)
        If (rethrow) Then
            Throw exnException
        End If
    End Sub

End Class
