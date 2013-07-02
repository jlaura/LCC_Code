Imports ESRI.ArcGIS.Editor
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.DataSourcesGDB
Imports System.Windows.Forms
Imports System.Drawing
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.Geometry

Public Class frm_trajecttool

    Private p_inGeometry As String = "line"
#Region "Form Button and Validation"
    Private Sub Frm_TrajectoryAnalysis_Load(ByVal sender As System.Object, _
                                        ByVal e As System.EventArgs) _
                                        Handles MyBase.Load

        Dim pEditor As IEditor = My.ArcMap.Editor

        'Make sure you are NOT in edit mode
        If pEditor.EditState = esriEditState.esriStateEditing Then
            MsgBox("You must be out of an edit session to continue." _
                    , MsgBoxStyle.Exclamation, "Edit Session in Progress")
            Me.btnCANCEL.PerformClick()
        End If

        'If not selected, set default modifiers
        If cboEMAMOD.SelectedIndex = -1 Then
            cboEMAMOD.SelectedIndex = 1
        End If
        If cboEIFMOD.SelectedIndex = -1 Then
            cboEIFMOD.SelectedIndex = 0
        End If

        'Select 180 degree trajectory distance
        cboTDDEGVAL.SelectedIndex = 1

        'Update help panel
        HELP_Form()

    End Sub

    Private Sub cboLAYER_DropDown(ByVal sender As Object, _
                              ByVal e As System.EventArgs) _
                              Handles cboLAYER.DropDown

        Dim pMxDoc As IMxDocument = My.ArcMap.Document

        'Populate layers dropdown
        Try

            'Clear cmbbox contents
            cboLAYER.Items.Clear()

            'Filter only for feature layers
            Dim pUID As New UID
            pUID.Value = "{E156D7E5-22AF-11D3-9F99-00C04F6BC78E}"
            Dim pELayer As IEnumLayer = pMxDoc.FocusMap.Layers(pUID, True)

            'Add layer names of active data frame to cmbbox
            Dim pLayer As ILayer = pELayer.Next
            While Not pLayer Is Nothing
                Dim pFLayer As IFeatureLayer = CType(pLayer, IFeatureLayer)
                'Add only polyline layers
                If pFLayer.FeatureClass.ShapeType = _
                ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolyline Then
                    cboLAYER.Items.Add(pLayer.Name)
                End If
                pLayer = pELayer.Next
            End While

        Catch ex As Exception
            MsgBox("No polyline layers available.", _
                   MsgBoxStyle.Exclamation, "No Polyline Layers")
            Exit Sub
        End Try

    End Sub

    Private Sub cboLAYER_DropDownClosed(ByVal sender As Object, _
                                  ByVal e As System.EventArgs) _
                                  Handles cboLAYER.DropDownClosed

        If cboLAYER.SelectedIndex = -1 Then
            m_sTAFLayer = Nothing
        End If

    End Sub

    Private Sub cboLAYER_SelectedIndexChanged(ByVal sender As System.Object, _
                                              ByVal e As System.EventArgs) _
                                              Handles cboLAYER.SelectedIndexChanged


        If Not cboLAYER.SelectedIndex = -1 Then
            Dim pFLayer As IFeatureLayer = GetFLayerByName(cboLAYER.Text)
            Dim pUID As New UID
            Dim pFClass As IFeatureClass = pFLayer.FeatureClass
            Dim iIFlat As Integer = pFClass.Fields.FindField("iflat")
            Dim iMajAxis As Integer = pFClass.Fields.FindField("majaxis")
            Dim iFFX As Integer = pFClass.Fields.FindField("ffx")
            Dim iFFY As Integer = pFClass.Fields.FindField("ffy")
            Dim iFTX As Integer = pFClass.Fields.FindField("ftx")
            Dim iFTY As Integer = pFClass.Fields.FindField("fty")
            Dim iTFX As Integer = pFClass.Fields.FindField("tfx")
            Dim iTFY As Integer = pFClass.Fields.FindField("tfy")
            Dim iTTX As Integer = pFClass.Fields.FindField("ttx")
            Dim iTTY As Integer = pFClass.Fields.FindField("tty")

            If Not iFFX = -1 And Not iFFY = -1 And Not iFTX = -1 And Not iFTY = -1 And _
               Not iTFX = -1 And Not iTFY = -1 And Not iTTX = -1 And Not iTTY = -1 And _
               Not iIFlat = -1 And Not iMajAxis = -1 Then
                p_inGeometry = "ellipse"
                pnlIF.Enabled = True
            ElseIf Not iIFlat = -1 And Not iMajAxis = -1 Then
                p_inGeometry = "line"
                pnlIF.Enabled = True
            Else
                p_inGeometry = "line no fields"
                pnlIF.Enabled = False
            End If

            m_sTAFLayer = cboLAYER.Text
        Else
            m_sTAFLayer = Nothing
        End If

    End Sub

    Private Sub RadButtons_TRAJDIST_Upd(ByVal sender As Object, _
                                       ByVal e As System.EventArgs) _
                                       Handles radTDDIST.Click, _
                                       radTDDEG.Click

        'Enable/Disable controls
        If radTDDIST.Checked = True Then
            txtTDDISTVAL.Enabled = True
            cboTDDEGVAL.Enabled = False
        Else
            cboTDDEGVAL.Enabled = True
            txtTDDISTVAL.Enabled = False
        End If

        'Update help panel 
        HELP_TrajDist()

    End Sub

    Private Sub General_EL_TA_Upd(ByVal sender As Object, _
                               ByVal e As System.EventArgs) _
                               Handles txtEMAVAL.Leave, _
                               txtEIFVAL.Leave, txtTDDISTVAL.Leave, _
                               txtCEEJVVAL.Leave, _
                               cboTDDEGVAL.SelectedIndexChanged, _
                               txtCEPROTP.Leave

        Try
            If CInt(txtEMAVAL.Text) > 0 Then txtEMAVAL.Text = _
                                    CInt(txtEMAVAL.Text).ToString
        Catch ex As Exception
            MsgBox("Please enter a 'Major axis length' value greater than 0.", _
                   MsgBoxStyle.Exclamation, _
                   "Invalid Parameter")
            txtEMAVAL.Text = "20000"
        End Try

        If pnlIF.Enabled Then
            Try
                If CDbl(txtEIFVAL.Text) > 0 Then txtEIFVAL.Text = txtEIFVAL.Text
            Catch ex As Exception
                MsgBox("Please enter an 'Inverse flattening' value greater than 0.", _
                       MsgBoxStyle.Exclamation, _
                       "Invalid Parameter")
                txtEIFVAL.Text = "1.4"
            End Try
        End If

        If radTDDIST.Checked Then
            Try
                If CInt(txtTDDISTVAL.Text) > 0 Then txtTDDISTVAL.Text = _
                                       CInt(txtTDDISTVAL.Text).ToString
            Catch ex As Exception
                MsgBox("Please enter a 'Trajectory distance' value greater than 0.", _
                       MsgBoxStyle.Exclamation, _
                       "Invalid Parameter")
                txtTDDISTVAL.Text = "500000"
            End Try
        End If

        If chkCEEJV.CheckState = CheckState.Checked Then
            Try
                If CInt(txtCEEJVVAL.Text) > 0 Then txtCEEJVVAL.Text = _
                                       CInt(txtCEEJVVAL.Text).ToString
            Catch ex As Exception
                MsgBox("Please enter an 'Average ejecta velocity' value greater than 0.", _
                       MsgBoxStyle.Exclamation, _
                       "Invalid Parameter")
                txtCEEJVVAL.Text = "4000"
            End Try
            Try
                If CDbl(txtCEPROTP.Text) > 0 Then txtCEPROTP.Text = CDbl(txtCEPROTP.Text)
            Catch ex As Exception
                MsgBox("Please enter a 'Planet rotation period' value grater than 0.", _
                       MsgBoxStyle.Exclamation, _
                       "Invalid Parameter")
                txtCEPROTP.Text = "88642.6632"
            End Try
        End If

    End Sub

    Private Sub chkCEEJV_CheckedChanged(ByVal sender As System.Object, _
                                        ByVal e As System.EventArgs) _
                                        Handles chkCEEJV.CheckedChanged

        If chkCEEJV.CheckState = CheckState.Checked Then
            txtCEEJVVAL.Enabled = True
            txtCEPROTP.Enabled = True
        Else
            txtCEEJVVAL.Enabled = False
            txtCEPROTP.Enabled = False
        End If

        'Update help panel
        HELP_Coriolis()

    End Sub

    Private Sub LogSaveDialog_ClickTraj(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LogSaveDialog.Click
        Dim saveFileDialog1 As New SaveFileDialog()

        saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"
        saveFileDialog1.FilterIndex = 2
        saveFileDialog1.RestoreDirectory = True

        If saveFileDialog1.ShowDialog() = DialogResult.OK Then
            LogFileName.Text = saveFileDialog1.FileName
        End If
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, _
                            ByVal e As System.EventArgs) _
                            Handles btnOK.Click

        FormErrorHandler()

    End Sub

    Private Sub FormErrorHandler()

        Dim pFLayer As IFeatureLayer = GetFLayerByName(m_sTAFLayer)

        'Make sure an input layer was selected and assigned to an object
        If cboLAYER.SelectedIndex = -1 Or pFLayer Is Nothing Then
            MsgBox("Please select an 'Input polyline layer'.", MsgBoxStyle.Exclamation, _
                   "Missing Parameter")
            Return
        End If

        Dim pFClass As IFeatureClass = pFLayer.FeatureClass
        Dim pDataset As IDataset = pFClass
        Dim pWrkspc2 As IWorkspace2 = DirectCast(pDataset.Workspace, IWorkspace2)

        'Major axis length'
        If CInt(txtEMAVAL.Text) <= 0 Then
            MsgBox("Please enter a 'Major axis length' " & _
                   "value greater than 0." _
                   , MsgBoxStyle.Exclamation, "Invalid Parameter")
            Return
        End If

        'Inverse flattening'
        If pnlIF.Enabled Then
            If IsNumeric(txtEIFVAL.Text) = False Then
                MsgBox("Please enter a numeric 'Inverse flattening' " & _
                       "value to continue." _
                       , MsgBoxStyle.Exclamation, "Invalid Parameter")
                Return
            ElseIf CDbl(txtEIFVAL.Text) <= 0 Then
                MsgBox("Please enter an 'Inverse flattening' " & _
                       "value greater than 0." _
                       , MsgBoxStyle.Exclamation, "Invalid Parameter")
                Return
            End If
        End If

        'Trajectory distance'
        If radTDDIST.Checked Then
            If CInt(txtTDDISTVAL.Text) <= 0 Then
                MsgBox("Please enter a 'Trajectory distance' " & _
                       "value greater than 0." _
                       , MsgBoxStyle.Exclamation, "Invalid Parameter")
                Return
            End If
        End If

        'Average ejecta velocity'
        If chkCEEJV.CheckState = CheckState.Checked Then
            If CInt(txtCEEJVVAL.Text) <= 0 Then
                MsgBox("Please enter an 'Average ejecta velocity' " & _
                       "value greater than 0." _
                       , MsgBoxStyle.Exclamation, "Invalid Parameter")
                Return
            End If
            If CDbl(txtCEPROTP.Text) <= 0 Then
                MsgBox("Please enter a 'Planet rotation period' " & _
                       "value greater than 0." _
                       , MsgBoxStyle.Exclamation, "Invalid Parameter")
                Return
            End If
        End If

        'Check for invalid output name
        If ValidateString(txtOUT.Text, "Output layer name", pFLayer.Name) = False Then
            Return
        End If

        'Check if feature class already exists on the workspace
        If FeatureClassExists(pWrkspc2, txtOUT.Text) Then
            MsgBox("The output layer feature class already exists. " & _
                   "Please enter a different 'Output layer name'.", _
                   MsgBoxStyle.Exclamation, _
                   "Invalid Parameter")
            Return
        End If

        'If all errors are handled, load progress form
        LoadProgressForm()

    End Sub

    Private Sub btnSHHELP_Click(ByVal sender As System.Object, _
                                ByVal e As System.EventArgs) _
                                Handles btnSHHELP.Click

        If btnSHHELP.Text = "<< Hide Help" Then
            splcHELP.Panel2Collapsed = True
            Me.MaximumSize = New Size(Me.MinimumSize.Width, _
                                      Me.MaximumSize.Height)
            Me.Size = New Size(Me.MinimumSize.Width, Me.Size.Height)
            btnSHHELP.Text = "Show Help >>"
        Else
            Me.MaximumSize = New Size(900, 495)
            Me.Size = New Size(543, Me.Size.Height)
            splcHELP.Panel2Collapsed = False
            btnSHHELP.Text = "<< Hide Help"
        End If

    End Sub
