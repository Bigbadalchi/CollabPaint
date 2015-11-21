<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Paint_Form
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.dlgFont = New System.Windows.Forms.FontDialog()
        Me.sfdSavePic = New System.Windows.Forms.FontDialog()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.lblEraser = New System.Windows.Forms.Label()
        Me.lblPenSize = New System.Windows.Forms.Label()
        Me.TrackBar1 = New System.Windows.Forms.TrackBar()
        Me.lblLine = New System.Windows.Forms.Label()
        Me.TrackBar = New System.Windows.Forms.TrackBar()
        Me.lblText = New System.Windows.Forms.Label()
        Me.lblBrushSize = New System.Windows.Forms.Label()
        Me.lblToolSelected = New System.Windows.Forms.Label()
        Me.lblBrush = New System.Windows.Forms.Label()
        Me.lblColorSelected = New System.Windows.Forms.Label()
        Me.lblBlack = New System.Windows.Forms.Label()
        Me.cmdFont = New System.Windows.Forms.Button()
        Me.lblFontDetails = New System.Windows.Forms.Label()
        Me.txtInsertText = New System.Windows.Forms.TextBox()
        Me.lblEditText = New System.Windows.Forms.Label()
        Me.pnlFrame = New System.Windows.Forms.Panel()
        Me.picCanvas = New System.Windows.Forms.PictureBox()
        Me.pnlMain.SuspendLayout()
        CType(Me.TrackBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrackBar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picCanvas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.lblEraser)
        Me.pnlMain.Controls.Add(Me.lblPenSize)
        Me.pnlMain.Controls.Add(Me.TrackBar1)
        Me.pnlMain.Controls.Add(Me.lblLine)
        Me.pnlMain.Controls.Add(Me.TrackBar)
        Me.pnlMain.Controls.Add(Me.lblText)
        Me.pnlMain.Controls.Add(Me.lblBrushSize)
        Me.pnlMain.Controls.Add(Me.lblToolSelected)
        Me.pnlMain.Controls.Add(Me.lblBrush)
        Me.pnlMain.Controls.Add(Me.lblColorSelected)
        Me.pnlMain.Controls.Add(Me.lblBlack)
        Me.pnlMain.Controls.Add(Me.cmdFont)
        Me.pnlMain.Controls.Add(Me.lblFontDetails)
        Me.pnlMain.Controls.Add(Me.txtInsertText)
        Me.pnlMain.Controls.Add(Me.lblEditText)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Enabled = False
        Me.pnlMain.Location = New System.Drawing.Point(0, 0)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(1104, 681)
        Me.pnlMain.TabIndex = 1
        '
        'lblEraser
        '
        Me.lblEraser.BackColor = System.Drawing.Color.White
        Me.lblEraser.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblEraser.Location = New System.Drawing.Point(1049, 403)
        Me.lblEraser.Name = "lblEraser"
        Me.lblEraser.Size = New System.Drawing.Size(36, 36)
        Me.lblEraser.TabIndex = 18
        Me.lblEraser.Tag = "Eraser"
        '
        'lblPenSize
        '
        Me.lblPenSize.AutoSize = True
        Me.lblPenSize.Location = New System.Drawing.Point(1021, 240)
        Me.lblPenSize.Name = "lblPenSize"
        Me.lblPenSize.Size = New System.Drawing.Size(53, 13)
        Me.lblPenSize.TabIndex = 17
        Me.lblPenSize.Text = "Line Size:"
        '
        'TrackBar1
        '
        Me.TrackBar1.Location = New System.Drawing.Point(989, 256)
        Me.TrackBar1.Name = "TrackBar1"
        Me.TrackBar1.Size = New System.Drawing.Size(104, 45)
        Me.TrackBar1.TabIndex = 16
        Me.TrackBar1.TickFrequency = 100000
        '
        'lblLine
        '
        Me.lblLine.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblLine.Location = New System.Drawing.Point(995, 454)
        Me.lblLine.Name = "lblLine"
        Me.lblLine.Size = New System.Drawing.Size(36, 36)
        Me.lblLine.TabIndex = 15
        Me.lblLine.Tag = "Line"
        '
        'TrackBar
        '
        Me.TrackBar.BackColor = System.Drawing.SystemColors.Control
        Me.TrackBar.Location = New System.Drawing.Point(989, 192)
        Me.TrackBar.Maximum = 20
        Me.TrackBar.Minimum = 4
        Me.TrackBar.Name = "TrackBar"
        Me.TrackBar.Size = New System.Drawing.Size(104, 45)
        Me.TrackBar.TabIndex = 14
        Me.TrackBar.TickFrequency = 1000
        Me.TrackBar.Value = 4
        '
        'lblText
        '
        Me.lblText.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblText.Location = New System.Drawing.Point(1050, 453)
        Me.lblText.Name = "lblText"
        Me.lblText.Size = New System.Drawing.Size(35, 37)
        Me.lblText.TabIndex = 13
        Me.lblText.Tag = "Text"
        '
        'lblBrushSize
        '
        Me.lblBrushSize.Location = New System.Drawing.Point(1005, 176)
        Me.lblBrushSize.Name = "lblBrushSize"
        Me.lblBrushSize.Size = New System.Drawing.Size(71, 13)
        Me.lblBrushSize.TabIndex = 9
        Me.lblBrushSize.Text = "Brush Size:"
        Me.lblBrushSize.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblToolSelected
        '
        Me.lblToolSelected.AutoSize = True
        Me.lblToolSelected.Location = New System.Drawing.Point(992, 373)
        Me.lblToolSelected.Name = "lblToolSelected"
        Me.lblToolSelected.Size = New System.Drawing.Size(101, 13)
        Me.lblToolSelected.TabIndex = 8
        Me.lblToolSelected.Text = "Tool: none selected"
        '
        'lblBrush
        '
        Me.lblBrush.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBrush.Location = New System.Drawing.Point(995, 403)
        Me.lblBrush.Name = "lblBrush"
        Me.lblBrush.Size = New System.Drawing.Size(36, 36)
        Me.lblBrush.TabIndex = 7
        Me.lblBrush.Tag = "Brush"
        '
        'lblColorSelected
        '
        Me.lblColorSelected.AutoSize = True
        Me.lblColorSelected.Location = New System.Drawing.Point(1021, 25)
        Me.lblColorSelected.Name = "lblColorSelected"
        Me.lblColorSelected.Size = New System.Drawing.Size(31, 13)
        Me.lblColorSelected.TabIndex = 6
        Me.lblColorSelected.Text = "Color"
        '
        'lblBlack
        '
        Me.lblBlack.BackColor = System.Drawing.Color.Black
        Me.lblBlack.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblBlack.Location = New System.Drawing.Point(985, 39)
        Me.lblBlack.Name = "lblBlack"
        Me.lblBlack.Size = New System.Drawing.Size(110, 119)
        Me.lblBlack.TabIndex = 4
        '
        'cmdFont
        '
        Me.cmdFont.Location = New System.Drawing.Point(565, 0)
        Me.cmdFont.Name = "cmdFont"
        Me.cmdFont.Size = New System.Drawing.Size(97, 23)
        Me.cmdFont.TabIndex = 3
        Me.cmdFont.Text = "Select Font"
        Me.cmdFont.UseVisualStyleBackColor = True
        '
        'lblFontDetails
        '
        Me.lblFontDetails.AutoSize = True
        Me.lblFontDetails.Location = New System.Drawing.Point(459, 6)
        Me.lblFontDetails.Name = "lblFontDetails"
        Me.lblFontDetails.Size = New System.Drawing.Size(100, 13)
        Me.lblFontDetails.TabIndex = 2
        Me.lblFontDetails.Text = "(Arial 12pt, Regular)"
        '
        'txtInsertText
        '
        Me.txtInsertText.Location = New System.Drawing.Point(75, 3)
        Me.txtInsertText.Name = "txtInsertText"
        Me.txtInsertText.Size = New System.Drawing.Size(378, 20)
        Me.txtInsertText.TabIndex = 1
        '
        'lblEditText
        '
        Me.lblEditText.AutoSize = True
        Me.lblEditText.Location = New System.Drawing.Point(12, 6)
        Me.lblEditText.Name = "lblEditText"
        Me.lblEditText.Size = New System.Drawing.Size(57, 13)
        Me.lblEditText.TabIndex = 0
        Me.lblEditText.Text = "Insert Text"
        '
        'pnlFrame
        '
        Me.pnlFrame.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlFrame.Location = New System.Drawing.Point(3, 22)
        Me.pnlFrame.Name = "pnlFrame"
        Me.pnlFrame.Size = New System.Drawing.Size(979, 673)
        Me.pnlFrame.TabIndex = 2
        '
        'picCanvas
        '
        Me.picCanvas.BackColor = System.Drawing.Color.Gray
        Me.picCanvas.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.picCanvas.Location = New System.Drawing.Point(0, 22)
        Me.picCanvas.Name = "picCanvas"
        Me.picCanvas.Size = New System.Drawing.Size(982, 658)
        Me.picCanvas.TabIndex = 3
        Me.picCanvas.TabStop = False
        '
        'Paint_Form
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1104, 681)
        Me.Controls.Add(Me.picCanvas)
        Me.Controls.Add(Me.pnlFrame)
        Me.Controls.Add(Me.pnlMain)
        Me.MaximumSize = New System.Drawing.Size(1120, 719)
        Me.Name = "Paint_Form"
        Me.Text = "Paint"
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMain.PerformLayout()
        CType(Me.TrackBar1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrackBar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picCanvas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dlgFont As System.Windows.Forms.FontDialog
    Friend WithEvents sfdSavePic As System.Windows.Forms.FontDialog
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents lblBrushSize As System.Windows.Forms.Label
    Friend WithEvents lblToolSelected As System.Windows.Forms.Label
    Friend WithEvents lblBrush As System.Windows.Forms.Label
    Friend WithEvents lblColorSelected As System.Windows.Forms.Label
    Friend WithEvents lblBlack As System.Windows.Forms.Label
    Friend WithEvents cmdFont As System.Windows.Forms.Button
    Friend WithEvents lblFontDetails As System.Windows.Forms.Label
    Friend WithEvents txtInsertText As System.Windows.Forms.TextBox
    Friend WithEvents lblEditText As System.Windows.Forms.Label
    Friend WithEvents pnlFrame As System.Windows.Forms.Panel
    Friend WithEvents picCanvas As System.Windows.Forms.PictureBox
    Friend WithEvents lblText As System.Windows.Forms.Label
    Friend WithEvents TrackBar As System.Windows.Forms.TrackBar
    Friend WithEvents lblLine As System.Windows.Forms.Label
    Friend WithEvents TrackBar1 As System.Windows.Forms.TrackBar
    Friend WithEvents lblPenSize As System.Windows.Forms.Label
    Friend WithEvents lblEraser As System.Windows.Forms.Label
End Class
