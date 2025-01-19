using System;
using System.Text;

namespace exos.utils;

internal class Calc
{
	public static string discordHook = "YOUR_WEBHOOK_URL_ENCRYPTED";

	public static string id = "YOUR_TELEGRAMCHATID_ENCRYPTED";

	public static string tokenbot = "YOUR_TELEGRAMBOT_TOKEN_ENCRYPTED";

	public static string encrypt(string text)
	{
		byte[] bytes = Convert.FromBase64String(text);
		byte[] bytes2 = Convert.FromBase64String(Encoding.UTF8.GetString(bytes));
		byte[] bytes3 = Convert.FromBase64String(Encoding.UTF8.GetString(bytes2));
		return Encoding.UTF8.GetString(bytes3);
	}

	public static string returnDiscordHook()
	{
		return encrypt(discordHook);
	}

	public static string returnid()
	{
		return encrypt(id);
	}

	public static string returnToken()
	{
		return encrypt(tokenbot);
	}
}
