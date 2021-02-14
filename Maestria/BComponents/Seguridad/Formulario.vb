
Option Strict On
Option Compare Text
Option Explicit On
Option Infer On
Imports BE = BEntities
Imports DAL = DALayer.Seguridad
Imports SEC = BEntities.Seguridad

Namespace Seguridad
    <Serializable>
    Public Class Formulario
        Inherits BCEntity

#Region " Search Methods "

        ''' <summary>
        ''' 	Search for business objects of type Option
        ''' </summary>
        ''' <param name="Id">Object identifier Option</param>
        ''' <param name="Relations">relationship enumetators</param>
        ''' <returns>An object of type Option</returns>
        ''' <remarks>
        ''' 	To get relationship objects, suply relationship enumetators
        ''' </remarks>
        Public Function Buscar(ByVal Id As Int32, ByVal ParamArray Relations() As [Enum]) As SEC.Formulario
            Dim BEObject As SEC.Formulario = Nothing
            Me.ErrorCollection.Clear()
            Try
                Using DALObject = New DAL.Formulario
                    BEObject = DALObject.Buscar(Id, Relations)
                End Using 'DALObject.Commit()
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
            End Try
            Return BEObject
        End Function
        Public Function BuscarPorUnidadNegocio(ByVal Url As String, ByVal UnidadNegocioId As Int32, ByVal ParamArray Relations() As [Enum]) As SEC.Formulario
            Dim BEObject As SEC.Formulario = Nothing
            Me.ErrorCollection.Clear()
            Try
                Using DALObject = New DAL.Formulario
                    BEObject = DALObject.BuscarporUnidadNegocio(Url, UnidadNegocioId, Relations)
                End Using 'DALObject.Commit()
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
            End Try
            Return BEObject
        End Function

#End Region

#Region " List Methods "

        ''' <summary>
        ''' 	Search for collection business objects of type Option
        ''' </summary>
        ''' <param name="Order">Property column to specify collection order</param>
        ''' <param name="Relations">Relationship enumerator</param>
        ''' <returns>Object collection of type Option</returns>
        ''' <remarks>
        ''' 	To get relationship objects, suply relationship enumetators
        ''' </remarks>
        Public Function Listar(ByVal Order As String, ByVal ParamArray Relations() As [Enum]) As List(Of SEC.Formulario)
            Try
                Dim BECollection As List(Of SEC.Formulario)

                Using DALObject As New DAL.Formulario
                    BECollection = DALObject.Listar(Order, Relations)
                End Using

                Return BECollection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
                Return Nothing
            End Try
        End Function
        Public Function ListByUrlContains(ByVal Url As String, ByVal ParamArray Relations() As [Enum]) As List(Of SEC.Formulario)
            Try
                Dim BECollection As List(Of SEC.Formulario)

                Using DALObject As New DAL.Formulario
                    BECollection = DALObject.ListByUrlContains(Url, Relations)
                End Using

                Return BECollection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
                Return Nothing
            End Try
        End Function
        Public Function List(ByVal HotelId As Int32, ByVal ParamArray Relations() As [Enum]) As List(Of SEC.Formulario)
            Try
                Dim BECollection As List(Of SEC.Formulario)

                Using DALObject As New DAL.Formulario
                    BECollection = DALObject.List(HotelId)
                End Using

                Return BECollection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
                Return Nothing
            End Try
        End Function
        Public Function ListByModulo(ByVal ModuloId As Int32, ByVal ParamArray Relations() As [Enum]) As List(Of SEC.Formulario)
            Try
                Dim BECollection As List(Of SEC.Formulario)

                Using DALObject As New DAL.Formulario
                    BECollection = DALObject.ListByModulo(ModuloId)
                End Using

                Return BECollection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
                Return Nothing
            End Try
        End Function
        Public Function ReturnListaChild(ByVal ids As IEnumerable(Of String), ByVal ParamArray Relations() As [Enum]) As List(Of SEC.Formulario)
            Try
                Dim BECollection As List(Of SEC.Formulario)

                Using DALObject As New DAL.Formulario
                    BECollection = DALObject.ReturnChild2(ids, Relations)
                End Using

                Return BECollection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
                Return Nothing
            End Try
        End Function

        '' <summary>
        '' 	Search for collection business objects of type Option
        '' </summary>
        '' <param name="IdMaster">Relationship Identifier</param>
        '' <param name="Relations">Relationship enumerator</param>
        '' <returns>Object collection of type Option</returns>
        '' <remarks>
        '' </remarks>
        'Public Function Listar(ByVal IdMaster As Long, ByVal ParamArray Relations() As [Enum]) As List(Of SEC.Formulario)
        '    Try
        '        Dim BECollection As List(Of SEC.Formulario)

        '        Using DALObject As New DAL.Formulario
        '            BECollection = DALObject.List(IdMaster, Relations)
        '        End Using

        '        Return BECollection
        '    Catch ex As Exception
        '        MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
        '        Return Nothing
        '    End Try
        'End Function

        Public Function ListMenuOptions(ByVal Id As Int32, ByVal UnidadNegocioId As Int32, ByVal ParamArray Relaciones() As [Enum]) As List(Of SEC.Formulario)
            Dim BEObject As List(Of SEC.Formulario) = Nothing
            Me.ErrorCollection.Clear()
            Try
                Using DALObject = New DAL.Formulario
                    BEObject = DALObject.ListMenuOptions(Id, UnidadNegocioId, Relaciones)
                End Using 'DALObject.Commit()
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
            End Try
            Return BEObject
        End Function


        Public Function ListaMenuFormulario(ByVal Id As Int32, ByVal UnidadNegocioId As Int32) As List(Of SEC.Formulario)
            Try
                Dim BECollection As List(Of SEC.Formulario)

                Using DALObject As New DAL.Formulario
                    BECollection = DALObject.ListaMenuFormulario(Id, UnidadNegocioId)
                End Using

                Return BECollection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
                Return Nothing
            End Try
        End Function
#End Region

#Region " Save Methods "

        Public Sub Guardar(ByRef BEObj As SEC.Formulario)
            Dim DALObject As DAL.Formulario = Nothing
            Me.ErrorCollection.Clear()

            If Me.Validate(BEObj) Then
                Try
                    DALObject = New DAL.Formulario
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
        Public Sub Guardar(ByRef BEObj As List(Of SEC.Formulario))
            Dim DALObject As DAL.Formulario = Nothing
            Me.ErrorCollection.Clear()

            'If Me.Validar(BEObj) Then
            Try
                DALObject = New DAL.Formulario
                DALObject.OpenConnection()
                DALObject.Save(BEObj)
                DALObject.Commit()
            Catch ex As Exception
                DALObject.Rollback()
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
            Finally
                DALObject.CloseConnection()
            End Try
            'Else
            '    MyBase.ErrorHandler(New BCException(Me.ErrorCollection), ErrorPolicy.BCNew)
            'End If
        End Sub

        ''' <summary>
        ''' 	Validates a business object before save it 
        ''' </summary>
        ''' <param name="BEObj">Object Type Option</param>
        ''' <returns>True: if object were validated</returns>
        ''' <remarks>
        ''' </remarks>
        Friend Function Validate(ByRef BEObj As SEC.Formulario) As Boolean
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