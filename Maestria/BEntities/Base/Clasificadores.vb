Option Explicit On
Option Compare Text
Option Strict On
Option Infer On

Imports System.ComponentModel.DataAnnotations

Namespace Base

    <Serializable>
    Public Class Clasificadores
        Inherits BEntity


#Region " Properties "

        Public Property Id As Int32

        <Required(ErrorMessage:="*")>
        Public Property TipoClasificadorId As Int32

        <Required(AllowEmptyStrings:=False, ErrorMessage:="*")>
        <StringLength(150, ErrorMessage:="No debe exeder los {1} caracteres.")>
        Public Property Nombre As String

        <Required(AllowEmptyStrings:=False, ErrorMessage:="*")>
        <StringLength(50, ErrorMessage:="No debe exeder los {1} caracteres.")>
        Public Property Valor As String

        <Required(ErrorMessage:="*")>
        Public Property PersonalId As Int32
        <Required(ErrorMessage:="*")>
        Public Property FechaReg As DateTime
        <Required(ErrorMessage:="*")>
        Public Property FechaActualizacion As DateTime
        <Required(ErrorMessage:="*")>
        Public Property Estado As Int32

#End Region

#Region " Additional properties "

        Public Property ClasificadoresTipo() As ClasificadoresTipo

#End Region

#Region " Contructors "

        Public Sub New()
        End Sub

#End Region

    End Class

    ''' <summary>
    ''' Relationship enumerator Classifiers
    ''' </summary>
    ''' <remarks></remarks>      
    Public Enum relClasificadores
        ClasificadoresTipo
    End Enum


End Namespace


