Imports System.Web
Imports System.IO
Imports System.Net
Imports System.Net.Mail
Imports System.Management
Imports System.Drawing.Text
Imports System.Runtime.InteropServices

Public Class frmMain

    '// Author     : DrWeabo
    '// Name       : Email Sender + Email Bomber + Email Spoofer
    '// Contact    : https://github.com/DrWeabo // https://www.DrWeabo.com
    '// Theme      : GunaUI Framework

    '// This script is distributed for educational purposes only & use at own risk.

    Dim CPU As Integer
    Dim RAM As Integer

    Dim MyMailMassage As New MailMessage
    Dim smtpServer As New SmtpClient


    '<------------------ GET YOUR COUNTRY LOCATE ------------------>'

    <DllImport("kernel32.dll")>
    Private Shared Function GetLocaleInfo(ByVal Locale As UInteger, ByVal LCType As UInteger, <Out()> ByVal lpLCData As System.Text.StringBuilder, ByVal cchData As Integer) As Integer
    End Function


    Private Const LOCALE_SYSTEM_DEFAULT As UInteger = &H400
    Private Const LOCALE_SENGCOUNTRY As UInteger = &H1002


    Private Shared Function GetInfo(ByVal lInfo As UInteger) As String

        Dim lpLCData = New System.Text.StringBuilder(256)
        Dim ret As Integer = GetLocaleInfo(LOCALE_SYSTEM_DEFAULT, lInfo, lpLCData, lpLCData.Capacity)
        If ret > 0 Then
            Return lpLCData.ToString().Substring(0, ret - 1)
        End If
        Return String.Empty

    End Function


    Public Shared Function GetLetters()

        Dim MyCountry As String = (GetInfo(LOCALE_SENGCOUNTRY))
        Return MyCountry

    End Function

    '<------------------ GET YOUR COUNTRY LOCATE ------------------>'


    Private Sub consolewrite(ByRef text As String)
        MainTextHISTORY.AppendText(text)
        MainTextHISTORY.AppendText(Environment.NewLine)
    End Sub


    Private Sub MainTimerSENDING_Tick(sender As Object, e As EventArgs) Handles MainTimerSENDING.Tick

        EmailSend()

    End Sub


    Private Sub EmailSend()

        Try

            Dim Sender As String = MainTextUSERNAME.Text
            Dim SenderName As String = MainTextNAMEALIAS.Text
            Dim Password As String = MainTextPASSWORD.Text
            Dim Receiver As String = MainTextTO.Text
            Dim Subject As String = MainTextSUBJECT.Text
            Dim Message As String = MainTextMESSAGE.Text

            MyMailMassage.From = New MailAddress(Sender, SenderName)
            MyMailMassage.To.Add(Receiver)
            MyMailMassage.Subject = Subject
            MyMailMassage.IsBodyHtml = True
            MyMailMassage.Body = Message

            If MainTextATTACHMENT.Text = "" Then
            Else
                Dim Attachment As New System.Net.Mail.Attachment(MainTextATTACHMENT.Text)
                MyMailMassage.Attachments.Add(Attachment)
            End If

            smtpServer.Host = MainTextHOST.Text
            smtpServer.EnableSsl = True
            smtpServer.Port = Convert.ToInt32(MainTextPORT.Text)
            smtpServer.Credentials = New System.Net.NetworkCredential(Sender, Password)
            smtpServer.Send(MyMailMassage)

            MainLabelSEND.Text += 1
            consolewrite("[SYSTEM]" + " [" + DateAndTime.DateString.ToString + "] " + "Messages to -> " + MainLabelSEND.Text + " Sends to " + MainTextTO.Text + " | SMTP Server : " + MainTextHOST.Text + " | Port : " + MainTextPORT.Text)

        Catch ex As Exception

            MainTimerSENDING.Stop()
            consolewrite("[ERROR]" + " [" + DateAndTime.DateString.ToString + "] " + ex.Message)
            MsgBox(ex.Message + ". Error to sent please try again...", MsgBoxStyle.Critical, "Error")

        End Try

    End Sub


    Sub Selected_ComboBox()

        MainComboSMTP.Items.Clear()
        MainComboSMTP.Items.Add("Server Gmail")
        MainComboSMTP.Items.Add("Server Yahoo")
        MainComboSMTP.Items.Add("Server Aol")
        MainComboSMTP.Items.Add("Server Outlook")
        MainComboSMTP.Items.Add("Server Yandex")
        MainComboSMTP.Items.Add("Server Custom")

    End Sub


    Sub Letter_ComboBox()

        If MainComboSMTP.SelectedItem = "Server Gmail" Then
            MainTextHOST.Text = "smtp.gmail.com"
            MainTextPORT.Text = "587"
        ElseIf MainComboSMTP.SelectedItem = "Server Yahoo" Then
            MainTextHOST.Text = "smtp.mail.yahoo.com"
            MainTextPORT.Text = "587"
        ElseIf MainComboSMTP.SelectedItem = "Server Aol" Then
            MainTextHOST.Text = "smtp.aol.com"
            MainTextPORT.Text = "587"
        ElseIf MainComboSMTP.SelectedItem = "Server Outlook" Then
            MainTextHOST.Text = "smtp.live.com"
            MainTextPORT.Text = "587"
        ElseIf MainComboSMTP.SelectedItem = "Server Yandex" Then
            MainTextHOST.Text = "smtp.yandex.com"
            MainTextPORT.Text = "587"
        ElseIf MainComboSMTP.SelectedItem = "Server Custom" Then
            MainTextHOST.Text = "smtp.domain.com"
            MainTextPORT.Text = "587"
        End If

    End Sub


    Private Sub MainButtonSEND_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MainButtonSEND.Click

        MainTimerSENDING.Enabled = True

    End Sub


    Private Sub MainButtonTESTSEND_Click(sender As Object, e As EventArgs) Handles MainButtonTESTSEND.Click

        Try

            Dim Sender2 As String = MainTextUSERNAME.Text
            Dim SenderName As String = MainTextNAMEALIAS.Text
            Dim Password As String = MainTextPASSWORD.Text
            Dim Receiver As String = MainTextTO.Text
            Dim Subject As String = MainTextSUBJECT.Text
            Dim Message As String = MainTextMESSAGE.Text

            MyMailMassage.From = New MailAddress(Sender2, SenderName)
            MyMailMassage.To.Add(Receiver)
            MyMailMassage.Subject = Subject
            MyMailMassage.IsBodyHtml = True
            MyMailMassage.Body = Message

            If MainTextATTACHMENT.Text = "" Then
            Else
                Dim Attachment As New System.Net.Mail.Attachment(MainTextATTACHMENT.Text)
                MyMailMassage.Attachments.Add(Attachment)
            End If

            smtpServer.Host = MainTextHOST.Text
            smtpServer.EnableSsl = True
            smtpServer.Port = Convert.ToInt32(MainTextPORT.Text)
            smtpServer.Credentials = New System.Net.NetworkCredential(Sender2, Password)
            smtpServer.Send(MyMailMassage)

            consolewrite("[SYSTEM]" + " [" + DateAndTime.DateString.ToString + "] " + "Messages to -> " + MainTextTO.Text + " | SMTP Server : " + MainTextHOST.Text + " | Port : " + MainTextPORT.Text)

        Catch ex As Exception

            consolewrite("[ERROR]" + " [" + DateAndTime.DateString.ToString + "] " + ex.Message)
            MsgBox(ex.Message + ". Error to sent please try again...", MsgBoxStyle.Critical, "Error")

        End Try

    End Sub


    Private Sub MainButtonSPOOFING_Click(sender As Object, e As EventArgs) Handles MainButtonSPOOFING.Click

        Try
            'Information
            Dim Target As String = (MainTextTO.Text)
            Dim Subject As String = (MainTextSUBJECT.Text)
            Dim Message As String = (MainTextMESSAGE.Text)
            Dim Amount As String = (MainTextAMOUNT.Text)
            Dim Senders As String = (MainTextUSERNAME.Text)

            'This a bonus free api access
            ' Free API's
            ' http://zeroapi.web1337.net/tools/free/send.php
            ' https://api-spoof.000webhostapp.com/spoofer/send.php
            ' https://tools.DrWeabo.com/api/spoof.php

            'Start
            Dim Send As New System.Net.WebClient
            Dim APIs As String = (MainTextAPI.Text)
            Dim Result As String = Send.DownloadString("https://" + APIs + "?target=" + Target + "&subject=" + Subject + "&message=" + Message + "&amount=" + Amount + "&sender=" + Senders)

            'Write History
            consolewrite("[SYSTEM]" + " [" + DateAndTime.DateString.ToString + "] " + Result)

            'Show messages
            MsgBox(Result, MsgBoxStyle.Information, "")

        Catch ex As Exception

            consolewrite("[ERROR]" + " [" + DateAndTime.DateString.ToString + "] " + ex.Message)
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")

        End Try

    End Sub


    Private Sub MainButtonLOAD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MainButtonLOAD.Click

        MainOpenFile.Title = "Please Select a File"
        MainOpenFile.FileName = " "
        MainOpenFile.InitialDirectory = "C:\"
        MainOpenFile.ShowDialog()

    End Sub


    Private Sub MainOpenFile_FileOk(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MainOpenFile.FileOk

        Dim strm As System.IO.Stream
        strm = MainOpenFile.OpenFile()
        MainTextATTACHMENT.Text = MainOpenFile.FileName.ToString()
        If Not (strm Is Nothing) Then
            strm.Close()
        End If

    End Sub


    Private Sub frmMain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Form Shadow
        Guna.UI.Lib.GraphicsHelper.ShadowForm(Me)
        Guna.UI.Lib.GraphicsHelper.DrawLineShadow(MainPanelCenter, Color.Black, 20, 5, Guna.UI.WinForms.VerHorAlign.HorizontalBottom)

        'Auto Add Font
        Dim Font As New PrivateFontCollection
        Font.AddFontFile("Requirements\Fonts\Archive.ttf")
        MainLabelLogo.Font = New Font(Font.Families(0), 14, FontStyle.Regular)
        MainLabelHome.Font = New Font(Font.Families(0), 23, FontStyle.Regular)
        MainLabelSYSTEM.Font = New Font(Font.Families(0), 14, FontStyle.Regular)
        MainLabelNEWS.Font = New Font(Font.Families(0), 14, FontStyle.Regular)
        MainLabelCPU.Font = New Font(Font.Families(0), 14, FontStyle.Regular)
        MainLabelRAM.Font = New Font(Font.Families(0), 14, FontStyle.Regular)
        MainLabelCPUTEMPERATURE.Font = New Font(Font.Families(0), 14, FontStyle.Regular)

        'Check Internet Connection
        Try
            Using Client = New System.Net.WebClient()
                Using Stream = Client.OpenRead("https://www.google.com")

                    'Start Here
                    MainTextCOMPUTERNAME.Text = My.Computer.Name.ToString
                    MainTextCOMPUTEROS.Text = My.Computer.Info.OSFullName.ToString
                    MainTextMYIP.Text = GetMYIP()
                    MainTextNEWS.Text = GetNEWS()
                    MainTextCOUNTRY.Text = (GetInfo(LOCALE_SENGCOUNTRY))

                    Selected_ComboBox()

                    MainTextPASSWORD.UseSystemPasswordChar = True

                    consolewrite("[SYSTEM]" + " [" + DateAndTime.DateString.ToString + "] " + "Welcome to Maddog - Email Sender !")

                    GetUPDATES()

                    MainLabelAMOUNTSPEED.Text = MainTrackSPEED.Value.ToString() + "%"

                End Using
            End Using
        Catch ex As Exception

            MsgBox(ex.Message & " Your not connected to Internet, please try again & connect to internet!", MsgBoxStyle.Critical, "")
            Application.ExitThread()

        End Try

    End Sub


    Private Sub GetUPDATES()

        'Check for Updates
        Dim Request As System.Net.HttpWebRequest = System.Net.HttpWebRequest.Create("https://paste.drweabo.xyz/paste.php?raw&id=145")
        Dim Response As System.Net.HttpWebResponse = Request.GetResponse()
        Dim Maddog As System.IO.StreamReader = New System.IO.StreamReader(Response.GetResponseStream())
        Dim NewestVersion As String = Maddog.ReadToEnd()
        Dim CurrentVersion As String = Application.ProductVersion

        If NewestVersion.Contains(CurrentVersion) Then

            MainButtonUPDATES.Visible = False
            'Message (You are already using the latest version)
        Else

            consolewrite("[SYSTEM]" + " [" + DateAndTime.DateString.ToString + "] " + "New version available, please download new version.")
            MsgBox("New version available, please download new version.", MsgBoxStyle.Exclamation, "")
            MainButtonUPDATES.Visible = True

        End If

    End Sub


    Function GetMYIP() As String

        Dim IP As New WebClient
        Return IP.DownloadString("https://tools.drweabo.com/api/ip.php")

    End Function


    Function GetNEWS() As String

        Dim INFO As New WebClient
        Return INFO.DownloadString("https://paste.drweabo.xyz/paste.php?raw&id=144")

    End Function


    Private Sub MainComboSMTP_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MainComboSMTP.SelectedIndexChanged

        Letter_ComboBox()

    End Sub


    Private Sub MainCheckSHOWPASS_CheckedChanged(sender As Object, e As EventArgs) Handles MainCheckSHOWPASS.CheckedChanged

        If MainCheckSHOWPASS.Checked = True Then
            MainTextPASSWORD.UseSystemPasswordChar = False
            MainCheckSHOWPASS.Text = "Hide Password"
        Else
            MainTextPASSWORD.UseSystemPasswordChar = True
            MainCheckSHOWPASS.Text = "Show Password"
        End If

    End Sub


    Dim Hitung As Integer = 11


    Private Sub MainTimerSEND_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MainTimerSEND.Tick

        If Hitung > 0 Then
            Hitung = Hitung - 1
            MainLabelNAMEALIAS.Text = Hitung.ToString()
        ElseIf Hitung = 0 Then
            MainTextTO.Text = "-"
            MainTimerSEND.Enabled = False
            Hitung = 11
        End If

    End Sub


    Sub Color_ListView()

        For Each i As ListViewItem In MainListView.Items
            If i.Index Mod 2 = 0 Then
                i.BackColor = Color.FromArgb(41, 133, 211)
            Else
                i.BackColor = Color.FromArgb(255, 255, 255)
            End If
        Next
    End Sub


    Private Sub MainButtonINSERT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MainButtonINSERT.Click

        Dim lvl As New ListViewItem
        lvl.Text = MainListView.Items.Count + 1
        lvl.SubItems.Add(MainTextEMAIL.Text)
        MainListView.Items.Add(lvl)
        lvl = Nothing
        Call Color_ListView()

    End Sub


    Private Sub MainButtonUPLOADEMAIL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MainButtonUPLOADEMAIL.Click

        MainOpenFile.Filter = ("ONLY Text Files (*.txt) | *.txt")
        MainOpenFile.FileName = ""

        If MainOpenFile.ShowDialog = Windows.Forms.DialogResult.OK Then

            Dim filePath As String = MainOpenFile.FileName
            Dim streamReader As New IO.StreamReader(MainOpenFile.FileName)
            While (streamReader.Peek() > -1)
                Dim lvl As New ListViewItem
                lvl.Text = MainListView.Items.Count + 1
                lvl.SubItems.Add(streamReader.ReadLine)
                MainListView.Items.Add(lvl)
                lvl = Nothing
                Call Color_ListView()
            End While
        End If

    End Sub


    Private Sub MainTimerCPU_Tick(sender As Object, e As EventArgs) Handles MainTimerCPU.Tick

        'Timer CPU
        CPU = MainCounterCPU.NextValue()
        RAM = MainCounterRAM.NextValue()

    End Sub


    Private Sub MainTimerRAM_Tick(sender As Object, e As EventArgs) Handles MainTimerRAM.Tick

        'Timer RAM
        If MainGaugeCPU.Value < CPU Then
            MainGaugeCPU.Value += 1
        ElseIf MainGaugeCPU.Value > CPU Then
            MainGaugeCPU.Value -= 1
        End If

        If MainGaugeRAM.Value < RAM Then
            MainGaugeRAM.Value += 1
        ElseIf MainGaugeRAM.Value > RAM Then
            MainGaugeRAM.Value -= 1
        End If

    End Sub


    Private Sub MainTrackSPEED_Scroll(sender As Object, e As ScrollEventArgs) Handles MainTrackSPEED.Scroll

        MainLabelAMOUNTSPEED.Text = MainTrackSPEED.Value.ToString() + "%"

    End Sub


    Private Sub MainTimerCPUTEMPERATURE_Tick(sender As Object, e As EventArgs) Handles MainTimerCPUTEMPERATURE.Tick

        'CPU Temperature
        Try

            Dim searcher As New ManagementObjectSearcher("root\WMI", "SELECT * FROM MSAcpi_ThermalZoneTemperature")
            For Each queryObj As ManagementObject In searcher.Get()
                Dim Temperature As Double = CDbl(queryObj("CurrentTemperature"))
                Temperature = (Temperature - 2732) / 10.0
                MainGaugeCPUTEMPERATURE.Value = Temperature.ToString
            Next

        Catch err As ManagementException

            MsgBox(err.Message & ". You do not have the required administrative privileges to run this program for check temperature CPU.", MsgBoxStyle.Critical)
            Application.ExitThread()

        End Try

    End Sub


    Private Sub MainButtonSAVEEMAIL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MainButtonSAVEEMAIL.Click

        MainSaveFile.Filter = ("ONLY Text Files (*.txt) | *.txt")
        If MainSaveFile.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Dim Write As New IO.StreamWriter(MainSaveFile.FileName)
            Dim HeaderColum As ListView.ColumnHeaderCollection = MainListView.Columns
            For Each lvl As ListViewItem In MainListView.Items
                Dim Save As String = ""
                For i = 0 To lvl.SubItems.Count - 1
                    Save += HeaderColum(i).Text + " :" + lvl.SubItems(i).Text + Space(3)
                Next
                Write.WriteLine(Save)
            Next
            Write.Close()
            MsgBox("Successfully Saved!", MsgBoxStyle.Information)
        End If

    End Sub


    Private Sub MainButtonHOME_Click(sender As Object, e As EventArgs) Handles MainButtonHOME.Click

        MainPanelHome.BringToFront()
        MainPanelHome.Visible = True

    End Sub


    Private Sub MainButtonEMAIL_Click(sender As Object, e As EventArgs) Handles MainButtonEMAIL.Click

        MainPanelEmail.BringToFront()
        MainPanelEmail.Visible = True

    End Sub


    Private Sub MainButtonCONTACT_Click(sender As Object, e As EventArgs) Handles MainButtonCONTACT.Click

        MainPanelContact.BringToFront()
        MainPanelContact.Visible = True

    End Sub


    Private Sub MainButtonHISTORY_Click(sender As Object, e As EventArgs) Handles MainButtonHISTORY.Click

        MainPanelHistory.BringToFront()
        MainPanelHistory.Visible = True

    End Sub


    Private Sub MainButtonSETTINGS_Click(sender As Object, e As EventArgs) Handles MainButtonSETTINGS.Click

        MainPanelSettings.BringToFront()
        MainPanelSettings.Visible = True

    End Sub


    Private Sub MainButtonUPDATES_Click(sender As Object, e As EventArgs) Handles MainButtonUPDATES.Click

        Dim Result As Integer = MessageBox.Show("New version available, please restart and download new version, Download updates now?", "MaddSender | New Updates", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
        If Result = DialogResult.Yes Then
            'Auto Updates
            My.Computer.Network.DownloadFile("https://tools.drweabo.com/releases/maddsend/MaddSend.zip", "MaddSend.zip")

            'Messages when done`
            Dim Result2 As Integer = MessageBox.Show("Successfully, now check on MaddSend.zip, are you wanna open the files?", "MaddSender", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
            If Result2 = DialogResult.Yes Then

                'Auto Start
                Process.Start("MaddSend.zip")

                'Auto Exit Application
                Application.ExitThread()
            End If
        End If

    End Sub


    Private Sub MainButtonCLEARHISTORY_Click(sender As Object, e As EventArgs) Handles MainButtonCLEARHISTORY.Click

        MainTextHISTORY.Clear()

    End Sub


    Private Sub MainButtonSTOP_Click(sender As Object, e As EventArgs) Handles MainButtonSTOP.Click

        MainTimerSENDING.Enabled = False
        MsgBox("Successfully sents " + MainLabelSEND.Text + " to " + MainTextTO.Text, MsgBoxStyle.Information, "")

    End Sub


    Private Sub MainButtonDONATE2_Click(sender As Object, e As EventArgs) Handles MainButtonDONATE2.Click

        Process.Start("IExplore.exe", "https://paypal.me/DrWeabo")

    End Sub


    Private Sub MainButtonYOUTUBE_Click(sender As Object, e As EventArgs) Handles MainButtonYOUTUBE.Click

        Process.Start("IExplore.exe", "https://youtube.com/DrWeabo")

    End Sub


    Private Sub MainButtonGETFREEAPI_Click(sender As Object, e As EventArgs) Handles MainButtonGETFREEAPI.Click

        Process.Start("IExplore.exe", "https://discord.DrWeabo.com")

    End Sub


    Private Sub MainButtonCREATEAPI_Click(sender As Object, e As EventArgs) Handles MainButtonCREATEAPI.Click

        Process.Start("IExplore.exe", "https://paste.drweabo.xyz/paste.php?raw&id=146")

    End Sub

End Class