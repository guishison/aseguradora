Option Explicit On
Option Compare Text
Option Strict On
Option Infer On

Imports System.ComponentModel.DataAnnotations

Namespace Seguridad

    <Serializable>
    Public Class Formulario
        Inherits BEntity

#Region "Propiedades"
        Public Property Id As Int32
        <StringLength(250, ErrorMessage:="No debe exeder los {1} caracteres.")>
        Public Property Url As String
        <StringLength(250, ErrorMessage:="No debe exeder los {1} caracteres.")>
        Public Property Nombre As String
        Public Property TipoFormulario As String
        Public Property ModuloId As Int32
        Public Property Icono As String
        Public Property Orden As Int32
        Public Property Estado As Int16
        Public Property Acciones As String
        Public Property UnidadNegocioId As Int32
#End Region

#Region " Additional properties "
        Public Property Modulo() As Modulo
        Public Property ColeccionPerfil As List(Of Perfil)
#End Region

#Region " Contructors "

        Public Sub New()
            ColeccionPerfil = New List(Of Perfil)
        End Sub

#End Region


    End Class
    Public Enum relFormulario
        Modulo
        Perfil
    End Enum


End Namespace