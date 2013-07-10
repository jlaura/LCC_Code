Imports ESRI.ArcGIS.Editor
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Carto
Imports System.Windows.Forms
Imports System.Drawing
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.DataSourcesGDB
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.Geometry
Imports System.Math
Imports ESRI.ArcGIS.Geoprocessor
Imports ESRI.ArcGIS.AnalysisTools
Imports System.Threading
Imports ESRI.ArcGIS.Geoprocessing



Public Class frm_distancetool
    Private Sub distancetool_Load(ByVal sender As System.Object, _
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
        'HELP_Form()

    End Sub
#Region "Input Layer DropDown"
    Private Sub inputlayer_DropDown(ByVal sender As Object, _
                                 ByVal e As System.EventArgs) Handles inputlayer.DropDown


        Dim pMxDoc As IMxDocument = My.ArcMap.Document

        'Populate layers dropdown
        Try
            'Clear cmbbox contents
            inputlayer.Items.Clear()

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
                    inputlayer.Items.Add(pLayer.Name)
                End If
                pLayer = pELayer.Next
            End While

        Catch ex As Exception
            MsgBox("No point layers available.", _
                   MsgBoxStyle.Exclamation, "No Point Layers")
            Exit Sub
        End Try

    End Sub

    Private Sub inputlayer_DropDownClosed(ByVal sender As Object, _
                                        ByVal e As System.EventArgs) Handles inputlayer.DropDownClosed


        If inputlayer.SelectedIndex = -1 Then
            distlayer = Nothing
        End If

    End Sub

    Private Sub inputlayer_SelectedIndexChanged _
                                (ByVal sender As System.Object, _
                                 ByVal e As System.EventArgs) Handles inputlayer.SelectedIndexChanged


        'If an input layer is selected
        If Not inputlayer.SelectedIndex = -1 Then
            'Assign layer name to global variable
            distlayer = inputlayer.Text
        Else
            distlayer = Nothing
        End If

    End Sub
#End Region

    Private Sub TextBoxes_Update(ByVal sender As Object, ByVal e As System.EventArgs)

        'Make sure that we have an integer
        Try
            If CInt(knn.Text) > 0 Then knn.Text = CInt(knn.Text).ToString
        Catch ex As Exception
            MsgBox("Please enter an integer number of neighbors.", MsgBoxStyle.Exclamation, "Invalid Parameter")
            knn.Text = "10"
        End Try

    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles okay.Click
        validateform()
    End Sub

    Private Sub validateform()
        'On form submission this method is called to validate all the form fields.
        Dim layer As IFeatureLayer = GetFLayerByName(distlayer)

        'Make sure an input layer was selected and assigned to an object
        If distlayer Is Nothing Then
            MsgBox("Please select an 'Input point layer'.", MsgBoxStyle.Exclamation, _
                   "Missing Parameter")
            Return
        End If

        Dim featureclass As IFeatureClass = layer.FeatureClass
        Dim dataset As IDataset = featureclass
        Dim workspace2 As IWorkspace2 = DirectCast(dataset.Workspace, IWorkspace2)

        'Query points by nearest distance' distance number required
        If CInt(knn.Text) <= 0 Then
            MsgBox("Table must be generated for 1 or more nearest neighbors" _
                   , MsgBoxStyle.Exclamation, "Invalid Parameter")
            Return
        End If

        'Check for measurement type (geo or planar)
        Dim distance_measure As String
        If planar_measure.Checked = True Then
            distance_measure = "Planar"
        Else
            distance_measure = "Geodesic"
        End If

        'Check for invalid output name
        If ValidateString(distanceTableOut.Text, "Output layer name", layer.Name) = False Then
            Return
        End If

        'Check if feature class already exists on the workspace
        If FeatureClassExists(workspace2, distanceTableOut.Text) Then
            MsgBox("The output layer feature class already exists. " & _
                   "Please enter a different 'Output layer name'.", _
                   MsgBoxStyle.Exclamation, _
                   "Invalid Parameter")
            Return
        End If

        'If all errors are handled, load progress form
        LoadProgressForm(distlayer, knn.Text, distance_measure, distanceTableOut.Text, workspace2)

    End Sub

    Private Sub LoadProgressForm(ByVal distlayer, ByVal knn, ByVal distance_measure, ByVal distanceTableOut, ByRef workspace)
        'This method launches the progress form, packages the parameters from the form, and calls the RunClustering method to initiate processing.

        'Hide the Cluster Analysis form
        Me.Hide()

        'Check for the license type
        Dim license = GetArcGISLicenseName()

        If license = "Advanced" And distance_measure = "Planar" Then
            'Generate near table using geoprocessing
            generateNearTable(distlayer, knn, distanceTableOut)
        ElseIf distance_measure = "Geodesic" Then
            'Generate a near table manually using geodesic distance
            generateNearGeodesicTable(distlayer, knn, distanceTableOut, workspace)
        Else
            'Generate a near table manually using planar distance
            generateNearTablePlanar(distlayer, knn, distanceTableOut, workspace)
        End If

        'Close and dispose of form
        m_clusterForm = Nothing
        Me.Close()
        Me.Dispose()

    End Sub

    Private Sub generateNearTablePlanar(ByVal distlayer, ByVal knn, ByVal distanceTableOut, ByRef workspace)
        MsgBox("ALERT: NOT YET IMPLEMENTED", MsgBoxStyle.Exclamation, "ERROR")
    End Sub

    Private Sub generateNearGeodesicTable(ByVal distlayer, ByVal knn, ByVal distanceTableOut, ByRef workspace)
        'This method generates a geodesic KNN table.  This is signifigantly slower than using near in planar space due to the additional math involved.

        'Get a hook into the application
        Dim mxdoc As IMxDocument = My.ArcMap.Document 'This is an interface that allows access to the map document.  It is used to add the cluster layer to the ToC and update the ToC.
        Dim map As IMap = mxdoc.FocusMap

        'Access the feature class
        Dim featurelayer = GetFLayerByName(distlayer)
        Dim featureclass As IFeatureClass = featurelayer.FeatureClass

        'Grab all the spatial reference information from the input shapefile
        Dim gcs As IGeographicCoordinateSystem2 = Nothing
        Dim semi_major_axis As Double = 0
        Dim semi_minor_axis As Double = 0
        Try
            Dim spatial_reference As ISpatialReference = GetFLayerSpatRef(featurelayer)
            gcs = GetGCS(spatial_reference)
            semi_major_axis = gcs.Datum.Spheroid.SemiMajorAxis
            semi_minor_axis = gcs.Datum.Spheroid.SemiMinorAxis
        Catch ex As Exception
            MsgBox("Unable to determine input data spatial reference.  Please ensure the file has a spatial reference associated with it", MsgBoxStyle.Exclamation, "Error")
        End Try

        'Create the output table with the necessary fields.
        Dim tName = distanceTableOut
        Dim tTable = CreateTable(workspace, tName, Nothing)

        'Setup to insert into the table
        Dim tablebuffer As IRowBuffer = tTable.CreateRowBuffer()
        Dim tablerow As IRow = CType(tablebuffer, IRow)
        Dim tablerowbuffer As IRowBuffer = tablebuffer
        Dim tableinsert As ICursor = tTable.Insert(True)

        '>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        ' Create a CancelTracker
        Dim pTrkCan As ITrackCancel = New CancelTracker

        ' Create the ProgressDialog. This automatically displays the dialog
        Dim pProDlgFact As IProgressDialogFactory = New ProgressDialogFactory
        Dim pProDlg As IProgressDialog2 = pProDlgFact.Create(pTrkCan, My.ArcMap.Application.hWnd)

        ' Set the properties of the ProgressDialog
        pProDlg.CancelEnabled = True
        pProDlg.Title = "KNN Distance Table Generation - Geodesic Distances"
        pProDlg.Animation = esriProgressAnimationTypes.esriProgressSpiral

        ' Set the properties of the Step Progressor
        Dim pStepPro As IStepProgressor = pProDlg
        pStepPro.MinRange = 0
        pStepPro.MaxRange = featureclass.FeatureCount(Nothing)
        pStepPro.StepValue = 1
        pStepPro.Message = "Progress:"

        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

        'Extract the OID, AID, Xcoord, Ycoord
        pProDlg.Description = "Extracting OID, X and Y values..."

        Dim count As Integer = 0
        Dim Ar(4, featureclass.FeatureCount(Nothing) - 1) As Double
        Dim cursor As IFeatureCursor = featureclass.Search(Nothing, False)
        Dim row As IFeature = cursor.NextFeature
        While Not row Is Nothing
            Ar(0, count) = count
            Ar(1, count) = row.OID
            Dim point As IPoint = row.ShapeCopy
            point.Project(gcs)
            Ar(2, count) = point.X
            Ar(3, count) = point.Y
            row = cursor.NextFeature
            count += 1
        End While

        'Progress Bar Cancel Options
        If Not pTrkCan.Continue Then
            ProgressDialogDispose(pProDlg, pStepPro, pTrkCan, pProDlgFact)
            Return
        End If

        'Find K-Nearest Neighbors to each input point
        pProDlg.Description = "Extracting Nearest Neighbor distances..."

        Dim feature(2, 0) As Double 'Indices are OID | xcoord | ycoord
        For i As Integer = 0 To Ar.GetUpperBound(1)
            feature(0, 0) = Ar(1, i)
            feature(1, 0) = Ar(2, i)
            feature(2, 0) = Ar(3, i)
            Dim dist(3, knn) As Double ' Indices are OID | xcoord | ycoord | distance

            'Find the K-Nearest Neighbors in Geodesic space.
            FindNearestGeo(Ar, feature, dist, semi_major_axis, semi_minor_axis, knn)

            'Insert the returned distances into the distance table
            Dim InFID = tTable.FindField("IN_FID")
            Dim NearFID = tTable.FindField("NEAR_FID")
            Dim DistField = tTable.FindField("NEAR_DIST")
            Dim xCoord = tTable.FindField("XCOORD")
            Dim yCoord = tTable.FindField("YCOORD")

            For j As Integer = 0 To dist.GetUpperBound(1) - 1
                tablerowbuffer.Value(InFID) = Ar(1, i)
                tablerowbuffer.Value(NearFID) = dist(0, j)
                tablerowbuffer.Value(DistField) = dist(3, j)
                tablerowbuffer.Value(xCoord) = dist(1, j)
                tablerowbuffer.Value(yCoord) = dist(2, j)
                tableinsert.InsertRow(tablerowbuffer)
            Next

            If Not pTrkCan.Continue Then
                ProgressDialogDispose(pProDlg, pStepPro, pTrkCan, pProDlgFact)
                Return
            End If

        Next

        'Add the table to the ToC.  If the table exists, this is the existing table.
        Dim pStandAloneTable As IStandaloneTable = New StandaloneTable()
        pStandAloneTable.Table = tTable
        Dim pStandAloneColl As IStandaloneTableCollection = map
        pStandAloneColl.AddStandaloneTable(pStandAloneTable)
        mxdoc.UpdateContents()

        'Close the progress dialog.
        ProgressDialogDispose(pProDlg, pStepPro, pTrkCan, pProDlgFact)

    End Sub

    Private Sub generateNearTable(ByVal distlayer, ByVal knn, ByVal DistanceTableOut)
        'Setup the geoprocessor
        Dim gp As ESRI.ArcGIS.Geoprocessor.Geoprocessor = New ESRI.ArcGIS.Geoprocessor.Geoprocessor()
        AddHandler gp.ToolExecuted, AddressOf gpToolExecuted

        'Access the feature layer twice so we can aim at it twice with the gp tool.
        Dim FeatureLayer = GetFLayerByName(distlayer)
        Dim dataset As IDataset = CType(FeatureLayer, IDataset)
        Dim working_dir As String = dataset.Workspace.PathName


        '>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        ' Create a CancelTracker
        Dim pTrkCan As ITrackCancel = New CancelTracker

        ' Create the ProgressDialog. This automatically displays the dialog
        Dim pProDlgFact As IProgressDialogFactory = New ProgressDialogFactory
        Dim pProDlg As IProgressDialog2 = pProDlgFact.Create(pTrkCan, My.ArcMap.Application.hWnd)

        ' Set the properties of the ProgressDialog
        pProDlg.CancelEnabled = True
        pProDlg.Title = "Distance Table Tool"
        pProDlg.Animation = esriProgressAnimationTypes.esriProgressSpiral

        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        'Setup Params
        Dim lcount As Long = knn

        ' Create an IVariantArray to hold the parameter values.
        Dim parameters As IVariantArray = New VarArray

        ' Populate the variant array with parameter values.
        parameters.Add(FeatureLayer)
        parameters.Add(distlayer.ToString())
        parameters.Add(working_dir + "\\" + DistanceTableOut.ToString())
        parameters.Add("")
        parameters.Add("LOCATION")
        parameters.Add("NO_ANGLE")
        parameters.Add("CLOSEST")
        parameters.Add(lcount)

        Dim gpResult As IGeoProcessorResult2 = CType(gp.ExecuteAsync("GenerateNearTable", parameters), IGeoProcessorResult2)


        ''Get the generate near table tool
        'Dim genneartable As GenerateNearTable = New GenerateNearTable()

        ''Setup the Gp Environment
        'Dim addtomap As Boolean = gp.AddOutputsToMap
        'gp.TemporaryMapLayers = False
        'gp.AddOutputsToMap = True 'Add the derived table to the map doc.
        'gp.OverwriteOutput = True

        ''Set Params 
        'genneartable.in_features = FeatureLayer
        'genneartable.near_features = distlayer.ToString()
        ''genneartable.near_features = "C:\Users\jlaura\Desktop\Zunil\Zunil_secondaries_proj.shp"
        'genneartable.out_table = working_dir + "\\" + DistanceTableOut.ToString()
        'genneartable.closest = False
        'genneartable.location = True
        'genneartable.closest_count = lcount

        Dim results As IGeoProcessorResult = CType(gp.Execute(genneartable, pTrkCan), IGeoProcessorResult)
        While results.Status <> esriJobStatus.esriJobSucceeded
            System.Diagnostics.Debug.Write(gp.GetMessage(0))
        End While
        'Execute
        'Try
        '    gp.Execute(genneartable, Nothing)
        '    If gp.MessageCount > 0 Then

        '        For Count As Integer = 0 To gp.MessageCount - 1

        '            MsgBox(gp.GetMessage(Count), MsgBoxStyle.Exclamation, "Progress")

        '        Next Count

        '    End If

        ProgressDialogDispose(pProDlg, Nothing, pTrkCan, pProDlgFact)

        'Catch ex As Exception

        'End Try



    End Sub

    Private Function CreateTable(ByVal workspace As IWorkspace2, ByVal tableName As System.String, ByVal fields As IFields) As ITable

        ' Create the behavior clasid for the featureclass  
        Dim uid As ESRI.ArcGIS.esriSystem.UID = New ESRI.ArcGIS.esriSystem.UIDClass

        If workspace Is Nothing Then
            Return Nothing ' valid feature workspace not passed in as an argument to the method
        End If

        Dim featureWorkspace As IFeatureWorkspace = CType(workspace, IFeatureWorkspace) ' Explicit Cast
        Dim table As ITable

        If workspace.NameExists(esriDatasetType.esriDTTable, tableName) Then

            '  A table with that name already exists so return that table 
            table = featureWorkspace.OpenTable(tableName)
            Return table
        End If

        uid.Value = "esriGeoDatabase.Object"

        Dim objectClassDescription As IObjectClassDescription = New ObjectClassDescriptionClass

        ' If a fields collection is not passed in then supply our own
        If fields Is Nothing Then

            ' Create the fields using the required fields method
            fields = New FieldsClass()

            Dim fieldsEdit As IFieldsEdit = CType(fields, IFieldsEdit) ' Explicit Cast

            'Threshold Field
            Dim fDist As IField = New FieldClass()
            Dim fDistEdit As IFieldEdit = CType(fDist, IFieldEdit)
            fDistEdit.Name_2 = "IN_FID"

            fDistEdit.Type_2 = esriFieldType.esriFieldTypeInteger
            fDistEdit.Length_2 = 12
            fDistEdit.Editable_2 = True
            fDistEdit.IsNullable_2 = False
            fieldsEdit.AddField(fDist)

            'FID
            Dim fCount As IField = New FieldClass()
            Dim fCountEdit As IFieldEdit = CType(fCount, IFieldEdit)
            fCountEdit.Name_2 = "NEAR_FID"
            fCountEdit.Length_2 = 12
            fCountEdit.Type_2 = esriFieldType.esriFieldTypeInteger
            fCountEdit.IsNullable_2 = False
            fieldsEdit.AddField(fCount)

            'Near Distance
            Dim fPercTot As IField = New FieldClass()
            Dim fpercTotEdit As IFieldEdit = CType(fPercTot, IFieldEdit)
            fpercTotEdit.Name_2 = "NEAR_DIST"
            fpercTotEdit.Type_2 = esriFieldType.esriFieldTypeDouble
            fpercTotEdit.Precision_2 = 12
            fpercTotEdit.Scale_2 = 4
            fpercTotEdit.Editable_2 = True
            fpercTotEdit.IsNullable_2 = False
            fieldsEdit.AddField(fPercTot)

            'XCoord
            Dim fMean As IField = New FieldClass()
            Dim fMeanEdit As IFieldEdit = CType(fMean, IFieldEdit)
            fMeanEdit.Name_2 = "XCOORD"
            fMeanEdit.Type_2 = esriFieldType.esriFieldTypeDouble
            fMeanEdit.Precision_2 = 12
            fMeanEdit.Scale_2 = 4
            fMeanEdit.Editable_2 = True
            fMeanEdit.IsNullable_2 = False
            fieldsEdit.AddField(fMean)

            'YCoord
            Dim fMedian As IField = New FieldClass()
            Dim fMedianEdit As IFieldEdit = CType(fMedian, IFieldEdit)
            fMedianEdit.Name_2 = "YCOORD"
            fMedianEdit.Type_2 = esriFieldType.esriFieldTypeDouble
            fMedianEdit.Precision_2 = 12
            fMedianEdit.Scale_2 = 4
            fMedianEdit.Editable_2 = True
            fMedianEdit.IsNullable_2 = False
            fieldsEdit.AddField(fMedian)

        End If

        ' Use IFieldChecker to create a validated fields collection.
        Dim fieldChecker As IFieldChecker = New FieldCheckerClass()
        Dim enumFieldError As IEnumFieldError = Nothing
        Dim validatedFields As IFields = Nothing
        fieldChecker.ValidateWorkspace = CType(workspace, IWorkspace)
        fieldChecker.Validate(fields, enumFieldError, validatedFields)

        ' The enumFieldError enumerator can be inspected at this point to determine 
        ' which fields were modified during validation.


        ' Create and return the table
        table = featureWorkspace.CreateTable(tableName, validatedFields, uid, Nothing, "")

        Return table

    End Function

#Region "Help Button & Contents"

    Private Sub btnSHHELP_Click_1(ByVal sender As System.Object, _
                                ByVal e As System.EventArgs) _
                                Handles btnSHHELP.Click

        If btnSHHELP.Text = "<< Hide Help" Then
            splcHELP.Panel2Collapsed = True
            Me.MaximumSize = New Size(Me.MinimumSize.Width, _
                                      Me.MaximumSize.Height)
            Me.Size = New Size(Me.MinimumSize.Width, Me.Size.Height)
            btnSHHELP.Text = "Show Help >>"
        Else
            Me.MaximumSize = New Size(900, 438)
            Me.Size = New Size(693, Me.Size.Height)
            splcHELP.Panel2Collapsed = False
            btnSHHELP.Text = "<< Hide Help"
        End If
    End Sub

#Region "General Help"
    Private Sub frm_distancetool_Click(ByVal sender As Object, _
                                             ByVal e As System.EventArgs) _
                                             Handles Me.Click
        HELP_Form()
    End Sub

    Private Sub splcHELP_Click(ByVal sender As Object, _
                           ByVal e As System.EventArgs) _
                           Handles btnSHHELP.Click
        HELP_Form()
    End Sub

    Private Sub HELP_Form()

        'Update help panel
        Dim strText As String = _
          "Computes the k nearest neighbors in tabular form " & vbCrLf & vbCrLf & _
          "Requirement: An input layer (shapefile or featureclass)."

        HELPCntUpdate("Distance Matrix Tool", strText)
    End Sub
#End Region

#Region "Input Layer Help"
    'Layer
    Private Sub lblLAYER_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblLAYER.Click
        HELP_Layer()
    End Sub

    Private Sub cboLAYER_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles inputlayer.Click
        HELP_Layer()
    End Sub

    Private Sub HELP_Layer()

        'Update help panel
        Dim strText As String = _
            "The point layer." & vbCrLf & vbCrLf & _
            "Requirement: The input layer must be projected in meters."

        HELPCntUpdate("Input point layer", strText)

    End Sub

#End Region

#Region "KNN Help"
    Private Sub knn_grp_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles knn_grp.Click
        HELP_KNN()
    End Sub

    Private Sub knn_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles knn.Click
        HELP_KNN()
    End Sub

    Private Sub HELP_KNN()

        'Update help panel
        Dim strText As String = _
            "The number of nearest neighbors." & vbCrLf & vbCrLf & _
            "Requirement: An integer number of nearest neighbors to compute for each point observation."

        HELPCntUpdate("K-Nearest Neighbors", strText)

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

    Private Sub geod_measure_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles geodesic_measure.CheckedChanged
        HELP_MeasurementSpace()
    End Sub

    Private Sub planar_measure_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles planar_measure.CheckedChanged
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

#Region "Output Table Help"
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
            "The output table name."

        HELPCntUpdate("Output table name", strText)

    End Sub
#End Region

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




End Class