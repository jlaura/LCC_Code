﻿Imports ESRI.ArcGIS.Editor
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.esriSystem
Imports System.Windows.Forms
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.DataSourcesGDB
Imports System.Math
Imports System.Drawing
Imports ESRI.ArcGIS.DataManagementTools

Public Class frm_clustertool

    Private Sub Frm_ClusterAnalysis_Load(ByVal sender As System.Object, _
                                         ByVal e As System.EventArgs) _
                                         Handles MyBase.Load

        Dim pEditor As IEditor3 = My.ArcMap.Editor

        'Make sure you are NOT in edit mode
        If pEditor.EditState = esriEditState.esriStateEditing Then
            MsgBox("You must be out of an edit session to continue." _
                    , MsgBoxStyle.Exclamation, "Edit Session in Progress")
            Me.btnCANCEL.PerformClick()
        End If

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
                'Add only point layers
                If pFLayer.FeatureClass.ShapeType = _
                ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPoint Then
                    cboLAYER.Items.Add(pLayer.Name)
                End If
                pLayer = pELayer.Next
            End While

        Catch ex As Exception
            MsgBox("No point layers available.", _
                   MsgBoxStyle.Exclamation, "No Point Layers")
            Exit Sub
        End Try

    End Sub

    Private Sub cboLAYER_DropDownClosed(ByVal sender As Object, _
                                        ByVal e As System.EventArgs) _
                                        Handles cboLAYER.DropDownClosed

        If cboLAYER.SelectedIndex = -1 Then
            m_sCAFLayer = Nothing
        End If

    End Sub

    Private Sub cboLAYER_SelectedIndexChanged _
                                (ByVal sender As System.Object, _
                                 ByVal e As System.EventArgs) _
                                 Handles cboLAYER.SelectedIndexChanged

        'If an input layer is selected
        If Not cboLAYER.SelectedIndex = -1 Then
            'Assign layer name to global variable
            m_sCAFLayer = cboLAYER.Text
        Else
            m_sCAFLayer = Nothing
        End If

    End Sub

    Private Sub TextBoxes_Update(ByVal sender As Object, _
                                 ByVal e As System.EventArgs) _
                                 Handles txtNQUERY.Leave, _
                                 txtCMSVAL.Leave, txtCMBFVAL.Leave, _
                                 txtCMBSVAL.Leave, radCMS.Click, _
                                 radCMBNND.Click, radCMBF.Click, _
                                 radCMBS.Click

        'Round the near 'Distance to closest point' value
        Try
            If CInt(txtNQUERY.Text) > 0 Then txtNQUERY.Text = _
                                    CInt(txtNQUERY.Text).ToString
        Catch ex As Exception
            MsgBox("Please enter a 'Threshold Distance'. Craters with no neighbors within this distance are ignored.", _
                   MsgBoxStyle.Exclamation, _
                   "Invalid Parameter")
            txtNQUERY.Text = "1500"
        End Try

        'Round the cluster method 'Same distance(m)' distance value
        Try
            If CInt(txtCMSVAL.Text) > 0 Then txtCMSVAL.Text = _
                                    CInt(txtCMSVAL.Text).ToString
        Catch ex As Exception
            MsgBox("Please enter a value greater than 0 for the selected clustering method.", _
                   MsgBoxStyle.Exclamation, _
                   "Invalid Parameter")
            txtCMSVAL.Text = "1700"
        End Try

        'Convert to double the cluster method 'Buffer options: Nearest
        'neighbor distance x' factor value
        Try
            If CDbl(txtCMBFVAL.Text) > 0 Then txtCMBFVAL.Text = _
                                    CDbl(txtCMBFVAL.Text).ToString
        Catch ex As Exception
            MsgBox("Please enter a value greater than 0 for the selected clustering method.", _
                   MsgBoxStyle.Exclamation, _
                   "Invalid Parameter")
            txtCMBFVAL.Text = "1.5"
        End Try

        'Round the cluster method 'Buffer options: Same distance(m)' 
        'distance value
        Try
            If CInt(txtCMBSVAL.Text) > 0 Then txtCMBSVAL.Text = _
                                    CInt(txtCMBSVAL.Text).ToString
        Catch ex As Exception
            MsgBox("Please enter a value greater than 0 for the selected clustering method.", _
                   MsgBoxStyle.Exclamation, _
                   "Invalid Parameter")
            txtCMBSVAL.Text = "1700"
        End Try

    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, _
                            ByVal e As System.EventArgs) _
                            Handles btnOK.Click
        'This method handles the OK OnClick call.
        'Check for errors in all field values
        FormErrorHandler()

    End Sub

    Private Sub FormErrorHandler()
        'On form submission this method is called to validate all the form fields.
        Dim pFLayer As IFeatureLayer = GetFLayerByName(m_sCAFLayer)

        'Make sure an input layer was selected and assigned to an object
        If cboLAYER.SelectedIndex = -1 Or pFLayer Is Nothing Then
            MsgBox("Please select an 'Input point layer'.", MsgBoxStyle.Exclamation, _
                   "Missing Parameter")
            Return
        End If

        Dim pFClass As IFeatureClass = pFLayer.FeatureClass
        Dim pDataset As IDataset = pFClass
        Dim pWrkspc2 As IWorkspace2 = DirectCast(pDataset.Workspace, IWorkspace2)

        'Query points by nearest distance' distance number required
        If CInt(txtNQUERY.Text) <= 0 Then
            MsgBox("Please enter a 'Nearest Neighbor distance filter' value greater than 0." _
                   , MsgBoxStyle.Exclamation, "Invalid Parameter")
            Return
        End If

        'Buffer options values check
        If radCMS.Checked = True Then
            If CInt(txtCMSVAL.Text) <= 0 Then
                MsgBox("Please enter a value greater than 0 for the selected clustering method.", _
                       MsgBoxStyle.Exclamation, _
                       "Invalid Parameter")
                Return
            End If
        ElseIf radCMBF.Checked = True Then
            If IsNumeric(txtCMBFVAL.Text) = False Then
                MsgBox("Please enter a numeric value for the selected clustering method.", _
                       MsgBoxStyle.Exclamation, _
                       "Invalid Parameter")
                Return
            ElseIf CDbl(txtCMBFVAL.Text) <= 0 Then
                MsgBox("Please enter a value greater than 0 for the selected clustering method.", _
                       MsgBoxStyle.Exclamation, _
                       "Invalid Parameter")
                Return
            End If
        ElseIf radCMBS.Checked Then
            If CInt(txtCMBSVAL.Text) <= 0 Then
                MsgBox("Please enter a value greater than 0 for the selected clustering method.", _
                       MsgBoxStyle.Exclamation, _
                       "Invalid Parameter")
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

    Private Sub btnSHHELP_Click(ByVal sender As System.Object,
                                ByVal e As System.EventArgs) _
                                Handles btnSHHELP.Click
        'This method shows and hides the help dialog boxes.
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

    Private Sub LoadProgressForm()
        'This method launches the progress form, packages the parameters from the form, and calls the RunClustering method to initiate processing.

        'Hide the Cluster Analysis form
        Me.Hide()

        'Define parameter bundle
        Dim PARA As New CAPARAM
        PARA.sFLAYER = m_sCAFLayer
        PARA.sNQUERY = txtNQUERY.Text
        PARA.bMEASPLAN = radMEASPLAN.Checked
        PARA.bMEASGEO = radMEASGEO.Checked
        PARA.bCMS = radCMS.Checked
        PARA.sCMSVAL = txtCMSVAL.Text
        PARA.bCMBNND = radCMBNND.Checked
        PARA.bCMBF = radCMBF.Checked
        PARA.sCMBFVAL = txtCMBFVAL.Text
        PARA.bCMBS = radCMBS.Checked
        PARA.sCMBSVAL = txtCMBSVAL.Text
        PARA.sOUT = txtOUT.Text

        'Run the cluster analysis program
        RunClustering(PARA)

        'Close and dispose of form
        m_clusterForm = Nothing
        Me.Close()
        Me.Dispose()

    End Sub

    Public Sub RunClustering(ByVal CAF As CAPARAM)
        'This is the algorithm to run the clustering analysis.

        Dim PRINTtxt As String = ""

        Dim pMxDoc As IMxDocument = My.ArcMap.Document 'This is an interface that allows access to the map document.  It is used to add the cluster layer to the ToC and update the ToC.
        Dim pMap As IMap = pMxDoc.FocusMap
        Dim pFlayer As IFeatureLayer2 = GetFLayerByName(CAF.sFLAYER) ' Updated to use V2 - GetFLayerByName access the public function in mod_public, which iterates over the ToC just like the Arc example.
        Dim pFClass As IFeatureClass = pFlayer.FeatureClass 'A property - the datasource for the layer
        Dim pDataset As IDataset = pFClass
        Dim pFDataset As IFeatureDataset = pFClass.FeatureDataset
        Dim pWrkspc2 As IWorkspace2 = DirectCast(pDataset.Workspace, IWorkspace2)
        Dim pSpatRef As ISpatialReference = GetFLayerSpatRef(pFlayer)
        Dim pGCS As IGeographicCoordinateSystem2 = GetGCS(pSpatRef) ' GetGCS lives in mod_public
        Dim dSemiMajAxis As Double = pGCS.Datum.Spheroid.SemiMajorAxis
        Dim dSemiMinAxis As Double = pGCS.Datum.Spheroid.SemiMinorAxis

        '>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        ' Create a CancelTracker
        Dim pTrkCan As ITrackCancel = New CancelTracker

        ' Create the ProgressDialog. This automatically displays the dialog
        Dim pProDlgFact As IProgressDialogFactory = New ProgressDialogFactory
        Dim pProDlg As IProgressDialog2 = pProDlgFact.Create(pTrkCan, My.ArcMap.Application.hWnd)

        ' Set the properties of the ProgressDialog
        pProDlg.CancelEnabled = True
        pProDlg.Title = "Cluster Tool"
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
        PRINTtxt += SumProgramHeader("Cluster Tool", _
                                     sSDate, sSTime)

        'Print parameters
        PRINTtxt += vbCrLf
        PRINTtxt += vbCrLf & " INPUT PARAMETERS"
        PRINTtxt += vbCrLf & " ----------------"
        PRINTtxt += vbCrLf & " Input point layer: " & CAF.sFLAYER
        PRINTtxt += vbCrLf & " Nearest Neighbor distance filter: <= " & CAF.sNQUERY & " m."
        Dim measName As String = "Planar"
        If CAF.bMEASGEO Then measName = "Geodetic"
        PRINTtxt += vbCrLf & " Measurement space: " & measName
        PRINTtxt += vbCrLf & " Cluster method:"
        If CAF.bCMS Then
            PRINTtxt += " Fixed distance: " & CAF.sCMSVAL & " m."
        ElseIf CAF.bCMBNND Then
            PRINTtxt += " Buffer option - Nearest Neighbor distance"
        ElseIf CAF.bCMBF Then
            PRINTtxt += " Buffer option - Nearest Neighbor distance x " & CAF.sCMBFVAL & " units."
        ElseIf CAF.bCMBS Then
            PRINTtxt += " Buffer option - Fixed distance: " & CAF.sCMBSVAL & " m."
        End If
        PRINTtxt += vbCrLf & " Output layer name: " & CAF.sOUT
        PRINTtxt += vbCrLf & vbCrLf


        'Check for the license type
        'Dim license = GetArcGISLicenseName()

        'Here the initial feature array is populated.  Each point in the input is copied to the output and the following fields are created:

        Dim pFCursor1 As IFeatureCursor = pFClass.Search(Nothing, False) 'The input layer is accessed viat pFClass, which is defined above via PFLayer, which is accessed via a global which stores the layer name.  From the input form.
        Dim pFeature1 As IFeature = pFCursor1.NextFeature

        '   AID =   Array ID (or the 'cnt' value in iteration below)
        '   OID =   Object ID
        '   X   =   The x coordinate value
        '   Y   =   The y coordinate value
        '   CID =   Cluster ID
        '   CNT =   Number of points in cluster.

        'Create an array with fields: AID,OID,X,Y,NDIST.
        Dim Ar(4, pFClass.FeatureCount(Nothing) - 1) As Double
        'Create an array with field: CID
        Dim Ar2(pFClass.FeatureCount(Nothing) - 1) As Double
        'Create an array with field: CNT
        Dim Ar3(pFClass.FeatureCount(Nothing) - 1) As Double

        'PROGRESS UPDATE: 
        pProDlg.Description = "Extracting OID, X and Y values..."
        'PRINTtxt += vbCrLf & " [Extracting OID, X and Y values...]"

        'Populate array with AID,OID,X,Y from all records in feature class.
        Dim cnt As Integer = 0
        While Not pFeature1 Is Nothing
            Ar(0, cnt) = cnt 'AID
            Ar(1, cnt) = pFeature1.OID 'OID
            Dim pPoint As IPoint = pFeature1.ShapeCopy
            If radMEASGEO.Checked Then ' This is, I believe for geodeditic calculations, it is getting the X,Y coords in GCS, even if the input is in projected space.
                pPoint.Project(pGCS)
            End If
            Ar(2, cnt) = pPoint.X 'X
            Ar(3, cnt) = pPoint.Y 'Y
            Ar2(cnt) = -1 'CID
            Ar3(cnt) = -1 'CNT
            cnt += 1
            pFeature1 = pFCursor1.NextFeature ' This is just like GDAL, we are stepping through the features.
            If Not pTrkCan.Continue Then
                'SUMMARY PRINT: End program as interrupted
                PRINTtxt += SumEndProgram("INTERRUPTED: Process interrupted by user.", _
                                          sSDate, sSTime)
                'Destroy the progress dialog
                ProgressDialogDispose(pProDlg, pStepPro, pTrkCan, pProDlgFact)
                SaveSummaryReport(CAF.sOUT, PRINTtxt)
                Return
            End If
        End While

        'PROGRESS UPDATE: 
        pProDlg.Description = "Calculating Nearest Neighbor distances..."
        'PRINTtxt += vbCrLf & " [Calculating Nearest Neighbor distances...]"

        'Populate array with NDIST for all records in feature class
        'TODO: This finds nearest twice, and can be 100% faster.
        Dim dFeature1(2, 0) As Double
        pTrkCan.Reset()
        For i As Integer = 0 To Ar.GetUpperBound(1) 'From row 0 to row n
            'Store the OID,X,Y of current feature
            dFeature1(0, 0) = Ar(1, i)
            dFeature1(1, 0) = Ar(2, i)
            dFeature1(2, 0) = Ar(3, i)
            'Assign the Nearest distance returned to the main table
            'Is this stepping over the array feature by feature for every input?  There must be a faster way to do this using a spatial index, unless the function is already spatially indexed.
            Ar(4, i) = FindNearest(Ar, dFeature1, dSemiMajAxis, dSemiMinAxis, CAF.bMEASPLAN) 'NDIST
            If Not pTrkCan.Continue Then
                'SUMMARY PRINT: End program as interrupted
                PRINTtxt += SumEndProgram("INTERRUPTED: Process interrupted by user.", _
                                          sSDate, sSTime)
                'Destroy the progress dialog
                ProgressDialogDispose(pProDlg, pStepPro, pTrkCan, pProDlgFact)
                SaveSummaryReport(CAF.sOUT, PRINTtxt)
                Return
            End If
        Next

        'Compute statistics on the clusters.
        PRINTtxt += CalcBufferStats(Ar, CAF.sNQUERY)

        '********************************************************************************************
        'If the the clustering method is 'Same distance'
        If CAF.bCMS = True Then

            'PROGRESS UPDATE: 
            pProDlg.Description = "Clustering calculation 'Same distance'..."
            'PRINTtxt += vbCrLf & " [Clustering calculation 'Same distance'...]"

            Dim dGroup(3, 0) As Double
            'Main feature iteration
            pTrkCan.Reset()

            'This is fixed distance or 
            For j As Integer = 0 To Ar.GetUpperBound(1)
                'If the main feature NDIST is less than or equal to the user
                'distance query
                If Ar(4, j) <= CDbl(CAF.sNQUERY) Then 'Ar(4,j) = Ar(Ndist, n), where n is the nth point
                    'Leave a single blank line in the group table
                    ReDim dGroup(3, 0)
                    'Add the record to the potential cluster group
                    dGroup(0, 0) = Ar(0, j) 'AID
                    dGroup(1, 0) = Ar(1, j) 'OID
                    dGroup(2, 0) = Ar(2, j) 'X
                    dGroup(3, 0) = Ar(3, j) 'Y
                    'Current feature iteration: GROUP construction of features within Near distance
                    For k As Integer = 0 To Ar.GetUpperBound(1) ' Again this is iterating over the entire array...
                        'If the OID of current feature does not equal OID of main feature
                        'and the distance threshold is that specified by user:
                        '1. If the OID of the main feature does not equal the OID the
                        '   current feature
                        '2. The distance between the main feature and the current feature
                        '   is less than or equal to the 'Same distance' value
                        If Not Ar(1, k) = Ar(1, j) And Ar(4, k) <= CDbl(CAF.sNQUERY) And _
                           GetDist(Ar(2, k), Ar(3, k), Ar(2, j), Ar(3, j), _
                                   dSemiMajAxis, dSemiMinAxis, CAF.bMEASPLAN) <= _
                           CDbl(CAF.sCMSVAL) Then
                            'Add a single row to the end of the group table
                            ReDim Preserve dGroup(3, dGroup.GetUpperBound(1) + 1)
                            'Add the current feature to the potential cluster gorup
                            dGroup(0, dGroup.GetUpperBound(1)) = Ar(0, k) 'AID
                            dGroup(1, dGroup.GetUpperBound(1)) = Ar(1, k) 'OID
                            dGroup(2, dGroup.GetUpperBound(1)) = Ar(2, k) 'X
                            dGroup(3, dGroup.GetUpperBound(1)) = Ar(3, k) 'Y
                        End If

                    Next
                    'If there are features in the group other than the main feature
                    'If there are only two features in group:
                    If dGroup.GetUpperBound(1) = 1 Then
                        'If the second feature has a CID:
                        If Ar2(dGroup(0, 1)) <> -1 Then
                            'Assign the second current feature CID to the group's main feature CID
                            Ar2(dGroup(0, 0)) = Ar2(dGroup(0, 1))
                        Else
                            'Assign the second current feature OID to the group's main feature CID
                            Ar2(dGroup(0, 0)) = dGroup(1, 1)
                        End If
                    ElseIf dGroup.GetUpperBound(1) > 1 Then
                        'If there are more than two features in the group:
                        Dim nRepeat As Integer = 0
                        Dim nRepeatOld As Integer = 0
                        'Sort array with field: CID for ordering
                        Dim Ar2Sorted(Ar2.GetUpperBound(0)) As Double
                        System.Array.Copy(Ar2, 0, Ar2Sorted, 0, Ar2.Length)
                        System.Array.Sort(Ar2Sorted)

                        'Iterate through the group starting with second feature
                        For m As Integer = 1 To dGroup.GetUpperBound(1)
                            'If the CID of the group feature is set:
                            If Ar2(dGroup(0, m)) <> -1 Then
                                'Find the number of occurrances of this CID
                                Dim n1stIndex, nLastIndex As Integer
                                n1stIndex = System.Array.IndexOf(Ar2Sorted, Ar2(dGroup(0, m))) + 1
                                nLastIndex = System.Array.LastIndexOf(Ar2Sorted, Ar2(dGroup(0, m))) + 1
                                nRepeatOld = nRepeat
                                nRepeat = (nLastIndex - n1stIndex) + 1
                                'Set the CID of the main feature from the group to the CID
                                'having the most occurrances in the group
                                If Not nRepeat = 0 And nRepeat > nRepeatOld Then
                                    'Assign the CID with most occurrances in the group
                                    'to the main feature
                                    Ar2(dGroup(0, 0)) = Ar2(dGroup(0, m))
                                End If
                            End If
                        Next
                        'If there were no CIDs in the group:
                        If Ar2(dGroup(0, 0)) = -1 Then
                            'Assign the second current feature OID to the group's main feature CID
                            'and the rest of the group's CIDs
                            Ar2(dGroup(0, 0)) = dGroup(1, 1)
                            For a As Integer = 0 To dGroup.GetUpperBound(1)
                                Ar2(dGroup(0, a)) = Ar2(dGroup(0, 0))
                            Next
                        Else
                            'If a CID was chosen as most repeated in the group:
                            'Find all the group feature CID occurrances in the main CID list
                            'and replace them with the most repeated CID (found above)
                            For a As Integer = 1 To dGroup.GetUpperBound(1)
                                For f As Integer = 0 To Ar2.GetUpperBound(0)
                                    If Not Ar2(dGroup(0, a)) = -1 And Ar2(f) = Ar2(dGroup(0, a)) Then
                                        'Replace the found CID with the most repeated CID
                                        Ar2(f) = Ar2(dGroup(0, 0))
                                    End If
                                Next
                            Next
                            'Replace all the CIDs of the group for the main CID
                            For a As Integer = 1 To dGroup.GetUpperBound(1)
                                Ar2(dGroup(0, a)) = Ar2(dGroup(0, 0))
                            Next
                        End If
                    End If
                End If
                If Not pTrkCan.Continue Then
                    'SUMMARY PRINT: End program as interrupted
                    PRINTtxt += SumEndProgram("INTERRUPTED: Process interrupted by user.", _
                                              sSDate, sSTime)
                    'Destroy the progress dialog
                    ProgressDialogDispose(pProDlg, pStepPro, pTrkCan, pProDlgFact)
                    SaveSummaryReport(CAF.sOUT, PRINTtxt)
                    Return
                End If
            Next
            '********************************************************************************************
            'If the clustering method is 'Buffer: Nearest neighbor distance'
        ElseIf CAF.bCMBNND = True Then

            'PROGRESS UPDATE: 
            pProDlg.Description = "Clustering calculation 'Buffer: Nearest neighbor distance'..."
            PRINTtxt += vbCrLf & " [Clustering calculation 'Buffer: Nearest neighbor distance'...]"

            Dim dGroup(3, 0) As Double
            'Main feature iteration
            pTrkCan.Reset()
            For j As Integer = 0 To Ar.GetUpperBound(1)
                'If the main feature NDIST is less than or equal to the user
                'distance query
                If Ar(4, j) <= CDbl(CAF.sNQUERY) Then
                    'Leave a single blank line in the group table
                    ReDim dGroup(3, 0)
                    'Add the record to the potential cluster group
                    dGroup(0, 0) = Ar(0, j) 'AID
                    dGroup(1, 0) = Ar(1, j) 'OID
                    dGroup(2, 0) = Ar(2, j) 'X
                    dGroup(3, 0) = Ar(3, j) 'Y
                    'Current feature iteration: GROUP construction of features within Near distance
                    For k As Integer = 0 To Ar.GetUpperBound(1)
                        '1. The OID of the main feature does not equal the OID the
                        '   current feature
                        '2. The distance between the main feature and the current feature
                        '   is less than or equal to the sum of each feature's nearest
                        '   neighbor distance value
                        If Not Ar(1, k) = Ar(1, j) And Ar(4, k) <= CDbl(CAF.sNQUERY) And _
                           GetDist(Ar(2, k), Ar(3, k), Ar(2, j), Ar(3, j), _
                                   dSemiMajAxis, dSemiMinAxis, CAF.bMEASPLAN) <= _
                           (Ar(4, k) + Ar(4, j)) Then
                            'Add a single row to the end of the group table
                            ReDim Preserve dGroup(3, dGroup.GetUpperBound(1) + 1)
                            'Add the current feature to the potential cluster gorup
                            dGroup(0, dGroup.GetUpperBound(1)) = Ar(0, k) 'AID
                            dGroup(1, dGroup.GetUpperBound(1)) = Ar(1, k) 'OID
                            dGroup(2, dGroup.GetUpperBound(1)) = Ar(2, k) 'X
                            dGroup(3, dGroup.GetUpperBound(1)) = Ar(3, k) 'Y
                        End If
                    Next
                    'If there are features in the group other than the main feature
                    'If there are only two features in group:
                    If dGroup.GetUpperBound(1) = 1 Then
                        'If the second feature has a CID:
                        If Ar2(dGroup(0, 1)) <> -1 Then
                            'Assign the second current feature CID to the group's main feature CID
                            Ar2(dGroup(0, 0)) = Ar2(dGroup(0, 1))
                        Else
                            'Assign the second current feature OID to the group's main feature CID
                            Ar2(dGroup(0, 0)) = dGroup(1, 1)
                        End If
                    ElseIf dGroup.GetUpperBound(1) > 1 Then
                        'If there are more than two features in the group:
                        Dim nRepeat As Integer = 0
                        Dim nRepeatOld As Integer = 0
                        'Sort array with field: CID for ordering
                        Dim Ar2Sorted(Ar2.GetUpperBound(0)) As Double
                        System.Array.Copy(Ar2, 0, Ar2Sorted, 0, Ar2.Length)
                        System.Array.Sort(Ar2Sorted)

                        'Iterate through the group starting with second feature
                        For m As Integer = 1 To dGroup.GetUpperBound(1)
                            'If the CID of the group feature is set:
                            If Ar2(dGroup(0, m)) <> -1 Then
                                'Find the number of occurrances of this CID
                                Dim n1stIndex, nLastIndex As Integer
                                n1stIndex = System.Array.IndexOf(Ar2Sorted, Ar2(dGroup(0, m))) + 1
                                nLastIndex = System.Array.LastIndexOf(Ar2Sorted, Ar2(dGroup(0, m))) + 1
                                nRepeatOld = nRepeat
                                nRepeat = (nLastIndex - n1stIndex) + 1
                                'Set the CID of the main feature from the group to the CID
                                'having the most occurrances in the group
                                If Not nRepeat = 0 And nRepeat > nRepeatOld Then
                                    'Assign the CID with most occurrances in the group
                                    'to the main feature
                                    Ar2(dGroup(0, 0)) = Ar2(dGroup(0, m))
                                End If
                            End If
                        Next
                        'If there were no CIDs in the group:
                        If Ar2(dGroup(0, 0)) = -1 Then
                            'Assign the second current feature OID to the group's main feature CID
                            'and the rest of the group's CIDs
                            Ar2(dGroup(0, 0)) = dGroup(1, 1)
                            For a As Integer = 0 To dGroup.GetUpperBound(1)
                                Ar2(dGroup(0, a)) = Ar2(dGroup(0, 0))
                            Next
                        Else
                            'If a CID was chosen as most repeated in the group:
                            'Find all the group feature CID occurrances in the main CID list
                            'and replace them with the most repeated CID (found above)
                            For a As Integer = 1 To dGroup.GetUpperBound(1)
                                For f As Integer = 0 To Ar2.GetUpperBound(0)
                                    If Not Ar2(dGroup(0, a)) = -1 And Ar2(f) = Ar2(dGroup(0, a)) Then
                                        'Replace the found CID with the most repeated CID
                                        Ar2(f) = Ar2(dGroup(0, 0))
                                    End If
                                Next
                            Next
                            'Replace all the CIDs of the group for the main CID
                            For a As Integer = 1 To dGroup.GetUpperBound(1)
                                Ar2(dGroup(0, a)) = Ar2(dGroup(0, 0))
                            Next
                        End If
                    End If
                End If
                If Not pTrkCan.Continue Then
                    'SUMMARY PRINT: End program as interrupted
                    PRINTtxt += SumEndProgram("INTERRUPTED: Process interrupted by user.", _
                                              sSDate, sSTime)
                    'Destroy the progress dialog
                    ProgressDialogDispose(pProDlg, pStepPro, pTrkCan, pProDlgFact)
                    SaveSummaryReport(CAF.sOUT, PRINTtxt)
                    Return
                End If
            Next
            '********************************************************************************************
            'If the clustering method is 'Buffer: NND x factor'
        ElseIf CAF.bCMBF = True Then

            'PROGRESS UPDATE: 
            pProDlg.Description = "Clustering calculation 'Buffer: NND x factor'..."
            PRINTtxt += vbCrLf & " [Clustering calculation 'Buffer: NND x factor'...]"

            Dim dGroup(3, 0) As Double
            'Main feature iteration
            pTrkCan.Reset()
            For j As Integer = 0 To Ar.GetUpperBound(1)
                'If the main feature NDIST is less than or equal to the user
                'distance query
                If Ar(4, j) <= CDbl(CAF.sNQUERY) Then
                    'Leave a single blank line in the group table
                    ReDim dGroup(3, 0)
                    'Add the record to the potential cluster group
                    dGroup(0, 0) = Ar(0, j) 'AID
                    dGroup(1, 0) = Ar(1, j) 'OID
                    dGroup(2, 0) = Ar(2, j) 'X
                    dGroup(3, 0) = Ar(3, j) 'Y
                    'Current feature iteration: GROUP construction of features within Near distance
                    For k As Integer = 0 To Ar.GetUpperBound(1)
                        '1. The OID of the main feature does not equal the OID the
                        '   current feature
                        '2. The current feature's nearest neighbor distance is less than or
                        '   equal to the 'Distance to closest point' value
                        '3. The distance between the main feature and the current feature
                        '   is less than or equal to the sum of: each feature's nearest
                        '   neighbor distance value time the factor value
                        '- OR -
                        '1. The OID of the main feature does not equal the OID the
                        '   current feature
                        '3. The distance between the main feature and the current feature
                        '   is less than or equal to the main feature's nearest neighbor 
                        '   distance value time the factor value
                        If (Not Ar(1, k) = Ar(1, j) And Ar(4, k) <= CDbl(CAF.sNQUERY) And _
                             GetDist(Ar(2, k), Ar(3, k), Ar(2, j), Ar(3, j), _
                                     dSemiMajAxis, dSemiMinAxis, CAF.bMEASPLAN) <= _
                             (Ar(4, k) * CDbl(CAF.sCMBFVAL)) + (Ar(4, j) * CDbl(CAF.sCMBFVAL))) Or _
                           (Not Ar(1, k) = Ar(1, j) And GetDist(Ar(2, k), Ar(3, k), Ar(2, j), Ar(3, j), _
                                                                dSemiMajAxis, dSemiMinAxis, CAF.bMEASPLAN) <= _
                            (Ar(4, j) * CDbl(CAF.sCMBFVAL))) Then
                            'Add a single row to the end of the group table
                            ReDim Preserve dGroup(3, dGroup.GetUpperBound(1) + 1)
                            'Add the current feature to the potential cluster gorup
                            dGroup(0, dGroup.GetUpperBound(1)) = Ar(0, k) 'AID
                            dGroup(1, dGroup.GetUpperBound(1)) = Ar(1, k) 'OID
                            dGroup(2, dGroup.GetUpperBound(1)) = Ar(2, k) 'X
                            dGroup(3, dGroup.GetUpperBound(1)) = Ar(3, k) 'Y
                        End If
                    Next

                    'If there are only two features in group:
                    If dGroup.GetUpperBound(1) = 1 Then
                        'If the second feature has a CID:
                        If Ar2(dGroup(0, 1)) <> -1 Then
                            'Assign the second current feature CID to the group's main feature CID
                            Ar2(dGroup(0, 0)) = Ar2(dGroup(0, 1))
                        Else
                            'Assign the second current feature OID to the group's main feature CID
                            Ar2(dGroup(0, 0)) = dGroup(1, 1)
                        End If
                    ElseIf dGroup.GetUpperBound(1) > 1 Then
                        'If there are more than two features in the group:
                        Dim nRepeat As Integer = 0
                        Dim nRepeatOld As Integer = 0
                        'Sort array with field: CID for ordering
                        Dim Ar2Sorted(Ar2.GetUpperBound(0)) As Double
                        System.Array.Copy(Ar2, 0, Ar2Sorted, 0, Ar2.Length)
                        System.Array.Sort(Ar2Sorted)

                        'Iterate through the group starting with second feature
                        For m As Integer = 1 To dGroup.GetUpperBound(1)
                            'If the CID of the group feature is set:
                            If Ar2(dGroup(0, m)) <> -1 Then
                                'Find the number of occurrances of this CID
                                Dim n1stIndex, nLastIndex As Integer
                                n1stIndex = System.Array.IndexOf(Ar2Sorted, Ar2(dGroup(0, m))) + 1
                                nLastIndex = System.Array.LastIndexOf(Ar2Sorted, Ar2(dGroup(0, m))) + 1
                                nRepeatOld = nRepeat
                                nRepeat = (nLastIndex - n1stIndex) + 1
                                'Set the CID of the main feature from the group to the CID
                                'having the most occurrances in the group
                                If Not nRepeat = 0 And nRepeat > nRepeatOld Then
                                    'Assign the CID with most occurrances in the group
                                    'to the main feature
                                    Ar2(dGroup(0, 0)) = Ar2(dGroup(0, m))
                                End If
                            End If
                        Next
                        'If there were no CIDs in the group:
                        If Ar2(dGroup(0, 0)) = -1 Then
                            'Assign the second current feature OID to the group's main feature CID
                            'and the rest of the group's CIDs
                            Ar2(dGroup(0, 0)) = dGroup(1, 1)
                            For a As Integer = 0 To dGroup.GetUpperBound(1)
                                Ar2(dGroup(0, a)) = Ar2(dGroup(0, 0))
                            Next
                        Else
                            'If a CID was chosen as most repeated in the group:
                            'Find all the group feature CID occurrances in the main CID list
                            'and replace them with the most repeated CID (found above)
                            For a As Integer = 1 To dGroup.GetUpperBound(1)
                                For f As Integer = 0 To Ar2.GetUpperBound(0)
                                    If Not Ar2(dGroup(0, a)) = -1 And Ar2(f) = Ar2(dGroup(0, a)) Then
                                        'Replace the found CID with the most repeated CID
                                        Ar2(f) = Ar2(dGroup(0, 0))
                                    End If
                                Next
                            Next
                            'Replace all the CIDs of the group for the main CID
                            For a As Integer = 1 To dGroup.GetUpperBound(1)
                                Ar2(dGroup(0, a)) = Ar2(dGroup(0, 0))
                            Next
                        End If
                    End If
                End If
                If Not pTrkCan.Continue Then
                    'SUMMARY PRINT: End program as interrupted
                    PRINTtxt += SumEndProgram("INTERRUPTED: Process interrupted by user.", _
                                              sSDate, sSTime)
                    'Destroy the progress dialog
                    ProgressDialogDispose(pProDlg, pStepPro, pTrkCan, pProDlgFact)
                    SaveSummaryReport(CAF.sOUT, PRINTtxt)
                    Return
                End If
            Next
            '********************************************************************************************
            'If the clustering method is 'Buffer: Same distance'
        Else

            'PROGRESS UPDATE: 
            pProDlg.Description = "Clustering calculation 'Buffer: Same distance'..."
            PRINTtxt += vbCrLf & " [Clustering calculation 'Buffer: Same distance'...]"

            Dim dGroup(3, 0) As Double
            'Main feature iteration
            pTrkCan.Reset()
            For j As Integer = 0 To Ar.GetUpperBound(1)
                'If the main feature NDIST is less than or equal to the user
                'distance query
                If Ar(4, j) <= CDbl(CAF.sNQUERY) Then
                    'Leave a single blank line in the group table
                    ReDim dGroup(3, 0)
                    'Add the record to the potential cluster group
                    dGroup(0, 0) = Ar(0, j) 'AID
                    dGroup(1, 0) = Ar(1, j) 'OID
                    dGroup(2, 0) = Ar(2, j) 'X
                    dGroup(3, 0) = Ar(3, j) 'Y
                    'Current feature iteration: GROUP construction of features within Near distance
                    For k As Integer = 0 To Ar.GetUpperBound(1)
                        '1. The OID of the main feature does not equal the OID the
                        '   current feature
                        '2. The current feature's nearest neighbor distance is less than or
                        '   equal to the 'Distance to closest point' value
                        '3. The distance between the main feature and the current feature
                        '   is less than or equal to the 'Buffer option: Same distance' value
                        '   times two
                        '- OR -
                        '1. The OID of the main feature does not equal the OID the
                        '   current feature
                        '3. The distance between the main feature and the current feature
                        '   is less than or equal to the 'Buffer option: Same distance' value
                        If (Not Ar(1, k) = Ar(1, j) And Ar(4, k) <= CDbl(CAF.sNQUERY) And _
                             GetDist(Ar(2, k), Ar(3, k), Ar(2, j), Ar(3, j), _
                                     dSemiMajAxis, dSemiMinAxis, CAF.bMEASPLAN) <= _
                             (CDbl(CAF.sCMBSVAL) * 2)) Or _
                           (Not Ar(1, k) = Ar(1, j) And GetDist(Ar(2, k), Ar(3, k), Ar(2, j), Ar(3, j), _
                                                                dSemiMajAxis, dSemiMinAxis, CAF.bMEASPLAN) <= _
                            CDbl(CAF.sCMBSVAL)) Then
                            'Add a single row to the end of the group table
                            ReDim Preserve dGroup(3, dGroup.GetUpperBound(1) + 1)
                            'Add the current feature to the potential cluster gorup
                            dGroup(0, dGroup.GetUpperBound(1)) = Ar(0, k) 'AID
                            dGroup(1, dGroup.GetUpperBound(1)) = Ar(1, k) 'OID
                            dGroup(2, dGroup.GetUpperBound(1)) = Ar(2, k) 'X
                            dGroup(3, dGroup.GetUpperBound(1)) = Ar(3, k) 'Y
                        End If
                    Next
                    'If there are features in the group other than the main feature
                    'If there are only two features in group:
                    If dGroup.GetUpperBound(1) = 1 Then
                        'If the second feature has a CID:
                        If Ar2(dGroup(0, 1)) <> -1 Then
                            'Assign the second current feature CID to the group's main feature CID
                            Ar2(dGroup(0, 0)) = Ar2(dGroup(0, 1))
                        Else
                            'Assign the second current feature OID to the group's main feature CID
                            Ar2(dGroup(0, 0)) = dGroup(1, 1)
                        End If
                    ElseIf dGroup.GetUpperBound(1) > 1 Then
                        'If there are more than two features in the group:
                        Dim nRepeat As Integer = 0
                        Dim nRepeatOld As Integer = 0
                        'Sort array with field: CID for ordering
                        Dim Ar2Sorted(Ar2.GetUpperBound(0)) As Double
                        System.Array.Copy(Ar2, 0, Ar2Sorted, 0, Ar2.Length)
                        System.Array.Sort(Ar2Sorted)

                        'Iterate through the group starting with second feature
                        For m As Integer = 1 To dGroup.GetUpperBound(1)
                            'If the CID of the group feature is set:
                            If Ar2(dGroup(0, m)) <> -1 Then
                                'Find the number of occurrances of this CID
                                Dim n1stIndex, nLastIndex As Integer
                                n1stIndex = System.Array.IndexOf(Ar2Sorted, Ar2(dGroup(0, m))) + 1
                                nLastIndex = System.Array.LastIndexOf(Ar2Sorted, Ar2(dGroup(0, m))) + 1
                                nRepeatOld = nRepeat
                                nRepeat = (nLastIndex - n1stIndex) + 1
                                'Set the CID of the main feature from the group to the CID
                                'having the most occurrances in the group
                                If Not nRepeat = 0 And nRepeat > nRepeatOld Then
                                    'Assign the CID with most occurrances in the group
                                    'to the main feature
                                    Ar2(dGroup(0, 0)) = Ar2(dGroup(0, m))
                                End If
                            End If
                        Next
                        'If there were no CIDs in the group:
                        If Ar2(dGroup(0, 0)) = -1 Then
                            'Assign the second current feature OID to the group's main feature CID
                            'and the rest of the group's CIDs
                            Ar2(dGroup(0, 0)) = dGroup(1, 1)
                            For a As Integer = 0 To dGroup.GetUpperBound(1)
                                Ar2(dGroup(0, a)) = Ar2(dGroup(0, 0))
                            Next
                        Else
                            'If a CID was chosen as most repeated in the group:
                            'Find all the group feature CID occurrances in the main CID list
                            'and replace them with the most repeated CID (found above)
                            For a As Integer = 1 To dGroup.GetUpperBound(1)
                                For f As Integer = 0 To Ar2.GetUpperBound(0)
                                    If Not Ar2(dGroup(0, a)) = -1 And Ar2(f) = Ar2(dGroup(0, a)) Then
                                        'Replace the found CID with the most repeated CID
                                        Ar2(f) = Ar2(dGroup(0, 0))
                                    End If
                                Next
                            Next
                            'Replace all the CIDs of the group for the main CID
                            For a As Integer = 1 To dGroup.GetUpperBound(1)
                                Ar2(dGroup(0, a)) = Ar2(dGroup(0, 0))
                            Next
                        End If
                    End If
                End If
                If Not pTrkCan.Continue Then
                    'SUMMARY PRINT: End program as interrupted
                    PRINTtxt += SumEndProgram("INTERRUPTED: Process interrupted by user.", _
                                              sSDate, sSTime)
                    'Destroy the progress dialog
                    ProgressDialogDispose(pProDlg, pStepPro, pTrkCan, pProDlgFact)
                    SaveSummaryReport(CAF.sOUT, PRINTtxt)
                    Return
                End If
            Next
        End If
        '********************************************************************************************


        'Add the CID of the main feature to the master CID list
        Dim dCIDList(Ar.GetUpperBound(1)) As Double
        System.Array.Copy(Ar2, 0, dCIDList, 0, Ar2.Length)
        'Sort the CID master list
        System.Array.Sort(dCIDList)

        'PROGRESS UPDATE: 
        pProDlg.Description = "Counting number of features per cluster..."
        PRINTtxt += vbCrLf & " [Counting number of features per cluster...]"

        'Get the number of CID occurances from the CID master list for each main feature CID
        pTrkCan.Reset()
        For n As Integer = 0 To Ar.GetUpperBound(1)
            'If the main feature has no CID, skip it
            If Ar2(Ar(0, n)) <> -1 Then
                'Get the first and last occurance of the main feature CID
                'from the CID master list
                Dim n1stIndex, nLastIndex As Integer
                n1stIndex = System.Array.IndexOf(dCIDList, Ar2(Ar(0, n))) + 1
                nLastIndex = System.Array.LastIndexOf(dCIDList, Ar2(Ar(0, n))) + 1
                'Calculate the number of main feature CID occurrances 
                'from the first and last index of that CID on the master CID list
                Ar3(Ar(0, n)) = (nLastIndex - n1stIndex) + 1
            End If
            If Not pTrkCan.Continue Then
                'SUMMARY PRINT: End program as interrupted
                PRINTtxt += SumEndProgram("INTERRUPTED: Process interrupted by user.", _
                                          sSDate, sSTime)
                'Destroy the progress dialog
                ProgressDialogDispose(pProDlg, pStepPro, pTrkCan, pProDlgFact)
                SaveSummaryReport(CAF.sOUT, PRINTtxt)
                Return
            End If
        Next

        'PROGRESS UPDATE: 
        pProDlg.Description = "Computing Cluster Statistics..."
        PRINTtxt += vbCrLf & " [Computing Cluster Statistics...]"

        'Compute Cluster Statistics
        PRINTtxt += CalcClusterStats(Ar, Ar2, Ar3)

        'PROGRESS UPDATE: 
        pProDlg.Description = "Creating point feature class..."
        PRINTtxt += vbCrLf & " [Creating point feature class...]"

        Dim pNewReqFields As IFields = GetClusterReqFields(True)
        Dim pNewFLayer As IFeatureLayer = New FeatureLayerClass()
        pNewFLayer.FeatureClass = CreateFeatureClass(pWrkspc2, pFDataset, CAF.sOUT, _
                                                     pNewReqFields, _
                                                     esriGeometryType.esriGeometryPoint, _
                                                     pSpatRef)
        pNewFLayer.Name = pNewFLayer.FeatureClass.AliasName
        Dim pNewFClass As IFeatureClass = pNewFLayer.FeatureClass

        Dim pNewFCursor As IFeatureCursor = pNewFClass.Insert(True)

        'Begin edit session and operation
        Dim pEditor As IEditor = My.ArcMap.Editor
        pEditor.StartEditing(pWrkspc2)
        pEditor.StartOperation()

        'PROGRESS UPDATE: 
        pProDlg.Description = "Storing point features..."
        PRINTtxt += vbCrLf & " [Storing point features...]"

        'Update the features with the values computed above
        pTrkCan.Reset()
        For o As Integer = 0 To Ar.GetUpperBound(1)
            If Ar2(Ar(0, o)) <> -1 Then
                pFeature1 = pFClass.GetFeature(Ar(1, o))
                'Create the feature buffer
                Dim pNewFeatureBuff As IFeatureBuffer = pNewFClass.CreateFeatureBuffer
                pNewFeatureBuff.Shape = pFeature1.ShapeCopy
                pNewFeatureBuff.Value(pNewFeatureBuff.Fields.FindField("cid")) = Ar2(Ar(0, o))
                pNewFeatureBuff.Value(pNewFeatureBuff.Fields.FindField("cnt")) = Ar3(Ar(0, o))
                pNewFCursor.InsertFeature(pNewFeatureBuff)
                If Not pTrkCan.Continue Then
                    'SUMMARY PRINT: End program as interrupted
                    PRINTtxt += SumEndProgram("INTERRUPTED: Process interrupted by user.", _
                                              sSDate, sSTime)
                    'Destroy the progress dialog
                    ProgressDialogDispose(pProDlg, pStepPro, pTrkCan, pProDlgFact)
                    SaveSummaryReport(CAF.sOUT, PRINTtxt)
                    Return
                End If
            End If
        Next

        'Stop edit operation and session, save edits
        pEditor.StopOperation("Cluster features")
        StopEditSession(True)

        'Add the new layer to the map
        Dim pNewLayer As ILayer = pNewFLayer
        pMxDoc.ActiveView.FocusMap.AddLayer(pNewLayer)

        'Refresh the Toc and Map
        pMxDoc.UpdateContents()
        pMxDoc.ActiveView.Refresh()

        'SUMMARY PRINT: End program as complete
        PRINTtxt += SumEndProgram("COMPLETE: Cluster process complete.", _
                                  sSDate, sSTime)

        'Destroy the progress dialog
        ProgressDialogDispose(pProDlg, pStepPro, pTrkCan, pProDlgFact)
        SaveSummaryReport(CAF.sOUT, PRINTtxt)

    End Sub
    Private Function CalcClusterStats(ByVal Ar, ByVal Ar2, ByVal Ar3)
        Dim dCount, dCountout, cluster_count, sample, n, dRange, dMean, dStd, dIqr, dMin, dMax, dSumsq, dArray(), _
    dMedian, dLQ, dUQ As Double

        Dim clus As Dictionary(Of Integer, Integer) = New Dictionary(Of Integer, Integer)

        dCount = 0
        dMax = 0
        dMin = 9999999
        Dim cluster_threshold As Integer = 10
        For o As Integer = 0 To Ar.GetUpperBound(1) - 1
            If Ar2(Ar(0, o)) <> -1 Then
                Dim key = Ar2(Ar(0, o))
                If clus.ContainsKey(key) Then
                    clus([key]) = Ar3(Ar(0, o))
                    If Ar3(Ar(0, o)) > dMax Then dMax = Ar3(Ar(0, o))
                    If Ar3(Ar(0, o)) < dMin Then dMin = Ar3(Ar(0, o))
                Else
                    clus.Add(CStr(Ar2(Ar(0, o))), Ar3(Ar(0, o)))
                    If Ar3(Ar(0, o)) > dMax Then dMax = Ar3(Ar(0, o))
                    If Ar3(Ar(0, o)) < dMin Then dMin = Ar3(Ar(0, o))
                    dCount += Ar3(Ar(0, o))
                    cluster_count += 1
                    If Ar3(Ar(0, o)) >= cluster_threshold Then sample += 1
                End If
            End If
        Next

        dRange = dMax - dMin
        dMean = dCount / clus.Count 'Total number of craters / total number clusters

        'Variance and STD
        dCountout = 0
        dSumsq = 0
        ReDim dArray(clus.Count)
        'Iterate over keys to get values here
        Dim pair As KeyValuePair(Of Integer, Integer)
        n = 0
        For Each pair In clus
            If pair.Value > 10 Then dCountout += 1
            dSumsq = dSumsq + (pair.Value - dMean) ^ 2
            dArray(n) = pair.Value
            n += 1 ' counter for index into the dArray

        Next

        dStd = Sqrt(dSumsq / clus.Count)

        System.Array.Sort(dArray)

        'Compute the quartiles
        If IEEERemainder(clus.Count, 2) = 0 Then
            dMedian = (dArray(Round(clus.Count * 0.5)) + _
                       dArray(Round((clus.Count * 0.5) + 1))) / 2
            dLQ = (dArray(Round(clus.Count * 0.25)) + _
                   dArray(Round((clus.Count * 0.25) + 1))) / 2
            dUQ = (dArray(Round(clus.Count * 0.75)) + _
                   dArray(Round((clus.Count * 0.75) + 1))) / 2
        Else
            dMedian = dArray(Round(clus.Count * 0.5))
            dLQ = dArray(Round(clus.Count * 0.25))
            dUQ = dArray(Round(clus.Count * 0.75))
        End If

        'Approximate Interquartile range
        dIqr = dUQ - dLQ


        'Write the stats out
        Dim sReport As String = ""
        Dim f100 As String = Space(3) & "|" & "{0,-20}" & "{1}" & "{2,16:F4}" & "|"
        Dim f300 As String = Space(3) & "|" & StrDup(37, "-") & "|"
        Dim f400 As String = Space(3) & "|" & StrDup(37, "_") & "|"
        Dim f500 As String = Space(3) & StrDup(39, "_")

        sReport = vbCrLf & vbCrLf & _
          Space(3) & "STATS: Clustering " & vbCrLf & _
          (String.Format(f500, "_")) & vbCrLf & _
          (String.Format(f100, "Population Total", "=", cluster_count)) & vbCrLf & _
          (String.Format(f100, "Clus. > 10 Craters", "=", sample)) & vbCrLf & _
          (String.Format(f100, "Percentage of Total", "=", sample / cluster_count * 100)) & vbCrLf & _
          (String.Format(f300, "-")) & vbCrLf & _
          (String.Format(f100, "  0% Min", "=", dMin)) & vbCrLf & _
          (String.Format(f100, " 25% Lower quartile", "=", dLQ)) & vbCrLf & _
          (String.Format(f100, " 50% Median", "=", dMedian)) & vbCrLf & _
          (String.Format(f100, " 75% Upper quartile", "=", dUQ)) & vbCrLf & _
          (String.Format(f100, "100% Max", "=", dMax)) & vbCrLf & _
          (String.Format(f300, "-")) & vbCrLf & _
          (String.Format(f100, "Range", "=", dRange)) & vbCrLf & _
          (String.Format(f100, "Mean", "=", dMean)) & vbCrLf & _
          (String.Format(f100, "St Dev", "=", dStd)) & vbCrLf & _
          (String.Format(f100, "Interquartile range", "=", dIqr)) & vbCrLf & _
          (String.Format(f400, "_")) & vbCrLf

        Return sReport

    End Function

    Private Function CalcBufferStats(ByVal Ar, ByVal dThreshold)
        Dim dCount, dCountout, dRange, dSum, dMean, dStd, dIqr, dMin, dMax, dSumsq, dArray(), _
            dMedian, dLQ, dUQ As Double

        dCount = 0
        dMax = Ar(4, 0)
        dMin = Ar(4, 0)
        ReDim dArray(Ar.GetUpperBound(1) - 1)
        'Ignore those points that are outliers in this computation.
        For i As Integer = 0 To Ar.GetUpperBound(1) - 1
            dCount += 1
            dArray(i) = Ar(4, i)
            If Ar(4, i) < dMin Then dMin = Ar(4, i)
            If Ar(4, i) > dMax Then dMax = Ar(4, i)
            If Ar(4, i) <= dThreshold Then dCountout = dCountout + 1
            dSum = dSum + Ar(4, i)
        Next
        dMean = dSum / dCount
        dRange = dMax - dMin

        'Variance and STD
        dSumsq = 0
        For i As Integer = 0 To Ar.GetUpperBound(1) - 1
            dSumsq = dSumsq + (Ar(4, i) - dMean) ^ 2
        Next

        dStd = Sqrt(dSumsq / dCount) ' We are not using dCount - 1 as the sample is the total population.

        System.Array.Sort(dArray)

        'Compute the quartiles
        If IEEERemainder(dCount, 2) = 0 Then
            dMedian = (dArray(Round(dCount * 0.5)) + _
                       dArray(Round((dCount * 0.5) + 1))) / 2
            dLQ = (dArray(Round(dCount * 0.25)) + _
                   dArray(Round((dCount * 0.25) + 1))) / 2
            dUQ = (dArray(Round(dCount * 0.75)) + _
                   dArray(Round((dCount * 0.75) + 1))) / 2
        Else
            dMedian = dArray(Round(dCount * 0.5))
            dLQ = dArray(Round(dCount * 0.25))
            dUQ = dArray(Round(dCount * 0.75))
        End If

        'Approximate Interquartile range
        dIqr = dUQ - dLQ

        'Write the stats out
        Dim sReport As String = ""
        Dim f100 As String = Space(3) & "|" & "{0,-20}" & "{1}" & "{2,16:F4}" & "|"
        Dim f300 As String = Space(3) & "|" & StrDup(37, "-") & "|"
        Dim f400 As String = Space(3) & "|" & StrDup(37, "_") & "|"
        Dim f500 As String = Space(3) & StrDup(39, "_")

        sReport = vbCrLf & vbCrLf & _
          Space(3) & "STATS: K-Nearest Neighbors " & vbCrLf & _
          (String.Format(f500, "_")) & vbCrLf & _
          (String.Format(f100, "Population total", "=", Ar.GetUpperBound(1))) & vbCrLf & _
          (String.Format(f100, "Population Sample", "=", dCountout)) & vbCrLf & _
          (String.Format(f100, "Percentage of Total", "=", dCountout / Ar.GetUpperBound(1) * 100)) & vbCrLf & _
          (String.Format(f300, "-")) & vbCrLf & _
          (String.Format(f100, "  0% Min", "=", dMin)) & vbCrLf & _
          (String.Format(f100, " 25% Lower quartile", "=", dLQ)) & vbCrLf & _
          (String.Format(f100, " 50% Median", "=", dMedian)) & vbCrLf & _
          (String.Format(f100, " 75% Upper quartile", "=", dUQ)) & vbCrLf & _
          (String.Format(f100, "100% Max", "=", dMax)) & vbCrLf & _
          (String.Format(f300, "-")) & vbCrLf & _
          (String.Format(f100, "Range", "=", dRange)) & vbCrLf & _
          (String.Format(f100, "Mean", "=", dMean)) & vbCrLf & _
          (String.Format(f100, "St Dev", "=", dStd)) & vbCrLf & _
          (String.Format(f100, "Interquartile range", "=", dIqr)) & vbCrLf & _
          (String.Format(f400, "_")) & vbCrLf

        Return sReport
    End Function

