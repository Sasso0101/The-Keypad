<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class TheKeypad
    Inherits System.Windows.Forms.Form

    'Form esegue l'override del metodo Dispose per pulire l'elenco dei componenti.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Richiesto da Progettazione Windows Form
    Private components As System.ComponentModel.IContainer

    'NOTA: la procedura che segue è richiesta da Progettazione Windows Form
    'Può essere modificata in Progettazione Windows Form.  
    'Non modificarla mediante l'editor del codice.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TheKeypad))
        Me.autoDiscovery = New System.Windows.Forms.Timer(Me.components)
        Me.connectionStatus = New System.Windows.Forms.Label()
        Me.title = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.buttonGrid = New System.Windows.Forms.FlowLayoutPanel()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.Button8 = New System.Windows.Forms.Button()
        Me.Button9 = New System.Windows.Forms.Button()
        Me.Button10 = New System.Windows.Forms.Button()
        Me.hotkeys = New System.Windows.Forms.FlowLayoutPanel()
        Me.checkSerialPort = New System.Windows.Forms.Timer(Me.components)
        Me.previousPage = New System.Windows.Forms.Button()
        Me.nextPage = New System.Windows.Forms.Button()
        Me.toTray = New System.Windows.Forms.Button()
        Me.tray = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.tabHotkeys = New System.Windows.Forms.Label()
        Me.tabAnimations = New System.Windows.Forms.Label()
        Me.buttonGrid.SuspendLayout()
        Me.SuspendLayout()
        '
        'autoDiscovery
        '
        Me.autoDiscovery.Interval = 1000
        '
        'connectionStatus
        '
        Me.connectionStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.connectionStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.connectionStatus.Location = New System.Drawing.Point(61, 468)
        Me.connectionStatus.Name = "connectionStatus"
        Me.connectionStatus.Size = New System.Drawing.Size(512, 71)
        Me.connectionStatus.TabIndex = 0
        Me.connectionStatus.Text = "Initializing"
        Me.connectionStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'title
        '
        Me.title.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.title.Location = New System.Drawing.Point(61, 3)
        Me.title.Name = "title"
        Me.title.Size = New System.Drawing.Size(512, 56)
        Me.title.TabIndex = 11
        Me.title.Text = "The Keypad"
        Me.title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(26, Byte), Integer), CType(CType(26, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(612, -2)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(294, 540)
        Me.Label1.TabIndex = 12
        '
        'buttonGrid
        '
        Me.buttonGrid.AllowDrop = True
        Me.buttonGrid.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.buttonGrid.Controls.Add(Me.Button1)
        Me.buttonGrid.Controls.Add(Me.Button2)
        Me.buttonGrid.Controls.Add(Me.Button3)
        Me.buttonGrid.Controls.Add(Me.Button4)
        Me.buttonGrid.Controls.Add(Me.Button5)
        Me.buttonGrid.Controls.Add(Me.Button6)
        Me.buttonGrid.Controls.Add(Me.Button7)
        Me.buttonGrid.Controls.Add(Me.Button8)
        Me.buttonGrid.Controls.Add(Me.Button9)
        Me.buttonGrid.Controls.Add(Me.Button10)
        Me.buttonGrid.Location = New System.Drawing.Point(61, 94)
        Me.buttonGrid.Name = "buttonGrid"
        Me.buttonGrid.Size = New System.Drawing.Size(512, 255)
        Me.buttonGrid.TabIndex = 13
        '
        'Button1
        '
        Me.Button1.Cursor = System.Windows.Forms.Cursors.No
        Me.Button1.FlatAppearance.BorderSize = 4
        Me.Button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White
        Me.Button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Location = New System.Drawing.Point(3, 3)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(96, 96)
        Me.Button1.TabIndex = 0
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Cursor = System.Windows.Forms.Cursors.No
        Me.Button2.FlatAppearance.BorderSize = 4
        Me.Button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White
        Me.Button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.Location = New System.Drawing.Point(105, 3)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(96, 96)
        Me.Button2.TabIndex = 1
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Cursor = System.Windows.Forms.Cursors.No
        Me.Button3.FlatAppearance.BorderSize = 4
        Me.Button3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White
        Me.Button3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.Button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button3.Location = New System.Drawing.Point(207, 3)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(96, 96)
        Me.Button3.TabIndex = 2
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Cursor = System.Windows.Forms.Cursors.No
        Me.Button4.FlatAppearance.BorderSize = 4
        Me.Button4.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White
        Me.Button4.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.Button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button4.Location = New System.Drawing.Point(309, 3)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(96, 96)
        Me.Button4.TabIndex = 3
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.Cursor = System.Windows.Forms.Cursors.No
        Me.Button5.FlatAppearance.BorderSize = 4
        Me.Button5.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White
        Me.Button5.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.Button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button5.Location = New System.Drawing.Point(411, 3)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(96, 96)
        Me.Button5.TabIndex = 4
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Button6
        '
        Me.Button6.Cursor = System.Windows.Forms.Cursors.No
        Me.Button6.FlatAppearance.BorderSize = 4
        Me.Button6.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White
        Me.Button6.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.Button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button6.Location = New System.Drawing.Point(3, 105)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(96, 96)
        Me.Button6.TabIndex = 5
        Me.Button6.UseVisualStyleBackColor = True
        '
        'Button7
        '
        Me.Button7.Cursor = System.Windows.Forms.Cursors.No
        Me.Button7.FlatAppearance.BorderSize = 4
        Me.Button7.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White
        Me.Button7.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.Button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button7.Location = New System.Drawing.Point(105, 105)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(96, 96)
        Me.Button7.TabIndex = 6
        Me.Button7.UseVisualStyleBackColor = True
        '
        'Button8
        '
        Me.Button8.Cursor = System.Windows.Forms.Cursors.No
        Me.Button8.FlatAppearance.BorderSize = 4
        Me.Button8.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White
        Me.Button8.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.Button8.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button8.Location = New System.Drawing.Point(207, 105)
        Me.Button8.Name = "Button8"
        Me.Button8.Size = New System.Drawing.Size(96, 96)
        Me.Button8.TabIndex = 7
        Me.Button8.UseVisualStyleBackColor = True
        '
        'Button9
        '
        Me.Button9.Cursor = System.Windows.Forms.Cursors.No
        Me.Button9.FlatAppearance.BorderSize = 4
        Me.Button9.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White
        Me.Button9.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.Button9.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button9.Location = New System.Drawing.Point(309, 105)
        Me.Button9.Name = "Button9"
        Me.Button9.Size = New System.Drawing.Size(96, 96)
        Me.Button9.TabIndex = 8
        Me.Button9.UseVisualStyleBackColor = True
        '
        'Button10
        '
        Me.Button10.Cursor = System.Windows.Forms.Cursors.No
        Me.Button10.FlatAppearance.BorderSize = 4
        Me.Button10.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White
        Me.Button10.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.Button10.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button10.Location = New System.Drawing.Point(411, 105)
        Me.Button10.Name = "Button10"
        Me.Button10.Size = New System.Drawing.Size(96, 96)
        Me.Button10.TabIndex = 9
        Me.Button10.UseVisualStyleBackColor = True
        '
        'hotkeys
        '
        Me.hotkeys.BackColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(26, Byte), Integer), CType(CType(26, Byte), Integer))
        Me.hotkeys.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.hotkeys.ForeColor = System.Drawing.Color.White
        Me.hotkeys.Location = New System.Drawing.Point(654, 37)
        Me.hotkeys.Name = "hotkeys"
        Me.hotkeys.Size = New System.Drawing.Size(219, 368)
        Me.hotkeys.TabIndex = 14
        '
        'checkSerialPort
        '
        '
        'previousPage
        '
        Me.previousPage.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.previousPage.BackColor = System.Drawing.Color.White
        Me.previousPage.Cursor = System.Windows.Forms.Cursors.Hand
        Me.previousPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.previousPage.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.previousPage.ForeColor = System.Drawing.Color.Black
        Me.previousPage.Location = New System.Drawing.Point(636, 430)
        Me.previousPage.Name = "previousPage"
        Me.previousPage.Size = New System.Drawing.Size(108, 45)
        Me.previousPage.TabIndex = 15
        Me.previousPage.Text = "previous"
        Me.previousPage.UseVisualStyleBackColor = False
        '
        'nextPage
        '
        Me.nextPage.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.nextPage.Cursor = System.Windows.Forms.Cursors.Hand
        Me.nextPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.nextPage.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nextPage.Location = New System.Drawing.Point(773, 430)
        Me.nextPage.Name = "nextPage"
        Me.nextPage.Size = New System.Drawing.Size(114, 45)
        Me.nextPage.TabIndex = 16
        Me.nextPage.Text = "next"
        Me.nextPage.UseVisualStyleBackColor = True
        '
        'toTray
        '
        Me.toTray.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.toTray.BackColor = System.Drawing.Color.White
        Me.toTray.Cursor = System.Windows.Forms.Cursors.Hand
        Me.toTray.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.toTray.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.toTray.ForeColor = System.Drawing.Color.Black
        Me.toTray.Location = New System.Drawing.Point(226, 408)
        Me.toTray.Name = "toTray"
        Me.toTray.Size = New System.Drawing.Size(178, 48)
        Me.toTray.TabIndex = 17
        Me.toTray.Text = "Hide in tray"
        Me.toTray.UseVisualStyleBackColor = False
        '
        'tray
        '
        Me.tray.Icon = CType(resources.GetObject("tray.Icon"), System.Drawing.Icon)
        Me.tray.Text = "The Keypad"
        Me.tray.Visible = True
        '
        'tabHotkeys
        '
        Me.tabHotkeys.AutoSize = True
        Me.tabHotkeys.BackColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(26, Byte), Integer), CType(CType(26, Byte), Integer))
        Me.tabHotkeys.Cursor = System.Windows.Forms.Cursors.Hand
        Me.tabHotkeys.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tabHotkeys.ForeColor = System.Drawing.Color.White
        Me.tabHotkeys.Location = New System.Drawing.Point(650, 9)
        Me.tabHotkeys.Name = "tabHotkeys"
        Me.tabHotkeys.Size = New System.Drawing.Size(67, 20)
        Me.tabHotkeys.TabIndex = 18
        Me.tabHotkeys.Text = "Hotkeys"
        '
        'tabAnimations
        '
        Me.tabAnimations.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tabAnimations.AutoSize = True
        Me.tabAnimations.BackColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(26, Byte), Integer), CType(CType(26, Byte), Integer))
        Me.tabAnimations.Cursor = System.Windows.Forms.Cursors.Hand
        Me.tabAnimations.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tabAnimations.ForeColor = System.Drawing.Color.White
        Me.tabAnimations.Location = New System.Drawing.Point(756, 9)
        Me.tabAnimations.Name = "tabAnimations"
        Me.tabAnimations.Size = New System.Drawing.Size(88, 20)
        Me.tabAnimations.TabIndex = 19
        Me.tabAnimations.Text = "Animations"
        '
        'TheKeypad
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(906, 535)
        Me.Controls.Add(Me.tabAnimations)
        Me.Controls.Add(Me.tabHotkeys)
        Me.Controls.Add(Me.toTray)
        Me.Controls.Add(Me.nextPage)
        Me.Controls.Add(Me.previousPage)
        Me.Controls.Add(Me.hotkeys)
        Me.Controls.Add(Me.buttonGrid)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.title)
        Me.Controls.Add(Me.connectionStatus)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(922, 574)
        Me.MinimumSize = New System.Drawing.Size(922, 574)
        Me.Name = "TheKeypad"
        Me.Text = "The Keypad"
        Me.buttonGrid.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents autoDiscovery As Timer
    Friend WithEvents connectionStatus As Label
    Friend WithEvents title As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents buttonGrid As FlowLayoutPanel
    Friend WithEvents hotkeys As FlowLayoutPanel
    Friend WithEvents checkSerialPort As Timer
    Friend WithEvents previousPage As Button
    Friend WithEvents nextPage As Button
    Friend WithEvents toTray As Button
    Friend WithEvents tray As NotifyIcon
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents Button5 As Button
    Friend WithEvents Button6 As Button
    Friend WithEvents Button7 As Button
    Friend WithEvents Button8 As Button
    Friend WithEvents Button9 As Button
    Friend WithEvents Button10 As Button
    Friend WithEvents tabHotkeys As Label
    Friend WithEvents tabAnimations As Label
End Class
