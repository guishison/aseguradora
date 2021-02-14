Option Explicit On
Option Compare Text
Option Strict On
Option Infer On

Imports System.ComponentModel.DataAnnotations

Namespace Base

    <Serializable>
    Public Class ClasificadoresTipo
        Inherits BEntity


#Region " Properties "

        Public Property Id As Int32

        <Required(AllowEmptyStrings:=False, ErrorMessage:="*")>
        <StringLength(150, ErrorMessage:="No debe exeder los {1} caracteres.")>
        Public Property Nombre As String

        <Required(ErrorMessage:="*")>
        Public Property PersonalId As Int32

        <Required(ErrorMessage:="*")>
        Public Property Fechareg As DateTime

        <Required(ErrorMessage:="*")>
        Public Property FechaActualizacion As DateTime

        <Required(ErrorMessage:="*")>
        Public Property Estado As Int32

#End Region

#Region " Additional properties "

        Public Property ColeccionClasificadores As List(Of Clasificadores)

#End Region

#Region " Contructors "

        Public Sub New()
            ColeccionClasificadores = New List(Of Clasificadores)
        End Sub

#End Region

    End Class

    ''' <summary>
    ''' Relationship enumerator ClassifierType
    ''' </summary>
    ''' <remarks></remarks>      
    Public Enum relClasificadoresTipo
        Clasificadores
    End Enum


End Namespace


