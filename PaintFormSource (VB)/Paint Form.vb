Imports System.Windows.Forms
Imports System.Drawing.Drawing2D
Imports System.Drawing

Public Class Paint_Form

    Dim G As Drawing.Graphics
    Dim drawFlag As Boolean = False
    Dim xDown, yDown, xUp, yUp As Integer
    Dim intL, intR, intT, intB, intW, intH As Integer
    Dim clrSelected As Color = Color.Black
    Dim intToolSelected As Integer = 0
    Dim intBrushSize As Integer = 6
    Dim intPenWidth As Integer = 2
    Dim intFontSize As Integer = 12
    Dim strText As String
    Dim strFont As String = "Arial"
    Dim styFontStyle As FontStyle
    Dim strFontStyleArray() As String = {"Regular", "Bold", "Italic", "Bold Italic", "Unknown", _
      "Unknown", "Unknown", "Unknown", "Regular Strikeout", "Bold Strikeout", _
         "Italic Strikeout", "Bold Italic Strikeout", "Regular Underline Strikeout", _
            "Bold Underline Strikeout", "Italic Underline Strikeout", _
               "Bold Italic Underline Strikeout"}
    Dim bmpPic As Bitmap
    Dim clr As New ColorDialog

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        pnlMain.Enabled = True
        picCanvas.BackColor = Color.White
        bmpPic = New Bitmap(picCanvas.Width, picCanvas.Height)
        picCanvas.Image = bmpPic
        G = System.Drawing.Graphics.FromImage(bmpPic)
        picCanvas.DrawToBitmap(bmpPic, picCanvas.ClientRectangle)

    End Sub

    Private Sub lblPalette_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles lblBlack.Click
        lblBlack.BackColor = sender.BackColor
        clrSelected = sender.BackColor

    End Sub

    Private Sub cmdFont_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFont.Click
        dlgFont.ShowDialog()
        strFont = dlgFont.Font.Name
        styFontStyle = dlgFont.Font.Style
        intFontSize = dlgFont.Font.Size
        lblFontDetails.Text = " (" & strFont & " " & intFontSize & "pt, " & strFontStyleArray(styFontStyle) & ") "
    End Sub

    Private Sub picCanvas_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picCanvas.MouseDown
        drawFlag = True
        xDown = e.X
        yDown = e.Y

        If intToolSelected = 1 Then
            G.FillEllipse(New SolidBrush(clrSelected), xDown, yDown, intBrushSize, intBrushSize)
            picCanvas.Refresh()
        End If
    End Sub

    Private Function Delta(ByVal x1 As Integer, ByVal y1 As Integer, ByVal x2 As Integer, ByVal y2 As Integer) As Double
        Dim DXSquared As Double = Math.Pow((x2 - x1), 2)
        Dim DYSquared As Double = Math.Pow((y2 - y1), 2)
        Return Math.Pow(DXSquared + DYSquared, 0.5)
    End Function

    Public Delegate Sub RenderMovementHandler(ByRef x As Integer, ByRef y As Integer, ByVal nX As Integer, ByVal nY As Integer, ByVal BRSH As SolidBrush, ByVal BrushSize As Integer)
    Private Sub RenderMovement(ByRef x As Integer, ByRef y As Integer, ByVal nX As Integer, ByVal nY As Integer, ByVal BRSH As SolidBrush, ByVal BrushSize As Integer)

        If (InvokeRequired) Then
            Me.Invoke(New RenderMovementHandler(AddressOf RenderMovement), x, y, nX, nY, BRSH, BrushSize)
            Exit Sub
        End If

        Dim Scaling As Double = BrushSize / (2 * Math.PI)

        'Get current X-Y position and get the corner for it
        Dim theta As Double = Math.Atan2(nY - y, nX - x)

        Dim cosTheta As Double = Math.Cos(theta)
        Dim sinTheta As Double = Math.Sin(theta)

        Dim rectX As Integer = -1 * Scaling * sinTheta + xDown
        Dim rectY As Integer = Scaling * cosTheta + yDown
        Dim fRectX As Integer = -1 * Scaling * sinTheta + nX
        Dim fRectY As Integer = Scaling * cosTheta + nY

        Dim Distance As Double = Delta(nX, nY, x, y)

        Dim num As Int16 = Distance / Scaling

        Dim DirectedVector As New PointF(cosTheta * Scaling, sinTheta * Scaling)
        Dim Point As PointF = New PointF(rectX, rectY)

        For i As Int16 = 1 To num
            G.FillEllipse(BRSH, Point.X, Point.Y, BrushSize, BrushSize)
            Point.X += DirectedVector.X
            Point.Y += DirectedVector.Y
        Next

        G.FillEllipse(BRSH, fRectX, fRectY, BrushSize, BrushSize)

        x = nX
        y = nY

        picCanvas.Refresh()

        'Dim Scaling As Double = intBrushSize / (2 * Math.PI)
        'If (Delta(e.X, e.Y, xDown, yDown) < Scaling) Then Exit Sub
        'Dim Path As New Drawing2D.GraphicsPath()
        'Dim xSgn As Int16 = Math.Sign(e.X - xDown) * Scaling
        'Dim ySgn As Int16 = Math.Sign(e.Y - yDown) * Scaling
        'Path.AddLine(xDown - xSgn, yDown - ySgn, e.X + xSgn, e.Y + ySgn)
        'G.DrawPath(New Pen(clrSelected, intBrushSize), Path)
        ''G.FillEllipse(New SolidBrush(clrSelected), e.X, e.Y, intBrushSize, intBrushSize)
        'xDown = e.X
        'yDown = e.Y
        'picCanvas.Refresh()

    End Sub

    Private Sub picCanvas_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picCanvas.MouseMove
        If intToolSelected = 1 AndAlso drawFlag Then
            RenderMovement(xDown, yDown, e.X, e.Y, New SolidBrush(clrSelected), intBrushSize)
        End If
    End Sub

    Private Sub picCanvas_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picCanvas.MouseUp
        Dim brushFill As SolidBrush = New System.Drawing.SolidBrush(clrSelected)
        Dim penLine As New Pen(clrSelected, intPenWidth)

        drawFlag = False
        xUp = e.X
        yUp = e.Y

        Select Case intToolSelected
            Case 3
                G.DrawLine(penLine, xDown, yDown, xUp, yUp)
            Case 2
                strText = txtInsertText.Text
                G.DrawString(strText, New System.Drawing.Font(strFont, intFontSize, styFontStyle), _
                brushFill, xUp, yUp)
            Case 4
                Dim mybrush = Brushes.White

        End Select
        picCanvas.Refresh()
    End Sub

    Private Sub lblTool_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles lblBrush.Click, lblText.Click, lblLine.Click, lblEraser.Click
        resetTools()
        Select Case sender.Tag
            Case "Brush"
                intToolSelected = 1
                lblBrush.BorderStyle = BorderStyle.FixedSingle
                lblToolSelected.Text = "Tool: Brush"
                intBrushSize = (TrackBar.Value)
                clrSelected = lblBlack.BackColor
                ResetCustomCursor()
            Case "Text"
                intToolSelected = 2
                lblText.BorderStyle = BorderStyle.FixedSingle
                lblToolSelected.Text = "Tool: Text"
                clrSelected = lblBlack.BackColor
                ResetCustomCursor()
            Case "Line"
                intToolSelected = 3
                lblLine.BorderStyle = BorderStyle.FixedSingle
                lblToolSelected.Text = "Tool: Line"
                intPenWidth = (TrackBar1.Value)
                clrSelected = lblBlack.BackColor
                ResetCustomCursor()
            Case "Eraser"
                intToolSelected = 1
                clrSelected = Color.White
                intBrushSize = (TrackBar.Value) * 4
                lblEraser.BorderStyle = BorderStyle.FixedSingle
                lblToolSelected.Text = "Tool: Eraser"
                ResetCustomCursor()
        End Select
    End Sub

    Private Sub ResetCustomCursor()
        'Dim cur As New Icon("C:\Users\Robert\Pictures\circle-md.ico")
        ' picCanvas.Cursor = New Cursor(cur.Handle)
        'cur.Size = TrackBar.Value

        Dim bm As New Bitmap(600, 600)
        Dim g As Graphics = Graphics.FromImage(bm)
        g.DrawEllipse(New Pen(Brushes.Black, 2), 300, 300, intBrushSize, intBrushSize)
        Dim ptrCur As IntPtr = bm.GetHicon
        Dim CustomCursor As Cursor
        CustomCursor = New Cursor(ptrCur)
        picCanvas.Cursor = CustomCursor
        picCanvas.Refresh()
    End Sub

    Sub resetTools()
        lblBrush.BorderStyle = BorderStyle.Fixed3D
        lblText.BorderStyle = BorderStyle.Fixed3D
        lblLine.BorderStyle = BorderStyle.Fixed3D
        lblEraser.BorderStyle = BorderStyle.Fixed3D
    End Sub

    Private Sub lblBlack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblBlack.Click
        If clr.ShowDialog = Windows.Forms.DialogResult.OK Then
            lblBlack.BackColor = clr.Color
        End If
    End Sub

    Private Sub TrackBar_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar.Scroll
        intBrushSize = CInt(TrackBar.Value)
        If clrSelected = Color.White Then
            intBrushSize *= 4
        End If
        ResetCustomCursor()
    End Sub

    Private Sub TrackBar1_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar1.Scroll
        intPenWidth = CInt(TrackBar1.Value)
    End Sub

End Class