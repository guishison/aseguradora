Option Strict On
Option Compare Text
Option Explicit On
Option Infer On

Namespace Base
    <Serializable>
    Public Class Album
        Inherits BEntity

        Public Property Id As Int32
        Public Property Nombre As String
        Public Property Descripcion As String
        Public Property PersonalId As Int32
        Public Property PersonalModificacionId As Int32
        Public Property FechaRegistro As DateTime
        Public Property FechaModificacion As DateTime
        Public Property Estado As Int32

        Public Property ListaFotos As List(Of Foto) = New List(Of Foto)
        Public ReadOnly Property NombreCompleto As String
            Get
                Dim texto As String = ""
                texto = String.Concat(Nombre.Trim, " ", Descripcion.Trim)
                Return texto
            End Get
        End Property

        Public Sub New()

        End Sub

    End Class
    Public Enum relAlbum
        Foto
    End Enum
End Namespace