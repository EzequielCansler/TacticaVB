Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Namespace DAL
    Public Class SqlDBHelper
        Private Tabla As DataTable
        Private strConexion As SqlConnection = New SqlConnection("Server=DESKTOP-8PE60FU\SQLEXPRESS;Uid=sa;Pwd=sasa;MultipleActiveResultSets=True;Timeout=120; Database=DBTactica;")
        Private cmd As SqlCommand = New SqlCommand()

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
    End Class
End Namespace
