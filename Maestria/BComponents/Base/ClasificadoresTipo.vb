
Option Strict On
Option Compare Text
Option Explicit On
Option Infer On

Imports BAS = BEntities.Base
Imports BE = BEntities
Imports DAL = DALayer.Base

Namespace Base
    <Serializable> Partial Public Class ClasificadoresTipo
        Inherits BCEntity

#Region " Search Methods "

        Public Function Search(ByVal Id As Int32, ByVal ParamArray Relations() As [Enum]) As BAS.ClasificadoresTipo
            Dim BEObject As BAS.ClasificadoresTipo = Nothing

            Try
                Using DALObject As New DAL.ClasificadoresTipo
                    BEObject = DALObject.Search(Id, Relations)
                End Using
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
            End Try
            Return BEObject
        End Function
        Public Function SearchPorId(ByVal Id As Int32, ByVal ParamArray Relations() As [Enum]) As BAS.ClasificadoresTipo
            Dim BEObject As BAS.ClasificadoresTipo = Nothing

            Try
                Using DALObject As New DAL.ClasificadoresTipo
                    BEObject = DALObject.SearchPorId(Id, Relations)
                End Using
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
            End Try
            Return BEObject
        End Function


#End Region

#Region " List Methods "

        Public Function List(ByVal ParamArray Relations() As [Enum]) As List(Of BAS.ClasificadoresTipo)
            Try
                Dim BECollection As List(Of BAS.ClasificadoresTipo)

                Using DALObject As New DAL.ClasificadoresTipo
                    BECollection = DALObject.List(Relations)
                End Using

                Return BECollection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
                Return Nothing
            End Try
        End Function

        Public Function List() As List(Of BAS.ClasificadoresTipo)
            Try
                Dim BECollection As List(Of BAS.ClasificadoresTipo)

                Using DALObject As New DAL.ClasificadoresTipo
                    BECollection = DALObject.List()
                End Using

                Return BECollection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
                Return Nothing
            End Try
        End Function

        Public Function List(ByVal text As String, ByVal Estado As Int16, ByVal PageIndex As Int32, ByVal pageSize As Int32, ByVal ParamArray Relations() As [Enum]) As List(Of BAS.ClasificadoresTipo)
            Try
                Dim BECollection As List(Of BAS.ClasificadoresTipo)

                Using DALObject As New DAL.ClasificadoresTipo
                    BECollection = DALObject.List(text, Estado, PageIndex, pageSize, Relations)
                End Using

                Return BECollection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
                Return Nothing
            End Try
        End Function
        Public Function Count(ByVal text As String, ByVal ParamArray Relations() As [Enum]) As List(Of BAS.ClasificadoresTipo)
            Try
                Dim BECollection As List(Of BAS.ClasificadoresTipo)

                Using DALObject As New DAL.ClasificadoresTipo
                    BECollection = DALObject.Count(text, Relations)
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
        ''' <param name="BEObj">Object type ClassifierType</param>    	
        ''' <remarks>
        ''' </remarks>
        Public Sub Save(ByRef BEObj As BAS.ClasificadoresTipo)
            Dim DALObject As DAL.ClasificadoresTipo = Nothing
            Me.ErrorCollection.Clear()
            If Me.Validate(BEObj) Then
                Try
                    DALObject = New DAL.ClasificadoresTipo
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
        ''' 	Saves data object of type Classifier
        ''' </summary>
        ''' <param name="colBEObj">Object type Classifier</param>
        ''' <remarks>
        ''' </remarks>
        Friend Sub Save(ByRef colBEObj As List(Of BAS.ClasificadoresTipo), ByVal IdMaster As Int32)
            Dim DALObject As DAL.ClasificadoresTipo = Nothing
            Try
                DALObject = New DAL.ClasificadoresTipo
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
        ''' <param name="BEObj">Object Type ClassifierType</param>
        ''' <returns>True: if object were validated</returns>
        ''' <remarks>
        ''' </remarks>
        Friend Function Validate(ByRef BEObj As BAS.ClasificadoresTipo) As Boolean
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