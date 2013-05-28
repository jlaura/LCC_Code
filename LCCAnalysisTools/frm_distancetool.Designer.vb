<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_distancetool
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
        Me.lblLAYER = New System.Windows.Forms.Label()
        Me.inputlayer = New System.Windows.Forms.ComboBox()
        Me.distanceTableOut = New System.Windows.Forms.TextBox()
        Me.grpOUT = New System.Windows.Forms.GroupBox()
        Me.knn = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnSHHELP = New System.Windows.Forms.Button()
        Me.okay = New System.Windows.Forms.Button()
        Me.btnCANCEL = New System.Windows.Forms.Button()
        Me.grpMEASSPACE = New System.Windows.Forms.GroupBox()
        Me.planar_measure = New System.Windows.Forms.RadioButton()
        Me.geodesic_measure = New System.Windows.Forms.RadioButton()
        Me.grpOUT.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.grpMEASSPACE.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblLAYER
        '
        Me.lblLAYER.AutoSize = True
        Me.lblLAYER.Location = New System.Drawing.Point(6, 11)
        Me.lblLAYER.Name = "lblLAYER"
        Me.lblLAYER.Size = New System.Drawing.Size(87, 13)
        Me.lblLAYER.TabIndex = 2
        Me.lblLAYER.Text = "Input Point Layer"
        '
        'inputlayer
        '
        Me.inputlayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.inputlayer.FormattingEnabled = True
        Me.inputlayer.Location = New System.Drawing.Point(9, 28)
        Me.inputlayer.Name = "inputlayer"
        Me.inputlayer.Size = New System.Drawing.Size(400, 21)
        Me.inputlayer.TabIndex = 1
        '
        'distanceTableOut
        '
        Me.distanceTableOut.Location = New System.Drawing.Point(18, 20)
        Me.distanceTableOut.Name = "distanceTableOut"
        Me.distanceTableOut.Size = New System.Drawing.Size(373, 20)
        Me.distanceTableOut.TabIndex = 0
        '
        'grpOUT
        '
        Me.grpOUT.Controls.Add(Me.distanceTableOut)
        Me.grpOUT.Location = New System.Drawing.Point(12, 170)
        Me.grpOUT.Name = "grpOUT"
        Me.grpOUT.Size = New System.Drawing.Size(397, 51)
        Me.grpOUT.TabIndex = 5
        Me.grpOUT.TabStop = False
        Me.grpOUT.Text = "Output layer name"
        '
        'knn
        '
        Me.knn.Location = New System.Drawing.Point(18, 20)
        Me.knn.Name = "knn"
        Me.knn.Size = New System.Drawing.Size(376, 20)
        Me.knn.TabIndex = 0
        Me.knn.Text = "10"
        Me.knn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.knn)
        Me.GroupBox1.Location = New System.Drawing.Point(9, 55)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(400, 50)
        Me.GroupBox1.TabIndex = 5
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Number of nearest neighbors"
        '
        'btnSHHELP
        '
        Me.btnSHHELP.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSHHELP.Location = New System.Drawing.Point(325, 231)
        Me.btnSHHELP.Name = "btnSHHELP"
        Me.btnSHHELP.Size = New System.Drawing.Size(84, 23)
        Me.btnSHHELP.TabIndex = 8
        Me.btnSHHELP.Text = "Show Help >>"
        Me.btnSHHELP.UseVisualStyleBackColor = True
        '
        'okay
        '
        Me.okay.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.okay.Location = New System.Drawing.Point(12, 231)
        Me.okay.Name = "okay"
        Me.okay.Size = New System.Drawing.Size(75, 23)
        Me.okay.TabIndex = 6
        Me.okay.Text = "OK"
        Me.okay.UseVisualStyleBackColor = True
        '
        'btnCANCEL
        '
        Me.btnCANCEL.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnCANCEL.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCANCEL.Location = New System.Drawing.Point(93, 231)
        Me.btnCANCEL.Name = "btnCANCEL"
        Me.btnCANCEL.Size = New System.Drawing.Size(75, 23)
        Me.btnCANCEL.TabIndex = 7
        Me.btnCANCEL.Text = "Close"
        Me.btnCANCEL.UseVisualStyleBackColor = True
        '
        'grpMEASSPACE
        '
        Me.grpMEASSPACE.Controls.Add(Me.planar_measure)
        Me.grpMEASSPACE.Controls.Add(Me.geodesic_measure)
        Me.grpMEASSPACE.Location = New System.Drawing.Point(9, 111)
        Me.grpMEASSPACE.Name = "grpMEASSPACE"
        Me.grpMEASSPACE.Size = New System.Drawing.Size(400, 53)
        Me.grpMEASSPACE.TabIndex = 3
        Me.grpMEASSPACE.TabStop = False
        Me.grpMEASSPACE.Text = "Measurement space"
        '
        'planar_measure
        '
        Me.planar_measure.Appearance = System.Windows.Forms.Appearance.Button
        Me.planar_measure.AutoSize = True
        Me.planar_measure.Checked = True
        Me.planar_measure.Location = New System.Drawing.Point(18, 19)
        Me.planar_measure.Name = "planar_measure"
        Me.planar_measure.Size = New System.Drawing.Size(47, 23)
        Me.planar_measure.TabIndex = 0
        Me.planar_measure.TabStop = True
        Me.planar_measure.Text = "Planar"
        Me.planar_measure.UseVisualStyleBackColor = True
        '
        'geodesic_measure
        '
        Me.geodesic_measure.Appearance = System.Windows.Forms.Appearance.Button
        Me.geodesic_measure.AutoSize = True
        Me.geodesic_measure.Location = New System.Drawing.Point(71, 19)
        Me.geodesic_measure.Name = "geodesic_measure"
        Me.geodesic_measure.Size = New System.Drawing.Size(62, 23)
        Me.geodesic_measure.TabIndex = 1
        Me.geodesic_measure.TabStop = True
        Me.geodesic_measure.Text = "Geodesic"
        Me.geodesic_measure.UseVisualStyleBackColor = True
        '
        'frm_distancetool
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(414, 266)
        Me.Controls.Add(Me.grpMEASSPACE)
        Me.Controls.Add(Me.btnSHHELP)
        Me.Controls.Add(Me.okay)
        Me.Controls.Add(Me.btnCANCEL)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.grpOUT)
        Me.Controls.Add(Me.lblLAYER)
        Me.Controls.Add(Me.inputlayer)
        Me.Name = "frm_distancetool"
        Me.Text = "frm_distancetool"
        Me.grpOUT.ResumeLayout(False)
        Me.grpOUT.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.grpMEASSPACE.ResumeLayout(False)
        Me.grpMEASSPACE.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblLAYER As System.Windows.Forms.Label
    Friend WithEvents inputlayer As System.Windows.Forms.ComboBox
    Friend WithEvents distanceTableOut As System.Windows.Forms.TextBox
    Friend WithEvents grpOUT As System.Windows.Forms.GroupBox
    Friend WithEvents knn As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnSHHELP As System.Windows.Forms.Button
    Friend WithEvents okay As System.Windows.Forms.Button
    Friend WithEvents btnCANCEL As System.Windows.Forms.Button
    Friend WithEvents grpMEASSPACE As System.Windows.Forms.GroupBox
    Friend WithEvents planar_measure As System.Windows.Forms.RadioButton
    Friend WithEvents geodesic_measure As System.Windows.Forms.RadioButton
End Class
