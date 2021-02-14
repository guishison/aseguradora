Namespace Seguridad
    <Serializable>
    Public Class ParametroLogin
        Inherits BEntity
        Public Property Id As Int32
        Public Property LongitudMinima As Int32
        Public Property LongitudMaxima As Int32
        Public Property NivelPassword As Int32
        Public Property Expiracion As Int32
        Public Property CantidadIntentosFallidos As Int32
        Public Property TiempoBloqueo As Int32
        Public Property ComprobacionesPasswordAnteriores As Int32
        Public Property PersonalId As Int32
        Public Property PersonalModificacionId As Int32
        Public Property FechaRegistro As DateTime
        Public Property FechaModificacion As DateTime
        Public Property Estado As Int16

        Public ReadOnly Property NombreEstado As String
            Get
                Return IIf(Estado = 0, "Inactivo", "Activo")
            End Get
        End Property
        Public ReadOnly Property NombreExpiracion As String
            Get
                Return IIf(Expiracion = 0, "NO", "SI")
            End Get
        End Property
        Public Property Nivel As BEntities.Base.Clasificadores

    End Class

    Public Enum relParametroLogin
        NivelPassword
    End Enum
End Namespace
