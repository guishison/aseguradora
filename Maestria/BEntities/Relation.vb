Option Explicit On
Option Compare Text
Option Strict On
Option Infer On

Imports System
Imports BE = BEntities

<Serializable()>
Public Class Relation

#Region " Properties "
    Public Property FirstKey As Long
    Public Property SecondKey As Long
#End Region

End Class
