﻿Public Class btn_trajecttool
  Inherits ESRI.ArcGIS.Desktop.AddIns.Button

  Public Sub New()

  End Sub

  Protected Overrides Sub OnClick()

        m_trajectForm = New frm_trajecttool

        m_trajectForm.ShowDialog()

  End Sub

  Protected Overrides Sub OnUpdate()
        Enabled = My.ArcMap.Application IsNot Nothing
  End Sub
End Class