#End Region

    Private Sub LoadProgressForm()

        'Hide the Trajectory Analysis form
        Me.Hide()

        Dim PARA As New TAPARAM
        PARA.sFLAYER = m_sTAFLayer
        PARA.sEMAMOD = cboEMAMOD.Text
        PARA.sEMAVAL = txtEMAVAL.Text
        PARA.sEIFMOD = cboEIFMOD.Text
        PARA.sEIFVAL = txtEIFVAL.Text
        PARA.bTDDEG = radTDDEG.Checked
        PARA.sTDDEGVAL = cboTDDEGVAL.Text
        PARA.bTDDIST = radTDDIST.Checked
        PARA.sTDDISTVAL = txtTDDISTVAL.Text
        PARA.bCEEJV = chkCEEJV.Checked
        PARA.sCEEJVVAL = txtCEEJVVAL.Text
        PARA.sCEPROTP = txtCEPROTP.Text
        PARA.sTAOUT = txtOUT.Text

        RunTrajectory(PARA)

        'Close and dispose of form
        m_trajectForm = Nothing
        Me.Close()
        Me.Dispose()

    End Sub

    Private Sub RunTrajectory(ByVal TAF As TAPARAM)

        Dim PRINTtxt As String = ""

        'Check to see if we are working on a single layer, not added to the layer list, or to one or more layers in the
        ' layer list
        Dim featurelayerlist As New List(Of String)
        Dim featuredict As New Dictionary(Of Object, String)
        If DataGridView1.RowCount = 0 Then
            featurelayerlist.Add(cboLAYER.Text)
        Else
            For row As Integer = 0 To DataGridView1.Rows.Count - 1
                If Not DataGridView1.Rows(row).Cells(0).Value = Nothing Then
                    featurelayerlist.Add(DataGridView1.Rows(row).Cells(0).Value)
                End If
            Next
        End If

        'Grab the map document
        Dim pMxDoc As IMxDocument = My.ArcMap.Document
        Dim pFLayer As IFeatureLayer = Nothing
        Dim pFClass As IFeatureClass = Nothing
        Dim pSpatRef As ISpatialReference = Nothing
        Dim pGCS As IGeographicCoordinateSystem = Nothing
        Dim initialGCS As String
        Dim totalRecordCounter As Integer = 0

        'Iterate through the layers, get a count of the features for the tracker and confirm that the spatial reference is valid.
        For i As Integer = 0 To featurelayerlist.Count - 1
            pFLayer = GetFLayerByName(featurelayerlist(i))
            pFClass = pFLayer.FeatureClass
            pSpatRef = GetFLayerSpatRef(pFLayer)
            pGCS = GetGCS(pSpatRef)

            If i = 0 Then initialGCS = pGCS.Name
            If Not pGCS.Name.Equals(initialGCS) Then
                MsgBox("Spatial References do not match for one or more input files.", MsgBoxStyle.Exclamation, "Spatial Reference Error")
            End If
            totalRecordCounter = totalRecordCounter + pFClass.FeatureCount(Nothing)
        Next

        'We assume that all the datasets are in the same workspace.  This is only used to create the output later in the tool.
        Dim pDataset As IDataset = pFClass
        Dim pFDataset As IFeatureDataset = pFClass.FeatureDataset
        Dim pWrkspc2 As IWorkspace2 = DirectCast(pDataset.Workspace, IWorkspace2)

        '>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        ' Create a CancelTracker
        Dim pTrkCan As ITrackCancel = New CancelTracker

        ' Create the ProgressDialog. This automatically displays the dialog
        Dim pProDlgFact As IProgressDialogFactory = New ProgressDialogFactory
        Dim pProDlg As IProgressDialog2 = pProDlgFact.Create(pTrkCan, My.ArcMap.Application.hWnd)

        ' Set the properties of the ProgressDialog
        pProDlg.CancelEnabled = True
        pProDlg.Title = "Trajectory Tool"
        pProDlg.Animation = esriProgressAnimationTypes.esriProgressSpiral

        ' Set the properties of the Step Progressor
        Dim pStepPro As IStepProgressor = pProDlg
        pStepPro.MinRange = 0
        pStepPro.MaxRange = pFClass.FeatureCount(Nothing)
        pStepPro.StepValue = 1
        pStepPro.Message = "Progress:"
        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

        'Assign program start date and time
        Dim sSDate As String = Now.Month.ToString & "/" & Now.Day.ToString & _
                                "/" & Now.Year.ToString
        Dim sSTime As String = Now.Hour.ToString & ":" & Now.Minute.ToString & _
                                ":" & ((CDbl(Now.Second + (Now.Millisecond / _
                                                            1000))).ToString)

        'SUMMARY PRINT: Progress header
        PRINTtxt += SumProgramHeader("Trajectory Tool", _
                                     sSDate, sSTime)
        Dim layertxt As String
        For layer As Integer = 0 To featurelayerlist.Count - 1
            If layer = 0 Then
                layertxt += featurelayerlist(layer)
            Else
                layertxt += ", " & featurelayerlist(layer)
            End If

        Next
        'Print parameters
        PRINTtxt += vbCrLf
        PRINTtxt += vbCrLf & " INPUT PARAMETERS"
        PRINTtxt += vbCrLf & " ----------------"
        PRINTtxt += vbCrLf & " Input polyline layer(s): " & layertxt
        PRINTtxt += vbCrLf & " Cluster distribution filters:"
        PRINTtxt += vbCrLf & vbTab & " Cluster length: " & TAF.sEMAMOD & " " & TAF.sEMAVAL & " m."
        Dim invFlatString As String = "N/A"
        If pnlIF.Enabled Then invFlatString = TAF.sEIFMOD & " " & TAF.sEIFVAL & " units"
        PRINTtxt += vbCrLf & vbTab & " Inverse flattening: " & invFlatString
        Dim trajDistString As String = TAF.sTDDEGVAL & " degrees"
        If TAF.bTDDIST Then trajDistString = TAF.sTDDISTVAL & " m."
        PRINTtxt += vbCrLf & " Trajectory distance: " & trajDistString
        PRINTtxt += vbCrLf & " Coriolis effect:"
        Dim avgHVelString As String = "N/A"
        Dim pRotPString As String = "N/A"
        If TAF.bCEEJV Then
            avgHVelString = TAF.sCEEJVVAL & " m/s"
            pRotPString = TAF.sCEPROTP & " s."
        End If
        PRINTtxt += vbCrLf & vbTab & " Avg. horizontal velocity: " & avgHVelString
        PRINTtxt += vbCrLf & vbTab & " Planet rotation period: " & pRotPString
        PRINTtxt += vbCrLf & " Output layer name: " & TAF.sTAOUT
        PRINTtxt += vbCrLf & vbCrLf

        'PROGRESS UPDATE: 
        pProDlg.Description = "Extracting field data..."
        PRINTtxt += vbCrLf & " [Extracting field data...]"

        Dim pFCursor1 As IFeatureCursor = Nothing 'pFClass.Search(Nothing, False)
        Dim pFeature1 As IFeature = Nothing ' pFCursor1.NextFeature

        Dim ArInDDs As New List(Of ClusterDD)

        'Reset the tracker
        pTrkCan.Reset()

        For i As Integer = 0 To featurelayerlist.Count - 1
            pFLayer = GetFLayerByName(featurelayerlist(i))
            pFClass = pFLayer.FeatureClass
            pFCursor1 = pFClass.Search(Nothing, False)
            pFeature1 = pFCursor1.NextFeature
            Dim ParentFeature As String = featurelayerlist(i) ' To tracl which featurelayer seeded this trajectory

            'Determine the geometry type for the input.
            Dim iIFlat As Integer = pFClass.Fields.FindField("iflat")
            Dim iMajAxis As Integer = pFClass.Fields.FindField("majaxis")
            Dim iFFX As Integer = pFClass.Fields.FindField("ffx")
            Dim iFFY As Integer = pFClass.Fields.FindField("ffy")
            Dim iFTX As Integer = pFClass.Fields.FindField("ftx")
            Dim iFTY As Integer = pFClass.Fields.FindField("fty")
            Dim iTFX As Integer = pFClass.Fields.FindField("tfx")
            Dim iTFY As Integer = pFClass.Fields.FindField("tfy")
            Dim iTTX As Integer = pFClass.Fields.FindField("ttx")
            Dim iTTY As Integer = pFClass.Fields.FindField("tty")

            If Not iFFX = -1 And Not iFFY = -1 And Not iFTX = -1 And Not iFTY = -1 And _
               Not iTFX = -1 And Not iTFY = -1 And Not iTTX = -1 And Not iTTY = -1 And _
               Not iIFlat = -1 And Not iMajAxis = -1 Then
                p_inGeometry = "ellipse"
                pnlIF.Enabled = True
            ElseIf Not iIFlat = -1 And Not iMajAxis = -1 Then
                p_inGeometry = "line"
                pnlIF.Enabled = True
            Else
                p_inGeometry = "line no fields"
                pnlIF.Enabled = False
            End If

            'Add the featureclass and the geometry type to the featuredict
            featuredict(pFClass) = p_inGeometry

            Select Case p_inGeometry
                Case "line"
                    While Not pFeature1 Is Nothing
                        Dim iCID As Integer = pFeature1.Value(pFeature1.Fields.FindField("cid"))
                        Dim dIFlat As Double = pFeature1.Value(pFeature1.Fields.FindField("iflat"))

                        Dim dMajAxis As Double = pFeature1.Value(pFeature1.Fields.FindField("majaxis"))
                        'Get the major axis line end-point segments for trajectory azimuth computations
                        Dim pLSPointColl As IPointCollection = TryCast(pFeature1.ShapeCopy, IPointCollection)
                        Dim pFP As IPoint = pLSPointColl.Point(0)
                        Dim pTP As IPoint = pLSPointColl.Point(pLSPointColl.PointCount - 1)
                        Dim pFPp As IPoint = Nothing
                        Dim pTPp As IPoint = Nothing
                        If pLSPointColl.PointCount > 2 Then
                            pFPp = pLSPointColl.Point(1)
                            pTPp = pLSPointColl.Point(pLSPointColl.PointCount - 2)
                        Else
                            pFPp = pTP
                            pTPp = pFP
                        End If
                        ArInDDs.Add(New ClusterDD(Nothing, iCID, Nothing, dIFlat, dMajAxis, _
                                                  pFPp.X, pFPp.Y, pFP.X, pFP.Y, _
                                                  pTPp.X, pTPp.Y, pTP.X, pTP.Y, ParentFeature))
                        pFeature1 = pFCursor1.NextFeature
                        If Not pTrkCan.Continue Then
                            'SUMMARY PRINT: End program as interrupted
                            PRINTtxt += SumEndProgram("INTERRUPTED: Process interrupted by user.", _
                                                      sSDate, sSTime)
                            'Destroy the progress dialog
                            ProgressDialogDispose(pProDlg, pStepPro, pTrkCan, pProDlgFact)
                            If LogFileName.Text <> "" Then
                                SaveLog(LogFileName.Text, PRINTtxt)
                            End If
                            Return
                        End If
                    End While
                Case "line no fields"
                    pTrkCan.Reset()
                    While Not pFeature1 Is Nothing
                        'Get the line end-point segments for trajectory azimuth computations
                        Dim pLSPointColl As IPointCollection = TryCast(pFeature1.ShapeCopy, IPointCollection)
                        Dim pFP As IPoint = pLSPointColl.Point(0)
                        Dim pTP As IPoint = pLSPointColl.Point(pLSPointColl.PointCount - 1)
                        Dim pFPp As IPoint = Nothing
                        Dim pTPp As IPoint = Nothing
                        If pLSPointColl.PointCount > 2 Then
                            pFPp = pLSPointColl.Point(1)
                            pTPp = pLSPointColl.Point(pLSPointColl.PointCount - 2)
                        Else
                            pFPp = pTP
                            pTPp = pFP
                        End If
                        'Get the geodesic lenght of the line in m
                        Dim pPolycurveGeodeticMaj As IPolycurveGeodetic = pFeature1.ShapeCopy
                        Dim lengthMaj As Double = pPolycurveGeodeticMaj.LengthGeodetic( _
                                                  esriGeodeticType.esriGeodeticTypeGeodesic, Nothing)
                        ArInDDs.Add(New ClusterDD(Nothing, pFeature1.OID, Nothing, Nothing, lengthMaj, _
                                                  pFPp.X, pFPp.Y, pFP.X, pFP.Y, _
                                                  pTPp.X, pTPp.Y, pTP.X, pTP.Y, ParentFeature))
                        pFeature1 = pFCursor1.NextFeature
                        If Not pTrkCan.Continue Then
                            'SUMMARY PRINT: End program as interrupted
                            PRINTtxt += SumEndProgram("INTERRUPTED: Process interrupted by user.", _
                                                      sSDate, sSTime)
                            'Destroy the progress dialog
                            ProgressDialogDispose(pProDlg, pStepPro, pTrkCan, pProDlgFact)
                            If LogFileName.Text <> "" Then
                                SaveLog(LogFileName.Text, PRINTtxt)
                            End If
                            Return
                        End If
                    End While
                Case "ellipse"
                    pTrkCan.Reset()
                    While Not pFeature1 Is Nothing
                        Dim iCID As Integer = pFeature1.Value(pFeature1.Fields.FindField("cid"))
                        Dim dIFlat As Double = pFeature1.Value(pFeature1.Fields.FindField("iflat"))
                        Dim dMajAxis As Double = pFeature1.Value(pFeature1.Fields.FindField("majaxis"))
                        Dim dFFX As Integer = pFeature1.Value(pFeature1.Fields.FindField("ffx"))
                        Dim dFFY As Integer = pFeature1.Value(pFeature1.Fields.FindField("ffy"))
                        Dim dFTX As Integer = pFeature1.Value(pFeature1.Fields.FindField("ftx"))
                        Dim dFTY As Integer = pFeature1.Value(pFeature1.Fields.FindField("fty"))
                        Dim dTFX As Integer = pFeature1.Value(pFeature1.Fields.FindField("tfx"))
                        Dim dTFY As Integer = pFeature1.Value(pFeature1.Fields.FindField("tfy"))
                        Dim dTTX As Integer = pFeature1.Value(pFeature1.Fields.FindField("ttx"))
                        Dim dTTY As Integer = pFeature1.Value(pFeature1.Fields.FindField("tty"))

                        ArInDDs.Add(New ClusterDD(Nothing, iCID, Nothing, dIFlat, dMajAxis, _
                                                  dFFX, dFFY, dFTX, dFTY, _
                                                  dTFX, dTFY, dTTX, dTTY, ParentFeature))
                        pFeature1 = pFCursor1.NextFeature
                        If Not pTrkCan.Continue Then
                            'SUMMARY PRINT: End program as interrupted
                            PRINTtxt += SumEndProgram("INTERRUPTED: Process interrupted by user.", _
                                                      sSDate, sSTime)
                            'Destroy the progress dialog
                            ProgressDialogDispose(pProDlg, pStepPro, pTrkCan, pProDlgFact)
                            If LogFileName.Text <> "" Then
                                SaveLog(LogFileName.Text, PRINTtxt)
                            End If
                            Return
                        End If
                    End While
            End Select
        Next


        'Get total number of features
        Dim population = ArInDDs.Count

        If p_inGeometry = "line" Or p_inGeometry = "ellipse" Then
            'Filter by Major Axis and Inverse Flattening
            p_TrajQuery = New TRJQUERY
            p_TrajQuery.MajAxisL = TAF.sEMAVAL
            p_TrajQuery.MajAxisLMod = TAF.sEMAMOD
            p_TrajQuery.IFlat = TAF.sEIFVAL
            p_TrajQuery.IFlatMod = TAF.sEIFMOD
            ArInDDs.RemoveAll(AddressOf FilterOutCDDsByMajAxis)
            ArInDDs.RemoveAll(AddressOf FilterOutCDDsByIFlat)

            'CHECK: At least one feature for trajectory computation
            If ArInDDs.Count <= 0 Then
                MsgBox("There are no features with major-axis " & TAF.sEMAMOD & " " & _
                        TAF.sEMAVAL & " and inverse flattening " & _
                        TAF.sEIFMOD & " " & TAF.sEIFVAL, MsgBoxStyle.Information, _
                        "Trajectory Tool Interrupted")
                'SUMMARY PRINT: End program as interrupted
                PRINTtxt += SumEndProgram("INTERRUPTED: No features to compute trajectories.", _
                                          sSDate, sSTime)
                'DONE:
                ProgressDialogDispose(pProDlg, pStepPro, pTrkCan, pProDlgFact)
                If LogFileName.Text <> "" Then
                    SaveLog(LogFileName.Text, PRINTtxt)
                End If
                Return
            End If
        ElseIf p_inGeometry = "line no fields" Then
            'Filter by Major Axis
            p_TrajQuery = New TRJQUERY
            p_TrajQuery.MajAxisL = TAF.sEMAVAL
            p_TrajQuery.MajAxisLMod = TAF.sEMAMOD
            ArInDDs.RemoveAll(AddressOf FilterOutCDDsByMajAxis)

            'CHECK: At least one feature for trajectory computation
            If ArInDDs.Count <= 0 Then
                MsgBox("There are no features with length " & TAF.sEMAMOD & " " & _
                        TAF.sEMAVAL, MsgBoxStyle.Information, _
                        "Trajectory Tool Interrupted")
                'SUMMARY PRINT: End program as interrupted
                PRINTtxt += SumEndProgram("INTERRUPTED: No features to compute trajectories.", _
                                          sSDate, sSTime)
                'DONE:
                ProgressDialogDispose(pProDlg, pStepPro, pTrkCan, pProDlgFact)
                If LogFileName.Text <> "" Then
                    SaveLog(LogFileName.Text, PRINTtxt)
                End If
                Return
            End If
        End If

        'Get sample size
        Dim sample = ArInDDs.Count

        Dim f100 As String = Space(3) & "|" & "{0,-20}" & "{1}" & "{2,16:F4}" & "|"
        Dim f300 As String = Space(3) & "|" & StrDup(37, "-") & "|"
        Dim f400 As String = Space(3) & "|" & StrDup(37, "_") & "|"
        Dim f500 As String = Space(3) & StrDup(39, "_")

        PRINTtxt += vbCrLf & vbCrLf & _
           Space(3) & "STATS: Trajectories" & vbCrLf & _
           (String.Format(f500, "_")) & vbCrLf & _
           (String.Format(f100, "Total Population", "=", population)) & vbCrLf & _
           (String.Format(f100, "Sample", "=", sample)) & vbCrLf & _
           (String.Format(f100, "Percentage of Total", "=", sample / population * 100)) & vbCrLf & _
           (String.Format(f400, "_")) & vbCrLf

        '>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        pStepPro.MaxRange = ArInDDs.Count
        pStepPro.StepValue = 1
        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

        'Set the trajectory distance in meters
        Dim dTrajDist As Double = Nothing
        If TAF.bTDDEG = True Then
            Select Case TAF.sTDDEGVAL
                Case "90"
                    dTrajDist = GetGeodeticDist(0, 0, 0.1, 45, _
                                              pGCS.Datum.Spheroid.SemiMajorAxis, _
                                              pGCS.Datum.Spheroid.SemiMinorAxis)
                Case "180"
                    dTrajDist = GetGeodeticDist(0, 0, 0.1, 90, _
                                              pGCS.Datum.Spheroid.SemiMajorAxis, _
                                              pGCS.Datum.Spheroid.SemiMinorAxis)
                Case "270"
                    dTrajDist = GetGeodeticDist(0, 0, 0.1, 135, _
                                              pGCS.Datum.Spheroid.SemiMajorAxis, _
                                              pGCS.Datum.Spheroid.SemiMinorAxis)
                Case "360"
                    dTrajDist = (GetGeodeticDist(0, 0, 0.1, 180, _
                                               pGCS.Datum.Spheroid.SemiMajorAxis, _
                                               pGCS.Datum.Spheroid.SemiMinorAxis)) * 3
            End Select
        ElseIf TAF.bTDDIST = True Then
            Dim Dist175 As Double = GetGeodeticDist(0, 0, 0.1, 175, _
                                                pGCS.Datum.Spheroid.SemiMajorAxis, _
                                                pGCS.Datum.Spheroid.SemiMinorAxis)
            If (CDbl(TAF.sTDDISTVAL) / 2) > Dist175 Then
                dTrajDist = Dist175
            Else
                dTrajDist = CDbl(TAF.sTDDISTVAL) / 2
            End If
        End If

        'PROGRESS UPDATE: 
        pProDlg.Description = "Generating trajectories..."
        PRINTtxt += vbCrLf & " [Generating trajectories...]"

        Dim ArTrajs As New List(Of TrajectoryLine)

        pTrkCan.Reset()
        For Each inDD In ArInDDs
            Dim pFf As IPoint = New PointClass()
            Dim pFt As IPoint = New PointClass()
            Dim pTf As IPoint = New PointClass()
            Dim pTt As IPoint = New PointClass()
            pFf.PutCoords(inDD.FromFX, inDD.FromFY)
            pFt.PutCoords(inDD.FromTX, inDD.FromTY)
            pTf.PutCoords(inDD.ToFX, inDD.ToFY)
            pTt.PutCoords(inDD.ToTX, inDD.ToTY)
            pFf.SpatialReference = pSpatRef
            pFt.SpatialReference = pSpatRef
            pTf.SpatialReference = pSpatRef
            pTt.SpatialReference = pSpatRef
            pFf.Project(pGCS)
            pFt.Project(pGCS)
            pTf.Project(pGCS)
            pTt.Project(pGCS)
            pFf.SpatialReference = pGCS
            pFt.SpatialReference = pGCS
            pTf.SpatialReference = pGCS
            pTt.SpatialReference = pGCS
            Dim dFromGBear As Double = GetFinalGeodeticBearing(pFf.Y, pFf.X, pFt.Y, pFt.X, _
                                                               pGCS.Datum.Spheroid.SemiMajorAxis, _
                                                               pGCS.Datum.Spheroid.SemiMinorAxis)
            Dim dToGBear As Double = GetFinalGeodeticBearing(pTf.Y, pTf.X, pTt.Y, pTt.X, _
                                                             pGCS.Datum.Spheroid.SemiMajorAxis, _
                                                             pGCS.Datum.Spheroid.SemiMinorAxis)
            Dim FromNewLatLon As Lat2BLon2B = GetNewLatLon(pFt.Y, pFt.X, dFromGBear, dTrajDist, _
                                                           pGCS.Datum.Spheroid.SemiMajorAxis, _
                                                           pGCS.Datum.Spheroid.SemiMinorAxis)
            Dim ToNewLatLon As Lat2BLon2B = GetNewLatLon(pTt.Y, pTt.X, dToGBear, dTrajDist, _
                                                         pGCS.Datum.Spheroid.SemiMajorAxis, _
                                                         pGCS.Datum.Spheroid.SemiMinorAxis)
            Dim pFromNewPt As IPoint = New PointClass()
            Dim pToNewPt As IPoint = New PointClass()
            pFromNewPt.PutCoords(FromNewLatLon.Lon2B, FromNewLatLon.Lat2B)
            pToNewPt.PutCoords(ToNewLatLon.Lon2B, ToNewLatLon.Lat2B)
            pFromNewPt.SpatialReference = pGCS
            pToNewPt.SpatialReference = pGCS
            Dim pFromPLine As IPolyline = New PolylineClass()
            Dim pToPLine As IPolyline = New PolylineClass()
            pFromPLine.FromPoint = pFt
            pFromPLine.ToPoint = pFromNewPt
            pToPLine.FromPoint = pTt
            pToPLine.ToPoint = pToNewPt
            pFromPLine.SpatialReference = pGCS
            pToPLine.SpatialReference = pGCS
            Dim pFromGeoLine As IPolycurveGeodetic = pFromPLine
            Dim pToGeoLine As IPolycurveGeodetic = pToPLine
            pFromGeoLine.DensifyGeodetic(esriGeodeticType.esriGeodeticTypeGeodesic, _
                                            Nothing, _
                                            esriCurveDensifyMethod.esriCurveDensifyByDeviation, _
                                            1D)
            pToGeoLine.DensifyGeodetic(esriGeodeticType.esriGeodeticTypeGeodesic, _
                                            Nothing, _
                                            esriCurveDensifyMethod.esriCurveDensifyByDeviation, _
                                            1D)
            Dim pFromPointColl As IPointCollection = TryCast(pFromPLine, IPointCollection)
            Dim pToPointColl As IPointCollection = TryCast(pToPLine, IPointCollection)
            Dim pFinalPColl As IGeometryCollection = New PolylineClass()
            Dim pFinalPLine As IPolyline = DirectCast(pFinalPColl, IPolyline)

            'Shift longitudes if Coriolis selected
            If TAF.bCEEJV Then
                Dim ejVel As Double = CType(TAF.sCEEJVVAL, Double)
                Dim pRotP As Double = CType(TAF.sCEPROTP, Double)
                'Shift From section
                Dim pNewFromPColl As IPointCollection = New PolylineClass()
                For p As Integer = 0 To pFromPointColl.PointCount - 1
                    Dim pPt As IPoint = pFromPointColl.Point(p)
                    Dim dist As Double = GetGeodeticDist(pFt.Y, pFt.X, pPt.Y, pPt.X, _
                                                         pGCS.Datum.Spheroid.SemiMajorAxis, _
                                                         pGCS.Datum.Spheroid.SemiMinorAxis)
                    Dim newLon As Double = GetCoriolisNewLon(pPt.X, ejVel, dist, pRotP)
                    Dim pNewPt As IPoint = New PointClass()
                    pNewPt.PutCoords(newLon, pPt.Y)
                    pNewFromPColl.AddPoint(pNewPt)
                Next
                'Shift To section
                Dim pNewToPColl As IPointCollection = New PolylineClass()
                For p As Integer = 0 To pToPointColl.PointCount - 1
                    Dim pPt As IPoint = pToPointColl.Point(p)
                    Dim dist As Double = GetGeodeticDist(pTt.Y, pTt.X, pPt.Y, pPt.X, _
                                                         pGCS.Datum.Spheroid.SemiMajorAxis, _
                                                         pGCS.Datum.Spheroid.SemiMinorAxis)
                    Dim newLon As Double = GetCoriolisNewLon(pPt.X, ejVel, dist, pRotP)
                    Dim pNewPt As IPoint = New PointClass()
                    pNewPt.PutCoords(newLon, pPt.Y)
                    pNewToPColl.AddPoint(pNewPt)
                Next
                pFinalPColl.AddGeometryCollection(pNewFromPColl)
                pFinalPColl.AddGeometryCollection(pNewToPColl)
            Else
                pFinalPColl.AddGeometryCollection(pFromPointColl)
                pFinalPColl.AddGeometryCollection(pToPointColl)
            End If

            pFinalPLine.SpatialReference = pGCS
            pFinalPLine.Project(pSpatRef)
            ArTrajs.Add(New TrajectoryLine(pFinalPLine, inDD.CID, inDD.ParentFeature, inDD.IFlat))
            If Not pTrkCan.Continue Then
                'SUMMARY PRINT: End program as interrupted
                PRINTtxt += SumEndProgram("INTERRUPTED: Process interrupted by user.", _
                                          sSDate, sSTime)
                'Destroy the progress dialog
                ProgressDialogDispose(pProDlg, pStepPro, pTrkCan, pProDlgFact)
                If LogFileName.Text <> "" Then
                    SaveLog(LogFileName.Text, PRINTtxt)
                End If
                Return
            End If
        Next

        'PROGRESS UPDATE: 
        pProDlg.Description = "Creating polyline feature class..."
        PRINTtxt += vbCrLf & " [Creating polyline feature class...]"

        Dim pTRReqFields As IFields = GetTrajectoryReqFields(True)
        Dim pTRFLayer As IFeatureLayer = New FeatureLayerClass()
        pTRFLayer.FeatureClass = CreateFeatureClass(pWrkspc2, pFDataset, TAF.sTAOUT, _
                                                    pTRReqFields, _
                                                    esriGeometryType.esriGeometryPolyline, _
                                                    pSpatRef)
        pTRFLayer.Name = pTRFLayer.FeatureClass.AliasName
        Dim pTRFClass As IFeatureClass = pTRFLayer.FeatureClass

        'PROGRESS UPDATE: 
        pProDlg.Description = "Storing polyline features..."
        PRINTtxt += vbCrLf & " [Storing polyline features...]"

        'Begin edit session and operation
        Dim pEditor As IEditor = My.ArcMap.Editor
        pEditor.StartEditing(pWrkspc2)
        pEditor.StartOperation()

        Dim pTRFCursor As IFeatureCursor = pTRFClass.Insert(True)

        pTrkCan.Reset()
        For Each trFeature In ArTrajs
            'Create the feature buffer
            Dim pTRFBuffer As IFeatureBuffer = pTRFClass.CreateFeatureBuffer
            pTRFBuffer.Shape = trFeature.Shape
            pTRFBuffer.Value(pTRFBuffer.Fields.FindField("cid")) = trFeature.CID
            pTRFBuffer.Value(pTRFBuffer.Fields.FindField("ParentFeat")) = trFeature.ParentFeature
            pTRFBuffer.Value(pTRFBuffer.Fields.FindField("iflat")) = trFeature.iflat

            pTRFCursor.InsertFeature(pTRFBuffer)
            If Not pTrkCan.Continue Then
                'SUMMARY PRINT: End program as interrupted
                PRINTtxt += SumEndProgram("INTERRUPTED: Process interrupted by user.", _
                                          sSDate, sSTime)
                'Destroy the progress dialog
                ProgressDialogDispose(pProDlg, pStepPro, pTrkCan, pProDlgFact)
                If LogFileName.Text <> "" Then
                    SaveLog(LogFileName.Text, PRINTtxt)
                End If
                Return
            End If
        Next

        'Stop edit operation and session, save edits
        pEditor.StopOperation("Directional Distribution features")
        StopEditSession(True)

        'Add the new layer to the map
        Dim pTRLayer As ILayer = pTRFLayer
        pMxDoc.ActiveView.FocusMap.AddLayer(pTRLayer)

        'SUMMARY PRINT: End program as complete
        PRINTtxt += SumEndProgram("COMPLETE: Trajectory process complete.", _
                                  sSDate, sSTime)

        'Destroy the progress dialog
        ProgressDialogDispose(pProDlg, pStepPro, pTrkCan, pProDlgFact)
        If LogFileName.Text <> "" Then
            SaveLog(LogFileName.Text, PRINTtxt)
        End If

    End Sub

    Public Structure TRJQUERY
        Public MajAxisL As Double
        Public IFlat As Double
        Public MajAxisLMod As String
        Public IFlatMod As String
    End Structure
    Private p_TrajQuery As TRJQUERY
    Public Function FilterOutCDDsByMajAxis(ByVal cluster As ClusterDD) As Boolean
        Select Case p_TrajQuery.MajAxisLMod
            Case ">="
                If cluster.MajAxis >= p_TrajQuery.MajAxisL Then
                    Return False
                Else
                    Return True
                End If
            Case "<="
                If cluster.MajAxis <= p_TrajQuery.MajAxisL Then
                    Return False
                Else
                    Return True
                End If
        End Select
    End Function
    Public Function FilterOutCDDsByIFlat(ByVal cluster As ClusterDD) As Boolean
        Select Case p_TrajQuery.IFlatMod
            Case ">="
                If cluster.IFlat >= p_TrajQuery.IFlat Then
                    Return False
                Else
                    Return True
                End If
            Case "<="
                If cluster.IFlat <= p_TrajQuery.IFlat Then
                    Return False
                Else
                    Return True
                End If
        End Select
    End Function

