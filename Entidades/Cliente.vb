Namespace Entidades
    Public Class Cliente
        Public Sub New()
            ' Constructor vacío
        End Sub
        Public Sub New(row As DataRow)
            ID = Convert.ToInt32(row("ID"))
            NombreCliente = Convert.ToString(row("Cliente"))
            Telefono = Convert.ToString(row("Telefono"))
            Correo = Convert.ToString(row("Correo"))
        End Sub

        Public Property ID As Integer
        Public Property NombreCliente As String
        Public Property Telefono As String
        Public Property Correo As String
    End Class
End Namespace
