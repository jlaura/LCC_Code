<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_trajecttool
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_trajecttool))
        Me.splcHELP = New System.Windows.Forms.SplitContainer()
        Me.log_grp = New System.Windows.Forms.GroupBox()
        Me.LogSaveDialog = New System.Windows.Forms.Button()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.LogFileName = New System.Windows.Forms.TextBox()
        Me.grpOUT = New System.Windows.Forms.GroupBox()
        Me.txtOUT = New System.Windows.Forms.TextBox()
        Me.grpCOREFF = New System.Windows.Forms.GroupBox()
        Me.txtCEPROTP = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblCEPROTP = New System.Windows.Forms.Label()
        Me.chkCEEJV = New System.Windows.Forms.CheckBox()
        Me.txtCEEJVVAL = New System.Windows.Forms.TextBox()
        Me.lblCEEJVUNITS = New System.Windows.Forms.Label()
        Me.grpTRAJDIST = New System.Windows.Forms.GroupBox()
        Me.radTDDIST = New System.Windows.Forms.RadioButton()
        Me.radTDDEG = New System.Windows.Forms.RadioButton()
        Me.lblTDDEGUNITS = New System.Windows.Forms.Label()
        Me.cboTDDEGVAL = New System.Windows.Forms.ComboBox()
        Me.txtTDDISTVAL = New System.Windows.Forms.TextBox()
        Me.lblTDDISTUNITS = New System.Windows.Forms.Label()
        Me.grpCLUSTERREQ = New System.Windows.Forms.GroupBox()
        Me.optimize = New System.Windows.Forms.Button()
        Me.pnlIF = New System.Windows.Forms.Panel()
        Me.lblEIFUNITS = New System.Windows.Forms.Label()
        Me.txtEIFVAL = New System.Windows.Forms.TextBox()
        Me.lblEIF = New System.Windows.Forms.Label()
        Me.cboEIFMOD = New System.Windows.Forms.ComboBox()
        Me.lblEMAUNITS = New System.Windows.Forms.Label()
        Me.txtEMAVAL = New System.Windows.Forms.TextBox()
        Me.cboEMAMOD = New System.Windows.Forms.ComboBox()
        Me.lblEMA = New System.Windows.Forms.Label()
        Me.cboLAYER = New System.Windows.Forms.ComboBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.inputds = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.remove_layer = New System.Windows.Forms.Button()
        Me.AddData = New System.Windows.Forms.Button()
        Me.pnlMARGIN2 = New System.Windows.Forms.Panel()
        Me.rtxtHELP_CNT = New System.Windows.Forms.RichTextBox()
        Me.pnlMARGIN1 = New System.Windows.Forms.Panel()
        Me.btnSHHELP = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnCANCEL = New System.Windows.Forms.Button()
        Me.splcHELP.Panel1.SuspendLayout()
        Me.splcHELP.Panel2.SuspendLayout()
        Me.splcHELP.SuspendLayout()
        Me.log_grp.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.grpOUT.SuspendLayout()
        Me.grpCOREFF.SuspendLayout()
        Me.grpTRAJDIST.SuspendLayout()
        Me.grpCLUSTERREQ.SuspendLayout()
        Me.pnlIF.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.splcHELP.Panel1.Controls.Add(Me.log_grp)
        Me.splcHELP.Panel1.Controls.Add(Me.grpOUT)
        Me.splcHELP.Panel1.Controls.Add(Me.grpCOREFF)
        Me.splcHELP.Panel1.Controls.Add(Me.grpTRAJDIST)
        Me.splcHELP.Panel1.Controls.Add(Me.grpCLUSTERREQ)
        Me.splcHELP.Panel1.Controls.Add(Me.cboLAYER)
        Me.splcHELP.Panel1.Controls.Add(Me.GroupBox1)
        Me.splcHELP.Panel1MinSize = 354
        '
        'splcHELP.Panel2
        '
        Me.splcHELP.Panel2.Controls.Add(Me.pnlMARGIN2)
        Me.splcHELP.Panel2.Controls.Add(Me.pnlMARGIN1)
        Me.splcHELP.Panel2Collapsed = True
        Me.splcHELP.Panel2MinSize = 0
        Me.splcHELP.Size = New System.Drawing.Size(356, 513)
        Me.splcHELP.SplitterDistance = 356
        Me.splcHELP.SplitterWidth = 1
        Me.splcHELP.TabIndex = 0
        '
        'log_grp
        '
        Me.log_grp.Controls.Add(Me.LogSaveDialog)
        Me.log_grp.Controls.Add(Me.GroupBox6)
        Me.log_grp.Controls.Add(Me.LogFileName)
        Me.log_grp.Location = New System.Drawing.Point(7, 453)
        Me.log_grp.Name = "log_grp"
        Me.log_grp.Size = New System.Drawing.Size(338, 50)
        Me.log_grp.TabIndex = 20
        Me.log_grp.TabStop = False
        Me.log_grp.Text = "Output Log File Name (Optional)"
        '
        'LogSaveDialog
        '
        Me.LogSaveDialog.Image = Global.LCCAnalysisTools.My.Resources.Resources.normal_folder
        Me.LogSaveDialog.Location = New System.Drawing.Point(308, 16)
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
        Me.LogFileName.Size = New System.Drawing.Size(288, 20)
        Me.LogFileName.TabIndex = 0
        '
        'grpOUT
        '
        Me.grpOUT.Controls.Add(Me.txtOUT)
        Me.grpOUT.Location = New System.Drawing.Point(7, 395)
        Me.grpOUT.Name = "grpOUT"
        Me.grpOUT.Size = New System.Drawing.Size(338, 50)
        Me.grpOUT.TabIndex = 4
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
        'grpCOREFF
        '
        Me.grpCOREFF.Controls.Add(Me.txtCEPROTP)
        Me.grpCOREFF.Controls.Add(Me.Label2)
        Me.grpCOREFF.Controls.Add(Me.lblCEPROTP)
        Me.grpCOREFF.Controls.Add(Me.chkCEEJV)
        Me.grpCOREFF.Controls.Add(Me.txtCEEJVVAL)
        Me.grpCOREFF.Controls.Add(Me.lblCEEJVUNITS)
        Me.grpCOREFF.Location = New System.Drawing.Point(7, 313)
        Me.grpCOREFF.Name = "grpCOREFF"
        Me.grpCOREFF.Size = New System.Drawing.Size(338, 76)
        Me.grpCOREFF.TabIndex = 3
        Me.grpCOREFF.TabStop = False
        Me.grpCOREFF.Text = "Coriolis effect"
        '
        'txtCEPROTP
        '
        Me.txtCEPROTP.Enabled = False
        Me.txtCEPROTP.Location = New System.Drawing.Point(202, 46)
        Me.txtCEPROTP.Name = "txtCEPROTP"
        Me.txtCEPROTP.Size = New System.Drawing.Size(100, 20)
        Me.txtCEPROTP.TabIndex = 2
        Me.txtCEPROTP.Text = "88642.6632"
        Me.txtCEPROTP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(306, 49)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(15, 13)
        Me.Label2.TabIndex = 13
        Me.Label2.Text = "s."
        '
        'lblCEPROTP
        '
        Me.lblCEPROTP.AutoSize = True
        Me.lblCEPROTP.Location = New System.Drawing.Point(33, 49)
        Me.lblCEPROTP.Name = "lblCEPROTP"
        Me.lblCEPROTP.Size = New System.Drawing.Size(107, 13)
        Me.lblCEPROTP.TabIndex = 11
        Me.lblCEPROTP.Text = "Planet rotation period"
        '
        'chkCEEJV
        '
        Me.chkCEEJV.AutoSize = True
        Me.chkCEEJV.Location = New System.Drawing.Point(18, 19)
        Me.chkCEEJV.Name = "chkCEEJV"
        Me.chkCEEJV.Size = New System.Drawing.Size(135, 17)
        Me.chkCEEJV.TabIndex = 0
        Me.chkCEEJV.Text = "Avg. horizontal velocity"
        Me.chkCEEJV.UseVisualStyleBackColor = True
        '
        'txtCEEJVVAL
        '
        Me.txtCEEJVVAL.Enabled = False
        Me.txtCEEJVVAL.Location = New System.Drawing.Point(202, 17)
        Me.txtCEEJVVAL.Name = "txtCEEJVVAL"
        Me.txtCEEJVVAL.Size = New System.Drawing.Size(100, 20)
        Me.txtCEEJVVAL.TabIndex = 1
        Me.txtCEEJVVAL.Text = "4000"
        Me.txtCEEJVVAL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblCEEJVUNITS
        '
        Me.lblCEEJVUNITS.AutoSize = True
        Me.lblCEEJVUNITS.Location = New System.Drawing.Point(306, 20)
        Me.lblCEEJVUNITS.Name = "lblCEEJVUNITS"
        Me.lblCEEJVUNITS.Size = New System.Drawing.Size(25, 13)
        Me.lblCEEJVUNITS.TabIndex = 2
        Me.lblCEEJVUNITS.Text = "m/s"
        '
        'grpTRAJDIST
        '
        Me.grpTRAJDIST.Controls.Add(Me.radTDDIST)
        Me.grpTRAJDIST.Controls.Add(Me.radTDDEG)
        Me.grpTRAJDIST.Controls.Add(Me.lblTDDEGUNITS)
        Me.grpTRAJDIST.Controls.Add(Me.cboTDDEGVAL)
        Me.grpTRAJDIST.Controls.Add(Me.txtTDDISTVAL)
        Me.grpTRAJDIST.Controls.Add(Me.lblTDDISTUNITS)
        Me.grpTRAJDIST.Location = New System.Drawing.Point(7, 257)
        Me.grpTRAJDIST.Name = "grpTRAJDIST"
        Me.grpTRAJDIST.Size = New System.Drawing.Size(338, 50)
        Me.grpTRAJDIST.TabIndex = 2
        Me.grpTRAJDIST.TabStop = False
        Me.grpTRAJDIST.Text = "Trajectory distance"
        '
        'radTDDIST
        '
        Me.radTDDIST.AutoSize = True
        Me.radTDDIST.Location = New System.Drawing.Point(182, 23)
        Me.radTDDIST.Name = "radTDDIST"
        Me.radTDDIST.Size = New System.Drawing.Size(14, 13)
        Me.radTDDIST.TabIndex = 2
        Me.radTDDIST.TabStop = True
        Me.radTDDIST.UseVisualStyleBackColor = True
        '
        'radTDDEG
        '
        Me.radTDDEG.AutoSize = True
        Me.radTDDEG.Checked = True
        Me.radTDDEG.Location = New System.Drawing.Point(18, 23)
        Me.radTDDEG.Name = "radTDDEG"
        Me.radTDDEG.Size = New System.Drawing.Size(14, 13)
        Me.radTDDEG.TabIndex = 0
        Me.radTDDEG.TabStop = True
        Me.radTDDEG.UseVisualStyleBackColor = True
        '
        'lblTDDEGUNITS
        '
        Me.lblTDDEGUNITS.AutoSize = True
        Me.lblTDDEGUNITS.Location = New System.Drawing.Point(123, 24)
        Me.lblTDDEGUNITS.Name = "lblTDDEGUNITS"
        Me.lblTDDEGUNITS.Size = New System.Drawing.Size(45, 13)
        Me.lblTDDEGUNITS.TabIndex = 3
        Me.lblTDDEGUNITS.Text = "degrees"
        '
        'cboTDDEGVAL
        '
        Me.cboTDDEGVAL.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTDDEGVAL.FormattingEnabled = True
        Me.cboTDDEGVAL.Items.AddRange(New Object() {"10", "24", "45", "90", "180", "270", "360"})
        Me.cboTDDEGVAL.Location = New System.Drawing.Point(36, 19)
        Me.cboTDDEGVAL.Name = "cboTDDEGVAL"
        Me.cboTDDEGVAL.Size = New System.Drawing.Size(83, 21)
        Me.cboTDDEGVAL.TabIndex = 1
        '
        'txtTDDISTVAL
        '
        Me.txtTDDISTVAL.Enabled = False
        Me.txtTDDISTVAL.Location = New System.Drawing.Point(202, 20)
        Me.txtTDDISTVAL.Name = "txtTDDISTVAL"
        Me.txtTDDISTVAL.Size = New System.Drawing.Size(100, 20)
        Me.txtTDDISTVAL.TabIndex = 3
        Me.txtTDDISTVAL.Text = "500000"
        Me.txtTDDISTVAL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblTDDISTUNITS
        '
        Me.lblTDDISTUNITS.AutoSize = True
        Me.lblTDDISTUNITS.Location = New System.Drawing.Point(306, 21)
        Me.lblTDDISTUNITS.Name = "lblTDDISTUNITS"
        Me.lblTDDISTUNITS.Size = New System.Drawing.Size(18, 13)
        Me.lblTDDISTUNITS.TabIndex = 2
        Me.lblTDDISTUNITS.Text = "m."
        '
        'grpCLUSTERREQ
        '
        Me.grpCLUSTERREQ.Controls.Add(Me.optimize)
        Me.grpCLUSTERREQ.Controls.Add(Me.pnlIF)
        Me.grpCLUSTERREQ.Controls.Add(Me.lblEMAUNITS)
        Me.grpCLUSTERREQ.Controls.Add(Me.txtEMAVAL)
        Me.grpCLUSTERREQ.Controls.Add(Me.cboEMAMOD)
        Me.grpCLUSTERREQ.Controls.Add(Me.lblEMA)
        Me.grpCLUSTERREQ.Location = New System.Drawing.Point(7, 146)
        Me.grpCLUSTERREQ.Name = "grpCLUSTERREQ"
        Me.grpCLUSTERREQ.Size = New System.Drawing.Size(338, 105)
        Me.grpCLUSTERREQ.TabIndex = 1
        Me.grpCLUSTERREQ.TabStop = False
        Me.grpCLUSTERREQ.Text = "Cluster requirements"
        '
        'optimize
        '
        Me.optimize.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.optimize.Location = New System.Drawing.Point(6, 77)
        Me.optimize.Name = "optimize"
        Me.optimize.Size = New System.Drawing.Size(329, 23)
        Me.optimize.TabIndex = 23
        Me.optimize.Text = "Optimize"
        Me.optimize.UseVisualStyleBackColor = True
        '
        'pnlIF
        '
        Me.pnlIF.Controls.Add(Me.lblEIFUNITS)
        Me.pnlIF.Controls.Add(Me.txtEIFVAL)
        Me.pnlIF.Controls.Add(Me.lblEIF)
        Me.pnlIF.Controls.Add(Me.cboEIFMOD)
        Me.pnlIF.Location = New System.Drawing.Point(6, 45)
        Me.pnlIF.Name = "pnlIF"
        Me.pnlIF.Size = New System.Drawing.Size(330, 26)
        Me.pnlIF.TabIndex = 5
        '
        'lblEIFUNITS
        '
        Me.lblEIFUNITS.AutoSize = True
        Me.lblEIFUNITS.Location = New System.Drawing.Point(300, 6)
        Me.lblEIFUNITS.Name = "lblEIFUNITS"
        Me.lblEIFUNITS.Size = New System.Drawing.Size(29, 13)
        Me.lblEIFUNITS.TabIndex = 10
        Me.lblEIFUNITS.Text = "units"
        '
        'txtEIFVAL
        '
        Me.txtEIFVAL.Location = New System.Drawing.Point(196, 3)
        Me.txtEIFVAL.Name = "txtEIFVAL"
        Me.txtEIFVAL.Size = New System.Drawing.Size(100, 20)
        Me.txtEIFVAL.TabIndex = 3
        Me.txtEIFVAL.Text = "1.4"
        Me.txtEIFVAL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblEIF
        '
        Me.lblEIF.AutoSize = True
        Me.lblEIF.Location = New System.Drawing.Point(9, 5)
        Me.lblEIF.Name = "lblEIF"
        Me.lblEIF.Size = New System.Drawing.Size(88, 13)
        Me.lblEIF.TabIndex = 5
        Me.lblEIF.Text = "Inverse flattening"
        '
        'cboEIFMOD
        '
        Me.cboEIFMOD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboEIFMOD.FormattingEnabled = True
        Me.cboEIFMOD.Items.AddRange(New Object() {"<=", ">="})
        Me.cboEIFMOD.Location = New System.Drawing.Point(153, 2)
        Me.cboEIFMOD.Name = "cboEIFMOD"
        Me.cboEIFMOD.Size = New System.Drawing.Size(35, 21)
        Me.cboEIFMOD.TabIndex = 2
        '
        'lblEMAUNITS
        '
        Me.lblEMAUNITS.AutoSize = True
        Me.lblEMAUNITS.Location = New System.Drawing.Point(306, 22)
        Me.lblEMAUNITS.Name = "lblEMAUNITS"
        Me.lblEMAUNITS.Size = New System.Drawing.Size(18, 13)
        Me.lblEMAUNITS.TabIndex = 9
        Me.lblEMAUNITS.Text = "m."
        '
        'txtEMAVAL
        '
        Me.txtEMAVAL.Location = New System.Drawing.Point(202, 19)
        Me.txtEMAVAL.Name = "txtEMAVAL"
        Me.txtEMAVAL.Size = New System.Drawing.Size(100, 20)
        Me.txtEMAVAL.TabIndex = 1
        Me.txtEMAVAL.Text = "20000"
        Me.txtEMAVAL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cboEMAMOD
        '
        Me.cboEMAMOD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboEMAMOD.FormattingEnabled = True
        Me.cboEMAMOD.Items.AddRange(New Object() {"<=", ">="})
        Me.cboEMAMOD.Location = New System.Drawing.Point(159, 19)
        Me.cboEMAMOD.Name = "cboEMAMOD"
        Me.cboEMAMOD.Size = New System.Drawing.Size(35, 21)
        Me.cboEMAMOD.TabIndex = 0
        '
        'lblEMA
        '
        Me.lblEMA.AutoSize = True
        Me.lblEMA.Location = New System.Drawing.Point(15, 22)
        Me.lblEMA.Name = "lblEMA"
        Me.lblEMA.Size = New System.Drawing.Size(71, 13)
        Me.lblEMA.TabIndex = 3
        Me.lblEMA.Text = "Cluster length"
        '
        'cboLAYER
        '
        Me.cboLAYER.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboLAYER.FormattingEnabled = True
        Me.cboLAYER.Location = New System.Drawing.Point(7, 22)
        Me.cboLAYER.Name = "cboLAYER"
        Me.cboLAYER.Size = New System.Drawing.Size(335, 21)
        Me.cboLAYER.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.DataGridView1)
        Me.GroupBox1.Controls.Add(Me.remove_layer)
        Me.GroupBox1.Controls.Add(Me.AddData)
        Me.GroupBox1.Location = New System.Drawing.Point(4, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(341, 137)
        Me.GroupBox1.TabIndex = 10
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Input Data"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToResizeColumns = False
        Me.DataGridView1.AllowUserToResizeRows = False
        Me.DataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.DataGridView1.BackgroundColor = System.Drawing.SystemColors.ControlLightLight
        Me.DataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.RaisedVertical
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.inputds})
        Me.DataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.DataGridView1.GridColor = System.Drawing.Color.Black
        Me.DataGridView1.Location = New System.Drawing.Point(3, 46)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.Size = New System.Drawing.Size(306, 85)
        Me.DataGridView1.TabIndex = 5
        '
        'inputds
        '
        Me.inputds.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.inputds.FillWeight = 300.0!
        Me.inputds.HeaderText = "Source"
        Me.inputds.Name = "inputds"
        Me.inputds.ReadOnly = True
        '
        'remove_layer
        '
        Me.remove_layer.Image = CType(resources.GetObject("remove_layer.Image"), System.Drawing.Image)
        Me.remove_layer.Location = New System.Drawing.Point(315, 75)
        Me.remove_layer.Name = "remove_layer"
        Me.remove_layer.Size = New System.Drawing.Size(23, 23)
        Me.remove_layer.TabIndex = 1
        Me.remove_layer.UseVisualStyleBackColor = True
        '
        'AddData
        '
        Me.AddData.Image = CType(resources.GetObject("AddData.Image"), System.Drawing.Image)
        Me.AddData.Location = New System.Drawing.Point(315, 46)
        Me.AddData.Name = "AddData"
        Me.AddData.Size = New System.Drawing.Size(23, 23)
        Me.AddData.TabIndex = 0
        Me.AddData.UseVisualStyleBackColor = True
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
        Me.btnSHHELP.Location = New System.Drawing.Point(263, 516)
        Me.btnSHHELP.Name = "btnSHHELP"
        Me.btnSHHELP.Size = New System.Drawing.Size(84, 23)
        Me.btnSHHELP.TabIndex = 3
        Me.btnSHHELP.Text = "Show Help >>"
        Me.btnSHHELP.UseVisualStyleBackColor = True
        '
        'btnOK
        '
        Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnOK.Location = New System.Drawing.Point(101, 516)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 1
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'btnCANCEL
        '
        Me.btnCANCEL.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnCANCEL.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCANCEL.Location = New System.Drawing.Point(182, 516)
        Me.btnCANCEL.Name = "btnCANCEL"
        Me.btnCANCEL.Size = New System.Drawing.Size(75, 23)
        Me.btnCANCEL.TabIndex = 2
        Me.btnCANCEL.Text = "Close"
        Me.btnCANCEL.UseVisualStyleBackColor = True
        '
        'frm_trajecttool
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCANCEL
        Me.ClientSize = New System.Drawing.Size(356, 541)
        Me.Controls.Add(Me.btnSHHELP)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.btnCANCEL)
        Me.Controls.Add(Me.splcHELP)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(372, 579)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(372, 545)
        Me.Name = "frm_trajecttool"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Trajectory Tool"
        Me.splcHELP.Panel1.ResumeLayout(False)
        Me.splcHELP.Panel2.ResumeLayout(False)
        Me.splcHELP.ResumeLayout(False)
        Me.log_grp.ResumeLayout(False)
        Me.log_grp.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.grpOUT.ResumeLayout(False)
        Me.grpOUT.PerformLayout()
        Me.grpCOREFF.ResumeLayout(False)
        Me.grpCOREFF.PerformLayout()
        Me.grpTRAJDIST.ResumeLayout(False)
        Me.grpTRAJDIST.PerformLayout()
        Me.grpCLUSTERREQ.ResumeLayout(False)
        Me.grpCLUSTERREQ.PerformLayout()
        Me.pnlIF.ResumeLayout(False)
        Me.pnlIF.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlMARGIN2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents splcHELP As System.Windows.Forms.SplitContainer
    Friend WithEvents grpOUT As System.Windows.Forms.GroupBox
    Friend WithEvents txtOUT As System.Windows.Forms.TextBox
    Friend WithEvents grpCOREFF As System.Windows.Forms.GroupBox
    Friend WithEvents chkCEEJV As System.Windows.Forms.CheckBox
    Friend WithEvents txtCEEJVVAL As System.Windows.Forms.TextBox
    Friend WithEvents lblCEEJVUNITS As System.Windows.Forms.Label
    Friend WithEvents grpTRAJDIST As System.Windows.Forms.GroupBox
    Friend WithEvents radTDDIST As System.Windows.Forms.RadioButton
    Friend WithEvents radTDDEG As System.Windows.Forms.RadioButton
    Friend WithEvents lblTDDEGUNITS As System.Windows.Forms.Label
    Friend WithEvents cboTDDEGVAL As System.Windows.Forms.ComboBox
    Friend WithEvents txtTDDISTVAL As System.Windows.Forms.TextBox
    Friend WithEvents lblTDDISTUNITS As System.Windows.Forms.Label
    Friend WithEvents grpCLUSTERREQ As System.Windows.Forms.GroupBox
    Friend WithEvents lblEIFUNITS As System.Windows.Forms.Label
    Friend WithEvents lblEMAUNITS As System.Windows.Forms.Label
    Friend WithEvents txtEIFVAL As System.Windows.Forms.TextBox
    Friend WithEvents txtEMAVAL As System.Windows.Forms.TextBox
    Friend WithEvents cboEIFMOD As System.Windows.Forms.ComboBox
    Friend WithEvents lblEIF As System.Windows.Forms.Label
    Friend WithEvents cboEMAMOD As System.Windows.Forms.ComboBox
    Friend WithEvents lblEMA As System.Windows.Forms.Label
    Friend WithEvents cboLAYER As System.Windows.Forms.ComboBox
    Friend WithEvents pnlMARGIN2 As System.Windows.Forms.Panel
    Friend WithEvents rtxtHELP_CNT As System.Windows.Forms.RichTextBox
    Friend WithEvents pnlMARGIN1 As System.Windows.Forms.Panel
    Friend WithEvents btnSHHELP As System.Windows.Forms.Button
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnCANCEL As System.Windows.Forms.Button
    Friend WithEvents txtCEPROTP As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblCEPROTP As System.Windows.Forms.Label
    Friend WithEvents pnlIF As System.Windows.Forms.Panel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents AddData As System.Windows.Forms.Button
    Friend WithEvents inputds As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents remove_layer As System.Windows.Forms.Button
    Friend WithEvents log_grp As System.Windows.Forms.GroupBox
    Friend WithEvents LogSaveDialog As System.Windows.Forms.Button
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents LogFileName As System.Windows.Forms.TextBox
    Friend WithEvents optimize As System.Windows.Forms.Button
End Class
