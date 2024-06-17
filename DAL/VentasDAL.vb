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
                Dim venta = New Venta(fila)
                venta.Items = ItemDetallePorVentaID(venta.ID)
                ventas.Add(venta)
            Next

            Return ventas
        End Function

        Public Function AgregarVenta(items As List(Of VentaItem), Fecha As Date, precioTotal As Double, clienteId As Integer, ID As Integer?) As Boolean
            Try

                Dim cmdComando As New SqlCommand()
                If ID <> 0 Then 'si tiene valor se modifica
                    cmdComando.CommandText = "UPDATE ventas SET Fecha = @Fecha, Total = @Total, IDCliente = @IDCliente WHERE ID = @ID; SELECT @ID"
                    cmdComando.Parameters.AddWithValue("@ID", ID.Value)
                Else 'sino se crea
                    cmdComando.CommandText = "INSERT INTO ventas (IDCliente, Fecha, Total) VALUES (@IDCliente, @Fecha, @Total); SELECT SCOPE_IDENTITY();"
                End If
                cmdComando.Parameters.AddWithValue("@IDCliente", clienteId)

                cmdComando.Parameters.AddWithValue("@Fecha", Fecha)
                cmdComando.Parameters.AddWithValue("@Total", precioTotal)
                cmdComando.CommandType = CommandType.Text

                'Dim ventaID As Integer = Convert.ToInt32(oConexion.EjecutarEscalar(cmdComando))
                Dim ventaID As Integer = Convert.ToInt32(oConexion.EjecutarEscalar(cmdComando))

                For Each item As VentaItem In items
                    cmdComando.Parameters.Clear()
                    cmdComando.CommandType = CommandType.Text

                    If item.ID > 0 Then
                        cmdComando.CommandText = "DELETE ventasitems WHERE ID = @ID;"
                        cmdComando.Parameters.AddWithValue("@ID", item.ID)

                    Else
                        cmdComando.CommandText = "INSERT INTO ventasitems (IDVenta, IDProducto, PrecioUnitario, Cantidad, PrecioTotal) VALUES (@IDVenta, @IDProducto, @PrecioUnitario, @Cantidad, @PrecioTotal);"
                        cmdComando.Parameters.AddWithValue("@IDVenta", ventaID)
                        cmdComando.Parameters.AddWithValue("@IDProducto", item.IDProducto)
                        cmdComando.Parameters.AddWithValue("@PrecioUnitario", item.PrecioUnitario)
                        cmdComando.Parameters.AddWithValue("@Cantidad", item.Cantidad)
                        cmdComando.Parameters.AddWithValue("@PrecioTotal", item.PrecioTotal)

                    End If
                    oConexion.EjecutarComandoSQL(cmdComando)
                Next


                Return True

            Catch ex As Exception

                Return False
            End Try
        End Function
        Public Function ItemDetallePorVentaID(ventaID As Integer) As List(Of VentaItem)
            Dim ventaItems As New List(Of VentaItem)

            Dim cmdComando As New SqlCommand()

            cmdComando.CommandText = "
        SELECT  Vi.*, p.Nombre              
        FROM ventasitems vi
        INNER JOIN productos p on p.ID = Vi.IDProducto
        WHERE vi.IDVenta =  @VentaID"
            cmdComando.CommandType = CommandType.Text
            cmdComando.Parameters.AddWithValue("@VentaID", ventaID)

            Dim TablaResultante As DataTable = oConexion.EjecutarSentenciaSQL(cmdComando)
            For Each fila As DataRow In TablaResultante.Rows
                Dim ventaItem = New VentaItem(fila)
                ventaItem.NombreProducto = fila("Nombre")
                ventaItems.Add(ventaItem)

            Next

            Return ventaItems
        End Function


        Public Function BuscarID(id As Integer) As Venta
            Dim cmdComando As New SqlCommand()
            cmdComando.CommandText = "SELECT * FROM ventas WHERE ID = @ID"
            cmdComando.CommandType = CommandType.Text
            cmdComando.Parameters.AddWithValue("@ID", id)
            Dim TablaResultante As DataTable = oConexion.EjecutarSentenciaSQL(cmdComando)
            If TablaResultante.Rows.Count > 0 Then
                Return New Venta(TablaResultante.Rows(0))
            Else
                Return Nothing
            End If
        End Function


        Public Function EliminarVenta(ID As Integer) As Boolean
                Try
                Dim cmdComando As New SqlCommand()

                cmdComando.CommandText = "DELETE FROM ventasitems WHERE IDVenta = @IDVenta"
                cmdComando.CommandType = CommandType.Text
                cmdComando.Parameters.AddWithValue("@IDVenta", ID)
                oConexion.EjecutarComandoSQL(cmdComando)



                cmdComando.CommandText = "DELETE FROM ventas WHERE ID = @ID"
                cmdComando.CommandType = CommandType.Text
                cmdComando.Parameters.AddWithValue("@ID", ID)
                oConexion.EjecutarComandoSQL(cmdComando)



                Return True
                Catch ex As Exception

                    Return False
                End Try
            End Function

        Public Function BuscarVentaPorNombreDeCliente(nombre As String) As List(Of Venta)
            Dim ventas As New List(Of Venta)()
            Dim cmdComando As New SqlCommand()
            cmdComando.CommandText = "
            SELECT v.*, c.Cliente As NombreCliente
            FROM ventas v
            INNER JOIN clientes c ON v.IDCliente = c.ID
            WHERE c.Cliente LIKE '%' + @nombre + '%'"
            cmdComando.CommandType = CommandType.Text
            cmdComando.Parameters.AddWithValue("@nombre", nombre)

            Dim TablaResultante As DataTable = oConexion.EjecutarSentenciaSQL(cmdComando)
            For Each fila As DataRow In TablaResultante.Rows
                Dim venta As New Venta(fila)
                venta.Items = ItemDetallePorVentaID(venta.ID)
                ventas.Add(venta)
            Next

            Return ventas

        End Function



    End Class
End Namespace
