Option Explicit On
Option Compare Text
Option Strict On
Option Infer On

Imports System.ComponentModel.DataAnnotations
Imports BEntities.Base

Namespace Seguridad

    <Serializable>
    Public Class PersonalSupervisor
        Inherits BEntity

#Region "Propiedades"
        Public Property Id As Int32
        Public Property PersonalId As Int32
        Public Property SupervisorId As Int32
        Public Property PersonalIdL As Int32
        Public Property FechaReg As DateTime
        Public Property FechaActualizacion As DateTime
        Public Property Estado As Int16
#End Region

#Region " Additional properties "
        Public Property NombreSupervisor As String
        Public Property Personal() As Personal
        Public Property Cargo() As Cargo
        Public Property UnidadNegocio() As UnidadNegocio
        Public Property UNSucursal() As UNSucursal

        'Public Property ColeccionFormulario As List(Of Formulario)

        Public Property Supervisor() As Personal
        Public Property Perfil() As Perfil
        Public Property PerfilPersonal() As PerfilPersonal
        Public Property ColeccionPerfilPersonal As List(Of PerfilPersonal)
        Public Property ColeccionCargo As List(Of Cargo)
#End Region

#Region " Contructors "

        Public Sub New()
            ColeccionPerfilPersonal = New List(Of PerfilPersonal)
            ColeccionCargo = New List(Of Cargo)
        End Sub

        Public Function DataAnnotationsValidator(objeto As Object, resultado As List(Of ValidationResult)) As Boolean
            Dim contexto = New ValidationContext(objeto, serviceProvider:=Nothing, items:=Nothing)
            resultado = New List(Of ValidationResult)
            Return Validator.TryValidateObject(objeto, contexto, resultado, validateAllProperties:=True)
        End Function



#End Region


    End Class
    Public Enum relPersonalSupervisor
        Personal
        PerfilPersonal
        Supervisor
        Cargo
        UnidadNegocio
        UNSucursal
    End Enum


End Namespace