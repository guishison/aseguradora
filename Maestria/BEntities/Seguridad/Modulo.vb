Option Explicit On
Option Compare Text
Option Strict On
Option Infer On

Imports System.ComponentModel.DataAnnotations

Namespace Seguridad
    <Serializable>
    Public Class Modulo
        Inherits BEntity

#Region "Propiedades"
        Public Property Id As Int32
        <StringLength(250, ErrorMessage:="No debe exeder los {1} caracteres.")>
        Public Property Nombre As String
        Public Property Icono As String
        Public Property Orden As Int32
        Public Property Estado As Int16
        Public Property SubModuloId As Int32
        Public Property UnidadNegocioId As Int32
#End Region

#Region " Additional properties "
        Public Property ColeccionFormulario As List(Of Formulario)
#End Region

#Region " Contructors "
        Public Sub New()
            ColeccionFormulario = New List(Of Formulario)
        End Sub
#End Region


    End Class
    Public Enum relModulo
        Formulario
    End Enum


End Namespace