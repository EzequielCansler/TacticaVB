
Namespace Util
    Public Class Convertidor
        Public Shared Function ConvertirAVentaItem(ventaItemJSON As VentaItemJSON) As Entidades.Entidades.VentaItem
            Return New Entidades.Entidades.VentaItem() With {
                .IDProducto = ventaItemJSON.ProductoID,
                .Cantidad = ventaItemJSON.Cantidad
            }
        End Function
    End Class
End Namespace
