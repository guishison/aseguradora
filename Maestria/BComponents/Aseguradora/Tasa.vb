﻿Option Strict On
Option Compare Text
Option Explicit On
Option Infer On

Imports BE = BEntities
Imports DAL = DALayer.Aseguradora
Imports MEB = BEntities.Aseguradora

Namespace Aseguradora
    <Serializable>
    Public Class Tasa
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
        Public Function Search(ByVal Id As Int32, ByVal ParamArray Relations() As [Enum]) As MEB.Tasa
            Dim BEObject As MEB.Tasa = Nothing

            Try
                Using DALObject As New DAL.Tasa
                    BEObject = DALObject.Search(Id, Relations)
                End Using
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
            End Try
            Return BEObject
        End Function
        Public Function Search(ByVal UnidadNegocioId As Int32, ByVal CiudadId As Int32, ByVal TipoVehiculoId As Int32, ByVal ParamArray Relations() As [Enum]) As MEB.Tasa
            Dim BEObject As MEB.Tasa = Nothing

            Try
                Using DALObject As New DAL.Tasa
                    BEObject = DALObject.SearchByCiudadTipoVehiculo(UnidadNegocioId, CiudadId, TipoVehiculoId, Relations)
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
        Public Function List(ByVal UnidadNegocioId As Int32, ByVal CantidadRegistros As Int32, ByVal NumeroPagina As Int32, ByVal ParamArray Relations() As [Enum]) As List(Of MEB.Tasa)
            Try
                Dim BECollection As List(Of MEB.Tasa)

                Using DALObject As New DAL.Tasa
                    BECollection = DALObject.List(UnidadNegocioId, CantidadRegistros, NumeroPagina, Relations)
                End Using

                Return BECollection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
                Return Nothing
            End Try
        End Function


        Public Function ListCount(ByVal UnidadNegocioId As Int32) As Int32
            'Dim lngResult As Long = 0

            Dim BECollection As Int32 = 0
            Me.ErrorCollection.Clear()
            Try
                Using DALObject As New DAL.Tasa
                    BECollection = DALObject.ListCount(UnidadNegocioId)
                End Using
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
            End Try
            Return BECollection
            'Return lngResult
        End Function

        Public Function List(ByVal ParamArray Relations() As [Enum]) As List(Of MEB.Tasa)
            Try
                Dim BECollection As List(Of MEB.Tasa)
                Using DALObject As New DAL.Tasa
                    BECollection = DALObject.List(Relations)
                End Using
                Return BECollection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
                Return Nothing
            End Try
        End Function

        Public Function List(ByVal Filter As String, ByVal Order As String, ByVal ParamArray Relations() As [Enum]) As List(Of MEB.Tasa)
            Try
                Dim BECollection As List(Of MEB.Tasa)

                Using DALObject As New DAL.Tasa
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
        Public Function List(ByVal IdMaster As Long, ByVal ParamArray Relations() As [Enum]) As List(Of MEB.Tasa)
            Try
                Dim BECollection As List(Of MEB.Tasa)

                Using DALObject As New DAL.Tasa
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
        Public Sub Save(ByRef BEObj As MEB.Tasa)
            Dim DALObject As DAL.Tasa = Nothing


            Me.ErrorCollection.Clear()

            If Me.Validate(BEObj) Then
                Try
                    DALObject = New DAL.Tasa
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
        Friend Sub Save(ByRef colBEObj As List(Of MEB.Tasa), ByVal IdMaster As Long)
            Dim DALObject As DAL.Tasa = Nothing
            Try
                DALObject = New DAL.Tasa
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
        Friend Function Validate(ByRef BEObj As MEB.Tasa) As Boolean
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

