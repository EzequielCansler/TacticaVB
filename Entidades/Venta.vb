Namespace Entidades
    Public Class Venta
        Public Sub New()
            ' Constructor vacío
        End Sub
        Public Sub New(row As DataRow)
            ID = Convert.ToInt32(row("ID"))
            IDCliente = Convert.ToInt32(row("IDCliente"))
            Fecha = Convert.ToDateTime(row("Fecha"))
            Total = Convert.ToDouble(row("Total"))
        End Sub

        Public Property ID As Integer
        Public Property IDCliente As Integer
        Public Property Fecha As Date
        Public Property Total As Double
    End Class

End Namespace

