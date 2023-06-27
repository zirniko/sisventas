Public Class frmlogin

    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        lblhora.Text = TimeOfDay
    End Sub

    Private Sub frmlogin_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        lblhora.Text = TimeOfDay
    End Sub

    Private Sub BtnSalir_Click(sender As System.Object, e As System.EventArgs) Handles BtnSalir.Click
        End
    End Sub

    Private Sub BtnIngresar_Click(sender As System.Object, e As System.EventArgs) Handles BtnIngresar.Click
        Try
            Dim dts As New vusuario
            Dim func As New fusuario

            dts.glogin = txtlogin.Text
            dts.gpassword = txtpassword.Text

            If func.validar_usuario(dts) = True Then
                frminicio.Show()
                Me.Hide()
            Else
                MsgBox("ingrese nuevamente sus datos correctos", MsgBoxStyle.Information, "Acceso Denegado al sistema")
                txtpassword.Clear()
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class