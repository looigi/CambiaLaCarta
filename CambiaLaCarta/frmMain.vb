Imports System.Drawing.Imaging
Imports System.IO

Public Class frmMain
	Private Const SPI_SETDESKWALLPAPER As Integer = &H14
	Private Const SPIF_UPDATEINIFILE As Integer = &H1
	Private Const SPIF_SENDWININICHANGE As Integer = &H2

	Private Declare Auto Function SystemParametersInfo Lib "user32.dll" (ByVal uAction As Integer, ByVal uParam As Integer, ByVal lpvParam As String, ByVal fuWinIni As Integer) As Integer
	Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog = New System.Windows.Forms.FolderBrowserDialog()

	Private NotifyIcon1 As NotifyIcon = New NotifyIcon
	Private contextMenu1 As System.Windows.Forms.ContextMenu = New System.Windows.Forms.ContextMenu
	Friend WithEvents menuItem1 As System.Windows.Forms.MenuItem = New System.Windows.Forms.MenuItem
	Friend WithEvents menuItem2 As System.Windows.Forms.MenuItem = New System.Windows.Forms.MenuItem
	Friend WithEvents menuItem3 As System.Windows.Forms.MenuItem = New System.Windows.Forms.MenuItem

	'Private Sub frmMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
	'    'e.Cancel = True
	'End Sub

	Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		dimeX = My.Computer.Screen.Bounds.Width
		dimeY = My.Computer.Screen.Bounds.Height

		ImpostaIcona()

		Percorso = GetSetting("CambiaLaCarta", "Impostazioni", "Percorso", Application.StartupPath)
		lblSecondi.Text = GetSetting("CambiaLaCarta", "Impostazioni", "Secondi", 300)
		ScriveMinuti()

		Avanzamento = GetSetting("CambiaLaCarta", "Impostazioni", "Avanzamento", 2)
		Visualizzazione = GetSetting("CambiaLaCarta", "Impostazioni", "Visualizzazione", 2)

		NumeroImmagineVisualizzata = GetSetting("CambiaLaCarta", "Impostazioni", "ImmagineVisualizzata", 0)

		Dim Cornice As String = GetSetting("CambiaLaCarta", "Impostazioni", "Cornice", "True")

		If Cornice = "True" Then chkCornice.Checked = True Else chkCornice.Checked = False

		ImpostaOptions()

		lblDirectory.Text = Percorso

		CaricaImmagini()

		frmNomeImmagine.Show()
		frmNomeImmagine.Opacity = 0.5
		frmNomeImmagine.Left = 4
		frmNomeImmagine.Top = 4

		frmNomeImmagine.lblNomeImmagine.Text = GetSetting("CambiaLaCarta", "Impostazioni", "UltimaScritta", "")

		frmNomeImmagine.Width = frmNomeImmagine.lblNomeImmagine.Width + 10

		For i As Integer = 0 To lstImmagini.Items.Count
			If NomeImmagine(i) = NomeImmagine(NumeroImmagineVisualizzata) Then
				lstImmagini.SelectedIndex = i - 1
				Exit For
			End If
		Next

		Me.TopMost = False
	End Sub

	Private Function TornaDataImmagine(NomeFile As String) As Date
		Dim info As New FileInfo(NomeFile)
		Dim Datella(2) As Date
		Datella(0) = info.LastWriteTime
		Datella(1) = info.CreationTime
		Datella(2) = info.LastAccessTime
		Dim AppoData As Date
		For i As Integer = 0 To 2
			For k As Integer = i + 1 To 2
				If Datella(i) > Datella(k) Then
					AppoData = Datella(i)
					Datella(i) = Datella(k)
					Datella(k) = AppoData
				End If
			Next
		Next

		Return Datella(0)
	End Function

	Private Sub ImpostaIcona()
		Me.contextMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() _
					{Me.menuItem1, Me.menuItem2, Me.menuItem3})

		Me.menuItem1.Index = 0
		Me.menuItem1.Text = "A&pre maschera"
		Me.menuItem3.Index = 1
		Me.menuItem3.Text = "C&ambia sfondo ora"
		Me.menuItem2.Index = 2
		Me.menuItem2.Text = "U&scita"

		NotifyIcon1.Icon = New Icon("Images\ComputerDesktop.ICO")
		NotifyIcon1.Text = "Cambia la carta .NET"
		NotifyIcon1.ContextMenu = Me.contextMenu1
		NotifyIcon1.Visible = True

		Me.Left = dimeX + 10
		Me.Top = dimeY + 10
	End Sub

	Private Sub menuItem1_Click(Sender As Object, e As EventArgs) Handles menuItem1.Click
		Me.Left = (dimeX / 2) - (Me.Width / 2)
		Me.Top = (dimeY / 2) - (Me.Height / 2)

		Me.WindowState = FormWindowState.Normal
		Me.TopMost = True
	End Sub

	Private Sub menuItem3_Click(Sender As Object, e As EventArgs) Handles menuItem3.Click
		CambiaSfondo()
	End Sub

	Private Sub menuItem2_Click(Sender As Object, e As EventArgs) Handles menuItem2.Click
		NotifyIcon1.Visible = False
		NotifyIcon1 = Nothing

		End
	End Sub

	Private Sub ImpostaOptions()
		Select Case Avanzamento
			Case 1
				optSequenziale.Checked = True
				optRandom.Checked = False
				optOra.Checked = False
			Case 2
				optSequenziale.Checked = False
				optRandom.Checked = True
				optOra.Checked = False
			Case 3
				optSequenziale.Checked = False
				optRandom.Checked = False
				optOra.Checked = True
		End Select

		Select Case Visualizzazione
			Case 1
				optAdatta.Checked = False
				optTuttoSchermo.Checked = True
				optNormale.Checked = False
			Case 2
				optAdatta.Checked = True
				optTuttoSchermo.Checked = False
				optNormale.Checked = False
			Case 3
				optAdatta.Checked = False
				optTuttoSchermo.Checked = False
				optNormale.Checked = True
		End Select
	End Sub

	Private Sub CaricaImmagini()
		Dim gf As New GestioneFilesDirectory

		gf.ScansionaDirectorySingola(Percorso, "*.JPG;*.JPEG;*.BMP;*.PCX;*.PNG;")

		lstImmagini.Items.Clear()

		QuanteImmagini = gf.RitornaQuantiFilesRilevati
		NomeImmagine = gf.RitornaFilesRilevati
		NomeImmagine = gf.Ordina(NomeImmagine)

		If NomeImmagine Is Nothing = False Then
			For i As Integer = 0 To QuanteImmagini
				If NomeImmagine(i) <> "" Then
					lstImmagini.Items.Add(NomeImmagine(i).Replace(Percorso & "\", ""))
				End If
			Next
		End If

		lblQuante.Text = "Immagini: " & QuanteImmagini

		gf = Nothing

		picImmagine.Visible = False
	End Sub

	Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
		Secondo += 1

		lblSecondiAlCambio.Text = Val(lblSecondi.Text) - Secondo

		If Secondo >= Val(lblSecondi.Text) Then
			CambiaSfondo()
		End If
	End Sub

	Private Sub CambiaSfondo(Optional ImmagineImpostata As Integer = -1)
		Secondo = 0

		Dim Key As Microsoft.Win32.RegistryKey = My.Computer.Registry.CurrentUser.OpenSubKey("Control Panel\Desktop", True)
		Dim WallpaperStyle As Object = 1
		Dim TileWallpaper As Object = 0

		'Key.SetValue("WallpaperStyle", WallpaperStyle)
		'Key.SetValue("TileWallpaper", TileWallpaper)
		'Key.Close()

		If ImmagineImpostata = -1 Then
			If optSequenziale.Checked = True Then
				NumeroImmagineVisualizzata += 1
			Else
				If optRandom.Checked = True Then
					Dim Vecchia As Integer = NumeroImmagineVisualizzata

					Do While NumeroImmagineVisualizzata = Vecchia
						Randomize()
						NumeroImmagineVisualizzata = CInt(Int((QuanteImmagini * Rnd()))) + 1
					Loop
				Else
					Dim Ora As Integer = Now.Hour
					Dim Minuti As Integer = Now.Minute
					Dim Secondi As Integer = Now.Second

					Dim Tot As Integer = (Ora * 60 * 60) + (Minuti * 60) + Secondi
					Tot = Int(Tot / Val(lblSecondi.Text)) * Val(lblSecondi.Text)

					Dim Resto As Integer = Tot Mod QuanteImmagini

					NumeroImmagineVisualizzata = Resto
				End If
			End If
		Else
			NumeroImmagineVisualizzata = ImmagineImpostata
		End If

		Dim Immaginella As String = NomeImmagine(NumeroImmagineVisualizzata)
		Dim NomeOriginale As String = Immaginella

		If File.Exists(Immaginella) = False Then
			Exit Sub
		End If

		Dim gi As New GestioneImmagini

		Dim vDime As String = gi.RitornaDimensioneImmagine(Immaginella)
		Dim Dime() As String = vDime.Split("x")

		If Dime(0) = 0 Or Dime(0) = 0 Then
			Exit Sub
		End If

		SaveSetting("CambiaLaCarta", "Impostazioni", "ImmagineVisualizzata", NumeroImmagineVisualizzata)

		Dim dimeSchermoX = My.Computer.Screen.Bounds.Width
		Dim dimeSchermoY = My.Computer.Screen.Bounds.Height

		If Dime(0) > dimeSchermoX Or Dime(1) > dimeSchermoY Then
			' In caso di dimensioni esageratamente grandi riduce l'immagine
			Dim dX As Integer = Val(Dime(0))
			Dim dY As Integer = Val(Dime(1))

			Dim odX As Integer = dX
			Dim odY As Integer = dY

			Dim sX As Integer = dimeSchermoX - 20
			Dim sY As Integer = dimeSchermoY - 20

			Dim d1 As Single = dX / (sX)
			Dim d2 As Single = dY / (sY)
			Dim PercentualeResize As Single

			If d1 > d2 Then
				PercentualeResize = d1
			Else
				PercentualeResize = d2
			End If

			dX /= PercentualeResize
			dY /= PercentualeResize

			gi.Ridimensiona(Immaginella, Immaginella & ".rsz", dX, dY)

			Kill(Immaginella)
			Rename(Immaginella & ".rsz", Immaginella)
			' In caso di dimensioni esageratamente grandi riduce l'immagine
		End If

		frmNomeImmagine.lblNomeImmagine.Text = NomeOriginale.Replace(Percorso & "\", "") & vbCrLf & "Dimensioni: " & vDime & vbCrLf & "Data: " & TornaDataImmagine(Immaginella) & vbCrLf
		frmNomeImmagine.Width = frmNomeImmagine.lblNomeImmagine.Width + 10

		SaveSetting("CambiaLaCarta", "Impostazioni", "UltimaScritta", frmNomeImmagine.lblNomeImmagine.Text)

		CaricaImmagine(Immaginella)

		If optAdatta.Checked = True Then
			Dim dX As Integer = Val(Dime(0))
			Dim dY As Integer = Val(Dime(1))

			Dim odX As Integer = dX
			Dim odY As Integer = dY

			Dim sX As Integer = dimeX - 20
			Dim sY As Integer = dimeY - 10

			Dim d1 As Single = dX / (sX)
			Dim d2 As Single = dY / (sY)
			Dim PercentualeResize As Single

			If d1 > d2 Then
				PercentualeResize = d1
			Else
				PercentualeResize = d2
			End If

			dX /= PercentualeResize
			dY /= PercentualeResize

			gi.Ridimensiona(Immaginella, Application.StartupPath & "\Appoggio.jpg", dX * 0.9, dY * 0.9)

			If File.Exists(Application.StartupPath & "\Appoggio.jpg") Then
				Immaginella = Application.StartupPath & "\Appoggio.jpg"
			End If
		Else
			If optTuttoSchermo.Checked = True Then
				Try
					Kill(Application.StartupPath & "\Appoggio.jpg")
				Catch ex As Exception

				End Try
				gi.Ridimensiona(Immaginella, Application.StartupPath & "\Appoggio.jpg", dimeX, dimeY)

				If File.Exists(Application.StartupPath & "\Appoggio.jpg") Then
					Immaginella = Application.StartupPath & "\Appoggio.jpg"
				End If
			Else
				' Modalità normale... Non devo toccare niente
			End If
		End If

		If chkCornice.Checked = True Then
			gi.MetteCorniceAImmagine(Immaginella, Application.StartupPath & "\AppoggioCornice.jpg")

			If File.Exists(Application.StartupPath & "\Appoggio.jpg") Then
				Immaginella = Application.StartupPath & "\AppoggioCornice.jpg"
			End If
		End If

		' Titolo
		Dim TitoloScritta1 As String = NomeOriginale.Replace(Percorso & "\", "")
		Dim TitoloScritta2 As String = "Dimensioni: " & vDime & " - Data: " & TornaDataImmagine(Immaginella)
		If TitoloScritta1.Length > 50 Then
			TitoloScritta1 = Mid(TitoloScritta1, 1, 23) & "..." & Mid(TitoloScritta1, TitoloScritta1.Length - 23, 23)
		End If
		If TitoloScritta2.Length > 50 Then
			TitoloScritta2 = Mid(TitoloScritta2, 1, 23) & "..." & Mid(TitoloScritta2, TitoloScritta2.Length - 23, 23)
		End If

		Dim Titolo As String = Application.StartupPath & "\Images\Titolo.png"
		Dim titBitmap As Bitmap = gi.LoadBitmapSenzaLock(Titolo)
		titBitmap = gi.LoadBitmapSenzaLock(Application.StartupPath & "\Images\Titolo.png")
		Dim gr As Graphics = Graphics.FromImage(titBitmap)
		gr.DrawString(TitoloScritta1,
			  New Font("Comic Sans Ms", 34),
			  New SolidBrush(Color.Blue),
			  120, 120)
		gr.DrawString(TitoloScritta2,
			  New Font("Comic Sans Ms", 24),
			  New SolidBrush(Color.Red),
			  140, 175)
		gr.Dispose()
		If File.Exists(Application.StartupPath & "\AppoggioTit.jpg") Then
			File.Delete(Application.StartupPath & "\AppoggioTit.jpg")
		End If
		titBitmap.Save(Application.StartupPath & "\AppoggioTit.png", ImageFormat.Png)
		gi.Ridimensiona(Application.StartupPath & "\AppoggioTit.png", Application.StartupPath & "\AppoggioTit.png", 400, 150)

		Randomize()
		Dim xx As Random = New Random()
		Dim x As Integer = xx.Next(1, 30)

		If x / 3 = Int(x / 3) Then
			Dim b As Bitmap = gi.LoadBitmapSenzaLock(Immaginella)
			Using GraphicsObject As Graphics = Graphics.FromImage(b)
				' Pins
				Dim Pin1 As String = Application.StartupPath & "\Pins\4.png"

				Dim bmpPins As Bitmap = gi.LoadBitmapSenzaLock(Pin1)
				GraphicsObject.DrawImage(bmpPins, 20, 20)

				bmpPins = gi.LoadBitmapSenzaLock(Pin1)
				GraphicsObject.DrawImage(bmpPins, b.Width - bmpPins.Width, 20)

				bmpPins = gi.LoadBitmapSenzaLock(Pin1)
				GraphicsObject.DrawImage(bmpPins, 0, b.Height - bmpPins.Height)

				bmpPins = gi.LoadBitmapSenzaLock(Pin1)
				GraphicsObject.DrawImage(bmpPins, b.Width - bmpPins.Width, b.Height - bmpPins.Height)
			End Using
			File.Delete(Immaginella)
			b.Save(Immaginella)
		End If

		'If x / 4 = Int(x / 4) Then
		Randomize()
		Dim yy As Random = New Random
		Dim y As Integer = yy.Next(1, 35)
		If y > 17 Then
			y = 17 - y
		End If

		gi.RotateImage(Immaginella, y)
		'End If

		Select Case x
			Case 1, 11, 21
				gi.ConverteImmaginInBN(Immaginella, Immaginella & ".BN")
				File.Delete(Immaginella)
				Rename(Immaginella & ".BN", Immaginella)
			Case 2, 12, 22
				gi.ConverteInBlur1(Immaginella)
			Case 3, 13, 23
				gi.ConverteInSeppia(Immaginella)
			Case 4
				gi.ConverteEdge(Immaginella)
			'Case 5
			'	gi.ConverteXRay(Immaginella)
			Case 6, 14, 24
				gi.ConverteInBlur2(Immaginella)
			'Case 7
			'	gi.ConverteInEmboss1(Immaginella)
			'Case 8
			'	gi.ConverteInEmboss2(Immaginella)
			'Case 9
			'	gi.ConverteInPixel(Immaginella)
			Case 10
				gi.ConverteInPointellate(Immaginella)
				'Case 11
				'	gi.ConverteInHighPass1(Immaginella)
				'Case 12
				'	gi.ConverteInHighPass2(Immaginella)
		End Select

		' Dim gf As New GestioneFilesDirectory
		' gf.ScansionaDirectorySingola(Application.StartupPath & "\Sfondi")
		' Dim quanteImmSfondo As Integer = gf.RitornaQuantiFilesRilevati
		' gf = Nothing
		Dim quanteImmSfondo As Integer = 27

		Dim xxx As New Random
		Dim xxxx As Integer = xxx.Next(0, quanteImmSfondo + 3)
		If xxxx <> 0 And xxxx <= quanteImmSfondo Then
			Dim Sfondo As String = Application.StartupPath & "\Sfondi\" & xxxx & ".jpg"
			Try
				MkDir(Application.StartupPath & "\Sfondi\Ridotti\")
			Catch ex As Exception

			End Try
			Dim SfondoRid As String = Application.StartupPath & "\Sfondi\Ridotti\" & System.Net.Dns.GetHostName & "_" & xxxx & ".jpg"
			Dim screenWidth As Integer = Screen.PrimaryScreen.Bounds.Width
			Dim screenHeight As Integer = Screen.PrimaryScreen.Bounds.Height
			If Not File.Exists(SfondoRid) Then
				gi.Ridimensiona(Sfondo, SfondoRid, screenWidth, screenHeight)
			End If
			Dim bitmapSfondo As Bitmap = gi.LoadBitmapSenzaLock(SfondoRid)

			Using GraphicsObject As Graphics = Graphics.FromImage(bitmapSfondo)
				Dim bmp As Bitmap = gi.LoadBitmapSenzaLock(Immaginella)
				' gi.ApplicaOmbraABitmap(bmp, Color.Black, Color.White, GestioneImmagini.ShadowDirections.BOTTOM_RIGHT, 180, 8, 10)
				Dim px As Integer = (screenWidth / 2) - (bmp.Width / 2)
				Dim py As Integer = (screenHeight / 2) - (bmp.Height / 2)
				GraphicsObject.DrawImage(bmp, px, py)

				Dim bmpTitolo As Bitmap = gi.LoadBitmapSenzaLock(Application.StartupPath & "\AppoggioTit.png")
				px = (screenWidth / 2) - (bmpTitolo.Width / 2)
				py = 10
				GraphicsObject.DrawImage(bmpTitolo, px, py)
			End Using
			File.Delete(Immaginella)
			bitmapSfondo.Save(Immaginella)
		End If

		gi = Nothing

		Try
			SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, Immaginella, SPIF_UPDATEINIFILE Or SPIF_SENDWININICHANGE)
		Catch ex As Exception

		End Try

		CaricaImmagini()

		For i As Integer = 0 To lstImmagini.Items.Count
			If NomeImmagine(i) = NomeOriginale Then
				lstImmagini.SelectedIndex = i - 1
				Exit For
			End If
		Next
	End Sub

	Private Sub lstImmagini_DoubleClick(sender As Object, e As EventArgs) Handles lstImmagini.DoubleClick
		Dim Nome As String = Percorso & "\" & lstImmagini.Text
		Dim Quale As Integer

		For i As Integer = 1 To QuanteImmagini
			If NomeImmagine(i).IndexOf(lstImmagini.Text) > -1 Then
				Quale = i
				Exit For
			End If
		Next

		CaricaImmagine(Nome)
		CambiaSfondo(Quale)
	End Sub

	Private Sub lstImmagini_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstImmagini.SelectedIndexChanged
		Dim Nome As String = Percorso & "\" & lstImmagini.Text

		CaricaImmagine(Nome)
	End Sub

	Private Sub CaricaImmagine(Immagine As String)
		Try
			Dim fs As System.IO.FileStream
			fs = New System.IO.FileStream(Immagine,
				 IO.FileMode.Open, IO.FileAccess.Read)
			picImmagine.Image = System.Drawing.Image.FromStream(fs)
			fs.Close()
			fs = Nothing

			Dim gi As New GestioneImmagini

			Dim Dime() As String = gi.RitornaDimensioneImmagine(Immagine).Split("x")
			Dim dX As Integer = Val(Dime(0))
			Dim dY As Integer = Val(Dime(1))
			Dim odX As Integer = dX
			Dim odY As Integer = dY

			gi = Nothing

			Dim sX As Integer = 300
			Dim sY As Integer = 170

			Dim d1 As Single = dX / (sX)
			Dim d2 As Single = dY / (sY)
			Dim PercentualeResize As Single

			If d1 > d2 Then
				PercentualeResize = d1
			Else
				PercentualeResize = d2
			End If

			dX /= PercentualeResize
			dY /= PercentualeResize

			picImmagine.Width = dX
			picImmagine.Height = dY

			picImmagine.Left = (lstImmagini.Left + lstImmagini.Width) + ((sX / 2) - (dX / 2))
			picImmagine.Top = 10

			picImmagine.Visible = True
		Catch ex As Exception

		End Try
	End Sub

	Private Sub cmdImpostaDir_Click(sender As Object, e As EventArgs) Handles cmdImpostaDir.Click
		FolderBrowserDialog1.ShowDialog()
		If (FolderBrowserDialog1.ShowDialog() = DialogResult.OK) Then
			Percorso = FolderBrowserDialog1.SelectedPath
			lblDirectory.Text = Percorso
			SaveSetting("CambiaLaCarta", "Impostazioni", "Percorso", Percorso)

			CaricaImmagini()
		End If
	End Sub

	Private Sub ScriveMinuti()
		Dim Minuti As Integer = Int(lblSecondi.Text / 60)
		Dim Secondi As Single = (lblSecondi.Text / 60) - Int(lblSecondi.Text / 60)
		If Secondi < 0 Then Secondi = 0
		Secondi *= 60

		lblMinuti.Text = "Minuti: " & Format(Minuti, "00") & ":" & Format(Secondi, "00")
	End Sub

	Private Sub cmdMenoSecondi_Click(sender As Object, e As EventArgs) Handles cmdMenoSecondi.Click
		lblSecondi.Text = Val(lblSecondi.Text) - 10
		ScriveMinuti()
		SaveSetting("CambiaLaCarta", "Impostazioni", "Secondi", lblSecondi.Text)
	End Sub

	Private Sub cmdPiuSecondi_Click(sender As Object, e As EventArgs) Handles cmdPiuSecondi.Click
		lblSecondi.Text = Val(lblSecondi.Text) + 10
		ScriveMinuti()
		SaveSetting("CambiaLaCarta", "Impostazioni", "Secondi", lblSecondi.Text)
	End Sub

	Private Sub optSequenziale_Click(sender As Object, e As EventArgs) Handles optSequenziale.Click
		Avanzamento = 1
		SaveSetting("CambiaLaCarta", "Impostazioni", "Avanzamento", Avanzamento)
		ImpostaOptions()
	End Sub

	Private Sub optRandom_Click(sender As Object, e As EventArgs) Handles optRandom.Click
		Avanzamento = 2
		SaveSetting("CambiaLaCarta", "Impostazioni", "Avanzamento", Avanzamento)
		ImpostaOptions()
	End Sub

	Private Sub optOra_Click(sender As Object, e As EventArgs) Handles optOra.Click
		Avanzamento = 3
		SaveSetting("CambiaLaCarta", "Impostazioni", "Avanzamento", Avanzamento)
		ImpostaOptions()
	End Sub

	Private Sub optTuttoSchermo_Click(sender As Object, e As EventArgs) Handles optTuttoSchermo.Click
		Visualizzazione = 1
		SaveSetting("CambiaLaCarta", "Impostazioni", "Visualizzazione", Visualizzazione)
		ImpostaOptions()
	End Sub

	Private Sub optAdatta_Click(sender As Object, e As EventArgs) Handles optAdatta.Click
		Visualizzazione = 2
		SaveSetting("CambiaLaCarta", "Impostazioni", "Visualizzazione", Visualizzazione)
		ImpostaOptions()
	End Sub

	Private Sub optNormale_Click(sender As Object, e As EventArgs) Handles optNormale.Click
		Visualizzazione = 3
		SaveSetting("CambiaLaCarta", "Impostazioni", "Visualizzazione", Visualizzazione)
		ImpostaOptions()
	End Sub

	Private Sub cmdRefresh_Click(sender As Object, e As EventArgs) Handles cmdRefresh.Click
		CaricaImmagini()
	End Sub

	Private Sub cmdNasconde_Click(sender As Object, e As EventArgs) Handles cmdNasconde.Click
		Me.Left = dimeX + 10
		Me.Top = dimeY + 10

		Me.TopMost = False
	End Sub

	Private Sub cmdPausa_Click(sender As Object, e As EventArgs) Handles cmdPausa.Click
		If Timer1.Enabled = True Then
			Timer1.Enabled = False
			cmdPausa.Text = ">"
		Else
			Timer1.Enabled = True
			cmdPausa.Text = "| |"
		End If
	End Sub

	Private Sub cmdChiude_Click(sender As Object, e As EventArgs) Handles cmdChiude.Click
		NotifyIcon1.Visible = False
		NotifyIcon1 = Nothing

		End
	End Sub

	Private Sub chkCornice_Click(sender As Object, e As EventArgs) Handles chkCornice.Click
		Dim Cosa As String

		Select Case chkCornice.Checked
			Case True
				Cosa = "True"
			Case False
				Cosa = "False"
			Case Else
				Cosa = "True"
		End Select

		SaveSetting("CambiaLaCarta", "Impostazioni", "Cornice", Cosa)
	End Sub

	Private Sub cmdDelete_Click(sender As Object, e As EventArgs) Handles cmdDelete.Click
		If lstImmagini.Text = "" Then
			MsgBox("Selezionare un'immagine", vbInformation)
			Exit Sub
		End If

		Dim Nome As String = Percorso & "\" & lstImmagini.Text

		If MsgBox("Si è sicuri di voler eliminare l'immagine: " & vbCrLf & vbCrLf & lstImmagini.Text, vbYesNo + vbInformation + vbDefaultButton2) = vbYes Then
			Try
				Kill(Nome)
				CaricaImmagini()
				MsgBox("Immagine eliminata", vbInformation)
			Catch ex As Exception

			End Try
		End If
	End Sub
End Class
