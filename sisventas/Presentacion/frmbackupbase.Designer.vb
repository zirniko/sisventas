<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmbackupbase
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.BtnBackup = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'BtnBackup
        '
        Me.BtnBackup.BackColor = System.Drawing.Color.Blue
        Me.BtnBackup.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.BtnBackup.Location = New System.Drawing.Point(85, 74)
        Me.BtnBackup.Name = "BtnBackup"
        Me.BtnBackup.Size = New System.Drawing.Size(137, 70)
        Me.BtnBackup.TabIndex = 0
        Me.BtnBackup.Text = "Respaldo de datos"
        Me.BtnBackup.UseVisualStyleBackColor = False
        '
        'frmbackupbase
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(284, 261)
        Me.Controls.Add(Me.BtnBackup)
        Me.Name = "frmbackupbase"
        Me.Text = "Respaldo del sistema"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BtnBackup As System.Windows.Forms.Button
End Class
