Imports BEntities.Base
Namespace Aseguradora

    <Serializable>
    Public Class Vehiculo
        Inherits BEntity

        Public Property Id As Int32
        Public Property ClienteId As Int32
        Public Property MarcaIdc As Int32
        Public Property ModeloIdc As Int32
        Public Property TipoVehiculoIdc As Int32
        Public Property OrigenIdc As Int32
        Public Property Placa As String
        Public Property Potencia As String
        Public Property UnidadNegocioId As Int32
        Public Property PersonalId As Int32
        Public Property PersonalModificacionId As Int32
        Public Property FechaRegistro As DateTime
        Public Property FechaModificacion As DateTime
        Public Property Estado As Int16

#Region "Aditional Property"
        Public Property Cliente As Cliente
        Public Property Marca As Clasificadores
        Public Property Modelo As Clasificadores
        Public Property TipoVehiculo As Clasificadores
        Public Property Origen As Clasificadores
        Public ReadOnly Property DetalleVehiculo As String
            Get
                If Marca IsNot Nothing And Modelo IsNot Nothing And TipoVehiculo IsNot Nothing And Origen IsNot Nothing Then
                    Return TipoVehiculo.Nombre & " " & Marca.Nombre & " " & Modelo.Nombre
                End If
                Return ""
            End Get
        End Property

#End Region
    End Class
    Public Enum relVehiculo
        Cliente
        Marca
        Modelo
        TipoVehiculo
        Origen
    End Enum
End Namespace
