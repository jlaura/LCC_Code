Imports ESRI.ArcGIS.Editor
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

#Region "Form Controls and Entry Validation"
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

    Private Sub DistTable_DropDown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Disttable.DropDown
        'Populate the dist table dropdown
        Dim mxdoc As IMxDocument = My.ArcMap.Document
        Dim map As IMap = mxdoc.FocusMap
        Dim tablecollection As ITableCollection = map
        Dim table As ITable
        Dim dataset As IDataset

        'Populate with tables
        Try
            'Clear the dropdown from previous launches
            Disttable.Items.Clear()
            'Populate the dropdown
            For i = 0 To tablecollection.TableCount - 1
                table = tablecollection.Table(i)
                dataset = table
                Disttable.Items.Add(dataset.Name)
            Next
        Catch ex As Exception
            MsgBox("No distance tables available.  Run the distance table tool first.", MsgBoxStyle.Exclamation, "No Distance Tables")
            Exit Sub
        End Try
    End Sub

    Private Sub DistTable_DropdownClose(ByVal sender As Object, ByVal e As System.EventArgs) Handles Disttable.DropDownClosed
        If Disttable.SelectedIndex = -1 Then
            distance_table = Nothing
        End If
    End Sub

    Private Sub Distance_Table_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Disttable.SelectedIndexChanged
        If Not Disttable.SelectedIndex = -1 Then
            distance_table = Disttable.Text

        Else
            distance_table = Nothing
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

    Private Sub DBScan_Update(ByVal Sender As Object, ByVal e As System.EventArgs) Handles minpts.Leave
        'Distance Around point
        Try
            If CInt(txtNQUERY.Text) > 0 Then txtNQUERY.Text = _
                                    CInt(txtNQUERY.Text).ToString
        Catch ex As Exception
            MsgBox("Please enter a 'Search Distance'.  Points outside this distance cannot initiate cluster generation.", _
                   MsgBoxStyle.Exclamation, _
                   "Invalid Parameter")
            txtNQUERY.Text = "1500"
        End Try

        'Min Points
        Try
            If CInt(minpts.Text) > 0 Then minpts.Text = _
                                    CInt(minpts.Text).ToString
        Catch ex As Exception
            MsgBox("Please enter a 'Minimum Number of Points'. Clusters must meet this threshold to be created.", _
                   MsgBoxStyle.Exclamation, _
                   "Invalid Parameter")
            minpts.Text = "1500"
        End Try

        'Epsilon
        Try
            If CInt(eps.Text) < 1 Then eps.Text = CStr(eps.Text)
        Catch ex As Exception
            MsgBox("Please enter an integer greater than or equal to 1.", MsgBoxStyle.Exclamation, "Invalid Parameter")
            eps.Text = "1500"
        End Try


    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, _
                            ByVal e As System.EventArgs) _
                            Handles btnOK.Click
        'This method handles the OK OnClick call.
        'Check for errors in all field values
        FormErrorHandler()

    End Sub

    Private Sub Optimize_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optimize.Click
        Compute_DatasetStats()

    End Sub

    Private Sub kgraph_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles kgraph.Click
        'Get the statistics, the distance, and the distance array matrix if it hasn't been computed.
        'Make sure that we have an input layer..
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

        'Make sure that a distance table was selected.
        If distance_table Is Nothing Then
            MsgBox("Please select a 'Distance Table'.", MsgBoxStyle.Exclamation, _
                   "Missing Parameter")
            Return
        End If

        '>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        ' Create a CancelTracker
        Dim pTrkCan As ITrackCancel = New CancelTracker

        ' Create the ProgressDialog. This automatically displays the dialog
        Dim pProDlgFact As IProgressDialogFactory = New ProgressDialogFactory
        Dim pProDlg As IProgressDialog2 = pProDlgFact.Create(pTrkCan, My.ArcMap.Application.hWnd)

        ' Set the properties of the ProgressDialog
        pProDlg.CancelEnabled = True
        pProDlg.Title = "Generating Statistics"
        pProDlg.Animation = esriProgressAnimationTypes.esriProgressSpiral

        ' Set the properties of the Step Progressor
        Dim pStepPro As IStepProgressor = pProDlg
        pStepPro.MinRange = 0
        pStepPro.MaxRange = pFClass.FeatureCount(Nothing)
        pStepPro.StepValue = 1
        pStepPro.Message = "Progress:"
        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

        'PROGRESS UPDATE: 
        pProDlg.Description = "Computing Statistics..."
        'Create an array that is the size of the input shapefile
        'Each index store the nearest neighbor distance
        ReDim kdist(pFClass.FeatureCount(Nothing))

        'Open the distance table
        Dim table = getTableByName(distance_table)
        Dim InFid As Integer = table.FindField("IN_FID")
        Dim NearDist As Integer = table.FindField("NEAR_DIST")

        Dim cursor As ICursor = table.Search(Nothing, True)
        Dim row As IRow = cursor.NextRow()
        Dim epsilon_list As New List(Of Double)
        Dim sum As Double = 0
        Dim counter As Integer = 0

        While Not row Is Nothing
            Try
                While row.Value(InFid) = counter
                    epsilon_list.Add(row.Value(NearDist))
                    row = cursor.NextRow()
                End While

                epsilon_list.Sort()
                kdist(counter) = epsilon_list(minpts.Text)
                epsilon_list.Clear()

                counter += 1
            Catch ex As Exception

            End Try

            If Not pTrkCan.Continue Then
                ProgressDialogDispose(pProDlg, pStepPro, pTrkCan, pProDlgFact)
                Return
            End If

        End While

        'Destroy the progress dialog
        ProgressDialogDispose(pProDlg, pStepPro, pTrkCan, pProDlgFact)

        'Draw the graph using a windows form.
        Dim distanceform As kDistance
        distanceform = New kDistance()
        distanceform.Show()
        distanceform = Nothing
    End Sub

    Private Sub LogSaveDialog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LogSaveDialog.Click
        Dim saveFileDialog1 As New SaveFileDialog()

        saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"
        saveFileDialog1.FilterIndex = 2
        saveFileDialog1.RestoreDirectory = True

        If saveFileDialog1.ShowDialog() = DialogResult.OK Then
            LogFileName.Text = saveFileDialog1.FileName
        End If
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
            Me.MaximumSize = New Size(900, 720)
            Me.Size = New Size(720, Me.Size.Height)
            splcHELP.Panel2Collapsed = False
            btnSHHELP.Text = "<< Hide Help"
        End If

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
        Dim pDataset As IDataset = CType(pFClass, IDataset)
        Dim pWrkspc2 As IWorkspace2 = DirectCast(pDataset.Workspace, IWorkspace2)

        'Make sure that a distance table was selected.
        If distance_table Is Nothing Then
            MsgBox("Please select a 'Distance Table'.", MsgBoxStyle.Exclamation, _
                   "Missing Parameter")
            Return
        End If

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
            MsgBox("The output layer name contains invlaid characters unsupported by ArcMap.  Please rename the output layer.", MsgBoxStyle.Exclamation, "Invalid Output Name")
            Return
        End If

        'Check if feature class already exists on the workspace
        If FeatureClassExists(pWrkspc2, txtOUT.Text) Then
            MsgBox("The output layer already exsits.  Please select a new output layer name.", MsgBoxStyle.Exclamation, "Output Exists")
            Return
        End If

        'Check to see which tab is active to know which clustering method to use.
        '0: Heirarchal, 1: S-Link, 2: D-Link, 3: DBScan
        Dim clusteringmethod = New Dictionary(Of Integer, String) From {{0, "dbscan"}, {1, "heirarchal"}, {2, "slink"}, {3, "dlink"}}
        Dim currenttab As Integer = tabcontrol.SelectedIndex
        c_method = clusteringmethod(currenttab)
        'If all errors are handled, load progress form
        LoadProgressForm()

    End Sub

    Private Sub LoadProgressForm()
        'This method launches the progress form, packages the parameters from the form, and calls the RunClustering method to initiate processing.

        'Hide the Cluster Analysis form
        Me.Hide()

        'Define parameter bundle
        Dim PARA As New CAPARAM
        PARA.sFLAYER = m_sCAFLayer
        PARA.sNQUERY = txtNQUERY.Text
        PARA.bCMS = radCMS.Checked
        PARA.bMEASPLAN = radMEASPLAN.Checked
        PARA.bMEASGEO = radMEASGEO.Checked
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
#End Region

    Public Sub RunClustering(ByVal CAF As CAPARAM)
        'This is the algorithm to run the clustering analysis.

        Dim PRINTtxt As String = ""

        Dim pMxDoc As IMxDocument = My.ArcMap.Document 'This is an interface that allows access to the map document.  It is used to add the cluster layer to the ToC and update the ToC.
        Dim pMap As IMap = pMxDoc.FocusMap
        Dim pFlayer As IFeatureLayer2 = GetFLayerByName(CAF.sFLAYER) ' Updated to use V2 - GetFLayerByName access the public function in mod_public, which iterates over the ToC just like the Arc example.
        Dim pFClass As IFeatureClass = pFlayer.FeatureClass 'A property - the datasource for the layer
        Dim pDataset As IDataset = pFClass
        Dim pFDataset As IFeatureDataset = pFClass.FeatureDataset
        Dim pWrkspc2 As IWorkspace = DirectCast(pDataset.Workspace, IWorkspace)
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
            PRINTtxt += " Buffer  - Nearest Neighbor distance x " & CAF.sCMBFVAL & " units."
        ElseIf CAF.bCMBS Then
            PRINTtxt += " Buffer option - Fixed distance: " & CAF.sCMBSVAL & " m."
        End If
        PRINTtxt += vbCrLf & " Output layer name: " & CAF.sOUT
        PRINTtxt += vbCrLf & vbCrLf

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

        'If the spatial reference is planar and the distance computation is geodesic, we need to store the x,y values as lat / lon pairs
        Dim gcs As Boolean = False
        If TypeOf pSpatRef Is IProjectedCoordinateSystem And radMEASPLAN.Checked = False Then
            gcs = True
        End If

        'Populate array with AID,OID,X,Y from all records in feature class.
        Dim cnt As Integer = 0
        Dim pPoint As IPoint = Nothing
        While Not pFeature1 Is Nothing
            Ar(0, cnt) = cnt 'AID
            Ar(1, cnt) = pFeature1.OID 'OID
            pPoint = pFeature1.ShapeCopy
            If gcs = True Then
                pPoint.Project(pGCS)
                Ar(2, cnt) = pPoint.X 'X
                Ar(3, cnt) = pPoint.Y 'Y
            Else
                Ar(2, cnt) = pPoint.X 'X
                Ar(3, cnt) = pPoint.Y 'Y
            End If
            Ar(4, cnt) = 9999999 ' Placeholder 'really large distance'
            Ar2(cnt) = -1 'CID
            Ar3(cnt) = -1 'CNT
            cnt += 1
            pFeature1 = pFCursor1.NextFeature()
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

        'PROGRESS UPDATE: 
        pProDlg.Description = "Extracting Nearest Neighbor Distances From Table..."
        'PRINTtxt += vbCrLf & " [Calculating Nearest Neighbor distances...]"

        Dim dFeature1(2, 0) As Double
        pTrkCan.Reset()

        'Check whether this is a shapefile or a geodatabase.
        Dim fid_offset As Integer = 0
        If pWrkspc2.Type = esriWorkspaceType.esriLocalDatabaseWorkspace Then fid_offset = 1

        ExtractNearest(Ar, distance_table, fid_offset)

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

        'Compute statistics on the clusters.
        PRINTtxt += CalcBufferStats(Ar, CAF.sNQUERY)

        If c_method = "heirarchal" Then
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
                    If Ar(4, j) <= CDbl(CAF.sNQUERY) Then
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
                            'and the distance CAF.sNQUERY is that specified by user:
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
                        If LogFileName.Text <> "" Then
                            SaveLog(LogFileName.Text, PRINTtxt)
                            Return
                        End If
                        Return
                    End If
                Next
                '********************************************************************************************
                'If the clustering method is 'Buffer: Nearest neighbor distance'
            ElseIf CAF.bCMS = True Then

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
                        If LogFileName.Text <> "" Then
                            SaveLog(LogFileName.Text, PRINTtxt)
                            Return
                        End If
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
                        If LogFileName.Text <> "" Then
                            SaveLog(LogFileName.Text, PRINTtxt)
                            Return
                        End If
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
                        If LogFileName.Text <> "" Then
                            SaveLog(LogFileName.Text, PRINTtxt)
                            Return
                        End If
                        Return
                    End If
                Next
            End If

        ElseIf c_method = "dbscan" Then
            Dim measurement_space As Boolean = False
            If radMEASPLAN.Checked Then
                measurement_space = True

            End If
            'DBSCAN
            Dim counter As Integer = 0
            For i As Integer = 0 To Ar.GetUpperBound(1) - 1
                If Ar(4, i) < CDbl(txtNQUERY.Text) Then
                    Dim Arr As New List(Of Double)
                    Arr.Add(Ar(0, i))
                    Arr.Add(Ar(1, i))
                    Arr.Add(Ar(2, i))
                    Arr.Add(Ar(3, i))
                    dist_lists.Add(Arr)
                    counter += 1
                End If
            Next

            Dim cluster_id As Integer = 0
            'Create a list of the unvisited nodes.  We iterate over these.
            Dim Unvisited As New List(Of Integer)
            Unvisited.Clear()
            For i As Integer = 0 To dist_lists.Count - 1
                Unvisited.Add(CInt(dist_lists(i)(0)))
            Next

            'Progress Bar
            pProDlg.Description = "Clustering using DBScan..."
            pStepPro.Message = String.Format("{0} / {1} Points Processed", dist_lists.Count - Unvisited.Count, dist_lists.Count)
            pStepPro.MinRange = 0
            pStepPro.MaxRange = dist_lists.Count - 1

            'Setup to start at a random node
            Dim randomnumber As New Random
            Dim index As Integer = 0
            Dim old_count As Integer = 0

            'Iterate until we have visited all nodes.
            Do Until Unvisited.Count = 0
                'Grab the index of the current node
                index = randomnumber.Next(0, Unvisited.Count - 1)
                Dim node = Unvisited(index)
                old_count = Unvisited.Count
                'If node = 1077 Or node = 1078 Or node = 1079 Or node = 1076 Or node = 1080 Or node = 1081 Or node = 1082 Or node = 1083 Or node = 1084 Then MsgBox("here", MsgBoxStyle.OkOnly, "gotcha")

                'Remove the node from the unvisited list and the node from the dist_lists
                Unvisited.RemoveAt(index)

                'Get the index from dist_lists from the node_id
                Dim node_index As Integer = 0
                node_index = GetNodeIndex(node)

                'Get the neighbors to the current node
                Dim neighbors = getNeighbors(eps.Text, node_index, dSemiMajAxis, dSemiMinAxis, measurement_space)

                'If we are greater than epsilon we have a cluster, otherwise we have noise.  Unmarked nodes are implicitly noise.
                If neighbors.Count >= CInt(minpts.Text) Then 'neighbors includes the source point in the list

                    'Attempt to expand the cluster
                    Unvisited = ExpandCluster(neighbors, cluster_id, eps.Text, minpts.Text, Ar2, Unvisited, dSemiMajAxis, dSemiMinAxis, measurement_space)

                End If
                pStepPro.StepValue = old_count - Unvisited.Count
                pStepPro.Message = String.Format("{0} / {1} Points Processed", dist_lists.Count - Unvisited.Count, dist_lists.Count)
                If Not pTrkCan.Continue Then
                    'SUMMARY PRINT: End program as interrupted
                    PRINTtxt += SumEndProgram("INTERRUPTED: Process interrupted by user.", _
                                              sSDate, sSTime)
                    'Destroy the progress dialog
                    ProgressDialogDispose(pProDlg, pStepPro, pTrkCan, pProDlgFact)
                    Return
                End If

            Loop
            'Cleanup
            dist_lists.Clear()

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
        pStepPro.Message = "Processing:"
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
                If LogFileName.Text <> "" Then
                    SaveLog(LogFileName.Text, PRINTtxt)
                    Return
                End If
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
        For o As Integer = 0 To Ar2.Length - 1
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
                    If LogFileName.Text <> "" Then
                        SaveLog(LogFileName.Text, PRINTtxt)
                        Return
                    End If
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
        'Save the log here without a dialog
        If LogFileName.Text <> "" Then
            SaveLog(LogFileName.Text, PRINTtxt)
        End If

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

    Private Function CalcBufferStats(ByVal Ar, ByVal dThreshold) As String
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

    '#Region "DBScan"
    '    Private Function getNeighbors(ByVal epsilon As Double, ByVal index As Double, ByVal semimajor As Double, ByVal semiminor As Double, ByVal measurement_space As Boolean) As Stack(Of Integer)
    '        Dim neighbors As New Stack(Of Integer)
    '        Dim dist As Double = 0.0
    '        For i As Integer = 0 To dist_lists.Count - 1
    '            dist = GetDist(dist_lists(index)(2), dist_lists(index)(3), dist_lists(i)(2), dist_lists(i)(3), semimajor, semiminor, measurement_space)
    '            If dist <= epsilon Then
    '                neighbors.Push(i) 'By Index
    '            End If
    '        Next

    '        Return neighbors
    '    End Function

    '    Private Function ExpandCluster(ByRef neighbors As Stack(Of Integer), ByRef cluster_id As Integer, ByVal epsilon As Double, ByVal minpts As Integer, _
    '                                   ByRef Ar2() As Double, ByVal Unvisited As List(Of Integer), ByVal semimajor As Double, ByVal semiminor As Double, ByVal measurement_space As Boolean) As List(Of Integer)

    '        Dim new_neighbors As New Stack(Of Integer)
    '        Dim neighbor_node As Integer = 0
    '        'Dim node_index As Integer = 0
    '        Dim new_neighbor As Integer = 0
    '        Dim visited As New List(Of Integer)

    '        'Increment the cluster counter
    '        cluster_id = cluster_id + 1


    '        'For each neighbor in neighbors
    '        While neighbors.Count > 0

    '            'Iterate over the neighbors, popping one at a time from the stack
    '            neighbor_node = neighbors.Pop
    '            'Add node to the visited list
    '            visited.Add(neighbor_node)

    '            'If we have not visited the node yet, check to see if the cluster extends
    '            If Unvisited.Contains(dist_lists(neighbor_node)(0)) Then
    '                'Mark neighbor as visited
    '                Unvisited.Remove(dist_lists(neighbor_node)(0))
    '                'Get the index of the neighbor_node in the dist_lists
    '                'node_index = GetNodeIndex(neighbor_node)
    '                'Get the neighbors to the new neighbor, i.e. is the cluster expanding by epsilon
    '                new_neighbors = getNeighbors(epsilon, neighbor_node, semimajor, semiminor, measurement_space)
    '                'If the number of new neighbors constitutes a new cluster, start adding that cluster as well.  Grow by density essentially.
    '                If new_neighbors.Count + Unvisited.Count + neighbors.Count > minpts Then
    '                    Do Until new_neighbors.Count = 0
    '                        new_neighbor = new_neighbors.Peek
    '                        If visited.Contains(new_neighbor) Or neighbors.Contains(new_neighbor) Then
    '                            new_neighbors.Pop()
    '                        Else
    '                            neighbors.Push(new_neighbors.Pop)
    '                        End If
    '                    Loop
    '                End If
    '            End If

    '            'If the neighbor is not part of a cluster, add it to the current cluster.
    '            If Ar2(dist_lists(neighbor_node)(0)) = -1 Then Ar2(dist_lists(neighbor_node)(0)) = cluster_id

    '        End While
    '        Return Unvisited
    '    End Function

    '    Private Function GetNodeIndex(ByVal node As Integer) As Integer
    '        For i As Integer = 0 To dist_lists.Count - 1
    '            If dist_lists(i)(0) = node Then
    '                node = i
    '            End If
    '        Next
    '        Return node
    '    End Function
    '#End Region

