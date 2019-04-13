<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form esegue l'override del metodo Dispose per pulire l'elenco dei componenti.
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

    'Richiesto da Progettazione Windows Form
    Private components As System.ComponentModel.IContainer

    'NOTA: la procedura che segue è richiesta da Progettazione Windows Form
    'Può essere modificata in Progettazione Windows Form.  
    'Non modificarla mediante l'editor del codice.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.lstImmagini = New System.Windows.Forms.ListBox()
        Me.lblQuante = New System.Windows.Forms.Label()
        Me.picImmagine = New System.Windows.Forms.PictureBox()
        Me.cmdImpostaDir = New System.Windows.Forms.Button()
        Me.lblDirectory = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.optOra = New System.Windows.Forms.RadioButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.optRandom = New System.Windows.Forms.RadioButton()
        Me.optSequenziale = New System.Windows.Forms.RadioButton()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.lblMinuti = New System.Windows.Forms.Label()
        Me.cmdPiuSecondi = New System.Windows.Forms.Button()
        Me.cmdMenoSecondi = New System.Windows.Forms.Button()
        Me.lblSecondi = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.chkCornice = New System.Windows.Forms.CheckBox()
        Me.optNormale = New System.Windows.Forms.RadioButton()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.optAdatta = New System.Windows.Forms.RadioButton()
        Me.optTuttoSchermo = New System.Windows.Forms.RadioButton()
        Me.cmdRefresh = New System.Windows.Forms.Button()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.cmdPausa = New System.Windows.Forms.Button()
        Me.lblSecondiAlCambio = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmdNasconde = New System.Windows.Forms.Button()
        Me.cmdChiude = New System.Windows.Forms.Button()
        Me.cmdDelete = New System.Windows.Forms.Button()
        CType(Me.picImmagine, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 1000
        '
        'lstImmagini
        '
        Me.lstImmagini.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lstImmagini.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstImmagini.FormattingEnabled = True
        Me.lstImmagini.ItemHeight = 15
        Me.lstImmagini.Location = New System.Drawing.Point(12, 29)
        Me.lstImmagini.Name = "lstImmagini"
        Me.lstImmagini.Size = New System.Drawing.Size(242, 152)
        Me.lstImmagini.TabIndex = 0
        '
        'lblQuante
        '
        Me.lblQuante.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblQuante.Location = New System.Drawing.Point(12, 9)
        Me.lblQuante.Name = "lblQuante"
        Me.lblQuante.Size = New System.Drawing.Size(199, 17)
        Me.lblQuante.TabIndex = 1
        Me.lblQuante.Text = "Label1"
        '
        'picImmagine
        '
        Me.picImmagine.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.picImmagine.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picImmagine.Location = New System.Drawing.Point(270, 29)
        Me.picImmagine.Name = "picImmagine"
        Me.picImmagine.Size = New System.Drawing.Size(275, 150)
        Me.picImmagine.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picImmagine.TabIndex = 2
        Me.picImmagine.TabStop = False
        Me.picImmagine.Visible = False
        '
        'cmdImpostaDir
        '
        Me.cmdImpostaDir.Location = New System.Drawing.Point(470, 194)
        Me.cmdImpostaDir.Name = "cmdImpostaDir"
        Me.cmdImpostaDir.Size = New System.Drawing.Size(32, 23)
        Me.cmdImpostaDir.TabIndex = 3
        Me.cmdImpostaDir.Text = "..."
        Me.cmdImpostaDir.UseVisualStyleBackColor = True
        '
        'lblDirectory
        '
        Me.lblDirectory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDirectory.Font = New System.Drawing.Font("Arial", 10.0!)
        Me.lblDirectory.Location = New System.Drawing.Point(12, 194)
        Me.lblDirectory.Name = "lblDirectory"
        Me.lblDirectory.Size = New System.Drawing.Size(452, 23)
        Me.lblDirectory.TabIndex = 4
        Me.lblDirectory.Text = "Label1"
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.optOra)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.optRandom)
        Me.Panel1.Controls.Add(Me.optSequenziale)
        Me.Panel1.Location = New System.Drawing.Point(12, 229)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(102, 96)
        Me.Panel1.TabIndex = 7
        '
        'optOra
        '
        Me.optOra.AutoSize = True
        Me.optOra.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optOra.Location = New System.Drawing.Point(6, 72)
        Me.optOra.Name = "optOra"
        Me.optOra.Size = New System.Drawing.Size(66, 19)
        Me.optOra.TabIndex = 10
        Me.optOra.TabStop = True
        Me.optOra.Text = "Sull'ora"
        Me.optOra.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(3, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(97, 17)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Avanzamento"
        '
        'optRandom
        '
        Me.optRandom.AutoSize = True
        Me.optRandom.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optRandom.Location = New System.Drawing.Point(6, 49)
        Me.optRandom.Name = "optRandom"
        Me.optRandom.Size = New System.Drawing.Size(73, 19)
        Me.optRandom.TabIndex = 8
        Me.optRandom.TabStop = True
        Me.optRandom.Text = "Random"
        Me.optRandom.UseVisualStyleBackColor = True
        '
        'optSequenziale
        '
        Me.optSequenziale.AutoSize = True
        Me.optSequenziale.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optSequenziale.Location = New System.Drawing.Point(6, 29)
        Me.optSequenziale.Name = "optSequenziale"
        Me.optSequenziale.Size = New System.Drawing.Size(93, 19)
        Me.optSequenziale.TabIndex = 7
        Me.optSequenziale.TabStop = True
        Me.optSequenziale.Text = "Sequenziale"
        Me.optSequenziale.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.lblMinuti)
        Me.Panel2.Controls.Add(Me.cmdPiuSecondi)
        Me.Panel2.Controls.Add(Me.cmdMenoSecondi)
        Me.Panel2.Controls.Add(Me.lblSecondi)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Location = New System.Drawing.Point(120, 229)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(113, 96)
        Me.Panel2.TabIndex = 8
        '
        'lblMinuti
        '
        Me.lblMinuti.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMinuti.Location = New System.Drawing.Point(5, 72)
        Me.lblMinuti.Name = "lblMinuti"
        Me.lblMinuti.Size = New System.Drawing.Size(97, 17)
        Me.lblMinuti.TabIndex = 14
        Me.lblMinuti.Text = "Ritardo"
        '
        'cmdPiuSecondi
        '
        Me.cmdPiuSecondi.Location = New System.Drawing.Point(74, 38)
        Me.cmdPiuSecondi.Name = "cmdPiuSecondi"
        Me.cmdPiuSecondi.Size = New System.Drawing.Size(26, 23)
        Me.cmdPiuSecondi.TabIndex = 13
        Me.cmdPiuSecondi.Text = "+"
        Me.cmdPiuSecondi.UseVisualStyleBackColor = True
        '
        'cmdMenoSecondi
        '
        Me.cmdMenoSecondi.Location = New System.Drawing.Point(8, 38)
        Me.cmdMenoSecondi.Name = "cmdMenoSecondi"
        Me.cmdMenoSecondi.Size = New System.Drawing.Size(26, 23)
        Me.cmdMenoSecondi.TabIndex = 12
        Me.cmdMenoSecondi.Text = "-"
        Me.cmdMenoSecondi.UseVisualStyleBackColor = True
        '
        'lblSecondi
        '
        Me.lblSecondi.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSecondi.Location = New System.Drawing.Point(37, 41)
        Me.lblSecondi.Name = "lblSecondi"
        Me.lblSecondi.Size = New System.Drawing.Size(31, 20)
        Me.lblSecondi.TabIndex = 11
        Me.lblSecondi.Text = "Label1"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(3, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(97, 17)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "Ritardo"
        '
        'Panel3
        '
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.chkCornice)
        Me.Panel3.Controls.Add(Me.optNormale)
        Me.Panel3.Controls.Add(Me.Label3)
        Me.Panel3.Controls.Add(Me.optAdatta)
        Me.Panel3.Controls.Add(Me.optTuttoSchermo)
        Me.Panel3.Location = New System.Drawing.Point(239, 229)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(179, 96)
        Me.Panel3.TabIndex = 9
        '
        'chkCornice
        '
        Me.chkCornice.AutoSize = True
        Me.chkCornice.Font = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCornice.Location = New System.Drawing.Point(102, 72)
        Me.chkCornice.Name = "chkCornice"
        Me.chkCornice.Size = New System.Drawing.Size(64, 20)
        Me.chkCornice.TabIndex = 11
        Me.chkCornice.Text = "Cornice"
        Me.chkCornice.UseVisualStyleBackColor = True
        '
        'optNormale
        '
        Me.optNormale.AutoSize = True
        Me.optNormale.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optNormale.Location = New System.Drawing.Point(6, 72)
        Me.optNormale.Name = "optNormale"
        Me.optNormale.Size = New System.Drawing.Size(73, 19)
        Me.optNormale.TabIndex = 10
        Me.optNormale.TabStop = True
        Me.optNormale.Text = "Normale"
        Me.optNormale.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(3, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(151, 17)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Visualizzazione"
        '
        'optAdatta
        '
        Me.optAdatta.AutoSize = True
        Me.optAdatta.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optAdatta.Location = New System.Drawing.Point(6, 49)
        Me.optAdatta.Name = "optAdatta"
        Me.optAdatta.Size = New System.Drawing.Size(59, 19)
        Me.optAdatta.TabIndex = 8
        Me.optAdatta.TabStop = True
        Me.optAdatta.Text = "Adatta"
        Me.optAdatta.UseVisualStyleBackColor = True
        '
        'optTuttoSchermo
        '
        Me.optTuttoSchermo.AutoSize = True
        Me.optTuttoSchermo.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optTuttoSchermo.Location = New System.Drawing.Point(6, 27)
        Me.optTuttoSchermo.Name = "optTuttoSchermo"
        Me.optTuttoSchermo.Size = New System.Drawing.Size(104, 19)
        Me.optTuttoSchermo.TabIndex = 7
        Me.optTuttoSchermo.TabStop = True
        Me.optTuttoSchermo.Text = "Tutto schermo"
        Me.optTuttoSchermo.UseVisualStyleBackColor = True
        '
        'cmdRefresh
        '
        Me.cmdRefresh.Location = New System.Drawing.Point(508, 194)
        Me.cmdRefresh.Name = "cmdRefresh"
        Me.cmdRefresh.Size = New System.Drawing.Size(37, 23)
        Me.cmdRefresh.TabIndex = 17
        Me.cmdRefresh.Text = "R."
        Me.cmdRefresh.UseVisualStyleBackColor = True
        '
        'Panel4
        '
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Controls.Add(Me.cmdPausa)
        Me.Panel4.Controls.Add(Me.lblSecondiAlCambio)
        Me.Panel4.Controls.Add(Me.Label4)
        Me.Panel4.Location = New System.Drawing.Point(424, 229)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(121, 95)
        Me.Panel4.TabIndex = 18
        '
        'cmdPausa
        '
        Me.cmdPausa.Location = New System.Drawing.Point(79, 66)
        Me.cmdPausa.Name = "cmdPausa"
        Me.cmdPausa.Size = New System.Drawing.Size(37, 23)
        Me.cmdPausa.TabIndex = 19
        Me.cmdPausa.Text = "| |"
        Me.cmdPausa.UseVisualStyleBackColor = True
        '
        'lblSecondiAlCambio
        '
        Me.lblSecondiAlCambio.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSecondiAlCambio.Location = New System.Drawing.Point(-1, 26)
        Me.lblSecondiAlCambio.Name = "lblSecondiAlCambio"
        Me.lblSecondiAlCambio.Size = New System.Drawing.Size(121, 16)
        Me.lblSecondiAlCambio.TabIndex = 18
        Me.lblSecondiAlCambio.Text = "Ritardo"
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(3, 10)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(121, 16)
        Me.Label4.TabIndex = 17
        Me.Label4.Text = "Secondi al cambio:"
        '
        'cmdNasconde
        '
        Me.cmdNasconde.Location = New System.Drawing.Point(465, 3)
        Me.cmdNasconde.Name = "cmdNasconde"
        Me.cmdNasconde.Size = New System.Drawing.Size(37, 23)
        Me.cmdNasconde.TabIndex = 19
        Me.cmdNasconde.Text = "\/"
        Me.cmdNasconde.UseVisualStyleBackColor = True
        '
        'cmdChiude
        '
        Me.cmdChiude.Location = New System.Drawing.Point(508, 3)
        Me.cmdChiude.Name = "cmdChiude"
        Me.cmdChiude.Size = New System.Drawing.Size(37, 23)
        Me.cmdChiude.TabIndex = 20
        Me.cmdChiude.Text = "X"
        Me.cmdChiude.UseVisualStyleBackColor = True
        '
        'cmdDelete
        '
        Me.cmdDelete.Location = New System.Drawing.Point(217, 6)
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.Size = New System.Drawing.Size(37, 23)
        Me.cmdDelete.TabIndex = 21
        Me.cmdDelete.Text = "D"
        Me.cmdDelete.UseVisualStyleBackColor = True
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(552, 336)
        Me.ControlBox = False
        Me.Controls.Add(Me.cmdDelete)
        Me.Controls.Add(Me.cmdChiude)
        Me.Controls.Add(Me.cmdNasconde)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.cmdRefresh)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.lblDirectory)
        Me.Controls.Add(Me.cmdImpostaDir)
        Me.Controls.Add(Me.picImmagine)
        Me.Controls.Add(Me.lblQuante)
        Me.Controls.Add(Me.lstImmagini)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmMain"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cambia la carta .NET"
        CType(Me.picImmagine, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Timer1 As Timer
    Friend WithEvents lstImmagini As ListBox
    Friend WithEvents lblQuante As Label
    Friend WithEvents picImmagine As PictureBox
    Friend WithEvents cmdImpostaDir As Button
    Friend WithEvents lblDirectory As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents optRandom As RadioButton
    Friend WithEvents optSequenziale As RadioButton
    Friend WithEvents Panel2 As Panel
    Friend WithEvents cmdPiuSecondi As Button
    Friend WithEvents cmdMenoSecondi As Button
    Friend WithEvents lblSecondi As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Label3 As Label
    Friend WithEvents optAdatta As RadioButton
    Friend WithEvents optTuttoSchermo As RadioButton
    Friend WithEvents lblMinuti As System.Windows.Forms.Label
    Friend WithEvents optNormale As System.Windows.Forms.RadioButton
    Friend WithEvents cmdRefresh As System.Windows.Forms.Button
    Friend WithEvents optOra As System.Windows.Forms.RadioButton
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents lblSecondiAlCambio As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmdNasconde As System.Windows.Forms.Button
    Friend WithEvents cmdPausa As System.Windows.Forms.Button
    Friend WithEvents cmdChiude As System.Windows.Forms.Button
    Friend WithEvents chkCornice As System.Windows.Forms.CheckBox
    Friend WithEvents cmdDelete As System.Windows.Forms.Button
End Class
