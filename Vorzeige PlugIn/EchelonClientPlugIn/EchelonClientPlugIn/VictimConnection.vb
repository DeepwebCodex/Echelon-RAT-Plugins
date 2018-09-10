Public Class VictimConnection
    Shared tcpClient As System.Net.Sockets.TcpClient
    Shared sReader As System.IO.StreamReader
    Shared sWriter As System.IO.StreamWriter
    Shared T As System.Threading.Thread
    Public Event Connected()
    Public Event NewMessage(ByVal sMessage As String)
    Public Event ClientError(ByVal sError As String)

    Public Sub GetConnection(ByVal c As System.Net.Sockets.TcpClient)
        tcpClient = c
        sReader = New System.IO.StreamReader(c.GetStream)
        sWriter = New System.IO.StreamWriter(c.GetStream)

        T = New System.Threading.Thread(AddressOf Listen)
        T.IsBackground = True
        T.Start()
        RaiseEvent Connected()
    End Sub

    Private Sub Listen()
        Try
            While True
                Dim sMessage As String = sReader.ReadLine.Replace("vbNewline", vbNewLine)
                RaiseEvent NewMessage(sMessage)
            End While
        Catch ex As Exception
            RaiseEvent ClientError(ex.Message)
        End Try
    End Sub

    Public Sub SendMessageToServerPlugIn(ByVal sMessage As String)
        Try
            sWriter.WriteLine(sMessage.Replace(vbNewLine, "vbNewline"))
            sWriter.Flush()
        Catch ex As Exception
            RaiseEvent ClientError(ex.Message)
        End Try
    End Sub
    Public Sub Close()
        Try
            tcpClient.Close()
            sWriter.Close()
            sReader.Close()
            T.Abort()
        Catch ex As Exception
        End Try

    End Sub
End Class
