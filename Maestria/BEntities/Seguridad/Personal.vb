Option Explicit On
Option Compare Text
Option Strict On
Option Infer On

Imports System.ComponentModel.DataAnnotations
Imports BEntities.Base

Namespace Seguridad

    <Serializable>
    Public Class Personal
        Inherits BEntity

#Region "Propiedades"
        Public Property Id As Int32
        Public Property PersonalId As Int32
        Public Property SupervisorId As Int32
        Public Property CargoId As Int32
        Public Property MontoComision As Decimal
        <StringLength(250, ErrorMessage:="No debe exeder los {1} caracteres.")>
        Public Property Nombre As String
        Public Property Rut As String
        <StringLength(150, ErrorMessage:="No debe exeder los {1} caracteres.")>
        Public Property Correo As String
        <StringLength(50, ErrorMessage:="No debe exeder los {1} caracteres.")>
        Public Property Login As String
        <StringLength(50, ErrorMessage:="No debe exeder los {1} caracteres.")>
        Public Property Password As String
        Public Property UnidadNegocioId As Int32
        Public Property DepartamentoId As Int32
        Public Property Telefono As String

        Public Property DependenciaIdc As Int32

        Public Property FechaReg As DateTime
        Public Property FechaActualizacion As DateTime
        Public Property Estado As Int16
        Public Property PersonalEstado As Int32

        Public Property NombreCobrador As String
        Public Property ClientesAsignados As Int32
        Public Property CantidadMaxAsignacion As Int32
        Public Property FechaBloqueo As DateTime?
        Public Property IntentosFallidos As Int32
#End Region

#Region " Additional properties "
        Public Property Clasificador() As Clasificadores
        Public Property Personal() As Personal
        Public Property Cargo() As Cargo
        Public Property UnidadNegocio() As UnidadNegocio
        Public Property UNSucursal() As UNSucursal
        Public Property Supervisor() As Personal
        Public Property Perfil() As Perfil

        Public Property Departamento() As Departamento
        'Public Property PerfilPersonal() As PerfilPersonal

        Public Property SupervisorPersonal() As PersonalSupervisor
        'Public Property ColeccionPerfilPersonal As List(Of PerfilPersonal)
        Public Property ColeccionCargo As List(Of Cargo)

        Public Property CollectionOption As List(Of Formulario)

        Public Property CollectionPrivilegios As List(Of Privilegio)
        'Public Property CollectionCartera As List(Of Cobranzas.CarteraCobranza)
        Public Property PersonalPassword As PersonalPassword


        Public ReadOnly Property CantClientes As String
            Get
                Dim cantidad As String
                'cantidad = CollectionCartera.Count.ToString
                Return cantidad
            End Get
        End Property
        Public ReadOnly Property EstadoDeCobrador As String
            Get
                Dim Estado As String
                If PersonalEstado = 1 Then
                    Estado = "Habilitado"
                Else
                    Estado = "Deshabilitado"
                End If
                Return Estado
            End Get
        End Property


#End Region

#Region " Contructors "

        Public Sub New()
            'ColeccionPerfilPersonal = New List(Of PerfilPersonal)
            ColeccionCargo = New List(Of Cargo)
        End Sub

        Public Function DataAnnotationsValidator(objeto As Object, resultado As List(Of ValidationResult)) As Boolean
            Dim contexto = New ValidationContext(objeto, serviceProvider:=Nothing, items:=Nothing)
            resultado = New List(Of ValidationResult)
            Return Validator.TryValidateObject(objeto, contexto, resultado, validateAllProperties:=True)
        End Function



#End Region


    End Class
    Public Enum relPersonal
        Personal
        'PerfilPersonal
        Supervisor
        Cargo
        UnidadNegocio
        UNSucursal
        Privilegios
        CarteraCobranza
    End Enum


End Namespace