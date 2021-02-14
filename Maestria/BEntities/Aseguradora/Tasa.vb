Imports BEntities.Base
Namespace Aseguradora
    <Serializable>
    Public Class Tasa
        Inherits BEntity
#Region "Properties"
        Public Property Id As Int32
        Public Property CiudadId As Int32
        Public Property TipoVehiculoIdc As Int32
        Public Property Valor As Decimal
        Public Property UnidadNegocioId As Int32
        Public Property PersonalId As Int32
        Public Property PersonalModificacionId As Int32
        Public Property FechaRegistro As DateTime
        Public Property FechaModificacion As DateTime
        Public Property Estado As Int16
#End Region

#Region "Aditional Properties"
        Public Property Ciudad As Comuna
        Public Property TipoVehiculo As Clasificadores
#End Region

    End Class
    Public Enum relTasa
        Ciudad
        TipoVehiculo
    End Enum
End Namespace
