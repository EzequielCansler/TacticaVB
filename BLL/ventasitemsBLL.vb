Imports Entidades.Entidades
Imports DAL.DAL

Namespace BLL
    Public Class ventasitemsBLL
        Public Function ListarItems() As List(Of VentaItem)
            Dim ventaItemDAL As New VentasItemDAL()
            Return ventaItemDAL.Listar()
        End Function

    End Class
End Namespace
