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
    Public Class Album
        Inherits BCEntity

        Public Sub Save(ByRef BEObj As BAS.Album)
            Dim DALObject As DAL.Album = Nothing
            Dim DALFoto As DAL.Foto = Nothing

            Me.ErrorCollection.Clear()
            Try
                DALObject = New DAL.Album
                DALObject.OpenConnection()
                DALFoto = New DALayer.Base.Foto(True, CObj(DALObject.DBFactory), DALObject.Transaction)

                DALObject.Save(BEObj)
                If BEObj.ListaFotos IsNot Nothing Then
                    For Each x In BEObj.ListaFotos
                        x.AlbumId = BEObj.Id
                        DALFoto.Save(x)
                    Next
                End If

                DALObject.Commit()
            Catch ex As Exception
                DALObject.Rollback()
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
            Finally
                DALObject.CloseConnection()
            End Try
        End Sub


        Public Function ListById(ByVal Id As Int32, ByVal ParamArray Relations() As [Enum]) As BAS.Album
            Try
                Dim BECollection As BAS.Album

                Using DALObject As New DAL.Album
                    BECollection = DALObject.ListById(Id, Relations)
                End Using

                Return BECollection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
                Return Nothing
            End Try
        End Function
        Public Function ListBuscar(ByVal text As String, ByVal CantidadRegistros As Int32, ByVal NumeroPagina As Int32, ByVal ParamArray Relations() As [Enum]) As List(Of BAS.Album)
            Try
                Dim BECollection As List(Of BAS.Album)

                Using DALObject As New DAL.Album
                    BECollection = DALObject.ListBuscar(text, CantidadRegistros, NumeroPagina, Relations)
                End Using

                Return BECollection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
                Return Nothing
            End Try
        End Function
        Public Function ListByAlbum(ByVal text As String) As List(Of List(Of Object))
            Try
                Dim BECollection As List(Of List(Of Object))

                Using DALObject As New DAL.Album
                    BECollection = DALObject.ListByAlbum(text)
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

                Using DALObject As New DAL.Album
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