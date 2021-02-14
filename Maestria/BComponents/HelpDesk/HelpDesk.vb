﻿Option Strict On
Option Compare Text
Option Explicit On
Option Infer On

Imports BAS = BEntities.AyudaUsuario
Imports BE = BEntities
Imports DAL = DALayer.AyudaUsuario

Namespace AyudaUsuario
    <Serializable>
    Partial Public Class HelpDesk
        Inherits BCEntity

#Region " Search Methods "

        ''' <summary>
        ''' 	Search for business objects of type Position
        ''' </summary>
        ''' <param name="Id">Object identifier Position</param>
        ''' <param name="Relaciones">Relacioneship enumetators</param>
        ''' <returns>An object of type Position</returns>
        ''' <remarks>
        ''' 	To get Relacioneship objects, suply Relacioneship enumetators
        ''' </remarks>
        Public Function Search(ByVal Id As Int32, ByVal ParamArray Relaciones() As [Enum]) As BAS.HelpDesk
            Dim BEObject As BAS.HelpDesk = Nothing

            Try
                Using DALObject As New DAL.HelpDesk
                    BEObject = DALObject.Search(Id, Relaciones)
                End Using
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
            End Try
            Return BEObject
        End Function
        Public Function SearchPorComentario(ByVal IdPersona As Int32, ByVal ParamArray Relaciones() As [Enum]) As BAS.HelpDesk
            Dim BEObject As BAS.HelpDesk = Nothing

            Try
                Using DALObject As New DAL.HelpDesk
                    BEObject = DALObject.SearchPorComentario(IdPersona, Relaciones)
                End Using
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
            End Try
            Return BEObject
        End Function

#End Region

#Region " List Methods "

        '' <summary>
        '' 	Search for collection business objects of type Position
        '' </summary>
        '' <param name="Order">Property column to specify collection order</param>
        '' <param name="Relaciones">Relacioneship enumerator</param>
        '' <returns>Object collection of type Position</returns>
        '' <remarks>
        '' 	To get Relacioneship objects, suply Relacioneship enumetators
        '' </remarks>
        'Public Function List(ByVal Order As String, ByVal ParamArray Relaciones() As [Enum]) As List(Of BAS.HelpDesk)
        '    Try
        '        Dim BECollection As List(Of BAS.HelpDesk)

        '        Using DALObject As New DAL.HelpDesk
        '            BECollection = DALObject.List(Order, Relaciones)
        '        End Using

        '        Return BECollection
        '    Catch ex As Exception
        '        MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
        '        Return Nothing
        '    End Try
        'End Function

        Public Function Listas(ByVal Texto As String, ByVal Inicial As DateTime, ByVal Final As DateTime, ByVal EstadoHelpDesk As Int32, PageIndex As Int32, PageSize As Int32, ByVal ParamArray Relaciones() As [Enum]) As List(Of BAS.HelpDesk)
            Dim BECollection As List(Of BAS.HelpDesk) = New List(Of BAS.HelpDesk)
            'Dim DALObject As DAL.HelpDesk = Nothing
            Me.ErrorCollection.Clear()
            Try
                Using DALObject = New DAL.HelpDesk
                    BECollection = DALObject.ListaBusqueda(Texto, Inicial, Final, EstadoHelpDesk, PageIndex, PageSize, Relaciones)
                End Using
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
            End Try
            Return BECollection
        End Function
        Public Function ListasCount(ByVal Texto As String, ByVal Inicial As DateTime, ByVal Final As DateTime, ByVal EstadoHelpDesk As Int32, ByVal ParamArray Relaciones() As [Enum]) As List(Of BAS.HelpDesk)
            Dim BECollection As List(Of BAS.HelpDesk) = New List(Of BAS.HelpDesk)
            'Dim DALObject As DAL.HelpDesk = Nothing
            Me.ErrorCollection.Clear()
            Try
                Using DALObject = New DAL.HelpDesk
                    BECollection = DALObject.ListaBusquedaCount(Texto, Inicial, Final, EstadoHelpDesk, Relaciones)
                End Using
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
            End Try
            Return BECollection
        End Function
        Public Function Count(ByVal text As String, ByVal ParamArray Relations() As [Enum]) As List(Of BAS.HelpDesk)
            Try
                Dim BECollection As List(Of BAS.HelpDesk)

                Using DALObject As New DAL.HelpDesk
                    BECollection = DALObject.Count(text, Relations)
                End Using

                Return BECollection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
                Return Nothing
            End Try
        End Function
        Public Function ListAutorizados(ByVal ParamArray Relaciones() As [Enum]) As List(Of BAS.HelpDesk)
            Dim BECollection As List(Of BAS.HelpDesk) = New List(Of BAS.HelpDesk)
            'Dim DALObject As DAL.HelpDesk = Nothing
            Me.ErrorCollection.Clear()
            Try
                Using DALObject = New DAL.HelpDesk
                    BECollection = DALObject.ListAutorizados(Relaciones)
                End Using
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
            End Try
            Return BECollection
        End Function

        Public Function Listar(ByVal buscar As String, ByVal estado As Int16, PageIndex As Int32, PageSize As Int32, ByVal ParamArray Relations() As [Enum]) As List(Of BAS.HelpDesk)
            Dim BECollection As List(Of BAS.HelpDesk) = New List(Of BAS.HelpDesk)
            'Dim DALObject As DAL.HelpDesk = Nothing
            Me.ErrorCollection.Clear()
            Try
                Using DALObject = New DAL.HelpDesk
                    BECollection = DALObject.List(buscar, estado, PageIndex, PageSize, Relations)
                End Using
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
            End Try
            Return BECollection
        End Function

        'Public Function List(ByVal Filter As String, ByVal Order As String, ByVal ParamArray Relaciones() As [Enum]) As List(Of BAS.HelpDesk)
        '    Try
        '        Dim BECollection As List(Of BAS.HelpDesk)

        '        Using DALObject As New DAL.HelpDesk
        '            BECollection = DALObject.List(Filter, Order, Relaciones)
        '        End Using

        '        Return BECollection
        '    Catch ex As Exception
        '        MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
        '        Return Nothing
        '    End Try
        'End Function

        ''' <summary>
        ''' 	Search for collection business objects of type Position
        ''' </summary>
        ''' <param name="IdMaster">Relacioneship Identifier</param>
        ''' <param name="Relaciones">Relacioneship enumerator</param>
        ''' <returns>Object collection of type Position</returns>
        ''' <remarks>
        ''' </remarks>
        Public Function List(ByVal IdMaster As Int32, ByVal ParamArray Relaciones() As [Enum]) As List(Of BAS.HelpDesk)
            Try
                Dim BECollection As List(Of BAS.HelpDesk)

                Using DALObject As New DAL.HelpDesk
                    BECollection = DALObject.List(IdMaster, Relaciones)
                End Using

                Return BECollection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
                Return Nothing
            End Try
        End Function
        Public Function Listita(ByVal ParamArray Relaciones() As [Enum]) As List(Of BAS.HelpDesk)
            Try
                Dim BECollection As List(Of BAS.HelpDesk)

                Using DALObject As New DAL.HelpDesk
                    BECollection = DALObject.List(Relaciones)
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
        ''' <param name="BEObj">Object type Position</param>
        ''' <remarks>
        ''' </remarks>
        Public Sub Save(ByRef BEObj As BAS.HelpDesk)
            Dim DALObject As DAL.HelpDesk = Nothing
            Dim DALHelpDeskDetalle As DAL.HelpDeskDetalle = Nothing
            Me.ErrorCollection.Clear()
            If Me.Validate(BEObj) Then
                Try
                    DALObject = New DAL.HelpDesk
                    DALObject.OpenConnection()
                    DALObject.Save(BEObj)
                    DALHelpDeskDetalle = New DAL.HelpDeskDetalle(True, CObj(DALObject.DBFactory), DALObject.Transaction)
                    For Each item In BEObj.ListaDetalleHelpDesk
                        item.HelpDeskId = BEObj.Id
                    Next
                    DALHelpDeskDetalle.Save(BEObj.ListaDetalleHelpDesk)
                    DALObject.Commit()
                Catch ex As Exception
                    MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
                    DALObject.Rollback()
                Finally
                    DALObject.CloseConnection()
                End Try
            Else
                MyBase.ErrorHandler(New BCException(Me.ErrorCollection), ErrorPolicy.BCNew)
            End If
        End Sub

        '''' <summary>
        '''' 	Saves data object of type Classifier
        '''' </summary>
        '''' <param name="colBEObj">Object type Classifier</param>
        '''' <remarks>
        '''' </remarks>
        'Friend Sub Save(ByRef colBEObj As List(Of BAS.HelpDesk), ByVal IdMaster As Int32)
        '    Dim DALObject As DAL.HelpDesk = Nothing
        '    Try
        '        DALObject = New DAL.HelpDesk
        '        DALObject.OpenConnection()
        '        DALObject.Save(colBEObj, IdMaster)
        '        DALObject.Commit()
        '    Catch ex As Exception
        '        DALObject.Rollback()
        '        MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
        '    Finally
        '        DALObject.CloseConnection()
        '    End Try
        'End Sub

        ''' <summary>
        ''' 	Validates a business object before save it
        ''' </summary>
        ''' <param name="BEObj">Object Type Position</param>
        ''' <returns>True: if object were validated</returns>
        ''' <remarks>
        ''' </remarks>
        Friend Function Validate(ByRef BEObj As BAS.HelpDesk) As Boolean
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