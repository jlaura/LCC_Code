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
        Me.splcHELP = New System.Windows.Forms.SplitContainer()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txtOUT = New System.Windows.Forms.TextBox()
        Me.grpOUTGEOM = New System.Windows.Forms.GroupBox()
        Me.radOUTLINE = New System.Windows.Forms.RadioButton()
        Me.radOUTELLIPSE = New System.Windows.Forms.RadioButton()
        Me.grpDDPNUM = New System.Windows.Forms.GroupBox()
        Me.optimize = New System.Windows.Forms.Button()
        Me.txtDDPNUM = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboLAYER = New System.Windows.Forms.ComboBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.rtxtHELP_CNT = New System.Windows.Forms.RichTextBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.grpOUT.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.grpMEASSPACE.SuspendLayout()
        Me.splcHELP.Panel1.SuspendLayout()
        Me.splcHELP.Panel2.SuspendLayout()
        Me.splcHELP.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.grpOUTGEOM.SuspendLayout()
        Me.grpDDPNUM.SuspendLayout()
        Me.Panel1.SuspendLayout()
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
        Me.btnSHHELP.Location = New System.Drawing.Point(266, 249)
        Me.btnSHHELP.Name = "btnSHHELP"
        Me.btnSHHELP.Size = New System.Drawing.Size(84, 23)
        Me.btnSHHELP.TabIndex = 8
        Me.btnSHHELP.Text = "Show Help >>"
        Me.btnSHHELP.UseVisualStyleBackColor = True
        '
        'okay
        '
        Me.okay.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.okay.Location = New System.Drawing.Point(104, 249)
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
        Me.btnCANCEL.Location = New System.Drawing.Point(185, 249)
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
        'splcHELP
        '
        Me.splcHELP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.splcHELP.Dock = System.Windows.Forms.DockStyle.Top
        Me.splcHELP.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.splcHELP.IsSplitterFixed = True
        Me.splcHELP.Location = New System.Drawing.Point(0, 0)
        Me.splcHELP.Name = "splcHELP"
        '
        'splcHELP.Panel1
        '
        Me.splcHELP.Panel1.Controls.Add(Me.GroupBox2)
        Me.splcHELP.Panel1.Controls.Add(Me.grpOUTGEOM)
        Me.splcHELP.Panel1.Controls.Add(Me.grpDDPNUM)
        Me.splcHELP.Panel1.Controls.Add(Me.Label1)
        Me.splcHELP.Panel1.Controls.Add(Me.cboLAYER)
        Me.splcHELP.Panel1MinSize = 354
        '
        'splcHELP.Panel2
        '
        Me.splcHELP.Panel2.Controls.Add(Me.Panel1)
        Me.splcHELP.Panel2.Controls.Add(Me.Panel2)
        Me.splcHELP.Panel2Collapsed = True
        Me.splcHELP.Panel2MinSize = 0
        Me.splcHELP.Size = New System.Drawing.Size(355, 241)
        Me.splcHELP.SplitterDistance = 355
        Me.splcHELP.SplitterWidth = 1
        Me.splcHELP.TabIndex = 9
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtOUT)
        Me.GroupBox2.Location = New System.Drawing.Point(7, 176)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(338, 50)
        Me.GroupBox2.TabIndex = 3
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Output layer name"
        '
        'txtOUT
        '
        Me.txtOUT.Location = New System.Drawing.Point(18, 20)
        Me.txtOUT.Name = "txtOUT"
        Me.txtOUT.Size = New System.Drawing.Size(303, 20)
        Me.txtOUT.TabIndex = 0
        '
        'grpOUTGEOM
        '
        Me.grpOUTGEOM.Controls.Add(Me.radOUTLINE)
        Me.grpOUTGEOM.Controls.Add(Me.radOUTELLIPSE)
        Me.grpOUTGEOM.Location = New System.Drawing.Point(7, 117)
        Me.grpOUTGEOM.Name = "grpOUTGEOM"
        Me.grpOUTGEOM.Size = New System.Drawing.Size(338, 53)
        Me.grpOUTGEOM.TabIndex = 2
        Me.grpOUTGEOM.TabStop = False
        Me.grpOUTGEOM.Text = "Output geometry"
        '
        'radOUTLINE
        '
        Me.radOUTLINE.Appearance = System.Windows.Forms.Appearance.Button
        Me.radOUTLINE.AutoSize = True
        Me.radOUTLINE.Checked = True
        Me.radOUTLINE.Location = New System.Drawing.Point(18, 19)
        Me.radOUTLINE.Name = "radOUTLINE"
        Me.radOUTLINE.Size = New System.Drawing.Size(37, 23)
        Me.radOUTLINE.TabIndex = 0
        Me.radOUTLINE.TabStop = True
        Me.radOUTLINE.Text = "Line"
        Me.radOUTLINE.UseVisualStyleBackColor = True
        '
        'radOUTELLIPSE
        '
        Me.radOUTELLIPSE.Appearance = System.Windows.Forms.Appearance.Button
        Me.radOUTELLIPSE.AutoSize = True
        Me.radOUTELLIPSE.Location = New System.Drawing.Point(61, 19)
        Me.radOUTELLIPSE.Name = "radOUTELLIPSE"
        Me.radOUTELLIPSE.Size = New System.Drawing.Size(47, 23)
        Me.radOUTELLIPSE.TabIndex = 1
        Me.radOUTELLIPSE.TabStop = True
        Me.radOUTELLIPSE.Text = "Ellipse"
        Me.radOUTELLIPSE.UseVisualStyleBackColor = True
        '
        'grpDDPNUM
        '
        Me.grpDDPNUM.Controls.Add(Me.optimize)
        Me.grpDDPNUM.Controls.Add(Me.txtDDPNUM)
        Me.grpDDPNUM.Location = New System.Drawing.Point(7, 58)
        Me.grpDDPNUM.Name = "grpDDPNUM"
        Me.grpDDPNUM.Size = New System.Drawing.Size(338, 53)
        Me.grpDDPNUM.TabIndex = 1
        Me.grpDDPNUM.TabStop = False
        Me.grpDDPNUM.Text = "Only when number of points in cluster is greater than or equal to"
        '
        'optimize
        '
        Me.optimize.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.optimize.Location = New System.Drawing.Point(193, 17)
        Me.optimize.Name = "optimize"
        Me.optimize.Size = New System.Drawing.Size(139, 23)
        Me.optimize.TabIndex = 3
        Me.optimize.Text = "Optimize"
        Me.optimize.UseVisualStyleBackColor = True
        '
        'txtDDPNUM
        '
        Me.txtDDPNUM.Location = New System.Drawing.Point(18, 20)
        Me.txtDDPNUM.Name = "txtDDPNUM"
        Me.txtDDPNUM.Size = New System.Drawing.Size(149, 20)
        Me.txtDDPNUM.TabIndex = 0
        Me.txtDDPNUM.Text = "10"
        Me.txtDDPNUM.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(4, 5)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(82, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Input point layer"
        '
        'cboLAYER
        '
        Me.cboLAYER.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboLAYER.FormattingEnabled = True
        Me.cboLAYER.Location = New System.Drawing.Point(7, 22)
        Me.cboLAYER.Name = "cboLAYER"
        Me.cboLAYER.Size = New System.Drawing.Size(338, 21)
        Me.cboLAYER.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.rtxtHELP_CNT)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(12, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(9, 96)
        Me.Panel1.TabIndex = 2
        '
        'rtxtHELP_CNT
        '
        Me.rtxtHELP_CNT.BackColor = System.Drawing.SystemColors.Window
        Me.rtxtHELP_CNT.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.rtxtHELP_CNT.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rtxtHELP_CNT.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtxtHELP_CNT.HideSelection = False
        Me.rtxtHELP_CNT.Location = New System.Drawing.Point(0, 0)
        Me.rtxtHELP_CNT.Name = "rtxtHELP_CNT"
        Me.rtxtHELP_CNT.ReadOnly = True
        Me.rtxtHELP_CNT.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical
        Me.rtxtHELP_CNT.Size = New System.Drawing.Size(9, 96)
        Me.rtxtHELP_CNT.TabIndex = 0
        Me.rtxtHELP_CNT.TabStop = False
        Me.rtxtHELP_CNT.Text = ""
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.SystemColors.Window
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(12, 96)
        Me.Panel2.TabIndex = 14
        '
        'frm_distancetool
        '
        Me.AcceptButton = Me.okay
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCANCEL
        Me.ClientSize = New System.Drawing.Size(355, 278)
        Me.Controls.Add(Me.splcHELP)
        Me.Controls.Add(Me.grpMEASSPACE)
        Me.Controls.Add(Me.btnSHHELP)
        Me.Controls.Add(Me.okay)
        Me.Controls.Add(Me.btnCANCEL)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.grpOUT)
        Me.Controls.Add(Me.lblLAYER)
        Me.Controls.Add(Me.inputlayer)
        Me.MaximumSize = New System.Drawing.Size(371, 316)
        Me.MinimumSize = New System.Drawing.Size(371, 316)
        Me.Name = "frm_distancetool"
        Me.Text = "frm_distancetool"
        Me.grpOUT.ResumeLayout(False)
        Me.grpOUT.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.grpMEASSPACE.ResumeLayout(False)
        Me.grpMEASSPACE.PerformLayout()
        Me.splcHELP.Panel1.ResumeLayout(False)
        Me.splcHELP.Panel1.PerformLayout()
        Me.splcHELP.Panel2.ResumeLayout(False)
        Me.splcHELP.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.grpOUTGEOM.ResumeLayout(False)
        Me.grpOUTGEOM.PerformLayout()
        Me.grpDDPNUM.ResumeLayout(False)
        Me.grpDDPNUM.PerformLayout()
        Me.Panel1.ResumeLayout(False)
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
    Friend WithEvents splcHELP As System.Windows.Forms.SplitContainer
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtOUT As System.Windows.Forms.TextBox
    Friend WithEvents grpOUTGEOM As System.Windows.Forms.GroupBox
    Friend WithEvents radOUTLINE As System.Windows.Forms.RadioButton
    Friend WithEvents radOUTELLIPSE As System.Windows.Forms.RadioButton
    Friend WithEvents grpDDPNUM As System.Windows.Forms.GroupBox
    Friend WithEvents optimize As System.Windows.Forms.Button
    Friend WithEvents txtDDPNUM As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cboLAYER As System.Windows.Forms.ComboBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents rtxtHELP_CNT As System.Windows.Forms.RichTextBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
End Class
