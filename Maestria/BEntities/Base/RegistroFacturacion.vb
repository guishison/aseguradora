Option Strict On
Option Compare Text
Option Explicit On
Option Infer On

Namespace Base
    <Serializable>
    Public Class RegistroFacturacion
        Inherits BEntity

        Public Property Id As Int32
        Public Property Nombre As String
        Public Property Apellidos As String
        Public Property RazonSocial As String
        Public Property Nit As String
        Public Property Logo As String
        Public Property FotoNit As String
        Public Property Direccion As String
        Public Property Telefono As String
        Public Property Email As String
        Public Property NumeroPatronal As String
        Public Property RepresentanteLegal As String
        Public Property Ciudad As String
        Public Property Fecha As DateTime
        Public Property EstadoSolicitud As Int32
        Public Property Estado As Int32

        Public ReadOnly Property NombreCompleto As String
            Get
                Dim texto As String = ""
                texto = String.Concat(Nombre.Trim, " ", Apellidos.Trim)
                Return texto
            End Get
        End Property
        Public ReadOnly Property EstadoClienteGrid As String
            Get
                Select Case EstadoSolicitud
                    Case 1
                        Return "CREADO"
                    Case 2
                        Return "EN PROCESO"
                    Case 3
                        Return "REALIZADO"
                    Case 4
                        Return "ANULADO"
                    Case Else
                        Return "SIN CALIFICACION"
                End Select
            End Get
        End Property

        Public Sub New()

        End Sub

    End Class
End Namespace