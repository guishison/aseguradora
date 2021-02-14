Option Explicit On
Option Compare Text
Option Strict On
Option Infer On

Imports System.ComponentModel.DataAnnotations

Namespace Seguridad

    <Serializable>
    Public Class Plantilla
        Inherits BEntity

#Region "Propiedades"
        Public Property Id As Int32
        Public Property PersonalId As Int32
        Public Property FechaReg As DateTime
        <StringLength(250, ErrorMessage:="No debe exeder los {1} caracteres.")>
        Public Property Nombre As String
        Public Property UnidadNegocioId As Int32
        Public Property Estado As Int16
#End Region

#Region " Additional properties "
        Public Property Personal() As Personal
        Public Property ColeccionFormulario As List(Of Formulario)
        Public Property ColeccionPlantillaDetalle As List(Of PlantillaDetalle)
        Public Property ColeccionPrivilegio As List(Of Privilegio)
#End Region

#Region " Contructors "

        Public Sub New()
            Me.ColeccionFormulario = New List(Of Formulario)
            Me.ColeccionPlantillaDetalle = New List(Of PlantillaDetalle)
            Me.ColeccionPrivilegio = New List(Of Privilegio)
        End Sub

#End Region


    End Class
    Public Enum relPlanilla
        Formulario
        Personal
        PlantillaDetalle
        Privilegio
    End Enum

End Namespace