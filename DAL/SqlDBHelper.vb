Imports System.Data.SqlClient
Imports System.Configuration

Namespace DAL
    Public Class SqlDBHelper
        Private Tabla As DataTable
        Private strConexion As SqlConnection
        Private cmd As SqlCommand

        ' Constructor que inicializa la conexión desde el Web.config
        Public Sub New()
            Dim connectionString As String = ConfigurationManager.ConnectionStrings("MiConexion").ConnectionString
            strConexion = New SqlConnection(connectionString)
            cmd = New SqlCommand()
        End Sub

        Public Function EjecutarComandoSQL(ByVal strSQLCommand As SqlCommand) As Boolean
            ' INSERT UPDATE DELETE
            Dim Res As Boolean = True
            cmd = strSQLCommand
            cmd.Connection = strConexion
            strConexion.Open()
            Res = (cmd.ExecuteNonQuery() <= 0) = False
            strConexion.Close()
            Return Res
        End Function

        Public Function EjecutarSentenciaSQL(ByVal strSQLCommand As SqlCommand) As DataTable
            ' SELECT
            cmd = strSQLCommand
            cmd.Connection = strConexion
            strConexion.Open()
            Tabla = New DataTable()
            Tabla.Load(cmd.ExecuteReader())
            strConexion.Close()
            Return Tabla
        End Function

        Public Function EjecutarEscalar(ByVal strSQLCommand As SqlCommand) As Object
            ' Ejecutar comando y devolver resultado escalar
            Dim result As Object = Nothing
            cmd = strSQLCommand
            cmd.Connection = strConexion
            strConexion.Open()
            result = cmd.ExecuteScalar()
            strConexion.Close()
            Return result
        End Function
    End Class
End Namespace
