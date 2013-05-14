Public Class btn_clustertool
  Inherits ESRI.ArcGIS.Desktop.AddIns.Button

  Public Sub New()

  End Sub

  Protected Overrides Sub OnClick()

        m_clusterForm = New frm_clustertool

        m_clusterForm.ShowDialog()

  End Sub

  Protected Overrides Sub OnUpdate()
        Enabled = My.ArcMap.Application IsNot Nothing
  End Sub
End Class
