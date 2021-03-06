﻿
Option Strict On
Option Compare Text
Option Explicit On
Option Infer On

Imports BAS = BEntities.Base
Imports BE = BEntities
Imports DAL = DALayer.Base

Namespace Base
    <Serializable> Partial Public Class UnidadNegocio
        Inherits BCEntity

#Region " Search Methods "

        ''' <summary>
        ''' 	Search for business objects of type BusinessUnit
        ''' </summary>
        ''' <param name="Id">Object identifier BusinessUnit</param>
        ''' <param name="Relations">relationship enumetators</param>
        ''' <returns>An object of type BusinessUnit</returns>
        ''' <remarks>
        ''' 	To get relationship objects, suply relationship enumetators
        ''' </remarks>
        Public Function Search(ByVal Id As Int32) As BAS.UnidadNegocio
            Dim BEObject As BAS.UnidadNegocio = Nothing

            Try
                Using DALObject As New DAL.UnidadNegocio
                    BEObject = DALObject.Search(Id)
                End Using
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
            End Try
            Return BEObject
        End Function

        ''' <summary>
        ''' 	Search for business objects of type BusinessUnit
        ''' </summary>
        ''' <param name="PlaceId">Object identifier BusinessUnit</param>
        ''' <param name="Relations">relationship enumetators</param>
        ''' <returns>An object of type BusinessUnit</returns>
        ''' <remarks>
        ''' 	To get relationship objects, suply relationship enumetators
        ''' </remarks>
        Public Function SearchByPlace(ByVal PlaceId As Int32, ByVal ParamArray Relations() As [Enum]) As BAS.UnidadNegocio
            Dim BEObject As BAS.UnidadNegocio = Nothing

            Try
                Using DALObject As New DAL.UnidadNegocio
                    BEObject = DALObject.SearchByPlace(PlaceId, Relations)
                End Using
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
            End Try
            Return BEObject
        End Function

#End Region

#Region " List Methods "

        ''' <summary>
        ''' 	Search for collection business objects of type BusinessUnit
        ''' </summary>
        ''' <param name="Relations">Relationship enumerator</param>
        ''' <returns>Object collection of type BusinessUnit</returns>
        ''' <remarks>
        ''' 	To get relationship objects, suply relationship enumetators
        ''' </remarks>


        Public Function List(ByVal ParamArray Relations() As [Enum]) As List(Of BAS.UnidadNegocio)
            Try
                Dim BECollection As List(Of BAS.UnidadNegocio)

                Using DALObject As New DAL.UnidadNegocio
                    BECollection = DALObject.List(Relations)
                End Using

                Return BECollection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
                Return Nothing
            End Try
        End Function

        ''' <summary>
        ''' 	Search for collection business objects of type BusinessUnit
        ''' </summary>
        ''' <param name="IdMaster">Relationship Identifier</param>
        ''' <param name="Relations">Relationship enumerator</param>
        ''' <returns>Object collection of type BusinessUnit</returns>
        ''' <remarks>
        ''' </remarks>
        Public Function List(ByVal IdMaster As Int32, ByVal ParamArray Relations() As [Enum]) As List(Of BAS.UnidadNegocio)
            Try
                Dim BECollection As List(Of BAS.UnidadNegocio)

                Using DALObject As New DAL.UnidadNegocio
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
        ''' <param name="BEObj">Object type BusinessUnit</param>    	
        ''' <remarks>
        ''' </remarks>
        Public Sub Save(ByRef BEObj As BAS.UnidadNegocio)
            Dim DALObject As DAL.UnidadNegocio = Nothing
            Me.ErrorCollection.Clear()
            If Me.Validate(BEObj) Then
                Try
                    DALObject = New DAL.UnidadNegocio
                    DALObject.OpenConnection()
                    DALObject.Save(BEObj)
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
        ''' 	Saves data object of type BusinessUnit
        ''' </summary>
        ''' <param name="colBEObj">Object type BusinessUnit</param>
        ''' <remarks>
        ''' </remarks>
        Friend Sub Save(ByRef colBEObj As List(Of BAS.UnidadNegocio), ByVal IdMaster As Int32)
            Dim DALObject As DAL.UnidadNegocio = Nothing
            Try
                DALObject = New DAL.UnidadNegocio
                DALObject.OpenConnection()
                DALObject.Save(colBEObj, IdMaster)
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
        ''' <param name="BEObj">Object Type BusinessUnit</param>
        ''' <returns>True: if object were validated</returns>
        ''' <remarks>
        ''' </remarks>
        Friend Function Validate(ByRef BEObj As BAS.UnidadNegocio) As Boolean
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
