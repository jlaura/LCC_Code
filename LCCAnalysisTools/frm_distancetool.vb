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
                                 ByVal e As System.EventArgs) _
                                 Handles inputlayer.DropDown

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
                                        ByVal e As System.EventArgs)


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

    Private Sub TextBoxes_Update(ByVal sender As Object, ByVal e As System.EventArgs) Handles knn.Leave

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
    Private Sub btnSHHELP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    Private Sub btnCANCEL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

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
        LoadProgressForm(distlayer, knn.Text, distance_measure, distanceTableOut.Text)

    End Sub

    Private Sub LoadProgressForm(ByVal distlayer, ByVal knn, ByVal distance_measure, ByVal distanceTableOut)
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
            generateNearGeodesicTable(distlayer, knn, distanceTableOut)
        Else
            'Generate a near table manually using planar distance
            generateNearTablePlanar(distlayer, knn, distanceTableOut)
        End If


        'Close and dispose of form
        m_clusterForm = Nothing
        Me.Close()
        Me.Dispose()

    End Sub

    Private Sub generateNearTablePlanar(ByVal distlayer, ByVal knn, ByVal distanceTableOut)


    End Sub

    Private Sub generateNearGeodesicTable(ByVal distlayer, ByVal knn, ByVal distanceTableOut)
        'This method generates a geodesic KNN table.  This is signifigantly slower than using near in planar space due to the additional math involved.

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
        For i As Integer = 0 To Ar.GetUpperBound(0) - 1
            feature(0, 0) = Ar(1, i)
            feature(1, 0) = Ar(2, i)
            feature(2, 0) = Ar(3, i)
            Dim dist(3, knn) As Double ' Indices are OID | xcoord | ycoord | distance

            'Find the K-Nearest Neighbors in Geodesic space.
            FindNearestGeo(Ar, feature, dist, semi_major_axis, semi_minor_axis, knn)
            MsgBox(dist(0, 3), MsgBoxStyle.OkCancel, "Sample Distance")
            If Not pTrkCan.Continue Then
                ProgressDialogDispose(pProDlg, pStepPro, pTrkCan, pProDlgFact)
                Return
            End If

        Next

    End Sub

    Private Sub generateNearTable(ByVal distlayer, ByVal knn, ByVal DistanceTableOut)
        'Setup the geoprocessor
        Dim gp As New ESRI.ArcGIS.Geoprocessor.Geoprocessor

        Dim FeatureLayer = GetFLayerByName(distlayer)
        Dim NearFeatureLayer = GetFLayerByName(distlayer)

        'Setup Params
        Dim lcount As Long = knn

        'Get the generate near table tool
        Dim genneartable As GenerateNearTable = New GenerateNearTable()

        'Setup the Gp Environment
        Dim addtomap As Boolean = gp.AddOutputsToMap
        gp.TemporaryMapLayers = False
        gp.AddOutputsToMap = True 'Add the derived table to the map doc.
        gp.OverwriteOutput = True

        'Set Params
        genneartable.in_features = FeatureLayer
        genneartable.near_features = "C:\Users\jlaura\Desktop\Zunil\Zunil_secondaries_proj.shp"
        genneartable.out_table = DistanceTableOut
        genneartable.closest = False
        genneartable.location = True
        genneartable.closest_count = lcount

        'Execute
        Try
            gp.Execute(genneartable, Nothing)
        Catch ex As Exception

        End Try



    End Sub


End Class