Namespace Entidades
    Public Class Producto
        Public Sub New()
            ' Constructor vacío
        End Sub
        Public Sub New(row As DataRow)
            ID = Convert.ToInt32(row("ID"))
            NombreProducto = Convert.ToString(row("Nombre"))
            Precio = Convert.ToDouble(row("Precio"))
            Categoria = Convert.ToString(row("Categoria"))
        End Sub

        Public Property ID As Integer
        Public Property NombreProducto As String
        Public Property Precio As Double
        Public Property Categoria As String
    End Class
End Namespace
