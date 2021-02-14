Option Explicit On
Option Compare Text
Option Strict On
Option Infer On

Imports System.ComponentModel.DataAnnotations

Namespace Seguridad

    <Serializable>
    Public Class PerfilPersonal
        Inherits BEntity

#Region "Propiedades"
        Public Property Id As Int32
        Public Property PersonalId As Int32
        Public Property PerfilId As Int32
        Public Property UnidadNegocioId As Int32
        Public Property SupervisorId As Int32
        Public Property FechaReg As DateTime
        Public Property FechaActualizacion As DateTime
        <StringLength(250, ErrorMessage:="No debe exeder los {1} caracteres.")>
        Public Property PersonalIdL As Int32
        Public Property Estado As Int32

#End Region

#Region " Additional properties "
        Public Property Personal() As Personal
        Public Property ColeccionFormulario As List(Of Formulario)
        Public Property Perfil() As Perfil
        Public Property ColeccionPerfil As List(Of Perfil)
#End Region

#Region " Contructors "

        Public Sub New()
            Me.ColeccionFormulario = New List(Of Formulario)
            Me.ColeccionPerfil = New List(Of Perfil)
        End Sub

#End Region


    End Class
    Public Enum relPerfilPersonal
        Formulario
        Personal
        Perfil
    End Enum


End Namespace