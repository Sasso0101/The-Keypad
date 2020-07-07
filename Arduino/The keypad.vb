Imports System.IO.Ports
Imports System.Text.RegularExpressions
Imports System.Net.Http
Imports Newtonsoft.Json

Public Class TheKeypad

    'Keyboard emulation
    <Runtime.InteropServices.DllImport("user32.dll")>
    Private Shared Function keybd_event(bVk As Byte, bScan As Byte, dwFlags As UInteger, dwExtraInfo As Integer) As Boolean
    End Function

    Const KEYEVENTF_KEYDOWN = &H0
    Const KEYEVENTF_KEYUP = &H2

    Dim allPorts As New List(Of String)
    Dim scannedPorts As New List(Of String)
    Dim nowScanning As SerialPort

    Dim page As Integer = 0

    Dim availableHotkeys As New Dictionary(Of Integer, String)
    Dim assignedHotkeys(10) As Integer

    Shared ReadOnly client As HttpClient = New HttpClient()

    Dim privateFonts As New Text.PrivateFontCollection()
    Dim customFont As FontFamily


    Private Sub TheKeypadLoad(sender As Object, e As EventArgs) Handles MyBase.Load
        privateFonts.AddFontFile(Application.StartupPath + "\CascadiaMono.ttf")
        customFont = privateFonts.Families(0)
        Me.Font = Font
        title.Font = New Font(customFont, 24, FontStyle.Regular, GraphicsUnit.Point)
        toTray.Font = New Font(customFont, 14, FontStyle.Regular, GraphicsUnit.Point)
        connectionStatus.Font = New Font(customFont, 14, FontStyle.Regular, GraphicsUnit.Point)
        previousPage.Font = New Font(customFont, 12, FontStyle.Regular, GraphicsUnit.Point)
        nextPage.Font = New Font(customFont, 12, FontStyle.Regular, GraphicsUnit.Point)
        tabHotkeys.Font = New Font(customFont, 14, FontStyle.Regular, GraphicsUnit.Point)
        tabAnimations.Font = New Font(customFont, 14, FontStyle.Regular, GraphicsUnit.Point)
        autoDiscovery.Start()
        availableHotkeys.Add(0, "None")
        availableHotkeys.Add(175, "V+")
        availableHotkeys.Add(174, "V-")
        availableHotkeys.Add(177, "<<")
        availableHotkeys.Add(179, "►")
        availableHotkeys.Add(176, ">>")
        availableHotkeys.Add(173, "Vx")
        availableHotkeys.Add(124, "F13")
        availableHotkeys.Add(125, "F14")
        availableHotkeys.Add(126, "F15")
        availableHotkeys.Add(127, "F16")
        availableHotkeys.Add(128, "F17")
        availableHotkeys.Add(129, "F18")
        availableHotkeys.Add(130, "F19")
        availableHotkeys.Add(131, "F20")
        availableHotkeys.Add(132, "F21")

        'Initializes list of available hotkeys
        changePage(page)
    End Sub

    'Automatically detects keypads serial port
    Private Sub autoDiscoveryTick(sender As Object, e As EventArgs) Handles autoDiscovery.Tick
        Try
            connectionStatus.Text = "Searching for Keypad..."
            ' Refresh port list
            'allPorts = SerialPort.GetPortNames().ToList()
            allPorts = New List(Of String) From {"COM5"}
            If allPorts.Count > 0 Then
                ' First loop
                If nowScanning Is Nothing Then
                    nowScanning = New SerialPort(allPorts(0))
                    nowScanning.Open()
                    nowScanning.Write("UQzbRPHMVr7JYw")
                    autoDiscovery.Interval = 4000
                Else
                    If nowScanning.IsOpen Then
                        Dim serial As String = nowScanning.ReadExisting()
                        If serial = "qrJjhbWQxm5Pbi" Then
                            ' Port found
                            autoDiscovery.Stop()
                            checkSerialPort.Start()
                            connectionStatus.Text = "Getting hotkeys..."
                            Dim t As Task = New Task(AddressOf initializeKeys)
                            t.RunSynchronously()
                            Return
                        End If
                    End If
                    scannedPorts.Add(nowScanning.PortName())
                    subArray(allPorts, scannedPorts)
                    nowScanning.Close()
                    If allPorts.Count() > 0 Then
                        ' Try next port
                        nowScanning.PortName = allPorts(0)
                        nowScanning.Open()
                        nowScanning.Write("UQzbRPHMVr7JYw")
                    Else
                        ' No port was found, try again from the beginning
                        scannedPorts.Clear()
                        nowScanning = Nothing
                    End If
                End If
            End If
        Catch ex As UnauthorizedAccessException
            Debug.WriteLine("Port " + nowScanning.PortName + " busy.")
        Catch ex As IO.IOException
            Debug.WriteLine("Port " + nowScanning.PortName + " not available.")
        End Try
    End Sub
    'Removes elements in items from array fromArray
    Sub subArray(ByRef fromArray As List(Of String), ByRef items As List(Of String))
        For Each item In items
            fromArray.Remove(item)
        Next
    End Sub

    ' Handles physical keypresses
    Private Sub checkSerialPortTick(sender As Object, e As EventArgs) Handles checkSerialPort.Tick
        Dim pushedButtons As String
        Try
            pushedButtons = nowScanning.ReadExisting
            If pushedButtons = Nothing Then
                Return
            End If
        Catch ex As InvalidOperationException
            nowScanning = Nothing
            connectionStatus.Text = "Connection lost!"
            checkSerialPort.Stop()
            autoDiscovery.Start()
            Return
        End Try

        pushedButtons = pushedButtons.Trim()
        If pushedButtons <> Nothing Then
            Dim pushedKeys() As String
            pushedKeys = pushedButtons.Split(" ")
            For Each pushedKey In pushedKeys
                SendKey(assignedHotkeys(Convert.ToInt32(pushedKey)))
            Next
        End If
    End Sub
    'Sends the keystroke
    Private Sub SendKey(KeyCode As Integer)
        keybd_event(CByte(KeyCode), 0, KEYEVENTF_KEYDOWN, 0)
        Threading.Thread.Sleep(15)
        keybd_event(CByte(KeyCode), 0, KEYEVENTF_KEYUP, 0)
    End Sub
    'Handles virtual keypresses (fired by the buttons in the program)
    Private Sub ButtonClick(sender As Object, e As EventArgs)
        Dim buttonNumber As Integer = Integer.Parse(Regex.Replace(sender.Name, "[^\d]", "")) 'Pressed button number
        SendKey(assignedHotkeys(buttonNumber))
    End Sub

    'Drag and Drop
    Private Sub HotkeyMouseDown(ByVal sender As Object, ByVal e As MouseEventArgs)
        sender.DoDragDrop(sender.Tag.ToString, DragDropEffects.Copy)
    End Sub
    Private Sub ButtonMouseEnter(ByVal sender As Object, ByVal e As DragEventArgs)
        e.Effect = DragDropEffects.Copy
    End Sub
    Private Sub ButtonDragDrop(sender As Object, e As DragEventArgs)
        Dim buttonNumber As Integer = Integer.Parse(Regex.Replace(sender.Name, "[^\d]", "")) 'Pressed button number
        Dim draggedHotkey = CInt(e.Data.GetData(DataFormats.Text))
        assignedHotkeys(buttonNumber) = draggedHotkey
        sender.Text = availableHotkeys(draggedHotkey)
        updateHotkeys(assignedHotkeys)
    End Sub

    'Page handling
    Private Sub NextPageClick(sender As Object, e As EventArgs) Handles nextPage.Click
        If page + 2 <= Math.Ceiling(availableHotkeys.Count / 8) Then
            page += 1
            changePage(page)
        End If
    End Sub
    Private Sub PreviousPageClick(sender As Object, e As EventArgs) Handles previousPage.Click
        If page - 1 >= 0 Then
            page -= 1
            changePage(page)
        End If
    End Sub
    Sub changePage(page)
        hotkeys.Controls.Clear()
        For Each hotkey In availableHotkeys.Skip(page * 8).Take(8)
            Dim lbl As New Label
            lbl.Tag = hotkey.Key
            lbl.Text = hotkey.Value
            lbl.TextAlign = ContentAlignment.MiddleCenter
            lbl.Size = New Size(hotkeys.Width, 43)
            lbl.Font = New Font(customFont, 15, FontStyle.Regular, GraphicsUnit.Point)
            lbl.Cursor = Cursors.SizeAll
            AddHandler lbl.MouseDown, AddressOf HotkeyMouseDown
            hotkeys.Controls.Add(lbl)
        Next
    End Sub

    'Tray icon
    Private Sub toTrayClick(sender As Object, e As EventArgs) Handles toTray.Click
        tray.Visible = True
        Me.Hide()
    End Sub
    Private Sub trayMouseDoubleClick(sender As Object, e As MouseEventArgs) Handles tray.MouseClick
        Me.Show()
        Me.WindowState = FormWindowState.Normal
        tray.Visible = False
    End Sub

    'Downloads key layout
    Async Sub initializeKeys()
        Try
            Dim buttonNumber As Integer = 0 'Pressed button number
            Dim responseBody As String = Await client.GetStringAsync("http://salvatoreandaloro.altervista.org/keypad/keypad.json")
            assignedHotkeys = JsonConvert.DeserializeObject(Of Integer())(responseBody)
            buttonGrid.Controls.Clear()
            'Initializes buttons
            For Each assignedHotkey In assignedHotkeys
                Dim button = New Button()
                button.Font = New Font(customFont, 12, FontStyle.Regular, GraphicsUnit.Point)
                button.Text = availableHotkeys(assignedHotkeys(buttonNumber))
                button.Name = "button" + buttonNumber.ToString()
                button.FlatStyle = FlatStyle.Flat
                button.BackColor = Color.White
                button.AllowDrop = True
                button.Size = New Size(96, 96)
                button.Cursor = Cursors.Hand
                button.FlatAppearance.BorderSize = 4
                button.FlatAppearance.MouseDownBackColor = Color.White
                button.FlatAppearance.MouseOverBackColor = Color.White

                buttonGrid.Controls.Add(button)

                AddHandler button.DragEnter, AddressOf ButtonMouseEnter
                AddHandler button.DragDrop, AddressOf ButtonDragDrop
                AddHandler button.Click, AddressOf ButtonClick
                buttonNumber += 1
            Next
            connectionStatus.Text = "Connected"
        Catch e As HttpRequestException
            Console.WriteLine(Environment.NewLine & "Exception Caught!")
            Console.WriteLine("Message :{0} ", e.Message)
            connectionStatus.Text = "Error getting hotkeys!"
        End Try
    End Sub

    'Update hotkey
    Async Sub updateHotkeys(hotkeys As Integer())
        Try
            Dim json = JsonConvert.SerializeObject(hotkeys)
            Dim content As New StringContent(json, System.Text.Encoding.UTF8, "application/json")
            Await client.PostAsync("http://salvatoreandaloro.altervista.org/keypad/update.php", content)
        Catch e As HttpRequestException
            Console.WriteLine(Environment.NewLine & "Exception Caught!")
            Console.WriteLine("Message :{0} ", e.Message)
        End Try
    End Sub
End Class
