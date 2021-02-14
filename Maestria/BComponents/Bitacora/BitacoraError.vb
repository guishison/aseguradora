Option Strict On
Option Compare Text
Option Explicit On
Option Infer On

Imports BAS = BEntities.Bitacora
Imports BE = BEntities
Imports DAL = DALayer.Bitacora

Namespace Bitacora
    <Serializable>
    Partial Public Class BitacoraError
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
        Public Function Search(ByVal Id As Int32, ByVal ParamArray Relaciones() As [Enum]) As BAS.BitacoraError
            Dim BEObject As BAS.BitacoraError = Nothing

            Try
                Using DALObject As New DAL.BitacoraError
                    BEObject = DALObject.Search(Id, Relaciones)
                End Using
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
            End Try
            Return BEObject
        End Function
        Public Function SearchPorComentario(ByVal IdPersona As Int32, ByVal ParamArray Relaciones() As [Enum]) As BAS.BitacoraError
            Dim BEObject As BAS.BitacoraError = Nothing

            Try
                Using DALObject As New DAL.BitacoraError
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
        'Public Function List(ByVal Order As String, ByVal ParamArray Relaciones() As [Enum]) As List(Of BAS.BitacoraError)
        '    Try
        '        Dim BECollection As List(Of BAS.BitacoraError)

        '        Using DALObject As New DAL.BitacoraError
        '            BECollection = DALObject.List(Order, Relaciones)
        '        End Using

        '        Return BECollection
        '    Catch ex As Exception
        '        MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
        '        Return Nothing
        '    End Try
        'End Function

        Public Function Listas(ByVal ParamArray Relaciones() As [Enum]) As List(Of BAS.BitacoraError)
            Dim BECollection As List(Of BAS.BitacoraError) = New List(Of BAS.BitacoraError)
            'Dim DALObject As DAL.BitacoraError = Nothing
            Me.ErrorCollection.Clear()
            Try
                Using DALObject = New DAL.BitacoraError
                    BECollection = DALObject.List(Relaciones)
                End Using
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
            End Try
            Return BECollection
        End Function
        Public Function Count(ByVal text As String, ByVal ParamArray Relations() As [Enum]) As List(Of BAS.BitacoraError)
            Try
                Dim BECollection As List(Of BAS.BitacoraError)

                Using DALObject As New DAL.BitacoraError
                    BECollection = DALObject.Count(text, Relations)
                End Using

                Return BECollection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
                Return Nothing
            End Try
        End Function
        Public Function ListAutorizados(ByVal ParamArray Relaciones() As [Enum]) As List(Of BAS.BitacoraError)
            Dim BECollection As List(Of BAS.BitacoraError) = New List(Of BAS.BitacoraError)
            'Dim DALObject As DAL.BitacoraError = Nothing
            Me.ErrorCollection.Clear()
            Try
                Using DALObject = New DAL.BitacoraError
                    BECollection = DALObject.ListAutorizados(Relaciones)
                End Using
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
            End Try
            Return BECollection
        End Function

        Public Function Listar(ByVal buscar As String, ByVal estado As Int16, PageIndex As Int32, PageSize As Int32, ByVal ParamArray Relations() As [Enum]) As List(Of BAS.BitacoraError)
            Dim BECollection As List(Of BAS.BitacoraError) = New List(Of BAS.BitacoraError)
            'Dim DALObject As DAL.BitacoraError = Nothing
            Me.ErrorCollection.Clear()
            Try
                Using DALObject = New DAL.BitacoraError
                    BECollection = DALObject.List(buscar, estado, PageIndex, PageSize, Relations)
                End Using
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
            End Try
            Return BECollection
        End Function

        'Public Function List(ByVal Filter As String, ByVal Order As String, ByVal ParamArray Relaciones() As [Enum]) As List(Of BAS.BitacoraError)
        '    Try
        '        Dim BECollection As List(Of BAS.BitacoraError)

        '        Using DALObject As New DAL.BitacoraError
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
        Public Function List(ByVal IdMaster As Int32, ByVal ParamArray Relaciones() As [Enum]) As List(Of BAS.BitacoraError)
            Try
                Dim BECollection As List(Of BAS.BitacoraError)

                Using DALObject As New DAL.BitacoraError
                    BECollection = DALObject.List(IdMaster, Relaciones)
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
        Public Sub Save(ByRef BEObj As BAS.BitacoraError)
            Dim DALObject As DAL.BitacoraError = Nothing
            Me.ErrorCollection.Clear()
            If Me.Validate(BEObj) Then
                Try
                    DALObject = New DAL.BitacoraError
                    DALObject.OpenConnection()
                    DALObject.Save(BEObj)
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

        ''' <summary>
        ''' 	Saves data object of type Classifier
        ''' </summary>
        ''' <param name="colBEObj">Object type Classifier</param>
        ''' <remarks>
        ''' </remarks>
        Friend Sub Save(ByRef colBEObj As List(Of BAS.BitacoraError), ByVal IdMaster As Int32)
            Dim DALObject As DAL.BitacoraError = Nothing
            Try
                DALObject = New DAL.BitacoraError
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
        ''' <param name="BEObj">Object Type Position</param>
        ''' <returns>True: if object were validated</returns>
        ''' <remarks>
        ''' </remarks>
        Friend Function Validate(ByRef BEObj As BAS.BitacoraError) As Boolean
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