
Option Explicit On
Option Compare Text
Option Strict On
Option Infer On

Imports System.ComponentModel.DataAnnotations

Namespace Base

    <Serializable>
    Public Class UNSucursal
        Inherits BEntity


#Region " Properties "

        Public Property Id As Int32
        Public Property UnidadNegocioId As Int32

        <Required(AllowEmptyStrings:=False, ErrorMessage:="*")>
        <StringLength(150, ErrorMessage:="No debe exeder los {1} caracteres.")>
        Public Property Nombre As String
        <Required(ErrorMessage:="*")>
        Public Property Correlativo As Int32

        <Required(AllowEmptyStrings:=False, ErrorMessage:="*")>
        <StringLength(50, ErrorMessage:="No debe exeder los {1} caracteres.")>
        Public Property AliasUN As String


        <Required(ErrorMessage:="*")>
        Public Property PersonalId As Int32
        <Required(ErrorMessage:="*")>
        Public Property FechaReg As DateTime

        <Required(ErrorMessage:="*")>
        Public Property FechaActualizacion As DateTime
        Public Property Estado As Int32


#End Region

#Region " Additional properties "

        Public Property Clasificadores As Clasificadores

#End Region

#Region " Contructors "

        Public Sub New()

        End Sub

#End Region

    End Class

    ''' <summary>
    ''' Relationship enumerator ClassifierType
    ''' </summary>
    ''' <remarks></remarks>      
    Public Enum relUNSucursal
        Clasificadores
    End Enum


End Namespace