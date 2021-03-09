Option Strict On
Option Compare Text
Option Explicit On
Option Infer On
Imports BEntities

Imports BE = BEntities
Imports BAS = BEntities.Base
Imports DAL = DALayer.Base
Namespace Base
    <Serializable>
    Public Class RegistroFacturacion
        Inherits BCEntity

        Public Sub Save(ByRef BEObj As BAS.RegistroFacturacion)
            Dim DALObject As DAL.RegistroFacturacion = Nothing
            'Dim DALEmpresaActividad As DAL.EmpresaActividad = Nothing

            Me.ErrorCollection.Clear()
            Try
                DALObject = New DAL.RegistroFacturacion
                DALObject.OpenConnection()
                'DALEmpresaActividad = New DALayer.Base.EmpresaActividad(True, CObj(DALObject.DBFactory), DALObject.Transaction)

                DALObject.Save(BEObj)
                'If BEObj.CollecionActividades IsNot Nothing Then
                '    For Each x In BEObj.CollecionActividades
                '        x.SucursalId = BEObj.idempresa
                '        DALEmpresaActividad.Save(x)
                '    Next
                'End If

                DALObject.Commit()
            Catch ex As Exception
                DALObject.Rollback()
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
            Finally
                DALObject.CloseConnection()
            End Try
        End Sub


        Public Function ListById(ByVal Id As Int32, ByVal ParamArray Relations() As [Enum]) As BAS.RegistroFacturacion
            Try
                Dim BECollection As BAS.RegistroFacturacion

                Using DALObject As New DAL.RegistroFacturacion
                    BECollection = DALObject.ListById(Id, Relations)
                End Using

                Return BECollection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
                Return Nothing
            End Try
        End Function
        Public Function ListBuscar(ByVal text As String, ByVal CantidadRegistros As Int32, ByVal NumeroPagina As Int32, ByVal ParamArray Relations() As [Enum]) As List(Of BAS.RegistroFacturacion)
            Try
                Dim BECollection As List(Of BAS.RegistroFacturacion)

                Using DALObject As New DAL.RegistroFacturacion
                    BECollection = DALObject.ListBuscar(text, CantidadRegistros, NumeroPagina, Relations)
                End Using

                Return BECollection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
                Return Nothing
            End Try
        End Function
        Public Function ListBuscarCount(ByVal text As String) As Int32
            Try
                Dim BECollection As Int32

                Using DALObject As New DAL.RegistroFacturacion
                    BECollection = DALObject.ListBusquedaCount(text)
                End Using

                Return BECollection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
                Return Nothing
            End Try
        End Function
    End Class
End Namespace