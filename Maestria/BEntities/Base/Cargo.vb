Option Explicit On
Option Compare Text
Option Strict On
Option Infer On

Imports System.ComponentModel.DataAnnotations
Imports BEntities.Seguridad

Namespace Base

    <Serializable>
    Public Class Cargo
        Inherits BEntity


#Region " Properties "

        Public Property Id As Int32

        <Required(ErrorMessage:="*")>
        Public Property CargoId As Int32

        <Required(AllowEmptyStrings:=False, ErrorMessage:="*")>
        <StringLength(150, ErrorMessage:="No debe exeder los {1} caracteres.")>
        Public Property Nombre As String

        <Required(ErrorMessage:="*")>
        Public Property OrgGrafica As Decimal

        Public Property ComisionBase As Decimal

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

        Public Property Cargo() As Cargo

        Public Property ColeccionCargo As List(Of Cargo)

        Public Property ColeccionPersonal As List(Of Personal)

#End Region

#Region " Contructors "

        Public Sub New()
            ColeccionCargo = New List(Of Cargo)
            ColeccionPersonal = New List(Of Personal)
        End Sub

#End Region

    End Class

    ''' <summary>
    ''' Relationship enumerator Position
    ''' </summary>
    ''' <remarks></remarks>      
    Public Enum relCargo
        Cargo
        Personal
    End Enum


End Namespace


