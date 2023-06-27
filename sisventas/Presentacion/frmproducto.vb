Imports System.ComponentModel
Public Class frmproducto
    Private dt As New DataTable
    Private Sub frmproducto_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        mostrar()
    End Sub


    Public Sub limpiar()
        BtnGuardar.Visible = True
        Btneditar.Visible = False
        txtnombre.Text = ""
        txtidcategoria.Text = ""
        txtdescripcion.Text = ""
        txtstock.Text = "0"
        txtprecio_compra.Text = "0"
        txtprecio_venta.Text = "0"
        txtidproducto.Text = ""

        imagen.Image = Nothing
        imagen.BackgroundImage = My.Resources.add
        imagen.SizeMode = PictureBoxSizeMode.StretchImage
    End Sub

    Private Sub mostrar()
        Try
            Dim func As New fproducto
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



    Private Sub txtnombre_Validating(sender As Object, e As CancelEventArgs) Handles txtnombre.Validating
        If DirectCast(sender, TextBox).Text.Length > 0 Then
            Me.erroricono.SetError(sender, "")
        Else
            Me.erroricono.SetError(sender, "Ingrese nombre del cliente,dato obligatorio")
        End If
    End Sub










    Private Sub Btnuevo_Click(sender As Object, e As EventArgs) Handles Btnuevo.Click
        limpiar()
        mostrar()

    End Sub

    Private Sub BtnGuardar_Click(sender As Object, e As EventArgs) Handles BtnGuardar.Click
        If Me.ValidateChildren = True And txtnombre.Text <> "" And txtidcategoria.Text <> "" And txtdescripcion.Text <> "" And txtstock.Text <> "" And txtprecio_compra.Text <> "" Then
            Try
                Dim dts As New vproducto
                Dim func As New fproducto

                dts.gnombre = txtnombre.Text
                dts.gidcategoria = txtidcategoria.Text
                dts.gdescripcion = txtdescripcion.Text
                dts.gstock = txtstock.Text
                dts.gprecio_compra = txtprecio_compra.Text
                dts.gprecio_venta = txtprecio_venta.Text
                dts.gfecha_vencimiento = txtfechaVencimiento.Text

                Dim ms As New IO.MemoryStream()
                If Not imagen.Image Is Nothing Then
                    imagen.Image.Save(ms, imagen.Image.RawFormat)
                Else
                    imagen.Image = My.Resources.file_add
                    imagen.Image.Save(ms, imagen.Image.RawFormat)
                End If
                dts.gimagen = ms.GetBuffer

                If func.insertar(dts) Then
                    MessageBox.Show("producto registrado correctamente", "Datos guardado", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    mostrar()
                    limpiar()

                Else
                    MessageBox.Show("producto NO registrado ", "ERROR guardado", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
        txtidproducto.Text = datalistado.SelectedCells.Item(1).Value
        txtidcategoria.Text = datalistado.SelectedCells.Item(2).Value
        txtnom_categoria.Text = datalistado.SelectedCells.Item(3).Value
        txtnombre.Text = datalistado.SelectedCells.Item(4).Value
        txtdescripcion.Text = datalistado.SelectedCells.Item(5).Value
        txtstock.Text = datalistado.SelectedCells.Item(6).Value
        txtprecio_compra.Text = datalistado.SelectedCells.Item(7).Value
        txtprecio_venta.Text = datalistado.SelectedCells.Item(8).Value
        txtfechaVencimiento.Text = datalistado.SelectedCells.Item(9).Value

        imagen.BackgroundImage = Nothing
        Dim b() As Byte = datalistado.SelectedCells.Item(10).Value
        Dim ms As New IO.MemoryStream(b)

        imagen.Image = Image.FromStream(ms)
        imagen.SizeMode = PictureBoxSizeMode.StretchImage


        Btneditar.Visible = True
        BtnGuardar.Visible = False
    End Sub

    Private Sub Btneditar_Click(sender As Object, e As EventArgs) Handles Btneditar.Click
        Dim resul As DialogResult
        resul = MessageBox.Show("desea editar los datos del producto", "modificando registros", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
        If resul = DialogResult.OK Then

            If Me.ValidateChildren = True And txtnombre.Text <> "" And txtidcategoria.Text <> "" And txtdescripcion.Text <> "" And txtstock.Text <> "" And txtprecio_compra.Text <> "" And txtidproducto.Text <> "" Then
                Try
                    Dim dts As New vproducto
                    Dim func As New fproducto

                    dts.gidproducto = txtidproducto.Text
                    dts.gnombre = txtnombre.Text
                    dts.gidcategoria = txtidcategoria.Text
                    dts.gdescripcion = txtdescripcion.Text
                    dts.gstock = txtstock.Text
                    dts.gprecio_compra = txtprecio_compra.Text
                    dts.gprecio_venta = txtprecio_venta.Text
                    dts.gfecha_vencimiento = txtfechaVencimiento.Text

                    Dim ms As New IO.MemoryStream()
                    If Not imagen.Image Is Nothing Then
                        imagen.Image.Save(ms, imagen.Image.RawFormat)
                    Else
                        imagen.Image = My.Resources.file_add
                        imagen.Image.Save(ms, imagen.Image.RawFormat)
                    End If
                    dts.gimagen = ms.GetBuffer

                    If func.editar(dts) Then
                        MessageBox.Show("producto editado correctamente", "Modificando registros", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        mostrar()
                        limpiar()

                    Else
                        MessageBox.Show("producto NO editado ", "Modificando registros", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

    Private Sub BtnEliminar_Click(sender As Object, e As EventArgs) Handles BtnEliminar.Click
        Dim result As DialogResult
        result = MessageBox.Show("relamente desea elimianr los productos seleccioandos?", "Eliminando registros", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)

        If result = DialogResult.OK Then
            Try
                For Each row As DataGridViewRow In datalistado.Rows
                    Dim marcado As Boolean = Convert.ToBoolean(row.Cells("Eliminar").Value)

                    If marcado Then
                        Dim onekey As Integer = Convert.ToInt32(row.Cells("idproducto").Value)
                        Dim vdb As New vproducto
                        Dim func As New fproducto
                        vdb.gidproducto = onekey

                        If func.eliminar(vdb) Then
                        Else
                            MessageBox.Show("producto No eliminado", "Eliminando registros", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    End If
                Next
                Call mostrar()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Else
            MessageBox.Show("Cancelando eliminacion de registros", "Eliminando registros", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Call mostrar()
        End If
        Call limpiar()
    End Sub

    Private Sub BtnEliminar_Validating(sender As Object, e As CancelEventArgs) Handles BtnEliminar.Validating

    End Sub




    Private Sub Btncargar_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Btnlimpiar_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub btnCargar_Click_1(sender As Object, e As EventArgs) Handles btnCargar.Click
        If dlg.ShowDialog() = DialogResult.OK Then
            imagen.BackgroundImage = Nothing
            imagen.Image = New Bitmap(dlg.FileName)
            imagen.SizeMode = PictureBoxSizeMode.StretchImage
        End If
    End Sub

    Private Sub btnLimpiar_Click_1(sender As Object, e As EventArgs)

    End Sub

    Private Sub Btnlimpiar_Click_2(sender As Object, e As EventArgs) Handles Btnlimpiar.Click
        imagen.Image = Nothing
        imagen.BackgroundImage = My.Resources.add
        imagen.SizeMode = PictureBoxSizeMode.StretchImage
    End Sub

    Private Sub Btnbuscarcategoria_Click(sender As Object, e As EventArgs) Handles Btnbuscarcategoria.Click
        frmcategoria.txtflag.Text = "1"
        frmcategoria.ShowDialog()
    End Sub

    Private Sub datalistado_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles datalistado.CellDoubleClick
        If txtflag.Text = "1" Then
            frmdetalle_venta.txtidproducto.Text = datalistado.SelectedCells.Item(1).Value
            frmdetalle_venta.txtnombre_producto.Text = datalistado.SelectedCells.Item(4).Value
            frmdetalle_venta.txtstock.Text = datalistado.SelectedCells.Item(6).Value
            frmdetalle_venta.txtprecio_unitario.Text = datalistado.SelectedCells.Item(8).Value


            Me.Close()

        End If
    End Sub
End Class