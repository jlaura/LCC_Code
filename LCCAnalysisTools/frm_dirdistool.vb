Imports System.Windows.Forms
Imports System.Drawing
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Editor
Imports System.Math

Public Class frm_dirdistool

    Private Sub frm_dirdistool_Load(sender As System.Object,
                                    e As System.EventArgs) _
                                Handles MyBase.Load

        'Get the editor object by its extension name
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
                'Add only point layers
                If pFLayer.FeatureClass.ShapeType = _
                ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPoint Then
                    cboLAYER.Items.Add(pLayer.Name)
                End If
                'Next layer
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

        'If there is no selected layer
        If cboLAYER.SelectedIndex = -1 Then
            m_sDDFLayer = Nothing
        End If

    End Sub

    Private Sub cboLAYER_SelectedIndexChanged _
                                (ByVal sender As System.Object, _
                                 ByVal e As System.EventArgs) _
                                 Handles cboLAYER.SelectedIndexChanged

        'If an input layer is selected
        If Not cboLAYER.SelectedIndex = -1 Then
            'Assign layer name to private global variable
            m_sDDFLayer = cboLAYER.Text
        Else
            m_sDDFLayer = Nothing
        End If

    End Sub

    Private Sub TextBoxes_Update(sender As Object, _
                                 e As System.EventArgs) _
                             Handles txtDDPNUM.Leave
        Try
            If CInt(txtDDPNUM.Text) > 0 Then txtDDPNUM.Text = _
                                    CInt(txtDDPNUM.Text).ToString
        Catch ex As Exception
            MsgBox("Please enter a 'Point count filter' value greater than 0.", _
                   MsgBoxStyle.Exclamation, _
                   "Invalid Parameter")
            txtDDPNUM.Text = "10"
        End Try

    End Sub

    Private Sub Optimize_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optimize.Click
        Dim pFLayer As IFeatureLayer = GetFLayerByName(m_sDDFLayer)

        'Make sure an input layer was selected and assigned to an object
        If cboLAYER.SelectedIndex = -1 Or pFLayer Is Nothing Then
            MsgBox("Please select an 'Input point layer'.", MsgBoxStyle.Exclamation, _
                   "Missing Parameter")
            Return
        End If
        Dim pFClass As IFeatureClass = pFLayer.FeatureClass
        Dim pDataset As IDataset = pFClass
        Dim pWrkspc2 As IWorkspace2 = DirectCast(pDataset.Workspace, IWorkspace2)

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
        Dim CntArray As New Dictionary(Of Integer, Integer)

        Dim pFCursor1 As IFeatureCursor = pFClass.Search(Nothing, False) 'The input layer is accessed viat pFClass, which is defined above via PFLayer, which is accessed via a global which stores the layer name.  From the input form.
        Dim pFeature1 As IFeature = pFCursor1.NextFeature

        While Not pFeature1 Is Nothing
            Dim cid As Integer = CType(pFeature1.Value(pFeature1.Fields.FindField("cid")), Integer)
            Dim cnt As Integer = CType(pFeature1.Value(pFeature1.Fields.FindField("cnt")), Integer)
            If Not CntArray.ContainsKey(cid) Then
                CntArray.Add(cid, cnt)
            End If
            pFeature1 = pFCursor1.NextFeature()

            If Not pTrkCan.Continue Then
                'Destroy the progress dialog
                ProgressDialogDispose(pProDlg, pStepPro, pTrkCan, pProDlgFact)
                Return
            End If
        End While

        'Get the mean cluster size
        Dim pair As KeyValuePair(Of Integer, Integer)
        Dim sum As Integer = 0
        For Each pair In CntArray
            sum = sum + pair.Value
        Next

        Dim Mean As Double = sum / CntArray.Count
        'Populate the mininum distance text box with the optimized distance

        txtDDPNUM.Text = CStr(Math.Round(Mean, 0))
        
        'Destroy the progress dialog
        ProgressDialogDispose(pProDlg, pStepPro, pTrkCan, pProDlgFact)

    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object,
                            ByVal e As System.EventArgs) _
                        Handles btnOK.Click

        'Check for errors in all field values
        FormErrorHandler()

    End Sub

    Private Sub LogSaveDialogDir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LogSaveDialog.Click
        Dim saveFileDialog1 As New SaveFileDialog()

        saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"
        saveFileDialog1.FilterIndex = 2
        saveFileDialog1.RestoreDirectory = True

        If saveFileDialog1.ShowDialog() = DialogResult.OK Then
            LogFileName.Text = saveFileDialog1.FileName
        End If
    End Sub

    Private Sub FormErrorHandler()

        Dim pFLayer As IFeatureLayer = GetFLayerByName(m_sDDFLayer)

        'Make sure an input layer was selected and assigned to an object
        If cboLAYER.SelectedIndex = -1 Or pFLayer Is Nothing Then
            MsgBox("Please select an 'Input point layer'.", MsgBoxStyle.Exclamation, _
                   "Missing Parameter")
            Return
        End If

        Dim pFClass As IFeatureClass = pFLayer.FeatureClass
        Dim pDataset As IDataset = pFClass
        Dim pWrkspc2 As IWorkspace2 = DirectCast(pDataset.Workspace, IWorkspace2)

        'Point count filter' number required
        If CInt(txtDDPNUM.Text) <= 0 Then
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

        If CInt(std.Text) <= 0 Then
            MsgBox("Please enter an integer greater than or equal to 1.", MsgBoxStyle.Exclamation, "Standard Deviation Error")
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
            Me.Size = New Size(643, Me.Size.Height)
            splcHELP.Panel2Collapsed = False
            btnSHHELP.Text = "<< Hide Help"
        End If

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

    Private Sub LoadProgressForm()

        'Hide the Directional Distribution Tool form
        Me.Hide()

        'Define parameter bundle
        Dim PARA As New DDPARAM
        PARA.sFLAYER = m_sDDFLayer
        PARA.sDDPNUM = txtDDPNUM.Text
        PARA.bOUTLINE = radOUTLINE.Checked
        PARA.bOUTELLIPSE = radOUTELLIPSE.Checked
        PARA.sDDOUT = txtOUT.Text

        'Run the directional distribution analysis program
        RunDirDis(PARA)

        'Close and dispose of form
        m_dirdisForm = Nothing
        Me.Close()
        Me.Dispose()

    End Sub

    Private Sub RunDirDis(ByVal DD As DDPARAM)

        Dim PRINTtxt As String = ""

        Dim pMxDoc As IMxDocument = My.ArcMap.Document
        Dim pFLayer As IFeatureLayer = GetFLayerByName(DD.sFLAYER)
        Dim pFClass As IFeatureClass = pFLayer.FeatureClass
        Dim pDataset As IDataset = pFClass
        Dim pFDataset As IFeatureDataset = pFClass.FeatureDataset
        Dim pWrkspc2 As IWorkspace2 = DirectCast(pDataset.Workspace, IWorkspace2)
        Dim pSpatRef As ISpatialReference = GetFLayerSpatRef(pFLayer)
        Dim pGCS As IGeographicCoordinateSystem = GetGCS(pSpatRef)

        '>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        ' Create a CancelTracker
        Dim pTrkCan As ITrackCancel = New CancelTracker

        ' Create the ProgressDialog. This automatically displays the dialog
        Dim pProDlgFact As IProgressDialogFactory = New ProgressDialogFactory
        Dim pProDlg As IProgressDialog2 = pProDlgFact.Create(pTrkCan, My.ArcMap.Application.hWnd)

        ' Set the properties of the ProgressDialog
        pProDlg.CancelEnabled = True
        pProDlg.Title = "Directional Distribution Tool"
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
        PRINTtxt += SumProgramHeader("Directional Distribution Tool", _
                                     sSDate, sSTime)

        'Print parameters
        PRINTtxt += vbCrLf
        PRINTtxt += vbCrLf & " INPUT PARAMETERS"
        PRINTtxt += vbCrLf & " ----------------"
        PRINTtxt += vbCrLf & " Input point layer: " & DD.sFLAYER
        PRINTtxt += vbCrLf & " Point count filter: >= " & DD.sDDPNUM & " points"
        Dim geoName As String = "Line"
        If DD.bOUTELLIPSE Then geoName = "Ellipse"
        PRINTtxt += vbCrLf & " Output geometry: " & geoName
        PRINTtxt += vbCrLf & " Output layer name: " & DD.sDDOUT
        PRINTtxt += vbCrLf & vbCrLf

        'PROGRESS UPDATE: 
        pProDlg.Description = "Extracting point X, Y, and CID values..."
        PRINTtxt += vbCrLf & " [Extracting point X, Y, and CID values...]"

        Dim pFCursor1 As IFeatureCursor = pFClass.Search(Nothing, False)
        Dim pFeature1 As IFeature = pFCursor1.NextFeature

        Dim ArCPoints As New List(Of ClusterPoint)

        pTrkCan.Reset()
        While Not pFeature1 Is Nothing
            Dim iCID As Integer = pFeature1.Value(pFeature1.Fields.FindField("cid"))
            Dim iCnt As Integer = pFeature1.Value(pFeature1.Fields.FindField("cnt"))
            Dim pPoint As IPoint = pFeature1.ShapeCopy
            ArCPoints.Add(New ClusterPoint(pPoint.X, pPoint.Y, iCID, iCnt, Nothing))
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

        p_cPnum = DD.sDDPNUM
        ArCPoints.RemoveAll(AddressOf FindCPointByPntCount)

        If ArCPoints.Count <= 0 Then
            'SUMMARY PRINT: End program as interrupted
            PRINTtxt += SumEndProgram("INTERRUPTED: There were no clusters with " & _
                                      DD.sDDPNUM & " or more points.", _
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
        'ArCIDs.Remove(-1)

        '>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        pStepPro.MaxRange = ArCIDs.Count
        pStepPro.StepValue = 1
        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

        'PROGRESS UPDATE: 
        pProDlg.Description = "Generating geometries..."
        PRINTtxt += vbCrLf & " [Generating geometries..]"

        'List to store feature constructs that will be added to the feature class as features
        Dim ArCDD As New List(Of ClusterDD)

        pTrkCan.Reset()
        For Each iCID In ArCIDs
            'Get points with current CID
            p_CID = iCID
            Dim newCPoints As List(Of ClusterPoint) = ArCPoints.FindAll(AddressOf FindCPointByCID)
            'Add points to a point collection
            Dim pPColl4 As IPointCollection4 = New MultipointClass()
            For Each newCPoint In newCPoints
                Dim pPoint As New PointClass()
                pPoint.PutCoords(newCPoint.X, newCPoint.Y)
                pPColl4.AddPoint(pPoint)
            Next
            'Assign the point collection to a multipoint object
            Dim pMultipoint As IMultipoint = pPColl4
            pMultipoint.SpatialReference = pSpatRef

            'If map has Projection, de-project the points and assign them their GCS.
            ' This method is used to avoid errors when working across the 180/-180 degree lines
            ' of the GCS
            If TypeOf pSpatRef Is IProjectedCoordinateSystem Then
                Dim pPCS As IProjectedCoordinateSystem5 = pSpatRef
                For i As Integer = 0 To pPColl4.PointCount - 1
                    Dim pWPoint As ESRI.ArcGIS.esriSystem.WKSPoint = New WKSPoint
                    pWPoint.X = pPColl4.Point(i).X
                    pWPoint.Y = pPColl4.Point(i).Y
                    pPCS.Inverse(1, pWPoint)
                    pPColl4.Point(i).X = pWPoint.X
                    pPColl4.Point(i).Y = pWPoint.Y
                Next
                pMultipoint.SpatialReference = pGCS
            End If
            'Create Transverse Mercator (conformal) projection
            Dim pTMSpatialReference As ISpatialReference = CreateTMercatorPCS _
                                                            (pMultipoint, pGCS)
            'Project all points to conformal projection
            pMultipoint.Project(pTMSpatialReference)
            pMultipoint.SpatialReference = pTMSpatialReference

            'Calculate: CenterPoint, semiMajorAxis length, semiMinorAxis length, and semiMajorAxis azimuth
            Dim stdDevs As Integer = CInt(std.Text) 'Used to get a major axis line large enough to reach the extents
            ' of the point collection used to compute it

            Dim pGeodeticEllipseParams As st_6PointEllipseParameters = _
                GetEllipseParameters(pPColl4, stdDevs)
            Dim pCenterPoint As IPoint = pGeodeticEllipseParams.centerPoint
            Dim pSMajAxis As Double = pGeodeticEllipseParams.semiMajorAxisLength
            Dim pSMinAxis As Double = pGeodeticEllipseParams.semiMinorAxisLength
            Dim pSMajAxisAz As Double = pGeodeticEllipseParams.semiMajorAxisAzimuth

            'De-project center point to GCS
            pCenterPoint.SpatialReference = pTMSpatialReference
            pCenterPoint.Project(pGCS)
            pCenterPoint.SpatialReference = pGCS

            'Create Azimuthal/Equidistant projection
            Dim pAEDSpatialReference As ISpatialReference = CreateAzEquiDistPCS(pCenterPoint, _
                                                                                pGCS)
            'Project the center point to the AED projection
            pCenterPoint.Project(pAEDSpatialReference)
            pCenterPoint.SpatialReference = pAEDSpatialReference

            'Transform rotation for major and minor axis lines
            Dim dZRotation, dZRotRad As Double
            If pSMajAxisAz < 90 Then dZRotation = 90 - pSMajAxisAz
            If pSMajAxisAz > 90 Then dZRotation = 360 - (pSMajAxisAz - 90)
            dZRotRad = ToRad(dZRotation)

            'Calculate the semi-minor axis line
            Dim pMinAxisTanPoint As IConstructPoint = New PointClass()
            pMinAxisTanPoint.ConstructAngleDistance(pCenterPoint, dZRotRad + ToRad(90), pSMinAxis)

            'Extend the semi-minor axis line (used only for the end-points)
            Dim pMinAxisLengthPLine As IPolyline = GetExtendedDiameterLine(pCenterPoint, pMinAxisTanPoint)
            pMinAxisLengthPLine.SpatialReference = pAEDSpatialReference

            'Make minor axis line geodesic
            Dim pPolycurveGeodeticMin As IPolycurveGeodetic = pMinAxisLengthPLine
            pPolycurveGeodeticMin.DensifyGeodetic(esriGeodeticType.esriGeodeticTypeGeodesic, _
                                                Nothing, _
                                                esriCurveDensifyMethod.esriCurveDensifyByDeviation, _
                                                1D)

            'Get the geodesic lenght of the minor axis length line in m
            Dim lengthMin As Double = pPolycurveGeodeticMin.LengthGeodetic( _
                esriGeodeticType.esriGeodeticTypeGeodesic, Nothing)

            'Calculate the semi-major axis line
            Dim pMajAxisTanPoint As IConstructPoint = New PointClass()
            pMajAxisTanPoint.ConstructAngleDistance(pCenterPoint, dZRotRad, pSMajAxis)

            'Extend the semi-major axis line (used only for the end-points)
            Dim pMajAxisLengthPLine As IPolyline = GetExtendedDiameterLine(pCenterPoint, pMajAxisTanPoint)
            pMajAxisLengthPLine.SpatialReference = pAEDSpatialReference

            'Make major axis line geodesic
            Dim pPolycurveGeodeticMaj As IPolycurveGeodetic = pMajAxisLengthPLine
            pPolycurveGeodeticMaj.DensifyGeodetic(esriGeodeticType.esriGeodeticTypeGeodesic, _
                                                Nothing, _
                                                esriCurveDensifyMethod.esriCurveDensifyByDeviation, _
                                                1D)

            'Get the geodesic lenght of the major axis in m
            Dim lengthMaj As Double = pPolycurveGeodeticMaj.LengthGeodetic( _
                esriGeodeticType.esriGeodeticTypeGeodesic, Nothing)

            Dim dInverseFlat As Double = lengthMaj / (lengthMaj - lengthMin)

            'Project the major axis for GCS splitting
            Dim pMajAxisLengthPLineGeom5 As IGeometry5 = pMajAxisLengthPLine
            pMajAxisLengthPLineGeom5.Project5(pSpatRef, _
                                            esriProjectionHint.esriProjectionHintForceSplittingInGCS)

            'Clip the major axis line to the extent of the points that were used to calculate it
            pMultipoint.Project(pSpatRef)
            Dim pTopoOp5 As ITopologicalOperator5 = pMajAxisLengthPLine
            pTopoOp5.Clip(pMultipoint.Envelope)

            Dim newLengthMaj As Double = pPolycurveGeodeticMaj.LengthGeodetic( _
                esriGeodeticType.esriGeodeticTypeGeodesic, Nothing)
            Dim newLengthMin As Double = ((newLengthMaj / dInverseFlat) - newLengthMaj) * -1
            Dim pNewCenter As IPoint = New PointClass()
            pMajAxisLengthPLine.QueryPoint(esriSegmentExtension.esriExtendAtFrom, 0.5, True, pNewCenter)

            Dim pLengthPolyline As IPolyline = pMajAxisLengthPLine
            Dim pDDC As ClusterDD = Nothing

            If DD.bOUTELLIPSE Then
                'Get the major axis line end-point segments for trajectory azimuth computations
                Dim pLSPointColl As IPointCollection = TryCast(pLengthPolyline, IPointCollection)
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

                'Generate the ellipse
                Dim pEllipse As IPolyline = New PolylineClass()
                pEllipse.SpatialReference = pSpatRef
                Dim pConstructGeodetic As IConstructGeodetic = pEllipse
                pConstructGeodetic.ConstructGeodesicEllipse(pNewCenter, _
                                                            Nothing, _
                                                            newLengthMaj / 2, _
                                                            newLengthMin / 2, _
                                                            pSMajAxisAz, _
                                                            esriCurveDensifyMethod.esriCurveDensifyByDeviation, _
                                                            1D)

                'Project the ellipse for GCS splitting
                Dim pPolylineGeom5 As IGeometry5 = pEllipse
                pPolylineGeom5.Project5(pSpatRef, _
                                        esriProjectionHint.esriProjectionHintForceSplittingInGCS)

                pDDC = New ClusterDD(pEllipse, iCID, newCPoints.Item(0).Count, _
                                     dInverseFlat, newLengthMaj, _
                                     pFPp.X, pFPp.Y, _
                                     pFP.X, pFP.Y, _
                                     pTPp.X, pTPp.Y, _
                                     pTP.X, pTP.Y, Nothing)
            Else
                pDDC = New ClusterDD(pLengthPolyline, iCID, newCPoints.Item(0).Count, _
                                     dInverseFlat, newLengthMaj)
            End If

            ArCDD.Add(pDDC)

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

        Dim pDDReqFields As IFields = GetDirDisReqFields(True, DD.bOUTELLIPSE)
        Dim pDDFLayer As IFeatureLayer = New FeatureLayerClass()
        pDDFLayer.FeatureClass = CreateFeatureClass(pWrkspc2, pFDataset, DD.sDDOUT, _
                                                        pDDReqFields, _
                                                        esriGeometryType.esriGeometryPolyline, _
                                                        pSpatRef)
        pDDFLayer.Name = pDDFLayer.FeatureClass.AliasName
        Dim pDDFClass As IFeatureClass = pDDFLayer.FeatureClass

        'PROGRESS UPDATE: 
        pProDlg.Description = "Storing polyline features..."
        PRINTtxt += vbCrLf & " [Storing polyline features...]"

        'Begin edit session and operation
        Dim pEditor As IEditor = My.ArcMap.Editor
        pEditor.StartEditing(pWrkspc2)
        pEditor.StartOperation()

        Dim pDDFCursor As IFeatureCursor = pDDFClass.Insert(True)

        If DD.bOUTELLIPSE Then
            pTrkCan.Reset()
            For Each ddFeature In ArCDD
                'Create the feature buffer
                Dim pDDFBuffer As IFeatureBuffer = pDDFClass.CreateFeatureBuffer
                pDDFBuffer.Shape = ddFeature.Shape
                pDDFBuffer.Value(pDDFBuffer.Fields.FindField("cid")) = ddFeature.CID
                pDDFBuffer.Value(pDDFBuffer.Fields.FindField("cnt")) = ddFeature.Count
                pDDFBuffer.Value(pDDFBuffer.Fields.FindField("iflat")) = ddFeature.IFlat
                pDDFBuffer.Value(pDDFBuffer.Fields.FindField("majaxis")) = ddFeature.MajAxis
                pDDFBuffer.Value(pDDFBuffer.Fields.FindField("ffx")) = ddFeature.FromFX
                pDDFBuffer.Value(pDDFBuffer.Fields.FindField("ffy")) = ddFeature.FromFY
                pDDFBuffer.Value(pDDFBuffer.Fields.FindField("ftx")) = ddFeature.FromTX
                pDDFBuffer.Value(pDDFBuffer.Fields.FindField("fty")) = ddFeature.FromTY
                pDDFBuffer.Value(pDDFBuffer.Fields.FindField("tfx")) = ddFeature.ToFX
                pDDFBuffer.Value(pDDFBuffer.Fields.FindField("tfy")) = ddFeature.ToFY
                pDDFBuffer.Value(pDDFBuffer.Fields.FindField("ttx")) = ddFeature.ToTX
                pDDFBuffer.Value(pDDFBuffer.Fields.FindField("tty")) = ddFeature.ToTY
                pDDFCursor.InsertFeature(pDDFBuffer)
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
        Else
            pTrkCan.Reset()
            For Each ddFeature In ArCDD
                'Create the feature buffer
                Dim pDDFBuffer As IFeatureBuffer = pDDFClass.CreateFeatureBuffer
                pDDFBuffer.Shape = ddFeature.Shape
                pDDFBuffer.Value(pDDFBuffer.Fields.FindField("cid")) = ddFeature.CID
                pDDFBuffer.Value(pDDFBuffer.Fields.FindField("cnt")) = ddFeature.Count
                pDDFBuffer.Value(pDDFBuffer.Fields.FindField("iflat")) = ddFeature.IFlat
                pDDFBuffer.Value(pDDFBuffer.Fields.FindField("majaxis")) = ddFeature.MajAxis
                pDDFCursor.InsertFeature(pDDFBuffer)
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

        'Write summary information about ellipses generation
        PRINTtxt += CalcEllipStats(ArCDD)
        PRINTtxt += CalcEllipInvStats(ArCDD)
        'Stop edit operation and session, save edits
        pEditor.StopOperation("Directional Distribution features")
        StopEditSession(True)

        'Add the new layer to the map
        Dim pDDLayer As ILayer = pDDFLayer
        pMxDoc.ActiveView.FocusMap.AddLayer(pDDLayer)

        'Refresh the Toc and Map
        pMxDoc.UpdateContents()
        pMxDoc.ActiveView.Refresh()

        'SUMMARY PRINT: End program as complete
        PRINTtxt += SumEndProgram("COMPLETE: Directional Distribution process complete.", _
                                  sSDate, sSTime)

        'Destroy the progress dialog
        ProgressDialogDispose(pProDlg, pStepPro, pTrkCan, pProDlgFact)
        If LogFileName.Text <> "" Then
            SaveLog(LogFileName.Text, PRINTtxt)
        End If

    End Sub

    Private p_CID As Integer
    Public Function FindCPointByCID(ByVal cPoint As ClusterPoint) As Boolean
        If cPoint.CID = p_CID Then
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

    Private Function GetEllipseParameters(ByVal pPointCollection As IPointCollection, _
                                          ByVal stdDevs As Integer) _
        As st_6PointEllipseParameters

        Try
            Dim i As Integer
            Dim dX As Double
            Dim dY As Double
            Dim dXSum As Double
            Dim dYSum As Double
            'Sum Xs and Ys
            For i = 0 To pPointCollection.PointCount - 1
                dX = pPointCollection.Point(i).X
                dY = pPointCollection.Point(i).Y
                dXSum = dXSum + dX
                dYSum = dYSum + dY
            Next

            'Calculate Mean Centers for X and Y
            Dim dCenterX, dCenterY As Double
            dCenterX = dXSum / pPointCollection.PointCount
            dCenterY = dYSum / pPointCollection.PointCount

            'Sum X and Y deviations from each center X and Y
            Dim dXDev As Double
            Dim dYDev As Double
            Dim dXDevSum As Double
            Dim dYDevSum As Double
            Dim dXSDevSum As Double
            Dim dYSDevSum As Double
            Dim dXYDevSum As Double
            For i = 0 To pPointCollection.PointCount - 1
                dXDev = pPointCollection.Point(i).X - dCenterX
                dYDev = pPointCollection.Point(i).Y - dCenterY
                dXDevSum = dXDevSum + dXDev
                dYDevSum = dYDevSum + dYDev
                dXSDevSum = dXSDevSum + (dXDev ^ 2)
                dYSDevSum = dYSDevSum + (dYDev ^ 2)
                dXYDevSum = dXYDevSum + (dXDev * dYDev)
            Next

            'Calculate Standard Ellipse. Source: Ebdon. Lenght of axes
            'adjusted by sqr(2) to correct under-estimate as described
            'in Ned Levine's CrimeStat documentation
            Dim dStdX As Double
            Dim dStdY As Double
            Dim dCRotation As Double
            Dim dDenom As Double
            Dim dDiff1 As Double
            Dim dSum1 As Double
            Dim dTan As Double
            Dim dSinT As Double
            Dim dCosT As Double

            dDenom = 2 * dXYDevSum
            dDiff1 = dXSDevSum - dYSDevSum
            dSum1 = (dXSDevSum - dYSDevSum) ^ 2 + 4 * dXYDevSum ^ 2
            If Not Math.Abs(dDenom) > 0 Then
                dTan = 0
            Else
                dTan = Math.Atan((dDiff1 + Math.Sqrt(dSum1)) / dDenom)
            End If
            If dTan < 0 Then
                dTan = dTan + (Math.PI / 2)
            End If
            dSinT = Math.Sin(dTan)
            dCosT = Math.Cos(dTan)
            dStdX = (Math.Sqrt(2) * _
                     Math.Sqrt(((dXSDevSum * dCosT ^ 2) - (2 * dXYDevSum * dSinT * dCosT) + _
                         (dYSDevSum * dSinT ^ 2)) / _
                          pPointCollection.PointCount) * stdDevs) 'Size in Standard Deviations
            dStdY = (Math.Sqrt(2) * _
                     Math.Sqrt(((dXSDevSum * dSinT ^ 2) + (2 * dXYDevSum * dSinT * dCosT) + _
                         (dYSDevSum * dCosT ^ 2)) / _
                          pPointCollection.PointCount) * stdDevs) 'Size in Standard Deviations
            dCRotation = 360 - (dTan * 57.2957795) 'Counter clockwise from NOON

            'Create the center point
            Dim pCenter As IPoint = New PointClass()
            pCenter.PutCoords(dCenterX, dCenterY)

            'Get the semiMajor and semiMinor axis lengths
            Dim SMajAxis, SMinAxis As Double
            If dStdX > dStdY Then
                SMajAxis = dStdX
                SMinAxis = dStdY
            Else
                SMajAxis = dStdY
                SMinAxis = dStdX
            End If

            'Get rotation (azimuth)
            Dim dRotation As Double = 360 - dCRotation
            If dStdX > dStdY Then
                dRotation = dRotation + 90
                If dRotation > 360 Then
                    dRotation = dRotation - 180
                End If
            End If
            Dim SMajAxisAzim As Double = dRotation

            Dim Results As st_6PointEllipseParameters
            Results.centerPoint = pCenter
            Results.semiMajorAxisLength = SMajAxis
            Results.semiMinorAxisLength = SMinAxis
            Results.semiMajorAxisAzimuth = SMajAxisAzim

            Return Results

        Catch ex As Exception

            MsgBox(ex.Message, MsgBoxStyle.Critical, "Ellipse computation error.")

            Dim Results As st_6PointEllipseParameters
            Results.centerPoint = Nothing
            Results.semiMajorAxisLength = 0
            Results.semiMinorAxisLength = 0
            Results.semiMajorAxisAzimuth = 0

            Return Results

        End Try

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

#Region "***** POINT QUERY *********"
#End Region

    Private Sub grpDDPNUM_Enter(ByVal sender As System.Object, _
                                ByVal e As System.EventArgs) _
                                Handles grpDDPNUM.Enter
        HELP_PntCount()
    End Sub

    Private Sub grpDDPNUM_Click(ByVal sender As System.Object, _
                                ByVal e As System.EventArgs) _
                                Handles grpDDPNUM.Click
        HELP_PntCount()
    End Sub

#Region "***** OUTPUT GEOMETRY *******"
#End Region

    Private Sub grpOUTGEOM_Enter(ByVal sender As System.Object, _
                                 ByVal e As System.EventArgs) _
                                 Handles grpOUTGEOM.Enter
        HELP_OutGeom()
    End Sub

    Private Sub grpOUTGEOM_Click(ByVal sender As System.Object, _
                                 ByVal e As System.EventArgs) _
                                 Handles grpOUTGEOM.Click
        HELP_OutGeom()
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
          "Generates linear or elliptical polylines for point clusters. " & _
          "The output polylines hold azimuth, length and distribution properties for each cluster." & _
          vbCrLf & vbCrLf & _
          "Requirement: The input layer must be projected in meters."

        HELPCntUpdate("Directional Distribution Tool", strText)

    End Sub

    Private Sub HELP_Layer()

        'Update help panel
        Dim strText As String = _
            "The clustered point layer." & vbCrLf & vbCrLf & _
            "Requirement: The input layer must be projected in meters."

        HELPCntUpdate("Input point layer", strText)

    End Sub

    Private Sub txtDDPNUM_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDDPNUM.Click
        HELP_PntCount()
    End Sub

    Private Sub HELP_PntCount()

        'Update help panel
        Dim strText As String = _
            "Clusters with a number of points outside this threshold are excluded from " & _
            "the analysis."

        HELPCntUpdate("Point count filter", strText)

    End Sub

    Private Sub HELP_OutGeom()

        'Update help panel
        Dim strText As String = _
            "Line - A line will be fitted to the cluster. " & _
             vbCrLf & vbCrLf & _
            "Ellipse - An elliptical polyline will be fitted to the cluster."

        HELPCntUpdate("Output geometry", strText)

    End Sub

    Private Sub HELP_Out()

        'Update help panel
        Dim strText As String = _
            "The output polyline layer name."

        HELPCntUpdate("Output layer name", strText)

    End Sub

#Region "Standard Deviation"
    Private Sub std_grp_Enter(ByVal sender As System.Object, _
                             ByVal e As System.EventArgs) Handles std_grp.Click

        HELP_Std()
    End Sub

    Private Sub std_name_Click(ByVal sender As System.Object, _
                             ByVal e As System.EventArgs) Handles std.Enter

        HELP_Std()
    End Sub

    Private Sub HELP_Std()

        'Update help panel
        Dim strText As String = _
            "The number of standard deviations at which to compute the bound ellipsoid" & _
            Environment.NewLine & _
            "This field is optional."

        HELPCntUpdate("Standard Deviation", strText)

    End Sub
#End Region

#Region "Log File"
    Private Sub log_grp_Enter(ByVal sender As System.Object, _
                             ByVal e As System.EventArgs) Handles log_grp.Click

        HELP_Out()
    End Sub

    Private Sub log_name_Click(ByVal sender As System.Object, _
                             ByVal e As System.EventArgs) Handles LogFileName.Enter

        HELP_Log()
    End Sub

    Private Sub HELP_Log()

        'Update help panel
        Dim strText As String = _
            "The output log file name." & _
            Environment.NewLine & _
            "This field is optional."

        HELPCntUpdate("Output Log File Name", strText)

    End Sub
#End Region




    Private Function CalcEllipStats(ByVal features)
        Dim count, range, min, lq, median, uq, max, mean, std, iqr, sum, sumsq, array() As Double
        count = 0
        max = 0
        min = 9999999
        sum = 0
        For Each feature In features
            count += 1
            If feature.MajAxis > max Then max = feature.MajAxis
            If feature.MajAxis < min Then min = feature.MajAxis
            sum += feature.MajAxis
        Next
        range = max - min
        mean = sum / count
        sumsq = 0
        ReDim array(count)

        Dim n As Integer = 0 'index counter
        For Each feature In features
            array(n) = feature.MajAxis
            sumsq += (feature.MajAxis - mean) ^ 2
            n += 1
        Next

        std = Sqrt(sumsq / count)

        System.Array.Sort(array)
        'Compute the quartiles
        If IEEERemainder(count, 2) = 0 Then
            median = (array(Round(count * 0.5)) + _
                       array(Round((count * 0.5) + 1))) / 2
            lq = (array(Round(count * 0.25)) + _
                   array(Round((count * 0.25) + 1))) / 2
            uq = (array(Round(count * 0.75)) + _
                   array(Round((count * 0.75) + 1))) / 2
        Else
            median = array(Round(count * 0.5))
            lq = array(Round(count * 0.25))
            uq = array(Round(count * 0.75))
        End If

        'Approximate Interquartile range
        iqr = uq - lq

        'Write the stats out
        Dim sReport As String = ""
        Dim f100 As String = Space(3) & "|" & "{0,-20}" & "{1}" & "{2,16:F4}" & "|"
        Dim f300 As String = Space(3) & "|" & StrDup(37, "-") & "|"
        Dim f400 As String = Space(3) & "|" & StrDup(37, "_") & "|"
        Dim f500 As String = Space(3) & StrDup(39, "_")

        sReport = vbCrLf & vbCrLf & _
          Space(3) & "STATS: Ellipse Major Axis " & vbCrLf & _
          (String.Format(f500, "_")) & vbCrLf & _
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

        Return sReport
    End Function

    Private Function CalcEllipInvStats(ByVal features)
        Dim count, range, min, lq, median, uq, max, mean, std, iqr, sum, sumsq, array() As Double

        count = 0
        max = 0
        min = 9999999
        sum = 0
        For Each feature In features
            count += 1
            If feature.IFlat > max Then max = feature.IFlat
            If feature.IFlat < min Then min = feature.IFlat
            sum += feature.IFlat
        Next
        range = max - min
        mean = sum / count
        sumsq = 0
        ReDim array(count)

        Dim n As Integer = 0 'index counter
        For Each feature In features
            array(n) = feature.IFlat
            sumsq += (feature.IFlat - mean) ^ 2
            n += 1
        Next

        std = Sqrt(sumsq / count)

        System.Array.Sort(array)
        'Compute the quartiles
        If IEEERemainder(count, 2) = 0 Then
            median = (array(Round(count * 0.5)) + _
                       array(Round((count * 0.5) + 1))) / 2
            lq = (array(Round(count * 0.25)) + _
                   array(Round((count * 0.25) + 1))) / 2
            uq = (array(Round(count * 0.75)) + _
                   array(Round((count * 0.75) + 1))) / 2
        Else
            median = array(Round(count * 0.5))
            lq = array(Round(count * 0.25))
            uq = array(Round(count * 0.75))
        End If

        'Approximate Interquartile range
        iqr = uq - lq

        'Write the stats out
        Dim sReport As String = ""
        Dim f100 As String = Space(3) & "|" & "{0,-20}" & "{1}" & "{2,16:F4}" & "|"
        Dim f300 As String = Space(3) & "|" & StrDup(37, "-") & "|"
        Dim f400 As String = Space(3) & "|" & StrDup(37, "_") & "|"
        Dim f500 As String = Space(3) & StrDup(39, "_")

        sReport = vbCrLf & vbCrLf & _
          Space(3) & "STATS: Ellipse Inverse flattening " & vbCrLf & _
          (String.Format(f500, "_")) & vbCrLf & _
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

        Return sReport
    End Function


End Class