Option Strict On
Option Compare Text
Option Explicit On
Option Infer On

Imports BE = BEntities
Imports DAL = DALayer.Base
Imports MEb = BEntities.Base

Namespace Base
    <Serializable>
    Partial Public Class Pais
        Inherits BCEntity

#Region " Search Methods "

        '' <summary>
        '' 	Search for business objects of type Provider
        '' </summary>
        '' <param name="Id">Object identifier Provider</param>
        '' <param name="Relations">relationship enumetators</param>
        '' <returns>An object of type Provider</returns>
        '' <remarks>
        '' 	To get relationship objects, suply relationship enumetators
        '' </remarks>
        ''
        Public Function Search(ByVal Id As Int32, ByVal ParamArray Relations() As [Enum]) As MEb.Pais
            Dim BEObject As MEb.Pais = Nothing

            Try
                Using DALObject As New DAL.Pais
                    BEObject = DALObject.Search(Id, Relations)
                End Using
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
            End Try
            Return BEObject
        End Function




#End Region

#Region " List Methods "

        ''' <summary>
        ''' 	Search for collection business objects of type Provider
        ''' </summary>
        ''' <param name="Relations">Relationship enumerator</param>
        ''' <returns>Object collection of type Provider</returns>
        ''' <remarks>
        ''' 	To get relationship objects, suply relationship enumetators
        ''' </remarks>
        Public Function List(ByVal instanciaSalaId As Int32, ByVal Estado As Int16, ByVal CantidadRegistros As Int32, ByVal NumeroPagina As Int32, ByVal ParamArray Relations() As [Enum]) As List(Of MEb.Pais)
            Try
                Dim BECollection As List(Of MEb.Pais)

                Using DALObject As New DAL.Pais
                    BECollection = DALObject.List(instanciaSalaId, Estado, CantidadRegistros, NumeroPagina, Relations)
                End Using

                Return BECollection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
                Return Nothing
            End Try
        End Function


        Public Function List(ByVal ParamArray Relations() As [Enum]) As List(Of MEb.Pais)
            Try
                Dim BECollection As List(Of MEb.Pais)
                Using DALObject As New DAL.Pais
                    BECollection = DALObject.List(Relations)
                End Using
                Return BECollection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
                Return Nothing
            End Try
        End Function

        Public Function List(ByVal Filter As String, ByVal Order As String, ByVal ParamArray Relations() As [Enum]) As List(Of MEb.Pais)
            Try
                Dim BECollection As List(Of MEb.Pais)

                Using DALObject As New DAL.Pais
                    BECollection = DALObject.List(Filter, Order, Relations)
                End Using

                Return BECollection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
                Return Nothing
            End Try
        End Function

        ''' <summary>
        ''' 	Search for collection business objects of type Provider
        ''' </summary>
        ''' <param name="IdMaster">Relationship Identifier</param>
        ''' <param name="Relations">Relationship enumerator</param>
        ''' <returns>Object collection of type Provider</returns>
        ''' <remarks>
        ''' </remarks>
        Public Function List(ByVal IdMaster As Long, ByVal ParamArray Relations() As [Enum]) As List(Of MEb.Pais)
            Try
                Dim BECollection As List(Of MEb.Pais)

                Using DALObject As New DAL.Pais
                    BECollection = DALObject.List(IdMaster, Relations)
                End Using

                Return BECollection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
                Return Nothing
            End Try
        End Function

#End Region

#Region " Save Methods "

        ''' <summary>
        ''' 	Saves data object of type
        ''' </summary>
        ''' <param name="BEObj">Object type Classifiers</param>
        ''' <remarks>
        ''' </remarks>
        Public Sub Save(ByRef BEObj As MEb.Pais)
            Dim DALObject As DAL.Pais = Nothing


            Me.ErrorCollection.Clear()

            If Me.Validate(BEObj) Then
                Try
                    DALObject = New DAL.Pais
                    DALObject.OpenConnection()
                    DALObject.Guardar(BEObj)
                    DALObject.Commit()
                Catch ex As Exception
                    DALObject.Rollback()
                    MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
                Finally
                    DALObject.CloseConnection()
                End Try
            Else
                MyBase.ErrorHandler(New BCException(Me.ErrorCollection), ErrorPolicy.BCNew)
            End If
        End Sub

        ''' <summary>
        ''' 	Saves data object of type Classifier
        ''' </summary>
        ''' <param name="colBEObj">Object type Classifier</param>
        ''' <remarks>
        ''' </remarks>
        Friend Sub Save(ByRef colBEObj As List(Of MEb.Pais), ByVal IdMaster As Long)
            Dim DALObject As DAL.Pais = Nothing
            Try
                DALObject = New DAL.Pais
                DALObject.OpenConnection()
                DALObject.Guardar(colBEObj)
                DALObject.Commit()
            Catch ex As Exception
                DALObject.Rollback()
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
            Finally
                DALObject.CloseConnection()
            End Try
        End Sub

        ''' <summary>
        ''' 	Validates a business object before save it
        ''' </summary>
        ''' <param name="BEObj">Object Type Provider</param>
        ''' <returns>True: if object were validated</returns>
        ''' <remarks>
        ''' </remarks>
        Friend Function Validate(ByRef BEObj As MEb.Pais) As Boolean
            Dim bolOk As Boolean = True

            If BEObj.StatusType <> BE.StatusType.NoAction Then
                If BEObj.StatusType <> BE.StatusType.Insert Then
                    If BEObj.Id = 0 Then
                        MyBase.ErrorCollection.Add("No se ha proporcionado el Identificador")
                        bolOk = False
                    End If
                End If
            End If

            Return bolOk
        End Function

#End Region

#Region " Constructors "
        ''' <summary>
        '''  Default Constructors
        ''' </summary>
        ''' <remarks>
        '''	</remarks>
        Public Sub New()
            MyBase.New()
        End Sub

#End Region

    End Class

End Namespace

