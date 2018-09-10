Public Class Class1
    Public Interface myInterface
        'Informations Funktionen, zum Laden der DLL
        Function GetPlugInName() As String
        Function getPlugInInfo() As String
        Function GetPlugDescription() As String
        Function GetPlugInType() As Integer

        'Funktionen für die DLL
        Function StartMe(ByVal VictimInformations() As String)
        Function GetConnection(ByVal Tcp_Client As System.Net.Sockets.TcpClient)
        Function CloseMe()

    End Interface

End Class
