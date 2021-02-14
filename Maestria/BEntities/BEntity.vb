Option Explicit On
Option Compare Text
Option Strict On
Option Infer On

Imports System
Imports BE = BEntities

<Serializable>
Public MustInherit Class BEntity

#Region " Private Variables "
    Private enuStatusType As StatusType = BE.StatusType.NoAction
#End Region
    Public Property Ocupado As String = "Ocupado"
    Public Property Reserva As String = "Reserva"
    Public Property Habilitado As String = "Habilitado"
    Public Property Deshabilitado As String = "Deshabilitado"
    Public Property Pendiente As String = "Porcentaje"
    Public Property Pagado As String = "Completa"
    Public Property Abierta As String = "Abierta"
    Public Property Cerrada As String = "Cerrada"
    Public Property ReAbierta As String = "ReAbierta"
    Public Property Soltero As String = "Soltero"
    Public Property Casado As String = "Casado"
    Public Property Viudo As String = "Viudo"
    Public Property Divorciado As String = "Divorciado"
    Public Property Devuelto As String = "Devuelto"
    Public Property Usado As String = "Usado"


    Public Property NoInsert As String = "Ud. no tiene permiso para registrar datos."
    Public Property NoDelete As String = "Ud. no tiene permiso para eliminar datos."
    Public Property NoUpdate As String = "Ud. no tiene permiso para modificar datos."
    Public Property NoExport As String = "Ud. no tiene permiso para exportar datos."
    Public Property NoExportImprimir As String = "Ud. no tiene permiso para imprimir o exportar datos."

#Region " Properties "

    Public Property StatusType() As StatusType
        Get
            Return enuStatusType
        End Get
        Set(ByVal Value As StatusType)
            enuStatusType = Value
        End Set
    End Property

#End Region

#Region " Methods "

    Public Function Clone() As BEntity
        Return CType(Me.MemberwiseClone, BEntity)
    End Function

#End Region

End Class