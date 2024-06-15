Namespace Entidades
    Public Class VentaItem
        Public Sub New()
            ' Constructor vacío
        End Sub
        Public Sub New(row As DataRow)
            ID = Convert.ToInt32(row("ID"))
            IDVenta = Convert.ToInt32(row("IDVenta"))
            IDProducto = Convert.ToInt32(row("IDProducto"))
            PrecioUnitario = Convert.ToDouble(row("PrecioUnitario"))
            Cantidad = Convert.ToInt32(row("Cantidad"))
            PrecioTotal = Convert.ToDouble(row("PrecioTotal"))

        End Sub

        Public Property ID As Integer
        Public Property IDVenta As Integer
        Public Property IDProducto As Integer
        Public Property PrecioUnitario As Double
        Public Property Cantidad As Integer
        Public Property PrecioTotal As Double


    End Class

End Namespace

