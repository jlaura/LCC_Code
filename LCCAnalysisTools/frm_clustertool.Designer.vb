﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
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
        Me.radMEASGEO = New System.Windows.Forms.RadioButton()
        Me.radMEASPLAN = New System.Windows.Forms.RadioButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.maxstats = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.minstats = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.lqstats = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.uqstats = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.stdstats = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.rangestats = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.medianstats = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.meanstats = New System.Windows.Forms.TextBox()
        Me.grpOUT = New System.Windows.Forms.GroupBox()
        Me.txtOUT = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Disttable = New System.Windows.Forms.ComboBox()
        Me.grpNQUERY = New System.Windows.Forms.GroupBox()
        Me.optimize = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.tabcontrol = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.kgraph = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.minpts = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.rtxtHELP_CNT = New System.Windows.Forms.RichTextBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.grpCLUSTER.SuspendLayout()
        Me.splcHELP.Panel1.SuspendLayout()
        Me.splcHELP.Panel2.SuspendLayout()
        Me.splcHELP.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.grpOUT.SuspendLayout()
        Me.grpNQUERY.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.tabcontrol.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
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
        Me.txtNQUERY.Location = New System.Drawing.Point(10, 19)
        Me.txtNQUERY.Name = "txtNQUERY"
        Me.txtNQUERY.Size = New System.Drawing.Size(212, 20)
        Me.txtNQUERY.TabIndex = 0
        Me.txtNQUERY.Text = "1500"
        Me.txtNQUERY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
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
        'lblLAYER
        '
        Me.lblLAYER.AutoSize = True
        Me.lblLAYER.Location = New System.Drawing.Point(4, 4)
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
        Me.grpCLUSTER.Location = New System.Drawing.Point(6, 15)
        Me.grpCLUSTER.Name = "grpCLUSTER"
        Me.grpCLUSTER.Size = New System.Drawing.Size(360, 141)
        Me.grpCLUSTER.TabIndex = 3
        Me.grpCLUSTER.TabStop = False
        Me.grpCLUSTER.Text = "Cluster Distance"
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
        Me.txtCMSVAL.Size = New System.Drawing.Size(205, 20)
        Me.txtCMSVAL.TabIndex = 4
        Me.txtCMSVAL.Text = "1700"
        Me.txtCMSVAL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtCMBSVAL
        '
        Me.txtCMBSVAL.Location = New System.Drawing.Point(149, 111)
        Me.txtCMBSVAL.Name = "txtCMBSVAL"
        Me.txtCMBSVAL.Size = New System.Drawing.Size(205, 20)
        Me.txtCMBSVAL.TabIndex = 6
        Me.txtCMBSVAL.Text = "1700"
        Me.txtCMBSVAL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtCMBFVAL
        '
        Me.txtCMBFVAL.Location = New System.Drawing.Point(202, 88)
        Me.txtCMBFVAL.Name = "txtCMBFVAL"
        Me.txtCMBFVAL.Size = New System.Drawing.Size(152, 20)
        Me.txtCMBFVAL.TabIndex = 5
        Me.txtCMBFVAL.Text = "1.5"
        Me.txtCMBFVAL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'btnCANCEL
        '
        Me.btnCANCEL.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnCANCEL.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCANCEL.Location = New System.Drawing.Point(90, 539)
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
        Me.cboLAYER.Location = New System.Drawing.Point(7, 21)
        Me.cboLAYER.Name = "cboLAYER"
        Me.cboLAYER.Size = New System.Drawing.Size(400, 21)
        Me.cboLAYER.TabIndex = 0
        '
        'btnOK
        '
        Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnOK.Location = New System.Drawing.Point(9, 539)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 1
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'btnSHHELP
        '
        Me.btnSHHELP.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSHHELP.Location = New System.Drawing.Point(325, 539)
        Me.btnSHHELP.Name = "btnSHHELP"
        Me.btnSHHELP.Size = New System.Drawing.Size(84, 23)
        Me.btnSHHELP.TabIndex = 4
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
        Me.splcHELP.Panel1.Controls.Add(Me.radMEASGEO)
        Me.splcHELP.Panel1.Controls.Add(Me.radMEASPLAN)
        Me.splcHELP.Panel1.Controls.Add(Me.GroupBox1)
        Me.splcHELP.Panel1.Controls.Add(Me.grpOUT)
        Me.splcHELP.Panel1.Controls.Add(Me.Label1)
        Me.splcHELP.Panel1.Controls.Add(Me.Disttable)
        Me.splcHELP.Panel1.Controls.Add(Me.grpNQUERY)
        Me.splcHELP.Panel1.Controls.Add(Me.lblLAYER)
        Me.splcHELP.Panel1.Controls.Add(Me.cboLAYER)
        Me.splcHELP.Panel1.Controls.Add(Me.GroupBox2)
        Me.splcHELP.Panel1MinSize = 354
        '
        'splcHELP.Panel2
        '
        Me.splcHELP.Panel2.Controls.Add(Me.Panel1)
        Me.splcHELP.Panel2.Controls.Add(Me.Panel2)
        Me.splcHELP.Panel2Collapsed = True
        Me.splcHELP.Panel2MinSize = 0
        Me.splcHELP.Size = New System.Drawing.Size(415, 536)
        Me.splcHELP.SplitterDistance = 356
        Me.splcHELP.SplitterWidth = 1
        Me.splcHELP.TabIndex = 0
        '
        'radMEASGEO
        '
        Me.radMEASGEO.Appearance = System.Windows.Forms.Appearance.Button
        Me.radMEASGEO.AutoSize = True
        Me.radMEASGEO.Location = New System.Drawing.Point(322, 75)
        Me.radMEASGEO.Name = "radMEASGEO"
        Me.radMEASGEO.Size = New System.Drawing.Size(60, 23)
        Me.radMEASGEO.TabIndex = 17
        Me.radMEASGEO.TabStop = True
        Me.radMEASGEO.Text = "Geodetic"
        Me.radMEASGEO.UseVisualStyleBackColor = True
        '
        'radMEASPLAN
        '
        Me.radMEASPLAN.Appearance = System.Windows.Forms.Appearance.Button
        Me.radMEASPLAN.AutoSize = True
        Me.radMEASPLAN.Checked = True
        Me.radMEASPLAN.Location = New System.Drawing.Point(323, 46)
        Me.radMEASPLAN.Name = "radMEASPLAN"
        Me.radMEASPLAN.Size = New System.Drawing.Size(47, 23)
        Me.radMEASPLAN.TabIndex = 16
        Me.radMEASPLAN.TabStop = True
        Me.radMEASPLAN.Text = "Planar"
        Me.radMEASPLAN.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.maxstats)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.minstats)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.lqstats)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.uqstats)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.stdstats)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.rangestats)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.medianstats)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.meanstats)
        Me.GroupBox1.Location = New System.Drawing.Point(7, 161)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(400, 97)
        Me.GroupBox1.TabIndex = 8
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Nearest Neighbor Statistics"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(283, 49)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(27, 13)
        Me.Label9.TabIndex = 15
        Me.Label9.Text = "Max"
        '
        'maxstats
        '
        Me.maxstats.Location = New System.Drawing.Point(315, 46)
        Me.maxstats.Name = "maxstats"
        Me.maxstats.ReadOnly = True
        Me.maxstats.Size = New System.Drawing.Size(75, 20)
        Me.maxstats.TabIndex = 14
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(283, 22)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(27, 13)
        Me.Label8.TabIndex = 13
        Me.Label8.Text = "Min."
        '
        'minstats
        '
        Me.minstats.Location = New System.Drawing.Point(315, 19)
        Me.minstats.Name = "minstats"
        Me.minstats.ReadOnly = True
        Me.minstats.Size = New System.Drawing.Size(75, 20)
        Me.minstats.TabIndex = 12
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(128, 78)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(65, 13)
        Me.Label12.TabIndex = 11
        Me.Label12.Text = "Lower Quar."
        '
        'lqstats
        '
        Me.lqstats.Location = New System.Drawing.Point(202, 71)
        Me.lqstats.Name = "lqstats"
        Me.lqstats.ReadOnly = True
        Me.lqstats.Size = New System.Drawing.Size(75, 20)
        Me.lqstats.TabIndex = 10
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(128, 49)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(65, 13)
        Me.Label7.TabIndex = 9
        Me.Label7.Text = "Upper Quar."
        '
        'uqstats
        '
        Me.uqstats.Location = New System.Drawing.Point(202, 45)
        Me.uqstats.Name = "uqstats"
        Me.uqstats.ReadOnly = True
        Me.uqstats.Size = New System.Drawing.Size(75, 20)
        Me.uqstats.TabIndex = 8
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(128, 22)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(68, 13)
        Me.Label11.TabIndex = 7
        Me.Label11.Text = "St. Deviation"
        '
        'stdstats
        '
        Me.stdstats.Location = New System.Drawing.Point(202, 19)
        Me.stdstats.Name = "stdstats"
        Me.stdstats.ReadOnly = True
        Me.stdstats.Size = New System.Drawing.Size(75, 20)
        Me.stdstats.TabIndex = 6
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(7, 74)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(39, 13)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "Range"
        '
        'rangestats
        '
        Me.rangestats.Location = New System.Drawing.Point(47, 71)
        Me.rangestats.Name = "rangestats"
        Me.rangestats.ReadOnly = True
        Me.rangestats.Size = New System.Drawing.Size(75, 20)
        Me.rangestats.TabIndex = 4
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(7, 49)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(42, 13)
        Me.Label5.TabIndex = 3
        Me.Label5.Text = "Median"
        '
        'medianstats
        '
        Me.medianstats.Location = New System.Drawing.Point(47, 46)
        Me.medianstats.Name = "medianstats"
        Me.medianstats.ReadOnly = True
        Me.medianstats.Size = New System.Drawing.Size(75, 20)
        Me.medianstats.TabIndex = 2
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(7, 22)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(34, 13)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "Mean"
        '
        'meanstats
        '
        Me.meanstats.Location = New System.Drawing.Point(47, 19)
        Me.meanstats.Name = "meanstats"
        Me.meanstats.ReadOnly = True
        Me.meanstats.Size = New System.Drawing.Size(75, 20)
        Me.meanstats.TabIndex = 0
        '
        'grpOUT
        '
        Me.grpOUT.Controls.Add(Me.txtOUT)
        Me.grpOUT.Location = New System.Drawing.Point(3, 479)
        Me.grpOUT.Name = "grpOUT"
        Me.grpOUT.Size = New System.Drawing.Size(404, 50)
        Me.grpOUT.TabIndex = 4
        Me.grpOUT.TabStop = False
        Me.grpOUT.Text = "Output layer name"
        '
        'txtOUT
        '
        Me.txtOUT.Location = New System.Drawing.Point(14, 19)
        Me.txtOUT.Name = "txtOUT"
        Me.txtOUT.Size = New System.Drawing.Size(384, 20)
        Me.txtOUT.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(4, 51)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(106, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Input Distance Table"
        '
        'Disttable
        '
        Me.Disttable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Disttable.FormattingEnabled = True
        Me.Disttable.Location = New System.Drawing.Point(7, 68)
        Me.Disttable.Name = "Disttable"
        Me.Disttable.Size = New System.Drawing.Size(277, 21)
        Me.Disttable.TabIndex = 5
        '
        'grpNQUERY
        '
        Me.grpNQUERY.Controls.Add(Me.optimize)
        Me.grpNQUERY.Controls.Add(Me.txtNQUERY)
        Me.grpNQUERY.Location = New System.Drawing.Point(7, 104)
        Me.grpNQUERY.Name = "grpNQUERY"
        Me.grpNQUERY.Size = New System.Drawing.Size(400, 51)
        Me.grpNQUERY.TabIndex = 1
        Me.grpNQUERY.TabStop = False
        Me.grpNQUERY.Text = "Outlier Threshold Distance"
        '
        'optimize
        '
        Me.optimize.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.optimize.Location = New System.Drawing.Point(255, 17)
        Me.optimize.Name = "optimize"
        Me.optimize.Size = New System.Drawing.Size(139, 23)
        Me.optimize.TabIndex = 3
        Me.optimize.Text = "Optimize"
        Me.optimize.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.tabcontrol)
        Me.GroupBox2.Location = New System.Drawing.Point(7, 264)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(400, 209)
        Me.GroupBox2.TabIndex = 9
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Clustering Method"
        '
        'tabcontrol
        '
        Me.tabcontrol.Controls.Add(Me.TabPage1)
        Me.tabcontrol.Controls.Add(Me.TabPage2)
        Me.tabcontrol.Controls.Add(Me.TabPage3)
        Me.tabcontrol.Controls.Add(Me.TabPage4)
        Me.tabcontrol.Location = New System.Drawing.Point(10, 19)
        Me.tabcontrol.Name = "tabcontrol"
        Me.tabcontrol.SelectedIndex = 0
        Me.tabcontrol.Size = New System.Drawing.Size(384, 185)
        Me.tabcontrol.TabIndex = 7
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.grpCLUSTER)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(376, 159)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Heirarchal"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(376, 159)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "S-Link"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'TabPage3
        '
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(376, 159)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "D-Link"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.kgraph)
        Me.TabPage4.Controls.Add(Me.GroupBox3)
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Size = New System.Drawing.Size(376, 159)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "DBSCAN"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'kgraph
        '
        Me.kgraph.Location = New System.Drawing.Point(8, 122)
        Me.kgraph.Name = "kgraph"
        Me.kgraph.Size = New System.Drawing.Size(360, 23)
        Me.kgraph.TabIndex = 5
        Me.kgraph.Text = "Display K-Distance Graph"
        Me.kgraph.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.minpts)
        Me.GroupBox3.Location = New System.Drawing.Point(8, 26)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(360, 49)
        Me.GroupBox3.TabIndex = 0
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Minimum Cluster Seed Size"
        '
        'minpts
        '
        Me.minpts.Location = New System.Drawing.Point(6, 20)
        Me.minpts.Name = "minpts"
        Me.minpts.Size = New System.Drawing.Size(347, 20)
        Me.minpts.TabIndex = 0
        Me.minpts.Text = "4"
        Me.minpts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
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
        Me.ClientSize = New System.Drawing.Size(415, 564)
        Me.Controls.Add(Me.splcHELP)
        Me.Controls.Add(Me.btnCANCEL)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.btnSHHELP)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(1000, 650)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(431, 602)
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
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.grpOUT.ResumeLayout(False)
        Me.grpOUT.PerformLayout()
        Me.grpNQUERY.ResumeLayout(False)
        Me.grpNQUERY.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.tabcontrol.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage4.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
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
    Friend WithEvents txtOUT As System.Windows.Forms.TextBox
    Friend WithEvents grpOUT As System.Windows.Forms.GroupBox
    Friend WithEvents grpNQUERY As System.Windows.Forms.GroupBox
    Friend WithEvents rtxtHELP_CNT As System.Windows.Forms.RichTextBox
    Friend WithEvents optimize As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Disttable As System.Windows.Forms.ComboBox
    Friend WithEvents tabcontrol As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents lqstats As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents uqstats As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents stdstats As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents rangestats As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents medianstats As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents meanstats As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents maxstats As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents minstats As System.Windows.Forms.TextBox
    Friend WithEvents radMEASGEO As System.Windows.Forms.RadioButton
    Friend WithEvents radMEASPLAN As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents minpts As System.Windows.Forms.TextBox
    Friend WithEvents kgraph As System.Windows.Forms.Button
End Class
