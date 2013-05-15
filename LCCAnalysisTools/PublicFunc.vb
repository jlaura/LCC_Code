Imports Microsoft.VisualBasic

Public Module PublicModule

    Public Function CalcStats(ByVal Title As String, ByVal inFLayer As String, _
                              ByVal FieldName As String) As String

        Dim pFLayer As IFeatureLayer = GetFLayerByName(inFLayer)

        Dim pFClass As IFeatureClass = pFLayer.FeatureClass
        Dim pFields As IFields = pFClass.Fields
        Dim pFCursor As IFeatureCursor = pFClass.Search(Nothing, False)
        Dim intField As Integer = pFields.FindField(FieldName)
        Dim sReport As String = ""
        Dim intFCount, intFTotal As Integer
        Dim dVal, dSum, dMin, dMax, dRange, dMean, dSumSqr, _
            dStDev, dArray(), dMedian, dLQ, dUQ, dIQ As Double
        Dim pFeature As IFeature = pFCursor.NextFeature

        intFTotal = pFClass.FeatureCount(Nothing) - 1
        ReDim dArray(intFTotal)
        dMin = pFeature.Value(intField)
        dMax = pFeature.Value(intField)

        For intFCount = 0 To intFTotal
            'Value
            dVal = pFeature.Value(intField)
            'Array
            dArray(intFCount) = dVal
            'Sum
            dSum = dSum + dVal
            'Netx feature
            pFeature = pFCursor.NextFeature
        Next
        'Mean
        dMean = dSum / intFTotal
        'Sort array
        System.Array.Sort(dArray)
        'Min
        dMin = dArray(dArray.GetLowerBound(0))
        'Max
        dMax = dArray(dArray.GetUpperBound(0))
        'Range
        dRange = dMax - dMin

        dSumSqr = 0

        pFCursor = pFClass.Search(Nothing, False)
        pFeature = pFCursor.NextFeature
        For intFCount = 0 To intFTotal
            dVal = pFeature.Value(intField)
            'Variance
            dSumSqr = dSumSqr + (dVal - dMean) ^ 2
            'Next feature
            pFeature = pFCursor.NextFeature
        Next
        'Standard deviation
        dStDev = Sqrt(dSumSqr / (intFTotal - 1))

        'Approximate: Median, Lower quartile, Upper quartile
        If IEEERemainder(intFCount, 2) = 0 Then
            dMedian = (dArray(Round(intFTotal * 0.5)) + _
                       dArray(Round((intFTotal * 0.5) + 1))) / 2
            dLQ = (dArray(Round(intFTotal * 0.25)) + _
                   dArray(Round((intFTotal * 0.25) + 1))) / 2
            dUQ = (dArray(Round(intFTotal * 0.75)) + _
                   dArray(Round((intFTotal * 0.75) + 1))) / 2
        Else
            dMedian = dArray(Round(intFTotal * 0.5))
            dLQ = dArray(Round(intFTotal * 0.25))
            dUQ = dArray(Round(intFTotal * 0.75))
        End If
        'Approximate: Interquartile range
        dIQ = dUQ - dLQ

        Dim f100 As String = Space(3) & "|" & "{0,-20}" & "{1}" & "{2,16:F4}" & "|"
        Dim f300 As String = Space(3) & "|" & StrDup(37, "-") & "|"
        Dim f400 As String = Space(3) & "|" & StrDup(37, "_") & "|"
        Dim f500 As String = Space(3) & StrDup(39, "_")

        sReport = vbCrLf & vbCrLf & _
                  Space(3) & "STATS: " & Title & vbCrLf & _
                  (String.Format(f500, "_")) & vbCrLf & _
                  (String.Format(f100, "Population total", "=", intFTotal + 1)) & vbCrLf & _
                  (String.Format(f300, "-")) & vbCrLf & _
                  (String.Format(f100, "  0% Min", "=", dMin)) & vbCrLf & _
                  (String.Format(f100, " 25% Lower quartile", "=", dLQ)) & vbCrLf & _
                  (String.Format(f100, " 50% Median", "=", dMedian)) & vbCrLf & _
                  (String.Format(f100, " 75% Upper quartile", "=", dUQ)) & vbCrLf & _
                  (String.Format(f100, "100% Max", "=", dMax)) & vbCrLf & _
                  (String.Format(f300, "-")) & vbCrLf & _
                  (String.Format(f100, "Range", "=", dRange)) & vbCrLf & _
                  (String.Format(f100, "Mean", "=", dMean)) & vbCrLf & _
                  (String.Format(f100, "St Dev", "=", dStDev)) & vbCrLf & _
                  (String.Format(f100, "Interquartile range", "=", dIQ)) & vbCrLf & _
                  (String.Format(f400, "_")) & vbCrLf

        Return sReport

    End Function
End Module
