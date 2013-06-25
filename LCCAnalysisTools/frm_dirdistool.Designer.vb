<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_dirdistool
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_dirdistool))
        Me.splcHELP = New System.Windows.Forms.SplitContainer()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.OutputLogFile = New System.Windows.Forms.TextBox()
        Me.SaveLog = New System.Windows.Forms.CheckBox()
        Me.grpOUT = New System.Windows.Forms.GroupBox()
        Me.txtOUT = New System.Windows.Forms.TextBox()
        Me.grpOUTGEOM = New System.Windows.Forms.GroupBox()
        Me.radOUTLINE = New System.Windows.Forms.RadioButton()
        Me.radOUTELLIPSE = New System.Windows.Forms.RadioButton()
        Me.grpDDPNUM = New System.Windows.Forms.GroupBox()
        Me.optimize = New System.Windows.Forms.Button()
        Me.txtDDPNUM = New System.Windows.Forms.TextBox()
        Me.lblLAYER = New System.Windows.Forms.Label()
        Me.cboLAYER = New System.Windows.Forms.ComboBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.rtxtHELP_CNT = New System.Windows.Forms.RichTextBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btnCANCEL = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnSHHELP = New System.Windows.Forms.Button()
        Me.splcHELP.Panel1.SuspendLayout()
        Me.splcHELP.Panel2.SuspendLayout()
        Me.splcHELP.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.grpOUT.SuspendLayout()
        Me.grpOUTGEOM.SuspendLayout()
        Me.grpDDPNUM.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
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
        Me.splcHELP.Panel1.Controls.Add(Me.GroupBox1)
        Me.splcHELP.Panel1.Controls.Add(Me.grpOUT)
        Me.splcHELP.Panel1.Controls.Add(Me.grpOUTGEOM)
        Me.splcHELP.Panel1.Controls.Add(Me.grpDDPNUM)
        Me.splcHELP.Panel1.Controls.Add(Me.lblLAYER)
        Me.splcHELP.Panel1.Controls.Add(Me.cboLAYER)
        Me.splcHELP.Panel1MinSize = 354
        '
        'splcHELP.Panel2
        '
        Me.splcHELP.Panel2.Controls.Add(Me.Panel1)
        Me.splcHELP.Panel2.Controls.Add(Me.Panel2)
        Me.splcHELP.Panel2Collapsed = True
        Me.splcHELP.Panel2MinSize = 0
        Me.splcHELP.Size = New System.Drawing.Size(356, 429)
        Me.splcHELP.SplitterDistance = 356
        Me.splcHELP.SplitterWidth = 1
        Me.splcHELP.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.OutputLogFile)
        Me.GroupBox1.Controls.Add(Me.SaveLog)
        Me.GroupBox1.Location = New System.Drawing.Point(7, 244)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(338, 73)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Log File Output Name"
        '
        'OutputLogFile
        '
        Me.OutputLogFile.Location = New System.Drawing.Point(18, 19)
        Me.OutputLogFile.Name = "OutputLogFile"
        Me.OutputLogFile.Size = New System.Drawing.Size(303, 20)
        Me.OutputLogFile.TabIndex = 0
        '
        'SaveLog
        '
        Me.SaveLog.AutoSize = True
        Me.SaveLog.Checked = True
        Me.SaveLog.CheckState = System.Windows.Forms.CheckState.Checked
        Me.SaveLog.Location = New System.Drawing.Point(200, 45)
        Me.SaveLog.Name = "SaveLog"
        Me.SaveLog.Size = New System.Drawing.Size(121, 17)
        Me.SaveLog.TabIndex = 4
        Me.SaveLog.Text = "Save Text Log File?"
        Me.SaveLog.UseVisualStyleBackColor = True
        '
        'grpOUT
        '
        Me.grpOUT.Controls.Add(Me.txtOUT)
        Me.grpOUT.Location = New System.Drawing.Point(7, 176)
        Me.grpOUT.Name = "grpOUT"
        Me.grpOUT.Size = New System.Drawing.Size(338, 50)
        Me.grpOUT.TabIndex = 3
        Me.grpOUT.TabStop = False
        Me.grpOUT.Text = "Output layer name"
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
        'lblLAYER
        '
        Me.lblLAYER.AutoSize = True
        Me.lblLAYER.Location = New System.Drawing.Point(4, 5)
        Me.lblLAYER.Name = "lblLAYER"
        Me.lblLAYER.Size = New System.Drawing.Size(82, 13)
        Me.lblLAYER.TabIndex = 0
        Me.lblLAYER.Text = "Input point layer"
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
        'btnCANCEL
        '
        Me.btnCANCEL.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnCANCEL.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCANCEL.Location = New System.Drawing.Point(182, 432)
        Me.btnCANCEL.Name = "btnCANCEL"
        Me.btnCANCEL.Size = New System.Drawing.Size(75, 23)
        Me.btnCANCEL.TabIndex = 2
        Me.btnCANCEL.Text = "Close"
        Me.btnCANCEL.UseVisualStyleBackColor = True
        '
        'btnOK
        '
        Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnOK.Location = New System.Drawing.Point(101, 432)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 1
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'btnSHHELP
        '
        Me.btnSHHELP.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSHHELP.Location = New System.Drawing.Point(263, 432)
        Me.btnSHHELP.Name = "btnSHHELP"
        Me.btnSHHELP.Size = New System.Drawing.Size(84, 23)
        Me.btnSHHELP.TabIndex = 3
        Me.btnSHHELP.Text = "Show Help >>"
        Me.btnSHHELP.UseVisualStyleBackColor = True
        '
        'frm_dirdistool
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCANCEL
        Me.ClientSize = New System.Drawing.Size(356, 457)
        Me.Controls.Add(Me.btnCANCEL)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.btnSHHELP)
        Me.Controls.Add(Me.splcHELP)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(900, 495)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(372, 495)
        Me.Name = "frm_dirdistool"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Directional Distribution Tool"
        Me.splcHELP.Panel1.ResumeLayout(False)
        Me.splcHELP.Panel1.PerformLayout()
        Me.splcHELP.Panel2.ResumeLayout(False)
        Me.splcHELP.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.grpOUT.ResumeLayout(False)
        Me.grpOUT.PerformLayout()
        Me.grpOUTGEOM.ResumeLayout(False)
        Me.grpOUTGEOM.PerformLayout()
        Me.grpDDPNUM.ResumeLayout(False)
        Me.grpDDPNUM.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents splcHELP As System.Windows.Forms.SplitContainer
    Friend WithEvents lblLAYER As System.Windows.Forms.Label
    Friend WithEvents cboLAYER As System.Windows.Forms.ComboBox
    Friend WithEvents rtxtHELP_CNT As System.Windows.Forms.RichTextBox
    Friend WithEvents btnCANCEL As System.Windows.Forms.Button
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnSHHELP As System.Windows.Forms.Button
    Friend WithEvents txtOUT As System.Windows.Forms.TextBox
    Friend WithEvents txtDDPNUM As System.Windows.Forms.TextBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents radOUTLINE As System.Windows.Forms.RadioButton
    Friend WithEvents radOUTELLIPSE As System.Windows.Forms.RadioButton
    Friend WithEvents grpOUT As System.Windows.Forms.GroupBox
    Friend WithEvents grpOUTGEOM As System.Windows.Forms.GroupBox
    Friend WithEvents grpDDPNUM As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents OutputLogFile As System.Windows.Forms.TextBox
    Friend WithEvents SaveLog As System.Windows.Forms.CheckBox
    Friend WithEvents optimize As System.Windows.Forms.Button
End Class
