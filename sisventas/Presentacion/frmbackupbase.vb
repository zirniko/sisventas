Public Class frmbackupbase

    Private Sub BtnBackup_Click(sender As System.Object, e As System.EventArgs) Handles BtnBackup.Click
        Try
            Dim func As New fbackup

            If func.backupbase Then
                MessageBox.Show("backup generado correctamente", "respaldo", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("backup NO  generado", "respaldo", MessageBoxButtons.OK, MessageBoxIcon.Error)

            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class