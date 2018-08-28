Public Class Form1
    Private Async Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim newuser As New Account_Creater With {.Dock = DockStyle.Top}
        Panel1.Controls.Add(newuser)
    End Sub
End Class
