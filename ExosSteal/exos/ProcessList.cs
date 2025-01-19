using System;
using System.Diagnostics;
using System.IO;
using System.Management;

namespace exos;

internal class ProcessList
{
	public static void WriteProcesses()
	{
		string exploitDir = Helper.ExploitDir;
		Process[] processes = Process.GetProcesses();
		foreach (Process process in processes)
		{
			File.AppendAllText(exploitDir + "\\ProcessList.txt", "NAME: " + process.ProcessName + "\n\n");
		}
	}

	public static string ProcessExecutablePath(Process process)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			return process.MainModule.FileName;
		}
		catch
		{
			ManagementObjectEnumerator enumerator = new ManagementObjectSearcher("SELECT ExecutablePath, ProcessID FROM Win32_Process").Get().GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					ManagementObject val = (ManagementObject)enumerator.Current;
					object obj = ((ManagementBaseObject)val)["ProcessID"];
					object obj2 = ((ManagementBaseObject)val)["ExecutablePath"];
					if (obj2 != null && obj.ToString() == process.Id.ToString())
					{
						return obj2.ToString();
					}
				}
			}
			finally
			{
				((IDisposable)enumerator)?.Dispose();
			}
		}
		return "";
	}
}
