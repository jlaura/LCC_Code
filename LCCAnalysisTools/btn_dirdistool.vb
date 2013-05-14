Public Class btn_dirdistool
  Inherits ESRI.ArcGIS.Desktop.AddIns.Button

  Public Sub New()

  End Sub

  Protected Overrides Sub OnClick()

        m_dirdisForm = New frm_dirdistool

        m_dirdisForm.ShowDialog()

  End Sub

  Protected Overrides Sub OnUpdate()
        Enabled = My.ArcMap.Application IsNot Nothing
  End Sub
End Class
