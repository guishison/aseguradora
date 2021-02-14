Option Compare Text
Option Explicit On
Option Strict On
Option Infer On

Imports System
Imports System.Runtime.Serialization

''' <summary>
''' Excepcion en la capa de negocios
''' </summary>
''' <remarks></remarks>
Public Class BCException
    Inherits Exception

    Private colErrorMessages As New List(Of String)

    Public ReadOnly Property ErrorCollection() As List(Of String)
        Get
            Return colErrorMessages
        End Get
    End Property

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(ByVal strMessage As String)
        MyBase.New(strMessage)
    End Sub

    Public Sub New(ByVal strMessage As String, ByVal NewException As Exception)
        MyBase.New(strMessage, NewException)
    End Sub

    Public Sub New(ByVal ErrorCollection As List(Of String))
        MyBase.New("Existen datos invalidos")
        Me.colErrorMessages = ErrorCollection
    End Sub

    Protected Sub New(ByVal info As SerializationInfo, ByVal context As StreamingContext)
        MyBase.New(info, context)
    End Sub

End Class