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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_distancetool))
        Me.btnSHHELP = New System.Windows.Forms.Button()
        Me.okay = New System.Windows.Forms.Button()
        Me.btnCANCEL = New System.Windows.Forms.Button()
        Me.splcHELP = New System.Windows.Forms.SplitContainer()
        Me.grpMEASSPACE = New System.Windows.Forms.GroupBox()
        Me.planar_measure = New System.Windows.Forms.RadioButton()
        Me.geodesic_measure = New System.Windows.Forms.RadioButton()
        Me.knn_grp = New System.Windows.Forms.GroupBox()
        Me.knn = New System.Windows.Forms.TextBox()
        Me.grpOUT = New System.Windows.Forms.GroupBox()
        Me.distanceTableOut = New System.Windows.Forms.TextBox()
        Me.lblLAYER = New System.Windows.Forms.Label()
        Me.inputlayer = New System.Windows.Forms.ComboBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.rtxtHELP_CNT = New System.Windows.Forms.RichTextBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.splcHELP.Panel1.SuspendLayout()
        Me.splcHELP.Panel2.SuspendLayout()
        Me.splcHELP.SuspendLayout()
        Me.grpMEASSPACE.SuspendLayout()
        Me.knn_grp.SuspendLayout()
        Me.grpOUT.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnSHHELP
        '
        Me.btnSHHELP.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSHHELP.Location = New System.Drawing.Point(325, 241)
        Me.btnSHHELP.Name = "btnSHHELP"
        Me.btnSHHELP.Size = New System.Drawing.Size(84, 23)
        Me.btnSHHELP.TabIndex = 8
        Me.btnSHHELP.Text = "Show Help >>"
        Me.btnSHHELP.UseVisualStyleBackColor = True
        '
        'okay
        '
        Me.okay.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.okay.Location = New System.Drawing.Point(12, 241)
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
        Me.btnCANCEL.Location = New System.Drawing.Point(93, 241)
        Me.btnCANCEL.Name = "btnCANCEL"
        Me.btnCANCEL.Size = New System.Drawing.Size(75, 23)
        Me.btnCANCEL.TabIndex = 7
        Me.btnCANCEL.Text = "Close"
        Me.btnCANCEL.UseVisualStyleBackColor = True
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
        Me.splcHELP.Panel1.Controls.Add(Me.grpMEASSPACE)
        Me.splcHELP.Panel1.Controls.Add(Me.knn_grp)
        Me.splcHELP.Panel1.Controls.Add(Me.grpOUT)
        Me.splcHELP.Panel1.Controls.Add(Me.lblLAYER)
        Me.splcHELP.Panel1.Controls.Add(Me.inputlayer)
        Me.splcHELP.Panel1MinSize = 421
        '
        'splcHELP.Panel2
        '
        Me.splcHELP.Panel2.Controls.Add(Me.Panel1)
        Me.splcHELP.Panel2.Controls.Add(Me.Panel2)
        Me.splcHELP.Panel2Collapsed = True
        Me.splcHELP.Panel2MinSize = 0
        Me.splcHELP.Size = New System.Drawing.Size(422, 232)
        Me.splcHELP.SplitterDistance = 421
        Me.splcHELP.SplitterWidth = 1
        Me.splcHELP.TabIndex = 10
        '
        'grpMEASSPACE
        '
        Me.grpMEASSPACE.Controls.Add(Me.planar_measure)
        Me.grpMEASSPACE.Controls.Add(Me.geodesic_measure)
        Me.grpMEASSPACE.Location = New System.Drawing.Point(13, 107)
        Me.grpMEASSPACE.Name = "grpMEASSPACE"
        Me.grpMEASSPACE.Size = New System.Drawing.Size(400, 53)
        Me.grpMEASSPACE.TabIndex = 13
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
        'knn_grp
        '
        Me.knn_grp.Controls.Add(Me.knn)
        Me.knn_grp.Location = New System.Drawing.Point(13, 51)
        Me.knn_grp.Name = "knn_grp"
        Me.knn_grp.Size = New System.Drawing.Size(400, 50)
        Me.knn_grp.TabIndex = 15
        Me.knn_grp.TabStop = False
        Me.knn_grp.Text = "Number of nearest neighbors"
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
        'grpOUT
        '
        Me.grpOUT.Controls.Add(Me.distanceTableOut)
        Me.grpOUT.Location = New System.Drawing.Point(16, 166)
        Me.grpOUT.Name = "grpOUT"
        Me.grpOUT.Size = New System.Drawing.Size(397, 51)
        Me.grpOUT.TabIndex = 14
        Me.grpOUT.TabStop = False
        Me.grpOUT.Text = "Output Table Name"
        '
        'distanceTableOut
        '
        Me.distanceTableOut.Location = New System.Drawing.Point(18, 20)
        Me.distanceTableOut.Name = "distanceTableOut"
        Me.distanceTableOut.Size = New System.Drawing.Size(373, 20)
        Me.distanceTableOut.TabIndex = 0
        '
        'lblLAYER
        '
        Me.lblLAYER.AutoSize = True
        Me.lblLAYER.Location = New System.Drawing.Point(10, 7)
        Me.lblLAYER.Name = "lblLAYER"
        Me.lblLAYER.Size = New System.Drawing.Size(87, 13)
        Me.lblLAYER.TabIndex = 12
        Me.lblLAYER.Text = "Input Point Layer"
        '
        'inputlayer
        '
        Me.inputlayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.inputlayer.FormattingEnabled = True
        Me.inputlayer.Location = New System.Drawing.Point(13, 24)
        Me.inputlayer.Name = "inputlayer"
        Me.inputlayer.Size = New System.Drawing.Size(400, 21)
        Me.inputlayer.TabIndex = 11
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
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(422, 276)
        Me.Controls.Add(Me.btnSHHELP)
        Me.Controls.Add(Me.okay)
        Me.Controls.Add(Me.btnCANCEL)
        Me.Controls.Add(Me.splcHELP)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(900, 314)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(438, 314)
        Me.Name = "frm_distancetool"
        Me.Text = "frm_distancetool"
        Me.splcHELP.Panel1.ResumeLayout(False)
        Me.splcHELP.Panel1.PerformLayout()
        Me.splcHELP.Panel2.ResumeLayout(False)
        Me.splcHELP.ResumeLayout(False)
        Me.grpMEASSPACE.ResumeLayout(False)
        Me.grpMEASSPACE.PerformLayout()
        Me.knn_grp.ResumeLayout(False)
        Me.knn_grp.PerformLayout()
        Me.grpOUT.ResumeLayout(False)
        Me.grpOUT.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnSHHELP As System.Windows.Forms.Button
    Friend WithEvents okay As System.Windows.Forms.Button
    Friend WithEvents btnCANCEL As System.Windows.Forms.Button
    Friend WithEvents splcHELP As System.Windows.Forms.SplitContainer
    Friend WithEvents grpMEASSPACE As System.Windows.Forms.GroupBox
    Friend WithEvents planar_measure As System.Windows.Forms.RadioButton
    Friend WithEvents geodesic_measure As System.Windows.Forms.RadioButton
    Friend WithEvents knn_grp As System.Windows.Forms.GroupBox
    Friend WithEvents knn As System.Windows.Forms.TextBox
    Friend WithEvents grpOUT As System.Windows.Forms.GroupBox
    Friend WithEvents distanceTableOut As System.Windows.Forms.TextBox
    Friend WithEvents lblLAYER As System.Windows.Forms.Label
    Friend WithEvents inputlayer As System.Windows.Forms.ComboBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents rtxtHELP_CNT As System.Windows.Forms.RichTextBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
End Class