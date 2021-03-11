Imports BEntities.Base
Namespace Aseguradora
    <Serializable>
    Public Class Poliza
    Inherits BEntity

#Region "Properties"
        Public Property Id As Int32
        Public Property CotizacionId As Int32
        Public Property NumeroPoliza As String
        Public Property FechaInicio As DateTime
        Public Property FechaPoliza As DateTime
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
        Public Property EstadoPolizaIdc As Int32
        Public Property Estado As Int16
#End Region
#Region "Aditional Properties"
        Public Property Cliente As Cliente
        Public Property Vehiculo As Vehiculo
        Public Property Cotizacion As Cotizacion
        Public Property Tasa As Tasa
        Public Property Ciudad As Comuna
#End Region
    End Class

    Public Enum relPoliza
        Cliente
        Vehiculo
        Tasa
        Ciudad
        Cotizacion
    End Enum
End Namespace