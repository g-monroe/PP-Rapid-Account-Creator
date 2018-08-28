Public Class Account_Creater
    Private Async Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        For i As Integer = 0 To 100000
            Using registerbot = New GhostBinProvider
                Dim response = Await registerbot.Register
                Label1.Text = "Currently Added: " & i.ToString
            End Using
        Next
    End Sub
End Class
