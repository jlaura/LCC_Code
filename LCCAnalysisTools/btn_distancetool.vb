Public Class distanceMatrix
    Inherits ESRI.ArcGIS.Desktop.AddIns.Button

    Public Sub New()

    End Sub

    Protected Overrides Sub OnClick()
        m_distanceForm = New frm_distancetool
        m_distanceForm.ShowDialog()
    End Sub

    Protected Overrides Sub OnUpdate()

    End Sub
End Class
