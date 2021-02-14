Option Compare Text
Option Explicit On
Option Strict On
Option Infer On

Imports System
Imports System.Runtime.Serialization

''' <summary>
''' Excepcion en la capa de acceso a Datos
''' </summary>
''' <remarks></remarks>
Public Class DALException
    Inherits Exception

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(ByVal Mensaje As String)
        MyBase.New(Mensaje)
    End Sub

    Public Sub New(ByVal Mensaje As String, ByVal Excepcion As Exception)
        MyBase.New(Mensaje, Excepcion)
    End Sub

End Class
