Public Class btn_intersecttool
  Inherits ESRI.ArcGIS.Desktop.AddIns.Button

  Public Sub New()

  End Sub

  Protected Overrides Sub OnClick()

        m_intersectForm = New frm_intersecttool

        m_intersectForm.ShowDialog()

  End Sub

  Protected Overrides Sub OnUpdate()
        Enabled = My.ArcMap.Application IsNot Nothing
  End Sub
End Class
