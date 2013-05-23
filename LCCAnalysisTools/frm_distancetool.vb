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
        LoadProgressForm(distlayer, knn.Text, distanceTableOut.Text)

    End Sub

    Private Sub LoadProgressForm(ByVal distlayer, ByVal knn, ByVal distanceTableOut)
        'This method launches the progress form, packages the parameters from the form, and calls the RunClustering method to initiate processing.

        'Hide the Cluster Analysis form
        Me.Hide()

        'Check for the license type
        'Check for the license type
        Dim license = GetArcGISLicenseName()

        If license = "Advanced" Then
            'Generate near table
            generateNearTable(distlayer, knn, distanceTableOut)
        Else
            'Run the cluster analysis program
            generateKnn(distlayer, knn, distanceTableOut)
        End If
       

        'Close and dispose of form
        m_clusterForm = Nothing
        Me.Close()
        Me.Dispose()

    End Sub

    Private Sub generateKnn(ByVal distlayer, ByVal knn, ByVal distanceTableOut)


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
        genneartable.closest_count = lcount 'This should be a parameter

        'Execute
        Try
            gp.Execute(genneartable, Nothing)
        Catch ex As Exception

        End Try



    End Sub


End Class