#Region "Compute Optimization Statistics"
    Private Sub Compute_DatasetStats()
        'Make sure that we have an input layer..
        Dim pFLayer As IFeatureLayer = GetFLayerByName(m_sCAFLayer)
        'Make sure an input layer was selected and assigned to an object
        If cboLAYER.SelectedIndex = -1 Or pFLayer Is Nothing Then
            MsgBox("Please select an 'Input point layer'.", MsgBoxStyle.Exclamation, _
                   "Missing Parameter")
            Return
        End If

        Dim pFClass As IFeatureClass = pFLayer.FeatureClass
        Dim pDataset As IDataset = pFClass
        Dim pWrkspc2 As IWorkspace = DirectCast(pDataset.Workspace, IWorkspace)

        'Make sure that a distance table was selected.
        If distance_table Is Nothing Then
            MsgBox("Please select a 'Distance Table'.", MsgBoxStyle.Exclamation, _
                   "Missing Parameter")
            Return
        End If

        '>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        ' Create a CancelTracker
        Dim pTrkCan As ITrackCancel = New CancelTracker

        ' Create the ProgressDialog. This automatically displays the dialog
        Dim pProDlgFact As IProgressDialogFactory = New ProgressDialogFactory
        Dim pProDlg As IProgressDialog2 = pProDlgFact.Create(pTrkCan, My.ArcMap.Application.hWnd)

        ' Set the properties of the ProgressDialog
        pProDlg.CancelEnabled = True
        pProDlg.Title = "Generating Statistics"
        pProDlg.Animation = esriProgressAnimationTypes.esriProgressSpiral

        ' Set the properties of the Step Progressor
        Dim pStepPro As IStepProgressor = pProDlg
        pStepPro.MinRange = 0
        pStepPro.MaxRange = pFClass.FeatureCount(Nothing)
        pStepPro.StepValue = 1
        pStepPro.Message = "Progress:"
        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

        'PROGRESS UPDATE: 
        pProDlg.Description = "Computing Statistics..."
        'Create an array that is the size of the input shapefile
        'Each index store the nearest neighbor distance
        Dim DistArray(pFClass.FeatureCount(Nothing) - 1)
        For i = 0 To DistArray.GetUpperBound(0) - 1
            DistArray(i) = 9999999
        Next

        Dim indexmap As New Dictionary(Of Integer, Integer)
        'This checks for an OID mapping so that non-sequentially mappings can be supported

        Dim fcursor As IFeatureCursor = pFClass.Search(Nothing, False)
        Dim frow As IFeature = fcursor.NextFeature
        Dim n As Integer = 0
        If pWrkspc2.Type = esriWorkspaceType.esriLocalDatabaseWorkspace Then
            n = 1
            Dim oid_counter As Integer = 1
            Do While Not frow Is Nothing
                indexmap.Add(CInt(frow.OID), oid_counter)
                oid_counter = oid_counter + 1
                frow = fcursor.NextFeature
            Loop
        Else
            Dim fid_counter As Integer = 0
            Do While Not frow Is Nothing
                indexmap.Add(fid_counter, fid_counter)
                fid_counter = fid_counter + 1
            Loop
        End If

        'Open the distance table
        Dim table = getTableByName(distance_table)
        Dim InFid As Integer = table.FindField("IN_FID")

        Dim NearDist As Integer = table.FindField("NEAR_DIST")

        Dim cursor As ICursor = table.Search(Nothing, False)
        Dim row As IRow = cursor.NextRow

        Do While Not row Is Nothing
            Try
                If CDbl(row.Value(NearDist)) < CDbl(DistArray(indexmap(row.Value(InFid)) - n)) Then
                    DistArray(indexmap(row.Value(InFid)) - n) = CDbl(row.Value(NearDist))
                End If
                row = cursor.NextRow
                If Not pTrkCan.Continue Then
                    ProgressDialogDispose(pProDlg, pStepPro, pTrkCan, pProDlgFact)
                    Return
                End If
            Catch ex As Exception
                row = cursor.NextRow
                MsgBox(row.Value(InFid), MsgBoxStyle.MsgBoxHelp, "Row OID")
            End Try

        Loop

        'Get the mean + std as the optimal max distance
        Dim dCount, dRange, dSum, dMean, dStd, dMin, dMax, dSumsq, dMedian, dLQ, dUQ As Double

        dCount = 0
        dMax = DistArray(0)
        dMin = DistArray(0)

        For i As Integer = 0 To DistArray.GetUpperBound(0) - 1
            dCount += 1
            If DistArray(i) < dMin Then dMin = DistArray(i)
            If DistArray(i) > dMax Then dMax = DistArray(i)
            dSum = dSum + DistArray(i)
        Next
        dMean = dSum / dCount
        dRange = dMax - dMin

        'Variance and STD
        dSumsq = 0
        For i As Integer = 0 To DistArray.GetUpperBound(0) - 1
            dSumsq = dSumsq + (DistArray(i) - dMean) ^ 2
        Next

        dStd = Sqrt(dSumsq / dCount)

        System.Array.Sort(DistArray)

        'Compute the quartiles
        If IEEERemainder(dCount, 2) = 0 Then
            dMedian = (DistArray(Round(dCount * 0.5)) + _
                        DistArray(Round((dCount * 0.5) + 1))) / 2
            dLQ = (DistArray(Round(dCount * 0.25)) + _
                    DistArray(Round((dCount * 0.25) + 1))) / 2
            dUQ = (DistArray(Round(dCount * 0.75)) + _
                    DistArray(Round((dCount * 0.75) + 1))) / 2
        Else
            dMedian = DistArray(Round(dCount * 0.5))
            dLQ = DistArray(Round(dCount * 0.25))
            dUQ = DistArray(Round(dCount * 0.75))
        End If

        'Populate the mininum distance text box with the optimized distance

        txtNQUERY.Text = CStr(Math.Round(dLQ, 0))
        eps.Text = CStr(Math.Round(2 * dLQ, 0))
        meanstats.Text = CStr(Math.Round(dMean, 3))
        rangestats.Text = CStr(Math.Round(dRange, 3))
        stdstats.Text = CStr(Math.Round(dStd, 3))
        medianstats.Text = CStr(Math.Round(dMedian, 3))
        lqstats.Text = CStr(Math.Round(dLQ, 3))
        uqstats.Text = CStr(Math.Round(dUQ, 3))
        minstats.Text = CStr(Math.Round(dMin, 3))
        maxstats.Text = CStr(Math.Round(dMax, 3))
        'Destroy the progress dialog
        ProgressDialogDispose(pProDlg, pStepPro, pTrkCan, pProDlgFact)

    End Sub
