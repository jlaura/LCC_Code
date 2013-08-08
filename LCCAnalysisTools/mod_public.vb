Imports System
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Geoprocessor
Imports ESRI.ArcGIS.Editor
Imports ESRI.ArcGIS.GeoDatabaseUI
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.AnalysisTools
Imports System.Math
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.Framework
Imports System.Windows.Forms
Imports System.IO

Public Module mod_public

#Region "*** GLOBAL VARIABLES, OBJECTS & CLASSES *************************************************"
#End Region

    Public m_clusterForm As frm_clustertool
    Public m_dirdisForm As frm_dirdistool
    Public m_trajectForm As frm_trajecttool
    Public m_intersectForm As frm_intersecttool
    Public m_distanceForm As frm_distancetool

    Public m_sCAFLayer As String
    Public m_sDDFLayer As String
    Public m_sTAFLayer As String
    Public m_sIAFLayer As String
    Public distlayer As String
    Public distance_table As String
    Public c_method As String
    Public dist_lists As New List(Of List(Of Double))
    Public kdist(1) As Double
    Public traj_selected_layer As Integer = -1

    'Structure to pass 'Cluster Analysis' parameters from 
    ''main form' to progress
    Public Structure CAPARAM
        Public sFLAYER As String
        Public sNQUERY As String
        Public bMEASPLAN As Boolean
        Public bMEASGEO As Boolean
        Public bCMS As Boolean
        Public sCMSVAL As String
        Public bCMBNND As Boolean
        Public bCMBF As Boolean
        Public sCMBFVAL As String
        Public bCMBS As Boolean
        Public sCMBSVAL As String
        Public sOUT As String
    End Structure

    'Structure to pass 'Directional Distribution Analysis' parameters from
    'main form' to progress
    Public Structure DDPARAM
        Public sFLAYER As String
        Public sDDPNUM As String
        Public bOUTLINE As Boolean
        Public bOUTELLIPSE As Boolean
        Public sDDOUT As String
    End Structure

    'Public structure used to pass Ellipse geometry definition parameters
    Public Structure st_6PointEllipseParameters
        Public centerPoint As IPoint
        Public semiMajorAxisLength As Double
        Public semiMinorAxisLength As Double
        Public semiMajorAxisAzimuth As Double
    End Structure

    'Structure to pass 'Trajectory Analysis' parameters from 
    ''main form' to progress
    Public Structure TAPARAM
        Public sFLAYER As String
        Public sEMAMOD As String
        Public sEMAVAL As String
        Public sEIFMOD As String
        Public sEIFVAL As String
        Public bTDDEG As Boolean
        Public sTDDEGVAL As String
        Public bTDDIST As Boolean
        Public sTDDISTVAL As String
        Public bCEEJV As Boolean
        Public sCEEJVVAL As String
        Public sCEPROTP As String
        Public sTAOUT As String
    End Structure

    'Structure to pass 'Intersection Analysis' parameters from 
    ''main form' to progress
    Public Structure IAPARAM
        Public sFLAYER As String
        Public sNQUERY As String
        Public bMEASPLAN As Boolean
        Public bMEASGEO As Boolean
        Public bCMS As Boolean
        Public sCMSVAL As String
        Public bCMBNND As Boolean
        Public bCMBF As Boolean
        Public sCMBFVAL As String
        Public bCMBS As Boolean
        Public sCMBSVAL As String
        Public sCPNUM As String
        Public sOUT As String
    End Structure

    Public Const Pi As Double = 3.14159265358979

    Public Structure Lat2BLon2B
        Public Lat2B As Double
        Public Lon2B As Double
    End Structure

    Public Class ClusterPoint
        Private m_x As Double
        Private m_y As Double
        Private m_cid As Integer
        Private m_cnt As Integer
        Private m_mean As Double
        Private m_std As Double
        Private m_weight As Double

        Public Sub New(ByVal x As Double, ByVal y As Double, _
                       ByVal cid As Integer, ByVal cnt As Integer, ByVal weight As Double)
            Me.m_x = x
            Me.m_y = y
            Me.m_cid = cid
            Me.m_cnt = cnt
            Me.m_weight = weight
        End Sub
        Public Sub New(ByVal x As Double, ByVal y As Double, _
                       ByVal cid As Integer, ByVal cnt As Integer, _
                       ByVal mean As Double, ByVal std As Double, ByVal weight As Double)
            Me.m_x = x
            Me.m_y = y
            Me.m_cid = cid
            Me.m_cnt = cnt
            Me.m_std = std
            Me.m_mean = mean
            Me.m_weight = weight
        End Sub
        Public Property X() As Double
            Get
                Return m_x
            End Get
            Set(ByVal value As Double)
                m_x = value
            End Set
        End Property
        Public Property Y() As Double
            Get
                Return m_y
            End Get
            Set(ByVal value As Double)
                m_y = value
            End Set
        End Property
        Public Property CID() As Integer
            Get
                Return m_cid
            End Get
            Set(ByVal value As Integer)
                m_cid = value
            End Set
        End Property
        Public Property Count() As Integer
            Get
                Return m_cnt
            End Get
            Set(ByVal value As Integer)
                m_cnt = value
            End Set
        End Property
        Public Property Stdev() As Double
            Get
                Return m_std
            End Get
            Set(ByVal value As Double)
                m_std = value
            End Set
        End Property
        Public Property Mean() As Double
            Get
                Return m_mean
            End Get
            Set(ByVal value As Double)
                m_mean = value
            End Set
        End Property
        Public Property Weight() As Double
            Get
                Return m_weight
            End Get
            Set(ByVal value As Double)
                m_weight = value
            End Set
        End Property

    End Class

    Public Class ClusterDD
        Private m_Shape As IGeometry
        Private m_CID As Integer
        Private m_Count As Integer
        Private m_IFlat As Double
        Private m_MajAxis As Double
        Private m_MinAxis As Double
        Private m_CenterX As Double
        Private m_CenterY As Double
        Private m_Azim As Double
        Private m_FFx As Double
        Private m_FFy As Double
        Private m_FTx As Double
        Private m_FTy As Double
        Private m_TFx As Double
        Private m_TFy As Double
        Private m_TTx As Double
        Private m_TTy As Double
        Private m_ParentFeature As String

        Public Sub New(ByVal Shape As IGeometry, ByVal CID As Integer, _
                       ByVal Count As Integer, _
                       ByVal IFlat As Double, ByVal MajAxis As Double)
            Me.m_Shape = Shape
            Me.m_CID = CID
            Me.m_Count = Count
            Me.m_IFlat = IFlat
            Me.m_MajAxis = MajAxis
        End Sub
        Public Sub New(ByVal Shape As IGeometry, ByVal CID As Integer, _
                       ByVal Count As Integer, _
                       ByVal IFlat As Double, ByVal MajAxis As Double, _
                       ByVal FromFX As Double, ByVal FromFY As Double, _
                       ByVal FromTX As Double, ByVal FromTY As Double, _
                       ByVal ToFX As Double, ByVal ToFY As Double, _
                       ByVal ToTX As Double, ByVal ToTY As Double, ByVal ParentFeature As String)
            Me.m_Shape = Shape
            Me.m_CID = CID
            Me.m_Count = Count
            Me.m_IFlat = IFlat
            Me.m_MajAxis = MajAxis
            Me.m_FFx = FromFX
            Me.m_FFy = FromFY
            Me.m_FTx = FromTX
            Me.m_FTy = FromTY
            Me.m_TFx = ToFX
            Me.m_TFy = ToFY
            Me.m_TTx = ToTX
            Me.m_TTy = ToTY
            Me.m_ParentFeature = ParentFeature
        End Sub
        Public Property Shape() As IGeometry
            Get
                Return m_Shape
            End Get
            Set(ByVal value As IGeometry)
                m_Shape = value
            End Set
        End Property
        Public Property CID() As Integer
            Get
                Return m_CID
            End Get
            Set(ByVal value As Integer)
                m_CID = value
            End Set
        End Property
        Public Property Count() As Integer
            Get
                Return m_Count
            End Get
            Set(ByVal value As Integer)
                m_Count = value
            End Set
        End Property
        Public Property IFlat() As Double
            Get
                Return m_IFlat
            End Get
            Set(ByVal value As Double)
                m_IFlat = value
            End Set
        End Property
        Public Property MajAxis() As Double
            Get
                Return m_MajAxis
            End Get
            Set(ByVal value As Double)
                m_MajAxis = value
            End Set
        End Property
        Public Property FromFX() As Double
            Get
                Return m_FFx
            End Get
            Set(ByVal value As Double)
                m_FFx = value
            End Set
        End Property
        Public Property FromFY() As Double
            Get
                Return m_FFy
            End Get
            Set(ByVal value As Double)
                m_FFy = value
            End Set
        End Property
        Public Property FromTX() As Double
            Get
                Return m_FTx
            End Get
            Set(ByVal value As Double)
                m_FTx = value
            End Set
        End Property
        Public Property FromTY() As Double
            Get
                Return m_FTy
            End Get
            Set(ByVal value As Double)
                m_FTy = value
            End Set
        End Property
        Public Property ToFX() As Double
            Get
                Return m_TFx
            End Get
            Set(ByVal value As Double)
                m_TFx = value
            End Set
        End Property
        Public Property ToFY() As Double
            Get
                Return m_TFy
            End Get
            Set(ByVal value As Double)
                m_TFy = value
            End Set
        End Property
        Public Property ToTX() As Double
            Get
                Return m_TTx
            End Get
            Set(ByVal value As Double)
                m_TTx = value
            End Set
        End Property
        Public Property ToTY() As Double
            Get
                Return m_TTy
            End Get
            Set(ByVal value As Double)
                m_TTy = value
            End Set
        End Property

        Property ParentFeature As String
            Get
                Return m_ParentFeature
            End Get
            Set(ByVal value As String)
                m_ParentFeature = value
            End Set
        End Property

    End Class

    Public Class TrajectoryLine
        Private m_Shape As IGeometry
        Private m_CID As Integer
        Private m_Parent As String
        Private m_iflat As Double

        Public Sub New(ByVal Shape As IGeometry, ByVal CID As Integer, ByVal Parent As String, ByVal iflat As Double)
            Me.m_Shape = Shape
            Me.m_CID = CID
            Me.m_Parent = Parent
            Me.m_iflat = iflat
        End Sub
        Public Property Shape() As IGeometry
            Get
                Return m_Shape
            End Get
            Set(ByVal value As IGeometry)
                m_Shape = value
            End Set
        End Property
        Public Property CID() As Integer
            Get
                Return m_CID
            End Get
            Set(ByVal value As Integer)
                m_CID = value
            End Set
        End Property

        Property ParentFeature As Object
            Get
                Return m_Parent
            End Get
            Set(ByVal value As Object)
                m_Parent = value
            End Set
        End Property

        Property iflat As Object
            Get
                Return m_iflat
            End Get
            Set(ByVal value As Object)
                m_iflat = value
            End Set
        End Property

    End Class
    Public Class IntersectionPoint
        Private m_pt As IPoint
        Private m_weight As Double

        Public Sub New(ByVal Point As IPoint, ByVal Weight As Double)
            Me.m_pt = Point
            Me.m_weight = Weight
        End Sub

        Public Property Point() As IPoint
            Get
                Return m_pt
            End Get
            Set(ByVal value As IPoint)
                m_pt = value
            End Set
        End Property

        Public Property Weight() As Double
            Get
                Return m_weight
            End Get
            Set(ByVal value As Double)
                m_weight = value
            End Set
        End Property
    End Class

    Public Class FeatureExtractions
        Private m_OID As Integer
        Private m_geom As IPolyline
        Private m_iflat As Double

        Public Sub New(ByVal OID As Integer, ByVal geom As IPolyline, ByVal iFlat As Double)
            Me.m_OID = OID
            Me.m_geom = geom
            Me.m_iflat = iFlat
        End Sub

        Public Property OID() As Integer
            Get
                Return m_OID
            End Get
            Set(ByVal value As Integer)
                m_OID = value
            End Set
        End Property

        Public Property geom() As IPolyline
            Get
                Return m_geom
            End Get
            Set(ByVal value As IPolyline)
                m_geom = value
            End Set
        End Property

        Public Property iFlat() As Double
            Get
                Return m_iflat
            End Get
            Set(ByVal value As Double)
                m_iflat = value
            End Set
        End Property
    End Class

    Public Class PLineOIDPair
        Private m_OID1 As Integer
        Private m_OID2 As Integer
        Private m_iflat1 As Double
        Private m_iflat2 As Double

        Public Sub New(ByVal OID1 As Integer, ByVal OID2 As Integer, ByVal iflat1 As Double, ByVal iflat2 As Double)
            Me.m_OID1 = OID1
            Me.m_OID2 = OID2
            Me.m_iflat1 = iflat1
            Me.m_iflat2 = iflat2
        End Sub
        Public Property OID1() As Integer
            Get
                Return m_OID1
            End Get
            Set(ByVal value As Integer)
                m_OID1 = value
            End Set
        End Property
        Public Property OID2() As Integer
            Get
                Return m_OID2
            End Get
            Set(ByVal value As Integer)
                m_OID2 = value
            End Set
        End Property
        Public Property iflat1() As Double
            Get
                Return m_iflat1
            End Get
            Set(ByVal value As Double)
                m_iflat1 = value
            End Set
        End Property
        Public Property iflat2() As Double
            Get
                Return m_iflat2
            End Get
            Set(ByVal value As Double)
                m_iflat2 = value
            End Set
        End Property
    End Class

    Public Function GetFLayerByName(ByVal strLayerName As String) _
                                    As IFeatureLayer

        If Not strLayerName <> "" Then Return Nothing

        Dim pMxDoc As IMxDocument = My.ArcMap.Document

        Dim pLayers As IEnumLayer
        Dim pLayer As ILayer
        Dim pFLayer As IFeatureLayer

        pLayers = pMxDoc.FocusMap.Layers

        'Return the layer that matches the name
        pLayer = pLayers.Next
        While Not pLayer Is Nothing
            If UCase(pLayer.Name) = UCase(strLayerName) Then
                pFLayer = CType(pLayer, IFeatureLayer)

                'Empty local variables
                pLayers = Nothing
                pLayer = Nothing

                Return pFLayer
            End If
            pLayer = pLayers.Next
        End While

        Return Nothing

    End Function

    Public Function getTableByName(ByVal TableName As String) As ITable
        If Not TableName <> "" Then Return Nothing
        Dim mxdoc As IMxDocument = My.ArcMap.Document
        Dim map = mxdoc.FocusMap
        Dim tablecollections As ITableCollection = map
        Dim table As ITable
        Dim table_dataset As IDataset

        For i = 0 To tablecollections.TableCount - 1
            table = tablecollections.Table(i)
            table_dataset = table
            If UCase(TableName) = UCase(table_dataset.Name) Then
                Return table
            End If
        Next
        Return Nothing
    End Function

    Public Sub StopEditSession(ByVal SaveChanges As Boolean)

        Dim pEditor As IEditor = My.ArcMap.Editor

        pEditor.StopEditing(SaveChanges)

    End Sub

    Public Function ValidateString(ByVal NameToCheck As String, _
                                   ByVal FormInputFieldName As String, _
                                   ByVal FormInputFile As String) As Boolean

        'Name must be something
        If NameToCheck = "" Then
            MsgBox("The '" & FormInputFieldName & "' is empty.", _
                   MsgBoxStyle.Exclamation, "Invalid Parameter")
            Return False

            'Checks string for invalid characters
        ElseIf InStr(NameToCheck, "`") > 0 Or InStr(NameToCheck, "~") > 0 Or _
           InStr(NameToCheck, "@") > 0 Or InStr(NameToCheck, "#") > 0 Or _
           InStr(NameToCheck, "$") > 0 Or InStr(NameToCheck, "%") > 0 Or _
           InStr(NameToCheck, "^") > 0 Or InStr(NameToCheck, "&") > 0 Or _
           InStr(NameToCheck, "*") > 0 Or InStr(NameToCheck, "(") > 0 Or _
           InStr(NameToCheck, ")") > 0 Or InStr(NameToCheck, "-") > 0 Or _
           InStr(NameToCheck, "+") > 0 Or InStr(NameToCheck, "|") > 0 Or _
           InStr(NameToCheck, "\") > 0 Or InStr(NameToCheck, ",") > 0 Or _
           InStr(NameToCheck, "<") > 0 Or InStr(NameToCheck, ">") > 0 Or _
           InStr(NameToCheck, "?") > 0 Or InStr(NameToCheck, "{") > 0 Or _
           InStr(NameToCheck, "}") > 0 Or InStr(NameToCheck, ".") > 0 Or _
           InStr(NameToCheck, "!") > 0 Or InStr(NameToCheck, "'") > 0 Or _
           InStr(NameToCheck, "[") > 0 Or InStr(NameToCheck, "]") > 0 Or _
           InStr(NameToCheck, ":") > 0 Or InStr(NameToCheck, ";") > 0 Or _
           InStr(NameToCheck, " ") > 0 Or InStr(NameToCheck, """") > 0 Or _
           InStr(NameToCheck, "/") > 0 Then

            MsgBox("Invalid '" & FormInputFieldName & "'." & _
                           vbNewLine & "Avoid using spaces or any " & _
                           "of the following " & _
                           "characters:" & vbNewLine & vbNewLine & _
                           "          `~@#$%^&*()-+|\/,<>?{}.!'[]:;", _
                           MsgBoxStyle.Exclamation, "Invalid Parameter")
            Return False

        End If

        'Name must not be the same as input file name
        If FormInputFile <> "" Then
            If UCase(NameToCheck) = UCase(FormInputFile) Then
                MsgBox("'" & FormInputFieldName & "' cannot " & _
                       "be the same as the input layer name.", _
                       MsgBoxStyle.Exclamation, "Invalid Parameter")
                Return False

            End If
        End If

        Return True

    End Function

    Public Function SumProgramHeader(ByVal sProgram As String, ByVal sDate As String, _
                                  ByVal sTime As String) As String

        Dim f9001 As String = Space(1) & StrDup(81, "=")
        Dim f9002 As String = Space(1) & "{0}" & Space(1) & "{1,-36}" & "{2}" & _
                              Space(1) & "{3,-12}" & "{4}" & Space(1) & "{5}"

        Dim strReturn As String = ""
        strReturn += vbCrLf & String.Format(f9001, "=")
        strReturn += vbCrLf & " LARGE CRATER CLUSTER (LCC) ANALYSIS"
        strReturn += vbCrLf & String.Format(f9001, "=")
        strReturn += vbCrLf
        strReturn += vbCrLf & String.Format(f9002, "Program:", sProgram, "Date:", _
                                            sDate, "Time:", sTime)
        strReturn += vbCrLf

        Return strReturn

    End Function

    Public Function SumEndProgram(ByVal sContent As String, ByVal sSDate As String, _
                                  ByVal sSTime As String) As String

        'Record end time and date
        Dim sCTime As String = Now.Hour.ToString & ":" & Now.Minute.ToString & ":" & _
                             ((CDbl(Now.Second + (Now.Millisecond / 1000))).ToString)
        Dim sCDate As String = Now.Month.ToString & "/" & Now.Day.ToString & "/" & _
                               Now.Year.ToString
        Return vbCrLf & vbCrLf & Space(1) & sContent & vbCrLf & vbCrLf & _
                            SumProgramRunTime(sCTime, sCDate, sSTime, sSDate)


    End Function

    Public Function SumProgramRunTime(ByVal sCTime As String, _
                            ByVal sCDate As String, _
                            ByVal sSTime As String, _
                            ByVal sSDate As String) As String

        Dim sReturn, f8025, f8030, f9002, f9003 As String
        Dim TDelta As Double
        Dim Days, Hours, Minute As Single

        'Assign string formats
        f8025 = "{0}" & Space(1) & "{1,2}" & Space(1) & "{2}" & Space(2) & "{3,2}" & _
                Space(1) & "{4}" & Space(2) & "{5,2}" & Space(1) & "{6}" & Space(2) & _
                "{7,6:F3}" & Space(1) & "{8}"
        f8030 = Space(2) & "{0,11:F3}" & Space(1) & "{1}" & "{2,11:F4}" & Space(1) & _
                "{3}" & "{4,10:F5}" & Space(1) & "{5}" & "{6,9:F6}" & Space(1) & "{7}"
        f9002 = Space(1) & StrDup(81, "=")
        f9003 = Space(1) & StrDup(81, "*")

        'Compute time and assign to string
        sReturn = ""
        sReturn += vbCrLf & String.Format(f9002, "=")
        sReturn += vbCrLf & " Program start time and date:  " & _
                   sSTime & " on " & sSDate
        sReturn += vbCrLf & " Program end time and date:    " & _
                  sCTime & " on " & sCDate
        TDelta = Seconds(sSTime, sSDate, sCTime, sCDate)
        Days = TDelta / 86400
        Hours = TDelta / 3600
        Minute = TDelta / 60
        sReturn += vbCrLf & String.Format(f9003, "*")
        sReturn += vbCrLf & (String.Format(f8025, _
             " Program run time:", Int(Days), "d", _
             Truncate((TDelta - Truncate(Days) * 86400) / 3600), "hr", _
             Truncate((TDelta - Truncate(Hours) * 3600) / 60), "min", _
             TDelta - Truncate(Minute) * 60, "sec"))
        sReturn += vbCrLf & (String.Format(f8030, _
            TDelta, "sec =", Minute, "min =", Hours, "hr =", Days, "d"))
        sReturn += vbCrLf & String.Format(f9002, "=") & vbCrLf

        'Return string with run-time information
        Return sReturn

    End Function

    Public Function Seconds(ByVal sSTime As String, ByVal sSDate As String, _
                            ByVal sCTime As String, ByVal sCDate As String) _
                            As Double

        '*  Determines the total amount of seconds for program to run.

        Dim FIndex, SIndex As Integer
        Dim SDays, SHours, SMins, SSecs, STotal As Double
        Dim CDays, CHours, CMins, CSecs, CTotal As Double
        Dim SecTotal As Double

        FIndex = sSDate.IndexOf("/")
        SDays = sSDate.Substring(FIndex + 1, sSDate.IndexOf("/", FIndex + 1) - (FIndex + 1))
        FIndex = sSTime.IndexOf(":")
        SHours = sSTime.Substring(0, FIndex - 0)
        SIndex = sSTime.IndexOf(":", FIndex + 1)
        SMins = sSTime.Substring(FIndex + 1, sSTime.IndexOf(":", FIndex + 1) - (FIndex + 1))
        SSecs = sSTime.Substring(SIndex + 1, (sSTime.Length - 1) - (SIndex))

        STotal = (SDays * 86400) + (SHours * 3600) + (SMins * 60) + SSecs

        FIndex = sCDate.IndexOf("/")
        CDays = sCDate.Substring(FIndex + 1, sCDate.IndexOf("/", FIndex + 1) - (FIndex + 1))
        FIndex = sCTime.IndexOf(":")
        CHours = sCTime.Substring(0, FIndex - 0)
        SIndex = sCTime.IndexOf(":", FIndex + 1)
        CMins = sCTime.Substring(FIndex + 1, sCTime.IndexOf(":", FIndex + 1) - (FIndex + 1))
        CSecs = sCTime.Substring(SIndex + 1, (sCTime.Length - 1) - (SIndex))

        CTotal = (CDays * 86400) + (CHours * 3600) + (CMins * 60) + CSecs

        SecTotal = CTotal - STotal

        Return SecTotal

    End Function

    Public Function GetGeodeticDist(ByVal Lat1 As Double, _
                                    ByVal Lon1 As Double, _
                                    ByVal Lat2 As Double, _
                                    ByVal Lon2 As Double, _
                                    ByVal semiMajorAxis As Double, _
                                    ByVal semiMinorAxis As Double) _
                                    As Double

        ' From: Vincenty direct formula - T Vincenty, "Direct and Inverse Solutions
        '       of Geodesics on the Ellipsoid with application of nested equations",
        '       Survey Review, vol XXII no 176, 1975
        '       http://www.ngs.noaa.gov/PUBS_LIB/inverse.pdf

        Dim a, b, f, LL1, l, U1, U2, SinU1, CosU1, SinU2, CosU2, Lambda, _
            SinLambda, CosLambda, Sigma, SinSigma, CosSigma, SinAlpha, _
            CosSqAlpha, Cos2SigmaM, C, LambdaP, uSq, A1L1, A1, B1, _
            DeltaSigma, s, Dist As Double
        Dim Counter As Integer

        a = semiMajorAxis
        b = semiMinorAxis
        f = (a - b) / a
        l = ToRad(Lon2 - Lon1)
        U1 = Atan((1 - f) * Tan(ToRad(Lat1)))
        U2 = Atan((1 - f) * Tan(ToRad(Lat2)))
        SinU1 = Sin(U1)
        CosU1 = Cos(U1)
        SinU2 = Sin(U2)
        CosU2 = Cos(U2)
        Lambda = l
        Counter = 0

        Try
            Do
                SinLambda = Sin(Lambda)
                CosLambda = Cos(Lambda)
                SinSigma = Sqrt((CosU2 * SinLambda) * (CosU2 * SinLambda) + _
                              (CosU1 * SinU2 - SinU1 * CosU2 * CosLambda) * _
                              (CosU1 * SinU2 - SinU1 * CosU2 * CosLambda))
                If SinSigma = 0 Then
                    Return 0 'Coincident points
                End If
                CosSigma = SinU1 * SinU2 + CosU1 * CosU2 * CosLambda
                Sigma = ATan2(SinSigma, CosSigma)
                SinAlpha = CosU1 * CosU2 * SinLambda / SinSigma
                CosSqAlpha = 1 - SinAlpha * SinAlpha
                Cos2SigmaM = CosSigma - 2 * SinU1 * SinU2 / CosSqAlpha
                If IsNumeric(Cos2SigmaM) = False Then
                    Cos2SigmaM = 0
                End If
                C = f / 16 * CosSqAlpha * (4 + f * (4 - 3 * CosSqAlpha))
                LambdaP = Lambda
                LL1 = -1 + 2 * Cos2SigmaM * Cos2SigmaM
                Lambda = l + (1 - C) * f * SinAlpha * (Sigma + C * SinSigma * _
                                            (Cos2SigmaM + C * CosSigma * LL1))
                If Abs(Lambda - LambdaP) > (10 ^ (-12)) Or Counter >= 100 Then
                    Exit Do
                End If
                Counter = Counter + 1
            Loop
        Catch ex As Exception
            MsgBox("GGD: formula failed to converge.", MsgBoxStyle.Critical, _
                   "GGD Vincenty's Formula Error")
            Return 0
        End Try

        uSq = CosSqAlpha * (a * a - b * b) / (b * b)
        A1L1 = 320 - 175 * uSq
        A1 = 1 + uSq / 16384 * (4096 + uSq * (-768 + uSq * A1L1))
        B1 = uSq / 1024 * (256 + uSq * (-128 + uSq * (74 - 47 * uSq)))
        DeltaSigma = B1 * SinSigma * (Cos2SigmaM + B1 / 4 * (CosSigma * _
                    (-1 + 2 * Cos2SigmaM * Cos2SigmaM) - B1 / 6 * Cos2SigmaM * _
                    (-3 + 4 * SinSigma * SinSigma) * (-3 + 4 * Cos2SigmaM * Cos2SigmaM)))
        s = b * A1 * (Sigma - DeltaSigma)
        Dist = Round(s, 5)

        Return Dist

    End Function

    Public Function GetFinalGeodeticBearing(ByVal Lat1 As Double, _
                                             ByVal Lon1 As Double, _
                                             ByVal Lat2 As Double, _
                                             ByVal Lon2 As Double, _
                                             ByVal semiMajorAxis As Double, _
                                             ByVal semiMinorAxis As Double) _
                                             As Double

        ' From: Vincenty direct formula - T Vincenty, "Direct and Inverse Solutions
        '       of Geodesics on the Ellipsoid with application of nested equations",
        '       Survey Review, vol XXII no 176, 1975
        '       http://www.ngs.noaa.gov/PUBS_LIB/inverse.pdf

        Dim a, b, f, LL1, l, U1, U2, SinU1, CosU1, SinU2, CosU2, Lambda, _
            SinLambda, CosLambda, Sigma, SinSigma, CosSigma, SinAlpha, _
            CosSqAlpha, Cos2SigmaM, C, LambdaP, uSq, A1L1, A1, B1, _
            DeltaSigma, s As Double
        Dim Counter As Integer

        a = semiMajorAxis
        b = semiMinorAxis
        f = (a - b) / a
        l = ToRad(Lon2 - Lon1)
        U1 = Atan((1 - f) * Tan(ToRad(Lat1)))
        U2 = Atan((1 - f) * Tan(ToRad(Lat2)))
        SinU1 = Sin(U1)
        CosU1 = Cos(U1)
        SinU2 = Sin(U2)
        CosU2 = Cos(U2)
        Lambda = l
        Counter = 0

        Try
            Do
                SinLambda = Sin(Lambda)
                CosLambda = Cos(Lambda)
                SinSigma = Sqrt((CosU2 * SinLambda) * (CosU2 * SinLambda) + _
                              (CosU1 * SinU2 - SinU1 * CosU2 * CosLambda) * _
                              (CosU1 * SinU2 - SinU1 * CosU2 * CosLambda))
                If SinSigma = 0 Then
                    MsgBox("GGB: coincident points", MsgBoxStyle.Critical, _
                           "GGB Vincenty's Formula Error")
                    Return 0
                End If
                CosSigma = SinU1 * SinU2 + CosU1 * CosU2 * CosLambda
                Sigma = ATan2(SinSigma, CosSigma)
                SinAlpha = CosU1 * CosU2 * SinLambda / SinSigma
                CosSqAlpha = 1 - SinAlpha * SinAlpha
                Cos2SigmaM = CosSigma - 2 * SinU1 * SinU2 / CosSqAlpha
                If IsNumeric(Cos2SigmaM) = False Then
                    Cos2SigmaM = 0
                End If
                C = f / 16 * CosSqAlpha * (4 + f * (4 - 3 * CosSqAlpha))
                LambdaP = Lambda
                LL1 = -1 + 2 * Cos2SigmaM * Cos2SigmaM
                Lambda = l + (1 - C) * f * SinAlpha * (Sigma + C * SinSigma * _
                                            (Cos2SigmaM + C * CosSigma * LL1))
                If Abs(Lambda - LambdaP) > (10 ^ (-12)) Or Counter >= 100 Then
                    Exit Do
                End If
                Counter = Counter + 1
            Loop
        Catch ex As Exception
            MsgBox("GGB: formula failed to converge.", MsgBoxStyle.Critical, _
                   "GGB Vincenty's Formula Error")
            Return 0
        End Try

        uSq = CosSqAlpha * (a * a - b * b) / (b * b)
        A1L1 = 320 - 175 * uSq
        A1 = 1 + uSq / 16384 * (4096 + uSq * (-768 + uSq * A1L1))
        B1 = uSq / 1024 * (256 + uSq * (-128 + uSq * (74 - 47 * uSq)))
        DeltaSigma = B1 * SinSigma * (Cos2SigmaM + B1 / 4 * (CosSigma * _
                    (-1 + 2 * Cos2SigmaM * Cos2SigmaM) - B1 / 6 * Cos2SigmaM * _
                    (-3 + 4 * SinSigma * SinSigma) * (-3 + 4 * Cos2SigmaM * Cos2SigmaM)))
        s = b * A1 * (Sigma - DeltaSigma)

        Dim Alpha1, Alpha2, InitBrng, FinalBrng As Double

        Alpha1 = ATan2(CosU2 * SinLambda, CosU1 * SinU2 - SinU1 * CosU2 * CosLambda)
        Alpha2 = ATan2(CosU1 * SinLambda, (-1) * SinU1 * CosU2 + CosU1 * SinU2 * CosLambda)
        InitBrng = Alpha1 * 180 / Pi 'Degrees
        FinalBrng = Alpha2 * 180 / Pi 'Degrees

        If FinalBrng < 0 Then
            Return FinalBrng + 360
        Else
            Return FinalBrng
        End If

    End Function

    Public Function GetNewLatLon(ByVal Lat1 As Double, _
                                 ByVal Lon1 As Double, _
                                 ByVal Brng As Double, _
                                 ByVal Dist As Double, _
                                 ByVal semiMajorAxis As Double, _
                                 ByVal semiMinorAxis As Double) _
                                 As Lat2BLon2B

        ' From: Vincenty direct formula - T Vincenty, "Direct and Inverse Solutions
        '       of Geodesics on the Ellipsoid with application of nested equations",
        '       Survey Review, vol XXII no 176, 1975
        '       http://www.ngs.noaa.gov/PUBS_LIB/inverse.pdf

        Dim a, b, f, s, Alpha1, sinAlpha1, cosAlpha1, Lat1toRad, tanU1, _
            CosU1, SinU1, Sigma1, SinAlpha, CosSqAlpha, uSq, _
            A1L1, A1L2, A1L3, A1, B1, Sigma, SigmaP, Cos2SigmaM, _
            SinSigma, CosSigma, dSL1, dSL2, DeltaSigma, Tmp, Lat2, _
            Lambda, C, LL1, LL2, LL3, l, revAz, Lat2toDeg, _
            Lon1LtoDeg As Double

        'Ellipsoid
        a = semiMajorAxis 'semimajor axis
        b = semiMinorAxis 'semiminor axis
        f = (a - b) / a 'flattening
        s = Dist

        'Equation Start
        Alpha1 = Brng * Pi / 180 'Radians
        sinAlpha1 = Sin(Alpha1)
        cosAlpha1 = Cos(Alpha1)

        Lat1toRad = Lat1 * Pi / 180 'Radians
        tanU1 = (1 - f) * Tan(Lat1toRad)
        CosU1 = 1 / Sqrt((1 + tanU1 ^ 2))
        SinU1 = tanU1 * CosU1
        Sigma1 = ATan2(tanU1, cosAlpha1)
        SinAlpha = CosU1 * sinAlpha1
        CosSqAlpha = 1 - SinAlpha * SinAlpha
        uSq = CosSqAlpha * (a ^ 2 - b ^ 2) / b ^ 2
        A1L1 = 320 - 175 * uSq
        A1L2 = -786 + uSq * A1L1
        A1L3 = 4096 + uSq * A1L2
        A1 = 1 + uSq / 16384 * A1L3
        B1 = uSq / 1024 * (256 + uSq * (-128 + uSq * (74 - 47 * uSq)))

        Sigma = s / (b * A1)
        SigmaP = 2 * Pi
        Do While (Abs(Sigma - SigmaP)) > (10 ^ (-12))
            Cos2SigmaM = Cos(2 * Sigma1 + Sigma)
            SinSigma = Sin(Sigma)
            CosSigma = Cos(Sigma)
            dSL1 = (-3 + 4 * SinSigma ^ 2) * (-3 + 4 * Cos2SigmaM ^ 2)
            dSL2 = (CosSigma * (-1 + 2 * Cos2SigmaM ^ 2) - B1 / 6 * _
                    Cos2SigmaM * dSL1)
            DeltaSigma = B1 * SinSigma * (Cos2SigmaM + B1 / 4 * dSL2)
            SigmaP = Sigma
            Sigma = s / (b * A1) + DeltaSigma
        Loop

        Tmp = SinU1 * SinSigma - CosU1 * CosSigma * cosAlpha1
        Lat2 = ATan2(SinU1 * CosSigma + CosU1 * SinSigma * cosAlpha1, _
                     (1 - f) * Sqrt(SinAlpha ^ 2 + Tmp ^ 2))
        Lambda = ATan2(SinSigma * sinAlpha1, CosU1 * CosSigma - SinU1 * SinSigma * cosAlpha1)
        C = f / 16 * CosSqAlpha * (4 + f * (4 - 3 * CosSqAlpha))
        LL1 = -1 + 2 * Cos2SigmaM ^ 2
        LL2 = Cos2SigmaM + C * CosSigma * LL1
        LL3 = Sigma + C * SinSigma * LL2
        l = Lambda - (1 - C) * f * SinAlpha * LL3

        revAz = ATan2(SinAlpha, -1 * Tmp) 'Final Bearing

        'New LatLon is equal to Lat2toDeg , Lat1LtoDeg
        Lat2toDeg = Lat2 * 180 / Pi
        Lon1LtoDeg = Lon1 + (l * 180 / Pi)

        Dim Results As Lat2BLon2B
        Results.Lat2B = Lat2toDeg
        Results.Lon2B = Lon1LtoDeg

        Return Results

    End Function

    Public Function GetCoriolisNewLon(ByVal OldLongitude As Double, _
                                      ByVal EjectaVelocity As Double, _
                                      ByVal Distance As Double, _
                                      ByVal RotationP As Double) As Double

        Dim Dist As Double                  'Distance Ejected [ejecta]
        Dim V As Double                     'Velocity Ejection [ejecta]
        Dim T As Double                     'Time Ejection [ejecta]
        Dim DeltaT As Double                'Rotation Period (lenght of sidereal day) [planet]
        Dim DeltaA As Double                'Rotation Angle (360 deg) [planet]
        Dim Omega As Double                 'Angular Speed [planet]
        Dim Delta As Double                 'Longitude Initial (from Vincenty's) [ejecta]
        Dim DeltaRad As Double              'Longitude Initial in radians [ejecta]
        Dim LambdaP As Double               'Longitude Final [ejecta]
        Dim LambdaPDeg As Double            'Longitude Final in degrees [ejecta]

        Dist = Distance                     'm
        V = EjectaVelocity                  'm/s
        T = Dist / V                        's
        DeltaT = RotationP                  's
        DeltaA = 2 * Pi                     'rad
        Omega = DeltaA / DeltaT             'rad/s
        Delta = OldLongitude                'deg
        DeltaRad = ToRad(Delta)             'rad
        LambdaP = DeltaRad + (Omega * T)    'rad (the plus sign indicates that the planet 
        '                                    is rotating backwards)
        LambdaPDeg = ToDeg(LambdaP)         'deg

        Return LambdaPDeg                  'deg


    End Function

    Public Function ATan2(ByVal y As Double, ByVal x As Double) As Double
        If x > 0 Then
            Return Atan(y / x)
        ElseIf x < 0 Then
            If y = 0 Then
                Return (Pi - Atan(Abs(y / x)))
            Else
                Return Sign(y) * (Pi - Atan(Abs(y / x)))
            End If
        Else
            If y = 0 Then
                Return 0
            Else
                Return Sign(y) * Pi / 2
            End If
        End If
    End Function

    Public Function ToDeg(ByVal Value As Double) As Double
        Return Value * (180 / Pi)
    End Function

    Public Function ToRad(ByVal Value As Double) As Double
        Return Value * (Pi / 180)
    End Function

    Public Function GetGCS(ByVal pSpatialReference As ISpatialReference) _
                          As IGeographicCoordinateSystem

        Dim ReturnGCS As IGeographicCoordinateSystem = Nothing
        Dim pIPCS As IProjectedCoordinateSystem
        Dim pIGCS As IGeographicCoordinateSystem

        If TypeOf pSpatialReference Is IGeographicCoordinateSystem Then
            ReturnGCS = pSpatialReference
        ElseIf TypeOf pSpatialReference Is IProjectedCoordinateSystem Then
            pIPCS = pSpatialReference
            pIGCS = pIPCS.GeographicCoordinateSystem
            ReturnGCS = pIGCS
        End If

        pIPCS = Nothing
        pIGCS = Nothing

        Return ReturnGCS

    End Function

    Public Function GetFLayerSpatRef(pFLayer As IFeatureLayer) As ISpatialReference

        Dim pGeoDataset As IGeoDataset = CType(pFLayer, IGeoDataset)
        Return pGeoDataset.SpatialReference

    End Function

    Public Function CreateTMercatorPCS(ByVal pGeo As IGeometry, _
                                       ByVal pGCS As IGeographicCoordinateSystem) _
                                       As IProjectedCoordinateSystem4GEN

        'Create a factory.
        Dim pSpatRefFact As ISpatialReferenceFactory2 = _
                New SpatialReferenceEnvironmentClass()

        'Create a projection and unit using the factory.
        Dim pTMProj As IProjectionGEN = TryCast(pSpatRefFact.CreateProjection _
                                          (CInt(esriSRProjectionType. _
                                           esriSRProjection_TransverseMercator)),  _
                                           IProjectionGEN)
        Dim pUnit As ILinearUnit = TryCast(pSpatRefFact.CreateUnit _
                                          (CInt(esriSRUnitType. _
                                           esriSRUnit_Meter)),  _
                                           ILinearUnit)

        'Get the default parameters from the projection.
        Dim pParameters As IParameter() = pTMProj.GetDefaultParameters()

        'Reset several of the parameter values.
        Dim pParameter As IParameter
        pParameter = pParameters(0)
        pParameter.Value = 0
        pParameter = pParameters(1)
        pParameter.Value = 0
        pParameter = pParameters(2)
        pParameter.Value = pGeo.Envelope.UpperLeft.X + (pGeo.Envelope.Width / 2)
        pParameter = pParameters(4)
        pParameter.Value = pGeo.Envelope.UpperLeft.Y - (pGeo.Envelope.Height / 2)

        'Create a projected coordinate system using the Define method.
        Dim pTMPCSEdit As IProjectedCoordinateSystemEdit = _
                                            New ProjectedCoordinateSystemClass()
        Dim NameObj As Object = "Custom_TMercator"
        Dim [Alias] As Object = "CUST_TMERC"
        Dim AbbObj As Object = "CTM"
        Dim RemObj As Object = "Shape Calculations"
        Dim UsageObj As Object = "When calculating shapes"
        Dim pTMGCSObj As Object = TryCast(pGCS, Object)
        Dim pUnitObj As Object = TryCast(pUnit, Object)
        Dim pTMProjObj As Object = TryCast(pTMProj, Object)
        Dim pParametersObj As Object = TryCast(pParameters, Object)
        pTMPCSEdit.Define(NameObj, [Alias], AbbObj, RemObj, UsageObj, pTMGCSObj, _
                          pUnitObj, pTMProjObj, pParametersObj)
        Dim pTMPCS As IProjectedCoordinateSystem4GEN = TryCast(pTMPCSEdit,  _
                                                    IProjectedCoordinateSystem4GEN)

        Return pTMPCS

    End Function

    Public Function CreateAzEquiDistPCS(ByVal Origin As IPoint, _
                                    ByVal pGCS As IGeographicCoordinateSystem) _
                                    As IProjectedCoordinateSystem4GEN

        'Create a factory.
        Dim pSpatRefFact As ISpatialReferenceFactory2 = New SpatialReferenceEnvironmentClass()

        'Create a projection and unit using the factory.
        Dim pAEDProj As IProjectionGEN = TryCast(pSpatRefFact.CreateProjection _
                                               (CInt(esriSRProjectionType. _
                                                esriSRProjection_AzimuthalEquidistant)),  _
                                                IProjectionGEN)
        Dim pUnit As ILinearUnit = TryCast(pSpatRefFact.CreateUnit _
                                          (CInt(esriSRUnitType. _
                                           esriSRUnit_Meter)),  _
                                           ILinearUnit)

        'Get the default parameters from the projection.
        Dim pParameters As IParameter() = pAEDProj.GetDefaultParameters()

        'Reset several of the parameter values.
        Dim pParameter As IParameter
        pParameter = pParameters(0)
        pParameter.Value = 0
        pParameter = pParameters(1)
        pParameter.Value = 0
        pParameter = pParameters(2)
        pParameter.Value = Origin.X
        pParameter = pParameters(3)
        pParameter.Value = Origin.Y

        'Create a projected coordinate system using the Define method.
        Dim pAEDPCSEdit As IProjectedCoordinateSystemEdit = New ProjectedCoordinateSystemClass()
        Dim NameObj As Object = "Custom_AziEquiDist"
        Dim [Alias] As Object = "CUST_AED"
        Dim AbbObj As Object = "CAED"
        Dim RemObj As Object = "Azimuth_Distance Calculations"
        Dim UsageObj As Object = "When calculating Azimuth and Distances"
        Dim pAEDGCSObj As Object = TryCast(pGCS, Object)
        Dim pUnitObj As Object = TryCast(pUnit, Object)
        Dim pAEDProjObj As Object = TryCast(pAEDProj, Object)
        Dim pParametersObj As Object = TryCast(pParameters, Object)
        pAEDPCSEdit.Define(NameObj, [Alias], AbbObj, RemObj, UsageObj, pAEDGCSObj, _
                           pUnitObj, pAEDProjObj, pParametersObj)
        Dim pAEDPCS As IProjectedCoordinateSystem4GEN = TryCast(pAEDPCSEdit,  _
                                                                IProjectedCoordinateSystem4GEN)

        Return pAEDPCS

    End Function

    Public Function GetExtendedDiameterLine(ByVal center As IPoint, _
                                        ByVal tanPoint As IPoint) As IPolyline

        'Extend the line to fit the diameter based on a CenterPoint
        'and a Tangetpoint (Radius)
        Dim pPolyline As IPolyline = New Polyline
        pPolyline.FromPoint = center
        pPolyline.ToPoint = tanPoint

        Dim pPtTo As IPoint = New Point
        pPolyline.QueryPoint(esriSegmentExtension.esriExtendAtFrom, _
                           (-1 * pPolyline.Length), False, pPtTo)
        pPolyline.FromPoint = pPtTo

        Return pPolyline

    End Function

    Public Function FeatureClassExists(ByVal workspace As IWorkspace2, _
                                   ByVal featureClassName As String) As Boolean

        If workspace.NameExists(esriDatasetType.esriDTFeatureClass, featureClassName) Then Return True

        Return False

    End Function

    Public Function CreateFeatureClass(ByVal workspace As IWorkspace2, _
                                       ByVal featureDataset As IFeatureDataset, _
                                       ByVal featureClassName As String, _
                                       ByVal fields As IFields, _
                                       ByVal geometryType As esriGeometryType, _
                                       ByVal spatialReference As ISpatialReference) As IFeatureClass

        If featureClassName = "" Then
            Return Nothing
        End If
        Dim featureClass As IFeatureClass = Nothing
        Dim featureWorkspace As IFeatureWorkspace = CType(workspace, IFeatureWorkspace)
        Dim fcDescription As IFeatureClassDescription = New FeatureClassDescriptionClass()
        Dim ocDescription As IObjectClassDescription = CType(fcDescription, IObjectClassDescription)
        Dim shapeFieldIndex As Integer = fields.FindField(fcDescription.ShapeFieldName)
        Dim shapeField As IField = fields.Field(shapeFieldIndex)
        Dim geometryDef As IGeometryDef = shapeField.GeometryDef
        Dim geometryDefEdit As IGeometryDefEdit = CType(geometryDef, IGeometryDefEdit)
        geometryDefEdit.GeometryType_2 = geometryType
        geometryDefEdit.SpatialReference_2 = spatialReference
        'Create a validated fields collection.
        Dim fieldChecker As IFieldChecker = New FieldCheckerClass()
        Dim enumFieldError As IEnumFieldError = Nothing
        Dim validatedFields As IFields = Nothing
        fieldChecker.ValidateWorkspace = CType(workspace, IWorkspace)
        fieldChecker.Validate(fields, enumFieldError, validatedFields)
        If featureDataset Is Nothing Then
            featureClass = featureWorkspace.CreateFeatureClass(featureClassName, validatedFields, _
                                                               ocDescription.InstanceCLSID, _
                                                               ocDescription.ClassExtensionCLSID, _
                                                               esriFeatureType.esriFTSimple, _
                                                               fcDescription.ShapeFieldName, _
                                                               "")
        Else
            featureClass = featureDataset.CreateFeatureClass(featureClassName, validatedFields, _
                                                               ocDescription.InstanceCLSID, _
                                                               ocDescription.ClassExtensionCLSID, _
                                                               esriFeatureType.esriFTSimple, _
                                                               fcDescription.ShapeFieldName, _
                                                               "")
        End If

        Return featureClass

    End Function

    Public Function GetClusterReqFields(ByVal withDefaultFields As Boolean, _
                                        Optional isIntersect As Boolean = False) As IFields

        Dim fieldsEdit As IFieldsEdit
        Dim fields As IFields
        If withDefaultFields Then
            'Create the fields using the required fields method
            Dim objectClassDescription As IObjectClassDescription = New FeatureClassDescriptionClass
            fields = objectClassDescription.RequiredFields
            fieldsEdit = CType(fields, IFieldsEdit)
        Else
            fieldsEdit = New FieldsClass
        End If

        '1. Cluster ID
        Dim field As IField = New FieldClass
        Dim fieldEdit As IFieldEdit = CType(field, IFieldEdit)
        fieldEdit.Name_2 = "cid"
        fieldEdit.Type_2 = esriFieldType.esriFieldTypeInteger
        fieldEdit.IsNullable_2 = True
        fieldEdit.AliasName_2 = "cid"
        fieldEdit.Editable_2 = True
        fieldsEdit.AddField(field)
        '2. Cluster count
        field = New FieldClass
        fieldEdit = CType(field, IFieldEdit)
        fieldEdit.Name_2 = "cnt"
        fieldEdit.Type_2 = esriFieldType.esriFieldTypeInteger
        fieldEdit.IsNullable_2 = True
        fieldEdit.AliasName_2 = "cnt"
        fieldEdit.Editable_2 = True
        fieldsEdit.AddField(field)
        If isIntersect Then
            '3. Centroid to points Mean
            field = New FieldClass
            fieldEdit = CType(field, IFieldEdit)
            fieldEdit.Name_2 = "distmean"
            fieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble
            fieldEdit.IsNullable_2 = True
            fieldEdit.AliasName_2 = "distmean"
            fieldEdit.Editable_2 = True
            fieldsEdit.AddField(field)
            '4. Centroid to points Standard deviation
            field = New FieldClass
            fieldEdit = CType(field, IFieldEdit)
            fieldEdit.Name_2 = "diststdev"
            fieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble
            fieldEdit.IsNullable_2 = True
            fieldEdit.AliasName_2 = "diststdev"
            fieldEdit.Editable_2 = True
            fieldsEdit.AddField(field)
        End If
        'Mean iflat
        field = New FieldClass
        fieldEdit = CType(field, IFieldEdit)
        fieldEdit.Name_2 = "mean_iflat"
        fieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble
        fieldEdit.IsNullable_2 = True
        fieldEdit.AliasName_2 = "mran_iflat"
        fieldEdit.Editable_2 = True
        fieldsEdit.AddField(field)
        'Pass to fields
        fields = CType(fieldsEdit, IFields)

        Return fields

    End Function

    Public Function GetDirDisReqFields(ByVal withDefaultFields As Boolean, ByVal isEllipse As Boolean) As IFields

        Dim fieldsEdit As IFieldsEdit
        Dim fields As IFields
        If withDefaultFields Then
            'Create the fields using the required fields method
            Dim objectClassDescription As IObjectClassDescription = New FeatureClassDescriptionClass
            fields = objectClassDescription.RequiredFields
            fieldsEdit = CType(fields, IFieldsEdit)
        Else
            fieldsEdit = New FieldsClass
        End If

        '1. Cluster ID
        Dim field As IField = New FieldClass
        Dim fieldEdit As IFieldEdit = CType(field, IFieldEdit)
        fieldEdit.Name_2 = "cid"
        fieldEdit.Type_2 = esriFieldType.esriFieldTypeInteger
        fieldEdit.IsNullable_2 = True
        fieldEdit.AliasName_2 = "cid"
        fieldEdit.Editable_2 = True
        fieldsEdit.AddField(field)
        '2. Cluster count
        field = New FieldClass
        fieldEdit = CType(field, IFieldEdit)
        fieldEdit.Name_2 = "cnt"
        fieldEdit.Type_2 = esriFieldType.esriFieldTypeInteger
        fieldEdit.IsNullable_2 = True
        fieldEdit.AliasName_2 = "cnt"
        fieldEdit.Editable_2 = True
        fieldsEdit.AddField(field)
        '3. Inverse flattening
        field = New FieldClass
        fieldEdit = CType(field, IFieldEdit)
        fieldEdit.Name_2 = "iflat"
        fieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble
        fieldEdit.IsNullable_2 = True
        fieldEdit.AliasName_2 = "iflat"
        fieldEdit.Editable_2 = True
        fieldsEdit.AddField(field)
        '4. Major axis length
        field = New FieldClass
        fieldEdit = CType(field, IFieldEdit)
        fieldEdit.Name_2 = "majaxis"
        fieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble
        fieldEdit.IsNullable_2 = True
        fieldEdit.AliasName_2 = "majaxis"
        fieldEdit.Editable_2 = True
        fieldsEdit.AddField(field)
        If isEllipse Then
            '5. From end, from point X
            field = New FieldClass
            fieldEdit = CType(field, IFieldEdit)
            fieldEdit.Name_2 = "ffx"
            fieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble
            fieldEdit.IsNullable_2 = True
            fieldEdit.AliasName_2 = "ffx"
            fieldEdit.Editable_2 = True
            fieldsEdit.AddField(field)
            '6. From end, from point Y
            field = New FieldClass
            fieldEdit = CType(field, IFieldEdit)
            fieldEdit.Name_2 = "ffy"
            fieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble
            fieldEdit.IsNullable_2 = True
            fieldEdit.AliasName_2 = "ffy"
            fieldEdit.Editable_2 = True
            fieldsEdit.AddField(field)
            '7. From end, to point X
            field = New FieldClass
            fieldEdit = CType(field, IFieldEdit)
            fieldEdit.Name_2 = "ftx"
            fieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble
            fieldEdit.IsNullable_2 = True
            fieldEdit.AliasName_2 = "ftx"
            fieldEdit.Editable_2 = True
            fieldsEdit.AddField(field)
            '8. From end, to point Y
            field = New FieldClass
            fieldEdit = CType(field, IFieldEdit)
            fieldEdit.Name_2 = "fty"
            fieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble
            fieldEdit.IsNullable_2 = True
            fieldEdit.AliasName_2 = "fty"
            fieldEdit.Editable_2 = True
            fieldsEdit.AddField(field)
            '9. To end, from point X
            field = New FieldClass
            fieldEdit = CType(field, IFieldEdit)
            fieldEdit.Name_2 = "tfx"
            fieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble
            fieldEdit.IsNullable_2 = True
            fieldEdit.AliasName_2 = "tfx"
            fieldEdit.Editable_2 = True
            fieldsEdit.AddField(field)
            '10. To end, from point Y
            field = New FieldClass
            fieldEdit = CType(field, IFieldEdit)
            fieldEdit.Name_2 = "tfy"
            fieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble
            fieldEdit.IsNullable_2 = True
            fieldEdit.AliasName_2 = "tfy"
            fieldEdit.Editable_2 = True
            fieldsEdit.AddField(field)
            '11. To end, to point X
            field = New FieldClass
            fieldEdit = CType(field, IFieldEdit)
            fieldEdit.Name_2 = "ttx"
            fieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble
            fieldEdit.IsNullable_2 = True
            fieldEdit.AliasName_2 = "ttx"
            fieldEdit.Editable_2 = True
            fieldsEdit.AddField(field)
            '12. To end, to point Y
            field = New FieldClass
            fieldEdit = CType(field, IFieldEdit)
            fieldEdit.Name_2 = "tty"
            fieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble
            fieldEdit.IsNullable_2 = True
            fieldEdit.AliasName_2 = "tty"
            fieldEdit.Editable_2 = True
            fieldsEdit.AddField(field)
        End If

        'Pass to fields
        fields = CType(fieldsEdit, IFields)

        Return fields

    End Function

    Public Function GetTrajectoryReqFields(ByVal withDefaultFields As Boolean) As IFields

        Dim fieldsEdit As IFieldsEdit
        Dim fields As IFields
        If withDefaultFields Then
            'Create the fields using the required fields method
            Dim objectClassDescription As IObjectClassDescription = New FeatureClassDescriptionClass
            fields = objectClassDescription.RequiredFields
            fieldsEdit = CType(fields, IFieldsEdit)
        Else
            fieldsEdit = New FieldsClass
        End If

        '1. Cluster ID
        Dim field As IField = New FieldClass
        Dim fieldEdit As IFieldEdit = CType(field, IFieldEdit)
        fieldEdit.Name_2 = "cid"
        fieldEdit.Type_2 = esriFieldType.esriFieldTypeInteger
        fieldEdit.IsNullable_2 = True
        fieldEdit.AliasName_2 = "cid"
        fieldEdit.Editable_2 = True
        fieldsEdit.AddField(field)

        '2. Parent Shapefile
        field = New FieldClass
        fieldEdit = CType(field, IFieldEdit)
        fieldEdit.Name_2 = "ParentFeat"
        fieldEdit.AliasName_2 = "Parent Feature"
        fieldEdit.Type_2 = esriFieldType.esriFieldTypeString
        fieldEdit.IsNullable_2 = True
        fieldEdit.Length_2 = 100
        fieldEdit.Editable_2 = True
        fieldsEdit.AddField(field)

        '3. Inverse flattening
        field = New FieldClass
        fieldEdit = CType(field, IFieldEdit)
        fieldEdit.Name_2 = "iflat"
        fieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble
        fieldEdit.IsNullable_2 = True
        fieldEdit.AliasName_2 = "iflat"
        fieldEdit.Editable_2 = True
        fieldsEdit.AddField(field)


        'Pass to fields
        fields = CType(fieldsEdit, IFields)

        Return fields

    End Function

    Public Function FindNearest(ByRef pArray(,) As Double, _
                                ByRef dFeature1(,) As Double, _
                                ByVal dSemiMajAxis As Double, _
                                ByVal dSemiMinAxis As Double, _
                                ByVal bPlan As Boolean) As Double 'Euclidean or Geodesic

        Dim dDist, dDistOld As Double



        If CInt(dFeature1(0, 0)) = pArray(1, pArray.GetLowerBound(1)) Then
            dDistOld = GetDist(pArray(2, pArray.GetLowerBound(1) + 1), _
                               pArray(3, pArray.GetLowerBound(1) + 1), _
                               dFeature1(1, 0), dFeature1(2, 0), dSemiMajAxis, dSemiMinAxis, bPlan)
        Else
            dDistOld = GetDist(pArray(2, pArray.GetLowerBound(1)), _
                               pArray(3, pArray.GetLowerBound(1)), _
                               dFeature1(1, 0), dFeature1(2, 0), dSemiMajAxis, dSemiMinAxis, bPlan)
        End If
        For i As Integer = 0 To pArray.GetUpperBound(1)
            dDist = GetDist(pArray(2, i), pArray(3, i), _
                            dFeature1(1, 0), dFeature1(2, 0), dSemiMajAxis, dSemiMinAxis, bPlan)
            If dDist <> 0 And dDist < dDistOld Then
                dDistOld = dDist
            End If
        Next

        Return dDistOld

    End Function

    Public Sub FindNearestBF(ByVal Ar(,) As Double, ByRef feature(,) As Double, ByRef dist(,) As Double, ByVal dSemiMajAxis As Double, ByVal dSemiMinAxis As Double, ByVal knn As Double, ByVal bPlan As Boolean)

        Dim distance As Double
        Dim maxdistance As Double = 0
        Dim counter As Integer = 0
        Dim index As Integer = 0

        For i As Integer = 0 To Ar.GetUpperBound(1) - 1
            distance = GetDist(Ar(2, i), Ar(3, i), feature(1, 0), feature(2, 0), dSemiMajAxis, dSemiMinAxis, bPlan)
            If distance <> 0 Then
                If counter < knn Then
                    'Autoload the first n distances and track where the max distance is in the array
                    dist(0, counter) = Ar(1, i)
                    dist(1, counter) = Ar(2, i)
                    dist(2, counter) = Ar(3, i)
                    dist(3, counter) = distance

                    If distance > maxdistance Then
                        maxdistance = distance
                        index = counter
                    End If
                    counter += 1
                Else
                    'Array is loaded with n values.  Now replace the greatest distance with the new, lower distances.
                    If distance < maxdistance Then
                        dist(0, index) = Ar(1, i)
                        dist(1, index) = Ar(2, i)
                        dist(2, index) = Ar(3, i)
                        dist(3, index) = distance

                        ' Seed the new max distance and check to see which value actually is the max
                        maxdistance = dist(3, index)
                        For j As Integer = 0 To dist.GetUpperBound(1) - 1
                            If dist(3, j) > maxdistance Then
                                maxdistance = dist(3, j)
                                index = j
                            End If
                        Next
                    End If
                End If
            End If
        Next
    End Sub

    Public Function GetDist(ByVal x2 As Double, ByVal y2 As Double, _
                            ByVal x1 As Double, ByVal y1 As Double, _
                            ByVal dSemiMajAxis As Double, _
                            ByVal dSemiMinAxis As Double, _
                            ByVal bPlan As Boolean) As Double

        If bPlan Then
            Return Sqrt(((x2 - x1) ^ 2) + ((y2 - y1) ^ 2)) ' This is the Euclidean distance between 2 pts in 2-D space
        Else
            Return GetGeodeticDist(y1, x1, y2, x2, dSemiMajAxis, dSemiMinAxis) 'This returns the geodesic distance.
        End If

    End Function

    Public Function GetDist2(ByVal x2 As Double, ByVal y2 As Double, _
                        ByVal x1 As Double, ByVal y1 As Double, _
                        ByVal dSemiMajAxis As Double, _
                        ByVal dSemiMinAxis As Double, _
                        ByVal epsilon As Double, _
                        ByVal bPlan As Boolean) As Double

        If bPlan Then
            Return Sqrt(((x2 - x1) ^ 2) + ((y2 - y1) ^ 2)) ' This is the Euclidean distance between 2 pts in 2-D space
        Else
            Return GetGeodeticDist(y1, x1, y2, x2, dSemiMajAxis, dSemiMinAxis) 'This returns the geodesic distance.
        End If

    End Function

    Public Sub ProgressDialogDispose(ByRef pProDlg As IProgressDialog2, _
                                     ByRef pStepPro As IStepProgressor, _
                                     ByRef pTrkCan As ITrackCancel, _
                                     ByRef pProDlgFact As IProgressDialogFactory)

        If Not pProDlg Is Nothing Then
            pProDlg.HideDialog()
            pStepPro = Nothing
            pProDlg = Nothing
            pTrkCan = Nothing
            pProDlgFact = Nothing
        End If

    End Sub

    Public Sub SaveSummaryReport(ByVal sFileName As String, ByVal sText As String)

        Dim MsgYN As MsgBoxResult = MsgBox _
              ("Save a summary file of the analysis?", _
               MsgBoxStyle.YesNo, "Summary File")
        If MsgYN = MsgBoxResult.No Then
            Exit Sub
        Else
            Dim sSumFileName As String = sFileName & "_SUM"

            Dim pSaveFDlg As SaveFileDialog = New SaveFileDialog()
            With pSaveFDlg
                .AddExtension = True
                .AutoUpgradeEnabled = True
                .CheckFileExists = False
                .CheckPathExists = True
                .CreatePrompt = False
                .DefaultExt = ".txt"
                .FileName = sSumFileName
                .Filter = "text files (*.txt)|*.txt"
                .FilterIndex = 1
                .OverwritePrompt = True
                .RestoreDirectory = True
                .ShowHelp = False
                .SupportMultiDottedExtensions = False
                .Title = "Save Summary File"
                .ValidateNames = True
            End With

            If pSaveFDlg.ShowDialog() = DialogResult.OK Then
                File.WriteAllText(pSaveFDlg.FileName, sText)
                MsgYN = MsgBox("Open summary file?", MsgBoxStyle.YesNo, _
                               "Summary File")
                If MsgYN = MsgBoxResult.Yes Then
                    If File.Exists(pSaveFDlg.FileName) Then
                        System.Diagnostics.Process.Start(pSaveFDlg.FileName)
                    End If
                End If
            End If
        End If
    End Sub
    Public Sub SaveLog(ByVal filename As String, ByVal text As String)
        My.Computer.FileSystem.WriteAllText(filename, text, False)
    End Sub
    Public Function GetArcGISLicenseName() As System.String

        Dim esriLicenseInfo As ESRI.ArcGIS.esriSystem.IESRILicenseInfo = New ESRI.ArcGIS.esriSystem.ESRILicenseInfoClass
        Dim string_LicenseLevel As System.String = Nothing

        Select Case esriLicenseInfo.DefaultProduct
            Case ESRI.ArcGIS.esriSystem.esriProductCode.esriProductCodeBasic
                string_LicenseLevel = "Basic"
            Case ESRI.ArcGIS.esriSystem.esriProductCode.esriProductCodeStandard
                string_LicenseLevel = "Standard"
            Case ESRI.ArcGIS.esriSystem.esriProductCode.esriProductCodeAdvanced
                string_LicenseLevel = "Advanced"
        End Select

        Return string_LicenseLevel

    End Function
    

    Public Sub ExtractNearest(ByRef Ar, ByVal distance_table, ByVal n)

        'Open the distance table
        Dim table = getTableByName(distance_table)
        Dim InFid As Integer = table.FindField("IN_FID")
        Dim NearDist As Integer = table.FindField("NEAR_DIST")

        'Generate the search curosr
        Dim cursor As ICursor = table.Search(Nothing, True)
        Dim row As IRow = cursor.NextRow()
        'If the workspace is a geodatabase subtract 1 from row.Value(InFID)
        'Else we are working with a shapefile, subtract 0.
        'Iterate through the rows.
        While Not row Is Nothing
            If row.Value(NearDist) < Ar(4, row.Value(InFid) - n) Then
                Ar(4, row.Value(InFid) - n) = row.Value(NearDist)
            End If
            row = cursor.NextRow()


        End While

    End Sub



#Region "DBScan"
    Public Function getNeighbors(ByVal epsilon As Double, ByVal index As Double, ByVal semimajor As Double, ByVal semiminor As Double, ByVal measurement_space As Boolean) As Stack(Of Integer)
        Dim neighbors As New Stack(Of Integer)
        Dim dist As Double = 0.0
        For i As Integer = 0 To dist_lists.Count - 1
            dist = GetDist(dist_lists(index)(2), dist_lists(index)(3), dist_lists(i)(2), dist_lists(i)(3), semimajor, semiminor, measurement_space)
            If dist <= epsilon Then
                neighbors.Push(i) 'By Index
            End If
        Next

        Return neighbors
    End Function

    Public Function ExpandCluster(ByRef neighbors As Stack(Of Integer), ByRef cluster_id As Integer, ByVal epsilon As Double, ByVal minpts As Integer, _
                                   ByRef Ar2() As Double, ByVal Unvisited As List(Of Integer), ByVal semimajor As Double, ByVal semiminor As Double, ByVal measurement_space As Boolean) As List(Of Integer)

        Dim new_neighbors As New Stack(Of Integer)
        Dim neighbor_node As Integer = 0
        'Dim node_index As Integer = 0
        Dim new_neighbor As Integer = 0
        Dim visited As New List(Of Integer)

        'Increment the cluster counter
        cluster_id = cluster_id + 1


        'For each neighbor in neighbors
        While neighbors.Count > 0

            'Iterate over the neighbors, popping one at a time from the stack
            neighbor_node = neighbors.Pop
            'Add node to the visited list
            visited.Add(neighbor_node)

            'If we have not visited the node yet, check to see if the cluster extends
            If Unvisited.Contains(dist_lists(neighbor_node)(0)) Then
                'Mark neighbor as visited
                Unvisited.Remove(dist_lists(neighbor_node)(0))
                'Get the index of the neighbor_node in the dist_lists
                'node_index = GetNodeIndex(neighbor_node)
                'Get the neighbors to the new neighbor, i.e. is the cluster expanding by epsilon
                new_neighbors = getNeighbors(epsilon, neighbor_node, semimajor, semiminor, measurement_space)
                'If the number of new neighbors constitutes a new cluster, start adding that cluster as well.  Grow by density essentially.
                If new_neighbors.Count + Unvisited.Count + neighbors.Count > minpts Then
                    Do Until new_neighbors.Count = 0
                        new_neighbor = new_neighbors.Peek
                        If visited.Contains(new_neighbor) Or neighbors.Contains(new_neighbor) Then
                            new_neighbors.Pop()
                        Else
                            neighbors.Push(new_neighbors.Pop)
                        End If
                    Loop
                End If
            End If

            'If the neighbor is not part of a cluster, add it to the current cluster.
            If Ar2(dist_lists(neighbor_node)(0)) = -1 Then Ar2(dist_lists(neighbor_node)(0)) = cluster_id

        End While
        Return Unvisited
    End Function

    Public Function GetNodeIndex(ByVal node As Integer) As Integer
        For i As Integer = 0 To dist_lists.Count - 1
            If dist_lists(i)(0) = node Then
                node = i
            End If
        Next
        Return node
    End Function
#End Region

    Public Class KDTree
        Private median As Double
        Private left As List(Of Double)
        Private right As List(Of Double)

        'Public Sub New(ByVal t1 As Object, ByVal t2 As Object)
        '    Me.t1 = t1
        '    Me.t2 = t2
        'End Sub
        'Public Property m_t1() As Object
        '    Get
        '        Return t1
        '    End Get
        '    Set(ByVal value As Object)
        '        t1 = t1
        '    End Set
        'End Property
        'Public Property m_t2() As Object
        '    Get
        '        Return t2
        '    End Get
        '    Set(ByVal value As Object)
        '        t2 = t2
        '    End Set
        'End Property
    End Class

#Region "KDTree"
    'This is a stub for a 2D-Tree that should significantly improve performance of the primary impact approximation tool.
    Public Function Build_KD_Tree(ByVal depth As Integer, ByVal Points(,) As Double)
        '1. Check to see that the tree has enough points.  Otherwise this recursively loops forever
        If Points.GetUpperBound(1) - 1 = 0 Then
            'Return
        ElseIf Points.GetUpperBound(1) - 1 = 1 Then
            'Create a Leaf
        End If

        '2. Partition the points and get the median
        If Points.GetUpperBound(1) - 1 Mod 2 = 0 Then
            'Even depth, partition on the x value
            partitionx(Points)
        Else
            'Odd depth, partition on the y value
            partitiony(Points)
        End If

        'left_children = Build_KD_Tree(depth +1, left_children)
        'right_children = Build_KD_Tree(depth + 1, right_children)

        'Return median, left_children, right_children
    End Function

    Private Function partitionx(ByVal Points)
        'Sort by the x coordinate
        'find the median, check that the median value is unique, if not step down until it is
        'Partition the list at the median, into lt and gt equal to
        ' return the median, the lt list and the gt list
    End Function

    Private Function partitiony(ByVal Points)
        'Sort by the y coordinate
        'find the median, check that the median value is unique, if not step down until it is
        'Partition the list at the median, into lt and gt equal to
        ' return the median, the lt list and the gt list
    End Function

    Public Function nearest_neighbor(ByVal lval As Double, ByVal rval As Double, ByVal kdtree As KDTree)
        'medianValue = kdtree.median
        'Dim SubTree As KDTree
        'If lval < medianValue Then
        '    subtree = kdtree.leftchildren
        'Else
        '    subtree = kdtree.rightchildren
        'End If

        'retval = nearest_neighbor(rval, lval, SubTree)

        'Return retval
        Return Nothing
    End Function

#End Region
End Module

