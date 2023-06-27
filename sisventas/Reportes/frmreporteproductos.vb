Public Class frmreporteproductos

    Private Sub frmreporteproductos_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'TODO: esta línea de código carga datos en la tabla 'sisventasDataSet.mostrar_producto' Puede moverla o quitarla según sea necesario.
        Me.mostrar_productoTableAdapter.Fill(Me.sisventasDataSet.mostrar_producto)

        Me.ReportViewer1.RefreshReport()
    End Sub
End Class