#End Region

#Region "Help Button & Contents"

#Region "Form Help"
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

    Private Sub HELP_Form()

        'Update help panel
        Dim strText As String = _
          "Identifies clusters of points using each point's Nearest " & _
          "Neighbor distance." & vbCrLf & vbCrLf & _
          "Requirement: The input layer must be projected in meters."

        HELPCntUpdate("Cluster Tool", strText)

    End Sub
#End Region

#Region "Input Layer Help"
    Private Sub lblLAYER_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblLAYER.Click
        HELP_Layer()
    End Sub

    Private Sub cboLAYER_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboLAYER.Click
        HELP_Layer()
    End Sub

    Private Sub HELP_Layer()

        'Update help panel
        Dim strText As String = _
            "The point layer." & vbCrLf & vbCrLf & _
            Environment.NewLine & _
            "Requirement: The input layer must be projected in meters."

        HELPCntUpdate("Input point layer", strText)

    End Sub
#End Region

#Region "Distance Table Help"
    'Distance Table
    Private Sub disttable_lbl_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles disttable_lbl.Click
        HELP_Distance()
    End Sub

    Private Sub disttable_ddown_click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Disttable.Click
        HELP_Distance()
    End Sub

    'Outlier Threshold Distance
    Private Sub grpNQUERY_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles outlierdist_grp.Click
        HELP_DistanceQuery()
    End Sub

    Private Sub grpNQUERY_Enter(ByVal sender As System.Object, _
                                ByVal e As System.EventArgs) _
                                Handles outlierdist_grp.Enter
        HELP_DistanceQuery()
    End Sub

    Private Sub grpNQUERY_Focus(ByVal sender As System.Object, _
                            ByVal e As System.EventArgs) _
                            Handles outlierdist_grp.GotFocus
        HELP_DistanceQuery()
    End Sub

    Private Sub HELP_Distance()
        'Update help panel
        Dim strText As String = _
            "The distance table associated with this input layer." & vbCrLf & vbCrLf & _
            "Requirement: A distance table generated using the distance table tool."

        HELPCntUpdate("Input Distance Table", strText)
    End Sub

    Private Sub HELP_DistanceQuery()

        'Update help panel
        Dim strText As String = _
            "Points without a neighbor within this distance are considered outliers and are not considered during the cluster analysis process. " & vbCrLf & vbCrLf & _
            "Optimization of this distance sets the threshold to the mean distance of the entire sample and reports statistics of the dataset for potential alteration to our 'best guess optima'."

        HELPCntUpdate("Threshold Distance", strText)

    End Sub
