Imports System.Data.SqlClient
Imports Entidades.Entidades

Namespace DAL
    ' capa de acceso a datos
    Public Class clientesDAL
        Private oConexion As SqlDBHelper ' conexion con DB

        Public Sub New()
            oConexion = New SqlDBHelper()
        End Sub

        Public Function AgregarModificar(cliente As Cliente, ID As Integer?) As Boolean
            Try
                Dim cmdComando As New SqlCommand()
                Dim oConexion As New SqlDBHelper()
                If ID IsNot Nothing Then
                    Dim clienteExistente As Cliente = BuscarID(ID.Value)
                    If clienteExistente IsNot Nothing Then
                        cmdComando.CommandText = "UPDATE clientes SET Cliente = @Cliente, Telefono = @Telefono, Correo = @Correo WHERE ID = @ID"
                        cmdComando.Parameters.AddWithValue("@ID", ID.Value)
                    Else
                        cmdComando.CommandText = "INSERT INTO clientes (Cliente,Telefono,Correo) VALUES (@Cliente,@telefono,@Correo)"
                    End If
                Else
                    cmdComando.CommandText = "INSERT INTO clientes (Cliente,Telefono,Correo) VALUES (@Cliente,@telefono,@Correo)"
                End If

                cmdComando.CommandType = CommandType.Text
                cmdComando.Parameters.AddWithValue("@Cliente", cliente.NombreCliente)
                cmdComando.Parameters.AddWithValue("@Telefono", cliente.Telefono)
                cmdComando.Parameters.AddWithValue("@Correo", cliente.Correo)

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
                cmdComando.CommandText = "DELETE FROM clientes WHERE ID = @ID"
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

        Public Function Listar() As List(Of Cliente)
            Dim clientes As New List(Of Cliente)()

            Dim cmdComando As New SqlCommand()
            cmdComando.CommandText = "SELECT * FROM clientes" ' sentencia SQL
            cmdComando.CommandType = CommandType.Text ' tipo de comando

            Dim TablaResultante As DataTable = oConexion.EjecutarSentenciaSQL(cmdComando)
            For Each fila As DataRow In TablaResultante.Rows
                clientes.Add(New Cliente(fila))
            Next

            Return clientes
        End Function

        Public Function BuscarID(id As Integer) As Cliente
            Dim cmdComando As New SqlCommand()
            cmdComando.CommandText = "SELECT * FROM clientes WHERE ID = @ID"
            cmdComando.CommandType = CommandType.Text
            cmdComando.Parameters.AddWithValue("@ID", id)
            Dim TablaResultante As DataTable = oConexion.EjecutarSentenciaSQL(cmdComando)
            If TablaResultante.Rows.Count > 0 Then
                Return New Cliente(TablaResultante.Rows(0))
            Else
                Return Nothing
            End If
        End Function

        Public Function BuscarClientesPorFiltro(nombre As String, telefono As String) As List(Of Cliente)
            Dim clientes As New List(Of Cliente)()
            Dim cmdComando As New SqlCommand("SELECT * FROM clientes WHERE Cliente LIKE @nombre OR Telefono LIKE @telefono")
            cmdComando.Parameters.AddWithValue("@nombre", "%" & nombre & "%")
            cmdComando.Parameters.AddWithValue("@telefono", "%" & telefono & "%")
            Dim oConexion As New SqlDBHelper()
            Dim dt As DataTable = oConexion.EjecutarSentenciaSQL(cmdComando)
            For Each row As DataRow In dt.Rows
                clientes.Add(New Cliente(row))
            Next
            Return clientes
        End Function


    End Class
End Namespace
