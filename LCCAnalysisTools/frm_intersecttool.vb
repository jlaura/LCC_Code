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

Public Class frm_intersecttool
#Region "Buttons, DropDowns, & Form Validation"

    Private Sub Frm_IntersectionAnalysis_Load(ByVal sender As System.Object, _
                                     ByVal e As System.EventArgs) _
                                     Handles MyBase.Load

        Dim pEditor As IEditor = My.ArcMap.Editor

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
            m_sIAFLayer = Nothing
        End If

    End Sub

    Private Sub cboLAYER_SelectedIndexChanged _
                                (ByVal sender As System.Object, _
                                 ByVal e As System.EventArgs) _
                                 Handles cboLAYER.SelectedIndexChanged

        'If an input layer is selected
        If Not cboLAYER.SelectedIndex = -1 Then
            'Assign layer name to global variable
            m_sIAFLayer = cboLAYER.Text
        Else
            m_sIAFLayer = Nothing
        End If

    End Sub

    Private Sub TextBoxes_Update(ByVal sender As Object, _
                                 ByVal e As System.EventArgs) _
                                 Handles txtNQUERY.Leave

        'Round the near 'Distance to closest point' value
        Try
            If CInt(txtNQUERY.Text) > 0 Then txtNQUERY.Text = _
                                    CInt(txtNQUERY.Text).ToString
        Catch ex As Exception
            MsgBox("Please enter a 'Nearest Neighbor distance filter' value greater than 0.", _
                   MsgBoxStyle.Exclamation, _
                   "Invalid Parameter")
            txtNQUERY.Text = "30000"
        End Try

        'Round the cluster method 'Same distance(m)' distance value
        Try
            If CInt(txtCMSVAL.Text) > 0 Then txtCMSVAL.Text = _
                                    CInt(txtCMSVAL.Text).ToString
        Catch ex As Exception
            MsgBox("Please enter a value greater than 0 for the selected clustering method.", _
                   MsgBoxStyle.Exclamation, _
                   "Invalid Parameter")
            txtCMSVAL.Text = "40000"
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
            txtCMBFVAL.Text = "1.7"
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
            txtCMBSVAL.Text = "40000"
        End Try

        'Intersection count filter
        Try
            If CInt(txtCPNUM.Text) > 0 Then txtCPNUM.Text = _
                                    CInt(txtCPNUM.Text).ToString
        Catch ex As Exception
            MsgBox("Please enter a 'Point count filter' value greater than 0.", _
                   MsgBoxStyle.Exclamation, _
                   "Invalid Parameter")
            txtCPNUM.Text = "5"
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

        FormErrorHandler()

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

    Private Sub FormErrorHandler()

        Dim pFLayer As IFeatureLayer = GetFLayerByName(m_sIAFLayer)

        'Make sure an input layer was selected and assigned to an object
        If cboLAYER.SelectedIndex = -1 Or pFLayer Is Nothing Then
            MsgBox("Please select an 'Input polyline layer'.", MsgBoxStyle.Exclamation, _
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

        'Intersection count filter
        If CInt(txtCPNUM.Text) <= 0 Then
            MsgBox("Please enter a 'Point count filter' value greater than 0." _
                   , MsgBoxStyle.Exclamation, "Invalid Parameter")
            Return
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

        'Check to see which tab is active to know which clustering method to use.
        '0: Heirarchal, 1: S-Link, 2: D-Link, 3: DBScan
        Dim clusteringmethod = New Dictionary(Of Integer, String) From {{0, "dbscan"}, {1, "heirarchal"}, {2, "slink"}, {3, "dlink"}}
        Dim currenttab As Integer = clustertab.SelectedIndex
        c_method = clusteringmethod(currenttab)

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
            Me.MaximumSize = New Size(900, 615)
            Me.Size = New Size(643, Me.Size.Height)
            splcHELP.Panel2Collapsed = False
            btnSHHELP.Text = "<< Hide Help"
        End If

    End Sub

#End Region

    Private Sub LoadProgressForm()

        'Hide the Cluster Analysis for
        Me.Hide()

        Dim PARA As New IAPARAM

        PARA.sFLAYER = m_sIAFLayer
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
        PARA.sCPNUM = txtCPNUM.Text
        PARA.sOUT = txtOUT.Text

        RunPIA(PARA)

        'Close and dispose of form
        m_intersectForm = Nothing
        Me.Close()
        Me.Dispose()

    End Sub

    Private Sub RunPIA(ByVal IAF As IAPARAM)

        Dim f100 As String = Space(3) & "|" & "{0,-20}" & "{1}" & "{2,16:F4}" & "|"
        Dim f300 As String = Space(3) & "|" & StrDup(37, "-") & "|"
        Dim f400 As String = Space(3) & "|" & StrDup(37, "_") & "|"
        Dim f500 As String = Space(3) & StrDup(39, "_")
        Dim PRINTtxt As String = ""

        Dim pMxDoc As IMxDocument = My.ArcMap.Document
        Dim pFLayer As IFeatureLayer = GetFLayerByName(IAF.sFLAYER)
        Dim pFClass As IFeatureClass = pFLayer.FeatureClass
        Dim pDataset As IDataset = pFClass
        Dim pFDataset As IFeatureDataset = pFClass.FeatureDataset
        Dim pWrkspc2 As IWorkspace2 = DirectCast(pDataset.Workspace, IWorkspace2)
        Dim pSpatRef As ISpatialReference = GetFLayerSpatRef(pFLayer)
        Dim pGCS As IGeographicCoordinateSystem = GetGCS(pSpatRef)
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
        pProDlg.Title = "Primary Impact Approximation Tool"
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
        PRINTtxt += SumProgramHeader("Primary Impact Approximation Tool", _
                                     sSDate, sSTime)

        'Print parameters
        PRINTtxt += vbCrLf
        PRINTtxt += vbCrLf & " INPUT PARAMETERS"
        PRINTtxt += vbCrLf & " ----------------"
        PRINTtxt += vbCrLf & " Input polyline layer: " & IAF.sFLAYER
        PRINTtxt += vbCrLf & " Nearest Neighbor distance filter: <= " & IAF.sNQUERY & " m."
        Dim measName As String = "Planar"
        If IAF.bMEASGEO Then measName = "Geodetic"
        PRINTtxt += vbCrLf & " Measurement space: " & measName
        PRINTtxt += vbCrLf & " Cluster method:"
        If IAF.bCMS Then
            PRINTtxt += " Fixed distance: " & IAF.sCMSVAL & " m."
        ElseIf IAF.bCMBNND Then
            PRINTtxt += " Buffer option - Nearest Neighbor distance"
        ElseIf IAF.bCMBF Then
            PRINTtxt += " Buffer option - Nearest Neighbor distance x " & IAF.sCMBFVAL & " units"
        ElseIf IAF.bCMBS Then
            PRINTtxt += " Buffer option - Fixed distance: " & IAF.sCMBSVAL & " m."
        End If
        PRINTtxt += vbCrLf & " Intersection count filter: >= " & IAF.sCPNUM & " intersections"
        PRINTtxt += vbCrLf & " Output layer name: " & IAF.sOUT
        PRINTtxt += vbCrLf & vbCrLf

        'PROGRESS UPDATE: 
        pProDlg.Description = "Identifying polyline intersections..."
        PRINTtxt += vbCrLf & " [Identifying polyline intersections...]"

        Dim pFCursorA As IFeatureCursor = pFClass.Search(Nothing, False)
        Dim pFeatureA As IFeature = pFCursorA.NextFeature

        Dim ArOIDPairs As New List(Of PLineOIDPair)

        'Load the geometries into a stack to intersect symmetrically.
        Dim feats As New List(Of FeatureExtractions)
        Dim iflat As Double
        Dim oid As Integer
        Dim geom As IPolyline
        While Not pFeatureA Is Nothing
            oid = pFeatureA.OID
            geom = CType(pFeatureA.ShapeCopy, ESRI.ArcGIS.Geometry.IPolyline)
            'Try to grab the inverse flattening.
            Try
                iflat = CType(pFeatureA.Value(pFeatureA.Fields.FindField("iflat")), Double)
            Catch ex As Exception
                iflat = 1
            End Try
            feats.Add(New FeatureExtractions(oid, geom, iflat))
            pFeatureA = pFCursorA.NextFeature
        End While

        'Reset the cursors
        Dim pFCursor1 As IFeatureCursor = pFClass.Search(Nothing, False)
        Dim pFeature1 As IFeature = pFCursor1.NextFeature

        Dim iflat1 As Double
        Dim iflat2 As Double

        pTrkCan.Reset()
        While Not pFeature1 Is Nothing
            'Pop the top feature off the list
            feats.RemoveAt(0)
            Dim pPLine1 As IPolyline = CType(pFeature1.ShapeCopy, ESRI.ArcGIS.Geometry.IPolyline)
            Dim pRelOp As IRelationalOperator = CType(pPLine1, ESRI.ArcGIS.Geometry.IRelationalOperator)
            Dim pFCursor2 As IFeatureCursor = pFClass.Search(Nothing, False)
            Dim pFeature2 As IFeature = pFCursor2.NextFeature
            For i As Integer = 0 To feats.Count - 1
                Dim entry As FeatureExtractions
                entry = feats(i)
                If Not pRelOp.Disjoint(entry.geom) AndAlso Not pRelOp.Equals(entry.geom) Then
                    Try
                        iflat1 = CType(pFeature1.Value(pFeature1.Fields.FindField("iflat")), Double)
                        iflat2 = entry.iFlat
                    Catch ex As Exception
                        'This catches shapefiles without an iflat field.
                        iflat1 = 1
                        iflat2 = 1
                    End Try
                    ArOIDPairs.Add(New PLineOIDPair(pFeature1.OID, entry.OID, iflat1, iflat2))
                End If
            Next

            'While Not pFeature2 Is Nothing
            '    Dim pPLine2 As IPolyline = CType(pFeature2.ShapeCopy, ESRI.ArcGIS.Geometry.IPolyline)
            '    If Not pFeature1.OID = pFeature2.OID AndAlso Not pRelOp.Disjoint(pPLine2) Then
            '        Try
            '            iflat1 = CType(pFeature1.Value(pFeature1.Fields.FindField("iflat")), Double)
            '            iflat2 = CType(pFeature1.Value(pFeature2.Fields.FindField("iflat")), Double)
            '        Catch ex As Exception
            '            'This catches coincident lines when more than one trajectory file is provided.
            '            iflat1 = 1
            '            iflat2 = 1
            '        End Try
            '        ArOIDPairs.Add(New PLineOIDPair(pFeature1.OID, pFeature2.OID, iflat1, iflat2))
            '    End If
            '    pFeature2 = pFCursor2.NextFeature
            'End While
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

        Dim intersectioncount As Integer = ArOIDPairs.Count

        '>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        pStepPro.MaxRange = ArOIDPairs.Count
        pStepPro.StepValue = 1
        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

        ''PROGRESS UPDATE: 
        'pProDlg.Description = "Removing duplicate intersections..."
        'PRINTtxt += vbCrLf & " [Removing duplicate intersections...]"

        'Dim ArOIDPairsRem As New List(Of PLineOIDPair)

        'pTrkCan.Reset()
        'For Each OIDPair In ArOIDPairs
        '    p_pOIDPair = OIDPair
        '    Dim toRemove As PLineOIDPair = ArOIDPairs.Find(AddressOf FindReversedPair)
        '    If Not toRemove Is Nothing AndAlso ArOIDPairs.IndexOf(toRemove) > ArOIDPairs.IndexOf(OIDPair) Then
        '        ArOIDPairsRem.Add(toRemove)
        '    End If
        '    If Not pTrkCan.Continue Then
        '        'SUMMARY PRINT: End program as interrupted
        '        PRINTtxt += SumEndProgram("INTERRUPTED: Process interrupted by user.", _
        '                                  sSDate, sSTime)
        '        'Destroy the progress dialog
        '        ProgressDialogDispose(pProDlg, pStepPro, pTrkCan, pProDlgFact)
        '        If LogFileName.Text <> "" Then
        '            SaveLog(LogFileName.Text, PRINTtxt)
        '        End If
        '        Return
        '    End If
        'Next
        'Dim counter As Integer = 0
        'For Each OIDPair In ArOIDPairsRem
        '    counter = counter + 1
        '    ArOIDPairs.Remove(OIDPair)
        'Next
        'MsgBox(counter, MsgBoxStyle.Exclamation, "Coincident")
        'Dim sampleintersectioncount = ArOIDPairs.Count

        ''Log the intersection computation stats.
        'Dim report As String = ""
        'report = vbCrLf & vbCrLf & _
        '  Space(3) & "STATS: Trajectory Intersection " & vbCrLf & _
        '  (String.Format(f500, "_")) & vbCrLf & _
        '  (String.Format(f100, "Total Intersections", "=", intersectioncount)) & vbCrLf & _
        '  (String.Format(f100, "Coindicent Removed", "=", sampleintersectioncount)) & vbCrLf & _
        '  (String.Format(f100, "Percentage of Total", "=", sampleintersectioncount / intersectioncount * 100)) & vbCrLf & _
        '  (String.Format(f400, "_")) & vbCrLf

        'PRINTtxt += report

        '>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        pStepPro.MaxRange = ArOIDPairs.Count
        pStepPro.StepValue = 1
        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

        'PROGRESS UPDATE: 
        pProDlg.Description = "Intersecting polylines..."
        PRINTtxt += vbCrLf & " [Intersecting polylines...]"
        'This generates a list of intersection coordinates (points).
        Dim ArIntPts As New List(Of IntersectionPoint)
        Dim Weight As Double

        pTrkCan.Reset()
        For Each OIDPair In ArOIDPairs
            'Try
            Dim pF1 As IFeature = pFClass.GetFeature(OIDPair.OID1)
            Dim pF2 As IFeature = pFClass.GetFeature(OIDPair.OID2)
            Weight = 1 / (OIDPair.iflat1 * OIDPair.iflat2) 'inverse product of each ellipsoid iflat
            Dim pTopoOp As ITopologicalOperator = CType(pF1.ShapeCopy, ESRI.ArcGIS.Geometry.ITopologicalOperator)
            Dim pGC As IGeometryCollection = CType(pTopoOp.Intersect(pF2.ShapeCopy, esriGeometryDimension.esriGeometry0Dimension), ESRI.ArcGIS.Geometry.IGeometryCollection)
            ArIntPts.Add(New IntersectionPoint(CType(pGC.Geometry(0), ESRI.ArcGIS.Geometry.IPoint), Weight))
            'Catch ex As Exception
            'MsgBox("Failure on OID Pair" & OIDPair.OID1 & ", " & OIDPair.OID2, MsgBoxStyle.OkOnly, "Failure")
            'End Try

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

        'Empty lists
        ArOIDPairs = Nothing
        'ArOIDPairsRem = Nothing

        '------------------------------------------------------------------------------------------------------

        '   AID =   Array ID (or the 'cnt' value in iteration below)
        '   OID =   Object ID
        '   X   =   The x coordinate value
        '   Y   =   The y coordinate value
        '   CID =   Cluster ID
        '   CNT =   Number of points in cluster.
        '   W   =   The sum of the weights of each intersecting trajectory

        'Create an array with fields: AID,OID,X,Y,NDIST.
        Dim Ar(5, ArIntPts.Count - 1) As Double
        'Create an array with field: CID
        Dim Ar2(ArIntPts.Count - 1) As Double
        'Create an array with field: CNT
        Dim Ar3(ArIntPts.Count - 1) As Double

        'Add a check here for an excessive number of intersections.


        'PROGRESS UPDATE: 
        pProDlg.Description = "Extracting OID, X and Y values..."
        PRINTtxt += vbCrLf & " [Extracting OID, X and Y values...]"

        'Populate array with AID,OID,X,Y from all records in feature class.
        Dim cnt As Integer = 0
        For Each interPt In ArIntPts
            Ar(0, cnt) = cnt 'AID
            Ar(1, cnt) = cnt  'OID
            Dim pPoint As IPoint = interPt.Point
            If radMEASGEO.Checked Then
                pPoint.Project(pGCS)
            End If
            Ar(2, cnt) = pPoint.X 'X
            Ar(3, cnt) = pPoint.Y 'Y
            Ar(5, cnt) = interPt.Weight
            Ar2(cnt) = -1 'CID
            Ar3(cnt) = -1 'CNT
            cnt += 1
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

        Dim potential_origin_count As Integer = Ar.GetLength(0)

        'PROGRESS UPDATE: 
        pProDlg.Description = "Calculating Nearest Neighbor distances..."
        PRINTtxt += vbCrLf & " [Calculating Nearest Neighbor distances...]"

        'Populate array with NDIST for all records in feature class
        Dim dFeature1(2, 0) As Double
        Dim neighbor As Double = Nothing
        pTrkCan.Reset()
        For i As Integer = 0 To Ar.GetUpperBound(1)
            'Store the OID,X,Y of current feature
            dFeature1(0, 0) = Ar(1, i)
            dFeature1(1, 0) = Ar(2, i)
            dFeature1(2, 0) = Ar(3, i)
            'Assign the Nearest distance returned to the main table
            Ar(4, i) = FindNearest(Ar, dFeature1, dSemiMajAxis, dSemiMinAxis, IAF.bMEASPLAN) 'NDIST
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

        PRINTtxt = compute_intersection_stats(Ar, IAF.sNQUERY)

        'Clustering
        If c_method = "heirarchal" Then

            'If the the clustering method is 'Same distance'
            If IAF.bCMS = True Then

                'PROGRESS UPDATE: 
                pProDlg.Description = "Clustering calculation 'Same distance'..."
                PRINTtxt += vbCrLf & " [Clustering calculation 'Same distance'...]"

                Dim dGroup(3, 0) As Double
                'Main feature iteration
                pTrkCan.Reset()
                For j As Integer = 0 To Ar.GetUpperBound(1)
                    'If the main feature NDIST is less than or equal to the user
                    'distance query
                    If Ar(4, j) <= CDbl(IAF.sNQUERY) Then
                        'Leave a single blank line in the group table
                        ReDim dGroup(3, 0)
                        'Add the record to the potential cluster group
                        dGroup(0, 0) = Ar(0, j) 'AID
                        dGroup(1, 0) = Ar(1, j) 'OID
                        dGroup(2, 0) = Ar(2, j) 'X
                        dGroup(3, 0) = Ar(3, j) 'Y
                        'Current feature iteration: GROUP construction of features within Near distance
                        For k As Integer = 0 To Ar.GetUpperBound(1)
                            'If the OID of current feature does not equal OID of main feature
                            'and the distance threshold is that specified by user:
                            '1. If the OID of the main feature does not equal the OID the
                            '   current feature
                            '2. The distance between the main feature and the current feature
                            '   is less than or equal to the 'Same distance' value
                            If Not Ar(1, k) = Ar(1, j) And Ar(4, k) <= CDbl(IAF.sNQUERY) And _
                               GetDist(Ar(2, k), Ar(3, k), Ar(2, j), Ar(3, j), _
                                       dSemiMajAxis, dSemiMinAxis, IAF.bMEASPLAN) <= _
                               CDbl(IAF.sCMSVAL) Then
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
                        End If
                        Return
                    End If
                Next
                '********************************************************************************************
                'If the clustering method is 'Buffer: Nearest neighbor distance'
            ElseIf IAF.bCMBNND = True Then

                'PROGRESS UPDATE: 
                pProDlg.Description = "Clustering calculation 'Buffer: Nearest neighbor distance'..."
                PRINTtxt += vbCrLf & " [Clustering calculation 'Buffer: Nearest neighbor distance'...]"

                Dim dGroup(3, 0) As Double
                'Main feature iteration
                pTrkCan.Reset()
                For j As Integer = 0 To Ar.GetUpperBound(1)
                    'If the main feature NDIST is less than or equal to the user
                    'distance query
                    If Ar(4, j) <= CDbl(IAF.sNQUERY) Then
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
                            If Not Ar(1, k) = Ar(1, j) And Ar(4, k) <= CDbl(IAF.sNQUERY) And _
                               GetDist(Ar(2, k), Ar(3, k), Ar(2, j), Ar(3, j), _
                                       dSemiMajAxis, dSemiMinAxis, IAF.bMEASPLAN) <= _
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
                        End If
                        Return
                    End If
                Next
                '********************************************************************************************
                'If the clustering method is 'Buffer: NND x factor'
            ElseIf IAF.bCMBF = True Then

                'PROGRESS UPDATE: 
                pProDlg.Description = "Clustering calculation 'Buffer: NND x factor'..."
                PRINTtxt += vbCrLf & " [Clustering calculation 'Buffer: NND x factor'...]"

                Dim dGroup(3, 0) As Double
                'Main feature iteration
                pTrkCan.Reset()
                For j As Integer = 0 To Ar.GetUpperBound(1)
                    'If the main feature NDIST is less than or equal to the user
                    'distance query
                    If Ar(4, j) <= CDbl(IAF.sNQUERY) Then
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
                            If (Not Ar(1, k) = Ar(1, j) And Ar(4, k) <= CDbl(IAF.sNQUERY) And _
                                 GetDist(Ar(2, k), Ar(3, k), Ar(2, j), Ar(3, j), _
                                         dSemiMajAxis, dSemiMinAxis, IAF.bMEASPLAN) <= _
                                 (Ar(4, k) * CDbl(IAF.sCMBFVAL)) + (Ar(4, j) * CDbl(IAF.sCMBFVAL))) Or _
                               (Not Ar(1, k) = Ar(1, j) And GetDist(Ar(2, k), Ar(3, k), Ar(2, j), Ar(3, j), _
                                                                    dSemiMajAxis, dSemiMinAxis, IAF.bMEASPLAN) <= _
                                (Ar(4, j) * CDbl(IAF.sCMBFVAL))) Then
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
                    If Ar(4, j) <= CDbl(IAF.sNQUERY) Then
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
                            If (Not Ar(1, k) = Ar(1, j) And Ar(4, k) <= CDbl(IAF.sNQUERY) And _
                                 GetDist(Ar(2, k), Ar(3, k), Ar(2, j), Ar(3, j), _
                                         dSemiMajAxis, dSemiMinAxis, IAF.bMEASPLAN) <= _
                                 (CDbl(IAF.sCMBSVAL) * 2)) Or _
                               (Not Ar(1, k) = Ar(1, j) And GetDist(Ar(2, k), Ar(3, k), Ar(2, j), Ar(3, j), _
                                                                    dSemiMajAxis, dSemiMinAxis, IAF.bMEASPLAN) <= _
                                CDbl(IAF.sCMBSVAL)) Then
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
                        End If
                        Return
                    End If
                Next
            End If
            '********************************************************************************************
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
                
                'Remove the node from the unvisited list and the node from the dist_lists
                Unvisited.RemoveAt(index)

                'Get the index from dist_lists from the node_id
                Dim node_index As Integer = 0
                node_index = GetNodeIndex(node)

                'Get the neighbors to the current node
                Dim neighbors = getNeighbors(CDbl(eps.Text), node_index, dSemiMajAxis, dSemiMinAxis, measurement_space)

                'If we are greater than epsilon we have a cluster, otherwise we have noise.  Unmarked nodes are implicitly noise.
                If neighbors.Count >= CInt(minpts.Text) Then 'neighbors includes the source point in the list

                    'Attempt to expand the cluster
                    Unvisited = ExpandCluster(neighbors, cluster_id, CDbl(eps.Text), CInt(minpts.Text), Ar2, Unvisited, dSemiMajAxis, dSemiMinAxis, measurement_space)

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

        'Add the CID of the main feature to the master CID list
        Dim dCIDList(Ar.GetUpperBound(1)) As Double
        System.Array.Copy(Ar2, 0, dCIDList, 0, Ar2.Length)
        'Sort the CID master list
        System.Array.Sort(dCIDList)

        'Check to ensure that we have at least one intersection.  If not, exit
        Dim cluster As Boolean = False
        For j As Integer = 0 To Ar2.GetUpperBound(0)
            If Ar2(j) <> -1 Then cluster = True
        Next
        If cluster = False Then
            MsgBox("No intersection clusters generated.  Try loosening the constraints.", MsgBoxStyle.Exclamation, "Clustering Error - No Clusters")
            ProgressDialogDispose(pProDlg, pStepPro, pTrkCan, pProDlgFact)
            Return
        End If

        'PROGRESS UPDATE: 
        pProDlg.Description = "Counting number of intersections per cluster..."
        PRINTtxt += vbCrLf & " [Counting number of intersections per cluster...]"

        'Get the number of CID occurances from the CID master list for each main feature CID
        pTrkCan.Reset()
        For n As Integer = 0 To Ar.GetUpperBound(1)
            'If the main feature has no CID, skip it
            If Ar2(CInt(Ar(0, n))) <> -1 Then
                'Get the first and last occurance of the main feature CID
                'from the CID master list
                Dim n1stIndex, nLastIndex As Integer
                n1stIndex = System.Array.IndexOf(dCIDList, Ar2(CInt(Ar(0, n)))) + 1
                nLastIndex = System.Array.LastIndexOf(dCIDList, Ar2(CInt(Ar(0, n)))) + 1
                'Calculate the number of main feature CID occurrances 
                'from the first and last index of that CID on the master CID list
                Ar3(CInt(Ar(0, n))) = (nLastIndex - n1stIndex) + 1
            End If
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
        pProDlg.Description = "Preparing clustered points..."
        PRINTtxt += vbCrLf & " [Preparing clustered points...]"

        Dim ArCPoints As New List(Of ClusterPoint)

        pTrkCan.Reset()
        For o As Integer = 0 To Ar.GetUpperBound(1)
            Dim iCID As Integer = Ar2(Ar(0, o))
            Dim iCnt As Integer = Ar3(Ar(0, o))
            Dim pPoint As IPoint = ArIntPts.Item(Ar(1, o)).Point
            Dim pt_weights As Double = ArIntPts.Item(Ar(1, o)).Weight
            ArCPoints.Add(New ClusterPoint(pPoint.X, pPoint.Y, iCID, iCnt, pt_weights))
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

        'The threshold number is dependent on the clustering method selected.
        Dim threshold_number As Integer = 0
        If c_method = "dbscn" Then
            threshold_number = CInt(eps.Text)
        Else
            threshold_number = CInt(IAF.sCPNUM)
        End If
        'Get intersections per cluster statistics and write to log.
        PRINTtxt += intersectionstats(Ar, Ar2, Ar3, threshold_number)

        p_cPnum = CInt(IAF.sCPNUM)
        ArCPoints.RemoveAll(AddressOf FindCPointByPntCount)

        If ArCPoints.Count <= 0 Then
            'SUMMARY PRINT: End program as interrupted
            PRINTtxt += SumEndProgram("INTERRUPTED: There were no clusters with " & _
                                      IAF.sCPNUM & " or more points.", _
                                      sSDate, sSTime)
            'Destroy the progress dialog
            ProgressDialogDispose(pProDlg, pStepPro, pTrkCan, pProDlgFact)
            If LogFileName.Text <> "" Then
                SaveLog(LogFileName.Text, PRINTtxt)
            End If
            Return
        End If

        'PROGRESS UPDATE: 
        pProDlg.Description = "Extracting unique cluster IDs..."
        PRINTtxt += vbCrLf & " [Extracting unique cluster IDs...]"

        Dim ArCIDs As New List(Of Integer)

        pTrkCan.Reset()
        For Each pCPoint In ArCPoints
            Dim newCID As Integer = pCPoint.CID
            If Not ArCIDs.Contains(newCID) Then
                ArCIDs.Add(newCID)
            End If
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

        'Remove the invalid CID value: -1
        ArCIDs.Remove(-1)

        '>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        pStepPro.MaxRange = ArCIDs.Count
        pStepPro.StepValue = 1
        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

        'PROGRESS UPDATE: 
        pProDlg.Description = "Generating geometries..."
        PRINTtxt += vbCrLf & " [Generating geometries..]"

        'List to store centroids
        Dim ArCentroids As New List(Of ClusterPoint)

        pTrkCan.Reset()
        Dim mean_weight As Double
        For Each iCID In ArCIDs
            mean_weight = 0
            'Get points with current CID
            p_CID = iCID
            Dim newCPoints As List(Of ClusterPoint) = ArCPoints.FindAll(AddressOf FindCPointByCID)
            'Add points to a point collection
            Dim pPColl4 As IPointCollection4 = New MultipointClass()
            Dim cluster_weights As New List(Of Double)
            For Each newCPoint In newCPoints
                Dim pPoint As New PointClass()
                pPoint.PutCoords(newCPoint.X, newCPoint.Y)
                pPColl4.AddPoint(pPoint)
                If Double.IsInfinity(newCPoint.Weight) Then
                    cluster_weights.Add(1)
                Else
                    cluster_weights.Add(newCPoint.Weight)
                End If

            Next
            'Get the centroid for the point collection
            Dim weighted As Boolean = False
            If weightedCentroids.Checked = True Then weighted = True
            'ERROR OCCURS HERE IF WEIGHTED IS TRUE AND IFLAT IS NOT IN THE TABLE
            Dim dCentCoord As Lat2BLon2B = GetCentroid(pPColl4, cluster_weights, weighted)
            Dim dStats As Lat2BLon2B = GetClusterPointsFromCentroidStats(pPColl4, dCentCoord, _
                                                                         dSemiMajAxis, dSemiMinAxis, _
                                                                         IAF.bMEASPLAN, cluster_weights, mean_weight)
            ArCentroids.Add(New ClusterPoint(dCentCoord.Lon2B, dCentCoord.Lat2B, _
                                             iCID, pPColl4.PointCount, dStats.Lat2B, dStats.Lon2B, mean_weight))
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
        pProDlg.Description = "Creating point feature class..."
        PRINTtxt += vbCrLf & " [Creating point feature class...]"

        Dim pNewReqFields As IFields = GetClusterReqFields(True, True)
        Dim pNewFLayer As IFeatureLayer = New FeatureLayerClass()
        pNewFLayer.FeatureClass = CreateFeatureClass(pWrkspc2, pFDataset, IAF.sOUT, _
                                                     pNewReqFields, _
                                                     esriGeometryType.esriGeometryPoint, _
                                                     pSpatRef)
        pNewFLayer.Name = pNewFLayer.FeatureClass.AliasName
        Dim pNewFClass As IFeatureClass = pNewFLayer.FeatureClass

        Dim pNewFCursor As IFeatureCursor = pNewFClass.Insert(True)

        '>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        pStepPro.MaxRange = ArCentroids.Count
        pStepPro.StepValue = 1
        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

        'Begin edit session and operation
        Dim pEditor As IEditor = My.ArcMap.Editor
        pEditor.StartEditing(pWrkspc2)
        pEditor.StartOperation()

        'PROGRESS UPDATE: 
        pProDlg.Description = "Storing point features..."
        PRINTtxt += vbCrLf & " [Storing point features...]"

        'Update the features with the values computed above
        pTrkCan.Reset()
        For Each Centroid In ArCentroids
            'Create the feature buffer
            Dim pNewFeatureBuff As IFeatureBuffer = pNewFClass.CreateFeatureBuffer
            Dim pPoint As IPoint = New PointClass()
            pPoint.PutCoords(Centroid.X, Centroid.Y)
            If IAF.bMEASGEO Then
                pPoint.SpatialReference = pGCS
                pPoint.Project(pSpatRef)
            Else
                pPoint.SpatialReference = pSpatRef
            End If
            pNewFeatureBuff.Shape = pPoint
            pNewFeatureBuff.Value(pNewFeatureBuff.Fields.FindField("cid")) = Centroid.CID
            pNewFeatureBuff.Value(pNewFeatureBuff.Fields.FindField("cnt")) = Centroid.Count
            pNewFeatureBuff.Value(pNewFeatureBuff.Fields.FindField("distmean")) = Centroid.Mean
            pNewFeatureBuff.Value(pNewFeatureBuff.Fields.FindField("diststdev")) = Centroid.Stdev
            pNewFeatureBuff.Value(pNewFeatureBuff.Fields.FindField("mean_iflat")) = Centroid.Weight
            pNewFCursor.InsertFeature(pNewFeatureBuff)
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
        pEditor.StopOperation("Primary Impact Approximation process")
        StopEditSession(True)

        'Add the new layer to the map
        Dim pNewLayer As ILayer = pNewFLayer
        pMxDoc.ActiveView.FocusMap.AddLayer(pNewLayer)


        'Refresh the Toc and Map
        pMxDoc.UpdateContents()
        pMxDoc.ActiveView.Refresh()

        'SUMMARY PRINT: End program as complete
        PRINTtxt += SumEndProgram("COMPLETE: Primary Impact Approximation process complete.", _
                                  sSDate, sSTime)

        'Destroy the progress dialog
        ProgressDialogDispose(pProDlg, pStepPro, pTrkCan, pProDlgFact)
        If LogFileName.Text <> "" Then
            SaveLog(LogFileName.Text, PRINTtxt)
        End If

    End Sub

    Private Function GetClusterPointsFromCentroidStats(ByVal pPtColl As IPointCollection, _
                                                     ByVal dCentroid As Lat2BLon2B, _
                                                     ByVal dSemiMajAxis As Double, _
                                                     ByVal dSemiMinAxis As Double, _
                                                     ByVal bPlan As Boolean, _
                                                     ByVal weights As List(Of Double), _
                                                     ByRef mean_weight As Double) As Lat2BLon2B

        Dim ArDists(pPtColl.PointCount - 1) As Double
        Dim dDistSum As Double = 0
        Dim sum_weights As Double = 0
        For i As Integer = 0 To pPtColl.PointCount - 1
            sum_weights = sum_weights + weights(i)
            Dim pPoint As IPoint = pPtColl.Point(i)
            ArDists(i) = GetDist(dCentroid.Lon2B, dCentroid.Lat2B, _
                                pPoint.X, pPoint.Y, _
                                dSemiMajAxis, dSemiMinAxis, bPlan)
            'Sum the distances
            dDistSum += ArDists(i)
        Next

        'Calculate mean
        Dim dDistMean As Double = dDistSum / pPtColl.PointCount

        'Calculate mean weight
        mean_weight = sum_weights / pPtColl.PointCount

        'Get the sum of the deviations
        Dim dSumStds As Double = 0
        For i = 0 To ArDists.Length - 1
            Dim dStd As Double = (ArDists(i) - dDistMean) ^ 2
            dSumStds += dStd
        Next

        'Standard deviation
        Dim dDistStd As Double = Math.Sqrt(dSumStds / (ArDists.Length))

        Dim pReturn As New Lat2BLon2B
        pReturn.Lat2B = dDistMean
        pReturn.Lon2B = dDistStd

        Return pReturn

    End Function

    Private Function GetCentroid(ByVal pPointCollection As IPointCollection, ByVal weights As List(Of Double), ByVal weighted As Boolean) As Lat2BLon2B
        'Computes the weighted centroid for each cluster of points.

        Dim i As Integer
        Dim dX As Double
        Dim dY As Double
        Dim dXSum As Double
        Dim dYSum As Double
        Dim W As Integer
        Dim counter As Integer = 0
        If weighted = True Then
            'Weighted Centroid
            For i = 0 To pPointCollection.PointCount - 1
                dX = pPointCollection.Point(i).X
                dY = pPointCollection.Point(i).Y
                W = CInt(weights(i) * 100) 'Scale to nondecimal.
                If W = 0 Then
                    'If the weighted centroid is checked but no weight field exists.
                    W = 1
                End If
                counter = counter + W
                Do Until W = 0
                    dXSum = dXSum + dX
                    dYSum = dYSum + dY
                    W = W - 1
                Loop
            Next
        Else
            For i = 0 To pPointCollection.PointCount - 1
                dX = pPointCollection.Point(i).X
                dY = pPointCollection.Point(i).Y
                counter = counter + 1
                dXSum = dXSum + dX
                dYSum = dYSum + dY
            Next
        End If

        'Calculate Mean Centers for X and Y
        Dim dCenterX, dCenterY As Double
        dCenterX = dXSum / counter
        dCenterY = dYSum / counter

        Dim pReturnPair As New Lat2BLon2B
        pReturnPair.Lon2B = dCenterX
        pReturnPair.Lat2B = dCenterY

        Return pReturnPair

    End Function

    Private p_pOIDPair As PLineOIDPair
    Public Function FindReversedPair(ByVal pair As PLineOIDPair) As Boolean
        If p_pOIDPair.OID1 = pair.OID2 And p_pOIDPair.OID2 = pair.OID1 Then
            Return True
        Else
            Return False
        End If
    End Function

    Private p_cPnum As Integer
    Public Function FindCPointByPntCount(ByVal cPoint As ClusterPoint) As Boolean
        If cPoint.Count >= p_cPnum Then
            Return False
        Else
            Return True
        End If
    End Function

    Private p_CID As Integer
    Public Function FindCPointByCID(ByVal cPoint As ClusterPoint) As Boolean
        If cPoint.CID = p_CID Then
            Return True
        Else
            Return False
        End If
    End Function


#Region "*** HELP CONTENT DISPLAY DYNAMICS ****************************************************"
#End Region

#Region "***** FORM *********"
#End Region
    Private Sub Frm_IntersectionAnalysis_Click(ByVal sender As Object, _
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
    Private Sub lblLAYER_Click(ByVal sender As Object, _
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
    Private Sub grpNQUERY_Enter(ByVal sender As Object, _
                              ByVal e As System.EventArgs) _
                               Handles grpNQUERY.Enter
        HELP_Near()
    End Sub

    Private Sub grpNQUERY_Click(ByVal sender As System.Object, _
                               ByVal e As System.EventArgs) _
                               Handles grpNQUERY.Click
        HELP_Near()
    End Sub

#Region "***** MEASUREMENT SPACE *******"
#End Region

    Private Sub grpMEASSPACE_Click(sender As System.Object, _
                           e As System.EventArgs) _
                           Handles grpMEASSPACE.Click
        HELP_MeasurementSpace()
    End Sub

    Private Sub grpMEASSPACE_Enter(sender As System.Object, _
                               e As System.EventArgs) _
                               Handles grpMEASSPACE.Enter
        HELP_MeasurementSpace()
    End Sub




#Region "***** POINT QUERY *********"
#End Region

    Private Sub grpCPNUM_Enter(ByVal sender As System.Object, _
                                ByVal e As System.EventArgs)

        HELP_PntCount()
    End Sub

    Private Sub grpCPNUM_Click(ByVal sender As System.Object, _
                                ByVal e As System.EventArgs)

        HELP_PntCount()
    End Sub

#Region "***** OUTPUT ********"
#End Region

    Private Sub grpOUT_Enter(sender As System.Object, _
                             e As System.EventArgs) _
                             Handles grpOUT.Enter
        HELP_Out()
    End Sub

    Private Sub grpOUT_Click(sender As System.Object, _
                         e As System.EventArgs) _
                         Handles grpOUT.Click
        HELP_Out()
    End Sub

#Region "*** HELP PANEL UPDATE CONTENT ********************************************************"
#End Region

    Private Sub HELP_Form()

        'Update help panel
        Dim strText As String = _
          "Identifies trajectory intersections, computes intersection clusters, and outputs " & _
          "cluster centroid points." & _
          vbCrLf & vbCrLf & _
          "Requirement: The input layer must be projected in meters."

        HELPCntUpdate("Primary Impact Approximation Tool", strText)

    End Sub

    Private Sub HELP_Layer()

        'Update help panel
        Dim strText As String = _
          "The trajectory poyline layer." & _
          vbCrLf & vbCrLf & _
          "Requirement: The input layer must be projected in meters."

        HELPCntUpdate("Input Features", strText)

    End Sub

    Private Sub HELP_Near()

        'Update help panel
        Dim strText As String = _
            "Intersections with a Nearest Neighbor distance outside this threshold are excluded from " & _
            "the cluster analysis."

        HELPCntUpdate("Nearest Neighbor distance filter", strText)

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


   

    Private Sub HELP_PntCount()

        'Update help panel
        Dim strText As String = _
            "Clusters with a number of intersections outside this threshold are excluded from " & _
            "the analysis."

        HELPCntUpdate("Intersection count filter", strText)

    End Sub

    Private Sub HELP_Out()

        'Update help panel
        Dim strText As String = _
            "The output point layer name."

        HELPCntUpdate("Output layer name", strText)

    End Sub


    Private Function compute_intersection_stats(ByVal Ar, ByVal dist) As String
        Dim count, sum, sample, range, mean, std, median, min, max, iqr, uq, lq, sumsq, array() As Double

        count = 0
        sample = 0
        max = 0
        min = 9999999
        sum = 0
        For i As Integer = 0 To Ar.GetUpperBound(1) - 1
            count += 1
            sum += Ar(4, i)
            If Ar(4, i) > max Then max = Ar(4, i)
            If Ar(4, i) < min Then min = Ar(4, i)
            If Ar(4, i) <= dist Then sample += 1
        Next
        range = max - min
        mean = sum / count

        'Variance & STD
        sumsq = 0
        ReDim array(count)
        For i As Integer = 0 To Ar.GetUpperBound(1) - 1
            sumsq += (Ar(4, i) - mean) ^ 2
            array(i) = Ar(4, i)
        Next
        std = Sqrt(sumsq / count)

        System.Array.Sort(array)

        If IEEERemainder(count, 2) = 0 Then
            median = (array(Round(count * 0.5)) + array(Round((count * 0.5) + 1))) / 2
            lq = (array(Round(count * 0.25)) + array(Round((count * 0.25) + 1))) / 2
            uq = (array(Round(count * 0.75)) + array(Round((count * 0.75) + 1))) / 2
        Else
            median = array(Round(count * 0.5))
            lq = array(Round(count * 0.25))
            uq = array(Round(count * 0.75))
        End If

        iqr = uq - lq

        'Write the stats out
        Dim report As String = ""
        Dim f100 As String = Space(3) & "|" & "{0,-20}" & "{1}" & "{2,16:F4}" & "|"
        Dim f300 As String = Space(3) & "|" & StrDup(37, "-") & "|"
        Dim f400 As String = Space(3) & "|" & StrDup(37, "_") & "|"
        Dim f500 As String = Space(3) & StrDup(39, "_")

        report = vbCrLf & vbCrLf & _
          Space(3) & "STATS: Intersections " & vbCrLf & _
          (String.Format(f500, "_")) & vbCrLf & _
          (String.Format(f100, "Population Total", "=", count)) & vbCrLf & _
          (String.Format(f100, "Inter. Within Dist.", "=", sample)) & vbCrLf & _
          (String.Format(f100, "Percentage of Total", "=", sample / count * 100)) & vbCrLf & _
          (String.Format(f300, "-")) & vbCrLf & _
          (String.Format(f100, "  0% Min", "=", min)) & vbCrLf & _
          (String.Format(f100, " 25% Lower quartile", "=", lq)) & vbCrLf & _
          (String.Format(f100, " 50% Median", "=", median)) & vbCrLf & _
          (String.Format(f100, " 75% Upper quartile", "=", uq)) & vbCrLf & _
          (String.Format(f100, "100% Max", "=", max)) & vbCrLf & _
          (String.Format(f300, "-")) & vbCrLf & _
          (String.Format(f100, "Range", "=", range)) & vbCrLf & _
          (String.Format(f100, "Mean", "=", mean)) & vbCrLf & _
          (String.Format(f100, "St Dev", "=", std)) & vbCrLf & _
          (String.Format(f100, "Interquartile range", "=", iqr)) & vbCrLf & _
          (String.Format(f400, "_")) & vbCrLf


        Return report
    End Function

    Private Function intersectionstats(ByVal Ar(,) As Double, ByVal Ar2() As Double, ByVal Ar3() As Double, ByVal cluster_threshold As Integer) As String
        Dim dCount, dCountout, n, dRange, dMean, dStd, dIqr, dMin, dMax, dSumsq, dArray(), _
     dMedian, dLQ, dUQ As Double

        Dim clus As Dictionary(Of Integer, Integer) = New Dictionary(Of Integer, Integer)

        dCount = 0
        dMax = 0
        dMin = 9999999

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
                End If
            End If
        Next

        dRange = dMax - dMin
        dMean = dCount / clus.Count 'Total number of craters / total number clusters

        'Variance and STD

        dSumsq = 0
        ReDim dArray(clus.Count)
        'Iterate over keys to get values here
        Dim pair As KeyValuePair(Of Integer, Integer)
        n = 0
        For Each pair In clus
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

        dCountout = 0
        For Each pair In clus
            If pair.Value > cluster_threshold Then dCountout += 1
        Next

        'Write the stats out
        Dim sReport As String = ""
        Dim f100 As String = Space(3) & "|" & "{0,-20}" & "{1}" & "{2,16:F4}" & "|"
        Dim f300 As String = Space(3) & "|" & StrDup(37, "-") & "|"
        Dim f400 As String = Space(3) & "|" & StrDup(37, "_") & "|"
        Dim f500 As String = Space(3) & StrDup(39, "_")

        sReport = vbCrLf & vbCrLf & _
          Space(3) & "STATS: Intersection Clusters " & vbCrLf & _
          (String.Format(f500, "_")) & vbCrLf & _
          (String.Format(f100, "Population Total", "=", dCount)) & vbCrLf & _
          (String.Format(f100, "Clus. > 10 Craters", "=", dCountout)) & vbCrLf & _
          (String.Format(f100, "Percentage of Total", "=", dCountout / clus.Count * 100)) & vbCrLf & _
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
#Region "Clustering"
    Private Sub clustmeth_grp_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles clustmeth_grp.Click
        Help_ClusterMethod()
    End Sub

    Private Sub clustertab_h_Click(ByVal Sender As System.Object, ByVal e As System.EventArgs) Handles hierarchical.Click
        Help_ClusterMethod()
    End Sub

    Private Sub clustertab_d_Click(ByVal Sender As System.Object, ByVal e As System.EventArgs) Handles dbscan.Click
        Help_ClusterMethod()
    End Sub

    Private Sub clustertab_h_Focus(ByVal Sender As System.Object, ByVal e As System.EventArgs) Handles hierarchical.GotFocus
        Help_ClusterMethod()
    End Sub

    Private Sub clustertab_d_Focus(ByVal Sender As System.Object, ByVal e As System.EventArgs) Handles dbscan.GotFocus
        Help_ClusterMethod()
    End Sub

    Private Sub DBScan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dbscan.Click
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

#Region "Weighted Help"
    Private Sub GroupBox2_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox2.Enter
        HELP_weighted()
    End Sub

    Private Sub GroupBox2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox2.Click
        HELP_weighted()
    End Sub

    Private Sub weightedCentroids_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles weightedCentroids.CheckedChanged
        HELP_weighted()
    End Sub

    Private Sub HELP_weighted()
        'Update help panel
        Dim strText As String = _
            "If the input polyline layer contains an inverse flattening attribute weight the center of the intersections based upon (1 / iflat1 * iflat2)." & _
            Environment.NewLine & _
            "The means that two intersecting lines with lower inverse flattenings will pull the centroid of the global population of intersections closer than two lines with higher inverse flattenings." & _
            Environment.NewLine & _
            "In testing this results in a shift on the order of 1km for most observations.  This is clearly data dependent though."

        HELPCntUpdate("Weighted Centroids", strText)
    End Sub
#End Region

#Region "Log File"
    Private Sub log_grp_Enter(ByVal sender As System.Object, _
                             ByVal e As System.EventArgs)

        HELP_Out()
    End Sub

    Private Sub log_name_Click(ByVal sender As System.Object, _
                             ByVal e As System.EventArgs)

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

End Class