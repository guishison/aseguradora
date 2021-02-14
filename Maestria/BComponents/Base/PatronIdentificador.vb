Option Strict On
Option Compare Text
Option Explicit On
Option Infer On
Imports DAL = DALayer.Base
Imports MEB = BEntities.Base


Namespace Base
    <Serializable>
    Public Class PatronIdentificador
        Inherits BCEntity
        Public Function ListByCountry(ByVal Id As Int32) As List(Of MEB.PatronIdentificador)
            Try
                Dim BECollection As List(Of MEB.PatronIdentificador)

                Using DALObject As New DAL.PatronIdentificador
                    BECollection = DALObject.ListByCountry(Id)
                End Using

                Return BECollection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
                Return Nothing
            End Try
        End Function

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