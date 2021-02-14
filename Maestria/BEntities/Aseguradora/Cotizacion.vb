Imports BEntities.Base
Namespace Aseguradora
    <Serializable>
    Public Class Cotizacion
        Inherits BEntity
#Region "Properties"
        Public Property Id As Int32
        Public Property ClienteId As Int32
        Public Property VehiculoId As Int32
        Public Property TasaId As Int32
        Public Property FechaCotizacion As DateTime
        Public Property CiudadId As Int32
        Public Property PrecioVehiculo As Decimal
        Public Property MontoAsegurable As Decimal
        Public Property CostoPrima As Decimal
        Public Property MesesAsegurable As Int32
        Public Property Descuento As Decimal
        Public Property CostoTotal As Decimal
        Public Property UnidadNegocioId As Int32
        Public Property PersonalId As Int32
        Public Property PersonalModificacionId As Int32
        Public Property FechaRegistro As DateTime
        Public Property FechaModificacion As DateTime
        Public Property EstadoCotizacionIdc As Int32
        Public Property Estado As Int16
#End Region
#Region "Aditional Properties"
        Public Property Cliente As Cliente
        Public Property Vehiculo As Vehiculo
        Public Property Tasa As Tasa
        Public Property Ciudad As Comuna
#End Region
    End Class
    Public Enum relCotizacion
        Cliente
        Vehiculo
        Tasa
        Ciudad
    End Enum
End Namespace