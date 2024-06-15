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
            cmdComando.CommandText = "SELECT * FROM ventas" ' sentencia SQL
            cmdComando.CommandType = CommandType.Text ' tipo de comando

            Dim TablaResultante As DataTable = oConexion.EjecutarSentenciaSQL(cmdComando)
            For Each fila As DataRow In TablaResultante.Rows
                ventas.Add(New Venta(fila))
            Next

            Return ventas
        End Function
    End Class

End Namespace
