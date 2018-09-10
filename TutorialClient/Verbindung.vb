Public Class Verbindung
    Dim IpAdresse As String
    Dim Port As Integer

    Dim Tcp_Client As System.Net.Sockets.TcpClient
    Dim STW As System.IO.StreamWriter
    Dim STR As System.IO.StreamReader

    Dim IstVerbunden As Boolean = False

    Public Event NeueNachricht(ByVal sNachricht As String)
    Public Event VerbindungUnterbrochen(ByVal sError As String)

    Public Sub Start(ByVal Ip As String, ByVal Port As Integer)
        Me.IpAdresse = Ip
        Me.Port = Port
        Dim t As New System.Threading.Thread(AddressOf Verbinden)
        t.IsBackground = True
        t.Start()
    End Sub

    Private Sub Verbinden()
        Tcp_Client = New System.Net.Sockets.TcpClient
        While IstVerbunden = False
            Try
                Tcp_Client.Connect(IpAdresse, Port)
                STW = New System.IO.StreamWriter(Tcp_Client.GetStream)
                STR = New System.IO.StreamReader(Tcp_Client.GetStream)
                IstVerbunden = True
                Dim t As New System.Threading.Thread(AddressOf Abhören)
                t.IsBackground = True
                t.Start()
            Catch ex As Exception
                IstVerbunden = False
            End Try
        End While
    End Sub
    Private Sub Abhören()
        Try
            While IstVerbunden
                Dim sNachricht As String = STR.ReadLine
                RaiseEvent NeueNachricht(sNachricht)
            End While
        Catch ex As Exception
            
            Stopp()
            RaiseEvent VerbindungUnterbrochen(ex.Message)
        End Try
    End Sub


    Public Sub Stopp()
        On Error Resume Next
        IstVerbunden = False
        STW.Close()
        STR.Close()
        Tcp_Client.Close()
    End Sub

    Public Sub Schreiben(ByVal sText As String)
        Try
            STW.WriteLine(sText)
            STW.Flush()
        Catch ex As Exception
            Stopp()
            RaiseEvent VerbindungUnterbrochen(ex.Message)
        End Try
    End Sub

End Class