#Region "*** HELP CONTENT DISPLAY DYNAMICS ****************************************************"
#End Region

#Region "***** FORM *********"
#End Region
    Private Sub Frm_TrajectoryAnalysis_Click(ByVal sender As Object, _
                                          ByVal e As System.EventArgs) _
                                          Handles Me.Click
        HELP_Form()
    End Sub

    Private Sub splcHELP_Click(ByVal sender As Object, _
                               ByVal e As System.EventArgs) _
                               Handles splcHELP.Click
        HELP_Form()
    End Sub

#Region "***** LAYER ********"
#End Region
    Private Sub lblLAYER_Click(ByVal sender As System.Object, _
                              ByVal e As System.EventArgs)

        HELP_Layer()
    End Sub

    Private Sub cboLAYER_Click(ByVal sender As Object, _
                               ByVal e As System.EventArgs) _
                               Handles cboLAYER.Click
        HELP_Layer()
    End Sub

#Region "***** ELLIPSE ******"
#End Region
    Private Sub grpCLUSTERREQ_Click(ByVal sender As Object, _
                             ByVal e As System.EventArgs) _
                             Handles grpCLUSTERREQ.Click
        HELP_ClusterReq()
    End Sub

    Private Sub grpCLUSTERREQ_Enter(ByVal sender As System.Object, _
                                 ByVal e As System.EventArgs) _
                                 Handles grpCLUSTERREQ.Enter
        HELP_ClusterReq()
    End Sub

    Private Sub lblEMA_Click(ByVal sender As System.Object, _
                             ByVal e As System.EventArgs) _
                         Handles lblEMA.Click
        HELP_ClusterReq()
    End Sub

    Private Sub lblEIF_Click(ByVal sender As System.Object, _
                             ByVal e As System.EventArgs) _
                         Handles lblEIF.Click
        HELP_ClusterReq()
    End Sub