#End Region

#Region "Stats"
    Private Sub stats_group_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles stats_group.Click
        HELP_stats()
    End Sub

    Private Sub meanstats_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles meanstats.Click
        HELP_stats()
    End Sub

    Private Sub medianstats_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles medianstats.Click
        HELP_stats()
    End Sub

    Private Sub rangestats_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rangestats.Click
        HELP_stats()
    End Sub

    Private Sub stdstats_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles stdstats.Click
        HELP_stats()
    End Sub

    Private Sub uqstats_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles uqstats.Click
        HELP_stats()
    End Sub

    Private Sub lqstats_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lqstats.Click
        HELP_stats()
    End Sub

    Private Sub minstats_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles minstats.Click
        HELP_stats()
    End Sub

    Private Sub maxstats_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles maxstats.Click
        HELP_stats()
    End Sub

    Private Sub HELP_stats()
        'Update help panel
        Dim strText As String = "Derived from the distance table, these fields are populated" & _
            " by the optimization button."

        HELPCntUpdate("Nearest Neighbor Statistics", strText)
    End Sub
#End Region

#Region "Measurement Space Help"
    Private Sub grpMEASSPACE_Click(ByVal sender As System.Object, _
                       ByVal e As System.EventArgs) Handles grpMEASSPACE.Click

        HELP_MeasurementSpace()
    End Sub

    Private Sub grpMEASSPACE_Enter(ByVal sender As System.Object, _
                               ByVal e As System.EventArgs)

        HELP_MeasurementSpace()
    End Sub

    Private Sub geod_measure_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radMEASGEO.CheckedChanged
        HELP_MeasurementSpace()
    End Sub

    Private Sub planar_measure_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radMEASPLAN.CheckedChanged
        HELP_MeasurementSpace()
    End Sub

    Private Sub HELP_MeasurementSpace()

        'Update help panel
        Dim strText As String = _
            "Planar - Distance measurements are performed in projected " & _
            "space." & vbCrLf & vbCrLf & _
            "Geodetic - Measurements are perfomed geodetically. This " & _
            "method will yield results independent of map projection " & _
            "and is considerably slower." & _
            Environment.NewLine & _
            Environment.NewLine & _
            "WARNING: Using geodesic measurement in projected space will not process.  To use geodesic measurements, the shapefile or featureclass must be in GCS." & _
            Environment.NewLine & _
            Environment.NewLine & _
            "As a metric to help drive your choice: Colorado is approximately 451km by 612km. Banerjee, S. (2004) reports that the compured distance between the most " & _
            "distant points using Euclidean (planar) distance was 933.8km, while geodetic distance was 741.7km.  This is using un-projected data.  If the input data " & _
            "is projected we see that (using a Mercator projection) euclidean distance is closer at 951.8km." & _
            Environment.NewLine & _
            Environment.NewLine & _
            "In short - euclidean distance accuracy errors increase proportional to the size of the study area." & _
            Environment.NewLine & _
            "Banerjee, S. (2004). On Geodetic Distance Computations in Spatial Modeling. Biometrics."

        HELPCntUpdate("Measurement space", strText)

    End Sub
