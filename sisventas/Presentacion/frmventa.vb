Imports System.ComponentModel
Public Class frmventa
    Private dt As New DataTable
    Private Sub frmventa_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        mostrar()
    End Sub


    Public Sub limpiar()
        BtnGuardar.Visible = True
        Btneditar.Visible = False
        txtidcliente.Text = ""
        txtnombre_cliente.Text = ""

        txtnum_documento.Text = ""
        txtidventa.Text = ""

    End Sub

    Private Sub mostrar()
        Try
            Dim func As New fventa
            dt = func.mostrar
            datalistado.Columns.Item("Eliminar").Visible = False

            If dt.Rows.Count <> 0 Then
                datalistado.DataSource = dt
                txtbuscar.Enabled = True
                datalistado.ColumnHeadersVisible = True
                Inexistente.Visible = False

            Else
                datalistado.DataSource = Nothing
                txtbuscar.Enabled = False
                datalistado.ColumnHeadersVisible = False
                Inexistente.Visible = True
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Btnuevo.Visible = True
        Btneditar.Visible = False

        buscar()

    End Sub


    Private Sub buscar()
        Try
            Dim ds As New DataSet
            ds.Tables.Add(dt.Copy)
            Dim dv As New DataView(ds.Tables(0))


            dv.RowFilter = cbocampo.Text & " like '" & txtbuscar.Text & "%'"

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
    End Sub


    Private Sub Btnuevo_Click(sender As Object, e As EventArgs) Handles Btnuevo.Click
        limpiar()
        mostrar()

    End Sub

    Private Sub BtnGuardar_Click(sender As Object, e As EventArgs) Handles BtnGuardar.Click
        If Me.ValidateChildren = True And txtidcliente.Text <> "" And txtnombre_cliente.Text <> "" And txtnum_documento.Text <> "" And txtnum_documento.Text <> "" Then
            Try
                Dim dts As New vventa
                Dim func As New fventa

                dts.gidcliente = txtidcliente.Text
                dts.gfecha_venta = txtfecha.Text
                dts.gtipo_documento = cbtipo_documento.Text
                dts.gnum_documento = txtnum_documento.Text


                If func.insertar(dts) Then
                    MessageBox.Show("venta registrado correctamente,añadir productos", "Datos guardado", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    mostrar()
                    limpiar()
                    cargar_detalle()
                Else
                    MessageBox.Show("venta NO registrado ", "ERROR guardado", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

    Private Sub datalistado_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles datalistado.CellClick
        txtidventa.Text = datalistado.SelectedCells.Item(1).Value
        txtidcliente.Text = datalistado.SelectedCells.Item(2).Value
        txtnombre_cliente.Text = datalistado.SelectedCells.Item(3).Value

        txtfecha.Text = datalistado.SelectedCells.Item(5).Value
        cbtipo_documento.Text = datalistado.SelectedCells.Item(6).Value
        txtnum_documento.Text = datalistado.SelectedCells.Item(7).Value
        Btneditar.Visible = True
        BtnGuardar.Visible = False
    End Sub

    Private Sub Btneditar_Click(sender As Object, e As EventArgs) Handles Btneditar.Click
        Dim resul As DialogResult
        resul = MessageBox.Show("desea editar los datos de la venta ", "modificando registros", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
        If resul = DialogResult.OK Then

            If Me.ValidateChildren = True And txtidcliente.Text <> "" And txtnum_documento.Text <> "" And txtidventa.Text <> "" Then
                Try
                    Dim dts As New vventa
                    Dim func As New fventa

                    dts.gidventa = txtidventa.Text
                    dts.gidcliente = txtidcliente.Text
                    dts.gfecha_venta = txtfecha.Text
                    dts.gtipo_documento = cbtipo_documento.Text
                    dts.gnum_documento = txtnum_documento.Text


                    If func.editar(dts) Then
                        MessageBox.Show("venta editada correctamente", "Modificando registros", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        mostrar()
                        limpiar()

                    Else
                        MessageBox.Show("venta NO editada ", "Modificando registros", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        mostrar()
                        limpiar()
                    End If

                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            Else
                MessageBox.Show("Falta ingresar datos", "NO Modficado", MessageBoxButtons.OK, MessageBoxIcon.Information)

            End If
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

    Private Sub txtbuscar_TextChanged(sender As Object, e As EventArgs) Handles txtbuscar.TextChanged
        buscar()
    End Sub
    Private Sub cargar_detalle()
        frmdetalle_venta.txtidventa.Text = datalistado.SelectedCells.Item(1).Value
        frmdetalle_venta.txtidcliente.Text = datalistado.SelectedCells.Item(2).Value
        frmdetalle_venta.txtnombre_cliente.Text = datalistado.SelectedCells.Item(3).Value
        frmdetalle_venta.txtfecha.Text = datalistado.SelectedCells.Item(5).Value
        frmdetalle_venta.cbtipo_documento.Text = datalistado.SelectedCells.Item(6).Value
        frmdetalle_venta.txtnum_documento.Text = datalistado.SelectedCells.Item(7).Value

        frmdetalle_venta.ShowDialog()
    End Sub

    Private Sub datalistado_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles datalistado.CellDoubleClick
        cargar_detalle()
    End Sub

    Private Sub Btnbuscar_cliente_Click(sender As Object, e As EventArgs) Handles Btnbuscar_cliente.Click
        frmcliente.txtflag.Text = "1"
        frmcliente.ShowDialog()
    End Sub
End Class