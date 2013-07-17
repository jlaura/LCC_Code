<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_intersecttool
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_intersecttool))
        Me.splcHELP = New System.Windows.Forms.SplitContainer()
        Me.clustmeth_grp = New System.Windows.Forms.GroupBox()
        Me.clustertab = New System.Windows.Forms.TabControl()
        Me.dbscan = New System.Windows.Forms.TabPage()
        Me.eps_grp = New System.Windows.Forms.GroupBox()
        Me.eps = New System.Windows.Forms.TextBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.minpts = New System.Windows.Forms.TextBox()
        Me.hierarchical = New System.Windows.Forms.TabPage()
        Me.grpCPNUM = New System.Windows.Forms.GroupBox()
        Me.txtCPNUM = New System.Windows.Forms.TextBox()
        Me.grpCLUSTER = New System.Windows.Forms.GroupBox()
        Me.radCMS = New System.Windows.Forms.RadioButton()
        Me.radCMBNND = New System.Windows.Forms.RadioButton()
        Me.radCMBS = New System.Windows.Forms.RadioButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.radCMBF = New System.Windows.Forms.RadioButton()
        Me.txtCMSVAL = New System.Windows.Forms.TextBox()
        Me.txtCMBSVAL = New System.Windows.Forms.TextBox()
        Me.txtCMBFVAL = New System.Windows.Forms.TextBox()
        Me.log_grp = New System.Windows.Forms.GroupBox()
        Me.LogSaveDialog = New System.Windows.Forms.Button()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.LogFileName = New System.Windows.Forms.TextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.weightedCentroids = New System.Windows.Forms.CheckBox()
        Me.grpOUT = New System.Windows.Forms.GroupBox()
        Me.txtOUT = New System.Windows.Forms.TextBox()
        Me.lblLAYER = New System.Windows.Forms.Label()
        Me.cboLAYER = New System.Windows.Forms.ComboBox()
        Me.grpMEASSPACE = New System.Windows.Forms.GroupBox()
        Me.radMEASPLAN = New System.Windows.Forms.RadioButton()
        Me.radMEASGEO = New System.Windows.Forms.RadioButton()
        Me.grpNQUERY = New System.Windows.Forms.GroupBox()
        Me.txtNQUERY = New System.Windows.Forms.TextBox()
        Me.pnlMARGIN2 = New System.Windows.Forms.Panel()
        Me.rtxtHELP_CNT = New System.Windows.Forms.RichTextBox()
        Me.pnlMARGIN1 = New System.Windows.Forms.Panel()
        Me.btnSHHELP = New System.Windows.Forms.Button()
        Me.btnCANCEL = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.splcHELP.Panel1.SuspendLayout()
        Me.splcHELP.Panel2.SuspendLayout()
        Me.splcHELP.SuspendLayout()
        Me.clustmeth_grp.SuspendLayout()
        Me.clustertab.SuspendLayout()
        Me.dbscan.SuspendLayout()
        Me.eps_grp.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.hierarchical.SuspendLayout()
        Me.grpCPNUM.SuspendLayout()
        Me.grpCLUSTER.SuspendLayout()
        Me.log_grp.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.grpOUT.SuspendLayout()
        Me.grpMEASSPACE.SuspendLayout()
        Me.grpNQUERY.SuspendLayout()
        Me.pnlMARGIN2.SuspendLayout()
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
        Me.splcHELP.Panel1.Controls.Add(Me.clustmeth_grp)
        Me.splcHELP.Panel1.Controls.Add(Me.log_grp)
        Me.splcHELP.Panel1.Controls.Add(Me.GroupBox2)
        Me.splcHELP.Panel1.Controls.Add(Me.grpOUT)
        Me.splcHELP.Panel1.Controls.Add(Me.lblLAYER)
        Me.splcHELP.Panel1.Controls.Add(Me.cboLAYER)
        Me.splcHELP.Panel1.Controls.Add(Me.grpMEASSPACE)
        Me.splcHELP.Panel1.Controls.Add(Me.grpNQUERY)
        Me.splcHELP.Panel1MinSize = 354
        '
        'splcHELP.Panel2
        '
        Me.splcHELP.Panel2.Controls.Add(Me.pnlMARGIN2)
        Me.splcHELP.Panel2.Controls.Add(Me.pnlMARGIN1)
        Me.splcHELP.Panel2Collapsed = True
        Me.splcHELP.Panel2MinSize = 0
        Me.splcHELP.Size = New System.Drawing.Size(356, 546)
        Me.splcHELP.SplitterDistance = 356
        Me.splcHELP.SplitterWidth = 1
        Me.splcHELP.TabIndex = 0
        '
        'clustmeth_grp
        '
        Me.clustmeth_grp.Controls.Add(Me.clustertab)
        Me.clustmeth_grp.Location = New System.Drawing.Point(8, 163)
        Me.clustmeth_grp.Name = "clustmeth_grp"
        Me.clustmeth_grp.Size = New System.Drawing.Size(337, 262)
        Me.clustmeth_grp.TabIndex = 10
        Me.clustmeth_grp.TabStop = False
        Me.clustmeth_grp.Text = "Clustering Method"
        '
        'clustertab
        '
        Me.clustertab.Controls.Add(Me.dbscan)
        Me.clustertab.Controls.Add(Me.hierarchical)
        Me.clustertab.Location = New System.Drawing.Point(10, 19)
        Me.clustertab.Name = "clustertab"
        Me.clustertab.SelectedIndex = 0
        Me.clustertab.Size = New System.Drawing.Size(321, 237)
        Me.clustertab.TabIndex = 7
        '
        'dbscan
        '
        Me.dbscan.Controls.Add(Me.eps_grp)
        Me.dbscan.Controls.Add(Me.GroupBox3)
        Me.dbscan.Location = New System.Drawing.Point(4, 22)
        Me.dbscan.Name = "dbscan"
        Me.dbscan.Size = New System.Drawing.Size(313, 211)
        Me.dbscan.TabIndex = 3
        Me.dbscan.Text = "DBSCAN"
        Me.dbscan.UseVisualStyleBackColor = True
        '
        'eps_grp
        '
        Me.eps_grp.Controls.Add(Me.eps)
        Me.eps_grp.Location = New System.Drawing.Point(8, 71)
        Me.eps_grp.Name = "eps_grp"
        Me.eps_grp.Size = New System.Drawing.Size(298, 49)
        Me.eps_grp.TabIndex = 1
        Me.eps_grp.TabStop = False
        Me.eps_grp.Text = "Epsilon (Minimum Distance)"
        '
        'eps
        '
        Me.eps.Location = New System.Drawing.Point(6, 20)
        Me.eps.Name = "eps"
        Me.eps.Size = New System.Drawing.Size(273, 20)
        Me.eps.TabIndex = 0
        Me.eps.Text = "7500"
        Me.eps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.minpts)
        Me.GroupBox3.Location = New System.Drawing.Point(8, 16)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(298, 49)
        Me.GroupBox3.TabIndex = 0
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Minimum Cluster Seed Size"
        '
        'minpts
        '
        Me.minpts.Location = New System.Drawing.Point(6, 20)
        Me.minpts.Name = "minpts"
        Me.minpts.Size = New System.Drawing.Size(273, 20)
        Me.minpts.TabIndex = 0
        Me.minpts.Text = "4"
        Me.minpts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'hierarchical
        '
        Me.hierarchical.Controls.Add(Me.grpCPNUM)
        Me.hierarchical.Controls.Add(Me.grpCLUSTER)
        Me.hierarchical.Location = New System.Drawing.Point(4, 22)
        Me.hierarchical.Name = "hierarchical"
        Me.hierarchical.Padding = New System.Windows.Forms.Padding(3)
        Me.hierarchical.Size = New System.Drawing.Size(313, 211)
        Me.hierarchical.TabIndex = 0
        Me.hierarchical.Text = "Hierarchical"
        Me.hierarchical.UseVisualStyleBackColor = True
        '
        'grpCPNUM
        '
        Me.grpCPNUM.Controls.Add(Me.txtCPNUM)
        Me.grpCPNUM.Location = New System.Drawing.Point(6, 8)
        Me.grpCPNUM.Name = "grpCPNUM"
        Me.grpCPNUM.Size = New System.Drawing.Size(301, 50)
        Me.grpCPNUM.TabIndex = 5
        Me.grpCPNUM.TabStop = False
        Me.grpCPNUM.Text = "Minimum Cluster Size"
        '
        'txtCPNUM
        '
        Me.txtCPNUM.Location = New System.Drawing.Point(18, 20)
        Me.txtCPNUM.Name = "txtCPNUM"
        Me.txtCPNUM.Size = New System.Drawing.Size(276, 20)
        Me.txtCPNUM.TabIndex = 0
        Me.txtCPNUM.Text = "5"
        Me.txtCPNUM.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'grpCLUSTER
        '
        Me.grpCLUSTER.Controls.Add(Me.radCMS)
        Me.grpCLUSTER.Controls.Add(Me.radCMBNND)
        Me.grpCLUSTER.Controls.Add(Me.radCMBS)
        Me.grpCLUSTER.Controls.Add(Me.Label1)
        Me.grpCLUSTER.Controls.Add(Me.radCMBF)
        Me.grpCLUSTER.Controls.Add(Me.txtCMSVAL)
        Me.grpCLUSTER.Controls.Add(Me.txtCMBSVAL)
        Me.grpCLUSTER.Controls.Add(Me.txtCMBFVAL)
        Me.grpCLUSTER.Location = New System.Drawing.Point(6, 64)
        Me.grpCLUSTER.Name = "grpCLUSTER"
        Me.grpCLUSTER.Size = New System.Drawing.Size(300, 141)
        Me.grpCLUSTER.TabIndex = 3
        Me.grpCLUSTER.TabStop = False
        Me.grpCLUSTER.Text = "Cluster Distance"
        '
        'radCMS
        '
        Me.radCMS.AutoSize = True
        Me.radCMS.Location = New System.Drawing.Point(18, 18)
        Me.radCMS.Name = "radCMS"
        Me.radCMS.Size = New System.Drawing.Size(110, 17)
        Me.radCMS.TabIndex = 0
        Me.radCMS.TabStop = True
        Me.radCMS.Text = "Fixed distance (m)"
        Me.radCMS.UseVisualStyleBackColor = True
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
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(7, 48)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(75, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Buffer options:"
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
        Me.txtCMSVAL.Size = New System.Drawing.Size(132, 20)
        Me.txtCMSVAL.TabIndex = 4
        Me.txtCMSVAL.Text = "1700"
        Me.txtCMSVAL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtCMBSVAL
        '
        Me.txtCMBSVAL.Location = New System.Drawing.Point(149, 111)
        Me.txtCMBSVAL.Name = "txtCMBSVAL"
        Me.txtCMBSVAL.Size = New System.Drawing.Size(132, 20)
        Me.txtCMBSVAL.TabIndex = 6
        Me.txtCMBSVAL.Text = "1700"
        Me.txtCMBSVAL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtCMBFVAL
        '
        Me.txtCMBFVAL.Location = New System.Drawing.Point(202, 88)
        Me.txtCMBFVAL.Name = "txtCMBFVAL"
        Me.txtCMBFVAL.Size = New System.Drawing.Size(79, 20)
        Me.txtCMBFVAL.TabIndex = 5
        Me.txtCMBFVAL.Text = "1.5"
        Me.txtCMBFVAL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'log_grp
        '
        Me.log_grp.Controls.Add(Me.LogSaveDialog)
        Me.log_grp.Controls.Add(Me.GroupBox6)
        Me.log_grp.Controls.Add(Me.LogFileName)
        Me.log_grp.Location = New System.Drawing.Point(7, 487)
        Me.log_grp.Name = "log_grp"
        Me.log_grp.Size = New System.Drawing.Size(338, 50)
        Me.log_grp.TabIndex = 20
        Me.log_grp.TabStop = False
        Me.log_grp.Text = "Output Log File Name (Optional)"
        '
        'LogSaveDialog
        '
        Me.LogSaveDialog.Image = Global.LCCAnalysisTools.My.Resources.Resources.normal_folder
        Me.LogSaveDialog.Location = New System.Drawing.Point(299, 16)
        Me.LogSaveDialog.Name = "LogSaveDialog"
        Me.LogSaveDialog.Size = New System.Drawing.Size(22, 23)
        Me.LogSaveDialog.TabIndex = 19
        Me.LogSaveDialog.UseVisualStyleBackColor = True
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.TextBox3)
        Me.GroupBox6.Location = New System.Drawing.Point(1, 50)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(404, 50)
        Me.GroupBox6.TabIndex = 18
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Output layer name"
        '
        'TextBox3
        '
        Me.TextBox3.Location = New System.Drawing.Point(14, 19)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(384, 20)
        Me.TextBox3.TabIndex = 0
        '
        'LogFileName
        '
        Me.LogFileName.Location = New System.Drawing.Point(14, 19)
        Me.LogFileName.Name = "LogFileName"
        Me.LogFileName.Size = New System.Drawing.Size(279, 20)
        Me.LogFileName.TabIndex = 0
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.weightedCentroids)
        Me.GroupBox2.Location = New System.Drawing.Point(180, 104)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(165, 53)
        Me.GroupBox2.TabIndex = 3
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Weight Centroid"
        '
        'weightedCentroids
        '
        Me.weightedCentroids.AutoSize = True
        Me.weightedCentroids.Checked = True
        Me.weightedCentroids.CheckState = System.Windows.Forms.CheckState.Checked
        Me.weightedCentroids.Location = New System.Drawing.Point(11, 24)
        Me.weightedCentroids.Name = "weightedCentroids"
        Me.weightedCentroids.Size = New System.Drawing.Size(107, 17)
        Me.weightedCentroids.TabIndex = 0
        Me.weightedCentroids.Text = "Weight Centroids"
        Me.weightedCentroids.UseVisualStyleBackColor = True
        '
        'grpOUT
        '
        Me.grpOUT.Controls.Add(Me.txtOUT)
        Me.grpOUT.Location = New System.Drawing.Point(7, 431)
        Me.grpOUT.Name = "grpOUT"
        Me.grpOUT.Size = New System.Drawing.Size(338, 50)
        Me.grpOUT.TabIndex = 5
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
        'lblLAYER
        '
        Me.lblLAYER.AutoSize = True
        Me.lblLAYER.Location = New System.Drawing.Point(4, 5)
        Me.lblLAYER.Name = "lblLAYER"
        Me.lblLAYER.Size = New System.Drawing.Size(94, 13)
        Me.lblLAYER.TabIndex = 0
        Me.lblLAYER.Text = "Input polyline layer"
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
        'grpMEASSPACE
        '
        Me.grpMEASSPACE.Controls.Add(Me.radMEASPLAN)
        Me.grpMEASSPACE.Controls.Add(Me.radMEASGEO)
        Me.grpMEASSPACE.Location = New System.Drawing.Point(7, 104)
        Me.grpMEASSPACE.Name = "grpMEASSPACE"
        Me.grpMEASSPACE.Size = New System.Drawing.Size(167, 53)
        Me.grpMEASSPACE.TabIndex = 2
        Me.grpMEASSPACE.TabStop = False
        Me.grpMEASSPACE.Text = "Measurement space"
        '
        'radMEASPLAN
        '
        Me.radMEASPLAN.Appearance = System.Windows.Forms.Appearance.Button
        Me.radMEASPLAN.AutoSize = True
        Me.radMEASPLAN.Location = New System.Drawing.Point(72, 18)
        Me.radMEASPLAN.Name = "radMEASPLAN"
        Me.radMEASPLAN.Size = New System.Drawing.Size(47, 23)
        Me.radMEASPLAN.TabIndex = 0
        Me.radMEASPLAN.Text = "Planar"
        Me.radMEASPLAN.UseVisualStyleBackColor = True
        '
        'radMEASGEO
        '
        Me.radMEASGEO.Appearance = System.Windows.Forms.Appearance.Button
        Me.radMEASGEO.AutoSize = True
        Me.radMEASGEO.Checked = True
        Me.radMEASGEO.Location = New System.Drawing.Point(6, 18)
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
        Me.grpNQUERY.Size = New System.Drawing.Size(338, 49)
        Me.grpNQUERY.TabIndex = 1
        Me.grpNQUERY.TabStop = False
        Me.grpNQUERY.Text = "Only when Nearest Neighbor distance is less than or equal to (m)"
        '
        'txtNQUERY
        '
        Me.txtNQUERY.Location = New System.Drawing.Point(18, 22)
        Me.txtNQUERY.Name = "txtNQUERY"
        Me.txtNQUERY.Size = New System.Drawing.Size(303, 20)
        Me.txtNQUERY.TabIndex = 0
        Me.txtNQUERY.Text = "30000"
        Me.txtNQUERY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'pnlMARGIN2
        '
        Me.pnlMARGIN2.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.pnlMARGIN2.Controls.Add(Me.rtxtHELP_CNT)
        Me.pnlMARGIN2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMARGIN2.Location = New System.Drawing.Point(12, 0)
        Me.pnlMARGIN2.Name = "pnlMARGIN2"
        Me.pnlMARGIN2.Size = New System.Drawing.Size(9, 96)
        Me.pnlMARGIN2.TabIndex = 2
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
        'pnlMARGIN1
        '
        Me.pnlMARGIN1.BackColor = System.Drawing.SystemColors.Window
        Me.pnlMARGIN1.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlMARGIN1.Location = New System.Drawing.Point(0, 0)
        Me.pnlMARGIN1.Name = "pnlMARGIN1"
        Me.pnlMARGIN1.Size = New System.Drawing.Size(12, 96)
        Me.pnlMARGIN1.TabIndex = 1
        '
        'btnSHHELP
        '
        Me.btnSHHELP.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSHHELP.Location = New System.Drawing.Point(263, 552)
        Me.btnSHHELP.Name = "btnSHHELP"
        Me.btnSHHELP.Size = New System.Drawing.Size(84, 23)
        Me.btnSHHELP.TabIndex = 3
        Me.btnSHHELP.Text = "Show Help >>"
        Me.btnSHHELP.UseVisualStyleBackColor = True
        '
        'btnCANCEL
        '
        Me.btnCANCEL.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnCANCEL.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCANCEL.Location = New System.Drawing.Point(182, 552)
        Me.btnCANCEL.Name = "btnCANCEL"
        Me.btnCANCEL.Size = New System.Drawing.Size(75, 23)
        Me.btnCANCEL.TabIndex = 2
        Me.btnCANCEL.Text = "Close"
        Me.btnCANCEL.UseVisualStyleBackColor = True
        '
        'btnOK
        '
        Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnOK.Location = New System.Drawing.Point(101, 552)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 1
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'frm_intersecttool
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCANCEL
        Me.ClientSize = New System.Drawing.Size(356, 577)
        Me.Controls.Add(Me.splcHELP)
        Me.Controls.Add(Me.btnSHHELP)
        Me.Controls.Add(Me.btnCANCEL)
        Me.Controls.Add(Me.btnOK)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(372, 615)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(372, 615)
        Me.Name = "frm_intersecttool"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Primary Impact Aproximation Tool"
        Me.splcHELP.Panel1.ResumeLayout(False)
        Me.splcHELP.Panel1.PerformLayout()
        Me.splcHELP.Panel2.ResumeLayout(False)
        Me.splcHELP.ResumeLayout(False)
        Me.clustmeth_grp.ResumeLayout(False)
        Me.clustertab.ResumeLayout(False)
        Me.dbscan.ResumeLayout(False)
        Me.eps_grp.ResumeLayout(False)
        Me.eps_grp.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.hierarchical.ResumeLayout(False)
        Me.grpCPNUM.ResumeLayout(False)
        Me.grpCPNUM.PerformLayout()
        Me.grpCLUSTER.ResumeLayout(False)
        Me.grpCLUSTER.PerformLayout()
        Me.log_grp.ResumeLayout(False)
        Me.log_grp.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.grpOUT.ResumeLayout(False)
        Me.grpOUT.PerformLayout()
        Me.grpMEASSPACE.ResumeLayout(False)
        Me.grpMEASSPACE.PerformLayout()
        Me.grpNQUERY.ResumeLayout(False)
        Me.grpNQUERY.PerformLayout()
        Me.pnlMARGIN2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents splcHELP As System.Windows.Forms.SplitContainer
    Friend WithEvents lblLAYER As System.Windows.Forms.Label
    Friend WithEvents cboLAYER As System.Windows.Forms.ComboBox
    Friend WithEvents pnlMARGIN2 As System.Windows.Forms.Panel
    Friend WithEvents rtxtHELP_CNT As System.Windows.Forms.RichTextBox
    Friend WithEvents pnlMARGIN1 As System.Windows.Forms.Panel
    Friend WithEvents btnSHHELP As System.Windows.Forms.Button
    Friend WithEvents btnCANCEL As System.Windows.Forms.Button
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents grpNQUERY As System.Windows.Forms.GroupBox
    Friend WithEvents txtNQUERY As System.Windows.Forms.TextBox
    Friend WithEvents grpMEASSPACE As System.Windows.Forms.GroupBox
    Friend WithEvents radMEASPLAN As System.Windows.Forms.RadioButton
    Friend WithEvents radMEASGEO As System.Windows.Forms.RadioButton
    Friend WithEvents grpOUT As System.Windows.Forms.GroupBox
    Friend WithEvents txtOUT As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents weightedCentroids As System.Windows.Forms.CheckBox
    Friend WithEvents log_grp As System.Windows.Forms.GroupBox
    Friend WithEvents LogSaveDialog As System.Windows.Forms.Button
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents LogFileName As System.Windows.Forms.TextBox
    Friend WithEvents clustmeth_grp As System.Windows.Forms.GroupBox
    Friend WithEvents clustertab As System.Windows.Forms.TabControl
    Friend WithEvents dbscan As System.Windows.Forms.TabPage
    Friend WithEvents eps_grp As System.Windows.Forms.GroupBox
    Friend WithEvents eps As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents minpts As System.Windows.Forms.TextBox
    Friend WithEvents hierarchical As System.Windows.Forms.TabPage
    Friend WithEvents grpCPNUM As System.Windows.Forms.GroupBox
    Friend WithEvents txtCPNUM As System.Windows.Forms.TextBox
    Friend WithEvents grpCLUSTER As System.Windows.Forms.GroupBox
    Friend WithEvents radCMS As System.Windows.Forms.RadioButton
    Friend WithEvents radCMBNND As System.Windows.Forms.RadioButton
    Friend WithEvents radCMBS As System.Windows.Forms.RadioButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents radCMBF As System.Windows.Forms.RadioButton
    Friend WithEvents txtCMSVAL As System.Windows.Forms.TextBox
    Friend WithEvents txtCMBSVAL As System.Windows.Forms.TextBox
    Friend WithEvents txtCMBFVAL As System.Windows.Forms.TextBox
End Class
