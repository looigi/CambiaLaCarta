Imports System.Drawing.Drawing2D
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

	Private quanteImmSfondo As Integer = 0
	Private numPins As Integer = 0

	Private spostamentoOmbraX As Integer = 22
	Private spostamentoOmbraY As Integer = 22

	Private Debug As Boolean = True
	Private nomeFileLog As String = ""

	'Private Sub frmMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
	'    'e.Cancel = True
	'End Sub

	Private Sub ScriveLog(Cosa As String)
		If Debug Then
			Dim gf As New GestioneFilesDirectory
			Dim dt As String = Now.Year & Format(Now.Month, "00") & Format(Now.Day, "00") & " " & Format(Now.Hour, "00") & Format(Now.Hour, "00") & Format(Now.Minute, "00") & Format(Now.Second, "00")
			gf.CreaAggiornaFile(nomeFileLog, dt & " -> " & Cosa)
			gf = Nothing
		End If
	End Sub

	Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		ultimaData = Now.Year & Format(Now.Month, "00") & Format(Now.Day, "00")

		nomeFileLog = Application.StartupPath & "\Log.txt"

		ScriveLog("Apertura")

		dimeX = My.Computer.Screen.Bounds.Width
		dimeY = My.Computer.Screen.Bounds.Height

		ImpostaIcona()

		Percorso = GetSetting("CambiaLaCarta", "Impostazioni", "Percorso", Application.StartupPath)
		lblSecondi.Text = GetSetting("CambiaLaCarta", "Impostazioni", "Secondi", 300)
		ScriveMinuti()

		Avanzamento = GetSetting("CambiaLaCarta", "Impostazioni", "Avanzamento", 2)
		Visualizzazione = GetSetting("CambiaLaCarta", "Impostazioni", "Visualizzazione", 2)

		NumeroImmagineVisualizzata = GetSetting("CambiaLaCarta", "Impostazioni", "ImmagineVisualizzata", 0)

		spostamentoOmbraX = GetSetting("CambiaLaCarta", "Impostazioni", "SpostamentoOmbraX", 20)
		spostamentoOmbraY = GetSetting("CambiaLaCarta", "Impostazioni", "SpostamentoOmbraY", 20)
		lblOmbraX.Text = spostamentoOmbraX
		lblOmbraY.Text = spostamentoOmbraY

		Dim ValoreCheck As String = GetSetting("CambiaLaCarta", "Impostazioni", "Cornice", "True")
		If ValoreCheck = "True" Then chkCornice.Checked = True Else chkCornice.Checked = False
		ValoreCheck = GetSetting("CambiaLaCarta", "Impostazioni", "BN", True)
		If ValoreCheck = "True" Then chkBN.Checked = True Else chkBN.Checked = False
		ValoreCheck = GetSetting("CambiaLaCarta", "Impostazioni", "Seppia", True)
		If ValoreCheck = "True" Then chkSeppia.Checked = True Else chkSeppia.Checked = False
		ValoreCheck = GetSetting("CambiaLaCarta", "Impostazioni", "Pixelate", True)
		If ValoreCheck = "True" Then chkPixelate.Checked = True Else chkPixelate.Checked = False
		ValoreCheck = GetSetting("CambiaLaCarta", "Impostazioni", "NomeImm", True)
		If ValoreCheck = "True" Then chkNomeImm.Checked = True Else chkNomeImm.Checked = False
		ValoreCheck = GetSetting("CambiaLaCarta", "Impostazioni", "Sfondo", True)
		If ValoreCheck = "True" Then chkSfondo.Checked = True Else chkSfondo.Checked = False
		ValoreCheck = GetSetting("CambiaLaCarta", "Impostazioni", "Rotazione", True)
		If ValoreCheck = "True" Then chkRotazione.Checked = True Else chkRotazione.Checked = False
		ValoreCheck = GetSetting("CambiaLaCarta", "Impostazioni", "Pin", True)
		If ValoreCheck = "True" Then chkPin.Checked = True Else chkPin.Checked = False
		ValoreCheck = GetSetting("CambiaLaCarta", "Impostazioni", "Ombra", True)
		If ValoreCheck = "True" Then chkOmbra.Checked = True Else chkOmbra.Checked = False
		ValoreCheck = GetSetting("CambiaLaCarta", "Impostazioni", "Oggetti", True)
		If ValoreCheck = "True" Then chkOggetti.Checked = True Else chkOggetti.Checked = False
		ValoreCheck = GetSetting("CambiaLaCarta", "Impostazioni", "Blur", True)
		If ValoreCheck = "True" Then chkBlur.Checked = True Else chkBlur.Checked = False
		Dim Valo As String = GetSetting("CambiaLaCarta", "Impostazioni", "SoloOggi", "N")
		chkOggi.Checked = (Valo = "S")

		lblDirectory.Text = Percorso

		ScriveLog("Carica Immagini")

		CaricaImmagini()

		ScriveLog("Imposta Opzioni")

		ImpostaOptions()

		' frmNomeImmagine.Show()
		' frmNomeImmagine.Opacity = 0.5
		' frmNomeImmagine.Left = 4
		' frmNomeImmagine.Top = 4
		' 
		' frmNomeImmagine.lblNomeImmagine.Text = GetSetting("CambiaLaCarta", "Impostazioni", "UltimaScritta", "")
		' 
		' frmNomeImmagine.Width = frmNomeImmagine.lblNomeImmagine.Width + 10

		For i As Integer = 0 To lstImmagini.Items.Count
			If NomeImmagine(i) = NomeImmagine(NumeroImmagineVisualizzata) Then
				lstImmagini.SelectedIndex = i - 1
				Exit For
			End If
		Next

		Dim gf As New GestioneFilesDirectory
		gf.ScansionaDirectorySingola(Application.StartupPath & "\Images\Sfondi")
		quanteImmSfondo = 0
		Dim filettis() As String = gf.RitornaFilesRilevati
		For i As Integer = 1 To gf.RitornaQuantiFilesRilevati
			If filettis(i).ToUpper.Contains(".JPG") And Not filettis(i).ToUpper.Contains("RIDOTTI") Then
				Dim nome As String = gf.TornaNomeFileDaPath(filettis(i))
				nome = nome.Replace(gf.TornaEstensioneFileDaPath(nome), "")
				If quanteImmSfondo < Val(nome) Then
					quanteImmSfondo = Val(nome)
				End If
			End If
		Next
		lblSfondi.Text = "Sfondi: " & quanteImmSfondo

		ScriveLog("Quante imm sfondi: " & quanteImmSfondo)

		' MsgBox(quanteImmSfondo)
		gf.ScansionaDirectorySingola(Application.StartupPath & "\Images\Pins")
		numPins = 0
		filettis = gf.RitornaFilesRilevati
		For i As Integer = 1 To gf.RitornaQuantiFilesRilevati
			If filettis(i).ToUpper.Contains(".PNG") Then
				numPins += 1
			End If
		Next
		gf = Nothing

		ScriveLog("Quante pins: " & numPins)

		Me.Left = dimeX + 1000
		Me.Top = dimeY + 1000

		Me.TopMost = False
		Me.Hide()
	End Sub

	Private Function TornaDataImmagine(NomeFile As String) As Date
		'Dim info As New FileInfo(NomeFile)
		'Dim Datella(2) As Date
		'Datella(0) = info.LastWriteTime
		'Datella(1) = info.CreationTime
		'Datella(2) = info.LastAccessTime
		'Dim AppoData As Date
		'For i As Integer = 0 To 2
		'	For k As Integer = i + 1 To 2
		'		If Datella(i) > Datella(k) Then
		'			AppoData = Datella(i)
		'			Datella(i) = Datella(k)
		'			Datella(k) = AppoData
		'		End If
		'	Next
		'Next

		Return File.GetCreationTime(NomeFile)
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
		Me.Show()
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
		Dim quale As New List(Of String)

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
			Case 4
				optSequenziale.Checked = False
				optRandom.Checked = False
				optOra.Checked = False
		End Select

		If chkOggi.Checked = True And lstOggi.Count > 0 Then
			quale = lstOggi
		Else
			quale = lstTutte
		End If

		lstImmagini.Items.Clear()
		For Each q As String In quale
			lstImmagini.Items.Add(q)
		Next

		lblQuante.Text = "Immagini: " & quale.Count

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

		lstTutte = New List(Of String)
		lstOggi = New List(Of String)

		QuanteImmagini = gf.RitornaQuantiFilesRilevati
		NomeImmagine = gf.RitornaFilesRilevati
		NomeImmagine = gf.Ordina(NomeImmagine)

		If NomeImmagine Is Nothing = False Then
			For i As Integer = 0 To QuanteImmagini
				If NomeImmagine(i) <> "" Then
					lstTutte.Add(NomeImmagine(i).Replace(Percorso & "\", ""))

					If ControllaData(NomeImmagine(i)) Then
						lstOggi.Add(NomeImmagine(i).Replace(Percorso & "\", ""))
					End If
				End If
			Next
		End If

		chkOggi.Visible = True
		If lstOggi.Count = 0 Then
			chkOggi.Visible = False
		Else
			Dim Valo As String = GetSetting("CambiaLaCarta", "Impostazioni", "SoloOggi", "N")
			chkOggi.Checked = (Valo = "S")
		End If

		lblQuante.Text = "Immagini: " & QuanteImmagini

		gf = Nothing

		picImmagine.Visible = False
	End Sub

	Private Function ControllaData(Immagine) As Boolean
		Dim Ritorno As Boolean = False
		Dim oggi As String = Format(Now.Month, "00") & Format(Now.Day, "00")

		'Dim lastWrite As DateTime = File.GetLastWriteTime(Immagine)
		'Dim lw As String = Format(lastWrite.Day, "00") & Format(lastWrite.Month, "00")
		Dim firstWrite As DateTime = File.GetCreationTime(Immagine)
		Dim fw As String = Format(firstWrite.Month, "00") & Format(firstWrite.Day, "00")

		If oggi = fw Or Immagine.contains(oggi) Then
			Ritorno = True
		End If

		Return Ritorno
	End Function

	Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
		Secondo += 1

		lblSecondiAlCambio.Text = Val(lblSecondi.Text) - Secondo

		If Secondo >= Val(lblSecondi.Text) Then
			CambiaSfondo()
		End If

		Dim dd As String = Now.Year & Format(Now.Month, "00") & Format(Now.Day, "00")

		If dd <> ultimaData Then
			ultimaData = dd

			CaricaImmagini()
			ImpostaOptions()
		End If
	End Sub

	Private Sub CambiaSfondo(Optional ImmagineImpostata As Integer = -1)
		Dim rotazioneImm As Integer

		Secondo = 0

		ScriveLog("Cambio Sfondo 1")

		Dim Key As Microsoft.Win32.RegistryKey = My.Computer.Registry.CurrentUser.OpenSubKey("Control Panel\Desktop", True)
		Dim WallpaperStyle As Object = 1
		Dim TileWallpaper As Object = 0

		'Key.SetValue("WallpaperStyle", WallpaperStyle)
		'Key.SetValue("TileWallpaper", TileWallpaper)
		'Key.Close()

		QuanteImmagini = lstImmagini.Items.Count ' - 1

		If ImmagineImpostata = -1 Then
			If optSequenziale.Checked = True Then
				NumeroImmagineVisualizzata += 1
				If NumeroImmagineVisualizzata > QuanteImmagini Then
					NumeroImmagineVisualizzata = 0
				End If
			Else
				If optRandom.Checked = True Then
					Dim Vecchia As Integer = NumeroImmagineVisualizzata

					Do While NumeroImmagineVisualizzata = Vecchia
						Randomize()
						NumeroImmagineVisualizzata = CInt(Int((QuanteImmagini * Rnd()))) '+ 1
					Loop
				Else
					Dim Vecchia As Integer = NumeroImmagineVisualizzata
					'Dim Ora As Integer = Now.Hour
					'Dim Minuti As Integer = Now.Minute
					'Dim Secondi As Integer = Now.Second

					'Dim Tot As Integer = (Ora * 60 * 60) + (Minuti * 60) + Secondi
					'Tot = Int(Tot / Val(lblSecondi.Text)) * Val(lblSecondi.Text)

					'Dim Resto As Integer = Tot Mod QuanteImmagini

					'NumeroImmagineVisualizzata = Resto
					Dim ws As New ServiceReference1.looVFSoapClient

					Try
						Dim Campi1 As String = ws.TornaNumeroImmaginePerSfondo()
						Dim Campi() As String = Campi1.Split(";")
						' MsgBox(Campi1)
						Dim Imma As String = Campi(1).Replace("/", "\")
						NumeroImmagineVisualizzata = -1

						For i As Integer = 1 To QuanteImmagini
							If lstImmagini.Items.Item(i).Contains(Imma) Then
								NumeroImmagineVisualizzata = i
								Exit For
							End If
						Next
						' NumeroImmagineVisualizzata = Val(Campi(0)) + 1

						If Vecchia = NumeroImmagineVisualizzata Then
							Exit Sub
						End If

						If NumeroImmagineVisualizzata = -1 Then
							Dim Vecchia2 As Integer = NumeroImmagineVisualizzata

							Do While NumeroImmagineVisualizzata = Vecchia2
								Randomize()
								NumeroImmagineVisualizzata = CInt(Int((QuanteImmagini * Rnd()))) '+ 1
							Loop
						End If
					Catch ex As Exception
						Dim Vecchia3 As Integer = NumeroImmagineVisualizzata

						Do While NumeroImmagineVisualizzata = Vecchia3
							Randomize()
							NumeroImmagineVisualizzata = CInt(Int((QuanteImmagini * Rnd()))) '+ 1
						Loop
					End Try
				End If
			End If
		Else
			NumeroImmagineVisualizzata = ImmagineImpostata
		End If

		ScriveLog("Cambio Sfondo 2. Numero Immagine: " & NumeroImmagineVisualizzata)

		Dim Immaginella As String = Percorso & "\" & lstImmagini.Items.Item(NumeroImmagineVisualizzata) '  NomeImmagine(NumeroImmagineVisualizzata)
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

		ScriveLog("Cambio Sfondo 3. Dimensioni immagine " & Dime(0) & "x" & Dime(1))

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

			ScriveLog("Cambio Sfondo 4. Immagine ridimensionata " & dX & "x" & dY)
		End If

		' frmNomeImmagine.lblNomeImmagine.Text = NomeOriginale.Replace(Percorso & "\", "") & vbCrLf & "Dimensioni: " & vDime & vbCrLf & "Data: " & TornaDataImmagine(Immaginella) & vbCrLf
		' frmNomeImmagine.Width = frmNomeImmagine.lblNomeImmagine.Width + 10

		Dim UltimaScritta As String = NomeOriginale.Replace(Percorso & "\", "") & vbCrLf & "Dimensioni: " & vDime & vbCrLf & "Data: " & TornaDataImmagine(Immaginella) & vbCrLf

		SaveSetting("CambiaLaCarta", "Impostazioni", "UltimaScritta", UltimaScritta)

		CaricaImmagine(Immaginella)

		ScriveLog("Cambio Sfondo 5. Immagine caricata " & Immaginella)

		Try
			MkDir(Application.StartupPath & "\Images\Sfondi\Ridotti\")
		Catch ex As Exception

		End Try
		Try
			MkDir(Application.StartupPath & "\Images\Appoggio\")
		Catch ex As Exception

		End Try

		ScriveLog("Cambio Sfondo 6. Tolgo orientamento")

		ToglieOrientamentoAImmagine(Immaginella)

		ScriveLog("Cambio Sfondo 7. Tolto orientamento")

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

			gi.Ridimensiona(Immaginella, Application.StartupPath & "\Images\Appoggio\Appoggio.png", dX * 0.9, dY * 0.8)

			ScriveLog("Cambio Sfondo 8. Immagine ridimensionata " & (dX * 0.9) & "x" & (dY * 0.8))

			If File.Exists(Application.StartupPath & "\Images\Appoggio\Appoggio.png") Then
				Immaginella = Application.StartupPath & "\Images\Appoggio\Appoggio.png"
			End If
		Else
			If optTuttoSchermo.Checked = True Then
				Try
					Kill(Application.StartupPath & "\Images\Appoggio\Appoggio.png")
				Catch ex As Exception

				End Try

				gi.Ridimensiona(Immaginella, Application.StartupPath & "\Images\Appoggio\Appoggio.png", dimeX, dimeY)

				ScriveLog("Cambio Sfondo 9. Immagine ridimensionata " & dimeX & "x" & dimeY)

				If File.Exists(Application.StartupPath & "\Images\Appoggio\Appoggio.png") Then
					Immaginella = Application.StartupPath & "\Images\Appoggio\Appoggio.png"
				End If
			Else
				' Modalità normale... Non devo toccare niente
			End If
		End If

		If chkCornice.Checked = True Then
			gi.MetteCorniceAImmagine(Immaginella, Application.StartupPath & "\Images\Appoggio\AppoggioCornice.png")

			ScriveLog("Cambio Sfondo 10. Messa cornice")

			If File.Exists(Application.StartupPath & "\Images\Appoggio\Appoggio.png") Then
				Immaginella = Application.StartupPath & "\Images\Appoggio\AppoggioCornice.png"
			End If
		End If

		'Dim bb As Bitmap = gi.LoadBitmapSenzaLock(Immaginella)
		'gi.ApplicaOmbraABitmap(bb, Color.DarkGray, Color.Transparent, ,,, 3, True)
		'File.Delete(Immaginella)
		'' bb.MakeTransparent(Color.Red)
		'bb.Save(Immaginella, ImageFormat.Png)
		'bb.Dispose()

		' Titolo
		Dim gf As New GestioneFilesDirectory
		Dim TitoloScritta1 As String = gf.TornaNomeFileDaPath(NomeOriginale.Replace(Percorso & "\", ""))
		If TitoloScritta1.Contains(".") Then
			TitoloScritta1 = Mid(TitoloScritta1, 1, TitoloScritta1.IndexOf("."))
		End If
		Dim TitoloScritta2 As String = "Path: " & gf.TornaNomeDirectoryDaPath(NomeOriginale.Replace(Percorso & "\", ""))
		Dim TitoloScritta3 As String = "Dimensioni: " & vDime & " - Data: " & TornaDataImmagine(Immaginella)
		If TitoloScritta1.Length > 34 Then
			TitoloScritta1 = Mid(TitoloScritta1, 1, 16) & "..." & Mid(TitoloScritta1, TitoloScritta1.Length - 16, 16)
		End If
		If TitoloScritta2.Length > 56 Then
			TitoloScritta2 = Mid(TitoloScritta2, 1, 26) & "..." & Mid(TitoloScritta2, TitoloScritta2.Length - 26, 26)
		End If
		If TitoloScritta3.Length > 56 Then
			TitoloScritta3 = Mid(TitoloScritta3, 1, 26) & "..." & Mid(TitoloScritta3, TitoloScritta3.Length - 26, 26)
		End If

		Dim Titolo As String = Application.StartupPath & "\Images\Dettagli\Titolo.png"
		Dim titBitmap As Bitmap = gi.LoadBitmapSenzaLock(Titolo)
		titBitmap = gi.LoadBitmapSenzaLock(Application.StartupPath & "\Images\Dettagli\Titolo.png")
		Dim gr As Graphics = Graphics.FromImage(titBitmap)
		gr.DrawString(TitoloScritta1,
			  New Font("Comic Sans Ms", 34),
			  New SolidBrush(Color.Blue),
			  120, 120)
		gr.DrawString(TitoloScritta2,
			  New Font("Comic Sans Ms", 24),
			  New SolidBrush(Color.Red),
			  140, 175)
		gr.DrawString(TitoloScritta3,
			  New Font("Comic Sans Ms", 24),
			  New SolidBrush(Color.Red),
			  140, 215)
		gr.Dispose()
		If File.Exists(Application.StartupPath & "\Images\Appoggio\AppoggioTit.jpg") Then
			File.Delete(Application.StartupPath & "\Images\Appoggio\AppoggioTit.jpg")
		End If
		titBitmap.MakeTransparent(Color.Black)
		titBitmap.Save(Application.StartupPath & "\Images\Appoggio\AppoggioTit.png", ImageFormat.Png)
		gi.Ridimensiona(Application.StartupPath & "\Images\Appoggio\AppoggioTit.png", Application.StartupPath & "\Images\Appoggio\AppoggioTit.png", dimeX * 0.35, 150, ImageFormat.Png)

		ScriveLog("Cambio Sfondo 10. Immagine ridimensionata e scritta applicata")

		Static yy As Random = New Random
		Randomize()
		Dim y As Integer = yy.Next(0, 14)
		If y > 7 Then
			y = 7 - y
		End If

		If chkRotazione.Checked Then
			gi.RuotaImmagine(Application.StartupPath & "\Images\Appoggio\AppoggioTit.png", y)
		End If
		' Titolo

		Randomize()
		Static xx As Random = New Random()
		Randomize()
		Dim x As Integer = xx.Next(1, 30)

		ScriveLog("Cambio Sfondo 11. Inizio rotazione immagine " & x)

		Dim b As Bitmap = gi.LoadBitmapSenzaLock(Immaginella)
		If chkPin.Checked = True Then
			'x = 3
			If x / 3 = Int(x / 3) Then
				Using GraphicsObject As Graphics = Graphics.FromImage(b)
					Randomize()
					x = xx.Next(1, 3)
					'x = 3
					Select Case x
						Case 1
							' 4 Pins
							Dim bmpOmbraPin As New Bitmap(70, 20)
							Dim flagGraphics As Graphics = Graphics.FromImage(bmpOmbraPin)
							Dim rect As New Rectangle(0, 0, bmpOmbraPin.Width, 20)
							flagGraphics.FillEllipse(New SolidBrush(Color.FromArgb(185, 40, 40, 40)), rect)

							Randomize()
							x = xx.Next(1, numPins)
							Dim Pin1 As String = Application.StartupPath & "\Images\Pins\" & x.ToString.Trim & ".png"

							Dim bmpPins As Bitmap = gi.LoadBitmapSenzaLock(Pin1)
							Dim wh As Integer = 20
							Dim hh As Integer = 20 + (bmpPins.Height - 5)
							If x <> 4 Then
								GraphicsObject.DrawImage(bmpOmbraPin, wh, hh)
							End If
							GraphicsObject.DrawImage(bmpPins, 20, 20)

							Randomize()
							x = xx.Next(1, numPins)
							Pin1 = Application.StartupPath & "\Images\Pins\" & x.ToString.Trim & ".png"
							bmpPins = gi.LoadBitmapSenzaLock(Pin1)
							If x <> 4 Then
								wh = b.Width - bmpPins.Width - 20
								GraphicsObject.DrawImage(bmpOmbraPin, wh, hh)
							End If
							GraphicsObject.DrawImage(bmpPins, b.Width - bmpPins.Width - 20, 20)

							Randomize()
							x = xx.Next(1, numPins)
							Pin1 = Application.StartupPath & "\Images\Pins\" & x.ToString.Trim & ".png"
							bmpPins = gi.LoadBitmapSenzaLock(Pin1)
							If x <> 4 Then
								wh = 20
								hh = b.Height - 25
								GraphicsObject.DrawImage(bmpOmbraPin, wh, hh)
							End If
							GraphicsObject.DrawImage(bmpPins, 20, b.Height - bmpPins.Height - 20)

							Randomize()
							x = xx.Next(1, numPins)
							Pin1 = Application.StartupPath & "\Images\Pins\" & x.ToString.Trim & ".png"
							bmpPins = gi.LoadBitmapSenzaLock(Pin1)
							If x <> 4 Then
								wh = b.Width - bmpPins.Width - 20
								hh = b.Height - 25
								GraphicsObject.DrawImage(bmpOmbraPin, wh, hh)
							End If
							GraphicsObject.DrawImage(bmpPins, b.Width - bmpPins.Width - 20, b.Height - bmpPins.Height - 20)
							' 4 Pins

							ScriveLog("Cambio Sfondo 12. Applicato pin")
						Case 2
							' Graffetta
							Dim Graffetta As String = Application.StartupPath & "\Images\Dettagli\Graffetta.png"
							Dim bmpGraffetta As Bitmap = gi.LoadBitmapSenzaLock(Graffetta)
							GraphicsObject.DrawImage(bmpGraffetta, 30, -5)
							' Graffetta

							ScriveLog("Cambio Sfondo 13. Applicata graffetta")
						Case 3
							' Pin singolo
							Randomize()
							Dim xy = xx.Next(1, numPins)
							Dim Pin1 As String = Application.StartupPath & "\Images\Pins\" & xy.ToString.Trim & ".png"

							Dim bmpPins As Bitmap = gi.LoadBitmapSenzaLock(Pin1)
							Dim w As Integer = (b.Width / 2) - (bmpPins.Width / 2)
							Randomize()
							x = xx.Next(1, 16)
							If x > 8 Then
								x = 8 - x
							End If
							w += x
							Randomize()
							x = xx.Next(1, 8)
							If x > 4 Then
								x = 4 - x
							End If

							If xy <> 4 Then
								Dim bmpOmbraPin As New Bitmap(70, 20)
								Dim flagGraphics As Graphics = Graphics.FromImage(bmpOmbraPin)
								Dim rect As New Rectangle(0, 0, bmpOmbraPin.Width, 20)
								flagGraphics.FillEllipse(New SolidBrush(Color.FromArgb(185, 40, 40, 40)), rect)

								GraphicsObject.DrawImage(bmpOmbraPin, w, x + bmpPins.Height - 10)
							End If

							GraphicsObject.DrawImage(bmpPins, w, x + 3)
							' Pin singolo

							ScriveLog("Cambio Sfondo 14. Applicato pin singolo")
					End Select
				End Using
				File.Delete(Immaginella)
				b.Save(Immaginella)

				ScriveLog("Cambio Sfondo 15. Immagine salvata " & Immaginella)
			End If
		End If

		Dim bmpOmbra As New Bitmap(b.Width, b.Height)
		'Dim flagGraphics As Graphics = Graphics.FromImage(bmpOmbra)
		'Dim c As Color = Color.FromArgb(240, 0, 0, 0)
		'Dim brush As Brush = New SolidBrush(c)
		'' flagGraphics.FillRectangle(brush, 0, 0, b.Width, b.Height)
		'Dim rect As New Rectangle(0, 0, b.Width, b.Height)
		'flagGraphics.FillRectangle(New SolidBrush(Color.FromArgb(40, 0, 0, 255)), rect)
		'' bmpOmbra.MakeTransparent(Color.LawnGreen)
		'bmpOmbra.Save(Application.StartupPath & "\Images\Appoggio\Ombra.png", ImageFormat.Png)
		'bmpOmbra.Dispose()

		Randomize()
		y = yy.Next(1, 35)
		If y > 17 Then
			y = 17 - y
		End If
		rotazioneimm = y

		'If x / 4 = Int(x / 4) Then
		Randomize()
		If chkRotazione.Checked Then
			gi.RuotaImmagine(Immaginella, y)
		End If
		' gi.RuotaImmagine(Application.StartupPath & "\Images\Appoggio\Ombra.png", y)
		'End If

		Select Case x
			Case 1
				If chkBN.Checked Then
					gi.ConverteImmaginInBN(Immaginella, Immaginella & ".BN")
					File.Delete(Immaginella)
					Rename(Immaginella & ".BN", Immaginella)

					ScriveLog("Cambio Sfondo 16. Convertita BN")
				End If
			Case 2, 12, 22
				If chkBlur.Checked Then
					gi.ConverteInBlur1(Immaginella)

					ScriveLog("Cambio Sfondo 17. Convertita BLUR")
				End If
			Case 3, 13, 23
				If chkSeppia.Checked Then
					gi.ConverteInSeppia(Immaginella)

					ScriveLog("Cambio Sfondo 18. Convertita Seppia")
				End If
			Case 4
				' gi.ConverteEdge(Immaginella)
			'Case 5
			'	gi.ConverteXRay(Immaginella)
			Case 6, 14, 24
				If chkBlur.Checked Then
					gi.ConverteInBlur2(Immaginella)

					ScriveLog("Cambio Sfondo 19. Convertita Blur2")
				End If
			'Case 7
			'	gi.ConverteInEmboss1(Immaginella)
			'Case 8
			'	gi.ConverteInEmboss2(Immaginella)
			'Case 9
			'	gi.ConverteInPixel(Immaginella)
			Case 10
				If chkPixelate.Checked Then
					gi.ConverteInPointellate(Immaginella)

					ScriveLog("Cambio Sfondo 20. Convertita POINTELLATE")
				End If
				'Case 11
				'	gi.ConverteInHighPass1(Immaginella)
				'Case 12
				'	gi.ConverteInHighPass2(Immaginella)
		End Select

		Dim Sfondo As String = ""
		Dim SfondoRid As String = ""
		Dim Ancora As Boolean = True
		Dim screenWidth As Integer = Screen.PrimaryScreen.Bounds.Width
		Dim screenHeight As Integer = Screen.PrimaryScreen.Bounds.Height

		Do While Ancora
			Static xxx As New Random()
			Dim xxxx As Integer = xxx.Next(1, quanteImmSfondo)

			'xxxx = 35

			Sfondo = Application.StartupPath & "\Images\Sfondi\" & xxxx & ".jpg"
			SfondoRid = Application.StartupPath & "\Images\Sfondi\Ridotti\" & System.Net.Dns.GetHostName & "_" & xxxx & ".png"

			If Not File.Exists(SfondoRid) Then
				gi.Ridimensiona(Sfondo, SfondoRid, screenWidth, screenHeight)
				If Not File.Exists(SfondoRid) Then
					Ancora = True
				Else
					Ancora = False
				End If
			Else
				Ancora = False
			End If
		Loop

		ScriveLog("Cambio Sfondo 21. Applico sfondo: " & Sfondo)
		ScriveLog("Cambio Sfondo 22. Applico sfondo rid: " & SfondoRid)

		Dim bitmapSfondo As Bitmap
		Dim bmp As Bitmap = gi.LoadBitmapSenzaLock(Immaginella)
		If chkSfondo.Checked Then
			bitmapSfondo = gi.LoadBitmapSenzaLock(SfondoRid)
		Else
			bitmapSfondo = New Bitmap(bmp.Width, bmp.Height)
		End If
		Dim dd As Integer

		Using GraphicsObject As Graphics = Graphics.FromImage(bitmapSfondo)
			' gi.ApplicaOmbraABitmap(bmp, Color.Black, Color.White, GestioneImmagini.ShadowDirections.BOTTOM_RIGHT, 180, 8, 10)

			Dim px As Integer ' = (screenWidth / 2) - (bmp.Width / 2)
			Dim py As Integer ' = (screenHeight / 2) - (bmp.Height / 2)

			If chkOmbra.Checked Then
				Randomize()
				dd = screenWidth - bmp.Width
				If dd <= 0 Then
					dd = 1
				Else
					dd -= 10
					If dd < 1 Then dd = 1
				End If
				y = yy.Next(0, dd)
				px = y
				Randomize()
				dd = screenHeight - bmp.Height
				If dd <= 0 Then
					dd = 1
				Else
					dd -= 10
					If dd < 1 Then dd = 1
				End If
				y = yy.Next(-50, dd + 50)
				py = y

				Dim flagGraphics As Graphics = Graphics.FromImage(bmpOmbra)
				Dim rect As New Rectangle(0, 0, bmpOmbra.Width, bmpOmbra.Height)
				flagGraphics.FillRectangle(New SolidBrush(Color.FromArgb(175, 20, 20, 20)), rect)
				If chkOmbra.Checked Then
					bmpOmbra = gi.RuotaImmagineSenzaSalvare(bmpOmbra, rotazioneImm)
				End If

				GraphicsObject.DrawImage(bmpOmbra, px + spostamentoOmbraX, py + spostamentoOmbraY)
			End If

			GraphicsObject.DrawImage(bmp, px, py)

			ScriveLog("Cambio Sfondo 23")

			If chkOggetti.Checked Then
				Randomize()
				y = yy.Next(1, 3)
				If y = 2 Then
					Randomize()
					y = yy.Next(1, 12)
					Dim bmpOggetto As Bitmap = Nothing
					Dim sOggetto As String = ""
					Select Case y
						Case 1, 2, 3, 4
							sOggetto = Application.StartupPath & "\Images\Dettagli\floppy" & y & ".png"
							bmpOggetto = gi.LoadBitmapSenzaLock(sOggetto)
							py = screenHeight - bmpOggetto.Height
							y = yy.Next(10, bmpOggetto.Height - 10)
							py += y
						Case 5, 6
							sOggetto = Application.StartupPath & "\Images\Dettagli\usb" & (y - 4) & ".png"
							bmpOggetto = gi.LoadBitmapSenzaLock(sOggetto)
							py = 0
							y = yy.Next(10, bmpOggetto.Height - 10)
							py -= y
						Case 7, 8, 9, 10
							sOggetto = Application.StartupPath & "\Images\Dettagli\penna" & (y - 6) & ".png"
							bmpOggetto = gi.LoadBitmapSenzaLock(sOggetto)
							py = 0
							y = yy.Next(-50, screenHeight + 50)
							py -= y
						Case 11, 12
							sOggetto = Application.StartupPath & "\Images\Dettagli\squadretta.png"
							bmpOggetto = gi.LoadBitmapSenzaLock(sOggetto)
							y = yy.Next(1, 20)
							If y > 10 Then y = 10 - y
							bmpOggetto = gi.RuotaImmagineSenzaSalvare(bmpOggetto, y)
							py = 0
							y = yy.Next(10, screenHeight - bmpOggetto.Height - 10)
							py -= y
					End Select

					ScriveLog("Cambio Sfondo 24. Applico oggetto: " & soggetto)

					dd = screenWidth - bmpOggetto.Width
					If dd <= 0 Then
						dd = 1
					Else
						dd -= 10
						If dd < 1 Then dd = 1
					End If
					y = yy.Next(0, dd)
					px = y
					GraphicsObject.DrawImage(bmpOggetto, px, py)
				End If
			End If

			ScriveLog("Cambio Sfondo 25")

			If chkNomeImm.Checked Then
				Dim bmpTitolo As Bitmap = gi.LoadBitmapSenzaLock(Application.StartupPath & "\Images\Appoggio\AppoggioTit.png")
				Randomize()
				dd = screenWidth - bmp.Width
				If dd <= 0 Then
					dd = 1
				Else
					dd -= 10
					If dd < 1 Then dd = 1
				End If
				y = yy.Next(0, dd)
				px = y
				'Randomize()
				'y = yy.Next(0, (screenHeight - bmpTitolo.Height) - 10)
				'py = y
				Randomize()
				y = yy.Next(1, 5)
				If y > 3 Then y = 3 - y
				py = 5 + y
				GraphicsObject.DrawImage(bmpTitolo, px, py)
			End If
		End Using

		ScriveLog("Cambio Sfondo 26. Sfondo da applicare: " & Immaginella)

		File.Delete(Immaginella)
		bitmapSfondo.Save(Immaginella)

		ScriveLog("Cambio Sfondo 27. Imposto sfondo: " & Immaginella)
		'End If

		gi = Nothing

		Try
			SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, Immaginella, SPIF_UPDATEINIFILE Or SPIF_SENDWININICHANGE)
		Catch ex As Exception
			ScriveLog("Cambio Sfondo 28. Errore: " & ex.Message)
		End Try

		ScriveLog("Cambio Sfondo 29")

		CaricaImmagini()

		For i As Integer = 0 To lstImmagini.Items.Count
			If NomeImmagine(i) = NomeOriginale Then
				lstImmagini.SelectedIndex = i - 1
				Exit For
			End If
		Next

		ScriveLog("Cambio Sfondo 30. Uscita")
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

			'picImmagine.Width = dX
			'picImmagine.Height = dY

			'picImmagine.Left = (lstImmagini.Left + lstImmagini.Width) + ((sX / 2) - (dX / 2))
			'picImmagine.Top = 10

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
		Me.Left = dimeX + 1000
		Me.Top = dimeY + 1000

		Me.TopMost = False
		Me.Hide()
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
		SaveSetting("CambiaLaCarta", "Impostazioni", "Cornice", ControllaCheck(chkCornice))
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

	Private Function ControllaCheck(chk As CheckBox) As String
		Dim Cosa As String

		Select Case chkBN.Checked
			Case True
				Cosa = "True"
			Case False
				Cosa = "False"
			Case Else
				Cosa = "True"
		End Select

		Return Cosa
	End Function

	Private Sub chkBN_Click(sender As Object, e As EventArgs) Handles chkBN.Click
		SaveSetting("CambiaLaCarta", "Impostazioni", "BN", ControllaCheck(chkBN))
	End Sub

	Private Sub chkSeppia_Click(sender As Object, e As EventArgs) Handles chkSeppia.Click
		SaveSetting("CambiaLaCarta", "Impostazioni", "Seppia", ControllaCheck(chkSeppia))
	End Sub

	Private Sub chkPixelate_Click(sender As Object, e As EventArgs) Handles chkPixelate.Click
		SaveSetting("CambiaLaCarta", "Impostazioni", "Pixelate", ControllaCheck(chkPixelate))
	End Sub

	Private Sub chkNomeImm_Click(sender As Object, e As EventArgs) Handles chkNomeImm.Click
		SaveSetting("CambiaLaCarta", "Impostazioni", "NomeImm", ControllaCheck(chkNomeImm))
	End Sub

	Private Sub chkSfondo_Click(sender As Object, e As EventArgs) Handles chkSfondo.Click
		SaveSetting("CambiaLaCarta", "Impostazioni", "Sfondo", ControllaCheck(chkSfondo))
	End Sub

	Private Sub chkRotazione_Click(sender As Object, e As EventArgs) Handles chkRotazione.Click
		SaveSetting("CambiaLaCarta", "Impostazioni", "Rotazione", ControllaCheck(chkRotazione))
	End Sub

	Private Sub chkPin_Click(sender As Object, e As EventArgs) Handles chkPin.Click
		SaveSetting("CambiaLaCarta", "Impostazioni", "Pin", ControllaCheck(chkPin))
	End Sub

	Private Sub chkOmbra_Click(sender As Object, e As EventArgs) Handles chkOmbra.Click
		SaveSetting("CambiaLaCarta", "Impostazioni", "Ombra", ControllaCheck(chkOmbra))
	End Sub

	Private Sub chkOggetti_Click(sender As Object, e As EventArgs) Handles chkOggetti.Click
		SaveSetting("CambiaLaCarta", "Impostazioni", "Oggetti", ControllaCheck(chkOggetti))
	End Sub

	Private Sub chkBlur_Click(sender As Object, e As EventArgs) Handles chkBlur.Click
		SaveSetting("CambiaLaCarta", "Impostazioni", "Blur", ControllaCheck(chkBlur))
	End Sub

	Private Sub cmdMenoOmbraX_Click(sender As Object, e As EventArgs) Handles cmdMenoOmbraX.Click
		If spostamentoOmbraX > 0 Then
			spostamentoOmbraX -= 1
			lblOmbraX.Text = spostamentoOmbraX
			SaveSetting("CambiaLaCarta", "Impostazioni", "SpostamentoOmbraX", spostamentoOmbraX)
		End If
	End Sub

	Private Sub cmdPiuOmbraX_Click(sender As Object, e As EventArgs) Handles cmdPiuOmbraX.Click
		spostamentoOmbraX += 1
		lblOmbraX.Text = spostamentoOmbraX
		SaveSetting("CambiaLaCarta", "Impostazioni", "SpostamentoOmbraX", spostamentoOmbraX)
	End Sub

	Private Sub cmdMenoOmbraY_Click(sender As Object, e As EventArgs) Handles cmdMenoOmbraY.Click
		If spostamentoOmbraY > 0 Then
			spostamentoOmbraY -= 1
			lblOmbraY.Text = spostamentoOmbraY
			SaveSetting("CambiaLaCarta", "Impostazioni", "SpostamentoOmbraY", spostamentoOmbraY)
		End If
	End Sub

	Private Sub cmdPiuOmbraY_Click(sender As Object, e As EventArgs) Handles cmdPiuOmbraY.Click
		spostamentoOmbraY += 1
		lblOmbraY.Text = spostamentoOmbraY
		SaveSetting("CambiaLaCarta", "Impostazioni", "SpostamentoOmbraY", spostamentoOmbraY)
	End Sub

	Private Sub ToglieOrientamentoAImmagine(Immagine As String)
		Dim sistemato As Boolean = False

		If File.Exists(Immagine) = True Then
			Dim imm As Bitmap = Image.FromFile(Immagine)
			If imm.PropertyIdList.Contains(274) Then
				imm.RemovePropertyItem(274)
				sistemato = True
			End If
			If imm.PropertyIdList.Contains(20521) Then
				imm.RemovePropertyItem(20521)
				sistemato = True
			End If

			If sistemato Then
				imm.Save(Immagine & ".rot")
				sistemato = True
			End If

			imm.Dispose()
			imm = Nothing

			If sistemato = True Then
				Dim gf As New GestioneFilesDirectory
				gf.EliminaFileFisico(Immagine)
				gf.CopiaFileFisico(Immagine & ".rot", Immagine, True)
				gf.EliminaFileFisico(Immagine & ".rot")
			End If
		End If
	End Sub

	Private Sub chkOggi_Click(sender As Object, e As EventArgs) Handles chkOggi.Click
		Dim valore As String = IIf(chkOggi.Checked = True, "S", "N")

		SaveSetting("CambiaLaCarta", "Impostazioni", "SoloOggi", valore)
		ImpostaOptions()
	End Sub
End Class
