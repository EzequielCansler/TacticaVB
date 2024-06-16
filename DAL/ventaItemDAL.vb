Imports System.Data.SqlClient
Imports Entidades.Entidades

Namespace DAL
    Public Class VentasItemDAL

        Private oConexion As SqlDBHelper

        Public Sub New()
            oConexion = New SqlDBHelper()
        End Sub

        Public Function Listar() As List(Of VentaItem)
            Dim ventasitems As New List(Of VentaItem)()

            Dim cmdComando As New SqlCommand()
            cmdComando.CommandText = "SELECT * FROM ventasitems"
            cmdComando.CommandType = CommandType.Text

            Dim TablaResultante As DataTable = oConexion.EjecutarSentenciaSQL(cmdComando)
            For Each fila As DataRow In TablaResultante.Rows
                ventasitems.Add(New VentaItem(fila))
            Next

            Return ventasitems
        End Function


    End Class
End Namespace
