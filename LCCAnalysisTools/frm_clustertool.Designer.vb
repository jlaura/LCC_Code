<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_clustertool
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_clustertool))
        Me.radCMBNND = New System.Windows.Forms.RadioButton()
        Me.radCMBS = New System.Windows.Forms.RadioButton()
        Me.txtNQUERY = New System.Windows.Forms.TextBox()
        Me.radCMS = New System.Windows.Forms.RadioButton()
        Me.lblLAYER = New System.Windows.Forms.Label()
        Me.grpCLUSTER = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.radCMBF = New System.Windows.Forms.RadioButton()
        Me.txtCMSVAL = New System.Windows.Forms.TextBox()
        Me.txtCMBSVAL = New System.Windows.Forms.TextBox()
        Me.txtCMBFVAL = New System.Windows.Forms.TextBox()
        Me.btnCANCEL = New System.Windows.Forms.Button()
        Me.cboLAYER = New System.Windows.Forms.ComboBox()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnSHHELP = New System.Windows.Forms.Button()
        Me.splcHELP = New System.Windows.Forms.SplitContainer()
        Me.btnOptParam = New System.Windows.Forms.Button()
        Me.grpOUT = New System.Windows.Forms.GroupBox()
        Me.txtOUT = New System.Windows.Forms.TextBox()
        Me.grpMEASSPACE = New System.Windows.Forms.GroupBox()
        Me.radMEASPLAN = New System.Windows.Forms.RadioButton()
        Me.radMEASGEO = New System.Windows.Forms.RadioButton()
        Me.grpNQUERY = New System.Windows.Forms.GroupBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.rtxtHELP_CNT = New System.Windows.Forms.RichTextBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.grpCLUSTER.SuspendLayout()
        Me.splcHELP.Panel1.SuspendLayout()
        Me.splcHELP.Panel2.SuspendLayout()
        Me.splcHELP.SuspendLayout()
        Me.grpOUT.SuspendLayout()
        Me.grpMEASSPACE.SuspendLayout()
        Me.grpNQUERY.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'radCMBNND
        '
        Me.radCMBNND.AutoSize = True
        Me.radCMBNND.Location = New System.Drawing.Point(19, 66)
        Me.radCMBNND.Name = "radCMBNND"
        Me.radCMBNND.Size = New System.Drawing.Size(149, 17)
        Me.radCMBNND.TabIndex = 1
        Me.radCMBNND.TabStop = True
        Me.radCMBNND.Text = "Nearest neighbor distance"
        Me.radCMBNND.UseVisualStyleBackColor = True
        '
        'radCMBS
        '
        Me.radCMBS.AutoSize = True
        Me.radCMBS.Location = New System.Drawing.Point(19, 112)
        Me.radCMBS.Name = "radCMBS"
        Me.radCMBS.Size = New System.Drawing.Size(110, 17)
        Me.radCMBS.TabIndex = 3
        Me.radCMBS.TabStop = True
        Me.radCMBS.Text = "Fixed distance (m)"
        Me.radCMBS.UseVisualStyleBackColor = True
        '
        'txtNQUERY
        '
        Me.txtNQUERY.Location = New System.Drawing.Point(18, 19)
        Me.txtNQUERY.Name = "txtNQUERY"
        Me.txtNQUERY.Size = New System.Drawing.Size(366, 20)
        Me.txtNQUERY.TabIndex = 0
        Me.txtNQUERY.Text = "1500"
        Me.txtNQUERY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'radCMS
        '
        Me.radCMS.AutoSize = True
        Me.radCMS.Location = New System.Drawing.Point(18, 19)
        Me.radCMS.Name = "radCMS"
        Me.radCMS.Size = New System.Drawing.Size(110, 17)
        Me.radCMS.TabIndex = 0
        Me.radCMS.TabStop = True
        Me.radCMS.Text = "Fixed distance (m)"
        Me.radCMS.UseVisualStyleBackColor = True
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
        'grpCLUSTER
        '
        Me.grpCLUSTER.Controls.Add(Me.radCMS)
        Me.grpCLUSTER.Controls.Add(Me.radCMBNND)
        Me.grpCLUSTER.Controls.Add(Me.radCMBS)
        Me.grpCLUSTER.Controls.Add(Me.Label3)
        Me.grpCLUSTER.Controls.Add(Me.radCMBF)
        Me.grpCLUSTER.Controls.Add(Me.txtCMSVAL)
        Me.grpCLUSTER.Controls.Add(Me.txtCMBSVAL)
        Me.grpCLUSTER.Controls.Add(Me.txtCMBFVAL)
        Me.grpCLUSTER.Location = New System.Drawing.Point(7, 165)
        Me.grpCLUSTER.Name = "grpCLUSTER"
        Me.grpCLUSTER.Size = New System.Drawing.Size(400, 141)
        Me.grpCLUSTER.TabIndex = 3
        Me.grpCLUSTER.TabStop = False
        Me.grpCLUSTER.Text = "Cluster method"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(7, 48)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(75, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Buffer options:"
        '
        'radCMBF
        '
        Me.radCMBF.AutoSize = True
        Me.radCMBF.Checked = True
        Me.radCMBF.Location = New System.Drawing.Point(19, 89)
        Me.radCMBF.Name = "radCMBF"
        Me.radCMBF.Size = New System.Drawing.Size(166, 17)
        Me.radCMBF.TabIndex = 2
        Me.radCMBF.TabStop = True
        Me.radCMBF.Text = "Nearest neighbor distance    x"
        Me.radCMBF.UseVisualStyleBackColor = True
        '
        'txtCMSVAL
        '
        Me.txtCMSVAL.Location = New System.Drawing.Point(149, 18)
        Me.txtCMSVAL.Name = "txtCMSVAL"
        Me.txtCMSVAL.Size = New System.Drawing.Size(235, 20)
        Me.txtCMSVAL.TabIndex = 4
        Me.txtCMSVAL.Text = "1700"
        Me.txtCMSVAL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtCMBSVAL
        '
        Me.txtCMBSVAL.Location = New System.Drawing.Point(149, 111)
        Me.txtCMBSVAL.Name = "txtCMBSVAL"
        Me.txtCMBSVAL.Size = New System.Drawing.Size(235, 20)
        Me.txtCMBSVAL.TabIndex = 6
        Me.txtCMBSVAL.Text = "1700"
        Me.txtCMBSVAL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtCMBFVAL
        '
        Me.txtCMBFVAL.Location = New System.Drawing.Point(202, 88)
        Me.txtCMBFVAL.Name = "txtCMBFVAL"
        Me.txtCMBFVAL.Size = New System.Drawing.Size(182, 20)
        Me.txtCMBFVAL.TabIndex = 5
        Me.txtCMBFVAL.Text = "1.5"
        Me.txtCMBFVAL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'btnCANCEL
        '
        Me.btnCANCEL.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnCANCEL.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCANCEL.Location = New System.Drawing.Point(102, 432)
        Me.btnCANCEL.Name = "btnCANCEL"
        Me.btnCANCEL.Size = New System.Drawing.Size(75, 23)
        Me.btnCANCEL.TabIndex = 2
        Me.btnCANCEL.Text = "Close"
        Me.btnCANCEL.UseVisualStyleBackColor = True
        '
        'cboLAYER
        '
        Me.cboLAYER.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboLAYER.FormattingEnabled = True
        Me.cboLAYER.Location = New System.Drawing.Point(7, 22)
        Me.cboLAYER.Name = "cboLAYER"
        Me.cboLAYER.Size = New System.Drawing.Size(400, 21)
        Me.cboLAYER.TabIndex = 0
        '
        'btnOK
        '
        Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnOK.Location = New System.Drawing.Point(9, 432)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 1
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'btnSHHELP
        '
        Me.btnSHHELP.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSHHELP.Location = New System.Drawing.Point(325, 432)
        Me.btnSHHELP.Name = "btnSHHELP"
        Me.btnSHHELP.Size = New System.Drawing.Size(84, 23)
        Me.btnSHHELP.TabIndex = 3
        Me.btnSHHELP.Text = "Show Help >>"
        Me.btnSHHELP.UseVisualStyleBackColor = True
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
        Me.splcHELP.Panel1.Controls.Add(Me.grpOUT)
        Me.splcHELP.Panel1.Controls.Add(Me.grpMEASSPACE)
        Me.splcHELP.Panel1.Controls.Add(Me.grpNQUERY)
        Me.splcHELP.Panel1.Controls.Add(Me.grpCLUSTER)
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
        Me.splcHELP.Size = New System.Drawing.Size(419, 429)
        Me.splcHELP.SplitterDistance = 356
        Me.splcHELP.SplitterWidth = 1
        Me.splcHELP.TabIndex = 0
        '
        'btnOptParam
        '
        Me.btnOptParam.Location = New System.Drawing.Point(192, 487)
        Me.btnOptParam.Name = "btnOptParam"
        Me.btnOptParam.Size = New System.Drawing.Size(119, 23)
        Me.btnOptParam.TabIndex = 4
        Me.btnOptParam.Text = "Optimize Parameters"
        Me.btnOptParam.UseVisualStyleBackColor = True
        '
        'grpOUT
        '
        Me.grpOUT.Controls.Add(Me.txtOUT)
        Me.grpOUT.Location = New System.Drawing.Point(3, 320)
        Me.grpOUT.Name = "grpOUT"
        Me.grpOUT.Size = New System.Drawing.Size(400, 50)
        Me.grpOUT.TabIndex = 4
        Me.grpOUT.TabStop = False
        Me.grpOUT.Text = "Output layer name"
        '
        'txtOUT
        '
        Me.txtOUT.Location = New System.Drawing.Point(18, 20)
        Me.txtOUT.Name = "txtOUT"
        Me.txtOUT.Size = New System.Drawing.Size(366, 20)
        Me.txtOUT.TabIndex = 0
        '
        'grpMEASSPACE
        '
        Me.grpMEASSPACE.Controls.Add(Me.radMEASPLAN)
        Me.grpMEASSPACE.Controls.Add(Me.radMEASGEO)
        Me.grpMEASSPACE.Location = New System.Drawing.Point(7, 106)
        Me.grpMEASSPACE.Name = "grpMEASSPACE"
        Me.grpMEASSPACE.Size = New System.Drawing.Size(400, 53)
        Me.grpMEASSPACE.TabIndex = 2
        Me.grpMEASSPACE.TabStop = False
        Me.grpMEASSPACE.Text = "Measurement space"
        '
        'radMEASPLAN
        '
        Me.radMEASPLAN.Appearance = System.Windows.Forms.Appearance.Button
        Me.radMEASPLAN.AutoSize = True
        Me.radMEASPLAN.Checked = True
        Me.radMEASPLAN.Location = New System.Drawing.Point(18, 19)
        Me.radMEASPLAN.Name = "radMEASPLAN"
        Me.radMEASPLAN.Size = New System.Drawing.Size(47, 23)
        Me.radMEASPLAN.TabIndex = 0
        Me.radMEASPLAN.TabStop = True
        Me.radMEASPLAN.Text = "Planar"
        Me.radMEASPLAN.UseVisualStyleBackColor = True
        '
        'radMEASGEO
        '
        Me.radMEASGEO.Appearance = System.Windows.Forms.Appearance.Button
        Me.radMEASGEO.AutoSize = True
        Me.radMEASGEO.Location = New System.Drawing.Point(71, 19)
        Me.radMEASGEO.Name = "radMEASGEO"
        Me.radMEASGEO.Size = New System.Drawing.Size(60, 23)
        Me.radMEASGEO.TabIndex = 1
        Me.radMEASGEO.TabStop = True
        Me.radMEASGEO.Text = "Geodetic"
        Me.radMEASGEO.UseVisualStyleBackColor = True
        '
        'grpNQUERY
        '
        Me.grpNQUERY.Controls.Add(Me.txtNQUERY)
        Me.grpNQUERY.Location = New System.Drawing.Point(7, 49)
        Me.grpNQUERY.Name = "grpNQUERY"
        Me.grpNQUERY.Size = New System.Drawing.Size(400, 51)
        Me.grpNQUERY.TabIndex = 1
        Me.grpNQUERY.TabStop = False
        Me.grpNQUERY.Text = "Threshold Distance"
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
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(4, 5)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(85, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Input point layer:"
        '
        'frm_clustertool
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCANCEL
        Me.ClientSize = New System.Drawing.Size(419, 457)
        Me.Controls.Add(Me.btnOptParam)
        Me.Controls.Add(Me.btnCANCEL)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.btnSHHELP)
        Me.Controls.Add(Me.splcHELP)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(900, 550)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(372, 495)
        Me.Name = "frm_clustertool"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Cluster Tool"
        Me.grpCLUSTER.ResumeLayout(False)
        Me.grpCLUSTER.PerformLayout()
        Me.splcHELP.Panel1.ResumeLayout(False)
        Me.splcHELP.Panel1.PerformLayout()
        Me.splcHELP.Panel2.ResumeLayout(False)
        Me.splcHELP.ResumeLayout(False)
        Me.grpOUT.ResumeLayout(False)
        Me.grpOUT.PerformLayout()
        Me.grpMEASSPACE.ResumeLayout(False)
        Me.grpMEASSPACE.PerformLayout()
        Me.grpNQUERY.ResumeLayout(False)
        Me.grpNQUERY.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents radCMBNND As System.Windows.Forms.RadioButton
    Friend WithEvents radCMBS As System.Windows.Forms.RadioButton
    Friend WithEvents txtNQUERY As System.Windows.Forms.TextBox
    Friend WithEvents radCMS As System.Windows.Forms.RadioButton
    Friend WithEvents lblLAYER As System.Windows.Forms.Label
    Friend WithEvents grpCLUSTER As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents radCMBF As System.Windows.Forms.RadioButton
    Friend WithEvents txtCMSVAL As System.Windows.Forms.TextBox
    Friend WithEvents txtCMBSVAL As System.Windows.Forms.TextBox
    Friend WithEvents txtCMBFVAL As System.Windows.Forms.TextBox
    Friend WithEvents btnCANCEL As System.Windows.Forms.Button
    Friend WithEvents cboLAYER As System.Windows.Forms.ComboBox
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnSHHELP As System.Windows.Forms.Button
    Friend WithEvents splcHELP As System.Windows.Forms.SplitContainer
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents radMEASPLAN As System.Windows.Forms.RadioButton
    Friend WithEvents radMEASGEO As System.Windows.Forms.RadioButton
    Friend WithEvents txtOUT As System.Windows.Forms.TextBox
    Friend WithEvents grpOUT As System.Windows.Forms.GroupBox
    Friend WithEvents grpMEASSPACE As System.Windows.Forms.GroupBox
    Friend WithEvents grpNQUERY As System.Windows.Forms.GroupBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents rtxtHELP_CNT As System.Windows.Forms.RichTextBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnOptParam As System.Windows.Forms.Button
End Class