#Region "*** HELP CONTENT DISPLAY DYNAMICS ****************************************************"
#End Region

#Region "***** FORM *********"
#End Region
    Private Sub Frm_ClusterAnalysis_Click(ByVal sender As Object, _
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
                              ByVal e As System.EventArgs) _
                              Handles lblLAYER.Click
        HELP_Layer()
    End Sub

    Private Sub cboLAYER_Click(ByVal sender As Object, _
                               ByVal e As System.EventArgs) _
                               Handles cboLAYER.Click
        HELP_Layer()
    End Sub

#Region "***** NEAR QUERY *********"
#End Region

    Private Sub grpNQUERY_Click(ByVal sender As Object, _
                                ByVal e As System.EventArgs) _
                                Handles grpNQUERY.Click
        HELP_DistanceQuery()
    End Sub

    Private Sub grpNQUERY_Enter(ByVal sender As System.Object, _
                                ByVal e As System.EventArgs) _
                                Handles grpNQUERY.Enter
        HELP_DistanceQuery()
    End Sub

#Region "***** MEASUREMENT SPACE *******"
#End Region

    Private Sub grpMEASSPACE_Click(ByVal sender As System.Object, _
                           ByVal e As System.EventArgs) _
                           Handles grpMEASSPACE.Click
        HELP_MeasurementSpace()
    End Sub

    Private Sub grpMEASSPACE_Enter(ByVal sender As System.Object, _
                               ByVal e As System.EventArgs) _
                               Handles grpMEASSPACE.Enter
        HELP_MeasurementSpace()
    End Sub

