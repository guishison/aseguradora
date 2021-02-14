Option Explicit On
Option Compare Text
Option Strict On
Option Infer On

Imports System.ComponentModel.DataAnnotations

Namespace Seguridad

    <Serializable>
    Public Class Perfil
        Inherits BEntity

#Region "Propiedades"
        Public Property Id As Int32
        Public Property PersonalId As Int32
        Public Property FechaReg As DateTime
        <StringLength(250, ErrorMessage:="No debe exeder los {1} caracteres.")>
        Public Property Nombre As String
        Public Property UnidadNegocioId As Int32
        Public Property Estado As Int32
#End Region

#Region " Additional properties "
        Public Property Personal() As Personal
        Public Property Perfil() As Perfil
        Public Property ColeccionFormulario As List(Of Formulario)
#End Region

#Region " Contructors "

        Public Sub New()
            Me.ColeccionFormulario = New List(Of Formulario)
        End Sub

#End Region


    End Class
    Public Enum relPerfil
        Formulario
        Personal
    End Enum


End Namespace