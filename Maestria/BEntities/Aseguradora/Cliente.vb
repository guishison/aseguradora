Namespace Aseguradora
    <Serializable>
    Public Class Cliente
        Inherits BEntity
#Region "Properties"
        Public Property Id As Int32
        Public Property Nombre As String
        Public Property Apellido As String
        Public Property CI As String
        Public Property ExpedidoIdc As Int32
        Public Property Telefono As String
        Public Property FechaNacimiento As DateTime
        Public Property Direccion As String
        Public Property UnidadNegocioId As Int32
        Public Property PersonalId As Int32
        Public Property PersonalModificacionId As Int32
        Public Property FechaRegistro As DateTime
        Public Property FechaModificacion As DateTime
        Public Property Estado As Int16
#End Region
#Region "Aditional Properties"
        Public Property Vehiculo As Vehiculo
        Public ReadOnly Property NombreCompleto As String
            Get
                Return Apellido & " " & Nombre
            End Get
        End Property
#End Region

    End Class
    Public Enum relCliente
        Vehiculo
    End Enum
End Namespace