#Region "***** TRAJECTORY DISTANCE ***"
#End Region
    Private Sub grpTRAJDIST_Click(ByVal sender As Object, _
                              ByVal e As System.EventArgs) _
                              Handles grpTRAJDIST.Click
        HELP_TrajDist()
    End Sub

    Private Sub grpTRAJDIST_Enter(ByVal sender As System.Object, _
                                  ByVal e As System.EventArgs) _
                                  Handles grpTRAJDIST.Enter
        HELP_TrajDist()
    End Sub

#Region "***** CORIOLIS *****"
#End Region
    Private Sub grpCOREFF_Click(ByVal sender As Object, _
                            ByVal e As System.EventArgs) _
                            Handles grpCOREFF.Click
        HELP_Coriolis()
    End Sub

    Private Sub grpCOREFF_Enter(ByVal sender As System.Object, _
                                ByVal e As System.EventArgs) _
                                Handles grpCOREFF.Enter
        HELP_Coriolis()
    End Sub

    Private Sub lblCEPROTP_Click(ByVal sender As System.Object, _
                             ByVal e As System.EventArgs) _
                         Handles lblCEPROTP.Click
        HELP_Coriolis()
    End Sub

#Region "***** OUTPUT *******"
#End Region
    Private Sub grpTRJAOUT_Click(ByVal sender As Object, _
                             ByVal e As System.EventArgs) _
                             Handles grpOUT.Click
        HELP_Out()
    End Sub

    Private Sub grpTRJAOUT_Enter(ByVal sender As System.Object, _
                                 ByVal e As System.EventArgs) _
                                 Handles grpOUT.Enter
        HELP_Out()
    End Sub


