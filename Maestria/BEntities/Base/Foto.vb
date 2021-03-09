Option Strict On
Option Compare Text
Option Explicit On
Option Infer On

Namespace Base
    <Serializable>
    Public Class Foto
        Inherits BEntity

        Public Property Id As Int32
        Public Property AlbumId As Int32
        Public Property Nombre As String
        Public Property Descripcion As String
        Public Property Foto As String
        Public Property PersonalId As Int32
        Public Property PersonalModificacionId As Int32
        Public Property FechaRegistro As DateTime
        Public Property FechaModificacion As DateTime
        Public Property Estado As Int32

        Public Property Archivo As Byte()
        Public Sub New()

        End Sub

    End Class
End Namespace