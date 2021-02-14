Option Explicit On
Option Compare Text
Option Strict On
Option Infer On

Imports System
Imports System.Configuration
Imports System.Transactions
Imports BE = BEntities
Imports Microsoft.Practices.EnterpriseLibrary.Logging
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports Microsoft.Practices.EnterpriseLibrary.Validation
Imports Microsoft.Practices.EnterpriseLibrary.Validation.Configuration
Imports Microsoft.Practices.EnterpriseLibrary.Common.Configuration
Imports System.Security.Cryptography

Public MustInherit Class BCEntity

#Region " Enumeradores "
    Protected Enum ErrorPolicy As Byte
        BCReplace = 1
        BCWrap = 2
        BCPropagate = 3
        BCNew = 4
    End Enum
#End Region

#Region " Declaracion de Variables "
    Protected Token As String = ""
    Protected ErrorCollection As New List(Of String)
    Protected strConnection As String
    Protected objDatabase As Object = Nothing
#End Region

#Region " Definición de las propiedades "

    Public ReadOnly Property ErrorMessage() As List(Of String)
        Get
            Return Me.ErrorCollection
        End Get
    End Property

    Public Property Connection() As String
        Get
            Return strConnection
        End Get
        Set(ByVal value As String)
            strConnection = value
        End Set
    End Property

    Public Property DataBase As Object
        Get
            Return objDatabase
        End Get
        Set(value As Object)
            objDatabase = value
        End Set
    End Property

#End Region

#Region " Declaracion de Metodos "

    Protected Function GenerateBusinessTransaction() As TransactionScope
        Dim tsOptions As TransactionOptions
        tsOptions.IsolationLevel = IsolationLevel.Serializable
        tsOptions.Timeout = New TimeSpan(0, 10, 0)
        Return New TransactionScope(TransactionScopeOption.Required, tsOptions)
    End Function

    Protected Sub LogearError(ByVal Excepcion As Exception)
        Dim m_Log As New LogEntry

        Try
            With m_Log

            End With
            Logger.Write(m_Log)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ErrorHandler(ByVal exnException As Exception, ByVal Policy As ErrorPolicy)
        Dim rethrow As Boolean = ExceptionPolicy.HandleException(exnException, Policy.ToString)

        If (rethrow) Then

            Throw exnException

        End If



    End Sub

    Protected Sub ErrorHandler(ByVal strMessage As String, ByVal Policy As ErrorPolicy)
        Dim Excepcion As New BCException(strMessage)
        LogearError(Excepcion)
        Dim rethrow As Boolean = ExceptionPolicy.HandleException(Excepcion, Policy.ToString)
        If (rethrow) Then
            Throw Excepcion
        End If
    End Sub

    Protected Friend Function ValidateBusinessObject(Of T As BE.BEntity)(ByRef BEObj As T, ByVal Ruleset As String) As Boolean
        Dim EntityValidation As Validator(Of T)
        Dim ValidationErrors As ValidationResults = Nothing
        Dim bolOk As Boolean = True

        Try
            If BEObj.StatusType <> BE.StatusType.Delete Then
                EntityValidation = ValidationFactory.CreateValidator(Of T)(Ruleset)
                ValidationErrors = EntityValidation.Validate(BEObj)
                Me.ErrorCollection.AddRange(From ErrorMessage In ValidationErrors Select ErrorMessage.Key & "|" & ErrorMessage.Message)
                bolOk = ValidationErrors.IsValid
            End If
        Catch ex As Exception
            Throw ex
        End Try
        Return bolOk
    End Function

    Public Function ValidateToken(ByVal TokenCliente As String, ByVal User As String) As Boolean
        Dim bolResult As Boolean = True
        Dim strHash As String = ""

        'System.Security.Cryptography.

        'If Cryptographer.CompareHash("Sha1", User.Trim & Now.Date.ToString("dd-MM-yyyy"), TokenCliente) Then
        '    bolResult = True
        'Else
        '    bolResult = False
        'End If
        Return bolResult
    End Function

#End Region

#Region " Definición de Constructores "

    Protected Sub New()
    End Sub

    Protected Sub New(ByVal NombreConexion As String)
        strConnection = NombreConexion
    End Sub

    Protected Sub New(ByVal Usuario As String, ByVal Clave As String, ByRef Token As String)
        Token = "123456789"
    End Sub

    Protected Sub New(Database As Object)


    End Sub

#End Region

End Class