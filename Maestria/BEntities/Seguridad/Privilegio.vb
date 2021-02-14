Option Explicit On
Option Compare Text
Option Strict On
Option Infer On

Imports System.ComponentModel.DataAnnotations

Namespace Seguridad
    <Serializable>
    Public Class Privilegio
        Inherits BEntity

#Region "Propiedades"
        Public Property Id As Int32
        <StringLength(250, ErrorMessage:="No debe exeder los {1} caracteres.")>
        Public Property PersonalIdL As Int32
        Public Property FechaReg As DateTime
        Public Property PersonalId As Int32
        Public Property FormularioId As Int32
        Public Property Permiso As String
        Public Property TiempoDias As Int32
        Public Property UnidadNegocioId As Int32
        Public Property Estado As Int16
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
    Public Enum relPrivilegio
        Formulario
        'Personal
    End Enum


End Namespace