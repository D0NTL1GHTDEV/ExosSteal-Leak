using System;

namespace exos;

internal class GenStrings
{
	public static string Generate()
	{
		string text = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
		string text2 = "";
		Random random = new Random();
		int num = random.Next(0, text.Length);
		for (int i = 0; i < num; i++)
		{
			text2 += text[random.Next(10, text.Length)];
		}
		return text2;
	}

	public static int GenNumbersTo()
	{
		return new Random().Next(11, 99);
	}
}
