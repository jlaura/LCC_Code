Imports System.Windows.Forms.DataVisualization.Charting
Public Class kDistance
    Private Sub kDistance_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load

        Chart1.Series.Clear()
        Chart1.Series.Add("kdist")
        Chart1.Series(0).ChartType = SeriesChartType.Point

        Chart1.ChartAreas(0).CursorX.IsUserSelectionEnabled = True
        Chart1.ChartAreas(0).CursorY.IsUserSelectionEnabled = True

        System.Array.Sort(kdist)

        For i As Integer = 1 To kdist.GetUpperBound(0) - 1
            Dim DataPoint As New DataPoint(i, kdist(i))
            Chart1.Series(0).Points.AddXY(i, kdist(i))
        Next i

    End Sub
End Class