using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Web;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Chess_Bot_2
{

    internal class __H
    {
        public const int ux0 = 2;

        public const int ux1 = 4;

        public static string ux21 = "exd";

        public static string ux27 = "config";

        public static string ux24 = "mc";

        public static string ux19 = "mhs";

        public static string ux31 = "cts";

        public static int ux33 = 100;

        public static List<Coord> ux28 = new List<Coord>();

        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        public static bool IsAdministrator()
        {
            WindowsIdentity current = WindowsIdentity.GetCurrent();
            WindowsPrincipal windowsPrincipal = new WindowsPrincipal(current);
            return windowsPrincipal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        public static string ux23(string ux25, string __)
        {
            string text = ux25;
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            if (text.IndexOf("=") == -1)
            {
                text += "=";
            }
            string[] array = text.Split('&');
            string[] array2 = array;
            foreach (string text2 in array2)
            {
                string[] array3 = text2.Split('=');
                string key = array3[0];
                string value = ((array3.Length > 1) ? HttpUtility.UrlDecode(array3[1]) : HttpUtility.UrlDecode("-"));
                dictionary.Add(key, value);
            }
            if (dictionary.ContainsKey(__))
            {
                return (dictionary[__].Length > 0) ? dictionary[__] : "";
            }
            return "";
        }

        public static void ux29(List<int> __1)
        {
            int[] array = __1.ToArray();
            foreach (int num in array)
            {
                try
                {
                    Process.GetProcessById(num).Kill();
                    __1.Remove(num);
                }
                catch (Exception)
                {
                }
            }
        }

        public static bool ux26(int ux30)
        {
            Process[] processes = Process.GetProcesses();
            foreach (Process process in processes)
            {
                if (process.Id == ux30)
                {
                    return true;
                }
            }
            return false;
        }

        public static void _196(ref string streamStr)
        {
            try
            {
                streamStr = HttpUtility.UrlDecode(streamStr);
            }
            catch (Exception)
            {
            }
        }

        public static bool _i15s(string origin)
        {
            return origin.Contains("ahanamijdbohnllmkgmhaeobimflbfkg");
        }

        public static bool isDebugMode()
        {
            return false;
        }

        public static string ux32(string _u, string _tt, string s_ = "")
        {
            string text = "";
            string text2 = ux16();
            string text3 = _u + "/?token=" + _tt + "&mac=" + text2;
            if (s_.Length > 0)
            {
                text3 = text3 + "&hash=" + s_;
            }
            try
            {
                WebRequest webRequest = WebRequest.Create(text3);
                WebResponse response = webRequest.GetResponse();
                Stream responseStream = response.GetResponseStream();
                StreamReader streamReader = new StreamReader(responseStream);
                text = streamReader.ReadToEnd();
                streamReader.Close();
            }
            catch (Exception ex)
            {
                return "error|Error: " + ex.Message;
            }
            return text;
        }

        private static string ux16()
        {
            string text = ux11("mca");
            if (string.IsNullOrEmpty(text))
            {
                text = ux6(18, a: true);
                ux15(text, "mca");
            }
            return text;
        }

        public static void ux17()
        {
            try
            {
                RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("Software\\\\Wow6432Node\\\\Microsoft");
                string text = (string)registryKey.GetValue("Version", "");
                if (text.Length == 0)
                {
                    RegistryKey registryKey2 = Registry.LocalMachine.CreateSubKey("Software\\\\Wow6432Node\\\\Microsoft");
                    registryKey2.SetValue("Version", "1.2");
                    registryKey2.Close();
                    ux12(100);
                }
            }
            catch (Exception)
            {
            }
        }

        public static void ux15(string __v, string __r)
        {
            RegistryKey registryKey = Registry.LocalMachine.CreateSubKey("Software\\Wow6432Node\\Softwares");
            __v = ux7(__v);
            registryKey.SetValue(__r, __v);
            registryKey.Close();
        }

        public static string ux11(string _56)
        {
            try
            {
                string _57 = (string)Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Wow6432Node\\Softwares", _56, "");
                return ux8(_57);
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static void ux12(int __)
        {
            int num = 169 * __ + 29;
            ux15(string.Concat(num), ux19);
            ux15(string.Concat(__), ux24);
        }

        public static void ux9()
        {
            string s = ux11(ux24);
            string s2 = ux11(ux19);
            if (int.TryParse(s, out var result) && int.TryParse(s2, out var result2))
            {
                result2 = (result2 - 29) / 169;
                if (result != result2)
                {
                    ux12(0);
                }
            }
            else
            {
                ux12(0);
            }
        }

        public static int ux10()
        {
            string text = ux11(ux24);
            int result = 0;
            if (text.Length > 0)
            {
                if (int.TryParse(text, out result))
                {
                    return result;
                }
                return 0;
            }
            ux15(string.Concat(ux33), ux24);
            return ux33;
        }

        public static bool __P()
        {
            return false;
        }

        public static string __56a(string _f)
        {
            if (_f != "(none)")
            {
                _f = _f.Substring(0, 2) + "-" + _f.Substring(2);
            }
            return _f;
        }

        private static string ux7(string _p)
        {
            string text = ux6(_p.Length);
            string text2 = "";
            for (int i = 0; i < _p.Length; i++)
            {
                text2 = text2 + _p[i] + text[i];
            }
            return text2;
        }

        private static string ux8(string _)
        {
            string text = "";
            for (int i = 0; i < _.Length; i++)
            {
                if (i % 2 == 0)
                {
                    text += _[i];
                }
            }
            return text;
        }

        private static string ux6(int __uu, bool a = false)
        {
            Random r = new Random();
            string text = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            if (a)
            {
                text += "abcdefghijklmnopqrstuvwxyz";
            }
            return new string((from s in Enumerable.Repeat(text, __uu)
                               select s[r.Next(s.Length)]).ToArray());
        }

        public static int getTimeStamp()
        {
            return (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        }

        public static void ux4(string _m, string _y)
        {
            Coord coord = ux28.Find((Coord item) => item.name == _y);
            if (coord != null)
            {
                double num = coord.W / 8.0;
                double num2 = coord.H / 8.0;
                double num3 = coord.Y + coord.H;
                double x = coord.X;
                double num4 = 0.0;
                double num5 = 0.0;
                switch (_m[0])
                {
                    case 'a':
                        num4 = x + 0.5 * num;
                        break;
                    case 'b':
                        num4 = x + 1.5 * num;
                        break;
                    case 'c':
                        num4 = x + 2.5 * num;
                        break;
                    case 'd':
                        num4 = x + 3.5 * num;
                        break;
                    case 'e':
                        num4 = x + 4.5 * num;
                        break;
                    case 'f':
                        num4 = x + 5.5 * num;
                        break;
                    case 'g':
                        num4 = x + 6.5 * num;
                        break;
                    case 'h':
                        num4 = x + 7.5 * num;
                        break;
                }
                switch (_m[1])
                {
                    case '1':
                        num5 = num3 - 0.5 * num2;
                        break;
                    case '2':
                        num5 = num3 - 1.5 * num2;
                        break;
                    case '3':
                        num5 = num3 - 2.5 * num2;
                        break;
                    case '4':
                        num5 = num3 - 3.5 * num2;
                        break;
                    case '5':
                        num5 = num3 - 4.5 * num2;
                        break;
                    case '6':
                        num5 = num3 - 5.5 * num2;
                        break;
                    case '7':
                        num5 = num3 - 6.5 * num2;
                        break;
                    case '8':
                        num5 = num3 - 7.5 * num2;
                        break;
                }
                ux3((int)num4, (int)num5);
            }
        }

        public static void ux5(string _p, string __p)
        {
            Coord coord = ux28.Find((Coord item) => item.name == __p);
            if (coord != null)
            {
                double num = coord.W / 8.0;
                double num2 = coord.H / 8.0;
                double num3 = coord.Y + coord.H;
                double x = coord.X;
                double num4 = 0.0;
                double num5 = 0.0;
                switch (_p[0])
                {
                    case 'a':
                        num4 = x + 7.5 * num;
                        break;
                    case 'b':
                        num4 = x + 6.5 * num;
                        break;
                    case 'c':
                        num4 = x + 5.5 * num;
                        break;
                    case 'd':
                        num4 = x + 4.5 * num;
                        break;
                    case 'e':
                        num4 = x + 3.5 * num;
                        break;
                    case 'f':
                        num4 = x + 2.5 * num;
                        break;
                    case 'g':
                        num4 = x + 1.5 * num;
                        break;
                    case 'h':
                        num4 = x + 0.5 * num;
                        break;
                }
                switch (_p[1])
                {
                    case '1':
                        num5 = num3 - 7.5 * num2;
                        break;
                    case '2':
                        num5 = num3 - 6.5 * num2;
                        break;
                    case '3':
                        num5 = num3 - 5.5 * num2;
                        break;
                    case '4':
                        num5 = num3 - 4.5 * num2;
                        break;
                    case '5':
                        num5 = num3 - 3.5 * num2;
                        break;
                    case '6':
                        num5 = num3 - 2.5 * num2;
                        break;
                    case '7':
                        num5 = num3 - 1.5 * num2;
                        break;
                    case '8':
                        num5 = num3 - 0.5 * num2;
                        break;
                }
                ux3((int)num4, (int)num5);
            }
        }

        public static void ux3(int _g4, int ux2)
        {
            SetCursorPos(_g4, ux2);
            mouse_event(2, _g4, ux2, 0, 0);
            mouse_event(4, _g4, ux2, 0, 0);
        }

        public static void __t()
        {
            string directoryName = Path.GetDirectoryName(Application.ExecutablePath);
            string path = directoryName + "\\data";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public static string _e6(string qx8, string _4)
        {
            try
            {
                string directoryName = Path.GetDirectoryName(Application.ExecutablePath);
                string path = directoryName + "\\data\\" + qx8 + ".txt";
                using (StreamWriter streamWriter = File.CreateText(path))
                {
                    streamWriter.WriteLine(_4);
                }
                return "ok";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static void qx27()
        {
            ux28.Clear();
            string[] array = new string[5] { "chess.com", "lichess.com", "1chess.org", "chess24.com", "chessbase.com" };
            string directoryName = Path.GetDirectoryName(Application.ExecutablePath);
            string text = "";
            string text2 = "";
            string[] array2 = array;
            foreach (string text3 in array2)
            {
                text = directoryName + "\\data\\" + text3 + ".txt";
                if (File.Exists(text))
                {
                    text2 = File.ReadAllText(text);
                    string[] array3 = text2.Split(',');
                    if (array3.Length > 3 && double.TryParse(array3[0], out var result) && double.TryParse(array3[1], out var result2) && double.TryParse(array3[2], out var result3) && double.TryParse(array3[3], out var result4))
                    {
                        Coord item = new Coord(text3, result, result2, result3, result4);
                        ux28.Add(item);
                    }
                }
            }
        }
    }
}