#End Region

#Region "Clustering"
    Private Sub clustmeth_grp_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles clustmeth_grp.Click
        Help_ClusterMethod()
    End Sub

    Private Sub DBScan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DBScan.Click
        HELP_DBScan()
    End Sub

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

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Help_ClusterMethod()
    End Sub

    Private Sub GroupBox3_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox3.Enter
        HELP_DBScan()
    End Sub

    Private Sub minpts_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles minpts.Click
        HELP_DBScan()
    End Sub

    Private Sub Help_ClusterMethod()
        'Update help panel
        Dim strText As String = _
            "DBScan - A density based clustering method which requires a minimum threshold number of impacts as a parameter.  This is the computationally faster clustering method." & _
            Environment.NewLine & _
            Environment.NewLine & _
           "Heirarchal - A traditional clustering method that iterates over all point sequentially and merges clusters.  This is a signifigantly slower clustering method."

        HELPCntUpdate("Clustering Method", strText)
    End Sub

    Private Sub eps_grp_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles eps_grp.Enter
        HELP_DBScan()
    End Sub

    Private Sub eps_grp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles eps_grp.Click
        HELP_DBScan()
    End Sub

    Private Sub HELP_DBScan()
        'Update help panel
        Dim strText As String = _
            "Minimum Cluster Seed Size determines the minimum number of clusters within the threshold distance in order to begin a new crater cluster." & _
            Environment.NewLine & _
            "Epsilon is the distance threshold at which points are clustered.  At distances beyond epsilon, a new cluster will be generated if the max seed size (density) is met." & _
            Environment.NewLine & _
           "The K-Distance graph provides a metric to assist in determining the distance at which outliers (noise) begin to impact cluster creation. " & _
           "A drastic increase in slope is indicative of the distance at which outliers should be thresholded as noise. " & _
           "Note that this is data specific, i.e. low ressolution source data with distinct spacecraft track boundaries skews this metric signifigantly."

        HELPCntUpdate("Density Based Clustering (DBScan)", strText)
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
#End Region

#Region "Output File"
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

    Private Sub HELP_Out()

        'Update help panel
        Dim strText As String = _
            "The output point layer name."

        HELPCntUpdate("Output layer name", strText)

    End Sub
#End Region

#Region "Log File"
    Private Sub log_grp_Enter(ByVal sender As System.Object, _
                             ByVal e As System.EventArgs) _
                             Handles log_grp.Click
        HELP_Out()
    End Sub

    Private Sub log_name_Click(ByVal sender As System.Object, _
                             ByVal e As System.EventArgs) _
                             Handles LogFileName.Click
        HELP_Log()
    End Sub

    Private Sub LogFileName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LogFileName.Click
        HELP_Log()
    End Sub

    Private Sub HELP_Log()

        'Update help panel
        Dim strText As String = _
            "The output log file name." & _
            Environment.NewLine & _
            "This field is optional."

        HELPCntUpdate("Output layer name", strText)

    End Sub
#End Region
#End Region


End Class