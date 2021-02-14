
Option Strict On
Option Compare Text
Option Explicit On
Option Infer On

Imports BE = BEntities
Imports DAL = DALayer.Seguridad
Imports SEC = BEntities.Seguridad

Namespace Seguridad
    <Serializable>
    Public Class ParametroLogin
        Inherits BCEntity

#Region " Search Methods "

        ''' <summary>
        ''' 	Search for business objects of type Profile
        ''' </summary>
        ''' <param name="Id">Object identifier Profile</param>
        ''' <param name="Relations">relationship enumetators</param>
        ''' <returns>An object of type Profile</returns>
        ''' <remarks>
        ''' 	To get relationship objects, suply relationship enumetators
        ''' </remarks>
        Public Function Search(ByVal Id As Long, ByVal ParamArray Relations() As [Enum]) As SEC.ParametroLogin
            Dim BEObject As SEC.ParametroLogin = Nothing

            Try
                Using DALObject As New DAL.ParametroLogin
                    BEObject = DALObject.Buscar(Id, Relations)
                End Using
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
            End Try
            Return BEObject
        End Function

#End Region

#Region " List Methods "

        ''' <summary>
        ''' 	Search for collection business objects of type Profile
        ''' </summary>
        ''' <param name="Order">Property column to specify collection order</param>
        ''' <param name="Relations">Relationship enumerator</param>
        ''' <returns>Object collection of type Profile</returns>
        ''' <remarks>
        ''' 	To get relationship objects, suply relationship enumetators
        ''' </remarks>
        'Public Function Listar(ByVal Order As String, ByVal ParamArray Relations() As [Enum]) As List(Of SEC.ParametroLogin)
        '    Try
        '        Dim BECollection As List(Of SEC.ParametroLogin)

        '        Using DALObject As New DAL.ParametroLogin
        '            BECollection = DALObject.Listar(Relations)
        '        End Using

        '        Return BECollection
        '    Catch ex As Exception
        '        MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
        '        Return Nothing
        '    End Try
        'End Function
        Public Function Listar(ByVal ParamArray Relations() As [Enum]) As List(Of SEC.ParametroLogin)
            Dim BECollection As List(Of SEC.ParametroLogin) = New List(Of SEC.ParametroLogin)
            Me.ErrorCollection.Clear()
            Try
                Using DALObject = New DAL.ParametroLogin
                    BECollection = DALObject.Listar(Relations)
                End Using
                Return BECollection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
                Return Nothing
            End Try
        End Function
        Public Function ListarTodo(ByVal ParamArray Relations() As [Enum]) As List(Of SEC.ParametroLogin)
            Dim BECollection As List(Of SEC.ParametroLogin) = New List(Of SEC.ParametroLogin)
            Me.ErrorCollection.Clear()
            Try
                Using DALObject = New DAL.ParametroLogin
                    BECollection = DALObject.ListarTodo(Relations)
                End Using
                Return BECollection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
                Return Nothing
            End Try
        End Function

        Public Function ListFilter(PageIndex As Integer, PageSize As Integer, order As String, ByVal ParamArray Relations() As [Enum]) As List(Of SEC.ParametroLogin)
            Try
                Dim BECollection As List(Of SEC.ParametroLogin)
                Using DALObject As New DAL.ParametroLogin
                    BECollection = DALObject.PagedFilterList(PageIndex, PageSize, order, Relations)
                End Using

                Return BECollection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
                Return Nothing
            End Try
        End Function

        Public Function Count() As Long
            Dim lngResult As Long = 0

            Try
                Using DALObject As New DAL.ParametroLogin
                    lngResult = DALObject.Count()
                End Using
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
            End Try
            Return lngResult
        End Function

        ''' <summary>
        ''' 	Search for collection business objects of type Profile
        ''' </summary>
        ''' <param name="IdMaster">Relationship Identifier</param>
        ''' <param name="Relations">Relationship enumerator</param>
        ''' <returns>Object collection of type Profile</returns>
        ''' <remarks>
        ''' </remarks>
        'Public Function Listar(ByVal IdMaster As Long, ByVal ParamArray Relations() As [Enum]) As List(Of SEC.ParametroLogin)
        '    Try
        '        Dim BECollection As List(Of SEC.ParametroLogin)

        '        Using DALObject As New DAL.ParametroLogin
        '            BECollection = DALObject.Listar(IdMaster, Relations)
        '        End Using

        '        Return BECollection
        '    Catch ex As Exception
        '        MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
        '        Return Nothing
        '    End Try
        'End Function

#End Region

#Region " Save Methods "

        ''' <summary>
        ''' 	Saves data object of type 
        ''' </summary>
        ''' <param name="BEObj">Object type Profile</param>    	
        ''' <remarks>
        ''' </remarks>
        Public Sub Guardar(ByRef BEObj As SEC.ParametroLogin)
            Dim DALObject As DAL.ParametroLogin = Nothing

            Me.ErrorCollection.Clear()

            If Me.Validar(BEObj) Then
                Try
                    DALObject = New DAL.ParametroLogin
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
        Public Sub Guardar(ByRef BEObj As List(Of SEC.ParametroLogin))
            Dim DALObject As DAL.ParametroLogin = Nothing

            Me.ErrorCollection.Clear()

            Try
                DALObject = New DAL.ParametroLogin
                DALObject.OpenConnection()
                DALObject.Guardar(BEObj)
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
        ''' <param name="BEObj">Object Type Profile</param>
        ''' <returns>True: if object were validated</returns>
        ''' <remarks>
        ''' </remarks>
        Friend Function Validar(ByRef BEObj As SEC.ParametroLogin) As Boolean
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