#Region "***** CLUSTER METHOD *******"
#End Region

    Private Sub radCMS_GotFocus(ByVal sender As Object, _
                                   ByVal e As System.EventArgs) _
                                   Handles radCMS.GotFocus
        HELP_ClusterMethodSameDist()
    End Sub

    Private Sub txtCMSVAL_GotFocus(ByVal sender As Object, _
                                ByVal e As System.EventArgs) _
                                Handles txtCMSVAL.GotFocus
        HELP_ClusterMethodSameDist()
    End Sub

    Private Sub radCMBNND_GotFocus(ByVal sender As Object, _
                                     ByVal e As System.EventArgs) _
                                     Handles radCMBNND.GotFocus
        HELP_ClusterMethodBNearestNDist()
    End Sub

    Private Sub radCMBF_GotFocus(ByVal sender As Object, _
                                      ByVal e As System.EventArgs) _
                                      Handles radCMBF.GotFocus
        HELP_ClusterMethodBNearestNDistFact()
    End Sub

    Private Sub txtCMBFVAL_GotFocus(ByVal sender As Object, _
                                 ByVal e As System.EventArgs) _
                                 Handles txtCMBFVAL.GotFocus
        HELP_ClusterMethodBNearestNDistFact()
    End Sub

    Private Sub radCMBS_GotFocus(ByVal sender As Object, _
                                    ByVal e As System.EventArgs) _
                                    Handles radCMBS.GotFocus
        HELP_ClusterMethodBSameDist()
    End Sub

    Private Sub txtCMBSVAL_GotFocus(ByVal sender As Object, _
                                 ByVal e As System.EventArgs) _
                                 Handles txtCMBSVAL.GotFocus
        HELP_ClusterMethodBSameDist()
    End Sub

