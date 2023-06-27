Imports System.ComponentModel
Public Class frmdetalle_venta
    Private dt As New DataTable
    Private Sub frmdetalle_venta_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        mostrar()
    End Sub

    Public Sub limpiar()
        BtnGuardar.Visible = True
        txtidproducto.Text = ""
        txtnombre_producto.Text = ""
        txtprecio_unitario.Text = ""
        txtcantidad.Text = 0
        txtstock.Text = 1
    End Sub

    Private Sub mostrar()
        Try
            Dim func As New fdetalle_venta
            dt = func.mostrar
            datalistado.Columns.Item("Eliminar").Visible = False

            If dt.Rows.Count <> 0 Then
                datalistado.DataSource = dt
                datalistado.ColumnHeadersVisible = True
                Inexistente.Visible = False

            Else
                datalistado.DataSource = Nothing
                datalistado.ColumnHeadersVisible = False
                Inexistente.Visible = True
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Btnuevo.Visible = True

        buscar()

    End Sub


    Private Sub buscar()
        Try
            Dim ds As New DataSet
            ds.Tables.Add(dt.Copy)
            Dim dv As New DataView(ds.Tables(0))


            dv.RowFilter = "idventa='" & txtidventa.Text & "'"

            If dv.Count <> 0 Then
                Inexistente.Visible = False
                datalistado.DataSource = dv
                ocultar_columnas()
            Else
                Inexistente.Visible = True
                datalistado.DataSource = Nothing
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ocultar_columnas()
        datalistado.Columns(1).Visible = False
        datalistado.Columns(2).Visible = False

        datalistado.Columns(3).Visible = False
    End Sub


    Private Sub Btnuevo_Click(sender As Object, e As EventArgs) Handles Btnuevo.Click
        limpiar()
        mostrar()

    End Sub

    Private Sub BtnGuardar_Click(sender As Object, e As EventArgs) Handles BtnGuardar.Click
        If Me.ValidateChildren = True And txtidproducto.Text <> "" And txtcantidad.Text <> "" And txtprecio_unitario.Text <> "" Then
            Try
                Dim dts As New vdetalle_venta
                Dim func As New fdetalle_venta

                dts.gidventa = txtidventa.Text
                dts.gidproducto = txtidproducto.Text
                dts.gcantidad = txtcantidad.Text
                dts.gprecio_unitario = txtprecio_unitario.Text


                If func.insertar(dts) Then
                    If func.disminuir_stock(dts) Then

                    End If
                    MessageBox.Show("Articulo añadido correctamente,añadir productos", "Datos guardado", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    mostrar()
                    limpiar()
                Else
                    MessageBox.Show("Articulo NO añadido ", "ERROR guardado", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    mostrar()
                    limpiar()
                End If

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Else
            MessageBox.Show("Falta ingresar datos", "NO guardado", MessageBoxButtons.OK, MessageBoxIcon.Information)

        End If
    End Sub

    Private Sub datalistado_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles datalistado.CellContentClick
        If e.ColumnIndex = Me.datalistado.Columns.Item("Eliminar").Index Then
            Dim chkcell As DataGridViewCheckBoxCell = Me.datalistado.Rows(e.RowIndex).Cells("Eliminar")
            chkcell.Value = Not chkcell.Value
        End If
    End Sub




    Private Sub cbeliminar_CheckedChanged(sender As Object, e As EventArgs) Handles cbeliminar.CheckedChanged
        If cbeliminar.CheckState = CheckState.Checked Then
            datalistado.Columns.Item("Eliminar").Visible = True
        Else
            datalistado.Columns.Item("Eliminar").Visible = False
        End If
    End Sub
    Private Sub BtnEliminar_Validating(sender As Object, e As CancelEventArgs)

    End Sub

    Private Sub txtbuscar_TextChanged(sender As Object, e As EventArgs)
        buscar()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim result As DialogResult
        result = MessageBox.Show("Desea eliminar los articulos de la venta?", "Eliminando registros", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)

        If result = DialogResult.OK Then
            Try
                For Each row As DataGridViewRow In datalistado.Rows
                    Dim marcado As Boolean = Convert.ToBoolean(row.Cells("Eliminar").Value)

                    If marcado Then
                        Dim onekey As Integer = Convert.ToInt32(row.Cells("iddetalle_venta").Value)
                        Dim vdb As New vdetalle_venta
                        Dim func As New fdetalle_venta
                        vdb.giddetalle_venta = onekey

                        vdb.gidproducto = datalistado.SelectedCells.Item(3).Value
                        vdb.gidventa = datalistado.SelectedCells.Item(2).Value
                        vdb.gcantidad = datalistado.SelectedCells.Item(5).Value

                        If func.eliminar(vdb) Then
                            If func.aumentar_stock(vdb) Then

                            End If
                        Else
                            MessageBox.Show("Articulos   eliminado", "Eliminando Articulos", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    End If
                Next
                Call mostrar()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Else
            MessageBox.Show("Cancelando eliminacion de Articulos", "Eliminando Articulos", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Call mostrar()
        End If
        Call limpiar()
    End Sub

    Private Sub btn_buscarProducto_Click(sender As Object, e As EventArgs) Handles btn_buscarProducto.Click
        frmproducto.txtflag.Text = "1"
        frmproducto.ShowDialog()
    End Sub

    Private Sub txtcantidad_ValueChanged(sender As Object, e As EventArgs) Handles txtcantidad.ValueChanged
        Dim cant As Double
        cant = txtcantidad.Value
        If txtcantidad.Value > txtstock.Value Then
            MessageBox.Show("la cantidad que intenta vender supera stock", "error al vender producto", MessageBoxButtons.OK, MessageBoxIcon.Information)
            BtnGuardar.Visible = 0
            txtcantidad.Value = txtstock.Value
        Else
            BtnGuardar.Visible = 1
        End If

        If txtcantidad.Value = 0 Then
            BtnGuardar.Visible = 0
        Else
            BtnGuardar.Visible = 1
        End If
    End Sub

    Private Sub BtnImprimir_Click(sender As System.Object, e As System.EventArgs) Handles BtnImprimir.Click
        frmreportecomprobante.txtidventa.Text = Me.txtidventa.Text
        frmreportecomprobante.ShowDialog()
    End Sub
End Class