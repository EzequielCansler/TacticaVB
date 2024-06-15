Imports System.Data.SqlClient
Imports Entidades.Entidades
Namespace DAL

    Public Class ProductosDAL
        Private oConexion As SqlDBHelper

        Public Sub New()
            oConexion = New SqlDBHelper()
        End Sub

        Public Function Listar() As List(Of Producto)
            Dim productos As New List(Of Producto)()

            Dim cmdComando As New SqlCommand()
            cmdComando.CommandText = "SELECT * FROM productos" ' sentencia SQL
            cmdComando.CommandType = CommandType.Text ' tipo de comando

            Dim TablaResultante As DataTable = oConexion.EjecutarSentenciaSQL(cmdComando)
            For Each fila As DataRow In TablaResultante.Rows
                productos.Add(New Producto(fila))
            Next

            Return productos
        End Function

        Public Function BuscarID(ID As Integer) As Producto
            Dim cmdComando As New SqlCommand()
            cmdComando.CommandText = "SELECT * FROM productos WHERE ID = @ID"
            cmdComando.CommandType = CommandType.Text
            cmdComando.Parameters.AddWithValue("@ID", ID)
            Dim TablaResultante As DataTable = oConexion.EjecutarSentenciaSQL(cmdComando)
            If TablaResultante.Rows.Count > 0 Then
                Return New Producto(TablaResultante.Rows(0))
            Else
                Return Nothing
            End If
        End Function
        Public Function AgregarModificarProdu(producto As Producto, ID As Integer?) As Boolean
            Try
                Dim cmdComando As New SqlCommand()
                Dim oConexion As New SqlDBHelper()
                If ID IsNot Nothing Then
                    Dim productoExistente As Producto = BuscarID(ID.Value)
                    If productoExistente IsNot Nothing Then
                        cmdComando.CommandText = "UPDATE productos SET Nombre = @Nombre, Precio = @Precio, Categoria = @Categoria WHERE ID = @ID"
                        cmdComando.Parameters.AddWithValue("@ID", ID.Value)
                    Else
                        cmdComando.CommandText = "INSERT INTO productos (Nombre,Precio,Categoria) VALUES (@Nombre,@Precio,@Categoria)"
                    End If
                Else
                    cmdComando.CommandText = "INSERT INTO productos (Nombre,Precio,Categoria) VALUES (@Nombre,@Precio,@Categoria)"
                End If

                cmdComando.CommandType = CommandType.Text
                cmdComando.Parameters.AddWithValue("@Nombre", producto.NombreProducto)
                cmdComando.Parameters.AddWithValue("@Precio", producto.Precio)
                cmdComando.Parameters.AddWithValue("@Categoria", producto.Categoria)

                oConexion.EjecutarComandoSQL(cmdComando)

                Return True
            Catch ex As Exception
                Return False
            End Try

        End Function
        Public Function Eliminar(id As Integer?) As Boolean
            If Not id.HasValue Then
                Return False
            End If

            Try
                Dim cmdComando As New SqlCommand()
                cmdComando.CommandText = "DELETE FROM productos WHERE ID = @ID"
                cmdComando.CommandType = CommandType.Text
                cmdComando.Parameters.AddWithValue("@ID", id)

                Dim oConexion As New SqlDBHelper()
                oConexion.EjecutarComandoSQL(cmdComando)

                Return True
            Catch ex As Exception
                Console.WriteLine("Error al eliminar cliente: " & ex.Message)
                Return False
            End Try
        End Function

        Public Function BuscarProductosPorFiltro(nombre As String, categoria As String) As List(Of Producto)
            Dim productos As New List(Of Producto)()
            Dim cmdComando As New SqlCommand("SELECT * FROM productos WHERE Nombre LIKE @nombre OR Categoria LIKE @categoria")
            cmdComando.Parameters.AddWithValue("@nombre", "%" & nombre & "%")
            cmdComando.Parameters.AddWithValue("@categoria", "%" & categoria & "%")
            Dim oConexion As New SqlDBHelper()
            Dim dt As DataTable = oConexion.EjecutarSentenciaSQL(cmdComando)
            For Each row As DataRow In dt.Rows
                productos.Add(New Producto(row))
            Next
            Return productos
        End Function

    End Class
End Namespace