#Region "***** OUTPUT ********"
#End Region

    Private Sub grpOUT_Enter(ByVal sender As System.Object, _
                             ByVal e As System.EventArgs) _
                             Handles grpOUT.Enter
        HELP_Out()
    End Sub

    Private Sub grpOUT_Click(ByVal sender As System.Object, _
                             ByVal e As System.EventArgs) _
                             Handles grpOUT.Click
        HELP_Out()
    End Sub

#Region "*** HELP PANEL UPDATE CONTENT ********************************************************"
#End Region

    Private Sub HELP_Form()

        'Update help panel
        Dim strText As String = _
          "Identifies clusters of points using each point's Nearest " & _
          "Neighbor distance." & vbCrLf & vbCrLf & _
          "Requirement: The input layer must be projected in meters."

        HELPCntUpdate("Cluster Tool", strText)

    End Sub

    Private Sub HELP_Layer()

        'Update help panel
        Dim strText As String = _
            "The point layer." & vbCrLf & vbCrLf & _
            "Requirement: The input layer must be projected in meters."

        HELPCntUpdate("Input point layer", strText)

    End Sub

    Private Sub HELP_DistanceQuery()

        'Update help panel
        Dim strText As String = _
            "Points without a neighbor within this distance are considered outluers and are not " & _
            "considered during the cluster analysis process."

        HELPCntUpdate("Threshold Distance", strText)

    End Sub

    Private Sub HELP_MeasurementSpace()

        'Update help panel
        Dim strText As String = _
            "Planar - Distance measurements are performed in projected " & _
            "space." & vbCrLf & vbCrLf & _
            "Geodetic - Measurements are perfomed geodetically. This " & _
            "method will yield results independent of map projection " & _
            "and is considerably slower."

        HELPCntUpdate("Measurement space", strText)

    End Sub

    Private Sub HELP_ClusterMethodSameDist()

        'Update help panel
        Dim strText As String = _
            "Points that fall within this distance from a " & _
            "point are considered part of the same cluster."

        HELPCntUpdate("Fixed distance", strText)

    End Sub

    Private Sub HELP_ClusterMethodBNearestNDist()

        'Update help panel
        Dim strText As String = _
            "Points are buffered by a distance equal to their Nearest " & _
            "Neighbor distance. Once buffers are merged, all points " & _
            "that fall inside the merged area are considered part of the " & _
            "same cluster."

        HELPCntUpdate("Buffer option: Nearest Neighbor distance", strText)

    End Sub

    Private Sub HELP_ClusterMethodBNearestNDistFact()

        'Update help panel
        Dim strText As String = _
            "Points are buffered by a distance equal to their Nearest " & _
            "Neighbor distance times a factor value. Once buffers " & _
            "are merged, all points that fall inside the merged area are " & _
            "considered part of the same cluster."

        HELPCntUpdate("Buffer option: Nearest Neighbor distance x factor", strText)

    End Sub

    Private Sub HELP_ClusterMethodBSameDist()

        'Update help panel
        Dim strText As String = _
            "Points are buffered by this distance. Once buffers " & _
            "are merged, all points that fall inside the merged area are " & _
            "considered part of the same cluster."

        HELPCntUpdate("Buffer option: Fixed distance", strText)

    End Sub

    Private Sub HELP_Out()

        'Update help panel
        Dim strText As String = _
            "The output point layer name."

        HELPCntUpdate("Output layer name", strText)

    End Sub

    Private Sub HELPCntUpdate(ByVal Title As String, ByVal Text As String)

        'Update the content of the help panel
        rtxtHELP_CNT.Clear()

        rtxtHELP_CNT.AppendText("   " & vbCrLf & Title)
        rtxtHELP_CNT.Find(Title, RichTextBoxFinds.MatchCase)
        rtxtHELP_CNT.SelectionFont = New Drawing.Font("Arial", 12, Drawing.FontStyle.Bold)
        rtxtHELP_CNT.SelectionColor = Drawing.Color.Black
        rtxtHELP_CNT.DeselectAll()
        rtxtHELP_CNT.AppendText(vbCrLf & vbCrLf)

        rtxtHELP_CNT.AppendText(Text)

        rtxtHELP_CNT.AppendText(vbCrLf & vbCrLf & vbCrLf)
        rtxtHELP_CNT.Find("   ", RichTextBoxFinds.MatchCase)
        rtxtHELP_CNT.ScrollToCaret()
        rtxtHELP_CNT.DeselectAll()

        rtxtHELP_CNT.Refresh()

    End Sub

    Private Sub btnCANCEL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCANCEL.Click

    End Sub

    Private Sub Optimize_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOptParam.Click

    End Sub

    Private Sub GroupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
End Class