#Region "*** HELP PANEL UPDATE CONTENT ********************************************************"
#End Region

    Private Sub HELP_Form()

        'Update help panel
        Dim strText As String = _
          "Generates ejecta trajectories from the endpoints of linear or elliptical geometries " & _
          "representing clusters. Note: for elliptical " & _
          "geometries to work, they need to be generated with the 'Directional " & _
          "Distribution Tool' included in this toolset." & _
          vbCrLf & vbCrLf & _
          "Requirement: The input layer must be projected in meters."

        HELPCntUpdate("Trajectory Tool", strText)

    End Sub

    Private Sub HELP_Layer()

        'Update help panel
        Dim strText As String = _
          "The polyline layer to generate trajectories for." & _
          vbCrLf & vbCrLf & _
          "Requirement: The input layer must be projected in meters."

        HELPCntUpdate("Input Features", strText)

    End Sub

    Private Sub HELP_ClusterReq()

        'Update help panel
        Dim strText As String = _
            "Filter clusters by the following:" & _
                    vbCrLf & vbCrLf & _
            "-Cluster length:" & _
                    vbCrLf & _
            "Only cluster lines with a length value inside this threshold " & _
            "will be used to generate the trajectories." & _
            vbCrLf & vbCrLf & _
            "-Inverse flattening:" & _
            vbCrLf & _
            "Only clusters with an inverse flattening value inside this threshold " & _
            "will be used to generate the trajectories."

        HELPCntUpdate("Cluster distribution filter", strText)

    End Sub

    Private Sub HELP_TrajDist()

        'Update help panel
        Dim strText As String = _
            "The total length of each trajectory " & _
            "(half of the total in each direction)."

        HELPCntUpdate("Trajectory distance", strText)

    End Sub

    Private Sub HELP_Coriolis()

        'Update help panel
        Dim strText As String = _
            "Applies a shift to the longitudinal component of each vertex in the trajectory " & _
            "based on the planet's angular velocity due its rotation " & _
            "and the distance of the trajectory away from the cluster." & _
             vbCrLf & vbCrLf & _
             "-Avg. horizontal velocity: " & _
             vbCrLf & _
            "The average horizontal velocity component of the ejecta.." & _
            vbCrLf & vbCrLf & _
            "-Planet rotation period:" & _
             vbCrLf & _
            "The time (in seconds) that it takes the planetary body to complete one " & _
            "revolution around its axis of rotation relative to the background stars."

        HELPCntUpdate("Coriolis effect", strText)

    End Sub

    Private Sub HELP_Out()

        'Update help panel
        Dim strText As String = _
            "The output polyline layer name."

        HELPCntUpdate("Output layer name", strText)

    End Sub

    Private Sub HELPCntUpdate(ByVal Title As String, ByVal Text As String)

        rtxtHELP_CNT.Clear()

        rtxtHELP_CNT.AppendText("   " & vbCrLf & Title)
        rtxtHELP_CNT.Find(Title, RichTextBoxFinds.MatchCase)
        rtxtHELP_CNT.SelectionFont = New Font("Arial", 12, FontStyle.Bold)
        rtxtHELP_CNT.SelectionColor = Color.Black
        rtxtHELP_CNT.DeselectAll()
        rtxtHELP_CNT.AppendText(vbCrLf & vbCrLf)

        rtxtHELP_CNT.AppendText(Text)

        rtxtHELP_CNT.AppendText(vbCrLf & vbCrLf & vbCrLf)
        rtxtHELP_CNT.Find("   ", RichTextBoxFinds.MatchCase)
        rtxtHELP_CNT.ScrollToCaret()
        rtxtHELP_CNT.DeselectAll()

        rtxtHELP_CNT.Refresh()

    End Sub

    Private Sub AddData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddData.Click
        'Add the selected polyline shapefile to the data viewer.
        For row As Integer = 0 To DataGridView1.Rows.Count - 1
            If DataGridView1.Rows(row).Cells(0).Value = cboLAYER.Text Then Return
        Next
        DataGridView1.Rows.Add(cboLAYER.Text)
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles remove_layer.Click
        If Not DataGridView1.CurrentRow.IsNewRow Then
            DataGridView1.Rows.Remove(DataGridView1.CurrentRow)
        End If
    End Sub

    Private Sub LogSaveDialog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
End Class