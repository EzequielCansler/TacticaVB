Imports System.Data.SqlClient
Imports Entidades.Entidades

Namespace DAL
    Public Class VentasDAL

        Private oConexion As SqlDBHelper

        Public Sub New()
            oConexion = New SqlDBHelper()
        End Sub

        Public Function Listar() As List(Of Venta)
            Dim ventas As New List(Of Venta)()

            Dim cmdComando As New SqlCommand()
            cmdComando.CommandText = "SELECT * FROM ventas"
            cmdComando.CommandType = CommandType.Text

            Dim TablaResultante As DataTable = oConexion.EjecutarSentenciaSQL(cmdComando)
            For Each fila As DataRow In TablaResultante.Rows
                ventas.Add(New Venta(fila))
            Next

            Return ventas
        End Function

        Public Function AgregarVenta(items As List(Of VentaItem), Fecha As Date, precioTotal As Double, clienteId As Integer) As Boolean
            Try

                Dim cmdComando As New SqlCommand()


                cmdComando.CommandText = "INSERT INTO ventas (IDCliente, Fecha, Total) VALUES (@IDCliente, @Fecha, @Total); SELECT SCOPE_IDENTITY();"
                cmdComando.CommandType = CommandType.Text

                cmdComando.Parameters.AddWithValue("@IDCliente", clienteId)
                cmdComando.Parameters.AddWithValue("@Fecha", Fecha)
                cmdComando.Parameters.AddWithValue("@Total", precioTotal)

                Dim ventaID As Integer = Convert.ToInt32(oConexion.EjecutarEscalar(cmdComando))

                For Each item As VentaItem In items
                    cmdComando.CommandText = "INSERT INTO ventasitems (IDVenta, IDProducto, PrecioUnitario, Cantidad, PrecioTotal) VALUES (@IDVenta, @IDProducto, @PrecioUnitario, @Cantidad, @PrecioTotal);"
                    cmdComando.Parameters.Clear()

                    cmdComando.CommandType = CommandType.Text
                    cmdComando.Parameters.AddWithValue("@IDVenta", ventaID)
                    cmdComando.Parameters.AddWithValue("@IDProducto", item.IDProducto)
                    cmdComando.Parameters.AddWithValue("@PrecioUnitario", item.PrecioUnitario)
                    cmdComando.Parameters.AddWithValue("@Cantidad", item.Cantidad)
                    cmdComando.Parameters.AddWithValue("@PrecioTotal", item.PrecioTotal)


                    oConexion.EjecutarComandoSQL(cmdComando)
                Next


                Return True

            Catch ex As Exception
                ' Manejo de excepciones: podrías registrar el error en un archivo de registro
                Return False
            End Try
        End Function

    End Class
End Namespace
